using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.ServiceProcess;

namespace TaskSchedulerConfig
{
	class Validator : IDisposable
	{
		private WindowsIdentity id;
		public SecurityIdentifier sid;
		private WindowsPrincipal prin;
		private string server = null;
		private Firewall fw = null;
		private ServiceController sc = null;

		public Validator(string svr = null)
		{
			server = svr;
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

		public Firewall Firewall => fw ?? (fw = new Firewall(server));

		public ServiceController RemoteRegistryService => sc ?? (sc = new ServiceController("RemoteRegistry", server ?? "."));

		public string Server => server;

		public string User => id.Name;

		public bool RemoteRegistryServiceRunning => RemoteRegistryService.Status == ServiceControllerStatus.Running;

		void IDisposable.Dispose()
		{
			if (sc != null) { sc.Close(); sc = null; }
			if (fw != null) { fw = null; }
		}

		public bool UserHasRight(string privName) => new LocalSecurity(Server).UserAccountRights(User)[privName];

		public void GrantUserRight(string privName) { new LocalSecurity(Server).UserAccountRights(User)[privName] = true; }
	}
}