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
			this.connectToAnotherComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createBasicTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.displayAllRunningTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.runMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.propMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.taskPropertiesControl1 = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.taskListView1 = new Microsoft.Win32.TaskScheduler.TaskListView();
			this.taskService = new Microsoft.Win32.TaskScheduler.TaskService();
			this.taskServiceConnectDialog1 = new Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog();
			this.taskRunTimesDialog1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesDialog();
			this.taskSchedulerWizard1 = new Microsoft.Win32.TaskScheduler.TaskSchedulerWizard();
			this.taskEditDialog1 = new Microsoft.Win32.TaskScheduler.TaskEditDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.exportTaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.taskService)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(959, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToAnotherComputerToolStripMenuItem,
            this.createBasicTaskToolStripMenuItem,
            this.createTaskToolStripMenuItem,
            this.importTaskToolStripMenuItem,
            this.displayAllRunningTasksToolStripMenuItem});
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.actionToolStripMenuItem.Text = "Action";
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
			this.createBasicTaskToolStripMenuItem.Click += new System.EventHandler(this.createBasicTaskToolStripMenuItem_Click);
			// 
			// createTaskToolStripMenuItem
			// 
			this.createTaskToolStripMenuItem.Name = "createTaskToolStripMenuItem";
			this.createTaskToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.createTaskToolStripMenuItem.Text = "Create Task...";
			this.createTaskToolStripMenuItem.Click += new System.EventHandler(this.createTaskToolStripMenuItem_Click);
			// 
			// importTaskToolStripMenuItem
			// 
			this.importTaskToolStripMenuItem.Name = "importTaskToolStripMenuItem";
			this.importTaskToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.importTaskToolStripMenuItem.Text = "Import Task...";
			this.importTaskToolStripMenuItem.Click += new System.EventHandler(this.importTaskToolStripMenuItem_Click);
			// 
			// displayAllRunningTasksToolStripMenuItem
			// 
			this.displayAllRunningTasksToolStripMenuItem.Name = "displayAllRunningTasksToolStripMenuItem";
			this.displayAllRunningTasksToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.displayAllRunningTasksToolStripMenuItem.Text = "Display All Running Tasks";
			this.displayAllRunningTasksToolStripMenuItem.Click += new System.EventHandler(this.displayAllRunningTasksToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(959, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 664);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(959, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView1.Location = new System.Drawing.Point(0, 49);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(215, 615);
			this.treeView1.TabIndex = 3;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(215, 49);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 615);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMenu,
            this.propMenu,
            this.deleteMenu,
            this.exportTaskToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(119, 92);
			// 
			// runMenu
			// 
			this.runMenu.Name = "runMenu";
			this.runMenu.Size = new System.Drawing.Size(102, 22);
			this.runMenu.Text = "Run";
			this.runMenu.Click += new System.EventHandler(this.runMenu_Click);
			// 
			// propMenu
			// 
			this.propMenu.Name = "propMenu";
			this.propMenu.Size = new System.Drawing.Size(102, 22);
			this.propMenu.Text = "Properties";
			this.propMenu.Click += new System.EventHandler(this.propMenu_Click);
			// 
			// deleteMenu
			// 
			this.deleteMenu.Name = "deleteMenu";
			this.deleteMenu.Size = new System.Drawing.Size(102, 22);
			this.deleteMenu.Text = "Delete";
			this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(218, 281);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(741, 3);
			this.splitter2.TabIndex = 6;
			this.splitter2.TabStop = false;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "xml";
			this.openFileDialog1.Filter = "Xml files (*.xml)|*.xml";
			this.openFileDialog1.Title = "Import Task";
			// 
			// taskPropertiesControl1
			// 
			this.taskPropertiesControl1.AutoScroll = true;
			this.taskPropertiesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.taskPropertiesControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskPropertiesControl1.Location = new System.Drawing.Point(218, 284);
			this.taskPropertiesControl1.MinimumSize = new System.Drawing.Size(622, 400);
			this.taskPropertiesControl1.Name = "taskPropertiesControl1";
			this.taskPropertiesControl1.Size = new System.Drawing.Size(741, 400);
			this.taskPropertiesControl1.TabIndex = 7;
			// 
			// taskListView1
			// 
			this.taskListView1.ContextMenuStrip = this.contextMenuStrip1;
			this.taskListView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.taskListView1.Location = new System.Drawing.Point(218, 49);
			this.taskListView1.Name = "taskListView1";
			this.taskListView1.SelectedIndex = -1;
			this.taskListView1.Size = new System.Drawing.Size(741, 232);
			this.taskListView1.TabIndex = 5;
			this.taskListView1.TaskSelected += new System.EventHandler<Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs>(this.taskListView1_TaskSelected);
			this.taskListView1.DoubleClick += new System.EventHandler(this.propMenu_Click);
			// 
			// taskServiceConnectDialog1
			// 
			this.taskServiceConnectDialog1.AutoSize = true;
			this.taskServiceConnectDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskServiceConnectDialog1.ClientSize = new System.Drawing.Size(444, 181);
			this.taskServiceConnectDialog1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskServiceConnectDialog1.Name = "TaskServiceConnectDialog";
			this.taskServiceConnectDialog1.TaskService = this.taskService;
			this.taskServiceConnectDialog1.Text = "Select Computer";
			this.taskServiceConnectDialog1.Visible = false;
			// 
			// taskRunTimesDialog1
			// 
			this.taskRunTimesDialog1.AutoSize = true;
			this.taskRunTimesDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskRunTimesDialog1.ClientSize = new System.Drawing.Size(269, 294);
			this.taskRunTimesDialog1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskRunTimesDialog1.Name = "TaskRunTimesDialog";
			this.taskRunTimesDialog1.Text = "Task Run Times";
			this.taskRunTimesDialog1.Visible = false;
			// 
			// taskSchedulerWizard1
			// 
			this.taskSchedulerWizard1.ClientSize = new System.Drawing.Size(537, 391);
			this.taskSchedulerWizard1.Icon = ((System.Drawing.Icon)(resources.GetObject("taskSchedulerWizard1.Icon")));
			this.taskSchedulerWizard1.MinimumSize = new System.Drawing.Size(477, 374);
			this.taskSchedulerWizard1.Name = "TaskSchedulerWizard";
			this.taskSchedulerWizard1.RegisterTaskOnFinish = true;
			this.taskSchedulerWizard1.ShowIcon = false;
			this.taskSchedulerWizard1.TaskService = this.taskService;
			this.taskSchedulerWizard1.Visible = false;
			// 
			// taskEditDialog1
			// 
			this.taskEditDialog1.AutoSize = true;
			this.taskEditDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskEditDialog1.ClientSize = new System.Drawing.Size(651, 493);
			this.taskEditDialog1.Editable = true;
			this.taskEditDialog1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskEditDialog1.MaximizeBox = false;
			this.taskEditDialog1.Name = "TaskEditDialog";
			this.taskEditDialog1.RegisterTaskOnAccept = true;
			this.taskEditDialog1.ShowIcon = false;
			this.taskEditDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.taskEditDialog1.TaskService = this.taskService;
			this.taskEditDialog1.Text = "Create Task";
			this.taskEditDialog1.Title = "Create Task";
			this.taskEditDialog1.Visible = false;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "xml";
			this.saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
			// 
			// exportTaskToolStripMenuItem1
			// 
			this.exportTaskToolStripMenuItem1.Name = "exportTaskToolStripMenuItem1";
			this.exportTaskToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
			this.exportTaskToolStripMenuItem1.Text = "Export Task...";
			// 
			// TSMMCMockup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(959, 686);
			this.Controls.Add(this.taskPropertiesControl1);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.taskListView1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.treeView1);
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
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.taskService)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Splitter splitter1;
		private Microsoft.Win32.TaskScheduler.TaskListView taskListView1;
		private System.Windows.Forms.Splitter splitter2;
		private Microsoft.Win32.TaskScheduler.TaskPropertiesControl taskPropertiesControl1;
		private Microsoft.Win32.TaskScheduler.TaskService taskService;
		private Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog taskServiceConnectDialog1;
		private Microsoft.Win32.TaskScheduler.TaskRunTimesDialog taskRunTimesDialog1;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectToAnotherComputerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createBasicTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem displayAllRunningTasksToolStripMenuItem;
		private Microsoft.Win32.TaskScheduler.TaskSchedulerWizard taskSchedulerWizard1;
		private Microsoft.Win32.TaskScheduler.TaskEditDialog taskEditDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem runMenu;
		private System.Windows.Forms.ToolStripMenuItem propMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteMenu;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem exportTaskToolStripMenuItem1;
	}
}