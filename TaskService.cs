using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Quick simple trigger types for the <see cref="TaskService.AddTask(string,Microsoft.Win32.TaskScheduler.Trigger,Microsoft.Win32.TaskScheduler.Action,string,string,Microsoft.Win32.TaskScheduler.TaskLogonType,string)"/> method.
	/// </summary>
	public enum QuickTriggerType
	{
		/// <summary>At boot.</summary>
		Boot,
		/// <summary>On system idle.</summary>
		Idle,
		/// <summary>At logon of any user.</summary>
		Logon,
		/// <summary>When the task is registered.</summary>
		TaskRegistration,
		/// <summary>Hourly, starting now.</summary>
		Hourly,
		/// <summary>Daily, starting now.</summary>
		Daily,
		/// <summary>Weekly, starting now.</summary>
		Weekly,
		/// <summary>Monthly, starting now.</summary>
		Monthly
	}

	/// <summary>
	/// Provides access to the Task Scheduler service for managing registered tasks.
	/// </summary>
	[Description("Provides access to the Task Scheduler service.")]
	[ToolboxItem(true), Serializable]
	public sealed partial class TaskService : Component, ISupportInitialize, System.Runtime.Serialization.ISerializable
	{
		private static readonly bool hasV2 = (Environment.OSVersion.Version >= new Version(6, 0));
		private static readonly Version v1Ver = new Version(1, 1);
		private static Guid CLSID_Ctask = Marshal.GenerateGuidForType(typeof(V1Interop.CTask));
		private static Guid IID_ITask = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
		internal static readonly Guid PowerShellActionGuid = new Guid("dab4c1e3-cd12-46f1-96fc-3981143c9bab");

		private static TaskService instance;

		internal V1Interop.ITaskScheduler v1TaskScheduler;
		internal V2Interop.ITaskService v2TaskService;

		private bool forceV1;
		private bool initializing;
		private Version maxVer;
		private bool maxVerSet;
		private string targetServer;
		private bool targetServerSet;
		private string userDomain;
		private bool userDomainSet;
		private string userName;
		private bool userNameSet;
		private string userPassword;
		private bool userPasswordSet;
		private WindowsImpersonatedIdentity v1Impersonation;

		/// <summary>
		/// Creates a new instance of a TaskService connecting to the local machine as the current user.
		/// </summary>
		public TaskService()
		{
			ResetHighestSupportedVersion();
			Connect();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskService"/> class.
		/// </summary>
		/// <param name="targetServer">The name of the computer that you want to connect to. If the this parameter is empty, then this will connect to the local computer.</param>
		/// <param name="userName">The user name that is used during the connection to the computer. If the user is not specified, then the current token is used.</param>
		/// <param name="accountDomain">The domain of the user specified in the <paramref name="userName"/> parameter.</param>
		/// <param name="password">The password that is used to connect to the computer. If the user name and password are not specified, then the current token is used.</param>
		/// <param name="forceV1">If set to <c>true</c> force Task Scheduler 1.0 compatibility.</param>
		public TaskService(string targetServer, string userName = null, string accountDomain = null, string password = null, bool forceV1 = false)
		{
			BeginInit();
			TargetServer = targetServer;
			UserName = userName;
			UserAccountDomain = accountDomain;
			UserPassword = password;
			this.forceV1 = forceV1;
			ResetHighestSupportedVersion();
			EndInit();
		}

		private TaskService([NotNull] System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			BeginInit();
			TargetServer = (string)info.GetValue("TargetServer", typeof(string));
			UserName = (string)info.GetValue("UserName", typeof(string));
			UserAccountDomain = (string)info.GetValue("UserAccountDomain", typeof(string));
			UserPassword = (string)info.GetValue("UserPassword", typeof(string));
			forceV1 = (bool)info.GetValue("forceV1", typeof(bool));
			ResetHighestSupportedVersion();
			EndInit();
		}

		/// <summary>
		/// Gets or sets a value indicating whether to allow tasks from later OS versions with new properties to be retrieved as read only tasks.
		/// </summary>
		/// <value><c>true</c> if allow read only tasks; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Allow tasks from later OS versions with new properties to be retrieved as read only tasks.")]
		public bool AllowReadOnlyTasks { get; set; }

		/// <summary>
		/// Gets a <see cref="System.Collections.Generic.IEnumerator{T}" /> which enumerates all the tasks in all folders.
		/// </summary>
		/// <value>
		/// A <see cref="System.Collections.Generic.IEnumerator{T}" /> for all <see cref="Task" /> instances.
		/// </value>
		[Browsable(false)]
		public System.Collections.Generic.IEnumerable<Task> AllTasks => RootFolder.AllTasks;

		/// <summary>
		/// Gets a Boolean value that indicates if you are connected to the Task Scheduler service.
		/// </summary>
		[Browsable(false)]
		public bool Connected => (v2TaskService != null && v2TaskService.Connected) || v1TaskScheduler != null;

		/// <summary>
		/// Gets the name of the domain to which the <see cref="TargetServer"/> computer is connected.
		/// </summary>
		[Browsable(false)]
		[DefaultValue(null)]
		[Obsolete("This property has been superseded by the UserAccountDomin property and may not be available in future releases.")]
		public string ConnectedDomain
		{
			get
			{
				if (v2TaskService != null)
					return v2TaskService.ConnectedDomain;
				string[] parts = v1Impersonation.Name.Split('\\');
				if (parts.Length == 2)
					return parts[0];
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets the name of the user that is connected to the Task Scheduler service.
		/// </summary>
		[Browsable(false)]
		[DefaultValue(null)]
		[Obsolete("This property has been superseded by the UserName property and may not be available in future releases.")]
		public string ConnectedUser
		{
			get
			{
				if (v2TaskService != null)
					return v2TaskService.ConnectedUser;
				string[] parts = v1Impersonation.Name.Split('\\');
				if (parts.Length == 2)
					return parts[1];
				return parts[0];
			}
		}

		/// <summary>
		/// Gets the highest version of Task Scheduler that a computer supports.
		/// </summary>
		[Category("Data"), TypeConverter(typeof(VersionConverter)), Description("Highest version of library that should be used.")]
		public Version HighestSupportedVersion
		{
			get { return maxVer; }
			set
			{
				maxVer = value;
				maxVerSet = true;
				bool localForceV1 = (value <= v1Ver);
				if (localForceV1 == forceV1) return;
				forceV1 = localForceV1;
				Connect();
			}
		}

		/// <summary>
		/// Gets the library version. This is the highest version supported by the local library. Tasks cannot be created using any compatibility level higher than this version.
		/// </summary>
		/// <value>
		/// The library version.
		/// </value>
		[Browsable(false)]
		public static Version LibraryVersion => new Version(1, Task.GetOSLibraryMinorVersion());

		/// <summary>
		/// Gets the root ("\") folder. For Task Scheduler 1.0, this is the only folder.
		/// </summary>
		[Browsable(false)]
		public TaskFolder RootFolder => GetFolder(TaskFolder.rootString);

		/// <summary>
		/// Gets or sets the name of the computer that is running the Task Scheduler service that the user is connected to.
		/// </summary>
		[Category("Data"), DefaultValue(null), Description("The name of the computer to connect to.")]
		public string TargetServer
		{
			get { return ShouldSerializeTargetServer() ? targetServer : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, targetServer, StringComparison.OrdinalIgnoreCase) != 0)
				{
					targetServerSet = true;
					targetServer = value;
					Connect();
				}
			}
		}

		/// <summary>
		/// Gets or sets the user account domain to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user account domain.</value>
		[Category("Data"), DefaultValue(null), Description("The user account domain to be used when connecting.")]
		public string UserAccountDomain
		{
			get { return ShouldSerializeUserAccountDomain() ? userDomain : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, userDomain, StringComparison.OrdinalIgnoreCase) != 0)
				{
					userDomainSet = true;
					userDomain = value;
					Connect();
				}
			}
		}

		/// <summary>
		/// Gets a local instance of the <see cref="TaskService"/> using the current user's credentials.
		/// </summary>
		/// <value>
		/// Local user <see cref="TaskService"/> instance.
		/// </value>
		public static TaskService Instance => instance ?? (instance = new TaskService());

		/// <summary>
		/// Gets or sets the user name to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user name.</value>
		[Category("Data"), DefaultValue(null), Description("The user name to be used when connecting.")]
		public string UserName
		{
			get { return ShouldSerializeUserName() ? userName : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, userName, StringComparison.OrdinalIgnoreCase) != 0)
				{
					userNameSet = true;
					userName = value;
					Connect();
				}
			}
		}

		/// <summary>
		/// Gets or sets the user password to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user password.</value>
		[Category("Data"), DefaultValue(null), Description("The user password to be used when connecting.")]
		public string UserPassword
		{
			get { return userPassword; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.CompareOrdinal(value, userPassword) != 0)
				{
					userPasswordSet = true;
					userPassword = value;
					Connect();
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the component can raise an event.
		/// </summary>
		protected override bool CanRaiseEvents { get; } = false;

		/// <summary>
		/// Adds or updates an Automatic Maintenance Task on the connected machine.
		/// </summary>
		/// <param name="taskPathAndName">Name of the task with full path.</param>
		/// <param name="period">The amount of time the task needs once executed during regular Automatic maintenance.</param>
		/// <param name="deadline">The amount of time after which the Task Scheduler attempts to run the task during emergency Automatic maintenance, if the task failed to complete during regular Automatic Maintenance.</param>
		/// <param name="executablePath">The path to an executable file.</param>
		/// <param name="arguments">The arguments associated with the command-line operation.</param>
		/// <param name="workingDirectory">The directory that contains either the executable file or the files that are used by the executable file.</param>
		/// <returns>A <see cref="Task" /> instance of the Automatic Maintenance Task.</returns>
		/// <exception cref="System.InvalidOperationException">Automatic Maintenance tasks are only supported on Windows 8/Server 2012 and later.</exception>
		public Task AddAutomaticMaintenanceTask([NotNull] string taskPathAndName, TimeSpan period, TimeSpan deadline, string executablePath, string arguments = null, string workingDirectory = null)
		{
			if (HighestSupportedVersion.Minor < 4)
				throw new InvalidOperationException("Automatic Maintenance tasks are only supported on Windows 8/Server 2012 and later.");
			TaskDefinition td = NewTask();
			td.Settings.UseUnifiedSchedulingEngine = true;
			td.Settings.MaintenanceSettings.Period = period;
			td.Settings.MaintenanceSettings.Deadline = deadline;
			td.Actions.Add(executablePath, arguments, workingDirectory);
			// The task needs to grant explicit FRFX to LOCAL SERVICE (A;;FRFX;;;LS)
			return RootFolder.RegisterTaskDefinition(taskPathAndName, td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken, "D:P(A;;FA;;;BA)(A;;FA;;;SY)(A;;FRFX;;;LS)");
		}

		/// <summary>
		/// Creates a new task, registers the task, and returns the instance.
		/// </summary>
		/// <param name="path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="trigger">The <see cref="Trigger"/> to determine when to run the task.</param>
		/// <param name="action">The <see cref="Action"/> to determine what happens when the task is triggered.</param>
		/// <param name="userId">The user credentials used to register the task.</param>
		/// <param name="password">The password for the userId used to register the task.</param>
		/// <param name="logonType">A <see cref="TaskLogonType"/> value that defines what logon technique is used to run the registered task.</param>
		/// <param name="description">The task description.</param>
		/// <returns>
		/// A <see cref="Task"/> instance of the registered task.
		/// </returns>
		public Task AddTask([NotNull] string path, [NotNull] Trigger trigger, [NotNull] Action action, string userId = null, string password = null, TaskLogonType logonType = TaskLogonType.InteractiveToken, string description = null)
		{
			TaskDefinition td = NewTask();
			if (!string.IsNullOrEmpty(description))
				td.RegistrationInfo.Description = description;

			// Create a trigger that will fire the task at a specific date and time
			td.Triggers.Add(trigger);

			// Create an action that will launch Notepad whenever the trigger fires
			td.Actions.Add(action);

			// Register the task in the root folder
			return RootFolder.RegisterTaskDefinition(path, td, TaskCreation.CreateOrUpdate, userId, password, logonType);
		}

		/// <summary>
		/// Creates a new task, registers the task, and returns the instance.
		/// </summary>
		/// <param name="path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
		/// <param name="trigger">The <see cref="Trigger" /> to determine when to run the task.</param>
		/// <param name="exePath">The executable path.</param>
		/// <param name="arguments">The arguments (optional). Value can be NULL.</param>
		/// <param name="userId">The user credentials used to register the task.</param>
		/// <param name="password">The password for the userId used to register the task.</param>
		/// <param name="logonType">A <see cref="TaskLogonType" /> value that defines what logon technique is used to run the registered task.</param>
		/// <param name="description">The task description.</param>
		/// <returns>
		/// A <see cref="Task" /> instance of the registered task.
		/// </returns>
		public Task AddTask([NotNull] string path, QuickTriggerType trigger, [NotNull] string exePath, string arguments = null, string userId = null, string password = null, TaskLogonType logonType = TaskLogonType.InteractiveToken, string description = null)
		{
			// Create a trigger based on quick trigger
			Trigger newTrigger;
			switch (trigger)
			{
				case QuickTriggerType.Boot:
					newTrigger = new BootTrigger();
					break;
				case QuickTriggerType.Idle:
					newTrigger = new IdleTrigger();
					break;
				case QuickTriggerType.Logon:
					newTrigger = new LogonTrigger();
					break;
				case QuickTriggerType.TaskRegistration:
					newTrigger = new RegistrationTrigger();
					break;
				case QuickTriggerType.Hourly:
					newTrigger = new DailyTrigger { Repetition = new RepetitionPattern(TimeSpan.FromHours(1), TimeSpan.FromDays(1)) };
					break;
				case QuickTriggerType.Daily:
					newTrigger = new DailyTrigger();
					break;
				case QuickTriggerType.Weekly:
					newTrigger = new WeeklyTrigger();
					break;
				case QuickTriggerType.Monthly:
					newTrigger = new MonthlyTrigger();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(trigger), trigger, null);
			}

			return AddTask(path, newTrigger, new ExecAction(exePath, arguments), userId, password, logonType, description);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			TaskService tsobj = obj as TaskService;
			if (tsobj != null)
				return tsobj.TargetServer == TargetServer && tsobj.UserAccountDomain == UserAccountDomain && tsobj.UserName == UserName && tsobj.UserPassword == UserPassword && tsobj.forceV1 == forceV1;
			return base.Equals(obj);
		}

		/// <summary>
		/// Finds all tasks matching a name or standard wildcards.
		/// </summary>
		/// <param name="name">Name of the task in regular expression form.</param>
		/// <param name="searchAllFolders">if set to <c>true</c> search all sub folders.</param>
		/// <returns>An array of <see cref="Task"/> containing all tasks matching <paramref name="name"/>.</returns>
		public Task[] FindAllTasks(System.Text.RegularExpressions.Regex name, bool searchAllFolders = true)
		{
			System.Collections.Generic.List<Task> results = new System.Collections.Generic.List<Task>();
			FindTaskInFolder(RootFolder, name, ref results, searchAllFolders);
			return results.ToArray();
		}

		/// <summary>
		/// Finds all tasks matching a name or standard wildcards.
		/// </summary>
		/// <param name="filter">The filter used to determine tasks to select.</param>
		/// <param name="searchAllFolders">if set to <c>true</c> search all sub folders.</param>
		/// <returns>An array of <see cref="Task" /> containing all tasks matching <paramref name="filter" />.</returns>
		public Task[] FindAllTasks(Predicate<Task> filter, bool searchAllFolders = true)
		{
			if (filter == null) filter = t => true;
			System.Collections.Generic.List<Task> results = new System.Collections.Generic.List<Task>();
			FindTaskInFolder(RootFolder, filter, ref results, searchAllFolders);
			return results.ToArray();
		}

		/// <summary>
		/// Finds a task given a name and standard wildcards.
		/// </summary>
		/// <param name="name">The task name. This can include the wildcards * or ?.</param>
		/// <param name="searchAllFolders">if set to <c>true</c> search all sub folders.</param>
		/// <returns>A <see cref="Task"/> if one matches <paramref name="name"/>, otherwise NULL.</returns>
		public Task FindTask([NotNull] string name, bool searchAllFolders = true)
		{
			Task[] results = FindAllTasks(new Wildcard(name), searchAllFolders);
			if (results.Length > 0)
				return results[0];
			return null;
		}

		/// <summary>
		/// Gets the event log for this <see cref="TaskService"/> instance.
		/// </summary>
		/// <param name="taskPath">(Optional) The task path if only the events for a single task are desired.</param>
		/// <returns>A <see cref="TaskEventLog"/> instance.</returns>
		public TaskEventLog GetEventLog(string taskPath = null) => new TaskEventLog(TargetServer, taskPath, UserAccountDomain, UserName, UserPassword);

		/// <summary>Gets the path to a folder of registered tasks.</summary>
		/// <param name="folderName">
		/// The path to the folder to retrieve. Do not use a backslash following the last folder name in the path. The root task folder is specified with a
		/// backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the
		/// current task folder and the '..' characters cannot be used to specify the parent task folder in the path.
		/// </param>
		/// <returns><see cref="TaskFolder"/> instance for the requested folder or <c>null</c> if <paramref name="folderName"/> was unrecognized.</returns>
		/// <exception cref="NotV1SupportedException">Folder other than the root (\) was requested on a system not supporting Task Scheduler 2.0.</exception>
		public TaskFolder GetFolder(string folderName)
		{
			TaskFolder f = null;
			if (v2TaskService != null)
			{
				if (string.IsNullOrEmpty(folderName)) folderName = TaskFolder.rootString;
				try
				{
					var ifld = v2TaskService.GetFolder(folderName);
					if (ifld != null)
						f = new TaskFolder(this, ifld);
				}
				catch (System.IO.DirectoryNotFoundException) { }
				catch (System.IO.FileNotFoundException) { }
			}
			else if (folderName == TaskFolder.rootString || string.IsNullOrEmpty(folderName))
				f = new TaskFolder(this);
			else
				throw new NotV1SupportedException("Folder other than the root (\\) was requested on a system only supporting Task Scheduler 1.0.");
			return f;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() => new {A = TargetServer, B = UserAccountDomain, C = UserName, D = UserPassword, E = forceV1}.GetHashCode();

		/// <summary>
		/// Gets a collection of running tasks.
		/// </summary>
		/// <param name="includeHidden">True to include hidden tasks.</param>
		/// <returns><see cref="RunningTaskCollection"/> instance with the list of running tasks.</returns>
		public RunningTaskCollection GetRunningTasks(bool includeHidden = true)
		{
			if (v2TaskService != null)
				try
				{
					return new RunningTaskCollection(this, v2TaskService.GetRunningTasks(includeHidden ? 1 : 0));
				}
				catch { }
			return new RunningTaskCollection(this);
		}

		/// <summary>
		/// Gets the task with the specified path.
		/// </summary>
		/// <param name="taskPath">The task path.</param>
		/// <returns>The <see cref="Task"/> instance matching the <paramref name="taskPath"/>, if found. If not found, this method returns <c>null</c>.</returns>
		public Task GetTask([NotNull] string taskPath)
		{
			Task t = null;
			if (v2TaskService != null)
			{
				V2Interop.IRegisteredTask iTask = GetTask(v2TaskService, taskPath);
				if (iTask != null)
					t = Task.CreateTask(this, iTask);
			}
			else
			{
				taskPath = System.IO.Path.GetFileNameWithoutExtension(taskPath);
				V1Interop.ITask iTask = GetTask(v1TaskScheduler, taskPath);
				if (iTask != null)
					t = new Task(this, iTask);
			}
			return t;
		}

		/// <summary>
		/// Signals the object that initialization is starting.
		/// </summary>
		public void BeginInit() { initializing = true; }

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			initializing = false;
			Connect();
		}

		/// <summary>
		/// Returns an empty task definition object to be filled in with settings and properties and then registered using the <see cref="TaskFolder.RegisterTaskDefinition(string, TaskDefinition)"/> method.
		/// </summary>
		/// <returns>A <see cref="TaskDefinition"/> instance for setting properties.</returns>
		public TaskDefinition NewTask()
		{
			if (v2TaskService != null)
				return new TaskDefinition(v2TaskService.NewTask(0));
			string v1Name = "Temp" + Guid.NewGuid().ToString("B");
			return new TaskDefinition(v1TaskScheduler.NewWorkItem(v1Name, CLSID_Ctask, IID_ITask), v1Name);
		}

		/// <summary>
		/// Returns a <see cref="TaskDefinition"/> populated with the properties defined in an XML file.
		/// </summary>
		/// <param name="xmlFile">The XML file to use as input.</param>
		/// <returns>A <see cref="TaskDefinition"/> instance.</returns>
		/// <exception cref="NotV1SupportedException">Importing from an XML file is only supported under Task Scheduler 2.0.</exception>
		public TaskDefinition NewTaskFromFile([NotNull] string xmlFile)
		{
			TaskDefinition td = NewTask();
			td.XmlText = System.IO.File.ReadAllText(xmlFile);
			return td;
		}

		/// <summary>
		/// Starts the Task Scheduler UI for the OS hosting the assembly if the session is running in interactive mode.
		/// </summary>
		public void StartSystemTaskSchedulerManager()
		{
			if (Environment.UserInteractive)
				System.Diagnostics.Process.Start("control.exe", "schedtasks");
		}

		/// <summary>
		/// Gets a formatted string that tells the Task Scheduler to retrieve a string from a resource .dll file.
		/// </summary>
		/// <param name="dllPath">The path to the .dll file that contains the resource.</param>
		/// <param name="resourceId">The identifier for the resource text (typically a negative number).</param>
		/// <returns>A string in the format of $(@ [dllPath], [resourceId]).</returns>
		/// <example>For example, the setting this property value to $(@ %SystemRoot%\System32\ResourceName.dll, -101) will set the property to the value of the resource text with an identifier equal to -101 in the %SystemRoot%\System32\ResourceName.dll file.</example>
		public static string GetDllResourceString([NotNull] string dllPath, int resourceId) => $"$(@ {dllPath}, {resourceId})";

		/// <summary>
		/// Runs an action that is defined via a COM handler. COM CLSID must be registered to an object that implements the <see cref="ITaskHandler" /> interface.
		/// </summary>
		/// <param name="clsid">The CLSID of the COM object.</param>
		/// <param name="data">An optional string passed to the COM object at startup.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait or -1 for indefinitely.</param>
		/// <param name="onUpdate">An optional <see cref="ComHandlerUpdate" /> delegate that is called when the COM object calls the <see cref="ITaskHandlerStatus.UpdateStatus(short, string)" /> method.</param>
		/// <returns>
		/// The value set by the COM object via a call to the <see cref="ITaskHandlerStatus.TaskCompleted(int)" /> method.
		/// </returns>
		public static int RunComHandlerAction(Guid clsid, string data = null, int millisecondsTimeout = -1, ComHandlerUpdate onUpdate = null)
		{
			var thread = new ComHandlerThread(clsid, data, millisecondsTimeout, onUpdate, null);
			thread.Start().Join();
			return thread.ReturnCode;
		}

		/// <summary>
		/// Runs an action that is defined via a COM handler. COM CLSID must be registered to an object that implements the <see cref="ITaskHandler" /> interface.
		/// </summary>
		/// <param name="clsid">The CLSID of the COM object.</param>
		/// <param name="onComplete">The action to run on thread completion.</param>
		/// <param name="data">An optional string passed to the COM object at startup.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait or -1 for indefinitely.</param>
		/// <param name="onUpdate">An optional <see cref="ComHandlerUpdate" /> delegate that is called when the COM object calls the <see cref="ITaskHandlerStatus.UpdateStatus(short, string)" /> method.</param>
		public static void RunComHandlerActionAsync(Guid clsid, Action<int> onComplete, string data = null, int millisecondsTimeout = -1, ComHandlerUpdate onUpdate = null) { new ComHandlerThread(clsid, data, millisecondsTimeout, onUpdate, onComplete).Start(); }

		/// <summary>
		/// Delegate for methods that support update calls during COM handler execution.
		/// </summary>
		/// <param name="percentage">The percentage of completion (0 to 100).</param>
		/// <param name="message">An optional message.</param>
		public delegate void ComHandlerUpdate(short percentage, string message);

		private class ComHandlerThread
		{
			private readonly System.Threading.AutoResetEvent completed = new System.Threading.AutoResetEvent(false);
			private readonly Type objType;
			private readonly string Data;
			private readonly int Timeout;
			private readonly TaskHandlerStatus status;
			public int ReturnCode;

			public ComHandlerThread(Guid clsid, string data, int millisecondsTimeout, ComHandlerUpdate onUpdate, Action<int> onComplete)
			{
				objType = Type.GetTypeFromCLSID(clsid, true);
				Data = data;
				Timeout = millisecondsTimeout;
				status = new TaskHandlerStatus(i =>
				{
					completed.Set();
					onComplete?.Invoke(i);
				}, onUpdate);
			}

			public System.Threading.Thread Start()
			{
				var t = new System.Threading.Thread(ThreadProc);
				t.Start();
				return t;
			}

			private void ThreadProc()
			{
				completed.Reset();
				var obj = Activator.CreateInstance(objType);
				var taskHandler = (ITaskHandler)obj;
				try
				{
					if (taskHandler != null)
					{
						taskHandler.Start(status, Data);
						completed.WaitOne(Timeout);
						taskHandler.Stop(out ReturnCode);
					}
				}
				finally
				{
					if (taskHandler != null)
					{
						Marshal.ReleaseComObject(taskHandler);
						taskHandler = null;
					}
					if (obj != null)
					{
						Marshal.ReleaseComObject(obj);
						obj = null;
					}
				}
			}

			private class TaskHandlerStatus : ITaskHandlerStatus
			{
				private readonly Action<int> OnCompleted;
				private readonly ComHandlerUpdate OnUpdate;

				public TaskHandlerStatus(Action<int> onCompleted, ComHandlerUpdate onUpdate)
				{
					OnCompleted = onCompleted;
					OnUpdate = onUpdate;
				}

				public void TaskCompleted([In, MarshalAs(UnmanagedType.Error)] int taskErrCode) { OnCompleted?.Invoke(taskErrCode); }

				public void UpdateStatus([In] short percentComplete, [In, MarshalAs(UnmanagedType.BStr)] string statusMessage) { OnUpdate?.Invoke(percentComplete, statusMessage); }
			}
		}

		internal static V2Interop.IRegisteredTask GetTask([NotNull] V2Interop.ITaskService iSvc, [NotNull] string name)
		{
			V2Interop.ITaskFolder fld = null;
			try
			{
				fld = iSvc.GetFolder("\\");
				return fld.GetTask(name);
			}
			catch
			{
				return null;
			}
			finally
			{
				if (fld != null) Marshal.ReleaseComObject(fld);
			}
		}

		internal static V1Interop.ITask GetTask([NotNull] V1Interop.ITaskScheduler iSvc, [NotNull] string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));
			try
			{
				return iSvc.Activate(name, IID_ITask);
			}
			catch (UnauthorizedAccessException)
			{
				// TODO: Take ownership of the file and try again
				throw;
			}
			catch (ArgumentException)
			{
				return iSvc.Activate(name + ".job", IID_ITask);
			}
			catch (FileNotFoundException)
			{
				return null;
			}
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (v2TaskService != null)
			{
				try
				{
					Marshal.ReleaseComObject(v2TaskService);
				}
				catch { }
				v2TaskService = null;
			}
			if (v1TaskScheduler != null)
			{
				try
				{
					Marshal.ReleaseComObject(v1TaskScheduler);
				}
				catch { }
				v1TaskScheduler = null;
			}
			if (v1Impersonation != null)
			{
				v1Impersonation.Dispose();
				v1Impersonation = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Connects this instance of the <see cref="TaskService"/> class to a running Task Scheduler.
		/// </summary>
		private void Connect()
		{
			ResetUnsetProperties();

			if (!initializing && !DesignMode)
			{
				if (((!string.IsNullOrEmpty(userDomain) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPassword)) || (string.IsNullOrEmpty(userDomain) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(userPassword))))
				{
					// Clear stuff if already connected
					Dispose(true);

					if (hasV2 && !forceV1)
					{
						v2TaskService = new V2Interop.ITaskService();
						if (!string.IsNullOrEmpty(targetServer))
						{
							// Check to ensure character only server name. (Suggested by bigsan)
							if (targetServer.StartsWith(@"\"))
								targetServer = targetServer.TrimStart('\\');
							// Make sure null is provided for local machine to compensate for a native library oddity (Found by ctrollen)
							if (targetServer.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase))
								targetServer = null;
						}
						else
							targetServer = null;
						v2TaskService.Connect(targetServer, userName, userDomain, userPassword);
						targetServer = v2TaskService.TargetServer;
						userName = v2TaskService.ConnectedUser;
						userDomain = v2TaskService.ConnectedDomain;
						maxVer = GetV2Version();
					}
					else
					{
						v1Impersonation = new WindowsImpersonatedIdentity(userName, userDomain, userPassword);
						v1TaskScheduler = new V1Interop.ITaskScheduler();
						if (!string.IsNullOrEmpty(targetServer))
						{
							// Check to ensure UNC format for server name. (Suggested by bigsan)
							if (!targetServer.StartsWith(@"\\"))
								targetServer = @"\\" + targetServer;
						}
						else
							targetServer = null;
						v1TaskScheduler.SetTargetComputer(targetServer);
						targetServer = v1TaskScheduler.GetTargetComputer();
						maxVer = v1Ver;
					}
				}
				else
				{
					throw new ArgumentException("A username, password, and domain must be provided.");
				}
			}
		}

		/// <summary>
		/// Finds the task in folder.
		/// </summary>
		/// <param name="fld">The folder.</param>
		/// <param name="taskName">The wildcard expression to compare task names with.</param>
		/// <param name="results">The results.</param>
		/// <param name="recurse">if set to <c>true</c> recurse folders.</param>
		/// <returns>True if any tasks are found, False if not.</returns>
		private bool FindTaskInFolder([NotNull] TaskFolder fld, System.Text.RegularExpressions.Regex taskName, ref System.Collections.Generic.List<Task> results, bool recurse = true)
		{
			results.AddRange(fld.GetTasks(taskName));

			if (recurse)
			{
				foreach (var f in fld.SubFolders)
				{
					if (FindTaskInFolder(f, taskName, ref results))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Finds the task in folder.
		/// </summary>
		/// <param name="fld">The folder.</param>
		/// <param name="filter">The filter to use when looking for tasks.</param>
		/// <param name="results">The results.</param>
		/// <param name="recurse">if set to <c>true</c> recurse folders.</param>
		/// <returns>True if any tasks are found, False if not.</returns>
		private bool FindTaskInFolder([NotNull] TaskFolder fld, Predicate<Task> filter, ref System.Collections.Generic.List<Task> results, bool recurse = true)
		{
			foreach (Task t in fld.GetTasks())
				try
				{
					if (filter(t))
						results.Add(t);
				}
				catch
				{
					System.Diagnostics.Debug.WriteLine($"Unable to evaluate filter for task '{t.Path}'.");
				}

			if (recurse)
			{
				foreach (var f in fld.SubFolders)
				{
					if (FindTaskInFolder(f, filter, ref results))
						return true;
				}
			}
			return false;
		}

		[System.Security.SecurityCritical]
		void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			info.AddValue("TargetServer", TargetServer, typeof(string));
			info.AddValue("UserName", UserName, typeof(string));
			info.AddValue("UserAccountDomain", UserAccountDomain, typeof(string));
			info.AddValue("UserPassword", UserPassword, typeof(string));
			info.AddValue("forceV1", forceV1, typeof(bool));
		}

		private Version GetV2Version()
		{
			uint v = v2TaskService.HighestVersion;
			return new Version((int)(v >> 16), (int)(v & 0x0000FFFF));
		}

		private void ResetUnsetProperties()
		{
			if (!maxVerSet) ResetHighestSupportedVersion();
			if (!targetServerSet) targetServer = null;
			if (!userDomainSet) userDomain = null;
			if (!userNameSet) userName = null;
			if (!userPasswordSet) userPassword = null;
		}

		private void ResetHighestSupportedVersion()
		{
			if (Connected)
				maxVer = v2TaskService != null ? GetV2Version() : v1Ver;
			else
			{
				if (!hasV2)
					maxVer = v1Ver;
				else if (Environment.OSVersion.Version.Major == 6)
				{
					switch (Environment.OSVersion.Version.Minor)
					{
						case 0:
							maxVer = new Version(1, 2);
							break;
						case 1:
							maxVer = new Version(1, 3);
							break;
						case 2:
						case 3:
							maxVer = new Version(1, 4);
							break;
					}
				}
			}
		}

		private bool ShouldSerializeHighestSupportedVersion() => (hasV2 && maxVer <= v1Ver);

		private bool ShouldSerializeTargetServer() => targetServer != null && !targetServer.Trim('\\').Equals(Environment.MachineName.Trim('\\'), StringComparison.InvariantCultureIgnoreCase);

		private bool ShouldSerializeUserAccountDomain() => userDomain != null && !userDomain.Equals(Environment.UserDomainName, StringComparison.InvariantCultureIgnoreCase);

		private bool ShouldSerializeUserName() => userName != null && !userName.Equals(Environment.UserName, StringComparison.InvariantCultureIgnoreCase);

		private class VersionConverter : TypeConverter
		{
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				if (sourceType == typeof(string))
					return true;
				return base.CanConvertFrom(context, sourceType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
			{
				var s = value as string;
				return s != null ? new Version(s) : base.ConvertFrom(context, culture, value);
			}
		}
	}
}