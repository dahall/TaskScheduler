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
			this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelBtn.Location = new System.Drawing.Point(467, 236);
			this.cancelBtn.Name = "cancelBtn";
			this.cancelBtn.Size = new System.Drawing.Size(75, 23);
			this.cancelBtn.TabIndex = 0;
			this.cancelBtn.Text = "Cancel";
			this.cancelBtn.UseVisualStyleBackColor = true;
			this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
			// 
			// okBtn
			// 
			this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okBtn.Location = new System.Drawing.Point(386, 236);
			this.okBtn.Name = "okBtn";
			this.okBtn.Size = new System.Drawing.Size(75, 23);
			this.okBtn.TabIndex = 0;
			this.okBtn.Text = "OK";
			this.okBtn.UseVisualStyleBackColor = true;
			this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
			// 
			// upBtn
			// 
			this.upBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.upBtn.Location = new System.Drawing.Point(467, 90);
			this.upBtn.Name = "upBtn";
			this.upBtn.Size = new System.Drawing.Size(75, 23);
			this.upBtn.TabIndex = 0;
			this.upBtn.Text = "M&ove Up";
			this.upBtn.UseVisualStyleBackColor = true;
			this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
			// 
			// downBtn
			// 
			this.downBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.downBtn.Location = new System.Drawing.Point(467, 119);
			this.downBtn.Name = "downBtn";
			this.downBtn.Size = new System.Drawing.Size(75, 23);
			this.downBtn.TabIndex = 0;
			this.downBtn.Text = "Mo&ve Down";
			this.downBtn.UseVisualStyleBackColor = true;
			this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "&Available columns:";
			// 
			// availColsListBox
			// 
			this.availColsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.availColsListBox.FormattingEnabled = true;
			this.availColsListBox.Location = new System.Drawing.Point(13, 30);
			this.availColsListBox.Name = "availColsListBox";
			this.availColsListBox.Size = new System.Drawing.Size(170, 186);
			this.availColsListBox.TabIndex = 2;
			this.availColsListBox.SelectedIndexChanged += new System.EventHandler(this.availColsListBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(291, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(98, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Display&ed columns:";
			// 
			// dispColsListBox
			// 
			this.dispColsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.dispColsListBox.FormattingEnabled = true;
			this.dispColsListBox.Location = new System.Drawing.Point(291, 30);
			this.dispColsListBox.Name = "dispColsListBox";
			this.dispColsListBox.Size = new System.Drawing.Size(170, 186);
			this.dispColsListBox.TabIndex = 2;
			this.dispColsListBox.SelectedIndexChanged += new System.EventHandler(this.dispColsListBox_SelectedIndexChanged);
			// 
			// addBtn
			// 
			this.addBtn.Location = new System.Drawing.Point(189, 90);
			this.addBtn.Name = "addBtn";
			this.addBtn.Size = new System.Drawing.Size(96, 23);
			this.addBtn.TabIndex = 0;
			this.addBtn.Text = "A&dd ->";
			this.addBtn.UseVisualStyleBackColor = true;
			this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
			// 
			// remBtn
			// 
			this.remBtn.Location = new System.Drawing.Point(189, 119);
			this.remBtn.Name = "remBtn";
			this.remBtn.Size = new System.Drawing.Size(96, 23);
			this.remBtn.TabIndex = 0;
			this.remBtn.Text = "<- &Remove";
			this.remBtn.UseVisualStyleBackColor = true;
			this.remBtn.Click += new System.EventHandler(this.remBtn_Click);
			// 
			// restoreBtn
			// 
			this.restoreBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.restoreBtn.Location = new System.Drawing.Point(189, 193);
			this.restoreBtn.Name = "restoreBtn";
			this.restoreBtn.Size = new System.Drawing.Size(96, 23);
			this.restoreBtn.TabIndex = 0;
			this.restoreBtn.Text = "Re&store Defaults";
			this.restoreBtn.UseVisualStyleBackColor = true;
			this.restoreBtn.Click += new System.EventHandler(this.restoreBtn_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(13, 228);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(529, 2);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// ListColumnEditor
			// 
			this.AcceptButton = this.okBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelBtn;
			this.ClientSize = new System.Drawing.Size(554, 271);
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
			this.Text = "Add/Remove Columns";
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