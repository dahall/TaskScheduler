using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
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
		private string sddl;
		private NativeObjectSecurity sec;

		public SecurityProperties()
		{
			InitializeComponent();
			Editable = false;
		}

		[DefaultValue(false)]
		public bool Editable
		{
			get { return editable; }
			set
			{
				this.addRemoveLayoutPanel.Visible = value;
				this.editPermLayoutPanel.Visible = this.advSettingsLayoutPanel.Visible = !value;
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
				}
			}
		}

		[DefaultValue(null)]
		public NativeObjectSecurity ObjectSecurity
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

		private void UpdateUI()
		{
			if (nameLookUpper.IsBusy)
				nameLookUpper.CancelAsync();
			var list = new System.Collections.Generic.List<SecurityIdentifier>();
			foreach (AccessRule rule in sec.GetAccessRules(true, true, typeof(SecurityIdentifier)))
			{
				if (list.Find(delegate(SecurityIdentifier s) { return s.Equals(rule.IdentityReference); }) == null)
					list.Add((SecurityIdentifier)rule.IdentityReference);
			}
			this.userList.Items.Clear();
			this.userList.Items.AddRange(list.ConvertAll<ListViewItem>(BuildUserItem).ToArray());
			//nameLookUpper.RunWorkerAsync(list.ToArray());
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
					this.userList.Items.Add(lvi);
				var aed = new AceEditor() { ObjectName = this.ObjectName, ObjectSecurity = this.ObjectSecurity };
				if (aed.ShowDialog(this) == DialogResult.OK)
				{
					// TODO: Figure out what to do if cancelled
				}
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
				editBtn.SetElevationRequiredState(true);
			var parentBackColor = this.GetTrueParentBackColor();
			objNameText.BackColor = parentBackColor;
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (nameLookUpper.IsBusy)
				nameLookUpper.CancelAsync();
		}

		private void nameLookUpper_DoWork(object sender, DoWorkEventArgs e)
		{
			SecurityIdentifier[] sids = (SecurityIdentifier[])e.Argument;
			for (int i = 0; i < sids.Length; i++)
			{
				if (nameLookUpper.CancellationPending)
					break;
				string name = sids[i].Translate(typeof(NTAccount)).Value;
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

		private void userList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (userList.SelectedIndices.Count > 0)
			{
				removeBtn.Enabled = true;
				var s = userList.SelectedItems[0].Tag;
				curUser = s is AccountInfo ? ((AccountInfo)s).sid : (SecurityIdentifier)s;
				permissionList.Initialize(sec, curUser, SecurityEditor.SecurityRuleType.Access);
			}
			else
			{
				curUser = null;
				removeBtn.Enabled = false;
				permissionList.ResetList(SecurityEditor.SecurityRuleType.Access);
			}
		}

		private void editBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new SecurityPropertiesDialog())
			{
				dlg.Initialize(this.ObjectName, this.ObjectSecurity, this.TargetComputer, true);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					this.ObjectSecurity = dlg.secProps.ObjectSecurity;
				}
			}
		}

		private void advancedBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new AdvancedSecuritySettingsDialog() { Editable = false, ObjectName = this.ObjectName, ObjectSecurity = this.ObjectSecurity })
			{
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					this.ObjectSecurity = dlg.ObjectSecurity;
				}
			}
		}

		private void learnAboutLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void permissionList_SizeChanged(object sender, EventArgs e)
		{
			var margin = tableLayoutPanel5.Margin;
			margin.Right = permissionList.VerticalScroll.Visible ? 3 + SystemInformation.VerticalScrollBarWidth : 3;
			tableLayoutPanel5.Margin = margin;
		}
	}
}
