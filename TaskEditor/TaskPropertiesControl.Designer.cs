namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskPropertiesControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesControl));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.taskNameLabel = new System.Windows.Forms.Label();
			this.taskAuthorLabel = new System.Windows.Forms.Label();
			this.taskDescLabel = new System.Windows.Forms.Label();
			this.taskNameText = new System.Windows.Forms.TextBox();
			this.taskAuthorText = new System.Windows.Forms.Label();
			this.taskDescText = new System.Windows.Forms.TextBox();
			this.taskVersionCombo = new System.Windows.Forms.ComboBox();
			this.taskVersionLabel = new System.Windows.Forms.Label();
			this.taskHiddenCheck = new System.Windows.Forms.CheckBox();
			this.taskSecurityGroupBox = new System.Windows.Forms.GroupBox();
			this.taskRunLevelCheck = new System.Windows.Forms.CheckBox();
			this.taskLocalOnlyCheck = new System.Windows.Forms.CheckBox();
			this.taskLoggedOptionalRadio = new System.Windows.Forms.RadioButton();
			this.taskLoggedOnRadio = new System.Windows.Forms.RadioButton();
			this.taskPrincipalText = new System.Windows.Forms.TextBox();
			this.changePrincipalButton = new System.Windows.Forms.Button();
			this.taskUserAcctLabel = new System.Windows.Forms.Label();
			this.triggersTab = new System.Windows.Forms.TabPage();
			this.triggerDeleteButton = new System.Windows.Forms.Button();
			this.triggerEditButton = new System.Windows.Forms.Button();
			this.triggerNewButton = new System.Windows.Forms.Button();
			this.triggerListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.taskTriggerIntroLabel = new System.Windows.Forms.Label();
			this.actionsTab = new System.Windows.Forms.TabPage();
			this.actionDownButton = new System.Windows.Forms.Button();
			this.actionUpButton = new System.Windows.Forms.Button();
			this.actionDeleteButton = new System.Windows.Forms.Button();
			this.actionEditButton = new System.Windows.Forms.Button();
			this.actionNewButton = new System.Windows.Forms.Button();
			this.actionListView = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.actionIntroLabel = new System.Windows.Forms.Label();
			this.conditionsTab = new System.Windows.Forms.TabPage();
			this.networkConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.availableConnectionsCombo = new System.Windows.Forms.ComboBox();
			this.taskStartIfConnectionCheck = new System.Windows.Forms.CheckBox();
			this.powerConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.taskStopIfBatteryCheck = new System.Windows.Forms.CheckBox();
			this.taskWakeToRunCheck = new System.Windows.Forms.CheckBox();
			this.taskStartOnlyOnACCheck = new System.Windows.Forms.CheckBox();
			this.idleConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.taskIdleDelayCombo = new System.Windows.Forms.ComboBox();
			this.taskStartAfterIdleCombo = new System.Windows.Forms.ComboBox();
			this.taskRestartWhenIdleCheck = new System.Windows.Forms.CheckBox();
			this.taskStopIfNotIdleCheck = new System.Windows.Forms.CheckBox();
			this.taskIdleDelayLabel = new System.Windows.Forms.Label();
			this.taskStartAfterIdleCheck = new System.Windows.Forms.CheckBox();
			this.conditionIntroLabel = new System.Windows.Forms.Label();
			this.settingsTab = new System.Windows.Forms.TabPage();
			this.taskRestartAttemptsText = new System.Windows.Forms.TextBox();
			this.taskDeleteAfterCombo = new System.Windows.Forms.ComboBox();
			this.taskStopIfRunningAfterCombo = new System.Windows.Forms.ComboBox();
			this.taskRestartAfterSpanCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleLabel = new System.Windows.Forms.Label();
			this.taskRestartAttemptTimesLabel = new System.Windows.Forms.Label();
			this.taskRestartAttemptsLabel = new System.Windows.Forms.Label();
			this.taskDeleteAfterCheck = new System.Windows.Forms.CheckBox();
			this.taskForceStopCheck = new System.Windows.Forms.CheckBox();
			this.taskStopIfRunningAfterCheck = new System.Windows.Forms.CheckBox();
			this.taskRestartAfterFailureCheck = new System.Windows.Forms.CheckBox();
			this.taskRunAfterMissedCheck = new System.Windows.Forms.CheckBox();
			this.taskRunOnDemandCheck = new System.Windows.Forms.CheckBox();
			this.settingsIntroLabel = new System.Windows.Forms.Label();
			this.historyTab = new System.Windows.Forms.TabPage();
			this.historyListView = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.historyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.tabControl1.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.taskSecurityGroupBox.SuspendLayout();
			this.triggersTab.SuspendLayout();
			this.actionsTab.SuspendLayout();
			this.conditionsTab.SuspendLayout();
			this.networkConditionGroupBox.SuspendLayout();
			this.powerConditionGroupBox.SuspendLayout();
			this.idleConditionGroupBox.SuspendLayout();
			this.settingsTab.SuspendLayout();
			this.historyTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.generalTab);
			this.tabControl1.Controls.Add(this.triggersTab);
			this.tabControl1.Controls.Add(this.actionsTab);
			this.tabControl1.Controls.Add(this.conditionsTab);
			this.tabControl1.Controls.Add(this.settingsTab);
			this.tabControl1.Controls.Add(this.historyTab);
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// generalTab
			// 
			this.generalTab.Controls.Add(this.taskNameLabel);
			this.generalTab.Controls.Add(this.taskAuthorLabel);
			this.generalTab.Controls.Add(this.taskDescLabel);
			this.generalTab.Controls.Add(this.taskNameText);
			this.generalTab.Controls.Add(this.taskAuthorText);
			this.generalTab.Controls.Add(this.taskDescText);
			this.generalTab.Controls.Add(this.taskVersionCombo);
			this.generalTab.Controls.Add(this.taskVersionLabel);
			this.generalTab.Controls.Add(this.taskHiddenCheck);
			this.generalTab.Controls.Add(this.taskSecurityGroupBox);
			resources.ApplyResources(this.generalTab, "generalTab");
			this.generalTab.Name = "generalTab";
			this.generalTab.UseVisualStyleBackColor = true;
			// 
			// taskNameLabel
			// 
			resources.ApplyResources(this.taskNameLabel, "taskNameLabel");
			this.taskNameLabel.Name = "taskNameLabel";
			// 
			// taskAuthorLabel
			// 
			resources.ApplyResources(this.taskAuthorLabel, "taskAuthorLabel");
			this.taskAuthorLabel.Name = "taskAuthorLabel";
			// 
			// taskDescLabel
			// 
			resources.ApplyResources(this.taskDescLabel, "taskDescLabel");
			this.taskDescLabel.Name = "taskDescLabel";
			// 
			// taskNameText
			// 
			resources.ApplyResources(this.taskNameText, "taskNameText");
			this.taskNameText.Name = "taskNameText";
			// 
			// taskAuthorText
			// 
			resources.ApplyResources(this.taskAuthorText, "taskAuthorText");
			this.taskAuthorText.Name = "taskAuthorText";
			// 
			// taskDescText
			// 
			resources.ApplyResources(this.taskDescText, "taskDescText");
			this.taskDescText.Name = "taskDescText";
			// 
			// taskVersionCombo
			// 
			resources.ApplyResources(this.taskVersionCombo, "taskVersionCombo");
			this.taskVersionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskVersionCombo.Items.AddRange(new object[] {
            resources.GetString("taskVersionCombo.Items"),
            resources.GetString("taskVersionCombo.Items1")});
			this.taskVersionCombo.Name = "taskVersionCombo";
			// 
			// taskVersionLabel
			// 
			resources.ApplyResources(this.taskVersionLabel, "taskVersionLabel");
			this.taskVersionLabel.Name = "taskVersionLabel";
			// 
			// taskHiddenCheck
			// 
			resources.ApplyResources(this.taskHiddenCheck, "taskHiddenCheck");
			this.taskHiddenCheck.Name = "taskHiddenCheck";
			this.taskHiddenCheck.UseVisualStyleBackColor = true;
			// 
			// taskSecurityGroupBox
			// 
			resources.ApplyResources(this.taskSecurityGroupBox, "taskSecurityGroupBox");
			this.taskSecurityGroupBox.Controls.Add(this.taskRunLevelCheck);
			this.taskSecurityGroupBox.Controls.Add(this.taskLocalOnlyCheck);
			this.taskSecurityGroupBox.Controls.Add(this.taskLoggedOptionalRadio);
			this.taskSecurityGroupBox.Controls.Add(this.taskLoggedOnRadio);
			this.taskSecurityGroupBox.Controls.Add(this.taskPrincipalText);
			this.taskSecurityGroupBox.Controls.Add(this.changePrincipalButton);
			this.taskSecurityGroupBox.Controls.Add(this.taskUserAcctLabel);
			this.taskSecurityGroupBox.Name = "taskSecurityGroupBox";
			this.taskSecurityGroupBox.TabStop = false;
			// 
			// taskRunLevelCheck
			// 
			resources.ApplyResources(this.taskRunLevelCheck, "taskRunLevelCheck");
			this.taskRunLevelCheck.Name = "taskRunLevelCheck";
			this.taskRunLevelCheck.UseVisualStyleBackColor = true;
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			// 
			// taskLoggedOptionalRadio
			// 
			resources.ApplyResources(this.taskLoggedOptionalRadio, "taskLoggedOptionalRadio");
			this.taskLoggedOptionalRadio.Name = "taskLoggedOptionalRadio";
			this.taskLoggedOptionalRadio.TabStop = true;
			this.taskLoggedOptionalRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOptionalRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOptionalRadio_CheckedChanged);
			// 
			// taskLoggedOnRadio
			// 
			resources.ApplyResources(this.taskLoggedOnRadio, "taskLoggedOnRadio");
			this.taskLoggedOnRadio.Name = "taskLoggedOnRadio";
			this.taskLoggedOnRadio.TabStop = true;
			this.taskLoggedOnRadio.UseVisualStyleBackColor = true;
			// 
			// taskPrincipalText
			// 
			resources.ApplyResources(this.taskPrincipalText, "taskPrincipalText");
			this.taskPrincipalText.Name = "taskPrincipalText";
			// 
			// changePrincipalButton
			// 
			resources.ApplyResources(this.changePrincipalButton, "changePrincipalButton");
			this.changePrincipalButton.Name = "changePrincipalButton";
			this.changePrincipalButton.UseVisualStyleBackColor = true;
			this.changePrincipalButton.Click += new System.EventHandler(this.changePrincipalButton_Click);
			// 
			// taskUserAcctLabel
			// 
			resources.ApplyResources(this.taskUserAcctLabel, "taskUserAcctLabel");
			this.taskUserAcctLabel.Name = "taskUserAcctLabel";
			// 
			// triggersTab
			// 
			this.triggersTab.Controls.Add(this.triggerDeleteButton);
			this.triggersTab.Controls.Add(this.triggerEditButton);
			this.triggersTab.Controls.Add(this.triggerNewButton);
			this.triggersTab.Controls.Add(this.triggerListView);
			this.triggersTab.Controls.Add(this.taskTriggerIntroLabel);
			resources.ApplyResources(this.triggersTab, "triggersTab");
			this.triggersTab.Name = "triggersTab";
			this.triggersTab.UseVisualStyleBackColor = true;
			// 
			// triggerDeleteButton
			// 
			resources.ApplyResources(this.triggerDeleteButton, "triggerDeleteButton");
			this.triggerDeleteButton.Name = "triggerDeleteButton";
			this.triggerDeleteButton.UseVisualStyleBackColor = true;
			this.triggerDeleteButton.Click += new System.EventHandler(this.triggerDeleteButton_Click);
			// 
			// triggerEditButton
			// 
			resources.ApplyResources(this.triggerEditButton, "triggerEditButton");
			this.triggerEditButton.Name = "triggerEditButton";
			this.triggerEditButton.UseVisualStyleBackColor = true;
			this.triggerEditButton.Click += new System.EventHandler(this.triggerEditButton_Click);
			// 
			// triggerNewButton
			// 
			resources.ApplyResources(this.triggerNewButton, "triggerNewButton");
			this.triggerNewButton.Name = "triggerNewButton";
			this.triggerNewButton.UseVisualStyleBackColor = true;
			this.triggerNewButton.Click += new System.EventHandler(this.triggerNewButton_Click);
			// 
			// triggerListView
			// 
			resources.ApplyResources(this.triggerListView, "triggerListView");
			this.triggerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.triggerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.triggerListView.HideSelection = false;
			this.triggerListView.Name = "triggerListView";
			this.triggerListView.UseCompatibleStateImageBehavior = false;
			this.triggerListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// columnHeader2
			// 
			resources.ApplyResources(this.columnHeader2, "columnHeader2");
			// 
			// columnHeader3
			// 
			resources.ApplyResources(this.columnHeader3, "columnHeader3");
			// 
			// taskTriggerIntroLabel
			// 
			resources.ApplyResources(this.taskTriggerIntroLabel, "taskTriggerIntroLabel");
			this.taskTriggerIntroLabel.Name = "taskTriggerIntroLabel";
			// 
			// actionsTab
			// 
			this.actionsTab.Controls.Add(this.actionDownButton);
			this.actionsTab.Controls.Add(this.actionUpButton);
			this.actionsTab.Controls.Add(this.actionDeleteButton);
			this.actionsTab.Controls.Add(this.actionEditButton);
			this.actionsTab.Controls.Add(this.actionNewButton);
			this.actionsTab.Controls.Add(this.actionListView);
			this.actionsTab.Controls.Add(this.actionIntroLabel);
			resources.ApplyResources(this.actionsTab, "actionsTab");
			this.actionsTab.Name = "actionsTab";
			this.actionsTab.UseVisualStyleBackColor = true;
			// 
			// actionDownButton
			// 
			resources.ApplyResources(this.actionDownButton, "actionDownButton");
			this.actionDownButton.Name = "actionDownButton";
			this.actionDownButton.UseVisualStyleBackColor = true;
			this.actionDownButton.Click += new System.EventHandler(this.actionDownButton_Click);
			// 
			// actionUpButton
			// 
			resources.ApplyResources(this.actionUpButton, "actionUpButton");
			this.actionUpButton.Name = "actionUpButton";
			this.actionUpButton.UseVisualStyleBackColor = true;
			this.actionUpButton.Click += new System.EventHandler(this.actionUpButton_Click);
			// 
			// actionDeleteButton
			// 
			resources.ApplyResources(this.actionDeleteButton, "actionDeleteButton");
			this.actionDeleteButton.Name = "actionDeleteButton";
			this.actionDeleteButton.UseVisualStyleBackColor = true;
			this.actionDeleteButton.Click += new System.EventHandler(this.actionDeleteButton_Click);
			// 
			// actionEditButton
			// 
			resources.ApplyResources(this.actionEditButton, "actionEditButton");
			this.actionEditButton.Name = "actionEditButton";
			this.actionEditButton.UseVisualStyleBackColor = true;
			this.actionEditButton.Click += new System.EventHandler(this.actionEditButton_Click);
			// 
			// actionNewButton
			// 
			resources.ApplyResources(this.actionNewButton, "actionNewButton");
			this.actionNewButton.Name = "actionNewButton";
			this.actionNewButton.UseVisualStyleBackColor = true;
			this.actionNewButton.Click += new System.EventHandler(this.actionNewButton_Click);
			// 
			// actionListView
			// 
			resources.ApplyResources(this.actionListView, "actionListView");
			this.actionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
			this.actionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.actionListView.HideSelection = false;
			this.actionListView.Name = "actionListView";
			this.actionListView.UseCompatibleStateImageBehavior = false;
			this.actionListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			resources.ApplyResources(this.columnHeader4, "columnHeader4");
			// 
			// columnHeader5
			// 
			resources.ApplyResources(this.columnHeader5, "columnHeader5");
			// 
			// actionIntroLabel
			// 
			resources.ApplyResources(this.actionIntroLabel, "actionIntroLabel");
			this.actionIntroLabel.Name = "actionIntroLabel";
			// 
			// conditionsTab
			// 
			this.conditionsTab.Controls.Add(this.networkConditionGroupBox);
			this.conditionsTab.Controls.Add(this.powerConditionGroupBox);
			this.conditionsTab.Controls.Add(this.idleConditionGroupBox);
			this.conditionsTab.Controls.Add(this.conditionIntroLabel);
			resources.ApplyResources(this.conditionsTab, "conditionsTab");
			this.conditionsTab.Name = "conditionsTab";
			this.conditionsTab.UseVisualStyleBackColor = true;
			this.conditionsTab.Enter += new System.EventHandler(this.conditionsTab_Enter);
			// 
			// networkConditionGroupBox
			// 
			resources.ApplyResources(this.networkConditionGroupBox, "networkConditionGroupBox");
			this.networkConditionGroupBox.Controls.Add(this.availableConnectionsCombo);
			this.networkConditionGroupBox.Controls.Add(this.taskStartIfConnectionCheck);
			this.networkConditionGroupBox.Name = "networkConditionGroupBox";
			this.networkConditionGroupBox.TabStop = false;
			// 
			// availableConnectionsCombo
			// 
			resources.ApplyResources(this.availableConnectionsCombo, "availableConnectionsCombo");
			this.availableConnectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			// 
			// taskStartIfConnectionCheck
			// 
			resources.ApplyResources(this.taskStartIfConnectionCheck, "taskStartIfConnectionCheck");
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new System.EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// powerConditionGroupBox
			// 
			resources.ApplyResources(this.powerConditionGroupBox, "powerConditionGroupBox");
			this.powerConditionGroupBox.Controls.Add(this.taskStopIfBatteryCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskWakeToRunCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskStartOnlyOnACCheck);
			this.powerConditionGroupBox.Name = "powerConditionGroupBox";
			this.powerConditionGroupBox.TabStop = false;
			// 
			// taskStopIfBatteryCheck
			// 
			resources.ApplyResources(this.taskStopIfBatteryCheck, "taskStopIfBatteryCheck");
			this.taskStopIfBatteryCheck.Name = "taskStopIfBatteryCheck";
			this.taskStopIfBatteryCheck.UseVisualStyleBackColor = true;
			// 
			// taskWakeToRunCheck
			// 
			resources.ApplyResources(this.taskWakeToRunCheck, "taskWakeToRunCheck");
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			// 
			// taskStartOnlyOnACCheck
			// 
			resources.ApplyResources(this.taskStartOnlyOnACCheck, "taskStartOnlyOnACCheck");
			this.taskStartOnlyOnACCheck.Name = "taskStartOnlyOnACCheck";
			this.taskStartOnlyOnACCheck.UseVisualStyleBackColor = true;
			this.taskStartOnlyOnACCheck.CheckedChanged += new System.EventHandler(this.taskStartOnlyOnACCheck_CheckedChanged);
			// 
			// idleConditionGroupBox
			// 
			resources.ApplyResources(this.idleConditionGroupBox, "idleConditionGroupBox");
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDelayCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskStartAfterIdleCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskRestartWhenIdleCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskStopIfNotIdleCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDelayLabel);
			this.idleConditionGroupBox.Controls.Add(this.taskStartAfterIdleCheck);
			this.idleConditionGroupBox.Name = "idleConditionGroupBox";
			this.idleConditionGroupBox.TabStop = false;
			// 
			// taskIdleDelayCombo
			// 
			this.taskIdleDelayCombo.FormattingEnabled = true;
			this.taskIdleDelayCombo.Items.AddRange(new object[] {
            resources.GetString("taskIdleDelayCombo.Items"),
            resources.GetString("taskIdleDelayCombo.Items1"),
            resources.GetString("taskIdleDelayCombo.Items2"),
            resources.GetString("taskIdleDelayCombo.Items3"),
            resources.GetString("taskIdleDelayCombo.Items4"),
            resources.GetString("taskIdleDelayCombo.Items5"),
            resources.GetString("taskIdleDelayCombo.Items6"),
            resources.GetString("taskIdleDelayCombo.Items7")});
			resources.ApplyResources(this.taskIdleDelayCombo, "taskIdleDelayCombo");
			this.taskIdleDelayCombo.Name = "taskIdleDelayCombo";
			// 
			// taskStartAfterIdleCombo
			// 
			this.taskStartAfterIdleCombo.FormattingEnabled = true;
			this.taskStartAfterIdleCombo.Items.AddRange(new object[] {
            resources.GetString("taskStartAfterIdleCombo.Items"),
            resources.GetString("taskStartAfterIdleCombo.Items1"),
            resources.GetString("taskStartAfterIdleCombo.Items2"),
            resources.GetString("taskStartAfterIdleCombo.Items3"),
            resources.GetString("taskStartAfterIdleCombo.Items4"),
            resources.GetString("taskStartAfterIdleCombo.Items5")});
			resources.ApplyResources(this.taskStartAfterIdleCombo, "taskStartAfterIdleCombo");
			this.taskStartAfterIdleCombo.Name = "taskStartAfterIdleCombo";
			// 
			// taskRestartWhenIdleCheck
			// 
			resources.ApplyResources(this.taskRestartWhenIdleCheck, "taskRestartWhenIdleCheck");
			this.taskRestartWhenIdleCheck.Name = "taskRestartWhenIdleCheck";
			this.taskRestartWhenIdleCheck.UseVisualStyleBackColor = true;
			// 
			// taskStopIfNotIdleCheck
			// 
			resources.ApplyResources(this.taskStopIfNotIdleCheck, "taskStopIfNotIdleCheck");
			this.taskStopIfNotIdleCheck.Name = "taskStopIfNotIdleCheck";
			this.taskStopIfNotIdleCheck.UseVisualStyleBackColor = true;
			this.taskStopIfNotIdleCheck.CheckedChanged += new System.EventHandler(this.taskStopIfNotIdleCheck_CheckedChanged);
			// 
			// taskIdleDelayLabel
			// 
			resources.ApplyResources(this.taskIdleDelayLabel, "taskIdleDelayLabel");
			this.taskIdleDelayLabel.Name = "taskIdleDelayLabel";
			// 
			// taskStartAfterIdleCheck
			// 
			resources.ApplyResources(this.taskStartAfterIdleCheck, "taskStartAfterIdleCheck");
			this.taskStartAfterIdleCheck.Name = "taskStartAfterIdleCheck";
			this.taskStartAfterIdleCheck.UseVisualStyleBackColor = true;
			this.taskStartAfterIdleCheck.CheckedChanged += new System.EventHandler(this.taskStartAfterIdleCheck_CheckedChanged);
			// 
			// conditionIntroLabel
			// 
			resources.ApplyResources(this.conditionIntroLabel, "conditionIntroLabel");
			this.conditionIntroLabel.Name = "conditionIntroLabel";
			// 
			// settingsTab
			// 
			this.settingsTab.Controls.Add(this.taskRestartAttemptsText);
			this.settingsTab.Controls.Add(this.taskDeleteAfterCombo);
			this.settingsTab.Controls.Add(this.taskStopIfRunningAfterCombo);
			this.settingsTab.Controls.Add(this.taskRestartAfterSpanCombo);
			this.settingsTab.Controls.Add(this.taskRunningRuleCombo);
			this.settingsTab.Controls.Add(this.taskRunningRuleLabel);
			this.settingsTab.Controls.Add(this.taskRestartAttemptTimesLabel);
			this.settingsTab.Controls.Add(this.taskRestartAttemptsLabel);
			this.settingsTab.Controls.Add(this.taskDeleteAfterCheck);
			this.settingsTab.Controls.Add(this.taskForceStopCheck);
			this.settingsTab.Controls.Add(this.taskStopIfRunningAfterCheck);
			this.settingsTab.Controls.Add(this.taskRestartAfterFailureCheck);
			this.settingsTab.Controls.Add(this.taskRunAfterMissedCheck);
			this.settingsTab.Controls.Add(this.taskRunOnDemandCheck);
			this.settingsTab.Controls.Add(this.settingsIntroLabel);
			resources.ApplyResources(this.settingsTab, "settingsTab");
			this.settingsTab.Name = "settingsTab";
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// taskRestartAttemptsText
			// 
			resources.ApplyResources(this.taskRestartAttemptsText, "taskRestartAttemptsText");
			this.taskRestartAttemptsText.Name = "taskRestartAttemptsText";
			// 
			// taskDeleteAfterCombo
			// 
			this.taskDeleteAfterCombo.FormattingEnabled = true;
			this.taskDeleteAfterCombo.Items.AddRange(new object[] {
            resources.GetString("taskDeleteAfterCombo.Items"),
            resources.GetString("taskDeleteAfterCombo.Items1"),
            resources.GetString("taskDeleteAfterCombo.Items2"),
            resources.GetString("taskDeleteAfterCombo.Items3"),
            resources.GetString("taskDeleteAfterCombo.Items4")});
			resources.ApplyResources(this.taskDeleteAfterCombo, "taskDeleteAfterCombo");
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			// 
			// taskStopIfRunningAfterCombo
			// 
			this.taskStopIfRunningAfterCombo.FormattingEnabled = true;
			this.taskStopIfRunningAfterCombo.Items.AddRange(new object[] {
            resources.GetString("taskStopIfRunningAfterCombo.Items"),
            resources.GetString("taskStopIfRunningAfterCombo.Items1"),
            resources.GetString("taskStopIfRunningAfterCombo.Items2"),
            resources.GetString("taskStopIfRunningAfterCombo.Items3"),
            resources.GetString("taskStopIfRunningAfterCombo.Items4"),
            resources.GetString("taskStopIfRunningAfterCombo.Items5"),
            resources.GetString("taskStopIfRunningAfterCombo.Items6")});
			resources.ApplyResources(this.taskStopIfRunningAfterCombo, "taskStopIfRunningAfterCombo");
			this.taskStopIfRunningAfterCombo.Name = "taskStopIfRunningAfterCombo";
			// 
			// taskRestartAfterSpanCombo
			// 
			this.taskRestartAfterSpanCombo.FormattingEnabled = true;
			this.taskRestartAfterSpanCombo.Items.AddRange(new object[] {
            resources.GetString("taskRestartAfterSpanCombo.Items"),
            resources.GetString("taskRestartAfterSpanCombo.Items1"),
            resources.GetString("taskRestartAfterSpanCombo.Items2"),
            resources.GetString("taskRestartAfterSpanCombo.Items3"),
            resources.GetString("taskRestartAfterSpanCombo.Items4"),
            resources.GetString("taskRestartAfterSpanCombo.Items5"),
            resources.GetString("taskRestartAfterSpanCombo.Items6")});
			resources.ApplyResources(this.taskRestartAfterSpanCombo, "taskRestartAfterSpanCombo");
			this.taskRestartAfterSpanCombo.Name = "taskRestartAfterSpanCombo";
			// 
			// taskRunningRuleCombo
			// 
			this.taskRunningRuleCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskRunningRuleCombo.FormattingEnabled = true;
			this.taskRunningRuleCombo.Items.AddRange(new object[] {
            resources.GetString("taskRunningRuleCombo.Items"),
            resources.GetString("taskRunningRuleCombo.Items1"),
            resources.GetString("taskRunningRuleCombo.Items2"),
            resources.GetString("taskRunningRuleCombo.Items3")});
			resources.ApplyResources(this.taskRunningRuleCombo, "taskRunningRuleCombo");
			this.taskRunningRuleCombo.Name = "taskRunningRuleCombo";
			// 
			// taskRunningRuleLabel
			// 
			resources.ApplyResources(this.taskRunningRuleLabel, "taskRunningRuleLabel");
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			// 
			// taskRestartAttemptTimesLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptTimesLabel, "taskRestartAttemptTimesLabel");
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			// 
			// taskRestartAttemptsLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptsLabel, "taskRestartAttemptsLabel");
			this.taskRestartAttemptsLabel.Name = "taskRestartAttemptsLabel";
			// 
			// taskDeleteAfterCheck
			// 
			resources.ApplyResources(this.taskDeleteAfterCheck, "taskDeleteAfterCheck");
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new System.EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskForceStopCheck
			// 
			resources.ApplyResources(this.taskForceStopCheck, "taskForceStopCheck");
			this.taskForceStopCheck.Name = "taskForceStopCheck";
			this.taskForceStopCheck.UseVisualStyleBackColor = true;
			// 
			// taskStopIfRunningAfterCheck
			// 
			resources.ApplyResources(this.taskStopIfRunningAfterCheck, "taskStopIfRunningAfterCheck");
			this.taskStopIfRunningAfterCheck.Name = "taskStopIfRunningAfterCheck";
			this.taskStopIfRunningAfterCheck.UseVisualStyleBackColor = true;
			this.taskStopIfRunningAfterCheck.CheckedChanged += new System.EventHandler(this.taskStopIfRunningAfterCheck_CheckedChanged);
			// 
			// taskRestartAfterFailureCheck
			// 
			resources.ApplyResources(this.taskRestartAfterFailureCheck, "taskRestartAfterFailureCheck");
			this.taskRestartAfterFailureCheck.Name = "taskRestartAfterFailureCheck";
			this.taskRestartAfterFailureCheck.UseVisualStyleBackColor = true;
			this.taskRestartAfterFailureCheck.CheckedChanged += new System.EventHandler(this.taskRestartAfterFailureCheck_CheckedChanged);
			// 
			// taskRunAfterMissedCheck
			// 
			resources.ApplyResources(this.taskRunAfterMissedCheck, "taskRunAfterMissedCheck");
			this.taskRunAfterMissedCheck.Name = "taskRunAfterMissedCheck";
			this.taskRunAfterMissedCheck.UseVisualStyleBackColor = true;
			// 
			// taskRunOnDemandCheck
			// 
			resources.ApplyResources(this.taskRunOnDemandCheck, "taskRunOnDemandCheck");
			this.taskRunOnDemandCheck.Name = "taskRunOnDemandCheck";
			this.taskRunOnDemandCheck.UseVisualStyleBackColor = true;
			// 
			// settingsIntroLabel
			// 
			resources.ApplyResources(this.settingsIntroLabel, "settingsIntroLabel");
			this.settingsIntroLabel.Name = "settingsIntroLabel";
			// 
			// historyTab
			// 
			this.historyTab.Controls.Add(this.historyListView);
			resources.ApplyResources(this.historyTab, "historyTab");
			this.historyTab.Name = "historyTab";
			this.historyTab.UseVisualStyleBackColor = true;
			this.historyTab.Enter += new System.EventHandler(this.historyTab_Enter);
			// 
			// historyListView
			// 
			resources.ApplyResources(this.historyListView, "historyListView");
			this.historyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
			this.historyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.historyListView.HideSelection = false;
			this.historyListView.Name = "historyListView";
			this.historyListView.UseCompatibleStateImageBehavior = false;
			this.historyListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			resources.ApplyResources(this.columnHeader6, "columnHeader6");
			// 
			// columnHeader7
			// 
			resources.ApplyResources(this.columnHeader7, "columnHeader7");
			// 
			// columnHeader8
			// 
			resources.ApplyResources(this.columnHeader8, "columnHeader8");
			// 
			// columnHeader9
			// 
			resources.ApplyResources(this.columnHeader9, "columnHeader9");
			// 
			// columnHeader10
			// 
			resources.ApplyResources(this.columnHeader10, "columnHeader10");
			// 
			// columnHeader11
			// 
			resources.ApplyResources(this.columnHeader11, "columnHeader11");
			// 
			// historyBackgroundWorker
			// 
			this.historyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.historyBackgroundWorker_DoWork);
			// 
			// TaskPropertiesControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.MinimumSize = new System.Drawing.Size(533, 375);
			this.Name = "TaskPropertiesControl";
			this.tabControl1.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.generalTab.PerformLayout();
			this.taskSecurityGroupBox.ResumeLayout(false);
			this.taskSecurityGroupBox.PerformLayout();
			this.triggersTab.ResumeLayout(false);
			this.actionsTab.ResumeLayout(false);
			this.conditionsTab.ResumeLayout(false);
			this.networkConditionGroupBox.ResumeLayout(false);
			this.networkConditionGroupBox.PerformLayout();
			this.powerConditionGroupBox.ResumeLayout(false);
			this.powerConditionGroupBox.PerformLayout();
			this.idleConditionGroupBox.ResumeLayout(false);
			this.idleConditionGroupBox.PerformLayout();
			this.settingsTab.ResumeLayout(false);
			this.settingsTab.PerformLayout();
			this.historyTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.GroupBox taskSecurityGroupBox;
		private System.Windows.Forms.TabPage triggersTab;
		private System.Windows.Forms.TabPage actionsTab;
		private System.Windows.Forms.TabPage conditionsTab;
		private System.Windows.Forms.TabPage settingsTab;
		private System.Windows.Forms.TabPage historyTab;
		private System.Windows.Forms.Label taskUserAcctLabel;
		private System.Windows.Forms.TextBox taskPrincipalText;
		private System.Windows.Forms.Button changePrincipalButton;
		private System.Windows.Forms.CheckBox taskLocalOnlyCheck;
		private System.Windows.Forms.RadioButton taskLoggedOptionalRadio;
		private System.Windows.Forms.RadioButton taskLoggedOnRadio;
		private System.Windows.Forms.ComboBox taskVersionCombo;
		private System.Windows.Forms.Label taskVersionLabel;
		private System.Windows.Forms.CheckBox taskHiddenCheck;
		private System.Windows.Forms.CheckBox taskRunLevelCheck;
		private System.Windows.Forms.Button triggerDeleteButton;
		private System.Windows.Forms.Button triggerEditButton;
		private System.Windows.Forms.Button triggerNewButton;
		private System.Windows.Forms.ListView triggerListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label taskTriggerIntroLabel;
		private System.Windows.Forms.Button actionDeleteButton;
		private System.Windows.Forms.Button actionEditButton;
		private System.Windows.Forms.Button actionNewButton;
		private System.Windows.Forms.ListView actionListView;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label actionIntroLabel;
		private System.Windows.Forms.Button actionUpButton;
		private System.Windows.Forms.Button actionDownButton;
		private System.Windows.Forms.Label conditionIntroLabel;
		private System.Windows.Forms.Label taskNameLabel;
		private System.Windows.Forms.Label taskAuthorLabel;
		private System.Windows.Forms.Label taskDescLabel;
		private System.Windows.Forms.TextBox taskNameText;
		private System.Windows.Forms.Label taskAuthorText;
		private System.Windows.Forms.TextBox taskDescText;
		private System.Windows.Forms.GroupBox powerConditionGroupBox;
		private System.Windows.Forms.GroupBox idleConditionGroupBox;
		private System.Windows.Forms.ComboBox taskIdleDelayCombo;
		private System.Windows.Forms.ComboBox taskStartAfterIdleCombo;
		private System.Windows.Forms.CheckBox taskRestartWhenIdleCheck;
		private System.Windows.Forms.CheckBox taskStopIfNotIdleCheck;
		private System.Windows.Forms.Label taskIdleDelayLabel;
		private System.Windows.Forms.CheckBox taskStartAfterIdleCheck;
		private System.Windows.Forms.CheckBox taskStopIfBatteryCheck;
		private System.Windows.Forms.CheckBox taskWakeToRunCheck;
		private System.Windows.Forms.CheckBox taskStartOnlyOnACCheck;
		private System.Windows.Forms.GroupBox networkConditionGroupBox;
		private System.Windows.Forms.ComboBox availableConnectionsCombo;
		private System.Windows.Forms.CheckBox taskStartIfConnectionCheck;
		private System.Windows.Forms.ComboBox taskRunningRuleCombo;
		private System.Windows.Forms.Label taskRunningRuleLabel;
		private System.Windows.Forms.Label taskRestartAttemptsLabel;
		private System.Windows.Forms.CheckBox taskDeleteAfterCheck;
		private System.Windows.Forms.CheckBox taskForceStopCheck;
		private System.Windows.Forms.CheckBox taskStopIfRunningAfterCheck;
		private System.Windows.Forms.CheckBox taskRestartAfterFailureCheck;
		private System.Windows.Forms.CheckBox taskRunAfterMissedCheck;
		private System.Windows.Forms.CheckBox taskRunOnDemandCheck;
		private System.Windows.Forms.Label settingsIntroLabel;
		private System.Windows.Forms.ComboBox taskDeleteAfterCombo;
		private System.Windows.Forms.ComboBox taskStopIfRunningAfterCombo;
		private System.Windows.Forms.ComboBox taskRestartAfterSpanCombo;
		private System.Windows.Forms.TextBox taskRestartAttemptsText;
		private System.Windows.Forms.Label taskRestartAttemptTimesLabel;
		private System.Windows.Forms.ListView historyListView;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.ComponentModel.BackgroundWorker historyBackgroundWorker;
	}
}
