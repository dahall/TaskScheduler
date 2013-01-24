using System;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace SecurityEditor
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TaskSDDLEditDialog() { TaskName = "Test", SecurityDescriptorSddlForm = "D:P(A;;FA;;;BA)(A;;FRFW;;;SY)(A;;FRFX;;;LS)" });
		}
	}
}
