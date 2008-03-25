using System;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	#region Task Enumerations

	/// <summary>Defines what versions of Task Scheduler or the AT command that the task is compatible with.</summary>
	public enum TaskCompatibility
	{
		/// <summary>The task is compatible with the AT command.</summary>
		AT,
		/// <summary>The task is compatible with Task Scheduler 1.0.</summary>
		V1,
		/// <summary>The task is compatible with Task Scheduler 2.0.</summary>
		V2
	}

	/// <summary>Defines how the Task Scheduler service creates, updates, or disables the task.</summary>
	public enum TaskCreation
	{
		/// <summary>The Task Scheduler service registers the task as a new task.</summary>
		Create = 2,
		/// <summary>The Task Scheduler service either registers the task as a new task or as an updated version if the task already exists. Equivalent to Create | Update.</summary>
		CreateOrUpdate = 6,
		/// <summary>The Task Scheduler service registers the disabled task. A disabled task cannot run until it is enabled. For more information, see Enabled Property of TaskSettings and Enabled Property of RegisteredTask.</summary>
		Disable = 8,
		/// <summary>The Task Scheduler service is prevented from adding the allow access-control entry (ACE) for the context principal. When the TaskFolder.RegisterTaskDefinition or TaskFolder.RegisterTask functions are called with this flag to update a task, the Task Scheduler service does not add the ACE for the new context principal and does not remove the ACE from the old context principal.</summary>
		DontAddPrincipalAce = 0x10,
		/// <summary>The Task Scheduler service creates the task, but ignores the registration triggers in the task. By ignoring the registration triggers, the task will not execute when it is registered unless a time-based trigger causes it to execute on registration.</summary>
		IgnoreRegistrationTriggers = 0x20,
		/// <summary>The Task Scheduler service registers the task as an updated version of an existing task. When a task with a registration trigger is updated, the task will execute after the update occurs.</summary>
		Update = 4,
		/// <summary>The Task Scheduler service checks the syntax of the XML that describes the task but does not register the task. This constant cannot be combined with the Create, Update, or CreateOrUpdate values.</summary>
		ValidateOnly = 1
	}

	/// <summary>Defines how the Task Scheduler handles existing instances of the task when it starts a new instance of the task.</summary>
	public enum TaskInstancesPolicy
	{
		/// <summary>Starts new instance while an existing instance is running.</summary>
		Parallel,
		/// <summary>Starts a new instance of the task after all other instances of the task are complete.</summary>
		Queue,
		/// <summary>Does not start a new instance if an existing instance of the task is running.</summary>
		IgnoreNew,
		/// <summary>Stops an existing instance of the task before it starts a new instance.</summary>
		StopExisting
	}

	/// <summary>Defines what logon technique is required to run a task.</summary>
	public enum TaskLogonType
	{
		/// <summary>The logon method is not specified. Used for non-NT credentials.</summary>
		None,
		/// <summary>Use a password for logging on the user. The password must be supplied at registration time.</summary>
		Password,
		/// <summary>Use an existing interactive token to run a task. The user must log on using a service for user (S4U) logon. When an S4U logon is used, no password is stored by the system and there is no access to either the network or to encrypted files.</summary>
		S4U,
		/// <summary>User must already be logged on. The task will be run only in an existing interactive session.</summary>
		InteractiveToken,
		/// <summary>Group activation. The groupId field specifies the group.</summary>
		Group,
		/// <summary>Indicates that a Local System, Local Service, or Network Service account is being used as a security context to run the task.</summary>
		ServiceAccount,
		/// <summary>First use the interactive token. If the user is not logged on (no interactive token is available), then the password is used. The password must be specified when a task is registered. This flag is not recommended for new tasks because it is less reliable than Password.</summary>
		InteractiveTokenOrPassword
	}

	/// <summary>Defines how a task is run.</summary>
	public enum TaskRunFlags
	{
		/// <summary>The task is run as the user who is calling the Run method.</summary>
		AsSelf = 1,
		/// <summary>The task is run regardless of constraints such as "do not run on batteries" or "run only if idle".</summary>
		IgnoreConstraints = 2,
		/// <summary>The task is run with all flags ignored.</summary>
		NoFlags = 0,
		/// <summary>The task is run using a terminal server session identifier.</summary>
		UseSessionId = 4,
		/// <summary>The task is run using a security identifier.</summary>
		UserSID = 8
	}

	/// <summary>Defines LUA elevation flags that specify with what privilege level the task will be run.</summary>
	public enum TaskRunLevel
	{
		/// <summary>Tasks will be run with the least privileges.</summary>
		LUA,
		/// <summary>Tasks will be run with the highest privileges.</summary>
		Highest
	}

	/// <summary>Defines what kind of Terminal Server session state change you can use to trigger a task to start. These changes are used to specify the type of state change in the SessionStateChangeTrigger.</summary>
	public enum TaskSessionStateChangeType
	{
		/// <summary>Terminal Server console connection state change. For example, when you connect to a user session on the local computer by switching users on the computer.</summary>
		ConsoleConnect = 1,
		/// <summary>Terminal Server console disconnection state change. For example, when you disconnect to a user session on the local computer by switching users on the computer.</summary>
		ConsoleDisconnect = 2,
		/// <summary>Terminal Server remote connection state change. For example, when a user connects to a user session by using the Remote Desktop Connection program from a remote computer.</summary>
		RemoteConnect = 3,
		/// <summary>Terminal Server remote disconnection state change. For example, when a user disconnects from a user session while using the Remote Desktop Connection program from a remote computer.</summary>
		RemoteDisconnect = 4,
		/// <summary>Terminal Server session locked state change. For example, this state change causes the task to run when the computer is locked.</summary>
		SessionLock = 7,
		/// <summary>Terminal Server session unlocked state change. For example, this state change causes the task to run when the computer is unlocked.</summary>
		SessionUnlock = 8
	}

	/// <summary>Defines the different states that a registered task can be in.</summary>
	public enum TaskState
	{
		/// <summary>The state of the task is unknown.</summary>
		Unknown,
		/// <summary>The task is registered but is disabled and no instances of the task are queued or running. The task cannot be run until it is enabled.</summary>
		Disabled,
		/// <summary>Instances of the task are queued.</summary>
		Queued,
		/// <summary>The task is ready to be executed, but no instances are queued or running.</summary>
		Ready,
		/// <summary>One or more instances of the task is running.</summary>
		Running
	}

	#endregion

	#region Task Support Classes

	public sealed class TaskRegistrationInfo : IDisposable
	{
		private TaskScheduler.V1Interop.ITask v1Task = null;
		private TaskScheduler.V2Interop.IRegistrationInfo v2RegInfo = null;

		internal TaskRegistrationInfo(TaskScheduler.V2Interop.IRegistrationInfo iRegInfo)
		{
			v2RegInfo = iRegInfo;
		}

		internal TaskRegistrationInfo(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		public void Dispose()
		{
			v1Task = null;
			if (v2RegInfo != null)
				Marshal.ReleaseComObject(v2RegInfo);
		}

		public string Description
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Description;
				return v1Task.GetComment();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Description = value;
				else
					v1Task.SetComment(value);
			}
		}

		public string Author
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Author;
				return v1Task.GetCreator();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Author = value;
				else
					v1Task.SetCreator(value);
			}
		}

		public string Version
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Version;
				return string.Empty;
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Version = value;
				else
					throw new NotSupportedException();
			}
		}

		public string Date
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Date;
				throw new NotSupportedException();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Date = value;
				else
					throw new NotSupportedException();
			}
		}

		internal static object GetTaskData(V1Interop.ITask v1Task)
		{
			ushort DataLen;
			IntPtr Data;
			try
			{
				v1Task.GetWorkItemData(out DataLen, out Data);
				byte[] bytes = new byte[DataLen];
				Marshal.Copy(Data, bytes, 0, DataLen);
				System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes, false);
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter b = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				return b.Deserialize(stream);
			}
			catch { }
			return string.Empty;
		}

		internal static void SetTaskData(V1Interop.ITask v1Task, object value)
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter b = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			System.IO.MemoryStream stream = new System.IO.MemoryStream();
			b.Serialize(stream, value);
			v1Task.SetWorkItemData((ushort)stream.Length, stream.GetBuffer());
		}

		public string Documentation
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Documentation;
				return GetTaskData(v1Task).ToString();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Documentation = value;
				else
					SetTaskData(v1Task, value);
			}
		}

		public string XmlText
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.XmlText;
				throw new NotSupportedException();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.XmlText = value;
				else
					throw new NotSupportedException();
			}
		}

		public string URI
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.URI;
				throw new NotSupportedException();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.URI = value;
				else
					throw new NotSupportedException();
			}
		}

		public string SecurityDescriptorSddlForm
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.SecurityDescriptor.ToString();
				throw new NotSupportedException();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.SecurityDescriptor = value;
				else
					throw new NotSupportedException();
			}
		}

		public string Source
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Source;
				throw new NotSupportedException();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Source = value;
				else
					throw new NotSupportedException();
			}
		}
	}

	public sealed class TaskSettings : IDisposable
	{
		private TaskScheduler.V1Interop.ITask v1Task = null;
		private TaskScheduler.V2Interop.ITaskSettings v2Settings = null;
		private IdleSettings idleSettings = null;
		private NetworkSettings networkSettings = null;

		internal TaskSettings(TaskScheduler.V2Interop.ITaskSettings iSettings)
		{
			v2Settings = iSettings;
		}

		internal TaskSettings(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
			idleSettings = null;
			networkSettings = null;
			v1Task = null;
		}

		public bool AllowDemandStart
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.AllowDemandStart;
				return true;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.AllowDemandStart = value;
				else
					throw new NotSupportedException();
			}
		}

		public int RestartInterval
		{
			get
			{
				if (v2Settings != null)
					return Int32.Parse(v2Settings.RestartInterval);
				return 0;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RestartInterval = value.ToString();
				else
					throw new NotSupportedException();
			}
		}

		public int RestartCount
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.RestartCount;
				return 0;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RestartCount = value;
				else
					throw new NotSupportedException();
			}
		}

		public TaskInstancesPolicy MultipleInstances
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.MultipleInstances;
				return TaskInstancesPolicy.IgnoreNew;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.MultipleInstances = value;
				else
					throw new NotSupportedException();
			}
		}

		public bool StopIfGoingOnBatteries
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.StopIfGoingOnBatteries;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.KillIfGoingOnBatteries) == V1Interop.TaskFlags.KillIfGoingOnBatteries;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.StopIfGoingOnBatteries = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.KillIfGoingOnBatteries);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.KillIfGoingOnBatteries);
				}
			}
		}

		public bool DisallowStartIfOnBatteries
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.DisallowStartIfOnBatteries;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.DontStartIfOnBatteries) == V1Interop.TaskFlags.DontStartIfOnBatteries;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.DisallowStartIfOnBatteries = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.DontStartIfOnBatteries);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.DontStartIfOnBatteries);
				}
			}
		}

		public bool AllowHardTerminate
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.AllowHardTerminate;
				return true;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.AllowHardTerminate = value;
				else
					throw new NotSupportedException();
			}
		}

		public bool StartWhenAvailable
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.StartWhenAvailable;
				return false;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.StartWhenAvailable = value;
				else
					throw new NotSupportedException();
			}
		}

		public string XmlText
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.XmlText;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.XmlText = value;
				else
					throw new NotSupportedException();
			}
		}

		public bool RunOnlyIfNetworkAvailable
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.RunOnlyIfNetworkAvailable;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.RunIfConnectedToInternet) == V1Interop.TaskFlags.RunIfConnectedToInternet;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RunOnlyIfNetworkAvailable = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.RunIfConnectedToInternet);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.RunIfConnectedToInternet);
				}
			}
		}

		public TimeSpan ExecutionTimeLimit
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.ExecutionTimeLimit);
				return TimeSpan.FromMilliseconds(v1Task.GetMaxRunTime());
			}
			set
			{
				if (v2Settings != null)
					v2Settings.ExecutionTimeLimit = Task.TimeSpanToString(value);
				else
					v1Task.SetMaxRunTime(Convert.ToUInt32(value.TotalMilliseconds));
			}
		}

		public bool Enabled
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Enabled;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.Disabled) != V1Interop.TaskFlags.Disabled;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Enabled = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (!value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.Disabled);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.Disabled);
				}
			}
		}

		public TimeSpan DeleteExpiredTaskAfter
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.DeleteExpiredTaskAfter);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.DeleteExpiredTaskAfter = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}

		public System.Diagnostics.ProcessPriorityClass Priority
		{
			get
			{
				if (v2Settings != null)
					return (System.Diagnostics.ProcessPriorityClass)v2Settings.Priority;
				return (System.Diagnostics.ProcessPriorityClass)v1Task.GetPriority();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Priority = (int)value;
				else
					v1Task.SetPriority((uint)value);
			}
		}

		public TaskCompatibility Compatibility
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Compatibility;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Compatibility = value;
				else
					throw new NotSupportedException();
			}
		}

		public bool Hidden
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Hidden;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.Hidden) == V1Interop.TaskFlags.Hidden;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Hidden = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.Hidden);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.Hidden);
				}
			}
		}

		public IdleSettings IdleSettings
		{
			get
			{
				if (idleSettings == null)
				{
					if (v2Settings != null)
						idleSettings = new IdleSettings(v2Settings.IdleSettings);
					else
						idleSettings = new IdleSettings(v1Task);
				}
				return idleSettings;
			}
		}

		public bool RunOnlyIfIdle
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.RunOnlyIfIdle;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.StartOnlyIfIdle) == V1Interop.TaskFlags.StartOnlyIfIdle;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RunOnlyIfIdle = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.StartOnlyIfIdle);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.StartOnlyIfIdle);
				}
			}
		}

		public bool WakeToRun
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.WakeToRun;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.WakeToRun = value;
				else
					throw new NotSupportedException();
			}
		}

		public NetworkSettings NetworkSettings
		{
			get
			{
				if (networkSettings == null)
				{
					if (v2Settings != null)
						networkSettings = new NetworkSettings(v2Settings.NetworkSettings);
					else
						networkSettings = new NetworkSettings();
				}
				return networkSettings;
			}
		}
	}

	public sealed class IdleSettings : IDisposable
	{
		private V1Interop.ITask v1Task = null;
		private V2Interop.IIdleSettings v2Settings;

		internal IdleSettings(V2Interop.IIdleSettings iSettings)
		{
			v2Settings = iSettings;
		}

		internal IdleSettings(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
			v1Task = null;
		}

		public TimeSpan IdleDuration
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.IdleDuration);
				ushort idleMin, deadMin;
				v1Task.GetIdleWait(out idleMin, out deadMin);
				return TimeSpan.FromMinutes((double)deadMin);
			}
			set
			{
				if (v2Settings != null)
					v2Settings.IdleDuration = Task.TimeSpanToString(value);
				else
				{
					v1Task.SetIdleWait((ushort)this.WaitTimeout.TotalMinutes, (ushort)value.TotalMinutes);
				}
			}
		}

		public TimeSpan WaitTimeout
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.WaitTimeout);
				ushort idleMin, deadMin;
				v1Task.GetIdleWait(out idleMin, out deadMin);
				return TimeSpan.FromMinutes(idleMin);
			}
			set
			{
				if (v2Settings != null)
					v2Settings.WaitTimeout = Task.TimeSpanToString(value);
				else
				{
					v1Task.SetIdleWait((ushort)value.TotalMinutes, (ushort)this.IdleDuration.TotalMinutes);
				}
			}
		}

		public bool StopOnIdleEnd
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.StopOnIdleEnd;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.KillOnIdleEnd) == V1Interop.TaskFlags.KillOnIdleEnd;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.StopOnIdleEnd = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.KillOnIdleEnd);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.KillOnIdleEnd);
				}
			}
		}

		public bool RestartOnIdle
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.RestartOnIdle;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.RestartOnIdleResume) == V1Interop.TaskFlags.RestartOnIdleResume;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RestartOnIdle = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.RestartOnIdleResume);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.RestartOnIdleResume);
				}
			}
		}
	}

	public sealed class NetworkSettings : IDisposable
	{
		private V2Interop.INetworkSettings v2Settings = null;

		internal NetworkSettings(V2Interop.INetworkSettings iSettings)
		{
			v2Settings = iSettings;
		}

		internal NetworkSettings()
		{
		}

		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
		}

		public string Name
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Name;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Name = value;
				else
					throw new NotSupportedException();
			}
		}

		public string Id
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Id;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Id = value;
				else
					throw new NotSupportedException();
			}
		}
	}

	public sealed class TaskPrincipal
	{
		private V2Interop.IPrincipal v2Principal;
		private V1Interop.ITask v1Task = null;

		internal TaskPrincipal(V2Interop.IPrincipal iPrincipal)
		{
			v2Principal = iPrincipal;
		}

		internal TaskPrincipal(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		public void Dispose()
		{
			if (v2Principal != null)
				Marshal.ReleaseComObject(v2Principal);
			v1Task = null;
		}

		public string Id
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.Id;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.Id = value;
				else
					throw new NotSupportedException();
			}
		}

		public string DisplayName
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.DisplayName;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.DisplayName = value;
				else
					throw new NotSupportedException();
			}
		}

		public string UserId
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.UserId;
				return v1Task.GetAccountInformation();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.UserId = value;
				else
					v1Task.SetAccountInformation(value, IntPtr.Zero);
			}
		}

		public TaskLogonType LogonType
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.LogonType;
				if ((v1Task.GetFlags() & V1Interop.TaskFlags.Interactive) == V1Interop.TaskFlags.Interactive)
					return TaskLogonType.InteractiveToken;
				else if ((v1Task.GetFlags() & V1Interop.TaskFlags.SystemRequired) == V1Interop.TaskFlags.SystemRequired)
					return TaskLogonType.ServiceAccount;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.LogonType = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value == TaskLogonType.InteractiveToken)
						flags |= V1Interop.TaskFlags.Interactive;
					else
						flags &= ~V1Interop.TaskFlags.Interactive;
					if (value == TaskLogonType.ServiceAccount)
						flags |= V1Interop.TaskFlags.SystemRequired;
					else
						flags &= ~V1Interop.TaskFlags.SystemRequired;
					v1Task.SetFlags(flags);
					if (value == TaskLogonType.Group || value == TaskLogonType.InteractiveTokenOrPassword || value == TaskLogonType.None || value == TaskLogonType.Password || value == TaskLogonType.S4U)
						throw new NotSupportedException();
				}
			}
		}

		public string GroupId
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.GroupId;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.GroupId = value;
				else
					throw new NotSupportedException();
			}
		}

		public TaskRunLevel RunLevel
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.RunLevel;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Principal != null)
					v2Principal.RunLevel = value;
				else
					throw new NotSupportedException();
			}
		}
	}

	public sealed class TaskDefinition
	{
		internal V2Interop.ITaskDefinition v2Def = null;
		internal V1Interop.ITask v1Task = null;
		internal string v1Name = string.Empty;
		private TaskRegistrationInfo regInfo = null;
		private TriggerCollection triggers = null;
		private TaskSettings settings = null;
		private TaskPrincipal principal = null;
		private ActionCollection actions = null;

		internal TaskDefinition(V1Interop.ITask iTask, string name)
		{
			v1Task = iTask;
			v1Name = name;
		}

		internal TaskDefinition(V2Interop.ITaskDefinition iDef)
		{
			v2Def = iDef;
		}

		public void Dispose()
		{
			regInfo = null;
			triggers = null;
			settings = null;
			principal = null;
			actions = null;
			if (v2Def != null) Marshal.ReleaseComObject(v2Def);
			v1Task = null;
		}

		internal void V1Save(string newName)
		{
			if (v1Task != null)
			{
				this.triggers.Bind();
	
				IPersistFile iFile = (IPersistFile)v1Task;
				if (string.IsNullOrEmpty(newName) || newName == v1Name)
					iFile.Save(null, false);
				else
				{
					string path;
					iFile.GetCurFile(out path);
					iFile.Save(System.IO.Path.GetDirectoryName(path) + System.IO.Path.DirectorySeparatorChar + newName + System.IO.Path.GetExtension(path), true);
				}
				iFile = null;
			}
		}

		public TaskRegistrationInfo RegistrationInfo
		{
			get
			{
				if (regInfo == null)
				{
					if (v2Def != null)
						regInfo = new TaskRegistrationInfo(v2Def.RegistrationInfo);
					else
						regInfo = new TaskRegistrationInfo(v1Task);					
				}
				return regInfo;
			}
		}

		public TriggerCollection Triggers
		{
			get
			{
				if (triggers == null)
				{
					if (v2Def != null)
						triggers = new TriggerCollection(v2Def.Triggers);
					else
						triggers = new TriggerCollection(v1Task);
				}
				return triggers;
			}
		}

		public TaskSettings Settings
		{
			get
			{
				if (settings == null)
				{
					if (v2Def != null)
						settings = new TaskSettings(v2Def.Settings);
					else
						settings = new TaskSettings(v1Task);
				}
				return settings;
			}
		}

		public string Data
		{
			get
			{
				if (v2Def != null)
					return v2Def.Data;
				return TaskRegistrationInfo.GetTaskData(v1Task).ToString();
			}
			set
			{
				if (v2Def != null)
					v2Def.Data = value;
				else
					TaskRegistrationInfo.SetTaskData(v1Task, value);
			}
		}

		public TaskPrincipal Principal
		{
			get
			{
				if (principal == null)
				{
					if (v2Def != null)
						principal = new TaskPrincipal(v2Def.Principal);
					else
						principal = new TaskPrincipal(v1Task);
				}
				return principal;
			}
		}

		public ActionCollection Actions
		{
			get
			{
				if (actions == null)
				{
					if (v2Def != null)
						actions = new ActionCollection(v2Def);
					else
						actions = new ActionCollection(v1Task);
				}
				return actions;
			}
		}

		public string XmlText
		{
			get
			{
				if (v2Def != null)
					return v2Def.XmlText;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Def != null)
					v2Def.XmlText = value;
				else
					throw new NotSupportedException();
			}
		}

	}

	#endregion

	public sealed class Task : IDisposable
	{
		internal V1Interop.ITask v1Task;
		private V2Interop.IRegisteredTask v2Task;

		internal Task(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		internal Task(TaskScheduler.V2Interop.IRegisteredTask iTask)
		{
			v2Task = iTask;
		}

		public void Dispose()
		{
			if (v2Task != null)
				Marshal.ReleaseComObject(v2Task);
			v1Task = null;
		}

		internal static TimeSpan StringToTimeSpan(string input)
		{
			TimeSpan span = new TimeSpan(0);
			System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(input, "P(?:(?<Y>[0-9]*)Y)?(?:(?<Mo>[0-9]*)M)?(?:(?<D>[0-9]*)D)?T(?:(?<H>[0-9]*)H)?(?:(?<M>[0-9]*)M)?(?:(?<S>[0-9]*)S)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
			if (m.Success)
			{
				DateTime now = DateTime.Now;
				int years = string.IsNullOrEmpty(m.Groups["Y"].Value) ? 0 : Int32.Parse(m.Groups["Y"].Value);
				int months = string.IsNullOrEmpty(m.Groups["Mo"].Value) ? 0 : Int32.Parse(m.Groups["Mo"].Value);
				int days = string.IsNullOrEmpty(m.Groups["D"].Value) ? 0 : Int32.Parse(m.Groups["D"].Value);
				if (years > 0 || months > 0)
					days += (now - new DateTime(now.Year + years + (now.Month + months) / 12, (now.Month + months) % 12, 1)).Days;
				int hours = string.IsNullOrEmpty(m.Groups["H"].Value) ? 0 : Int32.Parse(m.Groups["H"].Value);
				int minutes = string.IsNullOrEmpty(m.Groups["M"].Value) ? 0 : Int32.Parse(m.Groups["M"].Value);
				int seconds = string.IsNullOrEmpty(m.Groups["S"].Value) ? 0 : Int32.Parse(m.Groups["S"].Value);
				span = new TimeSpan(days, hours, minutes, seconds);
			}
			return span;
		}

		internal static string TimeSpanToString(TimeSpan span)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder("P", 20);
			if (span.Days > 0) sb.AppendFormat("{0}D", span.Days);
			sb.Append('T');
			if (span.Hours > 0) sb.AppendFormat("{0}H", span.Hours);
			if (span.Minutes > 0) sb.AppendFormat("{0}M", span.Minutes);
			if (span.Seconds > 0) sb.AppendFormat("{0}S", span.Seconds);
			return sb.ToString();
		}

		internal static string GetV1Path(V1Interop.ITask v1Task)
		{
			string fileName = string.Empty;
			try
			{
				IPersistFile iFile = (IPersistFile)v1Task;
				iFile.GetCurFile(out fileName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return fileName;
		}

		public string Name
		{
			get
			{
				if (v2Task != null)
					return v2Task.Name;
				return System.IO.Path.GetFileNameWithoutExtension(GetV1Path(v1Task));
			}
		}

		public string Path
		{
			get
			{
				if (v2Task != null)
					return v2Task.Path;
				return System.IO.Path.GetDirectoryName(GetV1Path(v1Task));
			}
		}

		public TaskState State
		{
			get
			{
				if (v2Task != null)
					return v2Task.State;

				switch (v1Task.GetStatus())
				{
					case V1Interop.TaskStatus.Ready:
					case V1Interop.TaskStatus.NeverRun:
					case V1Interop.TaskStatus.NoMoreRuns:
					case V1Interop.TaskStatus.Terminated:
						return TaskState.Ready;
					case V1Interop.TaskStatus.Running:
						return TaskState.Running;
					case V1Interop.TaskStatus.Disabled:
						return TaskState.Disabled;
					case V1Interop.TaskStatus.NotScheduled:
					case V1Interop.TaskStatus.NoTriggers:
					case V1Interop.TaskStatus.NoTriggerTime:
					default:
						return TaskState.Unknown;
				}
			}
		}

		public bool Enabled
		{
			get
			{
				if (v2Task != null)
					return v2Task.Enabled;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.Disabled) != V1Interop.TaskFlags.Disabled;
			}
			set
			{
				if (v2Task != null)
					v2Task.Enabled = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (!value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.Disabled);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.Disabled);
				}
			}
		}

		public RunningTask Run(object parameters)
		{
			if (v2Task != null)
				return new RunningTask(v2Task.Run(parameters));

			v1Task.Run();
			return new RunningTask(this);
		}

		public RunningTask RunEx(object parameters, int flags, int sessionID, string user)
		{
			if (v2Task != null)
				return new RunningTask(v2Task.RunEx(parameters, flags, sessionID, user));
			throw new NotSupportedException();
		}

		/*public Microsoft.Win32.TaskScheduler.InternalV2.IRunningTaskCollection GetInstances(int flags)
		{
			if (v2Task != null)
				return v2Task.GetInstances(flags);
			throw new NotSupportedException();
		}*/

		public DateTime LastRunTime
		{
			get
			{
				if (v2Task != null)
					return v2Task.LastRunTime;
				return v1Task.GetMostRecentRunTime();
			}
		}

		public int LastTaskResult
		{
			get
			{
				if (v2Task != null)
					return v2Task.LastTaskResult;
				return (int)v1Task.GetExitCode();
			}
		}

		public int NumberOfMissedRuns
		{
			get
			{
				if (v2Task != null)
					return v2Task.NumberOfMissedRuns;

				throw new NotSupportedException();
			}
		}

		public DateTime NextRunTime
		{
			get
			{
				if (v2Task != null)
					return v2Task.NextRunTime;
				return v1Task.GetNextRunTime();
			}
		}

		public TaskDefinition Definition
		{
			get
			{
				if (v2Task != null)
					return new TaskDefinition(v2Task.Definition);
				TaskDefinition td = new TaskDefinition(v1Task, this.Name);
				// TODO: Populate td
				return td;
			}
		}

		public string Xml
		{
			get
			{
				if (v2Task != null)
					return v2Task.Xml;

				throw new NotSupportedException();
			}
		}

		public string GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Task != null)
				return v2Task.GetSecurityDescriptor((int)includeSections);

			throw new NotSupportedException();
		}

		public void SetSecurityDescriptorSddlForm(string sddl, System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Task != null)
				v2Task.SetSecurityDescriptor(sddl, (int)includeSections);

			throw new NotSupportedException();
		}

		public void Stop()
		{
			if (v2Task != null)
				v2Task.Stop(0);
			v1Task.Terminate();
		}

		public DateTime[] GetRunTimes(DateTime start, DateTime end)
		{
			if (v2Task != null)
			{
				TaskScheduler.V1Interop.SystemTime stStart = new TaskScheduler.V1Interop.SystemTime(start);
				TaskScheduler.V1Interop.SystemTime stEnd = new TaskScheduler.V1Interop.SystemTime(end);
				uint count;
				IntPtr runTimes = IntPtr.Zero, st;
				v2Task.GetRunTimes(stStart, stEnd, out count, ref runTimes);
				DateTime[] ret = new DateTime[count];
				for (int i = 0; i < count; i++)
				{
					st = Marshal.ReadIntPtr(runTimes, i * IntPtr.Size);
					ret[i] = (TaskScheduler.V1Interop.SystemTime)Marshal.PtrToStructure(st, typeof(TaskScheduler.V1Interop.SystemTime));
					Marshal.FreeCoTaskMem(st);
				}
				Marshal.FreeCoTaskMem(runTimes);
				return ret;
			}
			throw new NotSupportedException();
		}
	}

	public class RunningTask : IDisposable
	{
		private Task v1Task;
		private TaskScheduler.V2Interop.IRunningTask v2Task;

		internal RunningTask(TaskScheduler.V2Interop.IRunningTask iTask)
		{
			v2Task = iTask;
		}

		internal RunningTask(Task iTask)
		{
			v1Task = iTask;
		}

		public void Dispose()
		{
			v1Task = null;
			if (v2Task != null) Marshal.ReleaseComObject(v2Task);
		}

		public string Name
		{
			get
			{
				if (v2Task != null)
					return v2Task.Name;
				return v1Task.Name;
			}
		}

		public Guid InstanceGuid
		{
			get
			{
				if (v2Task != null)
					return new Guid(v2Task.InstanceGuid);
				return Guid.Empty;
			}
		}

		public string Path
		{
			get
			{
				if (v2Task != null)
					return v2Task.Path;
				return v1Task.Path;
			}
		}

		public TaskState State
		{
			get
			{
				if (v2Task != null)
					return v2Task.State;
				return v1Task.State;
			}
		}

		public string CurrentAction
		{
			get
			{
				if (v2Task != null)
					return v2Task.CurrentAction;
				throw new NotSupportedException();
			}
		}

		public void Stop()
		{
			if (v2Task != null)
				v2Task.Stop();
			v1Task.v1Task.Terminate();
		}

		public void Refresh()
		{
			if (v2Task != null)
				v2Task.Refresh();
		}

		public uint EnginePID
		{
			get
			{
				if (v2Task != null)
					return v2Task.EnginePID;
				throw new NotSupportedException();
			}
		}
	}
}
