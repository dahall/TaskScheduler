namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ExecActionUI
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
			this.execProgBrowseBtn = new System.Windows.Forms.Button();
			this.execDirText = new System.Windows.Forms.TextBox();
			this.execArgText = new System.Windows.Forms.TextBox();
			this.execProgText = new System.Windows.Forms.TextBox();
			this.execDirLabel = new System.Windows.Forms.Label();
			this.execArgLabel = new System.Windows.Forms.Label();
			this.execProgLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// execProgBrowseBtn
			// 
			this.execProgBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.execProgBrowseBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.execProgBrowseBtn.Location = new System.Drawing.Point(321, 17);
			this.execProgBrowseBtn.Name = "execProgBrowseBtn";
			this.execProgBrowseBtn.Size = new System.Drawing.Size(114, 25);
			this.execProgBrowseBtn.TabIndex = 9;
			this.execProgBrowseBtn.Text = "Browse...";
			this.execProgBrowseBtn.UseVisualStyleBackColor = true;
			this.execProgBrowseBtn.Click += new System.EventHandler(this.execProgBrowseBtn_Click);
			// 
			// execDirText
			// 
			this.execDirText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.execDirText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execDirText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.execDirText.Location = new System.Drawing.Point(159, 78);
			this.execDirText.Name = "execDirText";
			this.execDirText.Size = new System.Drawing.Size(275, 23);
			this.execDirText.TabIndex = 13;
			// 
			// execArgText
			// 
			this.execArgText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.execArgText.Location = new System.Drawing.Point(159, 48);
			this.execArgText.Name = "execArgText";
			this.execArgText.Size = new System.Drawing.Size(275, 23);
			this.execArgText.TabIndex = 11;
			// 
			// execProgText
			// 
			this.execProgText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.execProgText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.execProgText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.execProgText.Location = new System.Drawing.Point(0, 18);
			this.execProgText.Name = "execProgText";
			this.execProgText.Size = new System.Drawing.Size(313, 23);
			this.execProgText.TabIndex = 8;
			this.execProgText.TextChanged += new System.EventHandler(this.execProgText_TextChanged);
			// 
			// execDirLabel
			// 
			this.execDirLabel.AutoSize = true;
			this.execDirLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.execDirLabel.Location = new System.Drawing.Point(-3, 82);
			this.execDirLabel.Name = "execDirLabel";
			this.execDirLabel.Size = new System.Drawing.Size(102, 15);
			this.execDirLabel.TabIndex = 12;
			this.execDirLabel.Text = "Start in (optional):";
			// 
			// execArgLabel
			// 
			this.execArgLabel.AutoSize = true;
			this.execArgLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.execArgLabel.Location = new System.Drawing.Point(-3, 52);
			this.execArgLabel.Name = "execArgLabel";
			this.execArgLabel.Size = new System.Drawing.Size(147, 15);
			this.execArgLabel.TabIndex = 10;
			this.execArgLabel.Text = "Add arguments (optional):";
			// 
			// execProgLabel
			// 
			this.execProgLabel.AutoSize = true;
			this.execProgLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.execProgLabel.Location = new System.Drawing.Point(-3, 0);
			this.execProgLabel.Name = "execProgLabel";
			this.execProgLabel.Size = new System.Drawing.Size(90, 15);
			this.execProgLabel.TabIndex = 7;
			this.execProgLabel.Text = "Program/script:";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// ExecActionUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.execProgBrowseBtn);
			this.Controls.Add(this.execDirText);
			this.Controls.Add(this.execArgText);
			this.Controls.Add(this.execProgText);
			this.Controls.Add(this.execDirLabel);
			this.Controls.Add(this.execArgLabel);
			this.Controls.Add(this.execProgLabel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Name = "ExecActionUI";
			this.Size = new System.Drawing.Size(434, 102);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button execProgBrowseBtn;
		private System.Windows.Forms.TextBox execDirText;
		private System.Windows.Forms.TextBox execArgText;
		private System.Windows.Forms.TextBox execProgText;
		private System.Windows.Forms.Label execDirLabel;
		private System.Windows.Forms.Label execArgLabel;
		private System.Windows.Forms.Label execProgLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;

	}
}
