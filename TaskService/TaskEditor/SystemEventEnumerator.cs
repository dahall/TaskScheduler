using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class SystemEventEnumerator
	{
		public static string[] GetEventLogs(string computerName)
		{
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
				{
					var l = new List<string>(session.GetLogNames());
					l.Sort();
					return l.ToArray();
				}
			}
			catch {}
			return new string[0];
		}

		public static string[] GetEventProviders(string computerName, string log = null, bool getDisplayName = false)
		{
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
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
					if (names != null && getDisplayName)
					{
						var ret = new List<string>(names);
						ret.Sort();
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
						return ret.ToArray();
						//return new List<string>(names).ToArray();
					}
				}
			}
			catch {}
			return new string[0];
		}

		public static List<string> GetLogsForProviders(string computerName, IEnumerable<string> providers)
		{
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			List<string> ret = new List<string>();
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
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
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			var ret = new Dictionary<int, string>();
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
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
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
					using (var meta = new ProviderMetadata(provider, session, System.Globalization.CultureInfo.CurrentUICulture))
						return meta.Keywords;
			}
			catch { }
			return null;
		}
	}
}