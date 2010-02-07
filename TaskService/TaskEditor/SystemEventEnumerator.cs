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
			using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
				return new List<string>(session.GetLogNames()).ToArray();
		}

		public static string[] GetEventSources(string computerName, string log)
		{
			bool isLocal = (string.IsNullOrEmpty(computerName) || computerName == "." || computerName.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase));
			using (EventLogSession session = isLocal ? new EventLogSession() : new EventLogSession(computerName))
				using (EventLogConfiguration ec = new EventLogConfiguration(log, session))
					return new List<string>(ec.ProviderNames).ToArray();
		}
	}
}
