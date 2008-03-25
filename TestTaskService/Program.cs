using System;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskService
{
	class Program
	{
		static void Main(string[] args)
		{
			TaskService ts = new TaskService();
			Version ver = ts.HighestSupportedVersion;
			bool newVer = (ver == new Version(1, 2));
			Console.WriteLine("Highest version: " + ver);
			Console.WriteLine("Server: {0} ({1})", ts.TargetServer, ts.Connected ? "Connected" : "Disconnected");

			Console.WriteLine("Running tasks:");
			foreach (RunningTask rt in ts.GetRunningTasks(true))
			{
				Console.WriteLine("+ {0}, {1} ({2})", rt.Name, rt.Path, rt.State);
				if (ver.Minor > 0)
					Console.WriteLine("  Current Action: " + rt.CurrentAction);
			}
			Console.WriteLine();

			TaskFolder tf = ts.GetFolder("\\");
			Console.WriteLine("Root folder tasks ({0}):", tf.Tasks.Count);
			foreach (Task t in tf.Tasks)
			{
				Console.WriteLine("+ {0}, {1} ({2})", t.Name, t.Path, t.State);
				foreach (Trigger trg in t.Definition.Triggers)
					Console.WriteLine(" + {0}", trg.Id);
			}
			Console.WriteLine();

			TaskFolderCollection tfs = tf.SubFolders;
			if (tfs.Count > 0)
			{
				Console.WriteLine("Sub folders:");
				foreach (TaskFolder sf in tfs)
					Console.WriteLine("+ {0}", sf.Path);
				Console.WriteLine();
			}
			try
			{
				TaskFolder sub = tf.SubFolders["Microsoft"];
				Console.WriteLine("Subfolder path: " + sub.Path);
			}
			catch (NotSupportedException nse) { }
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			
			TaskDefinition td = ts.NewTask();
			td.RegistrationInfo.Author = "dahall";
			td.RegistrationInfo.Description = "Does something";
			td.Principal.UserId = "AMERICAS\\dahall";
			td.Principal.LogonType = TaskLogonType.InteractiveToken;
			td.Settings.ExecutionTimeLimit = TimeSpan.FromHours(1);
			td.Settings.IdleSettings.IdleDuration = TimeSpan.FromMinutes(20);
			td.Settings.IdleSettings.WaitTimeout = TimeSpan.FromMinutes(10);
			if (newVer)
				td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromMinutes(1);

			TimeTrigger tTrigger = (TimeTrigger)td.Triggers.AddNew(TaskTriggerType.Time);
			tTrigger.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1);
			tTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(7);
			tTrigger.Enabled = true;
			
			ExecAction action = (ExecAction)td.Actions.AddNew(TaskActionType.Execute);
			action.Path = "notepad.exe";
			action.Arguments = "c:\\pdanetbt.log";
			if (newVer)
			{
				ShowMessageAction showMsg = (ShowMessageAction)td.Actions.AddNew(TaskActionType.ShowMessage);
				showMsg.MessageBody = "Running Notepad";
			}

			Task runningTask = tf.RegisterTaskDefinition(@"Test", td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken, null);
			Console.WriteLine("New task will run at " + runningTask.NextRunTime);

			Console.ReadKey(false);
			tf.DeleteTask("Test");
		}
	}
}
