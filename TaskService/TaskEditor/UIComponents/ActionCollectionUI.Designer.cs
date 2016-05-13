namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ActionCollectionUI
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionCollectionUI));
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.actionListView = new Microsoft.Win32.TaskScheduler.ReorderableListView();
			this.actionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.detailsColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.actionNewButton = new System.Windows.Forms.Button();
			this.actionDeleteButton = new System.Windows.Forms.Button();
			this.actionEditButton = new System.Windows.Forms.Button();
			this.upDownTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.actionUpButton = new System.Windows.Forms.Button();
			this.actionDownButton = new System.Windows.Forms.Button();
			this.allowPowerShellConvCheck = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel2.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.upDownTableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.actionListView, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.actionNewButton, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.actionDeleteButton, 2, 2);
			this.tableLayoutPanel2.Controls.Add(this.actionEditButton, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.upDownTableLayoutPanel, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.allowPowerShellConvCheck, 0, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// actionListView
			// 
			this.actionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.actionColumnHeader,
            this.detailsColumnHeader});
			this.tableLayoutPanel2.SetColumnSpan(this.actionListView, 3);
			this.actionListView.ContextMenuStrip = this.contextMenu;
			resources.ApplyResources(this.actionListView, "actionListView");
			this.actionListView.FullRowSelect = true;
			this.actionListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.actionListView.HideSelection = false;
			this.actionListView.LargeImageList = this.imageList;
			this.actionListView.MultiSelect = false;
			this.actionListView.Name = "actionListView";
			this.actionListView.ShowItemToolTips = true;
			this.actionListView.UseCompatibleStateImageBehavior = false;
			this.actionListView.View = System.Windows.Forms.View.Details;
			this.actionListView.Reordered += new System.EventHandler<Microsoft.Win32.TaskScheduler.ListViewReorderedEventArgs>(this.actionListView_Reordered);
			this.actionListView.SelectedIndexChanged += new System.EventHandler(this.actionListView_SelectedIndexChanged);
			this.actionListView.SizeChanged += new System.EventHandler(this.actionListView_SizeChanged);
			this.actionListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.actionListView_MouseDoubleClick);
			// 
			// actionColumnHeader
			// 
			resources.ApplyResources(this.actionColumnHeader, "actionColumnHeader");
			// 
			// detailsColumnHeader
			// 
			resources.ApplyResources(this.detailsColumnHeader, "detailsColumnHeader");
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newActionToolStripMenuItem,
            this.deleteActionToolStripMenuItem,
            this.editActionToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
			this.contextMenu.Name = "contextMenuStrip1";
			this.contextMenu.ShowImageMargin = false;
			resources.ApplyResources(this.contextMenu, "contextMenu");
			// 
			// newActionToolStripMenuItem
			// 
			this.newActionToolStripMenuItem.Name = "newActionToolStripMenuItem";
			resources.ApplyResources(this.newActionToolStripMenuItem, "newActionToolStripMenuItem");
			this.newActionToolStripMenuItem.Click += new System.EventHandler(this.actionNewButton_Click);
			// 
			// deleteActionToolStripMenuItem
			// 
			this.deleteActionToolStripMenuItem.Name = "deleteActionToolStripMenuItem";
			resources.ApplyResources(this.deleteActionToolStripMenuItem, "deleteActionToolStripMenuItem");
			this.deleteActionToolStripMenuItem.Click += new System.EventHandler(this.actionDeleteButton_Click);
			// 
			// editActionToolStripMenuItem
			// 
			this.editActionToolStripMenuItem.Name = "editActionToolStripMenuItem";
			resources.ApplyResources(this.editActionToolStripMenuItem, "editActionToolStripMenuItem");
			this.editActionToolStripMenuItem.Click += new System.EventHandler(this.actionEditButton_Click);
			// 
			// moveUpToolStripMenuItem
			// 
			this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			resources.ApplyResources(this.moveUpToolStripMenuItem, "moveUpToolStripMenuItem");
			this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.actionUpButton_Click);
			// 
			// moveDownToolStripMenuItem
			// 
			this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			resources.ApplyResources(this.moveDownToolStripMenuItem, "moveDownToolStripMenuItem");
			this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.actionDownButton_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.imageList, "imageList");
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// actionNewButton
			// 
			resources.ApplyResources(this.actionNewButton, "actionNewButton");
			this.actionNewButton.Name = "actionNewButton";
			this.actionNewButton.UseVisualStyleBackColor = true;
			this.actionNewButton.Click += new System.EventHandler(this.actionNewButton_Click);
			// 
			// actionDeleteButton
			// 
			resources.ApplyResources(this.actionDeleteButton, "actionDeleteButton");
			this.actionDeleteButton.Name = "actionDeleteButton";
			this.actionDeleteButton.UseVisualStyleBackColor = true;
			this.actionDeleteButton.Click += new System.EventHandler(this.actionDeleteButton_Click);
			// 
			// actionEditButton
			// 
			resources.ApplyResources(this.actionEditButton, "actionEditButton");
			this.actionEditButton.Name = "actionEditButton";
			this.actionEditButton.UseVisualStyleBackColor = true;
			this.actionEditButton.Click += new System.EventHandler(this.actionEditButton_Click);
			// 
			// upDownTableLayoutPanel
			// 
			resources.ApplyResources(this.upDownTableLayoutPanel, "upDownTableLayoutPanel");
			this.upDownTableLayoutPanel.Controls.Add(this.actionUpButton, 0, 0);
			this.upDownTableLayoutPanel.Controls.Add(this.actionDownButton, 0, 1);
			this.upDownTableLayoutPanel.Name = "upDownTableLayoutPanel";
			// 
			// actionUpButton
			// 
			resources.ApplyResources(this.actionUpButton, "actionUpButton");
			this.actionUpButton.Name = "actionUpButton";
			this.actionUpButton.UseVisualStyleBackColor = true;
			this.actionUpButton.Click += new System.EventHandler(this.actionUpButton_Click);
			// 
			// actionDownButton
			// 
			resources.ApplyResources(this.actionDownButton, "actionDownButton");
			this.actionDownButton.Name = "actionDownButton";
			this.actionDownButton.UseVisualStyleBackColor = true;
			this.actionDownButton.Click += new System.EventHandler(this.actionDownButton_Click);
			// 
			// allowPowerShellConvCheck
			// 
			resources.ApplyResources(this.allowPowerShellConvCheck, "allowPowerShellConvCheck");
			this.tableLayoutPanel2.SetColumnSpan(this.allowPowerShellConvCheck, 3);
			this.allowPowerShellConvCheck.Name = "allowPowerShellConvCheck";
			this.allowPowerShellConvCheck.UseVisualStyleBackColor = true;
			this.allowPowerShellConvCheck.CheckedChanged += new System.EventHandler(this.allowPowerShellConvCheck_CheckedChanged);
			// 
			// ActionCollectionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.MinimumSize = new System.Drawing.Size(237, 89);
			this.Name = "ActionCollectionUI";
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.contextMenu.ResumeLayout(false);
			this.upDownTableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private ReorderableListView actionListView;
		private System.Windows.Forms.ColumnHeader actionColumnHeader;
		private System.Windows.Forms.ColumnHeader detailsColumnHeader;
		private System.Windows.Forms.Button actionNewButton;
		private System.Windows.Forms.Button actionDeleteButton;
		private System.Windows.Forms.Button actionEditButton;
		private System.Windows.Forms.TableLayoutPanel upDownTableLayoutPanel;
		private System.Windows.Forms.Button actionUpButton;
		private System.Windows.Forms.Button actionDownButton;
		private System.Windows.Forms.ContextMenuStrip contextMenu;
		private System.Windows.Forms.ToolStripMenuItem newActionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteActionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editActionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.CheckBox allowPowerShellConvCheck;
	}
}
