using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
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

		internal bool RunRemoteRegistryService(object obj)
		{
			v.RemoteRegistryService.SetStartType(ServiceStartMode.Automatic);
			v.RemoteRegistryService.Start();
			return v.RemoteRegistryServiceRunning;
		}
	}

	internal static class ServiceHelper
	{
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern Boolean ChangeServiceConfig(IntPtr hService, UInt32 nServiceType, UInt32 nStartType, UInt32 nErrorControl, String lpBinaryPathName, String lpLoadOrderGroup, IntPtr lpdwTagId, [In] char[] lpDependencies, String lpServiceStartName, String lpPassword, String lpDisplayName);

		private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;

		public static void SetStartType(this ServiceController svc, ServiceStartMode mode)
		{
			using (var serviceHandle = svc.ServiceHandle)
				if (!ChangeServiceConfig(serviceHandle.DangerousGetHandle(), SERVICE_NO_CHANGE, (uint)mode, SERVICE_NO_CHANGE, null, null, IntPtr.Zero, null, null, null, null))
					throw new ExternalException("Could not change service start type.", new Win32Exception());
		}
	}
}
