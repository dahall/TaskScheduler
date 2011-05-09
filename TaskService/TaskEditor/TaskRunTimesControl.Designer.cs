namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskRunTimesControl
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.endDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.startDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.FormatString = "F";
			this.listBox1.FormattingEnabled = true;
			this.listBox1.IntegralHeight = false;
			this.listBox1.Location = new System.Drawing.Point(0, 53);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(245, 132);
			this.listBox1.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(-3, 29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "To:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "From:";
			// 
			// endDatePicker
			// 
			this.endDatePicker.Location = new System.Drawing.Point(36, 26);
			this.endDatePicker.Name = "endDatePicker";
			this.endDatePicker.Size = new System.Drawing.Size(206, 20);
			this.endDatePicker.TabIndex = 11;
			this.endDatePicker.UTCPrompt = null;
			this.endDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// startDatePicker
			// 
			this.startDatePicker.Location = new System.Drawing.Point(36, 0);
			this.startDatePicker.Name = "startDatePicker";
			this.startDatePicker.Size = new System.Drawing.Size(206, 20);
			this.startDatePicker.TabIndex = 12;
			this.startDatePicker.UTCPrompt = null;
			this.startDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// TaskRunTimesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.endDatePicker);
			this.Controls.Add(this.startDatePicker);
			this.Name = "TaskRunTimesControl";
			this.Size = new System.Drawing.Size(245, 185);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private FullDateTimePicker endDatePicker;
		private FullDateTimePicker startDatePicker;
	}
}
