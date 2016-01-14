using System.Collections.Generic;
using Res = TaskSchedulerConfig.Properties.Resources;

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
			ret.Add(TorF(validator.Firewall.Enabled) + "  Firewall Enabled");
			return ret.ToArray();
		}

		public string[] V1()
		{
			var ret = new List<string>();
			ret.Add($"{TorF(validator.UserIsAdmin || validator.UserIsBackupOperator || validator.UserIsServerOperator)}  User is a member of at least one of the required groups");
			ret.Add($"{TorF(validator.V1TaskPathAccess)}  User has access rights to C:\\Windows\\Tasks");
			ret.Add("");
			ret.Add($"{TorF(validator.Firewall.Rules[Firewall.Rule.FileAndPrinterSharing])}  {Res.FileAndPrinterSharingRule}");
			return ret.ToArray();
		}

		public string[] V2()
		{
			var ret = new List<string>();
			ret.Add($"{TorF(validator.Firewall.Rules[Firewall.Rule.RemoteTaskManagement])}  {Res.RemoteTaskManagementRule}");
			return ret.ToArray();
		}

		public static string TorF(bool condition) => condition ? "√" : "X";
	}
}
