using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class TaskRunTimesDialog : Form
	{
		private Task task;
		private bool initializing = false;

		public TaskRunTimesDialog(Task task, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			initializing = true;
			this.StartDate = startDate;
			this.EndDate = endDate;
			this.Task = task;
			initializing = false;
		}

		public Task Task
		{
			get
			{
				return task;
			}
			set
			{
				task = value;
				this.Text = string.Format(Properties.Resources.TaskRunTimesDialogTitle, value.Name);
				Fetch();
			}
		}

		public DateTime StartDate
		{
			get { return startDatePicker.Value; }
			set { startDatePicker.Value = value; }
		}

		public DateTime EndDate
		{
			get { return endDatePicker.Value; }
			set { endDatePicker.Value = value; }
		}

		private void Fetch()
		{
			listBox1.Items.Clear();
			if (task != null)
			{
				foreach (var dt in task.GetRunTimes(this.StartDate, this.EndDate))
					listBox1.Items.Add(string.Format("{0:F}", dt));
			}
		}

		private void dateValueChanged(object sender, EventArgs e)
		{
			if (!initializing)
				Fetch();
		}
	}
}
