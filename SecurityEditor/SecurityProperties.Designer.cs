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
			this.objNameText = new System.Windows.Forms.DisplayOnlyTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.advSettingsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.advancedBtn = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.editPermLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.editBtn = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.permissionList = new SecurityEditor.PermissionList();
			this.nameLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.addRemoveLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.learnAboutLink = new System.Windows.Forms.LinkLabel();
			this.helpProvider = new System.Windows.Forms.HelpProvider();
			this.tableLayoutPanel1.SuspendLayout();
			this.advSettingsLayoutPanel.SuspendLayout();
			this.editPermLayoutPanel.SuspendLayout();
			this.permissionList.SuspendLayout();
			this.nameLayoutPanel.SuspendLayout();
			this.addRemoveLayoutPanel.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// userList
			// 
			this.userList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.userList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1});
			resources.ApplyResources(this.userList, "userList");
			this.userList.FullRowSelect = true;
			this.userList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.userList.HideSelection = false;
			this.userList.MultiSelect = false;
			this.userList.Name = "userList";
			this.userList.SmallImageList = this.userImageList;
			this.userList.UseCompatibleStateImageBehavior = false;
			this.userList.View = System.Windows.Forms.View.Details;
			this.userList.SelectedIndexChanged += new System.EventHandler(this.userList_SelectedIndexChanged);
			this.userList.SizeChanged += new System.EventHandler(this.userList_SizeChanged);
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
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
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// removeBtn
			// 
			resources.ApplyResources(this.removeBtn, "removeBtn");
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
			// 
			// addBtn
			// 
			resources.ApplyResources(this.addBtn, "addBtn");
			this.addBtn.Name = "addBtn";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// permissionsLabel
			// 
			resources.ApplyResources(this.permissionsLabel, "permissionsLabel");
			this.permissionsLabel.Name = "permissionsLabel";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// objNameText
			// 
			resources.ApplyResources(this.objNameText, "objNameText");
			this.objNameText.Name = "objNameText";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.advSettingsLayoutPanel, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.editPermLayoutPanel, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.permissionList, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.nameLayoutPanel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.userList, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.addRemoveLayoutPanel, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.learnAboutLink, 0, 8);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// advSettingsLayoutPanel
			// 
			resources.ApplyResources(this.advSettingsLayoutPanel, "advSettingsLayoutPanel");
			this.advSettingsLayoutPanel.Controls.Add(this.advancedBtn, 1, 0);
			this.advSettingsLayoutPanel.Controls.Add(this.label6, 0, 0);
			this.advSettingsLayoutPanel.Name = "advSettingsLayoutPanel";
			// 
			// advancedBtn
			// 
			resources.ApplyResources(this.advancedBtn, "advancedBtn");
			this.advancedBtn.Name = "advancedBtn";
			this.advancedBtn.UseVisualStyleBackColor = true;
			this.advancedBtn.Click += new System.EventHandler(this.advancedBtn_Click);
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// editPermLayoutPanel
			// 
			resources.ApplyResources(this.editPermLayoutPanel, "editPermLayoutPanel");
			this.editPermLayoutPanel.Controls.Add(this.editBtn, 1, 0);
			this.editPermLayoutPanel.Controls.Add(this.label2, 0, 0);
			this.editPermLayoutPanel.Name = "editPermLayoutPanel";
			// 
			// editBtn
			// 
			resources.ApplyResources(this.editBtn, "editBtn");
			this.editBtn.Name = "editBtn";
			this.editBtn.UseVisualStyleBackColor = true;
			this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// permissionList
			// 
			resources.ApplyResources(this.permissionList, "permissionList");
			this.permissionList.BackColor = System.Drawing.SystemColors.Window;
			this.permissionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.permissionList.ItemSpacing = new System.Drawing.Size(0, 5);
			this.permissionList.Name = "permissionList";
			this.permissionList.ItemChanged += new System.EventHandler<SecurityEditor.CheckedColumnList.ItemChangedEventArgs>(this.permissionList_ItemChanged);
			this.permissionList.SizeChanged += new System.EventHandler(this.permissionList_SizeChanged);
			// 
			// nameLayoutPanel
			// 
			resources.ApplyResources(this.nameLayoutPanel, "nameLayoutPanel");
			this.nameLayoutPanel.Controls.Add(this.label3, 0, 0);
			this.nameLayoutPanel.Controls.Add(this.objNameText, 1, 0);
			this.nameLayoutPanel.Name = "nameLayoutPanel";
			// 
			// addRemoveLayoutPanel
			// 
			resources.ApplyResources(this.addRemoveLayoutPanel, "addRemoveLayoutPanel");
			this.addRemoveLayoutPanel.Controls.Add(this.addBtn, 0, 0);
			this.addRemoveLayoutPanel.Controls.Add(this.removeBtn, 1, 0);
			this.addRemoveLayoutPanel.Name = "addRemoveLayoutPanel";
			// 
			// tableLayoutPanel5
			// 
			resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
			this.tableLayoutPanel5.Controls.Add(this.permissionsLabel, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.label5, 2, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			// 
			// learnAboutLink
			// 
			resources.ApplyResources(this.learnAboutLink, "learnAboutLink");
			this.learnAboutLink.Name = "learnAboutLink";
			this.learnAboutLink.TabStop = true;
			this.learnAboutLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.learnAboutLink_LinkClicked);
			// 
			// SecurityProperties
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.helpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "SecurityProperties";
			this.helpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.advSettingsLayoutPanel.ResumeLayout(false);
			this.advSettingsLayoutPanel.PerformLayout();
			this.editPermLayoutPanel.ResumeLayout(false);
			this.editPermLayoutPanel.PerformLayout();
			this.permissionList.ResumeLayout(true);
			this.nameLayoutPanel.ResumeLayout(false);
			this.nameLayoutPanel.PerformLayout();
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
		private System.Windows.Forms.DisplayOnlyTextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel advSettingsLayoutPanel;
		private System.Windows.Forms.Button advancedBtn;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel editPermLayoutPanel;
		private System.Windows.Forms.Button editBtn;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel nameLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel addRemoveLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.LinkLabel learnAboutLink;
		private System.Windows.Forms.HelpProvider helpProvider;
	}
}