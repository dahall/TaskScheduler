using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Determines the format of the <see cref="FullDateTimePicker"/> control.</summary>
	public enum FullDateTimePickerTimeFormat
	{
		/// <summary>Shows hours, minutes and seconds</summary>
		LongTime,
		/// <summary>Shows hours and minutes</summary>
		ShortTime,
		/// <summary>No time box shown</summary>
		Hidden
	}

	/// <summary>A single control that can represent a full date and time.</summary>
	[DefaultEvent("ValueChanged"), DefaultProperty("Value"), DefaultBindingProperty("Value")]
	[System.Drawing.ToolboxBitmap(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog), "Control")]
	public partial class FullDateTimePicker : UserControl
	{
		private DateTime currentValue;
		private bool initializing = false;
		private FullDateTimePickerTimeFormat timeFormat = FullDateTimePickerTimeFormat.LongTime;
		private bool userHasSetValue;
		private FieldConversionUtcCheckBehavior utcBehavior = FieldConversionUtcCheckBehavior.ConvertLocalToUtc;
		private string utcPrompt = "Synchronize across time zones";

		/// <summary>Initializes a new instance of the <see cref="FullDateTimePicker"/> class.</summary>
		public FullDateTimePicker()
		{
			InitializeComponent();
			//dateTimePickerTime.Format = DateTimePickerFormat.Custom;
			//dateTimePickerTime.CustomFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
			ResetValue();
		}

		/// <summary>Occurs when the <see cref="Value"/> property changes.</summary>
		[Category("Action"), Description("Occurs when the Value property changes.")]
		public event EventHandler ValueChanged;

		/// <summary>Behavior of producing value when Utc check is checked</summary>
		public enum FieldConversionUtcCheckBehavior
		{
			/// <summary>Takes time in fields as local and produces value in Utc.</summary>
			ConvertLocalToUtc = 0,
			/// <summary>Takes time in fields as Utc and produces value in local.</summary>
			AssumeUtc = 1,
			/// <summary>Takes time in fields as local and leaves them local.</summary>
			AssumeLocal = 2
		}

		/// <summary>Gets or sets a value indicating whether [auto size].</summary>
		/// <value><c>true</c> if [auto size]; otherwise, <c>false</c>.</value>
		[Browsable(true), Category("Layout"), DefaultValue(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get => base.AutoSize;
			set => base.AutoSize = value;
		}

		/// <summary>Gets or sets how the control will resize itself.</summary>
		/// <value></value>
		/// <returns>A value from the <see cref="T:System.Windows.Forms.AutoSizeMode"/> enumeration. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly"/>.</returns>
		[Browsable(true), Category("Layout"), Description("How the control will resize itself"), DefaultValue(AutoSizeMode.GrowAndShrink), Localizable(true)]
		public new AutoSizeMode AutoSizeMode
		{
			get => base.AutoSizeMode;
			set => base.AutoSizeMode = value;
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <value></value>
		/// <returns>A string that represents the text associated with this control.</returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public override string Text
		{
			get => base.Text;
			set
			{
				if ((value == null) || (value.Length == 0))
					ResetValue();
				else
					try { Value = DateTime.Parse(value, System.Globalization.CultureInfo.CurrentUICulture); } catch { }
			}
		}

		/// <summary>Gets or sets the format of the time portion of the control.</summary>
		/// <value>The time format.</value>
		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(FullDateTimePickerTimeFormat.LongTime), Category("Behavior")]
		[Description("The format of the time portion of the control.")]
		public FullDateTimePickerTimeFormat TimeFormat
		{
			get => timeFormat;
			set
			{
				if (timeFormat != value)
				{
					timeFormat = value;
					switch (value)
					{
						case FullDateTimePickerTimeFormat.ShortTime:
							dateTimePickerTime.Format = DateTimePickerFormat.Custom;
							dateTimePickerTime.CustomFormat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortTimePattern;
							dateTimePickerTime.Visible = true;
							break;
						case FullDateTimePickerTimeFormat.Hidden:
							//dateTimePickerTime.Value = dateTimePickerTime.Value.Date;
							dateTimePickerTime.Visible = false;
							break;
						case FullDateTimePickerTimeFormat.LongTime:
						default:
							dateTimePickerTime.Format = DateTimePickerFormat.Time;
							dateTimePickerTime.CustomFormat = null; //System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
							dateTimePickerTime.Visible = true;
							break;
					}
				}
			}
		}

		/// <summary>Gets or sets how fields are processed when the Utc Checkbox is checked.</summary>
		/// <value>The UTC check behavior.</value>
		[DefaultValue(FieldConversionUtcCheckBehavior.ConvertLocalToUtc), Category("Behavior"), Description("Determines how to process fields when Utc Checkbox is checked")]
		public FieldConversionUtcCheckBehavior UtcCheckBehavior
		{
			get => utcBehavior;
			set => utcBehavior = value;
		}

		/// <summary>Gets or sets the text prompt for the UTC CheckBox. Leave blank to remove the CheckBox.</summary>
		/// <value>The text prompt for the UTC CheckBox.</value>
		[RefreshProperties(RefreshProperties.Repaint), DefaultValue("Synchronize across time zones"), Category("Behavior"), Localizable(true), Bindable(true)]
		[Description("The text prompt for the UTC CheckBox.")]
		public string UTCPrompt
		{
			get => utcPrompt;
			set
			{
				if (utcPrompt != value)
				{
					utcPrompt = value;
					if (string.IsNullOrEmpty(utcPrompt))
					{
						utcCheckBox.Checked = false;
						utcCheckBox.Visible = false;
					}
					else
					{
						utcCheckBox.Text = utcPrompt;
						utcCheckBox.Checked = false;
						utcCheckBox.Visible = true;
					}
				}
			}
		}

		/// <summary>Gets or sets the value.</summary>
		/// <value>The value.</value>
		[Category("Data"), RefreshProperties(RefreshProperties.All), Bindable(true), Description("The full date and time.")]
		public DateTime Value
		{
			get => userHasSetValue ? currentValue : DateTimePicker.MinimumDateTime;
			set
			{
				bool newVal = currentValue != value;
				if (newVal || !userHasSetValue)
				{
					currentValue = value;
					userHasSetValue = true;
					initializing = true;
					DataToControls();
					initializing = false;
					if (newVal)
						OnValueChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether value is UTC.</summary>
		/// <value><c>true</c> if value is UTC; otherwise, <c>false</c>.</value>
		[Browsable(false)]
		public bool ValueIsUTC => currentValue.Kind == DateTimeKind.Utc;

		internal bool ShouldSerializeValue() => userHasSetValue;

		/// <summary>Raises the <see cref="ValueChanged"/> event.</summary>
		/// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected virtual void OnValueChanged(EventArgs eventArgs) => ValueChanged?.Invoke(this, EventArgs.Empty);

		/// <summary>Selects the date control.</summary>
		protected void SelectDate() => dateTimePickerDate.Select();

		private static DateTime ConstrainedDateTime(DateTime value)
		{
			DateTime displayTime = value.Kind == DateTimeKind.Utc ? value.ToLocalTime() : value;
			if (displayTime > DateTimePicker.MaximumDateTime) displayTime = DateTimePicker.MaximumDateTime;
			if (displayTime < DateTimePicker.MinimumDateTime) displayTime = DateTimePicker.MinimumDateTime;
			return displayTime;
		}

		private void ControlsToData()
		{
			DateTime time = dateTimePickerDate.Value;
			if (timeFormat != FullDateTimePickerTimeFormat.Hidden)
				time += dateTimePickerTime.Value.TimeOfDay;
			if (!utcCheckBox.Checked)
				currentValue = DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
			else
			{
				if (currentValue.Kind == DateTimeKind.Unspecified)
				{
					switch (utcBehavior)
					{
						case FieldConversionUtcCheckBehavior.ConvertLocalToUtc:
							currentValue = DateTime.SpecifyKind(time, DateTimeKind.Local).ToUniversalTime();
							break;
						case FieldConversionUtcCheckBehavior.AssumeUtc:
							currentValue = DateTime.SpecifyKind(time, DateTimeKind.Utc);
							break;
						case FieldConversionUtcCheckBehavior.AssumeLocal:
							currentValue = DateTime.SpecifyKind(time, DateTimeKind.Local);
							break;
						default:
							break;
					}
				}
				else
					currentValue = DateTime.SpecifyKind(time, currentValue.Kind);
			}
		}

		private void DataToControls()
		{
			DateTime displayTime = ConstrainedDateTime(currentValue);
			dateTimePickerDate.Value = displayTime.Date;
			dateTimePickerTime.Value = displayTime;
			if (!string.IsNullOrEmpty(utcPrompt))
				utcCheckBox.Checked = currentValue.Kind != DateTimeKind.Unspecified;
		}

		private void FullDateTimePicker_Load(object sender, EventArgs e)
		{
			SetRightToLeft();
		}

		private void FullDateTimePicker_RightToLeftChanged(object sender, EventArgs e)
		{
			SetRightToLeft();
		}

		private void ResetValue()
		{
			currentValue = DateTime.Now;
			userHasSetValue = false;
			DataToControls();
		}

		private void SetRightToLeft()
		{
			RightToLeft rightToLeftProperty = RightToLeft;
			dateTimePickerDate.RightToLeft = rightToLeftProperty;
			dateTimePickerDate.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
			dateTimePickerTime.RightToLeft = rightToLeftProperty;
			dateTimePickerTime.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
		}

		private void subControl_ValueChanged(object sender, EventArgs e)
		{
			if (!initializing)
			{
				ControlsToData();
				OnValueChanged(e);
			}
		}
	}
}