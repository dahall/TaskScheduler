using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	/// <summary>
	/// User interface for setting properties on a time/calendar based trigger.
	/// </summary>
	[DefaultEvent("TriggerTypeChanged")]
	public partial class CalendarTriggerUI : UserControl, ITriggerHandler
	{
		private bool isV2 = true;
		private bool onAssignment = false;
		private bool useUnifiedSchedulingEngine = false;
		private TimeTrigger timeTrigger;

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarTriggerUI"/> class.
		/// </summary>
		public CalendarTriggerUI()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Occurs when the trigger type has changed.
		/// </summary>
		[Category("Property Changed"), Description("Occurs when the trigger type has changed.")]
		public event EventHandler TriggerTypeChanged;

		/// <summary>
		/// Gets or sets a value indicating whether this editor supports a V1 or V2 trigger.
		/// </summary>
		/// <value><c>true</c> if supports V2; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Indicates whether this editor supports a V1 or V2 trigger.")]
		public bool IsV2
		{
			get { return isV2; }
			set { isV2 = value; ResetControls(); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the start boundary.
		/// </summary>
		/// <value>
		///   <c>true</c> to display the start boundary control; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Appearance"), Description("Indicates whether to show the start boundary")]
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		bool ITriggerHandler.ShowStartBoundary
		{
			get { return this.dailyTriggerUI1.ShowStartBoundary; }
			set { this.dailyTriggerUI1.ShowStartBoundary = this.monthlyTriggerUI1.ShowStartBoundary = this.weeklyTriggerUI1.ShowStartBoundary = value; }
		}

		/// <summary>
		/// Gets or sets the trigger that is being edited. A new trigger may be returned so you remove and re-add the resulting value to the <see cref="TriggerCollection"/>.
		/// </summary>
		/// <value>The trigger.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Trigger Trigger
		{
			get
			{
				switch (this.schedTabControl.SelectedTab.Name)
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
				schedStartDatePicker.Value = value.StartBoundary;
				switch (value.TriggerType)
				{
					case TaskTriggerType.Daily:
						dailyTriggerUI1.Trigger = value;
						schedDailyRadio.Checked = true;
						break;
					case TaskTriggerType.Weekly:
						weeklyTriggerUI1.Trigger = value;
						schedWeeklyRadio.Checked = true;
						break;
					case TaskTriggerType.Monthly:
					case TaskTriggerType.MonthlyDOW:
						monthlyTriggerUI1.Trigger = value;
						schedMonthlyRadio.Checked = true;
						break;
					case TaskTriggerType.Time:
					default:
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
		/// <value>
		/// 	<c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether dialog should restrict items to those available when using the Unified Scheduling Engine.")]
		public bool UseUnifiedSchedulingEngine
		{
			get { return useUnifiedSchedulingEngine; }
			set
			{
				if (!isV2 && value)
					throw new NotSupportedException("Version 1.0 of the Task Scheduler library cannot use the Unified Scheduling Engine.");
				if (value != useUnifiedSchedulingEngine)
				{
					useUnifiedSchedulingEngine = value;
					schedMonthlyRadio.Enabled = !useUnifiedSchedulingEngine;
				}
			}
		}

		/// <summary>
		/// Determines whether trigger is valid. This method always returns <c>true</c>.
		/// </summary>
		/// <returns><c>true</c></returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		bool ITriggerHandler.IsTriggerValid()
		{
			return true;
		}

		/// <summary>
		/// Called when the trigger type has changed.
		/// </summary>
		protected void OnTriggerTypeChanged()
		{
			var h = this.TriggerTypeChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

		private void monthlyTriggerUI1_TriggerTypeChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				Trigger newT;
				if (monthlyTriggerUI1.TriggerType == TaskTriggerType.Monthly)
					(newT = new MonthlyTrigger()).CopyProperties(this.Trigger);
				else
					(newT = new MonthlyDOWTrigger()).CopyProperties(this.Trigger);
				monthlyTriggerUI1.Trigger = newT;
				schedTabControl.SelectedTab = monthlyTab;
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
			schedStartDatePicker.UTCPrompt = isV2 ? EditorProperties.Resources.DateTimeSyncText : null;
			schedStartDatePicker.TimeFormat = (isV2) ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.ShortTime;
		}

		private void schedOneRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				if (sender == schedOneRadio)
				{
					if (!onAssignment)
						(timeTrigger = new TimeTrigger()).CopyProperties(this.Trigger);
					schedTabControl.SelectedTab = oneTimeTab;
				}
				else if (sender == schedDailyRadio)
				{
					if (!onAssignment)
						(dailyTriggerUI1.Trigger = new DailyTrigger()).CopyProperties(this.Trigger);
					schedTabControl.SelectedTab = dailyTab;
				}
				else if (sender == schedWeeklyRadio)
				{
					if (!onAssignment)
						(weeklyTriggerUI1.Trigger = new WeeklyTrigger()).CopyProperties(this.Trigger);
					schedTabControl.SelectedTab = weeklyTab;
				}
				else if (sender == schedMonthlyRadio)
				{
					monthlyTriggerUI1_TriggerTypeChanged(this, EventArgs.Empty);
				}
				if (!onAssignment && sender != schedMonthlyRadio)
					OnTriggerTypeChanged();
			}
		}

		private void schedStartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment && this.Trigger != null)
				this.Trigger.StartBoundary = schedStartDatePicker.Value;
		}
	}
}
