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
			get => base.Trigger;
			set
			{
				if (!(value is WeeklyTrigger wt)) throw new ArgumentException("Trigger must of type WeeklyTrigger.");
				base.Trigger = value;
				weeklyRecurNumUpDn.Value = wt.WeeksInterval;
				weeklySunCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Sunday);
				weeklyMonCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Monday);
				weeklyTueCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Tuesday);
				weeklyWedCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Wednesday);
				weeklyThuCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Thursday);
				weeklyFriCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Friday);
				weeklySatCheck.Checked = wt.DaysOfWeek.IsFlagSet(DaysOfTheWeek.Saturday);
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

		private void weeklyRecurNumUpDn_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar.ToString() == System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NegativeSign)
				e.Handled = true;
		}
	}
}
