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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 2);
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
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.endDatePicker, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.startDatePicker, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// TaskRunTimesControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.tableLayoutPanel1);
			resources.ApplyResources(this, "$this");
			this.MinimumSize = new System.Drawing.Size(251, 75);
			this.Name = "TaskRunTimesControl";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private FullDateTimePicker endDatePicker;
		private FullDateTimePicker startDatePicker;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
