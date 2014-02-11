using Microsoft.Win32.TaskScheduler;
using System;
using System.Windows.Forms;

namespace SecurityEditor
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//using (TaskService ts = new TaskService())
			{
				var dlg = new SecurityPropertiesDialog();
				//dlg.Initialize(ts.AddTask("Test", new TimeTrigger(DateTime.Now.AddDays(1)), new ExecAction("notepad.exe")));
				dlg.Initialize(new System.IO.FileInfo(@"C:\RAT2Llog.txt"), false);
				Application.Run(dlg);
				//ts.RootFolder.DeleteTask("Test");
			}
		}
	}
}
