namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class EmailActionUI
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
			this.emailAttachementBrowseBtn = new System.Windows.Forms.Button();
			this.emailSMTPText = new System.Windows.Forms.TextBox();
			this.emailSMTPLabel = new System.Windows.Forms.Label();
			this.emailAttachmentText = new System.Windows.Forms.TextBox();
			this.emailAttachmentLabel = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.emailTextText = new System.Windows.Forms.TextBox();
			this.emailTextLabel = new System.Windows.Forms.Label();
			this.emailSubjectText = new System.Windows.Forms.TextBox();
			this.emailSubjectLabel = new System.Windows.Forms.Label();
			this.emailToText = new System.Windows.Forms.TextBox();
			this.emailToLabel = new System.Windows.Forms.Label();
			this.emailFromText = new System.Windows.Forms.TextBox();
			this.emailFromLabel = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// emailAttachementBrowseBtn
			// 
			this.emailAttachementBrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachementBrowseBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailAttachementBrowseBtn.Location = new System.Drawing.Point(365, 214);
			this.emailAttachementBrowseBtn.Name = "emailAttachementBrowseBtn";
			this.emailAttachementBrowseBtn.Size = new System.Drawing.Size(101, 25);
			this.emailAttachementBrowseBtn.TabIndex = 24;
			this.emailAttachementBrowseBtn.Text = "Browse...";
			this.emailAttachementBrowseBtn.UseVisualStyleBackColor = true;
			this.emailAttachementBrowseBtn.Click += new System.EventHandler(this.emailAttachementBrowseBtn_Click);
			// 
			// emailSMTPText
			// 
			this.emailSMTPText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailSMTPText.Location = new System.Drawing.Point(89, 257);
			this.emailSMTPText.Name = "emailSMTPText";
			this.emailSMTPText.Size = new System.Drawing.Size(377, 23);
			this.emailSMTPText.TabIndex = 27;
			this.emailSMTPText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailSMTPLabel
			// 
			this.emailSMTPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailSMTPLabel.AutoSize = true;
			this.emailSMTPLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailSMTPLabel.Location = new System.Drawing.Point(-1, 261);
			this.emailSMTPLabel.Name = "emailSMTPLabel";
			this.emailSMTPLabel.Size = new System.Drawing.Size(75, 15);
			this.emailSMTPLabel.TabIndex = 26;
			this.emailSMTPLabel.Text = "SMTP server:";
			// 
			// emailAttachmentText
			// 
			this.emailAttachmentText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachmentText.Location = new System.Drawing.Point(89, 215);
			this.emailAttachmentText.Name = "emailAttachmentText";
			this.emailAttachmentText.Size = new System.Drawing.Size(269, 23);
			this.emailAttachmentText.TabIndex = 23;
			// 
			// emailAttachmentLabel
			// 
			this.emailAttachmentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailAttachmentLabel.AutoSize = true;
			this.emailAttachmentLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailAttachmentLabel.Location = new System.Drawing.Point(-2, 218);
			this.emailAttachmentLabel.Name = "emailAttachmentLabel";
			this.emailAttachmentLabel.Size = new System.Drawing.Size(73, 15);
			this.emailAttachmentLabel.TabIndex = 22;
			this.emailAttachmentLabel.Text = "Attachment:";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Location = new System.Drawing.Point(2, 248);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(461, 2);
			this.groupBox3.TabIndex = 25;
			this.groupBox3.TabStop = false;
			// 
			// emailTextText
			// 
			this.emailTextText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailTextText.Location = new System.Drawing.Point(89, 90);
			this.emailTextText.Multiline = true;
			this.emailTextText.Name = "emailTextText";
			this.emailTextText.Size = new System.Drawing.Size(377, 112);
			this.emailTextText.TabIndex = 21;
			this.emailTextText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailTextLabel
			// 
			this.emailTextLabel.AutoSize = true;
			this.emailTextLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailTextLabel.Location = new System.Drawing.Point(-1, 93);
			this.emailTextLabel.Name = "emailTextLabel";
			this.emailTextLabel.Size = new System.Drawing.Size(32, 15);
			this.emailTextLabel.TabIndex = 20;
			this.emailTextLabel.Text = "Text:";
			// 
			// emailSubjectText
			// 
			this.emailSubjectText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailSubjectText.Location = new System.Drawing.Point(89, 60);
			this.emailSubjectText.Name = "emailSubjectText";
			this.emailSubjectText.Size = new System.Drawing.Size(377, 23);
			this.emailSubjectText.TabIndex = 19;
			this.emailSubjectText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailSubjectLabel
			// 
			this.emailSubjectLabel.AutoSize = true;
			this.emailSubjectLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailSubjectLabel.Location = new System.Drawing.Point(-1, 63);
			this.emailSubjectLabel.Name = "emailSubjectLabel";
			this.emailSubjectLabel.Size = new System.Drawing.Size(49, 15);
			this.emailSubjectLabel.TabIndex = 18;
			this.emailSubjectLabel.Text = "Subject:";
			// 
			// emailToText
			// 
			this.emailToText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailToText.Location = new System.Drawing.Point(89, 30);
			this.emailToText.Name = "emailToText";
			this.emailToText.Size = new System.Drawing.Size(377, 23);
			this.emailToText.TabIndex = 17;
			this.emailToText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailToLabel
			// 
			this.emailToLabel.AutoSize = true;
			this.emailToLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailToLabel.Location = new System.Drawing.Point(-2, 33);
			this.emailToLabel.Name = "emailToLabel";
			this.emailToLabel.Size = new System.Drawing.Size(24, 15);
			this.emailToLabel.TabIndex = 16;
			this.emailToLabel.Text = "To:";
			// 
			// emailFromText
			// 
			this.emailFromText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.emailFromText.Location = new System.Drawing.Point(89, 0);
			this.emailFromText.Name = "emailFromText";
			this.emailFromText.Size = new System.Drawing.Size(377, 23);
			this.emailFromText.TabIndex = 15;
			this.emailFromText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailFromLabel
			// 
			this.emailFromLabel.AutoSize = true;
			this.emailFromLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.emailFromLabel.Location = new System.Drawing.Point(-2, 3);
			this.emailFromLabel.Name = "emailFromLabel";
			this.emailFromLabel.Size = new System.Drawing.Size(38, 15);
			this.emailFromLabel.TabIndex = 14;
			this.emailFromLabel.Text = "From:";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// EmailActionUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.emailAttachementBrowseBtn);
			this.Controls.Add(this.emailSMTPText);
			this.Controls.Add(this.emailSMTPLabel);
			this.Controls.Add(this.emailAttachmentText);
			this.Controls.Add(this.emailAttachmentLabel);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.emailTextText);
			this.Controls.Add(this.emailTextLabel);
			this.Controls.Add(this.emailSubjectText);
			this.Controls.Add(this.emailSubjectLabel);
			this.Controls.Add(this.emailToText);
			this.Controls.Add(this.emailToLabel);
			this.Controls.Add(this.emailFromText);
			this.Controls.Add(this.emailFromLabel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Name = "EmailActionUI";
			this.Size = new System.Drawing.Size(467, 282);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button emailAttachementBrowseBtn;
		private System.Windows.Forms.TextBox emailSMTPText;
		private System.Windows.Forms.Label emailSMTPLabel;
		private System.Windows.Forms.TextBox emailAttachmentText;
		private System.Windows.Forms.Label emailAttachmentLabel;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox emailTextText;
		private System.Windows.Forms.Label emailTextLabel;
		private System.Windows.Forms.TextBox emailSubjectText;
		private System.Windows.Forms.Label emailSubjectLabel;
		private System.Windows.Forms.TextBox emailToText;
		private System.Windows.Forms.Label emailToLabel;
		private System.Windows.Forms.TextBox emailFromText;
		private System.Windows.Forms.Label emailFromLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}
