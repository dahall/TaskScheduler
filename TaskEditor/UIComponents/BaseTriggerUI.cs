using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class BaseTriggerUI : UserControl, ITriggerHandler
	{
		protected bool isV2 = true;
		protected bool onAssignment = false;
		protected Trigger trigger;
		private bool showStart = true;

		public BaseTriggerUI()
		{
			InitializeComponent();
		}

		[Browsable(false), DefaultValue(null)]
		public virtual Trigger Trigger
		{
			get { return trigger; }
			set
			{
				onAssignment = true;
				trigger = value;
				schedStartDatePicker.Value = trigger.StartBoundary;
			}
		}

		[DefaultValue(true)]
		public virtual bool IsV2
		{
			get { return isV2; }
			set { isV2 = value; }
		}

		[DefaultValue(true)]
		public virtual bool ShowStartBoundary
		{
			get { return showStart; }
			set
			{
				if (showStart != value)
				{
					showStart = value;
					panel1.Visible = showStart;
				}
			}
		}

		public virtual bool IsTriggerValid() { return true; }

		private void schedStartDatePicker_ValueChanged(object sender, EventArgs e)
		{
			if (showStart)
				trigger.StartBoundary = schedStartDatePicker.Value;
		}
	}
}
