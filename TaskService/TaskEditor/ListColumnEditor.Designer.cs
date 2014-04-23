namespace Microsoft.Win32.TaskScheduler
{
	partial class ListColumnEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListColumnEditor));
			this.cancelBtn = new System.Windows.Forms.Button();
			this.okBtn = new System.Windows.Forms.Button();
			this.upBtn = new System.Windows.Forms.Button();
			this.downBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.availColsListBox = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dispColsListBox = new System.Windows.Forms.ListBox();
			this.addBtn = new System.Windows.Forms.Button();
			this.remBtn = new System.Windows.Forms.Button();
			this.restoreBtn = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
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
			// upBtn
			// 
			resources.ApplyResources(this.upBtn, "upBtn");
			this.upBtn.Name = "upBtn";
			this.upBtn.UseVisualStyleBackColor = true;
			this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
			// 
			// downBtn
			// 
			resources.ApplyResources(this.downBtn, "downBtn");
			this.downBtn.Name = "downBtn";
			this.downBtn.UseVisualStyleBackColor = true;
			this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// availColsListBox
			// 
			resources.ApplyResources(this.availColsListBox, "availColsListBox");
			this.availColsListBox.FormattingEnabled = true;
			this.availColsListBox.Name = "availColsListBox";
			this.availColsListBox.SelectedIndexChanged += new System.EventHandler(this.availColsListBox_SelectedIndexChanged);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// dispColsListBox
			// 
			resources.ApplyResources(this.dispColsListBox, "dispColsListBox");
			this.dispColsListBox.FormattingEnabled = true;
			this.dispColsListBox.Name = "dispColsListBox";
			this.dispColsListBox.SelectedIndexChanged += new System.EventHandler(this.dispColsListBox_SelectedIndexChanged);
			// 
			// addBtn
			// 
			resources.ApplyResources(this.addBtn, "addBtn");
			this.addBtn.Name = "addBtn";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// remBtn
			// 
			resources.ApplyResources(this.remBtn, "remBtn");
			this.remBtn.Name = "remBtn";
			this.remBtn.UseVisualStyleBackColor = true;
			this.remBtn.Click += new System.EventHandler(this.remBtn_Click);
			// 
			// restoreBtn
			// 
			resources.ApplyResources(this.restoreBtn, "restoreBtn");
			this.restoreBtn.Name = "restoreBtn";
			this.restoreBtn.UseVisualStyleBackColor = true;
			this.restoreBtn.Click += new System.EventHandler(this.restoreBtn_Click);
			// 
			// groupBox1
			// 
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// ListColumnEditor
			// 
			this.AcceptButton = this.okBtn;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.dispColsListBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.availColsListBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.restoreBtn);
			this.Controls.Add(this.remBtn);
			this.Controls.Add(this.downBtn);
			this.Controls.Add(this.addBtn);
			this.Controls.Add(this.upBtn);
			this.Controls.Add(this.okBtn);
			this.Controls.Add(this.cancelBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ListColumnEditor";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Button okBtn;
		private System.Windows.Forms.Button upBtn;
		private System.Windows.Forms.Button downBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox availColsListBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox dispColsListBox;
		private System.Windows.Forms.Button addBtn;
		private System.Windows.Forms.Button remBtn;
		private System.Windows.Forms.Button restoreBtn;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}