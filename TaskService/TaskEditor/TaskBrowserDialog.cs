using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// A dialog for selecting tasks or task folders.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog allowing the browsing of all tasks on the specified machine."),
	Designer(typeof(Design.TaskServiceComponentDesigner)), DesignTimeVisible(true), DefaultProperty("TaskService")]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	public partial class TaskBrowserDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private bool internalSetTS = false;
		private string path;
		private Type pathType = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskBrowserDialog"/> class.
		/// </summary>
		public TaskBrowserDialog()
		{
			InitializeComponent();
			smallImages.Images.Add(new Icon(EditorProperties.Resources.tsFolderClosed, 16, 16));
			smallImages.Images.Add(new Icon(EditorProperties.Resources.folder, 16, 16));
			smallImages.Images.Add(new Icon(EditorProperties.Resources.ts, 16, 16));
			AllowFolderSelection = false;
			ShowTasks = true;
		}

		/// <summary>
		/// Gets or sets a value indicating whether folders may be selected.
		/// </summary>
		/// <value>
		/// <c>true</c> if folders may be selected; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Task Browsing"), Description("Allows task folders to be selected.")]
		public bool AllowFolderSelection { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[Category("Task Browsing"), Description("The string that is displayed above the tree view control in the dialog.")]
		public string Description
		{
			get { return descLabel.Text; }
			set { descLabel.Text = value; }
		}

		/// <summary>
		/// Gets or sets the selected item path. The path may reference either a <see cref="Task"/> or a <see cref="TaskFolder"/>.
		/// </summary>
		/// <value>The selected item path.</value>
		[DefaultValue(null), Category("Task Browsing"), Description("The path to the task or folder first selected in the dialog or the last one selected by the user.")]
		public string SelectedPath
		{
			get { return path; }
			set
			{
				if (!string.Equals(path, value, StringComparison.InvariantCultureIgnoreCase))
				{
					path = value;
					if (treeView.Nodes.Count > 0)
						SelectNodeByKey(path);
				}
			}
		}

		/// <summary>
		/// Gets the type of the selected path. Value is <c>null</c> if unresolved or not selected.
		/// </summary>
		/// <value>
		/// The type of the selected path.
		/// </value>
		[Browsable(false)]
		public Type SelectedPathType => pathType;

		/// <summary>
		/// Gets or sets a value indicating whether to show tasks along with the folders.
		/// </summary>
		/// <value>
		///   <c>true</c> if tasks are to be shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Task Browsing"), Description("Shows both tasks and folders within the tree view.")]
		public bool ShowTasks { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="TaskService"/>.
		/// </summary>
		/// <value>The task service.</value>
		[DefaultValue(null), Category("Task Browsing"), Description("The TaskService for this dialog")]
		public TaskService TaskService { get; set; }

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (treeView.SelectedNode != null && treeView.SelectedNode.Tag != null)
			{
				if (treeView.SelectedNode.Tag is Task)
					SelectedPath = (treeView.SelectedNode.Tag as Task).Path;
				else
					SelectedPath = (treeView.SelectedNode.Tag as TaskFolder).Path;
				pathType = treeView.SelectedNode.Tag.GetType();
			}
			else
			{
				SelectedPath = null;
				pathType = null;
			}
			Close();
		}

		private void RefreshList()
		{
			string curPath = treeView.SelectedNode == null ? SelectedPath : treeView.SelectedNode.Name;
			TaskService.AllowReadOnlyTasks = true;
			treeView.Nodes.Clear();
			loadingLabel.Visible = true;
			treeView.UseWaitCursor = true;
			treeView.BeginUpdate();
			TreeNode n = treeView.Nodes.Add(@"\", string.Format("Task Scheduler Library ({0})", TaskService.TargetServer == null || TaskService.TargetServer.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) ? "Local" : TaskService.TargetServer), 0, 0);
			n.Tag = TaskService.RootFolder;
			n.Expand();
			LoadChildren(n);
			loadingLabel.Visible = false;
			treeView.EndUpdate();
			treeView.UseWaitCursor = false;
			if (!string.IsNullOrEmpty(curPath))
				SelectNodeByKey(curPath);
			treeView.Focus();
		}

		private TreeNode SelectNodeByKey(string key)
		{
			TreeNode ret = null;
			var nodes = treeView.Nodes.Find(key, true);
			if (nodes != null && nodes.Length > 0)
			{
				ret = nodes[0];
				treeView.SelectedNode = ret;
			}
			return ret;
		}

		private void LoadChildren(TreeNode p)
		{
			foreach (var item in ((TaskFolder)p.Tag).SubFolders)
			{
				TreeNode n = p.Nodes.Add(item.Path, item.Name, 1, 1);
				n.Tag = item;
				LoadChildren(n);
			}
			if (ShowTasks)
			{
				foreach (var t in ((TaskFolder)p.Tag).Tasks)
				{
					TreeNode tn = p.Nodes.Add(t.Path, t.Name, 2, 2);
					tn.Tag = t;
				}
			}
		}

		private void ResetDescription()
		{
			var rm = new System.Resources.ResourceManager(GetType());
			Description = rm.GetString("descLabel.Text");
		}

		private bool ShouldSerializeDescription()
		{
			var rm = new System.Resources.ResourceManager(GetType());
			return !string.Equals(Description, rm.GetString("descLabel.Text"));
		}

		private bool ShouldSerializeTaskService() => !internalSetTS;

		private void TaskBrowserDialog_Load(object sender, EventArgs e)
		{
			if (TaskService == null)
			{
				TaskService = new TaskService();
				internalSetTS = true;
			}
			RefreshList();
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			okButton.Enabled = treeView.SelectedNode != null;
		}

		private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (!AllowFolderSelection && e.Node.Tag is TaskFolder)
				e.Cancel = true;
		}

		private void treeView_Resize(object sender, EventArgs e)
		{
			loadingLabel.SetBounds(treeView.Left + ((treeView.Width - loadingLabel.Width) / 2),
				treeView.Top + ((treeView.Height - loadingLabel.Height) / 2),
				loadingLabel.Width, loadingLabel.Height);
		}
	}
}