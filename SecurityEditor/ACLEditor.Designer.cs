namespace SecurityEditor
{
	partial class ACLEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACLEditor));
			this.permissionsListView = new System.Windows.Forms.ListView();
			this.imageColHdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.userImageList = new System.Windows.Forms.ImageList(this.components);
			this.addBtn = new System.Windows.Forms.Button();
			this.viewBtn = new System.Windows.Forms.Button();
			this.removeBtn = new System.Windows.Forms.Button();
			this.noInheritBtn = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.chgPermBtn = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// permissionsListView
			// 
			this.permissionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.imageColHdr,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.permissionsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.permissionsListView.FullRowSelect = true;
			this.permissionsListView.HideSelection = false;
			this.permissionsListView.Location = new System.Drawing.Point(1, 1);
			this.permissionsListView.MultiSelect = false;
			this.permissionsListView.Name = "permissionsListView";
			this.permissionsListView.Size = new System.Drawing.Size(572, 158);
			this.permissionsListView.SmallImageList = this.userImageList;
			this.permissionsListView.TabIndex = 1;
			this.permissionsListView.UseCompatibleStateImageBehavior = false;
			this.permissionsListView.View = System.Windows.Forms.View.Details;
			this.permissionsListView.SelectedIndexChanged += new System.EventHandler(this.permissionsListView_SelectedIndexChanged);
			this.permissionsListView.SizeChanged += new System.EventHandler(this.permissionsListView_SizeChanged);
			this.permissionsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.permissionsListView_MouseDoubleClick);
			// 
			// imageColHdr
			// 
			this.imageColHdr.Text = " ";
			this.imageColHdr.Width = 20;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Type";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Principal";
			this.columnHeader2.Width = 202;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Access";
			this.columnHeader3.Width = 117;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Inherited from";
			this.columnHeader4.Width = 182;
			// 
			// userImageList
			// 
			this.userImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("userImageList.ImageStream")));
			this.userImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.userImageList.Images.SetKeyName(0, "dsuiext_4099.ico");
			this.userImageList.Images.SetKeyName(1, "dsuiext_4108.ico");
			// 
			// addBtn
			// 
			this.addBtn.AutoSize = true;
			this.addBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.addBtn.Dock = System.Windows.Forms.DockStyle.Left;
			this.addBtn.Location = new System.Drawing.Point(0, 10);
			this.addBtn.Margin = new System.Windows.Forms.Padding(0, 10, 6, 0);
			this.addBtn.MinimumSize = new System.Drawing.Size(74, 0);
			this.addBtn.Name = "addBtn";
			this.addBtn.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.addBtn.Size = new System.Drawing.Size(74, 23);
			this.addBtn.TabIndex = 2;
			this.addBtn.Text = "A&dd...";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addButton_Click);
			// 
			// viewBtn
			// 
			this.viewBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.viewBtn.AutoSize = true;
			this.viewBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.viewBtn.Enabled = false;
			this.viewBtn.Location = new System.Drawing.Point(285, 10);
			this.viewBtn.Margin = new System.Windows.Forms.Padding(0, 10, 6, 0);
			this.viewBtn.MinimumSize = new System.Drawing.Size(74, 0);
			this.viewBtn.Name = "viewBtn";
			this.viewBtn.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.viewBtn.Size = new System.Drawing.Size(74, 23);
			this.viewBtn.TabIndex = 4;
			this.viewBtn.Text = "&View";
			this.viewBtn.UseVisualStyleBackColor = true;
			this.viewBtn.Click += new System.EventHandler(this.editButton_Click);
			// 
			// removeBtn
			// 
			this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removeBtn.AutoSize = true;
			this.removeBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.removeBtn.Enabled = false;
			this.removeBtn.Location = new System.Drawing.Point(80, 10);
			this.removeBtn.Margin = new System.Windows.Forms.Padding(0, 10, 6, 0);
			this.removeBtn.MinimumSize = new System.Drawing.Size(74, 0);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.removeBtn.Size = new System.Drawing.Size(74, 23);
			this.removeBtn.TabIndex = 3;
			this.removeBtn.Text = "&Remove";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// noInheritBtn
			// 
			this.noInheritBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.noInheritBtn.AutoSize = true;
			this.noInheritBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.SetColumnSpan(this.noInheritBtn, 4);
			this.noInheritBtn.Enabled = false;
			this.noInheritBtn.Location = new System.Drawing.Point(0, 43);
			this.noInheritBtn.Margin = new System.Windows.Forms.Padding(0, 10, 6, 0);
			this.noInheritBtn.Name = "noInheritBtn";
			this.noInheritBtn.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.noInheritBtn.Size = new System.Drawing.Size(115, 23);
			this.noInheritBtn.TabIndex = 5;
			this.noInheritBtn.Text = "Disable &inheritance";
			this.noInheritBtn.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.chgPermBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.removeBtn, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.addBtn, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.viewBtn, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.noInheritBtn, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 159);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 66);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// chgPermBtn
			// 
			this.chgPermBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chgPermBtn.AutoSize = true;
			this.chgPermBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.chgPermBtn.Location = new System.Drawing.Point(160, 10);
			this.chgPermBtn.Margin = new System.Windows.Forms.Padding(0, 10, 6, 0);
			this.chgPermBtn.Name = "chgPermBtn";
			this.chgPermBtn.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.chgPermBtn.Size = new System.Drawing.Size(119, 23);
			this.chgPermBtn.TabIndex = 2;
			this.chgPermBtn.Text = "&Change permissions";
			this.chgPermBtn.UseVisualStyleBackColor = true;
			this.chgPermBtn.Click += new System.EventHandler(this.addButton_Click);
			// 
			// ACLEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.permissionsListView);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ACLEditor";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(574, 226);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView permissionsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Button viewBtn;
		private System.Windows.Forms.Button removeBtn;
		private System.Windows.Forms.Button noInheritBtn;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button chgPermBtn;
		private System.Windows.Forms.ImageList userImageList;
		private System.Windows.Forms.ColumnHeader imageColHdr;
	}
}
