using System;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class StartupOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public StartupOptionPanel()
		{
			InitializeComponent();

			taskIdleDurationCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromMinutes(60) });
			taskIdleWaitTimeoutCombo.FormattedZero = EditorProperties.Resources.TimeSpanDoNotWait;
			taskIdleWaitTimeoutCombo.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2) });

			// Load network connections
			availableConnectionsCombo.BeginUpdate();
			availableConnectionsCombo.Items.Clear();
			availableConnectionsCombo.Items.Add(EditorProperties.Resources.AnyConnection);
			availableConnectionsCombo.Items.AddRange(Microsoft.Win32.NativeMethods.NetworkProfile.GetAllLocalProfiles());
			availableConnectionsCombo.EndUpdate();
		}

		protected override void InitializePanel()
		{
			bool editable = parent.Editable;
			bool v2 = parent.IsV2;
			taskStopIfGoingOnBatteriesCheck.Enabled = editable && td.Settings.DisallowStartIfOnBatteries;
			taskStartIfConnectionCheck.Enabled = editable && v2;
			availableConnectionsCombo.Enabled = editable && v2 && td.Settings.RunOnlyIfNetworkAvailable && ((parent.TaskService != null && parent.TaskService.HighestSupportedVersion < new Version(1, 5)) || !td.Settings.UseUnifiedSchedulingEngine);
			taskDisallowStartOnRemoteAppSessionCheck.Enabled = editable && td.Settings.Compatibility >= TaskCompatibility.V2_1;

			taskRestartOnIdleCheck.Checked = td.Settings.IdleSettings.RestartOnIdle;
			taskStopOnIdleEndCheck.Checked = td.Settings.IdleSettings.StopOnIdleEnd;
			taskIdleDurationCheck.Checked = td.Settings.RunOnlyIfIdle;
			taskIdleDurationCombo.Value = td.Settings.IdleSettings.IdleDuration;
			taskIdleWaitTimeoutCombo.Value = td.Settings.IdleSettings.WaitTimeout;
			UpdateIdleSettingsControls();
			taskDisallowStartIfOnBatteriesCheck.Checked = td.Settings.DisallowStartIfOnBatteries;
			taskStopIfGoingOnBatteriesCheck.Checked = td.Settings.StopIfGoingOnBatteries;
			taskWakeToRunCheck.Checked = td.Settings.WakeToRun;
			taskStartIfConnectionCheck.Checked = td.Settings.RunOnlyIfNetworkAvailable;
			taskDisallowStartOnRemoteAppSessionCheck.Checked = td.Settings.DisallowStartOnRemoteAppSession;

			if (parent.Task == null || td.Settings.NetworkSettings.Id == Guid.Empty)
				availableConnectionsCombo.SelectedIndex = 0;
			else
				availableConnectionsCombo.SelectedItem = td.Settings.NetworkSettings.Id;
		}

		private void taskDisallowStartIfOnBatteriesCheck_CheckedChanged(object sender, EventArgs e)
		{
			taskStopIfGoingOnBatteriesCheck.Enabled = parent.Editable && taskDisallowStartIfOnBatteriesCheck.Checked;
			if (!onAssignment)
				td.Settings.DisallowStartIfOnBatteries = taskDisallowStartIfOnBatteriesCheck.Checked;
		}

		private void taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.DisallowStartOnRemoteAppSession = taskDisallowStartOnRemoteAppSessionCheck.Checked;
		}

		private void taskIdleDurationCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				taskIdleDurationCombo.Value = TimeSpan.FromMinutes(10);
				taskIdleWaitTimeoutCombo.Value = TimeSpan.FromHours(1);
				td.Settings.RunOnlyIfIdle = taskIdleDurationCheck.Checked;
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

		private void taskRestartOnIdleCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.IdleSettings.RestartOnIdle = taskRestartOnIdleCheck.Checked;
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

		private void taskWakeToRunCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.WakeToRun = taskWakeToRunCheck.Checked;
		}

		private void taskStartIfConnectionCheck_CheckedChanged(object sender, EventArgs e)
		{
			availableConnectionsCombo.Enabled = parent.Editable && taskStartIfConnectionCheck.Checked && ((parent.TaskService != null && parent.TaskService.HighestSupportedVersion < new Version(1, 5)) || !td.Settings.UseUnifiedSchedulingEngine);
			if (!onAssignment)
				td.Settings.RunOnlyIfNetworkAvailable = taskStartIfConnectionCheck.Checked;
		}

		private void UpdateIdleSettingsControls()
		{
			bool idleEnabled = taskIdleDurationCheck.Checked ? parent.Editable : false;
			taskIdleDurationCombo.Enabled = taskIdleWaitTimeoutLabel.Enabled =
				taskIdleWaitTimeoutCombo.Enabled = taskStopOnIdleEndCheck.Enabled = idleEnabled;
			taskRestartOnIdleCheck.Enabled = parent.IsV2 && idleEnabled && td.Settings.IdleSettings.StopOnIdleEnd;
		}

		private void availableConnectionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (availableConnectionsCombo.SelectedIndex > 0)
				{
					td.Settings.NetworkSettings.Id = ((Microsoft.Win32.NativeMethods.NetworkProfile)availableConnectionsCombo.SelectedItem).Id;
					td.Settings.NetworkSettings.Name = ((Microsoft.Win32.NativeMethods.NetworkProfile)availableConnectionsCombo.SelectedItem).Name;
				}
				else
				{
					td.Settings.NetworkSettings.Id = Guid.Empty;
					td.Settings.NetworkSettings.Name = null;
				}
			}
		}
	}
}
