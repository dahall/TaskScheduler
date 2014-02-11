using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	public class PermissionList : CheckedColumnList
	{
		private NativeObjectSecurity acl;
		private SecurityIdentifier id;
		private CheckedColumnListItem[] items;
		private SecurityRuleType rType;

		public PermissionList()
		{
			//if (this.DesignMode)
			{
				ResetList(SecurityRuleType.Access);
				base.Items.AddRange(items);
			}
			this.Padding = new Padding(8, 5, 0, 5);
			this.ItemSpacing = new System.Drawing.Size(0, 5);
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public override int ColumnCount
		{
			get { return base.ColumnCount; }
			set { base.ColumnCount = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public override CheckedColumnListItemCollection Items
		{
			get { return base.Items; }
		}

		internal void Initialize(NativeObjectSecurity acl, SecurityIdentifier id, SecurityRuleType rType)
		{
			this.acl = acl;
			this.id = id;
			this.rType = rType;
			// Reset list
			ResetList(rType);
			// Apply applicable ACEs
			if (rType == SecurityRuleType.Access)
			{
				foreach (FileSystemAccessRule rule in acl.GetAccessRules(true, true, typeof(SecurityIdentifier)))
				{
					if (rule.IdentityReference.Equals(id))
					{
						int col = rule.AccessControlType == AccessControlType.Allow ? 0 : 1;
						for (int i = 0; i < 5; i++)
						{
							if (((FileSystemRights)items[i].Tag & rule.FileSystemRights) == (FileSystemRights)items[i].Tag)
								items[i].Values[col] = rule.IsInherited ? CheckState.CheckedDisabled : CheckState.Checked;
							else
								items[i].Values[col] = rule.IsInherited ? CheckState.UncheckedDisabled : CheckState.Unchecked;
						}
					}
				}
			}
			else
			{
				foreach (FileSystemAuditRule rule in acl.GetAuditRules(true, true, typeof(SecurityIdentifier)))
				{
					if (rule.IdentityReference.Equals(id))
					{
						int col = rule.AuditFlags == AuditFlags.Success ? 0 : 1;
						for (int i = 0; i < 5; i++)
						{
							if (((FileSystemRights)items[i].Tag & rule.FileSystemRights) == (FileSystemRights)items[i].Tag)
								items[i].Values[col] = rule.IsInherited ? CheckState.CheckedDisabled : CheckState.Checked;
							else
								items[i].Values[col] = rule.IsInherited ? CheckState.UncheckedDisabled : CheckState.Unchecked;
						}
					}
				}
			}
			base.Items.Clear();
			base.Items.AddRange(items);
			Refresh();
		}

		internal void ResetList(SecurityRuleType ruleType)
		{
			if (ruleType == SecurityRuleType.Access)
			{
				items = new CheckedColumnListItem[6];
				items[0] = new CheckedColumnListItem("Full control", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.FullControl };
				items[1] = new CheckedColumnListItem("Modify", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Modify };
				items[2] = new CheckedColumnListItem(@"Read & execute", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.ReadAndExecute };
				items[3] = new CheckedColumnListItem("Read", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Read };
				items[4] = new CheckedColumnListItem("Write", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Write };
				items[5] = new CheckedColumnListItem("Special permissions", CheckState.UncheckedDisabled, CheckState.UncheckedDisabled);
			}
			else
			{
				items = new CheckedColumnListItem[6];
				items[0] = new CheckedColumnListItem("Full control", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.FullControl };
				items[1] = new CheckedColumnListItem("Modify", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Modify };
				items[2] = new CheckedColumnListItem(@"Read & execute", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.ReadAndExecute };
				items[3] = new CheckedColumnListItem("Read", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Read };
				items[4] = new CheckedColumnListItem("Write", CheckState.Unchecked, CheckState.Unchecked) { Tag = FileSystemRights.Write };
				items[5] = new CheckedColumnListItem("Special permissions", CheckState.UncheckedDisabled, CheckState.UncheckedDisabled);
			}
		}

		protected override void OnItemChanged(CheckedColumnList.ItemChangedEventArgs e)
		{
			base.OnItemChanged(e);
			FileSystemRights right = (FileSystemRights)e.Item.Tag;
			bool set = e.Item.Values[e.ChangedColumn] == CheckState.Checked;
			int otherCol = e.ChangedColumn == 0 ? 1 : 0;
			//FileSystemRights newAllow, newDeny;
			for (int i = 0; i < 5; i++)
			{
				bool changed = false;
				if (set && (right & (FileSystemRights)base.Items[i].Tag) == (FileSystemRights)base.Items[i].Tag && (base.Items[i] == e.Item || base.Items[i].Values[e.ChangedColumn] == CheckState.Unchecked))
				{
					base.Items[i].Values[e.ChangedColumn] = CheckState.Checked;
					if (base.Items[i].Values[otherCol] == CheckState.Checked)
						base.Items[i].Values[otherCol] = CheckState.Unchecked;
					changed = true;
				}
				if (!set && (right & (FileSystemRights)base.Items[i].Tag) == right && (base.Items[i] == e.Item || base.Items[i].Values[e.ChangedColumn] == CheckState.Checked))
				{
					base.Items[i].Values[e.ChangedColumn] = CheckState.Unchecked;
					if (base.Items[i].Values[otherCol] == CheckState.Checked)
						base.Items[i].Values[otherCol] = CheckState.Unchecked;
					changed = true;
				}
				if (changed)
					InvalidateItem(i);
			}
		}
	}
}
