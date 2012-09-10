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

		public enum RuleType
		{
			Access,
			Audit
		}

		public ACLEditor()
		{
			InitializeComponent();
			Display = RuleType.Access;
			objNameText.BackColor = this.BackColor;
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
					if (Display == RuleType.Access)
					{
						AuthorizationRuleCollection coll = sec.GetAccessRules(true, true, typeof(NTAccount));
						foreach (FileSystemAccessRule rule in coll)
						{
							string text = SecurityEditorDialog.GetLocalizedString(rule.AccessControlType);
							FileSystemRights rights = rule.FileSystemRights;
							if (rights != FileSystemRights.FullControl && (rights & FileSystemRights.Synchronize) != 0)
								rights &= ~FileSystemRights.Synchronize;
							permissionsListView.Items.Add(text).SubItems.AddRange(new string[] { 
								rule.IdentityReference.Value,
								SecurityEditorDialog.GetLocalizedString(rights),
								string.Empty });
						}
					}
					else
					{
						AuthorizationRuleCollection coll = sec.GetAuditRules(true, true, typeof(NTAccount));
						foreach (FileSystemAuditRule rule in coll)
						{
							string text = SecurityEditorDialog.GetLocalizedString(rule.AuditFlags);
							permissionsListView.Items.Add(text).SubItems.AddRange(new string[] { 
								rule.IdentityReference.Value,
								SecurityEditorDialog.GetLocalizedString(rule.FileSystemRights),
								string.Empty });
						}
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

		public string ObjectName
		{
			get { return objName; }
			set { objName = value; objNameText.Text = objName; }
		}

		public RuleType Display { get; set; }

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

		}

		private void editButton_Click(object sender, EventArgs e)
		{

		}

		private void removeButton_Click(object sender, EventArgs e)
		{

		}

		private void inheritedCheck_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
