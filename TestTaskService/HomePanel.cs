using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class HomePanel : UserControl, ISupportTasks
	{
		private TaskService ts;

		public HomePanel()
		{
			InitializeComponent();
			comboBox1.SelectedIndex = 0;
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
			summaryLabel.Text = $"Task Scheduler Summary (Last refreshed: {now:G})";
			footerLabel.Text = $"Last refreshed at {now:G}";

			if (ts == null)
				return;

			// Update status list
			UpdateStatusList();

			// Update active list
			UpdateActiveList();
		}

		private void UpdateActiveList()
		{
			label6.Text = "Reading data...";
			activeListView.Items.Clear();
			activeListView.UseWaitCursor = true;
			activeBackgroundWorker.RunWorkerAsync();
		}

		private void UpdateStatusList()
		{
			label4.Text = "Reading data...";
			statusListView.Items.Clear();
			statusListView.Groups.Clear();
			statusListView.UseWaitCursor = true;
			statusBackgroundWorker.CancelAsync();
			while (statusBackgroundWorker.IsBusy)
				System.Threading.Thread.Sleep(250);
			statusBackgroundWorker.RunWorkerAsync(comboBox1.SelectedIndex);
		}

		public ToolStrip MenuItems => null;

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x0202: // WM_LBUTTONUP
					base.DefWndProc(ref m);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
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
#if NET_35_OR_GREATER
			CorrelatedTaskEventLog log = new CorrelatedTaskEventLog(st, null, ts.TargetServer);
			foreach (var t in log)
			{
				if (((System.ComponentModel.BackgroundWorker)sender).CancellationPending)
				{
					e.Cancel = true;
					return;
				}
				list.Add(new ListViewItem(new string[] { t.TaskName, t.RunResult.ToString(), t.RunStart.ToString(), t.RunEnd.ToString(), t.TriggeredBy.ToString() }) { Tag = t });
			}
#endif
			e.Result = list.ToArray();
		}

		private void statusBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			ListViewItem[] items = e.Result as ListViewItem[];
			if (items == null)
			{
				label4.Text = "No results";
				return;
			}
			ListViewGroup lvgroup = new ListViewGroup();
			statusListView.BeginUpdate();
			int running = 0, succeeded = 0, stopped = 0, failed = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (lvgroup.Header != items[i].Text)
				{
					lvgroup = new ListViewGroup(items[i].Text);
					statusListView.Groups.Add(lvgroup);
					lvgroup.SetCollapsible(true);
				}
				items[i].Group = lvgroup;
				statusListView.Items.Add(items[i]);
#if NET_35_OR_GREATER
				switch (((CorrelatedTaskEvent)items[i].Tag).RunResult)
				{
					case CorrelatedTaskEvent.Status.StillRunning:
						running++;
						break;
					case CorrelatedTaskEvent.Status.Success:
						succeeded++;
						break;
					case CorrelatedTaskEvent.Status.Failure:
						failed++;
						break;
					case CorrelatedTaskEvent.Status.Terminated:
						stopped++;
						break;
					default:
						break;
				}
#endif
			}
			statusListView.EndUpdate();
			statusListView.UseWaitCursor = false;
			label4.Text = string.Format(label4.Tag.ToString(), items.Length, running, succeeded, stopped, failed);
		}

		private void activeBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			using (TaskService lts = new TaskService(ts.TargetServer, ts.UserName, ts.UserAccountDomain, ts.UserPassword, ts.HighestSupportedVersion.Minor == 1))
			{
				List<ListViewItem> list = new List<ListViewItem>();
				foreach (var t in lts.FindAllTasks(x => x.IsActive))
					try
					{
						DateTime next = t.NextRunTime;
						list.Add(new ListViewItem(new string[] { t.Name, next == DateTime.MinValue ? "" : next.ToString("G"), t.Definition.Triggers.ToString(), t.Folder.Path }) { Tag = t });
					}
					catch
					{
						System.Diagnostics.Debug.WriteLine($"Unable to insert task '{t.Path}' into active task list.");
					}
				list.Sort(ActiveTaskSorter);
				e.Result = list.ToArray();
			}
		}

		private static int ActiveTaskSorter(ListViewItem al, ListViewItem bl)
		{
			Task a = al.Tag as Task, b = bl.Tag as Task;
			DateTime an = DateTime.MinValue, bn = DateTime.MinValue;
			bool ax = a == null || (an = a.NextRunTime) == DateTime.MinValue;
			bool bx = b == null || (bn = b.NextRunTime) == DateTime.MinValue;
			if (ax && !bx)
				return -1;
			if (!ax && bx)
				return 1;
			if (ax && bx)
				return string.Compare(a?.Name, b?.Name, StringComparison.CurrentCulture);
			return DateTime.Compare(an, bn);
		}

		private void activeBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			activeListView.UseWaitCursor = false;
			activeListView.Items.AddRange(e.Result as ListViewItem[]);
			label6.Text = string.Format(label6.Tag.ToString(), activeListView.Items.Count);
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ts != null)
				UpdateStatusList();
		}
	}
}
