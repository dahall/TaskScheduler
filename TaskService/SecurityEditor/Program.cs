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
				var dlg = new AdvancedSecuritySettingsDialog();
				//var dlg = new SecurityPropertiesDialog();
				//dlg.Initialize(ts.GetTask(@"Microsoft\Office\Office 15 Subscription Heartbeat"), false);
				dlg.Initialize(new System.IO.FileInfo(@"C:\RAT2Llog.txt"), false);
				//dlg.Initialize(new System.IO.DirectoryInfo(@"C:\Windows"), false);
				//dlg.Initialize(Microsoft.Win32.Registry.LocalMachine, false);
				//dlg.Initialize(System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew(@"RAT2Llog.txt", 1024), false);
				Application.Run(dlg);
				//ts.RootFolder.DeleteTask("Test");
			}
		}
	}
}
