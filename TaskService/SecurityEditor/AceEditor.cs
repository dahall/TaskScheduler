using System.Security.AccessControl;
using System.Windows.Forms;

namespace SecurityEditor
{
	internal partial class AceEditor : Form
	{
		private string objName;
		private FileSecurity sec;

		public AceEditor()
		{
			InitializeComponent();
		}

		public FileSecurity ObjectSecurity
		{
			get { return sec; }
			set
			{
				sec = value;
			}
		}

		public string ObjectName
		{
			get { return objName; }
			set { objName = value; SetTitle(); }
		}

		public SecurityRuleType Display { get; set; }

		private void SetTitle()
		{
			this.Text = string.Format("Permission Entry for {0}", objName);
		}

		private void changeNameBtn_Click(object sender, System.EventArgs e)
		{
			string acctName = string.Empty, sid; bool isGroup, isService;
			if (Microsoft.Win32.TaskScheduler.NativeMethods.AccountUtils.SelectAccount(this, null, ref acctName, out isGroup, out isService, out sid))
			{
				nameText.Text = acctName;
				accessPermissionList1.Initialize(sec, new System.Security.Principal.NTAccount(acctName), Display);
			}
		}

		private void clearAllBtn_Click(object sender, System.EventArgs e)
		{
			// TODO:
		}

		private void noInheritCheck_CheckedChanged(object sender, System.EventArgs e)
		{

		}

		private void applyToCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}
	}
}
