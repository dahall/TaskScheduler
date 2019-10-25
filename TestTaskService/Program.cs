using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Schema;

namespace TestTaskService
{
	internal static class Program
	{
		private static TaskEditDialog editorForm;

		// Format for command line is: C [W|E|F|S|M|L] [1|2] [Server] [User] [Domain] [Pwd]
		[STAThread]
		private static void Main(string[] args)
		{
			if (args.Length > 0 && char.ToUpper(args[0][0]) == 'C')
			{
				var newArgs = new string[args.Length - 1];
				Array.Copy(args, 1, newArgs, 0, newArgs.Length);
				ConsoleMain(newArgs);
				return;
			}

			//System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de-DE");
			//System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Main());
		}

		internal static void EditorTest(TaskService ts, TextWriter output, params string[] arg)
		{
			try
			{
				const string taskName = "Test";
				string Converter(DateTime dt) => dt == DateTime.MinValue || dt == DateTime.MaxValue ? "Never" : dt.ToString();

				var td = arg.Length > 0 && arg[0] != null ? ts.FindTask(arg[0])?.Definition : null;
				if (td == null)
				{
					// Create a new task definition and assign properties
					td = ts.NewTask();
					td.RegistrationInfo.Description = "Test for editor\r\n\r\nLine 2";
					//td.RegistrationInfo.Author = "incaunu";
					td.Triggers.Add(new TimeTrigger());
					td.Triggers.Add(new DailyTrigger { StartBoundary = DateTime.Now + TimeSpan.FromSeconds(5), EndBoundary = DateTime.Today.AddMonths(1) });
					/*EventTrigger eTrig = new EventTrigger("Security", "VSSAudit", 25);
					eTrig.ValueQueries.Add("Name", "Value");
					td.Triggers.Add(eTrig);*/
					td.Actions.PowerShellConversion = PowerShellActionPlatformOption.All;
					td.Actions.Add("cmd.exe", "/c \"date /t > c:\\cmd.txt\"");
					td.Actions.Add(new ShowMessageAction("x", "y"));
					//EmailAction ea = (EmailAction)td.Actions.Add(new EmailAction("Hi", "dahall@codeplex.com", "someone@mail.com; another@mail.com", "<p>How you been?</p>", "smtp.codeplex.com"));
					//ea.HeaderFields.Add("reply-to", "dh@mail.com");
					//ea.Attachments = new object[] { (string)new TemporaryScopedFile(), (string)new TemporaryScopedFile() };
					//WriteXml(td, taskName);
					//using (var op = new TaskOptionsEditor(t, true, false))
					//	if (op.ShowDialog() == DialogResult.OK) td = op.TaskDefinition;
					//Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "system", null, TaskLogonType.ServiceAccount);
					//WriteXml(t);
				}

				/*using (var ad = new ActionEditDialog { AvailableActions = AvailableActions.Execute | AvailableActions.ComHandler | AvailableActions.ShowMessage })
				{
					// Test unavailable action
					try { ad.Action = new EmailAction(); } catch { output.Write("ActionEditDialog: Successfully caught expected exception."); }
					// Test available action
					ad.Action = new ComHandlerAction() { ClassId = Guid.NewGuid() };
					ad.ShowDialog();
					// Add available and test
					ad.AvailableActions |= AvailableActions.SendEmail;
					ad.Action = new EmailAction();
					ad.ShowDialog();
					// Make current type unavailable and test
					ad.AvailableActions = AvailableActions.Execute | AvailableActions.ComHandler;
					ad.ShowDialog();
					return;
				}

				using (var tdlg = new TriggerEditDialog { AvailableTriggers = (AvailableTriggers.AllTriggers & ~AvailableTriggers.Boot) })
				{
					// Test unavailable trigger
					try { tdlg.Trigger = new BootTrigger(); } catch { output.Write("TriggerEditDialog: Successfully caught expected exception."); }
					// Test available trigger
					tdlg.Trigger = new MonthlyTrigger();
					tdlg.ShowDialog();
					// Add available and test
					tdlg.AvailableTriggers |= AvailableTriggers.Boot;
					tdlg.Trigger = new BootTrigger();
					tdlg.ShowDialog();
					// Make current type unavailable and test
					tdlg.AvailableTriggers = AvailableTriggers.Weekly | AvailableTriggers.Monthly | AvailableTriggers.SessionStateChange;
					tdlg.ShowDialog();
					return;
				}*/

				// Register then show task again
				//editorForm = new TaskEditDialog(ts, td, true, true)
				var task = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
				if (editorForm == null)
				{
					editorForm = new TaskEditDialog(task, true, true)
					{
						AvailableTabs = AvailableTaskTabs.All,
						//AvailableActions = AvailableActions.ComHandler | AvailableActions.Execute,
						//AvailableTriggers = AvailableTriggers.Daily | AvailableTriggers.Weekly,
						TaskName = "Test",
						TaskNameIsEditable = false,
						ShowActionRunButton = true,
						ShowConvertActionsToPowerShellCheck = true
					};
				}
				else
					editorForm.Task = task;
				if (ts.HighestSupportedVersion >= new Version(1, 2)) editorForm.TaskFolder = @"\Microsoft";
				if (editorForm.ShowDialog() == DialogResult.OK)
				{
					var t = editorForm.Task;
					while (DisplayTask(t, true) != null)
					{
						t = editorForm.Task;
						output.Write($"***********************\r\nName: {t.Name}\r\nEnabled: {t.Enabled}\r\nLastRunTime: {Converter(t.LastRunTime)}\r\nState: {t.State}\r\nIsActive: {t.IsActive}\r\n" +
							$"Registered: {t.GetLastRegistrationTime()}\r\nNextRunTime: {Converter(t.NextRunTime)}\r\n");
						if (t.Definition.Triggers.Count > 0)
							output.Write($"TriggerStart: {Converter(t.Definition.Triggers[0].StartBoundary)}\r\nTriggerEnd: {Converter(t.Definition.Triggers[0].EndBoundary)}\r\n");
					}

					// Remove the task we just created
					t.Folder.DeleteTask(taskName, false);
				}
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void FindTask(TaskService ts, TextWriter output, params string[] arg)
		{
			try
			{
				var t = arg.Length > 0 && !string.IsNullOrEmpty(arg[0])
					? (arg[0].StartsWith("\\") ? ts.GetTask(arg[0]) : ts.FindTask(arg[0]))
					: null;
				if (t == null)
				{
					output.WriteLine($"Task '{arg[0]}' not found.");
				}
				else
				{
					output.WriteLine(
						$"Task '{t.Name}' found. Created on {t.Definition.RegistrationInfo.Date:g} and last run on {t.LastRunTime:g}.");
					if (!t.Definition.Triggers.ContainsType(typeof(CustomTrigger))) return;
					foreach (var tr in t.Definition.Triggers)
					{
						if (!(tr is CustomTrigger ct) || ct.Properties.Count <= 0) continue;
						output.WriteLine("Custom Trigger Properties:");
						var i = 0;
						foreach (var name in ct.Properties.Names)
							output.WriteLine("{0}. {1} = {2}", ++i, name, ct.Properties[name]);
					}
				}
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void FindTaskWithProperty(TaskService ts, TextWriter output, params string[] arg)
		{
			try
			{
				var tf = ts.RootFolder;
				FindTaskWithPropertyInFolder(output, tf, arg[0]);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void FluentTest(TaskService ts, TextWriter output, params string[] arg)
		{
			var t = ts.Execute("notepad.exe").WithArguments(@"c:\temp\music.txt").Once().Starting(2013, 11, 11, 11, 0, 0).RepeatingEvery(TimeSpan.FromMinutes(5)).AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").Every(2).Days().Starting("12/25/2013 7:00pm").AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").Every(3).Weeks().AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").OnAll(DaysOfTheWeek.Monday).In(WhichWeek.FirstWeek).Of(MonthsOfTheYear.January).AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").InTheMonthOf(MonthsOfTheYear.January).OnTheDays(1, 3, 5).AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").OnBoot().AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").OnIdle().AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").OnStateChange(TaskSessionStateChangeType.ConsoleConnect).AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").AtLogonOf("AMERICAS\\dahall").AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").AtTaskRegistration().AsTask("Test");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

			t = ts.Execute("notepad.exe").OnIdle().When.OnlyIfNetworkAvailable().AsTask("Test", TaskCreation.CreateOrUpdate, "dahall");
			DisplayTask(t, false);
			ts.RootFolder.DeleteTask(t.Name);

		}

		internal static void FolderTaskAction(TaskFolder fld, Action<TaskFolder> fldAction, Action<Task> taskAction, int level = 0)
		{
			fldAction?.Invoke(fld);
			if (taskAction != null)
				foreach (var task in fld.Tasks)
					taskAction(task);
			foreach (var sfld in fld.SubFolders)
				FolderTaskAction(sfld, fldAction, taskAction, level + 1);
		}

		internal static void LongTest(TaskService ts, TextWriter output, params string[] arg)
		{
			var user = WindowsIdentity.GetCurrent().Name;

			var ver = ts.HighestSupportedVersion;
			var isV12 = ver >= new Version(1, 2);
			var isV13 = ver >= new Version(1, 3);
			var isV14 = ver >= new Version(1, 4);
			output.WriteLine($"Highest version: {ver}, Library version {TaskService.LibraryVersion}");
			output.WriteLine("Server: {0} ({1}); User: {2}\\{3}", ts.TargetServer, ts.Connected ? "Connected" : "Disconnected", ts.UserAccountDomain, ts.UserName);

			output.WriteLine("Running tasks:");
			try
			{
				foreach (var rt in ts.GetRunningTasks(true))
				{
					if (rt == null) continue;
					output.WriteLine("+ {0}, {1} ({2})", rt.Name, rt.Path, rt.State);
					if (ver.Minor > 0)
						output.WriteLine("  Current Action: " + rt.CurrentAction);
				}
			}
			catch (Exception ex) { output.WriteLine("  " + ex.Message); }

			var filter = arg.Length > 0 ? arg[0] : string.Empty;
			var tf = ts.RootFolder;
			var tasks = tf.GetTasks(new Wildcard(filter));
			output.WriteLine("\nRoot folder tasks matching \"{1}\" ({0}):", tasks.Count, filter);
			foreach (var t in tasks)
				try
				{
					output.WriteLine("+ {0}, {1} ({2}) - {3}; Actions:{4}; Triggers:{5}", t.Name, t.Definition.RegistrationInfo.Author, t.State, t.Definition.Settings.Compatibility, t.Definition.Actions.Count, t.Definition.Triggers.Count);
					foreach (var trg in t.Definition.Triggers)
						output.WriteLine(" + {0}", trg);
					foreach (var act in t.Definition.Actions)
						output.WriteLine(" = {0}", act);
				}
				catch { }
			output.WriteLine("\n***Finding defrag task***");
			var ft = ts.FindTask("*defrag*");
			if (ft != null)
				output.WriteLine("Defrag task found at " + ft.Path);
			else
				output.WriteLine("Defrag task not found.");

			var tfs = tf.SubFolders;
			if (tfs.Count > 0)
			{
				output.WriteLine("\nSub folders:");
				try
				{
					foreach (var sf in tfs)
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
					catch (COMException cex) { if (cex.ErrorCode != -2147024713) throw; }
					var sub = tf.SubFolders[testFolder];
					output.WriteLine("\nSubfolder path: " + sub.Path);
					try
					{
						ts.AddTask(testFolder + @"\MyTask", new DailyTrigger(), new ExecAction("notepad"));
						output.WriteLine(" - Tasks: " + sub.Tasks.Count);
						sub.DeleteTask("MyTask");
					}
					catch (Exception ex)
					{
						output.WriteLine(ex.ToString());
					}
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
				var td = ts.NewTask();
				td.Data = "Your data";
				//td.Principal.UserId = "SYSTEM";
				//td.Principal.LogonType = TaskLogonType.ServiceAccount;
				td.Principal.Id = "Author";
				td.Principal.LogonType = isV12 ? TaskLogonType.S4U : TaskLogonType.InteractiveToken;
				td.RegistrationInfo.Author = "dahall";
				td.RegistrationInfo.Description = "Does something";
				td.RegistrationInfo.Documentation = "Don't pretend this is real.";
				td.RegistrationInfo.Source = "Test App";
				td.RegistrationInfo.URI = "test://app";
				td.RegistrationInfo.Version = new Version(0, 9);
				td.Settings.DisallowStartIfOnBatteries = true;
				td.Settings.Enabled = false;
				td.Settings.ExecutionTimeLimit = TimeSpan.Zero; // FromHours(2);
				td.Settings.Hidden = false;
				td.Settings.IdleSettings.IdleDuration = TimeSpan.FromMinutes(20);
				td.Settings.IdleSettings.RestartOnIdle = false;
				td.Settings.IdleSettings.StopOnIdleEnd = false;
				td.Settings.IdleSettings.WaitTimeout = TimeSpan.FromMinutes(10);
				td.Settings.Priority = ProcessPriorityClass.Normal;
				td.Settings.RunOnlyIfIdle = true;
				td.Settings.RunOnlyIfNetworkAvailable = true;
				td.Settings.StopIfGoingOnBatteries = true;
				if (isV12)
				{
					td.Principal.RunLevel = TaskRunLevel.Highest; //.LUA;
					td.RegistrationInfo.SecurityDescriptorSddlForm = "D:P(A;;FA;;;BA)(A;;FA;;;SY)(A;;FRFX;;;LS)";
					//td.Settings.AllowDemandStart = false;
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
					if (td.Principal.LogonType == TaskLogonType.ServiceAccount)
					{
						td.Settings.MaintenanceSettings.Exclusive = true;
						td.Settings.MaintenanceSettings.Period = TimeSpan.FromDays(5);
						td.Settings.MaintenanceSettings.Deadline = TimeSpan.FromDays(15);
					}
				}

				// Setup Triggers
				if (isV12)
				{
					var bTrigger = td.Triggers.Add(new BootTrigger { Enabled = false }); //(BootTrigger)td.Triggers.AddNew(TaskTriggerType.Boot);
					if (isV12) bTrigger.Delay = TimeSpan.FromMinutes(5);
				}

				var dTrigger = td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
				if (isV12) dTrigger.RandomDelay = TimeSpan.FromHours(2);

				if (isV12)
				{
					var eTrigger = td.Triggers.Add(new EventTrigger());
					eTrigger.Subscription = "<QueryList><Query Id=\"0\" Path=\"Security\"><Select Path=\"Security\">*[System[Provider[@Name='VSSAudit'] and EventID=25]]</Select></Query></QueryList>";
					eTrigger.ValueQueries.Add("Name", "Value");
					eTrigger.ValueQueries["Name"] = "NewValue";

					td.Triggers.Add(new RegistrationTrigger { Delay = TimeSpan.FromMinutes(5) });

					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.ConsoleConnect, user));
					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.ConsoleDisconnect));
					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.RemoteConnect));
					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.RemoteDisconnect));
					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.SessionLock, user));
					td.Triggers.Add(new SessionStateChangeTrigger(TaskSessionStateChangeType.SessionUnlock));
				}

				td.Triggers.Add(new IdleTrigger());

				var lTrigger = td.Triggers.Add(new LogonTrigger());
				if (isV12)
				{
					lTrigger.Delay = TimeSpan.FromMinutes(15);
					lTrigger.UserId = user;
					lTrigger.Repetition.Interval = TimeSpan.FromSeconds(1000);
				}

				var mTrigger = td.Triggers.Add(new MonthlyTrigger());
				mTrigger.DaysOfMonth = new[] { 3, 6, 10, 18 };
				mTrigger.MonthsOfYear = MonthsOfTheYear.July | MonthsOfTheYear.November;
				if (isV12) mTrigger.RunOnLastDayOfMonth = true;
				mTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(90);

				var mdTrigger = td.Triggers.Add(new MonthlyDOWTrigger());
				mdTrigger.DaysOfWeek = DaysOfTheWeek.AllDays;
				mdTrigger.MonthsOfYear = MonthsOfTheYear.January | MonthsOfTheYear.December;
				if (isV12) mdTrigger.RunOnLastWeekOfMonth = true;
				mdTrigger.WeeksOfMonth = WhichWeek.FirstWeek;

				var tTrigger = td.Triggers.Add(new TimeTrigger());
				tTrigger.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(1);
				tTrigger.EndBoundary = DateTime.Today + TimeSpan.FromDays(7);
				if (isV12) tTrigger.ExecutionTimeLimit = TimeSpan.FromSeconds(19);
				if (isV12) tTrigger.Id = "Time test";
				tTrigger.Repetition = new RepetitionPattern(TimeSpan.FromMinutes(17), TimeSpan.FromMinutes(21), true);

				var wTrigger = td.Triggers.Add(new WeeklyTrigger());
				wTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
				wTrigger.WeeksInterval = 3;

				// Setup Actions
				td.Actions.PowerShellConversion = PowerShellActionPlatformOption.All;
				td.Actions.Add("notepad.exe", "c:\\test.log");
				if (isV12 || (td.Actions.PowerShellConversion & PowerShellActionPlatformOption.Version1) != 0)
				{
					td.Actions.Context = "Author";
					if (td.Principal.LogonType == TaskLogonType.InteractiveToken || td.Principal.LogonType == TaskLogonType.Group || td.Principal.LogonType == TaskLogonType.S4U)
						td.Actions.Add(new ShowMessageAction("Running Notepad", "Info"));
					var email = new EmailAction("Testing", "dahall@codeplex.com", "user@test.com", "You've got mail.", "mail.myisp.com")
					{
						Id = "Email",
						Attachments = new object[] { (string)new TemporaryScopedFile() },
						Priority = MailPriority.High
					};
					td.Actions.Add(email);
					email = (EmailAction)td.Actions["Email"];
					email.To = "user@t2.com";
					//email.HeaderFields.Add("Precedence", "bulk");
					//email.HeaderFields["Importance"] = "low";
					td.Actions.Add(new ComHandlerAction(new Guid("{BF300543-7BA5-4C17-A318-9BBDB7429A21}"), @"C:\Users\dahall\Documents\Visual Studio 2010\Projects\TaskHandlerProxy\TaskHandlerSample\bin\Release\TaskHandlerSample.dll|TaskHandlerSample.TaskHandler|MoreData"));
				}
				td.Actions[0] = new ExecAction("notepad.exe", "c:\\test2.log");

				// Validate and Register task
				WriteXml(td, "PreRegTest");
				td.Validate(true);
				var t = tf.RegisterTaskDefinition("Test1", td);
				WriteXml(t);

				// Try copying it
				var td2 = ts.NewTask();
				td2.Actions.PowerShellConversion = td.Actions.PowerShellConversion;
				td2.Triggers.AddRange(td.Triggers.ToArray());
				td2.Actions.AddRange(td.Actions.ToArray());
				var t2 = tf.RegisterTaskDefinition("Test2", td2, TaskCreation.CreateOrUpdate, user, null, TaskLogonType.InteractiveToken);
				WriteXml(t2.Definition, "ReRegTest");
				tf.DeleteTask("Test2");

				/*// Create raw task for V1 test with cleared and added actions
				td2 = ts.NewTask();
				td2.Actions.Clear();
				System.Diagnostics.Debug.Assert(td2.Actions.Count == 0);
				td2.Triggers.Add(new TimeTrigger(DateTime.Now.AddDays(1)));
				td2.Actions.Add("calc");
				System.Diagnostics.Debug.Assert(td2.Actions.Count == 1);
				t2 = tf.RegisterTaskDefinition("Test2", td2);
				tf.DeleteTask("Test2");*/
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
				return;
			}

			// Display results
			var runningTask = tf.Tasks["Test1"];
			if (isV12 || (runningTask.Definition.Actions.PowerShellConversion & PowerShellActionPlatformOption.Version1) != 0)
				Debug.Assert(runningTask.Definition.Actions.Count == 4);
			else
				Debug.Assert(runningTask.Definition.Actions.Count == 1);
			WriteXml(runningTask.Definition, "RunRegTest");
			output.WriteLine("\nNew task will next run at " + runningTask.NextRunTime);
			var times = runningTask.GetRunTimes(DateTime.Now, DateTime.Now + TimeSpan.FromDays(7));
			if (times.Length > 0)
			{
				output.WriteLine("\nNew task will run at the following times over the next week:");
				foreach (var dt in times)
					output.WriteLine("  {0}", dt);
			}
			output.WriteLine("\nNew task triggers:");
			for (var i = 0; i < runningTask.Definition.Triggers.Count; i++)
				output.WriteLine("  {0}: {1}", i, runningTask.Definition.Triggers[i]);
			output.WriteLine("\nNew task actions:");
			for (var i = 0; i < runningTask.Definition.Actions.Count; i++)
				output.WriteLine("  {0}: {1}", i, runningTask.Definition.Actions[i]);

			// Loop through event logs for this task and find action completed events newest to oldest
			if (isV12)
			{
				output.WriteLine("\nTask history enumeration:");
				var log = new TaskEventLog(@"\Microsoft\Windows\Autochk\Proxy", new[] { 201 }, DateTime.Now.AddDays(-7)) { EnumerateInReverse = false };
				foreach (var ev in log)
					output.WriteLine("  Completed action '{0}' ({2}) at {1}.", ev.DataValues["ActionName"], ev.TimeCreated.Value, ev.DataValues["ResultCode"]);
			}

			// Run ComHandler
			//TaskService.RunComHandlerActionAsync(new Guid("CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734"), i => output.WriteLine("Com task complete."), "5", 120000, (p, s) => output.WriteLine($"Com task running: {p}% complete = {s}"));

			if (arg.Length > 0 && arg[0] == "new")
				// Show on new editor
				new TaskOptionsEditor(runningTask).ShowDialog();
			else
				// Show on traditional editor
				DisplayTask(runningTask, true);

			tf.DeleteTask("Test1");
		}

		internal static void MMCTest(TaskService ts, TextWriter output, params string[] arg)
		{
		}

		internal static void OutputJson(TaskService ts, StringWriter output)
		{
#if NET_35_OR_GREATER
			try
			{
				using (var tt = new TempTask(ts, "Temp"))
				{
					/*var ms = new System.IO.MemoryStream();
					var js = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(TaskDefinition));
					js.WriteObject(ms, tt.Task.Definition);
					ms.Position = 0;
					var sr = new System.IO.StreamReader(ms);
					output.WriteLine(sr.ReadToEnd());*/
				}
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
#endif // NET_35_OR_GREATER
		}

		internal static void OutputXml(TaskService ts, StringWriter output)
		{
			// Get the service on the local machine
			try
			{
				TaskDefinition.GetV1SchemaFile(new XmlSchemaSet());

				// Create a new task definition and assign properties
				const string taskName = "Test";
				string fn;
				using (var tt = new TempTask(ts, taskName))
				{
					Thread.Sleep(1000);

					// Serialize task and output
					var xmlOutput = tt.Task.Xml;
					output.Write(xmlOutput);
					fn = WriteXml(tt);
				}

				if (fn != null)
					ts.RootFolder.ImportTask(taskName, fn);
				ts.RootFolder.DeleteTask(taskName);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void ShortTest(TaskService ts, TextWriter output, params string[] arg)
		{
			// Get the service on the local machine
			try
			{
				/*string sub = "<QueryList><Query Id=\"0\" Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
					"<Select Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
					"*[System[Provider[@Name='Microsoft-Windows-TaskScheduler'] and (Computer='dahall1') and (Level=0 or Level=4) and (Task=100 or Task=101) and (EventID=129) and Security[@UserID='AMERICAS\\dahall'] and TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]" +
					"*[EventData[Data[@Name='TaskName']='\\Maint' and Data[@Name='EventCode']='0']]" +
					"</Select>" +
					"</Query></QueryList>";
				using (var ed = new EventActionFilterEditor() { Subscription = sub })
				{
					ed.ShowDialog();
				}
				return;*/

				/*Action<string> d = delegate(string s) { var ar = s.Split('|'); foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(ar[2], @"\(A;(?<Flag>\w*);(?<Right>\w*);(?<Guid>\w*);(?<OIGuid>\w*);(?<Acct>[\w\-\d]*)(?:;[^\)]*)?\)")) output.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", ar[0], ar[1], m.Groups["Flag"], m.Groups["Right"], m.Groups["Guid"], m.Groups["OIGuid"], m.Groups["Acct"]); };
				FolderTaskAction(ts.RootFolder, delegate(TaskFolder f) { d("F|" + f.Name + "|" + f.GetSecurityDescriptorSddlForm()); }, delegate(Task s) { d("T|" + s.Name + "|" + s.GetSecurityDescriptorSddlForm()); });
				return;*/

				//FolderTaskAction(ts.RootFolder, null, delegate(Task tsk) { if (tsk.Definition.Triggers.ContainsType(typeof(CustomTrigger))) output.WriteLine(tsk.Path); });

				// Create a new task definition and assign properties
				//string[] names = arg[0].Split('\\');
				var taskName = "TesterTask";

				//string taskFolder = (names.Length == 1 || names[0].Length == 0) ? "\\" : names[0];

				var td = ts.NewTask();
				//td.RegistrationInfo.Description = "some description";
				td.Triggers.Add(new MonthlyTrigger
				{
					Repetition = new RepetitionPattern(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(10))
				});
				td.Actions.Add("cmd.exe", "-someparameter");
				//td.Actions.Add(new ComHandlerAction(new Guid("CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734"), ""));

				//td.Triggers.Add(new RegistrationTrigger { Delay = TimeSpan.FromSeconds(8), EndBoundary = DateTime.Now + TimeSpan.FromSeconds(20) });
				//td.Triggers.Add(new WeeklyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(2), DaysOfWeek = DaysOfTheWeek.Friday, Enabled = false, EndBoundary = DateTime.Today.AddDays(30) });

				//td.Settings.StartWhenAvailable = true;
				//td.Settings.MultipleInstances = TaskInstancesPolicy.StopExisting;
				//td.Settings.DisallowStartIfOnBatteries = false;
				//td.Settings.StopIfGoingOnBatteries = false;
				//td.Settings.IdleSettings.StopOnIdleEnd = false;
				//td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromSeconds(5);

				//TaskFolder testFolder = ts.RootFolder.CreateFolder(taskFolder, null, false);
				//var t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "Everyone", null, TaskLogonType.Group);
				//var t = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
				var t = ts.RootFolder.RegisterTaskDefinition(taskName, td);
				var xml = t.Xml;

				var td2 = ts.NewTask();
				td2.XmlText = xml;

				//TaskDefinition td = ts.NewTask();
				//td.RegistrationInfo.Documentation = "Does something";
				//td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
				//td.Principal.LogonType = TaskLogonType.InteractiveToken;

				// Add a cron trigger
				//td.Triggers.AddRange(Trigger.FromCronFormat("15 */6 */30 * *"));

				// Add a trigger that will fire the task at this time every other day
				/*DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
				dt.Repetition.Duration = TimeSpan.FromHours(4);
				dt.Repetition.Interval = TimeSpan.FromHours(1);

				// Add a trigger that will fire every week on Friday
				td.Triggers.Add(new WeeklyTrigger { StartBoundary = DateTime.Today + TimeSpan.FromHours(2), DaysOfWeek = DaysOfTheWeek.Friday, Enabled = false });

				// Add message and email actions
				if (ts.HighestSupportedVersion >= new Version(1, 2))
				{
					ShowMessageAction sm = (ShowMessageAction)td.Actions.AddNew(TaskActionType.ShowMessage);
					sm.Title = "title";
					sm.MessageBody = "body";

					EmailAction ma = new EmailAction("Subject", "x@x.com", "y@y.com; z@z.com", "Body", "mail.google.com") { Bcc = "c@c.com", Cc = "b@b.com" };
					ma.Attachments = new object[] { (string)new TemporaryScopedFile() };
					ma.HeaderFields.Add("N1", "V1");
					ma.HeaderFields.Add("N2", "V2");
					td.Actions.Add(ma);
				}

				// Add an action that will launch Notepad whenever the trigger fires
				td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));
				output.WriteLine(td.XmlText);
				Task t = ts.RootFolder.RegisterTaskDefinition(taskName, td); //, TaskCreation.CreateOrUpdate, "username", "password", TaskLogonType.Password);
				t.Enabled = false;
				*/
				//System.Threading.Thread.Sleep(15000);
				output.WriteLine("LastTime & Result: {0} ({1:x})",
					t.LastRunTime == DateTime.MinValue ? "Never" : t.LastRunTime.ToString("g"), t.LastTaskResult);
				output.WriteLine("NextRunTime: {0}", t.NextRunTime == DateTime.MinValue ? "None" : t.NextRunTime.ToString("g"));
				//System.Threading.Thread.Sleep(10000);
				//DisplayTask(t, true);
				/*using (var dlg = new TaskOptionsEditor { Editable = true })
				{
					dlg.Initialize(t);
					dlg.ShowDialog();
				}*/

				// Retrieve the task, add a trigger and save it.
				t = ts.GetTask(taskName);
				//ts.RootFolder.DeleteTask(taskName);
				td = t.Definition;
				td.Triggers.Clear();
				if (td.Triggers.AddNew(TaskTriggerType.Weekly) is WeeklyTrigger wt)
				{
					wt.DaysOfWeek = DaysOfTheWeek.Friday;
					wt.EndBoundary = DateTime.Today.AddYears(1);
				}
				//((ExecAction)td.Actions[0]).Path = "calc.exe";
				t.RegisterChanges();
				/*t = ts.RootFolder.RegisterTaskDefinition(taskName, td);
				output.WriteLine("Principal: {1}; Triggers: {0}", t.Definition.Triggers, t.Definition.Principal);*/
				ts.RootFolder.DeleteTask(taskName);
				//ts.RootFolder.DeleteFolder(taskFolder, false);
				output.WriteLine("Task removed.");
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		internal static void WizardTest(TaskService ts, TextWriter output, params string[] arg)
		{
			try
			{
				var FolderName = "My Folder";
				var v2 = ts.HighestSupportedVersion > new Version(1, 1);
				var taskFolder = ts.RootFolder;
				if (v2)
					try
					{
						taskFolder = ts.GetFolder(FolderName);
					}
					catch (FileNotFoundException)
					{
						taskFolder = ts.RootFolder.CreateFolder(FolderName);
					}

				using (var taskSchedulerWizard = new TaskSchedulerWizard())
				{
					var newTaskDefinition = ts.NewTask();
					newTaskDefinition.Actions.Add(new ExecAction("notepad.exe"));
					taskSchedulerWizard.Initialize(ts, newTaskDefinition);
					taskSchedulerWizard.TaskFolder = FolderName;
					taskSchedulerWizard.RegisterTaskOnFinish = true;
					taskSchedulerWizard.AvailableTriggers = TaskSchedulerWizard.AvailableWizardTriggers.Event |
															TaskSchedulerWizard.AvailableWizardTriggers.Logon;
					taskSchedulerWizard.AvailablePages = TaskSchedulerWizard.AvailableWizardPages.IntroPage |
														 TaskSchedulerWizard.AvailableWizardPages.TriggerSelectPage |
														 TaskSchedulerWizard.AvailableWizardPages.TriggerEditPage |
														 //TaskSchedulerWizard.AvailableWizardPages.TriggerPropertiesPage |
														 TaskSchedulerWizard.AvailableWizardPages.ActionEditPage |
														 //TaskSchedulerWizard.AvailableWizardPages.SecurityPage |
														 TaskSchedulerWizard.AvailableWizardPages.SummaryPage;

					if (taskSchedulerWizard.ShowDialog() == DialogResult.OK)
						taskFolder.DeleteTask(taskSchedulerWizard.Task.Name);
					// _tlv.Tasks = taskFolder.Tasks;
				}

				// Create a new task definition and assign properties
				/*TaskSchedulerWizard wiz = new TaskSchedulerWizard(ts, null, true) { TaskFolder = @"\Microsoft" };
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
					ts.RootFolder.DeleteTask(wiz.Task.Path);*/
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.ToString());
			}
		}

		private static void ConsoleMain(string[] args)
		{
			var init = 0;
			var test = 'L';
			if (args.Length > 0 && char.IsLetter(args[0][0]))
			{
				test = args[0].ToUpper()[0];
				init++;
			}
			var newArgs = new[] { args.Length > init ? args[init] : "2", null, null, null, null, null };
			for (var i = init + 1; i < init + 5; i++)
				if (args.Length > i) newArgs[i] = args[i];

			Application.EnableVisualStyles();
			using (var ts =
				new TaskService(newArgs[1], newArgs[2], newArgs[3], newArgs[4], newArgs[0] == "1") { AllowReadOnlyTasks = true })
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
						Console.Read();
						break;

					case 'S':
						ShortTest(ts, Console.Out);
						Console.Read();
						break;

					case 'M':
						MMCTest(ts, Console.Out);
						break;

					case 'T':
						new ScriptTestDlg { TaskService = ts }.ShowDialog();
						break;

					default:
						LongTest(ts, Console.Out);
						Console.Read();
						break;
				}
			}
		}

		private static TaskDefinition DisplayTask(Task t, bool editable)
		{
			if (editorForm == null)
				editorForm = new TaskEditDialog();
			editorForm.Editable = editable;
			editorForm.Initialize(t);
			editorForm.RegisterTaskOnAccept = editable;
			editorForm.AvailableTabs = AvailableTaskTabs.All;
			editorForm.ShowActionRunButton = true;
			editorForm.ShowConvertActionsToPowerShellCheck = true;
			return editorForm.ShowDialog() == DialogResult.OK ? editorForm.TaskDefinition : null;
		}

		private static TaskDefinition DisplayTask(TaskService ts, TaskDefinition td, bool editable)
		{
			if (editorForm == null)
				editorForm = new TaskEditDialog();
			editorForm.Editable = editable;
			editorForm.Initialize(ts, td);
			editorForm.RegisterTaskOnAccept = editable;
			editorForm.AvailableTabs = AvailableTaskTabs.All;
			editorForm.ShowActionRunButton = true;
			editorForm.ShowConvertActionsToPowerShellCheck = true;
			return editorForm.ShowDialog() == DialogResult.OK ? editorForm.TaskDefinition : null;
		}

		private static void FindTaskWithComAction(TextWriter output, TaskFolder tf)
		{
			foreach (var t in tf.Tasks)
				foreach (var ac in t.Definition.Actions)
				{
					var a = ac as ComHandlerAction;
					if (a == null)
						continue;
					string name = null, model = null, path = null, asm = null;
					try
					{
						var k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + a.ClassId.ToString("B")) ??
								Registry.ClassesRoot.OpenSubKey("Wow6432Node\\CLSID\\" + a.ClassId.ToString("B"));
						name = k?.GetValue(null, "").ToString();
						var sk = k.OpenSubKey("InprocServer32");
						path = sk.GetValue(null, "").ToString();
						if (!string.IsNullOrEmpty(path))
							try
							{
								AssemblyName.GetAssemblyName(path);
								asm = "Yes";
							}
							catch
							{
								asm = "No";
							}
						model = sk.GetValue("ThreadingModel", "").ToString();
					}
					catch
					{
					}
					output.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", t.Path, t.Name, a.ClassId, a.Data, name, path, model,
						asm);
				}
			foreach (var f in tf.SubFolders)
				FindTaskWithComAction(output, f);
		}

		private static void FindTaskWithPropertyInFolder(TextWriter output, TaskFolder tf, string arg, Match match = null)
		{
			if (match == null)
				match = Regex.Match(arg, "^(\\.?\\w+)+\\s*(==|!=)\\s*\\\"([^\"]*)\\\"$");
			if (!match.Success)
				return;

			foreach (var t in tf.Tasks)
				try
				{
					object lastObj = t;
					int i;
					for (i = 0; i < match.Groups[1].Captures.Count && lastObj != null; i++)
					{
						var prop = match.Groups[1].Captures[i].Value.TrimStart('.');
						var pi = lastObj.GetType().GetProperty(prop);
						if (pi == null)
						{
							output.WriteLine("Unable to locate property {0}", prop);
							return;
						}
						lastObj = pi.GetValue(lastObj, null);
					}
					if (i == match.Groups[1].Captures.Count)
					{
						var res = lastObj?.ToString() ?? string.Empty;
						var found = res.Equals(match.Groups[3].Value, StringComparison.InvariantCultureIgnoreCase);
						if (match.Groups[2].Value == "!=")
							found = !found;
						if (found)
							output.WriteLine($"+ {t.Path} ({t.State})({t.Definition.Settings.Compatibility})\n\r== {res}");
					}
				}
				catch
				{
				}

			var tfs = tf.SubFolders;
			if (tfs.Count > 0)
				try
				{
					foreach (var sf in tfs)
						FindTaskWithPropertyInFolder(output, sf, arg, match);
				}
				catch (Exception ex)
				{
					output.WriteLine(ex.ToString());
				}
		}

		private static string GetTempXmlFile(string taskName)
		{
			return Path.Combine(Path.GetTempPath(),
				taskName + DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss") + ".xml");
		}

		private static string WriteXml(Task t)
		{
			var fn = GetTempXmlFile(t.Name);
			t.Export(fn);
			return fn;
		}

		private static string WriteXml(TaskDefinition td, string name)
		{
			var fn = GetTempXmlFile(name);
			File.WriteAllText(fn, td.XmlText, Encoding.Unicode);
			return fn;
		}

		internal class TemporaryScopedFile : IDisposable
		{
			private readonly string fn;

			public TemporaryScopedFile()
			{
				fn = Path.GetTempFileName();
			}

			public TemporaryScopedFile(string ext, string fileName = null, string content = null)
			{
				fn = Path.Combine(Path.GetTempPath(), string.Concat(Guid.NewGuid().ToString(), ".", ext));
				using (var f = File.CreateText(fn))
				{
					if (content != null)
						f.Write(content);
				}
			}

			public static implicit operator string(TemporaryScopedFile tsf) => tsf.ToString();

			public void Dispose()
			{
				if (File.Exists(fn))
					File.Delete(fn);
			}

			public override string ToString() => fn;
		}

		private class TempTask : IDisposable
		{
			private readonly string taskName;
			private readonly TaskService ts;

			public TempTask(TaskService ts, string taskName)
			{
				this.ts = ts;
				this.taskName = taskName;

				var td = ts.NewTask();
				td.Data = "Some data";
				td.Settings.DeleteExpiredTaskAfter = TimeSpan.FromHours(12);
				td.Settings.IdleSettings.RestartOnIdle = true;
				td.RegistrationInfo.Author = "Me";
				td.Triggers.Add(new BootTrigger());
				td.Triggers.Add(new LogonTrigger());
				td.Triggers.Add(new IdleTrigger());
				td.Triggers.Add(new TimeTrigger { Enabled = false, EndBoundary = DateTime.Now.AddYears(1), Repetition = new RepetitionPattern(TimeSpan.FromHours(1), TimeSpan.FromHours(4)) });
				td.Triggers.Add(new DailyTrigger(3) { Enabled = false, Repetition = new RepetitionPattern(TimeSpan.FromHours(2), TimeSpan.FromHours(24)) });
				td.Triggers.Add(new MonthlyDOWTrigger { DaysOfWeek = DaysOfTheWeek.AllDays, MonthsOfYear = MonthsOfTheYear.AllMonths, WeeksOfMonth = WhichWeek.FirstWeek, RunOnLastWeekOfMonth = true });
				td.Triggers.Add(new MonthlyTrigger { DaysOfMonth = new[] { 3, 6, 9 }, RunOnLastDayOfMonth = true, MonthsOfYear = MonthsOfTheYear.April });
				td.Triggers.Add(new WeeklyTrigger(DaysOfTheWeek.Saturday, 2));
				td.Actions.Add(new ExecAction("notepad.exe"));
				Task = ts.RootFolder.RegisterTaskDefinition(taskName, td, TaskCreation.CreateOrUpdate, "SYSTEM", null, TaskLogonType.ServiceAccount);
			}

			public Task Task { get; }

			public static implicit operator Task(TempTask tt) => tt.Task;

			public void Dispose()
			{
				ts.RootFolder.DeleteTask(taskName);
			}
		}
	}
}