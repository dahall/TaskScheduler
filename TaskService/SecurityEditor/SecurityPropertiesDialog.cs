using System.ComponentModel;
using System.Windows.Forms;

namespace SecurityEditor
{
	public partial class SecurityPropertiesDialog : Form
	{
		public SecurityPropertiesDialog()
		{
			InitializeComponent();
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

		public void Initialize(System.IO.FileInfo file, bool editable = true)
		{
			this.Initialize(file.FullName, file.GetAccessControl().GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections.All), null, editable);
		}

		public void Initialize(string objName, System.Security.AccessControl.NativeObjectSecurity objSec, string targetComputer = null, bool editable = true)
		{
			this.Initialize(objName, objSec.GetSecurityDescriptorSddlForm(System.Security.AccessControl.AccessControlSections.All), targetComputer, editable);
		}

		public void Initialize(Microsoft.Win32.TaskScheduler.Task task, bool editable = true)
		{
			this.Initialize(task.Name, task.GetSecurityDescriptorSddlForm(Microsoft.Win32.TaskScheduler.TaskSecurityDescriptorSections.All), task.TaskService.TargetServer, editable);
		}

		public void Initialize(string objectName, string sddl, string targetComputer = null, bool editable = true)
		{
			secProps.ObjectName = objectName;
			secProps.TargetComputer = targetComputer;
			secProps.SecurityDescriptorSddlForm = sddl;
			this.Editable = editable;
		}

		public string SecurityDescriptorSddlForm
		{
			get { return secProps.SecurityDescriptorSddlForm; }
			set { secProps.SecurityDescriptorSddlForm = value; }
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			applyBtn_Click(sender, e);
			Close();
		}

		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void applyBtn_Click(object sender, System.EventArgs e)
		{

		}
	}
}
