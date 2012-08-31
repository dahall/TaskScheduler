using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	#region Enumerations

	/// <summary>Defines what versions of Task Scheduler or the AT command that the task is compatible with.</summary>
	public enum TaskCompatibility
	{
		/// <summary>The task is compatible with the AT command.</summary>
		AT,
		/// <summary>The task is compatible with Task Scheduler 1.0.</summary>
		V1,
		/// <summary>The task is compatible with Task Scheduler 2.0.</summary>
		V2,
		/// <summary>The task is compatible with Task Scheduler 2.1.</summary>
		V2_1,
		/// <summary>The task is compatible with Task Scheduler 2.2.</summary>
		V2_2
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

	public enum TaskPrincipalPrivilege
	{
		/// <summary>Required to create a primary token. User Right: Create a token object.</summary>
		SeCreateTokenPrivilege = 1,
		/// <summary>Required to assign the primary token of a process. User Right: Replace a process-level token.</summary>
		SeAssignPrimaryTokenPrivilege,
		/// <summary>Required to lock physical pages in memory. User Right: Lock pages in memory. </summary>
		SeLockMemoryPrivilege,
		/// <summary>Required to increase the quota assigned to a process. User Right: Adjust memory quotas for a process.</summary>
		SeIncreaseQuotaPrivilege,
		/// <summary>Required to read unsolicited input from a terminal device. User Right: Not applicable.</summary>
		SeUnsolicitedInputPrivilege,
		/// <summary>Required to create a computer account. User Right: Add workstations to domain. </summary>
		SeMachineAccountPrivilege,
		/// <summary>This privilege identifies its holder as part of the trusted computer base. Some trusted protected subsystems are granted this privilege. User Right: Act as part of the operating system.</summary>
		SeTcbPrivilege,
		/// <summary>Required to perform a number of security-related functions, such as controlling and viewing audit messages. This privilege identifies its holder as a security operator. User Right: Manage auditing and the security log.</summary>
		SeSecurityPrivilege,
		/// <summary>Required to take ownership of an object without being granted discretionary access. This privilege allows the owner value to be set only to those values that the holder may legitimately assign as the owner of an object. User Right: Take ownership of files or other objects.</summary>
		SeTakeOwnershipPrivilege,
		/// <summary>Required to load or unload a device driver. User Right: Load and unload device drivers.</summary>
		SeLoadDriverPrivilege,
		/// <summary>Required to gather profiling information for the entire system. User Right: Profile system performance.</summary>
		SeSystemProfilePrivilege,
		/// <summary>Required to modify the system time. User Right: Change the system time. </summary>
		SeSystemtimePrivilege,
		/// <summary>Required to gather profiling information for a single process. User Right: Profile single process.</summary>
		SeProfileSingleProcessPrivilege,
		/// <summary>Required to increase the base priority of a process. User Right: Increase scheduling priority.</summary>
		SeIncreaseBasePriorityPrivilege,
		/// <summary>Required to create a paging file. User Right: Create a pagefile. </summary>
		SeCreatePagefilePrivilege,
		/// <summary>Required to create a permanent object. User Right: Create permanent shared objects.</summary>
		SeCreatePermanentPrivilege,
		/// <summary>Required to perform backup operations. This privilege causes the system to grant all read access control to any file, regardless of the access control list (ACL) specified for the file. Any access request other than read is still evaluated with the ACL. This privilege is required by the RegSaveKey and RegSaveKeyExfunctions. The following access rights are granted if this privilege is held: READ_CONTROL, ACCESS_SYSTEM_SECURITY, FILE_GENERIC_READ, FILE_TRAVERSE. User Right: Back up files and directories.</summary>
		SeBackupPrivilege,
		/// <summary>Required to perform restore operations. This privilege causes the system to grant all write access control to any file, regardless of the ACL specified for the file. Any access request other than write is still evaluated with the ACL. Additionally, this privilege enables you to set any valid user or group security identifier (SID) as the owner of a file. This privilege is required by the RegLoadKey function. The following access rights are granted if this privilege is held: WRITE_DAC, WRITE_OWNER, ACCESS_SYSTEM_SECURITY, FILE_GENERIC_WRITE, FILE_ADD_FILE, FILE_ADD_SUBDIRECTORY, DELETE. User Right: Restore files and directories.</summary>
		SeRestorePrivilege,
		/// <summary>Required to shut down a local system. User Right: Shut down the system. </summary>
		SeShutdownPrivilege,
		/// <summary>Required to debug and adjust the memory of a process owned by another account. User Right: Debug programs.</summary>
		SeDebugPrivilege,
		/// <summary>Required to generate audit-log entries. Give this privilege to secure servers. User Right: Generate security audits.</summary>
		SeAuditPrivilege,
		/// <summary>Required to modify the nonvolatile RAM of systems that use this type of memory to store configuration information. User Right: Modify firmware environment values.</summary>
		SeSystemEnvironmentPrivilege,
		/// <summary>Required to receive notifications of changes to files or directories. This privilege also causes the system to skip all traversal access checks. It is enabled by default for all users. User Right: Bypass traverse checking.</summary>
		SeChangeNotifyPrivilege,
		/// <summary>Required to shut down a system by using a network request. User Right: Force shutdown from a remote system.</summary>
		SeRemoteShutdownPrivilege,
		/// <summary>Required to undock a laptop. User Right: Remove computer from docking station. </summary>
		SeUndockPrivilege,
		/// <summary>Required for a domain controller to use the LDAP directory synchronization services. This privilege allows the holder to read all objects and properties in the directory, regardless of the protection on the objects and properties. By default, it is assigned to the Administrator and LocalSystem accounts on domain controllers. User Right: Synchronize directory service data.</summary>
		SeSyncAgentPrivilege,
		/// <summary>Required to mark user and computer accounts as trusted for delegation. User Right: Enable computer and user accounts to be trusted for delegation.</summary>
		SeEnableDelegationPrivilege,
		/// <summary>Required to enable volume management privileges. User Right: Manage the files on a volume.</summary>
		SeManageVolumePrivilege,
		/// <summary>Required to impersonate. User Right: Impersonate a client after authentication. Windows XP/2000: This privilege is not supported. Note that this value is supported starting with Windows Server 2003, Windows XP with SP2, and Windows 2000 with SP4.</summary>
		SeImpersonatePrivilege,
		/// <summary>Required to create named file mapping objects in the global namespace during Terminal Services sessions. This privilege is enabled by default for administrators, services, and the local system account. User Right: Create global objects. Windows XP/2000: This privilege is not supported. Note that this value is supported starting with Windows Server 2003, Windows XP with SP2, and Windows 2000 with SP4.</summary>
		SeCreateGlobalPrivilege,
		/// <summary>Required to access Credential Manager as a trusted caller. User Right: Access Credential Manager as a trusted caller.</summary>
		SeTrustedCredManAccessPrivilege,
		/// <summary>Required to modify the mandatory integrity level of an object. User Right: Modify an object label.</summary>
		SeRelabelPrivilege,
		/// <summary>Required to allocate more memory for applications that run in the context of users. User Right: Increase a process working set.</summary>
		SeIncreaseWorkingSetPrivilege,
		/// <summary>Required to adjust the time zone associated with the computer's internal clock. User Right: Change the time zone.</summary>
		SeTimeZonePrivilege,
		/// <summary>Required to create a symbolic link. User Right: Create symbolic links.</summary>
		SeCreateSymbolicLinkPrivilege
	}

	/// <summary>Defines the types of process security identifier (SID) that can be used by tasks. These changes are used to specify the type of process SID in the IPrincipal2 interface.</summary>
	public enum TaskProcessTokenSidType
	{
		/// <summary>No changes will be made to the process token groups list.</summary>
		None = 0,
		/// <summary>A task SID that is derived from the task name will be added to the process token groups list, and the token default discretionary access control list (DACL) will be modified to allow only the task SID and local system full control and the account SID read control.</summary>
		Unrestricted = 1,
		/// <summary>A Task Scheduler will apply default settings to the task process.</summary>
		Default = 2
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
		[XmlEnum("LeastPrivilege")]
		LUA,
		/// <summary>Tasks will be run with the highest privileges.</summary>
		[XmlEnum("HighestAvailable")]
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

	/***** WAITING TO DETERMINE USE CASE *****
	/// <summary>
	/// Success and error codes that some methods will expose through <see cref="COMExcpetion"/>.
	/// </summary>
	public enum TaskResultCode
	{
		/// <summary>The task is ready to run at its next scheduled time.</summary>
		TaskReady = 0x00041300,
		/// <summary>The task is currently running.</summary>
		TaskRunning = 0x00041301,
		/// <summary>The task will not run at the scheduled times because it has been disabled.</summary>
		TaskDisabled = 0x00041302,
		/// <summary>The task has not yet run.</summary>
		TaskHasNotRun = 0x00041303,
		/// <summary>There are no more runs scheduled for this task.</summary>
		TaskNoMoreRuns = 0x00041304,
		/// <summary>One or more of the properties that are needed to run this task on a schedule have not been set.</summary>
		TaskNotScheduled = 0x00041305,
		/// <summary>The last run of the task was terminated by the user.</summary>
		TaskTerminated = 0x00041306,
		/// <summary>Either the task has no triggers or the existing triggers are disabled or not set.</summary>
		TaskNoValidTriggers = 0x00041307,
		/// <summary>Event triggers do not have set run times.</summary>
		EventTrigger = 0x00041308,
		/// <summary>A task's trigger is not found.</summary>
		TriggerNotFound = 0x80041309,
		/// <summary>One or more of the properties required to run this task have not been set.</summary>
		TaskNotReady = 0x8004130A,
		/// <summary>There is no running instance of the task.</summary>
		TaskNotRunning = 0x8004130B,
		/// <summary>The Task Scheduler service is not installed on this computer.</summary>
		ServiceNotInstalled = 0x8004130C,
		/// <summary>The task object could not be opened.</summary>
		CannotOpenTask = 0x8004130D,
		/// <summary>The object is either an invalid task object or is not a task object.</summary>
		InvalidTask = 0x8004130E,
		/// <summary>No account information could be found in the Task Scheduler security database for the task indicated.</summary>
		AccountInformationNotSet = 0x8004130F,
		/// <summary>Unable to establish existence of the account specified.</summary>
		AccountNameNotFound = 0x80041310,
		/// <summary>Corruption was detected in the Task Scheduler security database; the database has been reset.</summary>
		AccountDbaseCorrupt = 0x80041311,
		/// <summary>Task Scheduler security services are available only on Windows NT.</summary>
		NoSecurityServices = 0x80041312,
		/// <summary>The task object version is either unsupported or invalid.</summary>
		UnknownObjectVersion = 0x80041313,
		/// <summary>The task has been configured with an unsupported combination of account settings and run time options.</summary>
		UnsupportedAccountOption = 0x80041314,
		/// <summary>The Task Scheduler Service is not running.</summary>
		ServiceNotRunning = 0x80041315,
		/// <summary>The task XML contains an unexpected node.</summary>
		UnexpectedNode = 0x80041316,
		/// <summary>The task XML contains an element or attribute from an unexpected namespace.</summary>
		Namespace = 0x80041317,
		/// <summary>The task XML contains a value which is incorrectly formatted or out of range.</summary>
		InvalidValue = 0x80041318,
		/// <summary>The task XML is missing a required element or attribute.</summary>
		MissingNode = 0x80041319,
		/// <summary>The task XML is malformed.</summary>
		MalformedXml = 0x8004131A,
		/// <summary>The task is registered, but not all specified triggers will start the task.</summary>
		SomeTriggersFailed = 0x0004131B,
		/// <summary>The task is registered, but may fail to start. Batch logon privilege needs to be enabled for the task principal.</summary>
		BatchLogonProblem = 0x0004131C,
		/// <summary>The task XML contains too many nodes of the same type.</summary>
		TooManyNodes = 0x8004131D,
		/// <summary>The task cannot be started after the trigger end boundary.</summary>
		PastEndBoundary = 0x8004131E,
		/// <summary>An instance of this task is already running.</summary>
		AlreadyRunning = 0x8004131F,
		/// <summary>The task will not run because the user is not logged on.</summary>
		UserNotLoggedOn = 0x80041320,
		/// <summary>The task image is corrupt or has been tampered with.</summary>
		InvalidTaskHash = 0x80041321,
		/// <summary>The Task Scheduler service is not available.</summary>
		ServiceNotAvailable = 0x80041322,
		/// <summary>The Task Scheduler service is too busy to handle your request. Please try again later.</summary>
		ServiceTooBusy = 0x80041323,
		/// <summary>The Task Scheduler service attempted to run the task, but the task did not run due to one of the constraints in the task definition.</summary>
		TaskAttempted = 0x80041324,
		/// <summary>The Task Scheduler service has asked the task to run.</summary>
		TaskQueued = 0x00041325,
		/// <summary>The task is disabled.</summary>
		TaskDisabled = 0x80041326,
		/// <summary>The task has properties that are not compatible with earlier versions of Windows.</summary>
		TaskNotV1Compatible = 0x80041327,
		/// <summary>The task settings do not allow the task to start on demand.</summary>
		StartOnDemand = 0x80041328,
	}
	*/

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

	#endregion Enumerations

	/// <summary>
	/// Specifies how the Task Scheduler performs tasks when the computer is in an idle condition. For information about idle conditions, see Task Idle Conditions.
	/// </summary>
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

		/// <summary>
		/// Gets or sets a value that indicates the amount of time that the computer must be in an idle state before the task is run.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:10:00")]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates whether the task is restarted when the computer cycles into an idle condition more than once.
		/// </summary>
		[DefaultValue(false)]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler will terminate the task if the idle condition ends before the task is completed.
		/// </summary>
		[DefaultValue(true)]
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

		/// <summary>
		/// Gets or sets a value that indicates the amount of time that the Task Scheduler will wait for an idle condition to occur.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "01:00:00")]
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

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
			v1Task = null;
		}
	}

	/// <summary>
	/// Specifies the task settings the Task scheduler will use to start task during Automatic maintenance.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	public sealed class MaintenanceSettings : IDisposable
	{
		private V2Interop.IMaintenanceSettings v2Settings = null;

		internal MaintenanceSettings(V2Interop.ITaskSettings3 iSettings)
		{
			if (iSettings != null)
			{
				v2Settings = iSettings.MaintenanceSettings;
				if (v2Settings == null)
					v2Settings = iSettings.CreateMaintenanceSettings();
			}
		}

		/// <summary>
		/// Gets or sets the amount of time after which the Task scheduler attempts to run the task during emergency Automatic maintenance, if the task failed to complete during regular Automatic maintenance. The minimum value is one day. The value of the <see cref="Deadline"/> property should be greater than the value of the <see cref="Period"/> property. If the deadline is not specified the task will not be started during emergency Automatic maintenance. 
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Deadline
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.Deadline);
				return TimeSpan.Zero;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Deadline = Task.TimeSpanToString(value);
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_2);
			}
		}

		/// <summary>
		/// Gets or sets a valud indicating whether the Task Scheduler must start the task during the Automatic maintenance in exclusive mode. The exclusivity is guaranteed only between other maintenance tasks and doesn't grant any ordering priority of the task. If exclusivity is not specified, the task is started in parallel with other maintenance tasks.
		/// </summary>
		[DefaultValue(false)]
		public bool Exclusive
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Exclusive;
				return false;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Exclusive = value;
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_2);
			}
		}

		/// <summary>
		/// Gets or sets the amount of time the task needs to be started during Automatic maintenance. The minimum value is one minute.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Period
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.Period);
				return TimeSpan.Zero;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Period = Task.TimeSpanToString(value);
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_2);
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
		}
	}

	/// <summary>
	/// Provides the settings that the Task Scheduler service uses to obtain a network profile.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	public sealed class NetworkSettings : IDisposable
	{
		private V2Interop.INetworkSettings v2Settings = null;

		internal NetworkSettings(V2Interop.INetworkSettings iSettings)
		{
			v2Settings = iSettings;
		}

		/// <summary>
		/// Gets or sets a GUID value that identifies a network profile.
		/// </summary>
		[DefaultValue(typeof(Guid), "00000000-0000-0000-0000-000000000000")]
		public Guid Id
		{
			get
			{
				string id = null;
				if (v2Settings != null)
					id = v2Settings.Id;
				return string.IsNullOrEmpty(id) ? Guid.Empty : new Guid(id);
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Id = value == Guid.Empty ? null : value.ToString();
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the name of a network profile. The name is used for display purposes.
		/// </summary>
		[DefaultValue(null)]
		public string Name
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Name;
				return null;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Name = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
		}
	}

	/// <summary>
	/// Provides the methods to get information from and control a running task.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	public sealed class RunningTask : Task
	{
		private TaskScheduler.V2Interop.IRunningTask v2RunningTask;

		internal RunningTask(TaskService svc, V2Interop.IRegisteredTask iTask, V2Interop.IRunningTask iRunningTask)
			: base(svc, iTask)
		{
			v2RunningTask = iRunningTask;
		}

		internal RunningTask(TaskService svc, V1Interop.ITask iTask)
			: base(svc, iTask)
		{
		}

		/// <summary>
		/// Gets the name of the current action that the running task is performing.
		/// </summary>
		public string CurrentAction
		{
			get
			{
				if (v2RunningTask != null)
					return v2RunningTask.CurrentAction;
				return base.v1Task.GetApplicationName();
			}
		}

		/// <summary>
		/// Gets the process ID for the engine (process) which is running the task.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public uint EnginePID
		{
			get
			{
				if (v2RunningTask != null)
					return v2RunningTask.EnginePID;
				throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets the GUID identifier for this instance of the task.
		/// </summary>
		public Guid InstanceGuid
		{
			get
			{
				if (v2RunningTask != null)
					return new Guid(v2RunningTask.InstanceGuid);
				return Guid.Empty;
			}
		}

		/// <summary>
		/// Gets the operational state of the running task.
		/// </summary>
		public override TaskState State
		{
			get
			{
				if (v2RunningTask != null)
					return v2RunningTask.State;
				return base.State;
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public new void Dispose()
		{
			base.Dispose();
			if (v2RunningTask != null) Marshal.ReleaseComObject(v2RunningTask);
		}

		/// <summary>
		/// Refreshes all of the local instance variables of the task.
		/// </summary>
		public void Refresh()
		{
			if (v2RunningTask != null)
				v2RunningTask.Refresh();
		}
	}

	/// <summary>
	/// Provides the methods that are used to run the task immediately, get any running instances of the task, get or set the credentials that are used to register the task, and the properties that describe the task.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	public class Task : IDisposable
	{
		internal V1Interop.ITask v1Task;

		private static readonly DateTime v2InvalidDate = new DateTime(1899, 12, 30);

		private V2Interop.IRegisteredTask v2Task;
		private TaskDefinition myTD;

		internal Task(TaskService svc, TaskScheduler.V1Interop.ITask iTask)
		{
			this.TaskService = svc;
			v1Task = iTask;
		}

		internal Task(TaskService svc, TaskScheduler.V2Interop.IRegisteredTask iTask)
		{
			this.TaskService = svc;
			v2Task = iTask;
		}

		/// <summary>
		/// Gets the definition of the task.
		/// </summary>
		public TaskDefinition Definition
		{
			get
			{
				if (myTD == null)
				{
					if (v2Task != null)
						myTD = new TaskDefinition(v2Task.Definition);
					else
						myTD = new TaskDefinition(v1Task, this.Name);
				}
				return myTD;
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates if the registered task is enabled.
		/// </summary>
		/// <remarks>As of version 1.8.1, under V1 systems (prior to Vista), this method will immediately set the enabled property and re-save the current task. If changes have been made to the <see cref="TaskDefinition"/>, then those changes will be saved.</remarks>
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
					this.Definition.V1Save(null);
				}
			}
		}

		/// <summary>
		/// Gets the time the registered task was last run.
		/// </summary>
		/// <value>Returns <see cref="DateTime.MinValue"/> if there are no prior run times.</value>
		public DateTime LastRunTime
		{
			get
			{
				if (v2Task != null)
				{
					DateTime dt = v2Task.LastRunTime;
					return dt == v2InvalidDate ? DateTime.MinValue : dt;
				}
				return v1Task.GetMostRecentRunTime();
			}
		}

		/// <summary>
		/// Gets the results that were returned the last time the registered task was run.
		/// </summary>
		public int LastTaskResult
		{
			get
			{
				if (v2Task != null)
					return v2Task.LastTaskResult;
				return (int)v1Task.GetExitCode();
			}
		}

		/// <summary>
		/// Gets the name of the registered task.
		/// </summary>
		public string Name
		{
			get
			{
				if (v2Task != null)
					return v2Task.Name;
				return System.IO.Path.GetFileNameWithoutExtension(GetV1Path(v1Task));
			}
		}

		/// <summary>
		/// Gets the time when the registered task is next scheduled to run.
		/// </summary>
		/// <value>Returns <see cref="DateTime.MinValue"/> if there are no future run times.</value>
		/// <remarks>
		/// Potentially breaking change in release 1.8.2. For Task Scheduler 2.0, the return value prior to 1.8.2 would be Dec 30, 1899
		/// if there were no future run times. For 1.0, that value would have been <c>DateTime.MinValue</c>. In release 1.8.2 and later, all 
		/// versions will return <c>DateTime.MinValue</c> if there are no future run times. While this is different from the native 2.0
		/// library, it was deemed more appropriate to have consistency between the two libraries and with other .NET libraries.
		/// </remarks>
		public DateTime NextRunTime
		{
			get
			{
				if (v2Task != null)
				{
					DateTime ret = v2Task.NextRunTime;
					if (ret == DateTime.MinValue || ret == v2InvalidDate)
					{
						DateTime[] nrts = this.GetRunTimes(DateTime.Now, DateTime.MaxValue, 1);
						ret = nrts.Length > 0 ? nrts[0] : DateTime.MinValue;
					}
					return ret == v2InvalidDate ? DateTime.MinValue : ret;
				}
				return v1Task.GetNextRunTime();
			}
		}

		/// <summary>
		/// Gets the number of times the registered task has missed a scheduled run.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public int NumberOfMissedRuns
		{
			get
			{
				if (v2Task != null)
					return v2Task.NumberOfMissedRuns;

				throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets the path to where the registered task is stored.
		/// </summary>
		public string Path
		{
			get
			{
				if (v2Task != null)
					return v2Task.Path;
				return GetV1Path(v1Task);
			}
		}

		/// <summary>
		/// Gets or sets the security descriptor for the task.
		/// </summary>
		/// <value>The security descriptor.</value>
		public System.Security.AccessControl.GenericSecurityDescriptor SecurityDescriptor
		{
			get
			{
				string sddl = GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections.All);
				return new System.Security.AccessControl.RawSecurityDescriptor(sddl);
			}
			set
			{
				SetSecurityDescriptorSddlForm(value.GetSddlForm(System.Security.AccessControl.AccessControlSections.All), System.Security.AccessControl.AccessControlSections.All);
			}
		}

		/// <summary>
		/// Gets the operational state of the registered task.
		/// </summary>
		public virtual TaskState State
		{
			get
			{
				if (v2Task != null)
					return v2Task.State;

				V1Reactivate();
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

		/// <summary>
		/// Gets or sets the <see cref="TaskService"/> that manages this task.
		/// </summary>
		/// <value>The task service.</value>
		public TaskService TaskService
		{
			get; private set;
		}

		/// <summary>
		/// Gets the XML-formatted registration information for the registered task.
		/// </summary>
		public string Xml
		{
			get
			{
				if (v2Task != null)
					return v2Task.Xml;

				return this.Definition.XmlText;
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Task != null)
				Marshal.ReleaseComObject(v2Task);
			v1Task = null;
		}

		/// <summary>
		/// Exports the task to the specified file in XML.
		/// </summary>
		/// <param name="outputFileName">Name of the output file.</param>
		public void Export(string outputFileName)
		{
			System.IO.File.WriteAllText(outputFileName, this.Xml, System.Text.Encoding.Unicode);
		}

		/// <summary>
		/// Gets all instances of the currently running registered task.
		/// </summary>
		/// <returns>A <see cref="RunningTaskCollection"/> with all instances of current task.</returns>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public RunningTaskCollection GetInstances()
		{
			if (v2Task != null)
				return new RunningTaskCollection(this.TaskService, v2Task.GetInstances(0));
			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Gets the times that the registered task is scheduled to run during a specified time.
		/// </summary>
		/// <param name="start">The starting time for the query.</param>
		/// <param name="end">The ending time for the query.</param>
		/// <param name="count">The requested number of runs. A value of 0 will return all times requested.</param>
		/// <returns>The scheduled times that the task will run.</returns>
		public DateTime[] GetRunTimes(DateTime start, DateTime end, uint count = 0)
		{
			const ushort TASK_MAX_RUN_TIMES = 1440;

			TaskScheduler.V1Interop.SystemTime stStart = new TaskScheduler.V1Interop.SystemTime(start);
			TaskScheduler.V1Interop.SystemTime stEnd = new TaskScheduler.V1Interop.SystemTime(end);
			IntPtr runTimes = IntPtr.Zero, st;
			if (v2Task != null)
				v2Task.GetRunTimes(ref stStart, ref stEnd, ref count, ref runTimes);
			else
			{
				ushort count1 = (count > 0 && count <= TASK_MAX_RUN_TIMES) ? (ushort)count : TASK_MAX_RUN_TIMES;
				v1Task.GetRunTimes(ref stStart, ref stEnd, ref count1, ref runTimes);
				count = count1;
			}
			DateTime[] ret = new DateTime[count];
			int stSize = Marshal.SizeOf(typeof(TaskScheduler.V1Interop.SystemTime));
			for (int i = 0; i < count; i++)
			{
				st = new IntPtr(runTimes.ToInt64() + (i * stSize));
				ret[i] = (TaskScheduler.V1Interop.SystemTime)Marshal.PtrToStructure(st, typeof(TaskScheduler.V1Interop.SystemTime));
			}
			Marshal.FreeCoTaskMem(runTimes);
			System.Diagnostics.Debug.WriteLine(string.Format("Task.GetRunTimes ({0}): Returned {1} items from {2} to {3}.", v2Task != null ? "V2" : "V1", count, stStart, stEnd));
			return ret;
		}

		/// <summary>
		/// Gets the security descriptor for the task. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="includeSections">Section(s) of the security descriptor to return.</param>
		/// <returns>The security descriptor for the task.</returns>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public string GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Task != null)
				return v2Task.GetSecurityDescriptor((int)includeSections);

			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Updates the task with any changes made to the <see cref="Definition"/> by calling <see cref="TaskFolder.RegisterTaskDefinition(string, TaskDefinition)"/> from the currently registered folder using the currently registered name.
		/// </summary>
		/// <exception cref="System.Security.SecurityException">Thrown if task was previously registered with a password.</exception>
		public void RegisterChanges()
		{
			if (this.Definition.Principal.LogonType == TaskLogonType.InteractiveTokenOrPassword || this.Definition.Principal.LogonType == TaskLogonType.Password)
				throw new System.Security.SecurityException("Tasks which have been registered previously with stored passwords must use the TaskFolder.RegisterTaskDefinition method for updates.");
			TaskService.GetFolder(System.IO.Path.GetDirectoryName(this.Path)).RegisterTaskDefinition(this.Name, this.Definition);
		}

		/// <summary>
		/// Runs the registered task immediately.
		/// </summary>
		/// <param name="parameters">The parameters used as values in the task actions.</param>
		/// <returns>A <see cref="RunningTask"/> instance that defines the new instance of the task.</returns>
		public RunningTask Run(params string[] parameters)
		{
			if (v2Task != null)
			{
				if (parameters != null && parameters.Length > 32)
					throw new ArgumentOutOfRangeException("parameters", "A maximum of 32 values is allowed.");
				return new RunningTask(this.TaskService, this.v2Task, v2Task.Run(parameters));
			}

			v1Task.Run();
			return new RunningTask(this.TaskService, this.v1Task);
		}

		/// <summary>
		/// Runs the registered task immediately using specified flags and a session identifier.
		/// </summary>
		/// <param name="flags">Defines how the task is run.</param>
		/// <param name="sessionID">The terminal server session in which you want to start the task.</param>
		/// <param name="user">The user for which the task runs.</param>
		/// <param name="parameters">The parameters used as values in the task actions.</param>
		/// <returns>A <see cref="RunningTask"/> instance that defines the new instance of the task.</returns>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public RunningTask RunEx(TaskRunFlags flags, int sessionID, string user, params string[] parameters)
		{
			if (v2Task != null)
				return new RunningTask(this.TaskService, this.v2Task, v2Task.RunEx(parameters, (int)flags, sessionID, user));
			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Sets the security descriptor for the task. Not available to Task Scheduler 1.0.
		/// </summary>
		/// <param name="sddlForm">The security descriptor for the task.</param>
		/// <param name="includeSections">Section(s) of the security descriptor to set.</param>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		public void SetSecurityDescriptorSddlForm(string sddlForm, System.Security.AccessControl.AccessControlSections includeSections)
		{
			if (v2Task != null)
				v2Task.SetSecurityDescriptor(sddlForm, (int)includeSections);

			throw new NotV1SupportedException();
		}

		/// <summary>
		/// Dynamically tries to load the assembly for the editor and displays it as editable for this task.
		/// </summary>
		/// <returns><c>true</c> if editor returns with Ok response; <c>false</c> otherwise.</returns>
		/// <remarks>The Microsoft.Win32.TaskSchedulerEditor.dll assembly must reside in the same directory as the Microsoft.Win32.TaskScheduler.dll or in the GAC.</remarks>
		public bool ShowEditor()
		{
			try
			{
				System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom("Microsoft.Win32.TaskSchedulerEditor.dll");
				if (asm != null)
				{
					Type t = asm.GetType("Microsoft.Win32.TaskScheduler.TaskEditDialog", false, false);
					if (t != null)
					{
						object o = Activator.CreateInstance(t, this, true, true);
						if (o != null)
						{
							System.Reflection.MethodInfo mi = t.GetMethod("ShowDialog", Type.EmptyTypes);
							if (mi != null)
							{
								object r = mi.Invoke(o, null);
								int ir = Convert.ToInt32(r);
								return ir == 1;
							}
						}
					}
				}
			}
			catch { }
			return false;
		}

		/// <summary>
		/// Shows the property page for the task (v1.0 only).
		/// </summary>
		public void ShowPropertyPage()
		{
			if (v1Task != null)
				v1Task.EditWorkItem(IntPtr.Zero, 0);
			else
				throw new NotV2SupportedException();
		}

		/// <summary>
		/// Stops the registered task immediately.
		/// </summary>
		public void Stop()
		{
			if (v2Task != null)
				v2Task.Stop(0);
			else
				v1Task.Terminate();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.Name;
		}

		internal void V1Reactivate()
		{
			V1Interop.ITask iTask = TaskService.GetTask(this.TaskService.v1TaskScheduler, GetV1Path(v1Task));
			if (iTask != null)
				v1Task = iTask;
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

		internal static TimeSpan StringToTimeSpan(string input)
		{
			if (!string.IsNullOrEmpty(input))
				try { return System.Xml.XmlConvert.ToTimeSpan(input); } catch { }
			return TimeSpan.Zero;
		}

		internal static string TimeSpanToString(TimeSpan span)
		{
			if (span != TimeSpan.Zero)
				try { return System.Xml.XmlConvert.ToString(span); } catch { }
			return null;
		}
	}

	/// <summary>
	/// Defines all the components of a task, such as the task settings, triggers, actions, and registration information.
	/// </summary>
	[XmlRootAttribute("Task", Namespace = TaskDefinition.tns, IsNullable = false)]
	[XmlSchemaProvider("GetV1SchemaFile")]
	public sealed class TaskDefinition : IXmlSerializable
	{
		internal const string tns = "http://schemas.microsoft.com/windows/2004/02/mit/task";

		internal string v1Name = string.Empty;
		internal V1Interop.ITask v1Task = null;
		internal V2Interop.ITaskDefinition v2Def = null;

		private ActionCollection actions = null;
		private TaskPrincipal principal = null;
		private TaskRegistrationInfo regInfo = null;
		private TaskSettings settings = null;
		private TriggerCollection triggers = null;

		internal TaskDefinition(V1Interop.ITask iTask, string name)
		{
			v1Task = iTask;
			v1Name = name;
		}

		internal TaskDefinition(V2Interop.ITaskDefinition iDef)
		{
			v2Def = iDef;
		}

		/// <summary>
		/// Gets a collection of actions that are performed by the task.
		/// </summary>
		[XmlArrayItem(ElementName = "Exec", IsNullable = true, Type = typeof(ExecAction))]
		[XmlArray]
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

		/// <summary>
		/// Gets or sets the data that is associated with the task. This data is ignored by the Task Scheduler service, but is used by third-parties who wish to extend the task format.
		/// </summary>
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

		/// <summary>
		/// Gets the principal for the task that provides the security credentials for the task.
		/// </summary>
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

		/// <summary>
		/// Gets a class instance of registration information that is used to describe a task, such as the description of the task, the author of the task, and the date the task is registered.
		/// </summary>
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

		/// <summary>
		/// Gets the settings that define how the Task Scheduler service performs the task.
		/// </summary>
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

		/// <summary>
		/// Gets a collection of triggers that are used to start a task.
		/// </summary>
		[XmlArrayItem(ElementName = "BootTrigger", IsNullable = true, Type = typeof(BootTrigger))]
		[XmlArrayItem(ElementName = "CalendarTrigger", IsNullable = true, Type = typeof(CalendarTrigger))]
		[XmlArrayItem(ElementName = "IdleTrigger", IsNullable = true, Type = typeof(IdleTrigger))]
		[XmlArrayItem(ElementName = "LogonTrigger", IsNullable = true, Type = typeof(LogonTrigger))]
		[XmlArrayItem(ElementName = "TimeTrigger", IsNullable = true, Type = typeof(TimeTrigger))]
		[XmlArray]
		public TriggerCollection Triggers
		{
			get
			{
				if (triggers == null)
				{
					if (v2Def != null)
						triggers = new TriggerCollection(v2Def);
					else
						triggers = new TriggerCollection(v1Task);
				}
				return triggers;
			}
		}

		/// <summary>
		/// Gets or sets the XML-formatted definition of the task.
		/// </summary>
		[XmlIgnore]
		public string XmlText
		{
			get
			{
				if (v2Def != null)
					return v2Def.XmlText;
				return XmlSerializationHelper.WriteObjectToXmlText(this);
			}
			set
			{
				if (v2Def != null)
					v2Def.XmlText = value;
				else
					XmlSerializationHelper.ReadObjectFromXmlText(value, this);
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
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

		/// <summary>
		/// Gets the Xml Schema file for V1 tasks.
		/// </summary>
		/// <param name="xs">The <see cref="System.Xml.Schema.XmlSchemaSet"/> for V1 tasks.</param>
		/// <returns>An object containing the Xml Schema for V1 tasks.</returns>
		public static XmlSchemaComplexType GetV1SchemaFile(XmlSchemaSet xs)
		{
			XmlSchema schema = null;
			using (System.IO.Stream xsdFile = System.Reflection.Assembly.GetAssembly(typeof(TaskDefinition)).GetManifestResourceStream("Microsoft.Win32.TaskScheduler.V1.TaskSchedulerV1Schema.xsd"))
			{
				XmlSerializer schemaSerializer = new XmlSerializer(typeof(XmlSchema));
				schema = (XmlSchema)schemaSerializer.Deserialize(System.Xml.XmlReader.Create(xsdFile));
				xs.Add(schema);
			}

			// target namespace
			XmlQualifiedName name = new XmlQualifiedName("taskType", tns);
			XmlSchemaComplexType productType = (XmlSchemaComplexType)schema.SchemaTypes[name];

			return productType;
		}

		internal void V1Save(string newName)
		{
			if (v1Task != null)
			{
				this.Triggers.Bind();

				IPersistFile iFile = (IPersistFile)v1Task;
				if (string.IsNullOrEmpty(newName) || newName == v1Name)
				{
					try
					{
						iFile.Save(null, false);
						iFile = null;
						return;
					}
					catch { }

				}

				string path;
				iFile.GetCurFile(out path);
				System.IO.File.Delete(path);
				path = System.IO.Path.GetDirectoryName(path) + System.IO.Path.DirectorySeparatorChar + newName + System.IO.Path.GetExtension(path);
				System.IO.File.Delete(path);
				iFile.Save(path, true);
				iFile = null;
			}
		}

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("Task", tns);

			XmlSerializationHelper.ReadObjectProperties(reader, this);

			reader.ReadEndElement();

			if (this.Triggers.Count == 0 || this.Actions.Count != 1)
				throw new XmlException("Invalid XML stream. Task element must include a Triggers and an Actions element to form a valid task.");
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			XmlSerializationHelper.WriteObjectProperties(writer, this);
		}
	}

	/// <summary>
	/// Provides the security credentials for a principal. These security credentials define the security context for the tasks that are associated with the principal.
	/// </summary>
	[XmlRoot("Principals", Namespace = TaskDefinition.tns, IsNullable = true)]
	public sealed class TaskPrincipal : IXmlSerializable
	{
		private const string localSystemAcct = "SYSTEM";
		private V1Interop.ITask v1Task = null;
		private V2Interop.IPrincipal v2Principal;
		private V2Interop.IPrincipal2 v2Principal2;
		private TaskPrincipalPrivileges reqPriv;

		internal TaskPrincipal(V2Interop.IPrincipal iPrincipal)
		{
			v2Principal = iPrincipal;
			try { v2Principal2 = (V2Interop.IPrincipal2)v2Principal; }
			catch { }
		}

		internal TaskPrincipal(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		/// <summary>
		/// Gets or sets the name of the principal that is displayed in the Task Scheduler UI.
		/// </summary>
		[DefaultValue(null)]
		[XmlIgnore]
		public string DisplayName
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.DisplayName;
				return null;
			}
			set
			{
				if (v2Principal != null)
					v2Principal.DisplayName = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the user group that is required to run the tasks that are associated with the principal. Setting this property to something other than a null or empty string, will set the <see cref="UserId"/> property to NULL and will set the <see cref="LogonType"/> property to TaskLogonType.Group;
		/// </summary>
		[DefaultValue(null)]
		[XmlIgnore]
		public string GroupId
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.GroupId;
				return null;
			}
			set
			{

				if (v2Principal != null)
				{
					if (string.IsNullOrEmpty(value))
						value = null;
					else
					{
						v2Principal.UserId = null;
						v2Principal.LogonType = TaskLogonType.Group;
					}
					v2Principal.GroupId = value;
				}
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the principal.
		/// </summary>
		[DefaultValue(null)]
		[XmlAttribute(AttributeName = "id", DataType = "ID")]
		[XmlIgnore]
		public string Id
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.Id;
				return Properties.Resources.TaskDefaultPrincipal;
			}
			set
			{
				if (v2Principal != null)
					v2Principal.Id = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the security logon method that is required to run the tasks that are associated with the principal.
		/// </summary>
		[DefaultValue(typeof(TaskLogonType), "None")]
		public TaskLogonType LogonType
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.LogonType;
				if (this.UserId == localSystemAcct)
					return TaskLogonType.ServiceAccount;
				if ((v1Task.GetFlags() & V1Interop.TaskFlags.RunOnlyIfLoggedOn) == V1Interop.TaskFlags.RunOnlyIfLoggedOn)
					return TaskLogonType.InteractiveToken;
				return TaskLogonType.InteractiveTokenOrPassword;
			}
			set
			{
				if (v2Principal != null)
					v2Principal.LogonType = value;
				else
				{
					if (value == TaskLogonType.Group || value == TaskLogonType.None || value == TaskLogonType.S4U)
						throw new NotV1SupportedException();
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value == TaskLogonType.InteractiveToken)
						flags |= V1Interop.TaskFlags.RunOnlyIfLoggedOn;
					else
						flags &= ~(V1Interop.TaskFlags.RunOnlyIfLoggedOn);
					v1Task.SetFlags(flags);
				}
			}
		}

		/// <summary>
		/// Gets or sets the task process security identifier (SID) type.
		/// </summary>
		/// <value>
		/// One of the <see cref="TaskProcessTokenSidType"/> enumeration constants.
		/// </value>
		/// <remarks>Setting this value appears to break the Task Scheduler MMC and does not output in XML. Removed to prevent problems.</remarks>
		[XmlIgnore]
		public TaskProcessTokenSidType ProcessTokenSidType
		{
			get
			{
				if (v2Principal2 != null)
					return v2Principal2.ProcessTokenSidType;
				return TaskProcessTokenSidType.Default;
			}
			set
			{
				if (v2Principal2 != null)
					v2Principal2.ProcessTokenSidType = value;
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_1);
			}
		}

		/// <summary>
		/// Gets the security credentials for a principal. These security credentials define the security context for the tasks that are associated with the principal.
		/// </summary>
		/// <remarks>Setting this value appears to break the Task Scheduler MMC and does not output in XML. Removed to prevent problems.</remarks>
		[XmlIgnore]
		public TaskPrincipalPrivileges RequiredPrivileges
		{
			get
			{
				if (reqPriv == null)
					reqPriv = new TaskPrincipalPrivileges(this.v2Principal2);
				return reqPriv;
			}
		}

		/// <summary>
		/// Gets or sets the identifier that is used to specify the privilege level that is required to run the tasks that are associated with the principal.
		/// </summary>
		[DefaultValue(typeof(TaskRunLevel), "LUA")]
		[XmlIgnore]
		public TaskRunLevel RunLevel
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.RunLevel;
				return TaskRunLevel.Highest;
			}
			set
			{
				if (v2Principal != null)
					v2Principal.RunLevel = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the user identifier that is required to run the tasks that are associated with the principal. Setting this property to something other than a null or empty string, will set the <see cref="GroupId"/> property to NULL;
		/// </summary>
		[DefaultValue("")]
		public string UserId
		{
			get
			{
				if (v2Principal != null)
					return v2Principal.UserId;
				try
				{
					string acct = v1Task.GetAccountInformation();
					return string.IsNullOrEmpty(acct) ? localSystemAcct : acct;
				}
				catch { return null; }
			}
			set
			{
				if (v2Principal != null)
				{
					if (string.IsNullOrEmpty(value))
						value = null;
					else
					{
						v2Principal.GroupId = null;
						if (value.Contains(@"\") && !value.Contains(@"\\"))
							value = value.Replace(@"\", @"\\");
					}
					v2Principal.UserId = value;
				}
				else
				{
					if (value.Equals(localSystemAcct, StringComparison.CurrentCultureIgnoreCase))
						value = "";
					v1Task.SetAccountInformation(value, IntPtr.Zero);
				}
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Principal != null)
				Marshal.ReleaseComObject(v2Principal);
			v1Task = null;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.LogonType == TaskLogonType.Group ? this.GroupId : this.UserId;
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.ReadStartElement("Principals", TaskDefinition.tns);
			reader.Read();
			while (reader.MoveToContent() == XmlNodeType.Element)
			{
				switch (reader.LocalName)
				{
					case "Principal":
						reader.Read();
						XmlSerializationHelper.ReadObjectProperties(reader, this);
						reader.ReadEndElement();
						break;
					default:
						reader.Skip();
						break;
				}
			}
			reader.ReadEndElement();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.ToString()) || this.LogonType != TaskLogonType.None)
			{
				writer.WriteStartElement("Principal");
				XmlSerializationHelper.WriteObjectProperties(writer, this);
				writer.WriteEndElement();
			}
		}
	}

	/// <summary>
	/// List of security credentials for a principal under version 1.3 of the Task Scheduler. These security credentials define the security context for the tasks that are associated with the principal.
	/// </summary>
	public sealed class TaskPrincipalPrivileges : System.Collections.Generic.IList<TaskPrincipalPrivilege>
	{
		private V2Interop.IPrincipal2 v2Principal2;

		internal TaskPrincipalPrivileges(V2Interop.IPrincipal2 iPrincipal2 = null)
		{
			this.v2Principal2 = iPrincipal2;
		}

		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
		/// <returns>
		/// The index of <paramref name="item"/> if found in the list; otherwise, -1.
		/// </returns>
		public int IndexOf(TaskPrincipalPrivilege item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (item == this[i])
					return i;
			}
			return -1;
		}

		/// <summary>
		/// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
		///   </exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
		///   </exception>
		public void Insert(int index, TaskPrincipalPrivilege item)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
		///   </exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
		///   </exception>
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <returns>
		/// The element at the specified index.
		///   </returns>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
		///   </exception>
		///   
		/// <exception cref="T:System.NotSupportedException">
		/// The property is set and the <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
		///   </exception>
		public TaskPrincipalPrivilege this[int index]
		{
			get
			{
				if (this.v2Principal2 != null)
					return (TaskPrincipalPrivilege)Enum.Parse(typeof(TaskPrincipalPrivilege), v2Principal2[index + 1]);
				throw new IndexOutOfRangeException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		///   </exception>
		public void Add(TaskPrincipalPrivilege item)
		{
			if (this.v2Principal2 != null)
				this.v2Principal2.AddRequiredPrivilege(item.ToString());
			else
				throw new NotSupportedPriorToException(TaskCompatibility.V2_1);
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		///   </exception>
		public void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
		/// <returns>
		/// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
		/// </returns>
		public bool Contains(TaskPrincipalPrivilege item)
		{
			return (this.IndexOf(item) != -1);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="arrayIndex">Index of the array.</param>
		public void CopyTo(TaskPrincipalPrivilege[] array, int arrayIndex)
		{
			var pEnum = GetEnumerator();
			for (int i = arrayIndex; i < array.Length; i++)
			{
				if (!pEnum.MoveNext())
					break;
				array[i] = pEnum.Current;
			}
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <returns>
		/// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		///   </returns>
		public int Count
		{
			get { return (this.v2Principal2 != null) ? (int)this.v2Principal2.RequiredPrivilegeCount : 0; }
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
		///   </returns>
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
		/// <returns>
		/// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </returns>
		/// <exception cref="T:System.NotSupportedException">
		/// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		///   </exception>
		public bool Remove(TaskPrincipalPrivilege item)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public System.Collections.Generic.IEnumerator<TaskPrincipalPrivilege> GetEnumerator()
		{
			return new TaskPrincipalPrivilegesEnumerator(this.v2Principal2);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Enumerates the privileges set for a principal under version 1.3 of the Task Scheduler.
		/// </summary>
		public sealed class TaskPrincipalPrivilegesEnumerator : System.Collections.Generic.IEnumerator<TaskPrincipalPrivilege>
		{
			private V2Interop.IPrincipal2 v2Principal2;
			int cur;
			TaskPrincipalPrivilege curVal;

			internal TaskPrincipalPrivilegesEnumerator(V2Interop.IPrincipal2 iPrincipal2 = null)
			{
				this.v2Principal2 = iPrincipal2;
				Reset();
			}

			/// <summary>
			/// Gets the element in the collection at the current position of the enumerator.
			/// </summary>
			/// <returns>
			/// The element in the collection at the current position of the enumerator.
			///   </returns>
			public TaskPrincipalPrivilege Current
			{
				get { return curVal; }
			}

			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			public void Dispose()
			{
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			/// <summary>
			/// Advances the enumerator to the next element of the collection.
			/// </summary>
			/// <returns>
			/// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
			/// </returns>
			/// <exception cref="T:System.InvalidOperationException">
			/// The collection was modified after the enumerator was created.
			///   </exception>
			public bool MoveNext()
			{
				if (this.v2Principal2 != null && cur < v2Principal2.RequiredPrivilegeCount)
				{
					cur++;
					curVal = (TaskPrincipalPrivilege)Enum.Parse(typeof(TaskPrincipalPrivilege), v2Principal2[cur]);
					return true;
				}
				curVal = 0;
				return false;
			}

			/// <summary>
			/// Sets the enumerator to its initial position, which is before the first element in the collection.
			/// </summary>
			/// <exception cref="T:System.InvalidOperationException">
			/// The collection was modified after the enumerator was created.
			///   </exception>
			public void Reset()
			{
				cur = 0;
				curVal = 0;
			}
		}
	}

	/// <summary>
	/// Provides the administrative information that can be used to describe the task. This information includes details such as a description of the task, the author of the task, the date the task is registered, and the security descriptor of the task.
	/// </summary>
	[XmlRoot("RegistrationInfo", Namespace = TaskDefinition.tns, IsNullable = true)]
	public sealed class TaskRegistrationInfo : IDisposable, IXmlSerializable
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

		/// <summary>
		/// Gets or sets the author of the task.
		/// </summary>
		[DefaultValue(null)]
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

		/// <summary>
		/// Gets or sets the date and time when the task is registered.
		/// </summary>
		[DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
		[XmlIgnore]
		public DateTime Date
		{
			get
			{
				if (v2RegInfo != null)
				{
					string d = v2RegInfo.Date;
					return string.IsNullOrEmpty(d) ? DateTime.MinValue : DateTime.Parse(d);
				}

				string v1Path = Task.GetV1Path(v1Task);
				if (!string.IsNullOrEmpty(v1Path) && System.IO.File.Exists(v1Path))
					return System.IO.File.GetLastWriteTime(v1Path);
				return DateTime.MinValue;
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Date = value.ToString(Trigger.V2BoundaryDateFormat);
				else
				{
					string v1Path = Task.GetV1Path(v1Task);
					if (!string.IsNullOrEmpty(v1Path) && System.IO.File.Exists(v1Path))
						System.IO.File.SetLastWriteTime(v1Path, value);
					else
						throw new NotV1SupportedException("This property cannot be set on an unregistered task.");
				}
			}
		}

		/// <summary>
		/// Gets or sets the description of the task.
		/// </summary>
		[DefaultValue(null)]
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

		/// <summary>
		/// Gets or sets any additional documentation for the task.
		/// </summary>
		[DefaultValue(null)]
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

		/// <summary>
		/// Gets or sets the security descriptor of the task.
		/// </summary>
		/// <value>The security descriptor.</value>
		[XmlIgnore]
		public System.Security.AccessControl.GenericSecurityDescriptor SecurityDescriptor
		{
			get
			{
				return new System.Security.AccessControl.RawSecurityDescriptor(this.SecurityDescriptorSddlForm);
			}
			set
			{
				this.SecurityDescriptorSddlForm = value.GetSddlForm(System.Security.AccessControl.AccessControlSections.All);
			}
		}

		/// <summary>
		/// Gets or sets the security descriptor of the task.
		/// </summary>
		[DefaultValue(null)]
		[XmlIgnore]
		public string SecurityDescriptorSddlForm
		{
			get
			{
				object sddl = null;
				if (v2RegInfo != null)
					sddl = v2RegInfo.SecurityDescriptor;
				return sddl == null ? null : sddl.ToString();
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.SecurityDescriptor = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets where the task originated from. For example, a task may originate from a component, service, application, or user.
		/// </summary>
		[DefaultValue(null)]
		[XmlIgnore]
		public string Source
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.Source;
				return null;
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Source = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the URI of the task.
		/// </summary>
		[DefaultValue(null)]
		[XmlIgnore]
		public Uri URI
		{
			get
			{
				string uri = null;
				if (v2RegInfo != null)
					uri = v2RegInfo.URI;
				if (string.IsNullOrEmpty(uri))
					return null;
				return new Uri(uri);
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.URI = value.ToString();
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the version number of the task.
		/// </summary>
		[DefaultValue(typeof(Version), "1.0")]
		[XmlIgnore]
		public Version Version
		{
			get
			{
				if (v2RegInfo != null && v2RegInfo.Version != null)
				{
					try { return new Version(v2RegInfo.Version); }
					catch { }
				}
				return new Version(1, 0);
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.Version = value.ToString();
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets an XML-formatted version of the registration information for the task.
		/// </summary>
		[XmlIgnore]
		public string XmlText
		{
			get
			{
				if (v2RegInfo != null)
					return v2RegInfo.XmlText;
				return XmlSerializationHelper.WriteObjectToXmlText(this);
			}
			set
			{
				if (v2RegInfo != null)
					v2RegInfo.XmlText = value;
				else
					XmlSerializationHelper.ReadObjectFromXmlText(value, this);
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			v1Task = null;
			if (v2RegInfo != null)
				Marshal.ReleaseComObject(v2RegInfo);
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
			v1Task.SetWorkItemData((ushort)stream.Length, stream.ToArray());
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.ReadStartElement("RegistrationInfo", TaskDefinition.tns);
			XmlSerializationHelper.ReadObjectProperties(reader, this);
			reader.ReadEndElement();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			XmlSerializationHelper.WriteObjectProperties(writer, this);
		}
	}

	/// <summary>
	/// Provides the settings that the Task Scheduler service uses to perform the task.
	/// </summary>
	[XmlRoot("Settings", Namespace = TaskDefinition.tns, IsNullable = true)]
	public sealed class TaskSettings : IDisposable, IXmlSerializable
	{
		private IdleSettings idleSettings = null;
		private MaintenanceSettings maintenanceSettings = null;
		private NetworkSettings networkSettings = null;
		private TaskScheduler.V1Interop.ITask v1Task = null;
		private TaskScheduler.V2Interop.ITaskSettings v2Settings = null;
		private TaskScheduler.V2Interop.ITaskSettings2 v2Settings2 = null;
		private TaskScheduler.V2Interop.ITaskSettings3 v2Settings3 = null;

		internal TaskSettings(TaskScheduler.V2Interop.ITaskSettings iSettings)
		{
			v2Settings = iSettings;
			try { v2Settings2 = (TaskScheduler.V2Interop.ITaskSettings2)v2Settings; }
			catch { }
			try { v2Settings3 = (TaskScheduler.V2Interop.ITaskSettings3)v2Settings; }
			catch { }
		}

		internal TaskSettings(TaskScheduler.V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task can be started by using either the Run command or the Context menu.
		/// </summary>
		[DefaultValue(true)]
		[XmlElement("AllowStartOnDemand")]
		[XmlIgnore]
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
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task may be terminated by using TerminateProcess.
		/// </summary>
		[DefaultValue(true)]
		[XmlIgnore]
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
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets an integer value that indicates which version of Task Scheduler a task is compatible with.
		/// </summary>
		[XmlIgnore]
		public TaskCompatibility Compatibility
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.Compatibility;
				return TaskCompatibility.V1;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.Compatibility = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the amount of time that the Task Scheduler will wait before deleting the task after it expires.
		/// </summary>
		/// <remarks>
		/// For Task Scheduler 1.0, this property will return a TimeSpan of 1 second if the task is set to delete when done. For either version, TimeSpan.Zero will indicate that the task should not be deleted.
		/// </remarks>
		[DefaultValue(typeof(TimeSpan), "12:00:00")]
		public TimeSpan DeleteExpiredTaskAfter
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.DeleteExpiredTaskAfter);
				return (v1Task.GetFlags() & V1Interop.TaskFlags.DeleteWhenDone) == V1Interop.TaskFlags.DeleteWhenDone ? TimeSpan.FromSeconds(1) : TimeSpan.Zero;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.DeleteExpiredTaskAfter = Task.TimeSpanToString(value);
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value >= TimeSpan.FromSeconds(1))
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.DeleteWhenDone);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.DeleteWhenDone);
				}
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task will not be started if the computer is running on battery power.
		/// </summary>
		[DefaultValue(true)]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task will not be started if the task is triggered to run in a Remote Applications Integrated Locally (RAIL) session.
		/// </summary>
		[DefaultValue(false)]
		[XmlIgnore]
		public bool DisallowStartOnRemoteAppSession 
		{
			get
			{
				if (v2Settings2 != null)
					return v2Settings2.DisallowStartOnRemoteAppSession;
				if (v2Settings3 != null)
					return v2Settings3.DisallowStartOnRemoteAppSession;
				return false;
			}
			set
			{
				if (v2Settings2 != null)
					v2Settings2.DisallowStartOnRemoteAppSession = value;
				else if (v2Settings3 != null)
					v2Settings3.DisallowStartOnRemoteAppSession = value;
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_1);
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task is enabled. The task can be performed only when this setting is TRUE.
		/// </summary>
		[DefaultValue(true)]
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

		/// <summary>
		/// Gets or sets the amount of time that is allowed to complete the task.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "72:00:00")]
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
					v2Settings.ExecutionTimeLimit = value == TimeSpan.Zero ? "PT0S" : Task.TimeSpanToString(value);
				else
					v1Task.SetMaxRunTime(Convert.ToUInt32(value.TotalMilliseconds));
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task will not be visible in the UI by default.
		/// </summary>
		[DefaultValue(false)]
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

		/// <summary>
		/// Gets or sets the information that specifies how the Task Scheduler performs tasks when the computer is in an idle state.
		/// </summary>
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

		/// <summary>
		/// Gets or sets the information that the Task Scheduler uses during Automatic maintenance.
		/// </summary>
		[XmlIgnore]
		public MaintenanceSettings MaintenanceSettings
		{
			get
			{
				if (maintenanceSettings == null)
					maintenanceSettings = new MaintenanceSettings(v2Settings3);
				return maintenanceSettings;
			}
		}

		/// <summary>
		/// Gets or sets the policy that defines how the Task Scheduler handles multiple instances of the task.
		/// </summary>
		[DefaultValue(typeof(TaskInstancesPolicy), "IgnoreNew")]
		[XmlIgnore]
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
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets the network settings object that contains a network profile identifier and name. If the RunOnlyIfNetworkAvailable property of ITaskSettings is true and a network propfile is specified in the NetworkSettings property, then the task will run only if the specified network profile is available.
		/// </summary>
		[XmlIgnore]
		public NetworkSettings NetworkSettings
		{
			get
			{
				if (networkSettings == null)
					networkSettings = new NetworkSettings(v2Settings == null ? null : v2Settings.NetworkSettings);
				return networkSettings;
			}
		}

		/// <summary>
		/// Gets or sets the priority level of the task.
		/// </summary>
		//[XmlElement(DataType = "byte")]
		[DefaultValue(typeof(System.Diagnostics.ProcessPriorityClass), "Normal")]
		public System.Diagnostics.ProcessPriorityClass Priority
		{
			get
			{
				if (v2Settings != null)
				{
					return GetPriorityFromInt(v2Settings.Priority);
				}
				return (System.Diagnostics.ProcessPriorityClass)v1Task.GetPriority();
			}
			set
			{
				if (v2Settings != null)
				{
					v2Settings.Priority = GetPriorityAsInt(value);
				}
				else
				{
					if (value == System.Diagnostics.ProcessPriorityClass.AboveNormal || value == System.Diagnostics.ProcessPriorityClass.BelowNormal)
						throw new NotV1SupportedException("Unsupported priority level on Task Scheduler 1.0.");
					v1Task.SetPriority((uint)value);
				}
			}
		}

		private System.Diagnostics.ProcessPriorityClass GetPriorityFromInt(int value)
		{
			switch (value)
			{
				case 0:
					return System.Diagnostics.ProcessPriorityClass.RealTime;
				case 1:
					return System.Diagnostics.ProcessPriorityClass.High;
				case 2:
				case 3:
					return System.Diagnostics.ProcessPriorityClass.AboveNormal;
				case 4:
				case 5:
				case 6:
					return System.Diagnostics.ProcessPriorityClass.Normal;
				case 7:
				case 8:
				default:
					return System.Diagnostics.ProcessPriorityClass.BelowNormal;
				case 9:
				case 10:
					return System.Diagnostics.ProcessPriorityClass.Idle;
			}
		}

		private int GetPriorityAsInt(System.Diagnostics.ProcessPriorityClass value)
		{
			int p = 7;
			switch (value)
			{
				case System.Diagnostics.ProcessPriorityClass.AboveNormal:
					p = 3;
					break;
				case System.Diagnostics.ProcessPriorityClass.High:
					p = 1;
					break;
				case System.Diagnostics.ProcessPriorityClass.Idle:
					p = 10;
					break;
				case System.Diagnostics.ProcessPriorityClass.Normal:
					p = 5;
					break;
				case System.Diagnostics.ProcessPriorityClass.RealTime:
					p = 0;
					break;
				case System.Diagnostics.ProcessPriorityClass.BelowNormal:
				default:
					break;
			}
			return p;
		}

		/// <summary>
		/// Gets or sets the number of times that the Task Scheduler will attempt to restart the task.
		/// </summary>
		[DefaultValue(0)]
		[XmlIgnore]
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
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies how long the Task Scheduler will attempt to restart the task.
		/// </summary>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RestartInterval
		{
			get
			{
				if (v2Settings != null)
					return Task.StringToTimeSpan(v2Settings.RestartInterval);
				return TimeSpan.Zero;
			}
			set
			{
				if (v2Settings != null)
					v2Settings.RestartInterval = Task.TimeSpanToString(value);
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler will run the task only if the computer is in an idle condition.
		/// </summary>
		[DefaultValue(false)]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler will run the task only if the user is logged on (v1.0 only)
		/// </summary>
		[XmlIgnore]
		public bool RunOnlyIfLoggedOn
		{
			get
			{
				if (v2Settings != null)
					return true;
				return (v1Task.GetFlags() & V1Interop.TaskFlags.RunOnlyIfLoggedOn) == V1Interop.TaskFlags.RunOnlyIfLoggedOn;
			}
			set
			{
				if (v1Task != null)
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.RunOnlyIfLoggedOn);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.RunOnlyIfLoggedOn);
				}
				else if (v2Settings != null)
					throw new NotV2SupportedException("Task Scheduler 2.0 (1.2) does not support setting this property. You must use an InteractiveToken in order to have the task run in the current user session.");
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler will run the task only when a network is available.
		/// </summary>
		[DefaultValue(false)]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler can start the task at any time after its scheduled time has passed.
		/// </summary>
		[DefaultValue(false)]
		[XmlIgnore]
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
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task will be stopped if the computer switches to battery power.
		/// </summary>
		[DefaultValue(true)]
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

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Unified Scheduling Engine will be utilized to run this task.
		/// </summary>
		[DefaultValue(false)]
		[XmlIgnore]
		public bool UseUnifiedSchedulingEngine
		{
			get
			{
				if (v2Settings2 != null)
					return v2Settings2.UseUnifiedSchedulingEngine;
				if (v2Settings3 != null)
					return v2Settings3.UseUnifiedSchedulingEngine;
				return false;
			}
			set
			{
				if (v2Settings2 != null)
					v2Settings2.UseUnifiedSchedulingEngine = value;
				else if (v2Settings3 != null)
					v2Settings3.UseUnifiedSchedulingEngine = value;
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_1);
			}
		}

		/// <summary>
		/// Gets or sets a boolean value that indicates whether the task is automatically disabled every time Windows starts.
		/// </summary>
		[DefaultValue(false)]
		[XmlIgnore]
		public bool Volatile
		{
			get
			{
				if (v2Settings3 != null)
					return v2Settings3.Volatile;
				return false;
			}
			set
			{
				if (v2Settings3 != null)
					v2Settings3.Volatile = value;
				else
					throw new NotSupportedPriorToException(TaskCompatibility.V2_2);
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the Task Scheduler will wake the computer when it is time to run the task.
		/// </summary>
		[DefaultValue(false)]
		public bool WakeToRun
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.WakeToRun;
				return ((v1Task.GetFlags() & V1Interop.TaskFlags.SystemRequired) == V1Interop.TaskFlags.SystemRequired);
			}
			set
			{
				if (v2Settings != null)
					v2Settings.WakeToRun = value;
				else
				{
					V1Interop.TaskFlags flags = v1Task.GetFlags();
					if (value)
						v1Task.SetFlags(flags |= V1Interop.TaskFlags.SystemRequired);
					else
						v1Task.SetFlags(flags &= ~V1Interop.TaskFlags.SystemRequired);
				}
			}
		}

		/// <summary>
		/// Gets or sets an XML-formatted definition of the task settings.
		/// </summary>
		[XmlIgnore]
		public string XmlText
		{
			get
			{
				if (v2Settings != null)
					return v2Settings.XmlText;
				return XmlSerializationHelper.WriteObjectToXmlText(this);
			}
			set
			{
				if (v2Settings != null)
					v2Settings.XmlText = value;
				else
					XmlSerializationHelper.ReadObjectFromXmlText(value, this);
			}
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Settings != null)
				Marshal.ReleaseComObject(v2Settings);
			idleSettings = null;
			networkSettings = null;
			v1Task = null;
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader.ReadStartElement("Settings", TaskDefinition.tns);
			XmlSerializationHelper.ReadObjectProperties(reader, this, this.ConvertXmlProperty);
			reader.ReadEndElement();
		}

		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			XmlSerializationHelper.WriteObjectProperties(writer, this, this.ConvertXmlProperty);
		}

		bool ConvertXmlProperty(System.Reflection.PropertyInfo pi, Object obj, ref Object value)
		{
			if (pi.Name == "Priority")
			{
				if (value.GetType() == typeof(int) || value.GetType() == typeof(System.Diagnostics.ProcessPriorityClass))
					value = this.GetPriorityFromInt((int)value);
				else
					value = this.GetPriorityAsInt((System.Diagnostics.ProcessPriorityClass)value);
				return true;
			}
			return false;
		}
	}
}