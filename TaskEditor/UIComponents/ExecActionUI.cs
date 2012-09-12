using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[System.ComponentModel.DefaultEvent("KeyValueChanged"), System.ComponentModel.DefaultProperty("Action")]
	internal partial class ExecActionUI : UserControl, IActionHandler
	{
		public ExecActionUI()
		{
			InitializeComponent();
		}

		public event EventHandler KeyValueChanged;

		[System.ComponentModel.Browsable(false),
		System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public Action Action
		{
			get
			{
				Action ret = new ExecAction(execProgText.Text, null, null);
				if (execArgText.TextLength > 0)
					((ExecAction)ret).Arguments = execArgText.Text;
				if (execDirText.TextLength > 0)
					((ExecAction)ret).WorkingDirectory = execDirText.Text;
				return ret;
			}
			set
			{
				execProgText.Text = ((ExecAction)value).Path;
				execArgText.Text = ((ExecAction)value).Arguments;
				execDirText.Text = ((ExecAction)value).WorkingDirectory;
			}
		}

		public bool IsActionValid()
		{
			return execProgText.TextLength > 0;
		}

		private void execProgBrowseBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				execProgText.Text = openFileDialog1.FileName;
		}

		private void execProgText_TextChanged(object sender, EventArgs e)
		{
			EventHandler h = this.KeyValueChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}
	}
}