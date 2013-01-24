namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskSDDLEditDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskSDDLEditDialog));
			this.userList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.userImageList = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.removeBtn = new System.Windows.Forms.Button();
			this.addBtn = new System.Windows.Forms.Button();
			this.permissionsLabel = new System.Windows.Forms.Label();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.permissionList = new SecurityEditor.PermissionList();
			this.objNameText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.nameLookUpper = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// userList
			// 
			this.userList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.userList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.userList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.userList.FullRowSelect = true;
			this.userList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.userList.HideSelection = false;
			this.userList.LabelWrap = false;
			this.userList.Location = new System.Drawing.Point(12, 54);
			this.userList.MultiSelect = false;
			this.userList.Name = "userList";
			this.userList.Size = new System.Drawing.Size(318, 107);
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
			this.userImageList.Images.SetKeyName(0, "backarrow.bmp");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Group or user names:";
			// 
			// removeBtn
			// 
			this.removeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.removeBtn.Location = new System.Drawing.Point(263, 167);
			this.removeBtn.Name = "removeBtn";
			this.removeBtn.Size = new System.Drawing.Size(67, 23);
			this.removeBtn.TabIndex = 2;
			this.removeBtn.Text = "Remove";
			this.removeBtn.UseVisualStyleBackColor = true;
			this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
			// 
			// addBtn
			// 
			this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.addBtn.Location = new System.Drawing.Point(190, 167);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(67, 23);
			this.addBtn.TabIndex = 2;
			this.addBtn.Text = "Add...";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// permissionsLabel
			// 
			this.permissionsLabel.Location = new System.Drawing.Point(10, 189);
			this.permissionsLabel.Name = "permissionsLabel";
			this.permissionsLabel.Size = new System.Drawing.Size(160, 30);
			this.permissionsLabel.TabIndex = 1;
			this.permissionsLabel.Text = "Permissions:";
			this.permissionsLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(263, 330);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(67, 23);
			this.cancelBtn.TabIndex = 2;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(190, 330);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(67, 23);
			this.okBtn.TabIndex = 2;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(218, 206);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Allow";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(270, 206);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Deny";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// permissionList
			// 
			this.permissionList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.permissionList.AutoScrollMinSize = new System.Drawing.Size(299, 113);
			this.permissionList.AutoSize = false;
			this.permissionList.BackColor = System.Drawing.SystemColors.Window;
			this.permissionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.permissionList.ItemSpacing = new System.Drawing.Size(0, 5);
			this.permissionList.Location = new System.Drawing.Point(12, 222);
			this.permissionList.Name = "permissionList";
			this.permissionList.Padding = new System.Windows.Forms.Padding(8, 5, 0, 5);
			this.permissionList.Size = new System.Drawing.Size(318, 102);
			this.permissionList.TabIndex = 12;
			// 
			// objNameText
			// 
			this.objNameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.objNameText.Location = new System.Drawing.Point(86, 9);
			this.objNameText.Name = "objNameText";
			this.objNameText.ReadOnly = true;
			this.objNameText.Size = new System.Drawing.Size(246, 13);
			this.objNameText.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Object name:";
			// 
			// nameLookUpper
			// 
			this.nameLookUpper.WorkerReportsProgress = true;
			this.nameLookUpper.WorkerSupportsCancellation = true;
			this.nameLookUpper.DoWork += new System.ComponentModel.DoWorkEventHandler(this.nameLookUpper_DoWork);
			this.nameLookUpper.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.nameLookUpper_ProgressChanged);
			// 
			// TaskSDDLEditDialog
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(342, 365);
			this.Controls.Add(this.objNameText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.permissionList);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.addBtn);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.removeBtn);
			this.Controls.Add(this.permissionsLabel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.userList);
			this.Name = "TaskSDDLEditDialog";
			this.Text = "Permissions for <Task>";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView userList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList userImageList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button removeBtn;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Label permissionsLabel;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private SecurityEditor.PermissionList permissionList;
		private System.Windows.Forms.TextBox objNameText;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.BackgroundWorker nameLookUpper;
	}
}