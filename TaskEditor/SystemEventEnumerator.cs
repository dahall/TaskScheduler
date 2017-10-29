using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Text;
using ELObj = Microsoft.Win32.TaskScheduler.DropDownCheckListItem;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class SystemEventEnumerator
	{
		private static readonly string[] ForwardLogs = new string[] { "Application", "HardwareEvents", "Setup", "System", "ForwardedEvents" };

		public static List<string> GetEventLogStrings(string computerName)
		{
			try
			{
				var ret = GetEventLogs(computerName).ConvertAll(l => l.LogName);
				ret.Sort(StringComparer.OrdinalIgnoreCase);
				return ret;
			}
			catch {}
			return new List<string>();
		}

		public static object[] GetEventLogDisplayObjects(string computerName, bool filterForTasks = true)
		{
			var ret = new List<ELObj>();
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
				{
					foreach (var s in session.GetLogNames())
					{
						try
						{
							var cfg = new EventLogConfiguration(s, session);
							if (!filterForTasks || IsValidTaskLog(cfg))
								ret.Add(new ELObj(session.GetLogDisplayName(s), s));
						}
						catch (Exception e) { System.Diagnostics.Debug.WriteLine($"Couldn't get display name for event log '{s}': {e.Message}"); }
					}
					ret.Sort();
				}
			}
			catch { }
			return ret.ToArray();
		}

		public static EventLogSession GetEventLogSession(string computerName)
		{
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			return isLocal ? new EventLogSession() : new EventLogSession(computerName);
		}

		public static List<EventLogConfiguration> GetEventLogs(string computerName, bool filterForTasks = true)
		{
			var ret = new List<EventLogConfiguration>();
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
				{
					foreach (var s in session.GetLogNames())
					{
						try
						{
							var cfg = new EventLogConfiguration(s, session);
							if (!filterForTasks || IsValidTaskLog(cfg))
								ret.Add(cfg);
						}
						catch (Exception e) { System.Diagnostics.Debug.WriteLine($"Couldn't get config for event log '{s}': {e.Message}"); }
					}
				}
			}
			catch { }
			return ret;
		}

		private static System.Reflection.PropertyInfo SessionHandlePI = null;

		public static string GetLogDisplayName(this EventLogSession session, string logPath)
		{
			int capacity = 0x200, bufferUsed = 0, error = 0;
			string str = logPath;
			StringBuilder displayName = new StringBuilder(capacity);

			// Get handle
			IntPtr hEvt = IntPtr.Zero;
			if (SessionHandlePI == null)
			{
				Type elhType = session?.GetType().Assembly.GetType("System.Diagnostics.Eventing.Reader.EventLogHandle");
				SessionHandlePI = session?.GetType().GetProperty("Handle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, elhType, Type.EmptyTypes, null);
			}
			object o = SessionHandlePI?.GetValue(session, null);
			if (o != null)
				hEvt = ((SafeHandle)o).DangerousGetHandle();

			if (!EvtIntGetClassicLogDisplayName(hEvt, logPath, 0, 0, capacity, displayName, ref bufferUsed))
			{
				error = Marshal.GetLastWin32Error();
				if (error == 0x7a)
				{
					displayName = new StringBuilder(bufferUsed);
					capacity = bufferUsed;
					error = 0;
					if (!EvtIntGetClassicLogDisplayName(hEvt, logPath, 0, 0, capacity, displayName, ref bufferUsed))
						throw new System.ComponentModel.Win32Exception();
				}
			}

			if ((error == 0) && !string.IsNullOrEmpty(displayName.ToString()))
				str = displayName.ToString();
			return str;
		}

		[DllImport("wevtapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool EvtIntGetClassicLogDisplayName(IntPtr session, [MarshalAs(UnmanagedType.LPWStr)] string logName, int locale, int flags, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder displayName, ref int bufferUsed);

		private static bool IsValidTaskLog(EventLogConfiguration log)
		{
			if (log == null)
				return false;
			bool canFwd = Array.Exists(ForwardLogs, s => string.Equals(s, log.LogName, StringComparison.OrdinalIgnoreCase));
			if (!canFwd && log.IsClassicLog)
				return false;
			else if (IsDirect(log))
				return false;
			return true;
		}

		private static bool IsDirect(EventLogConfiguration log)
		{
			if (log.LogType != EventLogType.Debug)
				return log.LogType == EventLogType.Analytical;
			return true;
		}

		public static List<string> GetEventProviders(string computerName, string log = null, bool getDisplayName = false)
		{
			var ret = new List<string>();
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
				{
					IEnumerable<string> names = null;
					if (string.IsNullOrEmpty(log))
					{
						names = session.GetProviderNames();
					}
					else
					{
						using (EventLogConfiguration ec = new EventLogConfiguration(log, session))
							names = ec.ProviderNames;
					}
					if (names != null)
					{
						ret.AddRange(names);
						ret.Sort(StringComparer.OrdinalIgnoreCase);
						if (getDisplayName)
						{
							for (int i = 0; i < ret.Count; i++)
							{
								string dn = ret[i];
								try
								{
									var md = new ProviderMetadata(ret[i], session, System.Globalization.CultureInfo.CurrentUICulture);
									if (!string.IsNullOrEmpty(md.DisplayName)) dn = md.DisplayName;
									var spl = dn.Split('-');
									dn = spl[spl.Length - 1];
								}
								catch { }
								ret[i] = string.Concat(ret[i], "|", dn);
							}
						}
					}
				}
			}
			catch {}
			return ret;
		}

		public static List<string> GetLogsForProviders(string computerName, IEnumerable<string> providers)
		{
			List<string> ret = new List<string>();
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
				{
					foreach (var item in providers)
					{
						var md = new ProviderMetadata(item, session, System.Globalization.CultureInfo.CurrentUICulture);
						ret.AddRange(new List<EventLogLink>(md.LogLinks).ConvertAll<string>(ll => ll.LogName));
					}
				}
			}
			catch { }
			ret.RemoveDuplicates();
			return ret;
		}

		private static void RemoveDuplicates<T>(this List<T> list)
		{
			list.Sort();
			Int32 index = 0;
			while (index < list.Count - 1)
			{
				if (Equals(list[index], list[index + 1]))
					list.RemoveAt(index);
				else
					index++;
			}
		}

		public static List<KeyValuePair<int, string>> GetEventTasks(string computerName, IEnumerable<string> providers)
		{
			var ret = new Dictionary<int, string>();
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
				{
					foreach (var item in providers)
					{
						var md = new ProviderMetadata(item, session, System.Globalization.CultureInfo.CurrentUICulture);
						foreach (var t in md.Tasks)
							if (!ret.ContainsKey(t.Value))
								ret.Add(t.Value, t.DisplayName);
					}
				}
			}
			catch { }
			return new List<KeyValuePair<int, string>>(ret);
		}

		public static IList<EventKeyword> GetEventKeyword(string computerName, string provider)
		{
			try
			{
				using (EventLogSession session = GetEventLogSession(computerName))
					using (var meta = new ProviderMetadata(provider, session, System.Globalization.CultureInfo.CurrentUICulture))
						return meta.Keywords;
			}
			catch { }
			return null;
		}
	}
}