using System;
using System.Windows.Forms;

namespace TaskSchedulerMockup
{
	public partial class TaskEventViewer : Form
	{
		public TaskEventViewer()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			taskEventWatcher1.Enabled = false;
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!taskEventWatcher1.Enabled)
			{
				taskEventWatcher1.BeginInit();

				taskEventWatcher1.Folder = System.IO.Path.GetDirectoryName(textBox1.Text);
				taskEventWatcher1.Filter.TaskName = System.IO.Path.GetFileName(textBox1.Text);
				taskEventWatcher1.IncludeSubfolders = checkBox1.Checked;

				if (textBox2.TextLength > 0)
				{
					var s = textBox2.Text.Replace(" ", "").Split(',');
					taskEventWatcher1.Filter.EventLevels = Array.ConvertAll(s, i => Convert.ToInt32(i));
				}
				else
					taskEventWatcher1.Filter.EventLevels = null;

				if (textBox3.TextLength > 0)
				{
					var s = textBox3.Text.Replace(" ", "").Split(',');
					taskEventWatcher1.Filter.EventIds = Array.ConvertAll(s, i => Convert.ToInt32(i));
				}
				else
					taskEventWatcher1.Filter.EventIds = null;

				taskEventWatcher1.Enabled = true;
				taskEventWatcher1.EndInit();
			}
			else
			{
				taskEventWatcher1.Enabled = false;
				listView1.Items.Clear();
			}
			progressBar1.Visible = taskEventWatcher1.Enabled;
			button2.Text = taskEventWatcher1.Enabled ? "Stop" : "Start";
		}

		private void Watcher_Changed(object sender, Microsoft.Win32.TaskScheduler.TaskEventArgs e)
		{
			if (listView1.InvokeRequired)
				listView1.BeginInvoke(new EventHandler<Microsoft.Win32.TaskScheduler.TaskEventArgs>(Watcher_Changed), sender, e);
			else
				listView1.Items.Add(new ListViewItem(new string[] { e.TaskPath, e.TaskEvent.StandardEventId.ToString(), e.TaskEvent.ToString()}));
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			var fn = System.IO.Path.GetFileName(textBox1.Text);
			var wild = (fn.IndexOfAny(new char[] { '?', '*' }) != -1);
			checkBox1.Enabled = checkBox1.Checked = wild;
		}
	}
}
