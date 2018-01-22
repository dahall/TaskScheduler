namespace TaskSchedulerMockup
{
	partial class FolderPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.itemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.runMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.endMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.folderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.itemMenuStrip = new System.Windows.Forms.ToolStrip();
			this.runMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.endMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.disableMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.exportMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.TaskPropertiesControl = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.TaskListView = new Microsoft.Win32.TaskScheduler.TaskListView();
			this.taskEditDialog1 = new Microsoft.Win32.TaskScheduler.TaskEditDialog();
			this.taskRunTimesDialog1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesDialog();
			this.itemMenu.SuspendLayout();
			this.folderMenu.SuspendLayout();
			this.itemMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// itemMenu
			// 
			this.itemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMenuItem,
            this.endMenuItem,
            this.disableMenuItem,
            this.exportMenuItem,
            this.propertiesMenuItem,
            this.deleteMenuItem});
			this.itemMenu.Name = "itemMenu";
			this.itemMenu.Size = new System.Drawing.Size(128, 136);
			// 
			// runMenuItem
			// 
			this.runMenuItem.Name = "runMenuItem";
			this.runMenuItem.Size = new System.Drawing.Size(127, 22);
			this.runMenuItem.Text = "Run";
			this.runMenuItem.Click += new System.EventHandler(this.runMenu_Click);
			// 
			// endMenuItem
			// 
			this.endMenuItem.Name = "endMenuItem";
			this.endMenuItem.Size = new System.Drawing.Size(127, 22);
			this.endMenuItem.Text = "End";
			this.endMenuItem.Click += new System.EventHandler(this.endMenu_Click);
			// 
			// disableMenuItem
			// 
			this.disableMenuItem.Name = "disableMenuItem";
			this.disableMenuItem.Size = new System.Drawing.Size(127, 22);
			this.disableMenuItem.Text = "Disable";
			this.disableMenuItem.Click += new System.EventHandler(this.disableMenu_Click);
			// 
			// exportMenuItem
			// 
			this.exportMenuItem.Name = "exportMenuItem";
			this.exportMenuItem.Size = new System.Drawing.Size(127, 22);
			this.exportMenuItem.Text = "Export...";
			this.exportMenuItem.Click += new System.EventHandler(this.exportMenu_Click);
			// 
			// propertiesMenuItem
			// 
			this.propertiesMenuItem.Name = "propertiesMenuItem";
			this.propertiesMenuItem.Size = new System.Drawing.Size(127, 22);
			this.propertiesMenuItem.Text = "Properties";
			this.propertiesMenuItem.Click += new System.EventHandler(this.propMenu_Click);
			// 
			// deleteMenuItem
			// 
			this.deleteMenuItem.Name = "deleteMenuItem";
			this.deleteMenuItem.Size = new System.Drawing.Size(127, 22);
			this.deleteMenuItem.Text = "Delete";
			this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 232);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(735, 3);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "xml";
			this.saveFileDialog1.Filter = "Xml files (*.xml)|*.xml";
			// 
			// folderMenu
			// 
			this.folderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFolderToolStripMenuItem,
            this.deleteFolderToolStripMenuItem});
			this.folderMenu.Name = "folderMenu";
			this.folderMenu.Size = new System.Drawing.Size(144, 48);
			// 
			// newFolderToolStripMenuItem
			// 
			this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
			this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.newFolderToolStripMenuItem.Text = "New Folder...";
			// 
			// deleteFolderToolStripMenuItem
			// 
			this.deleteFolderToolStripMenuItem.Name = "deleteFolderToolStripMenuItem";
			this.deleteFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.deleteFolderToolStripMenuItem.Text = "Delete Folder";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// itemMenuStrip
			// 
			this.itemMenuStrip.BackColor = System.Drawing.SystemColors.Window;
			this.itemMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.itemMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runMenuItem2,
            this.endMenuItem2,
            this.disableMenuItem2,
            this.exportMenuItem2,
            this.propertiesMenuItem2,
            this.deleteMenuItem2});
			this.itemMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
			this.itemMenuStrip.Location = new System.Drawing.Point(0, 235);
			this.itemMenuStrip.Name = "itemMenuStrip";
			this.itemMenuStrip.Size = new System.Drawing.Size(735, 147);
			this.itemMenuStrip.TabIndex = 3;
			this.itemMenuStrip.Visible = false;
			// 
			// runMenuItem2
			// 
			this.runMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.runMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.runMenuItem2.Name = "runMenuItem2";
			this.runMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.runMenuItem2.Text = "Run";
			this.runMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.runMenuItem2.Click += new System.EventHandler(this.runMenu_Click);
			// 
			// endMenuItem2
			// 
			this.endMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.endMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.endMenuItem2.Name = "endMenuItem2";
			this.endMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.endMenuItem2.Text = "End";
			this.endMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.endMenuItem2.Click += new System.EventHandler(this.endMenu_Click);
			// 
			// disableMenuItem2
			// 
			this.disableMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.disableMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.disableMenuItem2.Name = "disableMenuItem2";
			this.disableMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.disableMenuItem2.Text = "Disable";
			this.disableMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.disableMenuItem2.Click += new System.EventHandler(this.disableMenu_Click);
			// 
			// exportMenuItem2
			// 
			this.exportMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.exportMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.exportMenuItem2.Name = "exportMenuItem2";
			this.exportMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.exportMenuItem2.Text = "Export...";
			this.exportMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.exportMenuItem2.Click += new System.EventHandler(this.exportMenu_Click);
			// 
			// propertiesMenuItem2
			// 
			this.propertiesMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.propertiesMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.propertiesMenuItem2.Name = "propertiesMenuItem2";
			this.propertiesMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.propertiesMenuItem2.Text = "Properties";
			this.propertiesMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.propertiesMenuItem2.Click += new System.EventHandler(this.propMenu_Click);
			// 
			// deleteMenuItem2
			// 
			this.deleteMenuItem2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deleteMenuItem2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.deleteMenuItem2.Name = "deleteMenuItem2";
			this.deleteMenuItem2.Size = new System.Drawing.Size(733, 19);
			this.deleteMenuItem2.Text = "Delete";
			this.deleteMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.deleteMenuItem2.Click += new System.EventHandler(this.deleteMenu_Click);
			// 
			// TaskPropertiesControl
			// 
			this.TaskPropertiesControl.AvailableTabs = ((Microsoft.Win32.TaskScheduler.AvailableTaskTabs)(((((((((Microsoft.Win32.TaskScheduler.AvailableTaskTabs.General | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.Triggers) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.Actions) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.Conditions) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.Settings) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.RegistrationInfo) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.Properties) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.RunTimes) 
            | Microsoft.Win32.TaskScheduler.AvailableTaskTabs.History)));
			this.TaskPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TaskPropertiesControl.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.TaskPropertiesControl.Location = new System.Drawing.Point(0, 235);
			this.TaskPropertiesControl.Name = "TaskPropertiesControl";
			this.TaskPropertiesControl.Size = new System.Drawing.Size(735, 277);
			this.TaskPropertiesControl.TabIndex = 2;
			// 
			// TaskListView
			// 
			this.TaskListView.ContextMenuStrip = this.itemMenu;
			this.TaskListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.TaskListView.Location = new System.Drawing.Point(0, 0);
			this.TaskListView.Name = "TaskListView";
			this.TaskListView.SelectedIndex = -1;
			this.TaskListView.Size = new System.Drawing.Size(735, 232);
			this.TaskListView.TabIndex = 0;
			this.TaskListView.TaskSelected += new System.EventHandler<Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs>(this.taskListView_TaskSelected);
			this.TaskListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskListView_MouseDoubleClick);
			// 
			// taskEditDialog1
			// 
			this.taskEditDialog1.AvailableTabs = Microsoft.Win32.TaskScheduler.AvailableTaskTabs.All;
			this.taskEditDialog1.AvailableTriggers = Microsoft.Win32.TaskScheduler.AvailableTriggers.AllTriggers | Microsoft.Win32.TaskScheduler.AvailableTriggers.Custom;
			this.taskEditDialog1.AutoSize = true;
			this.taskEditDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskEditDialog1.ClientSize = new System.Drawing.Size(600, 462);
			this.taskEditDialog1.Editable = true;
			this.taskEditDialog1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskEditDialog1.Location = new System.Drawing.Point(50, 50);
			this.taskEditDialog1.MaximizeBox = false;
			this.taskEditDialog1.Name = "TaskEditDialog";
			this.taskEditDialog1.RegisterTaskOnAccept = true;
			this.taskEditDialog1.ShowActionRunButton = true;
			this.taskEditDialog1.ShowConvertActionsToPowerShellCheck = true;
			this.taskEditDialog1.ShowIcon = false;
			this.taskEditDialog1.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.taskEditDialog1.Text = "Create Task";
			this.taskEditDialog1.Title = "Create Task";
			this.taskEditDialog1.Visible = false;
			// 
			// taskRunTimesDialog1
			// 
			this.taskRunTimesDialog1.AutoSize = true;
			this.taskRunTimesDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskRunTimesDialog1.ClientSize = new System.Drawing.Size(347, 294);
			this.taskRunTimesDialog1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskRunTimesDialog1.Location = new System.Drawing.Point(75, 75);
			this.taskRunTimesDialog1.Name = "TaskRunTimesDialog";
			this.taskRunTimesDialog1.Text = "Task Run Times";
			this.taskRunTimesDialog1.Visible = false;
			// 
			// FolderPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.itemMenuStrip);
			this.Controls.Add(this.TaskPropertiesControl);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.TaskListView);
			this.Name = "FolderPanel";
			this.Size = new System.Drawing.Size(735, 512);
			this.itemMenu.ResumeLayout(false);
			this.folderMenu.ResumeLayout(false);
			this.itemMenuStrip.ResumeLayout(false);
			this.itemMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Splitter splitter1;
		internal Microsoft.Win32.TaskScheduler.TaskListView TaskListView;
		internal Microsoft.Win32.TaskScheduler.TaskPropertiesControl TaskPropertiesControl;
		private Microsoft.Win32.TaskScheduler.TaskEditDialog taskEditDialog1;
		private Microsoft.Win32.TaskScheduler.TaskRunTimesDialog taskRunTimesDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ContextMenuStrip folderMenu;
		private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteFolderToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip itemMenu;
		private System.Windows.Forms.ToolStripMenuItem runMenuItem;
		private System.Windows.Forms.ToolStripMenuItem endMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disableMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStrip itemMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem runMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem endMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem disableMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem exportMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem propertiesMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem deleteMenuItem2;

	}
}
