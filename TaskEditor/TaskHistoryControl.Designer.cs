namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskHistoryControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskHistoryControl));
			this.historySplitContainer = new System.Windows.Forms.SplitContainer();
			this.historyListView = new Microsoft.Win32.TaskScheduler.TaskHistoryControl.ListViewEx();
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.historyListImages = new System.Windows.Forms.ImageList(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.historyHeader = new System.Windows.Forms.Label();
			this.historyClearBtn = new System.Windows.Forms.Button();
			this.refreshBtn = new System.Windows.Forms.Button();
			this.historyFilterIcon = new System.Windows.Forms.Label();
			this.historyDetailView = new Microsoft.Win32.TaskScheduler.EventViewerControl();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.historyDetailTitleText = new System.Windows.Forms.Label();
			this.historyDetailHideBtn = new System.Windows.Forms.Button();
			this.historyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.listContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.eventPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.attachTaskToThisEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAllEventsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.columnContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addremoveColumnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sortEventsByThisColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.groupEventsByThisColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.sorterBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.historySplitContainer)).BeginInit();
			this.historySplitContainer.Panel1.SuspendLayout();
			this.historySplitContainer.Panel2.SuspendLayout();
			this.historySplitContainer.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.listContextMenu.SuspendLayout();
			this.columnContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// historySplitContainer
			// 
			this.historySplitContainer.BackColor = System.Drawing.SystemColors.Control;
			this.historySplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			resources.ApplyResources(this.historySplitContainer, "historySplitContainer");
			this.historySplitContainer.Name = "historySplitContainer";
			// 
			// historySplitContainer.Panel1
			// 
			this.historySplitContainer.Panel1.Controls.Add(this.historyListView);
			this.historySplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// historySplitContainer.Panel2
			// 
			resources.ApplyResources(this.historySplitContainer.Panel2, "historySplitContainer.Panel2");
			this.historySplitContainer.Panel2.Controls.Add(this.historyDetailView);
			this.historySplitContainer.Panel2.Controls.Add(this.tableLayoutPanel2);
			// 
			// historyListView
			// 
			this.historyListView.ColumnContextMenuStrip = null;
			this.historyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
			resources.ApplyResources(this.historyListView, "historyListView");
			this.historyListView.FullRowSelect = true;
			this.historyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.historyListView.HideSelection = false;
			this.historyListView.LastColumnClicked = -1;
			this.historyListView.MultiSelect = false;
			this.historyListView.Name = "historyListView";
			this.historyListView.OwnerDraw = true;
			this.historyListView.SmallImageList = this.historyListImages;
			this.historyListView.UseCompatibleStateImageBehavior = false;
			this.historyListView.View = System.Windows.Forms.View.Details;
			this.historyListView.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.historyListView_CacheVirtualItems);
			this.historyListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.historyListView_ColumnClick);
			this.historyListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.historyListView_RetrieveVirtualItem);
			this.historyListView.SelectedIndexChanged += new System.EventHandler(this.historyListView_SelectedIndexChanged);
			this.historyListView.DoubleClick += new System.EventHandler(this.historyListView_DoubleClick);
			this.historyListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.historyListView_MouseClick);
			// 
			// columnHeader6
			// 
			resources.ApplyResources(this.columnHeader6, "columnHeader6");
			// 
			// columnHeader7
			// 
			resources.ApplyResources(this.columnHeader7, "columnHeader7");
			// 
			// columnHeader8
			// 
			resources.ApplyResources(this.columnHeader8, "columnHeader8");
			// 
			// columnHeader9
			// 
			resources.ApplyResources(this.columnHeader9, "columnHeader9");
			// 
			// columnHeader10
			// 
			resources.ApplyResources(this.columnHeader10, "columnHeader10");
			// 
			// columnHeader11
			// 
			resources.ApplyResources(this.columnHeader11, "columnHeader11");
			// 
			// historyListImages
			// 
			this.historyListImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.historyListImages, "historyListImages");
			this.historyListImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.historyHeader, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.historyClearBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.refreshBtn, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.historyFilterIcon, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// historyHeader
			// 
			resources.ApplyResources(this.historyHeader, "historyHeader");
			this.historyHeader.Name = "historyHeader";
			// 
			// historyClearBtn
			// 
			resources.ApplyResources(this.historyClearBtn, "historyClearBtn");
			this.historyClearBtn.FlatAppearance.BorderSize = 0;
			this.historyClearBtn.ImageList = this.historyListImages;
			this.historyClearBtn.Name = "historyClearBtn";
			this.historyClearBtn.UseVisualStyleBackColor = true;
			// 
			// refreshBtn
			// 
			resources.ApplyResources(this.refreshBtn, "refreshBtn");
			this.refreshBtn.FlatAppearance.BorderSize = 0;
			this.refreshBtn.ImageList = this.historyListImages;
			this.refreshBtn.Name = "refreshBtn";
			this.refreshBtn.UseVisualStyleBackColor = true;
			// 
			// historyFilterIcon
			// 
			resources.ApplyResources(this.historyFilterIcon, "historyFilterIcon");
			this.historyFilterIcon.ImageList = this.historyListImages;
			this.historyFilterIcon.Name = "historyFilterIcon";
			// 
			// historyDetailView
			// 
			this.historyDetailView.ActiveTab = Microsoft.Win32.TaskScheduler.EventViewerControl.EventViewerActiveTab.General;
			this.historyDetailView.BackColor = System.Drawing.SystemColors.Control;
			resources.ApplyResources(this.historyDetailView, "historyDetailView");
			this.historyDetailView.Name = "historyDetailView";
			this.historyDetailView.TaskEvent = null;
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.historyDetailTitleText, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.historyDetailHideBtn, 1, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// historyDetailTitleText
			// 
			resources.ApplyResources(this.historyDetailTitleText, "historyDetailTitleText");
			this.historyDetailTitleText.Name = "historyDetailTitleText";
			// 
			// historyDetailHideBtn
			// 
			resources.ApplyResources(this.historyDetailHideBtn, "historyDetailHideBtn");
			this.historyDetailHideBtn.FlatAppearance.BorderSize = 0;
			this.historyDetailHideBtn.Name = "historyDetailHideBtn";
			this.historyDetailHideBtn.UseVisualStyleBackColor = true;
			this.historyDetailHideBtn.Click += new System.EventHandler(this.histDetailHideBtn_Click);
			// 
			// historyBackgroundWorker
			// 
			this.historyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.historyBackgroundWorker_DoWork);
			this.historyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.historyBackgroundWorker_RunWorkerCompleted);
			// 
			// listContextMenu
			// 
			this.listContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.eventPropertiesToolStripMenuItem,
            this.attachTaskToThisEventToolStripMenuItem,
            this.saveAllEventsAsToolStripMenuItem});
			this.listContextMenu.Name = "listContextMenu";
			resources.ApplyResources(this.listContextMenu, "listContextMenu");
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// eventPropertiesToolStripMenuItem
			// 
			this.eventPropertiesToolStripMenuItem.Name = "eventPropertiesToolStripMenuItem";
			resources.ApplyResources(this.eventPropertiesToolStripMenuItem, "eventPropertiesToolStripMenuItem");
			this.eventPropertiesToolStripMenuItem.Click += new System.EventHandler(this.eventPropertiesToolStripMenuItem_Click);
			// 
			// attachTaskToThisEventToolStripMenuItem
			// 
			this.attachTaskToThisEventToolStripMenuItem.Name = "attachTaskToThisEventToolStripMenuItem";
			resources.ApplyResources(this.attachTaskToThisEventToolStripMenuItem, "attachTaskToThisEventToolStripMenuItem");
			this.attachTaskToThisEventToolStripMenuItem.Click += new System.EventHandler(this.attachTaskToThisEventToolStripMenuItem_Click);
			// 
			// saveAllEventsAsToolStripMenuItem
			// 
			this.saveAllEventsAsToolStripMenuItem.Name = "saveAllEventsAsToolStripMenuItem";
			resources.ApplyResources(this.saveAllEventsAsToolStripMenuItem, "saveAllEventsAsToolStripMenuItem");
			this.saveAllEventsAsToolStripMenuItem.Click += new System.EventHandler(this.saveAllEventsAsToolStripMenuItem_Click);
			// 
			// columnContextMenu
			// 
			this.columnContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addremoveColumnsToolStripMenuItem,
            this.sortEventsByThisColumnToolStripMenuItem,
            this.groupEventsByThisColumnToolStripMenuItem});
			this.columnContextMenu.Name = "columnContextMenu";
			resources.ApplyResources(this.columnContextMenu, "columnContextMenu");
			// 
			// addremoveColumnsToolStripMenuItem
			// 
			this.addremoveColumnsToolStripMenuItem.Name = "addremoveColumnsToolStripMenuItem";
			resources.ApplyResources(this.addremoveColumnsToolStripMenuItem, "addremoveColumnsToolStripMenuItem");
			this.addremoveColumnsToolStripMenuItem.Click += new System.EventHandler(this.addremoveColumnsToolStripMenuItem_Click);
			// 
			// sortEventsByThisColumnToolStripMenuItem
			// 
			this.sortEventsByThisColumnToolStripMenuItem.Name = "sortEventsByThisColumnToolStripMenuItem";
			resources.ApplyResources(this.sortEventsByThisColumnToolStripMenuItem, "sortEventsByThisColumnToolStripMenuItem");
			this.sortEventsByThisColumnToolStripMenuItem.Click += new System.EventHandler(this.sortEventsByThisColumnToolStripMenuItem_Click);
			// 
			// groupEventsByThisColumnToolStripMenuItem
			// 
			this.groupEventsByThisColumnToolStripMenuItem.Name = "groupEventsByThisColumnToolStripMenuItem";
			resources.ApplyResources(this.groupEventsByThisColumnToolStripMenuItem, "groupEventsByThisColumnToolStripMenuItem");
			this.groupEventsByThisColumnToolStripMenuItem.Click += new System.EventHandler(this.groupEventsByThisColumnToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
			// 
			// sorterBackgroundWorker
			// 
			this.sorterBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.sorterBackgroundWorker_DoWork);
			this.sorterBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.sorterBackgroundWorker_RunWorkerCompleted);
			// 
			// TaskHistoryControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.historySplitContainer);
			this.Name = "TaskHistoryControl";
			this.historySplitContainer.Panel1.ResumeLayout(false);
			this.historySplitContainer.Panel1.PerformLayout();
			this.historySplitContainer.Panel2.ResumeLayout(false);
			this.historySplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.historySplitContainer)).EndInit();
			this.historySplitContainer.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.listContextMenu.ResumeLayout(false);
			this.columnContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer historySplitContainer;
		private Microsoft.Win32.TaskScheduler.TaskHistoryControl.ListViewEx historyListView;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label historyHeader;
		private System.Windows.Forms.Button historyClearBtn;
		private System.Windows.Forms.Button refreshBtn;
		private System.Windows.Forms.Label historyFilterIcon;
		private EventViewerControl historyDetailView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label historyDetailTitleText;
		private System.Windows.Forms.Button historyDetailHideBtn;
		private System.Windows.Forms.ImageList historyListImages;
		private System.ComponentModel.BackgroundWorker historyBackgroundWorker;
		private System.Windows.Forms.ContextMenuStrip listContextMenu;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem eventPropertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem attachTaskToThisEventToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAllEventsAsToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip columnContextMenu;
		private System.Windows.Forms.ToolStripMenuItem addremoveColumnsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortEventsByThisColumnToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem groupEventsByThisColumnToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.ComponentModel.BackgroundWorker sorterBackgroundWorker;
	}
}
