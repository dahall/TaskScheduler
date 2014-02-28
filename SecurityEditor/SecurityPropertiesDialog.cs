using System.ComponentModel;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace SecurityEditor
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog that allows editing of a Security Descriptor."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true)]
	public partial class SecurityPropertiesDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private SecuredObject so;

		public SecurityPropertiesDialog()
		{
			InitializeComponent();
		}

		[DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Dirty
		{
			get { return applyBtn.Enabled; }
		}

		[DefaultValue(false)]
		public bool Editable
		{
			get { return secProps.Editable; }
			set
			{
				secProps.Editable = value;
				this.Size = value ? this.MinimumSize : this.MaximumSize;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public System.Security.AccessControl.CommonObjectSecurity ObjectSecurity
		{
			get { return secProps.ObjectSecurity; }
			set { secProps.ObjectSecurity = value; }
		}

		public void Initialize(object obj, bool editable = true)
		{
			so = new SecuredObject(obj);
			Initialize(so.ObjectSecurity, so.DisplayName, so.TargetServer, editable);
		}

		public void Initialize(CommonObjectSecurity objSec, string objectName = null, string targetComputer = null, bool editable = true)
		{
			so = new SecuredObject(objSec);
			secProps.ObjectName = objectName;
			secProps.TargetComputer = targetComputer;
			secProps.ObjectSecurity = objSec;
			this.Editable = editable;
		}

		private void applyBtn_Click(object sender, System.EventArgs e)
		{
			so.Persist();
			applyBtn.Enabled = false;
		}

		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			applyBtn_Click(sender, e);
			Close();
		}

		private void secProps_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			applyBtn.Enabled = true;
		}
	}
}