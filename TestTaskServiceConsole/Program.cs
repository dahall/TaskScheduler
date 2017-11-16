using System;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskServiceConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Root folder tasks:");
			//using (var ts = new TaskService(null, forceV1: true))
			var ts = TaskService.Instance;
				foreach (var t in ts.RootFolder.EnumerateTasks())
					Console.WriteLine(t.Name);
		}
	}
}