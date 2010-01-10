using System.Windows.Forms;

namespace TestTaskService
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
