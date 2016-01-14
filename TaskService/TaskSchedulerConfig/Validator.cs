using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;

namespace TaskSchedulerConfig
{
	class Validator
	{
		private WindowsIdentity id;
		private SecurityIdentifier sid;
		private WindowsPrincipal prin;

		public Validator(string svr)
		{
			Firewall = new Firewall(svr);
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

		public Firewall Firewall { get; }

		public string User => id.Name;
	}
}
