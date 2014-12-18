using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class FolderPanel : UserControl, ISupportTasks
	{
		private Task selTask;

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

			itemMenu.ImageList = itemMenuStrip.ImageList = imageList1;
			runMenuItem.ImageIndex = runMenuItem2.ImageIndex = 2;
			endMenuItem.ImageIndex = endMenuItem2.ImageIndex = 3;
			disableMenuItem.ImageIndex = disableMenuItem2.ImageIndex = 4;
			exportMenuItem.ImageIndex = exportMenuItem2.ImageIndex = 5;
			propertiesMenuItem.ImageIndex = propertiesMenuItem2.ImageIndex = 6;
			deleteMenuItem.ImageIndex = deleteMenuItem2.ImageIndex = 7;
		}

		public TaskCollection Tasks
		{
			get { return TaskListView.Tasks; } 
			set
			{
				this.TaskListView.Tasks = value;
				if (value.Count > 0)
					this.TaskListView.SelectedIndex = 0;
				else
					taskListView_TaskSelected(null, Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs.Empty);
			}
		}

		private void taskListView_TaskSelected(object sender, Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs e)
		{
			if (itemMenuStrip.Enabled != (e.Task != null))
				itemMenuStrip.Enabled = (e.Task != null);
			bool hasValidTask = true;
			try { var d = e.Task.Definition; } catch { hasValidTask = false; }
			if (!hasValidTask)
			{
				TaskPropertiesControl.Hide();
				selTask = null;
			}
			else
			{
				TaskPropertiesControl.Show();
				TaskPropertiesControl.Initialize(e.Task);
				selTask = e.Task;

				endMenuItem.Enabled = endMenuItem2.Enabled = (selTask.State == TaskState.Running);
				disableMenuItem.Enabled = disableMenuItem2.Enabled = (selTask.Enabled);
			}
		}

		private void deleteMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
			{
				try
				{
					TaskService.GetFolder(System.IO.Path.GetDirectoryName(selTask.Path)).DeleteTask(selTask.Name);
				}
				catch (System.IO.FileNotFoundException)
				{
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				TaskListView.Refresh();
			}
		}

		private void disableMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null)
			{
				selTask.Enabled = !selTask.Enabled;
				TaskListView.Refresh();
			}
		}

		private void endMenu_Click(object sender, EventArgs e)
		{
			if (selTask != null && MessageBox.Show("Do you want to end all instances of this task?", "Task Scheduler", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				selTask.Stop();
		}

		private void exportMenu_Click(object sender, EventArgs e)
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

		public ToolStrip MenuItems
		{
			get { return itemMenuStrip; }
		}

		private void TaskListView_DoubleClick(object sender, EventArgs e)
		{
			propMenu_Click(sender, e);
		}
	}
}
