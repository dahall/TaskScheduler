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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskRunTimesControl));
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.endDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.startDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			resources.ApplyResources(this.listBox1, "listBox1");
			this.listBox1.FormatString = "F";
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Name = "listBox1";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// endDatePicker
			// 
			resources.ApplyResources(this.endDatePicker, "endDatePicker");
			this.endDatePicker.Name = "endDatePicker";
			this.endDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// startDatePicker
			// 
			resources.ApplyResources(this.startDatePicker, "startDatePicker");
			this.startDatePicker.Name = "startDatePicker";
			this.startDatePicker.ValueChanged += new System.EventHandler(this.dateValueChanged);
			// 
			// TaskRunTimesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.endDatePicker);
			this.Controls.Add(this.startDatePicker);
			this.Name = "TaskRunTimesControl";
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
