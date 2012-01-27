using System;
using Microsoft.Win32.TaskScheduler;
using System.Windows.Forms;

namespace TestTaskService
{
	class Program
	{
		private static TaskEditDialog editorForm;
		public delegate void TestMethod(TaskService ts, System.IO.TextWriter output, params string[] arg);

		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length > 0 && char.ToUpper(args[0][0]) == 'C')
			{
				string[] newArgs = new string[args.Length - 1];
				args.CopyTo(newArgs, 1);
				ConsoleMain(newArgs);
				return;
			}

			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
			//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}

		static void ConsoleMain(string[] args)
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

			System.Windows.Forms.Application.EnableVisualStyles();
			using (TaskService ts = new TaskService(newArgs[1], newArgs[2], newArgs[3], newArgs[4], newArgs[0] == "1"))
			{
				switch (test)
				{
					case 'W':
						WizardTest(ts, Console.Out);
						break;
					case 'E':
						EditorTest(ts, Console.Out);
						break;
					case 'F':
						FindActionString(ts, Console.Out, newArgs[5]);
						Console.ReadKey();
						break;
					case 'S':
						ShortTest(ts, Console.Out);
						Console.ReadKey();
						break;
					case 'M':
						MMCTest(ts, Console.Out);
						break;
					default:
						LongTest(ts, Console.Out);
						Console.ReadKey();
						break;
				}
			}
		}

		internal static void FindTask(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			try
			{
				Task t = ts.FindTask(arg[0]);
				if (t == null)
					output.WriteLine(string.Format("Task '{0}' not found.", arg[0]));
				else
					output.WriteLine(string.Format("Task '{0}' found. Created on {1:g} and last run on {2:g}.", t.Name, t.Definition.RegistrationInfo.Date, t.LastRunTime));
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void FindActionString(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			try
			{
				TaskFolder tf = ts.RootFolder;
				FindActionStringInFolder(output, tf, arg[0]);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		static void FindActionStringInFolder(System.IO.TextWriter output, TaskFolder tf, string arg)
		{
			foreach (Task t in tf.Tasks)
			{
				try
				{
					bool found = false;
					foreach (var action in t.Definition.Actions)
					{
						switch (action.ActionType)
						{
							case TaskActionType.ComHandler:
								/*if (((ComHandlerAction)action).ClassId.ToString().IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((ComHandlerAction)action).Data.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)*/
									//found = true;
								output.WriteLine(" > " + action.ToString());
								break;
							case TaskActionType.Execute:
								if (((ExecAction)action).Path.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((ExecAction)action).Arguments.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
									found = true;
								break;
							case TaskActionType.SendEmail:
								if (((EmailAction)action).Bcc.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).Body.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).Cc.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).From.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).ReplyTo.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).Server.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).Subject.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
									((EmailAction)action).To.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
									found = true;
									break;
							case TaskActionType.ShowMessage:
									if (((ShowMessageAction)action).MessageBody.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
										((ShowMessageAction)action).Title.IndexOf(arg, 0, StringComparison.CurrentCultureIgnoreCase) >= 0)
										found = true;
									break;
							default:
								break;
						}
					}
					if (found)
					{
						output.WriteLine("+ {0}, {1} ({2})", t.Name, t.Path, t.State);
					}
				}
				catch { }
			}

			TaskFolderCollection tfs = tf.SubFolders;
			if (tfs.Count > 0)
			{
				try
				{
					foreach (TaskFolder sf in tfs)
						FindActionStringInFolder(output, sf, arg);
				}
				catch (Exception ex)
				{
					output.WriteLine(ex.ToString());
				}
			}
		}

		internal static void WizardTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			try
			{
				// Create a new task definition and assign properties
				const string taskName = "Test";
				Task t = ts.FindTask(taskName, true);
				if (t == null)
				{
					TaskDefinition td = ts.NewTask();
					td.RegistrationInfo.Description = "Longer Description Of Task Goes Here";
					td.Triggers.Add(new DailyTrigger() { StartBoundary = DateTime.Now + TimeSpan.FromHours(1) });
					td.Actions.Add(new ExecAction("A", "B"));
					t = ts.RootFolder.RegisterTaskDefinition(taskName, td);
				}

				if (t.Definition.Triggers.Count > 1)
				{
					TaskEditDialog editorForm = new TaskEditDialog();
					editorForm.Editable = true;
					editorForm.RegisterTaskOnAccept = true;
					editorForm.Initialize(t);
					editorForm.ShowDialog();
				}
				else
				{
					TaskSchedulerWizard wiz = new TaskSchedulerWizard();
					wiz.AvailablePages = TaskSchedulerWizard.AvailableWizardPages.TriggerPropertiesPage | TaskSchedulerWizard.AvailableWizardPages.SecurityPage | TaskSchedulerWizard.AvailableWizardPages.TriggerSelectPage | TaskSchedulerWizard.AvailableWizardPages.SummaryPage;
					wiz.AvailableTriggers = TaskSchedulerWizard.AvailableWizardTriggers.Daily | TaskSchedulerWizard.AvailableWizardTriggers.Time | TaskSchedulerWizard.AvailableWizardTriggers.Weekly;
					wiz.AllowEditorOnFinish = true;
					wiz.EditorOnFinishText = "Show dialog";
					wiz.TriggerPagePrompt = "When???";
					wiz.RegisterTaskOnFinish = true;
					wiz.SummaryRegistrationNotice = "Done when you click Finish";
					wiz.SummaryFormatString = "Name: {0}\r\nDescription: {1}\r\nTrigger: {2}";
					wiz.Title = "My Wizard";
					wiz.Initialize(t);
					if (wiz.ShowDialog() == DialogResult.OK)
					{
						ts.RootFolder.RegisterTaskDefinition(wiz.TaskName, wiz.TaskDefinition, TaskCreation.CreateOrUpdate,
							wiz.TaskDefinition.Principal.ToString(), wiz.Password, wiz.TaskDefinition.Principal.LogonType);
					}
				}

				ts.RootFolder.DeleteTask(taskName);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void EditorTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			// Get the service on the local machine
			try
			{
				// Create a new task definition and assign properties
				const string taskName = "Test";
				TaskDefinition td = ts.NewTask();
				/*td.Triggers.Add(new TimeTrigger() { StartBoundary = DateTime.Now + TimeSpan.FromHours(1) });
				td.Actions.Add(new ExecAction("notepad.exe"));
				ts.RootFolder.RegisterTaskDefinition(taskName, td);*/
				const string user = @"AMERICAS\dahall";
				td.Data = "Your data";
				//td.Principal.UserId = user;
				//td.Principal.LogonType = TaskLogonType.InteractiveToken;
				td.RegistrationInfo.Author = "Elucidate";
				td.RegistrationInfo.Description = "Performs the SnapRAID Sync command after a small delay after logon";
				td.RegistrationInfo.Documentation = "http://elucidate.codeplex.com/documentation";
				td.Settings.DisallowStartIfOnBatteries = true;
				td.Settings.Enabled = true;
				td.Settings.ExecutionTimeLimit = TimeSpan.FromHours(24);
				td.Settings.Hidden = false;
				td.Settings.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
				td.Settings.RunOnlyIfIdle = false;
				td.Settings.RunOnlyIfNetworkAvailable = false;
				td.Settings.StopIfGoingOnBatteries = true;
				Version ver = ts.HighestSupportedVersion;
				bool newVer = (ver >= new Version(1, 2));
				// Create a trigger that fires 15 minutes after the current user logs on and then every 1000 seconds after that
				LogonTrigger lTrigger = (LogonTrigger)td.Triggers.Add(new LogonTrigger());
				if (newVer)
				{
					lTrigger.Delay = TimeSpan.FromSeconds(30);
					lTrigger.UserId = user;
				}
				// Create an action which opens a log file in notepad
				td.Actions.Add(new ExecAction("cmd", @"/k", null));
				// Register the task definition (saves it) in the security context of the interactive user
				ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken);

				// Edit task
				Task t = ts.GetTask(taskName);
				td = DisplayTask(t, true);

				// Register then show task again
				while (td != null)
				{
					//ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.Update, @"AMERICAS\Domain Users", null, TaskLogonType.Group, null);
					t = ts.GetTask(taskName);
					td = DisplayTask(t, true);
				}

				// Remove the task we just created
				ts.RootFolder.DeleteTask(taskName);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void ShortTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			// Get the service on the local machine
			try
			{
				// Create a new task definition and assign properties
				const string taskName = "Test";
				Task t;/* = ts.AddTask(taskName,
					new MonthlyDOWTrigger(DaysOfTheWeek.Monday, MonthsOfTheYear.January, WhichWeek.FirstWeek),
					new ExecAction("notepad.exe", "c:\\test.log", null), "SYSTEM", null, TaskLogonType.ServiceAccount);
				System.Threading.Thread.Sleep(1000);
				output.WriteLine("LastTime & Result: {0} ({1})", t.LastRunTime, t.LastTaskResult);
				output.WriteLine("NextRunTime: {0:g}", t.NextRunTime);
				TaskDefinition td = t.Definition;
				t = null;*/

				// Retrieve the task, add a trigger and save it.
				t = ts.GetTask(taskName);
				//ts.RootFolder.DeleteTask(taskName);
				TaskDefinition td = t.Definition;
				td.Triggers.Clear();
				td.Triggers.Add(new TimeTrigger());

				ts.RootFolder.RegisterTaskDefinition(taskName, td);
				System.Threading.Thread.Sleep(1000);
//				ts.RootFolder.DeleteTask(taskName);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void LongTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

			Version ver = ts.HighestSupportedVersion;
			bool isV12 = (ver >= new Version(1, 2));
			bool isV13 = (ver >= new Version(1, 3));
			output.WriteLine("Highest version: " + ver);
			output.WriteLine("Server: {0} ({1}); User: {2}\\{3}", ts.TargetServer, ts.Connected ? "Connected" : "Disconnected", ts.UserAccountDomain, ts.UserName);

			output.WriteLine("Running tasks:");
			foreach (RunningTask rt in ts.GetRunningTasks(true))
			{
				if (rt != null)
				{
					output.WriteLine("+ {0}, {1} ({2})", rt.Name, rt.Path, rt.State);
					if (ver.Minor > 0)
						output.WriteLine("  Current Action: " + rt.CurrentAction);
				}
			}

			string filter = arg.Length > 0 ? arg[0] : string.Empty;
			TaskFolder tf = ts.RootFolder;
			TaskCollection tasks = tf.GetTasks(new Wildcard(filter));
			output.WriteLine("\nRoot folder tasks matching \"{1}\" ({0}):", tasks.Count, filter);
			foreach (Task t in tasks)
			{
				try
				{
					output.WriteLine("+ {0}, {1} ({2}) - {3}", t.Name, t.Definition.RegistrationInfo.Author, t.State, t.Definition.Settings.Compatibility);
					foreach (Trigger trg in t.Definition.Triggers)
						output.WriteLine(" + {0}", trg);
					foreach (var act in t.Definition.Actions)
						output.WriteLine(" = {0}", act);
				}
				catch { }
			}

			output.WriteLine("\n***Finding defrag task***");
			Task ft = ts.FindTask("*defrag*");
			if (ft != null)
				output.WriteLine("Defrag task found at " + ft.Path);
			else
				output.WriteLine("Defrag task not found.");

			TaskFolderCollection tfs = tf.SubFolders;
			if (tfs.Count > 0)
			{
				output.WriteLine("\nSub folders:");
				try
				{
					foreach (TaskFolder sf in tfs)
						output.WriteLine("+ {0}", sf.Path);
				}
				catch (Exception ex)
				{
					output.WriteLine(ex.ToString());
				}
			}

			if (isV12)
			{
				output.WriteLine("\n***Checking folder retrieval***");
				try
				{
					const string testFolder = "David's TestFolder";
					tf.CreateFolder(testFolder);
					TaskFolder sub = tf.SubFolders[testFolder];
					output.WriteLine("\nSubfolder path: " + sub.Path);
					ts.AddTask(testFolder + @"\MyTask", new LogonTrigger(), new ExecAction("notepad"));
					output.WriteLine(" - Tasks: " + sub.Tasks.Count.ToString());
					sub.DeleteTask("MyTask");
					tf.DeleteFolder(testFolder);
				}
				catch (NotSupportedException) { }
				catch (Exception ex)
				{
					output.WriteLine(ex.ToString());
				}
			}

			output.WriteLine("\n***Checking task creation***");
			try
			{
				TaskDefinition td = ts.NewTask();
				td.Data = "Your data";
				td.Principal.UserId = "SYSTEM";
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				td.RegistrationInfo.Author = "dahall";
				td.RegistrationInfo.Description = "Does something";
				td.RegistrationInfo.Documentation = "Don't pretend this is real.";
				td.Settings.DisallowStartIfOnBatteries = true;
				td.Settings.Enabled = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.Zero; // FromHours(2);
				td.Settings.Hidden = false;
				td.Settings.IdleSettings.IdleDuration = TimeSpan.FromMinutes(20);
				td.Settings.IdleSettings.RestartOnIdle = false;
				td.Settings.IdleSettings.StopOnIdleEnd = false;
				td.Settings.IdleSettings.WaitTimeout = TimeSpan.FromMinutes(10);
				td.Settings.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
				td.Settings.RunOnlyIfIdle = true;
				td.Settings.RunOnlyIfNetworkAvailable = true;
				td.Settings.StopIfGoingOnBatteries = true;
				if (isV12)
				{
					td.Principal.RunLevel = TaskRunLevel.Highest; //.LUA;
					//td.RegistrationInfo.SecurityDescriptorSddlForm = "O:COG:CGD::(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)";
					td.RegistrationInfo.Source = "Test App";
					td.RegistrationInfo.URI = new Uri("test://app");
					td.RegistrationInfo.Version = new Version(0, 9);
					td.Settings.AllowDemandStart = false;
					td.Settings.AllowHardTerminate = false;
					td.Settings.Compatibility = TaskCompatibility.V2;
					td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromMinutes(1);
					td.Settings.MultipleInstances = TaskInstancesPolicy.StopExisting;
					td.Settings.StartWhenAvailable = true;
					td.Settings.WakeToRun = true;
					td.Settings.RestartCount = 5;
					td.Settings.RestartInterval = TimeSpan.FromSeconds(100);
					//td.Settings.NetworkSettings.Id = new Guid("{99AF272D-BC5B-4F64-A5B7-8688392C13E6}");
				}
				if (isV13)
				{
					/*td.Principal.ProcessTokenSidType = TaskProcessTokenSidType.Unrestricted;
					td.Principal.RequiredPrivileges.Add("SeBackupPrivilege");
					td.Principal.RequiredPrivileges.Add("SeDebugPrivilege");
					td.Principal.RequiredPrivileges.Add("SeImpersonatePrivilege");
					output.Write("Priv: ");
					foreach (string item in td.Principal.RequiredPrivileges)
						output.Write(item + ", ");
					output.WriteLine();*/
					td.Settings.DisallowStartOnRemoteAppSession = true;
					td.Settings.UseUnifiedSchedulingEngine = true;
				}

				if (isV12)
				{
					BootTrigger bTrigger = (BootTrigger)td.Triggers.Add(new BootTrigger { Enabled = false }); //(BootTrigger)td.Triggers.AddNew(TaskTriggerType.Boot);
					if (isV12) bTrigger.Delay = TimeSpan.FromMinutes(5);
				}

				DailyTrigger dTrigger = (DailyTrigger)td.Triggers.Add(new DailyTrigger());
				dTrigger.DaysInterval = 2;
				if (isV12) dTrigger.RandomDelay = TimeSpan.FromHours(2);

				if (isV12)
				{
					EventTrigger eTrigger = (EventTrigger)td.Triggers.Add(new EventTrigger());
					eTrigger.Subscription = "<QueryList><Query Id=\"0\" Path=\"Security\"><Select Path=\"Security\">*[System[Provider[@Name='VSSAudit'] and EventID=25]]</Select></Query></QueryList>";
					eTrigger.ValueQueries.Add("Name", "Value");

					td.Triggers.Add(new RegistrationTrigger { Delay = TimeSpan.FromMinutes(5) });

					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleConnect, UserId = user });
					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleDisconnect });
					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteConnect });
					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteDisconnect });
					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionLock, UserId = user });
					td.Triggers.Add(new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionUnlock });
				}

				td.Triggers.Add(new IdleTrigger());

				LogonTrigger lTrigger = (LogonTrigger)td.Triggers.Add(new LogonTrigger());
				if (isV12)
				{
					lTrigger.Delay = TimeSpan.FromMinutes(15);
					lTrigger.UserId = user;
					lTrigger.Repetition.Interval = TimeSpan.FromSeconds(1000);
				}

				MonthlyTrigger mTrigger = (MonthlyTrigger)td.Triggers.Add(new MonthlyTrigger());
				mTrigger.DaysOfMonth = new int[] { 3, 6, 10, 18 };
				mTrigger.MonthsOfYear = MonthsOfTheYear.July | MonthsOfTheYear.November;
				if (isV12) mTrigger.RunOnLastDayOfMonth = true;
				mTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(90);

				MonthlyDOWTrigger mdTrigger = (MonthlyDOWTrigger)td.Triggers.Add(new MonthlyDOWTrigger());
				mdTrigger.DaysOfWeek = DaysOfTheWeek.AllDays;
				mdTrigger.MonthsOfYear = MonthsOfTheYear.January | MonthsOfTheYear.December;
				if (isV12) mdTrigger.RunOnLastWeekOfMonth = true;
				mdTrigger.WeeksOfMonth = WhichWeek.FirstWeek;

				TimeTrigger tTrigger = (TimeTrigger)td.Triggers.Add(new TimeTrigger());
				tTrigger.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1);
				tTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(7);
				if (isV12) tTrigger.ExecutionTimeLimit = TimeSpan.FromSeconds(19);
				if (isV12) tTrigger.Id = "Time test";
				tTrigger.Repetition.Duration = TimeSpan.FromMinutes(21);
				tTrigger.Repetition.Interval = TimeSpan.FromMinutes(17);
				tTrigger.Repetition.StopAtDurationEnd = true;

				WeeklyTrigger wTrigger = (WeeklyTrigger)td.Triggers.Add(new WeeklyTrigger());
				wTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
				wTrigger.WeeksInterval = 3;

				td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
				if (isV12)
				{
					td.Actions.Add(new ShowMessageAction("Running Notepad", "Info"));
					td.Actions.Add(new EmailAction("Testing", "dahall@codeplex.com", "user@test.com", "You've got mail.", "mail.myisp.com"));
					td.Actions.Add(new ComHandlerAction(new Guid("{BF300543-7BA5-4C17-A318-9BBDB7429A21}"), @"C:\Users\dahall\Documents\Visual Studio 2010\Projects\TaskHandlerProxy\TaskHandlerSample\bin\Release\TaskHandlerSample.dll|TaskHandlerSample.TaskHandler|MoreData"));
				}
				
				tf.RegisterTaskDefinition("Test", td);

				// Try copying it
				TaskDefinition td2 = ts.NewTask();
				foreach (Trigger tg in td.Triggers)
					td2.Triggers.Add((Trigger)tg.Clone());
				foreach (Microsoft.Win32.TaskScheduler.Action a in td.Actions)
					td2.Actions.Add((Microsoft.Win32.TaskScheduler.Action)a.Clone());
				tf.RegisterTaskDefinition("Test2", td2, TaskCreation.CreateOrUpdate, user, null, TaskLogonType.InteractiveToken, null);
				tf.DeleteTask("Test2");
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
				return;
			}

			Task runningTask = tf.Tasks["Test"];
			output.WriteLine("\nNew task will next run at " + runningTask.NextRunTime);
			DateTime[] times = runningTask.GetRunTimes(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7), 0);
			if (times.Length > 0)
			{
				output.WriteLine("\nNew task will run at the following times over the next week:");
				foreach (DateTime dt in times)
					output.WriteLine("  {0}", dt);
			}
			output.WriteLine("\nNew task triggers:");
			for (int i = 0; i < runningTask.Definition.Triggers.Count; i++)
				output.WriteLine("  {0}: {1}", i, runningTask.Definition.Triggers[i]);
			output.WriteLine("\nNew task actions:");
			for (int i = 0; i < runningTask.Definition.Actions.Count; i++)
				output.WriteLine("  {0}: {1}", i, runningTask.Definition.Actions[i]);

			DisplayTask(runningTask, true);
			tf.DeleteTask("Test");
		}

		internal static void MMCTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			TSMMCMockup form = new TSMMCMockup();
			form.ShowDialog();
		}

		static TaskDefinition DisplayTask(Task t, bool editable)
		{
			if (editorForm == null)
				editorForm = new TaskEditDialog();
			editorForm.Editable = editable;
			editorForm.Initialize(t);
			editorForm.RegisterTaskOnAccept = true;
			return (editorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? editorForm.TaskDefinition : null;
		}
	}
}