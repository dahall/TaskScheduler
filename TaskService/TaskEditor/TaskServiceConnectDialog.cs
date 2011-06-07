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
	public partial class TaskServiceConnectDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private string server;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskServiceConnectDialog"/> class.
		/// </summary>
		public TaskServiceConnectDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		/// <value>The domain.</value>
		[Category("Data"), Description("The user's account domain."), DefaultValue("")]
		public string Domain { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to force the Task Scheduler to Version 1.0.
		/// </summary>
		/// <value><c>true</c> if forced to V1; otherwise, <c>false</c>.</value>
		[Category("Data"), Description("Force to Task Scheduler V1."), DefaultValue(false)]
		public bool ForceV1 { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[Category("Data"), Description("The user's password."), DefaultValue("")]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the target server.
		/// </summary>
		/// <value>The target server.</value>
		[Category("Data"), Description("The name of the server to get the Task Scheduler."), DefaultValue("")]
		public string TargetServer
		{
			get
			{
				if (server == null || string.Compare(server.Trim('\\', ' '), Environment.MachineName, true) == 0)
					return null;
				return string.IsNullOrEmpty(server.Trim()) ? null : server.Trim();
			}
			set { server = value; }
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
		public string User { get; set; }

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

		private void setUserBtn_Click(object sender, EventArgs e)
		{
			CredentialsDialog dlg = new CredentialsDialog(Properties.Resources.TaskSchedulerName);
			if (this.TargetServer != null)
				dlg.Target = this.TargetServer;
			if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				otherUserCheckbox.Text = string.Format("Connect as another user: {0}", dlg.UserName);
				string[] userParts = dlg.UserName.Split('\\');
				if (userParts.Length == 2)
				{
					this.Domain = userParts[0];
					this.User = userParts[1];
				}
				this.Password = dlg.Password;
			}
		}

		private void computerRadio_CheckedChanged(object sender, EventArgs e)
		{
			runButton.Enabled = (localComputerRadio.Checked || remoteComputerText.TextLength > 0);
			remoteComputerText.Enabled = computerBrowseBtn.Enabled = otherUserCheckbox.Enabled = !localComputerRadio.Checked;
			setUserBtn.Enabled = !localComputerRadio.Checked && otherUserCheckbox.Checked;
		}

		private void computerBrowseBtn_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog2 dlg = FolderBrowserDialog2.ComputerBrowser;
			dlg.Description = Properties.Resources.BrowseForTargetServerPrompt;
			dlg.SelectedPath = this.TargetServer == null ? Environment.MachineName : this.TargetServer;
			if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				this.TargetServer = this.remoteComputerText.Text = dlg.SelectedPath;
		}

		private void otherUserCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			setUserBtn.Enabled = otherUserCheckbox.Checked;
		}

		private void remoteComputerText_TextChanged(object sender, EventArgs e)
		{
			runButton.Enabled = remoteComputerText.TextLength > 0;
		}
	}
}