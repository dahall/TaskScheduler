namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class RuntimeOptionPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuntimeOptionPanel));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.taskMultInstCombo = new System.Windows.Forms.ComboBox();
			this.taskPriorityCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleLabel = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.taskDeleteAfterCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskRestartCountLabel = new System.Windows.Forms.Label();
			this.taskAllowHardTerminateCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskExecutionTimeLimitCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskDeleteAfterCombo = new System.Windows.Forms.TimeSpanPicker();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.taskRestartIntervalCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskExecutionTimeLimitCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskRestartIntervalCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskAllowDemandStartCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskStartWhenAvailableCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.taskRestartCountText = new System.Windows.Forms.NumericUpDown();
			this.taskRestartAttemptTimesLabel = new System.Windows.Forms.Label();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.taskMultInstCombo, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.taskPriorityCombo, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this.taskRunningRuleLabel, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.taskDeleteAfterCheck, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.taskRestartCountLabel, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.taskAllowHardTerminateCheck, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.taskExecutionTimeLimitCheck, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.taskDeleteAfterCombo, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.taskRestartIntervalCheck, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.taskExecutionTimeLimitCombo, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.taskRestartIntervalCombo, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.taskAllowDemandStartCheck, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.taskStartWhenAvailableCheck, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// taskMultInstCombo
			// 
			resources.ApplyResources(this.taskMultInstCombo, "taskMultInstCombo");
			this.tableLayoutPanel1.SetColumnSpan(this.taskMultInstCombo, 2);
			this.taskMultInstCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskMultInstCombo.FormattingEnabled = true;
			this.taskMultInstCombo.Name = "taskMultInstCombo";
			this.taskMultInstCombo.SelectedIndexChanged += new System.EventHandler(this.taskMultInstCombo_SelectedIndexChanged);
			// 
			// taskPriorityCombo
			// 
			resources.ApplyResources(this.taskPriorityCombo, "taskPriorityCombo");
			this.tableLayoutPanel1.SetColumnSpan(this.taskPriorityCombo, 2);
			this.taskPriorityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskPriorityCombo.Name = "taskPriorityCombo";
			this.taskPriorityCombo.SelectedIndexChanged += new System.EventHandler(this.taskPriorityCombo_SelectedIndexChanged);
			// 
			// taskRunningRuleLabel
			// 
			resources.ApplyResources(this.taskRunningRuleLabel, "taskRunningRuleLabel");
			this.tableLayoutPanel1.SetColumnSpan(this.taskRunningRuleLabel, 2);
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// taskDeleteAfterCheck
			// 
			resources.ApplyResources(this.taskDeleteAfterCheck, "taskDeleteAfterCheck");
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new System.EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskRestartCountLabel
			// 
			resources.ApplyResources(this.taskRestartCountLabel, "taskRestartCountLabel");
			this.taskRestartCountLabel.Name = "taskRestartCountLabel";
			// 
			// taskAllowHardTerminateCheck
			// 
			resources.ApplyResources(this.taskAllowHardTerminateCheck, "taskAllowHardTerminateCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskAllowHardTerminateCheck, 2);
			this.taskAllowHardTerminateCheck.Name = "taskAllowHardTerminateCheck";
			this.taskAllowHardTerminateCheck.UseVisualStyleBackColor = true;
			this.taskAllowHardTerminateCheck.CheckedChanged += new System.EventHandler(this.taskAllowHardTerminateCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCheck
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCheck, "taskExecutionTimeLimitCheck");
			this.taskExecutionTimeLimitCheck.Name = "taskExecutionTimeLimitCheck";
			this.taskExecutionTimeLimitCheck.UseVisualStyleBackColor = true;
			this.taskExecutionTimeLimitCheck.CheckedChanged += new System.EventHandler(this.taskExecutionTimeLimitCheck_CheckedChanged);
			// 
			// taskDeleteAfterCombo
			// 
			resources.ApplyResources(this.taskDeleteAfterCombo, "taskDeleteAfterCombo");
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			this.taskDeleteAfterCombo.ValueChanged += new System.EventHandler(this.taskDeleteAfterCombo_ValueChanged);
			// 
			// optionPanelHeaderLabel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel1, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel1, "optionPanelHeaderLabel1");
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			// 
			// taskRestartIntervalCheck
			// 
			resources.ApplyResources(this.taskRestartIntervalCheck, "taskRestartIntervalCheck");
			this.taskRestartIntervalCheck.Name = "taskRestartIntervalCheck";
			this.taskRestartIntervalCheck.UseVisualStyleBackColor = true;
			this.taskRestartIntervalCheck.CheckedChanged += new System.EventHandler(this.taskRestartIntervalCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCombo
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCombo, "taskExecutionTimeLimitCombo");
			this.taskExecutionTimeLimitCombo.Name = "taskExecutionTimeLimitCombo";
			this.taskExecutionTimeLimitCombo.ValueChanged += new System.EventHandler(this.taskExecutionTimeLimitCombo_ValueChanged);
			// 
			// taskRestartIntervalCombo
			// 
			resources.ApplyResources(this.taskRestartIntervalCombo, "taskRestartIntervalCombo");
			this.taskRestartIntervalCombo.Name = "taskRestartIntervalCombo";
			this.taskRestartIntervalCombo.ValueChanged += new System.EventHandler(this.taskRestartIntervalCombo_ValueChanged);
			// 
			// taskAllowDemandStartCheck
			// 
			resources.ApplyResources(this.taskAllowDemandStartCheck, "taskAllowDemandStartCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskAllowDemandStartCheck, 2);
			this.taskAllowDemandStartCheck.Name = "taskAllowDemandStartCheck";
			this.taskAllowDemandStartCheck.UseVisualStyleBackColor = true;
			this.taskAllowDemandStartCheck.CheckedChanged += new System.EventHandler(this.taskAllowDemandStartCheck_CheckedChanged);
			// 
			// taskStartWhenAvailableCheck
			// 
			resources.ApplyResources(this.taskStartWhenAvailableCheck, "taskStartWhenAvailableCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskStartWhenAvailableCheck, 2);
			this.taskStartWhenAvailableCheck.Name = "taskStartWhenAvailableCheck";
			this.taskStartWhenAvailableCheck.UseVisualStyleBackColor = true;
			this.taskStartWhenAvailableCheck.CheckedChanged += new System.EventHandler(this.taskStartWhenAvailableCheck_CheckedChanged);
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.flowLayoutPanel1.Controls.Add(this.taskRestartCountText);
			this.flowLayoutPanel1.Controls.Add(this.taskRestartAttemptTimesLabel);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			// 
			// taskRestartCountText
			// 
			resources.ApplyResources(this.taskRestartCountText, "taskRestartCountText");
			this.taskRestartCountText.Name = "taskRestartCountText";
			this.taskRestartCountText.ValueChanged += new System.EventHandler(this.taskRestartCountText_ValueChanged);
			// 
			// taskRestartAttemptTimesLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptTimesLabel, "taskRestartAttemptTimesLabel");
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			// 
			// RuntimeOptionPanel
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "RuntimeOptionPanel";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private System.Windows.Forms.HelpProvider helpProvider;
		private WrappingCheckBox taskAllowDemandStartCheck;
		private WrappingCheckBox taskStartWhenAvailableCheck;
		private WrappingCheckBox taskRestartIntervalCheck;
		private System.Windows.Forms.TimeSpanPicker taskRestartIntervalCombo;
		private System.Windows.Forms.Label taskRestartCountLabel;
		private System.Windows.Forms.NumericUpDown taskRestartCountText;
		private WrappingCheckBox taskExecutionTimeLimitCheck;
		private System.Windows.Forms.TimeSpanPicker taskExecutionTimeLimitCombo;
		private WrappingCheckBox taskAllowHardTerminateCheck;
		private WrappingCheckBox taskDeleteAfterCheck;
		private System.Windows.Forms.TimeSpanPicker taskDeleteAfterCombo;
		private System.Windows.Forms.Label taskRunningRuleLabel;
		private System.Windows.Forms.ComboBox taskMultInstCombo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox taskPriorityCombo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label taskRestartAttemptTimesLabel;
	}
}
