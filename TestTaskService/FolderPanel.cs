using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System;

namespace TestTaskService
{
	public partial class FolderPanel : UserControl, ISupportTasks
	{
		private Task selTask;
		private bool itemMenuMerged = false;

		public FolderPanel()
		{
			InitializeComponent();
			InitMenus();
		}

		private void InitMenus()
		{
			imageList1.Images.Add(Properties.Resources.NewFolder);
			imageList1.Images.Add(Properties.Resources.DeleteTask);
			imageList1.Images.Add(Properties.Resources.RunNow);
			imageList1.Images.Add(Properties.Resources.EndTask);
			imageList1.Images.Add(Properties.Resources.Disable);
			imageList1.Images.Add(Properties.Resources.statusUnknown);
			imageList1.Images.Add(Properties.Resources.Properties);
			imageList1.Images.Add(Properties.Resources.DeleteTask);

			folderMenu.ImageList = imageList1;
			newFolderToolStripMenuItem.ImageIndex = 0;
			deleteFolderToolStripMenuItem.ImageIndex = 1;

			itemMenu.ImageList = imageList1;
			runToolStripMenuItem.ImageIndex = 2;
			endToolStripMenuItem.ImageIndex = 3;
			disableToolStripMenuItem.ImageIndex = 4;
			exportToolStripMenuItem.ImageIndex = 5;
			propertiesToolStripMenuItem.ImageIndex = 6;
			deleteToolStripMenuItem.ImageIndex = 7;
		}

		public TaskCollection Tasks
		{
			get { return TaskListView.Tasks; } 
			set
			{
				this.TaskListView.Tasks = value;
				if (value.Count > 0)
					TaskListView.SelectedIndex = 0;
				else
					taskListView_TaskSelected(null, TaskListView.TaskSelectedEventArgs.Empty);
			}
		}

		private void taskListView_TaskSelected(object sender, Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs e)
		{
			if (e.Task == null)
			{
				TaskPropertiesControl.Hide();
				selTask = null;
			}
			else
			{
				TaskPropertiesControl.Show();
				TaskPropertiesControl.Initialize(e.Task);
				selTask = e.Task;
			}
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				TaskService.GetFolder(System.IO.Path.GetDirectoryName(selTask.Path)).DeleteTask(selTask.Name);
		}

		private void endToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selTask != null && MessageBox.Show("Do you want to end all instances of this task?", "Task Scheduler", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				selTask.Stop();
		}

		private void exportTaskMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
					selTask.Export(saveFileDialog1.FileName);
		}

		private void propMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
			{
				taskEditDialog1.Initialize(selTask);
				taskEditDialog1.ShowDialog(this);
			}
		}

		private void runMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
				selTask.Run();
		}

		public TaskService TaskService { get; set; }

		public ContextMenuStrip MenuItems
		{
			get
			{
				if (!itemMenuMerged && selTask != null)
				{
					ToolStripManager.Merge(folderMenu, itemMenu);
					itemMenuMerged = true;
				}
				else if (itemMenuMerged && selTask == null)
				{
					ToolStripManager.RevertMerge(folderMenu, itemMenu);
					itemMenuMerged = false;
				}
				return folderMenu;
			}
		}
	}
}
