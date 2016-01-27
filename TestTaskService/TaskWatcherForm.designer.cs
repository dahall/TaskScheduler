namespace TestTaskService
{
	partial class TaskWatcherForm
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
			this.connectionLink = new System.Windows.Forms.LinkLabel();
			this.folderButton = new System.Windows.Forms.Button();
			this.taskButton = new System.Windows.Forms.Button();
			this.inclSubsCheck = new System.Windows.Forms.CheckBox();
			this.outputList = new System.Windows.Forms.ListBox();
			this.outputMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.watchButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.levelsText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.idsText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ts = new Microsoft.Win32.TaskScheduler.TaskService();
			this.taskEventWatcher = new Microsoft.Win32.TaskScheduler.TaskEventWatcher();
			this.folderLabel = new System.Windows.Forms.Label();
			this.taskLabel = new System.Windows.Forms.Label();
			this.folderCheck = new System.Windows.Forms.CheckBox();
			this.taskText = new System.Windows.Forms.TextBox();
			this.folderText = new System.Windows.Forms.TextBox();
			this.taskServiceConnectDialog = new Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog();
			this.taskBrowserDialog = new Microsoft.Win32.TaskScheduler.TaskBrowserDialog();
			this.folderBrowserDialog = new Microsoft.Win32.TaskScheduler.TaskBrowserDialog();
			this.outputMenuStrip.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.taskEventWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// connectionLink
			// 
			this.connectionLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.connectionLink.AutoSize = true;
			this.connectionLink.Location = new System.Drawing.Point(9, 253);
			this.connectionLink.Name = "connectionLink";
			this.connectionLink.Size = new System.Drawing.Size(109, 13);
			this.connectionLink.TabIndex = 9;
			this.connectionLink.TabStop = true;
			this.connectionLink.Text = "Change connection...";
			this.connectionLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.connectionLink_LinkClicked);
			// 
			// folderButton
			// 
			this.folderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.folderButton.Location = new System.Drawing.Point(253, 36);
			this.folderButton.Name = "folderButton";
			this.folderButton.Size = new System.Drawing.Size(25, 20);
			this.folderButton.TabIndex = 3;
			this.folderButton.Text = "...";
			this.folderButton.UseVisualStyleBackColor = true;
			this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
			// 
			// taskButton
			// 
			this.taskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.taskButton.Location = new System.Drawing.Point(253, 96);
			this.taskButton.Name = "taskButton";
			this.taskButton.Size = new System.Drawing.Size(25, 20);
			this.taskButton.TabIndex = 7;
			this.taskButton.Text = "...";
			this.taskButton.UseVisualStyleBackColor = true;
			this.taskButton.Click += new System.EventHandler(this.taskButton_Click);
			// 
			// inclSubsCheck
			// 
			this.inclSubsCheck.AutoSize = true;
			this.inclSubsCheck.Location = new System.Drawing.Point(55, 62);
			this.inclSubsCheck.Name = "inclSubsCheck";
			this.inclSubsCheck.Size = new System.Drawing.Size(112, 17);
			this.inclSubsCheck.TabIndex = 4;
			this.inclSubsCheck.Text = "Include subfolders";
			this.inclSubsCheck.UseVisualStyleBackColor = true;
			// 
			// outputList
			// 
			this.outputList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.outputList.ContextMenuStrip = this.outputMenuStrip;
			this.outputList.FormattingEnabled = true;
			this.outputList.IntegralHeight = false;
			this.outputList.Location = new System.Drawing.Point(12, 133);
			this.outputList.Name = "outputList";
			this.outputList.Size = new System.Drawing.Size(443, 102);
			this.outputList.TabIndex = 11;
			// 
			// outputMenuStrip
			// 
			this.outputMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.clearToolStripMenuItem});
			this.outputMenuStrip.Name = "contextMenuStrip1";
			this.outputMenuStrip.Size = new System.Drawing.Size(102, 26);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// watchButton
			// 
			this.watchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.watchButton.Location = new System.Drawing.Point(380, 248);
			this.watchButton.Name = "watchButton";
			this.watchButton.Size = new System.Drawing.Size(75, 23);
			this.watchButton.TabIndex = 10;
			this.watchButton.Text = "Watch";
			this.watchButton.UseVisualStyleBackColor = true;
			this.watchButton.Click += new System.EventHandler(this.watchButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.levelsText);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.idsText);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(285, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(170, 103);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filter";
			// 
			// levelsText
			// 
			this.levelsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.levelsText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TestTaskService.Properties.Settings.Default, "LevelsFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.levelsText.Location = new System.Drawing.Point(38, 45);
			this.levelsText.Name = "levelsText";
			this.levelsText.Size = new System.Drawing.Size(126, 20);
			this.levelsText.TabIndex = 3;
			this.levelsText.Text = global::TestTaskService.Properties.Settings.Default.LevelsFilter;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Lvls:";
			// 
			// idsText
			// 
			this.idsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.idsText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TestTaskService.Properties.Settings.Default, "IDsFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.idsText.Location = new System.Drawing.Point(38, 19);
			this.idsText.Name = "idsText";
			this.idsText.Size = new System.Drawing.Size(126, 20);
			this.idsText.TabIndex = 1;
			this.idsText.Text = global::TestTaskService.Properties.Settings.Default.IDsFilter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "IDs:";
			// 
			// taskEventWatcher
			// 
			this.taskEventWatcher.SynchronizingObject = this;
			this.taskEventWatcher.TaskService = this.ts;
			this.taskEventWatcher.EventRecorded += new System.EventHandler<Microsoft.Win32.TaskScheduler.TaskEventArgs>(this.taskEventWatcher_EventRecorded);
			// 
			// folderLabel
			// 
			this.folderLabel.AutoSize = true;
			this.folderLabel.Location = new System.Drawing.Point(10, 40);
			this.folderLabel.Name = "folderLabel";
			this.folderLabel.Size = new System.Drawing.Size(39, 13);
			this.folderLabel.TabIndex = 1;
			this.folderLabel.Text = "Folder:";
			// 
			// taskLabel
			// 
			this.taskLabel.AutoSize = true;
			this.taskLabel.Location = new System.Drawing.Point(10, 99);
			this.taskLabel.Name = "taskLabel";
			this.taskLabel.Size = new System.Drawing.Size(45, 13);
			this.taskLabel.TabIndex = 5;
			this.taskLabel.Text = "Task(s):";
			// 
			// folderCheck
			// 
			this.folderCheck.AutoSize = true;
			this.folderCheck.Checked = global::TestTaskService.Properties.Settings.Default.WatchFolder;
			this.folderCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.folderCheck.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TestTaskService.Properties.Settings.Default, "WatchFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.folderCheck.Location = new System.Drawing.Point(13, 13);
			this.folderCheck.Name = "folderCheck";
			this.folderCheck.Size = new System.Drawing.Size(87, 17);
			this.folderCheck.TabIndex = 0;
			this.folderCheck.Text = "Watch folder";
			this.folderCheck.UseVisualStyleBackColor = true;
			this.folderCheck.CheckedChanged += new System.EventHandler(this.folderCheck_CheckedChanged);
			// 
			// taskText
			// 
			this.taskText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.taskText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TestTaskService.Properties.Settings.Default, "TaskFilter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.taskText.Location = new System.Drawing.Point(55, 96);
			this.taskText.Name = "taskText";
			this.taskText.Size = new System.Drawing.Size(191, 20);
			this.taskText.TabIndex = 6;
			this.taskText.Text = global::TestTaskService.Properties.Settings.Default.TaskFilter;
			// 
			// folderText
			// 
			this.folderText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.folderText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TestTaskService.Properties.Settings.Default, "Folder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.folderText.Location = new System.Drawing.Point(55, 36);
			this.folderText.Name = "folderText";
			this.folderText.Size = new System.Drawing.Size(191, 20);
			this.folderText.TabIndex = 2;
			this.folderText.Text = global::TestTaskService.Properties.Settings.Default.Folder;
			// 
			// taskServiceConnectDialog
			// 
			this.taskServiceConnectDialog.AutoSize = true;
			this.taskServiceConnectDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskServiceConnectDialog.ClientSize = new System.Drawing.Size(444, 181);
			this.taskServiceConnectDialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskServiceConnectDialog.Location = new System.Drawing.Point(234, 234);
			this.taskServiceConnectDialog.Name = "TaskServiceConnectDialog";
			this.taskServiceConnectDialog.TaskService = this.ts;
			this.taskServiceConnectDialog.Text = "Select Computer";
			this.taskServiceConnectDialog.Visible = false;
			// 
			// taskBrowserDialog
			// 
			this.taskBrowserDialog.ClientSize = new System.Drawing.Size(356, 345);
			this.taskBrowserDialog.Description = "Select the task.";
			this.taskBrowserDialog.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskBrowserDialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskBrowserDialog.Location = new System.Drawing.Point(156, 156);
			this.taskBrowserDialog.Name = "TaskBrowserDialog";
			this.taskBrowserDialog.ShowInTaskbar = false;
			this.taskBrowserDialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.taskBrowserDialog.TaskService = this.ts;
			this.taskBrowserDialog.Text = "Browse for Task";
			this.taskBrowserDialog.Visible = false;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.AllowFolderSelection = true;
			this.folderBrowserDialog.ClientSize = new System.Drawing.Size(356, 345);
			this.folderBrowserDialog.Description = "Select the folder.";
			this.folderBrowserDialog.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.folderBrowserDialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.folderBrowserDialog.Location = new System.Drawing.Point(182, 182);
			this.folderBrowserDialog.Name = "TaskBrowserDialog";
			this.folderBrowserDialog.ShowInTaskbar = false;
			this.folderBrowserDialog.ShowTasks = false;
			this.folderBrowserDialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.folderBrowserDialog.TaskService = this.ts;
			this.folderBrowserDialog.Text = "Browse for Task";
			this.folderBrowserDialog.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 283);
			this.Controls.Add(this.taskLabel);
			this.Controls.Add(this.folderLabel);
			this.Controls.Add(this.folderCheck);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.watchButton);
			this.Controls.Add(this.outputList);
			this.Controls.Add(this.inclSubsCheck);
			this.Controls.Add(this.taskButton);
			this.Controls.Add(this.folderButton);
			this.Controls.Add(this.connectionLink);
			this.Controls.Add(this.taskText);
			this.Controls.Add(this.folderText);
			this.Name = "Form1";
			this.Text = "Test Task Watcher";
			this.outputMenuStrip.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.taskEventWatcher)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox folderText;
		private System.Windows.Forms.LinkLabel connectionLink;
		private System.Windows.Forms.TextBox taskText;
		private System.Windows.Forms.Button folderButton;
		private System.Windows.Forms.Button taskButton;
		private System.Windows.Forms.CheckBox inclSubsCheck;
		private System.Windows.Forms.ListBox outputList;
		private System.Windows.Forms.Button watchButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox idsText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox levelsText;
		private System.Windows.Forms.Label label2;
		private Microsoft.Win32.TaskScheduler.TaskService ts;
		private Microsoft.Win32.TaskScheduler.TaskEventWatcher taskEventWatcher;
		private System.Windows.Forms.Label taskLabel;
		private System.Windows.Forms.Label folderLabel;
		private System.Windows.Forms.CheckBox folderCheck;
		private Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog taskServiceConnectDialog;
		private Microsoft.Win32.TaskScheduler.TaskBrowserDialog taskBrowserDialog;
		private Microsoft.Win32.TaskScheduler.TaskBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ContextMenuStrip outputMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
	}
}

