using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal partial class ACLEditor : UserControl
	{
		private NativeObjectSecurity sec;
		private bool editable;

		public ACLEditor()
		{
			InitializeComponent();
			Display = SecurityRuleType.Access;
			Editable = false;
		}

		public SecurityRuleType Display { get; set; }

		[DefaultValue(false)]
		public bool Editable
		{
			get { return editable; }
			set
			{
				editable = value;
				addBtn.Visible = removeBtn.Visible = value;
				chgPermBtn.Visible = !value;
			}
		}

		public string ObjectName { get; set; }

		public NativeObjectSecurity ObjectSecurity
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

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
				chgPermBtn.SetElevationRequiredState(true);
		}

		private void AddListItem(AuthorizationRule rule)
		{
			var ai = new AccountInfo((SecurityIdentifier)rule.IdentityReference.Translate(typeof(SecurityIdentifier)), this.TargetComputer);
			ListViewItem item;
			if (rule is AccessRule)
			{
				string text = AdvancedSecuritySettingsDialog.GetLocalizedString(((FileSystemAccessRule)rule).AccessControlType);
				item = permissionsListView.Items.Add("", ai.use == Microsoft.Win32.NativeMethods.SidNameUse.User ? 0 : 1);
				if (item.Index == 0) item.Selected = true;
				item.SubItems.AddRange(new string[] {
					text,
					ai.ToString(),
					AdvancedSecuritySettingsDialog.GetLocalizedString(((FileSystemAccessRule)rule).FileSystemRights),
					string.Empty });
				item.Tag = rule;
			}
			else if (rule is AuditRule)
			{
				string text = AdvancedSecuritySettingsDialog.GetLocalizedString(((FileSystemAuditRule)rule).AuditFlags);
				item = permissionsListView.Items.Add("", ai.use == Microsoft.Win32.NativeMethods.SidNameUse.User ? 0 : 1);
				item.SubItems.AddRange(new string[] { 
					text,
					ai.ToString(),
					AdvancedSecuritySettingsDialog.GetLocalizedString(((FileSystemAuditRule)rule).FileSystemRights),
					string.Empty });
				item.Tag = rule;
			}
		}

		private void permissionsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			viewBtn.Enabled = removeBtn.Enabled = (permissionsListView.SelectedIndices.Count > 0);
		}

		private void permissionsListView_SizeChanged(object sender, EventArgs e)
		{
			permissionsListView.Columns[3].Width = permissionsListView.Width - permissionsListView.Columns[0].Width - permissionsListView.Columns[1].Width - permissionsListView.Columns[2].Width;
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			bool modified;
			if (Microsoft.Win32.TaskScheduler.HelperMethods.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{
				AceEditor aed = new AceEditor() { ObjectName = this.ObjectName, ObjectSecurity = new FileSecurity() };
				if (Display == SecurityRuleType.Access)
				{
					FileSystemAccessRule aRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Allow);
					FileSystemAccessRule dRule = new FileSystemAccessRule(new SecurityIdentifier(sid), 0, AccessControlType.Deny);
					aed.ObjectSecurity.ModifyAccessRule(AccessControlModification.Add, aRule, out modified);
					aed.ObjectSecurity.ModifyAccessRule(AccessControlModification.Add, dRule, out modified);
					if (aed.ShowDialog(this) == DialogResult.OK)
					{
						if (aRule.FileSystemRights != 0)
						{
							sec.ModifyAccessRule(AccessControlModification.Add, aRule, out modified);
							AddListItem(aRule);
						}
						if (dRule.FileSystemRights != 0)
						{
							sec.ModifyAccessRule(AccessControlModification.Add, dRule, out modified);
							AddListItem(dRule);
						}
					}
				}
			}
		}

		private void editButton_Click(object sender, EventArgs e)
		{
			using (AceEditor aed = new AceEditor() { ObjectName = this.ObjectName, ObjectSecurity = sec })
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