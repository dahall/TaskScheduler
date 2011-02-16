using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Displays a <see cref="TaskCollection"/> in a <see cref="ListView"/> control. Mimics list in MMC.
	/// </summary>
	public partial class TaskListView : UserControl
	{
		private TaskCollection coll;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskListView"/> class.
		/// </summary>
		public TaskListView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Occurs when task selected in the list.
		/// </summary>
		public event EventHandler<TaskSelectedEventArgs> TaskSelected;

		/// <summary>
		/// Gets or sets the tasks.
		/// </summary>
		/// <value>The tasks.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), RefreshProperties(RefreshProperties.All)]
		public TaskCollection Tasks
		{
			get { return coll; }
			set
			{
				coll = value;
				listView1.Items.Clear();
				foreach (var item in coll)
					listView1.Items.Add(LVIFromTask(item));
			}
		}

		/// <summary>
		/// Raises the <see cref="E:TaskSelected"/> event.
		/// </summary>
		/// <param name="e">The <see cref="Microsoft.Win32.TaskScheduler.TaskListView.TaskSelectedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnTaskSelected(TaskSelectedEventArgs e)
		{
			EventHandler<TaskSelectedEventArgs> handler = TaskSelected;
			if (handler != null)
				handler(this, e);
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			Task t = null;
			if (listView1.SelectedIndices.Count == 1)
				t = coll[listView1.SelectedItems[0].Text];
			OnTaskSelected(new TaskSelectedEventArgs(t));
		}

		private ListViewItem LVIFromTask(Task task)
		{
			bool disabled = task.State == TaskState.Disabled;
			ListViewItem lvi = new ListViewItem(new string[] {
				task.Name,
				TaskPropertiesControl.BuildEnumString(TaskPropertiesControl.taskSchedResources, "TaskState", task.State),
				task.Definition.Triggers.ToString(),
				disabled || task.NextRunTime < DateTime.Now ? string.Empty : task.NextRunTime.ToString("G"),
				task.LastRunTime == DateTime.MinValue ? Properties.Resources.Never :  task.LastRunTime.ToString("G"),
				task.LastTaskResult == 0 ? Properties.Resources.LastResultSuccessful : string.Format("(0x{0:X})", task.LastTaskResult),
				task.Definition.RegistrationInfo.Author,
				string.Empty
				}, 0);
			return lvi;
		}

		/// <summary>
		/// Event args for when a task is selected.
		/// </summary>
		public class TaskSelectedEventArgs : EventArgs
		{
			/// <summary>
			/// Empty <see cref="TaskSelectedEventArgs"/> class.
			/// </summary>
			public static new readonly TaskSelectedEventArgs Empty = new TaskSelectedEventArgs();

			/// <summary>
			/// Gets or sets the task.
			/// </summary>
			/// <value>The task.</value>
			public Task Task { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="TaskSelectedEventArgs"/> class.
			/// </summary>
			/// <param name="task">The task.</param>
			public TaskSelectedEventArgs(Task task = null)
			{
				this.Task = task;
			}
		}
	}
}