using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Control which allows for the editing of all properties of a <see cref="TaskDefinition"/>.
	/// </summary>
	public partial class TaskPropertiesControl : UserControl
	{
		private bool editable = false;
		//private bool flagExecutorIsCurrentUser, flagExecutorIsTheMachineAdministrator;
		private bool flagUserIsAnAdmin, flagExecutorIsServiceAccount, flagRunOnlyWhenUserIsLoggedOn, flagExecutorIsGroup;
		private bool onAssignment = false;
		private string runTimesTaskName = null;
		private TaskService service = null;
		private bool showErrors = true;
		private bool showRunTimesTab = true;
		private Task task = null;
		private TaskDefinition td = null;
		private TabPage tempRunTimesTabPage = null;
		private bool v2 = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskPropertiesControl"/> class.
		/// </summary>
		public TaskPropertiesControl()
		{
			InitializeComponent();

			// Setup images on action up and down buttons
			Bitmap bmpUp = new Bitmap(6, 3);
			using (Graphics g = Graphics.FromImage(bmpUp))
			{
				g.Clear(actionUpButton.BackColor);
				using (SolidBrush b = new SolidBrush(SystemColors.ControlText))
					g.FillPolygon(b, new Point[] { new Point(0, 0), new Point(6, 0), new Point(3, 3), new Point(2, 3) });
			}
			Bitmap bmpDn = (Bitmap)bmpUp.Clone();
			bmpDn.RotateFlip(RotateFlipType.RotateNoneFlipY);
			actionUpButton.Image = bmpDn;
			actionDownButton.Image = bmpUp;

			taskIdleDurationCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromMinutes(60) });
			taskIdleWaitTimeoutCombo.FormattedZero = EditorProperties.Resources.TimeSpanDoNotWait;
			taskIdleWaitTimeoutCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });
			taskRestartIntervalCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });
			taskExecutionTimeLimitCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromHours(1), TimeSpan2.FromHours(2), TimeSpan2.FromHours(4), TimeSpan2.FromHours(8), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1), TimeSpan2.FromDays(3) });
			taskDeleteAfterCombo.FormattedZero = EditorProperties.Resources.TimeSpanImmediately;
			taskDeleteAfterCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromDays(30), TimeSpan2.FromDays(90), TimeSpan2.FromDays(180), TimeSpan2.FromDays(365) });

			Editable = false;
		}

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
				editable = value;

				// General tab
				taskDescText.ReadOnly = !value;
				changePrincipalButton.Visible = taskHiddenCheck.Enabled = taskRunLevelCheck.Enabled = taskVersionCombo.Enabled = value;
				SetUserControls(td != null ? td.Principal.LogonType : TaskLogonType.InteractiveTokenOrPassword);

				// Triggers tab
				triggerDeleteButton.Visible = triggerEditButton.Visible = triggerNewButton.Visible = value;
				triggerListView.Enabled = value;

				// Actions tab
				actionDeleteButton.Visible = actionEditButton.Visible = actionNewButton.Visible = value;
				actionListView.Enabled = actionUpButton.Visible = actionDownButton.Visible = value;

				// Conditions tab
				foreach (Control ctrl in conditionsTab.Controls)
					foreach (Control sub in ctrl.Controls)
						if (sub is CheckBox || sub is ComboBox)
							sub.Enabled = value;

				// Settings tab
				foreach (Control ctrl in settingsTab.Controls)
					ctrl.Enabled = value;

				// If the task has already been set, then reset it to make sure all the items are enabled correctly
				if (td != null)
					this.TaskDefinition = td;

				// Setup specific controls
				taskVersionCombo_SelectedIndexChanged(null, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value>
		///   <c>true</c> if errors are shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors
		{
			get
			{
				return showErrors;
			}
			set
			{
				showErrors = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Run Times' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Run Times tab; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether the 'Run Times' tab is shown.")]
		public bool ShowRunTimesTab
		{
			get
			{
				return showRunTimesTab;
			}
			set
			{
				if (value == showRunTimesTab || this.DesignMode == true)
					return;

				if (value)
				{
					tabControl.TabPages.Insert(tabControl.TabPages.Count - 1, tempRunTimesTabPage);
					tempRunTimesTabPage = null;
				}
				else
				{
					tempRunTimesTabPage = runTimesTab;
					tabControl.TabPages.Remove(runTimesTab);
				}
				showRunTimesTab = value;
			}
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
				if (service == null)
					throw new ArgumentNullException("TaskDefinition cannot be set until TaskService has been set with a valid object.");

				if (value == null)
					throw new ArgumentNullException("TaskDefinition cannot be set to null.");

				td = value;
				onAssignment = true;
				SetVersionComboItems();
				IsV2 = td.Settings.Compatibility == TaskCompatibility.V2;
				tabControl.SelectedIndex = 0;

				this.flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(service.TargetServer);
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(td.Principal.UserId);
				this.flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(service.UserName);
				//this.flagExecutorIsTheMachineAdministrator = this.ExecutorIsTheMachineAdministrator(executor);

				// Set General tab
				SetUserControls(td.Principal.LogonType);
				taskNameText.Text = task != null ? task.Name : string.Empty;
				taskNameText.ReadOnly = !(task == null && editable);
				taskLocationText.Text = GetTaskLocation();
				taskAuthorText.Text = string.IsNullOrEmpty(td.RegistrationInfo.Author) ? WindowsIdentity.GetCurrent().Name : td.RegistrationInfo.Author;
				taskDescText.Text = td.RegistrationInfo.Description;
				taskLoggedOnRadio.Checked = flagRunOnlyWhenUserIsLoggedOn;
				taskLoggedOptionalRadio.Checked = !flagRunOnlyWhenUserIsLoggedOn;
				taskLocalOnlyCheck.Checked = !flagRunOnlyWhenUserIsLoggedOn && td.Principal.LogonType == TaskLogonType.S4U;
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
				taskIdleDurationCombo.Value = td.Settings.IdleSettings.IdleDuration;
				taskIdleWaitTimeoutCombo.Value = td.Settings.IdleSettings.WaitTimeout;
				taskIdleDurationCheck.Checked = td.Settings.IdleSettings.IdleDuration != TimeSpan.FromMinutes(10) ||
					td.Settings.IdleSettings.WaitTimeout != TimeSpan.FromHours(1);
				UpdateIdleSettingsControls();
				taskDisallowStartIfOnBatteriesCheck.Checked = td.Settings.DisallowStartIfOnBatteries;
				taskStopIfGoingOnBatteriesCheck.Enabled = editable && td.Settings.DisallowStartIfOnBatteries;
				taskStopIfGoingOnBatteriesCheck.Checked = td.Settings.StopIfGoingOnBatteries;
				taskWakeToRunCheck.Checked = td.Settings.WakeToRun;
				taskStartIfConnectionCheck.Enabled = editable;
				taskStartIfConnectionCheck.Checked = td.Settings.RunOnlyIfNetworkAvailable;
				availableConnectionsCombo.Enabled = editable && td.Settings.RunOnlyIfNetworkAvailable;

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
				taskMultInstCombo.SelectedIndex = (int)td.Settings.MultipleInstances;

				onAssignment = false;
			}
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
		public TaskService TaskService
		{
			get { return service; }
			private set { service = value; }
		}

		private void SetVersionComboItems()
		{
			this.taskVersionCombo.Items.Clear();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesControl));
			if (td.Settings.Compatibility == TaskCompatibility.V2 || (TaskService != null && TaskService.HighestSupportedVersion.Major > 1))
				this.taskVersionCombo.Items.Add(resources.GetString("taskVersionCombo.Items1", System.Globalization.CultureInfo.CurrentUICulture));
			this.taskVersionCombo.Items.Add(resources.GetString("taskVersionCombo.Items", System.Globalization.CultureInfo.CurrentUICulture));
		}

		private bool IsV2
		{
			get { return v2; }
			set
			{
				if (v2 != value || onAssignment)
				{
					v2 = value;
					taskVersionCombo.SelectedIndex = v2 ? 0 : taskVersionCombo.Items.Count - 1;
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

		internal static string BuildEnumString(string preface, object enumValue)
		{
			string[] vals = enumValue.ToString().Split(new string[] { ", " }, StringSplitOptions.None);
			if (vals.Length == 0)
				return string.Empty;

			for (int i = 0; i < vals.Length; i++)
			{
				vals[i] = EditorProperties.Resources.ResourceManager.GetString(preface + vals[i], System.Globalization.CultureInfo.CurrentUICulture);
			}
			return string.Join(", ", vals);
		}

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				td.Actions.RemoveAt(idx);
				actionListView.Items.RemoveAt(idx);
				SetActionButtonState();
			}
		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{
			if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != (this.actionListView.Items.Count - 1)))
			{
				int index = actionListView.SelectedIndices[0];
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				td.Actions.RemoveAt(index);
				actionListView.Items.Insert(index + 1, lvi);
				td.Actions.Insert(index + 1, aTemp as Action);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				ActionEditDialog dlg = new ActionEditDialog(actionListView.Items[idx].Tag as Action);
				if (!v2 && !dlg.SupportV1Only) dlg.SupportV1Only = true;
				dlg.Text = EditorProperties.Resources.ActionDlgEditCaption;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					actionListView.Items.RemoveAt(idx);
					td.Actions[idx] = dlg.Action;
					AddActionToList(dlg.Action, idx);
					actionListView.Items[idx].Selected = true;
				}
			}
		}

		private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editable)
				actionEditButton_Click(sender, EventArgs.Empty);
		}

		private void actionListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetActionButtonState();
		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{
			ActionEditDialog dlg = new ActionEditDialog { SupportV1Only = !v2 };
			dlg.Text = EditorProperties.Resources.ActionDlgNewCaption;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				td.Actions.Add(dlg.Action);
				AddActionToList(dlg.Action, -1);
				SetActionButtonState();
			}
		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{
			if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != 0))
			{
				int index = actionListView.SelectedIndices[0];
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				td.Actions.RemoveAt(index);
				actionListView.Items.Insert(index - 1, lvi);
				td.Actions.Insert(index - 1, aTemp);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}

		private void AddActionToList(Action act, int index)
		{
			ListViewItem lvi = new ListViewItem(new string[] {
					TaskEnumGlobalizer.GetString(act.ActionType),
					act.ToString() }) { Tag = act };
			if (index < 0)
				actionListView.Items.Add(lvi);
			else
				actionListView.Items.Insert(index, lvi);
		}

		private void AddTriggerToList(Trigger tr, int index)
		{
			ListViewItem lvi = new ListViewItem(new string[] {
					TaskEnumGlobalizer.GetString(tr.TriggerType),
					tr.ToString(),
					tr.Enabled ? EditorProperties.Resources.Enabled : EditorProperties.Resources.Disabled
				});
			if (index < 0)
				triggerListView.Items.Add(lvi);
			else
				triggerListView.Items.Insert(index, lvi);
		}

		private void availableConnectionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (availableConnectionsCombo.SelectedIndex > 0)
				{
					td.Settings.NetworkSettings.Id = ((NetworkProfile)availableConnectionsCombo.SelectedItem).Id;
					td.Settings.NetworkSettings.Name = ((NetworkProfile)availableConnectionsCombo.SelectedItem).Name;
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
			InvokeObjectPicker(service.TargetServer);
		}

		private void conditionsTab_Enter(object sender, EventArgs e)
		{
			// Load network connections
			availableConnectionsCombo.BeginUpdate();
			availableConnectionsCombo.Items.Clear();
			availableConnectionsCombo.Items.Add(EditorProperties.Resources.AnyConnection);
			availableConnectionsCombo.Items.AddRange(NetworkProfile.GetAllLocalProfiles());
			availableConnectionsCombo.EndUpdate();
			onAssignment = true;
			if (task == null || td.Settings.NetworkSettings.Id == Guid.Empty)
				availableConnectionsCombo.SelectedIndex = 0;
			else
				availableConnectionsCombo.SelectedItem = td.Settings.NetworkSettings.Id;
			onAssignment = false;
		}

		private string GetTaskLocation()
		{
			if (task == null || TaskService.HighestSupportedVersion.CompareTo(new Version(1, 1)) == 0)
				return @"\";
			return System.IO.Path.GetDirectoryName(task.Path);
		}

		private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			List<ListViewItem> c = new List<ListViewItem>(100);
			try
			{
				TaskEventLog log = TaskService == null ? new TaskEventLog(task.Path) : new TaskEventLog(TaskService.TargetServer, task.Path);
				foreach (TaskEvent item in log)
					c.Add(new ListViewItem(new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(),
					item.TaskCategory, item.OpCode, item.ActivityId.ToString() }));
				c.Reverse();
				e.Result = c.ToArray();
			}
			catch (Exception ex) { e.Result = ex; }
		}

		private void historyBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			historyListView.Cursor = Cursors.Default;
			if (e.Result is ListViewItem[])
				historyListView.Items.AddRange(e.Result as ListViewItem[]);
			else if (e.Result is Exception && showErrors)
				MessageBox.Show(string.Format(EditorProperties.Resources.Error_CannotRetrieveHistory, ((Exception)e.Result).Message), null, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void historyTab_Enter(object sender, EventArgs e)
		{
			if (task == null)
				return;

			historyListView.Items.Clear();
			historyListView.Cursor = Cursors.WaitCursor;
			historyBackgroundWorker.RunWorkerAsync();
		}

		private void InvokeObjectPicker(string targetComputerName)
		{
			string acct = String.Empty;
			if (!NativeMethods.AccountUtils.SelectAccount(this, targetComputerName, ref acct, ref this.flagExecutorIsGroup, ref this.flagExecutorIsServiceAccount))
				return;

			if (this.flagExecutorIsServiceAccount)
			{
				if (!v2 && acct != "SYSTEM")
				{
					MessageBox.Show(this, EditorProperties.Resources.TaskSchedulerName, EditorProperties.Resources.Error_NoGroupsUnderV1, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				this.flagExecutorIsGroup = false;
				if (v2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				//this.flagExecutorIsCurrentUser = false;
			}
			else if (this.flagExecutorIsGroup)
			{
				if (!v2)
				{
					MessageBox.Show(this, EditorProperties.Resources.TaskSchedulerName, EditorProperties.Resources.Error_NoGroupsUnderV1, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

			Task tempTask = null;
			try
			{
				// Create a temporary task using current definition
				runTimesTaskName = "TempTask-" + Guid.NewGuid().ToString();
				TaskDefinition ttd = service.NewTask();
				//this.TaskDefinition.Principal.CopyTo(ttd.Principal);
				ttd.Settings.Enabled = false;
				ttd.Settings.Hidden = true;
				ttd.Actions.Add(new ExecAction("rundll32.exe"));
				foreach (Trigger tg in this.TaskDefinition.Triggers)
					ttd.Triggers.Add((Trigger)tg.Clone());
				tempTask = service.RootFolder.RegisterTaskDefinition(runTimesTaskName, ttd);
				if (tempTask != null)
				{
					taskRunTimesControl1.Show();
					tempTask.Enabled = true;
					taskRunTimesControl1.Initialize(tempTask, DateTime.Now, DateTime.Now + TimeSpan.FromDays(365));
					tempTask.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				// On error, post and delete temporary task
				runTimesErrorLabel.Text = showErrors ? ex.ToString() : null;
				taskRunTimesControl1.Hide();
			}
			finally
			{
				if (tempTask != null)
					service.RootFolder.DeleteTask(runTimesTaskName);
				runTimesTaskName = null;
			}
		}

		private void runTimesTab_Leave(object sender, EventArgs e)
		{
		}

		private void SetActionButtonState()
		{
			actionUpButton.Enabled = actionDownButton.Enabled = actionListView.Items.Count > 1;
			actionNewButton.Enabled = editable && (v2 || actionListView.Items.Count == 0);
			actionEditButton.Enabled = actionDeleteButton.Enabled = editable && actionListView.Items.Count > 0 && actionListView.SelectedIndices.Count > 0;
		}

		private void SetTriggerButtonState()
		{
			triggerNewButton.Enabled = editable;
			triggerEditButton.Enabled = triggerDeleteButton.Enabled = editable && triggerListView.Items.Count > 0 && triggerListView.SelectedIndices.Count > 0;
		}

		private void SetUserControls(TaskLogonType logonType)
		{
			switch (logonType)
			{
				case TaskLogonType.InteractiveToken:
					this.flagRunOnlyWhenUserIsLoggedOn = true;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = false;
					break;
				case TaskLogonType.Group:
					this.flagRunOnlyWhenUserIsLoggedOn = true;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = true;
					break;
				case TaskLogonType.ServiceAccount:
					this.flagRunOnlyWhenUserIsLoggedOn = false;
					this.flagExecutorIsServiceAccount = true;
					this.flagExecutorIsGroup = false;
					break;
				default:
					this.flagRunOnlyWhenUserIsLoggedOn = false;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = false;
					break;
			}

			if (this.flagExecutorIsServiceAccount)
			{
				taskLoggedOnRadio.Enabled = false;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (this.flagExecutorIsGroup)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (this.flagRunOnlyWhenUserIsLoggedOn)
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = false;
			}
			else
			{
				taskLoggedOnRadio.Enabled = editable;
				taskLoggedOptionalRadio.Enabled = editable;
				taskLocalOnlyCheck.Enabled = editable && (task == null || v2);
			}

			string user = td == null ? null : td.Principal.ToString();
			if (string.IsNullOrEmpty(user))
				user = WindowsIdentity.GetCurrent().Name;
			taskPrincipalText.Text = user;
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
				if (taskDeleteAfterCheck.Checked)
					taskDeleteAfterCombo.Value = TimeSpan.FromDays(30);
				else
					taskDeleteAfterCombo.Value = TimeSpan.Zero;
			}
		}

		private void taskDeleteAfterCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.DeleteExpiredTaskAfter = taskDeleteAfterCombo.Value;
		}

		private void taskDescText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment)
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
				if (taskExecutionTimeLimitCheck.Checked)
					taskExecutionTimeLimitCombo.Value = TimeSpan.FromDays(3);
				else
					taskExecutionTimeLimitCombo.Value = TimeSpan.Zero;
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
			taskLocalOnlyCheck.Enabled = editable && (task == null || v2) && taskLoggedOptionalRadio.Checked;
			taskLocalOnlyCheck_CheckedChanged(sender, e);
		}

		private void taskMultInstCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment && v2)
				td.Settings.MultipleInstances = (TaskInstancesPolicy)taskMultInstCombo.SelectedIndex;
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
			availableConnectionsCombo.Enabled = editable && taskStartIfConnectionCheck.Checked;
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

		private void taskVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool isVistaPlus = System.Environment.OSVersion.Version.Major >= 6;
			if (tabControl.TabPages.Contains(historyTab) && !isVistaPlus)
				tabControl.TabPages.Remove(historyTab);
			else if (!tabControl.TabPages.Contains(historyTab) && isVistaPlus)
				tabControl.TabPages.Add(historyTab);
			v2 = taskVersionCombo.Items.Count > 1 && taskVersionCombo.SelectedIndex == 0;
			taskRestartOnIdleCheck.Enabled = taskRunLevelCheck.Enabled =
			taskAllowDemandStartCheck.Enabled = taskStartWhenAvailableCheck.Enabled =
			taskRestartIntervalCheck.Enabled = taskRestartIntervalCombo.Enabled =
			taskRestartCountLabel.Enabled = taskRestartAttemptTimesLabel.Enabled = taskRestartCountText.Enabled =
			taskAllowHardTerminateCheck.Enabled = taskRunningRuleLabel.Enabled = taskMultInstCombo.Enabled =
			taskStartIfConnectionCheck.Enabled = availableConnectionsCombo.Enabled = editable && v2;
		}

		private void taskWakeToRunCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.WakeToRun = taskWakeToRunCheck.Checked;
		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				td.Triggers.RemoveAt(idx);
				triggerListView.Items.RemoveAt(idx);
			}
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				TriggerEditDialog dlg = new TriggerEditDialog(td.Triggers[idx], td.Settings.Compatibility != TaskCompatibility.V2);
				dlg.TargetServer = TaskService.TargetServer;
				dlg.Text = EditorProperties.Resources.TriggerDlgEditCaption;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					triggerListView.Items.RemoveAt(idx);
					td.Triggers[idx] = dlg.Trigger;
					AddTriggerToList(dlg.Trigger, idx);
					triggerListView.Items[idx].Selected = true;
				}
			}
		}

		private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editable)
				triggerEditButton_Click(sender, EventArgs.Empty);
		}

		private void triggerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetTriggerButtonState();
		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{
			TriggerEditDialog dlg = new TriggerEditDialog(null, td.Settings.Compatibility != TaskCompatibility.V2);
			dlg.TargetServer = TaskService.TargetServer;
			dlg.Text = EditorProperties.Resources.TriggerDlgNewCaption;
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				td.Triggers.Add(dlg.Trigger);
				AddTriggerToList(dlg.Trigger, -1);
			}
		}

		private void UpdateIdleSettingsControls()
		{
			bool isSet = taskIdleDurationCheck.Checked;
			bool alreadyOnAssigment = onAssignment;
			if (isSet)
			{
				taskIdleDurationCombo.Enabled = editable;
				taskIdleWaitTimeoutLabel.Enabled = taskIdleWaitTimeoutCombo.Enabled = editable;
				taskStopOnIdleEndCheck.Enabled = editable;
				onAssignment = true;
				taskStopOnIdleEndCheck.Checked = td.Settings.IdleSettings.StopOnIdleEnd;
				taskRestartOnIdleCheck.Enabled = editable && td.Settings.IdleSettings.StopOnIdleEnd;
				taskRestartOnIdleCheck.Checked = td.Settings.IdleSettings.RestartOnIdle ? td.Settings.IdleSettings.RestartOnIdle : false;
				if (!alreadyOnAssigment)
					onAssignment = false;
			}
			else
			{
				taskIdleDurationCombo.Enabled = false;
				taskIdleWaitTimeoutLabel.Enabled = taskIdleWaitTimeoutCombo.Enabled = false;
				onAssignment = true;
				taskRestartOnIdleCheck.Enabled = false;
				taskRestartOnIdleCheck.Checked = td.Settings.IdleSettings.RestartOnIdle ? td.Settings.IdleSettings.RestartOnIdle : false;
				taskStopOnIdleEndCheck.Enabled = false;
				taskStopOnIdleEndCheck.Checked = td.Settings.IdleSettings.StopOnIdleEnd;
				if (!alreadyOnAssigment)
					onAssignment = false;
			}
		}
	}
}