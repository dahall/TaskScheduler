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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityEditorDialog));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.aclEditor1 = new SecurityEditor.ACLEditor();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.aclEditor2 = new SecurityEditor.ACLEditor();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.ownerEditor1 = new SecurityEditor.OwnerEditor();
			this.okBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.helpProvider1 = new System.Windows.Forms.HelpProvider();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.aclEditor1);
			this.helpProvider1.SetHelpKeyword(this.tabPage1, resources.GetString("tabPage1.HelpKeyword"));
			this.helpProvider1.SetHelpNavigator(this.tabPage1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tabPage1.HelpNavigator"))));
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.helpProvider1.SetShowHelp(this.tabPage1, ((bool)(resources.GetObject("tabPage1.ShowHelp"))));
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// aclEditor1
			// 
			this.aclEditor1.Display = SecurityEditor.ACLEditor.RuleType.Access;
			resources.ApplyResources(this.aclEditor1, "aclEditor1");
			this.aclEditor1.Name = "aclEditor1";
			this.aclEditor1.ObjectName = null;
			this.aclEditor1.ObjectSecurity = null;
			this.aclEditor1.TargetComputer = null;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.aclEditor2);
			this.helpProvider1.SetHelpKeyword(this.tabPage2, resources.GetString("tabPage2.HelpKeyword"));
			this.helpProvider1.SetHelpNavigator(this.tabPage2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tabPage2.HelpNavigator"))));
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.helpProvider1.SetShowHelp(this.tabPage2, ((bool)(resources.GetObject("tabPage2.ShowHelp"))));
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// aclEditor2
			// 
			this.aclEditor2.Display = SecurityEditor.ACLEditor.RuleType.Audit;
			resources.ApplyResources(this.aclEditor2, "aclEditor2");
			this.aclEditor2.Name = "aclEditor2";
			this.aclEditor2.ObjectName = null;
			this.aclEditor2.ObjectSecurity = null;
			this.aclEditor2.TargetComputer = null;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.ownerEditor1);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// ownerEditor1
			// 
			resources.ApplyResources(this.ownerEditor1, "ownerEditor1");
			this.ownerEditor1.Identity = null;
			this.ownerEditor1.Name = "ownerEditor1";
			this.ownerEditor1.ObjectName = null;
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// SecurityEditorDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.cancelBtn);
			this.Controls.Add(this.tabControl1);
			this.Name = "SecurityEditorDialog";
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
		private System.Windows.Forms.HelpProvider helpProvider1;
	}
}