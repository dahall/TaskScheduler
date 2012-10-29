namespace SecurityEditor
{
	partial class AceEditor
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
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.permissionGrid = new SecurityEditor.PermissionGrid();
			this.noInheritCheck = new System.Windows.Forms.CheckBox();
			this.applyToCombo = new System.Windows.Forms.ComboBox();
			this.nameText = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.changeNameBtn = new System.Windows.Forms.Button();
			this.clearAllBtn = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(207, 378);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(67, 23);
			this.okBtn.TabIndex = 1;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(280, 378);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(67, 23);
			this.cancelBtn.TabIndex = 2;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Location = new System.Drawing.Point(6, 6);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(341, 366);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.permissionGrid);
			this.tabPage1.Controls.Add(this.noInheritCheck);
			this.tabPage1.Controls.Add(this.applyToCombo);
			this.tabPage1.Controls.Add(this.nameText);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.changeNameBtn);
			this.tabPage1.Controls.Add(this.clearAllBtn);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(333, 340);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Object";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// permissionGrid
			// 
			this.permissionGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.permissionGrid.Location = new System.Drawing.Point(6, 100);
			this.permissionGrid.Name = "permissionGrid";
			this.permissionGrid.Size = new System.Drawing.Size(321, 199);
			this.permissionGrid.TabIndex = 11;
			// 
			// noInheritCheck
			// 
			this.noInheritCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.noInheritCheck.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.noInheritCheck.Enabled = false;
			this.noInheritCheck.Location = new System.Drawing.Point(9, 306);
			this.noInheritCheck.Name = "noInheritCheck";
			this.noInheritCheck.Size = new System.Drawing.Size(237, 31);
			this.noInheritCheck.TabIndex = 9;
			this.noInheritCheck.Text = "Apply these permissions to objects and/or containers within this container only";
			this.noInheritCheck.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.noInheritCheck.UseVisualStyleBackColor = true;
			this.noInheritCheck.CheckedChanged += new System.EventHandler(this.noInheritCheck_CheckedChanged);
			// 
			// applyToCombo
			// 
			this.applyToCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.applyToCombo.Enabled = false;
			this.applyToCombo.Items.AddRange(new object[] {
            "This object only"});
			this.applyToCombo.Location = new System.Drawing.Point(70, 73);
			this.applyToCombo.Name = "applyToCombo";
			this.applyToCombo.Size = new System.Drawing.Size(257, 21);
			this.applyToCombo.TabIndex = 5;
			this.applyToCombo.SelectedIndexChanged += new System.EventHandler(this.applyToCombo_SelectedIndexChanged);
			// 
			// nameText
			// 
			this.nameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameText.Location = new System.Drawing.Point(59, 46);
			this.nameText.Name = "nameText";
			this.nameText.Size = new System.Drawing.Size(187, 20);
			this.nameText.TabIndex = 2;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(5, 12);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(322, 29);
			this.label10.TabIndex = 0;
			this.label10.Text = "This permission is inherited from the parent object. Make changes here to overrid" +
    "e the inherited permissions.";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 76);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 13);
			this.label11.TabIndex = 4;
			this.label11.Text = "Apply to:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name:";
			// 
			// changeNameBtn
			// 
			this.changeNameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.changeNameBtn.Location = new System.Drawing.Point(252, 44);
			this.changeNameBtn.Name = "changeNameBtn";
			this.changeNameBtn.Size = new System.Drawing.Size(75, 23);
			this.changeNameBtn.TabIndex = 3;
			this.changeNameBtn.Text = "Change...";
			this.changeNameBtn.UseVisualStyleBackColor = true;
			this.changeNameBtn.Click += new System.EventHandler(this.changeNameBtn_Click);
			// 
			// clearAllBtn
			// 
			this.clearAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearAllBtn.Location = new System.Drawing.Point(252, 305);
			this.clearAllBtn.Name = "clearAllBtn";
			this.clearAllBtn.Size = new System.Drawing.Size(75, 23);
			this.clearAllBtn.TabIndex = 10;
			this.clearAllBtn.Text = "Clear all";
			this.clearAllBtn.UseVisualStyleBackColor = true;
			this.clearAllBtn.Click += new System.EventHandler(this.clearAllBtn_Click);
			// 
			// AceEditor
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(351, 407);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.cancelBtn);
			this.Name = "AceEditor";
			this.Text = "AceEditor";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.CheckBox noInheritCheck;
		private System.Windows.Forms.ComboBox applyToCombo;
		private System.Windows.Forms.TextBox nameText;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button changeNameBtn;
		private System.Windows.Forms.Button clearAllBtn;
		private PermissionGrid permissionGrid;
	}
}