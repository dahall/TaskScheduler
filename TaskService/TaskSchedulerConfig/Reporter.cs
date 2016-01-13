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
			ret.Add($"{TorF(validator.UserIsAdmin)}  Administrators");
			ret.Add($"{TorF(validator.UserIsBackupOperator)}  Backup Operators");
			ret.Add($"{TorF(validator.UserIsServerOperator)}  Server Operators");
			return ret.ToArray();
		}

		public string[] V1()
		{
			var ret = new List<string>();
			ret.Add($"{TorF(validator.UserIsAdmin || validator.UserIsBackupOperator || validator.UserIsServerOperator)}  User is a member of at least one of the required groups");
			ret.Add($"{TorF(validator.V1TaskPathAccess)}  User has access rights to C:\\Windows\\Tasks");
			ret.Add("");
			ret.Add(TorF(validator.IsFirewallEnabled) + "  Firewall Enabled");
			foreach (string exc in validator.FirewallConfiguration.Keys)
				ret.Add($"{TorF(validator.FirewallConfiguration[exc])}  {exc}");
			return ret.ToArray();
		}

		public string[] V2()
		{
			var ret = new List<string>();
			ret.Add(TorF(validator.IsFirewallEnabled) + "  Firewall Enabled");
			foreach (string exc in validator.FirewallConfiguration.Keys)
				ret.Add($"{TorF(validator.FirewallConfiguration[exc])}  {exc}");
			return ret.ToArray();
		}

		public static string TorF(bool condition) => condition ? "√" : "X";
	}
}
