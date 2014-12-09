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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.taskMultInstCombo = new System.Windows.Forms.ComboBox();
			this.taskPriorityCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleLabel = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.taskDeleteAfterCheck = new System.Windows.Forms.CheckBox();
			this.taskRestartCountLabel = new System.Windows.Forms.Label();
			this.taskAllowHardTerminateCheck = new System.Windows.Forms.CheckBox();
			this.taskExecutionTimeLimitCheck = new System.Windows.Forms.CheckBox();
			this.taskDeleteAfterCombo = new System.Windows.Forms.TimeSpanPicker();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.taskRestartIntervalCheck = new System.Windows.Forms.CheckBox();
			this.taskExecutionTimeLimitCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskRestartIntervalCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskAllowDemandStartCheck = new System.Windows.Forms.CheckBox();
			this.taskStartWhenAvailableCheck = new System.Windows.Forms.CheckBox();
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
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
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
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 12;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(342, 320);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// taskMultInstCombo
			// 
			this.taskMultInstCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.taskMultInstCombo, 2);
			this.taskMultInstCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskMultInstCombo.FormattingEnabled = true;
			this.taskMultInstCombo.Location = new System.Drawing.Point(10, 244);
			this.taskMultInstCombo.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
			this.taskMultInstCombo.Name = "taskMultInstCombo";
			this.taskMultInstCombo.Size = new System.Drawing.Size(332, 23);
			this.taskMultInstCombo.TabIndex = 14;
			this.taskMultInstCombo.SelectedIndexChanged += new System.EventHandler(this.taskMultInstCombo_SelectedIndexChanged);
			// 
			// taskPriorityCombo
			// 
			this.taskPriorityCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.taskPriorityCombo, 2);
			this.taskPriorityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskPriorityCombo.Location = new System.Drawing.Point(10, 294);
			this.taskPriorityCombo.Margin = new System.Windows.Forms.Padding(10, 3, 0, 3);
			this.taskPriorityCombo.Name = "taskPriorityCombo";
			this.taskPriorityCombo.Size = new System.Drawing.Size(332, 23);
			this.taskPriorityCombo.TabIndex = 3;
			this.taskPriorityCombo.SelectedIndexChanged += new System.EventHandler(this.taskPriorityCombo_SelectedIndexChanged);
			// 
			// taskRunningRuleLabel
			// 
			this.taskRunningRuleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskRunningRuleLabel.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskRunningRuleLabel, 2);
			this.taskRunningRuleLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskRunningRuleLabel.Location = new System.Drawing.Point(0, 223);
			this.taskRunningRuleLabel.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			this.taskRunningRuleLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskRunningRuleLabel.Size = new System.Drawing.Size(339, 15);
			this.taskRunningRuleLabel.TabIndex = 13;
			this.taskRunningRuleLabel.Text = "If the task is already ru&nning, then the following rule applies:";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label8.Location = new System.Drawing.Point(0, 273);
			this.label8.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.label8.Name = "label8";
			this.label8.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.label8.Size = new System.Drawing.Size(181, 15);
			this.label8.TabIndex = 2;
			this.label8.Text = "Priority &Level:";
			// 
			// taskDeleteAfterCheck
			// 
			this.taskDeleteAfterCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskDeleteAfterCheck.AutoSize = true;
			this.taskDeleteAfterCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskDeleteAfterCheck.Location = new System.Drawing.Point(0, 196);
			this.taskDeleteAfterCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.taskDeleteAfterCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskDeleteAfterCheck.Size = new System.Drawing.Size(181, 19);
			this.taskDeleteAfterCheck.TabIndex = 11;
			this.taskDeleteAfterCheck.Text = "If the task is not scheduled to run again, &delete it after:";
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new System.EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskRestartCountLabel
			// 
			this.taskRestartCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskRestartCountLabel.AutoSize = true;
			this.taskRestartCountLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskRestartCountLabel.Location = new System.Drawing.Point(0, 115);
			this.taskRestartCountLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.taskRestartCountLabel.Name = "taskRestartCountLabel";
			this.taskRestartCountLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskRestartCountLabel.Size = new System.Drawing.Size(181, 15);
			this.taskRestartCountLabel.TabIndex = 5;
			this.taskRestartCountLabel.Text = "Attempt &restart up to:";
			// 
			// taskAllowHardTerminateCheck
			// 
			this.taskAllowHardTerminateCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskAllowHardTerminateCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskAllowHardTerminateCheck, 2);
			this.taskAllowHardTerminateCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskAllowHardTerminateCheck.Location = new System.Drawing.Point(0, 169);
			this.taskAllowHardTerminateCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskAllowHardTerminateCheck.Name = "taskAllowHardTerminateCheck";
			this.taskAllowHardTerminateCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskAllowHardTerminateCheck.Size = new System.Drawing.Size(339, 19);
			this.taskAllowHardTerminateCheck.TabIndex = 10;
			this.taskAllowHardTerminateCheck.Text = "If the running task does not end when requested, &force it to stop";
			this.taskAllowHardTerminateCheck.UseVisualStyleBackColor = true;
			this.taskAllowHardTerminateCheck.CheckedChanged += new System.EventHandler(this.taskAllowHardTerminateCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCheck
			// 
			this.taskExecutionTimeLimitCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskExecutionTimeLimitCheck.AutoSize = true;
			this.taskExecutionTimeLimitCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskExecutionTimeLimitCheck.Location = new System.Drawing.Point(0, 142);
			this.taskExecutionTimeLimitCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskExecutionTimeLimitCheck.Name = "taskExecutionTimeLimitCheck";
			this.taskExecutionTimeLimitCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskExecutionTimeLimitCheck.Size = new System.Drawing.Size(181, 19);
			this.taskExecutionTimeLimitCheck.TabIndex = 8;
			this.taskExecutionTimeLimitCheck.Text = "Stop the tas&k if it runs longer than:";
			this.taskExecutionTimeLimitCheck.UseVisualStyleBackColor = true;
			// 
			// taskDeleteAfterCombo
			// 
			this.taskDeleteAfterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskDeleteAfterCombo.Location = new System.Drawing.Point(187, 194);
			this.taskDeleteAfterCombo.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			this.taskDeleteAfterCombo.Size = new System.Drawing.Size(155, 23);
			this.taskDeleteAfterCombo.TabIndex = 12;
			this.taskDeleteAfterCombo.ValueChanged += new System.EventHandler(this.taskDeleteAfterCombo_ValueChanged);
			// 
			// optionPanelHeaderLabel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel1, 2);
			this.optionPanelHeaderLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel1.Location = new System.Drawing.Point(0, 0);
			this.optionPanelHeaderLabel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			this.optionPanelHeaderLabel1.Size = new System.Drawing.Size(342, 23);
			this.optionPanelHeaderLabel1.TabIndex = 0;
			this.optionPanelHeaderLabel1.Text = "Options";
			// 
			// taskRestartIntervalCheck
			// 
			this.taskRestartIntervalCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskRestartIntervalCheck.AutoSize = true;
			this.taskRestartIntervalCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskRestartIntervalCheck.Location = new System.Drawing.Point(0, 84);
			this.taskRestartIntervalCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskRestartIntervalCheck.Name = "taskRestartIntervalCheck";
			this.taskRestartIntervalCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskRestartIntervalCheck.Size = new System.Drawing.Size(181, 19);
			this.taskRestartIntervalCheck.TabIndex = 3;
			this.taskRestartIntervalCheck.Text = "If the &task fails, restart every:";
			this.taskRestartIntervalCheck.UseVisualStyleBackColor = true;
			this.taskRestartIntervalCheck.CheckedChanged += new System.EventHandler(this.taskRestartIntervalCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCombo
			// 
			this.taskExecutionTimeLimitCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskExecutionTimeLimitCombo.Location = new System.Drawing.Point(187, 140);
			this.taskExecutionTimeLimitCombo.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.taskExecutionTimeLimitCombo.Name = "taskExecutionTimeLimitCombo";
			this.taskExecutionTimeLimitCombo.Size = new System.Drawing.Size(155, 23);
			this.taskExecutionTimeLimitCombo.TabIndex = 9;
			this.taskExecutionTimeLimitCombo.ValueChanged += new System.EventHandler(this.taskExecutionTimeLimitCombo_ValueChanged);
			// 
			// taskRestartIntervalCombo
			// 
			this.taskRestartIntervalCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskRestartIntervalCombo.Location = new System.Drawing.Point(187, 82);
			this.taskRestartIntervalCombo.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.taskRestartIntervalCombo.Name = "taskRestartIntervalCombo";
			this.taskRestartIntervalCombo.Size = new System.Drawing.Size(155, 23);
			this.taskRestartIntervalCombo.TabIndex = 4;
			this.taskRestartIntervalCombo.ValueChanged += new System.EventHandler(this.taskRestartIntervalCombo_ValueChanged);
			// 
			// taskAllowDemandStartCheck
			// 
			this.taskAllowDemandStartCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskAllowDemandStartCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskAllowDemandStartCheck, 2);
			this.taskAllowDemandStartCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskAllowDemandStartCheck.Location = new System.Drawing.Point(0, 32);
			this.taskAllowDemandStartCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskAllowDemandStartCheck.Name = "taskAllowDemandStartCheck";
			this.taskAllowDemandStartCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskAllowDemandStartCheck.Size = new System.Drawing.Size(339, 19);
			this.taskAllowDemandStartCheck.TabIndex = 1;
			this.taskAllowDemandStartCheck.Text = "A&llow task to be run on demand";
			this.taskAllowDemandStartCheck.UseVisualStyleBackColor = true;
			this.taskAllowDemandStartCheck.CheckedChanged += new System.EventHandler(this.taskAllowDemandStartCheck_CheckedChanged);
			// 
			// taskStartWhenAvailableCheck
			// 
			this.taskStartWhenAvailableCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskStartWhenAvailableCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskStartWhenAvailableCheck, 2);
			this.taskStartWhenAvailableCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskStartWhenAvailableCheck.Location = new System.Drawing.Point(0, 57);
			this.taskStartWhenAvailableCheck.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskStartWhenAvailableCheck.Name = "taskStartWhenAvailableCheck";
			this.taskStartWhenAvailableCheck.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.taskStartWhenAvailableCheck.Size = new System.Drawing.Size(339, 19);
			this.taskStartWhenAvailableCheck.TabIndex = 2;
			this.taskStartWhenAvailableCheck.Text = "Run task as soon as possible after a &scheduled start is missed";
			this.taskStartWhenAvailableCheck.UseVisualStyleBackColor = true;
			this.taskStartWhenAvailableCheck.CheckedChanged += new System.EventHandler(this.taskStartWhenAvailableCheck_CheckedChanged);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.taskRestartCountText);
			this.flowLayoutPanel1.Controls.Add(this.taskRestartAttemptTimesLabel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(184, 108);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(158, 29);
			this.flowLayoutPanel1.TabIndex = 15;
			// 
			// taskRestartCountText
			// 
			this.taskRestartCountText.Location = new System.Drawing.Point(3, 3);
			this.taskRestartCountText.Name = "taskRestartCountText";
			this.taskRestartCountText.Size = new System.Drawing.Size(48, 23);
			this.taskRestartCountText.TabIndex = 6;
			this.taskRestartCountText.ValueChanged += new System.EventHandler(this.taskRestartCountText_ValueChanged);
			// 
			// taskRestartAttemptTimesLabel
			// 
			this.taskRestartAttemptTimesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.taskRestartAttemptTimesLabel.AutoSize = true;
			this.taskRestartAttemptTimesLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskRestartAttemptTimesLabel.Location = new System.Drawing.Point(54, 7);
			this.taskRestartAttemptTimesLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			this.taskRestartAttemptTimesLabel.Size = new System.Drawing.Size(36, 15);
			this.taskRestartAttemptTimesLabel.TabIndex = 7;
			this.taskRestartAttemptTimesLabel.Text = "times";
			// 
			// RuntimeOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "RuntimeOptionPanel";
			this.Size = new System.Drawing.Size(342, 412);
			this.Title = "Run-time Options";
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
		private System.Windows.Forms.CheckBox taskAllowDemandStartCheck;
		private System.Windows.Forms.CheckBox taskStartWhenAvailableCheck;
		private System.Windows.Forms.CheckBox taskRestartIntervalCheck;
		private System.Windows.Forms.TimeSpanPicker taskRestartIntervalCombo;
		private System.Windows.Forms.Label taskRestartCountLabel;
		private System.Windows.Forms.NumericUpDown taskRestartCountText;
		private System.Windows.Forms.CheckBox taskExecutionTimeLimitCheck;
		private System.Windows.Forms.TimeSpanPicker taskExecutionTimeLimitCombo;
		private System.Windows.Forms.CheckBox taskAllowHardTerminateCheck;
		private System.Windows.Forms.CheckBox taskDeleteAfterCheck;
		private System.Windows.Forms.TimeSpanPicker taskDeleteAfterCombo;
		private System.Windows.Forms.Label taskRunningRuleLabel;
		private System.Windows.Forms.ComboBox taskMultInstCombo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox taskPriorityCombo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label taskRestartAttemptTimesLabel;
	}
}
