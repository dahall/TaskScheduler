using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;

namespace TaskSchedulerConfig
{
	class Validator
	{
		private string svr;
		private WindowsIdentity id;
		private SecurityIdentifier sid;
		private WindowsPrincipal prin;

		public Validator(string svr)
		{
			this.svr = svr;
			oldFw = Environment.OSVersion.Version.Major == 5;
			id = WindowsIdentity.GetCurrent();
			sid = new SecurityIdentifier(id.User.Value);
			prin = new WindowsPrincipal(id);
		}

		public bool UserIsAdmin => prin.IsInRole(WindowsBuiltInRole.Administrator);
		public bool UserIsBackupOperator => prin.IsInRole(WindowsBuiltInRole.BackupOperator);
		public bool UserIsServerOperator => prin.IsInRole(WindowsBuiltInRole.SystemOperator);
		public bool V1TaskPathAccess
		{
			get
			{
				try
				{
					new FileIOPermission(FileIOPermissionAccess.AllAccess, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Tasks")).Demand();
					return true;
				}
				catch { }
				return false;
			}
		}

		public bool IsFirewallEnabled
		{
			get
			{
				if (oldFw)
					return Firewall.LocalPolicy.CurrentProfile.FirewallEnabled;
				else
				{
					const int NET_FW_PROFILE2_DOMAIN = 1;
					const int NET_FW_PROFILE2_PRIVATE = 2;
					const int NET_FW_PROFILE2_PUBLIC = 4;

					bool result = false;
					int CurrentProfiles = Firewall.CurrentProfileTypes;

					// The returned 'CurrentProfiles' bit mask can have more than 1 bit set if multiple profiles are active or current at the same time
					if ((CurrentProfiles & NET_FW_PROFILE2_DOMAIN) != 0 && Firewall.FirewallEnabled(NET_FW_PROFILE2_DOMAIN))
						result = true;
					if ((CurrentProfiles & NET_FW_PROFILE2_PRIVATE) != 0 && Firewall.FirewallEnabled(NET_FW_PROFILE2_PRIVATE))
						result = true;
					if ((CurrentProfiles & NET_FW_PROFILE2_PUBLIC) != 0 && Firewall.FirewallEnabled(NET_FW_PROFILE2_PUBLIC))
						result = true;
					return result;
				}
			}
		}

		private dynamic firewall;
		private bool oldFw;

		private dynamic Firewall
		{
			get
			{
				if (firewall == null)
				{
					Type NetFwMgrType = Type.GetTypeFromProgID(oldFw ? "HNetCfg.FwMgr" : "HNetCfg.FwPolicy2", svr, false);
					firewall = Activator.CreateInstance(NetFwMgrType);
				}
				return firewall;
			}
		}

		public Dictionary<string, bool> FirewallConfiguration
		{
			get
			{
				var ret = new Dictionary<string, bool>();
				if (IsFirewallEnabled)
				{
					if (oldFw)
					{
						const int NET_FW_SERVICE_FILE_AND_PRINT = 0;
						//const int NET_FW_SERVICE_UPNP = 1;
						//const int NET_FW_SERVICE_REMOTE_DESKTOP = 2;
						ret.Add("File and Printer Sharing", Firewall.LocalPolicy.CurrentProfile.Services.Item(NET_FW_SERVICE_FILE_AND_PRINT).Enabled);
					}
					else
					{
						ret.Add("Remote Scheduled Tasks Management", Firewall.IsRuleGroupCurrentlyEnabled("Remote Scheduled Tasks Management"));
					}
				}
				return ret;
			}
		}

		public string User => id.Name;
	}
}
