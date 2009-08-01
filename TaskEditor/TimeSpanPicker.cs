using System.Windows.Forms;
using System.Collections.Specialized;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Win32.TaskScheduler
{
	[DefaultEvent("ValueChanged"), DefaultProperty("Value")]
	public partial class TimeSpanPicker : UserControl
	{
		private TimeSpan value = TimeSpan.MinValue;

		public TimeSpanPicker()
		{
			InitializeComponent();
		}

		[Category("Property Changed")]
		public event EventHandler ValueChanged;

		protected virtual void OnValueChanged(EventArgs args)
		{
			EventHandler h = ValueChanged;
			if (h != null)
				h(this, args);
		}

		private void comboBoxTimeSpan_Leave(object sender, EventArgs e)
		{
			if (this.ControlsToData())
				OnValueChanged(EventArgs.Empty);
		}

		private bool ControlsToData()
		{
			try { value = GetValue(this.comboBoxTimeSpan.Text); }
			catch { return false; }
			return true;
		}

		private string GetString(TimeSpan t)
		{
			return t.ToString(string.IsNullOrEmpty(FormattedZero) ? "f" : "f;" + FormattedZero);
		}

		private TimeSpan GetValue(string s)
		{
			return TimeSpanExtension.Parse(this.comboBoxTimeSpan.Text);
		}

		private void DataToControls()
		{
			this.comboBoxTimeSpan.Text = GetString(value);
		}

		internal void DeselectText()
		{
			this.comboBoxTimeSpan.SelectionLength = 0;
		}

		[DefaultValue(null), Category("Appearance")]
		public string FormattedZero { get; set; }

		[DefaultValue("00:00:00"), Category("Data")]
		public TimeSpan Value
		{
			get
			{
				this.ControlsToData();
				return this.value;
			}
			set
			{
				this.value = value;
				this.DataToControls();
				OnValueChanged(EventArgs.Empty);
			}
		}

		public ComboBox.ObjectCollection Items
		{
			get { return comboBoxTimeSpan.Items; }
		}

		private void comboBoxTimeSpan_Format(object sender, ListControlConvertEventArgs e)
		{
			if (e.DesiredType == typeof(string) && e.ListItem is TimeSpan)
				e.Value = GetString(((TimeSpan)e.ListItem));
		}
	}
}
