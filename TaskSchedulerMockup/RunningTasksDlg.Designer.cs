namespace TaskSchedulerMockup
{
	partial class RunningTasksDlg
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
			this.closeBtn = new System.Windows.Forms.Button();
			this.refreshBtn = new System.Windows.Forms.Button();
			this.endTaskBtn = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// closeBtn
			// 
			this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeBtn.Location = new System.Drawing.Point(478, 342);
			this.closeBtn.Name = "closeBtn";
			this.closeBtn.Size = new System.Drawing.Size(75, 23);
			this.closeBtn.TabIndex = 0;
			this.closeBtn.Text = "Close";
			this.closeBtn.UseVisualStyleBackColor = true;
			this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// refreshBtn
			// 
			this.refreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.refreshBtn.Location = new System.Drawing.Point(397, 342);
			this.refreshBtn.Name = "refreshBtn";
			this.refreshBtn.Size = new System.Drawing.Size(75, 23);
			this.refreshBtn.TabIndex = 1;
			this.refreshBtn.Text = "Refresh";
			this.refreshBtn.UseVisualStyleBackColor = true;
			this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
			// 
			// endTaskBtn
			// 
			this.endTaskBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.endTaskBtn.Enabled = false;
			this.endTaskBtn.Location = new System.Drawing.Point(478, 290);
			this.endTaskBtn.Name = "endTaskBtn";
			this.endTaskBtn.Size = new System.Drawing.Size(75, 23);
			this.endTaskBtn.TabIndex = 2;
			this.endTaskBtn.Text = "End Task";
			this.endTaskBtn.UseVisualStyleBackColor = true;
			this.endTaskBtn.Click += new System.EventHandler(this.endTaskBtn_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.listView1.FullRowSelect = true;
			this.listView1.LabelWrap = false;
			this.listView1.Location = new System.Drawing.Point(13, 13);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.Size = new System.Drawing.Size(540, 271);
			this.listView1.TabIndex = 3;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(13, 326);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(540, 2);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Task Name";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Started";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Run Duration";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Current Action";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Task Folder";
			// 
			// RunningTasksDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 377);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.endTaskBtn);
			this.Controls.Add(this.refreshBtn);
			this.Controls.Add(this.closeBtn);
			this.Name = "RunningTasksDlg";
			this.Text = "All Running Tasks";
			this.Load += new System.EventHandler(this.RunningTasksDlg_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button closeBtn;
		private System.Windows.Forms.Button refreshBtn;
		private System.Windows.Forms.Button endTaskBtn;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
	}
}