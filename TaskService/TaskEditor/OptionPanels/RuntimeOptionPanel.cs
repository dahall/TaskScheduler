using System;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class RuntimeOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public RuntimeOptionPanel()
		{
			InitializeComponent();
			long allVal;
			ComboBoxExtension.InitializeFromEnum(taskPriorityCombo.Items, typeof(System.Diagnostics.ProcessPriorityClass), EditorProperties.Resources.ResourceManager, "ProcessPriority", out allVal);
		}

		protected override void InitializePanel()
		{
			bool editable = parent.Editable;
			bool v2 = parent.IsV2;

			taskAllowDemandStartCheck.Enabled = taskStartWhenAvailableCheck.Enabled =
				taskRestartIntervalCheck.Enabled = taskRestartIntervalCombo.Enabled =
				taskRestartCountLabel.Enabled = taskRestartAttemptTimesLabel.Enabled = taskRestartCountText.Enabled =
				taskRunningRuleLabel.Enabled = taskMultInstCombo.Enabled = editable && v2;
			taskAllowHardTerminateCheck.Enabled = editable && v2 && !td.Settings.UseUnifiedSchedulingEngine;

			taskAllowDemandStartCheck.Checked = td.Settings.AllowDemandStart;

			// Update Multiple Instances policy combo
			taskMultInstCombo.BeginUpdate();
			long allVal;
			ComboBoxExtension.InitializeFromEnum(taskMultInstCombo.Items, typeof(TaskInstancesPolicy), EditorProperties.Resources.ResourceManager, "TaskInstances", out allVal);
			if (td.Settings.UseUnifiedSchedulingEngine)
				taskMultInstCombo.Items.RemoveAt(taskMultInstCombo.Items.IndexOf((long)TaskInstancesPolicy.StopExisting));
			taskMultInstCombo.SelectedIndex = taskMultInstCombo.Items.IndexOf((long)td.Settings.MultipleInstances);
			taskMultInstCombo.EndUpdate();

			taskPriorityCombo.SelectedIndex = taskPriorityCombo.Items.IndexOf((long)td.Settings.Priority);
		}

		private void taskAllowDemandStartCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment && parent.IsV2)
				td.Settings.AllowDemandStart = taskAllowDemandStartCheck.Checked;
		}

		private void taskAllowHardTerminateCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment && parent.IsV2)
				td.Settings.AllowHardTerminate = taskAllowHardTerminateCheck.Checked;
		}

		private void taskDeleteAfterCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskDeleteAfterCombo.Enabled = parent.Editable && taskDeleteAfterCheck.Checked;
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

		private void taskExecutionTimeLimitCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskExecutionTimeLimitCombo.Enabled = parent.Editable && taskExecutionTimeLimitCheck.Checked;
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

		private void taskMultInstCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment && parent.IsV2 && td != null)
				td.Settings.MultipleInstances = (TaskInstancesPolicy)((DropDownCheckListItem)taskMultInstCombo.SelectedItem).Value;
		}

		private void taskPriorityCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Priority = (System.Diagnostics.ProcessPriorityClass)((DropDownCheckListItem)taskPriorityCombo.SelectedItem).Value;
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
			taskRestartIntervalCombo.Enabled = taskRestartCountLabel.Enabled = taskRestartCountText.Enabled = parent.Editable && taskRestartIntervalCheck.Checked;
		}

		private void taskRestartIntervalCombo_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.RestartInterval = taskRestartIntervalCombo.Value;
		}

		private void taskStartWhenAvailableCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.StartWhenAvailable = taskStartWhenAvailableCheck.Checked;
		}
	}
}
