using Microsoft.Win32.TaskScheduler;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestTaskService
{
	public interface ISupportTasks
	{
		TaskService TaskService { get; set; }
		ToolStrip MenuItems { get; }
	}

	public partial class TSMMCMockup : Form
	{
		private FolderPanel folderPanel;
		private HomePanel homePanel;
		private Control curPanel;

		public TSMMCMockup()
		{
			InitializeComponent();
			this.Icon = Properties.Resources.TaskScheduler;
		}

		private void SetActionMenu(ToolStrip menuItems)
		{
			if (menuItems == null)
			{
				itemPanel.Hide();
			}
			else
			{
				if (!itemPanel.DetailArea.Contains(menuItems))
				{
					itemPanel.DetailArea.Controls.Add(menuItems);
					menuItems.Show();
				}
				itemPanel.Show();
			}
		}

		private void ShowPanel(Control panel)
		{
			if (curPanel != null)
				curPanel.Hide();
			curPanel = panel;
			if (panel is ISupportTasks)
			{
				((ISupportTasks)panel).TaskService = this.TaskService;
				SetActionMenu(((ISupportTasks)panel).MenuItems);
			}
			panel.Show();
		}

		private void ShowHome()
		{
			if (homePanel == null)
			{
				homePanel = new HomePanel();
				splitContainer1.Panel2.Controls.Add(homePanel);
				homePanel.Dock = DockStyle.Fill;
			}
			ShowPanel(homePanel);
		}

		private void ShowFolder(TaskFolder folder)
		{
			if (folderPanel == null)
			{
				folderPanel = new FolderPanel();
				splitContainer1.Panel2.Controls.Add(folderPanel);
				folderPanel.Dock = DockStyle.Fill;
			}
			folderPanel.Tasks = folder.Tasks;
			ShowPanel(folderPanel);
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null)
				ShowHome();
			else
				ShowFolder(e.Node.Tag as TaskFolder);
			newFolderMenuItem.Enabled = (e.Node.Tag != null);
			delFolderMenuItem.Enabled = (e.Node.Parent != null && e.Node.Parent.Tag != null);
		}

		private void TSMMCMockup_Load(object sender, EventArgs e)
		{
			imageList1.Images.Add(new Bitmap(Properties.Resources.empty, 16, 16), System.Drawing.Color.FromArgb(0xff, 0, 0xff));
			imageList1.Images.Add(new Icon(Properties.Resources.Properties, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.TaskLibraryRootNode, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.Folder_16, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.ScheduledTaskWizard, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.CreateNewTask, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.ReviewRunningTasks, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.AdminLog, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.NewFolder, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.Refresh, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.RunNow, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.EndTask, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.Disable, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.DeleteTask, 16, 16));
			imageList1.Images.Add(new Icon(Properties.Resources.Help, 16, 16));

			libraryMenuStrip.ImageList = imageList1;
			connectToAnotherComputerToolStripMenuItem.ImageIndex = 0;
			createBasicTaskMenuItem.ImageIndex = 4;
			createTaskMenuItem.ImageIndex = 5;
			importTaskMenuItem.ImageIndex = 0;
			displayAllRunningTasksMenuItem.ImageIndex = 6;
			newFolderMenuItem.ImageIndex = 8;
			delFolderMenuItem.ImageIndex = 11;
			refreshMenuItem.ImageIndex = 9;

			RefreshList();
		}

		private void RefreshList()
		{
			treeView1.Nodes.Clear();
			TreeNode n = treeView1.Nodes.Add(null, string.Format("Task Scheduler ({0})", TaskService.TargetServer == null || TaskService.TargetServer.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) ? "Local" : TaskService.TargetServer), 1, 1);
			TreeNode p = n.Nodes.Add(null, "Task Scheduler Library", 2, 2);
			p.Tag = TaskService.RootFolder;
			n.Expand();
			LoadChildren(p);
			treeView1.SelectedNode = n;
		}

		private void LoadChildren(TreeNode p)
		{
			foreach (var item in ((TaskFolder)p.Tag).SubFolders)
			{
				TreeNode n = p.Nodes.Add(null, item.Name, 3, 3);
				n.Tag = item;
				LoadChildren(n);
			}
		}

		private void connectToAnotherComputerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (taskServiceConnectDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				RefreshList();
		}

		private void createBasicTaskMenuItem_Click(object sender, EventArgs e)
		{
			taskSchedulerWizard1.ShowDialog(this);
		}

		private void createTaskMenuItem_Click(object sender, EventArgs e)
		{
			taskEditDialog1.Initialize(TaskService);
			taskEditDialog1.ShowDialog(this);
		}

		private void importTaskMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					TaskDefinition td = TaskService.NewTaskFromFile(openFileDialog1.FileName);
					taskEditDialog1.Initialize(TaskService, td);
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
			RunningTasksDlg dlg = new RunningTasksDlg(TaskService);
			dlg.ShowDialog(this);
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RefreshList();
		}

		private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null)
			{
				NewFolderDlg dlg = new NewFolderDlg();
				if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					((TaskFolder)treeView1.SelectedNode.Tag).CreateFolder(dlg.FolderName); // Create folder under currently selected folder
					RefreshList();
				}
			}
		}

		private void delFolderMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent.Tag != null)
			{
				if (MessageBox.Show(this, "Do you want to delete this task folder?", "Task Scheduler", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
				{
					((TaskFolder)treeView1.SelectedNode.Parent.Tag).DeleteFolder(((TaskFolder)treeView1.SelectedNode.Tag).Name);
					RefreshList();
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
