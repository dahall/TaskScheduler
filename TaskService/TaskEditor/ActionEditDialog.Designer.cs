namespace Microsoft.Win32.TaskScheduler
{
	partial class ActionEditDialog
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
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.actionsLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.comTab = new System.Windows.Forms.TabPage();
			this.comCLSIDText = new System.Windows.Forms.TextBox();
			this.comCLSIDLabel = new System.Windows.Forms.Label();
			this.comIntroLabel = new System.Windows.Forms.Label();
			this.comDataLabel = new System.Windows.Forms.Label();
			this.comDataText = new System.Windows.Forms.TextBox();
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
			this.introLabel.AutoSize = true;
			this.introLabel.Location = new System.Drawing.Point(13, 13);
			this.introLabel.Name = "introLabel";
			this.introLabel.Size = new System.Drawing.Size(245, 13);
			this.introLabel.TabIndex = 0;
			this.introLabel.Text = "You must specify what action this task will perform.";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(16, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(404, 2);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// execProgLabel
			// 
			this.execProgLabel.AutoSize = true;
			this.execProgLabel.Location = new System.Drawing.Point(6, 3);
			this.execProgLabel.Name = "execProgLabel";
			this.execProgLabel.Size = new System.Drawing.Size(79, 13);
			this.execProgLabel.TabIndex = 0;
			this.execProgLabel.Text = "Program/script:";
			// 
			// actionsCombo
			// 
			this.actionsCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.actionsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionsCombo.FormattingEnabled = true;
			this.actionsCombo.Items.AddRange(new object[] {
            "Start a program",
            "Send an e-mail",
            "Display a message",
            "Custom handler"});
			this.actionsCombo.Location = new System.Drawing.Point(84, 53);
			this.actionsCombo.Name = "actionsCombo";
			this.actionsCombo.Size = new System.Drawing.Size(336, 21);
			this.actionsCombo.TabIndex = 2;
			this.actionsCombo.SelectedIndexChanged += new System.EventHandler(this.actionsCombo_SelectedIndexChanged);
			// 
			// settingsGroup
			// 
			this.settingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.settingsGroup.Controls.Add(this.settingsTabs);
			this.settingsGroup.Location = new System.Drawing.Point(12, 80);
			this.settingsGroup.Name = "settingsGroup";
			this.settingsGroup.Size = new System.Drawing.Size(408, 311);
			this.settingsGroup.TabIndex = 3;
			this.settingsGroup.TabStop = false;
			this.settingsGroup.Text = "Settings";
			// 
			// settingsTabs
			// 
			this.settingsTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.settingsTabs.Controls.Add(this.execTab);
			this.settingsTabs.Controls.Add(this.emailTab);
			this.settingsTabs.Controls.Add(this.messageTab);
			this.settingsTabs.Controls.Add(this.comTab);
			this.settingsTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsTabs.ItemSize = new System.Drawing.Size(0, 1);
			this.settingsTabs.Location = new System.Drawing.Point(3, 16);
			this.settingsTabs.Name = "settingsTabs";
			this.settingsTabs.SelectedIndex = 0;
			this.settingsTabs.Size = new System.Drawing.Size(402, 292);
			this.settingsTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.settingsTabs.TabIndex = 1;
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
			this.execTab.Location = new System.Drawing.Point(4, 5);
			this.execTab.Name = "execTab";
			this.execTab.Padding = new System.Windows.Forms.Padding(3);
			this.execTab.Size = new System.Drawing.Size(394, 283);
			this.execTab.TabIndex = 1;
			this.execTab.UseVisualStyleBackColor = true;
			// 
			// execProgBrowseBtn
			// 
			this.execProgBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.execProgBrowseBtn.Location = new System.Drawing.Point(304, 20);
			this.execProgBrowseBtn.Name = "execProgBrowseBtn";
			this.execProgBrowseBtn.Size = new System.Drawing.Size(84, 23);
			this.execProgBrowseBtn.TabIndex = 2;
			this.execProgBrowseBtn.Text = "Browse...";
			this.execProgBrowseBtn.UseVisualStyleBackColor = true;
			this.execProgBrowseBtn.Click += new System.EventHandler(this.execProgBrowseBtn_Click);
			// 
			// execDirText
			// 
			this.execDirText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.execDirText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execDirText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.execDirText.Location = new System.Drawing.Point(179, 77);
			this.execDirText.Name = "execDirText";
			this.execDirText.Size = new System.Drawing.Size(209, 20);
			this.execDirText.TabIndex = 1;
			// 
			// execArgText
			// 
			this.execArgText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.execArgText.Location = new System.Drawing.Point(179, 51);
			this.execArgText.Name = "execArgText";
			this.execArgText.Size = new System.Drawing.Size(209, 20);
			this.execArgText.TabIndex = 1;
			// 
			// execProgText
			// 
			this.execProgText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.execProgText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execProgText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.execProgText.Location = new System.Drawing.Point(9, 20);
			this.execProgText.Name = "execProgText";
			this.execProgText.Size = new System.Drawing.Size(288, 20);
			this.execProgText.TabIndex = 1;
			// 
			// execDirLabel
			// 
			this.execDirLabel.AutoSize = true;
			this.execDirLabel.Location = new System.Drawing.Point(6, 80);
			this.execDirLabel.Name = "execDirLabel";
			this.execDirLabel.Size = new System.Drawing.Size(89, 13);
			this.execDirLabel.TabIndex = 0;
			this.execDirLabel.Text = "Start in (optional):";
			// 
			// execArgLabel
			// 
			this.execArgLabel.AutoSize = true;
			this.execArgLabel.Location = new System.Drawing.Point(6, 54);
			this.execArgLabel.Name = "execArgLabel";
			this.execArgLabel.Size = new System.Drawing.Size(127, 13);
			this.execArgLabel.TabIndex = 0;
			this.execArgLabel.Text = "Add arguments (optional):";
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
			this.emailTab.Location = new System.Drawing.Point(4, 5);
			this.emailTab.Name = "emailTab";
			this.emailTab.Size = new System.Drawing.Size(394, 283);
			this.emailTab.TabIndex = 2;
			this.emailTab.UseVisualStyleBackColor = true;
			// 
			// emailAttachementBrowseBtn
			// 
			this.emailAttachementBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachementBrowseBtn.Location = new System.Drawing.Point(316, 218);
			this.emailAttachementBrowseBtn.Name = "emailAttachementBrowseBtn";
			this.emailAttachementBrowseBtn.Size = new System.Drawing.Size(75, 23);
			this.emailAttachementBrowseBtn.TabIndex = 4;
			this.emailAttachementBrowseBtn.Text = "Browse...";
			this.emailAttachementBrowseBtn.UseVisualStyleBackColor = true;
			this.emailAttachementBrowseBtn.Click += new System.EventHandler(this.emailAttachementBrowseBtn_Click);
			// 
			// emailSMTPText
			// 
			this.emailSMTPText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailSMTPText.Location = new System.Drawing.Point(100, 255);
			this.emailSMTPText.Name = "emailSMTPText";
			this.emailSMTPText.Size = new System.Drawing.Size(291, 20);
			this.emailSMTPText.TabIndex = 3;
			// 
			// emailSMTPLabel
			// 
			this.emailSMTPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailSMTPLabel.AutoSize = true;
			this.emailSMTPLabel.Location = new System.Drawing.Point(3, 258);
			this.emailSMTPLabel.Name = "emailSMTPLabel";
			this.emailSMTPLabel.Size = new System.Drawing.Size(72, 13);
			this.emailSMTPLabel.TabIndex = 2;
			this.emailSMTPLabel.Text = "SMTP server:";
			// 
			// emailAttachmentText
			// 
			this.emailAttachmentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachmentText.Location = new System.Drawing.Point(100, 220);
			this.emailAttachmentText.Name = "emailAttachmentText";
			this.emailAttachmentText.Size = new System.Drawing.Size(210, 20);
			this.emailAttachmentText.TabIndex = 3;
			// 
			// emailAttachmentLabel
			// 
			this.emailAttachmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachmentLabel.AutoSize = true;
			this.emailAttachmentLabel.Location = new System.Drawing.Point(3, 223);
			this.emailAttachmentLabel.Name = "emailAttachmentLabel";
			this.emailAttachmentLabel.Size = new System.Drawing.Size(64, 13);
			this.emailAttachmentLabel.TabIndex = 2;
			this.emailAttachmentLabel.Text = "Attachment:";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Location = new System.Drawing.Point(6, 247);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(384, 2);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			// 
			// emailTextText
			// 
			this.emailTextText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailTextText.Location = new System.Drawing.Point(65, 81);
			this.emailTextText.Multiline = true;
			this.emailTextText.Name = "emailTextText";
			this.emailTextText.Size = new System.Drawing.Size(326, 133);
			this.emailTextText.TabIndex = 3;
			// 
			// emailTextLabel
			// 
			this.emailTextLabel.AutoSize = true;
			this.emailTextLabel.Location = new System.Drawing.Point(3, 84);
			this.emailTextLabel.Name = "emailTextLabel";
			this.emailTextLabel.Size = new System.Drawing.Size(31, 13);
			this.emailTextLabel.TabIndex = 2;
			this.emailTextLabel.Text = "Text:";
			// 
			// emailSubjectText
			// 
			this.emailSubjectText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailSubjectText.Location = new System.Drawing.Point(65, 55);
			this.emailSubjectText.Name = "emailSubjectText";
			this.emailSubjectText.Size = new System.Drawing.Size(326, 20);
			this.emailSubjectText.TabIndex = 3;
			// 
			// emailSubjectLabel
			// 
			this.emailSubjectLabel.AutoSize = true;
			this.emailSubjectLabel.Location = new System.Drawing.Point(3, 58);
			this.emailSubjectLabel.Name = "emailSubjectLabel";
			this.emailSubjectLabel.Size = new System.Drawing.Size(46, 13);
			this.emailSubjectLabel.TabIndex = 2;
			this.emailSubjectLabel.Text = "Subject:";
			// 
			// emailToText
			// 
			this.emailToText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailToText.Location = new System.Drawing.Point(65, 29);
			this.emailToText.Name = "emailToText";
			this.emailToText.Size = new System.Drawing.Size(326, 20);
			this.emailToText.TabIndex = 3;
			// 
			// emailToLabel
			// 
			this.emailToLabel.AutoSize = true;
			this.emailToLabel.Location = new System.Drawing.Point(3, 32);
			this.emailToLabel.Name = "emailToLabel";
			this.emailToLabel.Size = new System.Drawing.Size(23, 13);
			this.emailToLabel.TabIndex = 2;
			this.emailToLabel.Text = "To:";
			// 
			// emailFromText
			// 
			this.emailFromText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.emailFromText.Location = new System.Drawing.Point(65, 3);
			this.emailFromText.Name = "emailFromText";
			this.emailFromText.Size = new System.Drawing.Size(326, 20);
			this.emailFromText.TabIndex = 3;
			// 
			// emailFromLabel
			// 
			this.emailFromLabel.AutoSize = true;
			this.emailFromLabel.Location = new System.Drawing.Point(3, 6);
			this.emailFromLabel.Name = "emailFromLabel";
			this.emailFromLabel.Size = new System.Drawing.Size(33, 13);
			this.emailFromLabel.TabIndex = 2;
			this.emailFromLabel.Text = "From:";
			// 
			// messageTab
			// 
			this.messageTab.Controls.Add(this.msgMsgText);
			this.messageTab.Controls.Add(this.msgMsgLabel);
			this.messageTab.Controls.Add(this.msgTitleText);
			this.messageTab.Controls.Add(this.msgTitleLabel);
			this.messageTab.Controls.Add(this.msgIntroLabel);
			this.messageTab.Location = new System.Drawing.Point(4, 5);
			this.messageTab.Name = "messageTab";
			this.messageTab.Size = new System.Drawing.Size(394, 283);
			this.messageTab.TabIndex = 3;
			this.messageTab.UseVisualStyleBackColor = true;
			// 
			// msgMsgText
			// 
			this.msgMsgText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.msgMsgText.Location = new System.Drawing.Point(65, 51);
			this.msgMsgText.Multiline = true;
			this.msgMsgText.Name = "msgMsgText";
			this.msgMsgText.Size = new System.Drawing.Size(326, 229);
			this.msgMsgText.TabIndex = 6;
			// 
			// msgMsgLabel
			// 
			this.msgMsgLabel.AutoSize = true;
			this.msgMsgLabel.Location = new System.Drawing.Point(3, 54);
			this.msgMsgLabel.Name = "msgMsgLabel";
			this.msgMsgLabel.Size = new System.Drawing.Size(53, 13);
			this.msgMsgLabel.TabIndex = 5;
			this.msgMsgLabel.Text = "Message:";
			// 
			// msgTitleText
			// 
			this.msgTitleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.msgTitleText.Location = new System.Drawing.Point(65, 25);
			this.msgTitleText.Name = "msgTitleText";
			this.msgTitleText.Size = new System.Drawing.Size(326, 20);
			this.msgTitleText.TabIndex = 7;
			// 
			// msgTitleLabel
			// 
			this.msgTitleLabel.AutoSize = true;
			this.msgTitleLabel.Location = new System.Drawing.Point(3, 28);
			this.msgTitleLabel.Name = "msgTitleLabel";
			this.msgTitleLabel.Size = new System.Drawing.Size(30, 13);
			this.msgTitleLabel.TabIndex = 4;
			this.msgTitleLabel.Text = "Title:";
			// 
			// msgIntroLabel
			// 
			this.msgIntroLabel.AutoSize = true;
			this.msgIntroLabel.Location = new System.Drawing.Point(3, 0);
			this.msgIntroLabel.Name = "msgIntroLabel";
			this.msgIntroLabel.Size = new System.Drawing.Size(250, 13);
			this.msgIntroLabel.TabIndex = 0;
			this.msgIntroLabel.Text = "This action displays a message box on the desktop.";
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(264, 399);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 23);
			this.okBtn.TabIndex = 4;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(345, 399);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 4;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// actionsLabel
			// 
			this.actionsLabel.AutoSize = true;
			this.actionsLabel.Location = new System.Drawing.Point(12, 56);
			this.actionsLabel.Name = "actionsLabel";
			this.actionsLabel.Size = new System.Drawing.Size(40, 13);
			this.actionsLabel.TabIndex = 0;
			this.actionsLabel.Text = "Action:";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// comTab
			// 
			this.comTab.Controls.Add(this.comDataText);
			this.comTab.Controls.Add(this.comDataLabel);
			this.comTab.Controls.Add(this.comCLSIDText);
			this.comTab.Controls.Add(this.comCLSIDLabel);
			this.comTab.Controls.Add(this.comIntroLabel);
			this.comTab.Location = new System.Drawing.Point(4, 5);
			this.comTab.Name = "comTab";
			this.comTab.Size = new System.Drawing.Size(394, 283);
			this.comTab.TabIndex = 4;
			this.comTab.UseVisualStyleBackColor = true;
			// 
			// comCLSIDText
			// 
			this.comCLSIDText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comCLSIDText.Location = new System.Drawing.Point(101, 25);
			this.comCLSIDText.Name = "comCLSIDText";
			this.comCLSIDText.Size = new System.Drawing.Size(290, 20);
			this.comCLSIDText.TabIndex = 10;
			this.comCLSIDText.Validating += new System.ComponentModel.CancelEventHandler(this.comCLSIDText_Validating);
			// 
			// comCLSIDLabel
			// 
			this.comCLSIDLabel.AutoSize = true;
			this.comCLSIDLabel.Location = new System.Drawing.Point(3, 28);
			this.comCLSIDLabel.Name = "comCLSIDLabel";
			this.comCLSIDLabel.Size = new System.Drawing.Size(76, 13);
			this.comCLSIDLabel.TabIndex = 9;
			this.comCLSIDLabel.Text = "COM Class ID:";
			// 
			// comIntroLabel
			// 
			this.comIntroLabel.AutoSize = true;
			this.comIntroLabel.Location = new System.Drawing.Point(3, 0);
			this.comIntroLabel.Name = "comIntroLabel";
			this.comIntroLabel.Size = new System.Drawing.Size(196, 13);
			this.comIntroLabel.TabIndex = 8;
			this.comIntroLabel.Text = "This action runs a custom COM handler.";
			// 
			// comDataLabel
			// 
			this.comDataLabel.AutoSize = true;
			this.comDataLabel.Location = new System.Drawing.Point(3, 54);
			this.comDataLabel.Name = "comDataLabel";
			this.comDataLabel.Size = new System.Drawing.Size(33, 13);
			this.comDataLabel.TabIndex = 9;
			this.comDataLabel.Text = "Data:";
			// 
			// comDataText
			// 
			this.comDataText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comDataText.Location = new System.Drawing.Point(101, 51);
			this.comDataText.Name = "comDataText";
			this.comDataText.Size = new System.Drawing.Size(290, 20);
			this.comDataText.TabIndex = 10;
			// 
			// ActionEditDialog
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(432, 434);
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
			this.Text = "New Action";
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