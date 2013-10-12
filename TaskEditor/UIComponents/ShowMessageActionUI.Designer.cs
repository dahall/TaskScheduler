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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowMessageActionUI));
			this.msgMsgText = new System.Windows.Forms.TextBox();
			this.msgMsgLabel = new System.Windows.Forms.Label();
			this.msgTitleText = new System.Windows.Forms.TextBox();
			this.msgTitleLabel = new System.Windows.Forms.Label();
			this.msgIntroLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// msgMsgText
			// 
			resources.ApplyResources(this.msgMsgText, "msgMsgText");
			this.msgMsgText.Name = "msgMsgText";
			this.msgMsgText.TextChanged += new System.EventHandler(this.msgMsgText_TextChanged);
			// 
			// msgMsgLabel
			// 
			resources.ApplyResources(this.msgMsgLabel, "msgMsgLabel");
			this.msgMsgLabel.Name = "msgMsgLabel";
			// 
			// msgTitleText
			// 
			resources.ApplyResources(this.msgTitleText, "msgTitleText");
			this.msgTitleText.Name = "msgTitleText";
			// 
			// msgTitleLabel
			// 
			resources.ApplyResources(this.msgTitleLabel, "msgTitleLabel");
			this.msgTitleLabel.Name = "msgTitleLabel";
			// 
			// msgIntroLabel
			// 
			resources.ApplyResources(this.msgIntroLabel, "msgIntroLabel");
			this.msgIntroLabel.Name = "msgIntroLabel";
			// 
			// ShowMessageActionUI
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.msgMsgText);
			this.Controls.Add(this.msgMsgLabel);
			this.Controls.Add(this.msgTitleText);
			this.Controls.Add(this.msgTitleLabel);
			this.Controls.Add(this.msgIntroLabel);
			this.Name = "ShowMessageActionUI";
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
