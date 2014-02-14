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
			this.Initialize(file.FullName, file.GetAccessControl(), null, editable);
		}

		public void Initialize(string objectName, System.Security.AccessControl.NativeObjectSecurity objSec, string targetComputer = null, bool editable = true)
		{
			secProps.ObjectName = objectName;
			secProps.TargetComputer = targetComputer;
			secProps.ObjectSecurity = objSec;
			this.Editable = editable;
		}

		public void Initialize(Microsoft.Win32.TaskScheduler.Task task, bool editable = true)
		{
			this.Initialize(task.Name, new System.Security.AccessControl.TaskSecurity(task), task.TaskService.TargetServer, editable);
		}

		public System.Security.AccessControl.NativeObjectSecurity ObjectSecurity
		{
			get { return secProps.ObjectSecurity; }
			set { secProps.ObjectSecurity = value; }
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
