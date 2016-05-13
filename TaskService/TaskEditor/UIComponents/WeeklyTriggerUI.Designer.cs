namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class WeeklyTriggerUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeeklyTriggerUI));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.weeklySunCheck = new System.Windows.Forms.CheckBox();
			this.weeklyMonCheck = new System.Windows.Forms.CheckBox();
			this.weeklyTueCheck = new System.Windows.Forms.CheckBox();
			this.weeklyWedCheck = new System.Windows.Forms.CheckBox();
			this.weeklyThuCheck = new System.Windows.Forms.CheckBox();
			this.weeklyFriCheck = new System.Windows.Forms.CheckBox();
			this.weeklySatCheck = new System.Windows.Forms.CheckBox();
			this.weeklyRecurNumUpDn = new System.Windows.Forms.NumericUpDown();
			this.weeklyOnWeeksLabel = new System.Windows.Forms.Label();
			this.weeklyRecurLabel = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.weeklyRecurNumUpDn)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.weeklySunCheck, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyMonCheck, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyTueCheck, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyWedCheck, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyThuCheck, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklyFriCheck, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklySatCheck, 2, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// weeklySunCheck
			// 
			resources.ApplyResources(this.weeklySunCheck, "weeklySunCheck");
			this.weeklySunCheck.Name = "weeklySunCheck";
			this.weeklySunCheck.UseVisualStyleBackColor = true;
			this.weeklySunCheck.CheckedChanged += new System.EventHandler(this.weeklySunCheck_CheckedChanged);
			// 
			// weeklyMonCheck
			// 
			resources.ApplyResources(this.weeklyMonCheck, "weeklyMonCheck");
			this.weeklyMonCheck.Name = "weeklyMonCheck";
			this.weeklyMonCheck.UseVisualStyleBackColor = true;
			this.weeklyMonCheck.CheckedChanged += new System.EventHandler(this.weeklyMonCheck_CheckedChanged);
			// 
			// weeklyTueCheck
			// 
			resources.ApplyResources(this.weeklyTueCheck, "weeklyTueCheck");
			this.weeklyTueCheck.Name = "weeklyTueCheck";
			this.weeklyTueCheck.UseVisualStyleBackColor = true;
			this.weeklyTueCheck.CheckedChanged += new System.EventHandler(this.weeklyTueCheck_CheckedChanged);
			// 
			// weeklyWedCheck
			// 
			resources.ApplyResources(this.weeklyWedCheck, "weeklyWedCheck");
			this.weeklyWedCheck.Name = "weeklyWedCheck";
			this.weeklyWedCheck.UseVisualStyleBackColor = true;
			this.weeklyWedCheck.CheckedChanged += new System.EventHandler(this.weeklyWedCheck_CheckedChanged);
			// 
			// weeklyThuCheck
			// 
			resources.ApplyResources(this.weeklyThuCheck, "weeklyThuCheck");
			this.weeklyThuCheck.Name = "weeklyThuCheck";
			this.weeklyThuCheck.UseVisualStyleBackColor = true;
			this.weeklyThuCheck.CheckedChanged += new System.EventHandler(this.weeklyThuCheck_CheckedChanged);
			// 
			// weeklyFriCheck
			// 
			resources.ApplyResources(this.weeklyFriCheck, "weeklyFriCheck");
			this.weeklyFriCheck.Name = "weeklyFriCheck";
			this.weeklyFriCheck.UseVisualStyleBackColor = true;
			this.weeklyFriCheck.CheckedChanged += new System.EventHandler(this.weeklyFriCheck_CheckedChanged);
			// 
			// weeklySatCheck
			// 
			resources.ApplyResources(this.weeklySatCheck, "weeklySatCheck");
			this.weeklySatCheck.Name = "weeklySatCheck";
			this.weeklySatCheck.UseVisualStyleBackColor = true;
			this.weeklySatCheck.CheckedChanged += new System.EventHandler(this.weeklySatCheck_CheckedChanged);
			// 
			// weeklyRecurNumUpDn
			// 
			resources.ApplyResources(this.weeklyRecurNumUpDn, "weeklyRecurNumUpDn");
			this.weeklyRecurNumUpDn.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
			this.weeklyRecurNumUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.weeklyRecurNumUpDn.Name = "weeklyRecurNumUpDn";
			this.weeklyRecurNumUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.weeklyRecurNumUpDn.ValueChanged += new System.EventHandler(this.weeklyRecurNumUpDn_ValueChanged);
			this.weeklyRecurNumUpDn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.weeklyRecurNumUpDn_KeyPress);
			// 
			// weeklyOnWeeksLabel
			// 
			resources.ApplyResources(this.weeklyOnWeeksLabel, "weeklyOnWeeksLabel");
			this.weeklyOnWeeksLabel.Name = "weeklyOnWeeksLabel";
			// 
			// weeklyRecurLabel
			// 
			resources.ApplyResources(this.weeklyRecurLabel, "weeklyRecurLabel");
			this.weeklyRecurLabel.Name = "weeklyRecurLabel";
			// 
			// panel2
			// 
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Controls.Add(this.weeklyRecurLabel);
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Controls.Add(this.weeklyOnWeeksLabel);
			this.panel2.Controls.Add(this.weeklyRecurNumUpDn);
			this.panel2.Name = "panel2";
			// 
			// WeeklyTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.panel2);
			this.Name = "WeeklyTriggerUI";
			this.Controls.SetChildIndex(this.panel2, 0);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.weeklyRecurNumUpDn)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox weeklySunCheck;
		private System.Windows.Forms.CheckBox weeklyMonCheck;
		private System.Windows.Forms.CheckBox weeklyTueCheck;
		private System.Windows.Forms.CheckBox weeklyWedCheck;
		private System.Windows.Forms.CheckBox weeklyThuCheck;
		private System.Windows.Forms.CheckBox weeklyFriCheck;
		private System.Windows.Forms.CheckBox weeklySatCheck;
		private System.Windows.Forms.NumericUpDown weeklyRecurNumUpDn;
		private System.Windows.Forms.Label weeklyOnWeeksLabel;
		private System.Windows.Forms.Label weeklyRecurLabel;
		private System.Windows.Forms.Panel panel2;
	}
}
