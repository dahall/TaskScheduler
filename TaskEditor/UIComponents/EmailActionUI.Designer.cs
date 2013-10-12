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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailActionUI));
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
			resources.ApplyResources(this.emailAttachementBrowseBtn, "emailAttachementBrowseBtn");
			this.emailAttachementBrowseBtn.Name = "emailAttachementBrowseBtn";
			this.emailAttachementBrowseBtn.UseVisualStyleBackColor = true;
			this.emailAttachementBrowseBtn.Click += new System.EventHandler(this.emailAttachementBrowseBtn_Click);
			// 
			// emailSMTPText
			// 
			resources.ApplyResources(this.emailSMTPText, "emailSMTPText");
			this.emailSMTPText.Name = "emailSMTPText";
			this.emailSMTPText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailSMTPLabel
			// 
			resources.ApplyResources(this.emailSMTPLabel, "emailSMTPLabel");
			this.emailSMTPLabel.Name = "emailSMTPLabel";
			// 
			// emailAttachmentText
			// 
			resources.ApplyResources(this.emailAttachmentText, "emailAttachmentText");
			this.emailAttachmentText.Name = "emailAttachmentText";
			// 
			// emailAttachmentLabel
			// 
			resources.ApplyResources(this.emailAttachmentLabel, "emailAttachmentLabel");
			this.emailAttachmentLabel.Name = "emailAttachmentLabel";
			// 
			// groupBox3
			// 
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// emailTextText
			// 
			resources.ApplyResources(this.emailTextText, "emailTextText");
			this.emailTextText.Name = "emailTextText";
			this.emailTextText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailTextLabel
			// 
			resources.ApplyResources(this.emailTextLabel, "emailTextLabel");
			this.emailTextLabel.Name = "emailTextLabel";
			// 
			// emailSubjectText
			// 
			resources.ApplyResources(this.emailSubjectText, "emailSubjectText");
			this.emailSubjectText.Name = "emailSubjectText";
			this.emailSubjectText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailSubjectLabel
			// 
			resources.ApplyResources(this.emailSubjectLabel, "emailSubjectLabel");
			this.emailSubjectLabel.Name = "emailSubjectLabel";
			// 
			// emailToText
			// 
			resources.ApplyResources(this.emailToText, "emailToText");
			this.emailToText.Name = "emailToText";
			this.emailToText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailToLabel
			// 
			resources.ApplyResources(this.emailToLabel, "emailToLabel");
			this.emailToLabel.Name = "emailToLabel";
			// 
			// emailFromText
			// 
			resources.ApplyResources(this.emailFromText, "emailFromText");
			this.emailFromText.Name = "emailFromText";
			this.emailFromText.TextChanged += new System.EventHandler(this.keyField_TextChanged);
			// 
			// emailFromLabel
			// 
			resources.ApplyResources(this.emailFromLabel, "emailFromLabel");
			this.emailFromLabel.Name = "emailFromLabel";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
			// 
			// EmailActionUI
			// 
			resources.ApplyResources(this, "$this");
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
			this.Name = "EmailActionUI";
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
