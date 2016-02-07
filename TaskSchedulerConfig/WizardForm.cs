using System;
using System.Windows.Forms;

namespace TaskSchedulerConfig
{
	public partial class WizardForm : Form
	{
		public WizardForm()
		{
			InitializeComponent();
		}

		private enum ScanState { Privileges, Firewall, Services, Complete };

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void connRemoteBtn_Click(object sender, EventArgs e)
		{
			wizardControl1.NextPage(selectRemote);
		}

		private void explOptionsBtn_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://taskscheduler.codeplex.com/wikipage?title=TaskSecurity");
		}

		private void intro_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			runAsAdminPrompt.Visible = !(new Validator().UserIsAdmin);
		}

		private void localCloseBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void localScanner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			localScanner.ReportProgress(0, ScanState.Privileges);
			Result r = new Result();
			Validator v = new Validator(null);
			if (localScanner.CancellationPending) return;
			r.SetPriv(v);
			localScanner.ReportProgress(33, ScanState.Firewall);
			if (localScanner.CancellationPending) return;
			r.SetFW(v);
			localScanner.ReportProgress(66, ScanState.Services);
			if (localScanner.CancellationPending) return;
			r.SetSvc(v);
			localScanner.ReportProgress(100, ScanState.Complete);
			e.Result = r;
		}

		private void localScanner_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			switch ((ScanState)e.UserState)
			{
				case ScanState.Privileges:
					scanLocalStatusLabel.Text = "Looking for problems with account privileges...";
					break;
				case ScanState.Firewall:
					scanLocalStatusLabel.Text = "Looking for problems with firewall...";
					break;
				case ScanState.Services:
					scanLocalStatusLabel.Text = "Looking for problems with services...";
					break;
				case ScanState.Complete:
				default:
					wizardControl1.NextPage();
					break;
			}
		}

		private void localScanner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				if (e.Error != null)
				{
					localResultLabel.Text = "An error occurred while troubleshooting. Error: " + e.Error.ToString();
				}
				else
				{
					Result r = e.Result as Result;
					if (r != null)
					{
						localResultLabel.Text = "";
					}
				}
			}
		}

		private void remoteConnBtn_Click(object sender, EventArgs e)
		{
			wizardControl1.NextPage(selectRemote);
		}

		private void remoteScanner_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
		}

		private void remoteScanner_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{

		}

		private void remoteScanner_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{

		}

		private void scanLocal_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			localScanner.RunWorkerAsync();
		}

		private void scanRemote_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
		}

		private void selectRemote_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			remoteScanner.RunWorkerAsync();
		}

		private void showLocalResults_HelpClicked(object sender, EventArgs e)
		{
			wizardControl1.NextPage(localReport);
		}

		private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (localScanner.IsBusy) localScanner.CancelAsync();
			if (remoteScanner.IsBusy) remoteScanner.CancelAsync();
		}

		private void WizardForm_Load(object sender, EventArgs e)
		{

		}

		private class Result
		{
			public string user;
			public bool userIsAdmin, userIsBO, userIsSO, firewallEnabled, fwFPSEnabled, fwRTMEnabled, svcRemRegEnabled;

			public void SetFW(Validator v)
			{
				firewallEnabled = v.Firewall.Enabled;
				fwFPSEnabled = v.Firewall.Rules[Firewall.Rule.FileAndPrinterSharing];
				fwRTMEnabled = v.Firewall.Rules[Firewall.Rule.RemoteTaskManagement];
			}

			public void SetPriv(Validator v)
			{
				user = v.User;
				userIsAdmin = v.UserIsAdmin;
				userIsBO = v.UserIsBackupOperator;
				userIsSO = v.UserIsServerOperator;
			}

			public void SetSvc(Validator v)
			{
				svcRemRegEnabled = v.RemoteRegistryServiceRunning;
			}
		}
	}
}
