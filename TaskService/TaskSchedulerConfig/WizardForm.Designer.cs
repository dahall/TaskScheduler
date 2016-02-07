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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardForm));
			this.wizardControl1 = new AeroWizard.WizardControl();
			this.intro = new AeroWizard.WizardPage();
			this.runAsAdminPrompt = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.autoRepairCheck = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.scanLocal = new AeroWizard.WizardPage();
			this.scanLocalStatusLabel = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.wizardPage1 = new AeroWizard.WizardPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.closeBtn = new TaskSchedulerConfig.CommandLink();
			this.connRemoteBtn = new TaskSchedulerConfig.CommandLink();
			this.label5 = new System.Windows.Forms.Label();
			this.explOptionsBtn = new TaskSchedulerConfig.CommandLink();
			this.showLocalResults = new AeroWizard.WizardPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.localResultLabel = new System.Windows.Forms.Label();
			this.localCloseBtn = new TaskSchedulerConfig.CommandLink();
			this.remoteConnBtn = new TaskSchedulerConfig.CommandLink();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.localReport = new AeroWizard.WizardPage();
			this.localConfigList = new System.Windows.Forms.ListView();
			this.selectRemote = new AeroWizard.WizardPage();
			this.computerBrowseBtn = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.scanRemote = new AeroWizard.WizardPage();
			this.remoteStatusLabel = new System.Windows.Forms.Label();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.showRemoteResults = new AeroWizard.WizardPage();
			this.localScanner = new System.ComponentModel.BackgroundWorker();
			this.remoteScanner = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
			this.intro.SuspendLayout();
			this.runAsAdminPrompt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.scanLocal.SuspendLayout();
			this.wizardPage1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.showLocalResults.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.localReport.SuspendLayout();
			this.selectRemote.SuspendLayout();
			this.scanRemote.SuspendLayout();
			this.SuspendLayout();
			// 
			// wizardControl1
			// 
			this.wizardControl1.BackColor = System.Drawing.Color.White;
			this.wizardControl1.ClassicStyle = AeroWizard.WizardClassicStyle.Automatic;
			this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wizardControl1.FinishButtonText = "C&lose";
			this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.wizardControl1.Location = new System.Drawing.Point(0, 0);
			this.wizardControl1.Name = "wizardControl1";
			this.wizardControl1.Pages.Add(this.intro);
			this.wizardControl1.Pages.Add(this.scanLocal);
			this.wizardControl1.Pages.Add(this.wizardPage1);
			this.wizardControl1.Pages.Add(this.showLocalResults);
			this.wizardControl1.Pages.Add(this.localReport);
			this.wizardControl1.Pages.Add(this.selectRemote);
			this.wizardControl1.Pages.Add(this.scanRemote);
			this.wizardControl1.Pages.Add(this.showRemoteResults);
			this.wizardControl1.Size = new System.Drawing.Size(562, 415);
			this.wizardControl1.TabIndex = 0;
			this.wizardControl1.Title = "Task Scheduler Diagnostics";
			this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
			this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
			// 
			// intro
			// 
			this.intro.Controls.Add(this.runAsAdminPrompt);
			this.intro.Controls.Add(this.autoRepairCheck);
			this.intro.Controls.Add(this.label3);
			this.intro.Controls.Add(this.label2);
			this.intro.Controls.Add(this.pictureBox1);
			this.intro.Name = "intro";
			this.intro.Size = new System.Drawing.Size(515, 261);
			this.intro.TabIndex = 6;
			this.intro.Text = "Troubleshoot and help prevent computer problems";
			this.intro.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.intro_Initialize);
			// 
			// runAsAdminPrompt
			// 
			this.runAsAdminPrompt.AutoSize = true;
			this.runAsAdminPrompt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.runAsAdminPrompt.ColumnCount = 1;
			this.runAsAdminPrompt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.runAsAdminPrompt.Controls.Add(this.label4, 0, 0);
			this.runAsAdminPrompt.Controls.Add(this.linkLabel1, 0, 1);
			this.runAsAdminPrompt.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.runAsAdminPrompt.Location = new System.Drawing.Point(0, 206);
			this.runAsAdminPrompt.Name = "runAsAdminPrompt";
			this.runAsAdminPrompt.RowCount = 2;
			this.runAsAdminPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.runAsAdminPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.runAsAdminPrompt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.runAsAdminPrompt.Size = new System.Drawing.Size(515, 36);
			this.runAsAdminPrompt.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(388, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Troubleshooting with administrator permissions might find more issues.";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Image = global::TaskSchedulerConfig.Properties.Resources.shield;
			this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel1.Location = new System.Drawing.Point(3, 18);
			this.linkLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(137, 15);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "       Run as administrator";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// autoRepairCheck
			// 
			this.autoRepairCheck.AutoSize = true;
			this.autoRepairCheck.Checked = true;
			this.autoRepairCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autoRepairCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.autoRepairCheck.Location = new System.Drawing.Point(0, 242);
			this.autoRepairCheck.Name = "autoRepairCheck";
			this.autoRepairCheck.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
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
			this.label3.Size = new System.Drawing.Size(442, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Find and fix problems with Task Scheduler connecting to this and other computers";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.label2.Location = new System.Drawing.Point(39, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188, 20);
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
			// scanLocal
			// 
			this.scanLocal.Controls.Add(this.scanLocalStatusLabel);
			this.scanLocal.Controls.Add(this.progressBar1);
			this.scanLocal.Name = "scanLocal";
			this.scanLocal.ShowNext = false;
			this.scanLocal.Size = new System.Drawing.Size(515, 261);
			this.scanLocal.TabIndex = 0;
			this.scanLocal.Text = "Detecting problems";
			this.scanLocal.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.scanLocal_Initialize);
			// 
			// scanLocalStatusLabel
			// 
			this.scanLocalStatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.scanLocalStatusLabel.Location = new System.Drawing.Point(0, 0);
			this.scanLocalStatusLabel.Name = "scanLocalStatusLabel";
			this.scanLocalStatusLabel.Size = new System.Drawing.Size(515, 37);
			this.scanLocalStatusLabel.TabIndex = 2;
			this.scanLocalStatusLabel.Text = "label1";
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
			// wizardPage1
			// 
			this.wizardPage1.Controls.Add(this.tableLayoutPanel2);
			this.wizardPage1.HelpText = "View detailed information";
			this.wizardPage1.IsFinishPage = true;
			this.wizardPage1.Name = "wizardPage1";
			this.wizardPage1.ShowNext = false;
			this.wizardPage1.Size = new System.Drawing.Size(515, 261);
			this.wizardPage1.TabIndex = 7;
			this.wizardPage1.Text = "Troubleshooting couldn\'t identify the problem";
			this.wizardPage1.HelpClicked += new System.EventHandler(this.showLocalResults_HelpClicked);
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
			this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.closeBtn.Location = new System.Drawing.Point(3, 137);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(508, 46);
			this.closeBtn.TabIndex = 10;
			this.closeBtn.Text = "Close the troubleshooter";
			this.closeBtn.UseVisualStyleBackColor = true;
			// 
			// connRemoteBtn
			// 
			this.connRemoteBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.connRemoteBtn.Location = new System.Drawing.Point(3, 85);
			this.connRemoteBtn.Name = "connRemoteBtn";
			this.connRemoteBtn.Size = new System.Drawing.Size(508, 46);
			this.connRemoteBtn.TabIndex = 9;
			this.connRemoteBtn.Text = "Troubleshoot connecting to a remote computer";
			this.connRemoteBtn.UseVisualStyleBackColor = true;
			this.connRemoteBtn.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(509, 30);
			this.label5.TabIndex = 6;
			this.label5.Text = "You can try exploring other options that might be helpful.";
			// 
			// explOptionsBtn
			// 
			this.explOptionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.explOptionsBtn.Location = new System.Drawing.Point(3, 33);
			this.explOptionsBtn.Name = "explOptionsBtn";
			this.explOptionsBtn.Size = new System.Drawing.Size(508, 46);
			this.explOptionsBtn.TabIndex = 7;
			this.explOptionsBtn.Text = "Explore additional options";
			this.explOptionsBtn.UseVisualStyleBackColor = true;
			this.explOptionsBtn.Click += new System.EventHandler(this.explOptionsBtn_Click);
			// 
			// showLocalResults
			// 
			this.showLocalResults.Controls.Add(this.tableLayoutPanel1);
			this.showLocalResults.HelpText = "View detailed information";
			this.showLocalResults.IsFinishPage = true;
			this.showLocalResults.Name = "showLocalResults";
			this.showLocalResults.ShowNext = false;
			this.showLocalResults.Size = new System.Drawing.Size(515, 261);
			this.showLocalResults.TabIndex = 1;
			this.showLocalResults.Text = "Troubleshooting has completed";
			this.showLocalResults.HelpClicked += new System.EventHandler(this.showLocalResults_HelpClicked);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.localResultLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.localCloseBtn, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.remoteConnBtn, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 238);
			this.tableLayoutPanel1.TabIndex = 9;
			// 
			// localResultLabel
			// 
			this.localResultLabel.Location = new System.Drawing.Point(3, 0);
			this.localResultLabel.Name = "localResultLabel";
			this.localResultLabel.Size = new System.Drawing.Size(509, 34);
			this.localResultLabel.TabIndex = 6;
			this.localResultLabel.Text = "Troubleshooting was able to find the following problems. To fix a problem, check " +
    "the resolution below each problem\'s description.";
			// 
			// localCloseBtn
			// 
			this.localCloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.localCloseBtn.Location = new System.Drawing.Point(3, 192);
			this.localCloseBtn.Name = "localCloseBtn";
			this.localCloseBtn.Size = new System.Drawing.Size(508, 43);
			this.localCloseBtn.TabIndex = 7;
			this.localCloseBtn.Text = "Close the troubleshooter";
			this.localCloseBtn.UseVisualStyleBackColor = true;
			this.localCloseBtn.Click += new System.EventHandler(this.localCloseBtn_Click);
			// 
			// remoteConnBtn
			// 
			this.remoteConnBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.remoteConnBtn.Location = new System.Drawing.Point(3, 143);
			this.remoteConnBtn.Name = "remoteConnBtn";
			this.remoteConnBtn.Size = new System.Drawing.Size(508, 43);
			this.remoteConnBtn.TabIndex = 7;
			this.remoteConnBtn.Text = "Troubleshoot connecting to a remote computer";
			this.remoteConnBtn.UseVisualStyleBackColor = true;
			this.remoteConnBtn.Click += new System.EventHandler(this.remoteConnBtn_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel1.Controls.Add(this.button1);
			this.flowLayoutPanel1.Controls.Add(this.button2);
			this.flowLayoutPanel1.Controls.Add(this.button3);
			this.flowLayoutPanel1.Controls.Add(this.button4);
			this.flowLayoutPanel1.Controls.Add(this.button5);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 37);
			this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(2, 100);
			this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(2, 20);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(2, 100);
			this.flowLayoutPanel1.TabIndex = 8;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(500, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(3, 32);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(500, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(3, 61);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(500, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "button3";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(3, 90);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(500, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(3, 119);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(500, 23);
			this.button5.TabIndex = 4;
			this.button5.Text = "button5";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// localReport
			// 
			this.localReport.Controls.Add(this.localConfigList);
			this.localReport.IsFinishPage = true;
			this.localReport.Name = "localReport";
			this.localReport.NextPage = this.showLocalResults;
			this.localReport.Size = new System.Drawing.Size(515, 261);
			this.localReport.Suppress = true;
			this.localReport.TabIndex = 5;
			this.localReport.Text = "Troubleshooting report";
			// 
			// localConfigList
			// 
			this.localConfigList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.localConfigList.FullRowSelect = true;
			this.localConfigList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.localConfigList.Location = new System.Drawing.Point(0, 0);
			this.localConfigList.MultiSelect = false;
			this.localConfigList.Name = "localConfigList";
			this.localConfigList.Size = new System.Drawing.Size(515, 261);
			this.localConfigList.TabIndex = 1;
			this.localConfigList.UseCompatibleStateImageBehavior = false;
			this.localConfigList.View = System.Windows.Forms.View.Details;
			// 
			// selectRemote
			// 
			this.selectRemote.Controls.Add(this.computerBrowseBtn);
			this.selectRemote.Controls.Add(this.textBox1);
			this.selectRemote.Controls.Add(this.label1);
			this.selectRemote.Name = "selectRemote";
			this.selectRemote.Size = new System.Drawing.Size(515, 261);
			this.selectRemote.TabIndex = 2;
			this.selectRemote.Text = "Select remote computer to troubleshoot";
			this.selectRemote.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.selectRemote_Commit);
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
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(4, 43);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(259, 23);
			this.textBox1.TabIndex = 6;
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
			// scanRemote
			// 
			this.scanRemote.Controls.Add(this.remoteStatusLabel);
			this.scanRemote.Controls.Add(this.progressBar2);
			this.scanRemote.Name = "scanRemote";
			this.scanRemote.ShowNext = false;
			this.scanRemote.Size = new System.Drawing.Size(515, 261);
			this.scanRemote.TabIndex = 3;
			this.scanRemote.Text = "Detecting remote computer problems";
			this.scanRemote.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.scanRemote_Initialize);
			// 
			// remoteStatusLabel
			// 
			this.remoteStatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.remoteStatusLabel.Location = new System.Drawing.Point(0, 0);
			this.remoteStatusLabel.Name = "remoteStatusLabel";
			this.remoteStatusLabel.Size = new System.Drawing.Size(515, 37);
			this.remoteStatusLabel.TabIndex = 4;
			this.remoteStatusLabel.Text = "label1";
			// 
			// progressBar2
			// 
			this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar2.Location = new System.Drawing.Point(47, 40);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(417, 16);
			this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar2.TabIndex = 3;
			// 
			// showRemoteResults
			// 
			this.showRemoteResults.IsFinishPage = true;
			this.showRemoteResults.Name = "showRemoteResults";
			this.showRemoteResults.ShowNext = false;
			this.showRemoteResults.Size = new System.Drawing.Size(515, 261);
			this.showRemoteResults.TabIndex = 4;
			this.showRemoteResults.Text = "Remote computer troubleshooting has completed";
			// 
			// localScanner
			// 
			this.localScanner.WorkerReportsProgress = true;
			this.localScanner.WorkerSupportsCancellation = true;
			this.localScanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.localScanner_DoWork);
			this.localScanner.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.localScanner_ProgressChanged);
			this.localScanner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.localScanner_RunWorkerCompleted);
			// 
			// remoteScanner
			// 
			this.remoteScanner.WorkerReportsProgress = true;
			this.remoteScanner.WorkerSupportsCancellation = true;
			this.remoteScanner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.remoteScanner_DoWork);
			this.remoteScanner.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.remoteScanner_ProgressChanged);
			this.remoteScanner.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.remoteScanner_RunWorkerCompleted);
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
			this.intro.ResumeLayout(false);
			this.intro.PerformLayout();
			this.runAsAdminPrompt.ResumeLayout(false);
			this.runAsAdminPrompt.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.scanLocal.ResumeLayout(false);
			this.wizardPage1.ResumeLayout(false);
			this.wizardPage1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.showLocalResults.ResumeLayout(false);
			this.showLocalResults.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.localReport.ResumeLayout(false);
			this.selectRemote.ResumeLayout(false);
			this.selectRemote.PerformLayout();
			this.scanRemote.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private AeroWizard.WizardControl wizardControl1;
		private AeroWizard.WizardPage scanLocal;
		private AeroWizard.WizardPage showLocalResults;
		private AeroWizard.WizardPage selectRemote;
		private System.Windows.Forms.Label scanLocalStatusLabel;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private AeroWizard.WizardPage scanRemote;
		private System.Windows.Forms.Label remoteStatusLabel;
		private System.Windows.Forms.ProgressBar progressBar2;
		private AeroWizard.WizardPage showRemoteResults;
		private System.Windows.Forms.Button computerBrowseBtn;
		private TaskSchedulerConfig.CommandLink remoteConnBtn;
		private TaskSchedulerConfig.CommandLink localCloseBtn;
		private System.Windows.Forms.Label localResultLabel;
		private AeroWizard.WizardPage localReport;
		private System.Windows.Forms.ListView localConfigList;
		private System.ComponentModel.BackgroundWorker localScanner;
		private System.ComponentModel.BackgroundWorker remoteScanner;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private AeroWizard.WizardPage intro;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel runAsAdminPrompt;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.CheckBox autoRepairCheck;
		private AeroWizard.WizardPage wizardPage1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private CommandLink closeBtn;
		private CommandLink connRemoteBtn;
		private CommandLink explOptionsBtn;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
	}
}