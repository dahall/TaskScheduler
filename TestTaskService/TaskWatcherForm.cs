using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class TaskWatcherForm : Form
	{
		public TaskWatcherForm()
		{
			InitializeComponent();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			Properties.Settings.Default.Save();
		}

		private void folderCheck_CheckedChanged(object sender, EventArgs e)
		{
			bool chk = folderCheck.Checked;
			folderLabel.Enabled = folderText.Enabled = folderButton.Enabled = inclSubsCheck.Enabled = chk;
			if (!chk) folderText.Clear();
		}

		private void connectionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (taskServiceConnectDialog.ShowDialog(this) == DialogResult.OK)
				ResetWatcher();
		}

		private void ResetWatcher()
		{
			if (taskEventWatcher.Enabled)
			{
				taskEventWatcher.Enabled = false;
				taskEventWatcher.Enabled = true;
			}
		}

		private void watchButton_Click(object sender, EventArgs e)
		{
			if (taskEventWatcher.Enabled)
			{
				taskEventWatcher.Enabled = false;
				watchButton.Text = "Watch";
			}
			else
			{
				taskEventWatcher.BeginInit();
				taskEventWatcher.Folder = folderCheck.Checked ? folderText.Text : null;
				taskEventWatcher.IncludeSubfolders = folderCheck.Checked && inclSubsCheck.Checked;
				taskEventWatcher.Filter.TaskName = taskText.Text;
				taskEventWatcher.SynchronizingObject = this;
				taskEventWatcher.Filter.EventIds = idsText.TextLength == 0 ? null : StringToInts(idsText.Text);
				taskEventWatcher.Filter.EventLevels = levelsText.TextLength == 0 ? null : StringToInts(levelsText.Text);
				taskEventWatcher.Enabled = true;
				taskEventWatcher.EndInit();
				watchButton.Text = "Stop";
			}
		}

		private int[] StringToInts(string text) => Array.ConvertAll(text.Replace(" ", "").Split(','), s => int.Parse(s));

		private void taskEventWatcher_EventRecorded(object sender, Microsoft.Win32.TaskScheduler.TaskEventArgs e)
		{
			outputList.Items.Insert(0, $"{e?.TaskEvent?.EventId}: {e?.TaskEvent}");
		}

		private void folderButton_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
				folderText.Text = folderBrowserDialog.SelectedPath;
		}

		private void taskButton_Click(object sender, EventArgs e)
		{
			if (taskBrowserDialog.ShowDialog(this) == DialogResult.OK)
				taskText.Text = taskBrowserDialog.SelectedPath;
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			outputList.Items.Clear();
		}
	}
}