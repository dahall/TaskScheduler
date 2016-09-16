using System;
using System.IO;
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
				Action ret = new ExecAction(execProgText.Text);
				if (execArgText.TextLength > 0)
					((ExecAction)ret).Arguments = execArgText.Text;
				if (execDirText.TextLength > 0)
					((ExecAction)ret).WorkingDirectory = execDirText.Text;
				return ret;
			}
			set
			{
				var execAction = value as ExecAction;
				execProgText.Text = execAction?.Path;
				execArgText.Text = execAction?.Arguments;
				execDirText.Text = execAction?.WorkingDirectory;
			}
		}

		public bool CanValidate => ExecAction.IsValidPath(execProgText.Text, false);

		public void Run()
		{
			System.Diagnostics.Process.Start(execProgText.Text, execArgText.TextLength == 0 ? null : execArgText.Text);
		}

		public bool ValidateFields()
		{
			// Check to ensure Path is a valid filename
			if (ExecAction.IsValidPath(execProgText.Text)) return true;
			MessageBox.Show(this, EditorProperties.Resources.Error_InvalidFileName, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
			return false;
		}

		private void execProgBrowseBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				execProgText.Text = openFileDialog1.FileName;
		}

		private void execProgText_TextChanged(object sender, EventArgs e)
		{
			KeyValueChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}