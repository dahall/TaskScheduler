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
        internal static global::System.Resources.ResourceManager taskSchedResources;

        private bool editable = false;

        //private bool flagExecutorIsCurrentUser, flagExecutorIsTheMachineAdministrator;
        private bool flagUserIsAnAdmin, flagExecutorIsServiceAccount, flagRunOnlyWhenUserIsLoggedOn, flagExecutorIsGroup;
        private bool onAssignment = false;
        private TaskService service = null;
        private Task task = null;
        private TaskDefinition td = null;
        private bool v2 = true;

        static TaskPropertiesControl()
        {
            taskSchedResources = new global::System.Resources.ResourceManager("Microsoft.Win32.TaskScheduler.Properties.Resources", typeof(Trigger).Assembly);
        }

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
            taskIdleWaitTimeoutCombo.FormattedZero = Properties.Resources.TimeSpanDoNotWait;
            taskIdleWaitTimeoutCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });
            taskRestartIntervalCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });
            taskExecutionTimeLimitCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromHours(1), TimeSpan2.FromHours(2), TimeSpan2.FromHours(4), TimeSpan2.FromHours(8), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1), TimeSpan2.FromDays(3) });
            taskDeleteAfterCombo.FormattedZero = Properties.Resources.TimeSpanImmediately;
            taskDeleteAfterCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromDays(30), TimeSpan2.FromDays(90), TimeSpan2.FromDays(180), TimeSpan2.FromDays(365) });
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
        /// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
        /// </summary>
        /// <value>The task.</value>
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
                IsV2 = td.Settings.Compatibility == TaskCompatibility.V2;
                tabControl1.SelectedIndex = 0;

                this.flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(service.TargetServer);
                //this.flagExecutorIsCurrentUser = this.UserIsExecutor(td.Principal.UserId);
                this.flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(service.UserName);
                //this.flagExecutorIsTheMachineAdministrator = this.ExecutorIsTheMachineAdministrator(executor);

                // Set General tab
                SetUserControls(td.Principal.LogonType);
                if (task != null) taskNameText.Text = task.Name;
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

                // Set Actions tab
                actionListView.Items.Clear();
                foreach (Action act in td.Actions)
                {
                    AddActionToList(act, -1);
                }
                SetActionUpDnState();

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
                if (taskStartIfConnectionCheck.Checked) availableConnectionsCombo.SelectedItem = td.Settings.NetworkSettings.Name;

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
        /// Gets the <see cref="TaskService"/> assigned at initialization.
        /// </summary>
        /// <value>The task service.</value>
        public TaskService TaskService
        {
            get { return service; }
            private set { service = value; }
        }

        private bool IsV2
        {
            get { return v2; }
            set
            {
                if (v2 != value || onAssignment)
                {
                    v2 = value;
                    this.taskVersionCombo.Items.Clear();
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesControl));
                    if (!v2)
                        this.taskVersionCombo.Items.Add(resources.GetString("taskVersionCombo.Items"));
                    this.taskVersionCombo.Items.Add(resources.GetString("taskVersionCombo.Items1"));
                    taskVersionCombo.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
        /// </summary>
        /// <param name="service">A <see cref="TaskService"/> instance.</param>
        public void Initialize(TaskService service)
        {
            this.TaskService = service;
            this.TaskDefinition = service.NewTask();
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
            return BuildEnumString(Properties.Resources.ResourceManager, preface, enumValue);
        }

        internal static string BuildEnumString(System.Resources.ResourceManager mgr, string preface, object enumValue)
        {
            string[] vals = enumValue.ToString().Split(new string[] { ", " }, StringSplitOptions.None);
            if (vals.Length == 0)
                return string.Empty;

            for (int i = 0; i < vals.Length; i++)
            {
                vals[i] = mgr.GetString(preface + vals[i]);
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
                SetActionUpDnState();
            }
        }

        private void actionDownButton_Click(object sender, EventArgs e)
        {
            if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != (this.actionListView.Items.Count - 1)))
            {
                int index = actionListView.SelectedIndices[0];
                actionListView.BeginUpdate();
                ListViewItem lvi = this.actionListView.Items[index];
                actionListView.Items.RemoveAt(index);
                actionListView.Items.Insert(index + 1, lvi);
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
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (v2)
                        td.Actions.RemoveAt(idx);
                    actionListView.Items.RemoveAt(idx);
                    td.Actions.Add(dlg.Action);
                    AddActionToList(dlg.Action, idx);
                    actionListView.Items[idx].Selected = true;
                }
            }
        }

        private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            actionEditButton_Click(sender, EventArgs.Empty);
        }

        private void actionNewButton_Click(object sender, EventArgs e)
        {
            ActionEditDialog dlg = new ActionEditDialog { SupportV1Only = !v2 };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                td.Actions.Add(dlg.Action);
                AddActionToList(dlg.Action, -1);
                SetActionUpDnState();
            }
        }

        private void actionUpButton_Click(object sender, EventArgs e)
        {
            if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != 0))
            {
                int index = actionListView.SelectedIndices[0];
                actionListView.BeginUpdate();
                ListViewItem lvi = this.actionListView.Items[index];
                actionListView.Items.RemoveAt(index);
                actionListView.Items.Insert(index - 1, lvi);
                actionListView.EndUpdate();
            }
        }

        private void AddActionToList(Action act, int index)
        {
            ListViewItem lvi = new ListViewItem(new string[] {
                    BuildEnumString(taskSchedResources, "ActionType", act.ActionType),
                    act.ToString() }) { Tag = act };
            if (index < 0)
                actionListView.Items.Add(lvi);
            else
                actionListView.Items.Insert(index, lvi);
        }

        private void AddTriggerToList(Trigger tr, int index)
        {
            ListViewItem lvi = new ListViewItem(new string[] {
                    BuildEnumString(taskSchedResources, "TriggerType", tr.TriggerType),
                    tr.ToString(),
                    tr.Enabled ? Properties.Resources.Enabled : Properties.Resources.Disabled
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
                bool onNet = td.Settings.RunOnlyIfNetworkAvailable = taskStartIfConnectionCheck.Checked && availableConnectionsCombo.SelectedIndex != -1;
                if (onNet && availableConnectionsCombo.SelectedIndex > 0)
                    td.Settings.NetworkSettings.Name = availableConnectionsCombo.SelectedItem.ToString();
                else
                    td.Settings.NetworkSettings.Name = null;
            }
        }

        private void changePrincipalButton_Click(object sender, EventArgs e)
        {
            InvokeObjectPicker(service.TargetServer);
        }

        private void conditionsTab_Enter(object sender, EventArgs e)
        {
            // Load network connections
            availableConnectionsCombo.Items.Clear();
            availableConnectionsCombo.Items.Add(Properties.Resources.AnyConnection);
            foreach (var n in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                availableConnectionsCombo.Items.Add(n.Name);
            if (task == null || string.IsNullOrEmpty(td.Settings.NetworkSettings.Name))
                availableConnectionsCombo.SelectedIndex = 0;
            else
                availableConnectionsCombo.SelectedText = td.Settings.NetworkSettings.Name;
        }

        private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TaskEventLog log = new TaskEventLog(task.Path);
            List<ListViewItem> c = new List<ListViewItem>(100);
            foreach (TaskEvent item in log)
                c.Add(new ListViewItem(new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(),
                    item.TaskCategory, item.OpCode, item.ActivityId.ToString() }));
            c.Reverse();
            e.Result = c.ToArray();
        }

        private void historyBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            historyListView.Items.AddRange(e.Result as ListViewItem[]);
            historyListView.Cursor = Cursors.Default;
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
                this.flagExecutorIsGroup = false;
                td.Principal.UserId = acct;
                td.Principal.LogonType = TaskLogonType.ServiceAccount;
                //this.flagExecutorIsCurrentUser = false;
            }
            else if (this.flagExecutorIsGroup)
            {
                td.Principal.GroupId = acct;
                td.Principal.LogonType = TaskLogonType.Group;
                //this.flagExecutorIsCurrentUser = false;
            }
            else
            {
                td.Principal.UserId = acct;
                //this.flagExecutorIsCurrentUser = this.UserIsExecutor(objArray[0].ObjectName);
                if (td.Principal.LogonType == TaskLogonType.Group)
                {
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                }
                else if (td.Principal.LogonType == TaskLogonType.ServiceAccount)
                {
                    td.Principal.LogonType = TaskLogonType.Password;
                }
            }
            SetUserControls(td.Principal.LogonType);
        }

        private void SetActionUpDnState()
        {
            actionUpButton.Enabled = actionDownButton.Enabled = actionListView.Items.Count > 1;
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
            if (task != null)
                taskPrincipalText.Text = this.flagExecutorIsGroup ? td.Principal.GroupId : td.Principal.UserId;
            else
                taskPrincipalText.Text = WindowsIdentity.GetCurrent().Name;
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
                if (taskIdleDurationCheck.Checked)
                    td.Settings.IdleSettings.StopOnIdleEnd = false;
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
            UpdatePrincipal();
        }

        private void taskLoggedOnRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrincipal();
        }

        private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
        {
            taskLocalOnlyCheck.Enabled = editable && (task == null || v2) && taskLoggedOptionalRadio.Checked;
            UpdatePrincipal();
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
            if (tabControl1.TabPages.Contains(historyTab) && !isVistaPlus)
                tabControl1.TabPages.Remove(historyTab);
            else if (!tabControl1.TabPages.Contains(historyTab) && isVistaPlus)
                tabControl1.TabPages.Add(historyTab);
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
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    td.Triggers.RemoveAt(idx);
                    triggerListView.Items.RemoveAt(idx);
                    td.Triggers.Add(dlg.Trigger);
                    AddTriggerToList(dlg.Trigger, idx);
                    triggerListView.Items[idx].Selected = true;
                }
            }
        }

        private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            triggerEditButton_Click(sender, EventArgs.Empty);
        }

        private void triggerNewButton_Click(object sender, EventArgs e)
        {
            TriggerEditDialog dlg = new TriggerEditDialog(null, td.Settings.Compatibility != TaskCompatibility.V2);
            dlg.TargetServer = TaskService.TargetServer;
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

        private void UpdatePrincipal()
        {
            if (!onAssignment)
                throw new NotImplementedException();
        }
    }
}