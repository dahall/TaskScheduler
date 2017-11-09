using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>An editor that handles all Task triggers.</summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog allowing the editing of a task trigger.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	public partial class TriggerEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private DateTime initialStartBoundary = DateTime.MinValue;
		private bool isV2;
		private bool onAssignment;
		private readonly bool showCustom;
		private Trigger trigger;
		private readonly List<ListControlItem> triggerComboItems = new List<ListControlItem>(12);
		private bool useUnifiedSchedulingEngine;

		/// <summary>Initializes a new instance of the <see cref="TriggerEditDialog"/> class.</summary>
		public TriggerEditDialog() : this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="TriggerEditDialog"/> class.</summary>
		/// <param name="trigger">The <see cref="Trigger"/> to edit.</param>
		/// <param name="supportV1Only">If set to <c>true</c> support V1 triggers only.</param>
		public TriggerEditDialog(Trigger trigger, bool supportV1Only)
		{
			InitializeComponent();

			showCustom = trigger != null && trigger.TriggerType == TaskTriggerType.Custom;
			SupportV1Only = supportV1Only;

			// Populate combo boxes
			delaySpan.Items.AddRange(new[] { TimeSpan2.FromSeconds(30), TimeSpan2.FromMinutes(1), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(8), TimeSpan2.FromDays(1) });
			repeatSpan.Items.AddRange(new[] { TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1) });
			durationSpan.Items.AddRange(new[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1) });
			durationSpan.FormattedZero = EditorProperties.Resources.TimeSpanIndefinitely;
			stopIfRunsSpan.Items.AddRange(new[] { TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(2), TimeSpan2.FromHours(4), TimeSpan2.FromHours(8), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1), TimeSpan2.FromDays(3) });

			if (trigger != null)
				Trigger = trigger;
			else
			{
				Trigger = new TimeTrigger();
				initialStartBoundary = DateTime.MinValue;
			}
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

			/// <summary>Triggers the task on a custom event. Version 1.2 only.</summary>
			Custom = 120,
		}

		/// <summary>Gets or sets a value indicating whether this editor only supports V1 triggers.</summary>
		/// <value><c>true</c> if supports V1 only; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool SupportV1Only
		{
			get => !isV2;
			set
			{
				isV2 = !value;
				ResetControls();
			}
		}

		/// <summary>Gets or sets the target server.</summary>
		/// <value>The target server.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(null)]
		public string TargetServer { get; set; }

		/// <summary>Gets or sets the trigger that is being edited.</summary>
		/// <value>The trigger.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Trigger Trigger
		{
			get => trigger;
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(Trigger));
				onAssignment = true;
				trigger = value is CustomTrigger ? value : (Trigger)value.Clone();
				initialStartBoundary = trigger.StartBoundary;
				switch (trigger.TriggerType)
				{
					case TaskTriggerType.Time:
					case TaskTriggerType.Daily:
					case TaskTriggerType.Weekly:
					case TaskTriggerType.Monthly:
					case TaskTriggerType.MonthlyDOW:
						TriggerView = TaskTriggerDisplayType.Schedule;
						calendarTriggerUI1.Trigger = trigger;
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
						var state = 110 + (int)((SessionStateChangeTrigger)trigger).StateChange;
						if (state == 113 || state == 114) state -= 2;
						TriggerView = (TaskTriggerDisplayType)state;
						logonRemoteRadio.Checked = ((SessionStateChangeTrigger)trigger).StateChange == TaskSessionStateChangeType.RemoteConnect || ((SessionStateChangeTrigger)trigger).StateChange == TaskSessionStateChangeType.RemoteDisconnect;
						logonLocalRadio.Checked = !logonRemoteRadio.Checked;
						break;

					case TaskTriggerType.Custom:
						TriggerView = TaskTriggerDisplayType.Custom;
						customNameText.Text = ((CustomTrigger)trigger).Name;
						customPropsListView.Items.Clear();
						foreach (var nvpair in ((CustomTrigger)trigger).Properties)
							customPropsListView.Items.Add(new ListViewItem(new[] { nvpair.Name, nvpair.Value }));
						break;
				}
				if (trigger is ITriggerDelay td && isV2)
				{
					delaySpan.Value = td.Delay;
					delayCheckBox.Checked = delaySpan.Enabled = td.Delay != TimeSpan.Zero;
				}
				else
				{
					delaySpan.Value = TimeSpan.Zero;
					delayCheckBox.Checked = delayCheckBox.Enabled = delaySpan.Enabled = false;
				}
				if (trigger is ITriggerUserId tu && isV2)
				{
					logonUserLabel.Text = tu.UserId;
					logonAnyUserRadio.Checked = logonUserLabel.Text.Length == 0;
					logonSpecUserRadio.Checked = logonUserLabel.Text.Length > 0;
				}
				var hasRep = trigger.Repetition.Interval != TimeSpan.Zero;
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
					triggerIdText.Text = trigger.Id;
					stopIfRunsSpan.Value = trigger.ExecutionTimeLimit;
					stopIfRunsCheckBox.Enabled = stopIfRunsCheckBox.Checked = stopIfRunsSpan.Enabled = trigger.ExecutionTimeLimit != TimeSpan.Zero;
				}
				var showActivate = TriggerView != TaskTriggerDisplayType.Schedule;
				activateCheckBox.Visible = activateDatePicker.Visible = showActivate;
				activateCheckBox.Checked = showActivate && trigger.StartBoundary != DateTime.MinValue;
				activateDatePicker.Enabled = showActivate && activateCheckBox.Checked && TriggerView != TaskTriggerDisplayType.Custom;
				activateDatePicker.Value = activateCheckBox.Checked ? MaxDate(trigger.StartBoundary, DateTimePicker.MinimumDateTime) : DateTime.Now;
				expireCheckBox.Checked = expireDatePicker.Enabled = trigger.EndBoundary != DateTime.MaxValue;
				expireDatePicker.Value = expireCheckBox.Checked ? trigger.EndBoundary : MaxDate(trigger.StartBoundary, DateTime.Now);
				enabledCheckBox.Checked = trigger.Enabled;

				if (value is CustomTrigger)
					advSettingsGroup.EnableChildren(false);

				onAssignment = false;
			}
		}

		/// <summary>Gets or sets a value indicating whether dialog should restrict items to those available when using the Unified Scheduling Engine.</summary>
		/// <value><c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
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
					calendarTriggerUI1.UseUnifiedSchedulingEngine = value;
					stopIfRunsCheckBox.Enabled = stopIfRunsSpan.Enabled =
						repeatCheckBox.Enabled = repeatSpan.Enabled = durationLabel.Enabled =
						durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = !useUnifiedSchedulingEngine;
				}
			}
		}

		private TaskTriggerDisplayType TriggerView
		{
			get => triggerTypeCombo.SelectedValue == null
				? TaskTriggerDisplayType.Schedule
				: (TaskTriggerDisplayType) Convert.ToInt32(triggerTypeCombo.SelectedValue);
			set
			{
				if (triggerTypeCombo.SelectedIndex == -1 || Convert.ToInt64(triggerTypeCombo.SelectedValue) != (long)value)
					triggerTypeCombo.SelectedValue = (long)value;
			}
		}

		private static DateTime MaxDate(DateTime dt1, DateTime dt2) => dt1 >= dt2 ? dt1 : dt2;

		private void activateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			activateDatePicker.Enabled = activateCheckBox.Checked;
			if (!onAssignment)
			{
				if (activateCheckBox.Checked)
					trigger.StartBoundary = activateDatePicker.Value == DateTimePicker.MinimumDateTime ? DateTime.MinValue : activateDatePicker.Value;
				else
					trigger.StartBoundary = DateTime.MinValue;
				initialStartBoundary = trigger.StartBoundary;
			}
		}

		private void activateDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				trigger.StartBoundary = activateDatePicker.Value == DateTimePicker.MinimumDateTime ? DateTime.MinValue : activateDatePicker.Value;
		}

		private void calendarTriggerUI1_TriggerTypeChanged(object sender, EventArgs e)
		{
			trigger = calendarTriggerUI1.Trigger;
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
			if (!onAssignment && trigger is ITriggerDelay td)
				td.Delay = delaySpan.Value;
		}

		private void durationSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (durationSpan.Value < TimeSpan2.FromMinutes(1) && durationSpan.Value != TimeSpan2.Zero)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_RepetitionDurationOutOfRange, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					durationSpan.Value = TimeSpan.FromMinutes(1);
					return;
				}
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
				if (expireCheckBox.Checked && expireDatePicker.Value != DateTimePicker.MinimumDateTime)
					trigger.EndBoundary = expireDatePicker.Value;
				else
					trigger.EndBoundary = DateTime.MaxValue;
			}
		}

		private void expireDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment && expireCheckBox.Checked)
				trigger.EndBoundary = expireDatePicker.Value == DateTimePicker.MinimumDateTime || expireDatePicker.Value == DateTimePicker.MaximumDateTime ? DateTime.MaxValue : expireDatePicker.Value;
		}

		private void logonAnyUserRadio_CheckedChanged(object sender, EventArgs e)
		{
			var any = logonAnyUserRadio.Checked;
			logonChgUserBtn.Enabled = logonUserLabel.Enabled = !any;
			((ITriggerUserId)trigger).UserId = any ? null : logonUserLabel.Text;
		}

		private void logonChgUserBtn_Click(object sender, EventArgs e)
		{
			string acct = String.Empty;
			if (!HelperMethods.SelectAccount(this, null, ref acct, out bool _, out bool _, out var _))
				return;
			((ITriggerUserId)trigger).UserId = logonUserLabel.Text = acct;
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
				repeatSpan.Enabled = durationLabel.Enabled = durationSpan.Enabled = stopAfterDurationCheckBox.Enabled = repeatCheckBox.Checked;
			}
		}

		private void repeatSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				var value = (TimeSpan)repeatSpan.Value;
				if (value < TimeSpan.FromMinutes(1) || value > TimeSpan.FromDays(31))
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_RepetitionIntervalOutOfRange, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					repeatSpan.Value = value < TimeSpan.FromMinutes(1) ? TimeSpan.FromMinutes(1) : TimeSpan.FromDays(31);
					return;
				}
				trigger.Repetition.Interval = value;
				if (trigger.Repetition.Duration < value && trigger.Repetition.Duration != TimeSpan.Zero)
				{
					onAssignment = true;
					durationSpan.Value = value + TimeSpan.FromMinutes(1);
					trigger.Repetition.Duration = durationSpan.Value;
					onAssignment = false;
				}
			}
		}

		private void ResetControls()
		{
			// Setup list of triggers available
			triggerComboItems.Clear();
			ComboBoxExtension.InitializeFromEnum(triggerComboItems, typeof(TaskTriggerDisplayType), EditorProperties.Resources.ResourceManager, "TriggerType", out long _);
			if (!isV2)
				triggerComboItems.RemoveRange(4, 7);
			if (!showCustom)
				triggerComboItems.RemoveAt(triggerComboItems.Count - 1);
			triggerTypeCombo.BeginUpdate();
			triggerTypeCombo.DataSource = null;
			triggerTypeCombo.DisplayMember = "Text";
			triggerTypeCombo.ValueMember = "Value";
			triggerTypeCombo.DataSource = triggerComboItems;
			triggerTypeCombo.EndUpdate();

			// Enable/disable version specific features
			calendarTriggerUI1.IsV2 = isV2;
			triggerIdText.Enabled = stopIfRunsCheckBox.Enabled = stopIfRunsSpan.Enabled = delayCheckBox.Enabled = delaySpan.Enabled = isV2;

			// Set date/time controls
			activateDatePicker.UTCPrompt = expireDatePicker.UTCPrompt = isV2 ? EditorProperties.Resources.DateTimeSyncText : null;
			activateDatePicker.TimeFormat = isV2 ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.ShortTime;
			expireDatePicker.TimeFormat = isV2 ? FullDateTimePickerTimeFormat.LongTime : FullDateTimePickerTimeFormat.Hidden;

			// Disable logon trigger options
			foreach (Control c in logonTab.Controls)
				c.Enabled = isV2;
		}

		private void span_Validating(object sender, CancelEventArgs e)
		{
			var pkr = sender as TimeSpanPicker;
			var valid = pkr != null && pkr.Enabled && pkr.IsTextValid;
			e.Cancel = !valid;
			if (pkr != null)
				errorProvider.SetError(pkr, valid ? string.Empty : EditorProperties.Resources.Error_InvalidSpanValue);
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

		private void triggerIdText_TextChanged(object sender, EventArgs e)
		{
			if (!onAssignment && isV2)
				trigger.Id = triggerIdText.TextLength == 0 ? null : triggerIdText.Text;
		}

		private void triggerTypeCombo_SelectedValueChanged(object sender, EventArgs e)
		{
			if (triggerTypeCombo.SelectedValue == null)
				return;

			Trigger newTrigger = null;
			switch (TriggerView)
			{
				//case TaskTriggerDisplayType.Schedule:
				default:
					settingsTabControl.SelectedTab = scheduleTab;
					if (!onAssignment) newTrigger = new TimeTrigger();
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

				case TaskTriggerDisplayType.Custom:
					settingsTabControl.SelectedTab = customTab;
					triggerTypeCombo.Enabled = okBtn.Enabled = false;
					break;
			}

			if (newTrigger != null && !onAssignment)
			{
				if (trigger != null)
					newTrigger.CopyProperties(trigger);
				if (newTrigger is ICalendarTrigger)
				{
					if (newTrigger.StartBoundary == DateTime.MinValue)
						newTrigger.StartBoundary = DateTime.Now;
				}
				else
					newTrigger.StartBoundary = initialStartBoundary;
				Trigger = newTrigger;
			}
		}
	}
}