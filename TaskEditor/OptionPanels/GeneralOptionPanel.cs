using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class GeneralOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public GeneralOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			taskHiddenCheck.Enabled = taskEnabledCheck.Enabled = parent.Editable;
			taskUseUnifiedSchedulingEngineCheck.Enabled = parent.Editable && td.Settings.Compatibility >= TaskCompatibility.V2_1;
			taskVolatileCheck.Enabled = parent.Editable && td.Settings.Compatibility >= TaskCompatibility.V2_2;
			taskDescText.ReadOnly = !parent.Editable;
			taskRegSourceText.ReadOnly = taskRegURIText.ReadOnly = taskRegVersionText.ReadOnly = taskRegDocText.ReadOnly = !parent.Editable;

			taskEnabledCheck.Checked = td.Settings.Enabled;
			taskHiddenCheck.Checked = td.Settings.Hidden;
			taskVolatileCheck.Checked = td.Settings.Volatile;
			taskUseUnifiedSchedulingEngineCheck.Checked = td.Settings.UseUnifiedSchedulingEngine;
			if (string.IsNullOrEmpty(td.RegistrationInfo.Author))
				td.RegistrationInfo.Author = WindowsIdentity.GetCurrent().Name;
			taskAuthorText.Text = parent.GetStringValue(td.RegistrationInfo.Author);
			taskDescText.Text = parent.GetStringValue(td.RegistrationInfo.Description);
			taskRegSourceText.Text = parent.GetStringValue(td.RegistrationInfo.Source);
			taskRegURIText.Text = td.RegistrationInfo.URI;
			taskRegVersionText.Text = td.RegistrationInfo.Version.ToString();
			taskRegDocText.Text = parent.GetStringValue(td.RegistrationInfo.Documentation);
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
				s => Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute),
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

		private bool ValidateText(Control ctrl, Predicate<string> pred, string error)
		{
			bool valid = pred(ctrl.Text);
			//errorProvider.SetError(ctrl, valid ? string.Empty : error);
			//OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, error));
			//hasError = valid;
			return valid;
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
						{
							if (td.Principal.LogonType == TaskLogonType.InteractiveTokenOrPassword)
								td.Principal.LogonType = TaskLogonType.InteractiveToken;
							//if (td.Settings.MultipleInstances == TaskInstancesPolicy.StopExisting)
							//	td.Settings.MultipleInstances = TaskInstancesPolicy.IgnoreNew;
							//td.Settings.AllowHardTerminate = true;
							if (!parent.TaskDefinition.Actions.PowerShellConversion.IsFlagSet(PowerShellActionPlatformOption.Version2))
								for (int i = td.Actions.Count - 1; i >= 0; i--)
								{
									if (td.Actions[i].ActionType == TaskActionType.SendEmail || td.Actions[i].ActionType == TaskActionType.ShowMessage)
										td.Actions.RemoveAt(i);
								}
							if (parent.TaskService != null && parent.TaskService.HighestSupportedVersion == TaskServiceVersion.V1_3)
								for (int i = td.Triggers.Count - 1; i >= 0; i--)
								{
									if (td.Triggers[i].TriggerType == TaskTriggerType.Monthly || td.Triggers[i].TriggerType == TaskTriggerType.MonthlyDOW)
									{
										td.Triggers.RemoveAt(i);
									}
									else
									{
										Trigger t = td.Triggers[i];
										t.ExecutionTimeLimit = TimeSpan.Zero;
										if (t is ICalendarTrigger)
										{
											t.Repetition.Duration = t.Repetition.Interval = TimeSpan.Zero;
											t.Repetition.StopAtDurationEnd = false;
										}
										else if (t is EventTrigger et)
											et.ValueQueries.Clear();
									}
								}
						}
						else
							taskUseUnifiedSchedulingEngineCheck.Checked = false;
					}
				}
				td.Settings.UseUnifiedSchedulingEngine = taskUseUnifiedSchedulingEngineCheck.Checked;
				parent.UpdateAvailableActions(parent.AvailableActions);
				parent.UpdateAvailableTriggers(parent.AvailableTriggers);
				parent.ReinitializeControls();
			}
		}

		private void taskVolatileCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Settings.Volatile = taskVolatileCheck.Checked;
		}
	}
}
