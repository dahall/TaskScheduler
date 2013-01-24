using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Win32.TaskScheduler
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog box to set the security properties of a task.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true), DefaultProperty("SecurityDescriptorSddlForm")]
	public partial class TaskSDDLEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private string curUser = null;
		private string name = "";
		private string sddl;
		private FileSecurity sec;

		public TaskSDDLEditDialog()
		{
			InitializeComponent();
		}

		public string SecurityDescriptorSddlForm
		{
			get { return sddl; }
			set
			{
				if (sddl != value)
				{
					sddl = value;
					sec = new FileSecurity();
					sec.SetSecurityDescriptorSddlForm(value, AccessControlSections.All);
					UpdateUI();
				}
			}
		}

		public string TargetComputer { get; set; }

		public string TaskName
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					this.Text = "Permissions for " + name;
					this.objNameText.Text = name;
				}
			}
		}

		private void UpdateUI()
		{
			if (nameLookUpper.IsBusy)
				nameLookUpper.CancelAsync();
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			foreach (FileSystemAccessRule rule in sec.GetAccessRules(true, true, typeof(SecurityIdentifier)))
			{
				if (!list.Contains(rule.IdentityReference.Value))
					list.Add(rule.IdentityReference.Value);
			}
			this.userList.Items.Clear();
			this.userList.Items.AddRange(list.ConvertAll<ListViewItem>(this.BuildUserItem).ToArray());
			nameLookUpper.RunWorkerAsync(list.ToArray());
		}

		private ListViewItem BuildUserItem(string s)
		{
			// TODO: Determine which icon to use
			int iconIndex = 0;
			return new ListViewItem(s, iconIndex);
		}

		private void addBtn_Click(object sender, EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.NativeMethods.AccountUtils.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{
				FileSystemAccessRule aRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Allow);
				FileSystemAccessRule dRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Deny);
				/*aed.ObjectSecurity.AddAccessRule(aRule);
				aed.ObjectSecurity.AddAccessRule(dRule);
				if (aed.ShowDialog(this) == DialogResult.OK)
				{
					if (aRule.FileSystemRights != 0)
					{
						sec.AddAccessRule(aRule);
						AddListItem(aRule);
					}
					if (dRule.FileSystemRights != 0)
					{
						sec.AddAccessRule(dRule);
						AddListItem(dRule);
					}
				}*/
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (nameLookUpper.IsBusy)
				nameLookUpper.CancelAsync();
			e.Cancel = false;
			base.OnClosing(e);
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void nameLookUpper_DoWork(object sender, DoWorkEventArgs e)
		{
			string[] sids = (string[])e.Argument;
			for (int i = 0; i < sids.Length; i++)
			{
				if (nameLookUpper.CancellationPending)
					break;
				SecurityIdentifier sid = new SecurityIdentifier(sids[i]);
				string name = sid.Translate(typeof(NTAccount)).Value;
				if (!nameLookUpper.CancellationPending)
					nameLookUpper.ReportProgress(i, name);
			}
			e.Cancel = true;
		}

		private void nameLookUpper_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			userList.Items[e.ProgressPercentage].Tag = userList.Items[e.ProgressPercentage].Text;
			userList.Items[e.ProgressPercentage].Text = e.UserState.ToString();
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			sddl = sec.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void removeBtn_Click(object sender, EventArgs e)
		{
			sec.RemoveAccessRuleAll(new FileSystemAccessRule(curUser, FileSystemRights.FullControl, AccessControlType.Allow));
			int curSel = userList.SelectedIndices[0];
			if (userList.Items.Count > 1)
			{
				if (curSel < (userList.Items.Count - 1))
					userList.Items[curSel + 1].Selected = true;
				else
					userList.Items[curSel - 1].Selected = true;
			}
			userList.Items.RemoveAt(curSel);
		}

		private void userList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (userList.SelectedIndices.Count > 0)
			{
				curUser = userList.SelectedItems[0].Text;
				permissionList.Initialize(sec, new NTAccount(curUser), SecurityEditor.SecurityRuleType.Access);
			}
			else
			{
				curUser = null;
				permissionList.ResetList(SecurityEditor.SecurityRuleType.Access);
			}
		}
	}
}
