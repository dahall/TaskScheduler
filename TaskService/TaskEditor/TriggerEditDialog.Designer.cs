namespace Microsoft.Win32.TaskScheduler
{
	public partial class TriggerEditDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerEditDialog));
			this.triggerTypeLabel = new System.Windows.Forms.Label();
			this.triggerTypeCombo = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.settingsTabControl = new System.Windows.Forms.TabControl();
			this.scheduleTab = new System.Windows.Forms.TabPage();
			this.schedStartDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.schedGroup = new System.Windows.Forms.GroupBox();
			this.schedTabControl = new System.Windows.Forms.TabControl();
			this.oneTimeTab = new System.Windows.Forms.TabPage();
			this.dailyTab = new System.Windows.Forms.TabPage();
			this.dailyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.DailyTriggerUI();
			this.weeklyTab = new System.Windows.Forms.TabPage();
			this.weeklyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.WeeklyTriggerUI();
			this.monthlyTab = new System.Windows.Forms.TabPage();
			this.monthlyTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.MonthlyTriggerUI();
			this.schedStartLabel = new System.Windows.Forms.Label();
			this.schedMonthlyRadio = new System.Windows.Forms.RadioButton();
			this.schedWeeklyRadio = new System.Windows.Forms.RadioButton();
			this.schedDailyRadio = new System.Windows.Forms.RadioButton();
			this.schedOneRadio = new System.Windows.Forms.RadioButton();
			this.logonTab = new System.Windows.Forms.TabPage();
			this.logonRemotePanel = new System.Windows.Forms.Panel();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.logonLocalRadio = new System.Windows.Forms.RadioButton();
			this.logonRemoteRadio = new System.Windows.Forms.RadioButton();
			this.logonChgUserBtn = new System.Windows.Forms.Button();
			this.logonUserLabel = new System.Windows.Forms.Label();
			this.logonSpecUserRadio = new System.Windows.Forms.RadioButton();
			this.logonAnyUserRadio = new System.Windows.Forms.RadioButton();
			this.startupTab = new System.Windows.Forms.TabPage();
			this.startupIntroLabel = new System.Windows.Forms.Label();
			this.idleTab = new System.Windows.Forms.TabPage();
			this.label8 = new System.Windows.Forms.Label();
			this.onEventTab = new System.Windows.Forms.TabPage();
			this.eventTriggerUI1 = new Microsoft.Win32.TaskScheduler.UIComponents.EventTriggerUI();
			this.advSettingsGroup = new System.Windows.Forms.GroupBox();
			this.stopIfRunsSpan = new System.Windows.Forms.TimeSpanPicker();
			this.durationSpan = new System.Windows.Forms.TimeSpanPicker();
			this.repeatSpan = new System.Windows.Forms.TimeSpanPicker();
			this.delaySpan = new System.Windows.Forms.TimeSpanPicker();
			this.expireDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.activateDatePicker = new Microsoft.Win32.TaskScheduler.FullDateTimePicker();
			this.durationLabel = new System.Windows.Forms.Label();
			this.stopAfterDurationCheckBox = new System.Windows.Forms.CheckBox();
			this.enabledCheckBox = new System.Windows.Forms.CheckBox();
			this.expireCheckBox = new System.Windows.Forms.CheckBox();
			this.activateCheckBox = new System.Windows.Forms.CheckBox();
			this.stopIfRunsCheckBox = new System.Windows.Forms.CheckBox();
			this.repeatCheckBox = new System.Windows.Forms.CheckBox();
			this.delayCheckBox = new System.Windows.Forms.CheckBox();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.settingsTabControl.SuspendLayout();
			this.scheduleTab.SuspendLayout();
			this.schedGroup.SuspendLayout();
			this.schedTabControl.SuspendLayout();
			this.dailyTab.SuspendLayout();
			this.weeklyTab.SuspendLayout();
			this.monthlyTab.SuspendLayout();
			this.logonTab.SuspendLayout();
			this.logonRemotePanel.SuspendLayout();
			this.startupTab.SuspendLayout();
			this.idleTab.SuspendLayout();
			this.onEventTab.SuspendLayout();
			this.advSettingsGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// triggerTypeLabel
			// 
			resources.ApplyResources(this.triggerTypeLabel, "triggerTypeLabel");
			this.triggerTypeLabel.Name = "triggerTypeLabel";
			// 
			// triggerTypeCombo
			// 
			this.triggerTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.triggerTypeCombo.FormattingEnabled = true;
			resources.ApplyResources(this.triggerTypeCombo, "triggerTypeCombo");
			this.triggerTypeCombo.Name = "triggerTypeCombo";
			this.triggerTypeCombo.SelectedValueChanged += new System.EventHandler(this.triggerTypeCombo_SelectedValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.settingsTabControl);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// settingsTabControl
			// 
			resources.ApplyResources(this.settingsTabControl, "settingsTabControl");
			this.settingsTabControl.Controls.Add(this.scheduleTab);
			this.settingsTabControl.Controls.Add(this.logonTab);
			this.settingsTabControl.Controls.Add(this.startupTab);
			this.settingsTabControl.Controls.Add(this.idleTab);
			this.settingsTabControl.Controls.Add(this.onEventTab);
			this.settingsTabControl.Name = "settingsTabControl";
			this.settingsTabControl.SelectedIndex = 0;
			this.settingsTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.settingsTabControl.TabStop = false;
			// 
			// scheduleTab
			// 
			this.scheduleTab.Controls.Add(this.schedStartDatePicker);
			this.scheduleTab.Controls.Add(this.groupBox3);
			this.scheduleTab.Controls.Add(this.schedGroup);
			this.scheduleTab.Controls.Add(this.schedStartLabel);
			this.scheduleTab.Controls.Add(this.schedMonthlyRadio);
			this.scheduleTab.Controls.Add(this.schedWeeklyRadio);
			this.scheduleTab.Controls.Add(this.schedDailyRadio);
			this.scheduleTab.Controls.Add(this.schedOneRadio);
			resources.ApplyResources(this.scheduleTab, "scheduleTab");
			this.scheduleTab.Name = "scheduleTab";
			this.scheduleTab.UseVisualStyleBackColor = true;
			// 
			// schedStartDatePicker
			// 
			resources.ApplyResources(this.schedStartDatePicker, "schedStartDatePicker");
			this.schedStartDatePicker.Name = "schedStartDatePicker";
			this.schedStartDatePicker.Value = new System.DateTime(2009, 7, 30, 13, 10, 46, 672);
			this.schedStartDatePicker.ValueChanged += new System.EventHandler(this.schedStartDatePicker_ValueChanged);
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// schedGroup
			// 
			this.schedGroup.Controls.Add(this.schedTabControl);
			resources.ApplyResources(this.schedGroup, "schedGroup");
			this.schedGroup.Name = "schedGroup";
			this.schedGroup.TabStop = false;
			// 
			// schedTabControl
			// 
			resources.ApplyResources(this.schedTabControl, "schedTabControl");
			this.schedTabControl.Controls.Add(this.oneTimeTab);
			this.schedTabControl.Controls.Add(this.dailyTab);
			this.schedTabControl.Controls.Add(this.weeklyTab);
			this.schedTabControl.Controls.Add(this.monthlyTab);
			this.schedTabControl.Name = "schedTabControl";
			this.schedTabControl.SelectedIndex = 0;
			this.schedTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.schedTabControl.TabStop = false;
			// 
			// oneTimeTab
			// 
			resources.ApplyResources(this.oneTimeTab, "oneTimeTab");
			this.oneTimeTab.Name = "oneTimeTab";
			this.oneTimeTab.UseVisualStyleBackColor = true;
			// 
			// dailyTab
			// 
			this.dailyTab.Controls.Add(this.dailyTriggerUI1);
			resources.ApplyResources(this.dailyTab, "dailyTab");
			this.dailyTab.Name = "dailyTab";
			this.dailyTab.UseVisualStyleBackColor = true;
			// 
			// dailyTriggerUI1
			// 
			resources.ApplyResources(this.dailyTriggerUI1, "dailyTriggerUI1");
			this.dailyTriggerUI1.Name = "dailyTriggerUI1";
			this.dailyTriggerUI1.ShowStartBoundary = false;
			// 
			// weeklyTab
			// 
			this.weeklyTab.Controls.Add(this.weeklyTriggerUI1);
			resources.ApplyResources(this.weeklyTab, "weeklyTab");
			this.weeklyTab.Name = "weeklyTab";
			this.weeklyTab.UseVisualStyleBackColor = true;
			// 
			// weeklyTriggerUI1
			// 
			resources.ApplyResources(this.weeklyTriggerUI1, "weeklyTriggerUI1");
			this.weeklyTriggerUI1.Name = "weeklyTriggerUI1";
			this.weeklyTriggerUI1.ShowStartBoundary = false;
			// 
			// monthlyTab
			// 
			this.monthlyTab.Controls.Add(this.monthlyTriggerUI1);
			resources.ApplyResources(this.monthlyTab, "monthlyTab");
			this.monthlyTab.Name = "monthlyTab";
			this.monthlyTab.UseVisualStyleBackColor = true;
			// 
			// monthlyTriggerUI1
			// 
			resources.ApplyResources(this.monthlyTriggerUI1, "monthlyTriggerUI1");
			this.monthlyTriggerUI1.Name = "monthlyTriggerUI1";
			this.monthlyTriggerUI1.ShowStartBoundary = false;
			this.monthlyTriggerUI1.TriggerTypeChanged += new System.EventHandler(this.monthlyTriggerUI1_TriggerTypeChanged);
			// 
			// schedStartLabel
			// 
			resources.ApplyResources(this.schedStartLabel, "schedStartLabel");
			this.schedStartLabel.Name = "schedStartLabel";
			// 
			// schedMonthlyRadio
			// 
			resources.ApplyResources(this.schedMonthlyRadio, "schedMonthlyRadio");
			this.schedMonthlyRadio.Name = "schedMonthlyRadio";
			this.schedMonthlyRadio.UseVisualStyleBackColor = true;
			this.schedMonthlyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedWeeklyRadio
			// 
			resources.ApplyResources(this.schedWeeklyRadio, "schedWeeklyRadio");
			this.schedWeeklyRadio.Name = "schedWeeklyRadio";
			this.schedWeeklyRadio.UseVisualStyleBackColor = true;
			this.schedWeeklyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedDailyRadio
			// 
			resources.ApplyResources(this.schedDailyRadio, "schedDailyRadio");
			this.schedDailyRadio.Name = "schedDailyRadio";
			this.schedDailyRadio.UseVisualStyleBackColor = true;
			this.schedDailyRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// schedOneRadio
			// 
			resources.ApplyResources(this.schedOneRadio, "schedOneRadio");
			this.schedOneRadio.Name = "schedOneRadio";
			this.schedOneRadio.UseVisualStyleBackColor = true;
			this.schedOneRadio.CheckedChanged += new System.EventHandler(this.schedOneRadio_CheckedChanged);
			// 
			// logonTab
			// 
			this.logonTab.Controls.Add(this.logonRemotePanel);
			this.logonTab.Controls.Add(this.logonChgUserBtn);
			this.logonTab.Controls.Add(this.logonUserLabel);
			this.logonTab.Controls.Add(this.logonSpecUserRadio);
			this.logonTab.Controls.Add(this.logonAnyUserRadio);
			resources.ApplyResources(this.logonTab, "logonTab");
			this.logonTab.Name = "logonTab";
			this.logonTab.UseVisualStyleBackColor = true;
			// 
			// logonRemotePanel
			// 
			this.logonRemotePanel.Controls.Add(this.groupBox6);
			this.logonRemotePanel.Controls.Add(this.logonLocalRadio);
			this.logonRemotePanel.Controls.Add(this.logonRemoteRadio);
			resources.ApplyResources(this.logonRemotePanel, "logonRemotePanel");
			this.logonRemotePanel.Name = "logonRemotePanel";
			// 
			// groupBox6
			// 
			resources.ApplyResources(this.groupBox6, "groupBox6");
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			// 
			// logonLocalRadio
			// 
			resources.ApplyResources(this.logonLocalRadio, "logonLocalRadio");
			this.logonLocalRadio.Name = "logonLocalRadio";
			this.logonLocalRadio.UseVisualStyleBackColor = true;
			// 
			// logonRemoteRadio
			// 
			resources.ApplyResources(this.logonRemoteRadio, "logonRemoteRadio");
			this.logonRemoteRadio.Name = "logonRemoteRadio";
			this.logonRemoteRadio.UseVisualStyleBackColor = true;
			// 
			// logonChgUserBtn
			// 
			resources.ApplyResources(this.logonChgUserBtn, "logonChgUserBtn");
			this.logonChgUserBtn.Name = "logonChgUserBtn";
			this.logonChgUserBtn.UseVisualStyleBackColor = true;
			this.logonChgUserBtn.Click += new System.EventHandler(this.logonChgUserBtn_Click);
			// 
			// logonUserLabel
			// 
			resources.ApplyResources(this.logonUserLabel, "logonUserLabel");
			this.logonUserLabel.Name = "logonUserLabel";
			// 
			// logonSpecUserRadio
			// 
			resources.ApplyResources(this.logonSpecUserRadio, "logonSpecUserRadio");
			this.logonSpecUserRadio.Name = "logonSpecUserRadio";
			this.logonSpecUserRadio.UseVisualStyleBackColor = true;
			this.logonSpecUserRadio.CheckedChanged += new System.EventHandler(this.logonAnyUserRadio_CheckedChanged);
			// 
			// logonAnyUserRadio
			// 
			resources.ApplyResources(this.logonAnyUserRadio, "logonAnyUserRadio");
			this.logonAnyUserRadio.Name = "logonAnyUserRadio";
			this.logonAnyUserRadio.UseVisualStyleBackColor = true;
			this.logonAnyUserRadio.CheckedChanged += new System.EventHandler(this.logonAnyUserRadio_CheckedChanged);
			// 
			// startupTab
			// 
			this.startupTab.Controls.Add(this.startupIntroLabel);
			resources.ApplyResources(this.startupTab, "startupTab");
			this.startupTab.Name = "startupTab";
			this.startupTab.UseVisualStyleBackColor = true;
			// 
			// startupIntroLabel
			// 
			resources.ApplyResources(this.startupIntroLabel, "startupIntroLabel");
			this.startupIntroLabel.Name = "startupIntroLabel";
			// 
			// idleTab
			// 
			this.idleTab.Controls.Add(this.label8);
			resources.ApplyResources(this.idleTab, "idleTab");
			this.idleTab.Name = "idleTab";
			this.idleTab.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// onEventTab
			// 
			this.onEventTab.Controls.Add(this.eventTriggerUI1);
			resources.ApplyResources(this.onEventTab, "onEventTab");
			this.onEventTab.Name = "onEventTab";
			this.onEventTab.UseVisualStyleBackColor = true;
			// 
			// eventTriggerUI1
			// 
			resources.ApplyResources(this.eventTriggerUI1, "eventTriggerUI1");
			this.eventTriggerUI1.Name = "eventTriggerUI1";
			// 
			// advSettingsGroup
			// 
			this.advSettingsGroup.Controls.Add(this.stopIfRunsSpan);
			this.advSettingsGroup.Controls.Add(this.durationSpan);
			this.advSettingsGroup.Controls.Add(this.repeatSpan);
			this.advSettingsGroup.Controls.Add(this.delaySpan);
			this.advSettingsGroup.Controls.Add(this.expireDatePicker);
			this.advSettingsGroup.Controls.Add(this.activateDatePicker);
			this.advSettingsGroup.Controls.Add(this.durationLabel);
			this.advSettingsGroup.Controls.Add(this.stopAfterDurationCheckBox);
			this.advSettingsGroup.Controls.Add(this.enabledCheckBox);
			this.advSettingsGroup.Controls.Add(this.expireCheckBox);
			this.advSettingsGroup.Controls.Add(this.activateCheckBox);
			this.advSettingsGroup.Controls.Add(this.stopIfRunsCheckBox);
			this.advSettingsGroup.Controls.Add(this.repeatCheckBox);
			this.advSettingsGroup.Controls.Add(this.delayCheckBox);
			resources.ApplyResources(this.advSettingsGroup, "advSettingsGroup");
			this.advSettingsGroup.Name = "advSettingsGroup";
			this.advSettingsGroup.TabStop = false;
			// 
			// stopIfRunsSpan
			// 
			resources.ApplyResources(this.stopIfRunsSpan, "stopIfRunsSpan");
			this.stopIfRunsSpan.Name = "stopIfRunsSpan";
			this.stopIfRunsSpan.ValueChanged += new System.EventHandler(this.stopIfRunsSpan_ValueChanged);
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
			// delaySpan
			// 
			resources.ApplyResources(this.delaySpan, "delaySpan");
			this.delaySpan.Name = "delaySpan";
			this.delaySpan.ValueChanged += new System.EventHandler(this.delaySpan_ValueChanged);
			// 
			// expireDatePicker
			// 
			resources.ApplyResources(this.expireDatePicker, "expireDatePicker");
			this.expireDatePicker.Name = "expireDatePicker";
			this.expireDatePicker.Value = new System.DateTime(2009, 7, 30, 13, 10, 48, 186);
			this.expireDatePicker.ValueChanged += new System.EventHandler(this.expireDatePicker_ValueChanged);
			// 
			// activateDatePicker
			// 
			resources.ApplyResources(this.activateDatePicker, "activateDatePicker");
			this.activateDatePicker.Name = "activateDatePicker";
			this.activateDatePicker.Value = new System.DateTime(2009, 7, 30, 13, 10, 48, 206);
			this.activateDatePicker.ValueChanged += new System.EventHandler(this.activateDatePicker_ValueChanged);
			// 
			// durationLabel
			// 
			resources.ApplyResources(this.durationLabel, "durationLabel");
			this.durationLabel.Name = "durationLabel";
			// 
			// stopAfterDurationCheckBox
			// 
			resources.ApplyResources(this.stopAfterDurationCheckBox, "stopAfterDurationCheckBox");
			this.stopAfterDurationCheckBox.Name = "stopAfterDurationCheckBox";
			this.stopAfterDurationCheckBox.UseVisualStyleBackColor = true;
			this.stopAfterDurationCheckBox.CheckedChanged += new System.EventHandler(this.stopAfterDurationCheckBox_CheckedChanged);
			// 
			// enabledCheckBox
			// 
			resources.ApplyResources(this.enabledCheckBox, "enabledCheckBox");
			this.enabledCheckBox.Name = "enabledCheckBox";
			this.enabledCheckBox.UseVisualStyleBackColor = true;
			this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
			// 
			// expireCheckBox
			// 
			resources.ApplyResources(this.expireCheckBox, "expireCheckBox");
			this.expireCheckBox.Name = "expireCheckBox";
			this.expireCheckBox.UseVisualStyleBackColor = true;
			this.expireCheckBox.CheckedChanged += new System.EventHandler(this.expireCheckBox_CheckedChanged);
			// 
			// activateCheckBox
			// 
			resources.ApplyResources(this.activateCheckBox, "activateCheckBox");
			this.activateCheckBox.Name = "activateCheckBox";
			this.activateCheckBox.UseVisualStyleBackColor = true;
			this.activateCheckBox.CheckedChanged += new System.EventHandler(this.activateCheckBox_CheckedChanged);
			// 
			// stopIfRunsCheckBox
			// 
			resources.ApplyResources(this.stopIfRunsCheckBox, "stopIfRunsCheckBox");
			this.stopIfRunsCheckBox.Name = "stopIfRunsCheckBox";
			this.stopIfRunsCheckBox.UseVisualStyleBackColor = true;
			this.stopIfRunsCheckBox.CheckedChanged += new System.EventHandler(this.stopIfRunsCheckBox_CheckedChanged);
			// 
			// repeatCheckBox
			// 
			resources.ApplyResources(this.repeatCheckBox, "repeatCheckBox");
			this.repeatCheckBox.Name = "repeatCheckBox";
			this.repeatCheckBox.UseVisualStyleBackColor = true;
			this.repeatCheckBox.CheckedChanged += new System.EventHandler(this.repeatCheckBox_CheckedChanged);
			// 
			// delayCheckBox
			// 
			resources.ApplyResources(this.delayCheckBox, "delayCheckBox");
			this.delayCheckBox.Name = "delayCheckBox";
			this.delayCheckBox.UseVisualStyleBackColor = true;
			this.delayCheckBox.CheckedChanged += new System.EventHandler(this.delayCheckBox_CheckedChanged);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// TriggerEditDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.advSettingsGroup);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.triggerTypeCombo);
			this.Controls.Add(this.triggerTypeLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TriggerEditDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.groupBox1.ResumeLayout(false);
			this.settingsTabControl.ResumeLayout(false);
			this.scheduleTab.ResumeLayout(false);
			this.scheduleTab.PerformLayout();
			this.schedGroup.ResumeLayout(false);
			this.schedTabControl.ResumeLayout(false);
			this.dailyTab.ResumeLayout(false);
			this.weeklyTab.ResumeLayout(false);
			this.monthlyTab.ResumeLayout(false);
			this.logonTab.ResumeLayout(false);
			this.logonTab.PerformLayout();
			this.logonRemotePanel.ResumeLayout(false);
			this.logonRemotePanel.PerformLayout();
			this.startupTab.ResumeLayout(false);
			this.startupTab.PerformLayout();
			this.idleTab.ResumeLayout(false);
			this.idleTab.PerformLayout();
			this.onEventTab.ResumeLayout(false);
			this.advSettingsGroup.ResumeLayout(false);
			this.advSettingsGroup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label triggerTypeLabel;
		private System.Windows.Forms.ComboBox triggerTypeCombo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabControl settingsTabControl;
		private System.Windows.Forms.TabPage scheduleTab;
		private System.Windows.Forms.TabPage logonTab;
		private System.Windows.Forms.RadioButton schedOneRadio;
		private System.Windows.Forms.Label schedStartLabel;
		private System.Windows.Forms.RadioButton schedMonthlyRadio;
		private System.Windows.Forms.RadioButton schedWeeklyRadio;
		private System.Windows.Forms.RadioButton schedDailyRadio;
		private System.Windows.Forms.GroupBox schedGroup;
		private System.Windows.Forms.TabControl schedTabControl;
		private System.Windows.Forms.TabPage oneTimeTab;
		private System.Windows.Forms.TabPage dailyTab;
		private System.Windows.Forms.TabPage weeklyTab;
		private System.Windows.Forms.TabPage monthlyTab;
		private System.Windows.Forms.TabPage startupTab;
		private System.Windows.Forms.TabPage idleTab;
		private System.Windows.Forms.TabPage onEventTab;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox advSettingsGroup;
		private System.Windows.Forms.CheckBox delayCheckBox;
		private System.Windows.Forms.CheckBox stopAfterDurationCheckBox;
		private System.Windows.Forms.CheckBox stopIfRunsCheckBox;
		private System.Windows.Forms.CheckBox repeatCheckBox;
		private System.Windows.Forms.CheckBox enabledCheckBox;
		private System.Windows.Forms.CheckBox expireCheckBox;
		private System.Windows.Forms.CheckBox activateCheckBox;
		private System.Windows.Forms.Label durationLabel;
		private FullDateTimePicker schedStartDatePicker;
		private System.Windows.Forms.TimeSpanPicker delaySpan;
		private FullDateTimePicker expireDatePicker;
		private FullDateTimePicker activateDatePicker;
		private System.Windows.Forms.TimeSpanPicker stopIfRunsSpan;
		private System.Windows.Forms.TimeSpanPicker durationSpan;
		private System.Windows.Forms.TimeSpanPicker repeatSpan;
		private System.Windows.Forms.Button logonChgUserBtn;
		private System.Windows.Forms.Label logonUserLabel;
		private System.Windows.Forms.RadioButton logonSpecUserRadio;
		private System.Windows.Forms.RadioButton logonAnyUserRadio;
		private System.Windows.Forms.Label startupIntroLabel;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel logonRemotePanel;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.RadioButton logonLocalRadio;
		private System.Windows.Forms.RadioButton logonRemoteRadio;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private UIComponents.DailyTriggerUI dailyTriggerUI1;
		private UIComponents.WeeklyTriggerUI weeklyTriggerUI1;
		private UIComponents.MonthlyTriggerUI monthlyTriggerUI1;
		private UIComponents.EventTriggerUI eventTriggerUI1;
	}
}