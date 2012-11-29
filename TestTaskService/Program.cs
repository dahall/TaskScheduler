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
						FindTaskWithProperty(ts, Console.Out, newArgs[5]);
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

		internal static void FindTaskWithProperty(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			try
			{
				TaskFolder tf = ts.RootFolder;
				FindTaskWithPropertyInFolder(output, tf, arg[0]);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		static void FindTaskWithPropertyInFolder(System.IO.TextWriter output, TaskFolder tf, string arg, System.Text.RegularExpressions.Match match = null)
		{
			if (match == null)
				match = System.Text.RegularExpressions.Regex.Match(arg, "(\\.?\\w+)*\\s*(==|!=)\\s*\\\"([^\"]*)\\\"");
			if (!match.Success)
				return;

			foreach (Task t in tf.Tasks)
			{
				try
				{
					bool found = false;
					System.Reflection.PropertyInfo pi;
					Object lastObj = t;
					int i;
					for (i = 0; i < match.Groups[1].Captures.Count && lastObj != null; i++)
					{
						string prop = match.Groups[1].Captures[i].Value.TrimStart('.');
						pi = lastObj.GetType().GetProperty(prop);
						if (pi == null)
						{
							output.WriteLine("Unable to locate property {0}", prop);
							return;
						}
						lastObj = pi.GetValue(lastObj, null);
					}
					if (i == match.Groups[1].Captures.Count)
					{
						string res = (lastObj == null) ? string.Empty : lastObj.ToString();
						found = res.Equals(match.Groups[3].Value, StringComparison.InvariantCultureIgnoreCase);
						if (match.Groups[2].Value == "!=")
							found = !found;
						if (found)
							output.WriteLine("+ {0}, {1} ({2})\n\r== {3}", t.Name, t.Path, t.State, res);
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
						FindTaskWithPropertyInFolder(output, sf, arg, match);
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
				TaskSchedulerWizard wiz = new TaskSchedulerWizard(ts);
				wiz.RegisterTaskOnFinish = true;
				wiz.TaskFolder = @"\Microsoft";
				if (wiz.ShowDialog() == DialogResult.OK)
				{
					Task t = wiz.Task;
					if (t.Definition.Triggers.Count > 1)
						new TaskEditDialog(t).ShowDialog();
					else
					{
						wiz.AvailablePages = TaskSchedulerWizard.AvailableWizardPages.TriggerPropertiesPage | TaskSchedulerWizard.AvailableWizardPages.TriggerSelectPage | TaskSchedulerWizard.AvailableWizardPages.SummaryPage;
						wiz.AvailableTriggers = TaskSchedulerWizard.AvailableWizardTriggers.Daily | TaskSchedulerWizard.AvailableWizardTriggers.Time | TaskSchedulerWizard.AvailableWizardTriggers.Weekly | TaskSchedulerWizard.AvailableWizardTriggers.Monthly | TaskSchedulerWizard.AvailableWizardTriggers.MonthlyDOW;
						wiz.AllowEditorOnFinish = true;
						wiz.EditorOnFinishText = "Show dialog";
						wiz.TriggerPagePrompt = "When???";
						wiz.RegisterTaskOnFinish = true;
						wiz.SummaryRegistrationNotice = "Done when you click Finish";
						wiz.SummaryFormatString = "Name: {0}\r\nDescription: {1}\r\nTrigger: {2}";
						wiz.Title = "My Wizard";
						wiz.Initialize(t);
						wiz.ShowDialog();
					}
				}

				if (wiz.Task != null)
					ts.RootFolder.DeleteTask(wiz.Task.Path);
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
				DisplayTask(ts, null, true);

				// Create a new task definition and assign properties
				const string taskName = "Test";
				TaskDefinition td = ts.NewTask();
				//td.Settings.DeleteExpiredTaskAfter = new TimeSpan(7, 0, 0, 0, 0);
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				td.Principal.UserId = "LOCAL SERVICE";
				td.Triggers.Add(new LogonTrigger());
				td.Settings.DisallowStartIfOnBatteries = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
				if (ts.HighestSupportedVersion >= new Version(1, 3))
				{
					td.Settings.Compatibility = TaskCompatibility.V2_1;
					td.RegistrationInfo.Author = "Me";
					td.RegistrationInfo.Documentation = "We rock";
					td.RegistrationInfo.Source = "Home";
					td.RegistrationInfo.SecurityDescriptorSddlForm = "D:P(A;;FA;;;BA)(A;;FRFW;;;SY)(A;;FRFX;;;LS)";
					td.RegistrationInfo.URI = new Uri("http://codeplex.com");
					td.Settings.AllowHardTerminate = false;
					td.Settings.UseUnifiedSchedulingEngine = true;
					if (ts.HighestSupportedVersion >= new Version(1, 4))
					{
						td.Settings.Compatibility = TaskCompatibility.V2_2;
						td.Settings.MaintenanceSettings.Period = TimeSpan.FromDays(2);
						td.Settings.MaintenanceSettings.Deadline = TimeSpan.FromDays(14);
					}
				}
				//td.Triggers.Add(new TimeTrigger() { StartBoundary = DateTime.Now.AddHours(1), EndBoundary = DateTime.Now.AddHours(2) });
				td.Actions.Add(new ExecAction("notepad.exe"));
				WriteXml(td, taskName);
				Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken);
				/*Task t = ts.AddTask(taskName,
					new DailyTrigger { StartBoundary = new DateTime(2012, 5, 1, 1, 0, 0, DateTimeKind.Local) },
					new ExecAction("notepad.exe"));*/
				//WriteXml(t);

				// Edit task
				td = DisplayTask(t, true);

				// Register then show task again
				while (td != null)
				{
					//ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.Update, @"AMERICAS\Domain Users", null, TaskLogonType.Group, null);
					t = ts.GetTask(taskName);
					WriteXml(t);
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

		static void FindTaskWithComAction(System.IO.TextWriter output, TaskFolder tf)
		{
			foreach (Task t in tf.Tasks)
			{
				foreach (Microsoft.Win32.TaskScheduler.Action ac in t.Definition.Actions)
				{
					ComHandlerAction a = ac as ComHandlerAction;
					if (a == null)
						continue;
					string name = null, model = null, path = null, asm = null;
					try
					{
						Microsoft.Win32.RegistryKey k = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\" + a.ClassId.ToString("B"));
						if (k == null)
							k = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("Wow6432Node\\CLSID\\" + a.ClassId.ToString("B"));
						name = k.GetValue(null, "").ToString();
						Microsoft.Win32.RegistryKey sk = k.OpenSubKey("InprocServer32");
						path = sk.GetValue(null, "").ToString();
						if (!string.IsNullOrEmpty(path))
						{
							try
							{
								System.Reflection.AssemblyName.GetAssemblyName(path);
								asm = "Yes";
							}
							catch { asm = "No"; }
						}
						model = sk.GetValue("ThreadingModel", "").ToString();
					}
					catch { }
					output.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", t.Path, t.Name, a.ClassId, a.Data, name, path, model, asm);
				}
			}
			foreach (var f in tf.SubFolders)
			{
				FindTaskWithComAction(output, f);
			}
		}

		internal static void ShortTest(TaskService ts, System.IO.TextWriter output, params string[] arg)
		{
			// Get the service on the local machine
			try
			{
				// Create a new task definition and assign properties
				const string taskName = "Test";
				TaskDefinition td = ts.NewTask();
				td.Triggers.Add(new DailyTrigger(2));
				td.Actions.Add(new ExecAction("notepad.exe"));
				Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);

				System.Threading.Thread.Sleep(1000);
				output.WriteLine("LastTime & Result: {0} ({1})", t.LastRunTime == DateTime.MinValue ? "Never" : t.LastRunTime.ToString("g"), t.LastTaskResult);
				output.WriteLine("NextRunTime: {0:g}", t.NextRunTime);

				/*using (var taskEditDialog = new TaskEditDialog(t, true, true))
				{
					if (taskEditDialog.ShowDialog() == DialogResult.OK)
					{
						var t2 = taskEditDialog.Task;
						output.WriteLine("Triggers: {0}", t2.Definition.Triggers);
					}
				}*/

				// Retrieve the task, add a trigger and save it.
				//t = ts.GetTask(taskName);
				//ts.RootFolder.DeleteTask(taskName);
				td = t.Definition;
				td.Triggers.Clear();
				WeeklyTrigger wt = td.Triggers.AddNew(TaskTriggerType.Weekly) as WeeklyTrigger;
				wt.DaysOfWeek = DaysOfTheWeek.Friday;

				t = ts.RootFolder.RegisterTaskDefinition(taskName, td);
				output.WriteLine("Principal: {1}; Triggers: {0}", t.Definition.Triggers, t.Definition.Principal);
				ts.RootFolder.DeleteTask(taskName);
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
			bool isV14 = (ver >= new Version(1, 4));
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
					try { tf.CreateFolder(testFolder); }
					catch (System.Runtime.InteropServices.COMException cex) { if (cex.ErrorCode != -2147024713) throw; }
					catch { throw; }
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
				//td.Principal.UserId = "SYSTEM";
				//td.Principal.LogonType = TaskLogonType.ServiceAccount;
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
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
					td.Principal.Id = "Author";
					td.RegistrationInfo.SecurityDescriptorSddlForm = "D:P(A;;FA;;;BA)(A;;FA;;;SY)(A;;FRFX;;;LS)";
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
					td.Settings.Compatibility = TaskCompatibility.V2_1;
					td.Settings.DisallowStartOnRemoteAppSession = true;
					td.Settings.UseUnifiedSchedulingEngine = false;

					/*td.Principal.ProcessTokenSidType = TaskProcessTokenSidType.Unrestricted;
					td.Principal.RequiredPrivileges.Add(TaskPrincipalPrivilege.SeBackupPrivilege);
					td.Principal.RequiredPrivileges.Add(TaskPrincipalPrivilege.SeDebugPrivilege);
					td.Principal.RequiredPrivileges.Add(TaskPrincipalPrivilege.SeImpersonatePrivilege);
					output.Write("Priv: ");
					//output.Write(td.Principal.RequiredPrivileges[0]);
					foreach (TaskPrincipalPrivilege item in td.Principal.RequiredPrivileges)
						output.Write(item.ToString() + ", ");
					output.WriteLine();*/
				}
				if (isV14)
				{
					td.Settings.Compatibility = TaskCompatibility.V2_2;
					td.Settings.Volatile = true;
					td.Settings.MaintenanceSettings.Exclusive = true;
					td.Settings.MaintenanceSettings.Period = TimeSpan.FromDays(5);
					td.Settings.MaintenanceSettings.Deadline = TimeSpan.FromDays(15);
				}

				// Setup Triggers
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

				// Setup Actions
				td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
				if (isV12)
				{
					td.Actions.Context = "Author";
					if (td.Principal.LogonType == TaskLogonType.InteractiveToken || td.Principal.LogonType == TaskLogonType.Group)
						td.Actions.Add(new ShowMessageAction("Running Notepad", "Info"));
					td.Actions.Add(new EmailAction("Testing", "dahall@codeplex.com", "user@test.com", "You've got mail.", "mail.myisp.com"));
					td.Actions.Add(new ComHandlerAction(new Guid("{BF300543-7BA5-4C17-A318-9BBDB7429A21}"), @"C:\Users\dahall\Documents\Visual Studio 2010\Projects\TaskHandlerProxy\TaskHandlerSample\bin\Release\TaskHandlerSample.dll|TaskHandlerSample.TaskHandler|MoreData"));
				}

				// Register task
				WriteXml(td, "PreRegTest");
				Task t = tf.RegisterTaskDefinition("Test", td);
				WriteXml(t);

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
			editorForm.AvailableTabs = AvailableTaskTabs.All;
			return (editorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? editorForm.TaskDefinition : null;
		}

		static TaskDefinition DisplayTask(TaskService ts, TaskDefinition td, bool editable)
		{
			if (editorForm == null)
				editorForm = new TaskEditDialog();
			editorForm.Editable = editable;
			editorForm.Initialize(ts, td);
			editorForm.RegisterTaskOnAccept = true;
			editorForm.AvailableTabs = AvailableTaskTabs.History;
			//editorForm.AvailableTabs = AvailableTaskTabs.General | AvailableTaskTabs.Triggers | AvailableTaskTabs.Actions | AvailableTaskTabs.Conditions | AvailableTaskTabs.Settings;
			return (editorForm.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? editorForm.TaskDefinition : null;
		}

		static void WriteXml(Task t)
		{
			t.Export(System.IO.Path.Combine(System.IO.Path.GetTempPath(), t.Name + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss") + ".xml"));
		}

		static void WriteXml(TaskDefinition td, string name)
		{
			System.IO.File.WriteAllText(System.IO.Path.Combine(System.IO.Path.GetTempPath(), name + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss") + ".xml"), td.XmlText, System.Text.Encoding.Unicode);
		}

		internal static void OutputXml(TaskService ts, System.IO.StringWriter output)
		{
			// Get the service on the local machine
			try
			{
				TaskDefinition.GetV1SchemaFile(new System.Xml.Schema.XmlSchemaSet());

				// Create a new task definition and assign properties
				const string taskName = "Test";
				TaskDefinition td = ts.NewTask();
				td.Data = "Some data";
				td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromHours(12);
				td.Settings.IdleSettings.RestartOnIdle = true;
				td.RegistrationInfo.Author = "Me";
				td.Triggers.Add(new BootTrigger());
				td.Triggers.Add(new LogonTrigger());
				td.Triggers.Add(new IdleTrigger());
				TimeTrigger tt = (TimeTrigger)td.Triggers.Add(new TimeTrigger() { Enabled = false, EndBoundary = DateTime.Now.AddYears(1) });
				tt.Repetition.Duration = TimeSpan.FromHours(4);
				tt.Repetition.Interval = TimeSpan.FromHours(1);
				DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(3) { Enabled = false });
				dt.Repetition.Duration = TimeSpan.FromHours(24);
				dt.Repetition.Interval = TimeSpan.FromHours(2);
				td.Triggers.Add(new MonthlyDOWTrigger { DaysOfWeek = DaysOfTheWeek.AllDays, MonthsOfYear = MonthsOfTheYear.AllMonths, WeeksOfMonth = WhichWeek.FirstWeek, RunOnLastWeekOfMonth = true });
				td.Triggers.Add(new MonthlyTrigger { DaysOfMonth = new int[] { 3, 6, 9 }, RunOnLastDayOfMonth = true, MonthsOfYear = MonthsOfTheYear.April });
				td.Triggers.Add(new WeeklyTrigger(DaysOfTheWeek.Saturday, 2));
				td.Actions.Add(new ExecAction("notepad.exe"));
				Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
				System.Threading.Thread.Sleep(1000);

				// Serialize task and output
				string xmlOutput = t.Xml;
				output.Write(xmlOutput);

				ts.RootFolder.DeleteTask(taskName);

				ts.RootFolder.ImportTask(taskName, @"C:\Users\dahall\Desktop\Test2012_07_02_08_45_46.xml");

				//ts.RootFolder.RegisterTask(taskName, xmlOutput, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
				ts.RootFolder.DeleteTask(taskName);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}
	}
}