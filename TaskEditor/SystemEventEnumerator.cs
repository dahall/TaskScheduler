using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class SystemEventEnumerator
	{
		private static readonly string[] ForwardLogs = { "Application", "HardwareEvents", "Setup", "System", "ForwardedEvents" };

		private static PropertyInfo SessionHandlePI;

		public static IList<EventKeyword> GetEventKeyword(string computerName, string provider)
		{
			try
			{
				using (var session = GetEventLogSession(computerName))
				using (var meta = new ProviderMetadata(provider, session, CultureInfo.CurrentUICulture))
					return meta.Keywords;
			}
			catch { }
			return null;
		}

		public static object[] GetEventLogDisplayObjects(string computerName, bool filterForTasks = true)
		{
			var ret = new List<ListControlItem>();
			try
			{
				using (var session = GetEventLogSession(computerName))
				{
					foreach (var s in session.GetLogNames())
					{
						try
						{
							var cfg = new EventLogConfiguration(s, session);
							if (!filterForTasks || IsValidTaskLog(cfg))
								ret.Add(new ListControlItem(session.GetLogDisplayName(s), s));
						}
						catch (Exception e) { Debug.WriteLine($"Couldn't get display name for event log '{s}': {e.Message}"); }
					}
					ret.Sort();
				}
			}
			catch { }
			return ret.ToArray();
		}

		public static List<EventLogConfiguration> GetEventLogs(string computerName, bool filterForTasks = true)
		{
			var ret = new List<EventLogConfiguration>();
			try
			{
				using (var session = GetEventLogSession(computerName))
				{
					foreach (var s in session.GetLogNames())
					{
						try
						{
							var cfg = new EventLogConfiguration(s, session);
							if (!filterForTasks || IsValidTaskLog(cfg))
								ret.Add(cfg);
						}
						catch (Exception e) { Debug.WriteLine($"Couldn't get config for event log '{s}': {e.Message}"); }
					}
				}
			}
			catch { }
			return ret;
		}

		public static EventLogSession GetEventLogSession(string computerName)
		{
			var isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			return isLocal ? new EventLogSession() : new EventLogSession(computerName);
		}

		public static List<string> GetEventLogStrings(string computerName)
		{
			try
			{
				var ret = GetEventLogs(computerName).ConvertAll(l => l.LogName);
				ret.Sort(StringComparer.OrdinalIgnoreCase);
				return ret;
			}
			catch { }
			return new List<string>();
		}

		public static List<string> GetEventProviders(string computerName, string log = null, bool getDisplayName = false)
		{
			var ret = new List<string>();
			try
			{
				using (var session = GetEventLogSession(computerName))
				{
					IEnumerable<string> names;
					if (string.IsNullOrEmpty(log))
					{
						names = session.GetProviderNames();
					}
					else
					{
						using (var ec = new EventLogConfiguration(log, session))
							names = ec.ProviderNames;
					}
					if (names != null)
					{
						ret.AddRange(names);
						ret.Sort(StringComparer.OrdinalIgnoreCase);
						if (getDisplayName)
						{
							for (var i = 0; i < ret.Count; i++)
							{
								var dn = ret[i];
								try
								{
									var md = new ProviderMetadata(ret[i], session, CultureInfo.CurrentUICulture);
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
			catch { }
			return ret;
		}

		public static List<KeyValuePair<int, string>> GetEventTasks(string computerName, IEnumerable<string> providers)
		{
			var ret = new Dictionary<int, string>();
			try
			{
				using (var session = GetEventLogSession(computerName))
				{
					foreach (var item in providers)
					{
						var md = new ProviderMetadata(item, session, CultureInfo.CurrentUICulture);
						foreach (var t in md.Tasks)
							if (!ret.ContainsKey(t.Value))
								ret.Add(t.Value, t.DisplayName);
					}
				}
			}
			catch { }
			return new List<KeyValuePair<int, string>>(ret);
		}

		public static string GetLogDisplayName(this EventLogSession session, string logPath)
		{
			int capacity = 0x200, bufferUsed = 0, error = 0;
			var str = logPath;
			var displayName = new StringBuilder(capacity);

			// Get handle
			var hEvt = IntPtr.Zero;
			if (SessionHandlePI == null)
			{
				var elhType = session?.GetType().Assembly.GetType("System.Diagnostics.Eventing.Reader.EventLogHandle");
				SessionHandlePI = session?.GetType().GetProperty("Handle", BindingFlags.Instance | BindingFlags.NonPublic, null, elhType, Type.EmptyTypes, null);
			}
			var o = SessionHandlePI?.GetValue(session, null);
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
						throw new Win32Exception();
				}
			}

			if ((error == 0) && !string.IsNullOrEmpty(displayName.ToString()))
				str = displayName.ToString();
			return str;
		}

		public static List<string> GetLogsForProviders(string computerName, IEnumerable<string> providers)
		{
			var ret = new List<string>();
			try
			{
				using (var session = GetEventLogSession(computerName))
				{
					foreach (var item in providers)
					{
						var md = new ProviderMetadata(item, session, CultureInfo.CurrentUICulture);
						ret.AddRange(new List<EventLogLink>(md.LogLinks).ConvertAll(ll => ll.LogName));
					}
				}
			}
			catch { }
			ret.RemoveDuplicates();
			return ret;
		}

		[DllImport("wevtapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool EvtIntGetClassicLogDisplayName(IntPtr session, [MarshalAs(UnmanagedType.LPWStr)] string logName, int locale, int flags, int bufferSize, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder displayName, ref int bufferUsed);

		private static bool IsDirect(EventLogConfiguration log)
		{
			if (log.LogType != EventLogType.Debug)
				return log.LogType == EventLogType.Analytical;
			return true;
		}

		private static bool IsValidTaskLog(EventLogConfiguration log)
		{
			if (log == null)
				return false;
			var canFwd = Array.Exists(ForwardLogs, s => string.Equals(s, log.LogName, StringComparison.OrdinalIgnoreCase));
			if (!canFwd && log.IsClassicLog)
				return false;
			if (IsDirect(log))
				return false;
			return true;
		}

		private static void RemoveDuplicates<T>(this List<T> list)
		{
			list.Sort();
			var index = 0;
			while (index < list.Count - 1)
			{
				if (Equals(list[index], list[index + 1]))
					list.RemoveAt(index);
				else
					index++;
			}
		}
	}
}