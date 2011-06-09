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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.taskListView1 = new Microsoft.Win32.TaskScheduler.TaskListView();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.taskPropertiesControl1 = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.taskService = new Microsoft.Win32.TaskScheduler.TaskService();
			this.taskServiceConnectDialog1 = new Microsoft.Win32.TaskScheduler.TaskServiceConnectDialog();
			this.taskRunTimesDialog1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesDialog();
			((System.ComponentModel.ISupportInitialize)(this.taskService)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(959, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
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
			// taskListView1
			// 
			this.taskListView1.Dock = System.Windows.Forms.DockStyle.Top;
			this.taskListView1.Location = new System.Drawing.Point(218, 49);
			this.taskListView1.Name = "taskListView1";
			this.taskListView1.Size = new System.Drawing.Size(741, 232);
			this.taskListView1.TabIndex = 5;
			this.taskListView1.TaskSelected += new System.EventHandler<Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs>(this.taskListView1_TaskSelected);
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
			// taskServiceConnectDialog1
			// 
			this.taskServiceConnectDialog1.Name = "TaskServiceConnectDialog";
			this.taskServiceConnectDialog1.TargetServer = "DHALL5";
			this.taskServiceConnectDialog1.TaskService = this.taskService;
			// 
			// taskRunTimesDialog1
			// 
			this.taskRunTimesDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskRunTimesDialog1.Name = "TaskRunTimesDialog";
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
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "TSMMCMockup";
			this.Text = "TSMMCMockup";
			this.Load += new System.EventHandler(this.TSMMCMockup_Load);
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
	}
}