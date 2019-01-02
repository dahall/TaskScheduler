using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler.Design;
using Microsoft.Win32.TaskScheduler.EditorProperties;
using Vanara.Extensions;
using Vanara.Windows.Forms;
using FolderBrowserDialog = Vanara.Windows.Forms.FolderBrowserDialog;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog box to set the properties of a TaskService.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog box to set the properties of a TaskService."),
	Designer(typeof(TaskServiceComponentDesigner)), DesignTimeVisible(true), DefaultProperty("TaskService")]
	[ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
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
		[Browsable(false), Description("The user's account domain."), DefaultValue(null)]
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
		[Browsable(false), Description("The user's password."), DefaultValue(null)]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the target server.
		/// </summary>
		/// <value>The target server.</value>
		[Browsable(false), Description("The name of the server to get the Task Scheduler."), DefaultValue(null)]
		public string TargetServer
		{
			get
			{
				if (server == null || string.Compare(server.Trim('\\', ' '), Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
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
					v1Check.Checked = ForceV1 = value.HighestSupportedVersion <= TaskServiceVersion.V1_1;

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
			get { return Text; }
			set { Text = value; }
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>The user.</value>
		[Browsable(false), Description("The user's username."), DefaultValue(null)]
		public string User { get; set; }

		private void computerBrowseBtn_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog
			{
				BrowseOption = FolderBrowserDialogOptions.Computers,
				RootFolder = KnownFolder.ComputerFolder,
				Description = Resources.BrowseForTargetServerPrompt,
				SelectedItem = TargetServer ?? Environment.MachineName
			};
			if (dlg.ShowDialog(this) == DialogResult.OK)
				TargetServer = remoteComputerText.Text = dlg.SelectedItem;
		}

		private void computerRadio_CheckedChanged(object sender, EventArgs e)
		{
			runButton.Enabled = localComputerRadio.Checked || remoteComputerText.TextLength > 0;
			remoteComputerText.Enabled = computerBrowseBtn.Enabled = otherUserCheckbox.Enabled = !localComputerRadio.Checked;
			setUserBtn.Enabled = !localComputerRadio.Checked && otherUserCheckbox.Checked;
			if (localComputerRadio.Checked)
			{
				TargetServer = User = Domain = Password = null;
				SetUserText(null);
				remoteComputerText.Clear();
			}
			else
				remoteComputerText.Focus();
		}

		private string GetLocalizedResourceString(string resourceName)
		{
			var resources = new ComponentResourceManager(GetType());
			return resources.GetString(resourceName);
		}

		private void otherUserCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			setUserBtn.Enabled = otherUserCheckbox.Checked;
			if (otherUserCheckbox.Checked && string.IsNullOrEmpty(User))
				setUserBtn_Click(sender, e);
		}

		private void remoteComputerText_TextChanged(object sender, EventArgs e)
		{
			runButton.Enabled = remoteComputerText.TextLength > 0;
			TargetServer = remoteComputerText.TextLength > 0 ? remoteComputerText.Text : null;
		}

		private void ResetTitle()
		{
			Text = GetLocalizedResourceString("$this.Text");
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			var success = true;
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
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void setUserBtn_Click(object sender, EventArgs e)
		{
			var dlg = new CredentialsDialog(Resources.TaskSchedulerName);
			if (TargetServer != null)
				dlg.Target = TargetServer;
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				SetUserText(dlg.UserName);
				var userParts = dlg.UserName.Split('\\');
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

		private void SetUserText(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				value = Resources.NoUserSpecifiedText;
				otherUserCheckbox.Checked = false;
			}
			otherUserCheckbox.Text = string.Format(GetLocalizedResourceString("otherUserCheckbox.Text"), value);
		}

		private bool ShouldSerializeTitle() => Text != GetLocalizedResourceString("$this.Text");

		private void v1Check_CheckedChanged(object sender, EventArgs e)
		{
			ForceV1 = v1Check.Checked;
		}
	}
}