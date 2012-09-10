namespace SecurityEditor
{
	partial class SecurityEditorDialog
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.aclEditor1 = new SecurityEditor.ACLEditor();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.aclEditor2 = new SecurityEditor.ACLEditor();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.ownerEditor1 = new SecurityEditor.OwnerEditor();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(4, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(666, 391);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.aclEditor1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(658, 365);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Permissions";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// aclEditor1
			// 
			this.aclEditor1.Display = SecurityEditor.ACLEditor.RuleType.Access;
			this.aclEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aclEditor1.Location = new System.Drawing.Point(3, 3);
			this.aclEditor1.MinimumSize = new System.Drawing.Size(534, 291);
			this.aclEditor1.Name = "aclEditor1";
			this.aclEditor1.ObjectName = null;
			this.aclEditor1.ObjectSecurity = null;
			this.aclEditor1.Size = new System.Drawing.Size(652, 359);
			this.aclEditor1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.aclEditor2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(658, 365);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Auditing";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// aclEditor2
			// 
			this.aclEditor2.Display = SecurityEditor.ACLEditor.RuleType.Audit;
			this.aclEditor2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.aclEditor2.Location = new System.Drawing.Point(3, 3);
			this.aclEditor2.MinimumSize = new System.Drawing.Size(534, 291);
			this.aclEditor2.Name = "aclEditor2";
			this.aclEditor2.ObjectName = null;
			this.aclEditor2.ObjectSecurity = null;
			this.aclEditor2.Size = new System.Drawing.Size(652, 359);
			this.aclEditor2.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.ownerEditor1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(658, 365);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Owner";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// ownerEditor1
			// 
			this.ownerEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ownerEditor1.Identity = null;
			this.ownerEditor1.Location = new System.Drawing.Point(0, 0);
			this.ownerEditor1.Name = "ownerEditor1";
			this.ownerEditor1.ObjectName = null;
			this.ownerEditor1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.ownerEditor1.Size = new System.Drawing.Size(658, 365);
			this.ownerEditor1.TabIndex = 0;
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(530, 400);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(67, 23);
			this.okBtn.TabIndex = 3;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(603, 400);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(67, 23);
			this.cancelBtn.TabIndex = 4;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			// 
			// SecurityEditorDialog
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(676, 428);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.tabControl1);
			this.Name = "SecurityEditorDialog";
			this.Text = "SecurityEditorDialog";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button cancelBtn;
		private OwnerEditor ownerEditor1;
		private ACLEditor aclEditor1;
		private ACLEditor aclEditor2;
	}
}