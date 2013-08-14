using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal partial class ACLEditor : UserControl
	{
		private FileSecurity sec;
		private string objName;

		public ACLEditor()
		{
			InitializeComponent();
			Display = SecurityRuleType.Access;
			objNameText.BackColor = this.BackColor;
		}

		public SecurityRuleType Display { get; set; }

		public string ObjectName
		{
			get { return objName; }
			set { objName = value; objNameText.Text = objName; }
		}

		public FileSecurity ObjectSecurity
		{
			get { return sec; }
			set
			{
				if (value != null)
				{
					sec = value;
					permissionsListView.BeginUpdate();
					permissionsListView.Items.Clear();
					if (Display == SecurityRuleType.Access)
					{
						AuthorizationRuleCollection coll = sec.GetAccessRules(true, true, typeof(NTAccount));
						foreach (FileSystemAccessRule rule in coll)
							AddListItem(rule);
					}
					else
					{
						AuthorizationRuleCollection coll = sec.GetAuditRules(true, true, typeof(NTAccount));
						foreach (FileSystemAuditRule rule in coll)
							AddListItem(rule);
					}
					permissionsListView.EndUpdate();
				}
				else
				{
					sec = null;
					permissionsListView.Items.Clear();
				}
			}
		}

		public string TargetComputer { get; set; }

		private void AddListItem(AuthorizationRule rule)
		{
			ListViewItem item;
			if (rule is FileSystemAccessRule)
			{
				string text = SecurityEditorDialog.GetLocalizedString(((FileSystemAccessRule)rule).AccessControlType);
				item = permissionsListView.Items.Add(text);
				if (item.Index == 0) item.Selected = true;
				item.SubItems.AddRange(new string[] { 
					rule.IdentityReference.Value,
					SecurityEditorDialog.GetLocalizedString(((FileSystemAccessRule)rule).FileSystemRights),
					string.Empty });
				item.Tag = rule;
			}
			else if (rule is FileSystemAuditRule)
			{
				string text = SecurityEditorDialog.GetLocalizedString(((FileSystemAuditRule)rule).AuditFlags);
				item = permissionsListView.Items.Add(text);
				item.SubItems.AddRange(new string[] { 
					rule.IdentityReference.Value,
					SecurityEditorDialog.GetLocalizedString(((FileSystemAuditRule)rule).FileSystemRights),
					string.Empty });
				item.Tag = rule;
			}
		}

		private void permissionsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			editButton.Enabled = removeButton.Enabled = (permissionsListView.SelectedIndices.Count > 0);
		}

		private void permissionsListView_SizeChanged(object sender, EventArgs e)
		{
			permissionsListView.Columns[3].Width = permissionsListView.Width - permissionsListView.Columns[0].Width - permissionsListView.Columns[1].Width - permissionsListView.Columns[2].Width;
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.HelperMethods.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{
				AceEditor aed = new AceEditor() { ObjectName = this.objName, ObjectSecurity = new FileSecurity() };
				if (Display == SecurityRuleType.Access)
				{
					FileSystemAccessRule aRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Allow);
					FileSystemAccessRule dRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Deny);
					aed.ObjectSecurity.AddAccessRule(aRule);
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
					}
				}
			}
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			using (AceEditor aed = new AceEditor() { ObjectName = this.objName, ObjectSecurity = sec })
			{
				if (aed.ShowDialog(this) == DialogResult.OK)
				{
					/*bool modified;
					if (Display == RuleType.Access)
						sec.ModifyAccessRule(AccessControlModification.Reset, aed.Rule as AccessRule, out modified);
					else
						sec.ModifyAuditRule(AccessControlModification.Reset, aed.Rule as AuditRule, out modified);
					permissionsListView.Items.RemoveAt(permissionsListView.SelectedIndices[0]);
					AddListItem(aed.Rule);*/
				}
			}
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			bool modified;
			if (Display == SecurityRuleType.Access)
				sec.ModifyAccessRule(AccessControlModification.RemoveAll, permissionsListView.SelectedItems[0].Tag as AccessRule, out modified);
			else
				sec.ModifyAuditRule(AccessControlModification.RemoveAll, permissionsListView.SelectedItems[0].Tag as AuditRule, out modified);
			permissionsListView.Items.RemoveAt(permissionsListView.SelectedIndices[0]);
		}

		private void inheritedCheck_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void permissionsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			editButton_Click(sender, EventArgs.Empty);
		}
	}
}
