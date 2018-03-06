namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class BaseTriggerUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseTriggerUI));
			this.schedStartDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.panel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// schedStartDatePicker
			// 
			resources.ApplyResources(this.schedStartDatePicker, "schedStartDatePicker");
			this.schedStartDatePicker.Name = "schedStartDatePicker";
			this.schedStartDatePicker.ValueChanged += new System.EventHandler(this.schedStartDatePicker_ValueChanged);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.schedStartDatePicker);
			this.panel1.Name = "panel1";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// BaseTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "BaseTriggerUI";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FullDateTimePicker schedStartDatePicker;
		private System.Windows.Forms.FlowLayoutPanel panel1;
		private System.Windows.Forms.Label label1;
	}
}
