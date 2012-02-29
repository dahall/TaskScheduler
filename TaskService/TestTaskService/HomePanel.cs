using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System;

namespace TestTaskService
{
	public partial class HomePanel : UserControl, ISupportTasks
	{
		public HomePanel()
		{
			InitializeComponent();
		}

		public TaskService TaskService { get; set; }

		public ContextMenuStrip MenuItems
		{
			get { return new ContextMenuStrip(); }
		}
	}
}
