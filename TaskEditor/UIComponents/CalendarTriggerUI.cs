using Microsoft.Win32.TaskScheduler.EditorProperties;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using Vanara.Extensions;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	/// <summary>User interface for setting properties on a time/calendar based trigger.</summary>
	[DefaultEvent("TriggerTypeChanged")]
	public partial class CalendarTriggerUI : UserControl, ITriggerHandler
	{
		internal const AvailableTriggers calendarTriggers = AvailableTriggers.Time | AvailableTriggers.Daily | AvailableTriggers.Weekly | AvailableTriggers.Monthly | AvailableTriggers.MonthlyDOW;
		private AvailableTriggers availableTriggers = calendarTriggers;
		private bool isV2 = true;
		private bool onAssignment;
		private TimeTrigger timeTrigger;
		private bool useUnifiedSchedulingEngine;

		/// <summary>Initializes a new instance of the <see cref="CalendarTriggerUI"/> class.</summary>
		public CalendarTriggerUI()
		{
			InitializeComponent();
			ResetControls();
		}

		/// <summary>Occurs when the trigger StartBoundary has changed.</summary>
		[Category("Property Changed"), Description("Occurs when the trigger StartBoundary has changed.")]
		public event EventHandler StartBoundaryChanged;

		/// <summary>Occurs when the trigger type has changed.</summary>
		[Category("Property Changed"), Description("Occurs when the trigger type has changed.")]
		public event EventHandler TriggerTypeChanged;

		/// <summary>Gets or sets the available triggers.</summary>
		/// <value>The available triggers.</value>
		[DefaultValue(calendarTriggers), Category("Appearance")]
		public AvailableTriggers AvailableTriggers
		{
			get => availableTriggers;
			set
			{
				value &= calendarTriggers; // filter out unneeded values
				if (availableTriggers == value) return;
				availableTriggers = value;
				ResetControls();
			}
		}

		/// <summary>Gets or sets a value indicating whether this editor supports a V1 or V2 trigger.</summary>
		/// <value><c>true</c> if supports V2; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Indicates whether this editor supports a V1 or V2 trigger.")]
		public bool IsV2
		{
			get => isV2;
			set { isV2 = value; ResetControls(); }
		}

		/// <summary>
		/// Gets or sets the trigger that is being edited. A new trigger may be returned so you remove and re-add the resulting value to the
		/// <see cref="TriggerCollection"/>.
		/// </summary>
		/// <value>The trigger.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Trigger Trigger
		{
			get
			{
				switch (schedTabControl.SelectedTab.Name)
				{
					case "dailyTab":
						return dailyTriggerUI1.Trigger;

					case "weeklyTab":
						return weeklyTriggerUI1.Trigger;

					case "monthlyTab":
						return monthlyTriggerUI1.Trigger;

					case "oneTimeTab":
					default:
						return timeTrigger;
				}
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				onAssignment = true;
				schedStartDatePicker.Value = value.StartBoundary == DateTime.MinValue ? DateTime.Now : value.StartBoundary;
				switch (value.TriggerType)
				{
					case TaskTriggerType.Daily:
						if (!availableTriggers.IsFlagSet(AvailableTriggers.Daily)) throw new ArgumentException("Type of Trigger is not permitted.", nameof(Trigger));
						dailyTriggerUI1.Trigger = value;
						schedDailyRadio.Checked = true;
						break;

					case TaskTriggerType.Weekly:
						if (!availableTriggers.IsFlagSet(AvailableTriggers.Weekly)) throw new ArgumentException("Type of Trigger is not permitted.", nameof(Trigger));
						weeklyTriggerUI1.Trigger = value;
						schedWeeklyRadio.Checked = true;
						break;

					case TaskTriggerType.Monthly:
					case TaskTriggerType.MonthlyDOW:
						if (value.TriggerType == TaskTriggerType.Monthly && !availableTriggers.IsFlagSet(AvailableTriggers.Monthly)) throw new ArgumentException("Type of Trigger is not permitted.", nameof(Trigger));
						if (value.TriggerType == TaskTriggerType.MonthlyDOW && !availableTriggers.IsFlagSet(AvailableTriggers.MonthlyDOW)) throw new ArgumentException("Type of Trigger is not permitted.", nameof(Trigger));
						monthlyTriggerUI1.Trigger = value;
						schedMonthlyRadio.Checked = true;
						break;

					case TaskTriggerType.Time:
					default:
						if (!availableTriggers.IsFlagSet(AvailableTriggers.Time)) throw new ArgumentException("Type of Trigger is not permitted.", nameof(Trigger));
						timeTrigger = value as TimeTrigger;
						schedOneRadio.Checked = true;
						break;
				}
				onAssignment = false;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether dialog should restrict items to those available when using the Unified Scheduling Engine.
		/// </summary>
		/// <value><c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether dialog should restrict items to those available when using the Unified Scheduling Engine.")]
		public bool UseUnifiedSchedulingEngine
		{
			get => useUnifiedSchedulingEngine;
			set
			{
				if (!isV2 && value)
					throw new NotSupportedException("Version 1.0 of the Task Scheduler library cannot use the Unified Scheduling Engine.");
				if (value != useUnifiedSchedulingEngine)
				{
					useUnifiedSchedulingEngine = value;
					ResetControls();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether to show the start boundary.</summary>
		/// <value><c>true</c> to display the start boundary control; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Indicates whether to show the start boundary")]
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		bool ITriggerHandler.ShowStartBoundary
		{
			get => dailyTriggerUI1.ShowStartBoundary;
			set => dailyTriggerUI1.ShowStartBoundary = monthlyTriggerUI1.ShowStartBoundary = weeklyTriggerUI1.ShowStartBoundary = value;
		}

		/// <summary>Determines whether trigger is valid. This method always returns <c>true</c>.</summary>
		/// <returns><c>true</c></returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		bool ITriggerHandler.IsTriggerValid() => true;

		/// <summary>Called when the trigger StartBoundary has changed.</summary>
		protected void OnStartBoundaryChanged()
		{
			StartBoundaryChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>Called when the trigger type has changed.</summary>
		protected void OnTriggerTypeChanged() => TriggerTypeChanged?.Invoke(this, EventArgs.Empty);

		private void monthlyTriggerUI1_TriggerTypeChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				Trigger newT;
				if (monthlyTriggerUI1.TriggerType == TaskTriggerType.Monthly)
					(newT = new MonthlyTrigger()).CopyProperties(Trigger);
				else
					(newT = new MonthlyDOWTrigger()).CopyProperties(Trigger);
				if (Trigger == null)
					newT.StartBoundary = schedStartDatePicker.Value;
				monthlyTriggerUI1.Trigger = newT;
				schedTabControl.SelectedTab = monthlyTab;
				OnStartBoundaryChanged();
				OnTriggerTypeChanged();
			}
			else
				schedTabControl.SelectedTab = monthlyTab;
		}

		private void ResetControls()
		{
			// Enable/disable version specific features
			dailyTriggerUI1.IsV2 = weeklyTriggerUI1.IsV2 = monthlyTriggerUI1.IsV2 = isV2;

			// Set date/time controls
			schedStartDatePicker.UTCPrompt = isV2 ? Resources.DateTimeSyncText : null;
			schedStartDatePicker.TimeFormat = (isV2) ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.ShortTime;

			// Disable items
			schedOneRadio.Enabled = availableTriggers.IsFlagSet(AvailableTriggers.Time);
			schedDailyRadio.Enabled = availableTriggers.IsFlagSet(AvailableTriggers.Daily);
			schedWeeklyRadio.Enabled = availableTriggers.IsFlagSet(AvailableTriggers.Weekly);
			schedMonthlyRadio.Enabled = !useUnifiedSchedulingEngine && (availableTriggers.IsFlagSet(AvailableTriggers.Monthly) || availableTriggers.IsFlagSet(AvailableTriggers.MonthlyDOW));

			monthlyTriggerUI1.AvailableTriggers = AvailableTriggers;
		}

		private void schedOneRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				var nullTrigger = Trigger == null;
				if (sender == schedOneRadio)
				{
					if (!onAssignment)
						(timeTrigger = new TimeTrigger()).CopyProperties(Trigger);
					schedTabControl.SelectedTab = oneTimeTab;
				}
				else if (sender == schedDailyRadio)
				{
					if (!onAssignment)
						(dailyTriggerUI1.Trigger = new DailyTrigger()).CopyProperties(Trigger);
					schedTabControl.SelectedTab = dailyTab;
				}
				else if (sender == schedWeeklyRadio)
				{
					if (!onAssignment)
						(weeklyTriggerUI1.Trigger = new WeeklyTrigger()).CopyProperties(Trigger);
					schedTabControl.SelectedTab = weeklyTab;
				}
				else if (sender == schedMonthlyRadio)
				{
					monthlyTriggerUI1_TriggerTypeChanged(this, EventArgs.Empty);
				}
				if (nullTrigger && Trigger != null)
				{
					Trigger.StartBoundary = schedStartDatePicker.Value;
					OnStartBoundaryChanged();
				}
				if (!onAssignment && sender != schedMonthlyRadio)
					OnTriggerTypeChanged();
			}
		}

		private void schedStartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment && Trigger != null)
			{
				Trigger.StartBoundary = schedStartDatePicker.Value;
				OnStartBoundaryChanged();
			}
		}
	}
}