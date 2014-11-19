using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that allows tasks to be edited
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Dialog allowing the editing of a task.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Editable"), DesignTimeVisible(true)]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	public partial class TaskOptionsEditor :
#if DEBUG
		Form
#else
		DialogBase
#endif
		, ITaskEditor
	{
		private OptionPanels.OptionPanel curPanel;
		private System.Collections.Generic.Dictionary<ToolStripMenuItem, OptionPanels.OptionPanel> panels = new System.Collections.Generic.Dictionary<ToolStripMenuItem, OptionPanels.OptionPanel>(10);
		private Task task;
		private TaskScheduler.TaskDefinition td;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskOptionsEditor"/> class.
		/// </summary>
		public TaskOptionsEditor()
		{
			InitializeComponent();
			panels.Add(generalItem, new OptionPanels.GeneralOptionPanel());
			panels.Add(triggersItem, new OptionPanels.TriggersOptionPanel());
			panels.Add(actionsItem, new OptionPanels.ActionsOptionPanel());
			panels.Add(securityItem, new OptionPanels.SecurityOptionPanel());
			panels.Add(startupItem, new OptionPanels.StartupOptionPanel());
			//panels.Add(runItem, new OptionPanels.RuntimeOptionPanel());
			UpdateTitleFont();
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskEditDialog"/> is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		public bool Editable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to register task when Accept (Ok) button pressed.
		/// </summary>
		/// <value><c>true</c> if updated task is to be registered; otherwise, <c>false</c>.</value>
		[Category("Behavior"), DefaultValue(false)]
		public bool RegisterTaskOnAccept { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value>
		///   <c>true</c> if errors are shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors { get; set; }

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
						this.Editable = false;
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
					throw new ArgumentNullException("TaskDefinition cannot be set until TaskService has been set with a valid object.");

				if (value == null)
					throw new ArgumentNullException("TaskDefinition cannot be set to null.");

				td = value;
				IsV2 = td.Settings.Compatibility >= TaskCompatibility.V2;
				/*
				onAssignment = true;
				SetVersionComboItems();
				tabControl.SelectedIndex = 0;

				this.flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(service.TargetServer);
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(td.Principal.UserId);
				this.flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(service.UserName);
				//this.flagExecutorIsTheMachineAdministrator = this.ExecutorIsTheMachineAdministrator(executor);

				// Remove invalid tabs on new task
				ValidateHistoryTab();

				// Set General tab
				SetUserControls(td.Principal.LogonType);
				taskNameText.Text = task != null ? task.Name : string.Empty;
				taskNameText.ReadOnly = !(task == null && editable);
				taskLocationText.Text = GetTaskLocation();
				taskAuthorText.Text = string.IsNullOrEmpty(td.RegistrationInfo.Author) ? WindowsIdentity.GetCurrent().Name : td.RegistrationInfo.Author;
				taskDescText.Text = td.RegistrationInfo.Description;
				taskRunLevelCheck.Checked = td.Principal.RunLevel == TaskRunLevel.Highest;
				taskHiddenCheck.Checked = td.Settings.Hidden;

				// Set Triggers tab
				triggerListView.Items.Clear();
				foreach (Trigger tr in td.Triggers)
				{
					AddTriggerToList(tr, -1);
				}
				SetTriggerButtonState();

				// Set Actions tab
				actionListView.Items.Clear();
				if (td.Actions.Count > 0) // Added to make sure that if this is V1 and the ExecAction is invalid, that dialog won't show any actions.
				{
					foreach (Action act in td.Actions)
						AddActionToList(act, -1);
				}
				SetActionButtonState();

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
				taskDeleteAfterCombo.Value = td.Settings.DeleteExpiredTaskAfter;
				taskMultInstCombo.SelectedIndex = taskMultInstCombo.Items.IndexOf((long)td.Settings.MultipleInstances);

				// Set Info tab
				taskRegDocText.Text = td.RegistrationInfo.Documentation;
				taskRegSDDLText.Text = td.RegistrationInfo.SecurityDescriptorSddlForm;
				taskRegSourceText.Text = td.RegistrationInfo.Source;
				taskRegURIText.Text = td.RegistrationInfo.URI;
				taskRegVersionText.Text = td.RegistrationInfo.Version.ToString();

				// Set Additional tab
				taskEnabledCheck.Checked = td.Settings.Enabled;
				taskPriorityCombo.SelectedIndex = taskPriorityCombo.Items.IndexOf((long)td.Settings.Priority);
				taskDisallowStartOnRemoteAppSessionCheck.Checked = td.Settings.DisallowStartOnRemoteAppSession;
				taskUseUnifiedSchedulingEngineCheck.Checked = td.Settings.UseUnifiedSchedulingEngine;
				if (td.Settings.Compatibility >= TaskCompatibility.V2_1)
				{
					principalSIDTypeCombo.SelectedIndex = principalSIDTypeCombo.Items.IndexOf((long)td.Principal.ProcessTokenSidType);
					principalReqPrivilegesDropDown.CheckedFlagValue = 0;
					foreach (var s in td.Principal.RequiredPrivileges)
						principalReqPrivilegesDropDown.SetItemChecked(principalReqPrivilegesDropDown.Items.IndexOf(s.ToString()), true);
				}
				if (td.Settings.Compatibility >= TaskCompatibility.V2_2)
				{
					taskVolatileCheck.Checked = td.Settings.Volatile;
					taskMaintenanceDeadlineCombo.Value = td.Settings.MaintenanceSettings.Deadline;
					taskMaintenancePeriodCombo.Value = td.Settings.MaintenanceSettings.Period;
					taskMaintenanceExclusiveCheck.Checked = td.Settings.MaintenanceSettings.Exclusive;
				}
				UpdateUnifiedSchedulingEngineControls();
				onAssignment = false;
				*/
				if (curPanel != null)
					curPanel.Initialize(this);
			}
		}

		/// <summary>
		/// Gets or sets the folder for the task. If control is initialized with a <see cref="Task"/>, this value will be set to the folder of the registered task.
		/// </summary>
		/// <value>The task folder name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskFolder { get; set; }

		/// <summary>
		/// Gets or sets the name of the task. If control is initialized with a <see cref="Task"/>, this value will be set to the name of the registered task.
		/// </summary>
		/// <value>The task name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskName { get; set; }

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
		///   <c>true</c> if this task definition is v2; otherwise, <c>false</c>.
		/// </value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsV2 { get; private set; }

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public void Initialize(TaskService service, TaskDefinition td = null)
		{
			this.TaskService = service;
			this.task = null;
			if (!this.IsDesignMode())
			{
				if (td == null)
					this.TaskDefinition = service.NewTask();
				else
					this.TaskDefinition = td;
			}
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			this.Task = task;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Font" /> property value of the control's container changes.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnParentFontChanged(EventArgs e)
		{
			base.OnParentFontChanged(e);
			UpdateTitleFont();
		}

		private void UpdateTitleFont()
		{
			this.panelTitleLabel.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size + 1, System.Drawing.FontStyle.Regular, this.Font.Unit);
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem)
			{
				OptionPanels.OptionPanel panel = null;
				if (panels.TryGetValue((ToolStripMenuItem)sender, out panel))
				{
					this.bodyPanel.SuspendLayout();
					panel.Dock = DockStyle.Fill;
					this.panelTitleLabel.Text = panel.Title;
					this.panelImage.Image = panel.Image;
					if (curPanel != null)
						this.bodyPanel.Controls.Remove(curPanel);
					this.bodyPanel.Controls.Add(panel);
					this.bodyPanel.Controls.SetChildIndex(panel, 0);
					curPanel = panel;
					if (td != null)
						panel.Initialize(this);
					this.bodyPanel.ResumeLayout();
				}
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (this.TaskDefinition.Actions.Count == 0)
			{
				MessageBox.Show(EditorProperties.Resources.TaskMustHaveActionsError, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.TaskDefinition.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero && !TaskEditDialog.ValidateOneTriggerExpires(this.TaskDefinition.Triggers))
			{
				MessageBox.Show(EditorProperties.Resources.Error_TaskDeleteMustHaveExpiringTrigger, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.TaskDefinition.LowestSupportedVersion > this.TaskDefinition.Settings.Compatibility)
			{
				MessageBox.Show(EditorProperties.Resources.Error_TaskPropertiesIncompatibleSimple, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (RegisterTaskOnAccept)
			{
				if (this.Task != null && this.Task.Definition.Principal.LogonType != TaskLogonType.InteractiveTokenOrPassword && this.Task.Definition.Principal.LogonType != TaskLogonType.Password)
					this.Task.RegisterChanges();
				else
				{
					string user = this.TaskDefinition.Principal.ToString();
					string pwd = null;
					TaskFolder fld = this.TaskService.GetFolder(this.TaskFolder);
					if (this.TaskDefinition.Principal.LogonType == TaskLogonType.InteractiveTokenOrPassword || this.TaskDefinition.Principal.LogonType == TaskLogonType.Password)
					{
						pwd = TaskEditDialog.InvokeCredentialDialog(user, this);
						if (pwd == null)
						{
							//throw new System.Security.Authentication.AuthenticationException(EditorProperties.Resources.UserAuthenticationError);
							MessageBox.Show(EditorProperties.Resources.Error_PasswordMustBeProvided, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
					this.Task = fld.RegisterTaskDefinition(this.TaskName, this.TaskDefinition, TaskCreation.CreateOrUpdate,
						user, pwd, this.TaskDefinition.Principal.LogonType);
				}
			}
			this.DialogResult = DialogResult.OK;
			Close();
		}

		private void TaskOptionsEditor_HelpButtonClicked(object sender, CancelEventArgs e)
		{

		}
	}
}