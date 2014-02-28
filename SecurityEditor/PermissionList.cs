using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal class PermissionList : CheckedColumnList
	{
		private CommonObjectSecurity acl;
		private SecurityIdentifier id;
		private CheckedColumnListItem[] items;
		private List<Perm> perms = new List<Perm>();

		class Perm : IComparable<Perm>
		{
			public AccessRule Rule;
			public object Rights;
			public AccountInfo Id;

			public Perm(CommonObjectSecurity sec, AccessRule ar)
			{
				Rule = ar;
				Rights = SecuredObject.GetAccessMask(sec, ar);
				Id = new AccountInfo(ar.IdentityReference);
			}

			public int CompareTo(Perm other)
			{
				int ret = Id.CompareTo(other.Id);
				if (ret == 0)
				{
					ret = Rule.AccessControlType.CompareTo(other.Rule.AccessControlType);
					if (ret == 0)
					{
						ret = other.Rule.IsInherited.CompareTo(Rule.IsInherited);
						if (ret == 0)
							ret = Convert.ToInt64(other.Rights).CompareTo(Convert.ToInt64(Rights));
					}
				}
				return ret;
			}

			public override string ToString()
			{
				return string.Format("{0} ({1}{3}) = 0x{2:X}:{2}", Id, Rule.AccessControlType, Rights, Rule.IsInherited ? "(I)" : "");
			}
		}

		public PermissionList()
		{
			this.Padding = new Padding(8, 5, 0, 5);
			this.ItemSpacing = new System.Drawing.Size(0, 5);
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public override int ColumnCount
		{
			get { return base.ColumnCount; }
			set { base.ColumnCount = value; }
		}

		[DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SecurityIdentifier CurrentSid
		{
			get { return id; }
			set
			{
				if (!this.DesignMode)
				{
					id = value;
					SetListState();
				}
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public override CheckedColumnListItemCollection Items
		{
			get { return base.Items; }
		}

		public static bool EnumHasValue(object enumValue, object value)
		{
			if (value == null) return false;
			long val = Convert.ToInt64(value);
			return (Convert.ToInt64(enumValue) & val) == val;
		}

		public void Initialize(CommonObjectSecurity acl)
		{
			this.acl = acl;
			ResetList();
		}

		internal void ResetList()
		{
			if (acl != null)
			{
				// Capture permissions
				perms.Clear();
				foreach (AccessRule ar in acl.GetAccessRules(true, true, typeof(SecurityIdentifier)))
					perms.Add(new Perm(acl, ar));
				perms.Sort();

				// Build out rights list
				var vals = AccessRights.GetValues(acl.AccessRightType);
				items = new CheckedColumnListItem[vals.Count];
				for (int i = 0; i < items.Length; i++)
				{
					var chkState = (vals[i].Key == null) ? CheckState.UncheckedDisabled : CheckState.Unchecked;
					items[i] = new CheckedColumnListItem(vals[i].Value, chkState, chkState) { Tag = vals[i].Key };
				}
				base.Items.Clear();
				base.Items.AddRange(items);
			}
		}

		protected override void OnItemChanged(CheckedColumnList.ItemChangedEventArgs e)
		{
			base.OnItemChanged(e);
			bool set = e.Item.Values[e.ChangedColumn] == CheckState.Checked;
			int otherCol = e.ChangedColumn == 0 ? 1 : 0;
			for (int i = 0; i < base.Items.Count; i++)
			{
				bool changed = false;
				if (set && EnumHasValue(e.Item.Tag, base.Items[i].Tag) && (base.Items[i] == e.Item || base.Items[i].Values[e.ChangedColumn] == CheckState.Unchecked))
				{
					base.Items[i].Values[e.ChangedColumn] = CheckState.Checked;
					if (base.Items[i].Values[otherCol] == CheckState.Checked)
						base.Items[i].Values[otherCol] = CheckState.Unchecked;
					changed = true;
				}
				if (!set && EnumHasValue(base.Items[i].Tag, e.Item.Tag) && (base.Items[i] == e.Item || base.Items[i].Values[e.ChangedColumn] == CheckState.Checked))
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

		private void SetListState()
		{
			if (id == null)
			{
				for (int i = 0; i < items.Length; i++)
					items[i].Values[0] = items[i].Values[1] = CheckState.Unchecked;
			}
			else
			{
				// Apply applicable ACEs
				var idPerms = perms.FindAll(delegate(Perm p) { return p.Id.CompareTo(id) == 0; });
				var nomessy = new bool[items.Length];
				for (int pi = 0; pi < idPerms.Count; pi++)
				{
					long leftover = Convert.ToInt64(idPerms[pi].Rights);
					int col = idPerms[pi].Rule.AccessControlType == AccessControlType.Allow ? 0 : 1;
					for (int i = 0; i < items.Length; i++)
					{
						if (items[i].Tag == null) // Handle "Special permissions case"
						{
							const long SYNCHRONIZE = 0x100000;
							if ((leftover & SYNCHRONIZE) == SYNCHRONIZE)
								leftover ^= SYNCHRONIZE;
							items[i].Values[col] = leftover > 0 ? CheckState.CheckedDisabled : CheckState.UncheckedDisabled;
						}
						else
						{
							if (EnumHasValue(idPerms[pi].Rights, items[i].Tag))
							{
								if (EnumHasValue(leftover, items[i].Tag))
									leftover ^= Convert.ToInt64(items[i].Tag);
								if (!nomessy[i])
									items[i].Values[col] = idPerms[pi].Rule.IsInherited ? CheckState.CheckedDisabled : CheckState.Checked;
								if (idPerms[pi].Rule.IsInherited)
									nomessy[i] = true;
							}
							else
								items[i].Values[col] = idPerms[pi].Rule.IsInherited ? CheckState.UncheckedDisabled : CheckState.Unchecked;
						}
					}
				}
				base.Items.Clear();
				base.Items.AddRange(items);
			}
			Refresh();
		}
	}
}