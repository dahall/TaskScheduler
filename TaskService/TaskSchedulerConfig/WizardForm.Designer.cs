namespace TaskSchedulerConfig
{
	partial class WizardForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardForm));
			this.wizardControl1 = new AeroWizard.WizardControl();
			this.introWizPg = new AeroWizard.WizardPage();
			this.runAsAdminPrompt = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.autoRepairCheck = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.detectWizPg = new AeroWizard.WizardPage();
			this.scanLocalStatusLabel = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.completeNoProbWizPg = new AeroWizard.WizardPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.closeBtn = new TaskSchedulerConfig.CommandLink();
			this.connRemoteBtn = new TaskSchedulerConfig.CommandLink();
			this.label5 = new System.Windows.Forms.Label();
			this.explOptionsBtn = new TaskSchedulerConfig.CommandLink();
			this.completeWithProbWizPg = new AeroWizard.WizardPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.localResultLabel = new System.Windows.Forms.Label();
			this.localCloseBtn = new TaskSchedulerConfig.CommandLink();
			this.remoteConnBtn = new TaskSchedulerConfig.CommandLink();
			this.issueList = new System.Windows.Forms.Panel();
			this.reportWizPg = new AeroWizard.WizardPage();
			this.localConfigList = new System.Windows.Forms.Panel();
			this.selectRemoteWizPg = new AeroWizard.WizardPage();
			this.computerBrowseBtn = new System.Windows.Forms.Button();
			this.remoteSvrText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.scanner = new System.ComponentModel.BackgroundWorker();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
			this.introWizPg.SuspendLayout();
			this.runAsAdminPrompt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.detectWizPg.SuspendLayout();
			this.completeNoProbWizPg.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.completeWithProbWizPg.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.reportWizPg.SuspendLayout();
			this.selectRemoteWizPg.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardControl1
			// 
			this.wizardControl1.BackColor = System.Drawing.Color.White;
			this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wizardControl1.FinishButtonText = "C&lose";
			this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.wizardControl1.Location = new System.Drawing.Point(0, 0);
			this.wizardControl1.Name = "wizardControl1";
			this.wizardControl1.Pages.Add(this.introWizPg);
			this.wizardControl1.Pages.Add(this.detectWizPg);
			this.wizardControl1.Pages.Add(this.completeNoProbWizPg);
			this.wizardControl1.Pages.Add(this.completeWithProbWizPg);
			this.wizardControl1.Pages.Add(this.reportWizPg);
			this.wizardControl1.Pages.Add(this.selectRemoteWizPg);
			this.wizardControl1.Size = new System.Drawing.Size(562, 415);
			this.wizardControl1.TabIndex = 0;
			this.wizardControl1.Title = "Task Scheduler Diagnostics";
			this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
			this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
			this.wizardControl1.Finished += new System.EventHandler(this.closeBtn_Click);
			// 
			// introWizPg
			// 
			this.introWizPg.Controls.Add(this.runAsAdminPrompt);
			this.introWizPg.Controls.Add(this.autoRepairCheck);
			this.introWizPg.Controls.Add(this.label3);
			this.introWizPg.Controls.Add(this.label2);
			this.introWizPg.Controls.Add(this.pictureBox1);
			this.introWizPg.Name = "introWizPg";
			this.introWizPg.Size = new System.Drawing.Size(515, 261);
			this.introWizPg.TabIndex = 6;
			this.introWizPg.Text = "Troubleshoot and help prevent computer problems";
			this.introWizPg.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.introWizPg_Commit);
			this.introWizPg.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.intro_Initialize);
			// 
			// runAsAdminPrompt
			// 
			this.runAsAdminPrompt.AutoSize = true;
			this.runAsAdminPrompt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.runAsAdminPrompt.ColumnCount = 2;
			this.runAsAdminPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.runAsAdminPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.runAsAdminPrompt.Controls.Add(this.label4, 0, 0);
			this.runAsAdminPrompt.Controls.Add(this.linkLabel1, 1, 1);
			this.runAsAdminPrompt.Controls.Add(this.pictureBox2, 0, 1);
			this.runAsAdminPrompt.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.runAsAdminPrompt.Location = new System.Drawing.Point(0, 202);
			this.runAsAdminPrompt.Name = "runAsAdminPrompt";
			this.runAsAdminPrompt.RowCount = 2;
			this.runAsAdminPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.runAsAdminPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.runAsAdminPrompt.Size = new System.Drawing.Size(515, 40);
			this.runAsAdminPrompt.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.runAsAdminPrompt.SetColumnSpan(this.label4, 2);
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(387, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Troubleshooting with administrator permissions might find more issues.";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.Location = new System.Drawing.Point(24, 18);
			this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(116, 15);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Run as administrator";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::TaskSchedulerConfig.Properties.Resources.shield;
			this.pictureBox2.Location = new System.Drawing.Point(5, 18);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(5, 0, 0, 6);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(16, 16);
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			// 
			// autoRepairCheck
			// 
			this.autoRepairCheck.AutoSize = true;
			this.autoRepairCheck.Checked = true;
			this.autoRepairCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autoRepairCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.autoRepairCheck.Location = new System.Drawing.Point(0, 242);
			this.autoRepairCheck.Name = "autoRepairCheck";
			this.autoRepairCheck.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.autoRepairCheck.Size = new System.Drawing.Size(515, 19);
			this.autoRepairCheck.TabIndex = 1;
			this.autoRepairCheck.Text = "Apply repairs automatically";
			this.autoRepairCheck.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(39, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(441, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Find and fix problems with Task Scheduler connecting to this and other computers";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.label2.Location = new System.Drawing.Point(39, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(186, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Task Scheduler Diagnostics";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// detectWizPg
			// 
			this.detectWizPg.AllowBack = false;
			this.detectWizPg.Controls.Add(this.scanLocalStatusLabel);
			this.detectWizPg.Controls.Add(this.progressBar1);
			this.detectWizPg.Name = "detectWizPg";
			this.detectWizPg.ShowNext = false;
			this.detectWizPg.Size = new System.Drawing.Size(515, 261);
			this.detectWizPg.TabIndex = 0;
			this.detectWizPg.Text = "Detecting problems";
			this.detectWizPg.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.scanLocal_Initialize);
			// 
			// scanLocalStatusLabel
			// 
			this.scanLocalStatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.scanLocalStatusLabel.Location = new System.Drawing.Point(0, 0);
			this.scanLocalStatusLabel.Name = "scanLocalStatusLabel";
			this.scanLocalStatusLabel.Size = new System.Drawing.Size(515, 37);
			this.scanLocalStatusLabel.TabIndex = 2;
			this.scanLocalStatusLabel.Text = "Running diagnosis...";
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(47, 40);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(417, 16);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 1;
			// 
			// completeNoProbWizPg
			// 
			this.completeNoProbWizPg.Controls.Add(this.tableLayoutPanel2);
			this.completeNoProbWizPg.HelpText = "View detailed information";
			this.completeNoProbWizPg.IsFinishPage = true;
			this.completeNoProbWizPg.Name = "completeNoProbWizPg";
			this.completeNoProbWizPg.ShowNext = false;
			this.completeNoProbWizPg.Size = new System.Drawing.Size(515, 261);
			this.completeNoProbWizPg.TabIndex = 7;
			this.completeNoProbWizPg.Text = "Troubleshooting didn\'t identify any problems";
			this.completeNoProbWizPg.HelpClicked += new System.EventHandler(this.showLocalResults_HelpClicked);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.closeBtn, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.connRemoteBtn, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.explOptionsBtn, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(515, 186);
			this.tableLayoutPanel2.TabIndex = 10;
			// 
			// closeBtn
			// 
			this.closeBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.closeBtn.Location = new System.Drawing.Point(3, 137);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(509, 46);
			this.closeBtn.TabIndex = 10;
			this.closeBtn.Text = "Close the troubleshooter";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// connRemoteBtn
			// 
			this.connRemoteBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.connRemoteBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.connRemoteBtn.Location = new System.Drawing.Point(3, 85);
			this.connRemoteBtn.Name = "connRemoteBtn";
			this.connRemoteBtn.Size = new System.Drawing.Size(509, 46);
			this.connRemoteBtn.TabIndex = 9;
			this.connRemoteBtn.Text = "Troubleshoot connecting to a remote computer";
			this.connRemoteBtn.UseVisualStyleBackColor = true;
			this.connRemoteBtn.Click += new System.EventHandler(this.connRemoteBtn_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(509, 30);
			this.label5.TabIndex = 6;
			this.label5.Text = "You can try exploring other options that might be helpful.";
			// 
			// explOptionsBtn
			// 
			this.explOptionsBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.explOptionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.explOptionsBtn.Location = new System.Drawing.Point(3, 33);
			this.explOptionsBtn.Name = "explOptionsBtn";
			this.explOptionsBtn.Size = new System.Drawing.Size(509, 46);
			this.explOptionsBtn.TabIndex = 7;
			this.explOptionsBtn.Text = "Explore additional options";
			this.explOptionsBtn.UseVisualStyleBackColor = true;
			this.explOptionsBtn.Click += new System.EventHandler(this.explOptionsBtn_Click);
			// 
			// completeWithProbWizPg
			// 
			this.completeWithProbWizPg.AllowBack = false;
			this.completeWithProbWizPg.AllowCancel = false;
			this.completeWithProbWizPg.AllowNext = false;
			this.completeWithProbWizPg.Controls.Add(this.tableLayoutPanel1);
			this.completeWithProbWizPg.HelpText = "View detailed information";
			this.completeWithProbWizPg.Name = "completeWithProbWizPg";
			this.completeWithProbWizPg.ShowCancel = false;
			this.completeWithProbWizPg.ShowNext = false;
			this.completeWithProbWizPg.Size = new System.Drawing.Size(515, 261);
			this.completeWithProbWizPg.TabIndex = 1;
			this.completeWithProbWizPg.Text = "Troubleshooting has completed";
			this.completeWithProbWizPg.HelpClicked += new System.EventHandler(this.showLocalResults_HelpClicked);
			this.completeWithProbWizPg.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.completeWithProbWizPg_Initialize);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.localResultLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.localCloseBtn, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.remoteConnBtn, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.issueList, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 234);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// localResultLabel
			// 
			this.localResultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.localResultLabel.Location = new System.Drawing.Point(3, 0);
			this.localResultLabel.Name = "localResultLabel";
			this.localResultLabel.Size = new System.Drawing.Size(509, 34);
			this.localResultLabel.TabIndex = 6;
			this.localResultLabel.Text = "Troubleshooting was able to find the following problems. To fix a problem, check " +
	"the resolution below each problem\'s description.";
			// 
			// localCloseBtn
			// 
			this.localCloseBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.localCloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.localCloseBtn.Location = new System.Drawing.Point(3, 188);
			this.localCloseBtn.Name = "localCloseBtn";
			this.localCloseBtn.Size = new System.Drawing.Size(509, 43);
			this.localCloseBtn.TabIndex = 7;
			this.localCloseBtn.Text = "Close the troubleshooter";
			this.localCloseBtn.UseVisualStyleBackColor = true;
			this.localCloseBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// remoteConnBtn
			// 
			this.remoteConnBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.remoteConnBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.remoteConnBtn.Location = new System.Drawing.Point(3, 139);
			this.remoteConnBtn.Name = "remoteConnBtn";
			this.remoteConnBtn.Size = new System.Drawing.Size(509, 43);
			this.remoteConnBtn.TabIndex = 7;
			this.remoteConnBtn.Text = "Troubleshoot connecting to a remote computer";
			this.remoteConnBtn.UseVisualStyleBackColor = true;
			this.remoteConnBtn.Click += new System.EventHandler(this.connRemoteBtn_Click);
			// 
			// issueList
			// 
			this.issueList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.issueList.AutoScroll = true;
			this.issueList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.issueList.Location = new System.Drawing.Point(3, 37);
			this.issueList.Name = "issueList";
			this.issueList.Size = new System.Drawing.Size(509, 96);
			this.issueList.TabIndex = 8;
			// 
			// reportWizPg
			// 
			this.reportWizPg.Controls.Add(this.localConfigList);
			this.reportWizPg.IsFinishPage = true;
			this.reportWizPg.Name = "reportWizPg";
			this.reportWizPg.NextPage = this.completeWithProbWizPg;
			this.reportWizPg.ShowNext = false;
			this.reportWizPg.Size = new System.Drawing.Size(515, 261);
			this.reportWizPg.TabIndex = 5;
			this.reportWizPg.Text = "Troubleshooting report";
			this.reportWizPg.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.reportWizPg_Initialize);
			// 
			// localConfigList
			// 
			this.localConfigList.AutoScroll = true;
			this.localConfigList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.localConfigList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.localConfigList.Location = new System.Drawing.Point(0, 0);
			this.localConfigList.Name = "localConfigList";
			this.localConfigList.Size = new System.Drawing.Size(515, 261);
			this.localConfigList.TabIndex = 1;
			// 
			// selectRemoteWizPg
			// 
			this.selectRemoteWizPg.Controls.Add(this.computerBrowseBtn);
			this.selectRemoteWizPg.Controls.Add(this.remoteSvrText);
			this.selectRemoteWizPg.Controls.Add(this.label1);
			this.selectRemoteWizPg.Name = "selectRemoteWizPg";
			this.selectRemoteWizPg.NextPage = this.detectWizPg;
			this.selectRemoteWizPg.Size = new System.Drawing.Size(515, 261);
			this.selectRemoteWizPg.Suppress = true;
			this.selectRemoteWizPg.TabIndex = 2;
			this.selectRemoteWizPg.Text = "Select remote computer to troubleshoot";
			this.selectRemoteWizPg.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.selectRemote_Commit);
			// 
			// computerBrowseBtn
			// 
			this.computerBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.computerBrowseBtn.Enabled = false;
			this.computerBrowseBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.computerBrowseBtn.Location = new System.Drawing.Point(269, 42);
			this.computerBrowseBtn.Name = "computerBrowseBtn";
			this.computerBrowseBtn.Size = new System.Drawing.Size(75, 24);
			this.computerBrowseBtn.TabIndex = 7;
			this.computerBrowseBtn.Text = "Browse...";
			this.computerBrowseBtn.UseVisualStyleBackColor = true;
			// 
			// remoteSvrText
			// 
			this.remoteSvrText.Location = new System.Drawing.Point(4, 43);
			this.remoteSvrText.Name = "remoteSvrText";
			this.remoteSvrText.Size = new System.Drawing.Size(259, 23);
			this.remoteSvrText.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(515, 39);
			this.label1.TabIndex = 5;
			this.label1.Text = "Select a remote computer for the troubleshooter to find problems with connecting " +
	"to it and accessing the Task Scheduler on that computer.";
			// 
			// scanner
			// 
			this.scanner.WorkerReportsProgress = true;
			this.scanner.WorkerSupportsCancellation = true;
			this.scanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.scanner_DoWork);
			this.scanner.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.scanner_ProgressChanged);
			this.scanner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.scanner_RunWorkerCompleted);
			// 
			// WizardForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(562, 415);
			this.Controls.Add(this.wizardControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "WizardForm";
			this.Load += new System.EventHandler(this.WizardForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
			this.introWizPg.ResumeLayout(false);
			this.introWizPg.PerformLayout();
			this.runAsAdminPrompt.ResumeLayout(false);
			this.runAsAdminPrompt.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.detectWizPg.ResumeLayout(false);
			this.completeNoProbWizPg.ResumeLayout(false);
			this.completeNoProbWizPg.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.completeWithProbWizPg.ResumeLayout(false);
			this.completeWithProbWizPg.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.reportWizPg.ResumeLayout(false);
			this.selectRemoteWizPg.ResumeLayout(false);
			this.selectRemoteWizPg.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private AeroWizard.WizardControl wizardControl1;
		private AeroWizard.WizardPage detectWizPg;
		private AeroWizard.WizardPage completeWithProbWizPg;
		private AeroWizard.WizardPage selectRemoteWizPg;
		private System.Windows.Forms.Label scanLocalStatusLabel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox remoteSvrText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button computerBrowseBtn;
		private TaskSchedulerConfig.CommandLink remoteConnBtn;
		private TaskSchedulerConfig.CommandLink localCloseBtn;
		private System.Windows.Forms.Label localResultLabel;
		private AeroWizard.WizardPage reportWizPg;
		private System.Windows.Forms.Panel localConfigList;
		private System.ComponentModel.BackgroundWorker scanner;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private AeroWizard.WizardPage introWizPg;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel runAsAdminPrompt;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.CheckBox autoRepairCheck;
		private AeroWizard.WizardPage completeNoProbWizPg;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private CommandLink closeBtn;
		private CommandLink connRemoteBtn;
		private CommandLink explOptionsBtn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel issueList;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}