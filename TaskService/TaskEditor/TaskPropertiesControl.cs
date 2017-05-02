using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Control which allows for the editing of all properties of a <see cref="TaskDefinition"/>.
	/// </summary>
	[Designer(typeof(Design.TaskPropertiesControlDesigner)), DesignTimeVisible(true)]
	[DefaultProperty("AvailableTabs"), DefaultEvent("ComponentError")]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskControl")]
	public partial class TaskPropertiesControl : UserControl, ITaskEditor
	{
		internal const string runTimesTempTaskPrefix = "TempTask-";

		private static readonly TimeSpan2 PT1D = TimeSpan2.FromDays(1);
		private static readonly SecEdShim secEd = SecEdShim.GetNew();

		private AvailableTaskTabs availableTabs = AvailableTaskTabs.All;
		private bool editable;
		//private bool flagExecutorIsCurrentUser, flagExecutorIsTheMachineAdministrator;
		private bool flagUserIsAnAdmin, flagExecutorIsServiceAccount, flagRunOnlyWhenUserIsLoggedOn, flagExecutorIsGroup;
		private bool onAssignment;
		private string runTimesTaskName;
		private readonly TabPage[] tabPages;
		private Task task;
		private TaskDefinition td;
		private bool v2 = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskPropertiesControl"/> class.
		/// </summary>
		public TaskPropertiesControl()
		{
			InitializeComponent();

			// Push all tab pages into a list so they don't get garbage collected while not displayed
			tabPages = new TabPage[tabControl.TabPages.Count];
			for (var i = 0; i < tabControl.TabPages.Count; i++)
				tabPages[i] = tabControl.TabPages[i];

			// Try to get the system help topic from the registry
			try
			{
				using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MMC\SnapIns\FX:{c7b8fb06-bfe1-4c2e-9217-7a69a95bbac4}"))
					helpProvider.HelpNamespace = key?.GetValue("HelpTopic", string.Empty).ToString();
			}
			catch { }

			// Settings for conditionsTab
			long allVal;
			taskIdleDurationCombo.Items.AddRange(new[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromMinutes(60) });
			taskIdleWaitTimeoutCombo.FormattedZero = EditorProperties.Resources.TimeSpanDoNotWait;
			taskIdleWaitTimeoutCombo.Items.AddRange(new[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });

			// Settings for settingsTab
			taskRestartIntervalCombo.Items.AddRange(new[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });
			taskExecutionTimeLimitCombo.Items.AddRange(new[] { TimeSpan2.FromHours(1), TimeSpan2.FromHours(2), TimeSpan2.FromHours(4), TimeSpan2.FromHours(8), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1), TimeSpan2.FromDays(3) });
			taskDeleteAfterCombo.FormattedZero = EditorProperties.Resources.TimeSpanImmediately;
			taskDeleteAfterCombo.Items.AddRange(new[] { TimeSpan2.Zero, TimeSpan2.FromDays(30), TimeSpan2.FromDays(90), TimeSpan2.FromDays(180), TimeSpan2.FromDays(365) });
			ComboBoxExtension.InitializeFromEnum(taskMultInstCombo.Items, typeof(TaskInstancesPolicy), EditorProperties.Resources.ResourceManager, "TaskInstances", out allVal);

			// Settings for addPropTab
			ComboBoxExtension.InitializeFromEnum(principalSIDTypeCombo.Items, typeof(TaskProcessTokenSidType), EditorProperties.Resources.ResourceManager, "SIDType", out allVal);
			ComboBoxExtension.InitializeFromEnum(taskPriorityCombo.Items, typeof(System.Diagnostics.ProcessPriorityClass), EditorProperties.Resources.ResourceManager, "ProcessPriority", out allVal);
			principalReqPrivilegesDropDown.Sorted = true;
			principalReqPrivilegesDropDown.InitializeFromEnum(typeof(TaskPrincipalPrivilege), EditorProperties.Resources.ResourceManager, "");
			taskMaintenanceDeadlineCombo.FormattedZero = EditorProperties.Resources.TimeSpanUnset;
			taskMaintenanceDeadlineCombo.Items.AddRange(new[] { TimeSpan2.Zero, TimeSpan2.FromDays(1), TimeSpan2.FromDays(2), TimeSpan2.FromDays(7), TimeSpan2.FromDays(14), TimeSpan2.FromDays(30) });
			taskMaintenancePeriodCombo.FormattedZero = EditorProperties.Resources.TimeSpanUnset;
			taskMaintenancePeriodCombo.Items.AddRange(new[] { TimeSpan2.Zero, TimeSpan2.FromDays(1), TimeSpan2.FromDays(2), TimeSpan2.FromDays(7), TimeSpan2.FromDays(14), TimeSpan2.FromDays(30) });

			// Settings for shown tabs
			AvailableTabs = AvailableTaskTabs.Default;

			// Initialize all the controls as not editable
			InitControls(false);
		}

		/// <summary>
		/// Error thrown within the component.
		/// </summary>
		public class ComponentErrorEventArgs : EventArgs
		{
			/// <summary>
			/// Empty arguments signifying that the error has been cleared.
			/// </summary>
			public new static readonly ComponentErrorEventArgs Empty = new ComponentErrorEventArgs();

			internal ComponentErrorEventArgs(Exception ex = null, string err = null)
			{
				ThrownException = ex;
				ErrorText = err;
			}

			/// <summary>
			/// Gets the thrown exception on the error. This value may be null.
			/// </summary>
			public Exception ThrownException { get; }

			/// <summary>
			/// Gets the text associated with the error event. This value may be null.
			/// </summary>
			public string ErrorText { get; }
		}

		/// <summary>
		/// Occurs when a component entry has an error.
		/// </summary>
		[Description("Occurs when a component entry has an error."), Category("Behavior")]
		public event EventHandler<ComponentErrorEventArgs> ComponentError;

		/// <summary>
		/// Gets or sets the available tabs.
		/// </summary>
		/// <value>
		/// The available tabs.
		/// </value>
		[DefaultValue(AvailableTaskTabs.Default), Category("Behavior"), Description("Determines which tabs are shown.")]
		public AvailableTaskTabs AvailableTabs
		{
			get { return availableTabs; }
			set
			{
				if (value != availableTabs)
				{
					var rembits = new System.Collections.BitArray(BitConverter.GetBytes((int)((value ^ availableTabs) & availableTabs)));
					var addbits = new System.Collections.BitArray(BitConverter.GetBytes((int)((value ^ availableTabs) & value)));
					for (var i = 0; i < tabPages.Length; i++)
					{
						if (rembits[i])
							tabControl.TabPages.Remove(tabPages[i]);
						if (addbits[i])
							InsertTab(i);
					}
					availableTabs = value;
					ValidateHistoryTab();
					tabControl.SelectedIndex = FindFirstVisibleTab();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to convert references to resource strings in libraries to their value.
		/// </summary>
		/// <value><c>true</c> if references to resource strings are converted; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Converts string references in libraries to value.")]
		public bool ConvertResourceStringReferences { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskPropertiesControl"/> is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		public bool Editable
		{
			get { return editable; }
			set
			{
				if (value && task != null && task.ReadOnly)
					value = false;

				if (editable != value)
				{
					editable = value;
					InitControls(value);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has error.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
		/// </value>
		[Browsable(false), DefaultValue(false), Description("Indicates whether there is currently an error with one of the components."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HasError { get; set; }

		/// <summary>
		/// Gets or sets the maximum history count. Use -1 for infinite or to retrieve all items.
		/// </summary>
		/// <value>
		/// The maximum history count.
		/// </value>
		[DefaultValue(-1), Category("Behavior"), Description("Determines maximum number of history items to retrieve.")]
		public int MaxHistoryCount
		{
			get { return taskHistoryControl1.MaxQueryCount; }
			set { taskHistoryControl1.MaxQueryCount = value; }
		}

		/// <summary>Gets or sets a value indicating whether a button is shown when editing an action that allows user to execute the current action.</summary>
		/// <value><c>true</c> if button is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a button is shown when editing an action that allows user to execute the current action.")]
		public bool ShowActionRunButton
		{
			get { return actionCollectionUI.ShowActionRunButton; }
			set { actionCollectionUI.ShowActionRunButton = value; }
		}

		/// <summary>Gets or sets a value indicating whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert unsupported actions.</summary>
		/// <value><c>true</c> if check box is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert unsupported actions.")]
		public bool ShowConvertActionsToPowerShellCheck
		{
			get { return actionCollectionUI.ShowPowerShellConversionCheck; }
			set { actionCollectionUI.ShowPowerShellConversionCheck = value; }
		}

		/// <summary>Gets or sets a value indicating whether errors are shown in the UI.</summary>
		/// <value><c>true</c> if errors are shown; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Additions' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Additions tab; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the 'Additions' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowAddedPropertiesTab
		{
			get { return AvailableTabs.IsFlagSet(AvailableTaskTabs.Properties); }
			set { AvailableTabs.SetFlags(AvailableTaskTabs.Properties, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Info' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Info tab; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the 'Info' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowRegistrationInfoTab
		{
			get { return AvailableTabs.IsFlagSet(AvailableTaskTabs.RegistrationInfo); }
			set { AvailableTabs.SetFlags(AvailableTaskTabs.RegistrationInfo, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Run Times' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Run Times tab; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether the 'Run Times' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowRunTimesTab
		{
			get { return AvailableTabs.IsFlagSet(AvailableTaskTabs.RunTimes); }
			set { AvailableTabs.SetFlags(AvailableTaskTabs.RunTimes, value); }
		}

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get
			{
				return task;
			}
			private set
			{
				task = value;
				if (task != null)
				{
					TaskService = task.TaskService;
					if (task.ReadOnly)
						Editable = false;
					TaskDefinition = task.Definition;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="TaskDefinition"/> in its edited state.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition
		{
			get { return td; }
			private set
			{
				if (TaskService == null)
					throw new ArgumentNullException(nameof(TaskDefinition), @"TaskDefinition cannot be set until TaskService has been set with a valid object.");

				if (value == null)
					throw new ArgumentNullException(nameof(TaskDefinition), @"TaskDefinition cannot be set to null.");

				td = value;
				onAssignment = true;
				SetVersionComboItems();
				IsV2 = td.Settings.Compatibility >= TaskCompatibility.V2 && TaskService.HighestSupportedVersion >= new Version(1, 2);

				tabControl.SelectedIndex = 0;

				flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(TaskService.TargetServer);
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(td.Principal.UserId);
				flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(TaskService.UserName);
				//this.flagExecutorIsTheMachineAdministrator = this.ExecutorIsTheMachineAdministrator(executor);

				// Remove invalid tabs on new task
				ValidateHistoryTab();

				// Set General tab
				if (td.Principal.LogonType == TaskLogonType.None) td.Principal.LogonType = TaskLogonType.InteractiveToken;
				SetUserControls(td.Principal.LogonType);
				taskNameText.Text = task?.Name ?? string.Empty;
				taskNameText.ReadOnly = !(task == null && editable);
				taskLocationText.Text = GetTaskLocation();
				if (string.IsNullOrEmpty(td.RegistrationInfo.Author))
					td.RegistrationInfo.Author = WindowsIdentity.GetCurrent().Name;
				taskAuthorText.Text = GetStringValue(td.RegistrationInfo.Author);
				taskDescText.Text = GetStringValue(td.RegistrationInfo.Description);
				taskRunLevelCheck.Checked = td.Principal.RunLevel == TaskRunLevel.Highest;
				taskHiddenCheck.Checked = td.Settings.Hidden;

				// Set Triggers tab
				triggerCollectionUI1.RefreshState();

				// Set Actions tab
				actionCollectionUI.RefreshState();

				// Set Conditions tab
				taskRestartOnIdleCheck.Checked = td.Settings.IdleSettings.RestartOnIdle;
				taskStopOnIdleEndCheck.Checked = td.Settings.IdleSettings.StopOnIdleEnd;
				taskIdleDurationCheck.Checked = td.Settings.RunOnlyIfIdle;
				taskIdleDurationCombo.Value = td.Settings.IdleSettings.IdleDuration;
				taskIdleWaitTimeoutCombo.Value = td.Settings.IdleSettings.WaitTimeout;
				UpdateIdleSettingsControls();
				taskDisallowStartIfOnBatteriesCheck.Checked = td.Settings.DisallowStartIfOnBatteries;
				taskStopIfGoingOnBatteriesCheck.Enabled = editable && td.Settings.DisallowStartIfOnBatteries;
				taskStopIfGoingOnBatteriesCheck.Checked = td.Settings.StopIfGoingOnBatteries;
				taskWakeToRunCheck.Checked = td.Settings.WakeToRun;
				taskStartIfConnectionCheck.Checked = td.Settings.RunOnlyIfNetworkAvailable;

				// Set Settings tab
				taskAllowDemandStartCheck.Checked = td.Settings.AllowDemandStart;
				taskStartWhenAvailableCheck.Checked = td.Settings.StartWhenAvailable;
				taskRestartIntervalCheck.Checked = td.Settings.RestartInterval != TimeSpan.Zero;
				taskRestartIntervalCheck_CheckedChanged(null, EventArgs.Empty);
				if (taskRestartIntervalCheck.Checked)
				{
					taskRestartIntervalCombo.Value = td.Settings.RestartInterval;
					taskRestartCountText.Value = td.Settings.RestartCount;
				}
				taskExecutionTimeLimitCheck.Checked = td.Settings.ExecutionTimeLimit != TimeSpan.Zero;
				taskExecutionTimeLimitCombo.Enabled = editable && taskExecutionTimeLimitCheck.Checked;
				taskExecutionTimeLimitCombo.Value = td.Settings.ExecutionTimeLimit;
				taskAllowHardTerminateCheck.Checked = td.Settings.AllowHardTerminate;
				taskDeleteAfterCheck.Checked = td.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero;
				taskDeleteAfterCombo.Enabled = editable && taskDeleteAfterCheck.Checked;
				taskDeleteAfterCombo.Value = td.Settings.DeleteExpiredTaskAfter == TimeSpan.FromSeconds(1) ? TimeSpan.Zero : td.Settings.DeleteExpiredTaskAfter;
				taskMultInstCombo.SelectedIndex = taskMultInstCombo.Items.IndexOf((long)td.Settings.MultipleInstances);

				// Set Info tab
				taskRegDocText.Text = GetStringValue(td.RegistrationInfo.Documentation);
				taskRegSDDLText.Text = td.RegistrationInfo.SecurityDescriptorSddlForm;
				if (secEd != null && IsV2)
					taskRegSDDLBtn.Visible = true;
				else
					taskRegLayoutPanel.SetColumnSpan(taskRegSDDLText, 2);
				taskRegSourceText.Text = GetStringValue(td.RegistrationInfo.Source);
				taskRegURIText.Text = td.RegistrationInfo.URI;
				taskRegVersionText.Text = td.RegistrationInfo.Version.ToString();

				// Set Additional tab
				taskEnabledCheck.Checked = td.Settings.Enabled;
				taskPriorityCombo.SelectedIndex = taskPriorityCombo.Items.IndexOf((long)td.Settings.Priority);
				taskDisallowStartOnRemoteAppSessionCheck.Checked = td.Settings.DisallowStartOnRemoteAppSession;
				taskUseUnifiedSchedulingEngineCheck.Checked = td.Settings.UseUnifiedSchedulingEngine;
				principalSIDTypeCombo.SelectedIndex = principalSIDTypeCombo.Items.IndexOf((long)td.Principal.ProcessTokenSidType);
				principalReqPrivilegesDropDown.CheckedFlagValue = 0;
				foreach (var s in td.Principal.RequiredPrivileges)
					principalReqPrivilegesDropDown.SetItemChecked(principalReqPrivilegesDropDown.Items.IndexOf(s.ToString()), true);
				taskVolatileCheck.Checked = td.Settings.Volatile;
				taskMaintenanceDeadlineCombo.Value = td.Settings.MaintenanceSettings.Deadline;
				taskMaintenancePeriodCombo.Value = td.Settings.MaintenanceSettings.Period;
				taskMaintenanceExclusiveCheck.Checked = td.Settings.MaintenanceSettings.Exclusive;
				UpdateUnifiedSchedulingEngineControls();

				// Set History tab
				taskHistoryControl1.Task = null;

				onAssignment = false;
			}
		}

		/// <summary>
		/// Gets or sets the folder for the task. If control is initialized with a <see cref="Task"/>, this value will be set to the folder of the registered task.
		/// </summary>
		/// <value>The task folder name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskFolder
		{
			get { return taskLocationText.Text; }
			set { taskLocationText.Text = value; }
		}

		/// <summary>
		/// Gets or sets the name of the task. If control is initialized with a <see cref="Task"/>, this value will be set to the name of the registered task.
		/// </summary>
		/// <value>The task name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskName
		{
			get { return taskNameText.Text; }
			set { taskNameText.Text = value; }
		}

		/// <summary>
		/// Gets the <see cref="TaskService"/> assigned at initialization.
		/// </summary>
		/// <value>The task service.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskService TaskService { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this task definition is v2.
		/// </summary>
		/// <value>
		/// <c>true</c> if this task definition is v2; otherwise, <c>false</c>.
		/// </value>
		public bool IsV2
		{
			get { return v2; }
			private set
			{
				if (v2 != value || onAssignment)
				{
					v2 = value;
					if (taskVersionCombo.Items.Count > 1 && taskVersionCombo.SelectedIndex == 0)
						taskVersionCombo.SelectedIndex = 1;
				}
			}
		}

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public void Initialize(TaskService service, TaskDefinition td = null)
		{
			TaskService = service;
			task = null;
			if (!this.IsDesignMode())
			{
				if (td == null)
				{
					var temp = service.NewTask();
					if (service.HighestSupportedVersion == new Version(1, 1))
						temp.Settings.Compatibility = TaskCompatibility.V1;
					TaskDefinition = temp;
				}
				else
					TaskDefinition = td;
			}
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			Task = task;
		}

		/// <summary>
		/// Reinitializes all the controls based on current <see cref="TaskDefinition" /> values.
		/// </summary>
		public void ReinitializeControls()
		{
			TaskDefinition = td;
		}

		internal static string BuildEnumString(string preface, object enumValue)
		{
			var vals = enumValue.ToString().Split(new[] { ", " }, StringSplitOptions.None);
			if (vals.Length == 0)
				return string.Empty;

			for (var i = 0; i < vals.Length; i++)
			{
				vals[i] = EditorProperties.Resources.ResourceManager.GetString(preface + vals[i], System.Globalization.CultureInfo.CurrentUICulture);
			}
			return string.Join(", ", vals);
		}

		/// <summary>Gets a string value that is converted if needed.</summary>
		/// <param name="input">The input string.</param>
		/// <returns>If the <see cref="ConvertResourceStringReferences"/> is <c>true</c>, and value is in the "$(string)" format, the reference will be pulled and returned.</returns>
		internal string GetStringValue(string input)
		{
			try
			{
				if (input != null && input.StartsWith(@"$(") && input.EndsWith(")") && ConvertResourceStringReferences)
					return NativeMethods.NativeResource.GetResourceString(input);
			}
			catch { }
			return input;
		}

		/// <summary>
		/// Raises the <see cref="ComponentError" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ComponentErrorEventArgs" /> instance containing the event data.</param>
		protected virtual void OnComponentError(ComponentErrorEventArgs e)
		{
			ComponentError?.Invoke(this, e);
		}

		private void availableConnectionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (availableConnectionsCombo.SelectedIndex > 0)
				{
					td.Settings.NetworkSettings.Id = ((NativeMethods.NetworkProfile)availableConnectionsCombo.SelectedItem).Id;
					td.Settings.NetworkSettings.Name = ((NativeMethods.NetworkProfile)availableConnectionsCombo.SelectedItem).Name;
				}
				else
				{
					td.Settings.NetworkSettings.Id = Guid.Empty;
					td.Settings.NetworkSettings.Name = null;
				}
			}
		}

		private void changePrincipalButton_Click(object sender, EventArgs e)
		{
			InvokeObjectPicker(TaskService.TargetServer);
		}

		private void conditionsTab_Enter(object sender, EventArgs e)
		{
			// Load network connections
			availableConnectionsCombo.BeginUpdate();
			availableConnectionsCombo.Items.Clear();
			availableConnectionsCombo.Items.Add(EditorProperties.Resources.AnyConnection);
			availableConnectionsCombo.Items.AddRange(NativeMethods.NetworkProfile.GetAllLocalProfiles());
			availableConnectionsCombo.EndUpdate();
			onAssignment = true;
			if (task == null || td.Settings.NetworkSettings.Id == Guid.Empty)
				availableConnectionsCombo.SelectedIndex = 0;
			else
				availableConnectionsCombo.SelectedItem = td.Settings.NetworkSettings.Id;
			onAssignment = false;
		}

		private int FindFirstVisibleTab(int startingIndex = -1)
		{
			var ins = -1;
			for (var i = startingIndex + 1; i < tabPages.Length; i++)
			{
				ins = tabControl.TabPages.IndexOf(tabPages[i]);
				if (ins != -1)
					return ins;
			}
			return tabControl.TabCount;
		}

		private void generalTab_Enter(object sender, EventArgs e)
		{
			SetVersionComboItems();
		}

		private string GetTaskLocation()
		{
			if (task == null || TaskService.HighestSupportedVersion.CompareTo(new Version(1, 1)) == 0)
				return @"\";
			return System.IO.Path.GetDirectoryName(task.Path);
		}

		private void historyTab_Enter(object sender, EventArgs e)
		{
			// Moved to historyTab_Intialize
		}

		private void InitControls(bool editable)
		{
			// General tab
			taskDescText.ReadOnly = !editable;
			changePrincipalButton.Visible = taskHiddenCheck.Enabled = taskRunLevelCheck.Enabled = taskVersionCombo.Enabled = editable;
			SetUserControls(td?.Principal.LogonType ?? TaskLogonType.InteractiveTokenOrPassword);

			// Triggers tab
			triggerCollectionUI1.RefreshState();

			// Actions tab
			actionCollectionUI.RefreshState();

			// Conditions tab
			conditionsTab.EnableChildren(editable);

			// Settings tab
			settingsTab.EnableChildren(editable);

			// Info tab
			regInfoTab.EnableChildren(editable);

			// Additions tab
			addPropTab.EnableChildren(editable);

			// If the task has already been set, then reset it to make sure all the items are enabled correctly
			if (td != null)
				TaskDefinition = td;

			// Setup specific controls
			taskVersionCombo_SelectedIndexChanged(null, EventArgs.Empty);
		}

		private void InsertTab(int idx)
		{
			var tab = tabPages[idx];
			if (tabControl.TabPages.IndexOf(tab) != -1)
				return;
			var h = tabControl.Handle;
			if (!tab.IsHandleCreated) tab.CreateControl();
			tabControl.TabPages.Insert(FindFirstVisibleTab(idx), tab);
		}

		private void InvokeObjectPicker(string targetComputerName)
		{
			string acct = String.Empty, sid;
			if (!HelperMethods.SelectAccount(this, targetComputerName, ref acct, out flagExecutorIsGroup, out flagExecutorIsServiceAccount, out sid))
				return;

			if (!ValidateAccountForSidType(acct))
				return;

			if (flagExecutorIsServiceAccount)
			{
				if (!v2 && acct != "SYSTEM")
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_NoGroupsUnderV1, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				flagExecutorIsGroup = false;
				if (v2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				//this.flagExecutorIsCurrentUser = false;
			}
			else if (flagExecutorIsGroup)
			{
				if (!v2)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_NoGroupsUnderV1, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				td.Principal.GroupId = acct;
				td.Principal.UserId = null;
				td.Principal.LogonType = TaskLogonType.Group;
				//this.flagExecutorIsCurrentUser = false;
			}
			else
			{
				if (v2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(objArray[0].ObjectName);
				if (td.Principal.LogonType == TaskLogonType.Group)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveToken;
				}
				else if (td.Principal.LogonType == TaskLogonType.ServiceAccount)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveTokenOrPassword;
				}
			}
			SetUserControls(td.Principal.LogonType);
		}

		private void runTimesTab_Enter(object sender, EventArgs e)
		{
			if (task == null)
				return;

			try
			{
				// Create a temporary task using current definition
				runTimesTaskName = runTimesTempTaskPrefix + Guid.NewGuid().ToString();
				var ttd = TaskService.NewTask();
				//this.TaskDefinition.Principal.CopyTo(ttd.Principal);
				ttd.Settings.Enabled = false;
				ttd.Settings.Hidden = true;
				ttd.Actions.Add(new ExecAction("rundll32.exe"));
				foreach (var tg in TaskDefinition.Triggers)
					if (tg.TriggerType != TaskTriggerType.Custom)
						ttd.Triggers.Add((Trigger)tg.Clone());
				var tempTask = TaskService.RootFolder.RegisterTaskDefinition(runTimesTaskName, ttd);
				if (tempTask != null)
				{
					taskRunTimesControl1.Show();
					tempTask.Enabled = true;
					taskRunTimesControl1.Initialize(tempTask, DateTime.Now, DateTime.Now.AddYears(1));
					tempTask.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				// On error, post and delete temporary task
				runTimesErrorLabel.Text = ShowErrors ? ex.ToString() : null;
				taskRunTimesControl1.Hide();
			}
		}

		private void runTimesTab_Leave(object sender, EventArgs e)
		{
			if (runTimesTaskName != null)
			{
				taskRunTimesControl1.Task = null;
				TaskService.RootFolder.DeleteTask(runTimesTaskName, false);
			}
			runTimesTaskName = null;
		}

		private void SetUserControls(TaskLogonType logonType)
		{
			var prevOnAssignment = onAssignment;
			onAssignment = true;
			switch (logonType)
			{
				case TaskLogonType.InteractiveToken:
					flagRunOnlyWhenUserIsLoggedOn = true;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = false;
					break;
				case TaskLogonType.Group:
					flagRunOnlyWhenUserIsLoggedOn = true;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = true;
					break;
				case TaskLogonType.ServiceAccount:
					flagRunOnlyWhenUserIsLoggedOn = false;
					flagExecutorIsServiceAccount = true;
					flagExecutorIsGroup = false;
					break;
				default:
					flagRunOnlyWhenUserIsLoggedOn = false;
					flagExecutorIsServiceAccount = false;
					flagExecutorIsGroup = false;
					break;
			}

			if (flagExecutorIsServiceAccount)
			{
				taskLoggedOnRadio.Enabled = false;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (flagExecutorIsGroup)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (flagRunOnlyWhenUserIsLoggedOn)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = false;
			}
			else
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = editable && v2;
			}

			taskLoggedOnRadio.Checked = flagRunOnlyWhenUserIsLoggedOn;
			taskLoggedOptionalRadio.Checked = !flagRunOnlyWhenUserIsLoggedOn;
			taskLocalOnlyCheck.Checked = !flagRunOnlyWhenUserIsLoggedOn && logonType == TaskLogonType.S4U;

			var user = td?.Principal.ToString();
			if (string.IsNullOrEmpty(user))
				user = WindowsIdentity.GetCurrent().Name;
			taskPrincipalText.Text = user;
			changePrincipalButton.Text = flagUserIsAnAdmin ? EditorProperties.Resources.ChangeUserBtn : EditorProperties.Resources.ChangeUserBtnNonAdmin;
			onAssignment = prevOnAssignment;
		}

		private class ComboItem : IEnableable
		{
			public readonly string Text;
			public readonly int Version;

			public ComboItem(string text, int ver, bool enabled = true) { Text = text; Version = ver; Enabled = enabled; }

			public bool Enabled { get; set; }

			public override bool Equals(object obj)
			{
				if (obj is ComboItem)
					return Version == ((ComboItem)obj).Version;
				if (obj is int)
					return Version == (int)obj;
				return Text.CompareTo(obj.ToString()) == 0;
			}

			public override int GetHashCode() => Version.GetHashCode();

			public override string ToString() => Text;
		}

		private void SetVersionComboItems()
		{
			const int expectedVersions = 6;

			taskVersionCombo.BeginUpdate();
			taskVersionCombo.Items.Clear();
			var versions = EditorProperties.Resources.TaskCompatibility.Split('|');
			if (versions.Length != expectedVersions)
				throw new ArgumentOutOfRangeException(nameof(TaskDefinition), @"Locale specific information about supported Operating Systems is insufficient.");
			var max = (TaskService == null) ? expectedVersions - 1 : Math.Min(TaskService.LibraryVersion.Minor, TaskService.HighestSupportedVersion.Minor);
			if (Environment.OSVersion.Version >= new Version(6, 2))
			{
				using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
					versions[TaskService.LibraryVersion.Minor] = key?.GetValue("ProductName", Environment.OSVersion).ToString();
			}
			var comp = td?.Settings.Compatibility ?? TaskCompatibility.V1;
			var lowestComp = td?.LowestSupportedVersion ?? TaskCompatibility.V1;
			if (lowestComp == TaskCompatibility.V1 && task != null && task.Folder.Path != "\\")
				lowestComp = TaskCompatibility.V2;
			switch (comp)
			{
				case TaskCompatibility.AT:
					for (var i = max; i > 1; i--)
						taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.Add(new ComboItem(versions[0], 0));
					break;
				default:
					for (var i = max; i > 0; i--)
						taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.IndexOf((int)comp);
					break;
			}
			taskVersionCombo.EndUpdate();
		}

		private void span_Validating(object sender, CancelEventArgs e)
		{
			var pkr = sender as TimeSpanPicker;
			var valid = pkr != null && pkr.Enabled && pkr.IsTextValid;
			e.Cancel = !valid;
			errorProvider.SetError(pkr, valid ? string.Empty : EditorProperties.Resources.Error_InvalidSpanValue);
		}

		private void tabControl_TabIndexChanged(object sender, EventArgs e)
		{
			if (task == null)
				return;

			if (tabControl.SelectedTab == historyTab)
			{
				if (taskHistoryControl1.Task == null)
					taskHistoryControl1.Task = task;
				else
					taskHistoryControl1.RefreshHistory();
			}
		}

		private void taskAllowDemandStartCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment && v2)
				td.Settings.AllowDemandStart = taskAllowDemandStartCheck.Checked;
		}

		private void taskAllowHardTerminateCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment && v2)
				td.Settings.AllowHardTerminate = taskAllowHardTerminateCheck.Checked;
		}

		private void taskDeleteAfterCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskDeleteAfterCombo.Enabled = editable && taskDeleteAfterCheck.Checked;
			if (!onAssignment)
			{
				taskDeleteAfterCombo.Value = taskDeleteAfterCheck.Checked ? TimeSpan.FromDays(30) : TimeSpan.Zero;
			}
		}

		private void taskDeleteAfterCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.DeleteExpiredTaskAfter = taskDeleteAfterCheck.Checked ? (taskDeleteAfterCombo.Value == TimeSpan2.Zero ? TimeSpan.FromSeconds(1) : (TimeSpan)taskDeleteAfterCombo.Value) : TimeSpan.Zero;
		}

		private void taskDescText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment && td != null)
				td.RegistrationInfo.Description = taskDescText.Text;
		}

		private void taskDisallowStartIfOnBatteriesCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStopIfGoingOnBatteriesCheck.Enabled = editable && taskDisallowStartIfOnBatteriesCheck.Checked;
			if (!onAssignment)
				td.Settings.DisallowStartIfOnBatteries = taskDisallowStartIfOnBatteriesCheck.Checked;
		}

		private void taskExecutionTimeLimitCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskExecutionTimeLimitCombo.Enabled = editable && taskExecutionTimeLimitCheck.Checked;
			if (!onAssignment)
			{
				taskExecutionTimeLimitCombo.Value = taskExecutionTimeLimitCheck.Checked ? TimeSpan.FromDays(3) : TimeSpan.Zero;
			}
		}

		private void taskExecutionTimeLimitCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.ExecutionTimeLimit = taskExecutionTimeLimitCombo.Value;
			taskExecutionTimeLimitCheck.Checked = taskExecutionTimeLimitCombo.Value != TimeSpan2.Zero;
		}

		private void taskHiddenCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Hidden = taskHiddenCheck.Checked;
		}

		private void taskIdleDurationCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				taskIdleDurationCombo.Value = TimeSpan.FromMinutes(10);
				taskIdleWaitTimeoutCombo.Value = TimeSpan.FromHours(1);
				td.Settings.RunOnlyIfIdle = taskIdleDurationCheck.Checked;
//				if (taskIdleDurationCheck.Checked)
//					td.Settings.IdleSettings.StopOnIdleEnd = true;
			}
			UpdateIdleSettingsControls();
		}

		private void taskIdleDurationCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.IdleSettings.IdleDuration = taskIdleDurationCombo.Value;
		}

		private void taskIdleWaitTimeoutCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.IdleSettings.WaitTimeout = taskIdleWaitTimeoutCombo.Value;
		}

		private void taskLocalOnlyCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = v2 ? ((taskLocalOnlyCheck.Checked) ? TaskLogonType.S4U : TaskLogonType.Password) : TaskLogonType.InteractiveTokenOrPassword;
		}

		private void taskLoggedOnRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
		}

		private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
		{
			taskLocalOnlyCheck.Enabled = editable && v2 && taskLoggedOptionalRadio.Checked && !(flagExecutorIsGroup | flagExecutorIsServiceAccount);
			taskLocalOnlyCheck_CheckedChanged(sender, e);
		}

		private void taskMultInstCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment && v2 && td != null)
				td.Settings.MultipleInstances = (TaskInstancesPolicy)Convert.ToInt32(((DropDownCheckListItem)taskMultInstCombo.SelectedItem).Value);
		}

		private void TaskPropertiesControl_Load(object sender, EventArgs e)
		{
			if ((availableTabs & AvailableTaskTabs.General) != 0)
				generalTab.SelectNextControl(taskNameText.ReadOnly ? (Control)taskNameText : generalTab, true, true, true, false);
		}

		private void taskRestartCountText_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.RestartCount = Convert.ToInt32(taskRestartCountText.Value);
		}

		private void taskRestartIntervalCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (taskRestartIntervalCheck.Checked)
				{
					taskRestartIntervalCombo.Value = TimeSpan.FromMinutes(1);
					taskRestartCountText.Value = 3;
				}
				else
				{
					taskRestartIntervalCombo.Value = TimeSpan.Zero;
					taskRestartCountText.Value = 0;
				}
			}
			taskRestartIntervalCombo.Enabled = taskRestartCountLabel.Enabled = taskRestartCountText.Enabled = editable && taskRestartIntervalCheck.Checked;
		}

		private void taskRestartIntervalCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.RestartInterval = taskRestartIntervalCombo.Value;
		}

		private void taskRestartOnIdleCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.IdleSettings.RestartOnIdle = taskRestartOnIdleCheck.Checked;
		}

		private void taskRunLevelCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.RunLevel = taskRunLevelCheck.Checked ? TaskRunLevel.Highest : TaskRunLevel.LUA;
		}

		private void taskStartIfConnectionCheck_CheckedChanged(object sender, EventArgs e)
		{
			availableConnectionsCombo.Enabled = editable && taskStartIfConnectionCheck.Checked && !taskUseUnifiedSchedulingEngineCheck.Checked;
			if (!onAssignment)
				td.Settings.RunOnlyIfNetworkAvailable = taskStartIfConnectionCheck.Checked;
		}

		private void taskStartWhenAvailableCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.StartWhenAvailable = taskStartWhenAvailableCheck.Checked;
		}

		private void taskStopIfGoingOnBatteriesCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.StopIfGoingOnBatteries = taskStopIfGoingOnBatteriesCheck.Checked;
		}

		private void taskStopOnIdleEndCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				td.Settings.IdleSettings.StopOnIdleEnd = taskStopOnIdleEndCheck.Checked;
				UpdateIdleSettingsControls();
			}
		}

		private void taskVersionCombo_GotFocus(object sender, EventArgs e)
		{
			var comp = td?.Settings.Compatibility ?? TaskCompatibility.V1;
			var lowestComp = td?.LowestSupportedVersion ?? TaskCompatibility.V1;
			for (var i = 0; i < taskVersionCombo.Items.Count; i++)
			{
				var ci = (ComboItem)taskVersionCombo.Items[i];
				ci.Enabled = ci.Version >= (int)lowestComp;
			}
		}

		private void taskVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			v2 = taskVersionCombo.SelectedIndex == -1 || ((ComboItem)taskVersionCombo.SelectedItem).Version > 1;
			var v2_1 = taskVersionCombo.SelectedIndex == -1 || ((ComboItem)taskVersionCombo.SelectedItem).Version > 2;
			var v2_2 = taskVersionCombo.SelectedIndex == -1 || ((ComboItem)taskVersionCombo.SelectedItem).Version > 3;
			var priorSetting = td?.Settings.Compatibility ?? TaskCompatibility.V1;
			if (!onAssignment && td != null && taskVersionCombo.SelectedIndex != -1)
				td.Settings.Compatibility = (TaskCompatibility)((ComboItem)taskVersionCombo.SelectedItem).Version;

			if ((onAssignment && task == null) || (sender != null && td != null && priorSetting > td.Settings.Compatibility))
			{
				try
				{
					if (!onAssignment)
						td?.Validate(true);
				}
				catch (InvalidOperationException ex)
				{
					var msg = new System.Text.StringBuilder();
					if (ShowErrors)
					{
						msg.AppendLine(EditorProperties.Resources.Error_TaskPropertiesIncompatible);
						foreach (var item in ex.Data.Keys)
							msg.AppendLine($"- {item} {ex.Data[item]}");
					}
					else
						msg.Append(EditorProperties.Resources.Error_TaskPropertiesIncompatibleSimple);
					MessageBox.Show(this, msg.ToString(), EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.IndexOf((int)priorSetting);
					return;
				}
			}
			taskRunLevelCheck.Enabled = taskAllowDemandStartCheck.Enabled = taskStartWhenAvailableCheck.Enabled =
				taskRestartIntervalCheck.Enabled = taskRestartAttemptTimesLabel.Enabled = 
				taskAllowHardTerminateCheck.Enabled = taskRunningRuleLabel.Enabled = taskMultInstCombo.Enabled =
				taskStartIfConnectionCheck.Enabled = taskRegSDDLText.Enabled = editable && v2;
			taskRestartIntervalCombo.Enabled = taskRestartCountLabel.Enabled = taskRestartCountText.Enabled = v2 && editable && taskRestartIntervalCheck.Checked;
			availableConnectionsCombo.Enabled = editable && v2 && taskStartIfConnectionCheck.Checked && !taskUseUnifiedSchedulingEngineCheck.Checked;
			principalSIDTypeLabel.Enabled = principalSIDTypeCombo.Enabled = principalReqPrivilegesLabel.Enabled = 
				principalReqPrivilegesDropDown.Enabled = taskDisallowStartOnRemoteAppSessionCheck.Enabled =
				taskUseUnifiedSchedulingEngineCheck.Enabled = principalSIDTypeCombo.Enabled = principalReqPrivilegesDropDown.Enabled =
				editable && v2_1;
			taskVolatileCheck.Enabled = taskMaintenanceDeadlineLabel.Enabled = taskMaintenanceDeadlineCombo.Enabled = 
				taskMaintenanceExclusiveCheck.Enabled = taskMaintenancePeriodLabel.Enabled = taskMaintenancePeriodCombo.Enabled = editable && v2_2;
		}

		private void taskWakeToRunCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.WakeToRun = taskWakeToRunCheck.Checked;
		}

		private void UpdateIdleSettingsControls()
		{
			var idleEnabled = taskIdleDurationCheck.Checked && editable;
			taskIdleDurationCombo.Enabled = taskIdleWaitTimeoutLabel.Enabled = 
				taskIdleWaitTimeoutCombo.Enabled = taskStopOnIdleEndCheck.Enabled = idleEnabled;
			taskRestartOnIdleCheck.Enabled = v2 && idleEnabled && td.Settings.IdleSettings.StopOnIdleEnd;
		}

		private void UpdateUnifiedSchedulingEngineControls()
		{
			var isSet = taskUseUnifiedSchedulingEngineCheck.Checked;
			var alreadyOnAssigment = onAssignment;
			onAssignment = true;
			availableConnectionsCombo.Enabled = editable && taskStartIfConnectionCheck.Checked && !isSet;
			taskAllowHardTerminateCheck.Enabled = editable && !isSet;
			// Update Multiple Instances policy combo
			taskMultInstCombo.BeginUpdate();
			long allVal;
			ComboBoxExtension.InitializeFromEnum(taskMultInstCombo.Items, typeof(TaskInstancesPolicy), EditorProperties.Resources.ResourceManager, "TaskInstances", out allVal);
			if (isSet)
				taskMultInstCombo.Items.RemoveAt(taskMultInstCombo.Items.IndexOf((long)TaskInstancesPolicy.StopExisting));
			taskMultInstCombo.SelectedIndex = taskMultInstCombo.Items.IndexOf((long)td.Settings.MultipleInstances);
			taskMultInstCombo.EndUpdate();
			if (!alreadyOnAssigment)
				onAssignment = false;
		}

		private void taskEnabledCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Enabled = taskEnabledCheck.Checked;
		}

		private void taskPriorityCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Priority = (System.Diagnostics.ProcessPriorityClass)Convert.ToInt32(((DropDownCheckListItem)taskPriorityCombo.SelectedItem).Value);
		}

		private void taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.DisallowStartOnRemoteAppSession = taskDisallowStartOnRemoteAppSessionCheck.Checked;
		}

		private void ResetForUnifiedSchedulingEngine()
		{
			if (td.Principal.LogonType == TaskLogonType.InteractiveTokenOrPassword)
			{
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
				SetUserControls(td.Principal.LogonType);
			}
			if (td.Settings.MultipleInstances == TaskInstancesPolicy.StopExisting)
				taskMultInstCombo.SelectedIndex = taskMultInstCombo.Items.IndexOf((long)TaskInstancesPolicy.IgnoreNew);
			if (availableConnectionsCombo.Items.Count > 0)
				availableConnectionsCombo.SelectedIndex = 0;
			taskAllowHardTerminateCheck.Checked = true;
			for (var i = td.Actions.Count - 1; i >= 0; i--)
			{
				if (td.Actions[i].ActionType == TaskActionType.SendEmail || td.Actions[i].ActionType == TaskActionType.ShowMessage)
				{
					td.Actions.RemoveAt(i);
					actionCollectionUI.RefreshState();
				}
			}
			for (var i = td.Triggers.Count - 1; i >= 0; i--)
			{
				if (td.Triggers[i].TriggerType == TaskTriggerType.Monthly || td.Triggers[i].TriggerType == TaskTriggerType.MonthlyDOW)
				{
					td.Triggers.RemoveAt(i);
					triggerCollectionUI1.RefreshState();
				}
				else
				{
					var t = td.Triggers[i];
					t.ExecutionTimeLimit = TimeSpan.Zero;
					if (t is ICalendarTrigger)
					{
						t.Repetition.Duration = t.Repetition.Interval = TimeSpan.Zero;
						t.Repetition.StopAtDurationEnd = false;
					}
					else
					{
						(t as EventTrigger)?.ValueQueries.Clear();
					}
				}
			}
		}

		private void taskUseUnifiedSchedulingEngineCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (taskUseUnifiedSchedulingEngineCheck.Checked)
				{
					if (!td.CanUseUnifiedSchedulingEngine())
					{
						if (MessageBox.Show(this, EditorProperties.Resources.UseUnifiedResetQuestion, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							ResetForUnifiedSchedulingEngine();
						else
							taskUseUnifiedSchedulingEngineCheck.Checked = false;
					}
				}
				td.Settings.UseUnifiedSchedulingEngine = taskUseUnifiedSchedulingEngineCheck.Checked;
				UpdateUnifiedSchedulingEngineControls();
			}
		}

		private bool ValidateAccountForSidType(string user)
		{
			if (!TaskPrincipal.ValidateAccountForSidType(user, td.Principal.ProcessTokenSidType))
			{
				MessageBox.Show(this, EditorProperties.Resources.Error_PrincipalSidTypeInvalid, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private void ValidateHistoryTab()
		{
			if (!this.IsDesignMode() && (availableTabs & AvailableTaskTabs.History) != 0)
			{
				if (Environment.OSVersion.Version.Major < 6 || task == null)
					tabControl.TabPages.Remove(historyTab);
				else if (Environment.OSVersion.Version.Major >= 6 && task != null)
					InsertTab(8);
			}
		}

		private void principalSIDTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (!ValidateAccountForSidType(td.Principal.ToString()))
				{
					principalSIDTypeCombo.SelectedIndex = principalSIDTypeCombo.Items.IndexOf((long)TaskProcessTokenSidType.Default);
					return;
				}
				td.Principal.ProcessTokenSidType = (TaskProcessTokenSidType)principalSIDTypeCombo.SelectedIndex;
			}
			principalReqPrivilegesDropDown.Enabled = principalReqPrivilegesLabel.Enabled = editable && (principalSIDTypeCombo.SelectedIndex == (int)TaskProcessTokenSidType.Unrestricted);
		}

		private void principalReqPrivilegesDropDown_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				// TODO: Find a way to clear this list
				foreach (var item in principalReqPrivilegesDropDown.SelectedItems)
					td.Principal.RequiredPrivileges.Add((TaskPrincipalPrivilege)((long)item.Value));
			}
		}

		private void taskVolatileCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Volatile = taskVolatileCheck.Checked;
		}

		private void taskMaintenanceDeadlineCombo_Validated(object sender, EventArgs e)
		{
			td.Settings.MaintenanceSettings.Deadline = taskMaintenanceDeadlineCombo.Value;
		}

		private void taskMaintenanceDeadlineCombo_Validating(object sender, CancelEventArgs e)
		{
			var valid = (taskMaintenanceDeadlineCombo.Value == TimeSpan2.Zero && taskMaintenancePeriodCombo.Value == TimeSpan2.Zero) || (taskMaintenanceDeadlineCombo.Value >= PT1D && taskMaintenanceDeadlineCombo.Value > taskMaintenancePeriodCombo.Value && (taskMaintenanceDeadlineCombo.Enabled && taskMaintenanceDeadlineCombo.IsTextValid));
			errorProvider.SetError(taskMaintenanceDeadlineCombo, valid ? string.Empty : EditorProperties.Resources.Error_MaintenanceDeadlineLimit);
			OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, EditorProperties.Resources.Error_MaintenanceDeadlineLimit));
			HasError = valid;
			e.Cancel = !valid;
		}

		private void taskMaintenanceDeadlineCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (taskMaintenanceDeadlineCombo.Value != TimeSpan2.Zero && taskMaintenancePeriodCombo.Value == TimeSpan2.Zero)
					taskMaintenancePeriodCombo.Value = taskMaintenanceDeadlineCombo.Value <= TimeSpan2.FromDays(2) ? PT1D : taskMaintenanceDeadlineCombo.Value - TimeSpan2.FromDays(1);
			}
		}

		private void taskMaintenancePeriodCombo_Validated(object sender, EventArgs e)
		{
			td.Settings.MaintenanceSettings.Period = taskMaintenancePeriodCombo.Value;
		}

		private void taskMaintenancePeriodCombo_Validating(object sender, CancelEventArgs e)
		{
			var valid = (taskMaintenanceDeadlineCombo.Value == TimeSpan2.Zero && taskMaintenancePeriodCombo.Value == TimeSpan2.Zero) || (taskMaintenancePeriodCombo.Value >= PT1D && taskMaintenanceDeadlineCombo.Value > taskMaintenancePeriodCombo.Value && (taskMaintenancePeriodCombo.Enabled && taskMaintenancePeriodCombo.IsTextValid));
			errorProvider.SetError(taskMaintenancePeriodCombo, valid ? string.Empty : EditorProperties.Resources.Error_MaintenanceDeadlineLimit);
			OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, EditorProperties.Resources.Error_MaintenanceDeadlineLimit));
			HasError = valid;
			e.Cancel = !valid;
		}

		private void taskMaintenancePeriodCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (taskMaintenancePeriodCombo.Value != TimeSpan2.Zero && taskMaintenanceDeadlineCombo.Value == TimeSpan2.Zero)
					taskMaintenanceDeadlineCombo.Value = taskMaintenancePeriodCombo.Value + TimeSpan2.FromDays(1);
			}
		}

		private void taskMaintenanceExclusiveCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.MaintenanceSettings.Exclusive = taskMaintenanceExclusiveCheck.Checked;
		}

		private void taskRegSourceText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Source = taskRegSourceText.TextLength > 0 ? taskRegSourceText.Text : null;
		}

		private void taskNameText_Validated(object sender, EventArgs e)
		{

		}

		private void taskRegURIText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.URI = taskRegURIText.TextLength > 0 ? taskRegURIText.Text : null;
		}

		private void taskRegVersionText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Version = taskRegVersionText.TextLength > 0 ? new Version(taskRegVersionText.Text) : null;
		}

		private void taskRegSDDLText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.SecurityDescriptorSddlForm = taskRegSDDLText.TextLength > 0 ? taskRegSDDLText.Text : null;
		}

		private void taskRegSDDLBtn_Click(object sender, EventArgs e)
		{
			if (Task != null)
				secEd.Initialize(Task);
			else
			{
				var tsec = TaskSecurity.DefaultTaskSecurity;
				if (taskRegSDDLText.TextLength > 0) { tsec = new TaskSecurity(); tsec.SetSecurityDescriptorSddlForm(taskRegSDDLText.Text); }
				secEd.Initialize(TaskName, false, TaskService.TargetServer, tsec);
			}
			if (secEd.ShowDialog(this) == DialogResult.OK)
			{
				td.RegistrationInfo.SecurityDescriptorSddlForm = secEd.SecurityDescriptorSddlForm;
				taskRegSDDLText.Text = td.RegistrationInfo.SecurityDescriptorSddlForm;
			}
		}

		private void taskRegDocText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Documentation = taskRegDocText.TextLength > 0 ? taskRegDocText.Text : null;
		}

		private void taskNameText_Validating(object sender, CancelEventArgs e)
		{
			var inv = System.IO.Path.GetInvalidFileNameChars();
			e.Cancel = !ValidateText(taskNameText,
				s => s.Length > 0 && s.IndexOfAny(inv) == -1,
				EditorProperties.Resources.Error_InvalidNameFormat);
		}

		private void taskRegURIText_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = !ValidateText(taskRegURIText, 
				s => Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute),
				EditorProperties.Resources.Error_InvalidUriFormat);
		}

		private void taskRegVersionText_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = !ValidateText(taskRegVersionText,
				s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^(\d+(\.\d+){0,2}(\.\d+))?$"),
				EditorProperties.Resources.Error_InvalidVersionFormat);
		}

		private void taskRegSDDLText_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = !ValidateText(taskRegSDDLText,
				s => System.Text.RegularExpressions.Regex.IsMatch(s, @"^(O:(?'owner'[A-Z]+?|S(-[0-9]+)+)?)?(G:(?'group'[A-Z]+?|S(-[0-9]+)+)?)?(D:(?'dacl'[A-Z]*(\([^\)]*\))*))?(S:(?'sacl'[A-Z]*(\([^\)]*\))*))?$"),
				EditorProperties.Resources.Error_InvalidSddlFormat);
		}

		private bool ValidateText(Control ctrl, Predicate<string> pred, string error)
		{
			var valid = pred(ctrl.Text);
			errorProvider.SetError(ctrl, valid ? string.Empty : error);
			OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, error));
			HasError = valid;
			return valid;
		}

		private void taskHistoryControl1_Load(object sender, EventArgs e)
		{

		}
	}

	/// <summary>
	/// Flags representing tabs that can be visible on a <see cref="TaskPropertiesControl"/>.
	/// </summary>
	[Flags]
	public enum AvailableTaskTabs
	{
		/// <summary>Displays General tab</summary>
		General = 0x01,
		/// <summary>Displays Triggers tab</summary>
		Triggers = 0x02,
		/// <summary>Displays Actions tab</summary>
		Actions = 0x04,
		/// <summary>Displays Conditions tab</summary>
		Conditions = 0x08,
		/// <summary>Displays Settings tab</summary>
		Settings = 0x10,
		/// <summary>Displays Info tab</summary>
		RegistrationInfo = 0x20,
		/// <summary>Displays Additional tab</summary>
		Properties = 0x40,
		/// <summary>Displays Run Times tab</summary>
		RunTimes = 0x80,
		/// <summary>Displays History tab</summary>
		History = 0x100,
		/// <summary>Displays General, Triggers, Actions, Conditions, Settings, Run Times and History tabs</summary>
		Default = 0x19F,
		/// <summary>Displays all tabs</summary>
		All = 0x1FF
	}
}