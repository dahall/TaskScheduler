namespace SecurityEditor
{
	partial class OwnerEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnerEditor));
			this.objNameText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.currentOwnerText = new System.Windows.Forms.TextBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.ownerListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.otherUserButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// objNameText
			// 
			this.objNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objNameText.Location = new System.Drawing.Point(93, 34);
			this.objNameText.Name = "objNameText";
			this.objNameText.ReadOnly = true;
			this.objNameText.Size = new System.Drawing.Size(323, 13);
			this.objNameText.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Current owner:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Object name:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(463, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "You can take or assign ownership of this object if you have the required permissi" +
    "ons or privileges.";
			// 
			// currentOwnerText
			// 
			this.currentOwnerText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.currentOwnerText.Location = new System.Drawing.Point(7, 82);
			this.currentOwnerText.Name = "currentOwnerText";
			this.currentOwnerText.Size = new System.Drawing.Size(532, 20);
			this.currentOwnerText.TabIndex = 6;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "backarrow.bmp");
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 111);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Change owner to:";
			// 
			// ownerListView
			// 
			this.ownerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ownerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.ownerListView.FullRowSelect = true;
			this.ownerListView.HideSelection = false;
			this.ownerListView.LabelWrap = false;
			this.ownerListView.Location = new System.Drawing.Point(6, 127);
			this.ownerListView.MultiSelect = false;
			this.ownerListView.Name = "ownerListView";
			this.ownerListView.Size = new System.Drawing.Size(533, 137);
			this.ownerListView.SmallImageList = this.imageList1;
			this.ownerListView.TabIndex = 7;
			this.ownerListView.UseCompatibleStateImageBehavior = false;
			this.ownerListView.View = System.Windows.Forms.View.Details;
			this.ownerListView.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 529;
			// 
			// otherUserButton
			// 
			this.otherUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.otherUserButton.Location = new System.Drawing.Point(6, 270);
			this.otherUserButton.Name = "otherUserButton";
			this.otherUserButton.Size = new System.Drawing.Size(161, 23);
			this.otherUserButton.TabIndex = 9;
			this.otherUserButton.Text = "Other users or groups...";
			this.otherUserButton.UseVisualStyleBackColor = true;
			// 
			// OwnerEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.otherUserButton);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.ownerListView);
			this.Controls.Add(this.currentOwnerText);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "OwnerEditor";
			this.Size = new System.Drawing.Size(547, 300);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox currentOwnerText;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView ownerListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button otherUserButton;
	}
}
