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
			this.panel2.AutoSize = true;
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.Controls.Add(this.dailyRecurNumUpDn);
			this.panel2.Controls.Add(this.dailyDaysLabel);
			this.panel2.Controls.Add(this.dailyRecurLabel);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 26);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(539, 26);
			this.panel2.TabIndex = 2;
			// 
			// dailyRecurNumUpDn
			// 
			this.dailyRecurNumUpDn.Location = new System.Drawing.Point(107, 0);
			this.dailyRecurNumUpDn.Name = "dailyRecurNumUpDn";
			this.dailyRecurNumUpDn.Size = new System.Drawing.Size(54, 23);
			this.dailyRecurNumUpDn.TabIndex = 4;
			this.dailyRecurNumUpDn.ValueChanged += new System.EventHandler(this.dailyRecurNumUpDn_ValueChanged);
			// 
			// dailyDaysLabel
			// 
			this.dailyDaysLabel.AutoSize = true;
			this.dailyDaysLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dailyDaysLabel.Location = new System.Drawing.Point(179, 2);
			this.dailyDaysLabel.Name = "dailyDaysLabel";
			this.dailyDaysLabel.Size = new System.Drawing.Size(31, 15);
			this.dailyDaysLabel.TabIndex = 5;
			this.dailyDaysLabel.Text = "days";
			// 
			// dailyRecurLabel
			// 
			this.dailyRecurLabel.AutoSize = true;
			this.dailyRecurLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dailyRecurLabel.Location = new System.Drawing.Point(4, 2);
			this.dailyRecurLabel.Name = "dailyRecurLabel";
			this.dailyRecurLabel.Size = new System.Drawing.Size(71, 15);
			this.dailyRecurLabel.TabIndex = 3;
			this.dailyRecurLabel.Text = "Recur every:";
			// 
			// DailyTriggerUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Name = "DailyTriggerUI";
			this.Size = new System.Drawing.Size(539, 55);
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
