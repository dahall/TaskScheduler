using System;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed class TaskFolder : IDisposable
	{
		V1Interop.ITaskScheduler v1List = null;
		V2Interop.ITaskFolder v2Folder = null;

		internal TaskFolder(V1Interop.ITaskScheduler ts)
		{
			v1List = ts;
		}

		internal TaskFolder(V2Interop.ITaskFolder iFldr)
		{
			v2Folder = iFldr;
		}

		public void Dispose()
		{
			if (v2Folder != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(v2Folder);
			v1List = null;
		}

		public string Name
		{
			get { return (v2Folder == null) ? @"\" : v2Folder.Name; }
		}

		public string Path
		{
			get { return (v2Folder == null) ? @"\" : v2Folder.Path; }
		}

		/*public TaskFolder GetFolder(string Path)
		{
			if (v2Folder != null)
				return new TaskFolder(v2Folder.GetFolder(Path));
			throw new NotSupportedException();
		}*/

		public TaskFolderCollection SubFolders
		{
			get
			{
				if (v2Folder != null)
					return new TaskFolderCollection(v2Folder.GetFolders(0));
				return new TaskFolderCollection();
			}
		}

		public TaskFolder CreateFolder(string subFolderName, string sddlForm)
		{
			if (v2Folder != null)
				return new TaskFolder(v2Folder.CreateFolder(subFolderName, sddlForm));
			throw new NotSupportedException();
		}

		public void DeleteFolder(string subFolderName)
		{
			if (v2Folder != null)
				v2Folder.DeleteFolder(subFolderName, 0);
			throw new NotSupportedException();
		}

		/*public Task GetTask(string Path)
		{
			if (v2Folder != null)
				return new Task(v2Folder.GetTask(Path));
			throw new NotImplementedException();
		}*/

		public TaskCollection Tasks
		{
			get
			{
				if (v2Folder != null)
					return new TaskCollection(v2Folder.GetTasks(0));
				return new TaskCollection(v1List);
			}
		}

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

		public Task RegisterTask(string Path, string XmlText, TaskCreation createType, string UserId, string password, TaskLogonType LogonType, string sddl)
		{
			if (v2Folder != null)
				return new Task(v2Folder.RegisterTask(Path, XmlText, (int)createType, UserId, password, LogonType, sddl));
			throw new NotSupportedException();
		}

		public Task RegisterTaskDefinition(string Path, TaskDefinition pDefinition, TaskCreation createType, string UserId, string password, TaskLogonType LogonType, string sddl)
		{
			if (v2Folder != null)
				return new Task(v2Folder.RegisterTaskDefinition(Path, pDefinition.v2Def, (int)createType, UserId, password, LogonType, sddl));

			pDefinition.V1Save(Path);
			return new Task(pDefinition.v1Task);
		}

		public string GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Folder != null)
				return v2Folder.GetSecurityDescriptor((int)includeSections);
			throw new NotSupportedException();
		}

		public void SetSecurityDescriptorSddlForm(string sddlForm, System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Folder != null)
				v2Folder.SetSecurityDescriptor(sddlForm, (int)includeSections);
			else
				throw new NotSupportedException();
		}
	}
}
