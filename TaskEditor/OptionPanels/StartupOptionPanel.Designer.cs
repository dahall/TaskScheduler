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
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
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
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// taskIdleDurationCombo
			// 
			resources.ApplyResources(this.taskIdleDurationCombo, "taskIdleDurationCombo");
			this.taskIdleDurationCombo.Name = "taskIdleDurationCombo";
			this.taskIdleDurationCombo.ValueChanged += new System.EventHandler(this.taskIdleDurationCombo_ValueChanged);
			// 
			// availableConnectionsCombo
			// 
			resources.ApplyResources(this.availableConnectionsCombo, "availableConnectionsCombo");
			this.tableLayoutPanel1.SetColumnSpan(this.availableConnectionsCombo, 2);
			this.availableConnectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			this.availableConnectionsCombo.SelectedIndexChanged += new System.EventHandler(this.availableConnectionsCombo_SelectedIndexChanged);
			// 
			// taskIdleDurationCheck
			// 
			resources.ApplyResources(this.taskIdleDurationCheck, "taskIdleDurationCheck");
			this.taskIdleDurationCheck.Name = "taskIdleDurationCheck";
			this.taskIdleDurationCheck.UseVisualStyleBackColor = true;
			this.taskIdleDurationCheck.CheckedChanged += new System.EventHandler(this.taskIdleDurationCheck_CheckedChanged);
			// 
			// taskWakeToRunCheck
			// 
			resources.ApplyResources(this.taskWakeToRunCheck, "taskWakeToRunCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskWakeToRunCheck, 2);
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			this.taskWakeToRunCheck.CheckedChanged += new System.EventHandler(this.taskWakeToRunCheck_CheckedChanged);
			// 
			// taskStartIfConnectionCheck
			// 
			resources.ApplyResources(this.taskStartIfConnectionCheck, "taskStartIfConnectionCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskStartIfConnectionCheck, 2);
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new System.EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// taskStopIfGoingOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskStopIfGoingOnBatteriesCheck, "taskStopIfGoingOnBatteriesCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskStopIfGoingOnBatteriesCheck, 2);
			this.taskStopIfGoingOnBatteriesCheck.Name = "taskStopIfGoingOnBatteriesCheck";
			this.taskStopIfGoingOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskStopIfGoingOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskStopIfGoingOnBatteriesCheck_CheckedChanged);
			// 
			// taskRestartOnIdleCheck
			// 
			resources.ApplyResources(this.taskRestartOnIdleCheck, "taskRestartOnIdleCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskRestartOnIdleCheck, 2);
			this.taskRestartOnIdleCheck.Name = "taskRestartOnIdleCheck";
			this.taskRestartOnIdleCheck.UseVisualStyleBackColor = true;
			this.taskRestartOnIdleCheck.CheckedChanged += new System.EventHandler(this.taskRestartOnIdleCheck_CheckedChanged);
			// 
			// taskDisallowStartOnRemoteAppSessionCheck
			// 
			resources.ApplyResources(this.taskDisallowStartOnRemoteAppSessionCheck, "taskDisallowStartOnRemoteAppSessionCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskDisallowStartOnRemoteAppSessionCheck, 2);
			this.taskDisallowStartOnRemoteAppSessionCheck.Name = "taskDisallowStartOnRemoteAppSessionCheck";
			this.taskDisallowStartOnRemoteAppSessionCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartOnRemoteAppSessionCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged);
			// 
			// taskDisallowStartIfOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskDisallowStartIfOnBatteriesCheck, "taskDisallowStartIfOnBatteriesCheck");
			this.tableLayoutPanel1.SetColumnSpan(this.taskDisallowStartIfOnBatteriesCheck, 2);
			this.taskDisallowStartIfOnBatteriesCheck.Name = "taskDisallowStartIfOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartIfOnBatteriesCheck, ((bool)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.ShowHelp"))));
			this.taskDisallowStartIfOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartIfOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartIfOnBatteriesCheck_CheckedChanged);
			// 
			// optionPanelHeaderLabel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel1, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel1, "optionPanelHeaderLabel1");
			this.optionPanelHeaderLabel1.Name = "optionPanelHeaderLabel1";
			// 
			// optionPanelHeaderLabel2
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel2, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel2, "optionPanelHeaderLabel2");
			this.optionPanelHeaderLabel2.Name = "optionPanelHeaderLabel2";
			// 
			// taskStopOnIdleEndCheck
			// 
			resources.ApplyResources(this.taskStopOnIdleEndCheck, "taskStopOnIdleEndCheck");
			this.taskStopOnIdleEndCheck.Checked = true;
			this.taskStopOnIdleEndCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel1.SetColumnSpan(this.taskStopOnIdleEndCheck, 2);
			this.taskStopOnIdleEndCheck.Name = "taskStopOnIdleEndCheck";
			this.taskStopOnIdleEndCheck.UseVisualStyleBackColor = true;
			this.taskStopOnIdleEndCheck.CheckedChanged += new System.EventHandler(this.taskStopOnIdleEndCheck_CheckedChanged);
			// 
			// optionPanelHeaderLabel3
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel3, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel3, "optionPanelHeaderLabel3");
			this.optionPanelHeaderLabel3.Name = "optionPanelHeaderLabel3";
			// 
			// optionPanelHeaderLabel4
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.optionPanelHeaderLabel4, 2);
			resources.ApplyResources(this.optionPanelHeaderLabel4, "optionPanelHeaderLabel4");
			this.optionPanelHeaderLabel4.Name = "optionPanelHeaderLabel4";
			// 
			// taskIdleWaitTimeoutCombo
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutCombo, "taskIdleWaitTimeoutCombo");
			this.taskIdleWaitTimeoutCombo.Name = "taskIdleWaitTimeoutCombo";
			this.taskIdleWaitTimeoutCombo.ValueChanged += new System.EventHandler(this.taskIdleWaitTimeoutCombo_ValueChanged);
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// taskIdleWaitTimeoutLabel
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutLabel, "taskIdleWaitTimeoutLabel");
			this.taskIdleWaitTimeoutLabel.Name = "taskIdleWaitTimeoutLabel";
			// 
			// StartupOptionPanel
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "StartupOptionPanel";
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
