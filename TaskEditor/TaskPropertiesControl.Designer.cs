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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesControl));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.taskNameLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.taskAuthorLabel = new System.Windows.Forms.Label();
			this.taskDescLabel = new System.Windows.Forms.Label();
			this.taskNameText = new System.Windows.Forms.TextBox();
			this.taskLocationText = new System.Windows.Forms.Label();
			this.taskAuthorText = new System.Windows.Forms.Label();
			this.taskDescText = new System.Windows.Forms.TextBox();
			this.taskVersionCombo = new System.Windows.Forms.DisabledItemComboBox();
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
			this.triggerCollectionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.TriggerCollectionUI();
			this.taskTriggerIntroLabel = new System.Windows.Forms.Label();
			this.actionsTab = new System.Windows.Forms.TabPage();
			this.actionCollectionUI = new Microsoft.Win32.TaskScheduler.UIComponents.ActionCollectionUI();
			this.actionIntroLabel = new System.Windows.Forms.Label();
			this.conditionsTab = new System.Windows.Forms.TabPage();
			this.networkConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.availableConnectionsCombo = new System.Windows.Forms.ComboBox();
			this.taskStartIfConnectionCheck = new System.Windows.Forms.CheckBox();
			this.powerConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.taskStopIfGoingOnBatteriesCheck = new System.Windows.Forms.CheckBox();
			this.taskWakeToRunCheck = new System.Windows.Forms.CheckBox();
			this.taskDisallowStartIfOnBatteriesCheck = new System.Windows.Forms.CheckBox();
			this.idleConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.taskIdleWaitTimeoutCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskIdleDurationCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskRestartOnIdleCheck = new System.Windows.Forms.CheckBox();
			this.taskStopOnIdleEndCheck = new System.Windows.Forms.CheckBox();
			this.taskIdleWaitTimeoutLabel = new System.Windows.Forms.Label();
			this.taskIdleDurationCheck = new System.Windows.Forms.CheckBox();
			this.conditionIntroLabel = new System.Windows.Forms.Label();
			this.settingsTab = new System.Windows.Forms.TabPage();
			this.taskRestartCountText = new System.Windows.Forms.NumericUpDown();
			this.taskMultInstCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleLabel = new System.Windows.Forms.Label();
			this.taskRestartAttemptTimesLabel = new System.Windows.Forms.Label();
			this.taskRestartCountLabel = new System.Windows.Forms.Label();
			this.taskDeleteAfterCheck = new System.Windows.Forms.CheckBox();
			this.taskAllowHardTerminateCheck = new System.Windows.Forms.CheckBox();
			this.taskExecutionTimeLimitCheck = new System.Windows.Forms.CheckBox();
			this.taskRestartIntervalCheck = new System.Windows.Forms.CheckBox();
			this.taskStartWhenAvailableCheck = new System.Windows.Forms.CheckBox();
			this.taskAllowDemandStartCheck = new System.Windows.Forms.CheckBox();
			this.settingsIntroLabel = new System.Windows.Forms.Label();
			this.taskDeleteAfterCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskExecutionTimeLimitCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskRestartIntervalCombo = new System.Windows.Forms.TimeSpanPicker();
			this.regInfoTab = new System.Windows.Forms.TabPage();
			this.taskRegLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.taskRegSourceLabel = new System.Windows.Forms.Label();
			this.taskRegURILabel = new System.Windows.Forms.Label();
			this.taskRegDocText = new System.Windows.Forms.TextBox();
			this.taskRegDocLabel = new System.Windows.Forms.Label();
			this.taskRegSDDLLabel = new System.Windows.Forms.Label();
			this.taskRegSourceText = new System.Windows.Forms.TextBox();
			this.taskRegURIText = new System.Windows.Forms.TextBox();
			this.taskRegVersionText = new System.Windows.Forms.TextBox();
			this.taskRegSDDLText = new System.Windows.Forms.TextBox();
			this.taskRegVersionLabel = new System.Windows.Forms.Label();
			this.taskRegSDDLBtn = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.addPropTab = new System.Windows.Forms.TabPage();
			this.autoMaintGroup = new System.Windows.Forms.GroupBox();
			this.taskMaintenanceDeadlineCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskMaintenanceExclusiveCheck = new System.Windows.Forms.CheckBox();
			this.taskMaintenanceDeadlineLabel = new System.Windows.Forms.Label();
			this.taskMaintenancePeriodLabel = new System.Windows.Forms.Label();
			this.taskMaintenancePeriodCombo = new System.Windows.Forms.TimeSpanPicker();
			this.secHardGroup = new System.Windows.Forms.GroupBox();
			this.principalSIDTypeLabel = new System.Windows.Forms.Label();
			this.principalSIDTypeCombo = new System.Windows.Forms.ComboBox();
			this.principalReqPrivilegesLabel = new System.Windows.Forms.Label();
			this.principalReqPrivilegesDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.panel1 = new System.Windows.Forms.Panel();
			this.taskEnabledCheck = new System.Windows.Forms.CheckBox();
			this.taskDisallowStartOnRemoteAppSessionCheck = new System.Windows.Forms.CheckBox();
			this.taskUseUnifiedSchedulingEngineCheck = new System.Windows.Forms.CheckBox();
			this.taskPriorityCombo = new System.Windows.Forms.ComboBox();
			this.taskVolatileCheck = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.runTimesTab = new System.Windows.Forms.TabPage();
			this.taskRunTimesControl1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesControl();
			this.runTimesErrorLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.historyTab = new System.Windows.Forms.TabPage();
			this.taskHistoryControl1 = new Microsoft.Win32.TaskScheduler.TaskHistoryControl();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.tabControl.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.taskSecurityGroupBox.SuspendLayout();
			this.triggersTab.SuspendLayout();
			this.actionsTab.SuspendLayout();
			this.conditionsTab.SuspendLayout();
			this.networkConditionGroupBox.SuspendLayout();
			this.powerConditionGroupBox.SuspendLayout();
			this.idleConditionGroupBox.SuspendLayout();
			this.settingsTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).BeginInit();
			this.regInfoTab.SuspendLayout();
			this.taskRegLayoutPanel.SuspendLayout();
			this.addPropTab.SuspendLayout();
			this.autoMaintGroup.SuspendLayout();
			this.secHardGroup.SuspendLayout();
			this.panel1.SuspendLayout();
			this.runTimesTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).BeginInit();
			this.historyTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			resources.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Controls.Add(this.generalTab);
			this.tabControl.Controls.Add(this.triggersTab);
			this.tabControl.Controls.Add(this.actionsTab);
			this.tabControl.Controls.Add(this.conditionsTab);
			this.tabControl.Controls.Add(this.settingsTab);
			this.tabControl.Controls.Add(this.regInfoTab);
			this.tabControl.Controls.Add(this.addPropTab);
			this.tabControl.Controls.Add(this.runTimesTab);
			this.tabControl.Controls.Add(this.historyTab);
			this.errorProvider.SetError(this.tabControl, resources.GetString("tabControl.Error"));
			this.helpProvider.SetHelpKeyword(this.tabControl, resources.GetString("tabControl.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.tabControl, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tabControl.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.tabControl, resources.GetString("tabControl.HelpString"));
			this.errorProvider.SetIconAlignment(this.tabControl, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tabControl.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.tabControl, ((int)(resources.GetObject("tabControl.IconPadding"))));
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.helpProvider.SetShowHelp(this.tabControl, ((bool)(resources.GetObject("tabControl.ShowHelp"))));
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
			// 
			// generalTab
			// 
			resources.ApplyResources(this.generalTab, "generalTab");
			this.generalTab.Controls.Add(this.taskNameLabel);
			this.generalTab.Controls.Add(this.label2);
			this.generalTab.Controls.Add(this.taskAuthorLabel);
			this.generalTab.Controls.Add(this.taskDescLabel);
			this.generalTab.Controls.Add(this.taskNameText);
			this.generalTab.Controls.Add(this.taskLocationText);
			this.generalTab.Controls.Add(this.taskAuthorText);
			this.generalTab.Controls.Add(this.taskDescText);
			this.generalTab.Controls.Add(this.taskVersionCombo);
			this.generalTab.Controls.Add(this.taskVersionLabel);
			this.generalTab.Controls.Add(this.taskHiddenCheck);
			this.generalTab.Controls.Add(this.taskSecurityGroupBox);
			this.errorProvider.SetError(this.generalTab, resources.GetString("generalTab.Error"));
			this.helpProvider.SetHelpKeyword(this.generalTab, resources.GetString("generalTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.generalTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("generalTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.generalTab, resources.GetString("generalTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.generalTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("generalTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.generalTab, ((int)(resources.GetObject("generalTab.IconPadding"))));
			this.generalTab.Name = "generalTab";
			this.helpProvider.SetShowHelp(this.generalTab, ((bool)(resources.GetObject("generalTab.ShowHelp"))));
			this.generalTab.UseVisualStyleBackColor = true;
			this.generalTab.Enter += new System.EventHandler(this.generalTab_Enter);
			// 
			// taskNameLabel
			// 
			resources.ApplyResources(this.taskNameLabel, "taskNameLabel");
			this.errorProvider.SetError(this.taskNameLabel, resources.GetString("taskNameLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskNameLabel, resources.GetString("taskNameLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskNameLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskNameLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskNameLabel, resources.GetString("taskNameLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskNameLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskNameLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskNameLabel, ((int)(resources.GetObject("taskNameLabel.IconPadding"))));
			this.taskNameLabel.Name = "taskNameLabel";
			this.helpProvider.SetShowHelp(this.taskNameLabel, ((bool)(resources.GetObject("taskNameLabel.ShowHelp"))));
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.errorProvider.SetError(this.label2, resources.GetString("label2.Error"));
			this.helpProvider.SetHelpKeyword(this.label2, resources.GetString("label2.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label2.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label2, resources.GetString("label2.HelpString"));
			this.errorProvider.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
			this.label2.Name = "label2";
			this.helpProvider.SetShowHelp(this.label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
			// 
			// taskAuthorLabel
			// 
			resources.ApplyResources(this.taskAuthorLabel, "taskAuthorLabel");
			this.errorProvider.SetError(this.taskAuthorLabel, resources.GetString("taskAuthorLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskAuthorLabel, resources.GetString("taskAuthorLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskAuthorLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskAuthorLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskAuthorLabel, resources.GetString("taskAuthorLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskAuthorLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskAuthorLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskAuthorLabel, ((int)(resources.GetObject("taskAuthorLabel.IconPadding"))));
			this.taskAuthorLabel.Name = "taskAuthorLabel";
			this.helpProvider.SetShowHelp(this.taskAuthorLabel, ((bool)(resources.GetObject("taskAuthorLabel.ShowHelp"))));
			// 
			// taskDescLabel
			// 
			resources.ApplyResources(this.taskDescLabel, "taskDescLabel");
			this.errorProvider.SetError(this.taskDescLabel, resources.GetString("taskDescLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDescLabel, resources.GetString("taskDescLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDescLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDescLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDescLabel, resources.GetString("taskDescLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDescLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDescLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDescLabel, ((int)(resources.GetObject("taskDescLabel.IconPadding"))));
			this.taskDescLabel.Name = "taskDescLabel";
			this.helpProvider.SetShowHelp(this.taskDescLabel, ((bool)(resources.GetObject("taskDescLabel.ShowHelp"))));
			// 
			// taskNameText
			// 
			resources.ApplyResources(this.taskNameText, "taskNameText");
			this.errorProvider.SetError(this.taskNameText, resources.GetString("taskNameText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskNameText, resources.GetString("taskNameText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskNameText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskNameText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskNameText, resources.GetString("taskNameText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskNameText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskNameText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskNameText, ((int)(resources.GetObject("taskNameText.IconPadding"))));
			this.taskNameText.Name = "taskNameText";
			this.taskNameText.ReadOnly = true;
			this.helpProvider.SetShowHelp(this.taskNameText, ((bool)(resources.GetObject("taskNameText.ShowHelp"))));
			this.taskNameText.Validating += new System.ComponentModel.CancelEventHandler(this.taskNameText_Validating);
			this.taskNameText.Validated += new System.EventHandler(this.taskNameText_Validated);
			// 
			// taskLocationText
			// 
			resources.ApplyResources(this.taskLocationText, "taskLocationText");
			this.errorProvider.SetError(this.taskLocationText, resources.GetString("taskLocationText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskLocationText, resources.GetString("taskLocationText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskLocationText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskLocationText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskLocationText, resources.GetString("taskLocationText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskLocationText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskLocationText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskLocationText, ((int)(resources.GetObject("taskLocationText.IconPadding"))));
			this.taskLocationText.Name = "taskLocationText";
			this.helpProvider.SetShowHelp(this.taskLocationText, ((bool)(resources.GetObject("taskLocationText.ShowHelp"))));
			// 
			// taskAuthorText
			// 
			resources.ApplyResources(this.taskAuthorText, "taskAuthorText");
			this.errorProvider.SetError(this.taskAuthorText, resources.GetString("taskAuthorText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskAuthorText, resources.GetString("taskAuthorText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskAuthorText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskAuthorText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskAuthorText, resources.GetString("taskAuthorText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskAuthorText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskAuthorText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskAuthorText, ((int)(resources.GetObject("taskAuthorText.IconPadding"))));
			this.taskAuthorText.Name = "taskAuthorText";
			this.helpProvider.SetShowHelp(this.taskAuthorText, ((bool)(resources.GetObject("taskAuthorText.ShowHelp"))));
			// 
			// taskDescText
			// 
			resources.ApplyResources(this.taskDescText, "taskDescText");
			this.errorProvider.SetError(this.taskDescText, resources.GetString("taskDescText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDescText, resources.GetString("taskDescText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDescText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDescText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDescText, resources.GetString("taskDescText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDescText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDescText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDescText, ((int)(resources.GetObject("taskDescText.IconPadding"))));
			this.taskDescText.Name = "taskDescText";
			this.helpProvider.SetShowHelp(this.taskDescText, ((bool)(resources.GetObject("taskDescText.ShowHelp"))));
			this.taskDescText.Leave += new System.EventHandler(this.taskDescText_Leave);
			// 
			// taskVersionCombo
			// 
			resources.ApplyResources(this.taskVersionCombo, "taskVersionCombo");
			this.errorProvider.SetError(this.taskVersionCombo, resources.GetString("taskVersionCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskVersionCombo, resources.GetString("taskVersionCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskVersionCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskVersionCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskVersionCombo, resources.GetString("taskVersionCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskVersionCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskVersionCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskVersionCombo, ((int)(resources.GetObject("taskVersionCombo.IconPadding"))));
			this.taskVersionCombo.Name = "taskVersionCombo";
			this.helpProvider.SetShowHelp(this.taskVersionCombo, ((bool)(resources.GetObject("taskVersionCombo.ShowHelp"))));
			this.taskVersionCombo.SelectedIndexChanged += new System.EventHandler(this.taskVersionCombo_SelectedIndexChanged);
			this.taskVersionCombo.GotFocus += new System.EventHandler(this.taskVersionCombo_GotFocus);
			// 
			// taskVersionLabel
			// 
			resources.ApplyResources(this.taskVersionLabel, "taskVersionLabel");
			this.errorProvider.SetError(this.taskVersionLabel, resources.GetString("taskVersionLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskVersionLabel, resources.GetString("taskVersionLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskVersionLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskVersionLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskVersionLabel, resources.GetString("taskVersionLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskVersionLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskVersionLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskVersionLabel, ((int)(resources.GetObject("taskVersionLabel.IconPadding"))));
			this.taskVersionLabel.Name = "taskVersionLabel";
			this.helpProvider.SetShowHelp(this.taskVersionLabel, ((bool)(resources.GetObject("taskVersionLabel.ShowHelp"))));
			// 
			// taskHiddenCheck
			// 
			resources.ApplyResources(this.taskHiddenCheck, "taskHiddenCheck");
			this.errorProvider.SetError(this.taskHiddenCheck, resources.GetString("taskHiddenCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskHiddenCheck, resources.GetString("taskHiddenCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskHiddenCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskHiddenCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskHiddenCheck, resources.GetString("taskHiddenCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskHiddenCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskHiddenCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskHiddenCheck, ((int)(resources.GetObject("taskHiddenCheck.IconPadding"))));
			this.taskHiddenCheck.Name = "taskHiddenCheck";
			this.helpProvider.SetShowHelp(this.taskHiddenCheck, ((bool)(resources.GetObject("taskHiddenCheck.ShowHelp"))));
			this.taskHiddenCheck.UseVisualStyleBackColor = true;
			this.taskHiddenCheck.CheckedChanged += new System.EventHandler(this.taskHiddenCheck_CheckedChanged);
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
			this.errorProvider.SetError(this.taskSecurityGroupBox, resources.GetString("taskSecurityGroupBox.Error"));
			this.helpProvider.SetHelpKeyword(this.taskSecurityGroupBox, resources.GetString("taskSecurityGroupBox.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskSecurityGroupBox, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskSecurityGroupBox.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskSecurityGroupBox, resources.GetString("taskSecurityGroupBox.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskSecurityGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskSecurityGroupBox.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskSecurityGroupBox, ((int)(resources.GetObject("taskSecurityGroupBox.IconPadding"))));
			this.taskSecurityGroupBox.Name = "taskSecurityGroupBox";
			this.helpProvider.SetShowHelp(this.taskSecurityGroupBox, ((bool)(resources.GetObject("taskSecurityGroupBox.ShowHelp"))));
			this.taskSecurityGroupBox.TabStop = false;
			// 
			// taskRunLevelCheck
			// 
			resources.ApplyResources(this.taskRunLevelCheck, "taskRunLevelCheck");
			this.errorProvider.SetError(this.taskRunLevelCheck, resources.GetString("taskRunLevelCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRunLevelCheck, resources.GetString("taskRunLevelCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRunLevelCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRunLevelCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRunLevelCheck, resources.GetString("taskRunLevelCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRunLevelCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRunLevelCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRunLevelCheck, ((int)(resources.GetObject("taskRunLevelCheck.IconPadding"))));
			this.taskRunLevelCheck.Name = "taskRunLevelCheck";
			this.helpProvider.SetShowHelp(this.taskRunLevelCheck, ((bool)(resources.GetObject("taskRunLevelCheck.ShowHelp"))));
			this.taskRunLevelCheck.UseVisualStyleBackColor = true;
			this.taskRunLevelCheck.CheckedChanged += new System.EventHandler(this.taskRunLevelCheck_CheckedChanged);
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.errorProvider.SetError(this.taskLocalOnlyCheck, resources.GetString("taskLocalOnlyCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskLocalOnlyCheck, resources.GetString("taskLocalOnlyCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskLocalOnlyCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskLocalOnlyCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskLocalOnlyCheck, resources.GetString("taskLocalOnlyCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskLocalOnlyCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskLocalOnlyCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskLocalOnlyCheck, ((int)(resources.GetObject("taskLocalOnlyCheck.IconPadding"))));
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.helpProvider.SetShowHelp(this.taskLocalOnlyCheck, ((bool)(resources.GetObject("taskLocalOnlyCheck.ShowHelp"))));
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			this.taskLocalOnlyCheck.CheckedChanged += new System.EventHandler(this.taskLocalOnlyCheck_CheckedChanged);
			// 
			// taskLoggedOptionalRadio
			// 
			resources.ApplyResources(this.taskLoggedOptionalRadio, "taskLoggedOptionalRadio");
			this.errorProvider.SetError(this.taskLoggedOptionalRadio, resources.GetString("taskLoggedOptionalRadio.Error"));
			this.helpProvider.SetHelpKeyword(this.taskLoggedOptionalRadio, resources.GetString("taskLoggedOptionalRadio.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskLoggedOptionalRadio, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskLoggedOptionalRadio.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskLoggedOptionalRadio, resources.GetString("taskLoggedOptionalRadio.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskLoggedOptionalRadio, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskLoggedOptionalRadio.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskLoggedOptionalRadio, ((int)(resources.GetObject("taskLoggedOptionalRadio.IconPadding"))));
			this.taskLoggedOptionalRadio.Name = "taskLoggedOptionalRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOptionalRadio, ((bool)(resources.GetObject("taskLoggedOptionalRadio.ShowHelp"))));
			this.taskLoggedOptionalRadio.TabStop = true;
			this.taskLoggedOptionalRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOptionalRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOptionalRadio_CheckedChanged);
			// 
			// taskLoggedOnRadio
			// 
			resources.ApplyResources(this.taskLoggedOnRadio, "taskLoggedOnRadio");
			this.errorProvider.SetError(this.taskLoggedOnRadio, resources.GetString("taskLoggedOnRadio.Error"));
			this.helpProvider.SetHelpKeyword(this.taskLoggedOnRadio, resources.GetString("taskLoggedOnRadio.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskLoggedOnRadio, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskLoggedOnRadio.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskLoggedOnRadio, resources.GetString("taskLoggedOnRadio.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskLoggedOnRadio, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskLoggedOnRadio.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskLoggedOnRadio, ((int)(resources.GetObject("taskLoggedOnRadio.IconPadding"))));
			this.taskLoggedOnRadio.Name = "taskLoggedOnRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOnRadio, ((bool)(resources.GetObject("taskLoggedOnRadio.ShowHelp"))));
			this.taskLoggedOnRadio.TabStop = true;
			this.taskLoggedOnRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOnRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOnRadio_CheckedChanged);
			// 
			// taskPrincipalText
			// 
			resources.ApplyResources(this.taskPrincipalText, "taskPrincipalText");
			this.errorProvider.SetError(this.taskPrincipalText, resources.GetString("taskPrincipalText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskPrincipalText, resources.GetString("taskPrincipalText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskPrincipalText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskPrincipalText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskPrincipalText, resources.GetString("taskPrincipalText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskPrincipalText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskPrincipalText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskPrincipalText, ((int)(resources.GetObject("taskPrincipalText.IconPadding"))));
			this.taskPrincipalText.Name = "taskPrincipalText";
			this.taskPrincipalText.ReadOnly = true;
			this.helpProvider.SetShowHelp(this.taskPrincipalText, ((bool)(resources.GetObject("taskPrincipalText.ShowHelp"))));
			this.taskPrincipalText.TabStop = false;
			// 
			// changePrincipalButton
			// 
			resources.ApplyResources(this.changePrincipalButton, "changePrincipalButton");
			this.errorProvider.SetError(this.changePrincipalButton, resources.GetString("changePrincipalButton.Error"));
			this.helpProvider.SetHelpKeyword(this.changePrincipalButton, resources.GetString("changePrincipalButton.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.changePrincipalButton, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("changePrincipalButton.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.changePrincipalButton, resources.GetString("changePrincipalButton.HelpString"));
			this.errorProvider.SetIconAlignment(this.changePrincipalButton, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("changePrincipalButton.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.changePrincipalButton, ((int)(resources.GetObject("changePrincipalButton.IconPadding"))));
			this.changePrincipalButton.Name = "changePrincipalButton";
			this.helpProvider.SetShowHelp(this.changePrincipalButton, ((bool)(resources.GetObject("changePrincipalButton.ShowHelp"))));
			this.changePrincipalButton.UseVisualStyleBackColor = true;
			this.changePrincipalButton.Click += new System.EventHandler(this.changePrincipalButton_Click);
			// 
			// taskUserAcctLabel
			// 
			resources.ApplyResources(this.taskUserAcctLabel, "taskUserAcctLabel");
			this.errorProvider.SetError(this.taskUserAcctLabel, resources.GetString("taskUserAcctLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskUserAcctLabel, resources.GetString("taskUserAcctLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskUserAcctLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskUserAcctLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskUserAcctLabel, resources.GetString("taskUserAcctLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskUserAcctLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskUserAcctLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskUserAcctLabel, ((int)(resources.GetObject("taskUserAcctLabel.IconPadding"))));
			this.taskUserAcctLabel.Name = "taskUserAcctLabel";
			this.helpProvider.SetShowHelp(this.taskUserAcctLabel, ((bool)(resources.GetObject("taskUserAcctLabel.ShowHelp"))));
			// 
			// triggersTab
			// 
			resources.ApplyResources(this.triggersTab, "triggersTab");
			this.triggersTab.Controls.Add(this.triggerCollectionUI1);
			this.triggersTab.Controls.Add(this.taskTriggerIntroLabel);
			this.errorProvider.SetError(this.triggersTab, resources.GetString("triggersTab.Error"));
			this.helpProvider.SetHelpKeyword(this.triggersTab, resources.GetString("triggersTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.triggersTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("triggersTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.triggersTab, resources.GetString("triggersTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.triggersTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("triggersTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.triggersTab, ((int)(resources.GetObject("triggersTab.IconPadding"))));
			this.triggersTab.Name = "triggersTab";
			this.helpProvider.SetShowHelp(this.triggersTab, ((bool)(resources.GetObject("triggersTab.ShowHelp"))));
			this.triggersTab.UseVisualStyleBackColor = true;
			// 
			// triggerCollectionUI1
			// 
			resources.ApplyResources(this.triggerCollectionUI1, "triggerCollectionUI1");
			this.errorProvider.SetError(this.triggerCollectionUI1, resources.GetString("triggerCollectionUI1.Error"));
			this.helpProvider.SetHelpKeyword(this.triggerCollectionUI1, resources.GetString("triggerCollectionUI1.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.triggerCollectionUI1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("triggerCollectionUI1.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.triggerCollectionUI1, resources.GetString("triggerCollectionUI1.HelpString"));
			this.errorProvider.SetIconAlignment(this.triggerCollectionUI1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("triggerCollectionUI1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.triggerCollectionUI1, ((int)(resources.GetObject("triggerCollectionUI1.IconPadding"))));
			this.triggerCollectionUI1.Name = "triggerCollectionUI1";
			this.helpProvider.SetShowHelp(this.triggerCollectionUI1, ((bool)(resources.GetObject("triggerCollectionUI1.ShowHelp"))));
			// 
			// taskTriggerIntroLabel
			// 
			resources.ApplyResources(this.taskTriggerIntroLabel, "taskTriggerIntroLabel");
			this.errorProvider.SetError(this.taskTriggerIntroLabel, resources.GetString("taskTriggerIntroLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskTriggerIntroLabel, resources.GetString("taskTriggerIntroLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskTriggerIntroLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskTriggerIntroLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskTriggerIntroLabel, resources.GetString("taskTriggerIntroLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskTriggerIntroLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskTriggerIntroLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskTriggerIntroLabel, ((int)(resources.GetObject("taskTriggerIntroLabel.IconPadding"))));
			this.taskTriggerIntroLabel.Name = "taskTriggerIntroLabel";
			this.helpProvider.SetShowHelp(this.taskTriggerIntroLabel, ((bool)(resources.GetObject("taskTriggerIntroLabel.ShowHelp"))));
			// 
			// actionsTab
			// 
			resources.ApplyResources(this.actionsTab, "actionsTab");
			this.actionsTab.Controls.Add(this.actionCollectionUI);
			this.actionsTab.Controls.Add(this.actionIntroLabel);
			this.errorProvider.SetError(this.actionsTab, resources.GetString("actionsTab.Error"));
			this.helpProvider.SetHelpKeyword(this.actionsTab, resources.GetString("actionsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.actionsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("actionsTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.actionsTab, resources.GetString("actionsTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.actionsTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("actionsTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.actionsTab, ((int)(resources.GetObject("actionsTab.IconPadding"))));
			this.actionsTab.Name = "actionsTab";
			this.helpProvider.SetShowHelp(this.actionsTab, ((bool)(resources.GetObject("actionsTab.ShowHelp"))));
			this.actionsTab.UseVisualStyleBackColor = true;
			// 
			// actionCollectionUI
			// 
			resources.ApplyResources(this.actionCollectionUI, "actionCollectionUI");
			this.errorProvider.SetError(this.actionCollectionUI, resources.GetString("actionCollectionUI.Error"));
			this.helpProvider.SetHelpKeyword(this.actionCollectionUI, resources.GetString("actionCollectionUI.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.actionCollectionUI, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("actionCollectionUI.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.actionCollectionUI, resources.GetString("actionCollectionUI.HelpString"));
			this.errorProvider.SetIconAlignment(this.actionCollectionUI, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("actionCollectionUI.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.actionCollectionUI, ((int)(resources.GetObject("actionCollectionUI.IconPadding"))));
			this.actionCollectionUI.Name = "actionCollectionUI";
			this.helpProvider.SetShowHelp(this.actionCollectionUI, ((bool)(resources.GetObject("actionCollectionUI.ShowHelp"))));
			// 
			// actionIntroLabel
			// 
			resources.ApplyResources(this.actionIntroLabel, "actionIntroLabel");
			this.errorProvider.SetError(this.actionIntroLabel, resources.GetString("actionIntroLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.actionIntroLabel, resources.GetString("actionIntroLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.actionIntroLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("actionIntroLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.actionIntroLabel, resources.GetString("actionIntroLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.actionIntroLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("actionIntroLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.actionIntroLabel, ((int)(resources.GetObject("actionIntroLabel.IconPadding"))));
			this.actionIntroLabel.Name = "actionIntroLabel";
			this.helpProvider.SetShowHelp(this.actionIntroLabel, ((bool)(resources.GetObject("actionIntroLabel.ShowHelp"))));
			// 
			// conditionsTab
			// 
			resources.ApplyResources(this.conditionsTab, "conditionsTab");
			this.conditionsTab.Controls.Add(this.networkConditionGroupBox);
			this.conditionsTab.Controls.Add(this.powerConditionGroupBox);
			this.conditionsTab.Controls.Add(this.idleConditionGroupBox);
			this.conditionsTab.Controls.Add(this.conditionIntroLabel);
			this.errorProvider.SetError(this.conditionsTab, resources.GetString("conditionsTab.Error"));
			this.helpProvider.SetHelpKeyword(this.conditionsTab, resources.GetString("conditionsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.conditionsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("conditionsTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.conditionsTab, resources.GetString("conditionsTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.conditionsTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("conditionsTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.conditionsTab, ((int)(resources.GetObject("conditionsTab.IconPadding"))));
			this.conditionsTab.Name = "conditionsTab";
			this.helpProvider.SetShowHelp(this.conditionsTab, ((bool)(resources.GetObject("conditionsTab.ShowHelp"))));
			this.conditionsTab.UseVisualStyleBackColor = true;
			this.conditionsTab.Enter += new System.EventHandler(this.conditionsTab_Enter);
			// 
			// networkConditionGroupBox
			// 
			resources.ApplyResources(this.networkConditionGroupBox, "networkConditionGroupBox");
			this.networkConditionGroupBox.Controls.Add(this.availableConnectionsCombo);
			this.networkConditionGroupBox.Controls.Add(this.taskStartIfConnectionCheck);
			this.errorProvider.SetError(this.networkConditionGroupBox, resources.GetString("networkConditionGroupBox.Error"));
			this.helpProvider.SetHelpKeyword(this.networkConditionGroupBox, resources.GetString("networkConditionGroupBox.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.networkConditionGroupBox, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("networkConditionGroupBox.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.networkConditionGroupBox, resources.GetString("networkConditionGroupBox.HelpString"));
			this.errorProvider.SetIconAlignment(this.networkConditionGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("networkConditionGroupBox.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.networkConditionGroupBox, ((int)(resources.GetObject("networkConditionGroupBox.IconPadding"))));
			this.networkConditionGroupBox.Name = "networkConditionGroupBox";
			this.helpProvider.SetShowHelp(this.networkConditionGroupBox, ((bool)(resources.GetObject("networkConditionGroupBox.ShowHelp"))));
			this.networkConditionGroupBox.TabStop = false;
			// 
			// availableConnectionsCombo
			// 
			resources.ApplyResources(this.availableConnectionsCombo, "availableConnectionsCombo");
			this.availableConnectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.errorProvider.SetError(this.availableConnectionsCombo, resources.GetString("availableConnectionsCombo.Error"));
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.helpProvider.SetHelpKeyword(this.availableConnectionsCombo, resources.GetString("availableConnectionsCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.availableConnectionsCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("availableConnectionsCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.availableConnectionsCombo, resources.GetString("availableConnectionsCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.availableConnectionsCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("availableConnectionsCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.availableConnectionsCombo, ((int)(resources.GetObject("availableConnectionsCombo.IconPadding"))));
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			this.helpProvider.SetShowHelp(this.availableConnectionsCombo, ((bool)(resources.GetObject("availableConnectionsCombo.ShowHelp"))));
			this.availableConnectionsCombo.SelectedIndexChanged += new System.EventHandler(this.availableConnectionsCombo_SelectedIndexChanged);
			// 
			// taskStartIfConnectionCheck
			// 
			resources.ApplyResources(this.taskStartIfConnectionCheck, "taskStartIfConnectionCheck");
			this.errorProvider.SetError(this.taskStartIfConnectionCheck, resources.GetString("taskStartIfConnectionCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskStartIfConnectionCheck, resources.GetString("taskStartIfConnectionCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskStartIfConnectionCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskStartIfConnectionCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskStartIfConnectionCheck, resources.GetString("taskStartIfConnectionCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskStartIfConnectionCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskStartIfConnectionCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskStartIfConnectionCheck, ((int)(resources.GetObject("taskStartIfConnectionCheck.IconPadding"))));
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.helpProvider.SetShowHelp(this.taskStartIfConnectionCheck, ((bool)(resources.GetObject("taskStartIfConnectionCheck.ShowHelp"))));
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new System.EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// powerConditionGroupBox
			// 
			resources.ApplyResources(this.powerConditionGroupBox, "powerConditionGroupBox");
			this.powerConditionGroupBox.Controls.Add(this.taskStopIfGoingOnBatteriesCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskWakeToRunCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskDisallowStartIfOnBatteriesCheck);
			this.errorProvider.SetError(this.powerConditionGroupBox, resources.GetString("powerConditionGroupBox.Error"));
			this.helpProvider.SetHelpKeyword(this.powerConditionGroupBox, resources.GetString("powerConditionGroupBox.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.powerConditionGroupBox, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("powerConditionGroupBox.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.powerConditionGroupBox, resources.GetString("powerConditionGroupBox.HelpString"));
			this.errorProvider.SetIconAlignment(this.powerConditionGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("powerConditionGroupBox.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.powerConditionGroupBox, ((int)(resources.GetObject("powerConditionGroupBox.IconPadding"))));
			this.powerConditionGroupBox.Name = "powerConditionGroupBox";
			this.helpProvider.SetShowHelp(this.powerConditionGroupBox, ((bool)(resources.GetObject("powerConditionGroupBox.ShowHelp"))));
			this.powerConditionGroupBox.TabStop = false;
			// 
			// taskStopIfGoingOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskStopIfGoingOnBatteriesCheck, "taskStopIfGoingOnBatteriesCheck");
			this.errorProvider.SetError(this.taskStopIfGoingOnBatteriesCheck, resources.GetString("taskStopIfGoingOnBatteriesCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskStopIfGoingOnBatteriesCheck, resources.GetString("taskStopIfGoingOnBatteriesCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskStopIfGoingOnBatteriesCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskStopIfGoingOnBatteriesCheck, resources.GetString("taskStopIfGoingOnBatteriesCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskStopIfGoingOnBatteriesCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskStopIfGoingOnBatteriesCheck, ((int)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.IconPadding"))));
			this.taskStopIfGoingOnBatteriesCheck.Name = "taskStopIfGoingOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskStopIfGoingOnBatteriesCheck, ((bool)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.ShowHelp"))));
			this.taskStopIfGoingOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskStopIfGoingOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskStopIfGoingOnBatteriesCheck_CheckedChanged);
			// 
			// taskWakeToRunCheck
			// 
			resources.ApplyResources(this.taskWakeToRunCheck, "taskWakeToRunCheck");
			this.errorProvider.SetError(this.taskWakeToRunCheck, resources.GetString("taskWakeToRunCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskWakeToRunCheck, resources.GetString("taskWakeToRunCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskWakeToRunCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskWakeToRunCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskWakeToRunCheck, resources.GetString("taskWakeToRunCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskWakeToRunCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskWakeToRunCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskWakeToRunCheck, ((int)(resources.GetObject("taskWakeToRunCheck.IconPadding"))));
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.helpProvider.SetShowHelp(this.taskWakeToRunCheck, ((bool)(resources.GetObject("taskWakeToRunCheck.ShowHelp"))));
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			this.taskWakeToRunCheck.CheckedChanged += new System.EventHandler(this.taskWakeToRunCheck_CheckedChanged);
			// 
			// taskDisallowStartIfOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskDisallowStartIfOnBatteriesCheck, "taskDisallowStartIfOnBatteriesCheck");
			this.errorProvider.SetError(this.taskDisallowStartIfOnBatteriesCheck, resources.GetString("taskDisallowStartIfOnBatteriesCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDisallowStartIfOnBatteriesCheck, resources.GetString("taskDisallowStartIfOnBatteriesCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDisallowStartIfOnBatteriesCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDisallowStartIfOnBatteriesCheck, resources.GetString("taskDisallowStartIfOnBatteriesCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDisallowStartIfOnBatteriesCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDisallowStartIfOnBatteriesCheck, ((int)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.IconPadding"))));
			this.taskDisallowStartIfOnBatteriesCheck.Name = "taskDisallowStartIfOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartIfOnBatteriesCheck, ((bool)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.ShowHelp"))));
			this.taskDisallowStartIfOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartIfOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartIfOnBatteriesCheck_CheckedChanged);
			// 
			// idleConditionGroupBox
			// 
			resources.ApplyResources(this.idleConditionGroupBox, "idleConditionGroupBox");
			this.idleConditionGroupBox.Controls.Add(this.taskIdleWaitTimeoutCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDurationCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskRestartOnIdleCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskStopOnIdleEndCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleWaitTimeoutLabel);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDurationCheck);
			this.errorProvider.SetError(this.idleConditionGroupBox, resources.GetString("idleConditionGroupBox.Error"));
			this.helpProvider.SetHelpKeyword(this.idleConditionGroupBox, resources.GetString("idleConditionGroupBox.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.idleConditionGroupBox, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("idleConditionGroupBox.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.idleConditionGroupBox, resources.GetString("idleConditionGroupBox.HelpString"));
			this.errorProvider.SetIconAlignment(this.idleConditionGroupBox, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("idleConditionGroupBox.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.idleConditionGroupBox, ((int)(resources.GetObject("idleConditionGroupBox.IconPadding"))));
			this.idleConditionGroupBox.Name = "idleConditionGroupBox";
			this.helpProvider.SetShowHelp(this.idleConditionGroupBox, ((bool)(resources.GetObject("idleConditionGroupBox.ShowHelp"))));
			this.idleConditionGroupBox.TabStop = false;
			// 
			// taskIdleWaitTimeoutCombo
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutCombo, "taskIdleWaitTimeoutCombo");
			this.errorProvider.SetError(this.taskIdleWaitTimeoutCombo, resources.GetString("taskIdleWaitTimeoutCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskIdleWaitTimeoutCombo, resources.GetString("taskIdleWaitTimeoutCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskIdleWaitTimeoutCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskIdleWaitTimeoutCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskIdleWaitTimeoutCombo, resources.GetString("taskIdleWaitTimeoutCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskIdleWaitTimeoutCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskIdleWaitTimeoutCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskIdleWaitTimeoutCombo, ((int)(resources.GetObject("taskIdleWaitTimeoutCombo.IconPadding"))));
			this.taskIdleWaitTimeoutCombo.Name = "taskIdleWaitTimeoutCombo";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutCombo, ((bool)(resources.GetObject("taskIdleWaitTimeoutCombo.ShowHelp"))));
			this.taskIdleWaitTimeoutCombo.ValueChanged += new System.EventHandler(this.taskIdleWaitTimeoutCombo_ValueChanged);
			// 
			// taskIdleDurationCombo
			// 
			resources.ApplyResources(this.taskIdleDurationCombo, "taskIdleDurationCombo");
			this.errorProvider.SetError(this.taskIdleDurationCombo, resources.GetString("taskIdleDurationCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskIdleDurationCombo, resources.GetString("taskIdleDurationCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskIdleDurationCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskIdleDurationCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskIdleDurationCombo, resources.GetString("taskIdleDurationCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskIdleDurationCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskIdleDurationCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskIdleDurationCombo, ((int)(resources.GetObject("taskIdleDurationCombo.IconPadding"))));
			this.taskIdleDurationCombo.Name = "taskIdleDurationCombo";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCombo, ((bool)(resources.GetObject("taskIdleDurationCombo.ShowHelp"))));
			this.taskIdleDurationCombo.ValueChanged += new System.EventHandler(this.taskIdleDurationCombo_ValueChanged);
			// 
			// taskRestartOnIdleCheck
			// 
			resources.ApplyResources(this.taskRestartOnIdleCheck, "taskRestartOnIdleCheck");
			this.errorProvider.SetError(this.taskRestartOnIdleCheck, resources.GetString("taskRestartOnIdleCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartOnIdleCheck, resources.GetString("taskRestartOnIdleCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartOnIdleCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartOnIdleCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartOnIdleCheck, resources.GetString("taskRestartOnIdleCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartOnIdleCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartOnIdleCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartOnIdleCheck, ((int)(resources.GetObject("taskRestartOnIdleCheck.IconPadding"))));
			this.taskRestartOnIdleCheck.Name = "taskRestartOnIdleCheck";
			this.helpProvider.SetShowHelp(this.taskRestartOnIdleCheck, ((bool)(resources.GetObject("taskRestartOnIdleCheck.ShowHelp"))));
			this.taskRestartOnIdleCheck.UseVisualStyleBackColor = true;
			this.taskRestartOnIdleCheck.CheckedChanged += new System.EventHandler(this.taskRestartOnIdleCheck_CheckedChanged);
			// 
			// taskStopOnIdleEndCheck
			// 
			resources.ApplyResources(this.taskStopOnIdleEndCheck, "taskStopOnIdleEndCheck");
			this.taskStopOnIdleEndCheck.Checked = true;
			this.taskStopOnIdleEndCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.errorProvider.SetError(this.taskStopOnIdleEndCheck, resources.GetString("taskStopOnIdleEndCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskStopOnIdleEndCheck, resources.GetString("taskStopOnIdleEndCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskStopOnIdleEndCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskStopOnIdleEndCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskStopOnIdleEndCheck, resources.GetString("taskStopOnIdleEndCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskStopOnIdleEndCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskStopOnIdleEndCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskStopOnIdleEndCheck, ((int)(resources.GetObject("taskStopOnIdleEndCheck.IconPadding"))));
			this.taskStopOnIdleEndCheck.Name = "taskStopOnIdleEndCheck";
			this.helpProvider.SetShowHelp(this.taskStopOnIdleEndCheck, ((bool)(resources.GetObject("taskStopOnIdleEndCheck.ShowHelp"))));
			this.taskStopOnIdleEndCheck.UseVisualStyleBackColor = true;
			this.taskStopOnIdleEndCheck.CheckedChanged += new System.EventHandler(this.taskStopOnIdleEndCheck_CheckedChanged);
			// 
			// taskIdleWaitTimeoutLabel
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutLabel, "taskIdleWaitTimeoutLabel");
			this.errorProvider.SetError(this.taskIdleWaitTimeoutLabel, resources.GetString("taskIdleWaitTimeoutLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskIdleWaitTimeoutLabel, resources.GetString("taskIdleWaitTimeoutLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskIdleWaitTimeoutLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskIdleWaitTimeoutLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskIdleWaitTimeoutLabel, resources.GetString("taskIdleWaitTimeoutLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskIdleWaitTimeoutLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskIdleWaitTimeoutLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskIdleWaitTimeoutLabel, ((int)(resources.GetObject("taskIdleWaitTimeoutLabel.IconPadding"))));
			this.taskIdleWaitTimeoutLabel.Name = "taskIdleWaitTimeoutLabel";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutLabel, ((bool)(resources.GetObject("taskIdleWaitTimeoutLabel.ShowHelp"))));
			// 
			// taskIdleDurationCheck
			// 
			resources.ApplyResources(this.taskIdleDurationCheck, "taskIdleDurationCheck");
			this.errorProvider.SetError(this.taskIdleDurationCheck, resources.GetString("taskIdleDurationCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskIdleDurationCheck, resources.GetString("taskIdleDurationCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskIdleDurationCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskIdleDurationCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskIdleDurationCheck, resources.GetString("taskIdleDurationCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskIdleDurationCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskIdleDurationCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskIdleDurationCheck, ((int)(resources.GetObject("taskIdleDurationCheck.IconPadding"))));
			this.taskIdleDurationCheck.Name = "taskIdleDurationCheck";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCheck, ((bool)(resources.GetObject("taskIdleDurationCheck.ShowHelp"))));
			this.taskIdleDurationCheck.UseVisualStyleBackColor = true;
			this.taskIdleDurationCheck.CheckedChanged += new System.EventHandler(this.taskIdleDurationCheck_CheckedChanged);
			// 
			// conditionIntroLabel
			// 
			resources.ApplyResources(this.conditionIntroLabel, "conditionIntroLabel");
			this.errorProvider.SetError(this.conditionIntroLabel, resources.GetString("conditionIntroLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.conditionIntroLabel, resources.GetString("conditionIntroLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.conditionIntroLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("conditionIntroLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.conditionIntroLabel, resources.GetString("conditionIntroLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.conditionIntroLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("conditionIntroLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.conditionIntroLabel, ((int)(resources.GetObject("conditionIntroLabel.IconPadding"))));
			this.conditionIntroLabel.Name = "conditionIntroLabel";
			this.helpProvider.SetShowHelp(this.conditionIntroLabel, ((bool)(resources.GetObject("conditionIntroLabel.ShowHelp"))));
			// 
			// settingsTab
			// 
			resources.ApplyResources(this.settingsTab, "settingsTab");
			this.settingsTab.Controls.Add(this.taskRestartCountText);
			this.settingsTab.Controls.Add(this.taskMultInstCombo);
			this.settingsTab.Controls.Add(this.taskRunningRuleLabel);
			this.settingsTab.Controls.Add(this.taskRestartAttemptTimesLabel);
			this.settingsTab.Controls.Add(this.taskRestartCountLabel);
			this.settingsTab.Controls.Add(this.taskDeleteAfterCheck);
			this.settingsTab.Controls.Add(this.taskAllowHardTerminateCheck);
			this.settingsTab.Controls.Add(this.taskExecutionTimeLimitCheck);
			this.settingsTab.Controls.Add(this.taskRestartIntervalCheck);
			this.settingsTab.Controls.Add(this.taskStartWhenAvailableCheck);
			this.settingsTab.Controls.Add(this.taskAllowDemandStartCheck);
			this.settingsTab.Controls.Add(this.settingsIntroLabel);
			this.settingsTab.Controls.Add(this.taskDeleteAfterCombo);
			this.settingsTab.Controls.Add(this.taskExecutionTimeLimitCombo);
			this.settingsTab.Controls.Add(this.taskRestartIntervalCombo);
			this.errorProvider.SetError(this.settingsTab, resources.GetString("settingsTab.Error"));
			this.helpProvider.SetHelpKeyword(this.settingsTab, resources.GetString("settingsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.settingsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("settingsTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.settingsTab, resources.GetString("settingsTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.settingsTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("settingsTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.settingsTab, ((int)(resources.GetObject("settingsTab.IconPadding"))));
			this.settingsTab.Name = "settingsTab";
			this.helpProvider.SetShowHelp(this.settingsTab, ((bool)(resources.GetObject("settingsTab.ShowHelp"))));
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// taskRestartCountText
			// 
			resources.ApplyResources(this.taskRestartCountText, "taskRestartCountText");
			this.errorProvider.SetError(this.taskRestartCountText, resources.GetString("taskRestartCountText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartCountText, resources.GetString("taskRestartCountText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartCountText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartCountText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartCountText, resources.GetString("taskRestartCountText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartCountText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartCountText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartCountText, ((int)(resources.GetObject("taskRestartCountText.IconPadding"))));
			this.taskRestartCountText.Name = "taskRestartCountText";
			this.helpProvider.SetShowHelp(this.taskRestartCountText, ((bool)(resources.GetObject("taskRestartCountText.ShowHelp"))));
			this.taskRestartCountText.ValueChanged += new System.EventHandler(this.taskRestartCountText_ValueChanged);
			// 
			// taskMultInstCombo
			// 
			resources.ApplyResources(this.taskMultInstCombo, "taskMultInstCombo");
			this.taskMultInstCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.errorProvider.SetError(this.taskMultInstCombo, resources.GetString("taskMultInstCombo.Error"));
			this.taskMultInstCombo.FormattingEnabled = true;
			this.helpProvider.SetHelpKeyword(this.taskMultInstCombo, resources.GetString("taskMultInstCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMultInstCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMultInstCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMultInstCombo, resources.GetString("taskMultInstCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMultInstCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMultInstCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMultInstCombo, ((int)(resources.GetObject("taskMultInstCombo.IconPadding"))));
			this.taskMultInstCombo.Name = "taskMultInstCombo";
			this.helpProvider.SetShowHelp(this.taskMultInstCombo, ((bool)(resources.GetObject("taskMultInstCombo.ShowHelp"))));
			this.taskMultInstCombo.SelectedIndexChanged += new System.EventHandler(this.taskMultInstCombo_SelectedIndexChanged);
			// 
			// taskRunningRuleLabel
			// 
			resources.ApplyResources(this.taskRunningRuleLabel, "taskRunningRuleLabel");
			this.errorProvider.SetError(this.taskRunningRuleLabel, resources.GetString("taskRunningRuleLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRunningRuleLabel, resources.GetString("taskRunningRuleLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRunningRuleLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRunningRuleLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRunningRuleLabel, resources.GetString("taskRunningRuleLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRunningRuleLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRunningRuleLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRunningRuleLabel, ((int)(resources.GetObject("taskRunningRuleLabel.IconPadding"))));
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			this.helpProvider.SetShowHelp(this.taskRunningRuleLabel, ((bool)(resources.GetObject("taskRunningRuleLabel.ShowHelp"))));
			// 
			// taskRestartAttemptTimesLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptTimesLabel, "taskRestartAttemptTimesLabel");
			this.errorProvider.SetError(this.taskRestartAttemptTimesLabel, resources.GetString("taskRestartAttemptTimesLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartAttemptTimesLabel, resources.GetString("taskRestartAttemptTimesLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartAttemptTimesLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartAttemptTimesLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartAttemptTimesLabel, resources.GetString("taskRestartAttemptTimesLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartAttemptTimesLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartAttemptTimesLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartAttemptTimesLabel, ((int)(resources.GetObject("taskRestartAttemptTimesLabel.IconPadding"))));
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			this.helpProvider.SetShowHelp(this.taskRestartAttemptTimesLabel, ((bool)(resources.GetObject("taskRestartAttemptTimesLabel.ShowHelp"))));
			// 
			// taskRestartCountLabel
			// 
			resources.ApplyResources(this.taskRestartCountLabel, "taskRestartCountLabel");
			this.errorProvider.SetError(this.taskRestartCountLabel, resources.GetString("taskRestartCountLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartCountLabel, resources.GetString("taskRestartCountLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartCountLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartCountLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartCountLabel, resources.GetString("taskRestartCountLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartCountLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartCountLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartCountLabel, ((int)(resources.GetObject("taskRestartCountLabel.IconPadding"))));
			this.taskRestartCountLabel.Name = "taskRestartCountLabel";
			this.helpProvider.SetShowHelp(this.taskRestartCountLabel, ((bool)(resources.GetObject("taskRestartCountLabel.ShowHelp"))));
			// 
			// taskDeleteAfterCheck
			// 
			resources.ApplyResources(this.taskDeleteAfterCheck, "taskDeleteAfterCheck");
			this.errorProvider.SetError(this.taskDeleteAfterCheck, resources.GetString("taskDeleteAfterCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDeleteAfterCheck, resources.GetString("taskDeleteAfterCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDeleteAfterCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDeleteAfterCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDeleteAfterCheck, resources.GetString("taskDeleteAfterCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDeleteAfterCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDeleteAfterCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDeleteAfterCheck, ((int)(resources.GetObject("taskDeleteAfterCheck.IconPadding"))));
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCheck, ((bool)(resources.GetObject("taskDeleteAfterCheck.ShowHelp"))));
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new System.EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskAllowHardTerminateCheck
			// 
			resources.ApplyResources(this.taskAllowHardTerminateCheck, "taskAllowHardTerminateCheck");
			this.errorProvider.SetError(this.taskAllowHardTerminateCheck, resources.GetString("taskAllowHardTerminateCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskAllowHardTerminateCheck, resources.GetString("taskAllowHardTerminateCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskAllowHardTerminateCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskAllowHardTerminateCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskAllowHardTerminateCheck, resources.GetString("taskAllowHardTerminateCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskAllowHardTerminateCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskAllowHardTerminateCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskAllowHardTerminateCheck, ((int)(resources.GetObject("taskAllowHardTerminateCheck.IconPadding"))));
			this.taskAllowHardTerminateCheck.Name = "taskAllowHardTerminateCheck";
			this.helpProvider.SetShowHelp(this.taskAllowHardTerminateCheck, ((bool)(resources.GetObject("taskAllowHardTerminateCheck.ShowHelp"))));
			this.taskAllowHardTerminateCheck.UseVisualStyleBackColor = true;
			this.taskAllowHardTerminateCheck.CheckedChanged += new System.EventHandler(this.taskAllowHardTerminateCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCheck
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCheck, "taskExecutionTimeLimitCheck");
			this.errorProvider.SetError(this.taskExecutionTimeLimitCheck, resources.GetString("taskExecutionTimeLimitCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskExecutionTimeLimitCheck, resources.GetString("taskExecutionTimeLimitCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskExecutionTimeLimitCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskExecutionTimeLimitCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskExecutionTimeLimitCheck, resources.GetString("taskExecutionTimeLimitCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskExecutionTimeLimitCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskExecutionTimeLimitCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskExecutionTimeLimitCheck, ((int)(resources.GetObject("taskExecutionTimeLimitCheck.IconPadding"))));
			this.taskExecutionTimeLimitCheck.Name = "taskExecutionTimeLimitCheck";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCheck, ((bool)(resources.GetObject("taskExecutionTimeLimitCheck.ShowHelp"))));
			this.taskExecutionTimeLimitCheck.UseVisualStyleBackColor = true;
			this.taskExecutionTimeLimitCheck.CheckedChanged += new System.EventHandler(this.taskExecutionTimeLimitCheck_CheckedChanged);
			// 
			// taskRestartIntervalCheck
			// 
			resources.ApplyResources(this.taskRestartIntervalCheck, "taskRestartIntervalCheck");
			this.errorProvider.SetError(this.taskRestartIntervalCheck, resources.GetString("taskRestartIntervalCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartIntervalCheck, resources.GetString("taskRestartIntervalCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartIntervalCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartIntervalCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartIntervalCheck, resources.GetString("taskRestartIntervalCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartIntervalCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartIntervalCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartIntervalCheck, ((int)(resources.GetObject("taskRestartIntervalCheck.IconPadding"))));
			this.taskRestartIntervalCheck.Name = "taskRestartIntervalCheck";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCheck, ((bool)(resources.GetObject("taskRestartIntervalCheck.ShowHelp"))));
			this.taskRestartIntervalCheck.UseVisualStyleBackColor = true;
			this.taskRestartIntervalCheck.CheckedChanged += new System.EventHandler(this.taskRestartIntervalCheck_CheckedChanged);
			// 
			// taskStartWhenAvailableCheck
			// 
			resources.ApplyResources(this.taskStartWhenAvailableCheck, "taskStartWhenAvailableCheck");
			this.errorProvider.SetError(this.taskStartWhenAvailableCheck, resources.GetString("taskStartWhenAvailableCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskStartWhenAvailableCheck, resources.GetString("taskStartWhenAvailableCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskStartWhenAvailableCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskStartWhenAvailableCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskStartWhenAvailableCheck, resources.GetString("taskStartWhenAvailableCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskStartWhenAvailableCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskStartWhenAvailableCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskStartWhenAvailableCheck, ((int)(resources.GetObject("taskStartWhenAvailableCheck.IconPadding"))));
			this.taskStartWhenAvailableCheck.Name = "taskStartWhenAvailableCheck";
			this.helpProvider.SetShowHelp(this.taskStartWhenAvailableCheck, ((bool)(resources.GetObject("taskStartWhenAvailableCheck.ShowHelp"))));
			this.taskStartWhenAvailableCheck.UseVisualStyleBackColor = true;
			this.taskStartWhenAvailableCheck.CheckedChanged += new System.EventHandler(this.taskStartWhenAvailableCheck_CheckedChanged);
			// 
			// taskAllowDemandStartCheck
			// 
			resources.ApplyResources(this.taskAllowDemandStartCheck, "taskAllowDemandStartCheck");
			this.errorProvider.SetError(this.taskAllowDemandStartCheck, resources.GetString("taskAllowDemandStartCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskAllowDemandStartCheck, resources.GetString("taskAllowDemandStartCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskAllowDemandStartCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskAllowDemandStartCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskAllowDemandStartCheck, resources.GetString("taskAllowDemandStartCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskAllowDemandStartCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskAllowDemandStartCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskAllowDemandStartCheck, ((int)(resources.GetObject("taskAllowDemandStartCheck.IconPadding"))));
			this.taskAllowDemandStartCheck.Name = "taskAllowDemandStartCheck";
			this.helpProvider.SetShowHelp(this.taskAllowDemandStartCheck, ((bool)(resources.GetObject("taskAllowDemandStartCheck.ShowHelp"))));
			this.taskAllowDemandStartCheck.UseVisualStyleBackColor = true;
			this.taskAllowDemandStartCheck.CheckedChanged += new System.EventHandler(this.taskAllowDemandStartCheck_CheckedChanged);
			// 
			// settingsIntroLabel
			// 
			resources.ApplyResources(this.settingsIntroLabel, "settingsIntroLabel");
			this.errorProvider.SetError(this.settingsIntroLabel, resources.GetString("settingsIntroLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.settingsIntroLabel, resources.GetString("settingsIntroLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.settingsIntroLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("settingsIntroLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.settingsIntroLabel, resources.GetString("settingsIntroLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.settingsIntroLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("settingsIntroLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.settingsIntroLabel, ((int)(resources.GetObject("settingsIntroLabel.IconPadding"))));
			this.settingsIntroLabel.Name = "settingsIntroLabel";
			this.helpProvider.SetShowHelp(this.settingsIntroLabel, ((bool)(resources.GetObject("settingsIntroLabel.ShowHelp"))));
			// 
			// taskDeleteAfterCombo
			// 
			resources.ApplyResources(this.taskDeleteAfterCombo, "taskDeleteAfterCombo");
			this.errorProvider.SetError(this.taskDeleteAfterCombo, resources.GetString("taskDeleteAfterCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDeleteAfterCombo, resources.GetString("taskDeleteAfterCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDeleteAfterCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDeleteAfterCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDeleteAfterCombo, resources.GetString("taskDeleteAfterCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDeleteAfterCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDeleteAfterCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDeleteAfterCombo, ((int)(resources.GetObject("taskDeleteAfterCombo.IconPadding"))));
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCombo, ((bool)(resources.GetObject("taskDeleteAfterCombo.ShowHelp"))));
			this.taskDeleteAfterCombo.ValueChanged += new System.EventHandler(this.taskDeleteAfterCombo_ValueChanged);
			// 
			// taskExecutionTimeLimitCombo
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCombo, "taskExecutionTimeLimitCombo");
			this.errorProvider.SetError(this.taskExecutionTimeLimitCombo, resources.GetString("taskExecutionTimeLimitCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskExecutionTimeLimitCombo, resources.GetString("taskExecutionTimeLimitCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskExecutionTimeLimitCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskExecutionTimeLimitCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskExecutionTimeLimitCombo, resources.GetString("taskExecutionTimeLimitCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskExecutionTimeLimitCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskExecutionTimeLimitCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskExecutionTimeLimitCombo, ((int)(resources.GetObject("taskExecutionTimeLimitCombo.IconPadding"))));
			this.taskExecutionTimeLimitCombo.Name = "taskExecutionTimeLimitCombo";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCombo, ((bool)(resources.GetObject("taskExecutionTimeLimitCombo.ShowHelp"))));
			this.taskExecutionTimeLimitCombo.ValueChanged += new System.EventHandler(this.taskExecutionTimeLimitCombo_ValueChanged);
			// 
			// taskRestartIntervalCombo
			// 
			resources.ApplyResources(this.taskRestartIntervalCombo, "taskRestartIntervalCombo");
			this.errorProvider.SetError(this.taskRestartIntervalCombo, resources.GetString("taskRestartIntervalCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRestartIntervalCombo, resources.GetString("taskRestartIntervalCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRestartIntervalCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRestartIntervalCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRestartIntervalCombo, resources.GetString("taskRestartIntervalCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRestartIntervalCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRestartIntervalCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRestartIntervalCombo, ((int)(resources.GetObject("taskRestartIntervalCombo.IconPadding"))));
			this.taskRestartIntervalCombo.Name = "taskRestartIntervalCombo";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCombo, ((bool)(resources.GetObject("taskRestartIntervalCombo.ShowHelp"))));
			this.taskRestartIntervalCombo.ValueChanged += new System.EventHandler(this.taskRestartIntervalCombo_ValueChanged);
			// 
			// regInfoTab
			// 
			resources.ApplyResources(this.regInfoTab, "regInfoTab");
			this.regInfoTab.Controls.Add(this.taskRegLayoutPanel);
			this.regInfoTab.Controls.Add(this.label5);
			this.errorProvider.SetError(this.regInfoTab, resources.GetString("regInfoTab.Error"));
			this.helpProvider.SetHelpKeyword(this.regInfoTab, resources.GetString("regInfoTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.regInfoTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("regInfoTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.regInfoTab, resources.GetString("regInfoTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.regInfoTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("regInfoTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.regInfoTab, ((int)(resources.GetObject("regInfoTab.IconPadding"))));
			this.regInfoTab.Name = "regInfoTab";
			this.helpProvider.SetShowHelp(this.regInfoTab, ((bool)(resources.GetObject("regInfoTab.ShowHelp"))));
			this.regInfoTab.UseVisualStyleBackColor = true;
			// 
			// taskRegLayoutPanel
			// 
			resources.ApplyResources(this.taskRegLayoutPanel, "taskRegLayoutPanel");
			this.taskRegLayoutPanel.Controls.Add(this.taskRegSourceLabel, 0, 0);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegURILabel, 0, 1);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegDocText, 1, 4);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegDocLabel, 0, 4);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegSDDLLabel, 0, 3);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegSourceText, 1, 0);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegURIText, 1, 1);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegVersionText, 1, 2);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegSDDLText, 1, 3);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegVersionLabel, 0, 2);
			this.taskRegLayoutPanel.Controls.Add(this.taskRegSDDLBtn, 2, 3);
			this.errorProvider.SetError(this.taskRegLayoutPanel, resources.GetString("taskRegLayoutPanel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegLayoutPanel, resources.GetString("taskRegLayoutPanel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegLayoutPanel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegLayoutPanel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegLayoutPanel, resources.GetString("taskRegLayoutPanel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegLayoutPanel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegLayoutPanel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegLayoutPanel, ((int)(resources.GetObject("taskRegLayoutPanel.IconPadding"))));
			this.taskRegLayoutPanel.Name = "taskRegLayoutPanel";
			this.helpProvider.SetShowHelp(this.taskRegLayoutPanel, ((bool)(resources.GetObject("taskRegLayoutPanel.ShowHelp"))));
			// 
			// taskRegSourceLabel
			// 
			resources.ApplyResources(this.taskRegSourceLabel, "taskRegSourceLabel");
			this.errorProvider.SetError(this.taskRegSourceLabel, resources.GetString("taskRegSourceLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegSourceLabel, resources.GetString("taskRegSourceLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegSourceLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegSourceLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegSourceLabel, resources.GetString("taskRegSourceLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegSourceLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegSourceLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegSourceLabel, ((int)(resources.GetObject("taskRegSourceLabel.IconPadding"))));
			this.taskRegSourceLabel.Name = "taskRegSourceLabel";
			this.helpProvider.SetShowHelp(this.taskRegSourceLabel, ((bool)(resources.GetObject("taskRegSourceLabel.ShowHelp"))));
			// 
			// taskRegURILabel
			// 
			resources.ApplyResources(this.taskRegURILabel, "taskRegURILabel");
			this.errorProvider.SetError(this.taskRegURILabel, resources.GetString("taskRegURILabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegURILabel, resources.GetString("taskRegURILabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegURILabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegURILabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegURILabel, resources.GetString("taskRegURILabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegURILabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegURILabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegURILabel, ((int)(resources.GetObject("taskRegURILabel.IconPadding"))));
			this.taskRegURILabel.Name = "taskRegURILabel";
			this.helpProvider.SetShowHelp(this.taskRegURILabel, ((bool)(resources.GetObject("taskRegURILabel.ShowHelp"))));
			// 
			// taskRegDocText
			// 
			resources.ApplyResources(this.taskRegDocText, "taskRegDocText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegDocText, 2);
			this.errorProvider.SetError(this.taskRegDocText, resources.GetString("taskRegDocText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegDocText, resources.GetString("taskRegDocText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegDocText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegDocText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegDocText, resources.GetString("taskRegDocText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegDocText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegDocText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegDocText, ((int)(resources.GetObject("taskRegDocText.IconPadding"))));
			this.taskRegDocText.Name = "taskRegDocText";
			this.helpProvider.SetShowHelp(this.taskRegDocText, ((bool)(resources.GetObject("taskRegDocText.ShowHelp"))));
			this.taskRegDocText.Leave += new System.EventHandler(this.taskRegDocText_Leave);
			// 
			// taskRegDocLabel
			// 
			resources.ApplyResources(this.taskRegDocLabel, "taskRegDocLabel");
			this.errorProvider.SetError(this.taskRegDocLabel, resources.GetString("taskRegDocLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegDocLabel, resources.GetString("taskRegDocLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegDocLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegDocLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegDocLabel, resources.GetString("taskRegDocLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegDocLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegDocLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegDocLabel, ((int)(resources.GetObject("taskRegDocLabel.IconPadding"))));
			this.taskRegDocLabel.Name = "taskRegDocLabel";
			this.helpProvider.SetShowHelp(this.taskRegDocLabel, ((bool)(resources.GetObject("taskRegDocLabel.ShowHelp"))));
			// 
			// taskRegSDDLLabel
			// 
			resources.ApplyResources(this.taskRegSDDLLabel, "taskRegSDDLLabel");
			this.errorProvider.SetError(this.taskRegSDDLLabel, resources.GetString("taskRegSDDLLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegSDDLLabel, resources.GetString("taskRegSDDLLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegSDDLLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegSDDLLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegSDDLLabel, resources.GetString("taskRegSDDLLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegSDDLLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegSDDLLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegSDDLLabel, ((int)(resources.GetObject("taskRegSDDLLabel.IconPadding"))));
			this.taskRegSDDLLabel.Name = "taskRegSDDLLabel";
			this.helpProvider.SetShowHelp(this.taskRegSDDLLabel, ((bool)(resources.GetObject("taskRegSDDLLabel.ShowHelp"))));
			// 
			// taskRegSourceText
			// 
			resources.ApplyResources(this.taskRegSourceText, "taskRegSourceText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegSourceText, 2);
			this.errorProvider.SetError(this.taskRegSourceText, resources.GetString("taskRegSourceText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegSourceText, resources.GetString("taskRegSourceText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegSourceText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegSourceText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegSourceText, resources.GetString("taskRegSourceText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegSourceText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegSourceText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegSourceText, ((int)(resources.GetObject("taskRegSourceText.IconPadding"))));
			this.taskRegSourceText.Name = "taskRegSourceText";
			this.helpProvider.SetShowHelp(this.taskRegSourceText, ((bool)(resources.GetObject("taskRegSourceText.ShowHelp"))));
			this.taskRegSourceText.Leave += new System.EventHandler(this.taskRegSourceText_Leave);
			// 
			// taskRegURIText
			// 
			resources.ApplyResources(this.taskRegURIText, "taskRegURIText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegURIText, 2);
			this.errorProvider.SetError(this.taskRegURIText, resources.GetString("taskRegURIText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegURIText, resources.GetString("taskRegURIText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegURIText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegURIText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegURIText, resources.GetString("taskRegURIText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegURIText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegURIText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegURIText, ((int)(resources.GetObject("taskRegURIText.IconPadding"))));
			this.taskRegURIText.Name = "taskRegURIText";
			this.helpProvider.SetShowHelp(this.taskRegURIText, ((bool)(resources.GetObject("taskRegURIText.ShowHelp"))));
			this.taskRegURIText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegURIText_Validating);
			this.taskRegURIText.Validated += new System.EventHandler(this.taskRegURIText_Validated);
			// 
			// taskRegVersionText
			// 
			resources.ApplyResources(this.taskRegVersionText, "taskRegVersionText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegVersionText, 2);
			this.errorProvider.SetError(this.taskRegVersionText, resources.GetString("taskRegVersionText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegVersionText, resources.GetString("taskRegVersionText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegVersionText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegVersionText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegVersionText, resources.GetString("taskRegVersionText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegVersionText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegVersionText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegVersionText, ((int)(resources.GetObject("taskRegVersionText.IconPadding"))));
			this.taskRegVersionText.Name = "taskRegVersionText";
			this.helpProvider.SetShowHelp(this.taskRegVersionText, ((bool)(resources.GetObject("taskRegVersionText.ShowHelp"))));
			this.taskRegVersionText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegVersionText_Validating);
			this.taskRegVersionText.Validated += new System.EventHandler(this.taskRegVersionText_Validated);
			// 
			// taskRegSDDLText
			// 
			resources.ApplyResources(this.taskRegSDDLText, "taskRegSDDLText");
			this.errorProvider.SetError(this.taskRegSDDLText, resources.GetString("taskRegSDDLText.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegSDDLText, resources.GetString("taskRegSDDLText.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegSDDLText, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegSDDLText.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegSDDLText, resources.GetString("taskRegSDDLText.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegSDDLText, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegSDDLText.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegSDDLText, ((int)(resources.GetObject("taskRegSDDLText.IconPadding"))));
			this.taskRegSDDLText.Name = "taskRegSDDLText";
			this.helpProvider.SetShowHelp(this.taskRegSDDLText, ((bool)(resources.GetObject("taskRegSDDLText.ShowHelp"))));
			this.taskRegSDDLText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegSDDLText_Validating);
			this.taskRegSDDLText.Validated += new System.EventHandler(this.taskRegSDDLText_Validated);
			// 
			// taskRegVersionLabel
			// 
			resources.ApplyResources(this.taskRegVersionLabel, "taskRegVersionLabel");
			this.errorProvider.SetError(this.taskRegVersionLabel, resources.GetString("taskRegVersionLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegVersionLabel, resources.GetString("taskRegVersionLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegVersionLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegVersionLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegVersionLabel, resources.GetString("taskRegVersionLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegVersionLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegVersionLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegVersionLabel, ((int)(resources.GetObject("taskRegVersionLabel.IconPadding"))));
			this.taskRegVersionLabel.Name = "taskRegVersionLabel";
			this.helpProvider.SetShowHelp(this.taskRegVersionLabel, ((bool)(resources.GetObject("taskRegVersionLabel.ShowHelp"))));
			// 
			// taskRegSDDLBtn
			// 
			resources.ApplyResources(this.taskRegSDDLBtn, "taskRegSDDLBtn");
			this.errorProvider.SetError(this.taskRegSDDLBtn, resources.GetString("taskRegSDDLBtn.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRegSDDLBtn, resources.GetString("taskRegSDDLBtn.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRegSDDLBtn, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRegSDDLBtn.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRegSDDLBtn, resources.GetString("taskRegSDDLBtn.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRegSDDLBtn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRegSDDLBtn.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRegSDDLBtn, ((int)(resources.GetObject("taskRegSDDLBtn.IconPadding"))));
			this.taskRegSDDLBtn.Name = "taskRegSDDLBtn";
			this.helpProvider.SetShowHelp(this.taskRegSDDLBtn, ((bool)(resources.GetObject("taskRegSDDLBtn.ShowHelp"))));
			this.taskRegSDDLBtn.UseVisualStyleBackColor = true;
			this.taskRegSDDLBtn.Click += new System.EventHandler(this.taskRegSDDLBtn_Click);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.errorProvider.SetError(this.label5, resources.GetString("label5.Error"));
			this.helpProvider.SetHelpKeyword(this.label5, resources.GetString("label5.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label5, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label5.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label5, resources.GetString("label5.HelpString"));
			this.errorProvider.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
			this.label5.Name = "label5";
			this.helpProvider.SetShowHelp(this.label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
			// 
			// addPropTab
			// 
			resources.ApplyResources(this.addPropTab, "addPropTab");
			this.addPropTab.Controls.Add(this.autoMaintGroup);
			this.addPropTab.Controls.Add(this.secHardGroup);
			this.addPropTab.Controls.Add(this.panel1);
			this.addPropTab.Controls.Add(this.label4);
			this.errorProvider.SetError(this.addPropTab, resources.GetString("addPropTab.Error"));
			this.helpProvider.SetHelpKeyword(this.addPropTab, resources.GetString("addPropTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.addPropTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("addPropTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.addPropTab, resources.GetString("addPropTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.addPropTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("addPropTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.addPropTab, ((int)(resources.GetObject("addPropTab.IconPadding"))));
			this.addPropTab.Name = "addPropTab";
			this.helpProvider.SetShowHelp(this.addPropTab, ((bool)(resources.GetObject("addPropTab.ShowHelp"))));
			this.addPropTab.UseVisualStyleBackColor = true;
			// 
			// autoMaintGroup
			// 
			resources.ApplyResources(this.autoMaintGroup, "autoMaintGroup");
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceDeadlineCombo);
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceExclusiveCheck);
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceDeadlineLabel);
			this.autoMaintGroup.Controls.Add(this.taskMaintenancePeriodLabel);
			this.autoMaintGroup.Controls.Add(this.taskMaintenancePeriodCombo);
			this.errorProvider.SetError(this.autoMaintGroup, resources.GetString("autoMaintGroup.Error"));
			this.helpProvider.SetHelpKeyword(this.autoMaintGroup, resources.GetString("autoMaintGroup.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.autoMaintGroup, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("autoMaintGroup.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.autoMaintGroup, resources.GetString("autoMaintGroup.HelpString"));
			this.errorProvider.SetIconAlignment(this.autoMaintGroup, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("autoMaintGroup.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.autoMaintGroup, ((int)(resources.GetObject("autoMaintGroup.IconPadding"))));
			this.autoMaintGroup.Name = "autoMaintGroup";
			this.helpProvider.SetShowHelp(this.autoMaintGroup, ((bool)(resources.GetObject("autoMaintGroup.ShowHelp"))));
			this.autoMaintGroup.TabStop = false;
			// 
			// taskMaintenanceDeadlineCombo
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineCombo, "taskMaintenanceDeadlineCombo");
			this.errorProvider.SetError(this.taskMaintenanceDeadlineCombo, resources.GetString("taskMaintenanceDeadlineCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskMaintenanceDeadlineCombo, resources.GetString("taskMaintenanceDeadlineCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMaintenanceDeadlineCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMaintenanceDeadlineCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMaintenanceDeadlineCombo, resources.GetString("taskMaintenanceDeadlineCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMaintenanceDeadlineCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMaintenanceDeadlineCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMaintenanceDeadlineCombo, ((int)(resources.GetObject("taskMaintenanceDeadlineCombo.IconPadding"))));
			this.taskMaintenanceDeadlineCombo.Name = "taskMaintenanceDeadlineCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineCombo, ((bool)(resources.GetObject("taskMaintenanceDeadlineCombo.ShowHelp"))));
			this.taskMaintenanceDeadlineCombo.ValueChanged += new System.EventHandler(this.taskMaintenanceDeadlineCombo_ValueChanged);
			// 
			// taskMaintenanceExclusiveCheck
			// 
			resources.ApplyResources(this.taskMaintenanceExclusiveCheck, "taskMaintenanceExclusiveCheck");
			this.errorProvider.SetError(this.taskMaintenanceExclusiveCheck, resources.GetString("taskMaintenanceExclusiveCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskMaintenanceExclusiveCheck, resources.GetString("taskMaintenanceExclusiveCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMaintenanceExclusiveCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMaintenanceExclusiveCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMaintenanceExclusiveCheck, resources.GetString("taskMaintenanceExclusiveCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMaintenanceExclusiveCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMaintenanceExclusiveCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMaintenanceExclusiveCheck, ((int)(resources.GetObject("taskMaintenanceExclusiveCheck.IconPadding"))));
			this.taskMaintenanceExclusiveCheck.Name = "taskMaintenanceExclusiveCheck";
			this.helpProvider.SetShowHelp(this.taskMaintenanceExclusiveCheck, ((bool)(resources.GetObject("taskMaintenanceExclusiveCheck.ShowHelp"))));
			this.taskMaintenanceExclusiveCheck.UseVisualStyleBackColor = true;
			this.taskMaintenanceExclusiveCheck.CheckedChanged += new System.EventHandler(this.taskMaintenanceExclusiveCheck_CheckedChanged);
			// 
			// taskMaintenanceDeadlineLabel
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineLabel, "taskMaintenanceDeadlineLabel");
			this.errorProvider.SetError(this.taskMaintenanceDeadlineLabel, resources.GetString("taskMaintenanceDeadlineLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskMaintenanceDeadlineLabel, resources.GetString("taskMaintenanceDeadlineLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMaintenanceDeadlineLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMaintenanceDeadlineLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMaintenanceDeadlineLabel, resources.GetString("taskMaintenanceDeadlineLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMaintenanceDeadlineLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMaintenanceDeadlineLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMaintenanceDeadlineLabel, ((int)(resources.GetObject("taskMaintenanceDeadlineLabel.IconPadding"))));
			this.taskMaintenanceDeadlineLabel.Name = "taskMaintenanceDeadlineLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineLabel, ((bool)(resources.GetObject("taskMaintenanceDeadlineLabel.ShowHelp"))));
			// 
			// taskMaintenancePeriodLabel
			// 
			resources.ApplyResources(this.taskMaintenancePeriodLabel, "taskMaintenancePeriodLabel");
			this.errorProvider.SetError(this.taskMaintenancePeriodLabel, resources.GetString("taskMaintenancePeriodLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.taskMaintenancePeriodLabel, resources.GetString("taskMaintenancePeriodLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMaintenancePeriodLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMaintenancePeriodLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMaintenancePeriodLabel, resources.GetString("taskMaintenancePeriodLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMaintenancePeriodLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMaintenancePeriodLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMaintenancePeriodLabel, ((int)(resources.GetObject("taskMaintenancePeriodLabel.IconPadding"))));
			this.taskMaintenancePeriodLabel.Name = "taskMaintenancePeriodLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodLabel, ((bool)(resources.GetObject("taskMaintenancePeriodLabel.ShowHelp"))));
			// 
			// taskMaintenancePeriodCombo
			// 
			resources.ApplyResources(this.taskMaintenancePeriodCombo, "taskMaintenancePeriodCombo");
			this.errorProvider.SetError(this.taskMaintenancePeriodCombo, resources.GetString("taskMaintenancePeriodCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskMaintenancePeriodCombo, resources.GetString("taskMaintenancePeriodCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskMaintenancePeriodCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskMaintenancePeriodCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskMaintenancePeriodCombo, resources.GetString("taskMaintenancePeriodCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskMaintenancePeriodCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskMaintenancePeriodCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskMaintenancePeriodCombo, ((int)(resources.GetObject("taskMaintenancePeriodCombo.IconPadding"))));
			this.taskMaintenancePeriodCombo.Name = "taskMaintenancePeriodCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodCombo, ((bool)(resources.GetObject("taskMaintenancePeriodCombo.ShowHelp"))));
			this.taskMaintenancePeriodCombo.ValueChanged += new System.EventHandler(this.taskMaintenancePeriodCombo_ValueChanged);
			// 
			// secHardGroup
			// 
			resources.ApplyResources(this.secHardGroup, "secHardGroup");
			this.secHardGroup.Controls.Add(this.principalSIDTypeLabel);
			this.secHardGroup.Controls.Add(this.principalSIDTypeCombo);
			this.secHardGroup.Controls.Add(this.principalReqPrivilegesLabel);
			this.secHardGroup.Controls.Add(this.principalReqPrivilegesDropDown);
			this.errorProvider.SetError(this.secHardGroup, resources.GetString("secHardGroup.Error"));
			this.helpProvider.SetHelpKeyword(this.secHardGroup, resources.GetString("secHardGroup.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.secHardGroup, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("secHardGroup.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.secHardGroup, resources.GetString("secHardGroup.HelpString"));
			this.errorProvider.SetIconAlignment(this.secHardGroup, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("secHardGroup.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.secHardGroup, ((int)(resources.GetObject("secHardGroup.IconPadding"))));
			this.secHardGroup.Name = "secHardGroup";
			this.helpProvider.SetShowHelp(this.secHardGroup, ((bool)(resources.GetObject("secHardGroup.ShowHelp"))));
			this.secHardGroup.TabStop = false;
			// 
			// principalSIDTypeLabel
			// 
			resources.ApplyResources(this.principalSIDTypeLabel, "principalSIDTypeLabel");
			this.errorProvider.SetError(this.principalSIDTypeLabel, resources.GetString("principalSIDTypeLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.principalSIDTypeLabel, resources.GetString("principalSIDTypeLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.principalSIDTypeLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("principalSIDTypeLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.principalSIDTypeLabel, resources.GetString("principalSIDTypeLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.principalSIDTypeLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("principalSIDTypeLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.principalSIDTypeLabel, ((int)(resources.GetObject("principalSIDTypeLabel.IconPadding"))));
			this.principalSIDTypeLabel.Name = "principalSIDTypeLabel";
			this.helpProvider.SetShowHelp(this.principalSIDTypeLabel, ((bool)(resources.GetObject("principalSIDTypeLabel.ShowHelp"))));
			// 
			// principalSIDTypeCombo
			// 
			resources.ApplyResources(this.principalSIDTypeCombo, "principalSIDTypeCombo");
			this.principalSIDTypeCombo.DisplayMember = "Text";
			this.principalSIDTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.errorProvider.SetError(this.principalSIDTypeCombo, resources.GetString("principalSIDTypeCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.principalSIDTypeCombo, resources.GetString("principalSIDTypeCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.principalSIDTypeCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("principalSIDTypeCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.principalSIDTypeCombo, resources.GetString("principalSIDTypeCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.principalSIDTypeCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("principalSIDTypeCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.principalSIDTypeCombo, ((int)(resources.GetObject("principalSIDTypeCombo.IconPadding"))));
			this.principalSIDTypeCombo.Name = "principalSIDTypeCombo";
			this.helpProvider.SetShowHelp(this.principalSIDTypeCombo, ((bool)(resources.GetObject("principalSIDTypeCombo.ShowHelp"))));
			this.principalSIDTypeCombo.ValueMember = "Value";
			this.principalSIDTypeCombo.SelectedIndexChanged += new System.EventHandler(this.principalSIDTypeCombo_SelectedIndexChanged);
			// 
			// principalReqPrivilegesLabel
			// 
			resources.ApplyResources(this.principalReqPrivilegesLabel, "principalReqPrivilegesLabel");
			this.errorProvider.SetError(this.principalReqPrivilegesLabel, resources.GetString("principalReqPrivilegesLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.principalReqPrivilegesLabel, resources.GetString("principalReqPrivilegesLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.principalReqPrivilegesLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("principalReqPrivilegesLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.principalReqPrivilegesLabel, resources.GetString("principalReqPrivilegesLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.principalReqPrivilegesLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("principalReqPrivilegesLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.principalReqPrivilegesLabel, ((int)(resources.GetObject("principalReqPrivilegesLabel.IconPadding"))));
			this.principalReqPrivilegesLabel.Name = "principalReqPrivilegesLabel";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesLabel, ((bool)(resources.GetObject("principalReqPrivilegesLabel.ShowHelp"))));
			// 
			// principalReqPrivilegesDropDown
			// 
			resources.ApplyResources(this.principalReqPrivilegesDropDown, "principalReqPrivilegesDropDown");
			this.principalReqPrivilegesDropDown.BackColor = System.Drawing.Color.White;
			this.principalReqPrivilegesDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.principalReqPrivilegesDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.errorProvider.SetError(this.principalReqPrivilegesDropDown, resources.GetString("principalReqPrivilegesDropDown.Error"));
			this.helpProvider.SetHelpKeyword(this.principalReqPrivilegesDropDown, resources.GetString("principalReqPrivilegesDropDown.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.principalReqPrivilegesDropDown, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("principalReqPrivilegesDropDown.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.principalReqPrivilegesDropDown, resources.GetString("principalReqPrivilegesDropDown.HelpString"));
			this.errorProvider.SetIconAlignment(this.principalReqPrivilegesDropDown, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("principalReqPrivilegesDropDown.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.principalReqPrivilegesDropDown, ((int)(resources.GetObject("principalReqPrivilegesDropDown.IconPadding"))));
			this.principalReqPrivilegesDropDown.Name = "principalReqPrivilegesDropDown";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesDropDown, ((bool)(resources.GetObject("principalReqPrivilegesDropDown.ShowHelp"))));
			this.principalReqPrivilegesDropDown.SelectedIndexChanged += new System.EventHandler(this.principalReqPrivilegesDropDown_SelectedIndexChanged);
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.taskEnabledCheck);
			this.panel1.Controls.Add(this.taskDisallowStartOnRemoteAppSessionCheck);
			this.panel1.Controls.Add(this.taskUseUnifiedSchedulingEngineCheck);
			this.panel1.Controls.Add(this.taskPriorityCombo);
			this.panel1.Controls.Add(this.taskVolatileCheck);
			this.panel1.Controls.Add(this.label8);
			this.errorProvider.SetError(this.panel1, resources.GetString("panel1.Error"));
			this.helpProvider.SetHelpKeyword(this.panel1, resources.GetString("panel1.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("panel1.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.panel1, resources.GetString("panel1.HelpString"));
			this.errorProvider.SetIconAlignment(this.panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("panel1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.panel1, ((int)(resources.GetObject("panel1.IconPadding"))));
			this.panel1.Name = "panel1";
			this.helpProvider.SetShowHelp(this.panel1, ((bool)(resources.GetObject("panel1.ShowHelp"))));
			// 
			// taskEnabledCheck
			// 
			resources.ApplyResources(this.taskEnabledCheck, "taskEnabledCheck");
			this.errorProvider.SetError(this.taskEnabledCheck, resources.GetString("taskEnabledCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskEnabledCheck, resources.GetString("taskEnabledCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskEnabledCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskEnabledCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskEnabledCheck, resources.GetString("taskEnabledCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskEnabledCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskEnabledCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskEnabledCheck, ((int)(resources.GetObject("taskEnabledCheck.IconPadding"))));
			this.taskEnabledCheck.Name = "taskEnabledCheck";
			this.helpProvider.SetShowHelp(this.taskEnabledCheck, ((bool)(resources.GetObject("taskEnabledCheck.ShowHelp"))));
			this.taskEnabledCheck.UseVisualStyleBackColor = true;
			this.taskEnabledCheck.CheckedChanged += new System.EventHandler(this.taskEnabledCheck_CheckedChanged);
			// 
			// taskDisallowStartOnRemoteAppSessionCheck
			// 
			resources.ApplyResources(this.taskDisallowStartOnRemoteAppSessionCheck, "taskDisallowStartOnRemoteAppSessionCheck");
			this.errorProvider.SetError(this.taskDisallowStartOnRemoteAppSessionCheck, resources.GetString("taskDisallowStartOnRemoteAppSessionCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskDisallowStartOnRemoteAppSessionCheck, resources.GetString("taskDisallowStartOnRemoteAppSessionCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskDisallowStartOnRemoteAppSessionCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskDisallowStartOnRemoteAppSessionCheck, resources.GetString("taskDisallowStartOnRemoteAppSessionCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskDisallowStartOnRemoteAppSessionCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskDisallowStartOnRemoteAppSessionCheck, ((int)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.IconPadding"))));
			this.taskDisallowStartOnRemoteAppSessionCheck.Name = "taskDisallowStartOnRemoteAppSessionCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartOnRemoteAppSessionCheck, ((bool)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.ShowHelp"))));
			this.taskDisallowStartOnRemoteAppSessionCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartOnRemoteAppSessionCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged);
			// 
			// taskUseUnifiedSchedulingEngineCheck
			// 
			resources.ApplyResources(this.taskUseUnifiedSchedulingEngineCheck, "taskUseUnifiedSchedulingEngineCheck");
			this.errorProvider.SetError(this.taskUseUnifiedSchedulingEngineCheck, resources.GetString("taskUseUnifiedSchedulingEngineCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskUseUnifiedSchedulingEngineCheck, resources.GetString("taskUseUnifiedSchedulingEngineCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskUseUnifiedSchedulingEngineCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskUseUnifiedSchedulingEngineCheck, resources.GetString("taskUseUnifiedSchedulingEngineCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskUseUnifiedSchedulingEngineCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskUseUnifiedSchedulingEngineCheck, ((int)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.IconPadding"))));
			this.taskUseUnifiedSchedulingEngineCheck.Name = "taskUseUnifiedSchedulingEngineCheck";
			this.helpProvider.SetShowHelp(this.taskUseUnifiedSchedulingEngineCheck, ((bool)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.ShowHelp"))));
			this.taskUseUnifiedSchedulingEngineCheck.UseVisualStyleBackColor = true;
			this.taskUseUnifiedSchedulingEngineCheck.CheckedChanged += new System.EventHandler(this.taskUseUnifiedSchedulingEngineCheck_CheckedChanged);
			// 
			// taskPriorityCombo
			// 
			resources.ApplyResources(this.taskPriorityCombo, "taskPriorityCombo");
			this.taskPriorityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.errorProvider.SetError(this.taskPriorityCombo, resources.GetString("taskPriorityCombo.Error"));
			this.helpProvider.SetHelpKeyword(this.taskPriorityCombo, resources.GetString("taskPriorityCombo.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskPriorityCombo, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskPriorityCombo.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskPriorityCombo, resources.GetString("taskPriorityCombo.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskPriorityCombo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskPriorityCombo.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskPriorityCombo, ((int)(resources.GetObject("taskPriorityCombo.IconPadding"))));
			this.taskPriorityCombo.Name = "taskPriorityCombo";
			this.helpProvider.SetShowHelp(this.taskPriorityCombo, ((bool)(resources.GetObject("taskPriorityCombo.ShowHelp"))));
			this.taskPriorityCombo.SelectedIndexChanged += new System.EventHandler(this.taskPriorityCombo_SelectedIndexChanged);
			// 
			// taskVolatileCheck
			// 
			resources.ApplyResources(this.taskVolatileCheck, "taskVolatileCheck");
			this.errorProvider.SetError(this.taskVolatileCheck, resources.GetString("taskVolatileCheck.Error"));
			this.helpProvider.SetHelpKeyword(this.taskVolatileCheck, resources.GetString("taskVolatileCheck.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskVolatileCheck, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskVolatileCheck.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskVolatileCheck, resources.GetString("taskVolatileCheck.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskVolatileCheck, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskVolatileCheck.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskVolatileCheck, ((int)(resources.GetObject("taskVolatileCheck.IconPadding"))));
			this.taskVolatileCheck.Name = "taskVolatileCheck";
			this.helpProvider.SetShowHelp(this.taskVolatileCheck, ((bool)(resources.GetObject("taskVolatileCheck.ShowHelp"))));
			this.taskVolatileCheck.UseVisualStyleBackColor = true;
			this.taskVolatileCheck.CheckedChanged += new System.EventHandler(this.taskVolatileCheck_CheckedChanged);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.errorProvider.SetError(this.label8, resources.GetString("label8.Error"));
			this.helpProvider.SetHelpKeyword(this.label8, resources.GetString("label8.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label8, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label8.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label8, resources.GetString("label8.HelpString"));
			this.errorProvider.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
			this.label8.Name = "label8";
			this.helpProvider.SetShowHelp(this.label8, ((bool)(resources.GetObject("label8.ShowHelp"))));
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.errorProvider.SetError(this.label4, resources.GetString("label4.Error"));
			this.helpProvider.SetHelpKeyword(this.label4, resources.GetString("label4.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label4, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label4.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label4, resources.GetString("label4.HelpString"));
			this.errorProvider.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
			this.label4.Name = "label4";
			this.helpProvider.SetShowHelp(this.label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
			// 
			// runTimesTab
			// 
			resources.ApplyResources(this.runTimesTab, "runTimesTab");
			this.runTimesTab.Controls.Add(this.taskRunTimesControl1);
			this.runTimesTab.Controls.Add(this.runTimesErrorLabel);
			this.runTimesTab.Controls.Add(this.label3);
			this.runTimesTab.Controls.Add(this.label1);
			this.errorProvider.SetError(this.runTimesTab, resources.GetString("runTimesTab.Error"));
			this.helpProvider.SetHelpKeyword(this.runTimesTab, resources.GetString("runTimesTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.runTimesTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("runTimesTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.runTimesTab, resources.GetString("runTimesTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.runTimesTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("runTimesTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.runTimesTab, ((int)(resources.GetObject("runTimesTab.IconPadding"))));
			this.runTimesTab.Name = "runTimesTab";
			this.helpProvider.SetShowHelp(this.runTimesTab, ((bool)(resources.GetObject("runTimesTab.ShowHelp"))));
			this.runTimesTab.UseVisualStyleBackColor = true;
			this.runTimesTab.Enter += new System.EventHandler(this.runTimesTab_Enter);
			this.runTimesTab.Leave += new System.EventHandler(this.runTimesTab_Leave);
			// 
			// taskRunTimesControl1
			// 
			resources.ApplyResources(this.taskRunTimesControl1, "taskRunTimesControl1");
			this.errorProvider.SetError(this.taskRunTimesControl1, resources.GetString("taskRunTimesControl1.Error"));
			this.helpProvider.SetHelpKeyword(this.taskRunTimesControl1, resources.GetString("taskRunTimesControl1.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskRunTimesControl1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskRunTimesControl1.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskRunTimesControl1, resources.GetString("taskRunTimesControl1.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskRunTimesControl1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskRunTimesControl1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskRunTimesControl1, ((int)(resources.GetObject("taskRunTimesControl1.IconPadding"))));
			this.taskRunTimesControl1.Name = "taskRunTimesControl1";
			this.helpProvider.SetShowHelp(this.taskRunTimesControl1, ((bool)(resources.GetObject("taskRunTimesControl1.ShowHelp"))));
			// 
			// runTimesErrorLabel
			// 
			resources.ApplyResources(this.runTimesErrorLabel, "runTimesErrorLabel");
			this.errorProvider.SetError(this.runTimesErrorLabel, resources.GetString("runTimesErrorLabel.Error"));
			this.helpProvider.SetHelpKeyword(this.runTimesErrorLabel, resources.GetString("runTimesErrorLabel.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.runTimesErrorLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("runTimesErrorLabel.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.runTimesErrorLabel, resources.GetString("runTimesErrorLabel.HelpString"));
			this.errorProvider.SetIconAlignment(this.runTimesErrorLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("runTimesErrorLabel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.runTimesErrorLabel, ((int)(resources.GetObject("runTimesErrorLabel.IconPadding"))));
			this.runTimesErrorLabel.Name = "runTimesErrorLabel";
			this.helpProvider.SetShowHelp(this.runTimesErrorLabel, ((bool)(resources.GetObject("runTimesErrorLabel.ShowHelp"))));
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.errorProvider.SetError(this.label3, resources.GetString("label3.Error"));
			this.helpProvider.SetHelpKeyword(this.label3, resources.GetString("label3.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label3, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label3.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label3, resources.GetString("label3.HelpString"));
			this.errorProvider.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
			this.label3.Name = "label3";
			this.helpProvider.SetShowHelp(this.label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.errorProvider.SetError(this.label1, resources.GetString("label1.Error"));
			this.helpProvider.SetHelpKeyword(this.label1, resources.GetString("label1.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.label1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label1.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.label1, resources.GetString("label1.HelpString"));
			this.errorProvider.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
			this.label1.Name = "label1";
			this.helpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// historyTab
			// 
			resources.ApplyResources(this.historyTab, "historyTab");
			this.historyTab.Controls.Add(this.taskHistoryControl1);
			this.errorProvider.SetError(this.historyTab, resources.GetString("historyTab.Error"));
			this.helpProvider.SetHelpKeyword(this.historyTab, resources.GetString("historyTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.historyTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("historyTab.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.historyTab, resources.GetString("historyTab.HelpString"));
			this.errorProvider.SetIconAlignment(this.historyTab, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("historyTab.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.historyTab, ((int)(resources.GetObject("historyTab.IconPadding"))));
			this.historyTab.Name = "historyTab";
			this.helpProvider.SetShowHelp(this.historyTab, ((bool)(resources.GetObject("historyTab.ShowHelp"))));
			this.historyTab.UseVisualStyleBackColor = true;
			this.historyTab.Enter += new System.EventHandler(this.historyTab_Enter);
			// 
			// taskHistoryControl1
			// 
			resources.ApplyResources(this.taskHistoryControl1, "taskHistoryControl1");
			this.errorProvider.SetError(this.taskHistoryControl1, resources.GetString("taskHistoryControl1.Error"));
			this.helpProvider.SetHelpKeyword(this.taskHistoryControl1, resources.GetString("taskHistoryControl1.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.taskHistoryControl1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("taskHistoryControl1.HelpNavigator"))));
			this.helpProvider.SetHelpString(this.taskHistoryControl1, resources.GetString("taskHistoryControl1.HelpString"));
			this.errorProvider.SetIconAlignment(this.taskHistoryControl1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("taskHistoryControl1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.taskHistoryControl1, ((int)(resources.GetObject("taskHistoryControl1.IconPadding"))));
			this.taskHistoryControl1.Name = "taskHistoryControl1";
			this.helpProvider.SetShowHelp(this.taskHistoryControl1, ((bool)(resources.GetObject("taskHistoryControl1.ShowHelp"))));
			this.taskHistoryControl1.Load += new System.EventHandler(this.taskHistoryControl1_Load);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			resources.ApplyResources(this.errorProvider, "errorProvider");
			// 
			// helpProvider
			// 
			resources.ApplyResources(this.helpProvider, "helpProvider");
			// 
			// TaskPropertiesControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.errorProvider.SetError(this, resources.GetString("$this.Error"));
			this.helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
			this.errorProvider.SetIconAlignment(this, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("$this.IconAlignment"))));
			this.errorProvider.SetIconPadding(this, ((int)(resources.GetObject("$this.IconPadding"))));
			this.Name = "TaskPropertiesControl";
			this.helpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Load += new System.EventHandler(this.TaskPropertiesControl_Load);
			this.tabControl.ResumeLayout(false);
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
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).EndInit();
			this.regInfoTab.ResumeLayout(false);
			this.taskRegLayoutPanel.ResumeLayout(false);
			this.taskRegLayoutPanel.PerformLayout();
			this.addPropTab.ResumeLayout(false);
			this.autoMaintGroup.ResumeLayout(false);
			this.autoMaintGroup.PerformLayout();
			this.secHardGroup.ResumeLayout(false);
			this.secHardGroup.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.runTimesTab.ResumeLayout(false);
			this.runTimesTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).EndInit();
			this.historyTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
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
		private System.Windows.Forms.DisabledItemComboBox taskVersionCombo;
		private System.Windows.Forms.Label taskVersionLabel;
		private System.Windows.Forms.CheckBox taskHiddenCheck;
		private System.Windows.Forms.CheckBox taskRunLevelCheck;
		private System.Windows.Forms.Label taskTriggerIntroLabel;
		private System.Windows.Forms.Label actionIntroLabel;
		private System.Windows.Forms.Label conditionIntroLabel;
		private System.Windows.Forms.Label taskNameLabel;
		private System.Windows.Forms.Label taskAuthorLabel;
		private System.Windows.Forms.Label taskDescLabel;
		private System.Windows.Forms.TextBox taskNameText;
		private System.Windows.Forms.Label taskAuthorText;
		private System.Windows.Forms.TextBox taskDescText;
		private System.Windows.Forms.GroupBox powerConditionGroupBox;
		private System.Windows.Forms.GroupBox idleConditionGroupBox;
		private System.Windows.Forms.TimeSpanPicker taskIdleWaitTimeoutCombo;
		private System.Windows.Forms.TimeSpanPicker taskIdleDurationCombo;
		private System.Windows.Forms.CheckBox taskRestartOnIdleCheck;
		private System.Windows.Forms.CheckBox taskStopOnIdleEndCheck;
		private System.Windows.Forms.Label taskIdleWaitTimeoutLabel;
		private System.Windows.Forms.CheckBox taskIdleDurationCheck;
		private System.Windows.Forms.CheckBox taskStopIfGoingOnBatteriesCheck;
		private System.Windows.Forms.CheckBox taskWakeToRunCheck;
		private System.Windows.Forms.CheckBox taskDisallowStartIfOnBatteriesCheck;
		private System.Windows.Forms.GroupBox networkConditionGroupBox;
		private System.Windows.Forms.ComboBox availableConnectionsCombo;
		private System.Windows.Forms.CheckBox taskStartIfConnectionCheck;
		private System.Windows.Forms.ComboBox taskMultInstCombo;
		private System.Windows.Forms.Label taskRunningRuleLabel;
		private System.Windows.Forms.Label taskRestartCountLabel;
		private System.Windows.Forms.CheckBox taskDeleteAfterCheck;
		private System.Windows.Forms.CheckBox taskAllowHardTerminateCheck;
		private System.Windows.Forms.CheckBox taskExecutionTimeLimitCheck;
		private System.Windows.Forms.CheckBox taskRestartIntervalCheck;
		private System.Windows.Forms.CheckBox taskStartWhenAvailableCheck;
		private System.Windows.Forms.CheckBox taskAllowDemandStartCheck;
		private System.Windows.Forms.Label settingsIntroLabel;
		private System.Windows.Forms.TimeSpanPicker taskDeleteAfterCombo;
		private System.Windows.Forms.TimeSpanPicker taskExecutionTimeLimitCombo;
		private System.Windows.Forms.TimeSpanPicker taskRestartIntervalCombo;
		private System.Windows.Forms.NumericUpDown taskRestartCountText;
		private System.Windows.Forms.Label taskRestartAttemptTimesLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label taskLocationText;
		private System.Windows.Forms.TabPage runTimesTab;
		private System.Windows.Forms.Label runTimesErrorLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private TaskRunTimesControl taskRunTimesControl1;
		private System.Windows.Forms.TabPage addPropTab;
		private System.Windows.Forms.ComboBox principalSIDTypeCombo;
		private System.Windows.Forms.Label principalSIDTypeLabel;
		private System.Windows.Forms.TimeSpanPicker taskMaintenancePeriodCombo;
		private System.Windows.Forms.Label taskMaintenancePeriodLabel;
		private System.Windows.Forms.TimeSpanPicker taskMaintenanceDeadlineCombo;
		private System.Windows.Forms.Label taskMaintenanceDeadlineLabel;
		private System.Windows.Forms.CheckBox taskMaintenanceExclusiveCheck;
		private System.Windows.Forms.CheckBox taskVolatileCheck;
		private System.Windows.Forms.CheckBox taskUseUnifiedSchedulingEngineCheck;
		private System.Windows.Forms.CheckBox taskDisallowStartOnRemoteAppSessionCheck;
		private System.Windows.Forms.CheckBox taskEnabledCheck;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label principalReqPrivilegesLabel;
		private DropDownCheckList principalReqPrivilegesDropDown;
		private System.Windows.Forms.ComboBox taskPriorityCombo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox secHardGroup;
		private System.Windows.Forms.GroupBox autoMaintGroup;
		private System.Windows.Forms.TabPage regInfoTab;
		private System.Windows.Forms.Label taskRegURILabel;
		private System.Windows.Forms.Label taskRegSourceLabel;
		private System.Windows.Forms.Label taskRegDocLabel;
		private System.Windows.Forms.TextBox taskRegURIText;
		private System.Windows.Forms.TextBox taskRegSourceText;
		private System.Windows.Forms.TextBox taskRegDocText;
		private System.Windows.Forms.Label taskRegSDDLLabel;
		private System.Windows.Forms.Label taskRegVersionLabel;
		private System.Windows.Forms.TextBox taskRegSDDLText;
		private System.Windows.Forms.TextBox taskRegVersionText;
		private System.Windows.Forms.Button taskRegSDDLBtn;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.HelpProvider helpProvider;
		private System.Windows.Forms.Label label5;
		private TaskHistoryControl taskHistoryControl1;
		private UIComponents.ActionCollectionUI actionCollectionUI;
		private UIComponents.TriggerCollectionUI triggerCollectionUI1;
		private System.Windows.Forms.TableLayoutPanel taskRegLayoutPanel;
		private System.Windows.Forms.Panel panel1;
	}
}
