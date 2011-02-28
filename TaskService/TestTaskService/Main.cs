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
		}

		private void reconnectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			TSConnectDlg dlg = new TSConnectDlg();
			dlg.TargetServer = ts.TargetServer;
			dlg.User = ts.UserName;
			dlg.Domain = ts.UserAccountDomain;
			dlg.Password = ts.UserPassword;
			dlg.ForceV1 = ts.HighestSupportedVersion <= new Version(1, 1);
			if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				ts.BeginInit();
				ts.TargetServer = dlg.TargetServer;
				ts.UserName = dlg.User;
				ts.UserAccountDomain = dlg.Domain;
				ts.UserPassword = dlg.Password;
				ts.HighestSupportedVersion = new Version(1, 3);
				ts.EndInit();
			}
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
				case 3: // Find
					Program.FindActionString(ts, output, textBox2.Text);
					break;
				case 4: // Wiz
					Program.WizardTest(ts, output);
					break;
				case 5: // MMC
					Program.MMCTest(ts, output);
					break;
				default:
					break;
			}

			textBox1.Text = output.ToString();
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
