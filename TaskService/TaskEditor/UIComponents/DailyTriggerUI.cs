using System;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class DailyTriggerUI : BaseTriggerUI
	{
		public DailyTriggerUI()
		{
			InitializeComponent();
		}

		public override Trigger Trigger
		{
			get { return base.Trigger; }
			set
			{
				base.Trigger = value;
				dailyRecurNumUpDn.Value = ((DailyTrigger)trigger).DaysInterval;
				onAssignment = false;
			}
		}

		private void dailyRecurNumUpDn_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((DailyTrigger)trigger).DaysInterval = Convert.ToInt16(dailyRecurNumUpDn.Value);
		}
	}
}
