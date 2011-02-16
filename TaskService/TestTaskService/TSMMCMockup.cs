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
		public TSMMCMockup()
		{
			InitializeComponent();
		}

		private void taskListView1_TaskSelected(object sender, Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs e)
		{
			if (e.Task == null)
				taskPropertiesControl1.Hide();
			else
			{
				taskPropertiesControl1.Show();
				taskPropertiesControl1.Initialize(e.Task);
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null)
				taskListView1.Tasks = null;
			else
				taskListView1.Tasks = ((TaskFolder)e.Node.Tag).Tasks;
		}

		private void TSMMCMockup_Load(object sender, EventArgs e)
		{
			TreeNode n = treeView1.Nodes.Add("Task Scheduler (Local)");
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
	}
}
