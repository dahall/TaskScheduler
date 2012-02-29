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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.TaskService = new Microsoft.Win32.TaskScheduler.TaskService();
			this.taskServiceConnectDialog1 = new Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog();
			this.actionToolStrip = new System.Windows.Forms.ToolStrip();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.connectToAnotherComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createBasicTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.displayAllRunningTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.taskEditDialog1 = new Microsoft.Win32.TaskScheduler.TaskEditDialog();
			this.taskSchedulerWizard1 = new Microsoft.Win32.TaskScheduler.TaskSchedulerWizard();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TaskService)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1126, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.actionToolStripMenuItem.Text = "Action";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1126, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 664);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1126, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
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
			this.taskServiceConnectDialog1.Location = new System.Drawing.Point(100, 100);
			this.taskServiceConnectDialog1.Name = "TaskServiceConnectDialog";
			this.taskServiceConnectDialog1.TaskService = this.TaskService;
			this.taskServiceConnectDialog1.Text = "Select Computer";
			this.taskServiceConnectDialog1.Visible = false;
			// 
			// actionToolStrip
			// 
			this.actionToolStrip.Dock = System.Windows.Forms.DockStyle.Right;
			this.actionToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.actionToolStrip.Location = new System.Drawing.Point(1100, 49);
			this.actionToolStrip.Name = "actionToolStrip";
			this.actionToolStrip.Size = new System.Drawing.Size(26, 615);
			this.actionToolStrip.TabIndex = 5;
			this.actionToolStrip.Text = "Actions";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 49);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			this.splitContainer1.Size = new System.Drawing.Size(1100, 615);
			this.splitContainer1.SplitterDistance = 366;
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
			this.treeView1.ShowPlusMinus = false;
			this.treeView1.ShowRootLines = false;
			this.treeView1.Size = new System.Drawing.Size(366, 615);
			this.treeView1.StateImageList = this.imageList1;
			this.treeView1.TabIndex = 4;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToAnotherComputerToolStripMenuItem,
            this.createBasicTaskToolStripMenuItem,
            this.createTaskToolStripMenuItem,
            this.importTaskToolStripMenuItem,
            this.displayAllRunningTasksToolStripMenuItem,
            this.refreshToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(242, 158);
			// 
			// connectToAnotherComputerToolStripMenuItem
			// 
			this.connectToAnotherComputerToolStripMenuItem.Name = "connectToAnotherComputerToolStripMenuItem";
			this.connectToAnotherComputerToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.connectToAnotherComputerToolStripMenuItem.Text = "Connect to another computer...";
			this.connectToAnotherComputerToolStripMenuItem.Click += new System.EventHandler(this.connectToAnotherComputerToolStripMenuItem_Click);
			// 
			// createBasicTaskToolStripMenuItem
			// 
			this.createBasicTaskToolStripMenuItem.Name = "createBasicTaskToolStripMenuItem";
			this.createBasicTaskToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.createBasicTaskToolStripMenuItem.Text = "Create Basic Task...";
			this.createBasicTaskToolStripMenuItem.Click += new System.EventHandler(this.createBasicTaskMenu_Click);
			// 
			// createTaskToolStripMenuItem
			// 
			this.createTaskToolStripMenuItem.Name = "createTaskToolStripMenuItem";
			this.createTaskToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.createTaskToolStripMenuItem.Text = "Create Task...";
			this.createTaskToolStripMenuItem.Click += new System.EventHandler(this.createTaskMenu_Click);
			// 
			// importTaskToolStripMenuItem
			// 
			this.importTaskToolStripMenuItem.Name = "importTaskToolStripMenuItem";
			this.importTaskToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.importTaskToolStripMenuItem.Text = "Import Task...";
			this.importTaskToolStripMenuItem.Click += new System.EventHandler(this.importTaskMenu_Click);
			// 
			// displayAllRunningTasksToolStripMenuItem
			// 
			this.displayAllRunningTasksToolStripMenuItem.Enabled = false;
			this.displayAllRunningTasksToolStripMenuItem.Name = "displayAllRunningTasksToolStripMenuItem";
			this.displayAllRunningTasksToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.displayAllRunningTasksToolStripMenuItem.Text = "Display All Running Tasks";
			this.displayAllRunningTasksToolStripMenuItem.Click += new System.EventHandler(this.displayAllRunningTasksToolStripMenuItem_Click);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
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
			this.taskEditDialog1.ClientSize = new System.Drawing.Size(651, 493);
			this.taskEditDialog1.Editable = true;
			this.taskEditDialog1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskEditDialog1.Location = new System.Drawing.Point(50, 50);
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
			this.taskSchedulerWizard1.ClientSize = new System.Drawing.Size(537, 391);
			this.taskSchedulerWizard1.Icon = ((System.Drawing.Icon)(resources.GetObject("taskSchedulerWizard1.Icon")));
			this.taskSchedulerWizard1.Location = new System.Drawing.Point(75, 75);
			this.taskSchedulerWizard1.MinimumSize = new System.Drawing.Size(477, 374);
			this.taskSchedulerWizard1.Name = "TaskSchedulerWizard";
			this.taskSchedulerWizard1.RegisterTaskOnFinish = true;
			this.taskSchedulerWizard1.Title = "Create Task Wizard";
			this.taskSchedulerWizard1.Visible = false;
			// 
			// TSMMCMockup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1126, 686);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.actionToolStrip);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "TSMMCMockup";
			this.Text = "Task Scheduler";
			this.Load += new System.EventHandler(this.TSMMCMockup_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TaskService)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog taskServiceConnectDialog1;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStrip actionToolStrip;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView1;
		private Microsoft.Win32.TaskScheduler.TaskService TaskService;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem connectToAnotherComputerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createBasicTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem displayAllRunningTasksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private Microsoft.Win32.TaskScheduler.TaskEditDialog taskEditDialog1;
		private Microsoft.Win32.TaskScheduler.TaskSchedulerWizard taskSchedulerWizard1;
	}
}