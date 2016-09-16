namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class TriggerCollectionUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerCollectionUI));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.triggerEditButton = new System.Windows.Forms.Button();
			this.triggerNewButton = new System.Windows.Forms.Button();
			this.triggerListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.triggerDeleteButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel2.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.triggerEditButton, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.triggerNewButton, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.triggerListView, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.triggerDeleteButton, 2, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// triggerEditButton
			// 
			resources.ApplyResources(this.triggerEditButton, "triggerEditButton");
			this.triggerEditButton.Name = "triggerEditButton";
			this.triggerEditButton.UseVisualStyleBackColor = true;
			this.triggerEditButton.Click += new System.EventHandler(this.triggerEditButton_Click);
			// 
			// triggerNewButton
			// 
			resources.ApplyResources(this.triggerNewButton, "triggerNewButton");
			this.triggerNewButton.Name = "triggerNewButton";
			this.triggerNewButton.UseVisualStyleBackColor = true;
			this.triggerNewButton.Click += new System.EventHandler(this.triggerNewButton_Click);
			// 
			// triggerListView
			// 
			this.triggerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.tableLayoutPanel2.SetColumnSpan(this.triggerListView, 3);
			this.triggerListView.ContextMenuStrip = this.contextMenu;
			resources.ApplyResources(this.triggerListView, "triggerListView");
			this.triggerListView.FullRowSelect = true;
			this.triggerListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.triggerListView.HideSelection = false;
			this.triggerListView.LargeImageList = this.imageList;
			this.triggerListView.MultiSelect = false;
			this.triggerListView.Name = "triggerListView";
			this.triggerListView.ShowItemToolTips = true;
			this.triggerListView.UseCompatibleStateImageBehavior = false;
			this.triggerListView.View = System.Windows.Forms.View.Details;
			this.triggerListView.SelectedIndexChanged += new System.EventHandler(this.triggerListView_SelectedIndexChanged);
			this.triggerListView.SizeChanged += new System.EventHandler(this.triggerListView_SizeChanged);
			this.triggerListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.triggerListView_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// columnHeader2
			// 
			resources.ApplyResources(this.columnHeader2, "columnHeader2");
			// 
			// columnHeader3
			// 
			resources.ApplyResources(this.columnHeader3, "columnHeader3");
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTriggerToolStripMenuItem,
            this.deleteTriggerToolStripMenuItem,
            this.editTriggerToolStripMenuItem,
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.ShowImageMargin = false;
			resources.ApplyResources(this.contextMenu, "contextMenu");
			// 
			// newTriggerToolStripMenuItem
			// 
			this.newTriggerToolStripMenuItem.Name = "newTriggerToolStripMenuItem";
			resources.ApplyResources(this.newTriggerToolStripMenuItem, "newTriggerToolStripMenuItem");
			this.newTriggerToolStripMenuItem.Click += new System.EventHandler(this.triggerNewButton_Click);
			// 
			// deleteTriggerToolStripMenuItem
			// 
			this.deleteTriggerToolStripMenuItem.Name = "deleteTriggerToolStripMenuItem";
			resources.ApplyResources(this.deleteTriggerToolStripMenuItem, "deleteTriggerToolStripMenuItem");
			this.deleteTriggerToolStripMenuItem.Click += new System.EventHandler(this.triggerDeleteButton_Click);
			// 
			// editTriggerToolStripMenuItem
			// 
			this.editTriggerToolStripMenuItem.Name = "editTriggerToolStripMenuItem";
			resources.ApplyResources(this.editTriggerToolStripMenuItem, "editTriggerToolStripMenuItem");
			this.editTriggerToolStripMenuItem.Click += new System.EventHandler(this.triggerEditButton_Click);
			// 
			// enableToolStripMenuItem
			// 
			this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
			resources.ApplyResources(this.enableToolStripMenuItem, "enableToolStripMenuItem");
			this.enableToolStripMenuItem.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
			// 
			// disableToolStripMenuItem
			// 
			this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
			resources.ApplyResources(this.disableToolStripMenuItem, "disableToolStripMenuItem");
			this.disableToolStripMenuItem.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.imageList, "imageList");
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// triggerDeleteButton
			// 
			resources.ApplyResources(this.triggerDeleteButton, "triggerDeleteButton");
			this.triggerDeleteButton.Name = "triggerDeleteButton";
			this.triggerDeleteButton.UseVisualStyleBackColor = true;
			this.triggerDeleteButton.Click += new System.EventHandler(this.triggerDeleteButton_Click);
			// 
			// TriggerCollectionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "TriggerCollectionUI";
			this.tableLayoutPanel2.ResumeLayout(false);
			this.contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button triggerEditButton;
		private System.Windows.Forms.Button triggerNewButton;
		private System.Windows.Forms.ListView triggerListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button triggerDeleteButton;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem newTriggerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteTriggerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editTriggerToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
	}
}
