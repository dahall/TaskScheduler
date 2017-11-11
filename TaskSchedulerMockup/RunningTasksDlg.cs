using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class RunningTasksDlg : Form
	{
		TaskService ts;

		public RunningTasksDlg(TaskService ts)
		{
			InitializeComponent();
			this.ts = ts;
		}

		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			endTaskBtn.Enabled = (listView1.SelectedIndices.Count > 0);
		}

		private void endTaskBtn_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedIndices.Count > 0)
			{
				((RunningTask)listView1.SelectedItems[0].Tag).Stop();
				Initialize();
			}
		}

		private void refreshBtn_Click(object sender, EventArgs e)
		{
			Initialize();
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void RunningTasksDlg_Load(object sender, EventArgs e)
		{
			Initialize();
		}

		private void Initialize()
		{
			listView1.Items.Clear();
			foreach (var t in ts.GetRunningTasks())
			{
				TimeSpan2 dur = DateTime.Now - t.LastRunTime;
				listView1.Items.Add(new ListViewItem(new string[] { t.Name, t.LastRunTime.ToString("G"), dur.ToString("[%d_@d],[%h_@h],[%m_@m],[%s_@s]"), t.CurrentAction, t.Path }) { Tag = t });
			}
		}
	}
}
