using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSchedulerConfig
{
	class Reporter
	{
		Validator validator;

		public Reporter(Validator v)
		{
			validator = v;
		}

		public string[] General()
		{
			var ret = new List<string>();
			ret.Add($"User '{validator.User}' has the following roles:");
			ret.Add($"  Administrators:   {validator.UserIsAdmin}");
			ret.Add($"  Backup Operators: {validator.UserIsBackupOperator}");
			ret.Add($"  Server Operators: {validator.UserIsServerOperator}");
			return ret.ToArray();
		}

		public string[] V1()
		{
			var ret = new List<string>();
			ret.Add($"User is a member of at least one of the required groups: {validator.UserIsAdmin || validator.UserIsBackupOperator || validator.UserIsServerOperator}");
			ret.Add($"User has access rights to C:\\Windows\\Tasks: {validator.V1TaskPathAccess}");
			ret.Add("");
			ret.Add($"Firewall must be enabled and have the following exceptions:");
			ret.Add("  Firewall: " + (validator.IsFirewallEnabled ? "Enabled" : "Disabled"));
			foreach (string exc in validator.FirewallConfiguration.Keys)
				ret.Add($"  {exc}: {validator.FirewallConfiguration[exc]}");
			return ret.ToArray();
		}

		public string[] V2()
		{
			var ret = new List<string>();
			ret.Add($"Firewall must be enabled and have the following exceptions:");
			ret.Add("  Firewall: " + (validator.IsFirewallEnabled ? "Enabled" : "Disabled"));
			foreach (string exc in validator.FirewallConfiguration.Keys)
				ret.Add($"  {exc}: {validator.FirewallConfiguration[exc]}");
			return ret.ToArray();
		}
	}
}
