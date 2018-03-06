using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Control that will display the run times for a provided task.</summary>
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskControl")]
	public partial class TaskRunTimesControl : UserControl, ISupportInitialize
	{
		private bool initializing = false;
		private bool isTemp = false;
		private Task task;

		/// <summary>Initializes a new instance of the <see cref="TaskRunTimesControl"/> class.</summary>
		public TaskRunTimesControl()
		{
			initializing = true;
			InitializeComponent();
			initializing = false;
		}

		/// <summary>Gets or sets the end date.</summary>
		/// <value>The end date.</value>
		[Category("Data")]
		public DateTime EndDate
		{
			get => endDatePicker.Value;
			set => endDatePicker.Value = value;
		}

		/// <summary>Gets or sets the start date.</summary>
		/// <value>The start date.</value>
		[Category("Data")]
		public DateTime StartDate
		{
			get => startDatePicker.Value;
			set => startDatePicker.Value = value;
		}

		/// <summary>Gets or sets the task.</summary>
		/// <value>The task.</value>
		[Browsable(false), DefaultValue((Task)null)]
		public Task Task
		{
			get => task;
			set
			{
				task = value;
				Text = task == null ? null : string.Format(EditorProperties.Resources.TaskRunTimesDialogTitle, value.Name);
				isTemp = task == null ? false : task.Name.StartsWith(TaskPropertiesControl.runTimesTempTaskPrefix) && task.Name.Length == (TaskPropertiesControl.runTimesTempTaskPrefix.Length + Guid.NewGuid().ToString().Length);
				if (!initializing)
					Fetch();
			}
		}

		/// <summary>Signals the object that initialization is starting.</summary>
		public void BeginInit() => initializing = true;

		/// <summary>Signals the object that initialization is complete.</summary>
		public void EndInit()
		{
			initializing = false;
			Fetch();
		}

		/// <summary>Initializes an instance of the <see cref="TaskRunTimesDialog"/> class.</summary>
		/// <param name="task">The task to display.</param>
		/// <param name="startDate">The date to begin looking for run times.</param>
		/// <param name="endDate">The date to end looking for run times.</param>
		public void Initialize(Task task = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			BeginInit();
			if (startDate.HasValue)
				StartDate = startDate.Value;
			if (endDate.HasValue)
				EndDate = endDate.Value;
			Task = task;
			EndInit();
		}

		internal bool ShouldSerializeEndDate() => endDatePicker.ShouldSerializeValue();

		internal bool ShouldSerializeStartDate() => startDatePicker.ShouldSerializeValue();

		private void dateValueChanged(object sender, EventArgs e)
		{
			if (!initializing)
				Fetch();
		}

		private void Fetch()
		{
			if (task == null)
				listBox1.DataSource = null;
			else
			{
				if (isTemp) task.Enabled = true;
				listBox1.DataSource = task.GetRunTimes(StartDate, EndDate);
				if (isTemp) task.Enabled = false;
			}
		}
	}
}