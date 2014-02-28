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
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.columnHeader4,
            this.columnHeader5});
			resources.ApplyResources(this.permissionsListView, "permissionsListView");
			this.permissionsListView.FullRowSelect = true;
			this.permissionsListView.HideSelection = false;
			this.permissionsListView.MultiSelect = false;
			this.permissionsListView.Name = "permissionsListView";
			this.permissionsListView.SmallImageList = this.userImageList;
			this.permissionsListView.UseCompatibleStateImageBehavior = false;
			this.permissionsListView.View = System.Windows.Forms.View.Details;
			this.permissionsListView.SelectedIndexChanged += new System.EventHandler(this.permissionsListView_SelectedIndexChanged);
			this.permissionsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.permissionsListView_MouseDoubleClick);
			// 
			// imageColHdr
			// 
			resources.ApplyResources(this.imageColHdr, "imageColHdr");
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
			// columnHeader4
			// 
			resources.ApplyResources(this.columnHeader4, "columnHeader4");
			// 
			// columnHeader5
			// 
			resources.ApplyResources(this.columnHeader5, "columnHeader5");
			// 
			// userImageList
			// 
			this.userImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.userImageList, "userImageList");
			this.userImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// addBtn
			// 
			resources.ApplyResources(this.addBtn, "addBtn");
			this.addBtn.Name = "addBtn";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addButton_Click);
			// 
			// viewBtn
			// 
			resources.ApplyResources(this.viewBtn, "viewBtn");
			this.viewBtn.Name = "viewBtn";
			this.viewBtn.UseVisualStyleBackColor = true;
			this.viewBtn.Click += new System.EventHandler(this.viewBtn_Click);
			// 
			// removeBtn
			// 
			resources.ApplyResources(this.removeBtn, "removeBtn");
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// noInheritBtn
			// 
			resources.ApplyResources(this.noInheritBtn, "noInheritBtn");
			this.tableLayoutPanel1.SetColumnSpan(this.noInheritBtn, 4);
			this.noInheritBtn.Name = "noInheritBtn";
			this.noInheritBtn.UseVisualStyleBackColor = true;
			this.noInheritBtn.Click += new System.EventHandler(this.noInheritBtn_Click);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.chgPermBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.removeBtn, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.addBtn, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.viewBtn, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.noInheritBtn, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// chgPermBtn
			// 
			resources.ApplyResources(this.chgPermBtn, "chgPermBtn");
			this.chgPermBtn.Name = "chgPermBtn";
			this.chgPermBtn.UseVisualStyleBackColor = true;
			this.chgPermBtn.Click += new System.EventHandler(this.chgPermBtn_Click);
			// 
			// ACLEditor
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.permissionsListView);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ACLEditor";
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
		private System.Windows.Forms.ColumnHeader columnHeader5;
	}
}
