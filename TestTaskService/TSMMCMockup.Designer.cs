namespace TestTaskService
{
	partial class TSMMCMockup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSMMCMockup));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.TaskService = new Microsoft.Win32.TaskScheduler.TaskService();
            this.taskServiceConnectDialog1 = new Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.taskEditDialog1 = new Microsoft.Win32.TaskScheduler.TaskEditDialog();
            this.taskSchedulerWizard1 = new Microsoft.Win32.TaskScheduler.TaskSchedulerWizard();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.hidableDetailPanel1 = new TestTaskService.HidableDetailPanel();
            this.libraryMenuStrip = new System.Windows.Forms.ToolStrip();
            this.connectToAnotherComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBasicTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayAllRunningTasksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hidableDetailPanel2 = new TestTaskService.HidableDetailPanel();
            this.itemMenuStrip = new System.Windows.Forms.ToolStrip();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHiddenTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskService)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.hidableDetailPanel1.DetailArea.SuspendLayout();
            this.libraryMenuStrip.SuspendLayout();
            this.hidableDetailPanel2.DetailArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1126, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1126, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "mainToolStrip";
            this.mainToolStrip.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 664);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1126, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // taskServiceConnectDialog1
            // 
            this.taskServiceConnectDialog1.AutoSize = true;
            this.taskServiceConnectDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.taskServiceConnectDialog1.ClientSize = new System.Drawing.Size(444, 181);
            this.taskServiceConnectDialog1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.taskServiceConnectDialog1.Location = new System.Drawing.Point(50, 50);
            this.taskServiceConnectDialog1.Name = "TaskServiceConnectDialog";
            this.taskServiceConnectDialog1.TaskService = this.TaskService;
            this.taskServiceConnectDialog1.Text = "Select Computer";
            this.taskServiceConnectDialog1.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(923, 640);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.TabIndex = 6;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowLines = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(272, 640);
            this.treeView1.StateImageList = this.imageList1;
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.Filter = "Xml files (*.xml)|*.xml";
            this.openFileDialog1.Title = "Import Task";
            // 
            // taskEditDialog1
            // 
            this.taskEditDialog1.AutoSize = true;
            this.taskEditDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.taskEditDialog1.ClientSize = new System.Drawing.Size(600, 462);
            this.taskEditDialog1.Editable = true;
            this.taskEditDialog1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.taskEditDialog1.Location = new System.Drawing.Point(75, 75);
            this.taskEditDialog1.MaximizeBox = false;
            this.taskEditDialog1.Name = "TaskEditDialog";
            this.taskEditDialog1.RegisterTaskOnAccept = true;
            this.taskEditDialog1.ShowIcon = false;
            this.taskEditDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.taskEditDialog1.Text = "Create Task";
            this.taskEditDialog1.Title = "Create Task";
            this.taskEditDialog1.Visible = false;
            // 
            // taskSchedulerWizard1
            // 
            this.taskSchedulerWizard1.AvailableTriggers = ((Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers)(((((((((((Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Event | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Time) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Daily) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Weekly) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Monthly) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.MonthlyDOW) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Idle) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Registration) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Boot) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.Logon) 
            | Microsoft.Win32.TaskScheduler.TaskSchedulerWizard.AvailableWizardTriggers.SessionStateChange)));
            this.taskSchedulerWizard1.ClientSize = new System.Drawing.Size(538, 391);
            this.taskSchedulerWizard1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.taskSchedulerWizard1.Icon = ((System.Drawing.Icon)(resources.GetObject("taskSchedulerWizard1.Icon")));
            this.taskSchedulerWizard1.Location = new System.Drawing.Point(100, 100);
            this.taskSchedulerWizard1.MinimumSize = new System.Drawing.Size(477, 374);
            this.taskSchedulerWizard1.Name = "TaskSchedulerWizard";
            this.taskSchedulerWizard1.RegisterTaskOnFinish = true;
            this.taskSchedulerWizard1.Title = "Create Task Wizard";
            this.taskSchedulerWizard1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hidableDetailPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hidableDetailPanel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(926, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 640);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Actions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hidableDetailPanel1
            // 
            this.hidableDetailPanel1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // hidableDetailPanel1.DetailArea
            // 
            this.hidableDetailPanel1.DetailArea.BackColor = System.Drawing.Color.Transparent;
            this.hidableDetailPanel1.DetailArea.Controls.Add(this.libraryMenuStrip);
            this.hidableDetailPanel1.DetailArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hidableDetailPanel1.DetailArea.Location = new System.Drawing.Point(3, 27);
            this.hidableDetailPanel1.DetailArea.Name = "DetailArea";
            this.hidableDetailPanel1.DetailArea.Size = new System.Drawing.Size(188, 248);
            this.hidableDetailPanel1.DetailArea.TabIndex = 1;
            this.hidableDetailPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.hidableDetailPanel1.Location = new System.Drawing.Point(3, 26);
            this.hidableDetailPanel1.Name = "hidableDetailPanel1";
            this.hidableDetailPanel1.Size = new System.Drawing.Size(194, 200);
            this.hidableDetailPanel1.TabIndex = 1;
            this.hidableDetailPanel1.Text = "Task Scheduler";
            // 
            // libraryMenuStrip
            // 
            this.libraryMenuStrip.BackColor = System.Drawing.SystemColors.Window;
            this.libraryMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.libraryMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToAnotherComputerToolStripMenuItem,
            this.createBasicTaskMenuItem,
            this.createTaskMenuItem,
            this.importTaskMenuItem,
            this.displayAllRunningTasksMenuItem,
            this.toolStripSeparator1,
            this.newFolderMenuItem,
            this.toolStripSeparator2,
            this.refreshMenuItem});
            this.libraryMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.libraryMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.libraryMenuStrip.Name = "libraryMenuStrip";
            this.libraryMenuStrip.Size = new System.Drawing.Size(188, 161);
            this.libraryMenuStrip.TabIndex = 0;
            // 
            // connectToAnotherComputerToolStripMenuItem
            // 
            this.connectToAnotherComputerToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectToAnotherComputerToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.connectToAnotherComputerToolStripMenuItem.Name = "connectToAnotherComputerToolStripMenuItem";
            this.connectToAnotherComputerToolStripMenuItem.Size = new System.Drawing.Size(186, 19);
            this.connectToAnotherComputerToolStripMenuItem.Text = "Connect to another computer...";
            this.connectToAnotherComputerToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.connectToAnotherComputerToolStripMenuItem.Click += new System.EventHandler(this.connectToAnotherComputerToolStripMenuItem_Click);
            // 
            // createBasicTaskMenuItem
            // 
            this.createBasicTaskMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createBasicTaskMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.createBasicTaskMenuItem.Name = "createBasicTaskMenuItem";
            this.createBasicTaskMenuItem.Size = new System.Drawing.Size(186, 19);
            this.createBasicTaskMenuItem.Text = "Create Basic Task...";
            this.createBasicTaskMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createBasicTaskMenuItem.Click += new System.EventHandler(this.createBasicTaskMenuItem_Click);
            // 
            // createTaskMenuItem
            // 
            this.createTaskMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createTaskMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.createTaskMenuItem.Name = "createTaskMenuItem";
            this.createTaskMenuItem.Size = new System.Drawing.Size(186, 19);
            this.createTaskMenuItem.Text = "Create Task...";
            this.createTaskMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createTaskMenuItem.Click += new System.EventHandler(this.createTaskMenuItem_Click);
            // 
            // importTaskMenuItem
            // 
            this.importTaskMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importTaskMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.importTaskMenuItem.Name = "importTaskMenuItem";
            this.importTaskMenuItem.Size = new System.Drawing.Size(186, 19);
            this.importTaskMenuItem.Text = "Import Task...";
            this.importTaskMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importTaskMenuItem.Click += new System.EventHandler(this.importTaskMenuItem_Click);
            // 
            // displayAllRunningTasksMenuItem
            // 
            this.displayAllRunningTasksMenuItem.Enabled = false;
            this.displayAllRunningTasksMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.displayAllRunningTasksMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.displayAllRunningTasksMenuItem.Name = "displayAllRunningTasksMenuItem";
            this.displayAllRunningTasksMenuItem.Size = new System.Drawing.Size(186, 19);
            this.displayAllRunningTasksMenuItem.Text = "Display All Running Tasks";
            this.displayAllRunningTasksMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.displayAllRunningTasksMenuItem.Click += new System.EventHandler(this.displayAllRunningTasksToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // newFolderMenuItem
            // 
            this.newFolderMenuItem.Enabled = false;
            this.newFolderMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newFolderMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.newFolderMenuItem.Name = "newFolderMenuItem";
            this.newFolderMenuItem.Size = new System.Drawing.Size(186, 19);
            this.newFolderMenuItem.Text = "New Folder...";
            this.newFolderMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newFolderMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // refreshMenuItem
            // 
            this.refreshMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshMenuItem.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.refreshMenuItem.Name = "refreshMenuItem";
            this.refreshMenuItem.Size = new System.Drawing.Size(186, 19);
            this.refreshMenuItem.Text = "Refresh";
            this.refreshMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.refreshMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // hidableDetailPanel2
            // 
            this.hidableDetailPanel2.BackColor = System.Drawing.SystemColors.Window;
            // 
            // hidableDetailPanel2.DetailArea
            // 
            this.hidableDetailPanel2.DetailArea.BackColor = System.Drawing.Color.Transparent;
            this.hidableDetailPanel2.DetailArea.Controls.Add(this.itemMenuStrip);
            this.hidableDetailPanel2.DetailArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hidableDetailPanel2.DetailArea.Location = new System.Drawing.Point(3, 27);
            this.hidableDetailPanel2.DetailArea.Name = "DetailArea";
            this.hidableDetailPanel2.DetailArea.Size = new System.Drawing.Size(188, 281);
            this.hidableDetailPanel2.DetailArea.TabIndex = 1;
            this.hidableDetailPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.hidableDetailPanel2.Location = new System.Drawing.Point(3, 232);
            this.hidableDetailPanel2.Name = "hidableDetailPanel2";
            this.hidableDetailPanel2.Size = new System.Drawing.Size(194, 311);
            this.hidableDetailPanel2.TabIndex = 1;
            this.hidableDetailPanel2.Text = "Item";
            // 
            // itemMenuStrip
            // 
            this.itemMenuStrip.BackColor = System.Drawing.SystemColors.Window;
            this.itemMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.itemMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.itemMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.itemMenuStrip.Name = "itemMenuStrip";
            this.itemMenuStrip.Size = new System.Drawing.Size(188, 102);
            this.itemMenuStrip.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(923, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 640);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHiddenTasksToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showHiddenTasksToolStripMenuItem
            // 
            this.showHiddenTasksToolStripMenuItem.CheckOnClick = true;
            this.showHiddenTasksToolStripMenuItem.Name = "showHiddenTasksToolStripMenuItem";
            this.showHiddenTasksToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showHiddenTasksToolStripMenuItem.Text = "Show hidden tasks";
            // 
            // TSMMCMockup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 686);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "TSMMCMockup";
            this.Text = "Task Scheduler";
            this.Load += new System.EventHandler(this.TSMMCMockup_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskService)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.hidableDetailPanel1.DetailArea.ResumeLayout(false);
            this.hidableDetailPanel1.DetailArea.PerformLayout();
            this.libraryMenuStrip.ResumeLayout(false);
            this.libraryMenuStrip.PerformLayout();
            this.hidableDetailPanel2.DetailArea.ResumeLayout(false);
            this.hidableDetailPanel2.DetailArea.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog taskServiceConnectDialog1;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView1;
		private Microsoft.Win32.TaskScheduler.TaskService TaskService;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Microsoft.Win32.TaskScheduler.TaskEditDialog taskEditDialog1;
		private Microsoft.Win32.TaskScheduler.TaskSchedulerWizard taskSchedulerWizard1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Splitter splitter1;
		private HidableDetailPanel hidableDetailPanel1;
		private HidableDetailPanel hidableDetailPanel2;
		private System.Windows.Forms.ToolStrip libraryMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem connectToAnotherComputerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createBasicTaskMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createTaskMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importTaskMenuItem;
		private System.Windows.Forms.ToolStripMenuItem displayAllRunningTasksMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem newFolderMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
		private System.Windows.Forms.ToolStrip itemMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHiddenTasksToolStripMenuItem;
	}
}