using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskService
{
	public partial class TSMMCMockup : Form
	{
		private Task selTask;

		public TSMMCMockup()
		{
			InitializeComponent();
		}

		private void taskListView1_TaskSelected(object sender, Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs e)
		{
			if (e.Task == null)
			{
				taskPropertiesControl1.Hide();
				selTask = null;
			}
			else
			{
				taskPropertiesControl1.Show();
				taskPropertiesControl1.Initialize(e.Task);
				selTask = e.Task;
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null)
			{
				taskListView1.Tasks = null;
				taskListView1_TaskSelected(null, TaskListView.TaskSelectedEventArgs.Empty);
			}
			else
			{
				taskListView1.Tasks = ((TaskFolder)e.Node.Tag).Tasks;
				if (taskListView1.Tasks.Count > 0)
					taskListView1.SelectedIndex = 0;
				else
					taskListView1_TaskSelected(null, TaskListView.TaskSelectedEventArgs.Empty);
			}
		}

		private void TSMMCMockup_Load(object sender, EventArgs e)
		{
			treeView1.Nodes.Clear();
			TreeNode n = treeView1.Nodes.Add(string.Format("Task Scheduler ({0})", taskService.TargetServer == null || taskService.TargetServer.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) ? "Local" : taskService.TargetServer));
			TreeNode p = n.Nodes.Add("Task Scheduler Library");
			p.Tag = taskService.RootFolder;
			LoadChildren(p);
		}

		private void LoadChildren(TreeNode p)
		{
			foreach (var item in ((TaskFolder)p.Tag).SubFolders)
			{
				TreeNode n = p.Nodes.Add(item.Name);
				n.Tag = item;
				LoadChildren(n);
			}
		}

		private void connectToAnotherComputerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (taskServiceConnectDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				TSMMCMockup_Load(this, EventArgs.Empty);
			}
		}

		private void createBasicTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			taskSchedulerWizard1.ShowDialog(this);
		}

		private void createTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			taskEditDialog1.Initialize(taskService);
			taskEditDialog1.ShowDialog(this);
		}

		private void importTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					TaskDefinition td = taskService.NewTask();
					td.XmlText = System.IO.File.ReadAllText(openFileDialog1.FileName);
					taskEditDialog1.Initialize(taskService, td);
					taskEditDialog1.TaskName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
					taskEditDialog1.ShowDialog(this);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex.ToString());
				}
			}
		}

		private void displayAllRunningTasksToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void runMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				selTask.Run();
		}

		private void propMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
			{
				taskEditDialog1.Initialize(selTask);
				taskEditDialog1.ShowDialog(this);
			}
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				taskService.GetFolder(System.IO.Path.GetDirectoryName(selTask.Path)).DeleteTask(selTask.Name);
		}

		private void exportTaskToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
					System.IO.File.WriteAllText(saveFileDialog1.FileName, selTask.Xml);
		}
	}
}
