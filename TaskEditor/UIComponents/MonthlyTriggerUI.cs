using System;
using System.ComponentModel;
using Vanara.Extensions;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[DefaultEvent("TriggerTypeChanged")]
	internal partial class MonthlyTriggerUI : BaseTriggerUI
	{
		private const AvailableTriggers monthlyTriggers = AvailableTriggers.MonthlyDOW | AvailableTriggers.Monthly;

		public MonthlyTriggerUI()
		{
			InitializeComponent();

			monthlyMonthsDropDown.InitializeFromTaskEnum(typeof(MonthsOfTheYear));
			monthlyMonthsDropDown.Items.RemoveAt(13);
			monthlyDaysDropDown.InitializeFromRange(1, 31);
			monthlyDaysDropDown.Items.Add(new ListControlItem(EditorProperties.Resources.Last, 99));
			monthlyDaysDropDown.MultiColumnList = true;
			monthlyOnWeekDropDown.InitializeFromTaskEnum(typeof(WhichWeek));
			monthlyOnWeekDropDown.Items.RemoveAt(5);
			InitDOWDropDown();
		}

		[Category("Property Changed")]
		public event EventHandler TriggerTypeChanged;

		/// <summary>Gets or sets the available triggers.</summary>
		/// <value>The available triggers.</value>
		[DefaultValue(monthlyTriggers), Category("Appearance")]
		public AvailableTriggers AvailableTriggers
		{
			get => (monthlyDaysRadio.Enabled ? AvailableTriggers.Monthly : 0) | (monthlyOnRadio.Enabled ? AvailableTriggers.MonthlyDOW : 0);
			set
			{
				value &= monthlyTriggers; // filter out unneeded values
				if (AvailableTriggers == value) return;
				monthlyDaysRadio.Enabled = value.IsFlagSet(AvailableTriggers.Monthly);
				monthlyOnRadio.Enabled = value.IsFlagSet(AvailableTriggers.MonthlyDOW);
				if (!monthlyDaysRadio.Enabled && monthlyDaysRadio.Checked)
					monthlyOnRadio.Checked = true;
				else if (!monthlyOnRadio.Enabled && monthlyOnRadio.Checked)
					monthlyDaysRadio.Checked = true;
			}
		}

		public override bool IsV2
		{
			get => !monthlyOnWeekDropDown.AllowOnlyOneCheckedItem;
			set
			{
				monthlyOnWeekDropDown.AllowOnlyOneCheckedItem = !value;
				if (!value && monthlyDaysDropDown.Items.Count == 31)
					monthlyDaysDropDown.Items.RemoveAt(31);
				else if (value && monthlyDaysDropDown.Items.Count <= 31)
					monthlyDaysDropDown.Items.Add(new ListControlItem(EditorProperties.Resources.Last, 99));
			}
		}

		public override Trigger Trigger
		{
			get => base.Trigger;
			set
			{
				base.Trigger = value;
				switch (trigger)
				{
					case MonthlyTrigger mt:
						monthlyDaysRadio.Checked = true;
						monthlyDaysDropDown.CheckedFlagValue = 0L;
						foreach (var i in mt.DaysOfMonth)
							monthlyDaysDropDown.SetItemChecked(i - 1, true);
						monthlyMonthsDropDown.CheckedFlagValue = (long)mt.MonthsOfYear;
						if (IsV2)
							monthlyDaysDropDown.SetItemChecked(31, mt.RunOnLastDayOfMonth);
						break;

					case MonthlyDOWTrigger dowt:
						monthlyOnRadio.Checked = true;
						monthlyOnDOWDropDown.CheckedFlagValue = (long)dowt.DaysOfWeek;
						monthlyMonthsDropDown.CheckedFlagValue = (long)dowt.MonthsOfYear;
						monthlyOnWeekDropDown.CheckedFlagValue = (long)dowt.WeeksOfMonth;
						monthlyOnWeekDropDown.SetItemChecked(4, dowt.RunOnLastWeekOfMonth);
						break;

					default:
						throw new ArgumentException("Trigger must be MonthlyDOWTrigger or MonthlyTrigger.", nameof(Trigger));
				}
				monthlyDaysRadio_CheckedChanged(this, EventArgs.Empty);
				onAssignment = false;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public TaskTriggerType TriggerType => monthlyDaysRadio.Checked ? TaskTriggerType.Monthly : TaskTriggerType.MonthlyDOW;

		protected void OnTriggerTypeChanged() => TriggerTypeChanged?.Invoke(this, new EventArgs());

		private void InitDOWDropDown()
		{
			monthlyOnDOWDropDown.BeginUpdate();
			monthlyOnDOWDropDown.Items.Clear();
			long allVal = 0;
			for (var i = 0; i < 7; i++)
			{
				var val = (long)1 << i;
				allVal |= val;
				var text = System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.GetDayName((DayOfWeek)i);
				monthlyOnDOWDropDown.Items.Add(new ListControlItem(text, val));
			}
			if (!string.IsNullOrEmpty(monthlyOnDOWDropDown.CheckAllText))
				monthlyOnDOWDropDown.Items.Insert(0, new ListControlItem(monthlyOnDOWDropDown.CheckAllText, allVal));
			monthlyOnDOWDropDown.EndUpdate();
		}

		private void monthlyDaysDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (onAssignment) return;
			var days = new System.Collections.Generic.List<int>(31);
			var runOnLastDay = false;
			foreach (var t in monthlyDaysDropDown.SelectedItems)
			{
				if ((int)t.Value == 99)
					runOnLastDay = true;
				else
					days.Add((int)t.Value);
			}
			((MonthlyTrigger)trigger).DaysOfMonth = days.ToArray();
			if (IsV2) ((MonthlyTrigger)trigger).RunOnLastDayOfMonth = runOnLastDay;
		}

		private void monthlyDaysRadio_CheckedChanged(object sender, EventArgs e)
		{
			var days = monthlyDaysRadio.Checked;
			monthlyDaysDropDown.Enabled = days;
			monthlyOnDOWDropDown.Enabled = monthlyOnWeekDropDown.Enabled = !days;
			if (!onAssignment)
				OnTriggerTypeChanged();
		}

		private void monthlyMonthsDropDown_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (onAssignment) return;
			if (monthlyDaysRadio.Checked)
				((MonthlyTrigger)trigger).MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue;
			else
				((MonthlyDOWTrigger)trigger).MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue;
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