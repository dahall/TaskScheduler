using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler.UIComponents;
using Vanara.Windows.Forms;

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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskPropertiesControl));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.taskHiddenCheck = new System.Windows.Forms.CheckBox();
			this.taskVersionLabel = new System.Windows.Forms.Label();
			this.taskVersionCombo = new Vanara.Windows.Forms.DisabledItemComboBox();
			this.taskSecurityGroupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.taskRunLevelCheck = new System.Windows.Forms.CheckBox();
			this.taskLocalOnlyCheck = new System.Windows.Forms.CheckBox();
			this.taskLoggedOptionalRadio = new System.Windows.Forms.RadioButton();
			this.taskLoggedOnRadio = new System.Windows.Forms.RadioButton();
			this.taskUserAcctLabel = new System.Windows.Forms.Label();
			this.taskPrincipalText = new System.Windows.Forms.TextBox();
			this.changePrincipalButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
			this.taskNameLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.taskDescText = new System.Windows.Forms.TextBox();
			this.taskAuthorText = new System.Windows.Forms.Label();
			this.taskLocationText = new System.Windows.Forms.Label();
			this.taskNameText = new System.Windows.Forms.TextBox();
			this.taskDescLabel = new System.Windows.Forms.Label();
			this.taskAuthorLabel = new System.Windows.Forms.Label();
			this.triggersTab = new System.Windows.Forms.TabPage();
			this.triggerCollectionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.TriggerCollectionUI();
			this.taskTriggerIntroLabel = new System.Windows.Forms.Label();
			this.actionsTab = new System.Windows.Forms.TabPage();
			this.actionCollectionUI = new Microsoft.Win32.TaskScheduler.UIComponents.ActionCollectionUI();
			this.actionIntroLabel = new System.Windows.Forms.Label();
			this.conditionsTab = new System.Windows.Forms.TabPage();
			this.networkConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.taskStartIfConnectionCheck = new System.Windows.Forms.CheckBox();
			this.availableConnectionsCombo = new System.Windows.Forms.ComboBox();
			this.powerConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.taskDisallowStartIfOnBatteriesCheck = new System.Windows.Forms.CheckBox();
			this.taskWakeToRunCheck = new System.Windows.Forms.CheckBox();
			this.taskStopIfGoingOnBatteriesCheck = new System.Windows.Forms.CheckBox();
			this.idleConditionGroupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.taskIdleDurationCheck = new System.Windows.Forms.CheckBox();
			this.taskIdleWaitTimeoutCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskIdleWaitTimeoutLabel = new System.Windows.Forms.Label();
			this.taskIdleDurationCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskStopOnIdleEndCheck = new System.Windows.Forms.CheckBox();
			this.taskRestartOnIdleCheck = new System.Windows.Forms.CheckBox();
			this.conditionIntroLabel = new System.Windows.Forms.Label();
			this.settingsTab = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.taskMultInstCombo = new System.Windows.Forms.ComboBox();
			this.taskRunningRuleLabel = new System.Windows.Forms.Label();
			this.taskAllowHardTerminateCheck = new System.Windows.Forms.CheckBox();
			this.taskStartWhenAvailableCheck = new System.Windows.Forms.CheckBox();
			this.taskAllowDemandStartCheck = new System.Windows.Forms.CheckBox();
			this.taskRestartIntervalCheck = new System.Windows.Forms.CheckBox();
			this.taskExecutionTimeLimitCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskRestartCountLabel = new System.Windows.Forms.Label();
			this.taskRestartIntervalCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskExecutionTimeLimitCheck = new System.Windows.Forms.CheckBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.taskDeleteAfterCheck = new System.Windows.Forms.CheckBox();
			this.taskDeleteAfterCombo = new System.Windows.Forms.TimeSpanPicker();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.taskRestartCountText = new System.Windows.Forms.NumericUpDown();
			this.taskRestartAttemptTimesLabel = new System.Windows.Forms.Label();
			this.settingsIntroLabel = new System.Windows.Forms.Label();
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
			this.addtPanel = new System.Windows.Forms.TableLayoutPanel();
			this.autoMaintGroup = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.taskMaintenanceDeadlineLabel = new System.Windows.Forms.Label();
			this.taskMaintenancePeriodCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskMaintenanceDeadlineCombo = new System.Windows.Forms.TimeSpanPicker();
			this.taskMaintenancePeriodLabel = new System.Windows.Forms.Label();
			this.taskMaintenanceExclusiveCheck = new System.Windows.Forms.CheckBox();
			this.taskEnabledCheck = new System.Windows.Forms.CheckBox();
			this.secHardGroup = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.principalSIDTypeLabel = new System.Windows.Forms.Label();
			this.principalReqPrivilegesDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.principalSIDTypeCombo = new System.Windows.Forms.ComboBox();
			this.principalReqPrivilegesLabel = new System.Windows.Forms.Label();
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
			this.tableLayoutPanel8.SuspendLayout();
			this.taskSecurityGroupBox.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			this.triggersTab.SuspendLayout();
			this.actionsTab.SuspendLayout();
			this.conditionsTab.SuspendLayout();
			this.networkConditionGroupBox.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.powerConditionGroupBox.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.idleConditionGroupBox.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.settingsTab.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).BeginInit();
			this.regInfoTab.SuspendLayout();
			this.taskRegLayoutPanel.SuspendLayout();
			this.addPropTab.SuspendLayout();
			this.addtPanel.SuspendLayout();
			this.autoMaintGroup.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.secHardGroup.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.runTimesTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).BeginInit();
			this.historyTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
			// 
			// generalTab
			// 
			resources.ApplyResources(this.generalTab, "generalTab");
			this.generalTab.Controls.Add(this.tableLayoutPanel8);
			this.generalTab.Controls.Add(this.taskSecurityGroupBox);
			this.generalTab.Controls.Add(this.tableLayoutPanel9);
			this.helpProvider.SetHelpKeyword(this.generalTab, resources.GetString("generalTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.generalTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("generalTab.HelpNavigator"))));
			this.generalTab.Name = "generalTab";
			this.helpProvider.SetShowHelp(this.generalTab, ((bool)(resources.GetObject("generalTab.ShowHelp"))));
			this.generalTab.UseVisualStyleBackColor = true;
			this.generalTab.Enter += new System.EventHandler(this.generalTab_Enter);
			// 
			// tableLayoutPanel8
			// 
			resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
			this.tableLayoutPanel8.Controls.Add(this.taskHiddenCheck, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.taskVersionLabel, 1, 0);
			this.tableLayoutPanel8.Controls.Add(this.taskVersionCombo, 2, 0);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			// 
			// taskHiddenCheck
			// 
			resources.ApplyResources(this.taskHiddenCheck, "taskHiddenCheck");
			this.taskHiddenCheck.Name = "taskHiddenCheck";
			this.helpProvider.SetShowHelp(this.taskHiddenCheck, ((bool)(resources.GetObject("taskHiddenCheck.ShowHelp"))));
			this.taskHiddenCheck.UseVisualStyleBackColor = true;
			this.taskHiddenCheck.CheckedChanged += new System.EventHandler(this.taskHiddenCheck_CheckedChanged);
			// 
			// taskVersionLabel
			// 
			resources.ApplyResources(this.taskVersionLabel, "taskVersionLabel");
			this.taskVersionLabel.Name = "taskVersionLabel";
			this.helpProvider.SetShowHelp(this.taskVersionLabel, ((bool)(resources.GetObject("taskVersionLabel.ShowHelp"))));
			// 
			// taskVersionCombo
			// 
			resources.ApplyResources(this.taskVersionCombo, "taskVersionCombo");
			this.taskVersionCombo.Name = "taskVersionCombo";
			this.helpProvider.SetShowHelp(this.taskVersionCombo, ((bool)(resources.GetObject("taskVersionCombo.ShowHelp"))));
			this.taskVersionCombo.SelectedIndexChanged += new System.EventHandler(this.taskVersionCombo_SelectedIndexChanged);
			this.taskVersionCombo.GotFocus += new System.EventHandler(this.taskVersionCombo_GotFocus);
			// 
			// taskSecurityGroupBox
			// 
			resources.ApplyResources(this.taskSecurityGroupBox, "taskSecurityGroupBox");
			this.taskSecurityGroupBox.Controls.Add(this.tableLayoutPanel7);
			this.taskSecurityGroupBox.Name = "taskSecurityGroupBox";
			this.helpProvider.SetShowHelp(this.taskSecurityGroupBox, ((bool)(resources.GetObject("taskSecurityGroupBox.ShowHelp"))));
			this.taskSecurityGroupBox.TabStop = false;
			// 
			// tableLayoutPanel7
			// 
			resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
			this.tableLayoutPanel7.Controls.Add(this.taskUserAcctLabel, 0, 0);
			this.tableLayoutPanel7.Controls.Add(this.taskRunLevelCheck, 0, 5);
			this.tableLayoutPanel7.Controls.Add(this.taskPrincipalText, 0, 1);
			this.tableLayoutPanel7.Controls.Add(this.taskLocalOnlyCheck, 0, 4);
			this.tableLayoutPanel7.Controls.Add(this.changePrincipalButton, 1, 1);
			this.tableLayoutPanel7.Controls.Add(this.taskLoggedOptionalRadio, 0, 3);
			this.tableLayoutPanel7.Controls.Add(this.taskLoggedOnRadio, 0, 2);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			// 
			// taskRunLevelCheck
			// 
			resources.ApplyResources(this.taskRunLevelCheck, "taskRunLevelCheck");
			this.tableLayoutPanel7.SetColumnSpan(this.taskRunLevelCheck, 2);
			this.taskRunLevelCheck.Name = "taskRunLevelCheck";
			this.helpProvider.SetShowHelp(this.taskRunLevelCheck, ((bool)(resources.GetObject("taskRunLevelCheck.ShowHelp"))));
			this.taskRunLevelCheck.UseVisualStyleBackColor = true;
			this.taskRunLevelCheck.CheckedChanged += new System.EventHandler(this.taskRunLevelCheck_CheckedChanged);
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.tableLayoutPanel7.SetColumnSpan(this.taskLocalOnlyCheck, 2);
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.helpProvider.SetShowHelp(this.taskLocalOnlyCheck, ((bool)(resources.GetObject("taskLocalOnlyCheck.ShowHelp"))));
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			this.taskLocalOnlyCheck.CheckedChanged += new System.EventHandler(this.taskLocalOnlyCheck_CheckedChanged);
			// 
			// taskLoggedOptionalRadio
			// 
			resources.ApplyResources(this.taskLoggedOptionalRadio, "taskLoggedOptionalRadio");
			this.tableLayoutPanel7.SetColumnSpan(this.taskLoggedOptionalRadio, 2);
			this.taskLoggedOptionalRadio.Name = "taskLoggedOptionalRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOptionalRadio, ((bool)(resources.GetObject("taskLoggedOptionalRadio.ShowHelp"))));
			this.taskLoggedOptionalRadio.TabStop = true;
			this.taskLoggedOptionalRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOptionalRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOptionalRadio_CheckedChanged);
			// 
			// taskLoggedOnRadio
			// 
			resources.ApplyResources(this.taskLoggedOnRadio, "taskLoggedOnRadio");
			this.tableLayoutPanel7.SetColumnSpan(this.taskLoggedOnRadio, 2);
			this.taskLoggedOnRadio.Name = "taskLoggedOnRadio";
			this.helpProvider.SetShowHelp(this.taskLoggedOnRadio, ((bool)(resources.GetObject("taskLoggedOnRadio.ShowHelp"))));
			this.taskLoggedOnRadio.TabStop = true;
			this.taskLoggedOnRadio.UseVisualStyleBackColor = true;
			this.taskLoggedOnRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOnRadio_CheckedChanged);
			// 
			// taskUserAcctLabel
			// 
			resources.ApplyResources(this.taskUserAcctLabel, "taskUserAcctLabel");
			this.tableLayoutPanel7.SetColumnSpan(this.taskUserAcctLabel, 2);
			this.taskUserAcctLabel.Name = "taskUserAcctLabel";
			this.helpProvider.SetShowHelp(this.taskUserAcctLabel, ((bool)(resources.GetObject("taskUserAcctLabel.ShowHelp"))));
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
			this.changePrincipalButton.Click += new System.EventHandler(this.changePrincipalButton_Click);
			// 
			// tableLayoutPanel9
			// 
			resources.ApplyResources(this.tableLayoutPanel9, "tableLayoutPanel9");
			this.tableLayoutPanel9.Controls.Add(this.taskNameLabel, 0, 0);
			this.tableLayoutPanel9.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel9.Controls.Add(this.taskDescText, 1, 3);
			this.tableLayoutPanel9.Controls.Add(this.taskAuthorText, 1, 2);
			this.tableLayoutPanel9.Controls.Add(this.taskLocationText, 1, 1);
			this.tableLayoutPanel9.Controls.Add(this.taskNameText, 1, 0);
			this.tableLayoutPanel9.Controls.Add(this.taskDescLabel, 0, 3);
			this.tableLayoutPanel9.Controls.Add(this.taskAuthorLabel, 0, 2);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
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
			// taskDescText
			// 
			this.taskDescText.AcceptsReturn = true;
			this.taskDescText.AcceptsTab = true;
			this.taskDescText.AllowDrop = true;
			resources.ApplyResources(this.taskDescText, "taskDescText");
			this.taskDescText.Name = "taskDescText";
			this.helpProvider.SetShowHelp(this.taskDescText, ((bool)(resources.GetObject("taskDescText.ShowHelp"))));
			this.taskDescText.Leave += new System.EventHandler(this.taskDescText_Leave);
			// 
			// taskAuthorText
			// 
			resources.ApplyResources(this.taskAuthorText, "taskAuthorText");
			this.taskAuthorText.Name = "taskAuthorText";
			this.helpProvider.SetShowHelp(this.taskAuthorText, ((bool)(resources.GetObject("taskAuthorText.ShowHelp"))));
			// 
			// taskLocationText
			// 
			resources.ApplyResources(this.taskLocationText, "taskLocationText");
			this.taskLocationText.Name = "taskLocationText";
			this.helpProvider.SetShowHelp(this.taskLocationText, ((bool)(resources.GetObject("taskLocationText.ShowHelp"))));
			// 
			// taskNameText
			// 
			resources.ApplyResources(this.taskNameText, "taskNameText");
			this.errorProvider.SetIconPadding(this.taskNameText, ((int)(resources.GetObject("taskNameText.IconPadding"))));
			this.taskNameText.Name = "taskNameText";
			this.taskNameText.ReadOnly = true;
			this.helpProvider.SetShowHelp(this.taskNameText, ((bool)(resources.GetObject("taskNameText.ShowHelp"))));
			this.taskNameText.Validating += new System.ComponentModel.CancelEventHandler(this.taskNameText_Validating);
			this.taskNameText.Validated += new System.EventHandler(this.taskNameText_Validated);
			// 
			// taskDescLabel
			// 
			resources.ApplyResources(this.taskDescLabel, "taskDescLabel");
			this.taskDescLabel.Name = "taskDescLabel";
			this.helpProvider.SetShowHelp(this.taskDescLabel, ((bool)(resources.GetObject("taskDescLabel.ShowHelp"))));
			// 
			// taskAuthorLabel
			// 
			resources.ApplyResources(this.taskAuthorLabel, "taskAuthorLabel");
			this.taskAuthorLabel.Name = "taskAuthorLabel";
			this.helpProvider.SetShowHelp(this.taskAuthorLabel, ((bool)(resources.GetObject("taskAuthorLabel.ShowHelp"))));
			// 
			// triggersTab
			// 
			resources.ApplyResources(this.triggersTab, "triggersTab");
			this.triggersTab.Controls.Add(this.triggerCollectionUI1);
			this.triggersTab.Controls.Add(this.taskTriggerIntroLabel);
			this.helpProvider.SetHelpKeyword(this.triggersTab, resources.GetString("triggersTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.triggersTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("triggersTab.HelpNavigator"))));
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
			this.helpProvider.SetHelpNavigator(this.actionsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("actionsTab.HelpNavigator"))));
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
			this.helpProvider.SetHelpNavigator(this.conditionsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("conditionsTab.HelpNavigator"))));
			this.conditionsTab.Name = "conditionsTab";
			this.helpProvider.SetShowHelp(this.conditionsTab, ((bool)(resources.GetObject("conditionsTab.ShowHelp"))));
			this.conditionsTab.UseVisualStyleBackColor = true;
			this.conditionsTab.Enter += new System.EventHandler(this.conditionsTab_Enter);
			// 
			// networkConditionGroupBox
			// 
			resources.ApplyResources(this.networkConditionGroupBox, "networkConditionGroupBox");
			this.networkConditionGroupBox.Controls.Add(this.tableLayoutPanel6);
			this.networkConditionGroupBox.Name = "networkConditionGroupBox";
			this.helpProvider.SetShowHelp(this.networkConditionGroupBox, ((bool)(resources.GetObject("networkConditionGroupBox.ShowHelp"))));
			this.networkConditionGroupBox.TabStop = false;
			// 
			// tableLayoutPanel6
			// 
			resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
			this.tableLayoutPanel6.Controls.Add(this.taskStartIfConnectionCheck, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.availableConnectionsCombo, 0, 1);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			// 
			// taskStartIfConnectionCheck
			// 
			resources.ApplyResources(this.taskStartIfConnectionCheck, "taskStartIfConnectionCheck");
			this.taskStartIfConnectionCheck.Name = "taskStartIfConnectionCheck";
			this.helpProvider.SetShowHelp(this.taskStartIfConnectionCheck, ((bool)(resources.GetObject("taskStartIfConnectionCheck.ShowHelp"))));
			this.taskStartIfConnectionCheck.UseVisualStyleBackColor = true;
			this.taskStartIfConnectionCheck.CheckedChanged += new System.EventHandler(this.taskStartIfConnectionCheck_CheckedChanged);
			// 
			// availableConnectionsCombo
			// 
			resources.ApplyResources(this.availableConnectionsCombo, "availableConnectionsCombo");
			this.availableConnectionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.availableConnectionsCombo.FormattingEnabled = true;
			this.availableConnectionsCombo.Name = "availableConnectionsCombo";
			this.helpProvider.SetShowHelp(this.availableConnectionsCombo, ((bool)(resources.GetObject("availableConnectionsCombo.ShowHelp"))));
			this.availableConnectionsCombo.SelectedIndexChanged += new System.EventHandler(this.availableConnectionsCombo_SelectedIndexChanged);
			// 
			// powerConditionGroupBox
			// 
			resources.ApplyResources(this.powerConditionGroupBox, "powerConditionGroupBox");
			this.powerConditionGroupBox.Controls.Add(this.tableLayoutPanel5);
			this.powerConditionGroupBox.Name = "powerConditionGroupBox";
			this.helpProvider.SetShowHelp(this.powerConditionGroupBox, ((bool)(resources.GetObject("powerConditionGroupBox.ShowHelp"))));
			this.powerConditionGroupBox.TabStop = false;
			// 
			// tableLayoutPanel5
			// 
			resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
			this.tableLayoutPanel5.Controls.Add(this.taskDisallowStartIfOnBatteriesCheck, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.taskWakeToRunCheck, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.taskStopIfGoingOnBatteriesCheck, 0, 1);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			// 
			// taskDisallowStartIfOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskDisallowStartIfOnBatteriesCheck, "taskDisallowStartIfOnBatteriesCheck");
			this.taskDisallowStartIfOnBatteriesCheck.Name = "taskDisallowStartIfOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartIfOnBatteriesCheck, ((bool)(resources.GetObject("taskDisallowStartIfOnBatteriesCheck.ShowHelp"))));
			this.taskDisallowStartIfOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartIfOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartIfOnBatteriesCheck_CheckedChanged);
			// 
			// taskWakeToRunCheck
			// 
			resources.ApplyResources(this.taskWakeToRunCheck, "taskWakeToRunCheck");
			this.taskWakeToRunCheck.Name = "taskWakeToRunCheck";
			this.helpProvider.SetShowHelp(this.taskWakeToRunCheck, ((bool)(resources.GetObject("taskWakeToRunCheck.ShowHelp"))));
			this.taskWakeToRunCheck.UseVisualStyleBackColor = true;
			this.taskWakeToRunCheck.CheckedChanged += new System.EventHandler(this.taskWakeToRunCheck_CheckedChanged);
			// 
			// taskStopIfGoingOnBatteriesCheck
			// 
			resources.ApplyResources(this.taskStopIfGoingOnBatteriesCheck, "taskStopIfGoingOnBatteriesCheck");
			this.taskStopIfGoingOnBatteriesCheck.Name = "taskStopIfGoingOnBatteriesCheck";
			this.helpProvider.SetShowHelp(this.taskStopIfGoingOnBatteriesCheck, ((bool)(resources.GetObject("taskStopIfGoingOnBatteriesCheck.ShowHelp"))));
			this.taskStopIfGoingOnBatteriesCheck.UseVisualStyleBackColor = true;
			this.taskStopIfGoingOnBatteriesCheck.CheckedChanged += new System.EventHandler(this.taskStopIfGoingOnBatteriesCheck_CheckedChanged);
			// 
			// idleConditionGroupBox
			// 
			resources.ApplyResources(this.idleConditionGroupBox, "idleConditionGroupBox");
			this.idleConditionGroupBox.Controls.Add(this.tableLayoutPanel4);
			this.idleConditionGroupBox.Name = "idleConditionGroupBox";
			this.helpProvider.SetShowHelp(this.idleConditionGroupBox, ((bool)(resources.GetObject("idleConditionGroupBox.ShowHelp"))));
			this.idleConditionGroupBox.TabStop = false;
			// 
			// tableLayoutPanel4
			// 
			resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
			this.tableLayoutPanel4.Controls.Add(this.taskIdleDurationCheck, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.taskIdleWaitTimeoutCombo, 1, 1);
			this.tableLayoutPanel4.Controls.Add(this.taskIdleWaitTimeoutLabel, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.taskIdleDurationCombo, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.taskStopOnIdleEndCheck, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.taskRestartOnIdleCheck, 0, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			// 
			// taskIdleDurationCheck
			// 
			resources.ApplyResources(this.taskIdleDurationCheck, "taskIdleDurationCheck");
			this.taskIdleDurationCheck.Name = "taskIdleDurationCheck";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCheck, ((bool)(resources.GetObject("taskIdleDurationCheck.ShowHelp"))));
			this.taskIdleDurationCheck.UseVisualStyleBackColor = true;
			this.taskIdleDurationCheck.CheckedChanged += new System.EventHandler(this.taskIdleDurationCheck_CheckedChanged);
			// 
			// taskIdleWaitTimeoutCombo
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutCombo, "taskIdleWaitTimeoutCombo");
			this.taskIdleWaitTimeoutCombo.Name = "taskIdleWaitTimeoutCombo";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutCombo, ((bool)(resources.GetObject("taskIdleWaitTimeoutCombo.ShowHelp"))));
			this.taskIdleWaitTimeoutCombo.ValueChanged += new System.EventHandler(this.taskIdleWaitTimeoutCombo_ValueChanged);
			this.taskIdleWaitTimeoutCombo.Validating += new System.ComponentModel.CancelEventHandler(this.span_Validating);
			// 
			// taskIdleWaitTimeoutLabel
			// 
			resources.ApplyResources(this.taskIdleWaitTimeoutLabel, "taskIdleWaitTimeoutLabel");
			this.taskIdleWaitTimeoutLabel.Name = "taskIdleWaitTimeoutLabel";
			this.helpProvider.SetShowHelp(this.taskIdleWaitTimeoutLabel, ((bool)(resources.GetObject("taskIdleWaitTimeoutLabel.ShowHelp"))));
			// 
			// taskIdleDurationCombo
			// 
			resources.ApplyResources(this.taskIdleDurationCombo, "taskIdleDurationCombo");
			this.taskIdleDurationCombo.Name = "taskIdleDurationCombo";
			this.helpProvider.SetShowHelp(this.taskIdleDurationCombo, ((bool)(resources.GetObject("taskIdleDurationCombo.ShowHelp"))));
			this.taskIdleDurationCombo.ValueChanged += new System.EventHandler(this.taskIdleDurationCombo_ValueChanged);
			this.taskIdleDurationCombo.Validating += new System.ComponentModel.CancelEventHandler(this.span_Validating);
			// 
			// taskStopOnIdleEndCheck
			// 
			resources.ApplyResources(this.taskStopOnIdleEndCheck, "taskStopOnIdleEndCheck");
			this.taskStopOnIdleEndCheck.Checked = true;
			this.taskStopOnIdleEndCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.taskStopOnIdleEndCheck.Name = "taskStopOnIdleEndCheck";
			this.helpProvider.SetShowHelp(this.taskStopOnIdleEndCheck, ((bool)(resources.GetObject("taskStopOnIdleEndCheck.ShowHelp"))));
			this.taskStopOnIdleEndCheck.UseVisualStyleBackColor = true;
			this.taskStopOnIdleEndCheck.CheckedChanged += new System.EventHandler(this.taskStopOnIdleEndCheck_CheckedChanged);
			// 
			// taskRestartOnIdleCheck
			// 
			resources.ApplyResources(this.taskRestartOnIdleCheck, "taskRestartOnIdleCheck");
			this.taskRestartOnIdleCheck.Name = "taskRestartOnIdleCheck";
			this.helpProvider.SetShowHelp(this.taskRestartOnIdleCheck, ((bool)(resources.GetObject("taskRestartOnIdleCheck.ShowHelp"))));
			this.taskRestartOnIdleCheck.UseVisualStyleBackColor = true;
			this.taskRestartOnIdleCheck.CheckedChanged += new System.EventHandler(this.taskRestartOnIdleCheck_CheckedChanged);
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
			this.settingsTab.Controls.Add(this.tableLayoutPanel3);
			this.settingsTab.Controls.Add(this.settingsIntroLabel);
			this.helpProvider.SetHelpKeyword(this.settingsTab, resources.GetString("settingsTab.HelpKeyword"));
			this.helpProvider.SetHelpNavigator(this.settingsTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("settingsTab.HelpNavigator"))));
			this.settingsTab.Name = "settingsTab";
			this.helpProvider.SetShowHelp(this.settingsTab, ((bool)(resources.GetObject("settingsTab.ShowHelp"))));
			this.settingsTab.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Controls.Add(this.taskAllowDemandStartCheck, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.taskStartWhenAvailableCheck, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.taskMultInstCombo, 0, 8);
			this.tableLayoutPanel3.Controls.Add(this.taskRestartIntervalCheck, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.taskRunningRuleLabel, 0, 7);
			this.tableLayoutPanel3.Controls.Add(this.taskExecutionTimeLimitCombo, 1, 4);
			this.tableLayoutPanel3.Controls.Add(this.taskRestartCountLabel, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.taskRestartIntervalCombo, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.taskExecutionTimeLimitCheck, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.taskAllowHardTerminateCheck, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 1, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// taskMultInstCombo
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.taskMultInstCombo, 2);
			this.taskMultInstCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.taskMultInstCombo.FormattingEnabled = true;
			resources.ApplyResources(this.taskMultInstCombo, "taskMultInstCombo");
			this.taskMultInstCombo.Name = "taskMultInstCombo";
			this.helpProvider.SetShowHelp(this.taskMultInstCombo, ((bool)(resources.GetObject("taskMultInstCombo.ShowHelp"))));
			this.taskMultInstCombo.SelectedIndexChanged += new System.EventHandler(this.taskMultInstCombo_SelectedIndexChanged);
			// 
			// taskRunningRuleLabel
			// 
			resources.ApplyResources(this.taskRunningRuleLabel, "taskRunningRuleLabel");
			this.tableLayoutPanel3.SetColumnSpan(this.taskRunningRuleLabel, 2);
			this.taskRunningRuleLabel.Name = "taskRunningRuleLabel";
			this.helpProvider.SetShowHelp(this.taskRunningRuleLabel, ((bool)(resources.GetObject("taskRunningRuleLabel.ShowHelp"))));
			// 
			// taskAllowHardTerminateCheck
			// 
			resources.ApplyResources(this.taskAllowHardTerminateCheck, "taskAllowHardTerminateCheck");
			this.tableLayoutPanel3.SetColumnSpan(this.taskAllowHardTerminateCheck, 2);
			this.taskAllowHardTerminateCheck.Name = "taskAllowHardTerminateCheck";
			this.helpProvider.SetShowHelp(this.taskAllowHardTerminateCheck, ((bool)(resources.GetObject("taskAllowHardTerminateCheck.ShowHelp"))));
			this.taskAllowHardTerminateCheck.UseVisualStyleBackColor = true;
			this.taskAllowHardTerminateCheck.CheckedChanged += new System.EventHandler(this.taskAllowHardTerminateCheck_CheckedChanged);
			// 
			// taskStartWhenAvailableCheck
			// 
			resources.ApplyResources(this.taskStartWhenAvailableCheck, "taskStartWhenAvailableCheck");
			this.tableLayoutPanel3.SetColumnSpan(this.taskStartWhenAvailableCheck, 2);
			this.taskStartWhenAvailableCheck.Name = "taskStartWhenAvailableCheck";
			this.helpProvider.SetShowHelp(this.taskStartWhenAvailableCheck, ((bool)(resources.GetObject("taskStartWhenAvailableCheck.ShowHelp"))));
			this.taskStartWhenAvailableCheck.UseVisualStyleBackColor = true;
			this.taskStartWhenAvailableCheck.CheckedChanged += new System.EventHandler(this.taskStartWhenAvailableCheck_CheckedChanged);
			// 
			// taskAllowDemandStartCheck
			// 
			resources.ApplyResources(this.taskAllowDemandStartCheck, "taskAllowDemandStartCheck");
			this.tableLayoutPanel3.SetColumnSpan(this.taskAllowDemandStartCheck, 2);
			this.taskAllowDemandStartCheck.Name = "taskAllowDemandStartCheck";
			this.helpProvider.SetShowHelp(this.taskAllowDemandStartCheck, ((bool)(resources.GetObject("taskAllowDemandStartCheck.ShowHelp"))));
			this.taskAllowDemandStartCheck.UseVisualStyleBackColor = true;
			this.taskAllowDemandStartCheck.CheckedChanged += new System.EventHandler(this.taskAllowDemandStartCheck_CheckedChanged);
			// 
			// taskRestartIntervalCheck
			// 
			resources.ApplyResources(this.taskRestartIntervalCheck, "taskRestartIntervalCheck");
			this.taskRestartIntervalCheck.Name = "taskRestartIntervalCheck";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCheck, ((bool)(resources.GetObject("taskRestartIntervalCheck.ShowHelp"))));
			this.taskRestartIntervalCheck.UseVisualStyleBackColor = true;
			this.taskRestartIntervalCheck.CheckedChanged += new System.EventHandler(this.taskRestartIntervalCheck_CheckedChanged);
			// 
			// taskExecutionTimeLimitCombo
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCombo, "taskExecutionTimeLimitCombo");
			this.taskExecutionTimeLimitCombo.Name = "taskExecutionTimeLimitCombo";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCombo, ((bool)(resources.GetObject("taskExecutionTimeLimitCombo.ShowHelp"))));
			this.taskExecutionTimeLimitCombo.ValueChanged += new System.EventHandler(this.taskExecutionTimeLimitCombo_ValueChanged);
			this.taskExecutionTimeLimitCombo.Validating += new System.ComponentModel.CancelEventHandler(this.span_Validating);
			// 
			// taskRestartCountLabel
			// 
			resources.ApplyResources(this.taskRestartCountLabel, "taskRestartCountLabel");
			this.taskRestartCountLabel.Name = "taskRestartCountLabel";
			this.helpProvider.SetShowHelp(this.taskRestartCountLabel, ((bool)(resources.GetObject("taskRestartCountLabel.ShowHelp"))));
			// 
			// taskRestartIntervalCombo
			// 
			resources.ApplyResources(this.taskRestartIntervalCombo, "taskRestartIntervalCombo");
			this.taskRestartIntervalCombo.Name = "taskRestartIntervalCombo";
			this.helpProvider.SetShowHelp(this.taskRestartIntervalCombo, ((bool)(resources.GetObject("taskRestartIntervalCombo.ShowHelp"))));
			this.taskRestartIntervalCombo.ValueChanged += new System.EventHandler(this.taskRestartIntervalCombo_ValueChanged);
			this.taskRestartIntervalCombo.Validating += new System.ComponentModel.CancelEventHandler(this.span_Validating);
			// 
			// taskExecutionTimeLimitCheck
			// 
			resources.ApplyResources(this.taskExecutionTimeLimitCheck, "taskExecutionTimeLimitCheck");
			this.taskExecutionTimeLimitCheck.Name = "taskExecutionTimeLimitCheck";
			this.helpProvider.SetShowHelp(this.taskExecutionTimeLimitCheck, ((bool)(resources.GetObject("taskExecutionTimeLimitCheck.ShowHelp"))));
			this.taskExecutionTimeLimitCheck.UseVisualStyleBackColor = true;
			this.taskExecutionTimeLimitCheck.CheckedChanged += new System.EventHandler(this.taskExecutionTimeLimitCheck_CheckedChanged);
			// 
			// flowLayoutPanel1
			// 
			resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
			this.tableLayoutPanel3.SetColumnSpan(this.flowLayoutPanel1, 2);
			this.flowLayoutPanel1.Controls.Add(this.taskDeleteAfterCheck);
			this.flowLayoutPanel1.Controls.Add(this.taskDeleteAfterCombo);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			// 
			// taskDeleteAfterCheck
			// 
			resources.ApplyResources(this.taskDeleteAfterCheck, "taskDeleteAfterCheck");
			this.taskDeleteAfterCheck.Name = "taskDeleteAfterCheck";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCheck, ((bool)(resources.GetObject("taskDeleteAfterCheck.ShowHelp"))));
			this.taskDeleteAfterCheck.UseVisualStyleBackColor = true;
			this.taskDeleteAfterCheck.CheckedChanged += new System.EventHandler(this.taskDeleteAfterCheck_CheckedChanged);
			// 
			// taskDeleteAfterCombo
			// 
			resources.ApplyResources(this.taskDeleteAfterCombo, "taskDeleteAfterCombo");
			this.taskDeleteAfterCombo.Name = "taskDeleteAfterCombo";
			this.helpProvider.SetShowHelp(this.taskDeleteAfterCombo, ((bool)(resources.GetObject("taskDeleteAfterCombo.ShowHelp"))));
			this.taskDeleteAfterCombo.ValueChanged += new System.EventHandler(this.taskDeleteAfterCombo_ValueChanged);
			this.taskDeleteAfterCombo.Validating += new System.ComponentModel.CancelEventHandler(this.span_Validating);
			// 
			// flowLayoutPanel2
			// 
			resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
			this.flowLayoutPanel2.Controls.Add(this.taskRestartCountText);
			this.flowLayoutPanel2.Controls.Add(this.taskRestartAttemptTimesLabel);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.helpProvider.SetShowHelp(this.flowLayoutPanel2, ((bool)(resources.GetObject("flowLayoutPanel2.ShowHelp"))));
			// 
			// taskRestartCountText
			// 
			resources.ApplyResources(this.taskRestartCountText, "taskRestartCountText");
			this.taskRestartCountText.Name = "taskRestartCountText";
			this.helpProvider.SetShowHelp(this.taskRestartCountText, ((bool)(resources.GetObject("taskRestartCountText.ShowHelp"))));
			this.taskRestartCountText.ValueChanged += new System.EventHandler(this.taskRestartCountText_ValueChanged);
			// 
			// taskRestartAttemptTimesLabel
			// 
			resources.ApplyResources(this.taskRestartAttemptTimesLabel, "taskRestartAttemptTimesLabel");
			this.taskRestartAttemptTimesLabel.Name = "taskRestartAttemptTimesLabel";
			this.helpProvider.SetShowHelp(this.taskRestartAttemptTimesLabel, ((bool)(resources.GetObject("taskRestartAttemptTimesLabel.ShowHelp"))));
			// 
			// settingsIntroLabel
			// 
			resources.ApplyResources(this.settingsIntroLabel, "settingsIntroLabel");
			this.settingsIntroLabel.Name = "settingsIntroLabel";
			this.helpProvider.SetShowHelp(this.settingsIntroLabel, ((bool)(resources.GetObject("settingsIntroLabel.ShowHelp"))));
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
			this.taskRegDocText.Leave += new System.EventHandler(this.taskRegDocText_Leave);
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
			this.taskRegSourceText.Leave += new System.EventHandler(this.taskRegSourceText_Leave);
			// 
			// taskRegURIText
			// 
			resources.ApplyResources(this.taskRegURIText, "taskRegURIText");
			this.taskRegLayoutPanel.SetColumnSpan(this.taskRegURIText, 2);
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
			this.errorProvider.SetIconPadding(this.taskRegVersionText, ((int)(resources.GetObject("taskRegVersionText.IconPadding"))));
			this.taskRegVersionText.Name = "taskRegVersionText";
			this.helpProvider.SetShowHelp(this.taskRegVersionText, ((bool)(resources.GetObject("taskRegVersionText.ShowHelp"))));
			this.taskRegVersionText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegVersionText_Validating);
			this.taskRegVersionText.Validated += new System.EventHandler(this.taskRegVersionText_Validated);
			// 
			// taskRegSDDLText
			// 
			resources.ApplyResources(this.taskRegSDDLText, "taskRegSDDLText");
			this.errorProvider.SetIconPadding(this.taskRegSDDLText, ((int)(resources.GetObject("taskRegSDDLText.IconPadding"))));
			this.taskRegSDDLText.Name = "taskRegSDDLText";
			this.helpProvider.SetShowHelp(this.taskRegSDDLText, ((bool)(resources.GetObject("taskRegSDDLText.ShowHelp"))));
			this.taskRegSDDLText.Validating += new System.ComponentModel.CancelEventHandler(this.taskRegSDDLText_Validating);
			this.taskRegSDDLText.Validated += new System.EventHandler(this.taskRegSDDLText_Validated);
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
			this.taskRegSDDLBtn.Click += new System.EventHandler(this.taskRegSDDLBtn_Click);
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
			this.addPropTab.Controls.Add(this.addtPanel);
			this.addPropTab.Controls.Add(this.label4);
			this.addPropTab.Name = "addPropTab";
			this.helpProvider.SetShowHelp(this.addPropTab, ((bool)(resources.GetObject("addPropTab.ShowHelp"))));
			this.addPropTab.UseVisualStyleBackColor = true;
			// 
			// addtPanel
			// 
			resources.ApplyResources(this.addtPanel, "addtPanel");
			this.addtPanel.Controls.Add(this.autoMaintGroup, 0, 6);
			this.addtPanel.Controls.Add(this.taskEnabledCheck, 0, 0);
			this.addtPanel.Controls.Add(this.secHardGroup, 0, 5);
			this.addtPanel.Controls.Add(this.taskDisallowStartOnRemoteAppSessionCheck, 0, 1);
			this.addtPanel.Controls.Add(this.taskUseUnifiedSchedulingEngineCheck, 0, 2);
			this.addtPanel.Controls.Add(this.taskPriorityCombo, 1, 3);
			this.addtPanel.Controls.Add(this.taskVolatileCheck, 0, 4);
			this.addtPanel.Controls.Add(this.label8, 0, 3);
			this.addtPanel.Name = "addtPanel";
			this.helpProvider.SetShowHelp(this.addtPanel, ((bool)(resources.GetObject("addtPanel.ShowHelp"))));
			// 
			// autoMaintGroup
			// 
			resources.ApplyResources(this.autoMaintGroup, "autoMaintGroup");
			this.addtPanel.SetColumnSpan(this.autoMaintGroup, 2);
			this.autoMaintGroup.Controls.Add(this.tableLayoutPanel2);
			this.autoMaintGroup.Name = "autoMaintGroup";
			this.helpProvider.SetShowHelp(this.autoMaintGroup, ((bool)(resources.GetObject("autoMaintGroup.ShowHelp"))));
			this.autoMaintGroup.TabStop = false;
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.taskMaintenanceDeadlineLabel, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.taskMaintenancePeriodCombo, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.taskMaintenanceDeadlineCombo, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.taskMaintenancePeriodLabel, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.taskMaintenanceExclusiveCheck, 0, 2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// taskMaintenanceDeadlineLabel
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineLabel, "taskMaintenanceDeadlineLabel");
			this.taskMaintenanceDeadlineLabel.Name = "taskMaintenanceDeadlineLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineLabel, ((bool)(resources.GetObject("taskMaintenanceDeadlineLabel.ShowHelp"))));
			// 
			// taskMaintenancePeriodCombo
			// 
			resources.ApplyResources(this.taskMaintenancePeriodCombo, "taskMaintenancePeriodCombo");
			this.taskMaintenancePeriodCombo.Name = "taskMaintenancePeriodCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodCombo, ((bool)(resources.GetObject("taskMaintenancePeriodCombo.ShowHelp"))));
			this.taskMaintenancePeriodCombo.ValueChanged += new System.EventHandler(this.taskMaintenancePeriodCombo_ValueChanged);
			this.taskMaintenancePeriodCombo.Validating += new System.ComponentModel.CancelEventHandler(this.taskMaintenancePeriodCombo_Validating);
			this.taskMaintenancePeriodCombo.Validated += new System.EventHandler(this.taskMaintenancePeriodCombo_Validated);
			// 
			// taskMaintenanceDeadlineCombo
			// 
			resources.ApplyResources(this.taskMaintenanceDeadlineCombo, "taskMaintenanceDeadlineCombo");
			this.taskMaintenanceDeadlineCombo.Name = "taskMaintenanceDeadlineCombo";
			this.helpProvider.SetShowHelp(this.taskMaintenanceDeadlineCombo, ((bool)(resources.GetObject("taskMaintenanceDeadlineCombo.ShowHelp"))));
			this.taskMaintenanceDeadlineCombo.ValueChanged += new System.EventHandler(this.taskMaintenanceDeadlineCombo_ValueChanged);
			this.taskMaintenanceDeadlineCombo.Validating += new System.ComponentModel.CancelEventHandler(this.taskMaintenanceDeadlineCombo_Validating);
			this.taskMaintenanceDeadlineCombo.Validated += new System.EventHandler(this.taskMaintenanceDeadlineCombo_Validated);
			// 
			// taskMaintenancePeriodLabel
			// 
			resources.ApplyResources(this.taskMaintenancePeriodLabel, "taskMaintenancePeriodLabel");
			this.taskMaintenancePeriodLabel.Name = "taskMaintenancePeriodLabel";
			this.helpProvider.SetShowHelp(this.taskMaintenancePeriodLabel, ((bool)(resources.GetObject("taskMaintenancePeriodLabel.ShowHelp"))));
			// 
			// taskMaintenanceExclusiveCheck
			// 
			resources.ApplyResources(this.taskMaintenanceExclusiveCheck, "taskMaintenanceExclusiveCheck");
			this.tableLayoutPanel2.SetColumnSpan(this.taskMaintenanceExclusiveCheck, 2);
			this.taskMaintenanceExclusiveCheck.Name = "taskMaintenanceExclusiveCheck";
			this.helpProvider.SetShowHelp(this.taskMaintenanceExclusiveCheck, ((bool)(resources.GetObject("taskMaintenanceExclusiveCheck.ShowHelp"))));
			this.taskMaintenanceExclusiveCheck.UseVisualStyleBackColor = true;
			this.taskMaintenanceExclusiveCheck.CheckedChanged += new System.EventHandler(this.taskMaintenanceExclusiveCheck_CheckedChanged);
			// 
			// taskEnabledCheck
			// 
			resources.ApplyResources(this.taskEnabledCheck, "taskEnabledCheck");
			this.addtPanel.SetColumnSpan(this.taskEnabledCheck, 2);
			this.taskEnabledCheck.Name = "taskEnabledCheck";
			this.helpProvider.SetShowHelp(this.taskEnabledCheck, ((bool)(resources.GetObject("taskEnabledCheck.ShowHelp"))));
			this.taskEnabledCheck.UseVisualStyleBackColor = true;
			this.taskEnabledCheck.CheckedChanged += new System.EventHandler(this.taskEnabledCheck_CheckedChanged);
			// 
			// secHardGroup
			// 
			resources.ApplyResources(this.secHardGroup, "secHardGroup");
			this.addtPanel.SetColumnSpan(this.secHardGroup, 2);
			this.secHardGroup.Controls.Add(this.tableLayoutPanel1);
			this.secHardGroup.Name = "secHardGroup";
			this.helpProvider.SetShowHelp(this.secHardGroup, ((bool)(resources.GetObject("secHardGroup.ShowHelp"))));
			this.secHardGroup.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.principalSIDTypeLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.principalReqPrivilegesDropDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.principalSIDTypeCombo, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.principalReqPrivilegesLabel, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// principalSIDTypeLabel
			// 
			resources.ApplyResources(this.principalSIDTypeLabel, "principalSIDTypeLabel");
			this.principalSIDTypeLabel.Name = "principalSIDTypeLabel";
			this.helpProvider.SetShowHelp(this.principalSIDTypeLabel, ((bool)(resources.GetObject("principalSIDTypeLabel.ShowHelp"))));
			// 
			// principalReqPrivilegesDropDown
			// 
			resources.ApplyResources(this.principalReqPrivilegesDropDown, "principalReqPrivilegesDropDown");
			this.principalReqPrivilegesDropDown.BackColor = System.Drawing.Color.White;
			this.principalReqPrivilegesDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.principalReqPrivilegesDropDown.DropSize = new System.Drawing.Size(121, 106);
			this.principalReqPrivilegesDropDown.Name = "principalReqPrivilegesDropDown";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesDropDown, ((bool)(resources.GetObject("principalReqPrivilegesDropDown.ShowHelp"))));
			this.principalReqPrivilegesDropDown.SelectedIndexChanged += new System.EventHandler(this.principalReqPrivilegesDropDown_SelectedIndexChanged);
			// 
			// principalSIDTypeCombo
			// 
			resources.ApplyResources(this.principalSIDTypeCombo, "principalSIDTypeCombo");
			this.principalSIDTypeCombo.DisplayMember = "Text";
			this.principalSIDTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.principalSIDTypeCombo.Name = "principalSIDTypeCombo";
			this.helpProvider.SetShowHelp(this.principalSIDTypeCombo, ((bool)(resources.GetObject("principalSIDTypeCombo.ShowHelp"))));
			this.principalSIDTypeCombo.ValueMember = "Value";
			this.principalSIDTypeCombo.SelectedIndexChanged += new System.EventHandler(this.principalSIDTypeCombo_SelectedIndexChanged);
			// 
			// principalReqPrivilegesLabel
			// 
			resources.ApplyResources(this.principalReqPrivilegesLabel, "principalReqPrivilegesLabel");
			this.principalReqPrivilegesLabel.Name = "principalReqPrivilegesLabel";
			this.helpProvider.SetShowHelp(this.principalReqPrivilegesLabel, ((bool)(resources.GetObject("principalReqPrivilegesLabel.ShowHelp"))));
			// 
			// taskDisallowStartOnRemoteAppSessionCheck
			// 
			resources.ApplyResources(this.taskDisallowStartOnRemoteAppSessionCheck, "taskDisallowStartOnRemoteAppSessionCheck");
			this.addtPanel.SetColumnSpan(this.taskDisallowStartOnRemoteAppSessionCheck, 2);
			this.taskDisallowStartOnRemoteAppSessionCheck.Name = "taskDisallowStartOnRemoteAppSessionCheck";
			this.helpProvider.SetShowHelp(this.taskDisallowStartOnRemoteAppSessionCheck, ((bool)(resources.GetObject("taskDisallowStartOnRemoteAppSessionCheck.ShowHelp"))));
			this.taskDisallowStartOnRemoteAppSessionCheck.UseVisualStyleBackColor = true;
			this.taskDisallowStartOnRemoteAppSessionCheck.CheckedChanged += new System.EventHandler(this.taskDisallowStartOnRemoteAppSessionCheck_CheckedChanged);
			// 
			// taskUseUnifiedSchedulingEngineCheck
			// 
			resources.ApplyResources(this.taskUseUnifiedSchedulingEngineCheck, "taskUseUnifiedSchedulingEngineCheck");
			this.addtPanel.SetColumnSpan(this.taskUseUnifiedSchedulingEngineCheck, 2);
			this.taskUseUnifiedSchedulingEngineCheck.Name = "taskUseUnifiedSchedulingEngineCheck";
			this.helpProvider.SetShowHelp(this.taskUseUnifiedSchedulingEngineCheck, ((bool)(resources.GetObject("taskUseUnifiedSchedulingEngineCheck.ShowHelp"))));
			this.taskUseUnifiedSchedulingEngineCheck.UseVisualStyleBackColor = true;
			this.taskUseUnifiedSchedulingEngineCheck.CheckedChanged += new System.EventHandler(this.taskUseUnifiedSchedulingEngineCheck_CheckedChanged);
			// 
			// taskPriorityCombo
			// 
			this.taskPriorityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			resources.ApplyResources(this.taskPriorityCombo, "taskPriorityCombo");
			this.taskPriorityCombo.Name = "taskPriorityCombo";
			this.helpProvider.SetShowHelp(this.taskPriorityCombo, ((bool)(resources.GetObject("taskPriorityCombo.ShowHelp"))));
			this.taskPriorityCombo.SelectedIndexChanged += new System.EventHandler(this.taskPriorityCombo_SelectedIndexChanged);
			// 
			// taskVolatileCheck
			// 
			resources.ApplyResources(this.taskVolatileCheck, "taskVolatileCheck");
			this.addtPanel.SetColumnSpan(this.taskVolatileCheck, 2);
			this.taskVolatileCheck.Name = "taskVolatileCheck";
			this.helpProvider.SetShowHelp(this.taskVolatileCheck, ((bool)(resources.GetObject("taskVolatileCheck.ShowHelp"))));
			this.taskVolatileCheck.UseVisualStyleBackColor = true;
			this.taskVolatileCheck.CheckedChanged += new System.EventHandler(this.taskVolatileCheck_CheckedChanged);
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
			this.runTimesTab.Enter += new System.EventHandler(this.runTimesTab_Enter);
			this.runTimesTab.Leave += new System.EventHandler(this.runTimesTab_Leave);
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
			this.helpProvider.SetHelpNavigator(this.historyTab, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("historyTab.HelpNavigator"))));
			this.historyTab.Name = "historyTab";
			this.helpProvider.SetShowHelp(this.historyTab, ((bool)(resources.GetObject("historyTab.ShowHelp"))));
			this.historyTab.UseVisualStyleBackColor = true;
			this.historyTab.Enter += new System.EventHandler(this.historyTab_Enter);
			// 
			// taskHistoryControl1
			// 
			resources.ApplyResources(this.taskHistoryControl1, "taskHistoryControl1");
			this.taskHistoryControl1.Name = "taskHistoryControl1";
			this.helpProvider.SetShowHelp(this.taskHistoryControl1, ((bool)(resources.GetObject("taskHistoryControl1.ShowHelp"))));
			this.taskHistoryControl1.Load += new System.EventHandler(this.taskHistoryControl1_Load);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// TaskPropertiesControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl);
			this.Name = "TaskPropertiesControl";
			this.helpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Load += new System.EventHandler(this.TaskPropertiesControl_Load);
			this.tabControl.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.generalTab.PerformLayout();
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			this.taskSecurityGroupBox.ResumeLayout(false);
			this.taskSecurityGroupBox.PerformLayout();
			this.tableLayoutPanel7.ResumeLayout(false);
			this.tableLayoutPanel7.PerformLayout();
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			this.triggersTab.ResumeLayout(false);
			this.actionsTab.ResumeLayout(false);
			this.conditionsTab.ResumeLayout(false);
			this.conditionsTab.PerformLayout();
			this.networkConditionGroupBox.ResumeLayout(false);
			this.networkConditionGroupBox.PerformLayout();
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel6.PerformLayout();
			this.powerConditionGroupBox.ResumeLayout(false);
			this.powerConditionGroupBox.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.idleConditionGroupBox.ResumeLayout(false);
			this.idleConditionGroupBox.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.settingsTab.ResumeLayout(false);
			this.settingsTab.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRestartCountText)).EndInit();
			this.regInfoTab.ResumeLayout(false);
			this.taskRegLayoutPanel.ResumeLayout(false);
			this.taskRegLayoutPanel.PerformLayout();
			this.addPropTab.ResumeLayout(false);
			this.addPropTab.PerformLayout();
			this.addtPanel.ResumeLayout(false);
			this.addtPanel.PerformLayout();
			this.autoMaintGroup.ResumeLayout(false);
			this.autoMaintGroup.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.secHardGroup.ResumeLayout(false);
			this.secHardGroup.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.runTimesTab.ResumeLayout(false);
			this.runTimesTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskRunTimesControl1)).EndInit();
			this.historyTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
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
		private TableLayoutPanel addtPanel;
		private TableLayoutPanel tableLayoutPanel2;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel3;
		private FlowLayoutPanel flowLayoutPanel1;
		private FlowLayoutPanel flowLayoutPanel2;
		private TableLayoutPanel tableLayoutPanel6;
		private TableLayoutPanel tableLayoutPanel5;
		private TableLayoutPanel tableLayoutPanel4;
		private TableLayoutPanel tableLayoutPanel8;
		private TableLayoutPanel tableLayoutPanel7;
		private TableLayoutPanel tableLayoutPanel9;
	}
}
