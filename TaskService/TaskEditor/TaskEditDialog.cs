using System.Windows.Forms;
using System.ComponentModel;
using System;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that allows tasks to be edited
	/// </summary>
	public partial class TaskEditDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEditDialog"/> class.
		/// </summary>
		public TaskEditDialog()
		{
			InitializeComponent();
			RegisterTaskOnAccept = false;
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			if (RegisterTaskOnAccept)
				this.TaskService.RootFolder.RegisterTaskDefinition(this.Task.Name, this.TaskDefinition);
			Close();
		}

		/// <summary>
		/// Gets or sets a value indicating whether to register task when Accept (Ok) button pressed.
		/// </summary>
		/// <value><c>true</c> if updated task is to be registered; otherwise, <c>false</c>.</value>
		[DefaultValue(false)]
		public bool RegisterTaskOnAccept { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskEditDialog"/> is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		public bool Editable
		{
			get { return taskPropertiesControl1.Editable; }
			set { taskPropertiesControl1.Editable = value; }
		}

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		public Task Task
		{
			get { return taskPropertiesControl1.Task; }
		}

		/// <summary>
		/// Gets the <see cref="TaskDefinition"/> in its edited state.
		/// </summary>
		/// <value>The task definition.</value>
		public TaskDefinition TaskDefinition
		{
			get { return taskPropertiesControl1.TaskDefinition; }
		}

		/// <summary>
		/// Gets the <see cref="TaskService"/> assigned at initialization.
		/// </summary>
		/// <value>The task service.</value>
		public TaskService TaskService
		{
			get { return taskPropertiesControl1.TaskService; }
		}

		private string GetServerString(TaskService service)
		{
			return service.TargetServer.Equals(Environment.MachineName, StringComparison.CurrentCultureIgnoreCase) ?
				Properties.Resources.LocalMachine : service.TargetServer;
		}

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		public void Initialize(TaskService service)
		{
			this.Text = string.Format(Properties.Resources.TaskEditDlgTitle, "New Task", GetServerString(service));
			taskPropertiesControl1.Initialize(service);
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			this.Text = string.Format(Properties.Resources.TaskEditDlgTitle, task.Name, GetServerString(task.TaskService));
			taskPropertiesControl1.Initialize(task);
		}
	}
}
