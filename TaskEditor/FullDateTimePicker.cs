using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	internal enum FullDateTimePickerTimeFormat
	{
		/// <summary>Shows hours, minutes and seconds</summary>
		LongTime,
		/// <summary>Shows hours and minutes</summary>
		ShortTime,
		/// <summary>No time box shown</summary>
		Hidden
	}

	/// <summary>
	/// A single control that can represent a full date and time.
	/// </summary>
    internal partial class FullDateTimePicker : UserControl
    {
        private DateTime currentValue = new DateTime(0L);
		private FullDateTimePickerTimeFormat timeFormat = FullDateTimePickerTimeFormat.LongTime;
		private FieldConversionUtcCheckBehavior utcBehavior = FieldConversionUtcCheckBehavior.ConvertLocalToUtc;
		private string utcPrompt = "Synchronize across time zones";

		/// <summary>
		/// Behavior of producing value when Utc check is checked
		/// </summary>
		public enum FieldConversionUtcCheckBehavior
		{
			/// <summary>Takes time in fields as local and produces value in Utc.</summary>
			ConvertLocalToUtc = 0,
			/// <summary>Takes time in fields as Utc and produces value in local.</summary>
			AssumeUtc = 1
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FullDateTimePicker"/> class.
		/// </summary>
        public FullDateTimePicker()
        {
            InitializeComponent();
        }

		[RefreshProperties(RefreshProperties.Repaint), DefaultValue(FullDateTimePickerTimeFormat.LongTime), Category("Behavior")]
		public FullDateTimePickerTimeFormat TimeFormat
		{
			get { return timeFormat; }
			set
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
						dateTimePickerTime.Visible = true;
						break;
				}
			}
		}

		/// <summary>
		/// Occurs when the <see cref="Value"/> property changes.
		/// </summary>
		[Category("Action"), Description("Occurs when the Value property changes.")]
		public event EventHandler ValueChanged;

		/// <summary>
		/// Gets or sets how fields are processed when the Utc Checkbox is checked.
		/// </summary>
		/// <value>The UTC check behavior.</value>
		[DefaultValue(0), Category("Behavior"), Description("Determines how to process fields when Utc Checkbox is checked")]
		public FieldConversionUtcCheckBehavior UtcCheckBehavior
		{
			get { return utcBehavior; }
			set { utcBehavior = value; }
		}

		/// <summary>
		/// Gets or sets the UTC prompt.
		/// </summary>
		/// <value>The UTC prompt.</value>
		[RefreshProperties(RefreshProperties.Repaint), DefaultValue("Synchronize across time zones"), Category("Behavior")]
        public string UTCPrompt
        {
            get { return utcPrompt; }
            set
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

        // Properties

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
        [DefaultValue(0L), Category("Data")]
        public DateTime Value
        {
            get
            {
                this.ControlsToData();
                return this.currentValue;
            }
            set
            {
                this.currentValue = value;
                this.DataToControls();
            }
        }

		/// <summary>
		/// Gets a value indicating whether value is UTC.
		/// </summary>
		/// <value><c>true</c> if value is UTC; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool ValueIsUTC
        {
            get { return this.currentValue.Kind == DateTimeKind.Utc; }
        }

		/// <summary>
		/// Raises the <see cref="E:ValueChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected virtual void OnValueChanged(EventArgs eventArgs)
		{
			EventHandler h = this.ValueChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

        protected void SelectDate()
        {
            this.dateTimePickerDate.Select();
        }

        private void ControlsToData()
        {
            DateTime time = this.dateTimePickerDate.Value;
			if (timeFormat != FullDateTimePickerTimeFormat.Hidden)
				time += this.dateTimePickerTime.Value.TimeOfDay;
			if (!utcCheckBox.Checked)
				this.currentValue = DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
			else
			{
				if (utcBehavior == FieldConversionUtcCheckBehavior.ConvertLocalToUtc)
					this.currentValue = DateTime.SpecifyKind(time, DateTimeKind.Local).ToUniversalTime();
				else
					this.currentValue = DateTime.SpecifyKind(time, DateTimeKind.Utc);
			}
        }

        private void DataToControls()
        {
            this.dateTimePickerDate.Value = this.currentValue.Date;
            this.dateTimePickerTime.Value = this.currentValue;
			this.utcCheckBox.Checked = this.currentValue.Kind != DateTimeKind.Unspecified;
        }

        private void FullDateTimePicker_Load(object sender, EventArgs e)
        {
            SetRightToLeft();
        }

        private void FullDateTimePicker_RightToLeftChanged(object sender, EventArgs e)
        {
            SetRightToLeft();
        }

        private void SetRightToLeft()
        {
            RightToLeft rightToLeftProperty = ControlProcessing.GetRightToLeftProperty(this);
            this.dateTimePickerDate.RightToLeft = rightToLeftProperty;
            this.dateTimePickerDate.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
            this.dateTimePickerTime.RightToLeft = rightToLeftProperty;
            this.dateTimePickerTime.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
        }

		private void subControl_ValueChanged(object sender, EventArgs e)
		{
			OnValueChanged(e);
		}
    }
}