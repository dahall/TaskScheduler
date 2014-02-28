using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog box to set the security properties of a task.")]
	//[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true), DefaultProperty("SecurityDescriptorSddlForm")]
	public partial class SecurityProperties : UserControl
	{
		private SecurityIdentifier curUser = null;
		private bool editable;
		private string name = "";
		private CommonObjectSecurity sec;

		public SecurityProperties()
		{
			InitializeComponent();
			userImageList.Images.Add(Properties.Resources.User);
			userImageList.Images.Add(Properties.Resources.Group);
			Editable = false;
			helpProvider.HelpNamespace = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Help", "mui", System.Globalization.CultureInfo.CurrentUICulture.LCID.ToString(), "aclui.chm");
		}

		/// <summary>
		/// Occurs when any value on this control changes
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		[DefaultValue(false)]
		public bool Editable
		{
			get { return editable; }
			set
			{
				this.addRemoveLayoutPanel.Visible = value;
				this.editPermLayoutPanel.Visible = this.advSettingsLayoutPanel.Visible = !value;
				this.permissionList.Enabled = value;
				editable = value;
			}
		}

		[DefaultValue("")]
		public string ObjectName
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					this.objNameText.Text = name;
					this.nameLayoutPanel.Visible = this.objNameText.TextLength > 0;
				}
			}
		}

		[DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CommonObjectSecurity ObjectSecurity
		{
			get { return sec; }
			set
			{
				if (!this.DesignMode)
				{
					sec = value;
					UpdateUI();
				}
			}
		}

		[DefaultValue((string)null)]
		public string TargetComputer { get; set; }

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
				editBtn.SetElevationRequiredState(true);
			//var parentBackColor = this.GetTrueParentBackColor();
			//objNameText.BackColor = parentBackColor;
		}

		/// <summary>
		/// Raises the <see cref="E:PropertyChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var h = this.PropertyChanged;
			if (h != null)
				h(this, e);
		}

		private void addBtn_Click(object sender, EventArgs e)
		{
			string acctName = string.Empty, ssid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.HelperMethods.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out ssid))
			{
				SecurityIdentifier sid = new SecurityIdentifier(ssid);
				bool modified;
				AccessRule aRule = sec.AccessRuleFactory(sid, int.MaxValue, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);
				sec.ModifyAccessRule(AccessControlModification.Add, aRule, out modified);
				var lvi = BuildUserItem(sid);
				if (!this.userList.Items.ContainsKey(lvi.Text))
				{
					lvi = this.userList.Items.Add(lvi);
					lvi.Selected = true;
					OnPropertyChanged(new PropertyChangedEventArgs("AccessRule"));
				}
			}
		}

		private void advancedBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new AdvancedSecuritySettingsDialog())
			{
				dlg.Initialize(ObjectSecurity, ObjectName, TargetComputer, false);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					this.ObjectSecurity = dlg.ObjectSecurity;
				}
			}
		}

		private ListViewItem BuildUserItem(SecurityIdentifier s)
		{
			int iconIndex = 0;
			try
			{
				AccountInfo ai = new AccountInfo(s, TargetComputer);
				if (ai.use != Microsoft.Win32.NativeMethods.SidNameUse.User)
					iconIndex = 1;
				return new ListViewItem(ai.ToString(), iconIndex) { Tag = ai };
			}
			catch { }
			return new ListViewItem(s.Value, iconIndex) { Tag = s };
		}

		private void editBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new SecurityPropertiesDialog())
			{
				dlg.Initialize(this.ObjectSecurity, this.ObjectName, this.TargetComputer, true);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					this.ObjectSecurity = dlg.secProps.ObjectSecurity;
				}
			}
		}

		private void learnAboutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.OnHelpRequested(new HelpEventArgs(System.Drawing.Point.Empty));
		}

		private void permissionList_ItemChanged(object sender, CheckedColumnList.ItemChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("PermissionList"));
		}

		private void permissionList_SizeChanged(object sender, EventArgs e)
		{
			var margin = tableLayoutPanel5.Margin;
			margin.Right = permissionList.VerticalScroll.Visible ? 3 + SystemInformation.VerticalScrollBarWidth : 3;
			tableLayoutPanel5.Margin = margin;
		}

		private void removeBtn_Click(object sender, EventArgs e)
		{
			bool modified;
			sec.ModifyAccessRule(AccessControlModification.Remove, (AccessRule)userList.SelectedItems[0].Tag, out modified);
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

		private void UpdateUI()
		{
			var list = new System.Collections.Generic.List<SecurityIdentifier>();
			foreach (AccessRule rule in sec.GetAccessRules(true, true, typeof(SecurityIdentifier)))
			{
				if (list.Find(delegate(SecurityIdentifier s) { return s.Equals(rule.IdentityReference); }) == null)
					list.Add((SecurityIdentifier)rule.IdentityReference);
			}
			this.userList.Items.Clear();
			this.userList.Items.AddRange(list.ConvertAll<ListViewItem>(BuildUserItem).ToArray());
			this.permissionList.Initialize(sec);
			if (list.Count > 0)
				this.userList.Items[0].Selected = true;
		}

		private void userList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (userList.SelectedIndices.Count > 0)
			{
				removeBtn.Enabled = true;
				var s = userList.SelectedItems[0].Tag;
				curUser = s is AccountInfo ? ((AccountInfo)s).sid : (SecurityIdentifier)s;
				permissionList.CurrentSid = curUser;
			}
			else
			{
				curUser = null;
				removeBtn.Enabled = false;
				permissionList.CurrentSid = null;
			}
		}

		private void userList_SizeChanged(object sender, EventArgs e)
		{
			columnHeader1.Width = userList.ClientRectangle.Width - 1;
		}
	}
}