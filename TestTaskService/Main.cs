using System;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskService
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			radioButtonList1.SelectedIndex = 0;
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void reconnectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			taskServiceConnectDialog1.ShowDialog(this);
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			System.IO.StringWriter output = new System.IO.StringWriter();

			switch (radioButtonList1.SelectedIndex)
			{
				case 0: // Short
					Program.ShortTest(ts, output);
					break;
				case 1: // Long
					Program.LongTest(ts, output);
					break;
				case 2: // Editor
					Program.EditorTest(ts, output);
					break;
				case 3: // Find action
					Program.FindActionString(ts, output, textBox2.Text);
					break;
				case 4: // Wiz
					Program.WizardTest(ts, output);
					break;
				case 5: // MMC
					Program.MMCTest(ts, output);
					break;
				case 6: // Find task
					Program.FindTask(ts, output, textBox2.Text);
					break;
				default:
					break;
			}

			textBox1.Text = output.ToString();
		}
	}
}
