using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that will display the run times for a provided task.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), System.Drawing.ToolboxBitmap("Dialog.bmp"),
	DesignTimeVisible(true), Description("Dialog that will display the run times for a provided task."), 
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public partial class TaskRunTimesDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskRunTimesDialog"/> class.
		/// </summary>
		public TaskRunTimesDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskRunTimesDialog"/> class.
		/// </summary>
		/// <param name="task">The task to display.</param>
		/// <param name="startDate">The date to begin looking for run times.</param>
		/// <param name="endDate">The date to end looking for run times.</param>
		public TaskRunTimesDialog(Task task, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Initialize(task, startDate, endDate);
		}

		/// <summary>
		/// Gets or sets the task.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get { return taskRunTimesControl1.Task; }
			set { taskRunTimesControl1.Task = value; }
		}

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>The start date.</value>
		[Category("Data"), Description("The date to start looking for run times.")]
		public DateTime StartDate
		{
			get { return taskRunTimesControl1.StartDate; }
			set { taskRunTimesControl1.StartDate = value; }
		}

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>The end date.</value>
		[Category("Data"), Description("The date to end looking for run times.")]
		public DateTime EndDate
		{
			get { return taskRunTimesControl1.EndDate; }
			set { taskRunTimesControl1.EndDate = value; }
		}

		/// <summary>
		/// Initializes the dialog with the specified task.
		/// </summary>
		/// <param name="task">The task.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		protected void Initialize(Task task, DateTime? startDate, DateTime? endDate)
		{
			taskRunTimesControl1.Initialize(task, startDate, endDate);
		}

		private bool ShouldSerializeEndDate()
		{
			return taskRunTimesControl1.ShouldSerializeEndDate();
		}

		private bool ShouldSerializeStartDate()
		{
			return taskRunTimesControl1.ShouldSerializeStartDate();
		}
	}
}
