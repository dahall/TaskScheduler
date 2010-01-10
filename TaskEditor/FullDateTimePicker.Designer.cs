namespace Microsoft.Win32.TaskScheduler
{
	internal partial class FullDateTimePicker
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullDateTimePicker));
			this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			this.tableLayoutPanelFullDateTime = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
			this.utcCheckBox = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanelFullDateTime.SuspendLayout();
			this.SuspendLayout();
			// 
			// dateTimePickerDate
			// 
			this.dateTimePickerDate.AccessibleDescription = null;
			this.dateTimePickerDate.AccessibleName = null;
			resources.ApplyResources(this.dateTimePickerDate, "dateTimePickerDate");
			this.dateTimePickerDate.BackgroundImage = null;
			this.dateTimePickerDate.CalendarFont = null;
			this.dateTimePickerDate.CustomFormat = null;
			this.dateTimePickerDate.Font = null;
			this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerDate.Name = "dateTimePickerDate";
			this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.subControl_ValueChanged);
			// 
			// tableLayoutPanelFullDateTime
			// 
			this.tableLayoutPanelFullDateTime.AccessibleDescription = null;
			this.tableLayoutPanelFullDateTime.AccessibleName = null;
			resources.ApplyResources(this.tableLayoutPanelFullDateTime, "tableLayoutPanelFullDateTime");
			this.tableLayoutPanelFullDateTime.BackgroundImage = null;
			this.tableLayoutPanelFullDateTime.Controls.Add(this.dateTimePickerDate, 0, 0);
			this.tableLayoutPanelFullDateTime.Controls.Add(this.dateTimePickerTime, 1, 0);
			this.tableLayoutPanelFullDateTime.Controls.Add(this.utcCheckBox, 2, 0);
			this.tableLayoutPanelFullDateTime.Font = null;
			this.tableLayoutPanelFullDateTime.Name = "tableLayoutPanelFullDateTime";
			// 
			// dateTimePickerTime
			// 
			this.dateTimePickerTime.AccessibleDescription = null;
			this.dateTimePickerTime.AccessibleName = null;
			resources.ApplyResources(this.dateTimePickerTime, "dateTimePickerTime");
			this.dateTimePickerTime.BackgroundImage = null;
			this.dateTimePickerTime.CalendarFont = null;
			this.dateTimePickerTime.CustomFormat = null;
			this.dateTimePickerTime.Font = null;
			this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePickerTime.Name = "dateTimePickerTime";
			this.dateTimePickerTime.ShowUpDown = true;
			this.dateTimePickerTime.ValueChanged += new System.EventHandler(this.subControl_ValueChanged);
			// 
			// utcCheckBox
			// 
			this.utcCheckBox.AccessibleDescription = null;
			this.utcCheckBox.AccessibleName = null;
			resources.ApplyResources(this.utcCheckBox, "utcCheckBox");
			this.utcCheckBox.BackgroundImage = null;
			this.utcCheckBox.Font = null;
			this.utcCheckBox.Name = "utcCheckBox";
			this.utcCheckBox.UseVisualStyleBackColor = true;
			this.utcCheckBox.CheckedChanged += new System.EventHandler(this.subControl_ValueChanged);
			// 
			// FullDateTimePicker
			// 
			this.AccessibleDescription = null;
			this.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			this.Controls.Add(this.tableLayoutPanelFullDateTime);
			this.Font = null;
			this.Name = "FullDateTimePicker";
			this.Load += new System.EventHandler(this.FullDateTimePicker_Load);
			this.RightToLeftChanged += new System.EventHandler(this.FullDateTimePicker_RightToLeftChanged);
			this.tableLayoutPanelFullDateTime.ResumeLayout(false);
			this.tableLayoutPanelFullDateTime.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker dateTimePickerDate;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFullDateTime;
		private System.Windows.Forms.DateTimePicker dateTimePickerTime;
		private System.Windows.Forms.CheckBox utcCheckBox;
	}
}
