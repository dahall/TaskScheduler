using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog box to set the properties of a TaskService.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog box to set the properties of a TaskService.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true), DefaultProperty("TaskService")]
	public class TaskServiceConnectDialog : DialogBase
	{
		private Button closeButton;
		private TextBox domainText;
		private Label label1;
		private Label label2;
		private Label label3;
		private Label label4;
		private TextBox pwdText;
		private Button runButton;
		private TaskService ts;
		private TextBox serverText;
		private TextBox userText;
		private CheckBox v1Check;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskServiceConnectDialog"/> class.
		/// </summary>
		public TaskServiceConnectDialog()
		{
			InitDialog();
		}

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		/// <value>The domain.</value>
		[Category("Data"), Description("The user's account domain."), DefaultValue("")]
		public string Domain
		{
			get { return string.IsNullOrEmpty(domainText.Text.Trim()) ? null : domainText.Text.Trim(); }
			set { domainText.Text = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to force the Task Scheduler to Version 1.0.
		/// </summary>
		/// <value><c>true</c> if forced to V1; otherwise, <c>false</c>.</value>
		[Category("Data"), Description("Force to Task Scheduler V1."), DefaultValue(false)]
		public bool ForceV1
		{
			get { return v1Check.Checked; }
			set { v1Check.Checked = value; }
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[Category("Data"), Description("The user's password."), DefaultValue("")]
		public string Password
		{
			get { return string.IsNullOrEmpty(pwdText.Text.Trim()) ? null : pwdText.Text.Trim(); }
			set { pwdText.Text = value; }
		}

		/// <summary>
		/// Gets or sets the target server.
		/// </summary>
		/// <value>The target server.</value>
		[Category("Data"), Description("The name of the server to get the Task Scheduler."), DefaultValue("")]
		public string TargetServer
		{
			get
			{
				if (string.Compare(serverText.Text.Trim('\\', ' '), Environment.MachineName, true) == 0)
					return null;
				return string.IsNullOrEmpty(serverText.Text.Trim()) ? null : serverText.Text.Trim();
			}
			set { serverText.Text = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskService"/> instance associated with the dialog.
		/// </summary>
		/// <value>The TaskService.</value>
		[DefaultValue(null), Category("Data"), Description("The TaskService for this dialog.")]
		public TaskService TaskService
		{
			get
			{
				return ts;
			}
			set
			{
				this.ts = value;
				if (value != null && !this.IsDesignMode())
				{
					this.TargetServer = value.TargetServer;
					this.User = value.UserName;
					this.Domain = value.UserAccountDomain;
					this.Password = value.UserPassword;
					this.ForceV1 = value.HighestSupportedVersion <= new Version(1, 1);
				}
			}
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[Category("Appearance"), Description("A string to display in the title bar of the dialog box."), Localizable(true)]
		public string Title
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		[Category("Data"), Description("The user's username."), DefaultValue("")]
		public string User
		{
			get { return string.IsNullOrEmpty(userText.Text.Trim()) ? null : userText.Text.Trim(); }
			set { userText.Text = value; }
		}

		/// <summary>
		/// Initializes the dialog.
		/// </summary>
		protected void InitDialog()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskServiceConnectDialog));
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
			resources.ApplyResources(this.serverText, "serverText");
			this.serverText.Name = "serverText";
			//
			// label1
			//
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			//
			// label2
			//
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			//
			// userText
			//
			resources.ApplyResources(this.userText, "userText");
			this.userText.Name = "userText";
			//
			// label3
			//
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			//
			// domainText
			//
			resources.ApplyResources(this.domainText, "domainText");
			this.domainText.Name = "domainText";
			//
			// label4
			//
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			//
			// pwdText
			//
			resources.ApplyResources(this.pwdText, "pwdText");
			this.pwdText.Name = "pwdText";
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
			this.runButton.Click += new EventHandler(runButton_Click);
			//
			// closeButton
			//
			resources.ApplyResources(this.closeButton, "closeButton");
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Name = "closeButton";
			this.closeButton.UseVisualStyleBackColor = true;
			//
			// TSConnectDlg
			//
			this.AcceptButton = this.runButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
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
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private void ResetTitle()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskServiceConnectDialog));
			base.Text = resources.GetString("$this.Text");
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			this.UseWaitCursor = true;
			ts.BeginInit();
			ts.TargetServer = this.TargetServer;
			ts.UserName = this.User;
			ts.UserAccountDomain = this.Domain;
			ts.UserPassword = this.Password;
			ts.HighestSupportedVersion = new Version(1, this.ForceV1 ? 1 : 2);
			try { ts.EndInit(); }
			finally { this.UseWaitCursor = false; }
		}

		private bool ShouldSerializeTitle()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskServiceConnectDialog));
			return base.Text != resources.GetString("$this.Text");
		}
	}
}