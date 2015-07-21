using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog box to set the properties of a TaskService.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog box to set the properties of a TaskService."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true), DefaultProperty("TaskService")]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
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
			SetUserText(null);
		}

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		/// <value>The domain.</value>
		[Browsable(false), Description("The user's account domain."), DefaultValue((string)null)]
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
		[Browsable(false), Description("The user's password."), DefaultValue((string)null)]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the target server.
		/// </summary>
		/// <value>The target server.</value>
		[Browsable(false), Description("The name of the server to get the Task Scheduler."), DefaultValue((string)null)]
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
				ts = value;
				if (value != null && !this.IsDesignMode())
				{
					TargetServer = value.TargetServer;
					User = value.UserName;
					Domain = value.UserAccountDomain;
					Password = value.UserPassword;
					v1Check.Checked = ForceV1 = value.HighestSupportedVersion <= new Version(1, 1);

					if (TargetServer == null && User == null)
					{
						localComputerRadio.Checked = true;
					}
					else
					{
						remoteComputerText.Text = TargetServer;
						SetUserText(string.Concat(Domain, @"\", User));
						remoteComputerRadio.Checked = true;
					}
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
		[Browsable(false), Description("The user's username."), DefaultValue((string)null)]
		public string User { get; set; }

		private string GetLocalizedResourceString(string resourceName)
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(GetType());
			return resources.GetString(resourceName);
		}

		private void ResetTitle()
		{
			base.Text = GetLocalizedResourceString("$this.Text");
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			bool success = true;
			ts.BeginInit();
			ts.TargetServer = TargetServer;
			ts.UserName = User;
			ts.UserAccountDomain = Domain;
			ts.UserPassword = Password;
			ts.HighestSupportedVersion = new Version(1, ForceV1 ? 1 : 2);
			try { ts.EndInit(); }
			catch (Exception ex) { success = false; MessageBox.Show(this, ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error); }
			if (success)
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
				Close();
			}
		}

		private void SetUserText(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				value = EditorProperties.Resources.NoUserSpecifiedText;
				otherUserCheckbox.Checked = false;
			}
			otherUserCheckbox.Text = string.Format(GetLocalizedResourceString("otherUserCheckbox.Text"), value);
		}

		private bool ShouldSerializeTitle() => base.Text != GetLocalizedResourceString("$this.Text");

		private void setUserBtn_Click(object sender, EventArgs e)
		{
			CredentialsDialog dlg = new CredentialsDialog(EditorProperties.Resources.TaskSchedulerName);
			if (TargetServer != null)
				dlg.Target = TargetServer;
			if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				SetUserText(dlg.UserName);
				string[] userParts = dlg.UserName.Split('\\');
				if (userParts.Length == 1)
				{
					Domain = TargetServer;
					User = userParts[0];
				}
				else if (userParts.Length == 2)
				{
					Domain = userParts[0];
					User = userParts[1];
				}
				Password = dlg.Password;
			}
		}

		private void computerRadio_CheckedChanged(object sender, EventArgs e)
		{
			runButton.Enabled = (localComputerRadio.Checked || remoteComputerText.TextLength > 0);
			remoteComputerText.Enabled = computerBrowseBtn.Enabled = otherUserCheckbox.Enabled = !localComputerRadio.Checked;
			setUserBtn.Enabled = !localComputerRadio.Checked && otherUserCheckbox.Checked;
			if (localComputerRadio.Checked)
			{
				TargetServer = User = Domain = Password = null;
				SetUserText(null);
				remoteComputerText.Clear();
			}
		}

		private void computerBrowseBtn_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog2 dlg = FolderBrowserDialog2.ComputerBrowser;
			dlg.Description = EditorProperties.Resources.BrowseForTargetServerPrompt;
			dlg.SelectedPath = TargetServer == null ? Environment.MachineName : TargetServer;
			if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				TargetServer = remoteComputerText.Text = dlg.SelectedPath;
		}

		private void otherUserCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			setUserBtn.Enabled = otherUserCheckbox.Checked;
		}

		private void remoteComputerText_TextChanged(object sender, EventArgs e)
		{
			runButton.Enabled = remoteComputerText.TextLength > 0;
			TargetServer = remoteComputerText.TextLength > 0 ? remoteComputerText.Text : null;
		}

		private void v1Check_CheckedChanged(object sender, EventArgs e)
		{
			ForceV1 = v1Check.Checked;
		}
	}
}