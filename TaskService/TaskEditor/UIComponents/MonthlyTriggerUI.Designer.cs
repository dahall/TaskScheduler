namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class MonthlyTriggerUI
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
			this.monthlyOnRadio = new System.Windows.Forms.RadioButton();
			this.monthlyDaysRadio = new System.Windows.Forms.RadioButton();
			this.monthlyMonthsLabel = new System.Windows.Forms.Label();
			this.monthlyDaysDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyOnDOWDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyOnWeekDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyMonthsDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// monthlyOnRadio
			// 
			this.monthlyOnRadio.AutoSize = true;
			this.monthlyOnRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.monthlyOnRadio.Location = new System.Drawing.Point(7, 59);
			this.monthlyOnRadio.Name = "monthlyOnRadio";
			this.monthlyOnRadio.Size = new System.Drawing.Size(44, 19);
			this.monthlyOnRadio.TabIndex = 11;
			this.monthlyOnRadio.Text = "On:";
			this.monthlyOnRadio.UseVisualStyleBackColor = true;
			// 
			// monthlyDaysRadio
			// 
			this.monthlyDaysRadio.AutoSize = true;
			this.monthlyDaysRadio.Checked = true;
			this.monthlyDaysRadio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.monthlyDaysRadio.Location = new System.Drawing.Point(7, 30);
			this.monthlyDaysRadio.Name = "monthlyDaysRadio";
			this.monthlyDaysRadio.Size = new System.Drawing.Size(53, 19);
			this.monthlyDaysRadio.TabIndex = 9;
			this.monthlyDaysRadio.TabStop = true;
			this.monthlyDaysRadio.Text = "Days:";
			this.monthlyDaysRadio.UseVisualStyleBackColor = true;
			this.monthlyDaysRadio.CheckedChanged += new System.EventHandler(this.monthlyDaysRadio_CheckedChanged);
			// 
			// monthlyMonthsLabel
			// 
			this.monthlyMonthsLabel.AutoSize = true;
			this.monthlyMonthsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.monthlyMonthsLabel.Location = new System.Drawing.Point(4, 3);
			this.monthlyMonthsLabel.Name = "monthlyMonthsLabel";
			this.monthlyMonthsLabel.Size = new System.Drawing.Size(51, 15);
			this.monthlyMonthsLabel.TabIndex = 7;
			this.monthlyMonthsLabel.Text = "Months:";
			// 
			// monthlyDaysDropDown
			// 
			this.monthlyDaysDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.monthlyDaysDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyDaysDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyDaysDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyDaysDropDown.Location = new System.Drawing.Point(74, 29);
			this.monthlyDaysDropDown.Name = "monthlyDaysDropDown";
			this.monthlyDaysDropDown.Size = new System.Drawing.Size(448, 23);
			this.monthlyDaysDropDown.TabIndex = 10;
			this.monthlyDaysDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyDaysDropDown_SelectedItemsChanged);
			// 
			// monthlyOnDOWDropDown
			// 
			this.monthlyOnDOWDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.monthlyOnDOWDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnDOWDropDown.CheckAllText = "<Select all days>";
			this.monthlyOnDOWDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnDOWDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyOnDOWDropDown.Location = new System.Drawing.Point(307, 58);
			this.monthlyOnDOWDropDown.Name = "monthlyOnDOWDropDown";
			this.monthlyOnDOWDropDown.Size = new System.Drawing.Size(215, 23);
			this.monthlyOnDOWDropDown.TabIndex = 13;
			this.monthlyOnDOWDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyOnDOWDropDown_SelectedItemsChanged);
			// 
			// monthlyOnWeekDropDown
			// 
			this.monthlyOnWeekDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnWeekDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnWeekDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyOnWeekDropDown.Location = new System.Drawing.Point(74, 58);
			this.monthlyOnWeekDropDown.Name = "monthlyOnWeekDropDown";
			this.monthlyOnWeekDropDown.Size = new System.Drawing.Size(227, 23);
			this.monthlyOnWeekDropDown.TabIndex = 12;
			this.monthlyOnWeekDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyOnWeekDropDown_SelectedItemsChanged);
			// 
			// monthlyMonthsDropDown
			// 
			this.monthlyMonthsDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.monthlyMonthsDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyMonthsDropDown.CheckAllText = "<Select all months>";
			this.monthlyMonthsDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyMonthsDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyMonthsDropDown.Location = new System.Drawing.Point(73, 0);
			this.monthlyMonthsDropDown.Name = "monthlyMonthsDropDown";
			this.monthlyMonthsDropDown.Size = new System.Drawing.Size(448, 23);
			this.monthlyMonthsDropDown.TabIndex = 8;
			this.monthlyMonthsDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyMonthsDropDown_SelectedItemsChanged);
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.Controls.Add(this.monthlyOnWeekDropDown);
			this.panel2.Controls.Add(this.monthlyDaysRadio);
			this.panel2.Controls.Add(this.monthlyOnDOWDropDown);
			this.panel2.Controls.Add(this.monthlyMonthsDropDown);
			this.panel2.Controls.Add(this.monthlyMonthsLabel);
			this.panel2.Controls.Add(this.monthlyOnRadio);
			this.panel2.Controls.Add(this.monthlyDaysDropDown);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 26);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(522, 84);
			this.panel2.TabIndex = 14;
			// 
			// MonthlyTriggerUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.panel2);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "MonthlyTriggerUI";
			this.Size = new System.Drawing.Size(522, 112);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton monthlyOnRadio;
		private System.Windows.Forms.RadioButton monthlyDaysRadio;
		private System.Windows.Forms.Label monthlyMonthsLabel;
		private DropDownCheckList monthlyDaysDropDown;
		private DropDownCheckList monthlyOnDOWDropDown;
		private DropDownCheckList monthlyOnWeekDropDown;
		private DropDownCheckList monthlyMonthsDropDown;
		private System.Windows.Forms.Panel panel2;
	}
}
