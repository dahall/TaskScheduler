using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class WeeklyTriggerUI : BaseTriggerUI
	{
		public WeeklyTriggerUI()
		{
			InitializeComponent();
		}

		public override Trigger Trigger
		{
			get { return base.Trigger; }
			set
			{
				base.Trigger = value;
				weeklyRecurNumUpDn.Value = ((WeeklyTrigger)trigger).WeeksInterval;
				weeklySunCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Sunday) != 0;
				weeklyMonCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Monday) != 0;
				weeklyTueCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Tuesday) != 0;
				weeklyWedCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Wednesday) != 0;
				weeklyThuCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Thursday) != 0;
				weeklyFriCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Friday) != 0;
				weeklySatCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Saturday) != 0;
				onAssignment = false;
			}
		}

		private void SetWeeklyDay(CheckBox cb, DaysOfTheWeek dow)
		{
			if (!onAssignment && cb != null)
			{
				var weeklyTrigger = (WeeklyTrigger)trigger;

				if (cb.Checked)
					weeklyTrigger.DaysOfWeek |= dow;
				else
				{
					// Ensure that ONE day is always checked.
					if (weeklyTrigger.DaysOfWeek == dow)
						cb.Checked = true;
					else
						weeklyTrigger.DaysOfWeek &= ~dow;
				}
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

		private void weeklyRecurNumUpDn_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((WeeklyTrigger)trigger).WeeksInterval = Convert.ToInt16(weeklyRecurNumUpDn.Value);
		}
	}
}
