using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog box to set the security properties of a task.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true), DefaultProperty("SecurityDescriptorSddlForm")]
	public partial class TaskSDDLEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private string name = "task";
		private string sddl;

		public TaskSDDLEditDialog()
		{
			InitializeComponent();
		}

		public string SecurityDescriptorSddlForm
		{
			get { return sddl; }
			set
			{
				if (sddl != value)
				{
					sddl = value;
					UpdateUIFromSddl();
				}
			}
		}

		public string TaskName
		{
			get { return name; }
			set
			{
				if (name != value)
				{
					name = value;
					this.Text = "Permissions for " + name;
				}
			}
		}

		private void UpdateUIFromSddl()
		{
			var sd = new System.Security.AccessControl.RawSecurityDescriptor(this.SecurityDescriptorSddlForm);
			for (int i = 0; i < sd.DiscretionaryAcl.Count; i++)
			{
				/*sd.DiscretionaryAcl[i].AceType == System.Security.AccessControl.AceType.AccessAllowed;
				sd.DiscretionaryAcl[i].AceFlags == System.Security.AccessControl.AceFlags.ObjectInherit;
				sd.DiscretionaryAcl*/
			}

			this.listView1.Items.Clear();
			this.listView1.Items.AddRange(new ListViewItem[] {
				new System.Windows.Forms.ListViewItem("Authenticated Users", 0),
				new System.Windows.Forms.ListViewItem("SYSTEM", 0) });
		}		

		private void okBtn_Click(object sender, EventArgs e)
		{
			sddl = UpdateSddlFromUI();
		}

		private string UpdateSddlFromUI()
		{
			return string.Empty;
		}
	}
}
