using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal partial class ACLEditor : UserControl
	{
		private bool editable;
		private CommonObjectSecurity sec;

		public ACLEditor()
		{
			InitializeComponent();
			userImageList.Images.Add(Properties.Resources.User);
			userImageList.Images.Add(Properties.Resources.Group);
			Display = SecurityRuleType.Access;
			Editable = false;
		}

		[DefaultValue(SecurityRuleType.Access)]
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

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsContainer { get; set; }

		[DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CommonObjectSecurity ObjectSecurity
		{
			get { return sec; }
			set
			{
				if (value != null)
				{
					sec = value;
					IsContainer = SecuredObject.IsContainerObject(sec);
					permissionsListView.BeginUpdate();
					permissionsListView.Items.Clear();
					if (Display == SecurityRuleType.Access)
					{
						AuthorizationRuleCollection coll = sec.GetAccessRules(true, true, typeof(SecurityIdentifier));
						foreach (AccessRule rule in coll)
							AddListItem(rule);
					}
					else
					{
						AuthorizationRuleCollection coll = sec.GetAuditRules(true, true, typeof(SecurityIdentifier));
						foreach (AuditRule rule in coll)
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

		[DefaultValue((string)null)]
		public string TargetComputer { get; set; }

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
				chgPermBtn.SetElevationRequiredState(true);
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			var cw = permissionsListView.ClientSize.Width - imageColHdr.Width;
			if (!IsContainer)
			{
				columnHeader1.Width = (int)Math.Floor(0.07 * cw);
				columnHeader2.Width = (int)Math.Floor(0.31 * cw);
				columnHeader3.Width = (int)Math.Floor(0.36 * cw);
				columnHeader4.Width = (int)Math.Floor(0.26 * cw);
				columnHeader5.Width = 0;
			}
			else
			{
				columnHeader1.Width = (int)Math.Floor(0.07 * cw);
				columnHeader2.Width = (int)Math.Floor(0.31 * cw);
				columnHeader3.Width = (int)Math.Floor(0.19 * cw);
				columnHeader4.Width = (int)Math.Floor(0.19 * cw);
				columnHeader5.Width = (int)Math.Floor(0.24 * cw);
			}
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			using (var dlg = new RuleEditor())
			{
				dlg.Initialize(this.ObjectSecurity, null, true);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{

				}
			}
			/*string acctName = string.Empty, sid; bool isGroup, isService;
			bool modified;
			if (Microsoft.Win32.TaskScheduler.HelperMethods.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{
				sec.AccessRuleFactory(new SecurityIdentifier(sid), 0, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
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
			}*/
		}

		private void AddListItem(AuthorizationRule rule)
		{
			var ai = new AccountInfo((SecurityIdentifier)rule.IdentityReference, this.TargetComputer);
			string text = AccessRights.GetLocalizedString(rule is AccessRule ? (object)((AccessRule)rule).AccessControlType : ((AuditRule)rule).AuditFlags);
			object accessMask = SecuredObject.GetAccessMask(sec, rule);
			System.Diagnostics.Debug.WriteLine(string.Format("{0} {1} {2}; Inh={4}:{5};{6}", text, ai.ToString(), accessMask, ai.use, rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags));
			const long MASK = 0x101FFFFF;
			if ((Convert.ToInt64(accessMask) & MASK) == 0)
				return;
			if (Convert.ToInt64(accessMask) == 0x10000000 /*GENERIC_ALL*/)
				try { accessMask = Enum.Parse(sec.AccessRightType, "FullControl"); } catch { }
			const long SYNCH = 0x00100000;
			if (!accessMask.ToString().Equals("FullControl", StringComparison.InvariantCultureIgnoreCase) && (Convert.ToInt64(accessMask) & SYNCH) == SYNCH)
				accessMask = Enum.ToObject(sec.AccessRightType, Convert.ToInt64(accessMask) & ~SYNCH);
			string accessText = AccessRights.GetLocalizedAccessString(accessMask);
			string inheritedFromText = string.Empty; // TODO: Figure out how to get this value
			string appliesToText = IsContainer ? AccessRights.GetLocalizedInheritanceString(rule) : "";
			ListViewItem item = new ListViewItem(new string[] { "", text, ai.ToString(), accessText, inheritedFromText, appliesToText }, ai.use == Microsoft.Win32.NativeMethods.SidNameUse.User ? 0 : 1) { Tag = rule };
			item = permissionsListView.Items.Add(item);
			if (item.Index == 0) item.Selected = true;
		}

		private void chgPermBtn_Click(object sender, EventArgs e)
		{
			this.Editable = true;
			/*using (var dlg = new AdvancedSecuritySettingsDialog())
			{
				dlg.Initialize(this.ObjectSecurity, ((AdvancedSecuritySettingsDialog)this.ParentForm).ObjectName, this.TargetComputer, true);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{

				}
			}*/
		}

		private void editButton_Click(object sender, EventArgs e)
		{
		}

		private void noInheritBtn_Click(object sender, EventArgs e)
		{
		}

		private void permissionsListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			editButton_Click(sender, EventArgs.Empty);
		}

		private void permissionsListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			viewBtn.Enabled = removeBtn.Enabled = (permissionsListView.SelectedIndices.Count > 0);
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

		private void viewBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new RuleEditor())
			{
				dlg.Initialize(this.ObjectSecurity, permissionsListView.SelectedItems[0].Tag as AuthorizationRule, true);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{

				}
			}
		}
	}
}