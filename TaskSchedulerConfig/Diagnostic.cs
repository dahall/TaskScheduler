using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Security.Principal;

namespace TaskSchedulerConfig
{
	class Diagnostics : List<Diagnostics.Diagnostic>
	{
		Validator v;

		public Diagnostics(string server)
		{
			v = new Validator(server);

			Add(new Diagnostic
			{
				Name = "V1 Local Access: User has insufficient permissions to schedule tasks",
				Description = "To schedule a V1 task, the current user must be a member of the Administrators, Backup Operators, or Server Operators group on the local computer.",
				Troubleshooter = o => !(v.UserIsAdmin || v.UserIsBackupOperator || v.UserIsServerOperator),
				Resolution = new Resolution
				{
					Name = "Add user to Administrators group",
					Description = "Adding the user to the Administrators group will give it the right to schedule a V1 task.",
					RequiresConsent = true,
					RequiresElevation = true,
					Resolver = AddUserRole
				}
			});

			Add(new Diagnostic
			{
				Name = "V1 Remote Access: Firewall is not enabled",
				Description = "Firewall must be enabled on local system.",
				Troubleshooter = o => !v.Firewall.Enabled,
				Resolution = new Resolution
				{
					Name = "Enable the firewall",
					Description = "Enabling the firewall allows for the Task Scheduler to secure its interactions.",
					RequiresElevation = true,
					Resolver = o => v.Firewall.Enabled = true
				}
			});

			Add(new Diagnostic
			{
				Name = "V1 Remote Access: \"File and Printer Sharing\" rule on the firewall is not enabled",
				Description = "The \"File and Printer Sharing\" rule must be enabled in order for the Task Scheduler V1 to share its information with other computers.",
				Troubleshooter = o => !v.Firewall.Rules[Firewall.Rule.FileAndPrinterSharing],
				Resolution = new Resolution
				{
					Name = "Enable the \"File and Printer Sharing\" rule on the firewall",
					Description = "Enabling the \"File and Printer Sharing\" rule on the firewall allows the Task Scheduler V1 to share its information with other computers.",
					RequiresElevation = true,
					Resolver = o => v.Firewall.Rules[Firewall.Rule.FileAndPrinterSharing] = true
				}
			});

			Add(new Diagnostic
			{
				Name = "V1 Local Access: Invalid permissions on the \"%windir%\\Tasks\" directory",
				Description = "Permissions on the \"%windir%\\Tasks\" directory do not allow V1 tasks to be created or edited by the current user",
				Troubleshooter = CheckTasksDirPerms,
				Resolution = new Resolution
				{
					Name = "Give current user access to \"%windir%\\Tasks\" directory",
					Description = "Giving the current user access to the \"%windir%\\Tasks\" directory will, in effect, give the user the right to create or edit V1 tasks.",
					RequiresConsent = true,
					RequiresElevation = true,
					Resolver = UpdateTasksDirPerms
				}
			});

			if (System.Environment.OSVersion.Version.Major < 6)
				return;

			Add(new Diagnostic
			{
				Name = "V2 Local Access: User has insufficient permissions to schedule tasks",
				Description = "To schedule a V2 task, the current user must have \"Log on as a batch job\" and \"Log on as a service\" privileges.",
				//Troubleshooter = o => !v.UserRights[LocalSecurityAccountPrivileges.LogonAsBatchJob],
				RequiresElevation = true,
				Troubleshooter = o => !v.UserAccessRights[LocalSecurity.LsaSecurityAccessRights.BatchLogon],
				Resolution = new Resolution
				{
					Name = "Grant user \"Log on as a batch job\" right",
					Description = "Adding the \"Log on as a batch job\" right to the user will give it the right to schedule a V2 task on this and other computers.",
					RequiresConsent = true,
					RequiresElevation = true,
					Resolver = o => v.UserAccessRights[LocalSecurity.LsaSecurityAccessRights.BatchLogon] = true,
				}
			});

			Add(new Diagnostic
			{
				Name = "V2 Remote Access: \"Remote Task Management\" rule on the firewall is not enabled",
				Description = "The \"Remote Task Management\" rule must be enabled in order for the Task Scheduler V2 to share its information with other computers.",
				Troubleshooter = o => !v.Firewall.Rules[Firewall.Rule.RemoteTaskManagement],
				Resolution = new Resolution
				{
					Name = "Enable the \"Remote Task Management\" rule on the firewall",
					Description = "Enabling the \"Remote Task Management\" rule on the firewall allows the Task Scheduler V2 to share its information with other computers.",
					RequiresElevation = true,
					Resolver = o => v.Firewall.Rules[Firewall.Rule.RemoteTaskManagement] = true
				}
			});

			Add(new Diagnostic
			{
				Name = "V2 Remote Access: \"Remote Registry\" service is not running",
				Description = "The \"Remote Registry\" service must be running in order for the Task Scheduler V1 to connect to this computer.",
				Troubleshooter = o => !v.RemoteRegistryServiceRunning,
				Resolution = new Resolution
				{
					Name = "Automatically start the \"Remote Registry\" service",
					Description = "Starting the \"Remote Registry\" service and setting that service to start automatically will ensure that other V1 computers can connect to this computer.",
					RequiresElevation = true,
					Resolver = StartRemoteRegistryService
				}
			});
		}

		public event EventHandler<ShowMessageEventArgs> ShowMessage;

		public IEnumerable<Diagnostic> Issues
		{
			get
			{
				foreach (var item in this)
				{
					if (item.HasIssue.HasValue && item.HasIssue.Value)
						yield return item;
				}
			}
		}

		public IEnumerable<Diagnostic> NonIsseus
		{
			get
			{
				foreach (var item in this)
				{
					if (item.Condition(null) && item.HasIssue.HasValue && !item.HasIssue.Value)
						yield return item;
				}
			}
		}

		public IEnumerable<Diagnostic> UnRun
		{
			get
			{
				foreach (var item in this)
				{
					if (item.Condition(null) && !item.HasIssue.HasValue)
						yield return item;
				}
			}
		}

		private void ShowThisMessage(string s) { ShowMessage?.Invoke(this, new ShowMessageEventArgs(s)); }

		private void AddUserRole(object role)
		{
			throw new InvalidOperationException("You must add the current user manually to the Administrators, Backup Operators, or Server Operators group.");
		}

		private bool CheckTasksDirPerms(object obj)
		{
			ShowThisMessage("Checking permissions on \\Windows\\Tasks folder...");
			var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Tasks"));
			return !DirectoryHasPermission(dir, FileSystemRights.FullControl);
		}

		private static bool DirectoryHasPermission(DirectoryInfo DirectoryPath, FileSystemRights AccessRight)
		{
			if (DirectoryPath != null)
				try { return (DirectoryPath.GetEffectiveRights(WindowsIdentity.GetCurrent()) & AccessRight) == AccessRight; } catch { }
			return false;
		}

		private void StartRemoteRegistryService(object obj)
		{
			if (v.RemoteRegistryService.Status != System.ServiceProcess.ServiceControllerStatus.Stopped && v.RemoteRegistryService.CanStop)
			{
				ShowThisMessage("Stopping \"Remote Registry\" service...");
				v.RemoteRegistryService.Stop();
				v.RemoteRegistryService.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
			}
			if (v.RemoteRegistryService.Status == System.ServiceProcess.ServiceControllerStatus.Stopped)
			{
				ShowThisMessage("Setting \"Remote Registry\" service to start automatically...");
				v.RemoteRegistryService.SetStartType(System.ServiceProcess.ServiceStartMode.Automatic);
				ShowThisMessage("Starting \"Remote Registry\" service...");
				v.RemoteRegistryService.Start();
				v.RemoteRegistryService.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
			}
		}

		private void UpdateTasksDirPerms(object obj)
		{
			ShowThisMessage("Adding user rights to \\Windows\\Tasks folder...");
			string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Tasks");
			var di = new DirectoryInfo(dir);
			var sec = di.GetAccessControl(AccessControlSections.Access);
			sec.AddAccessRule(new FileSystemAccessRule(v.sid, FileSystemRights.Modify, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
			di.SetAccessControl(sec);
		}

		public class Diagnostic
		{
			public bool? HasIssue { get; set; }
			public bool? Resolved { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public Predicate<object> Condition { get; set; } = o => true;
			public bool RequiresElevation { get; set; } = false;
			public Predicate<object> Troubleshooter { get; set; }
			public Resolution Resolution { get; set; }
		}

		public class Resolution
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public bool RequiresConsent { get; set; } = false;
			public bool RequiresElevation { get; set; } = false;
			public Action<object> Resolver { get; set; }
		}

		public class ShowMessageEventArgs : EventArgs
		{
			public ShowMessageEventArgs(string msg) { Message = msg; }
			public string Message { get; }
		}
	}
}
