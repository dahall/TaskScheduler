using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class FullDateTimePicker : UserControl
	{
		private DateTime currentValue = new DateTime(0L);
		private string utcPrompt = null;

		public FullDateTimePicker()
		{
			InitializeComponent();
		}

		private void ControlsToData()
		{
			DateTime time = this.dateTimePickerDate.Value;
			DateTime time2 = this.dateTimePickerTime.Value;
			time = time.Add(time2.TimeOfDay);
			this.currentValue = time;
		}

		private void DataToControls()
		{
			this.dateTimePickerDate.Value = this.currentValue.Date;
			this.dateTimePickerTime.Value = this.currentValue;
		}

		private void FullDateTimePicker_Load(object sender, EventArgs e)
		{
			SetRightToLeft();
		}

		private void FullDateTimePicker_RightToLeftChanged(object sender, EventArgs e)
		{
			SetRightToLeft();
		}

		protected void SelectDate()
		{
			this.dateTimePickerDate.Select();
		}

		private void SetRightToLeft()
		{
			RightToLeft rightToLeftProperty = ControlProcessing.GetRightToLeftProperty(this);
			this.dateTimePickerDate.RightToLeft = rightToLeftProperty;
			this.dateTimePickerDate.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
			this.dateTimePickerTime.RightToLeft = rightToLeftProperty;
			this.dateTimePickerTime.RightToLeftLayout = rightToLeftProperty == RightToLeft.Yes;
		}

		// Properties
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

		[Browsable(false)]
		public bool ValueIsUTC
		{
			get { return this.currentValue.ToUniversalTime() == this.currentValue; }
		}

		[DefaultValue("Synchronize across time zones"), Category("Behavior")]
		public string UTCPrompt
		{
			get { return utcPrompt; }
			set
			{
				utcPrompt = value;
				if (string.IsNullOrEmpty(utcPrompt))
				{
					tableLayoutPanelFullDateTime.ColumnStyles[2].Width = 0;
					utcCheckBox.Checked = false;
				}
				else
				{
					utcCheckBox.Text = utcPrompt;
					tableLayoutPanelFullDateTime.ColumnStyles[2].Width = utcCheckBox.Width;
					utcCheckBox.Checked = false;
				}
			}
		}
	}
}
