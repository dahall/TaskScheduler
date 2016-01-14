using System;

namespace TaskSchedulerConfig
{
	class Firewall
	{
		internal static bool OldFirewall = Environment.OSVersion.Version.Major == 5;

		public enum Rule
		{
			FileAndPrinterSharing,
			RemoteTaskManagement
		}

		public Firewall(string server)
		{
			Rules = new RulesContainer(this);
			Type NetFwMgrType = Type.GetTypeFromProgID(OldFirewall ? "HNetCfg.FwMgr" : "HNetCfg.FwPolicy2", server, false);
			Instance = Activator.CreateInstance(NetFwMgrType);
		}

		public bool Enabled
		{
			get
			{
				if (OldFirewall)
					return Instance.LocalPolicy.CurrentProfile.FirewallEnabled;
				else
				{
					const int NET_FW_PROFILE2_DOMAIN = 1;
					const int NET_FW_PROFILE2_PRIVATE = 2;
					const int NET_FW_PROFILE2_PUBLIC = 4;

					bool result = false;
					int CurrentProfiles = Instance.CurrentProfileTypes;

					// The returned 'CurrentProfiles' bit mask can have more than 1 bit set if multiple profiles are active or current at the same time
					if ((CurrentProfiles & NET_FW_PROFILE2_DOMAIN) != 0 && Instance.FirewallEnabled(NET_FW_PROFILE2_DOMAIN))
						result = true;
					if ((CurrentProfiles & NET_FW_PROFILE2_PRIVATE) != 0 && Instance.FirewallEnabled(NET_FW_PROFILE2_PRIVATE))
						result = true;
					if ((CurrentProfiles & NET_FW_PROFILE2_PUBLIC) != 0 && Instance.FirewallEnabled(NET_FW_PROFILE2_PUBLIC))
						result = true;
					return result;
				}
			}
			set
			{
				if (OldFirewall)
				{
					Instance.LocalPolicy.CurrentProfile.FirewallEnabled = value;
				}
				else
				{
					int CurrentProfiles = Instance.CurrentProfileTypes;
					Instance.set_FirewallEnabled(CurrentProfiles, value);
				}
			}
		}

		public RulesContainer Rules { get; }

		public dynamic Instance { get; }

		public class RulesContainer
		{
			private const int NET_FW_SERVICE_FILE_AND_PRINT = 0;
			private const string FPS = "File and Printer Sharing";
			private const string RSTM = "Remote Scheduled Tasks Management";

			private Firewall firewall;

			internal RulesContainer(Firewall f) { firewall = f; }

			public bool this[Rule rule]
			{
				get
				{
					if (Firewall.OldFirewall)
					{
						switch (rule)
						{
							case Rule.FileAndPrinterSharing:
								return firewall.Instance.LocalPolicy.CurrentProfile.Services.Item(NET_FW_SERVICE_FILE_AND_PRINT).Enabled;
							default:
								throw new IndexOutOfRangeException("Unrecognized rule");
						}
					}
					else
					{
						switch (rule)
						{
							case Rule.FileAndPrinterSharing:
								return firewall.Instance.IsRuleGroupCurrentlyEnabled(FPS);
							case Rule.RemoteTaskManagement:
								return firewall.Instance.IsRuleGroupCurrentlyEnabled(RSTM);
							default:
								throw new IndexOutOfRangeException("Unrecognized rule");
						}
					}
				}
				set
				{
					if (Firewall.OldFirewall)
					{
						switch (rule)
						{
							case Rule.FileAndPrinterSharing:
								firewall.Instance.LocalPolicy.CurrentProfile.Services.Item(NET_FW_SERVICE_FILE_AND_PRINT).Enabled = value;
								break;
							default:
								throw new IndexOutOfRangeException("Unrecognized rule");
						}
					}
					else
					{
						switch (rule)
						{
							case Rule.FileAndPrinterSharing:
								firewall.Instance.EnableRuleGroup(firewall.Instance.CurrentProfileTypes, FPS, value);
								break;
							case Rule.RemoteTaskManagement:
								firewall.Instance.EnableRuleGroup(firewall.Instance.CurrentProfileTypes, RSTM, value);
								break;
							default:
								throw new IndexOutOfRangeException("Unrecognized rule");
						}
					}
				}
			}
		}
	}
}
