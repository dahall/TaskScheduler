using System;
using System.ComponentModel;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[DefaultEvent("TriggerTypeChanged")]
	internal partial class MonthlyTriggerUI : BaseTriggerUI
	{
		public MonthlyTriggerUI()
		{
			InitializeComponent();

			monthlyMonthsDropDown.InitializeFromTaskEnum(typeof(MonthsOfTheYear));
			monthlyMonthsDropDown.Items.RemoveAt(13);
			monthlyDaysDropDown.InitializeFromRange(1, 31);
			monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(EditorProperties.Resources.Last, 99));
			monthlyDaysDropDown.MultiColumnList = true;
			monthlyOnWeekDropDown.InitializeFromTaskEnum(typeof(WhichWeek));
			monthlyOnWeekDropDown.Items.RemoveAt(5);
			monthlyOnDOWDropDown.InitializeFromTaskEnum(typeof(DaysOfTheWeek));
			monthlyOnDOWDropDown.Items.RemoveAt(8);
		}

		[Category("Property Changed")]
		public event EventHandler TriggerTypeChanged;

		protected void OnTriggerTypeChanged()
		{
			EventHandler temp = TriggerTypeChanged;
			if (temp != null)
				temp(this, new EventArgs());
		}

		public override Trigger Trigger
		{
			get { return base.Trigger; }
			set
			{
				base.Trigger = value;
				if (trigger is MonthlyTrigger)
				{
					monthlyDaysRadio.Checked = true;
					monthlyDaysDropDown.CheckedFlagValue = 0L;
					foreach (int i in ((MonthlyTrigger)trigger).DaysOfMonth)
						monthlyDaysDropDown.SetItemChecked(i - 1, true);
					monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyTrigger)trigger).MonthsOfYear;
					if (IsV2)
						monthlyDaysDropDown.SetItemChecked(31, ((MonthlyTrigger)trigger).RunOnLastDayOfMonth);
				}
				else if (trigger is MonthlyDOWTrigger)
				{
					monthlyOnRadio.Checked = true;
					monthlyOnDOWDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).DaysOfWeek;
					monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).MonthsOfYear;
					monthlyOnWeekDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).WeeksOfMonth;
					monthlyOnWeekDropDown.SetItemChecked(4, ((MonthlyDOWTrigger)trigger).RunOnLastWeekOfMonth);
				}
				else
					throw new ArgumentException("Trigger must be MonthlyDOWTrigger or MonthlyTrigger.", "Trigger");

				monthlyDaysRadio_CheckedChanged(this, EventArgs.Empty);
				onAssignment = false;
			}
		}

		public override bool IsV2
		{
			get { return !monthlyOnWeekDropDown.AllowOnlyOneCheckedItem; }
			set
			{
				monthlyOnWeekDropDown.AllowOnlyOneCheckedItem = !value;
				if (!value)
					monthlyDaysDropDown.Items.RemoveAt(31);
				else if (monthlyDaysDropDown.Items.Count <= 31)
					monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(EditorProperties.Resources.Last, 99));
			}
		}

		[Browsable(false), DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public TaskTriggerType TriggerType
		{
			get { return monthlyDaysRadio.Checked ? TaskTriggerType.Monthly : TaskTriggerType.MonthlyDOW; }
		}

		private void monthlyDaysDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				var days = new System.Collections.Generic.List<int>(31);
				for (int i = 0; i < monthlyDaysDropDown.SelectedItems.Length; i++)
				{
					if ((int)monthlyDaysDropDown.SelectedItems[i].Value == 99)
					{
						if (IsV2) ((MonthlyTrigger)trigger).RunOnLastDayOfMonth = true;
					}
					else
						days.Add((int)monthlyDaysDropDown.SelectedItems[i].Value);
				}
				((MonthlyTrigger)trigger).DaysOfMonth = days.ToArray();
			}
		}

		private void monthlyDaysRadio_CheckedChanged(object sender, EventArgs e)
		{
			bool days = monthlyDaysRadio.Checked;
			monthlyDaysDropDown.Enabled = days;
			monthlyOnDOWDropDown.Enabled = monthlyOnWeekDropDown.Enabled = !days;
			if (!onAssignment)
				OnTriggerTypeChanged();
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

		private void monthlyOnDOWDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((MonthlyDOWTrigger)trigger).DaysOfWeek = (DaysOfTheWeek)monthlyOnDOWDropDown.CheckedFlagValue;
		}

		private void monthlyOnWeekDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				((MonthlyDOWTrigger)trigger).WeeksOfMonth = (WhichWeek)monthlyOnWeekDropDown.CheckedFlagValue;
		}
	}
}