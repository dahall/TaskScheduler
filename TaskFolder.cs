using System;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Provides the methods that are used to register (create) tasks in the folder, remove tasks from the folder, and create or remove subfolders from the folder.
	/// </summary>
	public sealed class TaskFolder : IDisposable
	{
		V1Interop.ITaskScheduler v1List = null;
		V2Interop.ITaskFolder v2Folder = null;

		internal TaskFolder(TaskService svc)
		{
			this.TaskService = svc;
			v1List = svc.v1TaskScheduler;
		}

		internal TaskFolder(TaskService svc, V2Interop.ITaskFolder iFldr)
		{
			this.TaskService = svc;
			v2Folder = iFldr;
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Folder != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(v2Folder);
			v1List = null;
		}

		/// <summary>
		/// Gets the name that is used to identify the folder that contains a task.
		/// </summary>
		public string Name
		{
			get { return (v2Folder == null) ? @"\" : v2Folder.Name; }
		}

		/// <summary>
		/// Gets the path to where the folder is stored.
		/// </summary>
		public string Path
		{
			get { return (v2Folder == null) ? @"\" : v2Folder.Path; }
		}

		/*public TaskFolder GetFolder(string Path)
		{
			if (v2Folder != null)
				return new TaskFolder(v2Folder.GetFolder(Path));
			throw new NotV1SupportedException();
		}*/

		/// <summary>
		/// Gets or sets the security descriptor of the task.
		/// </summary>
		/// <value>The security descriptor.</value>
		public System.Security.AccessControl.GenericSecurityDescriptor SecurityDescriptor
		{
			get
			{
				return GetSecurityDescriptor(System.Security.AccessControl.AccessControlSections.All);
			}
			set
			{
				SetSecurityDescriptor(value, System.Security.AccessControl.AccessControlSections.All);
			}
		}

		/// <summary>
		/// Gets all the subfolders in the folder.
		/// </summary>
		public TaskFolderCollection SubFolders
		{
			get
			{
				if (v2Folder != null)
					return new TaskFolderCollection(this, v2Folder.GetFolders(0));
				return new TaskFolderCollection();
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskService"/> that manages this task.
		/// </summary>
		/// <value>The task service.</value>
		public TaskService TaskService { get; private set; }

		/// <summary>
		/// Creates a folder for related tasks. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="subFolderName">The name used to identify the folder. If "FolderName\SubFolder1\SubFolder2" is specified, the entire folder tree will be created if the folders do not exist. This parameter can be a relative path to the current <see cref="TaskFolder"/> instance. The root task folder is specified with a backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <returns>A <see cref="TaskFolder"/> instance that represents the new subfolder.</returns>
		public TaskFolder CreateFolder(string subFolderName)
		{
			return this.CreateFolder(subFolderName, (string)null);
		}

		/// <summary>
		/// Creates a folder for related tasks. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="subFolderName">The name used to identify the folder. If "FolderName\SubFolder1\SubFolder2" is specified, the entire folder tree will be created if the folders do not exist. This parameter can be a relative path to the current <see cref="TaskFolder"/> instance. The root task folder is specified with a backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="sd">The security descriptor associated with the folder.</param>
		/// <returns>A <see cref="TaskFolder"/> instance that represents the new subfolder.</returns>
		public TaskFolder CreateFolder(string subFolderName, System.Security.AccessControl.GenericSecurityDescriptor sd)
		{
			return this.CreateFolder(subFolderName, sd.GetSddlForm(System.Security.AccessControl.AccessControlSections.All));
		}

		/// <summary>
		/// Creates a folder for related tasks. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="subFolderName">The name used to identify the folder. If "FolderName\SubFolder1\SubFolder2" is specified, the entire folder tree will be created if the folders do not exist. This parameter can be a relative path to the current <see cref="TaskFolder"/> instance. The root task folder is specified with a backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="sddlForm">The security descriptor associated with the folder.</param>
		/// <returns>A <see cref="TaskFolder"/> instance that represents the new subfolder.</returns>
		public TaskFolder CreateFolder(string subFolderName, string sddlForm)
		{
			if (v2Folder != null)
				return new TaskFolder(this.TaskService, v2Folder.CreateFolder(subFolderName, sddlForm));
			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Deletes a subfolder from the parent folder. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="subFolderName">The name of the subfolder to be removed. The root task folder is specified with a backslash (\). This parameter can be a relative path to the folder you want to delete. An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		public void DeleteFolder(string subFolderName)
		{
			if (v2Folder != null)
				v2Folder.DeleteFolder(subFolderName, 0);
			throw new NotV1SupportedException();
		}

		/*public Task GetTask(string Path)
		{
			if (v2Folder != null)
				return new Task(v2Folder.GetTask(Path));
			throw new NotImplementedException();
		}*/

		/// <summary>
		/// Gets a collection of all the tasks in the folder.
		/// </summary>
		public TaskCollection Tasks
		{
			get
			{
				if (v2Folder != null)
					return new TaskCollection(this, v2Folder.GetTasks(1));
				return new TaskCollection(this.TaskService);
			}
		}

		/// <summary>
		/// Deletes a task from the folder.
		/// </summary>
		/// <param name="Name">The name of the task that is specified when the task was registered. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		public void DeleteTask(string Name)
		{
			if (v2Folder != null)
				v2Folder.DeleteTask(Name, 0);
			else
			{
				if (!Name.EndsWith(".job", StringComparison.CurrentCultureIgnoreCase))
					Name += ".job";
				v1List.Delete(Name);
			}
		}

		/// <summary>
		/// Registers (creates) a new task in the folder using XML to define the task. Not available for Task Scheduler 1.0.
		/// </summary>
		/// <param name="Path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="XmlText">An XML-formatted definition of the task.</param>
		/// <param name="createType">A union of <see cref="TaskCreation"/> flags.</param>
		/// <param name="UserId">The user credentials used to register the task.</param>
		/// <param name="password">The password for the userId used to register the task.</param>
		/// <param name="LogonType">A <see cref="TaskLogonType"/> value that defines what logon technique is used to run the registered task.</param>
		/// <param name="sddl">The security descriptor associated with the registered task. You can specify the access control list (ACL) in the security descriptor for a task in order to allow or deny certain users and groups access to a task.</param>
		/// <returns>A <see cref="Task"/> instance that represents the new task.</returns>
		public Task RegisterTask(string Path, string XmlText, TaskCreation createType, string UserId, string password, TaskLogonType LogonType, string sddl)
		{
			if (v2Folder != null)
				return new Task(this.TaskService, v2Folder.RegisterTask(Path, XmlText, (int)createType, UserId, password, LogonType, sddl));
			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Registers (creates) a task in a specified location using a <see cref="TaskDefinition"/> instance to define a task.
		/// </summary>
		/// <param name="Path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="definition">The <see cref="TaskDefinition"/> of the registered task.</param>
		/// <returns>A <see cref="Task"/> instance that represents the new task.</returns>
		public Task RegisterTaskDefinition(string Path, TaskDefinition definition)
		{
			return RegisterTaskDefinition(Path, definition, TaskCreation.CreateOrUpdate, definition.Principal.UserId, null, definition.Principal.LogonType, null);
		}

		/// <summary>
		/// Registers (creates) a task in a specified location using a <see cref="TaskDefinition"/> instance to define a task.
		/// </summary>
		/// <param name="Path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="definition">The <see cref="TaskDefinition"/> of the registered task.</param>
		/// <param name="createType">A union of <see cref="TaskCreation"/> flags.</param>
		/// <param name="UserId">The user credentials used to register the task.</param>
		/// <param name="password">The password for the userId used to register the task.</param>
		/// <param name="LogonType">A <see cref="TaskLogonType"/> value that defines what logon technique is used to run the registered task.</param>
		/// <param name="sddl">The security descriptor associated with the registered task. You can specify the access control list (ACL) in the security descriptor for a task in order to allow or deny certain users and groups access to a task.</param>
		/// <returns>A <see cref="Task"/> instance that represents the new task.</returns>
		public Task RegisterTaskDefinition(string Path, TaskDefinition definition, TaskCreation createType, string UserId, string password, TaskLogonType LogonType, string sddl)
		{
			if (v2Folder != null)
				return new Task(this.TaskService, v2Folder.RegisterTaskDefinition(Path, definition.v2Def, (int)createType, UserId, password, LogonType, sddl));

			// Adds ability to set a password for a V1 task. Provided by Arcao.
			V1Interop.TaskFlags flags = definition.v1Task.GetFlags();
			switch (LogonType)
			{
				case TaskLogonType.Group:
				case TaskLogonType.S4U:
				case TaskLogonType.None:
					throw new NotV1SupportedException("This LogonType is not supported on Task Scheduler 1.0.");
				case TaskLogonType.InteractiveToken:
					flags |= (V1Interop.TaskFlags.RunOnlyIfLoggedOn | V1Interop.TaskFlags.Interactive);
					if (String.IsNullOrEmpty(UserId))
						UserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
					definition.v1Task.SetAccountInformation(UserId, IntPtr.Zero);
					break;
				case TaskLogonType.ServiceAccount:
					flags &= ~(V1Interop.TaskFlags.Interactive | V1Interop.TaskFlags.RunOnlyIfLoggedOn);
					definition.v1Task.SetAccountInformation((String.IsNullOrEmpty(UserId) || UserId.Equals("SYSTEM", StringComparison.CurrentCultureIgnoreCase)) ? String.Empty : UserId, IntPtr.Zero);
					break;
				case TaskLogonType.InteractiveTokenOrPassword:
					flags |= V1Interop.TaskFlags.Interactive;
					using (V1Interop.CoTaskMemString cpwd = new V1Interop.CoTaskMemString(password))
						definition.v1Task.SetAccountInformation(UserId, cpwd.DangerousGetHandle());
					break;
				case TaskLogonType.Password:
					using (V1Interop.CoTaskMemString cpwd = new V1Interop.CoTaskMemString(password))
						definition.v1Task.SetAccountInformation(UserId, cpwd.DangerousGetHandle());
					break;
				default:
					break;
			}
			definition.v1Task.SetFlags(flags);

			switch (createType)
			{
				case TaskCreation.Create:
				case TaskCreation.CreateOrUpdate:
				case TaskCreation.Disable:
				case TaskCreation.Update:
					if (createType == TaskCreation.Disable)
						definition.Settings.Enabled = false;
					definition.V1Save(Path);
					break;
				case TaskCreation.DontAddPrincipalAce:
					throw new NotV1SupportedException("Security settings are not available on Task Scheduler 1.0.");
				case TaskCreation.IgnoreRegistrationTriggers:
					throw new NotV1SupportedException("Registration triggers are not available on Task Scheduler 1.0.");
				case TaskCreation.ValidateOnly:
					throw new NotV1SupportedException("Xml validation not available on Task Scheduler 1.0.");
				default:
					break;
			}
			return new Task(this.TaskService, definition.v1Task);
		}

		/// <summary>
		/// Gets the security descriptor for the folder. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="includeSections">Section(s) of the security descriptor to return.</param>
		/// <returns>The security descriptor for the folder.</returns>
		public System.Security.AccessControl.GenericSecurityDescriptor GetSecurityDescriptor(System.Security.AccessControl.AccessControlSections includeSections)
		{
			return new System.Security.AccessControl.RawSecurityDescriptor(GetSecurityDescriptorSddlForm(includeSections));
		}

		/// <summary>
		/// Gets the security descriptor for the folder. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="includeSections">Section(s) of the security descriptor to return.</param>
		/// <returns>The security descriptor for the folder.</returns>
		public string GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Folder != null)
				return v2Folder.GetSecurityDescriptor((int)includeSections);
			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Sets the security descriptor for the folder. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="sd">The security descriptor for the folder.</param>
		/// <param name="includeSections">Section(s) of the security descriptor to set.</param>
		public void SetSecurityDescriptor(System.Security.AccessControl.GenericSecurityDescriptor sd, System.Security.AccessControl.AccessControlSections includeSections)
		{
			this.SetSecurityDescriptorSddlForm(sd.GetSddlForm(includeSections), includeSections);
		}

		/// <summary>
		/// Sets the security descriptor for the folder. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="sddlForm">The security descriptor for the folder.</param>
		/// <param name="includeSections">Section(s) of the security descriptor to set.</param>
		public void SetSecurityDescriptorSddlForm(string sddlForm, System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Folder != null)
				v2Folder.SetSecurityDescriptor(sddlForm, (int)includeSections);
			else
				throw new NotV1SupportedException();
		}
	}
}
