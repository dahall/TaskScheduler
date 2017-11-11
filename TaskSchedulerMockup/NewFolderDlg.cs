using System;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class NewFolderDlg : Form
	{
		private string name;

		public NewFolderDlg()
		{
			InitializeComponent();
		}

		public string FolderName
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				nameText.Text = value;
			}
		}

		private void nameText_TextChanged(object sender, EventArgs e)
		{
			okBtn.Enabled = nameText.TextLength > 0;
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			name = nameText.Text;
			Close();
		}
	}
}
