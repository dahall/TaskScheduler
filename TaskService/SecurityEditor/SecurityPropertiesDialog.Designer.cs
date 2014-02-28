namespace SecurityEditor
{
	partial class SecurityPropertiesDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityPropertiesDialog));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.secTabPage = new System.Windows.Forms.TabPage();
			this.secProps = new SecurityEditor.SecurityProperties();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.applyBtn = new System.Windows.Forms.Button();
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.secTabPage.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.secTabPage);
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// secTabPage
			// 
			this.secTabPage.Controls.Add(this.secProps);
			resources.ApplyResources(this.secTabPage, "secTabPage");
			this.secTabPage.Name = "secTabPage";
			this.secTabPage.UseVisualStyleBackColor = true;
			// 
			// secProps
			// 
			resources.ApplyResources(this.secProps, "secProps");
			this.secProps.Name = "secProps";
			this.secProps.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.secProps_PropertyChanged);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.applyBtn, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cancelBtn, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.okBtn, 0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// applyBtn
			// 
			resources.ApplyResources(this.applyBtn, "applyBtn");
			this.applyBtn.Name = "applyBtn";
			this.applyBtn.UseVisualStyleBackColor = true;
			this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
			// 
			// cancelBtn
			// 
			resources.ApplyResources(this.cancelBtn, "cancelBtn");
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			resources.ApplyResources(this.okBtn, "okBtn");
			this.okBtn.Name = "okBtn";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// SecurityPropertiesDialog
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "SecurityPropertiesDialog";
			this.tabControl1.ResumeLayout(false);
			this.secTabPage.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage secTabPage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button applyBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		internal SecurityProperties secProps;
	}
}