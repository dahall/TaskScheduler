namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class DailyTriggerUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyTriggerUI));
			this.panel2 = new System.Windows.Forms.Panel();
			this.dailyRecurNumUpDn = new System.Windows.Forms.NumericUpDown();
			this.dailyDaysLabel = new System.Windows.Forms.Label();
			this.dailyRecurLabel = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dailyRecurNumUpDn)).BeginInit();
			this.SuspendLayout();
			// 
			// panel2
			// 
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Controls.Add(this.dailyRecurNumUpDn);
			this.panel2.Controls.Add(this.dailyDaysLabel);
			this.panel2.Controls.Add(this.dailyRecurLabel);
			this.panel2.Name = "panel2";
			// 
			// dailyRecurNumUpDn
			// 
			resources.ApplyResources(this.dailyRecurNumUpDn, "dailyRecurNumUpDn");
			this.dailyRecurNumUpDn.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
			this.dailyRecurNumUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dailyRecurNumUpDn.Name = "dailyRecurNumUpDn";
			this.dailyRecurNumUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dailyRecurNumUpDn.ValueChanged += new System.EventHandler(this.dailyRecurNumUpDn_ValueChanged);
			this.dailyRecurNumUpDn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dailyRecurNumUpDn_KeyPress);
			// 
			// dailyDaysLabel
			// 
			resources.ApplyResources(this.dailyDaysLabel, "dailyDaysLabel");
			this.dailyDaysLabel.Name = "dailyDaysLabel";
			// 
			// dailyRecurLabel
			// 
			resources.ApplyResources(this.dailyRecurLabel, "dailyRecurLabel");
			this.dailyRecurLabel.Name = "dailyRecurLabel";
			// 
			// DailyTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Name = "DailyTriggerUI";
			this.Controls.SetChildIndex(this.panel2, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dailyRecurNumUpDn)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown dailyRecurNumUpDn;
		private System.Windows.Forms.Label dailyDaysLabel;
		private System.Windows.Forms.Label dailyRecurLabel;
	}
}
