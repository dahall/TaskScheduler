using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using TestTaskService.Properties;

namespace TestTaskService
{
	public interface ISupportTasks
	{
		ToolStrip MenuItems { get; }
		TaskService TaskService { get; set; }
	}

	public partial class TSMMCMockup : Form
	{
		private Control curPanel;
		private FolderPanel folderPanel;
		private HomePanel homePanel;

		public TSMMCMockup(TaskService taskSvc)
		{
			InitializeComponent();
			Icon = Resources.TaskScheduler;
			taskService = taskSvc;
		}

		private void connectToAnotherComputerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			taskServiceConnectDialog1.TaskService = taskService;
			if (taskServiceConnectDialog1.ShowDialog(this) == DialogResult.OK)
				RefreshList();
		}

		private void createBasicTaskMenuItem_Click(object sender, EventArgs e)
		{
			taskSchedulerWizard1.Initialize(taskService);
			taskSchedulerWizard1.ShowDialog(this);
		}

		private void createTaskMenuItem_Click(object sender, EventArgs e)
		{
			taskEditDialog1.Initialize(taskService);
			taskEditDialog1.ShowDialog(this);
		}

		private void delFolderMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode?.Parent.Tag == null) return;
			if (MessageBox.Show(this, "Do you want to delete this task folder?", "Task Scheduler", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
			((TaskFolder)treeView1.SelectedNode.Parent.Tag).DeleteFolder(((TaskFolder)treeView1.SelectedNode.Tag).Name);
			RefreshList();
		}

		private void displayAllRunningTasksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dlg = new RunningTasksDlg(taskService);
			dlg.ShowDialog(this);
		}

		private void enableHistoryMenuItem_Click(object sender, EventArgs e)
		{
			EnableTaskLog(true);
		}

		private void EnableTaskLog(bool toggle = false)
		{
			try
			{
				TaskEventLog log = null;
				try { log = taskService.GetEventLog(); } catch { }
				if (log != null && toggle)
					log.Enabled = !log.Enabled;
				var logEnabled = log != null && log.Enabled;
				enableHistoryMenuItem.Visible = !logEnabled;
				disableHistoryMenuItem.Visible = logEnabled;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void importTaskMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					var td = taskService.NewTaskFromFile(openFileDialog1.FileName);
					taskEditDialog1.Initialize(taskService, td);
					taskEditDialog1.TaskName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
					taskEditDialog1.ShowDialog(this);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: " + ex);
				}
			}
		}

		private static void LoadChildren(TreeNode p)
		{
			foreach (var item in ((TaskFolder)p.Tag).SubFolders)
			{
				var n = p.Nodes.Add(null, item.Name, 3, 3);
				n.Tag = item;
				LoadChildren(n);
			}
		}

		private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView1.SelectedNode?.Tag != null)
			{
				var dlg = new NewFolderDlg();
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					((TaskFolder)treeView1.SelectedNode.Tag).CreateFolder(dlg.FolderName); // Create folder under currently selected folder
					RefreshList();
				}
			}
		}

		private void RefreshList()
		{
			taskService.AllowReadOnlyTasks = true;
			treeView1.Nodes.Clear();
			var n = treeView1.Nodes.Add(null,
				$"Task Scheduler ({(taskService.TargetServer == null || taskService.TargetServer.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) ? "Local" : taskService.TargetServer)})", 1, 1);
			var p = n.Nodes.Add(null, "Task Scheduler Library", 2, 2);
			p.Tag = taskService.RootFolder;
			n.Expand();
			if (taskService.HighestSupportedVersion > new Version(1, 1))
				LoadChildren(p);
			treeView1.SelectedNode = n;
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RefreshList();
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

		private void ShowFolder(TaskFolder folder)
		{
			if (folderPanel == null)
			{
				folderPanel = new FolderPanel();
				splitContainer1.Panel2.Controls.Add(folderPanel);
				folderPanel.Dock = DockStyle.Fill;
			}
			folderPanel.TaskFolder = folder;
			ShowPanel(folderPanel);
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

		private void ShowPanel(Control panel)
		{
			curPanel?.Hide();
			curPanel = panel;
			var tasks = panel as ISupportTasks;
			if (tasks != null)
			{
				tasks.TaskService = taskService;
				SetActionMenu(tasks.MenuItems);
			}
			panel.Show();
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null)
				ShowHome();
			else
				ShowFolder(e.Node.Tag as TaskFolder);
			newFolderMenuItem.Enabled = e.Node.Tag != null;
			delFolderMenuItem.Enabled = e.Node.Parent?.Tag != null;
		}

		private void treeView1_HandleCreated(object sender, EventArgs e)
		{
			treeView1.SetExplorerTheme();
		}

		private void TSMMCMockup_Load(object sender, EventArgs e)
		{
			imageList1.Images.Add(new Bitmap(Resources.empty, 16, 16), Color.FromArgb(0xff, 0, 0xff));
			imageList1.Images.Add(new Icon(Resources.Properties, 16, 16));
			imageList1.Images.Add(new Icon(Resources.TaskLibraryRootNode, 16, 16));
			imageList1.Images.Add(new Icon(Resources.Folder_16, 16, 16));
			imageList1.Images.Add(new Icon(Resources.ScheduledTaskWizard, 16, 16));
			imageList1.Images.Add(new Icon(Resources.CreateNewTask, 16, 16));
			imageList1.Images.Add(new Icon(Resources.ReviewRunningTasks, 16, 16));
			imageList1.Images.Add(new Icon(Resources.AdminLog, 16, 16));
			imageList1.Images.Add(new Icon(Resources.NewFolder, 16, 16));
			imageList1.Images.Add(new Icon(Resources.Refresh, 16, 16));
			imageList1.Images.Add(new Icon(Resources.RunNow, 16, 16));
			imageList1.Images.Add(new Icon(Resources.EndTask, 16, 16));
			imageList1.Images.Add(new Icon(Resources.Disable, 16, 16));
			imageList1.Images.Add(new Icon(Resources.DeleteTask, 16, 16));
			imageList1.Images.Add(new Icon(Resources.Help, 16, 16));

			libraryMenuStrip.ImageList = imageList1;
			connectToAnotherComputerToolStripMenuItem.ImageIndex = 0;
			createBasicTaskMenuItem.ImageIndex = 4;
			createTaskMenuItem.ImageIndex = 5;
			importTaskMenuItem.ImageIndex = 0;
			displayAllRunningTasksMenuItem.ImageIndex = 6;
			enableHistoryMenuItem.ImageIndex = 7;
			disableHistoryMenuItem.ImageIndex = 7;
			EnableTaskLog();
			newFolderMenuItem.ImageIndex = 8;
			delFolderMenuItem.ImageIndex = 11;
			refreshMenuItem.ImageIndex = 9;

			RefreshList();
		}
	}
}