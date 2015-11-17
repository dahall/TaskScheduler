using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Information about the task event.
	/// </summary>
	public class TaskEventArgs : EventArgs
	{
		private TaskService taskSvc;

		internal TaskEventArgs(TaskEvent evt, TaskService ts = null)
		{
			TaskEvent = evt;
			taskSvc = ts;
		}

		/// <summary>
		/// If possible, gets the task associated with this event.
		/// </summary>
		/// <value>
		/// The task or <c>null</c> if unable to retrieve.
		/// </value>
		public Task Task => taskSvc?.GetTask(TaskPath);

		/// <summary>
		/// Gets the <see cref="TaskEvent"/>.
		/// </summary>
		/// <value>
		/// The TaskEvent.
		/// </value>
		public TaskEvent TaskEvent { get; }

		/// <summary>
		/// Gets the task nane.
		/// </summary>
		/// <value>
		/// The task name.
		/// </value>
		public string TaskName => System.IO.Path.GetFileName(TaskEvent.TaskPath);

		/// <summary>
		/// Gets the task path.
		/// </summary>
		/// <value>
		/// The task path.
		/// </value>
		public string TaskPath => TaskEvent.TaskPath;
	}

	/// <summary>
	/// Watches system events related to tasks and issues a <see cref="TaskEventWatcher.EventRecorded"/> event when the filtered conditions are met.
	/// </summary>
	[DefaultEvent(nameof(TaskEventWatcher.EventRecorded)), DefaultProperty(nameof(TaskEventWatcher.Folder))]
	public class TaskEventWatcher : Component, ISupportInitialize
	{
		private const string root = "\\";
		private const string star = "*";

		private bool disposed;
		private bool enabled;
		private string folder = root;
		private bool includeSubfolders;
		private bool initializing;
		private EventLogWatcher watcher;
		private ISynchronizeInvoke synchronizingObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher"/> class. If other
		/// properties are not set, this will watch for all events for all tasks on the local machine.
		/// </summary>
		public TaskEventWatcher() : this(TaskService.Instance)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher"/> class watching only
		/// those events for the task with the provided path on the local machine.
		/// </summary>
		/// <param name="taskPath">The full path (folders and name) of the task to watch.</param>
		/// <exception cref="System.ArgumentException">$Invalid task name: {taskPath}</exception>
		public TaskEventWatcher(string taskPath) : this()
		{
			InitTaskPath(taskPath);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher"/> class watching only
		/// those events for the specified task.
		/// </summary>
		/// <param name="task">The task to watch.</param>
		/// <exception cref="ArgumentNullException">Occurs if the <paramref name="task"/> is <c>null</c>.</exception>
		public TaskEventWatcher(Task task) : this(task?.TaskService)
		{
			if (task == null)
				throw new ArgumentNullException(nameof(task));
			InitTask(task);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher"/> class watching only those events for
		/// the tasks whose name matches the <paramref name="taskFilter"/> in the specified <paramref name="taskFolder"/>
		/// and optionally all subfolders.
		/// </summary>
		/// <param name="taskFolder">The task folder to watch.</param>
		/// <param name="taskFilter">The filter for task names using standard file system wildcards. Use "*" to include all tasks.</param>
		/// <param name="includeSubfolders">if set to <c>true</c> include events from tasks subfolders.</param>
		/// <exception cref="ArgumentNullException">Occurs if the <paramref name="taskFolder"/> is <c>null</c>.</exception>
		public TaskEventWatcher(TaskFolder taskFolder, string taskFilter = "*", bool includeSubfolders = false) : this(taskFolder?.TaskService)
		{
			if (taskFolder == null)
				throw new ArgumentNullException(nameof(taskFolder));
			InitTaskFolder(taskFolder, taskFilter, includeSubfolders);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher" /> class.
		/// </summary>
		/// <param name="folder">The task folder to watch.</param>
		/// <param name="taskFilter">The filter for task names using standard file system wildcards. Use "*" to include all tasks.</param>
		/// <param name="includeSubfolders">if set to <c>true</c> include events from tasks subfolders.</param>
		public TaskEventWatcher(string folder, string taskFilter, bool includeSubfolders) : this()
		{
			InitTaskFolder(folder, taskFilter, includeSubfolders);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher" /> class on a remote machine.
		/// </summary>
		/// <param name="machineName">Name of the remote machine.</param>
		/// <param name="taskPath">The task path.</param>
		/// <param name="domain">The domain of the user account.</param>
		/// <param name="user">The user name with permissions on the remote machine.</param>
		/// <param name="password">The password for the user.</param>
		public TaskEventWatcher(string machineName, string taskPath, string domain = null, string user = null, string password = null) : this(new TaskService(machineName, user, domain, password))
		{
			InitTaskPath(taskPath);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventWatcher" /> class on a remote machine.
		/// </summary>
		/// <param name="machineName">Name of the remote machine.</param>
		/// <param name="folder">The task folder to watch.</param>
		/// <param name="taskFilter">The filter for task names using standard file system wildcards. Use "*" to include all tasks.</param>
		/// <param name="includeSubfolders">if set to <c>true</c> include events from tasks subfolders.</param>
		/// <param name="domain">The domain of the user account.</param>
		/// <param name="user">The user name with permissions on the remote machine.</param>
		/// <param name="password">The password for the user.</param>
		public TaskEventWatcher(string machineName, string folder, string taskFilter = "*", bool includeSubfolders = false, string domain = null, string user = null, string password = null) : this(new TaskService(machineName, user, domain, password))
		{
			InitTaskFolder(folder, taskFilter, includeSubfolders);
		}

		private TaskEventWatcher(TaskService ts)
		{
			TaskService = ts;
			Filter = new EventFilter(this);
		}

		/// <summary>
		/// Occurs when a task or the task engine records an event.
		/// </summary>
		[Category("Action"), Description("Event recorded by a task or the task engine.")]
		public event EventHandler<TaskEventArgs> EventRecorded;

		/// <summary>
		/// Gets or sets a value indicating whether the component is enabled.
		/// </summary>
		/// <value>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether the component is enabled.")]
		public bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled != value)
				{
					enabled = value;
					if (!IsSuspended())
					{
						if (enabled)
							StartRaisingEvents();
						else
							StopRaisingEvents();
					}
				}
			}
		}

		/// <summary>
		/// Gets the filter for this <see cref="TaskEventWatcher"/>.
		/// </summary>
		/// <value>
		/// The filter.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Behavior"), Description("Indicates the filter for the watcher.")]
		public EventFilter Filter { get; }

		/// <summary>
		/// Gets or sets the folder to watch.
		/// </summary>
		/// <value>
		/// The folder path to watch. This value should include the leading "\" to indicate the root folder.
		/// </value>
		/// <exception cref="System.ArgumentException">Thrown if the folder specified does not exist or contains invalid characters.</exception>
		[DefaultValue(root), Category("Behavior"), Description("Indicates the folder to watch.")]
		public string Folder
		{
			get { return folder; }
			set
			{
				if (string.IsNullOrEmpty(value))
					value = root;
				if (string.Compare(folder, value, StringComparison.OrdinalIgnoreCase) != 0)
				{
					if ((base.DesignMode && (value.IndexOfAny(new char[] { '*', '?' }) != -1 || value.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)) || (TaskService.GetFolder(value) == null))
						throw new ArgumentException($"Invalid folder name: {value}");

					folder = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to include events from subfolders when the
		/// <see cref="Folder"/> property is set. If the <see cref="TaskEventWatcher.EventFilter.TaskName"/> property is set,
		/// this property is ignored.
		/// </summary>
		/// <value><c>true</c> if include events from subfolders; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether to include events from subfolders.")]
		public bool IncludeSubfolders
		{
			get { return includeSubfolders; }
			set
			{
				if (includeSubfolders != value)
				{
					includeSubfolders = value;
					Restart();
				}
			}
		}

		/// <summary>
		/// Gets or sets an <see cref="ISite"/> for the TaskEventWatcher.
		/// </summary>
		/// <value>
		/// The site.
		/// </value>
		[Browsable(false)]
		public override ISite Site
		{
			get { return base.Site; }
			set
			{
				base.Site = value;
				if (Site != null && Site.DesignMode)
					Enabled = true;
			}
		}

		/// <summary>
		/// Gets or sets the synchronizing object.
		/// </summary>
		/// <value>
		/// The synchronizing object.
		/// </value>
		[Browsable(false), DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (synchronizingObject == null && base.DesignMode)
				{
					var so = ((IDesignerHost)GetService(typeof(IDesignerHost)))?.RootComponent as ISynchronizeInvoke;
					if (so != null)
						synchronizingObject = so;
				}
				return synchronizingObject;
			}
			set { synchronizingObject = value; }
		}

		/// <summary>
		/// Gets or sets the name of the computer that is running the Task Scheduler service that the user is connected to.
		/// </summary>
		[Category("Connection"), DefaultValue((string)null), Description("The name of the computer to connect to.")]
		public string TargetServer
		{
			get { return ShouldSerializeTargetServer() ? TaskService.TargetServer : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, TaskService.TargetServer, true) != 0)
				{
					TaskService.TargetServer = value;
					Restart();
				}
			}
		}

		/// <summary>
		/// Gets or sets the user account domain to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user account domain.</value>
		[Category("Connection"), DefaultValue((string)null), Description("The user account domain to be used when connecting.")]
		public string UserAccountDomain
		{
			get { return ShouldSerializeUserAccountDomain() ? TaskService.UserAccountDomain : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, TaskService.UserAccountDomain, true) != 0)
				{
					TaskService.UserAccountDomain = value;
					Restart();
				}
			}
		}

		/// <summary>
		/// Gets or sets the user name to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user name.</value>
		[Category("Connection"), DefaultValue((string)null), Description("The user name to be used when connecting.")]
		public string UserName
		{
			get { return ShouldSerializeUserName() ? TaskService.UserName : null; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, TaskService.UserName, true) != 0)
				{
					TaskService.UserName = value;
					Restart();
				}
			}
		}

		/// <summary>
		/// Gets or sets the user password to be used when connecting to the <see cref="TargetServer"/>.
		/// </summary>
		/// <value>The user password.</value>
		[Category("Connection"), DefaultValue((string)null), Description("The user password to be used when connecting.")]
		public string UserPassword
		{
			get { return TaskService.UserPassword; }
			set
			{
				if (value == null || value.Trim() == string.Empty) value = null;
				if (string.Compare(value, TaskService.UserPassword, true) != 0)
				{
					TaskService.UserPassword = value;
					Restart();
				}
			}
		}

		/// <summary>
		/// Gets the task service.
		/// </summary>
		/// <value>
		/// The task service.
		/// </value>
		internal TaskService TaskService { get; set; }

		/// <summary>
		/// Gets a value indicating if watching is available.
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool IsHandleInvalid => watcher == null;

		/// <summary>
		/// Signals the object that initialization is starting.
		/// </summary>
		public void BeginInit()
		{
			bool enabled = this.enabled;
			StopRaisingEvents();
			this.enabled = enabled;
			TaskService.BeginInit();
			initializing = true;
		}

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			initializing = false;
			TaskService.EndInit();
			if (enabled)
				StartRaisingEvents();
		}

		/// <summary>
		/// Releases the unmanaged resources used by the FileSystemWatcher and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					StopRaisingEvents();
					TaskService = null;
				}
				else
				{
					StopListening();
				}
			}
			finally
			{
				disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>
		/// Fires the <see cref="EventRecorded"/> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="Microsoft.Win32.TaskScheduler.TaskEventArgs" /> instance containing the event data.</param>
		protected virtual void OnEventRecorded(object sender, TaskEventArgs e)
		{
			var h = EventRecorded;
			if (h != null)
			{
				if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
					SynchronizingObject.BeginInvoke(h, new object[] { this, e });
				else
					h(sender, e);
			}
		}

		private void InitTask(Task task)
		{
			Filter.TaskName = task.Name;
			Folder = task.Folder.Path;
		}

		private void InitTaskFolder(TaskFolder taskFolder, string taskFilter, bool includeSubfolders)
		{
			this.includeSubfolders = includeSubfolders;
			Filter.TaskName = taskFilter;
			Folder = taskFolder.Path;
		}

		private void InitTaskFolder(string taskFolder, string taskFilter, bool includeSubfolders)
		{
			TaskFolder fld;
			if ((fld = TaskService.GetFolder(taskFolder)) == null)
				throw new ArgumentException($"Invalid task folder name: {taskFolder}");
			InitTaskFolder(fld, taskFilter, includeSubfolders);
		}

		private void InitTaskPath(string taskPath)
		{
			Task task;
			if ((task = TaskService.GetTask(taskPath)) == null)
				throw new ArgumentException($"Invalid task name: {taskPath}");
			InitTask(task);
		}

		private bool IsSuspended()
		{
			if (!initializing)
				return base.DesignMode;
			return true;
		}

		private void ReleaseWatcher()
		{
			if (watcher != null)
			{
				watcher.Enabled = false;
				watcher.EventRecordWritten -= Watcher_EventRecordWritten;
				watcher = null;
			}
		}

		private void Restart()
		{
			if (!IsSuspended() && enabled)
			{
				StopRaisingEvents();
				StartRaisingEvents();
			}
		}

		private void SetupWatcher()
		{
			ReleaseWatcher();
			string taskPath = null;
			if (Filter.Wildcard == null)
				taskPath = System.IO.Path.Combine(folder, Filter.TaskName);
			var log = new TaskEventLog(taskPath, Filter.EventIds, Filter.EventLevels, DateTime.Now, TargetServer, UserAccountDomain, UserName, UserPassword);
			log.Query.ReverseDirection = false;
			watcher = new EventLogWatcher(log.Query);
			log = null;
			watcher.EventRecordWritten += Watcher_EventRecordWritten;
		}

		private bool ShouldSerializeFilter() => Filter.ShouldSerialize();

		private bool ShouldSerializeTargetServer() => TaskService.TargetServer != null && !TaskService.TargetServer.Trim('\\').Equals(System.Environment.MachineName.Trim('\\'), StringComparison.InvariantCultureIgnoreCase);

		private bool ShouldSerializeUserAccountDomain() => TaskService.UserAccountDomain != null && !TaskService.UserAccountDomain.Equals(System.Environment.UserDomainName, StringComparison.InvariantCultureIgnoreCase);

		private bool ShouldSerializeUserName() => TaskService.UserName != null && !TaskService.UserName.Equals(System.Environment.UserName, StringComparison.InvariantCultureIgnoreCase);

		private void StartRaisingEvents()
		{
			if (disposed)
				throw new ObjectDisposedException(base.GetType().Name);

			if (!IsSuspended())
			{
				enabled = true;
				SetupWatcher();
				watcher.Enabled = true;
			}
		}

		private void StopListening()
		{
			enabled = false;
			ReleaseWatcher();
		}

		private void StopRaisingEvents()
		{
			if (IsSuspended())
				enabled = false;
			else if (!IsHandleInvalid)
				StopListening();
		}

		private void Watcher_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
		{
			var taskEvent = new TaskEvent(e.EventRecord);
			System.Diagnostics.Debug.WriteLine("Task event: " + taskEvent.ToString());

			// Get the task name and folder
			string name = System.IO.Path.GetFileNameWithoutExtension(taskEvent.TaskPath);
			string fld = System.IO.Path.GetDirectoryName(taskEvent.TaskPath);

			// Check folder and name filters
			if (IncludeSubfolders && !fld.StartsWith(folder, StringComparison.OrdinalIgnoreCase))
				return;
			if (!IncludeSubfolders && string.Compare(folder, fld, StringComparison.OrdinalIgnoreCase) != 0)
				return;
			if (Filter.Wildcard != null && !Filter.Wildcard.IsMatch(name))
				return;

			OnEventRecorded(this, new TaskEventArgs(taskEvent, TaskService));
		}

		/// <summary>
		/// Holds filter information for a <see cref="TaskEventWatcher"/>.
		/// </summary>
		[TypeConverter(typeof(ExpandableObjectConverter)), Serializable]
		public class EventFilter
		{
			private string filter = star;
			private Wildcard filterWildcard = new Wildcard(star);
			private int[] ids = null;
			private int[] levels = null;
			private TaskEventWatcher parent;

			internal EventFilter(TaskEventWatcher parent)
			{
				this.parent = parent;
			}

			/// <summary>
			/// Gets or sets an optional array of event identifiers to use when filtering those events that will fire a <see cref="TaskEventWatcher.EventRecorded"/> event.
			/// </summary>
			/// <value>
			/// The array of event identifier filters. All know task event identifiers are declared in the <see cref="StandardTaskEventId"/> enumeration.
			/// </value>
			[DefaultValue(null), Category("Filter"), Description("An array of event identifiers to use when filtering.")]
			public int[] EventIds
			{
				get { return ids; }
				set
				{
					if (ids != value)
					{
						ids = value;
						parent.Restart();
					}
				}
			}

			/// <summary>
			/// Gets or sets an optional array of event levels to use when filtering those events that will fire a <see cref="TaskEventWatcher.EventRecorded"/> event.
			/// </summary>
			/// <value>
			/// The array of event levels. While event providers can define custom levels, most will use integers defined in the <see cref="StandardEventLevel"/> enumeration.
			/// </value>
			[DefaultValue(null), Category("Filter"), Description("An array of event levels to use when filtering.")]
			public int[] EventLevels
			{
				get { return levels; }
				set
				{
					if (levels != value)
					{
						levels = value;
						parent.Restart();
					}
				}
			}

			/// <summary>
			/// Gets or sets the task name, which can utilize wildcards, to look for when watching a folder.
			/// </summary>
			/// <value>A task name or wildcard.</value>
			[DefaultValue(star), Category("Filter"), Description("A task name, which can utilize wildcards, for filtering.")]
			public string TaskName
			{
				get { return filter; }
				set
				{
					if (string.IsNullOrEmpty(value))
						value = star;
					if (string.Compare(filter, value, StringComparison.OrdinalIgnoreCase) != 0)
					{
						filter = value;
						filterWildcard = (value.IndexOfAny(new char[] { '?', '*' }) == -1) ? null : new Wildcard(value);
						parent.Restart();
					}
				}
			}

			internal Wildcard Wildcard => filterWildcard;

			/// <summary>
			/// Returns a <see cref="System.String" /> that represents this instance.
			/// </summary>
			/// <returns>
			/// A <see cref="System.String" /> that represents this instance.
			/// </returns>
			public override string ToString() => filter + (levels == null ? "" : " +levels") + (ids == null ? "" : " +id's");

			internal bool ShouldSerialize() => ids != null || levels != null || filter != star;
		}
	}
}