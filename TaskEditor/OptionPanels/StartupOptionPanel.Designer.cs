namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	partial class StartupOptionPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupOptionPanel));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.taskIdleDurationCombo = new System.Windows.Forms.TimeSpanPicker();
			this.availableConnectionsCombo = new System.Windows.Forms.ComboBox();
			this.taskIdleDurationCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskWakeToRunCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskStartIfConnectionCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskStopIfGoingOnBatteriesCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskRestartOnIdleCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskDisallowStartOnRemoteAppSessionCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.taskDisallowStartIfOnBatteriesCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.optionPanelHeaderLabel1 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.optionPanelHeaderLabel2 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.taskStopOnIdleEndCheck = new Microsoft.Win32.TaskScheduler.OptionPanels.WrappingCheckBox();
			this.optionPanelHeaderLabel3 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.optionPanelHeaderLabel4 = new Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanelHeaderLabel();
			this.taskIdleWaitTimeoutCombo = new System.Windows.Forms.TimeSpanPicker();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.taskIdleWaitTimeoutLabel = new System.Windows.Forms.Label();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
			this.tableLayoutPanel1.Controls.Add(this.taskIdleDurationCombo, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.availableConnectionsCombo, 0, 13);
			this.tableLayoutPanel1.Controls.Add(this.taskIdleDurationCheck, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.taskWakeToRunCheck, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.taskStartIfConnectionCheck, 0, 12);
			this.tableLayoutPanel1.Controls.Add(this.taskStopIfGoingOnBatteriesCheck, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.taskRestartOnIdleCheck, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.taskDisallowStartOnRemoteAppSessionCheck, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.taskDisallowStartIfOnBatteriesCheck, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.taskStopOnIdleEndCheck, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel3, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.optionPanelHeaderLabel4, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this.taskIdleWaitTimeoutCombo, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.taskIdleWaitTimeoutLabel, 0, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 14;
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(302, 435);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// taskIdleDurationCombo
			// 
			this.taskIdleDurationCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskIdleDurationCombo.Location = new System.Drawing.Point(173, 104);
			this.taskIdleDurationCombo.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskIdleDurationCombo.Name = "taskIdleDurationCombo";
			this.taskIdleDurationCombo.Size = new System.Drawing.Size(126, 23);
			this.taskIdleDurationCombo.TabIndex = 2;
			this.taskIdleDurationCombo.ValueChanged += new System.EventHandler(this.taskIdleDurationCombo_ValueChanged);
			// 
			// availableConnectionsCombo
			// 
			this.availableConnectionsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.availableConnectionsCombo, 2);
			this.availableConnectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.availableConnectionsCombo.Location = new System.Drawing.Point(25, 404);
			this.availableConnectionsCombo.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			this.availableConnectionsCombo.Size = new System.Drawing.Size(274, 23);
			this.availableConnectionsCombo.TabIndex = 1;
			this.availableConnectionsCombo.SelectedIndexChanged += new System.EventHandler(this.availableConnectionsCombo_SelectedIndexChanged);
			// 
			// taskIdleDurationCheck
			// 
			this.taskIdleDurationCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskIdleDurationCheck.AutoSize = true;
			this.taskIdleDurationCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskIdleDurationCheck.Location = new System.Drawing.Point(7, 101);
			this.taskIdleDurationCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskIdleDurationCheck.Name = "taskIdleDurationCheck";
			this.taskIdleDurationCheck.Size = new System.Drawing.Size(163, 34);
			this.taskIdleDurationCheck.TabIndex = 1;
			this.taskIdleDurationCheck.Text = "Start the task only if the &computer is idle for:";
			this.taskIdleDurationCheck.UseVisualStyleBackColor = true;
			this.taskIdleDurationCheck.CheckedChanged += new System.EventHandler(this.taskIdleDurationCheck_CheckedChanged);
			// 
			// taskWakeToRunCheck
			// 
			this.taskWakeToRunCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskWakeToRunCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskWakeToRunCheck, 2);
			this.taskWakeToRunCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskWakeToRunCheck.Location = new System.Drawing.Point(7, 310);
			this.taskWakeToRunCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.taskWakeToRunCheck.Size = new System.Drawing.Size(292, 19);
			this.taskWakeToRunCheck.TabIndex = 2;
			this.taskWakeToRunCheck.Text = "&Wake the computer to run this task";
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			this.taskWakeToRunCheck.CheckedChanged += new System.EventHandler(this.taskWakeToRunCheck_CheckedChanged);
			// 
			// taskStartIfConnectionCheck
			// 
			this.taskStartIfConnectionCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskStartIfConnectionCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskStartIfConnectionCheck, 2);
			this.taskStartIfConnectionCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskStartIfConnectionCheck.Location = new System.Drawing.Point(7, 367);
			this.taskStartIfConnectionCheck.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.taskStartIfConnectionCheck.Size = new System.Drawing.Size(292, 34);
			this.taskStartIfConnectionCheck.TabIndex = 0;
			this.taskStartIfConnectionCheck.Text = "Start onl&y if the following network connection is available:";
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new System.EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// taskStopIfGoingOnBatteriesCheck
			// 
			this.taskStopIfGoingOnBatteriesCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskStopIfGoingOnBatteriesCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskStopIfGoingOnBatteriesCheck, 2);
			this.taskStopIfGoingOnBatteriesCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskStopIfGoingOnBatteriesCheck.Location = new System.Drawing.Point(7, 285);
			this.taskStopIfGoingOnBatteriesCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskStopIfGoingOnBatteriesCheck.Name = "taskStopIfGoingOnBatteriesCheck";
			this.taskStopIfGoingOnBatteriesCheck.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
			this.taskStopIfGoingOnBatteriesCheck.Size = new System.Drawing.Size(292, 19);
			this.taskStopIfGoingOnBatteriesCheck.TabIndex = 1;
			this.taskStopIfGoingOnBatteriesCheck.Text = "Stop if the computer switches to &battery power";
			this.taskStopIfGoingOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskStopIfGoingOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskStopIfGoingOnBatteriesCheck_CheckedChanged);
			// 
			// taskRestartOnIdleCheck
			// 
			this.taskRestartOnIdleCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskRestartOnIdleCheck, 2);
			this.taskRestartOnIdleCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskRestartOnIdleCheck.Location = new System.Drawing.Point(7, 200);
			this.taskRestartOnIdleCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskRestartOnIdleCheck.Name = "taskRestartOnIdleCheck";
			this.taskRestartOnIdleCheck.Padding = new System.Windows.Forms.Padding(36, 0, 0, 0);
			this.taskRestartOnIdleCheck.Size = new System.Drawing.Size(225, 19);
			this.taskRestartOnIdleCheck.TabIndex = 6;
			this.taskRestartOnIdleCheck.Text = "Restart if the idle state res&umes";
			this.taskRestartOnIdleCheck.UseVisualStyleBackColor = true;
			this.taskRestartOnIdleCheck.CheckedChanged += new System.EventHandler(this.taskRestartOnIdleCheck_CheckedChanged);
			// 
			// taskDisallowStartOnRemoteAppSessionCheck
			// 
			this.taskDisallowStartOnRemoteAppSessionCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskDisallowStartOnRemoteAppSessionCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskDisallowStartOnRemoteAppSessionCheck, 2);
			this.taskDisallowStartOnRemoteAppSessionCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskDisallowStartOnRemoteAppSessionCheck.Location = new System.Drawing.Point(7, 29);
			this.taskDisallowStartOnRemoteAppSessionCheck.Margin = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.taskDisallowStartOnRemoteAppSessionCheck.Name = "taskDisallowStartOnRemoteAppSessionCheck";
			this.taskDisallowStartOnRemoteAppSessionCheck.Size = new System.Drawing.Size(295, 34);
			this.taskDisallowStartOnRemoteAppSessionCheck.TabIndex = 5;
			this.taskDisallowStartOnRemoteAppSessionCheck.Text = "&Disallow start if in a Remote Applications Integrated Locally (RAIL) session";
			this.taskDisallowStartOnRemoteAppSessionCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartOnRemoteAppSessionCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged);
			// 
			// taskDisallowStartIfOnBatteriesCheck
			// 
			this.taskDisallowStartIfOnBatteriesCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.taskDisallowStartIfOnBatteriesCheck.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.taskDisallowStartIfOnBatteriesCheck, 2);
			this.taskDisallowStartIfOnBatteriesCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskDisallowStartIfOnBatteriesCheck.Location = new System.Drawing.Point(7, 260);
			this.taskDisallowStartIfOnBatteriesCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskDisallowStartIfOnBatteriesCheck.Name = "taskDisallowStartIfOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartIfOnBatteriesCheck, true);
			this.taskDisallowStartIfOnBatteriesCheck.Size = new System.Drawing.Size(292, 19);
			this.taskDisallowStartIfOnBatteriesCheck.TabIndex = 0;
			this.taskDisallowStartIfOnBatteriesCheck.Text = "Start the task only if the computer is on AC &power";
			this.taskDisallowStartIfOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartIfOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartIfOnBatteriesCheck_CheckedChanged);
			// 
			// optionPanelHeaderLabel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel1, 2);
			this.optionPanelHeaderLabel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel1.Location = new System.Drawing.Point(0, 0);
			this.optionPanelHeaderLabel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			this.optionPanelHeaderLabel1.Size = new System.Drawing.Size(302, 23);
			this.optionPanelHeaderLabel1.TabIndex = 0;
			this.optionPanelHeaderLabel1.Text = "Options";
			// 
			// optionPanelHeaderLabel2
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel2, 2);
			this.optionPanelHeaderLabel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel2.Location = new System.Drawing.Point(0, 69);
			this.optionPanelHeaderLabel2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.optionPanelHeaderLabel2.Name = "optionPanelHeaderLabel2";
			this.optionPanelHeaderLabel2.Size = new System.Drawing.Size(302, 23);
			this.optionPanelHeaderLabel2.TabIndex = 1;
			this.optionPanelHeaderLabel2.Text = "Idle";
			// 
			// taskStopOnIdleEndCheck
			// 
			this.taskStopOnIdleEndCheck.AutoSize = true;
			this.taskStopOnIdleEndCheck.Checked = true;
			this.taskStopOnIdleEndCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel1.SetColumnSpan(this.taskStopOnIdleEndCheck, 2);
			this.taskStopOnIdleEndCheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskStopOnIdleEndCheck.Location = new System.Drawing.Point(7, 175);
			this.taskStopOnIdleEndCheck.Margin = new System.Windows.Forms.Padding(7, 3, 3, 3);
			this.taskStopOnIdleEndCheck.Name = "taskStopOnIdleEndCheck";
			this.taskStopOnIdleEndCheck.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
			this.taskStopOnIdleEndCheck.Size = new System.Drawing.Size(242, 19);
			this.taskStopOnIdleEndCheck.TabIndex = 5;
			this.taskStopOnIdleEndCheck.Text = "Stop if the comput&er ceases to be idle";
			this.taskStopOnIdleEndCheck.UseVisualStyleBackColor = true;
			this.taskStopOnIdleEndCheck.CheckedChanged += new System.EventHandler(this.taskStopOnIdleEndCheck_CheckedChanged);
			// 
			// optionPanelHeaderLabel3
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel3, 2);
			this.optionPanelHeaderLabel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel3.Location = new System.Drawing.Point(0, 228);
			this.optionPanelHeaderLabel3.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.optionPanelHeaderLabel3.Name = "optionPanelHeaderLabel3";
			this.optionPanelHeaderLabel3.Size = new System.Drawing.Size(302, 23);
			this.optionPanelHeaderLabel3.TabIndex = 2;
			this.optionPanelHeaderLabel3.Text = "Power";
			// 
			// optionPanelHeaderLabel4
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel4, 2);
			this.optionPanelHeaderLabel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.optionPanelHeaderLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.optionPanelHeaderLabel4.Location = new System.Drawing.Point(0, 338);
			this.optionPanelHeaderLabel4.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
			this.optionPanelHeaderLabel4.Name = "optionPanelHeaderLabel4";
			this.optionPanelHeaderLabel4.Size = new System.Drawing.Size(302, 23);
			this.optionPanelHeaderLabel4.TabIndex = 3;
			this.optionPanelHeaderLabel4.Text = "Network";
			// 
			// taskIdleWaitTimeoutCombo
			// 
			this.taskIdleWaitTimeoutCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.taskIdleWaitTimeoutCombo.Location = new System.Drawing.Point(173, 141);
			this.taskIdleWaitTimeoutCombo.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.taskIdleWaitTimeoutCombo.Name = "taskIdleWaitTimeoutCombo";
			this.taskIdleWaitTimeoutCombo.Size = new System.Drawing.Size(126, 23);
			this.taskIdleWaitTimeoutCombo.TabIndex = 4;
			this.taskIdleWaitTimeoutCombo.ValueChanged += new System.EventHandler(this.taskIdleWaitTimeoutCombo_ValueChanged);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.87356F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.12644F));
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 197);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(173, 0);
			this.tableLayoutPanel3.TabIndex = 9;
			// 
			// taskIdleWaitTimeoutLabel
			// 
			this.taskIdleWaitTimeoutLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.taskIdleWaitTimeoutLabel.AutoSize = true;
			this.taskIdleWaitTimeoutLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.taskIdleWaitTimeoutLabel.Location = new System.Drawing.Point(7, 147);
			this.taskIdleWaitTimeoutLabel.Margin = new System.Windows.Forms.Padding(7, 0, 3, 0);
			this.taskIdleWaitTimeoutLabel.Name = "taskIdleWaitTimeoutLabel";
			this.taskIdleWaitTimeoutLabel.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
			this.taskIdleWaitTimeoutLabel.Size = new System.Drawing.Size(108, 15);
			this.taskIdleWaitTimeoutLabel.TabIndex = 3;
			this.taskIdleWaitTimeoutLabel.Text = "W&ait for idle for:";
			// 
			// StartupOptionPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
			this.Name = "StartupOptionPanel";
			this.Size = new System.Drawing.Size(302, 412);
			this.Title = "Task Start Options";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel1;
		private OptionPanelHeaderLabel optionPanelHeaderLabel2;
		private OptionPanelHeaderLabel optionPanelHeaderLabel3;
		private OptionPanelHeaderLabel optionPanelHeaderLabel4;
		private WrappingCheckBox taskDisallowStartOnRemoteAppSessionCheck;
		private WrappingCheckBox taskIdleDurationCheck;
		private System.Windows.Forms.TimeSpanPicker taskIdleDurationCombo;
		private System.Windows.Forms.Label taskIdleWaitTimeoutLabel;
		private WrappingCheckBox taskStopOnIdleEndCheck;
		private WrappingCheckBox taskRestartOnIdleCheck;
		private WrappingCheckBox taskDisallowStartIfOnBatteriesCheck;
		private System.Windows.Forms.HelpProvider helpProvider;
		private WrappingCheckBox taskStopIfGoingOnBatteriesCheck;
		private WrappingCheckBox taskWakeToRunCheck;
		private WrappingCheckBox taskStartIfConnectionCheck;
		private System.Windows.Forms.ComboBox availableConnectionsCombo;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TimeSpanPicker taskIdleWaitTimeoutCombo;
	}
}
