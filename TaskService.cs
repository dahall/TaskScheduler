using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
    /// <summary>
    /// Provides access to the Task Scheduler service for managing registered tasks.
    /// </summary>
    public sealed class TaskService : Component, IDisposable, ISupportInitialize
    {
        internal static readonly bool hasV2 = (Environment.OSVersion.Version >= new Version(6, 0));
        internal static readonly Version v1Ver = new Version(1, 1);

        internal V1Interop.ITaskScheduler v1TaskScheduler = null;
        internal bool v2 = false;
        internal V2Interop.TaskSchedulerClass v2TaskService = null;

        private bool forceV1 = false;
        private bool initializing = false;
        private Version maxVer;
        private string targetServer = null;
        private string userDomain = null;
        private string userName = null;
        private string userPassword = null;
        private WindowsImpersonatedIdentity v1Impersonation = null;

        /// <summary>
        /// Creates a new instance of a TaskService connecting to the local machine as the current user.
        /// </summary>
        public TaskService()
        {
            ResetHighestSupportedVersion();
			if (!base.DesignMode)
	            Connect();
        }

        /// <summary>
        /// Creates a new instance of a TaskService connecting to a remote machine as the current user.
        /// </summary>
        /// <param name="targetServer">The target server.</param>
        public TaskService(string targetServer)
        {
            this.targetServer = targetServer;
            ResetHighestSupportedVersion();
            Connect();
        }

        /// <summary>
        /// Creates a new instance of a TaskService connecting to a remote machine as the specified user.
        /// </summary>
        /// <param name="targetServer">The target server.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="accountDomain">The account domain.</param>
        /// <param name="password">The password.</param>
        public TaskService(string targetServer, string userName, string accountDomain, string password)
        {
            this.targetServer = targetServer;
            this.userName = userName;
            this.userDomain = accountDomain;
            this.userPassword = password;
            ResetHighestSupportedVersion();
            Connect();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="targetServer">The target server. A null value implies the local machine.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="accountDomain">The account domain.</param>
        /// <param name="password">The password.</param>
        /// <param name="forceV1">If set to <c>true</c> force Task Scheduler 1.0 compatibility.</param>
        public TaskService(string targetServer, string userName, string accountDomain, string password, bool forceV1)
        {
            this.targetServer = targetServer;
            this.userName = userName;
            this.userDomain = accountDomain;
            this.userPassword = password;
            this.forceV1 = forceV1;
            ResetHighestSupportedVersion();
            Connect();
        }

        /// <summary>
        /// Gets a Boolean value that indicates if you are connected to the Task Scheduler service.
        /// </summary>
        [Browsable(false)]
        public bool Connected
        {
            get { return v2 ? v2TaskService.Connected : true; }
        }

        /// <summary>
        /// Gets the name of the domain to which the <see cref="TargetServer"/> computer is connected.
        /// </summary>
        [Browsable(false)]
        [DefaultValue((string)null)]
        [Obsolete("This property has been superceded by the UserAccountDomin property and may not be available in future releases.")]
        public string ConnectedDomain
        {
            get
            {
                if (v2)
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
        [DefaultValue((string)null)]
        [Obsolete("This property has been superceded by the UserName property and may not be available in future releases.")]
        public string ConnectedUser
        {
            get
            {
                if (v2)
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
		[TypeConverter(typeof(VersionConverter))]
        public Version HighestSupportedVersion
        {
            get { return maxVer; }
            set
            {
                this.maxVer = value;
                bool forceV1 = (value <= v1Ver);
                if (forceV1 != this.forceV1)
                {
                    this.forceV1 = forceV1;
					Connect();
                }
            }
        }

        /// <summary>
        /// Gets the root ("\") folder. For Task Scheduler 1.0, this is the only folder.
        /// </summary>
        [Browsable(false)]
        public TaskFolder RootFolder
        {
            get { return GetFolder(@"\"); }
        }

        /// <summary>
        /// Gets or sets the name of the computer that is running the Task Scheduler service that the user is connected to.
        /// </summary>
        [DefaultValue((string)null)]
        public string TargetServer
        {
			get { return targetServer; }
			set
            {
                if (!targetServer.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    targetServer = value;
                    Connect();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user account domain to be used when connecting to the <see cref="TargetServer"/>.
        /// </summary>
        /// <value>The user account domain.</value>
        [DefaultValue((string)null)]
        public string UserAccountDomain
        {
			get { return ShouldSerializeUserAccountDomain() ? userDomain : null; }
			set
            {
                if (!userDomain.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    userDomain = value;
					Connect();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user name to be used when connecting to the <see cref="TargetServer"/>.
        /// </summary>
        /// <value>The user name.</value>
        [DefaultValue((string)null)]
        public string UserName
        {
            get { return ShouldSerializeUserName() ? userName : null; }
            set
            {
                if (!userName.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    userName = value;
                    Connect();
                }
            }
        }

        /// <summary>
        /// Gets or sets the user password to be used when connecting to the <see cref="TargetServer"/>.
        /// </summary>
        /// <value>The user password.</value>
        [DefaultValue((string)null)]
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                if (!userPassword.Equals(value, StringComparison.InvariantCulture))
                {
                    userPassword = value;
					Connect();
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the component can raise an event.
        /// </summary>
        /// <value></value>
        /// <returns>true if the component can raise events; otherwise, false. The default is true.
        /// </returns>
        protected override bool CanRaiseEvents
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a new task, registers the taks, and returns the instance.
        /// </summary>
        /// <param name="path">The task name. If this value is NULL, the task will be registered in the root task folder and the task name will be a GUID value that is created by the Task Scheduler service. A task name cannot begin or end with a space character. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
        /// <param name="trigger">The <see cref="Trigger"/> to determine when to run the task.</param>
        /// <param name="action">The <see cref="Action"/> to determine what happens when the task is triggered.</param>
        /// <returns>A <see cref="Task"/> instance of the registered task.</returns>
        public Task AddTask(string path, Trigger trigger, Action action)
        {
            TaskDefinition td = NewTask();
            td.Principal.LogonType = TaskLogonType.InteractiveToken;

            // Create a trigger that will fire the task at a specific date and time
            td.Triggers.Add(trigger);

            // Create an action that will launch Notepad whenever the trigger fires
            td.Actions.Add(action);

            // Register the task in the root folder
            return RootFolder.RegisterTaskDefinition(path, td);
        }

        /// <summary>
        /// Gets the path to a folder of registered tasks.
        /// </summary>
        /// <param name="folderName">The path to the folder to retrieve. Do not use a backslash following the last folder name in the path. The root task folder is specified with a backslash (\). An example of a task folder path, under the root task folder, is \MyTaskFolder. The '.' character cannot be used to specify the current task folder and the '..' characters cannot be used to specify the parent task folder in the path.</param>
        /// <returns><see cref="TaskFolder"/> instance for the requested folder.</returns>
        /// <exception cref="Exception">Requested folder was not found.</exception>
        /// <exception cref="NotV1SupportedException">Folder other than the root (\) was requested on a system not supporting Task Scheduler 2.0.</exception>
        public TaskFolder GetFolder(string folderName)
        {
            return v2 ? new TaskFolder(this, v2TaskService.GetFolder(folderName)) : new TaskFolder(this);
        }

        /// <summary>
        /// Gets a collection of running tasks.
        /// </summary>
        /// <param name="includeHidden">True to include hidden tasks.</param>
        /// <returns><see cref="RunningTaskCollection"/> instance with the list of running tasks.</returns>
        public RunningTaskCollection GetRunningTasks(bool includeHidden)
        {
            return v2 ? new RunningTaskCollection(this, v2TaskService.GetRunningTasks(includeHidden ? 1 : 0)) : new RunningTaskCollection(this);
        }

        /// <summary>
        /// Gets the task with the specified path.
        /// </summary>
        /// <param name="taskPath">The task path.</param>
        /// <returns>The task.</returns>
        public Task GetTask(string taskPath)
        {
            Task t = null;
            if (v2)
            {
                V2Interop.IRegisteredTask iTask = GetTask(this.v2TaskService, taskPath);
                if (iTask != null)
                    t = new Task(this, iTask);
            }
            else
            {
                V1Interop.ITask iTask = GetTask(this.v1TaskScheduler, taskPath);
                if (iTask != null)
                    t = new Task(this, iTask);
            }
            return t;
        }

        void ISupportInitialize.BeginInit()
        {
            initializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            initializing = false;
            Connect();
        }

        /// <summary>
        /// Returns an empty task definition object to be filled in with settings and properties and then registered using the <see cref="TaskFolder.RegisterTaskDefinition(string, TaskDefinition)"/> method.
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

        internal static V2Interop.IRegisteredTask GetTask(V2Interop.ITaskService iSvc, string name)
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

        internal static V1Interop.ITask GetTask(V1Interop.ITaskScheduler iSvc, string name)
        {
            Guid ITaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
            try { return iSvc.Activate(name, ref ITaskGuid); } catch {}
            return null;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (v2TaskService != null)
                Marshal.ReleaseComObject(v2TaskService);
            if (v1TaskScheduler != null)
                Marshal.ReleaseComObject(v1TaskScheduler);
            if (v1Impersonation != null)
                v1Impersonation.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Connects this instance of the <see cref="TaskService"/> class to a running Task Scheduler.
        /// </summary>
        private void Connect()
        {
			if (!initializing && !DesignMode &&
				((!string.IsNullOrEmpty(userDomain) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPassword)) ||
				(string.IsNullOrEmpty(userDomain) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(userPassword))))
			{
				if (hasV2 && !forceV1)
				{
					v2 = true;
					v2TaskService = new V2Interop.TaskSchedulerClass();
					if (!string.IsNullOrEmpty(targetServer))
					{
						// Check to ensure character only server name. (Suggested by bigsan)
						if (targetServer.StartsWith(@"\"))
							targetServer = targetServer.TrimStart('\\');
						// Make sure null is provided for local machine to compensate for a native library oddity (Found by ctrollen)
						if (targetServer.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase))
							targetServer = null;
					}
					v2TaskService.Connect(targetServer, userName, userDomain, userPassword);
					targetServer = v2TaskService.TargetServer;
					userName = v2TaskService.ConnectedUser;
					userDomain = v2TaskService.ConnectedDomain;
					maxVer = GetV2Version();
				}
				else
				{
					v1Impersonation = new WindowsImpersonatedIdentity(userName, userDomain, userPassword);
					V1Interop.CTaskScheduler csched = new V1Interop.CTaskScheduler();
					v1TaskScheduler = (V1Interop.ITaskScheduler)csched;
					if (!string.IsNullOrEmpty(targetServer))
					{
						// Check to ensure UNC format for server name. (Suggested by bigsan)
						if (!targetServer.StartsWith(@"\\"))
							targetServer = @"\\" + targetServer;
						v1TaskScheduler.SetTargetComputer(targetServer);
					}
					targetServer = v1TaskScheduler.GetTargetComputer();
					maxVer = v1Ver;
				}
			}
        }

        private Version GetV2Version()
        {
            uint v = v2TaskService.HighestVersion;
            return new Version((int)(v >> 16), (int)(v & 0x0000FFFF));
        }

        private void ResetHighestSupportedVersion()
        {
            if (this.Connected)
                this.maxVer = v2TaskService != null ? GetV2Version() : v1Ver;
            else
                this.maxVer = hasV2 ? (Environment.OSVersion.Version.Minor > 0 ? new Version(1, 3) : new Version(1, 2)) : v1Ver;
        }

        private bool ShouldSerializeHighestSupportedVersion()
        {
            return (hasV2 && maxVer <= v1Ver);
        }

		private bool ShouldSerializeTargetServer()
		{
			return !targetServer.Equals(System.Environment.MachineName, StringComparison.InvariantCultureIgnoreCase);
		}

		private bool ShouldSerializeUserAccountDomain()
		{
			return !userDomain.Equals(System.Environment.UserDomainName, StringComparison.InvariantCultureIgnoreCase);
		}

		private bool ShouldSerializeUserName()
		{
			return !userName.Equals(System.Environment.UserName, StringComparison.InvariantCultureIgnoreCase);
		}

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
				if (value is string)
					return new Version(value as string);
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}