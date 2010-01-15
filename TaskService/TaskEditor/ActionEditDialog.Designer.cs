namespace Microsoft.Win32.TaskScheduler
{
	public partial class ActionEditDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionEditDialog));
			this.introLabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.execProgLabel = new System.Windows.Forms.Label();
			this.actionsCombo = new System.Windows.Forms.ComboBox();
			this.settingsGroup = new System.Windows.Forms.GroupBox();
			this.settingsTabs = new System.Windows.Forms.TabControl();
			this.execTab = new System.Windows.Forms.TabPage();
			this.execProgBrowseBtn = new System.Windows.Forms.Button();
			this.execDirText = new System.Windows.Forms.TextBox();
			this.execArgText = new System.Windows.Forms.TextBox();
			this.execProgText = new System.Windows.Forms.TextBox();
			this.execDirLabel = new System.Windows.Forms.Label();
			this.execArgLabel = new System.Windows.Forms.Label();
			this.emailTab = new System.Windows.Forms.TabPage();
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
			this.messageTab = new System.Windows.Forms.TabPage();
			this.msgMsgText = new System.Windows.Forms.TextBox();
			this.msgMsgLabel = new System.Windows.Forms.Label();
			this.msgTitleText = new System.Windows.Forms.TextBox();
			this.msgTitleLabel = new System.Windows.Forms.Label();
			this.msgIntroLabel = new System.Windows.Forms.Label();
			this.comTab = new System.Windows.Forms.TabPage();
			this.comDataText = new System.Windows.Forms.TextBox();
			this.comDataLabel = new System.Windows.Forms.Label();
			this.comCLSIDText = new System.Windows.Forms.TextBox();
			this.comCLSIDLabel = new System.Windows.Forms.Label();
			this.comIntroLabel = new System.Windows.Forms.Label();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.actionsLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.settingsGroup.SuspendLayout();
			this.settingsTabs.SuspendLayout();
			this.execTab.SuspendLayout();
			this.emailTab.SuspendLayout();
			this.messageTab.SuspendLayout();
			this.comTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// introLabel
			// 
			resources.ApplyResources(this.introLabel, "introLabel");
			this.introLabel.Name = "introLabel";
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// execProgLabel
			// 
			resources.ApplyResources(this.execProgLabel, "execProgLabel");
			this.execProgLabel.Name = "execProgLabel";
			// 
			// actionsCombo
			// 
			resources.ApplyResources(this.actionsCombo, "actionsCombo");
			this.actionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionsCombo.FormattingEnabled = true;
			this.actionsCombo.Items.AddRange(new object[] {
            resources.GetString("actionsCombo.Items"),
            resources.GetString("actionsCombo.Items1"),
            resources.GetString("actionsCombo.Items2"),
            resources.GetString("actionsCombo.Items3")});
			this.actionsCombo.Name = "actionsCombo";
			this.actionsCombo.SelectedIndexChanged += new System.EventHandler(this.actionsCombo_SelectedIndexChanged);
			// 
			// settingsGroup
			// 
			resources.ApplyResources(this.settingsGroup, "settingsGroup");
			this.settingsGroup.Controls.Add(this.settingsTabs);
			this.settingsGroup.Name = "settingsGroup";
			this.settingsGroup.TabStop = false;
			// 
			// settingsTabs
			// 
			resources.ApplyResources(this.settingsTabs, "settingsTabs");
			this.settingsTabs.Controls.Add(this.execTab);
			this.settingsTabs.Controls.Add(this.emailTab);
			this.settingsTabs.Controls.Add(this.messageTab);
			this.settingsTabs.Controls.Add(this.comTab);
			this.settingsTabs.Name = "settingsTabs";
			this.settingsTabs.SelectedIndex = 0;
			this.settingsTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.settingsTabs.TabStop = false;
			// 
			// execTab
			// 
			this.execTab.Controls.Add(this.execProgBrowseBtn);
			this.execTab.Controls.Add(this.execDirText);
			this.execTab.Controls.Add(this.execArgText);
			this.execTab.Controls.Add(this.execProgText);
			this.execTab.Controls.Add(this.execDirLabel);
			this.execTab.Controls.Add(this.execArgLabel);
			this.execTab.Controls.Add(this.execProgLabel);
			resources.ApplyResources(this.execTab, "execTab");
			this.execTab.Name = "execTab";
			this.execTab.UseVisualStyleBackColor = true;
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
			// emailTab
			// 
			this.emailTab.Controls.Add(this.emailAttachementBrowseBtn);
			this.emailTab.Controls.Add(this.emailSMTPText);
			this.emailTab.Controls.Add(this.emailSMTPLabel);
			this.emailTab.Controls.Add(this.emailAttachmentText);
			this.emailTab.Controls.Add(this.emailAttachmentLabel);
			this.emailTab.Controls.Add(this.groupBox3);
			this.emailTab.Controls.Add(this.emailTextText);
			this.emailTab.Controls.Add(this.emailTextLabel);
			this.emailTab.Controls.Add(this.emailSubjectText);
			this.emailTab.Controls.Add(this.emailSubjectLabel);
			this.emailTab.Controls.Add(this.emailToText);
			this.emailTab.Controls.Add(this.emailToLabel);
			this.emailTab.Controls.Add(this.emailFromText);
			this.emailTab.Controls.Add(this.emailFromLabel);
			resources.ApplyResources(this.emailTab, "emailTab");
			this.emailTab.Name = "emailTab";
			this.emailTab.UseVisualStyleBackColor = true;
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
			// messageTab
			// 
			this.messageTab.Controls.Add(this.msgMsgText);
			this.messageTab.Controls.Add(this.msgMsgLabel);
			this.messageTab.Controls.Add(this.msgTitleText);
			this.messageTab.Controls.Add(this.msgTitleLabel);
			this.messageTab.Controls.Add(this.msgIntroLabel);
			resources.ApplyResources(this.messageTab, "messageTab");
			this.messageTab.Name = "messageTab";
			this.messageTab.UseVisualStyleBackColor = true;
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
			// comTab
			// 
			this.comTab.Controls.Add(this.comDataText);
			this.comTab.Controls.Add(this.comDataLabel);
			this.comTab.Controls.Add(this.comCLSIDText);
			this.comTab.Controls.Add(this.comCLSIDLabel);
			this.comTab.Controls.Add(this.comIntroLabel);
			resources.ApplyResources(this.comTab, "comTab");
			this.comTab.Name = "comTab";
			this.comTab.UseVisualStyleBackColor = true;
			// 
			// comDataText
			// 
			resources.ApplyResources(this.comDataText, "comDataText");
			this.comDataText.Name = "comDataText";
			// 
			// comDataLabel
			// 
			resources.ApplyResources(this.comDataLabel, "comDataLabel");
			this.comDataLabel.Name = "comDataLabel";
			// 
			// comCLSIDText
			// 
			resources.ApplyResources(this.comCLSIDText, "comCLSIDText");
			this.comCLSIDText.Name = "comCLSIDText";
			this.comCLSIDText.Validating += new System.ComponentModel.CancelEventHandler(this.comCLSIDText_Validating);
			// 
			// comCLSIDLabel
			// 
			resources.ApplyResources(this.comCLSIDLabel, "comCLSIDLabel");
			this.comCLSIDLabel.Name = "comCLSIDLabel";
			// 
			// comIntroLabel
			// 
			resources.ApplyResources(this.comIntroLabel, "comIntroLabel");
			this.comIntroLabel.Name = "comIntroLabel";
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// actionsLabel
			// 
			resources.ApplyResources(this.actionsLabel, "actionsLabel");
			this.actionsLabel.Name = "actionsLabel";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// ActionEditDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.settingsGroup);
			this.Controls.Add(this.actionsCombo);
			this.Controls.Add(this.actionsLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.introLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ActionEditDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.settingsGroup.ResumeLayout(false);
			this.settingsTabs.ResumeLayout(false);
			this.execTab.ResumeLayout(false);
			this.execTab.PerformLayout();
			this.emailTab.ResumeLayout(false);
			this.emailTab.PerformLayout();
			this.messageTab.ResumeLayout(false);
			this.messageTab.PerformLayout();
			this.comTab.ResumeLayout(false);
			this.comTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label introLabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label execProgLabel;
		private System.Windows.Forms.ComboBox actionsCombo;
		private System.Windows.Forms.GroupBox settingsGroup;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TabControl settingsTabs;
		private System.Windows.Forms.TabPage execTab;
		private System.Windows.Forms.TabPage emailTab;
		private System.Windows.Forms.TabPage messageTab;
		private System.Windows.Forms.Button execProgBrowseBtn;
		private System.Windows.Forms.TextBox execDirText;
		private System.Windows.Forms.TextBox execArgText;
		private System.Windows.Forms.TextBox execProgText;
		private System.Windows.Forms.Label execDirLabel;
		private System.Windows.Forms.Label execArgLabel;
		private System.Windows.Forms.TextBox emailFromText;
		private System.Windows.Forms.Label emailFromLabel;
		private System.Windows.Forms.Label actionsLabel;
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
		private System.Windows.Forms.TextBox msgMsgText;
		private System.Windows.Forms.Label msgMsgLabel;
		private System.Windows.Forms.TextBox msgTitleText;
		private System.Windows.Forms.Label msgTitleLabel;
		private System.Windows.Forms.Label msgIntroLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TabPage comTab;
		private System.Windows.Forms.TextBox comDataText;
		private System.Windows.Forms.Label comDataLabel;
		private System.Windows.Forms.TextBox comCLSIDText;
		private System.Windows.Forms.Label comCLSIDLabel;
		private System.Windows.Forms.Label comIntroLabel;
	}
}