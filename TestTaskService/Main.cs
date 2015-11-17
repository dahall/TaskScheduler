using System;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			Icon = Properties.Resources.TaskScheduler;
			radioButtonList1.SelectedIndex = 0;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Text = $"{Text} - {Environment.OSVersion.Version}";
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
					Program.ShortTest(ts, output, textBox2.Text);
					break;
				case 1: // Long
					Program.LongTest(ts, output, textBox2.Text);
					break;
				case 2: // Editor
					Program.EditorTest(ts, output);
					break;
				case 3: // Find action
					Program.FindTaskWithProperty(ts, output, textBox2.Text);
					//Program.FluentTest(ts, output);
					break;
				case 4: // Wiz
					Program.WizardTest(ts, output);
					break;
				case 5: // MMC
					Program.MMCTest(ts, output);
					break;
				case 6: // Find task
					//Program.FindTask(ts, output, textBox2.Text);
					//new ScriptTestDlg() { TaskService = ts }.ShowDialog(this);
					new TaskEventViewer().ShowDialog(this);
					break;
				case 7: // Output XML
					Program.OutputXml(ts, output);
					Program.OutputJson(ts, output);
					break;
				default:
					break;
			}

			textBox1.Text = output.ToString();
		}
	}
}
