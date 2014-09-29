using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class TaskOptionsEditor : Form
	{
		private Task task;
		private TaskScheduler.TaskDefinition td;
		private bool onAssignment;
		private TaskScheduler.TaskService service;
		public TaskOptionsEditor()
		{
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public void Initialize(TaskService service, TaskDefinition td = null)
		{
			this.TaskService = service;
			this.task = null;
			if (!this.IsDesignMode())
			{
				if (td == null)
					this.TaskDefinition = service.NewTask();
				else
					this.TaskDefinition = td;
			}
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			this.Task = task;
		}

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get
			{
				return task;
			}
			private set
			{
				task = value;
				if (task != null)
				{
					TaskService = task.TaskService;
					if (task.ReadOnly)
						this.Editable = false;
					TaskDefinition = task.Definition;
				}
			}
		}

		/// <summary>
		/// Gets the <see cref="TaskDefinition"/> in its edited state.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition
		{
			get { return td; }
			private set
			{
				if (service == null)
					throw new ArgumentNullException("TaskDefinition cannot be set until TaskService has been set with a valid object.");

				if (value == null)
					throw new ArgumentNullException("TaskDefinition cannot be set to null.");

				td = value;
				onAssignment = true;
			}
		}

		/// <summary>
		/// Gets the <see cref="TaskService"/> assigned at initialization.
		/// </summary>
		/// <value>The task service.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskService TaskService
		{
			get { return service; }
			private set { service = value; }
		}

		public bool Editable { get; set; }

		private void menuItem_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem)
			{
				ToolStripMenuItem item = (ToolStripMenuItem)sender;
				OptionPanels.OptionPanel panel = null;
				if (item.Tag == null || !(item.Tag is OptionPanels.OptionPanel))
				{
					switch (item.Name)
					{
						case "generalItem":
							panel = new OptionPanels.GeneralOptionPanel();
							break;
						case "triggersItem":
						case "actionsItem":
						default:
							break;
					}
				}
				if (panel != null)
				{
					item.Tag = panel;
					this.bodyPanel.Controls.Clear();
					this.bodyPanel.Controls.Add(panel);
					panel.TaskDefintion = this.TaskDefinition;
					panel.Editable = this.Editable;
				}
			}
		}
	}
}
