using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class TaskPropertiesControl : UserControl
	{
		private Task task = null;
		internal static global::System.Resources.ResourceManager taskSchedResources;

		static TaskPropertiesControl()
		{
			taskSchedResources = new global::System.Resources.ResourceManager("Microsoft.Win32.TaskScheduler.Properties.Resources", typeof(Trigger).Assembly);
		}

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

			taskIdleDurationCombo.Items.AddRange(new object[] { TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(60) });
			taskIdleWaitTimeoutCombo.FormattedZero = Properties.Resources.TimeSpanDoNotWait;
			taskIdleWaitTimeoutCombo.Items.AddRange(new object[] { TimeSpan.Zero, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromHours(1), TimeSpan.FromHours(2) });
			taskRestartIntervalCombo.Items.AddRange(new object[] { TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromHours(1), TimeSpan.FromHours(2) });
			taskExecutionTimeLimitCombo.Items.AddRange(new object[] { TimeSpan.FromHours(1), TimeSpan.FromHours(2), TimeSpan.FromHours(4), TimeSpan.FromHours(8), TimeSpan.FromHours(12), TimeSpan.FromDays(1), TimeSpan.FromDays(3) });
			taskDeleteAfterCombo.FormattedZero = Properties.Resources.TimeSpanImmediately;
			taskDeleteAfterCombo.Items.AddRange(new object[] { TimeSpan.Zero, TimeSpan.FromDays(30), TimeSpan.FromDays(90), TimeSpan.FromDays(180), TimeSpan.FromDays(365) });
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

		public Task GetTask()
		{
			return task;
		}

		//private bool flagExecutorIsCurrentUser, flagExecutorIsTheMachineAdministrator;
		private bool flagUserIsAnAdmin, flagExecutorIsServiceAccount, flagRunOnlyWhenUserIsLoggedOn, flagExecutorIsGroup;

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
				taskLocalOnlyCheck.Enabled = editable && (task == null || task.Definition.Settings.Compatibility == TaskCompatibility.V2);
			}
			if (task != null)
				taskPrincipalText.Text = this.flagExecutorIsGroup ? task.Definition.Principal.GroupId : task.Definition.Principal.UserId;
			else
				taskPrincipalText.Text = WindowsIdentity.GetCurrent().Name;
		}

		public void SetTask(Task newTask)
		{
			task = newTask;

			this.flagUserIsAnAdmin = NativeMethods.AccountUtils.CurrentUserIsAdmin(task.TaskService.TargetServer);
			//this.flagExecutorIsCurrentUser = this.UserIsExecutor(task.Definition.Principal.UserId);
			this.flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(task.TaskService.ConnectedUser);
			//this.flagExecutorIsTheMachineAdministrator = this.ExecutorIsTheMachineAdministrator(executor);

			// Set General tab
			SetUserControls(task.Definition.Principal.LogonType);
			taskNameText.Text = task.Name;
			taskAuthorText.Text = task.Definition.RegistrationInfo.Author;
			taskDescText.Text = task.Definition.RegistrationInfo.Description;
			taskLoggedOnRadio.Checked = flagRunOnlyWhenUserIsLoggedOn;
			taskLoggedOptionalRadio.Checked = !flagRunOnlyWhenUserIsLoggedOn;
			taskLocalOnlyCheck.Checked = !flagRunOnlyWhenUserIsLoggedOn && task.Definition.Principal.LogonType == TaskLogonType.S4U;
			taskRunLevelCheck.Checked = task.Definition.Principal.RunLevel == TaskRunLevel.Highest;
			taskHiddenCheck.Checked = task.Definition.Settings.Hidden;
			taskVersionCombo.SelectedIndex = task.Definition.Settings.Compatibility == TaskCompatibility.V1 ? 0 : 1;

			// Set Triggers tab
			foreach (Trigger tr in task.Definition.Triggers)
			{
				AddTriggerToList(tr);
			}

			// Set Actions tab
			foreach (Action act in task.Definition.Actions)
			{
				AddActionToList(act);
			}
			SetActionUpDnState();

			// Set Conditions tab
			taskIdleDurationCheck.Checked = task.Definition.Settings.IdleSettings.IdleDuration != TimeSpan.Zero;
			if (taskIdleDurationCheck.Checked)
			{
				taskIdleDurationCombo.Value = task.Definition.Settings.IdleSettings.IdleDuration;
				taskIdleWaitTimeoutCombo.Value = task.Definition.Settings.IdleSettings.WaitTimeout;
				taskIdleDurationCombo.Enabled = taskIdleWaitTimeoutCombo.Enabled = editable;
			}
			taskStopOnIdleEndCheck.Checked = task.Definition.Settings.IdleSettings.StopOnIdleEnd;
			taskRestartOnIdleCheck.Enabled = editable && task.Definition.Settings.IdleSettings.StopOnIdleEnd;
			taskRestartOnIdleCheck.Checked = task.Definition.Settings.IdleSettings.RestartOnIdle;
			taskDisallowStartIfOnBatteriesCheck.Checked = task.Definition.Settings.DisallowStartIfOnBatteries;
			taskStopIfGoingOnBatteriesCheck.Enabled = editable && task.Definition.Settings.DisallowStartIfOnBatteries;
			taskStopIfGoingOnBatteriesCheck.Checked = task.Definition.Settings.StopIfGoingOnBatteries;
			taskWakeToRunCheck.Checked = task.Definition.Settings.WakeToRun;
			taskStartIfConnectionCheck.Checked = task.Definition.Settings.RunOnlyIfNetworkAvailable;
			availableConnectionsCombo.Enabled = editable && task.Definition.Settings.RunOnlyIfNetworkAvailable;
			if (taskStartIfConnectionCheck.Checked) availableConnectionsCombo.SelectedItem = task.Definition.Settings.NetworkSettings.Name;

			// Set Settings tab
			taskAllowDemandStartCheck.Checked = task.Definition.Settings.AllowDemandStart;
			taskStartWhenAvailableCheck.Checked = task.Definition.Settings.StartWhenAvailable;
			taskRestartIntervalCheck.Checked = taskRestartIntervalCombo.Enabled = task.Definition.Settings.RestartInterval != TimeSpan.Zero;
			if (taskRestartIntervalCheck.Checked)
			{
				taskRestartIntervalCombo.Value = task.Definition.Settings.RestartInterval;
				taskRestartCountText.Value = task.Definition.Settings.RestartCount;
			}
			taskExecutionTimeLimitCheck.Checked = taskExecutionTimeLimitCombo.Enabled = task.Definition.Settings.ExecutionTimeLimit != TimeSpan.Zero;
			if (taskExecutionTimeLimitCombo.Enabled) taskExecutionTimeLimitCombo.Value = task.Definition.Settings.ExecutionTimeLimit;
			taskAllowHardTerminateCheck.Checked = task.Definition.Settings.AllowHardTerminate;
			taskDeleteAfterCheck.Checked = taskDeleteAfterCombo.Enabled = task.Definition.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero;
			if (taskDeleteAfterCombo.Enabled) taskDeleteAfterCombo.Value = task.Definition.Settings.DeleteExpiredTaskAfter;
			taskMultInstCombo.SelectedIndex = (int)task.Definition.Settings.MultipleInstances;
		}

		private void AddTriggerToList(Trigger tr)
		{
			triggerListView.Items.Add(new ListViewItem(new string[] {
					BuildEnumString(taskSchedResources, "TriggerType", tr.TriggerType),
					tr.ToString(),
					tr.Enabled ? Properties.Resources.Enabled : Properties.Resources.Disabled
				}));
		}

		private bool editable = false;

		public bool Editable
		{
			get { return editable; }
			set
			{
				editable = value;
				bool v2 = true;
				System.Security.Principal.WindowsIdentity wid = System.Security.Principal.WindowsIdentity.GetCurrent();
				if (task != null)
				{
					v2 = task.Definition.Settings.Compatibility == TaskCompatibility.V2;
					wid = new System.Security.Principal.WindowsIdentity(task.Definition.Principal.UserId);
				}

				// General tab
				taskDescText.ReadOnly = !value;
				changePrincipalButton.Visible = taskHiddenCheck.Enabled = taskRunLevelCheck.Enabled = taskVersionCombo.Enabled = value;
				SetUserControls(task != null ? task.Definition.Principal.LogonType : TaskLogonType.InteractiveTokenOrPassword);

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
			}
		}

		private void InvokeObjectPicker(string targetComputerName)
		{
			NativeMethods.ObjectPicker dlg = new NativeMethods.ObjectPicker();
			dlg.TargetComputer = targetComputerName;
			if (dlg.ShowDialog(this) != DialogResult.OK)
				return;

			this.flagExecutorIsGroup = NativeMethods.ObjectPickerTypes.Group == dlg.Picks[0].ObjectType;
			this.flagExecutorIsServiceAccount = NativeMethods.AccountUtils.UserIsServiceAccount(dlg.Picks[0].ObjectName);
			if (this.flagExecutorIsServiceAccount)
			{
				this.flagExecutorIsGroup = false;
				task.Definition.Principal.UserId = dlg.Picks[0].ObjectName;
				task.Definition.Principal.LogonType = TaskLogonType.ServiceAccount;
				//this.flagExecutorIsCurrentUser = false;
			}
			else if (this.flagExecutorIsGroup)
			{
				task.Definition.Principal.GroupId = dlg.Picks[0].ObjectName;
				task.Definition.Principal.LogonType = TaskLogonType.Group;
				//this.flagExecutorIsCurrentUser = false;
			}
			else
			{
				task.Definition.Principal.UserId = dlg.Picks[0].ObjectName;
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(objArray[0].ObjectName);
				if (task.Definition.Principal.LogonType == TaskLogonType.Group)
				{
					task.Definition.Principal.LogonType = TaskLogonType.InteractiveToken;
				}
				else if (task.Definition.Principal.LogonType == TaskLogonType.ServiceAccount)
				{
					task.Definition.Principal.LogonType = TaskLogonType.Password;
				}
			}
			SetUserControls(task.Definition.Principal.LogonType);
		}

		private void AddActionToList(Action act)
		{
			TaskActionType t = TaskActionType.Execute;
			switch (act.GetType().Name)
			{
				case "ComHandlerAction":
					t = TaskActionType.ComHandler;
					break;
				case "EmailAction":
					t = TaskActionType.SendEmail;
					break;
				case "ShowMessageAction":
					t = TaskActionType.ShowMessage;
					break;
				case "ExecAction":
				default:
					break;
			}
			actionListView.Items.Add(new ListViewItem(new string[] {
					BuildEnumString(taskSchedResources, "ActionType", t),
					act.ToString() }));
		}

		private void conditionsTab_Enter(object sender, EventArgs e)
		{
			// Load network connections
			availableConnectionsCombo.Items.Clear();
			availableConnectionsCombo.Items.Add(Properties.Resources.AnyConnection);
			foreach (var n in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
				availableConnectionsCombo.Items.Add(n.Name);
			if (task == null)
				availableConnectionsCombo.SelectedIndex = -1;
			else
				availableConnectionsCombo.SelectedText = task.Definition.Settings.NetworkSettings.Name;
		}

		private void changePrincipalButton_Click(object sender, EventArgs e)
		{
			InvokeObjectPicker(task.TaskService.TargetServer);
		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{
			TriggerEditDialog dlg = new TriggerEditDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				task.Definition.Triggers.Add(dlg.Trigger);
				AddTriggerToList(dlg.Trigger);
			}
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices[0];
			TriggerEditDialog dlg = new TriggerEditDialog();
			dlg.Trigger = task.Definition.Triggers[idx];
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				task.Definition.Triggers.RemoveAt(idx);
				triggerListView.Items.RemoveAt(idx);
				task.Definition.Triggers.Add(dlg.Trigger);
				AddTriggerToList(dlg.Trigger);
			}
		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices[0];
			task.Definition.Triggers.RemoveAt(idx);
			triggerListView.Items.RemoveAt(idx);
		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{
			ActionEditDialog dlg = new ActionEditDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				task.Definition.Actions.Add(dlg.Action);
				AddActionToList(dlg.Action);
				SetActionUpDnState();
			}
		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices[0];
			ActionEditDialog dlg = new ActionEditDialog();
			dlg.Action = task.Definition.Actions[idx];
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				task.Definition.Actions.RemoveAt(idx);
				actionListView.Items.RemoveAt(idx);
				task.Definition.Actions.Add(dlg.Action);
				AddActionToList(dlg.Action);
			}
		}

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices[0];
			task.Definition.Actions.RemoveAt(idx);
			actionListView.Items.RemoveAt(idx);
			SetActionUpDnState();
		}

		private void SetActionUpDnState()
		{
			actionUpButton.Enabled = actionDownButton.Enabled = actionListView.Items.Count > 1;
		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{

		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{

		}

		private void taskIdleDurationCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskIdleDurationCombo.Enabled = taskIdleWaitTimeoutCombo.Enabled = taskStopOnIdleEndCheck.Enabled = editable && taskIdleDurationCheck.Checked;
			if (taskIdleDurationCheck.Checked)
			{
				taskIdleDurationCombo.Value = TimeSpan.FromMinutes(10);
				taskIdleWaitTimeoutCombo.Value = TimeSpan.FromHours(1);
			}
			else
				taskIdleDurationCombo.Value = taskIdleWaitTimeoutCombo.Value = TimeSpan.Zero;
			taskRestartOnIdleCheck_CheckedChanged(sender, e);
		}

		private void taskStopOnIdleEndCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskRestartOnIdleCheck.Enabled = editable && (taskStopOnIdleEndCheck.Checked && taskStopOnIdleEndCheck.Enabled);
			task.Definition.Settings.IdleSettings.StopOnIdleEnd = taskStopOnIdleEndCheck.Checked;
		}

		private void taskRestartOnIdleCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.IdleSettings.RestartOnIdle = taskRestartOnIdleCheck.Checked;
		}

		private void taskStartIfConnectionCheck_CheckedChanged(object sender, EventArgs e)
		{
			availableConnectionsCombo.Enabled = editable && taskStartIfConnectionCheck.Checked;
		}

		private void taskRestartIntervalCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskRestartIntervalCombo.Enabled = taskRestartCountText.Enabled = editable && taskRestartIntervalCheck.Checked;
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

		private void taskExecutionTimeLimitCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskExecutionTimeLimitCombo.Enabled = editable && taskExecutionTimeLimitCheck.Checked;
			if (taskExecutionTimeLimitCheck.Checked)
				taskExecutionTimeLimitCombo.Value = TimeSpan.FromDays(3);
			else
				taskExecutionTimeLimitCombo.Value = TimeSpan.Zero;
		}

		private void taskDeleteAfterCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskDeleteAfterCombo.Enabled = editable && taskDeleteAfterCheck.Checked;
			if (taskDeleteAfterCheck.Checked)
				taskDeleteAfterCombo.Value = TimeSpan.FromDays(30);
			else
				taskDeleteAfterCombo.Value = TimeSpan.Zero;
		}

		private void taskDisallowStartIfOnBatteriesCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStopIfGoingOnBatteriesCheck.Enabled = editable && taskDisallowStartIfOnBatteriesCheck.Checked;
			task.Definition.Settings.DisallowStartIfOnBatteries = taskDisallowStartIfOnBatteriesCheck.Checked;
		}

		private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
		{
			taskLocalOnlyCheck.Enabled = editable && taskLoggedOptionalRadio.Checked;
		}

		private void historyTab_Enter(object sender, EventArgs e)
		{
			historyListView.Items.Clear();
			historyListView.Cursor = Cursors.WaitCursor;
			historyBackgroundWorker.RunWorkerAsync();
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

		private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			triggerEditButton_Click(sender, EventArgs.Empty);
		}

		private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			actionEditButton_Click(sender, EventArgs.Empty);
		}

		private void taskHiddenCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.Hidden = taskHiddenCheck.Checked;
		}

		private void taskVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.Compatibility = taskVersionCombo.SelectedIndex == 0 ? TaskCompatibility.V1 : TaskCompatibility.V2;
			// TODO: Enable appropriate settings
		}

		private void taskStartAfterIdleCombo_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.IdleSettings.IdleDuration = taskIdleDurationCombo.Value;
		}

		private void taskIdleDelayCombo_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.IdleSettings.WaitTimeout = taskIdleWaitTimeoutCombo.Value;
		}

		private void taskRestartIntervalCombo_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.RestartInterval = taskRestartIntervalCombo.Value;
			taskRestartIntervalCheck.Checked = task.Definition.Settings.RestartInterval != TimeSpan.Zero;
		}

		private void taskRestartCountText_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.RestartCount = Convert.ToInt32(taskRestartCountText.Value);
		}

		private void taskExecutionTimeLimitCombo_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.ExecutionTimeLimit = taskExecutionTimeLimitCombo.Value;
			taskExecutionTimeLimitCheck.Checked = taskExecutionTimeLimitCombo.Value != TimeSpan.Zero;
		}

		private void taskDeleteAfterCombo_ValueChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.DeleteExpiredTaskAfter = taskDeleteAfterCombo.Value;
			taskDeleteAfterCheck.Checked = taskDeleteAfterCombo.Value != TimeSpan.Zero;
		}

		private void taskAllowDemandStartCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.AllowDemandStart = taskAllowDemandStartCheck.Checked;
		}

		private void taskStartWhenAvailableCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.StartWhenAvailable = taskStartWhenAvailableCheck.Checked;
		}

		private void taskAllowHardTerminateCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.AllowHardTerminate = taskAllowHardTerminateCheck.Checked;
		}

		private void taskMultInstCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.MultipleInstances = (TaskInstancesPolicy)taskMultInstCombo.SelectedIndex;
		}

		private void taskStopIfGoingOnBatteriesCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.StopIfGoingOnBatteries = taskStopIfGoingOnBatteriesCheck.Checked;
		}

		private void taskWakeToRunCheck_CheckedChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.WakeToRun = taskWakeToRunCheck.Checked;
		}

		private void availableConnectionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			task.Definition.Settings.RunOnlyIfNetworkAvailable = availableConnectionsCombo.SelectedIndex != -1;
			task.Definition.Settings.NetworkSettings.Name = availableConnectionsCombo.SelectedValue.ToString();
		}

		private void taskDescText_Leave(object sender, EventArgs e)
		{
			task.Definition.RegistrationInfo.Description = taskDescText.Text;
		}
	}
}
