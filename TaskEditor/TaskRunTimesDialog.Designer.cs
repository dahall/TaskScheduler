namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskRunTimesDialog
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.startDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.endDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// startDatePicker
			// 
			this.startDatePicker.AutoSize = true;
			this.startDatePicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.startDatePicker.Location = new System.Drawing.Point(51, 12);
			this.startDatePicker.Name = "startDatePicker";
			this.startDatePicker.Size = new System.Drawing.Size(206, 20);
			this.startDatePicker.TabIndex = 7;
			this.startDatePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.ConvertLocalToUtc;
			this.startDatePicker.UTCPrompt = null;
			this.startDatePicker.Value = new System.DateTime(2009, 7, 30, 14, 15, 27, 75);
			this.startDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// endDatePicker
			// 
			this.endDatePicker.AutoSize = true;
			this.endDatePicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.endDatePicker.Location = new System.Drawing.Point(51, 38);
			this.endDatePicker.Name = "endDatePicker";
			this.endDatePicker.Size = new System.Drawing.Size(206, 20);
			this.endDatePicker.TabIndex = 7;
			this.endDatePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.ConvertLocalToUtc;
			this.endDatePicker.UTCPrompt = null;
			this.endDatePicker.Value = new System.DateTime(2009, 7, 30, 14, 15, 27, 75);
			this.endDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "From:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "To:";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(182, 259);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(15, 65);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(242, 186);
			this.listBox1.TabIndex = 10;
			// 
			// TaskRunTimesDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(269, 294);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.endDatePicker);
			this.Controls.Add(this.startDatePicker);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TaskRunTimesDialog";
			this.Text = "Task Run Times";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FullDateTimePicker startDatePicker;
		private FullDateTimePicker endDatePicker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ListBox listBox1;
	}
}