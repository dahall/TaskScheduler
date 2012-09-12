namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	partial class ShowMessageActionUI
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
			this.msgMsgText = new System.Windows.Forms.TextBox();
			this.msgMsgLabel = new System.Windows.Forms.Label();
			this.msgTitleText = new System.Windows.Forms.TextBox();
			this.msgTitleLabel = new System.Windows.Forms.Label();
			this.msgIntroLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// msgMsgText
			// 
			this.msgMsgText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.msgMsgText.Location = new System.Drawing.Point(84, 55);
			this.msgMsgText.Multiline = true;
			this.msgMsgText.Name = "msgMsgText";
			this.msgMsgText.Size = new System.Drawing.Size(334, 166);
			this.msgMsgText.TabIndex = 9;
			this.msgMsgText.TextChanged += new System.EventHandler(this.msgMsgText_TextChanged);
			// 
			// msgMsgLabel
			// 
			this.msgMsgLabel.AutoSize = true;
			this.msgMsgLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.msgMsgLabel.Location = new System.Drawing.Point(-3, 59);
			this.msgMsgLabel.Name = "msgMsgLabel";
			this.msgMsgLabel.Size = new System.Drawing.Size(56, 15);
			this.msgMsgLabel.TabIndex = 8;
			this.msgMsgLabel.Text = "Message:";
			// 
			// msgTitleText
			// 
			this.msgTitleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.msgTitleText.Location = new System.Drawing.Point(84, 25);
			this.msgTitleText.Name = "msgTitleText";
			this.msgTitleText.Size = new System.Drawing.Size(334, 23);
			this.msgTitleText.TabIndex = 7;
			// 
			// msgTitleLabel
			// 
			this.msgTitleLabel.AutoSize = true;
			this.msgTitleLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.msgTitleLabel.Location = new System.Drawing.Point(-3, 29);
			this.msgTitleLabel.Name = "msgTitleLabel";
			this.msgTitleLabel.Size = new System.Drawing.Size(33, 15);
			this.msgTitleLabel.TabIndex = 6;
			this.msgTitleLabel.Text = "Title:";
			// 
			// msgIntroLabel
			// 
			this.msgIntroLabel.AutoSize = true;
			this.msgIntroLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.msgIntroLabel.Location = new System.Drawing.Point(-3, 0);
			this.msgIntroLabel.Name = "msgIntroLabel";
			this.msgIntroLabel.Size = new System.Drawing.Size(275, 15);
			this.msgIntroLabel.TabIndex = 5;
			this.msgIntroLabel.Text = "This action displays a message box on the desktop.";
			// 
			// ShowMessageActionUI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.msgMsgText);
			this.Controls.Add(this.msgMsgLabel);
			this.Controls.Add(this.msgTitleText);
			this.Controls.Add(this.msgTitleLabel);
			this.Controls.Add(this.msgIntroLabel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Name = "ShowMessageActionUI";
			this.Size = new System.Drawing.Size(419, 222);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox msgMsgText;
		private System.Windows.Forms.Label msgMsgLabel;
		private System.Windows.Forms.TextBox msgTitleText;
		private System.Windows.Forms.Label msgTitleLabel;
		private System.Windows.Forms.Label msgIntroLabel;
	}
}
