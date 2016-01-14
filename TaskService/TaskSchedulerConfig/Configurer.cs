using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace TaskSchedulerConfig
{
	class Configurer
	{
		Validator v;

		public Configurer(Validator validator)
		{
			v = validator;
		}

		internal bool EnableFirewall(object obj)
		{
			v.Firewall.Enabled = true;
			return v.Firewall.Enabled;
		}

		internal bool EnableFirewallRule(object obj)
		{
			if (obj is Firewall.Rule)
			{
				v.Firewall.Rules[(Firewall.Rule)obj] = true;
				return v.Firewall.Rules[(Firewall.Rule)obj];
			}
			throw new ArgumentException();
		}
	}
}
