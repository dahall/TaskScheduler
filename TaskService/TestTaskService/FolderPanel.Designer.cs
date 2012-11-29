namespace TestTaskService
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
			this.TaskListView = new Microsoft.Win32.TaskScheduler.TaskListView();
			this.itemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.endToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.TaskPropertiesControl = new Microsoft.Win32.TaskScheduler.TaskPropertiesControl();
			this.taskEditDialog1 = new Microsoft.Win32.TaskScheduler.TaskEditDialog();
			this.taskRunTimesDialog1 = new Microsoft.Win32.TaskScheduler.TaskRunTimesDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.folderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.itemMenu.SuspendLayout();
			this.folderMenu.SuspendLayout();
			this.SuspendLayout();
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
			// 
			// itemMenu
			// 
			this.itemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.endToolStripMenuItem,
            this.disableToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.itemMenu.Name = "itemMenu";
			this.itemMenu.Size = new System.Drawing.Size(128, 136);
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.runToolStripMenuItem.Text = "Run";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runMenu_Click);
			// 
			// endToolStripMenuItem
			// 
			this.endToolStripMenuItem.Name = "endToolStripMenuItem";
			this.endToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.endToolStripMenuItem.Text = "End";
			this.endToolStripMenuItem.Click += new System.EventHandler(this.endToolStripMenuItem_Click);
			// 
			// disableToolStripMenuItem
			// 
			this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
			this.disableToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.disableToolStripMenuItem.Text = "Disable";
			// 
			// exportToolStripMenuItem
			// 
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.exportToolStripMenuItem.Text = "Export...";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportTaskMenu_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propMenu_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteMenu_Click);
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
			// TaskPropertiesControl
			// 
			this.TaskPropertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TaskPropertiesControl.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.TaskPropertiesControl.Location = new System.Drawing.Point(0, 235);
			this.TaskPropertiesControl.Name = "TaskPropertiesControl";
			this.TaskPropertiesControl.Size = new System.Drawing.Size(735, 277);
			this.TaskPropertiesControl.TabIndex = 2;
			// 
			// taskEditDialog1
			// 
			this.taskEditDialog1.AutoSize = true;
			this.taskEditDialog1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.taskEditDialog1.ClientSize = new System.Drawing.Size(600, 462);
			this.taskEditDialog1.Editable = true;
			this.taskEditDialog1.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.taskEditDialog1.Location = new System.Drawing.Point(125, 125);
			this.taskEditDialog1.MaximizeBox = false;
			this.taskEditDialog1.Name = "TaskEditDialog";
			this.taskEditDialog1.RegisterTaskOnAccept = true;
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
			this.taskRunTimesDialog1.ClientSize = new System.Drawing.Size(269, 294);
			this.taskRunTimesDialog1.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.taskRunTimesDialog1.Location = new System.Drawing.Point(75, 75);
			this.taskRunTimesDialog1.Name = "TaskRunTimesDialog";
			this.taskRunTimesDialog1.Text = "Task Run Times";
			this.taskRunTimesDialog1.Visible = false;
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
			// FolderPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.TaskPropertiesControl);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.TaskListView);
			this.Name = "FolderPanel";
			this.Size = new System.Drawing.Size(735, 512);
			this.itemMenu.ResumeLayout(false);
			this.folderMenu.ResumeLayout(false);
			this.ResumeLayout(false);

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
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem endToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;

	}
}
