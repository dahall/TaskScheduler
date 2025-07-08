using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class EventActionFilterTimeEditor : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventActionFilterTimeEditor"/> class.
		/// </summary>
		public EventActionFilterTimeEditor()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the "from" date time.
		/// </summary>
		/// <value>
		/// "From" date time.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DateTime? FromDateTime { get; set; }

		/// <summary>
		/// Gets or sets "to" date time.
		/// </summary>
		/// <value>
		/// "To" date time.
		/// </value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DateTime? ToDateTime { get; set; }

		private void okButton_Click(object sender, EventArgs e)
		{
			FromDateTime = fromCombo.SelectedIndex == 0 ? (DateTime?)null : fromDatePicker.Value;
			ToDateTime = toCombo.SelectedIndex == 0 ? (DateTime?)null : toDatePicker.Value;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EventActionFilterTimeEditor_Load(object sender, EventArgs e)
		{
			fromCombo.SelectedIndex = FromDateTime.HasValue ? 1 : 0;
			if (FromDateTime.HasValue) fromDatePicker.Value = FromDateTime.Value;
			toCombo.SelectedIndex = ToDateTime.HasValue ? 1 : 0;
			if (ToDateTime.HasValue) toDatePicker.Value = ToDateTime.Value;
		}

		private void fromCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			fromDatePicker.Enabled = fromCombo.SelectedIndex != 0;
		}

		private void toCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			toDatePicker.Enabled = toCombo.SelectedIndex != 0;
		}
	}
}
