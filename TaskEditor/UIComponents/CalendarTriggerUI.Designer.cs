namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class CalendarTriggerUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarTriggerUI));
			this.schedStartDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.schedGroup = new System.Windows.Forms.GroupBox();
			this.schedTabControl = new System.Windows.Forms.TabControl();
			this.oneTimeTab = new System.Windows.Forms.TabPage();
			this.dailyTab = new System.Windows.Forms.TabPage();
			this.dailyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.DailyTriggerUI();
			this.weeklyTab = new System.Windows.Forms.TabPage();
			this.weeklyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.WeeklyTriggerUI();
			this.monthlyTab = new System.Windows.Forms.TabPage();
			this.monthlyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.MonthlyTriggerUI();
			this.schedStartLabel = new System.Windows.Forms.Label();
			this.schedMonthlyRadio = new System.Windows.Forms.RadioButton();
			this.schedWeeklyRadio = new System.Windows.Forms.RadioButton();
			this.schedDailyRadio = new System.Windows.Forms.RadioButton();
			this.schedOneRadio = new System.Windows.Forms.RadioButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.schedGroup.SuspendLayout();
			this.schedTabControl.SuspendLayout();
			this.dailyTab.SuspendLayout();
			this.weeklyTab.SuspendLayout();
			this.monthlyTab.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// schedStartDatePicker
			// 
			resources.ApplyResources(this.schedStartDatePicker, "schedStartDatePicker");
			this.schedStartDatePicker.Name = "schedStartDatePicker";
			this.schedStartDatePicker.Value = new System.DateTime(2009, 7, 30, 13, 10, 46, 672);
			this.schedStartDatePicker.ValueChanged += new System.EventHandler(this.schedStartDatePicker_ValueChanged);
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.tableLayoutPanel1.SetRowSpan(this.groupBox3, 2);
			this.groupBox3.TabStop = false;
			// 
			// schedGroup
			// 
			this.schedGroup.Controls.Add(this.schedTabControl);
			resources.ApplyResources(this.schedGroup, "schedGroup");
			this.schedGroup.Name = "schedGroup";
			this.schedGroup.TabStop = false;
			// 
			// schedTabControl
			// 
			resources.ApplyResources(this.schedTabControl, "schedTabControl");
			this.schedTabControl.Controls.Add(this.oneTimeTab);
			this.schedTabControl.Controls.Add(this.dailyTab);
			this.schedTabControl.Controls.Add(this.weeklyTab);
			this.schedTabControl.Controls.Add(this.monthlyTab);
			this.schedTabControl.Name = "schedTabControl";
			this.schedTabControl.SelectedIndex = 0;
			this.schedTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.schedTabControl.TabStop = false;
			// 
			// oneTimeTab
			// 
			resources.ApplyResources(this.oneTimeTab, "oneTimeTab");
			this.oneTimeTab.Name = "oneTimeTab";
			this.oneTimeTab.UseVisualStyleBackColor = true;
			// 
			// dailyTab
			// 
			this.dailyTab.Controls.Add(this.dailyTriggerUI1);
			resources.ApplyResources(this.dailyTab, "dailyTab");
			this.dailyTab.Name = "dailyTab";
			this.dailyTab.UseVisualStyleBackColor = true;
			// 
			// dailyTriggerUI1
			// 
			resources.ApplyResources(this.dailyTriggerUI1, "dailyTriggerUI1");
			this.dailyTriggerUI1.Name = "dailyTriggerUI1";
			this.dailyTriggerUI1.ShowStartBoundary = false;
			// 
			// weeklyTab
			// 
			this.weeklyTab.Controls.Add(this.weeklyTriggerUI1);
			resources.ApplyResources(this.weeklyTab, "weeklyTab");
			this.weeklyTab.Name = "weeklyTab";
			this.weeklyTab.UseVisualStyleBackColor = true;
			// 
			// weeklyTriggerUI1
			// 
			resources.ApplyResources(this.weeklyTriggerUI1, "weeklyTriggerUI1");
			this.weeklyTriggerUI1.Name = "weeklyTriggerUI1";
			this.weeklyTriggerUI1.ShowStartBoundary = false;
			// 
			// monthlyTab
			// 
			this.monthlyTab.Controls.Add(this.monthlyTriggerUI1);
			resources.ApplyResources(this.monthlyTab, "monthlyTab");
			this.monthlyTab.Name = "monthlyTab";
			this.monthlyTab.UseVisualStyleBackColor = true;
			// 
			// monthlyTriggerUI1
			// 
			resources.ApplyResources(this.monthlyTriggerUI1, "monthlyTriggerUI1");
			this.monthlyTriggerUI1.Name = "monthlyTriggerUI1";
			this.monthlyTriggerUI1.ShowStartBoundary = false;
			this.monthlyTriggerUI1.TriggerTypeChanged += new System.EventHandler(this.monthlyTriggerUI1_TriggerTypeChanged);
			// 
			// schedStartLabel
			// 
			resources.ApplyResources(this.schedStartLabel, "schedStartLabel");
			this.schedStartLabel.Name = "schedStartLabel";
			// 
			// schedMonthlyRadio
			// 
			resources.ApplyResources(this.schedMonthlyRadio, "schedMonthlyRadio");
			this.schedMonthlyRadio.Name = "schedMonthlyRadio";
			this.schedMonthlyRadio.UseVisualStyleBackColor = true;
			this.schedMonthlyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedWeeklyRadio
			// 
			resources.ApplyResources(this.schedWeeklyRadio, "schedWeeklyRadio");
			this.schedWeeklyRadio.Name = "schedWeeklyRadio";
			this.schedWeeklyRadio.UseVisualStyleBackColor = true;
			this.schedWeeklyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedDailyRadio
			// 
			resources.ApplyResources(this.schedDailyRadio, "schedDailyRadio");
			this.schedDailyRadio.Name = "schedDailyRadio";
			this.schedDailyRadio.UseVisualStyleBackColor = true;
			this.schedDailyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedOneRadio
			// 
			resources.ApplyResources(this.schedOneRadio, "schedOneRadio");
			this.schedOneRadio.Name = "schedOneRadio";
			this.schedOneRadio.UseVisualStyleBackColor = true;
			this.schedOneRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.schedGroup, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.schedStartDatePicker, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.schedOneRadio);
			this.flowLayoutPanel1.Controls.Add(this.schedDailyRadio);
			this.flowLayoutPanel1.Controls.Add(this.schedWeeklyRadio);
			this.flowLayoutPanel1.Controls.Add(this.schedMonthlyRadio);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 2);
			// 
			// CalendarTriggerUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.schedStartLabel);
			this.MinimumSize = new System.Drawing.Size(512, 161);
			this.Name = "CalendarTriggerUI";
			this.schedGroup.ResumeLayout(false);
			this.schedTabControl.ResumeLayout(false);
			this.dailyTab.ResumeLayout(false);
			this.weeklyTab.ResumeLayout(false);
			this.monthlyTab.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private FullDateTimePicker schedStartDatePicker;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox schedGroup;
		private System.Windows.Forms.TabControl schedTabControl;
		private System.Windows.Forms.TabPage oneTimeTab;
		private System.Windows.Forms.TabPage dailyTab;
		private DailyTriggerUI dailyTriggerUI1;
		private System.Windows.Forms.TabPage weeklyTab;
		private WeeklyTriggerUI weeklyTriggerUI1;
		private System.Windows.Forms.TabPage monthlyTab;
		private MonthlyTriggerUI monthlyTriggerUI1;
		private System.Windows.Forms.Label schedStartLabel;
		private System.Windows.Forms.RadioButton schedMonthlyRadio;
		private System.Windows.Forms.RadioButton schedWeeklyRadio;
		private System.Windows.Forms.RadioButton schedDailyRadio;
		private System.Windows.Forms.RadioButton schedOneRadio;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}
