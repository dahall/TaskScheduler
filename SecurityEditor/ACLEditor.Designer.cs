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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.permissionsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.inheritedCheck = new System.Windows.Forms.CheckBox();
			this.addButton = new System.Windows.Forms.Button();
			this.editButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(491, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "To view or edit deails for a permission entry, select the entry and the click Edi" +
    "t or double-click the entry.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Object name:";
			// 
			// objNameText
			// 
			this.objNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objNameText.Location = new System.Drawing.Point(92, 34);
			this.objNameText.Name = "objNameText";
			this.objNameText.ReadOnly = true;
			this.objNameText.Size = new System.Drawing.Size(323, 13);
			this.objNameText.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(94, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Permission entries:";
			// 
			// permissionsListView
			// 
			this.permissionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.permissionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.permissionsListView.FullRowSelect = true;
			this.permissionsListView.HideSelection = false;
			this.permissionsListView.Location = new System.Drawing.Point(7, 82);
			this.permissionsListView.MultiSelect = false;
			this.permissionsListView.Name = "permissionsListView";
			this.permissionsListView.Size = new System.Drawing.Size(566, 182);
			this.permissionsListView.TabIndex = 2;
			this.permissionsListView.UseCompatibleStateImageBehavior = false;
			this.permissionsListView.View = System.Windows.Forms.View.Details;
			this.permissionsListView.SelectedIndexChanged += new System.EventHandler(this.permissionsListView_SelectedIndexChanged);
			this.permissionsListView.SizeChanged += new System.EventHandler(this.permissionsListView_SizeChanged);
			this.permissionsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.permissionsListView_MouseDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Type";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Name";
			this.columnHeader2.Width = 202;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Permission";
			this.columnHeader3.Width = 117;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Inherited From";
			this.columnHeader4.Width = 182;
			// 
			// inheritedCheck
			// 
			this.inheritedCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.inheritedCheck.AutoSize = true;
			this.inheritedCheck.Location = new System.Drawing.Point(7, 301);
			this.inheritedCheck.Name = "inheritedCheck";
			this.inheritedCheck.Size = new System.Drawing.Size(283, 17);
			this.inheritedCheck.TabIndex = 6;
			this.inheritedCheck.Text = "Include inheritable permissions from this object\'s parent";
			this.inheritedCheck.UseVisualStyleBackColor = true;
			this.inheritedCheck.CheckedChanged += new System.EventHandler(this.inheritedCheck_CheckedChanged);
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.addButton.Location = new System.Drawing.Point(7, 270);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(81, 23);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "Add...";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// editButton
			// 
			this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.editButton.Enabled = false;
			this.editButton.Location = new System.Drawing.Point(94, 270);
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(81, 23);
			this.editButton.TabIndex = 4;
			this.editButton.Text = "Edit...";
			this.editButton.UseVisualStyleBackColor = true;
			this.editButton.Click += new System.EventHandler(this.editButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.removeButton.Enabled = false;
			this.removeButton.Location = new System.Drawing.Point(181, 270);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(81, 23);
			this.removeButton.TabIndex = 5;
			this.removeButton.Text = "Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// ACLEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.editButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.inheritedCheck);
			this.Controls.Add(this.permissionsListView);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MinimumSize = new System.Drawing.Size(534, 291);
			this.Name = "ACLEditor";
			this.Size = new System.Drawing.Size(580, 326);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView permissionsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.CheckBox inheritedCheck;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button removeButton;
	}
}
