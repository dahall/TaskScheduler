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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyTriggerUI));
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
			resources.ApplyResources(this.monthlyOnRadio, "monthlyOnRadio");
			this.monthlyOnRadio.Name = "monthlyOnRadio";
			this.monthlyOnRadio.UseVisualStyleBackColor = true;
			// 
			// monthlyDaysRadio
			// 
			resources.ApplyResources(this.monthlyDaysRadio, "monthlyDaysRadio");
			this.monthlyDaysRadio.Checked = true;
			this.monthlyDaysRadio.Name = "monthlyDaysRadio";
			this.monthlyDaysRadio.TabStop = true;
			this.monthlyDaysRadio.UseVisualStyleBackColor = true;
			this.monthlyDaysRadio.CheckedChanged += new System.EventHandler(this.monthlyDaysRadio_CheckedChanged);
			// 
			// monthlyMonthsLabel
			// 
			resources.ApplyResources(this.monthlyMonthsLabel, "monthlyMonthsLabel");
			this.monthlyMonthsLabel.Name = "monthlyMonthsLabel";
			// 
			// monthlyDaysDropDown
			// 
			resources.ApplyResources(this.monthlyDaysDropDown, "monthlyDaysDropDown");
			this.monthlyDaysDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyDaysDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyDaysDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyDaysDropDown.Name = "monthlyDaysDropDown";
			this.monthlyDaysDropDown.RequireAtLeastOneCheckedItem = true;
			this.monthlyDaysDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyDaysDropDown_SelectedItemsChanged);
			// 
			// monthlyOnDOWDropDown
			// 
			resources.ApplyResources(this.monthlyOnDOWDropDown, "monthlyOnDOWDropDown");
			this.monthlyOnDOWDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnDOWDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnDOWDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyOnDOWDropDown.Name = "monthlyOnDOWDropDown";
			this.monthlyOnDOWDropDown.RequireAtLeastOneCheckedItem = true;
			this.monthlyOnDOWDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyOnDOWDropDown_SelectedItemsChanged);
			// 
			// monthlyOnWeekDropDown
			// 
			this.monthlyOnWeekDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnWeekDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnWeekDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.monthlyOnWeekDropDown, "monthlyOnWeekDropDown");
			this.monthlyOnWeekDropDown.Name = "monthlyOnWeekDropDown";
			this.monthlyOnWeekDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyOnWeekDropDown_SelectedItemsChanged);
			// 
			// monthlyMonthsDropDown
			// 
			resources.ApplyResources(this.monthlyMonthsDropDown, "monthlyMonthsDropDown");
			this.monthlyMonthsDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyMonthsDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyMonthsDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.monthlyMonthsDropDown.Name = "monthlyMonthsDropDown";
			this.monthlyMonthsDropDown.RequireAtLeastOneCheckedItem = true;
			this.monthlyMonthsDropDown.SelectedIndexChanged += new System.EventHandler(this.monthlyMonthsDropDown_SelectedItemsChanged);
			// 
			// panel2
			// 
			resources.ApplyResources(this.panel2, "panel2");
			this.panel2.Controls.Add(this.monthlyOnWeekDropDown);
			this.panel2.Controls.Add(this.monthlyDaysRadio);
			this.panel2.Controls.Add(this.monthlyOnDOWDropDown);
			this.panel2.Controls.Add(this.monthlyMonthsDropDown);
			this.panel2.Controls.Add(this.monthlyMonthsLabel);
			this.panel2.Controls.Add(this.monthlyOnRadio);
			this.panel2.Controls.Add(this.monthlyDaysDropDown);
			this.panel2.Name = "panel2";
			// 
			// MonthlyTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Name = "MonthlyTriggerUI";
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
