using System.Security.AccessControl;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal partial class AceEditor : Form
	{
		private string objName;
		private AuthorizationRule rule;

		public AceEditor()
		{
			InitializeComponent();
		}

		public AuthorizationRule Rule
		{
			get { return rule; }
			set
			{
				rule = value;
				nameText.Text = rule.IdentityReference.Value;
				applyToCombo.SelectedIndex = 0;
				PermissionItem[] items = null;
				if (rule is FileSystemAccessRule)
				{
					permissionGrid.SetColumns("Allow", "Deny");
				}
				else if (rule is FileSystemAuditRule)
				{
					permissionGrid.SetColumns("Successful", "Failed");
				}
				items = new PermissionItem[] {
						CreatePermissionItem(/*"Full control"*/FileSystemRights.FullControl),
						CreatePermissionItem(/*"Traverse folder / execute file"*/FileSystemRights.ExecuteFile),
						CreatePermissionItem(/*"List folder / read data"*/FileSystemRights.ListDirectory),
						CreatePermissionItem(/*"Read attributes"*/FileSystemRights.ReadAttributes),
						CreatePermissionItem(/*"Read extended attributes"*/FileSystemRights.ReadExtendedAttributes),
						CreatePermissionItem(/*"Create files / write data"*/FileSystemRights.CreateFiles),
						CreatePermissionItem(/*"Create folders / append data"*/FileSystemRights.CreateDirectories),
						CreatePermissionItem(/*"Write attributes"*/FileSystemRights.WriteAttributes),
						CreatePermissionItem(/*"Write extended attributes"*/FileSystemRights.WriteExtendedAttributes),
						CreatePermissionItem(/*"Delete"*/FileSystemRights.Delete),
						CreatePermissionItem(/*"Read permissions"*/FileSystemRights.ReadPermissions),
						CreatePermissionItem(/*"Change permissions"*/FileSystemRights.ChangePermissions),
						CreatePermissionItem(/*"Take ownership"*/FileSystemRights.TakeOwnership)
					};
				permissionGrid.Permissions = items;
			}
		}

		private PermissionItem CreatePermissionItem(FileSystemRights right)
		{
			FileSystemRights granted = rule is FileSystemAccessRule ? ((FileSystemAccessRule)rule).FileSystemRights : ((FileSystemAuditRule)rule).FileSystemRights;
			bool hasRight = (granted & right) != 0;
			bool allow = rule is FileSystemAccessRule ? ((FileSystemAccessRule)rule).AccessControlType == AccessControlType.Allow : ((FileSystemAuditRule)rule).AuditFlags == AuditFlags.Success;
			bool inherited = rule.IsInherited;
			return new PermissionItem(right.ToString(), (int)right,
				hasRight && allow, !(allow && inherited),
				hasRight && !allow, !(!allow && inherited));
		}

		public string ObjectName
		{
			get { return objName; }
			set { objName = value; SetTitle(); }
		}

		private void SetTitle()
		{
			this.Text = string.Format("Permission Entry for {0}", objName);
		}

		private void changeNameBtn_Click(object sender, System.EventArgs e)
		{
			string acctName = string.Empty; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.NativeMethods.AccountUtils.SelectAccount(this, null, ref acctName, out isGroup, out isService))
			{
				nameText.Text = acctName;
				if (rule is FileSystemAccessRule)
					rule = new FileSystemAccessRule(acctName, ((FileSystemAccessRule)rule).FileSystemRights, ((FileSystemAccessRule)rule).InheritanceFlags, ((FileSystemAccessRule)rule).PropagationFlags, ((FileSystemAccessRule)rule).AccessControlType);
				else
					rule = new FileSystemAuditRule(acctName, ((FileSystemAuditRule)rule).FileSystemRights, ((FileSystemAuditRule)rule).InheritanceFlags, ((FileSystemAuditRule)rule).PropagationFlags, ((FileSystemAuditRule)rule).AuditFlags);
			}
		}

		private void clearAllBtn_Click(object sender, System.EventArgs e)
		{
			permissionGrid.ClearAll();
		}

		private void noInheritCheck_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void applyToCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}
	}
}
