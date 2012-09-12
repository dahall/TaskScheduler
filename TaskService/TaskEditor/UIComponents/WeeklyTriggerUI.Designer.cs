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
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.weeklySunCheck, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyMonCheck, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyTueCheck, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyWedCheck, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyThuCheck, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklyFriCheck, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklySatCheck, 2, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 31);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(481, 53);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// weeklySunCheck
			// 
			this.weeklySunCheck.AutoSize = true;
			this.weeklySunCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklySunCheck.Location = new System.Drawing.Point(3, 3);
			this.weeklySunCheck.Name = "weeklySunCheck";
			this.weeklySunCheck.Size = new System.Drawing.Size(65, 19);
			this.weeklySunCheck.TabIndex = 0;
			this.weeklySunCheck.Text = "Sunday";
			this.weeklySunCheck.UseVisualStyleBackColor = true;
			this.weeklySunCheck.CheckedChanged += new System.EventHandler(this.weeklySunCheck_CheckedChanged);
			// 
			// weeklyMonCheck
			// 
			this.weeklyMonCheck.AutoSize = true;
			this.weeklyMonCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyMonCheck.Location = new System.Drawing.Point(123, 3);
			this.weeklyMonCheck.Name = "weeklyMonCheck";
			this.weeklyMonCheck.Size = new System.Drawing.Size(70, 19);
			this.weeklyMonCheck.TabIndex = 1;
			this.weeklyMonCheck.Text = "Monday";
			this.weeklyMonCheck.UseVisualStyleBackColor = true;
			this.weeklyMonCheck.CheckedChanged += new System.EventHandler(this.weeklyMonCheck_CheckedChanged);
			// 
			// weeklyTueCheck
			// 
			this.weeklyTueCheck.AutoSize = true;
			this.weeklyTueCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyTueCheck.Location = new System.Drawing.Point(243, 3);
			this.weeklyTueCheck.Name = "weeklyTueCheck";
			this.weeklyTueCheck.Size = new System.Drawing.Size(70, 19);
			this.weeklyTueCheck.TabIndex = 2;
			this.weeklyTueCheck.Text = "Tuesday";
			this.weeklyTueCheck.UseVisualStyleBackColor = true;
			this.weeklyTueCheck.CheckedChanged += new System.EventHandler(this.weeklyTueCheck_CheckedChanged);
			// 
			// weeklyWedCheck
			// 
			this.weeklyWedCheck.AutoSize = true;
			this.weeklyWedCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyWedCheck.Location = new System.Drawing.Point(363, 3);
			this.weeklyWedCheck.Name = "weeklyWedCheck";
			this.weeklyWedCheck.Size = new System.Drawing.Size(87, 19);
			this.weeklyWedCheck.TabIndex = 3;
			this.weeklyWedCheck.Text = "Wednesday";
			this.weeklyWedCheck.UseVisualStyleBackColor = true;
			this.weeklyWedCheck.CheckedChanged += new System.EventHandler(this.weeklyWedCheck_CheckedChanged);
			// 
			// weeklyThuCheck
			// 
			this.weeklyThuCheck.AutoSize = true;
			this.weeklyThuCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyThuCheck.Location = new System.Drawing.Point(3, 29);
			this.weeklyThuCheck.Name = "weeklyThuCheck";
			this.weeklyThuCheck.Size = new System.Drawing.Size(75, 19);
			this.weeklyThuCheck.TabIndex = 4;
			this.weeklyThuCheck.Text = "Thursday";
			this.weeklyThuCheck.UseVisualStyleBackColor = true;
			this.weeklyThuCheck.CheckedChanged += new System.EventHandler(this.weeklyThuCheck_CheckedChanged);
			// 
			// weeklyFriCheck
			// 
			this.weeklyFriCheck.AutoSize = true;
			this.weeklyFriCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyFriCheck.Location = new System.Drawing.Point(123, 29);
			this.weeklyFriCheck.Name = "weeklyFriCheck";
			this.weeklyFriCheck.Size = new System.Drawing.Size(58, 19);
			this.weeklyFriCheck.TabIndex = 5;
			this.weeklyFriCheck.Text = "Friday";
			this.weeklyFriCheck.UseVisualStyleBackColor = true;
			this.weeklyFriCheck.CheckedChanged += new System.EventHandler(this.weeklyFriCheck_CheckedChanged);
			// 
			// weeklySatCheck
			// 
			this.weeklySatCheck.AutoSize = true;
			this.weeklySatCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklySatCheck.Location = new System.Drawing.Point(243, 29);
			this.weeklySatCheck.Name = "weeklySatCheck";
			this.weeklySatCheck.Size = new System.Drawing.Size(72, 19);
			this.weeklySatCheck.TabIndex = 6;
			this.weeklySatCheck.Text = "Saturday";
			this.weeklySatCheck.UseVisualStyleBackColor = true;
			this.weeklySatCheck.CheckedChanged += new System.EventHandler(this.weeklySatCheck_CheckedChanged);
			// 
			// weeklyRecurNumUpDn
			// 
			this.weeklyRecurNumUpDn.Location = new System.Drawing.Point(106, 0);
			this.weeklyRecurNumUpDn.Name = "weeklyRecurNumUpDn";
			this.weeklyRecurNumUpDn.Size = new System.Drawing.Size(54, 23);
			this.weeklyRecurNumUpDn.TabIndex = 5;
			this.weeklyRecurNumUpDn.ValueChanged += new System.EventHandler(this.weeklyRecurNumUpDn_ValueChanged);
			// 
			// weeklyOnWeeksLabel
			// 
			this.weeklyOnWeeksLabel.AutoSize = true;
			this.weeklyOnWeeksLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyOnWeeksLabel.Location = new System.Drawing.Point(178, 2);
			this.weeklyOnWeeksLabel.Name = "weeklyOnWeeksLabel";
			this.weeklyOnWeeksLabel.Size = new System.Drawing.Size(59, 15);
			this.weeklyOnWeeksLabel.TabIndex = 6;
			this.weeklyOnWeeksLabel.Text = "weeks on:";
			// 
			// weeklyRecurLabel
			// 
			this.weeklyRecurLabel.AutoSize = true;
			this.weeklyRecurLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.weeklyRecurLabel.Location = new System.Drawing.Point(3, 2);
			this.weeklyRecurLabel.Name = "weeklyRecurLabel";
			this.weeklyRecurLabel.Size = new System.Drawing.Size(71, 15);
			this.weeklyRecurLabel.TabIndex = 4;
			this.weeklyRecurLabel.Text = "Recur every:";
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.Controls.Add(this.weeklyRecurLabel);
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Controls.Add(this.weeklyOnWeeksLabel);
			this.panel2.Controls.Add(this.weeklyRecurNumUpDn);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 26);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(539, 87);
			this.panel2.TabIndex = 8;
			// 
			// WeeklyTriggerUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.Controls.Add(this.panel2);
			this.Name = "WeeklyTriggerUI";
			this.Size = new System.Drawing.Size(539, 117);
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
