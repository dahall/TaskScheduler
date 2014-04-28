using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// An editor that handles all Task triggers.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog allowing the editing of a task trigger.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	public partial class TriggerEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private bool isV2;
		private bool onAssignment = false;
		private Trigger trigger;
		private List<DropDownCheckListItem> triggerComboItems = new List<DropDownCheckListItem>(12);
		private bool useUnifiedSchedulingEngine = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="TriggerEditDialog"/> class.
		/// </summary>
		public TriggerEditDialog() : this(null, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TriggerEditDialog"/> class.
		/// </summary>
		/// <param name="trigger">The <see cref="Trigger"/> to edit.</param>
		/// <param name="supportV1Only">If set to <c>true</c> support V1 triggers only.</param>
		public TriggerEditDialog(Trigger trigger, bool supportV1Only)
		{
			InitializeComponent();

			this.SupportV1Only = supportV1Only;

			// Populate combo boxes
			delaySpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromSeconds(30), TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(8), TimeSpan2.FromDays(1) });
			repeatSpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1) });
			durationSpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1) });
			durationSpan.FormattedZero = EditorProperties.Resources.TimeSpanIndefinitely;
			stopIfRunsSpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2), TimeSpan2.FromHours(4), TimeSpan2.FromHours(8), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1), TimeSpan2.FromDays(3) });

			if (trigger != null)
				this.Trigger = trigger;
			else
				this.Trigger = new TimeTrigger();
		}

		/// <summary>Defines the type of triggers that can be used by tasks.</summary>
		private enum TaskTriggerDisplayType
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

		/// <summary>
		/// Gets or sets a value indicating whether this editor only supports V1 triggers.
		/// </summary>
		/// <value><c>true</c> if supports V1 only; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool SupportV1Only
		{
			get { return !isV2; }
			set
			{
				isV2 = !value;
				ResetControls();
			}
		}

		/// <summary>
		/// Gets or sets the target server.
		/// </summary>
		/// <value>The target server.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue((string)null)]
		public string TargetServer
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the trigger that is being edited.
		/// </summary>
		/// <value>The trigger.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Trigger Trigger
		{
			get
			{
				return trigger;
			}
			set
			{
				onAssignment = true;
				trigger = value.Clone() as Trigger;
				switch (trigger.TriggerType)
				{
					case TaskTriggerType.Time:
						schedOneRadio.Checked = true;
						SetSchedTrigger();
						break;
					case TaskTriggerType.Daily:
						schedDailyRadio.Checked = true;
						SetSchedTrigger();
						dailyTriggerUI1.Trigger = trigger;
						break;
					case TaskTriggerType.Weekly:
						schedWeeklyRadio.Checked = true;
						SetSchedTrigger();
						weeklyTriggerUI1.Trigger = trigger;
						break;
					case TaskTriggerType.Monthly:
					case TaskTriggerType.MonthlyDOW:
						schedMonthlyRadio.Checked = true;
						SetSchedTrigger();
						monthlyTriggerUI1.Trigger = trigger;
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
						eventTriggerUI1.TargetServer = TargetServer;
						eventTriggerUI1.Trigger = trigger;
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
					delaySpan.Value = ((ITriggerDelay)trigger).Delay;
					delayCheckBox.Checked = delaySpan.Enabled = ((ITriggerDelay)trigger).Delay != TimeSpan.Zero;
				}
				else
				{
					delaySpan.Value = TimeSpan.Zero;
					delayCheckBox.Checked = delayCheckBox.Enabled = delaySpan.Enabled = false;
				}
				if (trigger is ITriggerUserId && isV2)
				{
					logonUserLabel.Text = ((ITriggerUserId)trigger).UserId;
					logonAnyUserRadio.Checked = (logonUserLabel.Text.Length == 0);
					logonSpecUserRadio.Checked = (logonUserLabel.Text.Length > 0);
				}
				bool hasRep = trigger.Repetition.Interval != TimeSpan.Zero;
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
				repeatCheckBox.Checked = repeatSpan.Enabled = durationLabel.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = hasRep;
				if (isV2)
				{
					stopIfRunsSpan.Value = trigger.ExecutionTimeLimit;
					stopIfRunsCheckBox.Enabled = stopIfRunsCheckBox.Checked = stopIfRunsSpan.Enabled = trigger.ExecutionTimeLimit != TimeSpan.Zero;
				}
				activateCheckBox.Visible = activateDatePicker.Visible = TriggerView != TaskTriggerDisplayType.Schedule;
				if (TriggerView != TaskTriggerDisplayType.Schedule)
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

		/// <summary>
		/// Gets or sets a value indicating whether dialog should restrict items to those available when using the Unified Scheduling Engine.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior")]
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
					ResetControls();
				}
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
					trigger.StartBoundary = DateTime.MinValue;
			}
		}

		private void activateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.StartBoundary = activateDatePicker.Value;
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void delayCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				delaySpan.Enabled = delayCheckBox.Checked;
				delaySpan.Value = delayCheckBox.Checked ? TimeSpan.FromHours(1) : TimeSpan.Zero;
			}
		}

		private void delaySpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment && trigger is ITriggerDelay)
				((ITriggerDelay)trigger).Delay = delaySpan.Value;
		}

		private void durationSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				trigger.Repetition.Duration = durationSpan.Value;
				if (trigger.Repetition.Duration < trigger.Repetition.Interval && trigger.Repetition.Duration != TimeSpan.Zero)
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

		private void expireDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment && expireCheckBox.Checked)
				trigger.EndBoundary = expireDatePicker.Value;
		}

		private void logonAnyUserRadio_CheckedChanged(object sender, EventArgs e)
		{
			bool any = logonAnyUserRadio.Checked;
			logonChgUserBtn.Enabled = logonUserLabel.Enabled = !any;
			((ITriggerUserId)trigger).UserId = any ? null : logonUserLabel.Text;
		}

		private void logonChgUserBtn_Click(object sender, EventArgs e)
		{
			string acct = String.Empty, sid;
			bool isGroup, isService;
			if (!HelperMethods.SelectAccount(this, null, ref acct, out isGroup, out isService, out sid))
				return;
			((ITriggerUserId)trigger).UserId = logonUserLabel.Text = acct;
		}

		private void monthlyTriggerUI1_TriggerTypeChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				Trigger newTrigger = null;
				if (monthlyTriggerUI1.TriggerType == TaskTriggerType.Monthly)
					newTrigger = new MonthlyTrigger();
				else
					newTrigger = new MonthlyDOWTrigger();
				if (newTrigger != null)
				{
					if (trigger != null)
						newTrigger.CopyProperties(trigger);
					this.Trigger = newTrigger;
				}
			}
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void repeatCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (repeatCheckBox.Checked)
				{
					durationSpan.Value = durationSpan.Items[durationSpan.Items.Count - 1];
					repeatSpan.Value = repeatSpan.Items[repeatSpan.Items.Count - 1];
				}
				else
				{
					trigger.Repetition.Duration = trigger.Repetition.Interval = TimeSpan.Zero;
				}
				repeatSpan.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = repeatCheckBox.Checked;
			}
		}

		private void repeatSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				trigger.Repetition.Interval = repeatSpan.Value;
				if (trigger.Repetition.Duration < trigger.Repetition.Interval && trigger.Repetition.Duration != TimeSpan.Zero)
				{
					onAssignment = true;
					durationSpan.Value = trigger.Repetition.Interval + TimeSpan.FromMinutes(1);
					trigger.Repetition.Duration = durationSpan.Value;
					onAssignment = false;
				}
			}
		}

		private void ResetControls()
		{
			// Setup list of triggers available
			long allVal;
			triggerComboItems.Clear();
			ComboBoxExtension.InitializeFromEnum(triggerComboItems, typeof(TaskTriggerDisplayType), EditorProperties.Resources.ResourceManager, "TriggerType", out allVal);
			if (!isV2)
				triggerComboItems.RemoveRange(4, 6);
			triggerTypeCombo.BeginUpdate();
			triggerTypeCombo.DataSource = null;
			triggerTypeCombo.DisplayMember = "Text";
			triggerTypeCombo.ValueMember = "Value";
			triggerTypeCombo.DataSource = triggerComboItems;
			triggerTypeCombo.EndUpdate();

			// Enable/disable version specific features
			stopIfRunsCheckBox.Enabled = stopIfRunsSpan.Enabled = delayCheckBox.Enabled = delaySpan.Enabled = isV2;
			dailyTriggerUI1.IsV2 = weeklyTriggerUI1.IsV2 = monthlyTriggerUI1.IsV2 = isV2;
			schedMonthlyRadio.Enabled = stopIfRunsCheckBox.Enabled = stopIfRunsSpan.Enabled = 
				repeatCheckBox.Enabled = repeatSpan.Enabled = durationLabel.Enabled = 
				durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = !useUnifiedSchedulingEngine;

			// Set date/time controls
			schedStartDatePicker.UTCPrompt = activateDatePicker.UTCPrompt = expireDatePicker.UTCPrompt = isV2 ? EditorProperties.Resources.DateTimeSyncText : null;
			schedStartDatePicker.TimeFormat = (isV2) ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.ShortTime;
			activateDatePicker.TimeFormat = (isV2) ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.ShortTime;
			expireDatePicker.TimeFormat = (isV2) ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.Hidden;

			// Disable logon trigger options
			foreach (Control c in logonTab.Controls)
				c.Enabled = isV2;
		}

		private void schedOneRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				Trigger newTrigger = null;
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
					monthlyTriggerUI1_TriggerTypeChanged(this, EventArgs.Empty);
				}

				if (newTrigger != null && !onAssignment)
				{
					if (trigger != null)
						newTrigger.CopyProperties(trigger);
					this.Trigger = newTrigger;
				}
			}
		}

		private void schedStartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.StartBoundary = schedStartDatePicker.Value;
		}

		private void SetSchedTrigger()
		{
			TriggerView = TaskTriggerDisplayType.Schedule;
			schedStartDatePicker.Value = trigger.StartBoundary;
		}

		private void stopAfterDurationCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			trigger.Repetition.StopAtDurationEnd = stopAfterDurationCheckBox.Checked;
		}

		private void stopIfRunsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				stopIfRunsSpan.Enabled = stopIfRunsCheckBox.Checked;
				stopIfRunsSpan.Value = stopIfRunsCheckBox.Checked ? TimeSpan.FromDays(3) : TimeSpan.Zero;
			}
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

			Trigger newTrigger = null;
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

			if (newTrigger != null && !onAssignment)
			{
				if (trigger != null)
					newTrigger.CopyProperties(trigger);
				this.Trigger = newTrigger;
			}
		}
	}
}