using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;

namespace TestTaskService
{
	public partial class HomePanel : UserControl, ISupportTasks
	{
		private TaskService ts;

		public HomePanel()
		{
			InitializeComponent();
		}

		public TaskService TaskService
		{
			get
			{
				return ts;
			}
			set
			{
				ts = value;
				RefreshPanels();
			}
		}

		private void RefreshPanels()
		{
			DateTime now = DateTime.Now;

			// Update summary
			summaryLabel.Text = string.Format("Task Scheduler Summary (Last refreshed: {0:G})", now);
			footerLabel.Text = string.Format("Last refreshed at {0:G}", now);

			// Update status list
			statusListView.Items.Clear();
			statusBackgroundWorker.RunWorkerAsync(comboBox1.SelectedIndex);

			// Update active list
			activeListView.Items.Clear();
			activeBackgroundWorker.RunWorkerAsync();
		}

		public ContextMenuStrip MenuItems
		{
			get { return new ContextMenuStrip(); }
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			RefreshPanels();
		}

		private void statusBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			List<ListViewItem> list = new List<ListViewItem>();
			TimeSpan span = TimeSpan.Zero;
			switch ((int)e.Argument)
			{
				case 0:
					span = TimeSpan.FromHours(1);
					break;
				case 1:
					span = TimeSpan.FromDays(1);
					break;
				case 2:
					span = TimeSpan.FromDays(7);
					break;
				case 3:
					span = TimeSpan.FromDays(30);
					break;
				default:
					break;
			}
			DateTime st = DateTime.Now.Subtract(span);
			TaskEventLog log = new TaskEventLog(st);
			foreach (var t in log)
				list.Add(new ListViewItem(new string[] { t.EventRecord.TaskDisplayName, "", "", "", "" }) { Tag = t });
			e.Result = list.ToArray();
		}

		private void statusBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			statusListView.Items.AddRange(e.Result as ListViewItem[]);
		}

		private void activeBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			using (TaskService lts = new TaskService(ts.TargetServer, ts.UserName, ts.UserAccountDomain, ts.UserPassword, ts.HighestSupportedVersion.Minor == 1))
			{
				List<ListViewItem> list = new List<ListViewItem>();
				foreach (var t in lts.FindAllTasks(null))
					if (t.IsActive)
						list.Add(new ListViewItem(new string[] { t.Name, t.NextRunTime.ToString("G"), t.Definition.Triggers.ToString(), t.Path }) { Tag = t });
				e.Result = list.ToArray();
			}
		}

		private void activeBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			activeListView.Items.AddRange(e.Result as ListViewItem[]);
		}
	}
}
