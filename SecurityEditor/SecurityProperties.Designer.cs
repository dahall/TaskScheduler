namespace SecurityEditor
{
	partial class SecurityProperties
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityProperties));
			this.userList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.userImageList = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.removeBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.permissionsLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.nameLookUpper = new System.ComponentModel.BackgroundWorker();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.advSettingsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.advancedBtn = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.editPermLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.editBtn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.permissionList = new SecurityEditor.PermissionList();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.addRemoveLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.learnAboutLink = new System.Windows.Forms.LinkLabel();
			this.tableLayoutPanel1.SuspendLayout();
			this.advSettingsLayoutPanel.SuspendLayout();
			this.editPermLayoutPanel.SuspendLayout();
			this.permissionList.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.addRemoveLayoutPanel.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// userList
			// 
			this.userList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.userList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.userList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userList.FullRowSelect = true;
			this.userList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.userList.HideSelection = false;
			this.userList.LabelWrap = false;
			this.userList.Location = new System.Drawing.Point(3, 46);
			this.userList.MultiSelect = false;
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(355, 121);
			this.userList.SmallImageList = this.userImageList;
			this.userList.TabIndex = 0;
			this.userList.UseCompatibleStateImageBehavior = false;
			this.userList.View = System.Windows.Forms.View.Details;
			this.userList.SelectedIndexChanged += new System.EventHandler(this.userList_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 316;
			// 
			// userImageList
			// 
			this.userImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("userImageList.ImageStream")));
			this.userImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.userImageList.Images.SetKeyName(0, "dsuiext_4099.ico");
			this.userImageList.Images.SetKeyName(1, "dsuiext_4108.ico");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(0, 30);
			this.label1.Margin = new System.Windows.Forms.Padding(0, 11, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Group or user names:";
			// 
			// removeBtn
			// 
			this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.removeBtn.Enabled = false;
			this.removeBtn.Location = new System.Drawing.Point(284, 3);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Size = new System.Drawing.Size(74, 22);
			this.removeBtn.TabIndex = 2;
			this.removeBtn.Text = "Remove";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(204, 3);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(74, 22);
			this.addBtn.TabIndex = 2;
			this.addBtn.Text = "Add...";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// permissionsLabel
			// 
			this.permissionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.permissionsLabel.Location = new System.Drawing.Point(0, 0);
			this.permissionsLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.permissionsLabel.Name = "permissionsLabel";
			this.permissionsLabel.Size = new System.Drawing.Size(249, 22);
			this.permissionsLabel.TabIndex = 1;
			this.permissionsLabel.Text = "Permissions:";
			this.permissionsLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(255, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Allow";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(308, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Deny";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// objNameText
			// 
			this.objNameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.objNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objNameText.Location = new System.Drawing.Point(76, 3);
			this.objNameText.Name = "objNameText";
			this.objNameText.ReadOnly = true;
			this.objNameText.Size = new System.Drawing.Size(282, 13);
			this.objNameText.TabIndex = 14;
			this.objNameText.TabStop = false;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 19);
			this.label3.TabIndex = 13;
			this.label3.Text = "Object name:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// nameLookUpper
			// 
			this.nameLookUpper.WorkerReportsProgress = true;
			this.nameLookUpper.WorkerSupportsCancellation = true;
			this.nameLookUpper.DoWork += new System.ComponentModel.DoWorkEventHandler(this.nameLookUpper_DoWork);
			this.nameLookUpper.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.nameLookUpper_ProgressChanged);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.advSettingsLayoutPanel, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.editPermLayoutPanel, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.permissionList, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.userList, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.addRemoveLayoutPanel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.learnAboutLink, 0, 8);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(361, 458);
			this.tableLayoutPanel1.TabIndex = 15;
			// 
			// advSettingsLayoutPanel
			// 
			this.advSettingsLayoutPanel.AutoSize = true;
			this.advSettingsLayoutPanel.ColumnCount = 2;
			this.advSettingsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.advSettingsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.advSettingsLayoutPanel.Controls.Add(this.advancedBtn, 1, 0);
			this.advSettingsLayoutPanel.Controls.Add(this.label6, 0, 0);
			this.advSettingsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.advSettingsLayoutPanel.Location = new System.Drawing.Point(0, 403);
			this.advSettingsLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.advSettingsLayoutPanel.Name = "advSettingsLayoutPanel";
			this.advSettingsLayoutPanel.RowCount = 1;
			this.advSettingsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.advSettingsLayoutPanel.Size = new System.Drawing.Size(361, 35);
			this.advSettingsLayoutPanel.TabIndex = 13;
			// 
			// advancedBtn
			// 
			this.advancedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.advancedBtn.Location = new System.Drawing.Point(269, 3);
			this.advancedBtn.Name = "advancedBtn";
			this.advancedBtn.Size = new System.Drawing.Size(89, 22);
			this.advancedBtn.TabIndex = 2;
			this.advancedBtn.Text = "&Advanced";
			this.advancedBtn.UseVisualStyleBackColor = true;
			this.advancedBtn.Click += new System.EventHandler(this.advancedBtn_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(0, 3);
			this.label6.Margin = new System.Windows.Forms.Padding(0, 3, 3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(263, 26);
			this.label6.TabIndex = 3;
			this.label6.Text = "For special permissions or advanced settings, click Advanced.";
			// 
			// editPermLayoutPanel
			// 
			this.editPermLayoutPanel.AutoSize = true;
			this.editPermLayoutPanel.ColumnCount = 2;
			this.editPermLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.editPermLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.editPermLayoutPanel.Controls.Add(this.editBtn, 1, 0);
			this.editPermLayoutPanel.Controls.Add(this.label2, 0, 0);
			this.editPermLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.editPermLayoutPanel.Location = new System.Drawing.Point(0, 198);
			this.editPermLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.editPermLayoutPanel.Name = "editPermLayoutPanel";
			this.editPermLayoutPanel.RowCount = 1;
			this.editPermLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.editPermLayoutPanel.Size = new System.Drawing.Size(361, 28);
			this.editPermLayoutPanel.TabIndex = 3;
			// 
			// editBtn
			// 
			this.editBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.editBtn.Location = new System.Drawing.Point(269, 3);
			this.editBtn.Name = "editBtn";
			this.editBtn.Size = new System.Drawing.Size(89, 22);
			this.editBtn.TabIndex = 2;
			this.editBtn.Text = "&Edit";
			this.editBtn.UseVisualStyleBackColor = true;
			this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(168, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "To change permissions, click Edit.";
			// 
			// permissionList
			// 
			this.permissionList.AutoScrollMinSize = new System.Drawing.Size(353, 113);
			this.permissionList.AutoSize = false;
			this.permissionList.BackColor = System.Drawing.SystemColors.Window;
			this.permissionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.permissionList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.permissionList.ItemSpacing = new System.Drawing.Size(0, 5);
			this.permissionList.Location = new System.Drawing.Point(3, 251);
			this.permissionList.Name = "permissionList";
			this.permissionList.Padding = new System.Windows.Forms.Padding(8, 5, 0, 5);
			this.permissionList.Size = new System.Drawing.Size(355, 149);
			this.permissionList.TabIndex = 12;
			this.permissionList.SizeChanged += new System.EventHandler(this.permissionList_SizeChanged);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.objNameText, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(361, 19);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// addRemoveLayoutPanel
			// 
			this.addRemoveLayoutPanel.AutoSize = true;
			this.addRemoveLayoutPanel.ColumnCount = 2;
			this.addRemoveLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.addRemoveLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.addRemoveLayoutPanel.Controls.Add(this.addBtn, 0, 0);
			this.addRemoveLayoutPanel.Controls.Add(this.removeBtn, 1, 0);
			this.addRemoveLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.addRemoveLayoutPanel.Location = new System.Drawing.Point(0, 170);
			this.addRemoveLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.addRemoveLayoutPanel.Name = "addRemoveLayoutPanel";
			this.addRemoveLayoutPanel.RowCount = 1;
			this.addRemoveLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.addRemoveLayoutPanel.Size = new System.Drawing.Size(361, 28);
			this.addRemoveLayoutPanel.TabIndex = 2;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.AutoSize = true;
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
			this.tableLayoutPanel5.Controls.Add(this.permissionsLabel, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.label5, 2, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 226);
			this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(358, 22);
			this.tableLayoutPanel5.TabIndex = 4;
			// 
			// learnAboutLink
			// 
			this.learnAboutLink.AutoSize = true;
			this.learnAboutLink.Location = new System.Drawing.Point(0, 444);
			this.learnAboutLink.Margin = new System.Windows.Forms.Padding(0, 6, 3, 0);
			this.learnAboutLink.Name = "learnAboutLink";
			this.learnAboutLink.Size = new System.Drawing.Size(214, 13);
			this.learnAboutLink.TabIndex = 14;
			this.learnAboutLink.TabStop = true;
			this.learnAboutLink.Text = "Learn about access control and permissions";
			this.learnAboutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.learnAboutLink_LinkClicked);
			// 
			// SecurityProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "SecurityProperties";
			this.Size = new System.Drawing.Size(361, 458);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.advSettingsLayoutPanel.ResumeLayout(false);
			this.advSettingsLayoutPanel.PerformLayout();
			this.editPermLayoutPanel.ResumeLayout(false);
			this.editPermLayoutPanel.PerformLayout();
			this.permissionList.ResumeLayout(true);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.addRemoveLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView userList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList userImageList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button removeBtn;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Label permissionsLabel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private SecurityEditor.PermissionList permissionList;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.BackgroundWorker nameLookUpper;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel advSettingsLayoutPanel;
		private System.Windows.Forms.Button advancedBtn;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel editPermLayoutPanel;
		private System.Windows.Forms.Button editBtn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel addRemoveLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.LinkLabel learnAboutLink;
	}
}