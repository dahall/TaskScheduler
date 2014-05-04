extern alias GrpCtrlDLL;
extern alias WizDLL;

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
			Microsoft.Win32.TaskScheduler.EventTrigger eventTrigger1 = new Microsoft.Win32.TaskScheduler.EventTrigger();
			this.wizardControl1 = new WizDLL::AeroWizard.WizardControl();
			this.introPage = new WizDLL::AeroWizard.WizardPage();
			this.nameText = new System.Windows.Forms.TextBox();
			this.descText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.triggerSelectPage = new WizDLL::AeroWizard.WizardPage();
			this.triggerSelectionList = new GrpCtrlDLL::GroupControls.RadioButtonList();
			this.dailyTriggerPage = new WizDLL::AeroWizard.WizardPage();
			this.dailyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.DailyTriggerUI();
			this.triggerPropPage = new WizDLL::AeroWizard.WizardPage();
			this.triggerPropText = new System.Windows.Forms.Label();
			this.durationSpan = new System.Windows.Forms.TimeSpanPicker();
			this.repeatSpan = new System.Windows.Forms.TimeSpanPicker();
			this.durationLabel = new System.Windows.Forms.Label();
			this.enabledCheckBox = new System.Windows.Forms.CheckBox();
			this.repeatCheckBox = new System.Windows.Forms.CheckBox();
			this.oneTimeTriggerPage = new WizDLL::AeroWizard.WizardPage();
			this.oneTimeStartTimePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.oneTimeStartLabel = new System.Windows.Forms.Label();
			this.weeklyTriggerPage = new WizDLL::AeroWizard.WizardPage();
			this.weeklyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.WeeklyTriggerUI();
			this.monthlyTriggerPage = new WizDLL::AeroWizard.WizardPage();
			this.monthlyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.MonthlyTriggerUI();
			this.onEventTriggerPage = new WizDLL::AeroWizard.WizardPage();
			this.eventTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.EventTriggerUI();
			this.actionSelectPage = new WizDLL::AeroWizard.WizardPage();
			this.actionSelectionList = new GrpCtrlDLL::GroupControls.RadioButtonList();
			this.runActionPage = new WizDLL::AeroWizard.WizardPage();
			this.execActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.ExecActionUI();
			this.secOptPage = new WizDLL::AeroWizard.WizardPage();
			this.taskLocalOnlyCheck = new System.Windows.Forms.CheckBox();
			this.taskLoggedOptionalRadio = new System.Windows.Forms.RadioButton();
			this.taskLoggedOnRadio = new System.Windows.Forms.RadioButton();
			this.taskPrincipalText = new System.Windows.Forms.TextBox();
			this.changePrincipalButton = new System.Windows.Forms.Button();
			this.taskUserAcctLabel = new System.Windows.Forms.Label();
			this.emailActionPage = new WizDLL::AeroWizard.WizardPage();
			this.emailActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.EmailActionUI();
			this.msgActionPage = new WizDLL::AeroWizard.WizardPage();
			this.showMessageActionUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.ShowMessageActionUI();
			this.summaryPage = new WizDLL::AeroWizard.WizardPage();
			this.sumText = new System.Windows.Forms.TextBox();
			this.openDlgAfterCheck = new System.Windows.Forms.CheckBox();
			this.summaryPrompt = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
			this.introPage.SuspendLayout();
			this.triggerSelectPage.SuspendLayout();
			this.triggerSelectionList.SuspendLayout();
			this.dailyTriggerPage.SuspendLayout();
			this.triggerPropPage.SuspendLayout();
			this.oneTimeTriggerPage.SuspendLayout();
			this.weeklyTriggerPage.SuspendLayout();
			this.monthlyTriggerPage.SuspendLayout();
			this.onEventTriggerPage.SuspendLayout();
			this.actionSelectPage.SuspendLayout();
			this.actionSelectionList.SuspendLayout();
			this.runActionPage.SuspendLayout();
			this.secOptPage.SuspendLayout();
			this.emailActionPage.SuspendLayout();
			this.msgActionPage.SuspendLayout();
			this.summaryPage.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardControl1
			// 
			resources.ApplyResources(this.wizardControl1, "wizardControl1");
			this.wizardControl1.Name = "wizardControl1";
			this.wizardControl1.Pages.Add(this.introPage);
			this.wizardControl1.Pages.Add(this.triggerSelectPage);
			this.wizardControl1.Pages.Add(this.dailyTriggerPage);
			this.wizardControl1.Pages.Add(this.oneTimeTriggerPage);
			this.wizardControl1.Pages.Add(this.weeklyTriggerPage);
			this.wizardControl1.Pages.Add(this.monthlyTriggerPage);
			this.wizardControl1.Pages.Add(this.onEventTriggerPage);
			this.wizardControl1.Pages.Add(this.triggerPropPage);
			this.wizardControl1.Pages.Add(this.actionSelectPage);
			this.wizardControl1.Pages.Add(this.runActionPage);
			this.wizardControl1.Pages.Add(this.emailActionPage);
			this.wizardControl1.Pages.Add(this.msgActionPage);
			this.wizardControl1.Pages.Add(this.secOptPage);
			this.wizardControl1.Pages.Add(this.summaryPage);
			this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
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
			// triggerSelectPage
			// 
			this.triggerSelectPage.AllowNext = false;
			this.triggerSelectPage.Controls.Add(this.triggerSelectionList);
			this.triggerSelectPage.Name = "triggerSelectPage";
			resources.ApplyResources(this.triggerSelectPage, "triggerSelectPage");
			this.triggerSelectPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.triggerSelectPage_Commit);
			this.triggerSelectPage.Initialize += new System.EventHandler<WizDLL::AeroWizard.WizardPageInitEventArgs>(this.triggerSelectPage_Initialize);
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
			this.dailyTriggerPage.Controls.Add(this.dailyTriggerUI1);
			this.dailyTriggerPage.Name = "dailyTriggerPage";
			this.dailyTriggerPage.NextPage = this.oneTimeTriggerPage;
			resources.ApplyResources(this.dailyTriggerPage, "dailyTriggerPage");
			// 
			// dailyTriggerUI1
			// 
			resources.ApplyResources(this.dailyTriggerUI1, "dailyTriggerUI1");
			this.dailyTriggerUI1.Name = "dailyTriggerUI1";
			// 
			// triggerPropPage
			// 
			this.triggerPropPage.Controls.Add(this.triggerPropText);
			this.triggerPropPage.Controls.Add(this.durationSpan);
			this.triggerPropPage.Controls.Add(this.repeatSpan);
			this.triggerPropPage.Controls.Add(this.durationLabel);
			this.triggerPropPage.Controls.Add(this.enabledCheckBox);
			this.triggerPropPage.Controls.Add(this.repeatCheckBox);
			this.triggerPropPage.Name = "triggerPropPage";
			resources.ApplyResources(this.triggerPropPage, "triggerPropPage");
			this.triggerPropPage.Initialize += new System.EventHandler<WizDLL::AeroWizard.WizardPageInitEventArgs>(this.triggerPropPage_Initialize);
			// 
			// triggerPropText
			// 
			resources.ApplyResources(this.triggerPropText, "triggerPropText");
			this.triggerPropText.Name = "triggerPropText";
			// 
			// durationSpan
			// 
			resources.ApplyResources(this.durationSpan, "durationSpan");
			this.durationSpan.Name = "durationSpan";
			this.durationSpan.ValueChanged += new System.EventHandler(this.durationSpan_ValueChanged);
			// 
			// repeatSpan
			// 
			resources.ApplyResources(this.repeatSpan, "repeatSpan");
			this.repeatSpan.Name = "repeatSpan";
			this.repeatSpan.ValueChanged += new System.EventHandler(this.repeatSpan_ValueChanged);
			// 
			// durationLabel
			// 
			resources.ApplyResources(this.durationLabel, "durationLabel");
			this.durationLabel.Name = "durationLabel";
			// 
			// enabledCheckBox
			// 
			resources.ApplyResources(this.enabledCheckBox, "enabledCheckBox");
			this.enabledCheckBox.Name = "enabledCheckBox";
			this.enabledCheckBox.UseVisualStyleBackColor = true;
			this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
			// 
			// repeatCheckBox
			// 
			resources.ApplyResources(this.repeatCheckBox, "repeatCheckBox");
			this.repeatCheckBox.Name = "repeatCheckBox";
			this.repeatCheckBox.UseVisualStyleBackColor = true;
			this.repeatCheckBox.CheckedChanged += new System.EventHandler(this.repeatCheckBox_CheckedChanged);
			// 
			// oneTimeTriggerPage
			// 
			this.oneTimeTriggerPage.Controls.Add(this.oneTimeStartTimePicker);
			this.oneTimeTriggerPage.Controls.Add(this.oneTimeStartLabel);
			this.oneTimeTriggerPage.Name = "oneTimeTriggerPage";
			this.oneTimeTriggerPage.NextPage = this.weeklyTriggerPage;
			resources.ApplyResources(this.oneTimeTriggerPage, "oneTimeTriggerPage");
			this.oneTimeTriggerPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.oneTimeTriggerPage_Commit);
			// 
			// oneTimeStartTimePicker
			// 
			resources.ApplyResources(this.oneTimeStartTimePicker, "oneTimeStartTimePicker");
			this.oneTimeStartTimePicker.Name = "oneTimeStartTimePicker";
			this.oneTimeStartTimePicker.UtcCheckBehavior = Microsoft.Win32.TaskScheduler.FullDateTimePicker.FieldConversionUtcCheckBehavior.AssumeLocal;
			// 
			// oneTimeStartLabel
			// 
			resources.ApplyResources(this.oneTimeStartLabel, "oneTimeStartLabel");
			this.oneTimeStartLabel.Name = "oneTimeStartLabel";
			// 
			// weeklyTriggerPage
			// 
			this.weeklyTriggerPage.Controls.Add(this.weeklyTriggerUI1);
			this.weeklyTriggerPage.Name = "weeklyTriggerPage";
			this.weeklyTriggerPage.NextPage = this.monthlyTriggerPage;
			resources.ApplyResources(this.weeklyTriggerPage, "weeklyTriggerPage");
			// 
			// weeklyTriggerUI1
			// 
			resources.ApplyResources(this.weeklyTriggerUI1, "weeklyTriggerUI1");
			this.weeklyTriggerUI1.Name = "weeklyTriggerUI1";
			// 
			// monthlyTriggerPage
			// 
			this.monthlyTriggerPage.Controls.Add(this.monthlyTriggerUI1);
			this.monthlyTriggerPage.Name = "monthlyTriggerPage";
			this.monthlyTriggerPage.NextPage = this.onEventTriggerPage;
			resources.ApplyResources(this.monthlyTriggerPage, "monthlyTriggerPage");
			// 
			// monthlyTriggerUI1
			// 
			resources.ApplyResources(this.monthlyTriggerUI1, "monthlyTriggerUI1");
			this.monthlyTriggerUI1.Name = "monthlyTriggerUI1";
			this.monthlyTriggerUI1.TriggerTypeChanged += new System.EventHandler(this.monthlyTriggerUI1_TriggerTypeChanged);
			// 
			// onEventTriggerPage
			// 
			this.onEventTriggerPage.AllowNext = false;
			this.onEventTriggerPage.Controls.Add(this.eventTriggerUI1);
			this.onEventTriggerPage.Name = "onEventTriggerPage";
			this.onEventTriggerPage.NextPage = this.triggerPropPage;
			resources.ApplyResources(this.onEventTriggerPage, "onEventTriggerPage");
			this.onEventTriggerPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.onEventTriggerPage_Commit);
			this.onEventTriggerPage.Initialize += new System.EventHandler<WizDLL::AeroWizard.WizardPageInitEventArgs>(this.onEventTriggerPage_Initialize);
			// 
			// eventTriggerUI1
			// 
			resources.ApplyResources(this.eventTriggerUI1, "eventTriggerUI1");
			this.eventTriggerUI1.Name = "eventTriggerUI1";
			this.eventTriggerUI1.TriggerChanged += new System.ComponentModel.PropertyChangedEventHandler(this.eventTriggerUI1_TriggerChanged);
			// 
			// actionSelectPage
			// 
			this.actionSelectPage.AllowNext = false;
			this.actionSelectPage.Controls.Add(this.actionSelectionList);
			this.actionSelectPage.Name = "actionSelectPage";
			resources.ApplyResources(this.actionSelectPage, "actionSelectPage");
			this.actionSelectPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.actionSelectPage_Commit);
			// 
			// actionSelectionList
			// 
			resources.ApplyResources(this.actionSelectionList, "actionSelectionList");
			this.actionSelectionList.Name = "actionSelectionList";
			this.actionSelectionList.SubtextForeColor = System.Drawing.SystemColors.GrayText;
			this.actionSelectionList.SelectedIndexChanged += new System.EventHandler(this.actionSelectionList_SelectedIndexChanged);
			// 
			// runActionPage
			// 
			this.runActionPage.AllowNext = false;
			this.runActionPage.Controls.Add(this.execActionUI1);
			this.runActionPage.Name = "runActionPage";
			this.runActionPage.NextPage = this.emailActionPage;
			resources.ApplyResources(this.runActionPage, "runActionPage");
			this.runActionPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.runActionPage_Commit);
			// 
			// execActionUI1
			// 
			resources.ApplyResources(this.execActionUI1, "execActionUI1");
			this.execActionUI1.Name = "execActionUI1";
			this.execActionUI1.KeyValueChanged += new System.EventHandler(this.execActionUI1_KeyValueChanged);
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
			this.secOptPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.secOptPage_Commit);
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
			// emailActionPage
			// 
			this.emailActionPage.AllowNext = false;
			this.emailActionPage.Controls.Add(this.emailActionUI1);
			this.emailActionPage.Name = "emailActionPage";
			this.emailActionPage.NextPage = this.msgActionPage;
			resources.ApplyResources(this.emailActionPage, "emailActionPage");
			this.emailActionPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.emailActionPage_Commit);
			// 
			// emailActionUI1
			// 
			resources.ApplyResources(this.emailActionUI1, "emailActionUI1");
			this.emailActionUI1.Name = "emailActionUI1";
			this.emailActionUI1.KeyValueChanged += new System.EventHandler(this.emailActionUI1_KeyValueChanged);
			// 
			// msgActionPage
			// 
			this.msgActionPage.AllowNext = false;
			this.msgActionPage.Controls.Add(this.showMessageActionUI1);
			this.msgActionPage.Name = "msgActionPage";
			this.msgActionPage.NextPage = this.secOptPage;
			resources.ApplyResources(this.msgActionPage, "msgActionPage");
			this.msgActionPage.Commit += new System.EventHandler<WizDLL::AeroWizard.WizardPageConfirmEventArgs>(this.msgActionPage_Commit);
			// 
			// showMessageActionUI1
			// 
			resources.ApplyResources(this.showMessageActionUI1, "showMessageActionUI1");
			this.showMessageActionUI1.Name = "showMessageActionUI1";
			this.showMessageActionUI1.KeyValueChanged += new System.EventHandler(this.showMessageActionUI1_KeyValueChanged);
			// 
			// summaryPage
			// 
			this.summaryPage.Controls.Add(this.sumText);
			this.summaryPage.Controls.Add(this.openDlgAfterCheck);
			this.summaryPage.Controls.Add(this.summaryPrompt);
			this.summaryPage.IsFinishPage = true;
			this.summaryPage.Name = "summaryPage";
			resources.ApplyResources(this.summaryPage, "summaryPage");
			this.summaryPage.Initialize += new System.EventHandler<WizDLL::AeroWizard.WizardPageInitEventArgs>(this.summaryPage_Initialize);
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
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
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
			this.triggerSelectPage.ResumeLayout(false);
			this.triggerSelectionList.ResumeLayout(true);
			this.dailyTriggerPage.ResumeLayout(false);
			this.triggerPropPage.ResumeLayout(false);
			this.triggerPropPage.PerformLayout();
			this.oneTimeTriggerPage.ResumeLayout(false);
			this.oneTimeTriggerPage.PerformLayout();
			this.weeklyTriggerPage.ResumeLayout(false);
			this.monthlyTriggerPage.ResumeLayout(false);
			this.onEventTriggerPage.ResumeLayout(false);
			this.actionSelectPage.ResumeLayout(false);
			this.actionSelectPage.PerformLayout();
			this.actionSelectionList.ResumeLayout(true);
			this.runActionPage.ResumeLayout(false);
			this.secOptPage.ResumeLayout(false);
			this.secOptPage.PerformLayout();
			this.emailActionPage.ResumeLayout(false);
			this.msgActionPage.ResumeLayout(false);
			this.summaryPage.ResumeLayout(false);
			this.summaryPage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private WizDLL::AeroWizard.WizardControl wizardControl1;
		private WizDLL::AeroWizard.WizardPage introPage;
		private System.Windows.Forms.TextBox descText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private WizDLL::AeroWizard.WizardPage triggerSelectPage;
		private GrpCtrlDLL::GroupControls.RadioButtonList triggerSelectionList;
		private WizDLL::AeroWizard.WizardPage actionSelectPage;
		private GrpCtrlDLL::GroupControls.RadioButtonList actionSelectionList;
		private WizDLL::AeroWizard.WizardPage summaryPage;
		private System.Windows.Forms.CheckBox openDlgAfterCheck;
		private System.Windows.Forms.Label summaryPrompt;
		private System.Windows.Forms.TextBox sumText;
		private WizDLL::AeroWizard.WizardPage dailyTriggerPage;
		private WizDLL::AeroWizard.WizardPage oneTimeTriggerPage;
		private WizDLL::AeroWizard.WizardPage weeklyTriggerPage;
		private WizDLL::AeroWizard.WizardPage monthlyTriggerPage;
		private WizDLL::AeroWizard.WizardPage onEventTriggerPage;
		private WizDLL::AeroWizard.WizardPage runActionPage;
		private WizDLL::AeroWizard.WizardPage emailActionPage;
		private WizDLL::AeroWizard.WizardPage msgActionPage;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label oneTimeStartLabel;
		private FullDateTimePicker oneTimeStartTimePicker;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private WizDLL::AeroWizard.WizardPage secOptPage;
		private System.Windows.Forms.CheckBox taskLocalOnlyCheck;
		private System.Windows.Forms.RadioButton taskLoggedOptionalRadio;
		private System.Windows.Forms.RadioButton taskLoggedOnRadio;
		private System.Windows.Forms.TextBox taskPrincipalText;
		private System.Windows.Forms.Button changePrincipalButton;
		private System.Windows.Forms.Label taskUserAcctLabel;
		private WizDLL::AeroWizard.WizardPage triggerPropPage;
		private System.Windows.Forms.TimeSpanPicker durationSpan;
		private System.Windows.Forms.TimeSpanPicker repeatSpan;
		private System.Windows.Forms.Label durationLabel;
		private System.Windows.Forms.CheckBox enabledCheckBox;
		private System.Windows.Forms.CheckBox repeatCheckBox;
		private System.Windows.Forms.Label triggerPropText;
		private UIComponents.ExecActionUI execActionUI1;
		private UIComponents.EmailActionUI emailActionUI1;
		private UIComponents.ShowMessageActionUI showMessageActionUI1;
		private UIComponents.DailyTriggerUI dailyTriggerUI1;
		private UIComponents.WeeklyTriggerUI weeklyTriggerUI1;
		private UIComponents.MonthlyTriggerUI monthlyTriggerUI1;
		private UIComponents.EventTriggerUI eventTriggerUI1;

	}
}