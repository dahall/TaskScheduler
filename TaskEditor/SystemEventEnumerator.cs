using System;
using System.Collections.Generic;
#if NET_35_OR_GREATER
using System.Diagnostics.Eventing.Reader;
#endif

namespace Microsoft.Win32.TaskScheduler
{
	internal static class SystemEventEnumerator
	{
		public static string[] GetEventLogs(string computerName)
		{
#if NET_35_OR_GREATER
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
#endif
			return new string[0];
		}

		public static string[] GetEventSources(string computerName, string log)
		{
#if NET_35_OR_GREATER
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			try
			{
				using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
					using (EventLogConfiguration ec = new EventLogConfiguration(log, session))
						return new List<string>(ec.ProviderNames).ToArray();
			}
			catch {}
#endif
			return new string[0];
		}
	}
}