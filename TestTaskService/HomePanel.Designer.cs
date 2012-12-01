namespace TestTaskService
{
	partial class HomePanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePanel));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.activePanel = new TestTaskService.HidableDetailPanel();
			this.activeListView = new System.Windows.Forms.ListView();
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.overviewPanel = new TestTaskService.HidableDetailPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.statusPanel = new TestTaskService.HidableDetailPanel();
			this.statusListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.headerPanel = new System.Windows.Forms.Panel();
			this.summaryLabel = new System.Windows.Forms.Label();
			this.footerLabel = new System.Windows.Forms.Label();
			this.refreshButton = new System.Windows.Forms.Button();
			this.footerPanel = new System.Windows.Forms.TableLayoutPanel();
			this.statusBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.activeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.tableLayoutPanel1.SuspendLayout();
			this.activePanel.DetailArea.SuspendLayout();
			this.overviewPanel.DetailArea.SuspendLayout();
			this.statusPanel.DetailArea.SuspendLayout();
			this.headerPanel.SuspendLayout();
			this.footerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoScroll = true;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.activePanel, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.overviewPanel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.statusPanel, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 635);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// activePanel
			// 
			this.activePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// activePanel.DetailArea
			// 
			this.activePanel.DetailArea.Controls.Add(this.activeListView);
			this.activePanel.DetailArea.Controls.Add(this.label6);
			this.activePanel.DetailArea.Controls.Add(this.label5);
			this.activePanel.DetailArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.activePanel.DetailArea.Location = new System.Drawing.Point(3, 27);
			this.activePanel.DetailArea.Name = "DetailArea";
			this.activePanel.DetailArea.Size = new System.Drawing.Size(625, 271);
			this.activePanel.DetailArea.TabIndex = 1;
			this.activePanel.Location = new System.Drawing.Point(11, 483);
			this.activePanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
			this.activePanel.Name = "activePanel";
			this.activePanel.Size = new System.Drawing.Size(631, 301);
			this.activePanel.TabIndex = 2;
			this.activePanel.Text = "Active Tasks";
			// 
			// activeListView
			// 
			this.activeListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.activeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
			this.activeListView.FullRowSelect = true;
			this.activeListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.activeListView.Location = new System.Drawing.Point(15, 69);
			this.activeListView.MultiSelect = false;
			this.activeListView.Name = "activeListView";
			this.activeListView.Size = new System.Drawing.Size(599, 182);
			this.activeListView.TabIndex = 2;
			this.activeListView.UseCompatibleStateImageBehavior = false;
			this.activeListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Task Name";
			this.columnHeader6.Width = 170;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Next Run Time";
			this.columnHeader7.Width = 138;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Triggers";
			this.columnHeader8.Width = 165;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Location";
			this.columnHeader9.Width = 164;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Summary: {0} total";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(371, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "Active tasks are tasks that are currently enabled and have not expired.";
			// 
			// overviewPanel
			// 
			this.overviewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// overviewPanel.DetailArea
			// 
			this.overviewPanel.DetailArea.Controls.Add(this.label2);
			this.overviewPanel.DetailArea.Controls.Add(this.label1);
			this.overviewPanel.DetailArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.overviewPanel.DetailArea.Location = new System.Drawing.Point(3, 27);
			this.overviewPanel.DetailArea.Name = "DetailArea";
			this.overviewPanel.DetailArea.Size = new System.Drawing.Size(625, 1038);
			this.overviewPanel.DetailArea.TabIndex = 1;
			this.overviewPanel.Location = new System.Drawing.Point(11, 11);
			this.overviewPanel.Name = "overviewPanel";
			this.overviewPanel.Size = new System.Drawing.Size(631, 133);
			this.overviewPanel.TabIndex = 0;
			this.overviewPanel.Text = "Overview of Task Scheduler";
			// 
			// label2
			// 
			this.label2.Image = global::TestTaskService.Properties.Resources.ts;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 43);
			this.label2.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(52, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(562, 85);
			this.label1.TabIndex = 1;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// statusPanel
			// 
			this.statusPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// statusPanel.DetailArea
			// 
			this.statusPanel.DetailArea.Controls.Add(this.statusListView);
			this.statusPanel.DetailArea.Controls.Add(this.comboBox1);
			this.statusPanel.DetailArea.Controls.Add(this.label4);
			this.statusPanel.DetailArea.Controls.Add(this.label3);
			this.statusPanel.DetailArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusPanel.DetailArea.Location = new System.Drawing.Point(3, 27);
			this.statusPanel.DetailArea.Name = "DetailArea";
			this.statusPanel.DetailArea.Size = new System.Drawing.Size(625, 285);
			this.statusPanel.DetailArea.TabIndex = 1;
			this.statusPanel.Location = new System.Drawing.Point(11, 156);
			this.statusPanel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
			this.statusPanel.Name = "statusPanel";
			this.statusPanel.Size = new System.Drawing.Size(631, 315);
			this.statusPanel.TabIndex = 1;
			this.statusPanel.Text = "Task Status";
			// 
			// statusListView
			// 
			this.statusListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.statusListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.statusListView.FullRowSelect = true;
			this.statusListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.statusListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
			this.statusListView.Location = new System.Drawing.Point(14, 97);
			this.statusListView.MultiSelect = false;
			this.statusListView.Name = "statusListView";
			this.statusListView.Size = new System.Drawing.Size(600, 173);
			this.statusListView.TabIndex = 2;
			this.statusListView.UseCompatibleStateImageBehavior = false;
			this.statusListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Task Name";
			this.columnHeader1.Width = 138;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Run Result";
			this.columnHeader2.Width = 77;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Run Start";
			this.columnHeader3.Width = 84;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Run End";
			this.columnHeader4.Width = 88;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Triggered By";
			this.columnHeader5.Width = 125;
			// 
			// comboBox1
			// 
			this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Last hour",
            "Last 24 hours",
            "Last 7 days",
            "Last 30 days"});
			this.comboBox1.Location = new System.Drawing.Point(413, 21);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(201, 23);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Summary: {0} total - {1} running, {2} succeeded, {3} stopped, {4} failed";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(326, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Status of tasks that have started in the following time period:";
			// 
			// headerPanel
			// 
			this.headerPanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.headerPanel.Controls.Add(this.summaryLabel);
			this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.headerPanel.Location = new System.Drawing.Point(0, 0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new System.Drawing.Size(670, 30);
			this.headerPanel.TabIndex = 3;
			// 
			// summaryLabel
			// 
			this.summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.summaryLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.summaryLabel.Location = new System.Drawing.Point(0, 0);
			this.summaryLabel.Name = "summaryLabel";
			this.summaryLabel.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.summaryLabel.Size = new System.Drawing.Size(670, 30);
			this.summaryLabel.TabIndex = 0;
			this.summaryLabel.Text = "Summary";
			this.summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// footerLabel
			// 
			this.footerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.footerLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.footerLabel.Location = new System.Drawing.Point(3, 0);
			this.footerLabel.Name = "footerLabel";
			this.footerLabel.Padding = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.footerLabel.Size = new System.Drawing.Size(571, 40);
			this.footerLabel.TabIndex = 1;
			this.footerLabel.Text = "Last refreshed:";
			this.footerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// refreshButton
			// 
			this.refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.refreshButton.Location = new System.Drawing.Point(580, 6);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(87, 27);
			this.refreshButton.TabIndex = 2;
			this.refreshButton.Text = "&Refresh";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// footerPanel
			// 
			this.footerPanel.ColumnCount = 2;
			this.footerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.footerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.footerPanel.Controls.Add(this.refreshButton, 1, 0);
			this.footerPanel.Controls.Add(this.footerLabel, 0, 0);
			this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.footerPanel.Location = new System.Drawing.Point(0, 665);
			this.footerPanel.Name = "footerPanel";
			this.footerPanel.RowCount = 1;
			this.footerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.footerPanel.Size = new System.Drawing.Size(670, 40);
			this.footerPanel.TabIndex = 5;
			// 
			// statusBackgroundWorker
			// 
			this.statusBackgroundWorker.WorkerSupportsCancellation = true;
			this.statusBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.statusBackgroundWorker_DoWork);
			this.statusBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.statusBackgroundWorker_RunWorkerCompleted);
			// 
			// activeBackgroundWorker
			// 
			this.activeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.activeBackgroundWorker_DoWork);
			this.activeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.activeBackgroundWorker_RunWorkerCompleted);
			// 
			// HomePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.footerPanel);
			this.Controls.Add(this.headerPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "HomePanel";
			this.Size = new System.Drawing.Size(670, 705);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.activePanel.DetailArea.ResumeLayout(false);
			this.activePanel.DetailArea.PerformLayout();
			this.overviewPanel.DetailArea.ResumeLayout(false);
			this.statusPanel.DetailArea.ResumeLayout(false);
			this.statusPanel.DetailArea.PerformLayout();
			this.headerPanel.ResumeLayout(false);
			this.footerPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private HidableDetailPanel overviewPanel;
		private HidableDetailPanel statusPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel headerPanel;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.TableLayoutPanel footerPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private HidableDetailPanel activePanel;
		public System.Windows.Forms.ListView activeListView;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.ListView statusListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ComboBox comboBox1;
		public System.Windows.Forms.Label summaryLabel;
		public System.Windows.Forms.Label footerLabel;
		private System.ComponentModel.BackgroundWorker statusBackgroundWorker;
		private System.ComponentModel.BackgroundWorker activeBackgroundWorker;
	}
}
