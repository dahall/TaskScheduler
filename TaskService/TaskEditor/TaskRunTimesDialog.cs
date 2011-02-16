using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that will display the run times for a provided task.
	/// </summary>
	public partial class TaskRunTimesDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskRunTimesDialog"/> class.
		/// </summary>
		/// <param name="task">The task to display.</param>
		/// <param name="startDate">The date to begin looking for run times.</param>
		/// <param name="endDate">The date to end looking for run times.</param>
		public TaskRunTimesDialog(Task task, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			taskRunTimesControl1.Initialize(task, startDate, endDate);
		}

		/// <summary>
		/// Gets or sets the task.
		/// </summary>
		/// <value>The task.</value>
		public Task Task
		{
			get { return taskRunTimesControl1.Task; }
			set { taskRunTimesControl1.Task = value; }
		}

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>The start date.</value>
		public DateTime StartDate
		{
			get { return taskRunTimesControl1.StartDate; }
			set { taskRunTimesControl1.StartDate = value; }
		}

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>The end date.</value>
		public DateTime EndDate
		{
			get { return taskRunTimesControl1.EndDate; }
			set { taskRunTimesControl1.EndDate = value; }
		}
	}
}
