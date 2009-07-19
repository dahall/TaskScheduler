using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class TaskPropertiesControl : UserControl
	{
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
		}

		private Task task = null;

		internal static string BuildEnumString(string preface, object enumValue)
		{
			string[] vals = enumValue.ToString().Split(new string[] { ", " }, StringSplitOptions.None);
			if (vals.Length == 0)
				return string.Empty;

			for (int i = 0; i < vals.Length; i++)
			{
				vals[i] = Properties.Resources.ResourceManager.GetString(preface + vals[i]);
			}
			return string.Join(", ", vals);
		}

		public Task GetTask()
		{
			return task;
		}

		public void SetTask(Task newTask)
		{
			task = newTask;

			// Set General tab
			taskNameText.Text = task.Name;
			taskAuthorText.Text = task.Definition.RegistrationInfo.Author;
			taskDescText.Text = task.Definition.RegistrationInfo.Description;
			taskPrincipalText.Text = task.Definition.Principal.DisplayName;
			taskLoggedOnRadio.Checked = task.Definition.Principal.LogonType == TaskLogonType.InteractiveToken;
			taskLoggedOptionalRadio.Checked = taskLocalOnlyCheck.Enabled = !taskLoggedOnRadio.Checked;
			taskRunLevelCheck.Checked = task.Definition.Principal.RunLevel == TaskRunLevel.Highest;
			taskHiddenCheck.Checked = task.Definition.Settings.Hidden;
			taskVersionCombo.SelectedIndex = task.Definition.Settings.Compatibility == TaskCompatibility.V1 ? 0 : 1;

			// Set Triggers tab
			foreach (Trigger tr in task.Definition.Triggers)
			{
				triggerListView.Items.Add(new ListViewItem(new string[] {
					BuildEnumString("TriggerType", tr.TriggerType),
					tr.ToString(),
					tr.Enabled ? Properties.Resources.Enabled : Properties.Resources.Disabled
				}));
			}

			// Set Actions tab
			foreach (Action act in task.Definition.Actions)
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
					BuildEnumString("ActionType", t),
					act.ToString() }));
			}

			// Set Conditions tab
			taskStartAfterIdleCheck.Checked = taskStartAfterIdleCombo.Enabled = task.Definition.Settings.IdleSettings.IdleDuration != TimeSpan.Zero;
			if (taskStartAfterIdleCombo.Enabled) taskStartAfterIdleCombo.Text = task.Definition.Settings.IdleSettings.IdleDuration.ToString("");
			taskIdleDelayCombo.Enabled = task.Definition.Settings.IdleSettings.WaitTimeout != TimeSpan.Zero;
			if (taskIdleDelayCombo.Enabled) taskIdleDelayCombo.Text = task.Definition.Settings.IdleSettings.WaitTimeout.ToString("");
			taskStopIfNotIdleCheck.Checked = taskRestartWhenIdleCheck.Enabled = task.Definition.Settings.IdleSettings.StopOnIdleEnd;
			taskRestartWhenIdleCheck.Checked = task.Definition.Settings.IdleSettings.RestartOnIdle;
			taskStartOnlyOnACCheck.Checked = taskStopIfBatteryCheck.Enabled = task.Definition.Settings.DisallowStartIfOnBatteries;
			taskStopIfBatteryCheck.Checked = task.Definition.Settings.StopIfGoingOnBatteries;
			taskWakeToRunCheck.Checked = task.Definition.Settings.WakeToRun;
			taskStartIfConnectionCheck.Checked = availableConnectionsCombo.Enabled = task.Definition.Settings.RunOnlyIfNetworkAvailable;
			if (taskStartIfConnectionCheck.Checked) availableConnectionsCombo.SelectedItem = task.Definition.Settings.NetworkSettings.Name;

			// Set Settings tab
			taskRunOnDemandCheck.Checked = task.Definition.Settings.AllowDemandStart;
			taskRunAfterMissedCheck.Checked = task.Definition.Settings.StartWhenAvailable;
			taskRestartAfterFailureCheck.Checked = taskRestartAfterSpanCombo.Enabled = task.Definition.Settings.RestartInterval != TimeSpan.Zero;
			if (taskRestartAfterFailureCheck.Checked)
			{
				taskRestartAfterSpanCombo.Text = task.Definition.Settings.RestartInterval.ToString("");
				taskRestartAttemptsText.Text = task.Definition.Settings.RestartCount.ToString("");
			}
			taskStopIfRunningAfterCheck.Checked = taskStopIfRunningAfterCombo.Enabled = task.Definition.Settings.ExecutionTimeLimit != TimeSpan.Zero;
			if (taskStopIfRunningAfterCombo.Enabled) taskStopIfRunningAfterCombo.Text = task.Definition.Settings.ExecutionTimeLimit.ToString("");
			taskForceStopCheck.Checked = task.Definition.Settings.AllowHardTerminate;
			taskDeleteAfterCheck.Checked = taskDeleteAfterCombo.Enabled = task.Definition.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero;
			if (taskDeleteAfterCombo.Enabled) taskDeleteAfterCombo.Text = task.Definition.Settings.DeleteExpiredTaskAfter.ToString("");
			taskRunningRuleCombo.SelectedIndex = (int)task.Definition.Settings.MultipleInstances;
		}

		private bool editable = false;

		public bool Editable
		{
			get { return editable; }
			set
			{
				editable = value;
				/*foreach (TabPage item in tabControl1.TabPages)
				{
					foreach (Control c in item.Controls)
					{
						c.Enabled = value;
					}
				}*/
			}
		}

		private void conditionsTab_Enter(object sender, EventArgs e)
		{
			// Load network connections
			availableConnectionsCombo.Items.Clear();
			availableConnectionsCombo.Items.Add(Properties.Resources.AnyConnection);
			foreach (var n in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
				availableConnectionsCombo.Items.Add(n.Name);
			availableConnectionsCombo.SelectedIndex = 0;
		}

		private void changePrincipalButton_Click(object sender, EventArgs e)
		{

		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{

		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{

		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{

		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{

		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{

		}

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{

		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{

		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{

		}

		private void taskStartAfterIdleCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStartAfterIdleCombo.Enabled = taskIdleDelayCombo.Enabled = taskStopIfNotIdleCheck.Enabled = taskStartAfterIdleCheck.Checked;
			taskStopIfNotIdleCheck_CheckedChanged(sender, e);
		}

		private void taskStopIfNotIdleCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskRestartWhenIdleCheck.Enabled = (taskStopIfNotIdleCheck.Checked && taskStopIfNotIdleCheck.Enabled);
		}

		private void taskStartIfConnectionCheck_CheckedChanged(object sender, EventArgs e)
		{
			availableConnectionsCombo.Enabled = taskStartIfConnectionCheck.Checked;
		}

		private void taskRestartAfterFailureCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskRestartAfterSpanCombo.Enabled = taskRestartAttemptsText.Enabled = taskRestartAfterFailureCheck.Checked;
		}

		private void taskStopIfRunningAfterCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStopIfRunningAfterCombo.Enabled = taskStopIfRunningAfterCheck.Checked;
		}

		private void taskDeleteAfterCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskDeleteAfterCombo.Enabled = taskDeleteAfterCheck.Checked;
		}

		private void taskStartOnlyOnACCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStopIfBatteryCheck.Enabled = taskStartOnlyOnACCheck.Checked;
		}

		private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
		{
			taskLocalOnlyCheck.Enabled = taskLoggedOptionalRadio.Checked;
		}

		private void historyTab_Enter(object sender, EventArgs e)
		{
			historyBackgroundWorker.RunWorkerAsync(task.Path);
		}

		private void AddHistoryItem(TaskEvent item)
		{
			string[] items = new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(), item.TaskCategory, item.OpCode, item.ActivityId.ToString() };
			historyListView.Items.Add(new ListViewItem(items));
		}

		private void ClearHistoryView()
		{
			historyListView.Clear();
		}

		private delegate void InvokeAddHistoryItem(TaskEvent te);
		private delegate void ClearListView();

		private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Invoke(new ClearListView(ClearHistoryView));
			TaskEventLog log = new TaskEventLog(e.Argument.ToString());
			foreach (TaskEvent item in log)
			{
				Invoke(new InvokeAddHistoryItem(AddHistoryItem), item);
			}
		}
	}
}
