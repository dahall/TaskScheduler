namespace TestTaskService
{
	partial class TSConnectDlg
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
			this.serverText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.userText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.domainText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pwdText = new System.Windows.Forms.TextBox();
			this.v1Check = new System.Windows.Forms.CheckBox();
			this.runButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// serverText
			// 
			this.serverText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.serverText.Location = new System.Drawing.Point(75, 12);
			this.serverText.Name = "serverText";
			this.serverText.Size = new System.Drawing.Size(283, 20);
			this.serverText.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "User:";
			// 
			// userText
			// 
			this.userText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.userText.Location = new System.Drawing.Point(75, 38);
			this.userText.Name = "userText";
			this.userText.Size = new System.Drawing.Size(283, 20);
			this.userText.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Domain:";
			// 
			// domainText
			// 
			this.domainText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.domainText.Location = new System.Drawing.Point(75, 64);
			this.domainText.Name = "domainText";
			this.domainText.Size = new System.Drawing.Size(283, 20);
			this.domainText.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 93);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Password:";
			// 
			// pwdText
			// 
			this.pwdText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pwdText.Location = new System.Drawing.Point(75, 90);
			this.pwdText.Name = "pwdText";
			this.pwdText.Size = new System.Drawing.Size(283, 20);
			this.pwdText.TabIndex = 7;
			// 
			// v1Check
			// 
			this.v1Check.AutoSize = true;
			this.v1Check.Location = new System.Drawing.Point(15, 116);
			this.v1Check.Name = "v1Check";
			this.v1Check.Size = new System.Drawing.Size(100, 17);
			this.v1Check.TabIndex = 8;
			this.v1Check.Text = "Force Version 1";
			this.v1Check.UseVisualStyleBackColor = true;
			// 
			// runButton
			// 
			this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.runButton.Location = new System.Drawing.Point(202, 140);
			this.runButton.Name = "runButton";
			this.runButton.Size = new System.Drawing.Size(75, 23);
			this.runButton.TabIndex = 9;
			this.runButton.Text = "Ok";
			this.runButton.UseVisualStyleBackColor = true;
			this.runButton.Click += new System.EventHandler(this.runButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(283, 140);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 10;
			this.closeButton.Text = "Cancel";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// TSConnectDlg
			// 
			this.AcceptButton = this.runButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(370, 175);
			this.Controls.Add(this.runButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.v1Check);
			this.Controls.Add(this.pwdText);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.domainText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.userText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.serverText);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TSConnectDlg";
			this.Text = "Reconnect";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox serverText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox userText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox domainText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox pwdText;
		private System.Windows.Forms.CheckBox v1Check;
		private System.Windows.Forms.Button runButton;
		private System.Windows.Forms.Button closeButton;
	}
}