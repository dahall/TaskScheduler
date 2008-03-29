using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Provides access to the Task Scheduler service for managing registered tasks.
	/// </summary>
	public sealed class TaskService : IDisposable
	{
		internal static readonly bool v2 = (Environment.OSVersion.Version >= new Version(6, 0));

		private V1Interop.ITaskScheduler v1TaskScheduler = null;
		private V2Interop.TaskSchedulerClass v2TaskService = null;

		/// <summary>
		/// Creates a new instance of a TaskService connecting to the local machine as the current user.
		/// </summary>
		public TaskService() : this(null) { }

		/// <summary>
		/// Creates a new instance of a TaskService connecting to a remote machine as the current user.
		/// </summary>
		public TaskService(string targetServer) : this(targetServer, null, null, null) { }

		/// <summary>
		/// Creates a new instance of a TaskService connecting to a remote machine as the specified user.
		/// </summary>
		public TaskService(string targetServer, string userName, string accountDomain, string password)
		{
			if (v2)
			{
				v2TaskService = new V2Interop.TaskSchedulerClass();
				v2TaskService.Connect(targetServer, userName, accountDomain, password);
			}
			else
			{
				if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(accountDomain) || !string.IsNullOrEmpty(password))
					throw new NotSupportedException();
				V1Interop.CTaskScheduler csched = new V1Interop.CTaskScheduler();
				v1TaskScheduler = (V1Interop.ITaskScheduler)csched;
				if (!string.IsNullOrEmpty(targetServer))
					v1TaskScheduler.SetTargetComputer(targetServer);
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2TaskService != null)
				Marshal.ReleaseComObject(v2TaskService);
			if (v1TaskScheduler != null)
				Marshal.ReleaseComObject(v1TaskScheduler);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the path to a folder of registered tasks.
		/// </summary>
		/// <param name="folderName">The path to the folder to retrieve. Do not use a backslash following the last folder name in the path. The root task folder is specified with a backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <returns><see cref="TaskFolder"/> instance for the requested folder.</returns>
		/// <exception cref="Exception">Requested folder was not found.</exception>
		/// <exception cref="NotSupportedException">Folder other than the root (\) was requested on a system not supporting Task Scheduler 2.0.</exception>
		public TaskFolder GetFolder(string folderName)
		{
			return v2 ? new TaskFolder(v2TaskService.GetFolder(folderName)) : new TaskFolder(v1TaskScheduler);
		}

		/// <summary>
		/// Gets a collection of running tasks.
		/// </summary>
		/// <param name="includeHidden">True to include hidden tasks.</param>
		/// <returns><see cref="RunningTaskCollection"/> instance with the list of running tasks.</returns>
		public RunningTaskCollection GetRunningTasks(bool includeHidden)
		{
			return v2 ? new RunningTaskCollection(v2TaskService.GetRunningTasks(includeHidden ? 1 : 0)) : new RunningTaskCollection(v1TaskScheduler);
		}
		
		/// <summary>
		/// Returns an empty task definition object to be filled in with settings and properties and then registered using the <see cref="TaskFolder.RegisterTaskDefinition"/> method.
		/// </summary>
		/// <returns><see cref="TaskDefinition"/> instance for setting properties.</returns>
		public TaskDefinition NewTask()
		{
			if (v2)
				return new TaskDefinition(v2TaskService.NewTask(0));
			Guid ITaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
			Guid CTaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.CTask));
			string v1Name = "Temp" + Guid.NewGuid().ToString("B");
			return new TaskDefinition(v1TaskScheduler.NewWorkItem(v1Name, ref CTaskGuid, ref ITaskGuid), v1Name);
		}

		/// <summary>
		/// Gets a Boolean value that indicates if you are connected to the Task Scheduler service.
		/// </summary>
		public bool Connected
		{
			get { return v2 ? v2TaskService.Connected : true; }
		}

		/// <summary>
		/// Gets the name of the computer that is running the Task Scheduler service that the user is connected to.
		/// </summary>
		public string TargetServer
		{
			get { return v2 ? v2TaskService.TargetServer : v1TaskScheduler.GetTargetComputer(); }
		}

		/// <summary>
		/// Gets the name of the domain to which the <see cref="TargetServer"/> computer is connected.
		/// </summary>
		/// <exception cref="NotSupportedException">Thrown when called against Task Scheduler 1.0.</exception>
		public string ConnectedDomain
		{
			get { if (v2) return v2TaskService.ConnectedDomain; throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the name of the user that is connected to the Task Scheduler service.
		/// </summary>
		/// <exception cref="NotSupportedException">Thrown when called against Task Scheduler 1.0.</exception>
		public string ConnectedUser
		{
			get { if (v2) return v2TaskService.ConnectedUser; throw new NotSupportedException(); }
		}

		/// <summary>
		/// Gets the highest version of Task Scheduler that a computer supports.
		/// </summary>
		public Version HighestSupportedVersion
		{
			get
			{
				if (v2)
				{
					uint v = v2TaskService.HighestVersion;
					return new Version((int)(v >> 16), (int)(v & 0x0000FFFF));
				}
				return new Version(1, 1);
			}
		}
	}
}
