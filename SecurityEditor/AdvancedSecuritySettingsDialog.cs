using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog that allows editing of a Security Descriptor."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true)]
	public partial class AdvancedSecuritySettingsDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		SecuredObject so;

		public AdvancedSecuritySettingsDialog()
		{
			InitializeComponent();
			Editable = false;
			helpProvider.HelpNamespace = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Help", "mui", System.Globalization.CultureInfo.CurrentUICulture.LCID.ToString(), "aclui.chm");
			effAccUserText.Text = effAccDeviceText.Text = string.Empty;
		}

		[DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Dirty
		{
			get { return applyBtn.Enabled; }
		}

		[DefaultValue(false)]
		public bool Editable
		{
			get { return permEditor.Editable; }
			set
			{
				permEditor.Editable = audEditor.Editable = value;
				audHeaderLayoutPanel.Visible = audEditor.Visible = value;
				notEditableLayoutPanel.Visible = !value;
			}
		}

		[DefaultValue("")]
		public string ObjectName
		{
			get { return so.DisplayName; }
			set
			{
				so.DisplayName = value;
				this.objNameText.Text = value;
				headerLayoutPanel.RowStyles[0] = this.objNameText.TextLength == 0 ? new RowStyle(SizeType.Absolute, 0) : new RowStyle(SizeType.AutoSize);
			}
		}

		private SecuredObject.SystemMandatoryLabelLevel MandatoryLabel
		{
			get { return so.MandatoryLabel.Level; }
			set
			{
				this.intLevelText.Text = so.MandatoryLabel.IsSet ? AccessRights.GetLocalizedString(so.MandatoryLabel.Level) : null;
				headerLayoutPanel.RowStyles[2] = this.intLevelText.TextLength == 0 ? new RowStyle(SizeType.Absolute, 0) : new RowStyle(SizeType.AutoSize);
			}
		}

		public void Initialize(object obj, bool editable = true)
		{
			so = new SecuredObject(obj);
			Initialize(so.ObjectSecurity, so.DisplayName, so.TargetServer, editable);
		}

		public void Initialize(CommonObjectSecurity objSec, string objectName = null, string targetComputer = null, bool editable = true)
		{
			if (so == null)
				so = new SecuredObject(objSec);
			this.ObjectSecurity = so.ObjectSecurity;
			this.ObjectName = objectName;
			this.TargetComputer = targetComputer;
			this.MandatoryLabel = so.MandatoryLabel.Level;
			this.Editable = editable;
		}

		public CommonObjectSecurity ObjectSecurity
		{
			get { return so.ObjectSecurity; }
			private set
			{
				permEditor.ObjectSecurity = so.ObjectSecurity;
				audEditor.ObjectSecurity = so.ObjectSecurity;
				ownerText.Text = new AccountInfo((SecurityIdentifier)so.ObjectSecurity.GetOwner(typeof(SecurityIdentifier))).ToString();
			}
		}

		public string TargetComputer
		{
			get { return permEditor.TargetComputer; }
			set
			{
				permEditor.TargetComputer = value;
				audEditor.TargetComputer = value;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
				audContinueBtn.SetElevationRequiredState(true);
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			tabControl1.Dock = DockStyle.None;
			tabControl1.SetBounds(headerLayoutPanel.Left - 1, headerLayoutPanel.Bottom, headerLayoutPanel.Width + 4, panel1.Height - headerLayoutPanel.Height);
		}

		private void applyBtn_Click(object sender, EventArgs e)
		{
			so.Persist();
			applyBtn.Enabled = false;
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			applyBtn_Click(sender, e);
			Close();
		}

		private void chgOwnerLink_LinkClicked(object sender, EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.HelperMethods.SelectAccount(this, this.TargetComputer, ref acctName, out isGroup, out isService, out sid))
			{
				var ai = new AccountInfo(new SecurityIdentifier(sid));
				ownerText.Text = ai.ToString();
				this.ObjectSecurity.SetOwner(ai);
			}
		}

		private void audContinueBtn_Click(object sender, EventArgs e)
		{
			notEditableLayoutPanel.Visible = false;
			audHeaderLayoutPanel.Visible = audEditor.Visible = true;
		}

		private void effAccSelUserBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		private void effAccSelDeviceBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}
	}
}
