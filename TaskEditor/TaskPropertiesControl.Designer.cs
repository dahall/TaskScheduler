using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler.UIComponents;

namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskPropertiesControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			this.components = new Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TaskPropertiesControl));
			this.tabControl = new TabControl();
			this.generalTab = new TabPage();
			this.taskNameLabel = new Label();
			this.label2 = new Label();
			this.taskAuthorLabel = new Label();
			this.taskDescLabel = new Label();
			this.taskNameText = new TextBox();
			this.taskLocationText = new Label();
			this.taskAuthorText = new Label();
			this.taskDescText = new TextBox();
			this.taskVersionCombo = new DisabledItemComboBox();
			this.taskVersionLabel = new Label();
			this.taskHiddenCheck = new CheckBox();
			this.taskSecurityGroupBox = new GroupBox();
			this.taskRunLevelCheck = new CheckBox();
			this.taskLocalOnlyCheck = new CheckBox();
			this.taskLoggedOptionalRadio = new RadioButton();
			this.taskLoggedOnRadio = new RadioButton();
			this.taskPrincipalText = new TextBox();
			this.changePrincipalButton = new Button();
			this.taskUserAcctLabel = new Label();
			this.triggersTab = new TabPage();
			this.triggerCollectionUI1 = new TriggerCollectionUI();
			this.taskTriggerIntroLabel = new Label();
			this.actionsTab = new TabPage();
			this.actionCollectionUI = new ActionCollectionUI();
			this.actionIntroLabel = new Label();
			this.conditionsTab = new TabPage();
			this.networkConditionGroupBox = new GroupBox();
			this.availableConnectionsCombo = new ComboBox();
			this.taskStartIfConnectionCheck = new CheckBox();
			this.powerConditionGroupBox = new GroupBox();
			this.taskStopIfGoingOnBatteriesCheck = new CheckBox();
			this.taskWakeToRunCheck = new CheckBox();
			this.taskDisallowStartIfOnBatteriesCheck = new CheckBox();
			this.idleConditionGroupBox = new GroupBox();
			this.taskIdleWaitTimeoutCombo = new TimeSpanPicker();
			this.taskIdleDurationCombo = new TimeSpanPicker();
			this.taskRestartOnIdleCheck = new CheckBox();
			this.taskStopOnIdleEndCheck = new CheckBox();
			this.taskIdleWaitTimeoutLabel = new Label();
			this.taskIdleDurationCheck = new CheckBox();
			this.conditionIntroLabel = new Label();
			this.settingsTab = new TabPage();
			this.taskRestartCountText = new NumericUpDown();
			this.taskMultInstCombo = new ComboBox();
			this.taskRunningRuleLabel = new Label();
			this.taskRestartAttemptTimesLabel = new Label();
			this.taskRestartCountLabel = new Label();
			this.taskDeleteAfterCheck = new CheckBox();
			this.taskAllowHardTerminateCheck = new CheckBox();
			this.taskExecutionTimeLimitCheck = new CheckBox();
			this.taskRestartIntervalCheck = new CheckBox();
			this.taskStartWhenAvailableCheck = new CheckBox();
			this.taskAllowDemandStartCheck = new CheckBox();
			this.settingsIntroLabel = new Label();
			this.taskDeleteAfterCombo = new TimeSpanPicker();
			this.taskExecutionTimeLimitCombo = new TimeSpanPicker();
			this.taskRestartIntervalCombo = new TimeSpanPicker();
			this.regInfoTab = new TabPage();
			this.taskRegLayoutPanel = new TableLayoutPanel();
			this.taskRegSourceLabel = new Label();
			this.taskRegURILabel = new Label();
			this.taskRegDocText = new TextBox();
			this.taskRegDocLabel = new Label();
			this.taskRegSDDLLabel = new Label();
			this.taskRegSourceText = new TextBox();
			this.taskRegURIText = new TextBox();
			this.taskRegVersionText = new TextBox();
			this.taskRegSDDLText = new TextBox();
			this.taskRegVersionLabel = new Label();
			this.taskRegSDDLBtn = new Button();
			this.label5 = new Label();
			this.addPropTab = new TabPage();
			this.autoMaintGroup = new GroupBox();
			this.taskMaintenanceDeadlineCombo = new TimeSpanPicker();
			this.taskMaintenanceExclusiveCheck = new CheckBox();
			this.taskMaintenanceDeadlineLabel = new Label();
			this.taskMaintenancePeriodLabel = new Label();
			this.taskMaintenancePeriodCombo = new TimeSpanPicker();
			this.secHardGroup = new GroupBox();
			this.principalSIDTypeLabel = new Label();
			this.principalSIDTypeCombo = new ComboBox();
			this.principalReqPrivilegesLabel = new Label();
			this.principalReqPrivilegesDropDown = new DropDownCheckList();
			this.panel1 = new Panel();
			this.taskEnabledCheck = new CheckBox();
			this.taskDisallowStartOnRemoteAppSessionCheck = new CheckBox();
			this.taskUseUnifiedSchedulingEngineCheck = new CheckBox();
			this.taskPriorityCombo = new ComboBox();
			this.taskVolatileCheck = new CheckBox();
			this.label8 = new Label();
			this.label4 = new Label();
			this.runTimesTab = new TabPage();
			this.taskRunTimesControl1 = new TaskRunTimesControl();
			this.runTimesErrorLabel = new Label();
			this.label3 = new Label();
			this.label1 = new Label();
			this.historyTab = new TabPage();
			this.taskHistoryControl1 = new TaskHistoryControl();
			this.errorProvider = new ErrorProvider(this.components);
			this.helpProvider = new HelpProvider();
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
			((ISupportInitialize)(this.taskRestartCountText)).BeginInit();
			this.regInfoTab.SuspendLayout();
			this.taskRegLayoutPanel.SuspendLayout();
			this.addPropTab.SuspendLayout();
			this.autoMaintGroup.SuspendLayout();
			this.secHardGroup.SuspendLayout();
			this.panel1.SuspendLayout();
			this.runTimesTab.SuspendLayout();
			((ISupportInitialize)(this.taskRunTimesControl1)).BeginInit();
			this.historyTab.SuspendLayout();
			((ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.generalTab);
			this.tabControl.Controls.Add(this.triggersTab);
			this.tabControl.Controls.Add(this.actionsTab);
			this.tabControl.Controls.Add(this.conditionsTab);
			this.tabControl.Controls.Add(this.settingsTab);
			this.tabControl.Controls.Add(this.regInfoTab);
			this.tabControl.Controls.Add(this.addPropTab);
			this.tabControl.Controls.Add(this.runTimesTab);
			this.tabControl.Controls.Add(this.historyTab);
			resources.ApplyResources(this.tabControl, "tabControl");
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.helpProvider.SetShowHelp(this.tabControl, ((bool)(resources.GetObject("tabControl.ShowHelp"))));
			this.tabControl.SelectedIndexChanged += new EventHandler(this.tabControl_TabIndexChanged);
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
			this.helpProvider.SetHelpKeyword(this.generalTab, resources.GetString("generalTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.generalTab, ((HelpNavigator)(resources.GetObject("generalTab.HelpNavigator"))));
			this.generalTab.Name = "generalTab";
			this.helpProvider.SetShowHelp(this.generalTab, ((bool)(resources.GetObject("generalTab.ShowHelp"))));
			this.generalTab.UseVisualStyleBackColor = true;
			this.generalTab.Enter += new EventHandler(this.generalTab_Enter);
			// 
			// taskNameLabel
			// 
			resources.ApplyResources(this.taskNameLabel, "taskNameLabel");
			this.taskNameLabel.Name = "taskNameLabel";
			this.helpProvider.SetShowHelp(this.taskNameLabel, ((bool)(resources.GetObject("taskNameLabel.ShowHelp"))));
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			this.helpProvider.SetShowHelp(this.label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
			// 
			// taskAuthorLabel
			// 
			resources.ApplyResources(this.taskAuthorLabel, "taskAuthorLabel");
			this.taskAuthorLabel.Name = "taskAuthorLabel";
			this.helpProvider.SetShowHelp(this.taskAuthorLabel, ((bool)(resources.GetObject("taskAuthorLabel.ShowHelp"))));
			// 
			// taskDescLabel
			// 
			resources.ApplyResources(this.taskDescLabel, "taskDescLabel");
			this.taskDescLabel.Name = "taskDescLabel";
			this.helpProvider.SetShowHelp(this.taskDescLabel, ((bool)(resources.GetObject("taskDescLabel.ShowHelp"))));
			// 
			// taskNameText
			// 
			resources.ApplyResources(this.taskNameText, "taskNameText");
			this.errorProvider.SetIconPadding(this.taskNameText, ((int)(resources.GetObject("taskNameText.IconPadding"))));
			this.taskNameText.Name = "taskNameText";
			this.taskNameText.ReadOnly = true;
			this.helpProvider.SetShowHelp(this.taskNameText, ((bool)(resources.GetObject("taskNameText.ShowHelp"))));
			this.taskNameText.Validating += new CancelEventHandler(this.taskNameText_Validating);
			this.taskNameText.Validated += new EventHandler(this.taskNameText_Validated);
			// 
			// taskLocationText
			// 
			resources.ApplyResources(this.taskLocationText, "taskLocationText");
			this.taskLocationText.Name = "taskLocationText";
			this.helpProvider.SetShowHelp(this.taskLocationText, ((bool)(resources.GetObject("taskLocationText.ShowHelp"))));
			// 
			// taskAuthorText
			// 
			resources.ApplyResources(this.taskAuthorText, "taskAuthorText");
			this.taskAuthorText.Name = "taskAuthorText";
			this.helpProvider.SetShowHelp(this.taskAuthorText, ((bool)(resources.GetObject("taskAuthorText.ShowHelp"))));
			// 
			// taskDescText
			// 
			this.taskDescText.AcceptsReturn = true;
			this.taskDescText.AcceptsTab = true;
			this.taskDescText.AllowDrop = true;
			resources.ApplyResources(this.taskDescText, "taskDescText");
			this.taskDescText.Name = "taskDescText";
			this.helpProvider.SetShowHelp(this.taskDescText, ((bool)(resources.GetObject("taskDescText.ShowHelp"))));
			this.taskDescText.Leave += new EventHandler(this.taskDescText_Leave);
			// 
			// taskVersionCombo
			// 
			resources.ApplyResources(this.taskVersionCombo, "taskVersionCombo");
			this.taskVersionCombo.Name = "taskVersionCombo";
			this.helpProvider.SetShowHelp(this.taskVersionCombo, ((bool)(resources.GetObject("taskVersionCombo.ShowHelp"))));
			this.taskVersionCombo.SelectedIndexChanged += new EventHandler(this.taskVersionCombo_SelectedIndexChanged);
			this.taskVersionCombo.GotFocus += new EventHandler(this.taskVersionCombo_GotFocus);
			// 
			// taskVersionLabel
			// 
			resources.ApplyResources(this.taskVersionLabel, "taskVersionLabel");
			this.taskVersionLabel.Name = "taskVersionLabel";
			this.helpProvider.SetShowHelp(this.taskVersionLabel, ((bool)(resources.GetObject("taskVersionLabel.ShowHelp"))));
			// 
			// taskHiddenCheck
			// 
			resources.ApplyResources(this.taskHiddenCheck, "taskHiddenCheck");
			this.taskHiddenCheck.Name = "taskHiddenCheck";
			this.helpProvider.SetShowHelp(this.taskHiddenCheck, ((bool)(resources.GetObject("taskHiddenCheck.ShowHelp"))));
			this.taskHiddenCheck.UseVisualStyleBackColor = true;
			this.taskHiddenCheck.CheckedChanged += new EventHandler(this.taskHiddenCheck_CheckedChanged);
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
			this.helpProvider.SetShowHelp(this.taskSecurityGroupBox, ((bool)(resources.GetObject("taskSecurityGroupBox.ShowHelp"))));
			this.taskSecurityGroupBox.TabStop = false;
			// 
			// taskRunLevelCheck
			// 
			resources.ApplyResources(this.taskRunLevelCheck, "taskRunLevelCheck");
			this.taskRunLevelCheck.Name = "taskRunLevelCheck";
			this.helpProvider.SetShowHelp(this.taskRunLevelCheck, ((bool)(resources.GetObject("taskRunLevelCheck.ShowHelp"))));
			this.taskRunLevelCheck.UseVisualStyleBackColor = true;
			this.taskRunLevelCheck.CheckedChanged += new EventHandler(this.taskRunLevelCheck_CheckedChanged);
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.helpProvider.SetShowHelp(this.taskLocalOnlyCheck, ((bool)(resources.GetObject("taskLocalOnlyCheck.ShowHelp"))));
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			this.taskLocalOnlyCheck.CheckedChanged += new EventHandler(this.taskLocalOnlyCheck_CheckedChanged);
			// 
			// taskLoggedOptionalRadio
			// 
			resources.ApplyResources(this.taskLoggedOptionalRadio, "taskLoggedOptionalRadio");
			this.taskLoggedOptionalRadio.Name = "taskLoggedOptionalRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOptionalRadio, ((bool)(resources.GetObject("taskLoggedOptionalRadio.ShowHelp"))));
			this.taskLoggedOptionalRadio.TabStop = true;
			this.taskLoggedOptionalRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOptionalRadio.CheckedChanged += new EventHandler(this.taskLoggedOptionalRadio_CheckedChanged);
			// 
			// taskLoggedOnRadio
			// 
			resources.ApplyResources(this.taskLoggedOnRadio, "taskLoggedOnRadio");
			this.taskLoggedOnRadio.Name = "taskLoggedOnRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOnRadio, ((bool)(resources.GetObject("taskLoggedOnRadio.ShowHelp"))));
			this.taskLoggedOnRadio.TabStop = true;
			this.taskLoggedOnRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOnRadio.CheckedChanged += new EventHandler(this.taskLoggedOnRadio_CheckedChanged);
			// 
			// taskPrincipalText
			// 
			resources.ApplyResources(this.taskPrincipalText, "taskPrincipalText");
			this.taskPrincipalText.Name = "taskPrincipalText";
			this.taskPrincipalText.ReadOnly = true;
			this.helpProvider.SetShowHelp(this.taskPrincipalText, ((bool)(resources.GetObject("taskPrincipalText.ShowHelp"))));
			this.taskPrincipalText.TabStop = false;
			// 
			// changePrincipalButton
			// 
			resources.ApplyResources(this.changePrincipalButton, "changePrincipalButton");
			this.changePrincipalButton.Name = "changePrincipalButton";
			this.helpProvider.SetShowHelp(this.changePrincipalButton, ((bool)(resources.GetObject("changePrincipalButton.ShowHelp"))));
			this.changePrincipalButton.UseVisualStyleBackColor = true;
			this.changePrincipalButton.Click += new EventHandler(this.changePrincipalButton_Click);
			// 
			// taskUserAcctLabel
			// 
			resources.ApplyResources(this.taskUserAcctLabel, "taskUserAcctLabel");
			this.taskUserAcctLabel.Name = "taskUserAcctLabel";
			this.helpProvider.SetShowHelp(this.taskUserAcctLabel, ((bool)(resources.GetObject("taskUserAcctLabel.ShowHelp"))));
			// 
			// triggersTab
			// 
			resources.ApplyResources(this.triggersTab, "triggersTab");
			this.triggersTab.Controls.Add(this.triggerCollectionUI1);
			this.triggersTab.Controls.Add(this.taskTriggerIntroLabel);
			this.helpProvider.SetHelpKeyword(this.triggersTab, resources.GetString("triggersTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.triggersTab, ((HelpNavigator)(resources.GetObject("triggersTab.HelpNavigator"))));
			this.triggersTab.Name = "triggersTab";
			this.helpProvider.SetShowHelp(this.triggersTab, ((bool)(resources.GetObject("triggersTab.ShowHelp"))));
			this.triggersTab.UseVisualStyleBackColor = true;
			// 
			// triggerCollectionUI1
			// 
			resources.ApplyResources(this.triggerCollectionUI1, "triggerCollectionUI1");
			this.triggerCollectionUI1.Name = "triggerCollectionUI1";
			this.helpProvider.SetShowHelp(this.triggerCollectionUI1, ((bool)(resources.GetObject("triggerCollectionUI1.ShowHelp"))));
			// 
			// taskTriggerIntroLabel
			// 
			resources.ApplyResources(this.taskTriggerIntroLabel, "taskTriggerIntroLabel");
			this.taskTriggerIntroLabel.Name = "taskTriggerIntroLabel";
			this.helpProvider.SetShowHelp(this.taskTriggerIntroLabel, ((bool)(resources.GetObject("taskTriggerIntroLabel.ShowHelp"))));
			// 
			// actionsTab
			// 
			resources.ApplyResources(this.actionsTab, "actionsTab");
			this.actionsTab.Controls.Add(this.actionCollectionUI);
			this.actionsTab.Controls.Add(this.actionIntroLabel);
			this.helpProvider.SetHelpKeyword(this.actionsTab, resources.GetString("actionsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.actionsTab, ((HelpNavigator)(resources.GetObject("actionsTab.HelpNavigator"))));
			this.actionsTab.Name = "actionsTab";
			this.helpProvider.SetShowHelp(this.actionsTab, ((bool)(resources.GetObject("actionsTab.ShowHelp"))));
			this.actionsTab.UseVisualStyleBackColor = true;
			// 
			// actionCollectionUI
			// 
			resources.ApplyResources(this.actionCollectionUI, "actionCollectionUI");
			this.actionCollectionUI.Name = "actionCollectionUI";
			this.helpProvider.SetShowHelp(this.actionCollectionUI, ((bool)(resources.GetObject("actionCollectionUI.ShowHelp"))));
			// 
			// actionIntroLabel
			// 
			resources.ApplyResources(this.actionIntroLabel, "actionIntroLabel");
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
			this.helpProvider.SetHelpKeyword(this.conditionsTab, resources.GetString("conditionsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.conditionsTab, ((HelpNavigator)(resources.GetObject("conditionsTab.HelpNavigator"))));
			this.conditionsTab.Name = "conditionsTab";
			this.helpProvider.SetShowHelp(this.conditionsTab, ((bool)(resources.GetObject("conditionsTab.ShowHelp"))));
			this.conditionsTab.UseVisualStyleBackColor = true;
			this.conditionsTab.Enter += new EventHandler(this.conditionsTab_Enter);
			// 
			// networkConditionGroupBox
			// 
			this.networkConditionGroupBox.Controls.Add(this.availableConnectionsCombo);
			this.networkConditionGroupBox.Controls.Add(this.taskStartIfConnectionCheck);
			resources.ApplyResources(this.networkConditionGroupBox, "networkConditionGroupBox");
			this.networkConditionGroupBox.Name = "networkConditionGroupBox";
			this.helpProvider.SetShowHelp(this.networkConditionGroupBox, ((bool)(resources.GetObject("networkConditionGroupBox.ShowHelp"))));
			this.networkConditionGroupBox.TabStop = false;
			// 
			// availableConnectionsCombo
			// 
			resources.ApplyResources(this.availableConnectionsCombo, "availableConnectionsCombo");
			this.availableConnectionsCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			this.helpProvider.SetShowHelp(this.availableConnectionsCombo, ((bool)(resources.GetObject("availableConnectionsCombo.ShowHelp"))));
			this.availableConnectionsCombo.SelectedIndexChanged += new EventHandler(this.availableConnectionsCombo_SelectedIndexChanged);
			// 
			// taskStartIfConnectionCheck
			// 
			resources.ApplyResources(this.taskStartIfConnectionCheck, "taskStartIfConnectionCheck");
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.helpProvider.SetShowHelp(this.taskStartIfConnectionCheck, ((bool)(resources.GetObject("taskStartIfConnectionCheck.ShowHelp"))));
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// powerConditionGroupBox
			// 
			this.powerConditionGroupBox.Controls.Add(this.taskStopIfGoingOnBatteriesCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskWakeToRunCheck);
			this.powerConditionGroupBox.Controls.Add(this.taskDisallowStartIfOnBatteriesCheck);
			resources.ApplyResources(this.powerConditionGroupBox, "powerConditionGroupBox");
			this.powerConditionGroupBox.Name = "powerConditionGroupBox";
			this.helpProvider.SetShowHelp(this.powerConditionGroupBox, ((bool)(resources.GetObject("powerConditionGroupBox.ShowHelp"))));
			this.powerConditionGroupBox.TabStop = false;
			// 
			// taskStopIfGoingOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskStopIfGoingOnBatteriesCheck, "taskStopIfGoingOnBatteriesCheck");
			this.taskStopIfGoingOnBatteriesCheck.Name = "taskStopIfGoingOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskStopIfGoingOnBatteriesCheck, ((bool)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.ShowHelp"))));
			this.taskStopIfGoingOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskStopIfGoingOnBatteriesCheck.CheckedChanged += new EventHandler(this.taskStopIfGoingOnBatteriesCheck_CheckedChanged);
			// 
			// taskWakeToRunCheck
			// 
			resources.ApplyResources(this.taskWakeToRunCheck, "taskWakeToRunCheck");
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.helpProvider.SetShowHelp(this.taskWakeToRunCheck, ((bool)(resources.GetObject("taskWakeToRunCheck.ShowHelp"))));
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			this.taskWakeToRunCheck.CheckedChanged += new EventHandler(this.taskWakeToRunCheck_CheckedChanged);
			// 
			// taskDisallowStartIfOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskDisallowStartIfOnBatteriesCheck, "taskDisallowStartIfOnBatteriesCheck");
			this.taskDisallowStartIfOnBatteriesCheck.Name = "taskDisallowStartIfOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartIfOnBatteriesCheck, ((bool)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.ShowHelp"))));
			this.taskDisallowStartIfOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartIfOnBatteriesCheck.CheckedChanged += new EventHandler(this.taskDisallowStartIfOnBatteriesCheck_CheckedChanged);
			// 
			// idleConditionGroupBox
			// 
			this.idleConditionGroupBox.Controls.Add(this.taskIdleWaitTimeoutCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDurationCombo);
			this.idleConditionGroupBox.Controls.Add(this.taskRestartOnIdleCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskStopOnIdleEndCheck);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleWaitTimeoutLabel);
			this.idleConditionGroupBox.Controls.Add(this.taskIdleDurationCheck);
			resources.ApplyResources(this.idleConditionGroupBox, "idleConditionGroupBox");
			this.idleConditionGroupBox.Name = "idleConditionGroupBox";
			this.helpProvider.SetShowHelp(this.idleConditionGroupBox, ((bool)(resources.GetObject("idleConditionGroupBox.ShowHelp"))));
			this.idleConditionGroupBox.TabStop = false;
			// 
			// taskIdleWaitTimeoutCombo
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutCombo, "taskIdleWaitTimeoutCombo");
			this.taskIdleWaitTimeoutCombo.Name = "taskIdleWaitTimeoutCombo";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutCombo, ((bool)(resources.GetObject("taskIdleWaitTimeoutCombo.ShowHelp"))));
			this.taskIdleWaitTimeoutCombo.ValueChanged += new EventHandler(this.taskIdleWaitTimeoutCombo_ValueChanged);
			this.taskIdleWaitTimeoutCombo.Validating += new CancelEventHandler(this.span_Validating);
			// 
			// taskIdleDurationCombo
			// 
			resources.ApplyResources(this.taskIdleDurationCombo, "taskIdleDurationCombo");
			this.taskIdleDurationCombo.Name = "taskIdleDurationCombo";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCombo, ((bool)(resources.GetObject("taskIdleDurationCombo.ShowHelp"))));
			this.taskIdleDurationCombo.ValueChanged += new EventHandler(this.taskIdleDurationCombo_ValueChanged);
			this.taskIdleDurationCombo.Validating += new CancelEventHandler(this.span_Validating);
			// 
			// taskRestartOnIdleCheck
			// 
			resources.ApplyResources(this.taskRestartOnIdleCheck, "taskRestartOnIdleCheck");
			this.taskRestartOnIdleCheck.Name = "taskRestartOnIdleCheck";
			this.helpProvider.SetShowHelp(this.taskRestartOnIdleCheck, ((bool)(resources.GetObject("taskRestartOnIdleCheck.ShowHelp"))));
			this.taskRestartOnIdleCheck.UseVisualStyleBackColor = true;
			this.taskRestartOnIdleCheck.CheckedChanged += new EventHandler(this.taskRestartOnIdleCheck_CheckedChanged);
			// 
			// taskStopOnIdleEndCheck
			// 
			resources.ApplyResources(this.taskStopOnIdleEndCheck, "taskStopOnIdleEndCheck");
			this.taskStopOnIdleEndCheck.Checked = true;
			this.taskStopOnIdleEndCheck.CheckState = CheckState.Checked;
			this.taskStopOnIdleEndCheck.Name = "taskStopOnIdleEndCheck";
			this.helpProvider.SetShowHelp(this.taskStopOnIdleEndCheck, ((bool)(resources.GetObject("taskStopOnIdleEndCheck.ShowHelp"))));
			this.taskStopOnIdleEndCheck.UseVisualStyleBackColor = true;
			this.taskStopOnIdleEndCheck.CheckedChanged += new EventHandler(this.taskStopOnIdleEndCheck_CheckedChanged);
			// 
			// taskIdleWaitTimeoutLabel
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutLabel, "taskIdleWaitTimeoutLabel");
			this.taskIdleWaitTimeoutLabel.Name = "taskIdleWaitTimeoutLabel";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutLabel, ((bool)(resources.GetObject("taskIdleWaitTimeoutLabel.ShowHelp"))));
			// 
			// taskIdleDurationCheck
			// 
			resources.ApplyResources(this.taskIdleDurationCheck, "taskIdleDurationCheck");
			this.taskIdleDurationCheck.Name = "taskIdleDurationCheck";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCheck, ((bool)(resources.GetObject("taskIdleDurationCheck.ShowHelp"))));
			this.taskIdleDurationCheck.UseVisualStyleBackColor = true;
			this.taskIdleDurationCheck.CheckedChanged += new EventHandler(this.taskIdleDurationCheck_CheckedChanged);
			// 
			// conditionIntroLabel
			// 
			resources.ApplyResources(this.conditionIntroLabel, "conditionIntroLabel");
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
			this.helpProvider.SetHelpKeyword(this.settingsTab, resources.GetString("settingsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.settingsTab, ((HelpNavigator)(resources.GetObject("settingsTab.HelpNavigator"))));
			this.settingsTab.Name = "settingsTab";
			this.helpProvider.SetShowHelp(this.settingsTab, ((bool)(resources.GetObject("settingsTab.ShowHelp"))));
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// taskRestartCountText
			// 
			resources.ApplyResources(this.taskRestartCountText, "taskRestartCountText");
			this.taskRestartCountText.Name = "taskRestartCountText";
			this.helpProvider.SetShowHelp(this.taskRestartCountText, ((bool)(resources.GetObject("taskRestartCountText.ShowHelp"))));
			this.taskRestartCountText.ValueChanged += new EventHandler(this.taskRestartCountText_ValueChanged);
			// 
			// taskMultInstCombo
			// 
			this.taskMultInstCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.taskMultInstCombo.FormattingEnabled = true;
			resources.ApplyResources(this.taskMultInstCombo, "taskMultInstCombo");
			this.taskMultInstCombo.Name = "taskMultInstCombo";
			this.helpProvider.SetShowHelp(this.taskMultInstCombo, ((bool)(resources.GetObject("taskMultInstCombo.ShowHelp"))));
			this.taskMultInstCombo.SelectedIndexChanged += new EventHandler(this.taskMultInstCombo_SelectedIndexChanged);
			// 
			// taskRunningRuleLabel
			// 
			resources.ApplyResources(this.taskRunningRuleLabel, "taskRunningRuleLabel");
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			this.helpProvider.SetShowHelp(this.taskRunningRuleLabel, ((bool)(resources.GetObject("taskRunningRuleLabel.ShowHelp"))));
			// 
			// taskRestartAttemptTimesLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptTimesLabel, "taskRestartAttemptTimesLabel");
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			this.helpProvider.SetShowHelp(this.taskRestartAttemptTimesLabel, ((bool)(resources.GetObject("taskRestartAttemptTimesLabel.ShowHelp"))));
			// 
			// taskRestartCountLabel
			// 
			resources.ApplyResources(this.taskRestartCountLabel, "taskRestartCountLabel");
			this.taskRestartCountLabel.Name = "taskRestartCountLabel";
			this.helpProvider.SetShowHelp(this.taskRestartCountLabel, ((bool)(resources.GetObject("taskRestartCountLabel.ShowHelp"))));
			// 
			// taskDeleteAfterCheck
			// 
			resources.ApplyResources(this.taskDeleteAfterCheck, "taskDeleteAfterCheck");
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCheck, ((bool)(resources.GetObject("taskDeleteAfterCheck.ShowHelp"))));
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskAllowHardTerminateCheck
			// 
			resources.ApplyResources(this.taskAllowHardTerminateCheck, "taskAllowHardTerminateCheck");
			this.taskAllowHardTerminateCheck.Name = "taskAllowHardTerminateCheck";
			this.helpProvider.SetShowHelp(this.taskAllowHardTerminateCheck, ((bool)(resources.GetObject("taskAllowHardTerminateCheck.ShowHelp"))));
			this.taskAllowHardTerminateCheck.UseVisualStyleBackColor = true;
			this.taskAllowHardTerminateCheck.CheckedChanged += new EventHandler(this.taskAllowHardTerminateCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCheck
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCheck, "taskExecutionTimeLimitCheck");
			this.taskExecutionTimeLimitCheck.Name = "taskExecutionTimeLimitCheck";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCheck, ((bool)(resources.GetObject("taskExecutionTimeLimitCheck.ShowHelp"))));
			this.taskExecutionTimeLimitCheck.UseVisualStyleBackColor = true;
			this.taskExecutionTimeLimitCheck.CheckedChanged += new EventHandler(this.taskExecutionTimeLimitCheck_CheckedChanged);
			// 
			// taskRestartIntervalCheck
			// 
			resources.ApplyResources(this.taskRestartIntervalCheck, "taskRestartIntervalCheck");
			this.taskRestartIntervalCheck.Name = "taskRestartIntervalCheck";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCheck, ((bool)(resources.GetObject("taskRestartIntervalCheck.ShowHelp"))));
			this.taskRestartIntervalCheck.UseVisualStyleBackColor = true;
			this.taskRestartIntervalCheck.CheckedChanged += new EventHandler(this.taskRestartIntervalCheck_CheckedChanged);
			// 
			// taskStartWhenAvailableCheck
			// 
			resources.ApplyResources(this.taskStartWhenAvailableCheck, "taskStartWhenAvailableCheck");
			this.taskStartWhenAvailableCheck.Name = "taskStartWhenAvailableCheck";
			this.helpProvider.SetShowHelp(this.taskStartWhenAvailableCheck, ((bool)(resources.GetObject("taskStartWhenAvailableCheck.ShowHelp"))));
			this.taskStartWhenAvailableCheck.UseVisualStyleBackColor = true;
			this.taskStartWhenAvailableCheck.CheckedChanged += new EventHandler(this.taskStartWhenAvailableCheck_CheckedChanged);
			// 
			// taskAllowDemandStartCheck
			// 
			resources.ApplyResources(this.taskAllowDemandStartCheck, "taskAllowDemandStartCheck");
			this.taskAllowDemandStartCheck.Name = "taskAllowDemandStartCheck";
			this.helpProvider.SetShowHelp(this.taskAllowDemandStartCheck, ((bool)(resources.GetObject("taskAllowDemandStartCheck.ShowHelp"))));
			this.taskAllowDemandStartCheck.UseVisualStyleBackColor = true;
			this.taskAllowDemandStartCheck.CheckedChanged += new EventHandler(this.taskAllowDemandStartCheck_CheckedChanged);
			// 
			// settingsIntroLabel
			// 
			resources.ApplyResources(this.settingsIntroLabel, "settingsIntroLabel");
			this.settingsIntroLabel.Name = "settingsIntroLabel";
			this.helpProvider.SetShowHelp(this.settingsIntroLabel, ((bool)(resources.GetObject("settingsIntroLabel.ShowHelp"))));
			// 
			// taskDeleteAfterCombo
			// 
			resources.ApplyResources(this.taskDeleteAfterCombo, "taskDeleteAfterCombo");
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCombo, ((bool)(resources.GetObject("taskDeleteAfterCombo.ShowHelp"))));
			this.taskDeleteAfterCombo.ValueChanged += new EventHandler(this.taskDeleteAfterCombo_ValueChanged);
			this.taskDeleteAfterCombo.Validating += new CancelEventHandler(this.span_Validating);
			// 
			// taskExecutionTimeLimitCombo
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCombo, "taskExecutionTimeLimitCombo");
			this.taskExecutionTimeLimitCombo.Name = "taskExecutionTimeLimitCombo";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCombo, ((bool)(resources.GetObject("taskExecutionTimeLimitCombo.ShowHelp"))));
			this.taskExecutionTimeLimitCombo.ValueChanged += new EventHandler(this.taskExecutionTimeLimitCombo_ValueChanged);
			this.taskExecutionTimeLimitCombo.Validating += new CancelEventHandler(this.span_Validating);
			// 
			// taskRestartIntervalCombo
			// 
			resources.ApplyResources(this.taskRestartIntervalCombo, "taskRestartIntervalCombo");
			this.taskRestartIntervalCombo.Name = "taskRestartIntervalCombo";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCombo, ((bool)(resources.GetObject("taskRestartIntervalCombo.ShowHelp"))));
			this.taskRestartIntervalCombo.ValueChanged += new EventHandler(this.taskRestartIntervalCombo_ValueChanged);
			this.taskRestartIntervalCombo.Validating += new CancelEventHandler(this.span_Validating);
			// 
			// regInfoTab
			// 
			resources.ApplyResources(this.regInfoTab, "regInfoTab");
			this.regInfoTab.Controls.Add(this.taskRegLayoutPanel);
			this.regInfoTab.Controls.Add(this.label5);
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
			this.taskRegLayoutPanel.Name = "taskRegLayoutPanel";
			this.helpProvider.SetShowHelp(this.taskRegLayoutPanel, ((bool)(resources.GetObject("taskRegLayoutPanel.ShowHelp"))));
			// 
			// taskRegSourceLabel
			// 
			resources.ApplyResources(this.taskRegSourceLabel, "taskRegSourceLabel");
			this.taskRegSourceLabel.Name = "taskRegSourceLabel";
			this.helpProvider.SetShowHelp(this.taskRegSourceLabel, ((bool)(resources.GetObject("taskRegSourceLabel.ShowHelp"))));
			// 
			// taskRegURILabel
			// 
			resources.ApplyResources(this.taskRegURILabel, "taskRegURILabel");
			this.taskRegURILabel.Name = "taskRegURILabel";
			this.helpProvider.SetShowHelp(this.taskRegURILabel, ((bool)(resources.GetObject("taskRegURILabel.ShowHelp"))));
			// 
			// taskRegDocText
			// 
			this.taskRegDocText.AcceptsReturn = true;
			this.taskRegDocText.AcceptsTab = true;
			this.taskRegDocText.AllowDrop = true;
			resources.ApplyResources(this.taskRegDocText, "taskRegDocText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegDocText, 2);
			this.taskRegDocText.Name = "taskRegDocText";
			this.helpProvider.SetShowHelp(this.taskRegDocText, ((bool)(resources.GetObject("taskRegDocText.ShowHelp"))));
			this.taskRegDocText.Leave += new EventHandler(this.taskRegDocText_Leave);
			// 
			// taskRegDocLabel
			// 
			resources.ApplyResources(this.taskRegDocLabel, "taskRegDocLabel");
			this.taskRegDocLabel.Name = "taskRegDocLabel";
			this.helpProvider.SetShowHelp(this.taskRegDocLabel, ((bool)(resources.GetObject("taskRegDocLabel.ShowHelp"))));
			// 
			// taskRegSDDLLabel
			// 
			resources.ApplyResources(this.taskRegSDDLLabel, "taskRegSDDLLabel");
			this.taskRegSDDLLabel.Name = "taskRegSDDLLabel";
			this.helpProvider.SetShowHelp(this.taskRegSDDLLabel, ((bool)(resources.GetObject("taskRegSDDLLabel.ShowHelp"))));
			// 
			// taskRegSourceText
			// 
			resources.ApplyResources(this.taskRegSourceText, "taskRegSourceText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegSourceText, 2);
			this.taskRegSourceText.Name = "taskRegSourceText";
			this.helpProvider.SetShowHelp(this.taskRegSourceText, ((bool)(resources.GetObject("taskRegSourceText.ShowHelp"))));
			this.taskRegSourceText.Leave += new EventHandler(this.taskRegSourceText_Leave);
			// 
			// taskRegURIText
			// 
			resources.ApplyResources(this.taskRegURIText, "taskRegURIText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegURIText, 2);
			this.errorProvider.SetIconPadding(this.taskRegURIText, ((int)(resources.GetObject("taskRegURIText.IconPadding"))));
			this.taskRegURIText.Name = "taskRegURIText";
			this.helpProvider.SetShowHelp(this.taskRegURIText, ((bool)(resources.GetObject("taskRegURIText.ShowHelp"))));
			this.taskRegURIText.Validating += new CancelEventHandler(this.taskRegURIText_Validating);
			this.taskRegURIText.Validated += new EventHandler(this.taskRegURIText_Validated);
			// 
			// taskRegVersionText
			// 
			resources.ApplyResources(this.taskRegVersionText, "taskRegVersionText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegVersionText, 2);
			this.errorProvider.SetIconPadding(this.taskRegVersionText, ((int)(resources.GetObject("taskRegVersionText.IconPadding"))));
			this.taskRegVersionText.Name = "taskRegVersionText";
			this.helpProvider.SetShowHelp(this.taskRegVersionText, ((bool)(resources.GetObject("taskRegVersionText.ShowHelp"))));
			this.taskRegVersionText.Validating += new CancelEventHandler(this.taskRegVersionText_Validating);
			this.taskRegVersionText.Validated += new EventHandler(this.taskRegVersionText_Validated);
			// 
			// taskRegSDDLText
			// 
			resources.ApplyResources(this.taskRegSDDLText, "taskRegSDDLText");
			this.errorProvider.SetIconPadding(this.taskRegSDDLText, ((int)(resources.GetObject("taskRegSDDLText.IconPadding"))));
			this.taskRegSDDLText.Name = "taskRegSDDLText";
			this.helpProvider.SetShowHelp(this.taskRegSDDLText, ((bool)(resources.GetObject("taskRegSDDLText.ShowHelp"))));
			this.taskRegSDDLText.Validating += new CancelEventHandler(this.taskRegSDDLText_Validating);
			this.taskRegSDDLText.Validated += new EventHandler(this.taskRegSDDLText_Validated);
			// 
			// taskRegVersionLabel
			// 
			resources.ApplyResources(this.taskRegVersionLabel, "taskRegVersionLabel");
			this.taskRegVersionLabel.Name = "taskRegVersionLabel";
			this.helpProvider.SetShowHelp(this.taskRegVersionLabel, ((bool)(resources.GetObject("taskRegVersionLabel.ShowHelp"))));
			// 
			// taskRegSDDLBtn
			// 
			resources.ApplyResources(this.taskRegSDDLBtn, "taskRegSDDLBtn");
			this.taskRegSDDLBtn.Name = "taskRegSDDLBtn";
			this.helpProvider.SetShowHelp(this.taskRegSDDLBtn, ((bool)(resources.GetObject("taskRegSDDLBtn.ShowHelp"))));
			this.taskRegSDDLBtn.UseVisualStyleBackColor = true;
			this.taskRegSDDLBtn.Click += new EventHandler(this.taskRegSDDLBtn_Click);
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
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
			this.addPropTab.Name = "addPropTab";
			this.helpProvider.SetShowHelp(this.addPropTab, ((bool)(resources.GetObject("addPropTab.ShowHelp"))));
			this.addPropTab.UseVisualStyleBackColor = true;
			// 
			// autoMaintGroup
			// 
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceDeadlineCombo);
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceExclusiveCheck);
			this.autoMaintGroup.Controls.Add(this.taskMaintenanceDeadlineLabel);
			this.autoMaintGroup.Controls.Add(this.taskMaintenancePeriodLabel);
			this.autoMaintGroup.Controls.Add(this.taskMaintenancePeriodCombo);
			resources.ApplyResources(this.autoMaintGroup, "autoMaintGroup");
			this.autoMaintGroup.Name = "autoMaintGroup";
			this.helpProvider.SetShowHelp(this.autoMaintGroup, ((bool)(resources.GetObject("autoMaintGroup.ShowHelp"))));
			this.autoMaintGroup.TabStop = false;
			// 
			// taskMaintenanceDeadlineCombo
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineCombo, "taskMaintenanceDeadlineCombo");
			this.taskMaintenanceDeadlineCombo.Name = "taskMaintenanceDeadlineCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineCombo, ((bool)(resources.GetObject("taskMaintenanceDeadlineCombo.ShowHelp"))));
			this.taskMaintenanceDeadlineCombo.ValueChanged += new EventHandler(this.taskMaintenanceDeadlineCombo_ValueChanged);
			this.taskMaintenanceDeadlineCombo.Validating += new CancelEventHandler(this.taskMaintenanceDeadlineCombo_Validating);
			this.taskMaintenanceDeadlineCombo.Validated += new EventHandler(this.taskMaintenanceDeadlineCombo_Validated);
			// 
			// taskMaintenanceExclusiveCheck
			// 
			resources.ApplyResources(this.taskMaintenanceExclusiveCheck, "taskMaintenanceExclusiveCheck");
			this.taskMaintenanceExclusiveCheck.Name = "taskMaintenanceExclusiveCheck";
			this.helpProvider.SetShowHelp(this.taskMaintenanceExclusiveCheck, ((bool)(resources.GetObject("taskMaintenanceExclusiveCheck.ShowHelp"))));
			this.taskMaintenanceExclusiveCheck.UseVisualStyleBackColor = true;
			this.taskMaintenanceExclusiveCheck.CheckedChanged += new EventHandler(this.taskMaintenanceExclusiveCheck_CheckedChanged);
			// 
			// taskMaintenanceDeadlineLabel
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineLabel, "taskMaintenanceDeadlineLabel");
			this.taskMaintenanceDeadlineLabel.Name = "taskMaintenanceDeadlineLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineLabel, ((bool)(resources.GetObject("taskMaintenanceDeadlineLabel.ShowHelp"))));
			// 
			// taskMaintenancePeriodLabel
			// 
			resources.ApplyResources(this.taskMaintenancePeriodLabel, "taskMaintenancePeriodLabel");
			this.taskMaintenancePeriodLabel.Name = "taskMaintenancePeriodLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodLabel, ((bool)(resources.GetObject("taskMaintenancePeriodLabel.ShowHelp"))));
			// 
			// taskMaintenancePeriodCombo
			// 
			resources.ApplyResources(this.taskMaintenancePeriodCombo, "taskMaintenancePeriodCombo");
			this.taskMaintenancePeriodCombo.Name = "taskMaintenancePeriodCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodCombo, ((bool)(resources.GetObject("taskMaintenancePeriodCombo.ShowHelp"))));
			this.taskMaintenancePeriodCombo.ValueChanged += new EventHandler(this.taskMaintenancePeriodCombo_ValueChanged);
			this.taskMaintenancePeriodCombo.Validating += new CancelEventHandler(this.taskMaintenancePeriodCombo_Validating);
			this.taskMaintenancePeriodCombo.Validated += new EventHandler(this.taskMaintenancePeriodCombo_Validated);
			// 
			// secHardGroup
			// 
			this.secHardGroup.Controls.Add(this.principalSIDTypeLabel);
			this.secHardGroup.Controls.Add(this.principalSIDTypeCombo);
			this.secHardGroup.Controls.Add(this.principalReqPrivilegesLabel);
			this.secHardGroup.Controls.Add(this.principalReqPrivilegesDropDown);
			resources.ApplyResources(this.secHardGroup, "secHardGroup");
			this.secHardGroup.Name = "secHardGroup";
			this.helpProvider.SetShowHelp(this.secHardGroup, ((bool)(resources.GetObject("secHardGroup.ShowHelp"))));
			this.secHardGroup.TabStop = false;
			// 
			// principalSIDTypeLabel
			// 
			resources.ApplyResources(this.principalSIDTypeLabel, "principalSIDTypeLabel");
			this.principalSIDTypeLabel.Name = "principalSIDTypeLabel";
			this.helpProvider.SetShowHelp(this.principalSIDTypeLabel, ((bool)(resources.GetObject("principalSIDTypeLabel.ShowHelp"))));
			// 
			// principalSIDTypeCombo
			// 
			resources.ApplyResources(this.principalSIDTypeCombo, "principalSIDTypeCombo");
			this.principalSIDTypeCombo.DisplayMember = "Text";
			this.principalSIDTypeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.principalSIDTypeCombo.Name = "principalSIDTypeCombo";
			this.helpProvider.SetShowHelp(this.principalSIDTypeCombo, ((bool)(resources.GetObject("principalSIDTypeCombo.ShowHelp"))));
			this.principalSIDTypeCombo.ValueMember = "Value";
			this.principalSIDTypeCombo.SelectedIndexChanged += new EventHandler(this.principalSIDTypeCombo_SelectedIndexChanged);
			// 
			// principalReqPrivilegesLabel
			// 
			resources.ApplyResources(this.principalReqPrivilegesLabel, "principalReqPrivilegesLabel");
			this.principalReqPrivilegesLabel.Name = "principalReqPrivilegesLabel";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesLabel, ((bool)(resources.GetObject("principalReqPrivilegesLabel.ShowHelp"))));
			// 
			// principalReqPrivilegesDropDown
			// 
			resources.ApplyResources(this.principalReqPrivilegesDropDown, "principalReqPrivilegesDropDown");
			this.principalReqPrivilegesDropDown.BackColor = Color.White;
			this.principalReqPrivilegesDropDown.ControlSize = new Size(187, 105);
			this.principalReqPrivilegesDropDown.DropSize = new Size(121, 106);
			this.principalReqPrivilegesDropDown.Name = "principalReqPrivilegesDropDown";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesDropDown, ((bool)(resources.GetObject("principalReqPrivilegesDropDown.ShowHelp"))));
			this.principalReqPrivilegesDropDown.SelectedIndexChanged += new EventHandler(this.principalReqPrivilegesDropDown_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.taskEnabledCheck);
			this.panel1.Controls.Add(this.taskDisallowStartOnRemoteAppSessionCheck);
			this.panel1.Controls.Add(this.taskUseUnifiedSchedulingEngineCheck);
			this.panel1.Controls.Add(this.taskPriorityCombo);
			this.panel1.Controls.Add(this.taskVolatileCheck);
			this.panel1.Controls.Add(this.label8);
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Name = "panel1";
			this.helpProvider.SetShowHelp(this.panel1, ((bool)(resources.GetObject("panel1.ShowHelp"))));
			// 
			// taskEnabledCheck
			// 
			resources.ApplyResources(this.taskEnabledCheck, "taskEnabledCheck");
			this.taskEnabledCheck.Name = "taskEnabledCheck";
			this.helpProvider.SetShowHelp(this.taskEnabledCheck, ((bool)(resources.GetObject("taskEnabledCheck.ShowHelp"))));
			this.taskEnabledCheck.UseVisualStyleBackColor = true;
			this.taskEnabledCheck.CheckedChanged += new EventHandler(this.taskEnabledCheck_CheckedChanged);
			// 
			// taskDisallowStartOnRemoteAppSessionCheck
			// 
			resources.ApplyResources(this.taskDisallowStartOnRemoteAppSessionCheck, "taskDisallowStartOnRemoteAppSessionCheck");
			this.taskDisallowStartOnRemoteAppSessionCheck.Name = "taskDisallowStartOnRemoteAppSessionCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartOnRemoteAppSessionCheck, ((bool)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.ShowHelp"))));
			this.taskDisallowStartOnRemoteAppSessionCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartOnRemoteAppSessionCheck.CheckedChanged += new EventHandler(this.taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged);
			// 
			// taskUseUnifiedSchedulingEngineCheck
			// 
			resources.ApplyResources(this.taskUseUnifiedSchedulingEngineCheck, "taskUseUnifiedSchedulingEngineCheck");
			this.taskUseUnifiedSchedulingEngineCheck.Name = "taskUseUnifiedSchedulingEngineCheck";
			this.helpProvider.SetShowHelp(this.taskUseUnifiedSchedulingEngineCheck, ((bool)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.ShowHelp"))));
			this.taskUseUnifiedSchedulingEngineCheck.UseVisualStyleBackColor = true;
			this.taskUseUnifiedSchedulingEngineCheck.CheckedChanged += new EventHandler(this.taskUseUnifiedSchedulingEngineCheck_CheckedChanged);
			// 
			// taskPriorityCombo
			// 
			this.taskPriorityCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.taskPriorityCombo, "taskPriorityCombo");
			this.taskPriorityCombo.Name = "taskPriorityCombo";
			this.helpProvider.SetShowHelp(this.taskPriorityCombo, ((bool)(resources.GetObject("taskPriorityCombo.ShowHelp"))));
			this.taskPriorityCombo.SelectedIndexChanged += new EventHandler(this.taskPriorityCombo_SelectedIndexChanged);
			// 
			// taskVolatileCheck
			// 
			resources.ApplyResources(this.taskVolatileCheck, "taskVolatileCheck");
			this.taskVolatileCheck.Name = "taskVolatileCheck";
			this.helpProvider.SetShowHelp(this.taskVolatileCheck, ((bool)(resources.GetObject("taskVolatileCheck.ShowHelp"))));
			this.taskVolatileCheck.UseVisualStyleBackColor = true;
			this.taskVolatileCheck.CheckedChanged += new EventHandler(this.taskVolatileCheck_CheckedChanged);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			this.helpProvider.SetShowHelp(this.label8, ((bool)(resources.GetObject("label8.ShowHelp"))));
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
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
			this.runTimesTab.Name = "runTimesTab";
			this.helpProvider.SetShowHelp(this.runTimesTab, ((bool)(resources.GetObject("runTimesTab.ShowHelp"))));
			this.runTimesTab.UseVisualStyleBackColor = true;
			this.runTimesTab.Enter += new EventHandler(this.runTimesTab_Enter);
			this.runTimesTab.Leave += new EventHandler(this.runTimesTab_Leave);
			// 
			// taskRunTimesControl1
			// 
			resources.ApplyResources(this.taskRunTimesControl1, "taskRunTimesControl1");
			this.taskRunTimesControl1.Name = "taskRunTimesControl1";
			this.helpProvider.SetShowHelp(this.taskRunTimesControl1, ((bool)(resources.GetObject("taskRunTimesControl1.ShowHelp"))));
			// 
			// runTimesErrorLabel
			// 
			resources.ApplyResources(this.runTimesErrorLabel, "runTimesErrorLabel");
			this.runTimesErrorLabel.Name = "runTimesErrorLabel";
			this.helpProvider.SetShowHelp(this.runTimesErrorLabel, ((bool)(resources.GetObject("runTimesErrorLabel.ShowHelp"))));
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			this.helpProvider.SetShowHelp(this.label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.helpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// historyTab
			// 
			resources.ApplyResources(this.historyTab, "historyTab");
			this.historyTab.Controls.Add(this.taskHistoryControl1);
			this.helpProvider.SetHelpKeyword(this.historyTab, resources.GetString("historyTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.historyTab, ((HelpNavigator)(resources.GetObject("historyTab.HelpNavigator"))));
			this.historyTab.Name = "historyTab";
			this.helpProvider.SetShowHelp(this.historyTab, ((bool)(resources.GetObject("historyTab.ShowHelp"))));
			this.historyTab.UseVisualStyleBackColor = true;
			this.historyTab.Enter += new EventHandler(this.historyTab_Enter);
			// 
			// taskHistoryControl1
			// 
			resources.ApplyResources(this.taskHistoryControl1, "taskHistoryControl1");
			this.taskHistoryControl1.Name = "taskHistoryControl1";
			this.helpProvider.SetShowHelp(this.taskHistoryControl1, ((bool)(resources.GetObject("taskHistoryControl1.ShowHelp"))));
			this.taskHistoryControl1.Load += new EventHandler(this.taskHistoryControl1_Load);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// TaskPropertiesControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "TaskPropertiesControl";
			this.helpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Load += new EventHandler(this.TaskPropertiesControl_Load);
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
			((ISupportInitialize)(this.taskRestartCountText)).EndInit();
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
			((ISupportInitialize)(this.taskRunTimesControl1)).EndInit();
			this.historyTab.ResumeLayout(false);
			((ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private TabControl tabControl;
		private TabPage generalTab;
		private GroupBox taskSecurityGroupBox;
		private TabPage triggersTab;
		private TabPage actionsTab;
		private TabPage conditionsTab;
		private TabPage settingsTab;
		private TabPage historyTab;
		private Label taskUserAcctLabel;
		private TextBox taskPrincipalText;
		private Button changePrincipalButton;
		private CheckBox taskLocalOnlyCheck;
		private RadioButton taskLoggedOptionalRadio;
		private RadioButton taskLoggedOnRadio;
		private DisabledItemComboBox taskVersionCombo;
		private Label taskVersionLabel;
		private CheckBox taskHiddenCheck;
		private CheckBox taskRunLevelCheck;
		private Label taskTriggerIntroLabel;
		private Label actionIntroLabel;
		private Label conditionIntroLabel;
		private Label taskNameLabel;
		private Label taskAuthorLabel;
		private Label taskDescLabel;
		private TextBox taskNameText;
		private Label taskAuthorText;
		private TextBox taskDescText;
		private GroupBox powerConditionGroupBox;
		private GroupBox idleConditionGroupBox;
		private TimeSpanPicker taskIdleWaitTimeoutCombo;
		private TimeSpanPicker taskIdleDurationCombo;
		private CheckBox taskRestartOnIdleCheck;
		private CheckBox taskStopOnIdleEndCheck;
		private Label taskIdleWaitTimeoutLabel;
		private CheckBox taskIdleDurationCheck;
		private CheckBox taskStopIfGoingOnBatteriesCheck;
		private CheckBox taskWakeToRunCheck;
		private CheckBox taskDisallowStartIfOnBatteriesCheck;
		private GroupBox networkConditionGroupBox;
		private ComboBox availableConnectionsCombo;
		private CheckBox taskStartIfConnectionCheck;
		private ComboBox taskMultInstCombo;
		private Label taskRunningRuleLabel;
		private Label taskRestartCountLabel;
		private CheckBox taskDeleteAfterCheck;
		private CheckBox taskAllowHardTerminateCheck;
		private CheckBox taskExecutionTimeLimitCheck;
		private CheckBox taskRestartIntervalCheck;
		private CheckBox taskStartWhenAvailableCheck;
		private CheckBox taskAllowDemandStartCheck;
		private Label settingsIntroLabel;
		private TimeSpanPicker taskDeleteAfterCombo;
		private TimeSpanPicker taskExecutionTimeLimitCombo;
		private TimeSpanPicker taskRestartIntervalCombo;
		private NumericUpDown taskRestartCountText;
		private Label taskRestartAttemptTimesLabel;
		private Label label2;
		private Label taskLocationText;
		private TabPage runTimesTab;
		private Label runTimesErrorLabel;
		private Label label3;
		private Label label1;
		private TaskRunTimesControl taskRunTimesControl1;
		private TabPage addPropTab;
		private ComboBox principalSIDTypeCombo;
		private Label principalSIDTypeLabel;
		private TimeSpanPicker taskMaintenancePeriodCombo;
		private Label taskMaintenancePeriodLabel;
		private TimeSpanPicker taskMaintenanceDeadlineCombo;
		private Label taskMaintenanceDeadlineLabel;
		private CheckBox taskMaintenanceExclusiveCheck;
		private CheckBox taskVolatileCheck;
		private CheckBox taskUseUnifiedSchedulingEngineCheck;
		private CheckBox taskDisallowStartOnRemoteAppSessionCheck;
		private CheckBox taskEnabledCheck;
		private Label label4;
		private Label principalReqPrivilegesLabel;
		private DropDownCheckList principalReqPrivilegesDropDown;
		private ComboBox taskPriorityCombo;
		private Label label8;
		private GroupBox secHardGroup;
		private GroupBox autoMaintGroup;
		private TabPage regInfoTab;
		private Label taskRegURILabel;
		private Label taskRegSourceLabel;
		private Label taskRegDocLabel;
		private TextBox taskRegURIText;
		private TextBox taskRegSourceText;
		private TextBox taskRegDocText;
		private Label taskRegSDDLLabel;
		private Label taskRegVersionLabel;
		private TextBox taskRegSDDLText;
		private TextBox taskRegVersionText;
		private Button taskRegSDDLBtn;
		private ErrorProvider errorProvider;
		private HelpProvider helpProvider;
		private Label label5;
		private TaskHistoryControl taskHistoryControl1;
		private ActionCollectionUI actionCollectionUI;
		private TriggerCollectionUI triggerCollectionUI1;
		private TableLayoutPanel taskRegLayoutPanel;
		private Panel panel1;
	}
}
