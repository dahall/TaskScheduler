using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class TriggerEditDialog : Form
	{
		private Trigger trigger;

		public TriggerEditDialog()
		{
			InitializeComponent();
			triggerTypeCombo.SelectedIndex = 0;

			// Populate monthly combo boxes
			monthlyMonthsDropDown.InitializeFromEnum(typeof(MonthsOfTheYear), TaskPropertiesControl.taskSchedResources, "MOY");
			monthlyMonthsDropDown.Items.RemoveAt(13);
			monthlyDaysDropDown.InitializeFromRange(1, 31);
			monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(Properties.Resources.Last, 99));
			monthlyDaysDropDown.MultiColumnList = true;
			monthlyOnWeekDropDown.InitializeFromEnum(typeof(WhichWeek), TaskPropertiesControl.taskSchedResources, "WW");
			monthlyOnWeekDropDown.Items.RemoveAt(5);
			monthlyOnDOWDropDown.InitializeFromEnum(typeof(DaysOfTheWeek), TaskPropertiesControl.taskSchedResources, "DOW");
			monthlyOnDOWDropDown.Items.RemoveAt(8);
		}

		private void SetSchedTrigger()
		{
			triggerTypeCombo.SelectedIndex = 0;
			schedStartDatePicker.Value = trigger.StartBoundary;
		}

		public Trigger Trigger
		{
			get
			{
				return trigger;
			}
			set
			{
				trigger = value;
				delayCheckBox.Enabled = true;
				switch (trigger.TriggerType)
				{
					case TaskTriggerType.Time:
						schedOneRadio.Checked = true;
						SetSchedTrigger();
						delayCheckBox.Checked = ((TimeTrigger)trigger).RandomDelay != TimeSpan.Zero;
						delaySpan.Value = ((TimeTrigger)trigger).RandomDelay;
						break;
					case TaskTriggerType.Daily:
						schedDailyRadio.Checked = true;
						SetSchedTrigger();
						dailyRecurNumUpDn.Value = ((DailyTrigger)trigger).DaysInterval;
						delayCheckBox.Checked = ((DailyTrigger)trigger).RandomDelay != TimeSpan.Zero;
						delaySpan.Value = ((DailyTrigger)trigger).RandomDelay;
						break;
					case TaskTriggerType.Weekly:
						schedWeeklyRadio.Checked = true;
						SetSchedTrigger();
						weeklyRecurNumUpDn.Value = ((WeeklyTrigger)trigger).WeeksInterval;
						weeklySunCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Sunday) != 0;
						weeklyMonCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Monday) != 0;
						weeklyTueCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Tuesday) != 0;
						weeklyWedCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Wednesday) != 0;
						weeklyThuCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Thursday) != 0;
						weeklyFriCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Friday) != 0;
						weeklySatCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Saturday) != 0;
						delayCheckBox.Checked = ((WeeklyTrigger)trigger).RandomDelay != TimeSpan.Zero;
						delaySpan.Value = ((WeeklyTrigger)trigger).RandomDelay;
						break;
					case TaskTriggerType.Monthly:
						schedMonthlyRadio.Checked = true;
						SetSchedTrigger();
						delayCheckBox.Checked = ((MonthlyTrigger)trigger).RandomDelay != TimeSpan.Zero;
						delaySpan.Value = ((MonthlyTrigger)trigger).RandomDelay;
						monthlyDaysRadio.Checked = true;
						monthlyDaysDropDown.CheckedFlagValue = 0L;
						foreach (int i in ((MonthlyTrigger)trigger).DaysOfMonth)
							monthlyDaysDropDown.SetItemChecked(i - 1, true);
						monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyTrigger)trigger).MonthsOfYear;
						monthlyDaysDropDown.SetItemChecked(31, ((MonthlyTrigger)trigger).RunOnLastDayOfMonth);
						break;
					case TaskTriggerType.MonthlyDOW:
						schedMonthlyRadio.Checked = true;
						SetSchedTrigger();
						delayCheckBox.Checked = ((MonthlyDOWTrigger)trigger).RandomDelay != TimeSpan.Zero;
						delaySpan.Value = ((MonthlyDOWTrigger)trigger).RandomDelay;
						monthlyOnRadio.Checked = true;
						monthlyOnDOWDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).DaysOfWeek;
						monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).MonthsOfYear;
						monthlyOnWeekDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).WeeksOfMonth;
						monthlyOnWeekDropDown.SetItemChecked(4, ((MonthlyDOWTrigger)trigger).RunOnLastWeekOfMonth);
						break;
					case TaskTriggerType.Logon:
						triggerTypeCombo.SelectedIndex = 1;
						delayCheckBox.Checked = ((LogonTrigger)trigger).Delay != TimeSpan.Zero;
						delaySpan.Value = ((LogonTrigger)trigger).Delay;
						break;
					case TaskTriggerType.Boot:
						triggerTypeCombo.SelectedIndex = 2;
						delayCheckBox.Checked = ((BootTrigger)trigger).Delay != TimeSpan.Zero;
						delaySpan.Value = ((BootTrigger)trigger).Delay;
						break;
					case TaskTriggerType.Idle:
						triggerTypeCombo.SelectedIndex = 3;
						delayCheckBox.Checked = delayCheckBox.Enabled = false;
						delaySpan.Value = TimeSpan.Zero;
						break;
					case TaskTriggerType.Event:
						triggerTypeCombo.SelectedIndex = 4;
						delayCheckBox.Checked = ((EventTrigger)trigger).Delay != TimeSpan.Zero;
						delaySpan.Value = ((EventTrigger)trigger).Delay;
						break;
					case TaskTriggerType.Registration:
						triggerTypeCombo.SelectedIndex = 5;
						delayCheckBox.Checked = ((RegistrationTrigger)trigger).Delay != TimeSpan.Zero;
						delaySpan.Value = ((RegistrationTrigger)trigger).Delay;
						break;
					case TaskTriggerType.SessionStateChange:
						delayCheckBox.Checked = ((SessionStateChangeTrigger)trigger).Delay != TimeSpan.Zero;
						delaySpan.Value = ((SessionStateChangeTrigger)trigger).Delay;
						switch (((SessionStateChangeTrigger)trigger).StateChange)
						{
							case TaskSessionStateChangeType.ConsoleConnect:
							case TaskSessionStateChangeType.RemoteConnect:
								triggerTypeCombo.SelectedIndex = 6;
								break;
							case TaskSessionStateChangeType.ConsoleDisconnect:
							case TaskSessionStateChangeType.RemoteDisconnect:
								triggerTypeCombo.SelectedIndex = 7;
								break;
							case TaskSessionStateChangeType.SessionLock:
								triggerTypeCombo.SelectedIndex = 8;
								break;
							case TaskSessionStateChangeType.SessionUnlock:
								triggerTypeCombo.SelectedIndex = 9;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
				bool hasRep = trigger.Repetition.Interval != TimeSpan.Zero;
				repeatCheckBox.Checked = repeatSpan.Enabled = durationLabel.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = hasRep;
				if (!hasRep)
				{
					stopAfterDurationCheckBox.Checked = false;
					repeatSpan.Value = durationSpan.Value = TimeSpan.Zero;
				}
				else
				{
					repeatSpan.Value = trigger.Repetition.Interval;
					durationSpan.Value = trigger.Repetition.Duration;
					stopAfterDurationCheckBox.Checked = trigger.Repetition.StopAtDurationEnd;
				}
				stopIfRunsCheckBox.Checked = stopIfRunsSpan.Enabled = trigger.ExecutionTimeLimit != TimeSpan.Zero;
				stopIfRunsSpan.Value = trigger.ExecutionTimeLimit;
				activateCheckBox.Visible = activateCheckBox.Checked = activateDatePicker.Visible = triggerTypeCombo.SelectedIndex != 0;
				if (activateCheckBox.Visible)
				{
					activateCheckBox.Checked = activateDatePicker.Enabled = trigger.StartBoundary != DateTime.MinValue;
					if (activateCheckBox.Checked)
						activateDatePicker.Value = trigger.StartBoundary;
				}
				expireCheckBox.Checked = expireDatePicker.Enabled = trigger.EndBoundary != DateTime.MaxValue;
				if (expireCheckBox.Checked)
					expireDatePicker.Value = trigger.EndBoundary;
				enabledCheckBox.Checked = trigger.Enabled;
			}
		}

		private void UpdateTrigger()
		{
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			UpdateTrigger();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void triggerTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (triggerTypeCombo.SelectedIndex)
			{
				case 0:
				default:
					settingsTabControl.SelectedTab = scheduleTab;
					break;
				case 1:
					logonRemotePanel.Visible = false;
					settingsTabControl.SelectedTab = logonTab;
					break;
				case 2:
					settingsTabControl.SelectedTab = startupTab;
					break;
				case 3:
					settingsTabControl.SelectedTab = idleTab;
					break;
				case 4:
					settingsTabControl.SelectedTab = onEventTab;
					break;
				case 5:
					settingsTabControl.SelectedTab = startupTab;
					break;
				case 6:
				case 7:
					logonRemotePanel.Visible = true;
					settingsTabControl.SelectedTab = logonTab;
					break;
				case 8:
				case 9:
					logonRemotePanel.Visible = false;
					settingsTabControl.SelectedTab = logonTab;
					break;
			}
		}

		private void schedOneRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				if (sender == schedOneRadio)
					schedTabControl.SelectedTab = oneTimeTab;
				else if (sender == schedDailyRadio)
					schedTabControl.SelectedTab = dailyTab;
				else if (sender == schedWeeklyRadio)
					schedTabControl.SelectedTab = weeklyTab;
				else if (sender == schedMonthlyRadio)
					schedTabControl.SelectedTab = monthlyTab;
			}
		}

		private void monthlyDaysRadio_CheckedChanged(object sender, EventArgs e)
		{
			bool days = monthlyDaysRadio.Checked;
			monthlyDaysDropDown.Enabled = days;
			monthlyOnDOWDropDown.Enabled = monthlyOnWeekDropDown.Enabled = !days;
		}
	}
}
