using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace SecurityEditor
{
	public partial class RuleEditor :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private bool advPerm = false;
		private bool editable;
		private CommonObjectSecurity sec;
		private AuthorizationRule rule;

		public RuleEditor()
		{
			InitializeComponent();
			Editable = false;
			helpProvider.HelpNamespace = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Help", "mui", System.Globalization.CultureInfo.CurrentUICulture.LCID.ToString(), "aclui.chm");
		}

		[DefaultValue(false)]
		public bool Editable
		{
			get { return editable; }
			set
			{
				editable = value;
				// Enable or show appropriate controls
			}
		}

		[DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CommonObjectSecurity ObjectSecurity
		{
			get { return sec; }
			private set
			{
				if (value != null)
				{
					sec = value;
					// Setup checklist
				}
				else
				{
					sec = null;
					// Clear checklist
				}
			}
		}

		class ComboBoxItem
		{
			public object Value { get; set; }
			public string Text { get; set; }
			
			public ComboBoxItem(object value, string text)
			{
				Value = value; Text = text;
			}

			public override string ToString() { return Text; }
		}

		[DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AuthorizationRule Rule
		{
			get { return rule; }
			private set
			{
				if (value != null)
				{
					rule = value;
					var rights = SecuredObject.GetAccessMask(sec, rule);

					// Principal
					var ai = new AccountInfo(rule.IdentityReference);
					this.principalTextBox.Text = ai.ToString();
					this.principalTextBox.Visible = true;

					// Type combo
					typeComboBox.BeginUpdate();
					typeComboBox.Items.Clear();
					var evals = rule is AccessRule ? Enum.GetValues(typeof(AccessControlType)) : Enum.GetValues(typeof(AuditFlags));
					var cbi = new ComboBoxItem[evals.Length];
					for (int i = 0; i < cbi.Length; i++)
						cbi[i] = new ComboBoxItem(evals.GetValue(i), AccessRights.GetLocalizedString(evals.GetValue(i)));
					typeComboBox.DataSource = cbi;
					typeComboBox.SelectedValue = ((AccessRule)rule).AccessControlType;
					typeComboBox.EndUpdate();

					// Applies to combo
					UpdateInheritanceCombo(rule);

					// Checklist
					this.rightsCheckBoxList.Items.Clear();
					foreach (var item in AccessRights.GetValues(sec.AccessRightType, advPerm))
						this.rightsCheckBoxList.Items.Add(new GroupControls.CheckBoxListItem(item.Value, null) { Tag = item.Key, Checked = PermissionList.EnumHasValue(rights, item.Key) });
				}
				else
				{
					rule = null;
					this.principalTextBox.Clear();
					this.principalTextBox.Visible = false;
					// Clear checklist
				}
			}
		}

		private void UpdateInheritanceCombo(AuthorizationRule rule)
		{
			if (SecuredObject.IsContainerObject(sec))
			{
				appliesToComboBox.BeginUpdate();
				appliesToComboBox.Items.Clear();
				ComboBoxItem[] items = new ComboBoxItem[0];
				if (rule is FileSystemAccessRule || rule is FileSystemAuditRule)
					items = new ComboBoxItem[] {
						BuildInhComboBoxItem(rule, PropagationFlags.None, InheritanceFlags.None),
						BuildInhComboBoxItem(rule, PropagationFlags.None, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit),
						BuildInhComboBoxItem(rule, PropagationFlags.None, InheritanceFlags.ContainerInherit),
						BuildInhComboBoxItem(rule, PropagationFlags.None, InheritanceFlags.ObjectInherit),
						BuildInhComboBoxItem(rule, PropagationFlags.InheritOnly, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit),
						BuildInhComboBoxItem(rule, PropagationFlags.InheritOnly, InheritanceFlags.ContainerInherit),
						BuildInhComboBoxItem(rule, PropagationFlags.InheritOnly, InheritanceFlags.ObjectInherit),
					};
				appliesToComboBox.DataSource = items;
				appliesToComboBox.SelectedValue = PI.ToInt(rule.PropagationFlags, rule.InheritanceFlags);
				appliesToComboBox.EndUpdate();
				hdrTableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.AutoSize);
			}
			else
			{
				appliesToComboBox.Items.Clear();
				hdrTableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Absolute, 0);
			}
		}

		ComboBoxItem BuildInhComboBoxItem(AuthorizationRule rule, PropagationFlags pFlags, InheritanceFlags iFlags)
		{
			return new ComboBoxItem(PI.ToInt(pFlags, iFlags), AccessRights.GetLocalizedInheritanceString(rule.GetType(), pFlags, iFlags));
		}

		struct PI
		{
			public InheritanceFlags InheritanceFlags;
			public PropagationFlags PropagationFlags;

			public PI(PropagationFlags pflags, InheritanceFlags iflags)
			{
				InheritanceFlags = iflags; PropagationFlags = pflags;
			}

			public static int ToInt(PropagationFlags pflags, InheritanceFlags iflags) { return (int)((((int)pflags) << 4) | (int)iflags); }
			public static implicit operator PI(int value) { return new PI((PropagationFlags)(value >> 4), (InheritanceFlags)(value & 0x0F)); }
		}

		private bool ShowAdvancedPermissions
		{
			get { return advPerm; }
			set
			{
				advPerm = value;
				basicPermLabel.Visible = !value;
				advPermLabel.Visible = value;
			}
		}		

		public void Initialize(CommonObjectSecurity objSec, AuthorizationRule rule = null, bool editable = true)
		{
			this.ObjectSecurity = objSec;
			this.Rule = rule;
			this.Editable = editable;
		}

		private void addCondBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void appliesToComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void applySettingsIntCheckBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void clearAllBtn_Click(object sender, EventArgs e)
		{

		}

		private void selectPrincipalBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void showAdvPermBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
