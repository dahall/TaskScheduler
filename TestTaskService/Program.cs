using System;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskService
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			int init = 0;
			char test = 'L';
			if (args.Length > 0 && char.IsLetter(args[0][0]))
			{
				test = args[0].ToUpper()[0];
				init++;
			}
			string[] newArgs = new string[] { args.Length > init ? args[init] : "2", null, null, null, null };
			for (int i = init + 1; i < init + 5; i++)
				if (args.Length > i) newArgs[i] = args[i];
			switch (test)
			{
				case 'W':
					Win7Test(newArgs);
					break;
				case 'E':
					EditorTest(newArgs);
					break;
				case 'S':
					ShortTest(newArgs);
					break;
				default:
					LongTest(newArgs);
					break;
			}
		}

		static void Win7Test(string[] args)
		{
			// Get the service on the local machine
			try
			{
				using (TaskService ts = new TaskService(args[1], args[2], args[3], args[4], args[0] == "1"))
				{
					// Create a new task definition and assign properties
					TaskDefinition td = ts.NewTask();

					switch (int.Parse(args[0]))
					{
						case 2:
							td.Triggers.Add(new BootTrigger { Delay = TimeSpan.FromMinutes(10) });
							break;
						case 3:
							td.Triggers.Add(new EventTrigger("Security", "VSSAudit", null));
							break;
						case 4:
							td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.SessionUnlock));
							break;
						case 5:
							td.Triggers.Add(new WeeklyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(2), DaysOfWeek = DaysOfTheWeek.Friday });
							break;
					}

					td.Actions.Add(new ShowMessageAction("Win7 task has fired", "Task Test"));

					const string taskName = "Test";
					ts.RootFolder.RegisterTaskDefinition(taskName, td);
					ts.RootFolder.DeleteTask(taskName);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.ReadKey(false);
			}
		}

		static void EditorTest(string[] args)
		{
			// Get the service on the local machine
			try
			{
				using (TaskService ts = new TaskService(args[1], args[2], args[3], args[4], args[0] == "1"))
				{
					// Create a new task definition and assign properties
					const string taskName = "Test";
					ts.AddTask(taskName, new TimeTrigger() { StartBoundary = DateTime.Now + TimeSpan.FromHours(1), Enabled = false }, new ExecAction("notepad.exe", "c:\\test.log", "C:\\"));

					// Edit task
					Task t = ts.GetTask(taskName);
					TaskDefinition td = DisplayTask(t, true);

					// Register then show task again
					if (td != null)
					{
						ts.RootFolder.RegisterTaskDefinition(taskName, td);
						t = ts.GetTask(taskName);
						DisplayTask(t, false);
					}

					// Remove the task we just created
					ts.RootFolder.DeleteTask(taskName);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.ReadKey(false);
			}
		}

		static void ShortTest(string[] args)
		{
			// Get the service on the local machine
			try
			{
				using (TaskService ts = new TaskService(args[1], args[2], args[3], args[4], args[0] == "1"))
				{
					// Create a new task definition and assign properties
					TaskDefinition td = ts.NewTask();
					td.RegistrationInfo.Description = "Does something";
					td.Principal.LogonType = TaskLogonType.InteractiveToken;

					// Create a trigger that will fire the task at this time every other day
					DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
					dt.Repetition.Duration = TimeSpan.FromHours(4);
					dt.Repetition.Interval = TimeSpan.FromHours(1);

					td.Triggers.Add(new WeeklyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(2), DaysOfWeek = DaysOfTheWeek.Friday });

					// Create an action that will launch Notepad whenever the trigger fires
					td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

					// Register the task in the root folder
					const string taskName = "Test";
					Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td);
					System.Threading.Thread.Sleep(1000);
					Console.WriteLine("LastTime & Result: {0} ({1})", t.LastRunTime, t.LastTaskResult);

					// Retrieve the task, add a trigger and save it.
					t = ts.GetTask(taskName);
					td = t.Definition;
					td.Triggers[0].StartBoundary = DateTime.Today + TimeSpan.FromDays(7);
					ts.RootFolder.RegisterTaskDefinition(taskName, td);//, TaskCreation.Update, "SYSTEM", null, TaskLogonType.ServiceAccount, null);

					// Remove the task we just created
					Console.ReadKey(false);
					ts.RootFolder.DeleteTask(taskName);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Console.ReadKey(false);
			}
		}

		static void LongTest(string[] args)
		{
			string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			int key = int.Parse(args[0]);

			TaskService ts = new TaskService(args[1], args[2], args[3], args[4], key == 1);
			Version ver = ts.HighestSupportedVersion;
			bool newVer = (ver >= new Version(1, 2));
			Console.WriteLine("Highest version: " + ver);
			Console.WriteLine("Server: {0} ({1})", ts.TargetServer, ts.Connected ? "Connected" : "Disconnected");

			Console.WriteLine("Running tasks:");
			foreach (RunningTask rt in ts.GetRunningTasks(true))
			{
				if (rt != null)
				{
					Console.WriteLine("+ {0}, {1} ({2})", rt.Name, rt.Path, rt.State);
					if (ver.Minor > 0)
						Console.WriteLine("  Current Action: " + rt.CurrentAction);
				}
			}

			TaskFolder tf = ts.RootFolder;
			Console.WriteLine("\nRoot folder tasks ({0}):", tf.Tasks.Count);
			foreach (Task t in tf.Tasks)
			{
				try
				{
					Console.WriteLine("+ {0}, {1} ({2})", t.Name, t.Definition.RegistrationInfo.Author, t.State);
					foreach (Trigger trg in t.Definition.Triggers)
						Console.WriteLine(" + {0}", trg);
					foreach (Action act in t.Definition.Actions)
						Console.WriteLine(" = {0}", act);
				}
				catch { }
			}

			Console.WriteLine("\n***Checking folder enum***");
			TaskFolderCollection tfs = tf.SubFolders;
			if (tfs.Count > 0)
			{
				Console.WriteLine("\nSub folders:");
				try
				{
					foreach (TaskFolder sf in tfs)
						Console.WriteLine("+ {0}", sf.Path);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			if (newVer)
			{
				Console.WriteLine("\n***Checking folder retrieval***");
				try
				{
					TaskFolder sub = tf.SubFolders["Microsoft"];
					Console.WriteLine("\nSubfolder path: " + sub.Path);
				}
				catch (NotSupportedException) { }
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}

			Console.WriteLine("\n***Checking task creation***");
			try
			{
				TaskDefinition td = ts.NewTask();
				td.Data = "Your data";
				td.Principal.UserId = user;
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
				td.RegistrationInfo.Author = "dahall";
				td.RegistrationInfo.Description = "Does something";
				td.RegistrationInfo.Documentation = "Don't pretend this is real.";
				td.Settings.DisallowStartIfOnBatteries = true;
				td.Settings.Enabled = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.FromHours(2);
				td.Settings.Hidden = false;
				td.Settings.IdleSettings.IdleDuration = TimeSpan.FromMinutes(20);
				td.Settings.IdleSettings.RestartOnIdle = false;
				td.Settings.IdleSettings.StopOnIdleEnd = false;
				td.Settings.IdleSettings.WaitTimeout = TimeSpan.FromMinutes(10);
				td.Settings.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
				td.Settings.RunOnlyIfIdle = false;
				td.Settings.RunOnlyIfNetworkAvailable = false;
				td.Settings.StopIfGoingOnBatteries = true;
				if (newVer)
				{
					td.Principal.RunLevel = TaskRunLevel.Highest; //.LUA;
					//td.RegistrationInfo.SecurityDescriptorSddlForm = "O:COG:CGD::(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)";
					td.RegistrationInfo.Source = "Test App";
					td.RegistrationInfo.URI = new Uri("test://app");
					td.RegistrationInfo.Version = new Version(0, 9);
					td.Settings.AllowDemandStart = true;
					td.Settings.AllowHardTerminate = true;
					td.Settings.Compatibility = TaskCompatibility.V2;
					td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromMinutes(1);
					td.Settings.MultipleInstances = TaskInstancesPolicy.StopExisting;
					td.Settings.StartWhenAvailable = true;
					td.Settings.WakeToRun = false;
					td.Settings.RestartCount = 5;
					td.Settings.RestartInterval = TimeSpan.FromSeconds(100);
				}

				if (key == 2)
				{
					BootTrigger bTrigger = (BootTrigger)td.Triggers.Add(new BootTrigger { Enabled = false }); //(BootTrigger)td.Triggers.AddNew(TaskTriggerType.Boot);
					if (newVer) bTrigger.Delay = TimeSpan.FromMinutes(5);
				}

				DailyTrigger dTrigger = (DailyTrigger)td.Triggers.Add(new DailyTrigger());
				dTrigger.DaysInterval = 2;
				if (newVer) dTrigger.RandomDelay = TimeSpan.FromHours(2);

				if (newVer)
				{
					if (key == 2)
					{
						EventTrigger eTrigger = (EventTrigger)td.Triggers.Add(new EventTrigger());
						eTrigger.Subscription = "<QueryList><Query Id=\"0\" Path=\"Security\"><Select Path=\"Security\">*[System[Provider[@Name='VSSAudit'] and EventID=25]]</Select></Query></QueryList>";
						eTrigger.ValueQueries.Add("Name", "Value");
					}

					td.Triggers.Add(new RegistrationTrigger { Delay = TimeSpan.FromMinutes(5) });

					if (key == 2)
					{
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleConnect, UserId = user });
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleDisconnect });
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteConnect });
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteDisconnect });
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionLock, UserId = user });
						td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionUnlock });
					}
				}

				td.Triggers.Add(new IdleTrigger());

				LogonTrigger lTrigger = (LogonTrigger)td.Triggers.Add(new LogonTrigger());
				if (newVer)
				{
					lTrigger.Delay = TimeSpan.FromMinutes(15);
					lTrigger.UserId = user;
					lTrigger.Repetition.Interval = TimeSpan.FromSeconds(1000);
				}

				MonthlyTrigger mTrigger = (MonthlyTrigger)td.Triggers.Add(new MonthlyTrigger());
				mTrigger.DaysOfMonth = new int[] { 3, 6, 10, 18 };
				mTrigger.MonthsOfYear = MonthsOfTheYear.July | MonthsOfTheYear.November;
				if (newVer) mTrigger.RunOnLastDayOfMonth = true;
				mTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(90);

				MonthlyDOWTrigger mdTrigger = (MonthlyDOWTrigger)td.Triggers.Add(new MonthlyDOWTrigger());
				mdTrigger.DaysOfWeek = DaysOfTheWeek.AllDays;
				mdTrigger.MonthsOfYear = MonthsOfTheYear.January | MonthsOfTheYear.December;
				if (newVer) mdTrigger.RunOnLastWeekOfMonth = true;
				mdTrigger.WeeksOfMonth = WhichWeek.FirstWeek;

				TimeTrigger tTrigger = (TimeTrigger)td.Triggers.Add(new TimeTrigger());
				tTrigger.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1);
				tTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(7);
				if (newVer) tTrigger.ExecutionTimeLimit = TimeSpan.FromSeconds(15);
				if (newVer) tTrigger.Id = "Time test";
				tTrigger.Repetition.Duration = TimeSpan.FromMinutes(20);
				tTrigger.Repetition.Interval = TimeSpan.FromMinutes(15);
				tTrigger.Repetition.StopAtDurationEnd = true;

				WeeklyTrigger wTrigger = (WeeklyTrigger)td.Triggers.Add(new WeeklyTrigger());
				wTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
				wTrigger.WeeksInterval = 3;

				td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
				if (newVer)
				{
					td.Actions.Add(new ShowMessageAction("Running Notepad", "Info"));
					td.Actions.Add(new EmailAction("Testing", "dahall@codeplex.com", "user@test.com", "You've got mail.", "mail.myisp.com"));
					td.Actions.Add(new ComHandlerAction(new Guid("CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734"), string.Empty));
				}
				
				tf.RegisterTaskDefinition("Test", td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken, null);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			Task runningTask = tf.Tasks["Test"];
			Console.WriteLine("\nNew task will run at " + runningTask.NextRunTime);
			Console.WriteLine("\nNew task triggers:");
			for (int i = 0; i < runningTask.Definition.Triggers.Count; i++)
				Console.WriteLine("  {0}: {1}", i, runningTask.Definition.Triggers[i]);
			Console.WriteLine("\nNew task actions:");
			for (int i = 0; i < runningTask.Definition.Actions.Count; i++)
				Console.WriteLine("  {0}: {1}", i, runningTask.Definition.Actions[i]);

			DisplayTask(runningTask, true);
			tf.DeleteTask("Test");
		}

		static TaskDefinition DisplayTask(Task t, bool editable)
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			Form1 frm = new Form1();
			frm.taskPropertiesControl1.Editable = editable;
			frm.taskPropertiesControl1.Initialize(t);
			return (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? frm.taskPropertiesControl1.TaskDefinition : null;
		}
	}
}