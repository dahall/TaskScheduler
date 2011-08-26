namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskSchedulerWizard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskSchedulerWizard));
			this.wizardControl1 = new AeroWizard.WizardControl();
			this.introPage = new AeroWizard.WizardPage();
			this.nameText = new System.Windows.Forms.TextBox();
			this.descText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.secOptPage = new AeroWizard.WizardPage();
			this.triggerSelectPage = new AeroWizard.WizardPage();
			this.triggerSelectionList = new GroupControls.RadioButtonList();
			this.dailyTriggerPage = new AeroWizard.WizardPage();
			this.dailyStartTimePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.dailyRecurNumUpDn = new System.Windows.Forms.NumericUpDown();
			this.dailyDaysLabel = new System.Windows.Forms.Label();
			this.dailyRecurLabel = new System.Windows.Forms.Label();
			this.actionSelectPage = new AeroWizard.WizardPage();
			this.actionSelectionList = new GroupControls.RadioButtonList();
			this.oneTimeTriggerPage = new AeroWizard.WizardPage();
			this.oneTimeStartTimePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.oneTimeStartLabel = new System.Windows.Forms.Label();
			this.weeklyTriggerPage = new AeroWizard.WizardPage();
			this.weeklyStartTimePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.weeklySunCheck = new System.Windows.Forms.CheckBox();
			this.weeklyMonCheck = new System.Windows.Forms.CheckBox();
			this.weeklyTueCheck = new System.Windows.Forms.CheckBox();
			this.weeklyWedCheck = new System.Windows.Forms.CheckBox();
			this.weeklyThuCheck = new System.Windows.Forms.CheckBox();
			this.weeklyFriCheck = new System.Windows.Forms.CheckBox();
			this.weeklySatCheck = new System.Windows.Forms.CheckBox();
			this.weeklyRecurNumUpDn = new System.Windows.Forms.NumericUpDown();
			this.weeklyOnWeeksLabel = new System.Windows.Forms.Label();
			this.weeklyRecurLabel = new System.Windows.Forms.Label();
			this.monthlyTriggerPage = new AeroWizard.WizardPage();
			this.monthlyDaysDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyOnDOWDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyOnWeekDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyMonthsDropDown = new Microsoft.Win32.TaskScheduler.DropDownCheckList();
			this.monthlyOnRadio = new System.Windows.Forms.RadioButton();
			this.monthlyDaysRadio = new System.Windows.Forms.RadioButton();
			this.monthlyMonthsLabel = new System.Windows.Forms.Label();
			this.monthlyStartTimePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.onEventTriggerPage = new AeroWizard.WizardPage();
			this.onEventLogLabel = new System.Windows.Forms.Label();
			this.onEventIdText = new System.Windows.Forms.TextBox();
			this.onEventSourceLabel = new System.Windows.Forms.Label();
			this.onEventSourceCombo = new System.Windows.Forms.ComboBox();
			this.onEventLogCombo = new System.Windows.Forms.ComboBox();
			this.onEventIdLabel = new System.Windows.Forms.Label();
			this.runActionPage = new AeroWizard.WizardPage();
			this.execProgBrowseBtn = new System.Windows.Forms.Button();
			this.execDirText = new System.Windows.Forms.TextBox();
			this.execArgText = new System.Windows.Forms.TextBox();
			this.execProgText = new System.Windows.Forms.TextBox();
			this.execDirLabel = new System.Windows.Forms.Label();
			this.execArgLabel = new System.Windows.Forms.Label();
			this.execProgLabel = new System.Windows.Forms.Label();
			this.summaryPage = new AeroWizard.WizardPage();
			this.sumText = new System.Windows.Forms.TextBox();
			this.openDlgAfterCheck = new System.Windows.Forms.CheckBox();
			this.summaryPrompt = new System.Windows.Forms.Label();
			this.emailActionPage = new AeroWizard.WizardPage();
			this.emailAttachementBrowseBtn = new System.Windows.Forms.Button();
			this.emailSMTPText = new System.Windows.Forms.TextBox();
			this.emailSMTPLabel = new System.Windows.Forms.Label();
			this.emailAttachmentText = new System.Windows.Forms.TextBox();
			this.emailAttachmentLabel = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.emailTextText = new System.Windows.Forms.TextBox();
			this.emailTextLabel = new System.Windows.Forms.Label();
			this.emailSubjectText = new System.Windows.Forms.TextBox();
			this.emailSubjectLabel = new System.Windows.Forms.Label();
			this.emailToText = new System.Windows.Forms.TextBox();
			this.emailToLabel = new System.Windows.Forms.Label();
			this.emailFromText = new System.Windows.Forms.TextBox();
			this.emailFromLabel = new System.Windows.Forms.Label();
			this.msgActionPage = new AeroWizard.WizardPage();
			this.msgMsgText = new System.Windows.Forms.TextBox();
			this.msgMsgLabel = new System.Windows.Forms.Label();
			this.msgTitleText = new System.Windows.Forms.TextBox();
			this.msgTitleLabel = new System.Windows.Forms.Label();
			this.msgIntroLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.taskLocalOnlyCheck = new System.Windows.Forms.CheckBox();
			this.taskLoggedOptionalRadio = new System.Windows.Forms.RadioButton();
			this.taskLoggedOnRadio = new System.Windows.Forms.RadioButton();
			this.taskPrincipalText = new System.Windows.Forms.TextBox();
			this.changePrincipalButton = new System.Windows.Forms.Button();
			this.taskUserAcctLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
			this.introPage.SuspendLayout();
			this.secOptPage.SuspendLayout();
			this.triggerSelectPage.SuspendLayout();
			this.dailyTriggerPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dailyRecurNumUpDn)).BeginInit();
			this.actionSelectPage.SuspendLayout();
			this.oneTimeTriggerPage.SuspendLayout();
			this.weeklyTriggerPage.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.weeklyRecurNumUpDn)).BeginInit();
			this.monthlyTriggerPage.SuspendLayout();
			this.onEventTriggerPage.SuspendLayout();
			this.runActionPage.SuspendLayout();
			this.summaryPage.SuspendLayout();
			this.emailActionPage.SuspendLayout();
			this.msgActionPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardControl1
			// 
			resources.ApplyResources(this.wizardControl1, "wizardControl1");
			this.wizardControl1.Name = "wizardControl1";
			this.wizardControl1.Pages.Add(this.introPage);
			this.wizardControl1.Pages.Add(this.secOptPage);
			this.wizardControl1.Pages.Add(this.triggerSelectPage);
			this.wizardControl1.Pages.Add(this.dailyTriggerPage);
			this.wizardControl1.Pages.Add(this.oneTimeTriggerPage);
			this.wizardControl1.Pages.Add(this.weeklyTriggerPage);
			this.wizardControl1.Pages.Add(this.monthlyTriggerPage);
			this.wizardControl1.Pages.Add(this.onEventTriggerPage);
			this.wizardControl1.Pages.Add(this.actionSelectPage);
			this.wizardControl1.Pages.Add(this.runActionPage);
			this.wizardControl1.Pages.Add(this.emailActionPage);
			this.wizardControl1.Pages.Add(this.msgActionPage);
			this.wizardControl1.Pages.Add(this.summaryPage);
			this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
			this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.wizardControl1_SelectedPageChanged);
			// 
			// introPage
			// 
			this.introPage.AllowNext = false;
			this.introPage.Controls.Add(this.nameText);
			this.introPage.Controls.Add(this.descText);
			this.introPage.Controls.Add(this.label2);
			this.introPage.Controls.Add(this.label1);
			this.introPage.Name = "introPage";
			resources.ApplyResources(this.introPage, "introPage");
			// 
			// nameText
			// 
			resources.ApplyResources(this.nameText, "nameText");
			this.nameText.Name = "nameText";
			this.nameText.TextChanged += new System.EventHandler(this.nameText_TextChanged);
			// 
			// descText
			// 
			resources.ApplyResources(this.descText, "descText");
			this.descText.Name = "descText";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// secOptPage
			// 
			this.secOptPage.Controls.Add(this.taskLocalOnlyCheck);
			this.secOptPage.Controls.Add(this.taskLoggedOptionalRadio);
			this.secOptPage.Controls.Add(this.taskLoggedOnRadio);
			this.secOptPage.Controls.Add(this.taskPrincipalText);
			this.secOptPage.Controls.Add(this.changePrincipalButton);
			this.secOptPage.Controls.Add(this.taskUserAcctLabel);
			this.secOptPage.Name = "secOptPage";
			resources.ApplyResources(this.secOptPage, "secOptPage");
			// 
			// triggerSelectPage
			// 
			this.triggerSelectPage.AllowNext = false;
			this.triggerSelectPage.Controls.Add(this.triggerSelectionList);
			this.triggerSelectPage.Name = "triggerSelectPage";
			resources.ApplyResources(this.triggerSelectPage, "triggerSelectPage");
			this.triggerSelectPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.triggerSelectPage_Commit);
			// 
			// triggerSelectionList
			// 
			resources.ApplyResources(this.triggerSelectionList, "triggerSelectionList");
			this.triggerSelectionList.Name = "triggerSelectionList";
			this.triggerSelectionList.SubtextForeColor = System.Drawing.SystemColors.GrayText;
			this.triggerSelectionList.SelectedIndexChanged += new System.EventHandler(this.triggerSelectionList_SelectedIndexChanged);
			// 
			// dailyTriggerPage
			// 
			this.dailyTriggerPage.Controls.Add(this.dailyStartTimePicker);
			this.dailyTriggerPage.Controls.Add(this.label3);
			this.dailyTriggerPage.Controls.Add(this.dailyRecurNumUpDn);
			this.dailyTriggerPage.Controls.Add(this.dailyDaysLabel);
			this.dailyTriggerPage.Controls.Add(this.dailyRecurLabel);
			this.dailyTriggerPage.Name = "dailyTriggerPage";
			this.dailyTriggerPage.NextPage = this.actionSelectPage;
			resources.ApplyResources(this.dailyTriggerPage, "dailyTriggerPage");
			this.dailyTriggerPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.dailyTriggerPage_Commit);
			// 
			// dailyStartTimePicker
			// 
			resources.ApplyResources(this.dailyStartTimePicker, "dailyStartTimePicker");
			this.dailyStartTimePicker.Name = "dailyStartTimePicker";
			this.dailyStartTimePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.AssumeUtc;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// dailyRecurNumUpDn
			// 
			resources.ApplyResources(this.dailyRecurNumUpDn, "dailyRecurNumUpDn");
			this.dailyRecurNumUpDn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.dailyRecurNumUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.dailyRecurNumUpDn.Name = "dailyRecurNumUpDn";
			this.dailyRecurNumUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// dailyDaysLabel
			// 
			resources.ApplyResources(this.dailyDaysLabel, "dailyDaysLabel");
			this.dailyDaysLabel.Name = "dailyDaysLabel";
			// 
			// dailyRecurLabel
			// 
			resources.ApplyResources(this.dailyRecurLabel, "dailyRecurLabel");
			this.dailyRecurLabel.Name = "dailyRecurLabel";
			// 
			// actionSelectPage
			// 
			this.actionSelectPage.AllowNext = false;
			this.actionSelectPage.Controls.Add(this.actionSelectionList);
			this.actionSelectPage.Name = "actionSelectPage";
			resources.ApplyResources(this.actionSelectPage, "actionSelectPage");
			this.actionSelectPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.actionSelectPage_Commit);
			// 
			// actionSelectionList
			// 
			resources.ApplyResources(this.actionSelectionList, "actionSelectionList");
			this.actionSelectionList.Name = "actionSelectionList";
			this.actionSelectionList.SubtextForeColor = System.Drawing.SystemColors.GrayText;
			this.actionSelectionList.SelectedIndexChanged += new System.EventHandler(this.actionSelectionList_SelectedIndexChanged);
			// 
			// oneTimeTriggerPage
			// 
			this.oneTimeTriggerPage.Controls.Add(this.oneTimeStartTimePicker);
			this.oneTimeTriggerPage.Controls.Add(this.oneTimeStartLabel);
			this.oneTimeTriggerPage.Name = "oneTimeTriggerPage";
			this.oneTimeTriggerPage.NextPage = this.actionSelectPage;
			resources.ApplyResources(this.oneTimeTriggerPage, "oneTimeTriggerPage");
			this.oneTimeTriggerPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.oneTimeTriggerPage_Commit);
			// 
			// oneTimeStartTimePicker
			// 
			resources.ApplyResources(this.oneTimeStartTimePicker, "oneTimeStartTimePicker");
			this.oneTimeStartTimePicker.Name = "oneTimeStartTimePicker";
			this.oneTimeStartTimePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.AssumeUtc;
			// 
			// oneTimeStartLabel
			// 
			resources.ApplyResources(this.oneTimeStartLabel, "oneTimeStartLabel");
			this.oneTimeStartLabel.Name = "oneTimeStartLabel";
			// 
			// weeklyTriggerPage
			// 
			this.weeklyTriggerPage.Controls.Add(this.weeklyStartTimePicker);
			this.weeklyTriggerPage.Controls.Add(this.label4);
			this.weeklyTriggerPage.Controls.Add(this.tableLayoutPanel1);
			this.weeklyTriggerPage.Controls.Add(this.weeklyRecurNumUpDn);
			this.weeklyTriggerPage.Controls.Add(this.weeklyOnWeeksLabel);
			this.weeklyTriggerPage.Controls.Add(this.weeklyRecurLabel);
			this.weeklyTriggerPage.Name = "weeklyTriggerPage";
			this.weeklyTriggerPage.NextPage = this.actionSelectPage;
			resources.ApplyResources(this.weeklyTriggerPage, "weeklyTriggerPage");
			this.weeklyTriggerPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.weeklyTriggerPage_Commit);
			this.weeklyTriggerPage.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.weeklyTriggerPage_Initialize);
			// 
			// weeklyStartTimePicker
			// 
			resources.ApplyResources(this.weeklyStartTimePicker, "weeklyStartTimePicker");
			this.weeklyStartTimePicker.Name = "weeklyStartTimePicker";
			this.weeklyStartTimePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.AssumeUtc;
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.weeklySunCheck, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyMonCheck, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyTueCheck, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyWedCheck, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.weeklyThuCheck, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklyFriCheck, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.weeklySatCheck, 2, 1);
			this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(420, 0);
			this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(374, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// weeklySunCheck
			// 
			resources.ApplyResources(this.weeklySunCheck, "weeklySunCheck");
			this.weeklySunCheck.Checked = true;
			this.weeklySunCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.weeklySunCheck.Name = "weeklySunCheck";
			this.weeklySunCheck.UseVisualStyleBackColor = true;
			this.weeklySunCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyMonCheck
			// 
			resources.ApplyResources(this.weeklyMonCheck, "weeklyMonCheck");
			this.weeklyMonCheck.Name = "weeklyMonCheck";
			this.weeklyMonCheck.UseVisualStyleBackColor = true;
			this.weeklyMonCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyTueCheck
			// 
			resources.ApplyResources(this.weeklyTueCheck, "weeklyTueCheck");
			this.weeklyTueCheck.Name = "weeklyTueCheck";
			this.weeklyTueCheck.UseVisualStyleBackColor = true;
			this.weeklyTueCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyWedCheck
			// 
			resources.ApplyResources(this.weeklyWedCheck, "weeklyWedCheck");
			this.weeklyWedCheck.Name = "weeklyWedCheck";
			this.weeklyWedCheck.UseVisualStyleBackColor = true;
			this.weeklyWedCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyThuCheck
			// 
			resources.ApplyResources(this.weeklyThuCheck, "weeklyThuCheck");
			this.weeklyThuCheck.Name = "weeklyThuCheck";
			this.weeklyThuCheck.UseVisualStyleBackColor = true;
			this.weeklyThuCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyFriCheck
			// 
			resources.ApplyResources(this.weeklyFriCheck, "weeklyFriCheck");
			this.weeklyFriCheck.Name = "weeklyFriCheck";
			this.weeklyFriCheck.UseVisualStyleBackColor = true;
			this.weeklyFriCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklySatCheck
			// 
			resources.ApplyResources(this.weeklySatCheck, "weeklySatCheck");
			this.weeklySatCheck.Name = "weeklySatCheck";
			this.weeklySatCheck.UseVisualStyleBackColor = true;
			this.weeklySatCheck.CheckedChanged += new System.EventHandler(this.weeklyCheck_CheckedChanged);
			// 
			// weeklyRecurNumUpDn
			// 
			resources.ApplyResources(this.weeklyRecurNumUpDn, "weeklyRecurNumUpDn");
			this.weeklyRecurNumUpDn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.weeklyRecurNumUpDn.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.weeklyRecurNumUpDn.Name = "weeklyRecurNumUpDn";
			this.weeklyRecurNumUpDn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// weeklyOnWeeksLabel
			// 
			resources.ApplyResources(this.weeklyOnWeeksLabel, "weeklyOnWeeksLabel");
			this.weeklyOnWeeksLabel.Name = "weeklyOnWeeksLabel";
			// 
			// weeklyRecurLabel
			// 
			resources.ApplyResources(this.weeklyRecurLabel, "weeklyRecurLabel");
			this.weeklyRecurLabel.Name = "weeklyRecurLabel";
			// 
			// monthlyTriggerPage
			// 
			this.monthlyTriggerPage.Controls.Add(this.monthlyDaysDropDown);
			this.monthlyTriggerPage.Controls.Add(this.monthlyOnDOWDropDown);
			this.monthlyTriggerPage.Controls.Add(this.monthlyOnWeekDropDown);
			this.monthlyTriggerPage.Controls.Add(this.monthlyMonthsDropDown);
			this.monthlyTriggerPage.Controls.Add(this.monthlyOnRadio);
			this.monthlyTriggerPage.Controls.Add(this.monthlyDaysRadio);
			this.monthlyTriggerPage.Controls.Add(this.monthlyMonthsLabel);
			this.monthlyTriggerPage.Controls.Add(this.monthlyStartTimePicker);
			this.monthlyTriggerPage.Controls.Add(this.label7);
			this.monthlyTriggerPage.Name = "monthlyTriggerPage";
			this.monthlyTriggerPage.NextPage = this.actionSelectPage;
			resources.ApplyResources(this.monthlyTriggerPage, "monthlyTriggerPage");
			this.monthlyTriggerPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.monthlyTriggerPage_Commit);
			this.monthlyTriggerPage.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.monthlyTriggerPage_Initialize);
			// 
			// monthlyDaysDropDown
			// 
			this.monthlyDaysDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyDaysDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyDaysDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.monthlyDaysDropDown, "monthlyDaysDropDown");
			this.monthlyDaysDropDown.Name = "monthlyDaysDropDown";
			// 
			// monthlyOnDOWDropDown
			// 
			this.monthlyOnDOWDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnDOWDropDown.CheckAllText = "<Select all days>";
			this.monthlyOnDOWDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnDOWDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.monthlyOnDOWDropDown, "monthlyOnDOWDropDown");
			this.monthlyOnDOWDropDown.Name = "monthlyOnDOWDropDown";
			// 
			// monthlyOnWeekDropDown
			// 
			this.monthlyOnWeekDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyOnWeekDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyOnWeekDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.monthlyOnWeekDropDown, "monthlyOnWeekDropDown");
			this.monthlyOnWeekDropDown.Name = "monthlyOnWeekDropDown";
			// 
			// monthlyMonthsDropDown
			// 
			this.monthlyMonthsDropDown.BackColor = System.Drawing.Color.White;
			this.monthlyMonthsDropDown.CheckAllText = "<Select all months>";
			this.monthlyMonthsDropDown.ControlSize = new System.Drawing.Size(187, 105);
			this.monthlyMonthsDropDown.DropSize = new System.Drawing.Size(121, 106);
			resources.ApplyResources(this.monthlyMonthsDropDown, "monthlyMonthsDropDown");
			this.monthlyMonthsDropDown.Name = "monthlyMonthsDropDown";
			// 
			// monthlyOnRadio
			// 
			resources.ApplyResources(this.monthlyOnRadio, "monthlyOnRadio");
			this.monthlyOnRadio.Name = "monthlyOnRadio";
			this.monthlyOnRadio.UseVisualStyleBackColor = true;
			this.monthlyOnRadio.CheckedChanged += new System.EventHandler(this.monthlyDaysRadio_CheckedChanged);
			// 
			// monthlyDaysRadio
			// 
			resources.ApplyResources(this.monthlyDaysRadio, "monthlyDaysRadio");
			this.monthlyDaysRadio.Name = "monthlyDaysRadio";
			this.monthlyDaysRadio.UseVisualStyleBackColor = true;
			this.monthlyDaysRadio.CheckedChanged += new System.EventHandler(this.monthlyDaysRadio_CheckedChanged);
			// 
			// monthlyMonthsLabel
			// 
			resources.ApplyResources(this.monthlyMonthsLabel, "monthlyMonthsLabel");
			this.monthlyMonthsLabel.Name = "monthlyMonthsLabel";
			// 
			// monthlyStartTimePicker
			// 
			resources.ApplyResources(this.monthlyStartTimePicker, "monthlyStartTimePicker");
			this.monthlyStartTimePicker.Name = "monthlyStartTimePicker";
			this.monthlyStartTimePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.AssumeUtc;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// onEventTriggerPage
			// 
			this.onEventTriggerPage.Controls.Add(this.onEventLogLabel);
			this.onEventTriggerPage.Controls.Add(this.onEventIdText);
			this.onEventTriggerPage.Controls.Add(this.onEventSourceLabel);
			this.onEventTriggerPage.Controls.Add(this.onEventSourceCombo);
			this.onEventTriggerPage.Controls.Add(this.onEventLogCombo);
			this.onEventTriggerPage.Controls.Add(this.onEventIdLabel);
			this.onEventTriggerPage.Name = "onEventTriggerPage";
			this.onEventTriggerPage.NextPage = this.actionSelectPage;
			resources.ApplyResources(this.onEventTriggerPage, "onEventTriggerPage");
			this.onEventTriggerPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.onEventTriggerPage_Commit);
			this.onEventTriggerPage.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.onEventTriggerPage_Initialize);
			// 
			// onEventLogLabel
			// 
			resources.ApplyResources(this.onEventLogLabel, "onEventLogLabel");
			this.onEventLogLabel.Name = "onEventLogLabel";
			// 
			// onEventIdText
			// 
			resources.ApplyResources(this.onEventIdText, "onEventIdText");
			this.onEventIdText.Name = "onEventIdText";
			// 
			// onEventSourceLabel
			// 
			resources.ApplyResources(this.onEventSourceLabel, "onEventSourceLabel");
			this.onEventSourceLabel.Name = "onEventSourceLabel";
			// 
			// onEventSourceCombo
			// 
			resources.ApplyResources(this.onEventSourceCombo, "onEventSourceCombo");
			this.onEventSourceCombo.MaximumSize = new System.Drawing.Size(320, 0);
			this.onEventSourceCombo.Name = "onEventSourceCombo";
			// 
			// onEventLogCombo
			// 
			resources.ApplyResources(this.onEventLogCombo, "onEventLogCombo");
			this.onEventLogCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.onEventLogCombo.FormattingEnabled = true;
			this.onEventLogCombo.MaximumSize = new System.Drawing.Size(320, 0);
			this.onEventLogCombo.Name = "onEventLogCombo";
			this.onEventLogCombo.SelectedIndexChanged += new System.EventHandler(this.onEventLogCombo_SelectedIndexChanged);
			// 
			// onEventIdLabel
			// 
			resources.ApplyResources(this.onEventIdLabel, "onEventIdLabel");
			this.onEventIdLabel.Name = "onEventIdLabel";
			// 
			// runActionPage
			// 
			this.runActionPage.Controls.Add(this.execProgBrowseBtn);
			this.runActionPage.Controls.Add(this.execDirText);
			this.runActionPage.Controls.Add(this.execArgText);
			this.runActionPage.Controls.Add(this.execProgText);
			this.runActionPage.Controls.Add(this.execDirLabel);
			this.runActionPage.Controls.Add(this.execArgLabel);
			this.runActionPage.Controls.Add(this.execProgLabel);
			this.runActionPage.Name = "runActionPage";
			this.runActionPage.NextPage = this.summaryPage;
			resources.ApplyResources(this.runActionPage, "runActionPage");
			this.runActionPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.runActionPage_Commit);
			// 
			// execProgBrowseBtn
			// 
			resources.ApplyResources(this.execProgBrowseBtn, "execProgBrowseBtn");
			this.execProgBrowseBtn.Name = "execProgBrowseBtn";
			this.execProgBrowseBtn.UseVisualStyleBackColor = true;
			this.execProgBrowseBtn.Click += new System.EventHandler(this.execProgBrowseBtn_Click);
			// 
			// execDirText
			// 
			resources.ApplyResources(this.execDirText, "execDirText");
			this.execDirText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execDirText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.execDirText.Name = "execDirText";
			// 
			// execArgText
			// 
			resources.ApplyResources(this.execArgText, "execArgText");
			this.execArgText.Name = "execArgText";
			// 
			// execProgText
			// 
			resources.ApplyResources(this.execProgText, "execProgText");
			this.execProgText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execProgText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.execProgText.Name = "execProgText";
			// 
			// execDirLabel
			// 
			resources.ApplyResources(this.execDirLabel, "execDirLabel");
			this.execDirLabel.Name = "execDirLabel";
			// 
			// execArgLabel
			// 
			resources.ApplyResources(this.execArgLabel, "execArgLabel");
			this.execArgLabel.Name = "execArgLabel";
			// 
			// execProgLabel
			// 
			resources.ApplyResources(this.execProgLabel, "execProgLabel");
			this.execProgLabel.Name = "execProgLabel";
			// 
			// summaryPage
			// 
			this.summaryPage.Controls.Add(this.sumText);
			this.summaryPage.Controls.Add(this.openDlgAfterCheck);
			this.summaryPage.Controls.Add(this.summaryPrompt);
			this.summaryPage.IsFinishPage = true;
			this.summaryPage.Name = "summaryPage";
			resources.ApplyResources(this.summaryPage, "summaryPage");
			this.summaryPage.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.summaryPage_Initialize);
			// 
			// sumText
			// 
			resources.ApplyResources(this.sumText, "sumText");
			this.sumText.Name = "sumText";
			this.sumText.ReadOnly = true;
			// 
			// openDlgAfterCheck
			// 
			resources.ApplyResources(this.openDlgAfterCheck, "openDlgAfterCheck");
			this.openDlgAfterCheck.Name = "openDlgAfterCheck";
			this.openDlgAfterCheck.UseVisualStyleBackColor = true;
			// 
			// summaryPrompt
			// 
			resources.ApplyResources(this.summaryPrompt, "summaryPrompt");
			this.summaryPrompt.Name = "summaryPrompt";
			// 
			// emailActionPage
			// 
			this.emailActionPage.Controls.Add(this.emailAttachementBrowseBtn);
			this.emailActionPage.Controls.Add(this.emailSMTPText);
			this.emailActionPage.Controls.Add(this.emailSMTPLabel);
			this.emailActionPage.Controls.Add(this.emailAttachmentText);
			this.emailActionPage.Controls.Add(this.emailAttachmentLabel);
			this.emailActionPage.Controls.Add(this.groupBox3);
			this.emailActionPage.Controls.Add(this.emailTextText);
			this.emailActionPage.Controls.Add(this.emailTextLabel);
			this.emailActionPage.Controls.Add(this.emailSubjectText);
			this.emailActionPage.Controls.Add(this.emailSubjectLabel);
			this.emailActionPage.Controls.Add(this.emailToText);
			this.emailActionPage.Controls.Add(this.emailToLabel);
			this.emailActionPage.Controls.Add(this.emailFromText);
			this.emailActionPage.Controls.Add(this.emailFromLabel);
			this.emailActionPage.Name = "emailActionPage";
			this.emailActionPage.NextPage = this.summaryPage;
			resources.ApplyResources(this.emailActionPage, "emailActionPage");
			this.emailActionPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.emailActionPage_Commit);
			// 
			// emailAttachementBrowseBtn
			// 
			resources.ApplyResources(this.emailAttachementBrowseBtn, "emailAttachementBrowseBtn");
			this.emailAttachementBrowseBtn.Name = "emailAttachementBrowseBtn";
			this.emailAttachementBrowseBtn.UseVisualStyleBackColor = true;
			this.emailAttachementBrowseBtn.Click += new System.EventHandler(this.emailAttachementBrowseBtn_Click);
			// 
			// emailSMTPText
			// 
			resources.ApplyResources(this.emailSMTPText, "emailSMTPText");
			this.emailSMTPText.Name = "emailSMTPText";
			// 
			// emailSMTPLabel
			// 
			resources.ApplyResources(this.emailSMTPLabel, "emailSMTPLabel");
			this.emailSMTPLabel.Name = "emailSMTPLabel";
			// 
			// emailAttachmentText
			// 
			resources.ApplyResources(this.emailAttachmentText, "emailAttachmentText");
			this.emailAttachmentText.Name = "emailAttachmentText";
			// 
			// emailAttachmentLabel
			// 
			resources.ApplyResources(this.emailAttachmentLabel, "emailAttachmentLabel");
			this.emailAttachmentLabel.Name = "emailAttachmentLabel";
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// emailTextText
			// 
			resources.ApplyResources(this.emailTextText, "emailTextText");
			this.emailTextText.Name = "emailTextText";
			// 
			// emailTextLabel
			// 
			resources.ApplyResources(this.emailTextLabel, "emailTextLabel");
			this.emailTextLabel.Name = "emailTextLabel";
			// 
			// emailSubjectText
			// 
			resources.ApplyResources(this.emailSubjectText, "emailSubjectText");
			this.emailSubjectText.Name = "emailSubjectText";
			// 
			// emailSubjectLabel
			// 
			resources.ApplyResources(this.emailSubjectLabel, "emailSubjectLabel");
			this.emailSubjectLabel.Name = "emailSubjectLabel";
			// 
			// emailToText
			// 
			resources.ApplyResources(this.emailToText, "emailToText");
			this.emailToText.Name = "emailToText";
			// 
			// emailToLabel
			// 
			resources.ApplyResources(this.emailToLabel, "emailToLabel");
			this.emailToLabel.Name = "emailToLabel";
			// 
			// emailFromText
			// 
			resources.ApplyResources(this.emailFromText, "emailFromText");
			this.emailFromText.Name = "emailFromText";
			// 
			// emailFromLabel
			// 
			resources.ApplyResources(this.emailFromLabel, "emailFromLabel");
			this.emailFromLabel.Name = "emailFromLabel";
			// 
			// msgActionPage
			// 
			this.msgActionPage.Controls.Add(this.msgMsgText);
			this.msgActionPage.Controls.Add(this.msgMsgLabel);
			this.msgActionPage.Controls.Add(this.msgTitleText);
			this.msgActionPage.Controls.Add(this.msgTitleLabel);
			this.msgActionPage.Controls.Add(this.msgIntroLabel);
			this.msgActionPage.Name = "msgActionPage";
			this.msgActionPage.NextPage = this.summaryPage;
			resources.ApplyResources(this.msgActionPage, "msgActionPage");
			this.msgActionPage.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.msgActionPage_Commit);
			// 
			// msgMsgText
			// 
			resources.ApplyResources(this.msgMsgText, "msgMsgText");
			this.msgMsgText.Name = "msgMsgText";
			// 
			// msgMsgLabel
			// 
			resources.ApplyResources(this.msgMsgLabel, "msgMsgLabel");
			this.msgMsgLabel.Name = "msgMsgLabel";
			// 
			// msgTitleText
			// 
			resources.ApplyResources(this.msgTitleText, "msgTitleText");
			this.msgTitleText.Name = "msgTitleText";
			// 
			// msgTitleLabel
			// 
			resources.ApplyResources(this.msgTitleLabel, "msgTitleLabel");
			this.msgTitleLabel.Name = "msgTitleLabel";
			// 
			// msgIntroLabel
			// 
			resources.ApplyResources(this.msgIntroLabel, "msgIntroLabel");
			this.msgIntroLabel.Name = "msgIntroLabel";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
			// 
			// taskLocalOnlyCheck
			// 
			resources.ApplyResources(this.taskLocalOnlyCheck, "taskLocalOnlyCheck");
			this.taskLocalOnlyCheck.Name = "taskLocalOnlyCheck";
			this.taskLocalOnlyCheck.UseVisualStyleBackColor = true;
			this.taskLocalOnlyCheck.CheckedChanged += new System.EventHandler(this.taskLocalOnlyCheck_CheckedChanged);
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
			this.taskLoggedOnRadio.CheckedChanged += new System.EventHandler(this.taskLoggedOnRadio_CheckedChanged);
			// 
			// taskPrincipalText
			// 
			resources.ApplyResources(this.taskPrincipalText, "taskPrincipalText");
			this.taskPrincipalText.Name = "taskPrincipalText";
			this.taskPrincipalText.ReadOnly = true;
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
			// TaskSchedulerWizard
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.wizardControl1);
			this.Name = "TaskSchedulerWizard";
			this.ShowIcon = false;
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
			this.introPage.ResumeLayout(false);
			this.introPage.PerformLayout();
			this.secOptPage.ResumeLayout(false);
			this.secOptPage.PerformLayout();
			this.triggerSelectPage.ResumeLayout(false);
			this.dailyTriggerPage.ResumeLayout(false);
			this.dailyTriggerPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dailyRecurNumUpDn)).EndInit();
			this.actionSelectPage.ResumeLayout(false);
			this.actionSelectPage.PerformLayout();
			this.oneTimeTriggerPage.ResumeLayout(false);
			this.oneTimeTriggerPage.PerformLayout();
			this.weeklyTriggerPage.ResumeLayout(false);
			this.weeklyTriggerPage.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.weeklyRecurNumUpDn)).EndInit();
			this.monthlyTriggerPage.ResumeLayout(false);
			this.monthlyTriggerPage.PerformLayout();
			this.onEventTriggerPage.ResumeLayout(false);
			this.onEventTriggerPage.PerformLayout();
			this.runActionPage.ResumeLayout(false);
			this.runActionPage.PerformLayout();
			this.summaryPage.ResumeLayout(false);
			this.summaryPage.PerformLayout();
			this.emailActionPage.ResumeLayout(false);
			this.emailActionPage.PerformLayout();
			this.msgActionPage.ResumeLayout(false);
			this.msgActionPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private AeroWizard.WizardControl wizardControl1;
		private AeroWizard.WizardPage introPage;
		private System.Windows.Forms.TextBox descText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private AeroWizard.WizardPage triggerSelectPage;
		private GroupControls.RadioButtonList triggerSelectionList;
		private AeroWizard.WizardPage actionSelectPage;
		private GroupControls.RadioButtonList actionSelectionList;
		private AeroWizard.WizardPage summaryPage;
		private System.Windows.Forms.CheckBox openDlgAfterCheck;
		private System.Windows.Forms.Label summaryPrompt;
		private System.Windows.Forms.TextBox sumText;
		private AeroWizard.WizardPage dailyTriggerPage;
		private AeroWizard.WizardPage oneTimeTriggerPage;
		private AeroWizard.WizardPage weeklyTriggerPage;
		private AeroWizard.WizardPage monthlyTriggerPage;
		private AeroWizard.WizardPage onEventTriggerPage;
		private AeroWizard.WizardPage runActionPage;
		private AeroWizard.WizardPage emailActionPage;
		private AeroWizard.WizardPage msgActionPage;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label oneTimeStartLabel;
		private FullDateTimePicker oneTimeStartTimePicker;
		private System.Windows.Forms.Button execProgBrowseBtn;
		private System.Windows.Forms.TextBox execDirText;
		private System.Windows.Forms.TextBox execArgText;
		private System.Windows.Forms.TextBox execProgText;
		private System.Windows.Forms.Label execDirLabel;
		private System.Windows.Forms.Label execArgLabel;
		private System.Windows.Forms.Label execProgLabel;
		private System.Windows.Forms.Button emailAttachementBrowseBtn;
		private System.Windows.Forms.TextBox emailSMTPText;
		private System.Windows.Forms.Label emailSMTPLabel;
		private System.Windows.Forms.TextBox emailAttachmentText;
		private System.Windows.Forms.Label emailAttachmentLabel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox emailTextText;
		private System.Windows.Forms.Label emailTextLabel;
		private System.Windows.Forms.TextBox emailSubjectText;
		private System.Windows.Forms.Label emailSubjectLabel;
		private System.Windows.Forms.TextBox emailToText;
		private System.Windows.Forms.Label emailToLabel;
		private System.Windows.Forms.TextBox emailFromText;
		private System.Windows.Forms.Label emailFromLabel;
		private System.Windows.Forms.TextBox msgMsgText;
		private System.Windows.Forms.Label msgMsgLabel;
		private System.Windows.Forms.TextBox msgTitleText;
		private System.Windows.Forms.Label msgTitleLabel;
		private System.Windows.Forms.Label msgIntroLabel;
		private System.Windows.Forms.Label onEventLogLabel;
		private System.Windows.Forms.TextBox onEventIdText;
		private System.Windows.Forms.Label onEventSourceLabel;
		private System.Windows.Forms.ComboBox onEventSourceCombo;
		private System.Windows.Forms.ComboBox onEventLogCombo;
		private System.Windows.Forms.Label onEventIdLabel;
		private FullDateTimePicker dailyStartTimePicker;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown dailyRecurNumUpDn;
		private System.Windows.Forms.Label dailyDaysLabel;
		private System.Windows.Forms.Label dailyRecurLabel;
		private FullDateTimePicker weeklyStartTimePicker;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox weeklySunCheck;
		private System.Windows.Forms.CheckBox weeklyMonCheck;
		private System.Windows.Forms.CheckBox weeklyTueCheck;
		private System.Windows.Forms.CheckBox weeklyWedCheck;
		private System.Windows.Forms.CheckBox weeklyThuCheck;
		private System.Windows.Forms.CheckBox weeklyFriCheck;
		private System.Windows.Forms.CheckBox weeklySatCheck;
		private System.Windows.Forms.NumericUpDown weeklyRecurNumUpDn;
		private System.Windows.Forms.Label weeklyOnWeeksLabel;
		private System.Windows.Forms.Label weeklyRecurLabel;
		private FullDateTimePicker monthlyStartTimePicker;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton monthlyOnRadio;
		private System.Windows.Forms.RadioButton monthlyDaysRadio;
		private System.Windows.Forms.Label monthlyMonthsLabel;
		private DropDownCheckList monthlyDaysDropDown;
		private DropDownCheckList monthlyOnDOWDropDown;
		private DropDownCheckList monthlyOnWeekDropDown;
		private DropDownCheckList monthlyMonthsDropDown;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private AeroWizard.WizardPage secOptPage;
		private System.Windows.Forms.CheckBox taskLocalOnlyCheck;
		private System.Windows.Forms.RadioButton taskLoggedOptionalRadio;
		private System.Windows.Forms.RadioButton taskLoggedOnRadio;
		private System.Windows.Forms.TextBox taskPrincipalText;
		private System.Windows.Forms.Button changePrincipalButton;
		private System.Windows.Forms.Label taskUserAcctLabel;

	}
}