using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed class TaskService : IDisposable
	{
		internal static readonly bool v2 = (Environment.OSVersion.Version >= new Version(6, 0));

		private V1Interop.ITaskScheduler v1TaskScheduler = null;
		private V2Interop.TaskSchedulerClass v2TaskService = null;

		public TaskService() : this(null) { }

		public TaskService(string targetServer) : this(targetServer, null, null, null) { }

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

		public void Dispose()
		{
			if (v2TaskService != null)
				Marshal.ReleaseComObject(v2TaskService);
			if (v1TaskScheduler != null)
				Marshal.ReleaseComObject(v1TaskScheduler);
			GC.SuppressFinalize(this);
		}

		public TaskFolder GetFolder(string folderName)
		{
			return v2 ? new TaskFolder(v2TaskService.GetFolder(folderName)) : new TaskFolder(v1TaskScheduler);
		}

		public RunningTaskCollection GetRunningTasks(bool includeHidden)
		{
			return v2 ? new RunningTaskCollection(v2TaskService.GetRunningTasks(includeHidden ? 1 : 0)) : new RunningTaskCollection(v1TaskScheduler);
		}

		public TaskDefinition NewTask()
		{
			if (v2)
				return new TaskDefinition(v2TaskService.NewTask(0));
			Guid ITaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
			Guid CTaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.CTask));
			string v1Name = "Temp" + Guid.NewGuid().ToString("B");
			return new TaskDefinition(v1TaskScheduler.NewWorkItem(v1Name, ref CTaskGuid, ref ITaskGuid), v1Name);
		}

		public bool Connected
		{
			get { return v2 ? v2TaskService.Connected : true; }
		}

		public string TargetServer
		{
			get { return v2 ? v2TaskService.TargetServer : v1TaskScheduler.GetTargetComputer(); }
		}

		public string ConnectedDomain
		{
			get { if (v2) return v2TaskService.ConnectedDomain; throw new NotSupportedException(); }
		}

		public string ConnectedUser
		{
			get { if (v2) return v2TaskService.ConnectedUser; throw new NotSupportedException(); }
		}

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
