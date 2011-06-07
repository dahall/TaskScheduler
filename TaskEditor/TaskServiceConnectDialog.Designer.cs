namespace Microsoft.Win32.TaskScheduler
{
	partial class TaskServiceConnectDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskServiceConnectDialog));
			this.v1Check = new System.Windows.Forms.CheckBox();
			this.runButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.localComputerRadio = new System.Windows.Forms.RadioButton();
			this.remoteComputerRadio = new System.Windows.Forms.RadioButton();
			this.otherUserCheckbox = new System.Windows.Forms.CheckBox();
			this.remoteComputerText = new System.Windows.Forms.TextBox();
			this.computerBrowseBtn = new System.Windows.Forms.Button();
			this.setUserBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// v1Check
			// 
			resources.ApplyResources(this.v1Check, "v1Check");
			this.v1Check.Name = "v1Check";
			this.v1Check.UseVisualStyleBackColor = true;
			// 
			// runButton
			// 
			resources.ApplyResources(this.runButton, "runButton");
			this.runButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.runButton.Name = "runButton";
			this.runButton.UseVisualStyleBackColor = true;
			this.runButton.Click += new System.EventHandler(this.runButton_Click);
			// 
			// closeButton
			// 
			resources.ApplyResources(this.closeButton, "closeButton");
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Name = "closeButton";
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// localComputerRadio
			// 
			resources.ApplyResources(this.localComputerRadio, "localComputerRadio");
			this.localComputerRadio.Checked = true;
			this.localComputerRadio.Name = "localComputerRadio";
			this.localComputerRadio.TabStop = true;
			this.localComputerRadio.UseVisualStyleBackColor = true;
			this.localComputerRadio.CheckedChanged += new System.EventHandler(this.computerRadio_CheckedChanged);
			// 
			// remoteComputerRadio
			// 
			resources.ApplyResources(this.remoteComputerRadio, "remoteComputerRadio");
			this.remoteComputerRadio.Name = "remoteComputerRadio";
			this.remoteComputerRadio.UseVisualStyleBackColor = true;
			this.remoteComputerRadio.CheckedChanged += new System.EventHandler(this.computerRadio_CheckedChanged);
			// 
			// otherUserCheckbox
			// 
			resources.ApplyResources(this.otherUserCheckbox, "otherUserCheckbox");
			this.otherUserCheckbox.Name = "otherUserCheckbox";
			this.otherUserCheckbox.UseVisualStyleBackColor = true;
			this.otherUserCheckbox.CheckedChanged += new System.EventHandler(this.otherUserCheckbox_CheckedChanged);
			// 
			// remoteComputerText
			// 
			resources.ApplyResources(this.remoteComputerText, "remoteComputerText");
			this.remoteComputerText.Name = "remoteComputerText";
			this.remoteComputerText.TextChanged += new System.EventHandler(this.remoteComputerText_TextChanged);
			// 
			// computerBrowseBtn
			// 
			resources.ApplyResources(this.computerBrowseBtn, "computerBrowseBtn");
			this.computerBrowseBtn.Name = "computerBrowseBtn";
			this.computerBrowseBtn.UseVisualStyleBackColor = true;
			this.computerBrowseBtn.Click += new System.EventHandler(this.computerBrowseBtn_Click);
			// 
			// setUserBtn
			// 
			resources.ApplyResources(this.setUserBtn, "setUserBtn");
			this.setUserBtn.Name = "setUserBtn";
			this.setUserBtn.UseVisualStyleBackColor = true;
			this.setUserBtn.Click += new System.EventHandler(this.setUserBtn_Click);
			// 
			// TaskServiceConnectDialog
			// 
			this.AcceptButton = this.runButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.Controls.Add(this.setUserBtn);
			this.Controls.Add(this.computerBrowseBtn);
			this.Controls.Add(this.remoteComputerText);
			this.Controls.Add(this.otherUserCheckbox);
			this.Controls.Add(this.remoteComputerRadio);
			this.Controls.Add(this.localComputerRadio);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.runButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.v1Check);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "TaskServiceConnectDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button runButton;
		private TaskService ts;
		private System.Windows.Forms.CheckBox v1Check;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.RadioButton localComputerRadio;
		private System.Windows.Forms.RadioButton remoteComputerRadio;
		private System.Windows.Forms.CheckBox otherUserCheckbox;
		private System.Windows.Forms.TextBox remoteComputerText;
		private System.Windows.Forms.Button computerBrowseBtn;
		private System.Windows.Forms.Button setUserBtn;
	}
}