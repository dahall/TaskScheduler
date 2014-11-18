using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class GeneralOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		private bool v2;

		public GeneralOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			taskNameText.Text = parent.Task != null ? parent.Task.Name : string.Empty;
			taskNameText.ReadOnly = !(parent.Task == null && parent.Editable);
			taskDescText.Text = td.RegistrationInfo.Description;
			SetVersionComboItems();
			taskEnabledCheck.Checked = td.Settings.Enabled;
			taskHiddenCheck.Checked = td.Settings.Hidden;
			taskAuthorText.Text = string.IsNullOrEmpty(td.RegistrationInfo.Author) ? WindowsIdentity.GetCurrent().Name : td.RegistrationInfo.Author;
			taskRegSourceText.Text = td.RegistrationInfo.Source;
			taskRegURIText.Text = td.RegistrationInfo.URI;
			taskRegVersionText.Text = td.RegistrationInfo.Version.ToString();
			taskRegDocText.Text = td.RegistrationInfo.Documentation;
		}

		private void SetVersionComboItems()
		{
			const int expectedVersions = 5;

			this.taskVersionCombo.BeginUpdate();
			this.taskVersionCombo.Items.Clear();
			string[] versions = EditorProperties.Resources.TaskCompatibility.Split('|');
			if (versions.Length != expectedVersions)
				throw new ArgumentOutOfRangeException("Locale specific information about supported Operating Systems is insufficient.");
			int max = (parent.TaskService == null) ? expectedVersions - 1 : TaskService.LibraryVersion.Minor;
			TaskCompatibility comp = (td != null) ? td.Settings.Compatibility : TaskCompatibility.V1;
			TaskCompatibility lowestComp = (td != null) ? td.LowestSupportedVersion : TaskCompatibility.V1;
			switch (comp)
			{
				case TaskCompatibility.AT:
					for (int i = max; i > 1; i--)
						this.taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					this.taskVersionCombo.SelectedIndex = this.taskVersionCombo.Items.Add(new ComboItem(versions[0], 0));
					break;
				default:
					for (int i = max; i > 0; i--)
						this.taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					this.taskVersionCombo.SelectedIndex = this.taskVersionCombo.Items.IndexOf((int)comp);
					break;
			}
			this.taskVersionCombo.EndUpdate();
		}

		private void taskDescText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment && td != null)
				td.RegistrationInfo.Description = taskDescText.Text;
		}

		private void taskEnabledCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Enabled = taskEnabledCheck.Checked;
		}

		private void taskHiddenCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Hidden = taskHiddenCheck.Checked;
		}

		private void taskNameText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			char[] inv = System.IO.Path.GetInvalidFileNameChars();
			e.Cancel = !ValidateText(taskNameText,
				delegate(string s) { return s.Length > 0 && s.IndexOfAny(inv) == -1; },
				EditorProperties.Resources.Error_InvalidNameFormat);
		}

		private void taskRegDocText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Documentation = taskRegDocText.TextLength > 0 ? taskRegDocText.Text : null;
		}

		private void taskRegSourceText_Leave(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Source = taskRegSourceText.TextLength > 0 ? taskRegSourceText.Text : null;
		}

		private void taskRegURIText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.URI = taskRegURIText.TextLength > 0 ? taskRegURIText.Text : null;
		}

		private void taskRegURIText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !ValidateText(taskRegURIText,
				delegate(string s) { return true; },
				EditorProperties.Resources.Error_InvalidUriFormat);
		}

		private void taskRegVersionText_Validated(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.RegistrationInfo.Version = taskRegVersionText.TextLength > 0 ? new Version(taskRegVersionText.Text) : null;
		}

		private void taskRegVersionText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !ValidateText(taskRegVersionText,
				delegate(string s) { return System.Text.RegularExpressions.Regex.IsMatch(s, @"^(\d+(\.\d+){0,2}(\.\d+))?$"); },
				EditorProperties.Resources.Error_InvalidVersionFormat);
		}

		private void taskVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			v2 = taskVersionCombo.SelectedIndex == -1 ? true : ((ComboItem)taskVersionCombo.SelectedItem).Version > 1;
			bool v2_1 = taskVersionCombo.SelectedIndex == -1 ? true : ((ComboItem)taskVersionCombo.SelectedItem).Version > 2;
			bool v2_2 = taskVersionCombo.SelectedIndex == -1 ? true : ((ComboItem)taskVersionCombo.SelectedItem).Version > 3;
			TaskCompatibility priorSetting = (td != null) ? td.Settings.Compatibility : TaskCompatibility.V1;
			if (!onAssignment && td != null && taskVersionCombo.SelectedIndex != -1)
				td.Settings.Compatibility = (TaskCompatibility)((ComboItem)taskVersionCombo.SelectedItem).Version;
			try
			{
				if (!onAssignment && td != null)
					td.Validate(true);
			}
			catch (InvalidOperationException ex)
			{
				var msg = new System.Text.StringBuilder();
				if (parent.ShowErrors)
				{
					msg.AppendLine(EditorProperties.Resources.Error_TaskPropertiesIncompatible);
					foreach (var item in ex.Data.Keys)
						msg.AppendLine(string.Format("- {0} {1}", item, ex.Data[item]));
				}
				else
					msg.Append(EditorProperties.Resources.Error_TaskPropertiesIncompatibleSimple);
				MessageBox.Show(this, msg.ToString(), EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.taskVersionCombo.SelectedIndex = this.taskVersionCombo.Items.IndexOf((int)priorSetting);
				return;
			}
			/*taskRunLevelCheck.Enabled = taskAllowDemandStartCheck.Enabled = taskStartWhenAvailableCheck.Enabled =
				taskRestartIntervalCheck.Enabled = taskRestartIntervalCombo.Enabled =
				taskRestartCountLabel.Enabled = taskRestartAttemptTimesLabel.Enabled = taskRestartCountText.Enabled =
				taskAllowHardTerminateCheck.Enabled = taskRunningRuleLabel.Enabled = taskMultInstCombo.Enabled =
				taskStartIfConnectionCheck.Enabled = taskRegSourceText.Enabled = taskRegURIText.Enabled =
				taskRegVersionText.Enabled = taskRegSDDLText.Enabled = editable && v2;
			availableConnectionsCombo.Enabled = editable && v2 && taskStartIfConnectionCheck.Checked && !taskUseUnifiedSchedulingEngineCheck.Checked;
			principalSIDTypeLabel.Enabled = principalSIDTypeCombo.Enabled = principalReqPrivilegesLabel.Enabled =
				principalReqPrivilegesDropDown.Enabled = taskDisallowStartOnRemoteAppSessionCheck.Enabled =
				taskUseUnifiedSchedulingEngineCheck.Enabled = principalSIDTypeCombo.Enabled = principalReqPrivilegesDropDown.Enabled =
				editable && v2_1;
			taskVolatileCheck.Enabled = taskMaintenanceDeadlineLabel.Enabled = taskMaintenanceDeadlineCombo.Enabled =
				taskMaintenanceExclusiveCheck.Enabled = taskMaintenancePeriodLabel.Enabled = taskMaintenancePeriodCombo.Enabled = editable && v2_2;*/
			taskDescText.ReadOnly = !parent.Editable;
			taskHiddenCheck.Enabled = taskEnabledCheck.Enabled = taskVersionCombo.Enabled = parent.Editable;
			taskRegSourceText.ReadOnly = taskRegURIText.ReadOnly = taskRegVersionText.ReadOnly = !(parent.Editable && v2);
		}

		private bool ValidateText(Control ctrl, Predicate<string> pred, string error)
		{
			bool valid = pred(ctrl.Text);
			//errorProvider.SetError(ctrl, valid ? string.Empty : error);
			//OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, error));
			//hasError = valid;
			return valid;
		}

		private class ComboItem : IEnableable
		{
			public string Text;
			public int Version;
			private bool enabled;
			public ComboItem(string text, int ver, bool enabled = true) { Text = text; Version = ver; this.enabled = enabled; }

			public bool Enabled
			{
				get { return enabled; }
				set { enabled = value; }
			}

			public override bool Equals(object obj)
			{
				if (obj is ComboItem)
					return Version == ((ComboItem)obj).Version;
				if (obj is int)
					return Version == (int)obj;
				return Text.CompareTo(obj.ToString()) == 0;
			}

			public override int GetHashCode()
			{
				return Version.GetHashCode();
			}

			public override string ToString() { return this.Text; }
		}
	}
}
