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
    internal partial class TriggerEditDialog : Form
    {
		private bool isV2;
        private bool onAssignment = false;
        private TaskDefinition td;
        private Trigger trigger;
        private List<DropDownCheckListItem> triggerComboItems = new List<DropDownCheckListItem>(12);

        internal TriggerEditDialog(TaskDefinition taskDefinition, Trigger trigger)
        {
            InitializeComponent();

            // Populate combo boxes
            long allVal;
            ComboBoxExtension.InitializeFromEnum(triggerComboItems, typeof(TaskTriggerDisplayType), Properties.Resources.ResourceManager, "TriggerType", out allVal);

            triggerTypeCombo.DisplayMember = "Text";
            triggerTypeCombo.ValueMember = "Value";

            ConfigForVersion(taskDefinition.Settings.Compatibility);

            monthlyMonthsDropDown.InitializeFromEnum(typeof(MonthsOfTheYear), TaskPropertiesControl.taskSchedResources, "MOY");
            monthlyMonthsDropDown.Items.RemoveAt(13);
            monthlyDaysDropDown.InitializeFromRange(1, 31);
            monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(Properties.Resources.Last, 99));
            monthlyDaysDropDown.MultiColumnList = true;
            monthlyOnWeekDropDown.InitializeFromEnum(typeof(WhichWeek), TaskPropertiesControl.taskSchedResources, "WW");
            monthlyOnWeekDropDown.Items.RemoveAt(5);
            monthlyOnDOWDropDown.InitializeFromEnum(typeof(DaysOfTheWeek), TaskPropertiesControl.taskSchedResources, "DOW");
            monthlyOnDOWDropDown.Items.RemoveAt(8);

            delaySpan.Items.AddRange(new TimeSpan[] { TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(30), TimeSpan.FromHours(1), TimeSpan.FromHours(8), TimeSpan.FromDays(1) });
            repeatSpan.Items.AddRange(new TimeSpan[] { TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromHours(1) });
            durationSpan.Items.AddRange(new TimeSpan[] { TimeSpan.Zero, TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromHours(1), TimeSpan.FromHours(12), TimeSpan.FromDays(1) });
            durationSpan.FormattedZero = Properties.Resources.TimeSpanIndefinitely;
            stopIfRunsSpan.Items.AddRange(new TimeSpan[] { TimeSpan.FromMinutes(30), TimeSpan.FromHours(1), TimeSpan.FromHours(2), TimeSpan.FromHours(4), TimeSpan.FromHours(8), TimeSpan.FromHours(12), TimeSpan.FromDays(1), TimeSpan.FromDays(3) });

            td = taskDefinition;
            if (trigger != null)
                this.Trigger = trigger.Clone() as Trigger;
            else
                this.Trigger = new TimeTrigger();
        }

        /// <summary>Defines the type of triggers that can be used by tasks.</summary>
        public enum TaskTriggerDisplayType
        {
            /// <summary>Triggers the task on a schedule.</summary>
            Schedule = 1,
            /// <summary>Triggers the task when a specific user logs on.</summary>
            Logon = 19,
            /// <summary>Triggers the task when the computer boots.</summary>
            Boot = 28,
            /// <summary>Triggers the task when the computer goes into an idle state.</summary>
            Idle = 36,
            /// <summary>Triggers the task when a specific event occurs. Version 1.2 only.</summary>
            Event = 40,
            /// <summary>Triggers the task when the task is registered. Version 1.2 only.</summary>
            Registration = 57,
            /// <summary>Triggers the task on connection to a user session. Version 1.2 only.</summary>
            SessionConnect = 111,
            /// <summary>Triggers the task on connection to a user session. Version 1.2 only.</summary>
            SessionDisconnect = 112,
            /// <summary>Triggers the task on workstation lock. Version 1.2 only.</summary>
            WorkstationLock = 117,
            /// <summary>Triggers the task on workstation unlock. Version 1.2 only.</summary>
            WorkstationUnlock = 118,
        }

        public Trigger Trigger
        {
            get
            {
                return trigger;
            }
            set
            {
                onAssignment = true;
                trigger = value;
				isV2 = td.Settings.Compatibility == TaskCompatibility.V2;
                switch (trigger.TriggerType)
                {
                    case TaskTriggerType.Time:
                        schedOneRadio.Checked = true;
                        SetSchedTrigger();
                        break;
                    case TaskTriggerType.Daily:
                        schedDailyRadio.Checked = true;
                        SetSchedTrigger();
                        dailyRecurNumUpDn.Value = ((DailyTrigger)trigger).DaysInterval;
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
                        break;
                    case TaskTriggerType.Monthly:
                        schedMonthlyRadio.Checked = true;
                        SetSchedTrigger();
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
                        monthlyOnRadio.Checked = true;
                        monthlyOnDOWDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).DaysOfWeek;
                        monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).MonthsOfYear;
                        monthlyOnWeekDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).WeeksOfMonth;
                        monthlyOnWeekDropDown.SetItemChecked(4, ((MonthlyDOWTrigger)trigger).RunOnLastWeekOfMonth);
                        break;
                    case TaskTriggerType.Logon:
                        TriggerView = TaskTriggerDisplayType.Logon;
                        break;
                    case TaskTriggerType.Boot:
                        TriggerView = TaskTriggerDisplayType.Boot;
                        break;
                    case TaskTriggerType.Idle:
                        TriggerView = TaskTriggerDisplayType.Idle;
                        break;
                    case TaskTriggerType.Event:
                        TriggerView = TaskTriggerDisplayType.Event;
                        string log, source; int? id;
                        bool basic = ((EventTrigger)trigger).GetBasic(out log, out source, out id);
                        onEventCustomText.Text = ((EventTrigger)trigger).Subscription;
                        if (basic)
                        {
                            onEventLogCombo.Text = log;
                            onEventSourceCombo.Text = source;
                            onEventIdText.Text = id.HasValue ? id.Value.ToString() : string.Empty;
                        }
                        eventBasicRadio.Checked = basic;
                        eventCustomRadio.Checked = !basic;
                        break;
                    case TaskTriggerType.Registration:
                        TriggerView = TaskTriggerDisplayType.Registration;
                        break;
                    case TaskTriggerType.SessionStateChange:
                        int state = 110 + (int)((SessionStateChangeTrigger)trigger).StateChange;
                        if (state == 113 || state == 114) state -= 2;
                        TriggerView = (TaskTriggerDisplayType)state;
                        logonRemoteRadio.Checked = (((SessionStateChangeTrigger)trigger).StateChange == TaskSessionStateChangeType.RemoteConnect || ((SessionStateChangeTrigger)trigger).StateChange == TaskSessionStateChangeType.RemoteDisconnect);
                        logonLocalRadio.Checked = !logonRemoteRadio.Checked;
                        break;
                    default:
                        break;
                }
                if (trigger is ITriggerDelay && isV2)
                {
                    delayCheckBox.Checked = delaySpan.Enabled = ((ITriggerDelay)trigger).Delay != TimeSpan.Zero;
                    delaySpan.Value = ((ITriggerDelay)trigger).Delay;
                }
                else
                {
                    delayCheckBox.Checked = delayCheckBox.Enabled = delaySpan.Enabled = false;
                    delaySpan.Value = TimeSpan.Zero;
                }
                if (trigger is ITriggerUserId && isV2)
                {
                    logonUserLabel.Text = ((ITriggerUserId)trigger).UserId;
                    logonAnyUserRadio.Checked = (logonUserLabel.Text.Length == 0);
                    logonSpecUserRadio.Checked = (logonUserLabel.Text.Length > 0);
                }
                bool hasRep = trigger.Repetition.Interval != TimeSpan.Zero;
                repeatCheckBox.Checked = repeatSpan.Enabled = durationLabel.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = hasRep;
                if (!hasRep)
                {
                    stopAfterDurationCheckBox.Checked = false;
					durationSpan.Value = repeatSpan.Value = TimeSpan.Zero;
                }
                else
                {
					durationSpan.Value = trigger.Repetition.Duration;
					repeatSpan.Value = trigger.Repetition.Interval;
                    stopAfterDurationCheckBox.Checked = trigger.Repetition.StopAtDurationEnd;
                }
                if (isV2)
                {
                    stopIfRunsCheckBox.Enabled = stopIfRunsCheckBox.Checked = stopIfRunsSpan.Enabled = trigger.ExecutionTimeLimit != TimeSpan.Zero;
                    stopIfRunsSpan.Value = trigger.ExecutionTimeLimit;
                }
                if (activateCheckBox.Visible)
                {
                    activateCheckBox.Checked = activateDatePicker.Enabled = trigger.StartBoundary != DateTime.MinValue;
                    if (activateCheckBox.Checked)
                        activateDatePicker.Value = trigger.StartBoundary;
                }
                expireCheckBox.Checked = expireDatePicker.Enabled = trigger.EndBoundary != DateTime.MaxValue;
				expireDatePicker.Value = expireCheckBox.Checked ? trigger.EndBoundary : trigger.StartBoundary;
                enabledCheckBox.Checked = trigger.Enabled;
                onAssignment = false;
            }
        }

        private TaskTriggerDisplayType TriggerView
        {
            get
            {
                if (triggerTypeCombo.SelectedValue == null)
                    return TaskTriggerDisplayType.Schedule;
                return (TaskTriggerDisplayType)Convert.ToInt32(triggerTypeCombo.SelectedValue);
            }
            set
            {
                if (triggerTypeCombo.SelectedIndex == -1 || Convert.ToInt64(triggerTypeCombo.SelectedValue) != (long)value)
                    triggerTypeCombo.SelectedValue = (long)value;
            }
        }

        private void activateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            activateDatePicker.Enabled = activateCheckBox.Checked;
			if (!onAssignment)
			{
				if (expireCheckBox.Checked)
					trigger.StartBoundary = activateDatePicker.Value;
				else
					trigger.StartBoundary = DateTime.MaxValue;
			}
		}

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConfigForVersion(TaskCompatibility ver)
        {
            bool isV2 = ver == TaskCompatibility.V2;
            if (!isV2)
                triggerComboItems.RemoveRange(4, 6);
            triggerTypeCombo.DataSource = triggerComboItems;
            stopIfRunsCheckBox.Enabled = stopIfRunsSpan.Enabled = isV2;
            delayCheckBox.Enabled = delaySpan.Enabled = isV2;
			monthlyOnWeekDropDown.AllowOnlyOneCheckedItem = !isV2;
            // Disable logon trigger options
            foreach (Control c in logonTab.Controls)
                c.Enabled = isV2;
        }

        private void delayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            delaySpan.Enabled = delayCheckBox.Checked;
            delaySpan.Value = delayCheckBox.Checked ? TimeSpan.FromHours(1) : TimeSpan.Zero;
        }

        private void delaySpan_ValueChanged(object sender, EventArgs e)
        {
            if (trigger is ITriggerDelay && !onAssignment)
                ((ITriggerDelay)trigger).Delay = delaySpan.Value;
        }

        private void durationSpan_ValueChanged(object sender, EventArgs e)
        {
			if (!onAssignment)
			{
				trigger.Repetition.Duration = durationSpan.Value;
				if (!this.isV2 && trigger.Repetition.Duration <= trigger.Repetition.Interval)
				{
					onAssignment = true;
					repeatSpan.Value = trigger.Repetition.Duration - TimeSpan.FromMinutes(1);
					trigger.Repetition.Interval = repeatSpan.Value;
					onAssignment = false;
				}
			}
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            trigger.Enabled = enabledCheckBox.Checked;
        }

        private void eventBasicRadio_CheckedChanged(object sender, EventArgs e)
        {
            bool basic = eventBasicRadio.Checked;
            onEventBasicPanel.Visible = basic;
            onEventCustomText.Visible = !basic;
        }

        private void expireCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            expireDatePicker.Enabled = expireCheckBox.Checked;
			if (!onAssignment)
			{
				if (expireCheckBox.Checked)
					trigger.EndBoundary = expireDatePicker.Value;
				else
					trigger.EndBoundary = DateTime.MaxValue;
			}
        }

        private void logonAnyUserRadio_CheckedChanged(object sender, EventArgs e)
        {
            bool any = logonAnyUserRadio.Checked;
            logonChgUserBtn.Enabled = logonUserLabel.Enabled = !any;
            ((ITriggerUserId)trigger).UserId = any ? null : logonUserLabel.Text;
        }

        private void logonChgUserBtn_Click(object sender, EventArgs e)
        {
            string acct = String.Empty;
            bool isGroup = false, isService = false;
            if (!NativeMethods.AccountUtils.SelectAccount(this, null, ref acct, ref isGroup, ref isService))
                return;
            ((ITriggerUserId)trigger).UserId = logonUserLabel.Text = acct;
        }

        private void monthlyDaysRadio_CheckedChanged(object sender, EventArgs e)
        {
            bool days = monthlyDaysRadio.Checked;
            monthlyDaysDropDown.Enabled = days;
            monthlyOnDOWDropDown.Enabled = monthlyOnWeekDropDown.Enabled = !days;

            Trigger newTrigger = null;
            if (monthlyDaysRadio.Checked)
                newTrigger = new MonthlyTrigger();
            else if (monthlyOnRadio.Checked)
                newTrigger = new MonthlyDOWTrigger();

            if (newTrigger != null && !onAssignment)
            {
                if (trigger != null)
                    newTrigger.CopyProperties(trigger);
                this.Trigger = newTrigger;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void repeatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            repeatSpan.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = repeatCheckBox.Checked;
			if (repeatCheckBox.Checked)
			{
				durationSpan.Value = durationSpan.Items[durationSpan.Items.Count - 1];
				repeatSpan.Value = repeatSpan.Items[repeatSpan.Items.Count - 1];
			}
			else
			{
				trigger.Repetition.Duration = trigger.Repetition.Interval = TimeSpan.Zero;
			}
        }

        private void repeatSpan_ValueChanged(object sender, EventArgs e)
        {
			if (!onAssignment)
			{
				trigger.Repetition.Interval = repeatSpan.Value;
				if (!this.isV2 && trigger.Repetition.Duration <= trigger.Repetition.Interval)
				{
					onAssignment = true;
					durationSpan.Value = trigger.Repetition.Interval + TimeSpan.FromMinutes(1);
					trigger.Repetition.Duration = durationSpan.Value;
					onAssignment = false;
				}
			}
        }

        private void schedOneRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                Trigger newTrigger = this.trigger;
                if (sender == schedOneRadio)
                {
                    schedTabControl.SelectedTab = oneTimeTab;
                    if (!onAssignment) newTrigger = new TimeTrigger();
                }
                else if (sender == schedDailyRadio)
                {
                    schedTabControl.SelectedTab = dailyTab;
                    if (!onAssignment) newTrigger = new DailyTrigger();
                }
                else if (sender == schedWeeklyRadio)
                {
                    schedTabControl.SelectedTab = weeklyTab;
                    if (!onAssignment) newTrigger = new WeeklyTrigger();
                }
                else if (sender == schedMonthlyRadio)
                {
                    schedTabControl.SelectedTab = monthlyTab;
                    if (monthlyOnRadio.Checked)
                        monthlyDaysRadio_CheckedChanged(this, EventArgs.Empty);
                    else
                        monthlyDaysRadio.Checked = true;
                    if (!onAssignment) return;
                }

                if (trigger != null && !onAssignment)
                {
                    // Copy base trigger information over
                    newTrigger.CopyProperties(trigger);
                    this.Trigger = newTrigger;
                }
            }
        }

        private void SetSchedTrigger()
        {
            TriggerView = TaskTriggerDisplayType.Schedule;
            schedStartDatePicker.Value = trigger.StartBoundary;
            activateCheckBox.Visible = activateDatePicker.Visible = false;
        }

        private void stopAfterDurationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            trigger.Repetition.StopAtDurationEnd = stopAfterDurationCheckBox.Checked;
        }

        private void stopIfRunsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            stopIfRunsSpan.Enabled = stopIfRunsCheckBox.Checked;
            stopIfRunsSpan.Value = stopIfRunsCheckBox.Checked ? TimeSpan.FromDays(3) : TimeSpan.Zero;
        }

        private void stopIfRunsSpan_ValueChanged(object sender, EventArgs e)
        {
            if (!onAssignment)
                trigger.ExecutionTimeLimit = stopIfRunsSpan.Value;
        }

        private void triggerTypeCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (triggerTypeCombo.SelectedValue == null)
                return;

            Trigger newTrigger = this.trigger;
            switch (TriggerView)
            {
                case TaskTriggerDisplayType.Schedule:
                default:
                    settingsTabControl.SelectedTab = scheduleTab;
                    if (!onAssignment)
                    {
                        schedOneRadio.Checked = schedMonthlyRadio.Checked = schedDailyRadio.Checked = schedWeeklyRadio.Checked = false;
                        schedOneRadio.Checked = true;
                        return;
                    }
                    break;
                case TaskTriggerDisplayType.Logon:
                    logonRemotePanel.Visible = false;
                    settingsTabControl.SelectedTab = logonTab;
                    if (!onAssignment) newTrigger = new LogonTrigger();
                    break;
                case TaskTriggerDisplayType.Boot:
                    settingsTabControl.SelectedTab = startupTab;
                    if (!onAssignment) newTrigger = new BootTrigger();
                    break;
                case TaskTriggerDisplayType.Idle:
                    settingsTabControl.SelectedTab = idleTab;
                    if (!onAssignment) newTrigger = new IdleTrigger();
                    break;
                case TaskTriggerDisplayType.Event:
                    settingsTabControl.SelectedTab = onEventTab;
                    if (!onAssignment) newTrigger = new EventTrigger();
                    break;
                case TaskTriggerDisplayType.Registration:
                    settingsTabControl.SelectedTab = startupTab;
                    if (!onAssignment) newTrigger = new RegistrationTrigger();
                    break;
                case TaskTriggerDisplayType.SessionConnect:
                case TaskTriggerDisplayType.SessionDisconnect:
                case TaskTriggerDisplayType.WorkstationLock:
                case TaskTriggerDisplayType.WorkstationUnlock:
                    logonRemotePanel.Visible = (int)TriggerView < (int)TaskTriggerDisplayType.WorkstationLock;
                    settingsTabControl.SelectedTab = logonTab;
                    if (!onAssignment)
                        newTrigger = new SessionStateChangeTrigger() { StateChange = (TaskSessionStateChangeType)(TriggerView - 110) };
                    break;
            }

            if (trigger != null && !onAssignment)
            {
                // Copy base trigger information over
                newTrigger.CopyProperties(trigger);
                this.Trigger = newTrigger;
            }
        }

		private void schedStartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.StartBoundary = schedStartDatePicker.Value;
		}

		private void expireDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.EndBoundary = expireDatePicker.Value;
		}

		private void activateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.StartBoundary = activateDatePicker.Value;
		}

		private void monthlyMonthsDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (monthlyDaysRadio.Checked)
					((MonthlyTrigger)trigger).MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue;
				else
					((MonthlyDOWTrigger)trigger).MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue;
			}
		}

		private void monthlyDaysDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				int[] days = new int[monthlyDaysDropDown.SelectedItems.Length];
				for (int i = 0; i < monthlyDaysDropDown.SelectedItems.Length; i++)
					days[i] = (int)monthlyDaysDropDown.SelectedItems[i].Value;
				((MonthlyTrigger)trigger).DaysOfMonth = days;
			}
		}

		private void monthlyOnWeekDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((MonthlyDOWTrigger)trigger).WeeksOfMonth = (WhichWeek)monthlyOnWeekDropDown.CheckedFlagValue;
		}

		private void monthlyOnDOWDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((MonthlyDOWTrigger)trigger).DaysOfWeek = (DaysOfTheWeek)monthlyOnDOWDropDown.CheckedFlagValue;
		}

		private void weeklyRecurNumUpDn_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((WeeklyTrigger)trigger).WeeksInterval = Convert.ToInt16(weeklyRecurNumUpDn.Value);
		}

		private void dailyRecurNumUpDn_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((DailyTrigger)trigger).DaysInterval = Convert.ToInt16(dailyRecurNumUpDn.Value);
		}

		private void SetWeeklyDay(CheckBox cb, DaysOfTheWeek dow)
		{
			if (!onAssignment && cb != null)
			{
				if (cb.Checked)
					((WeeklyTrigger)trigger).DaysOfWeek |= dow;
				else
					((WeeklyTrigger)trigger).DaysOfWeek &= ~dow;
			}
		}

		private void weeklySunCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Sunday);
		}

		private void weeklyMonCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Monday);
		}

		private void weeklyTueCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Tuesday);
		}

		private void weeklyWedCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Wednesday);
		}

		private void weeklyThuCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Thursday);
		}

		private void weeklyFriCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Friday);
		}

		private void weeklySatCheck_CheckedChanged(object sender, EventArgs e)
		{
			SetWeeklyDay(sender as CheckBox, DaysOfTheWeek.Saturday);
		}
    }
}