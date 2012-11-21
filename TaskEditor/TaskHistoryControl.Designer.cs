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
			this.historySplitContainer = new System.Windows.Forms.SplitContainer();
			this.historyListView = new System.Windows.Forms.ListView();
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.historyHeader = new System.Windows.Forms.Label();
			this.historyClearBtn = new System.Windows.Forms.Button();
			this.historyListImages = new System.Windows.Forms.ImageList(this.components);
			this.historyStopStartBtn = new System.Windows.Forms.Button();
			this.historyFilterIcon = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.historyDetailTitleText = new System.Windows.Forms.Label();
			this.historyDetailHideBtn = new System.Windows.Forms.Button();
			this.historyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.historyDetailView = new Microsoft.Win32.TaskScheduler.EventViewerControl();
			this.historySplitContainer.Panel1.SuspendLayout();
			this.historySplitContainer.Panel2.SuspendLayout();
			this.historySplitContainer.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// historySplitContainer
			// 
			this.historySplitContainer.BackColor = System.Drawing.SystemColors.Control;
			this.historySplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.historySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.historySplitContainer.Location = new System.Drawing.Point(0, 0);
			this.historySplitContainer.Name = "historySplitContainer";
			this.historySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// historySplitContainer.Panel1
			// 
			this.historySplitContainer.Panel1.Controls.Add(this.historyListView);
			this.historySplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// historySplitContainer.Panel2
			// 
			this.historySplitContainer.Panel2.AutoScroll = true;
			this.historySplitContainer.Panel2.Controls.Add(this.historyDetailView);
			this.historySplitContainer.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.historySplitContainer.Size = new System.Drawing.Size(602, 360);
			this.historySplitContainer.SplitterDistance = 166;
			this.historySplitContainer.TabIndex = 8;
			// 
			// historyListView
			// 
			this.historyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
			this.historyListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.historyListView.FullRowSelect = true;
			this.historyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.historyListView.HideSelection = false;
			this.historyListView.Location = new System.Drawing.Point(0, 25);
			this.historyListView.MultiSelect = false;
			this.historyListView.Name = "historyListView";
			this.historyListView.Size = new System.Drawing.Size(598, 137);
			this.historyListView.TabIndex = 5;
			this.historyListView.UseCompatibleStateImageBehavior = false;
			this.historyListView.View = System.Windows.Forms.View.Details;
			this.historyListView.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.historyListView_CacheVirtualItems);
			this.historyListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.historyListView_RetrieveVirtualItem);
			this.historyListView.SelectedIndexChanged += new System.EventHandler(this.historyListView_SelectedIndexChanged);
			this.historyListView.DoubleClick += new System.EventHandler(this.historyListView_DoubleClick);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Level";
			this.columnHeader6.Width = 50;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Date and Time";
			this.columnHeader7.Width = 115;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Event ID";
			this.columnHeader8.Width = 73;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Task Category";
			this.columnHeader9.Width = 88;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Operational Code";
			this.columnHeader10.Width = 77;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Correlation Id";
			this.columnHeader11.Width = 95;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.historyHeader, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.historyClearBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.historyStopStartBtn, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.historyFilterIcon, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 25);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// historyHeader
			// 
			this.historyHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyHeader.Location = new System.Drawing.Point(25, 0);
			this.historyHeader.Margin = new System.Windows.Forms.Padding(0);
			this.historyHeader.Name = "historyHeader";
			this.historyHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.historyHeader.Size = new System.Drawing.Size(334, 19);
			this.historyHeader.TabIndex = 0;
			this.historyHeader.Text = "Number of events: X";
			this.historyHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// historyClearBtn
			// 
			this.historyClearBtn.AutoSize = true;
			this.historyClearBtn.FlatAppearance.BorderSize = 0;
			this.historyClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.historyClearBtn.ImageList = this.historyListImages;
			this.historyClearBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyClearBtn.Location = new System.Drawing.Point(557, 0);
			this.historyClearBtn.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.historyClearBtn.Name = "historyClearBtn";
			this.historyClearBtn.Size = new System.Drawing.Size(19, 19);
			this.historyClearBtn.TabIndex = 0;
			this.historyClearBtn.UseVisualStyleBackColor = true;
			this.historyClearBtn.Visible = false;
			// 
			// historyListImages
			// 
			this.historyListImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.historyListImages.ImageSize = new System.Drawing.Size(16, 16);
			this.historyListImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// historyStopStartBtn
			// 
			this.historyStopStartBtn.AutoSize = true;
			this.historyStopStartBtn.FlatAppearance.BorderSize = 0;
			this.historyStopStartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.historyStopStartBtn.ImageList = this.historyListImages;
			this.historyStopStartBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyStopStartBtn.Location = new System.Drawing.Point(579, 0);
			this.historyStopStartBtn.Margin = new System.Windows.Forms.Padding(0);
			this.historyStopStartBtn.Name = "historyStopStartBtn";
			this.historyStopStartBtn.Size = new System.Drawing.Size(19, 19);
			this.historyStopStartBtn.TabIndex = 0;
			this.historyStopStartBtn.UseVisualStyleBackColor = true;
			this.historyStopStartBtn.Visible = false;
			// 
			// historyFilterIcon
			// 
			this.historyFilterIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.historyFilterIcon.ImageList = this.historyListImages;
			this.historyFilterIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyFilterIcon.Location = new System.Drawing.Point(3, 0);
			this.historyFilterIcon.Name = "historyFilterIcon";
			this.historyFilterIcon.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this.historyFilterIcon.Size = new System.Drawing.Size(19, 19);
			this.historyFilterIcon.TabIndex = 1;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.historyDetailTitleText, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.historyDetailHideBtn, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(598, 22);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// historyDetailTitleText
			// 
			this.historyDetailTitleText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyDetailTitleText.Location = new System.Drawing.Point(3, 0);
			this.historyDetailTitleText.Name = "historyDetailTitleText";
			this.historyDetailTitleText.Size = new System.Drawing.Size(400, 19);
			this.historyDetailTitleText.TabIndex = 1;
			this.historyDetailTitleText.Text = "Event 100, TaskScheduler";
			this.historyDetailTitleText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// historyDetailHideBtn
			// 
			this.historyDetailHideBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.historyDetailHideBtn.FlatAppearance.BorderSize = 0;
			this.historyDetailHideBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.historyDetailHideBtn.Font = new System.Drawing.Font("Marlett", 9F);
			this.historyDetailHideBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.historyDetailHideBtn.Location = new System.Drawing.Point(579, 0);
			this.historyDetailHideBtn.Margin = new System.Windows.Forms.Padding(0);
			this.historyDetailHideBtn.Name = "historyDetailHideBtn";
			this.historyDetailHideBtn.Size = new System.Drawing.Size(19, 19);
			this.historyDetailHideBtn.TabIndex = 0;
			this.historyDetailHideBtn.Text = "r";
			this.historyDetailHideBtn.UseVisualStyleBackColor = true;
			this.historyDetailHideBtn.Click += new System.EventHandler(this.histDetailHideBtn_Click);
			// 
			// historyBackgroundWorker
			// 
			this.historyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.historyBackgroundWorker_DoWork);
			this.historyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.historyBackgroundWorker_RunWorkerCompleted);
			// 
			// historyDetailView
			// 
			this.historyDetailView.ActiveTab = Microsoft.Win32.TaskScheduler.EventViewerControl.EventViewerActiveTab.General;
			this.historyDetailView.BackColor = System.Drawing.SystemColors.Control;
			this.historyDetailView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.historyDetailView.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.historyDetailView.Location = new System.Drawing.Point(0, 22);
			this.historyDetailView.Name = "historyDetailView";
			this.historyDetailView.Padding = new System.Windows.Forms.Padding(3);
			this.historyDetailView.Size = new System.Drawing.Size(598, 164);
			this.historyDetailView.TabIndex = 2;
			this.historyDetailView.TaskEvent = null;
			// 
			// TaskHistoryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.historySplitContainer);
			this.Name = "TaskHistoryControl";
			this.Size = new System.Drawing.Size(602, 360);
			this.historySplitContainer.Panel1.ResumeLayout(false);
			this.historySplitContainer.Panel1.PerformLayout();
			this.historySplitContainer.Panel2.ResumeLayout(false);
			this.historySplitContainer.Panel2.PerformLayout();
			this.historySplitContainer.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer historySplitContainer;
		private System.Windows.Forms.ListView historyListView;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label historyHeader;
		private System.Windows.Forms.Button historyClearBtn;
		private System.Windows.Forms.Button historyStopStartBtn;
		private System.Windows.Forms.Label historyFilterIcon;
		private EventViewerControl historyDetailView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label historyDetailTitleText;
		private System.Windows.Forms.Button historyDetailHideBtn;
		private System.Windows.Forms.ImageList historyListImages;
		private System.ComponentModel.BackgroundWorker historyBackgroundWorker;
	}
}
