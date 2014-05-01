Imports Microsoft.Win32.TaskScheduler

Module Module1

	Sub Main()
		SimpleExample()
	End Sub

	Sub ShowEditor()
		Using ts As New TaskService()
			' Create a new task
			Const taskName As String = "Test"
			Dim t As Task = ts.AddTask(taskName, New TimeTrigger(DateTime.Now + TimeSpan.FromHours(1)),
				New ExecAction("notepad.exe", "c:\test.log", "C:\"))

			' Edit task and re-register if user clicks Ok
			Dim editorForm As New TaskEditDialog(t)
			editorForm.ShowDialog()

			' Remove the task we just created
			ts.RootFolder.DeleteTask(taskName)
		End Using
	End Sub

	Sub SimpleExample()
		Using ts As New TaskService()
			' Create a new task definition and assign properties
			Const taskName As String = "Test"
			Dim td As TaskDefinition = ts.NewTask
			td.RegistrationInfo.Description = "Does something"

			' Add a trigger that will fire every other week on Monday and Saturday and
			' repeat every 10 minutes for the following 11 hours
			Dim wt As New WeeklyTrigger()
			wt.DaysOfWeek = DaysOfTheWeek.Monday Or DaysOfTheWeek.Saturday
			wt.WeeksInterval = 2
			wt.StartBoundary = Now
			wt.Repetition.Duration = TimeSpan.FromHours(11)
			wt.Repetition.Interval = TimeSpan.FromMinutes(10)
			td.Triggers.Add(wt)

			' Add an action (shorthand) that runs Notepad
			td.Actions.Add(New ExecAction("notepad.exe", "C:\Test.log"))

			' Register the task in the root folder
			ts.RootFolder.RegisterTaskDefinition(taskName, td)

			' Remove the task we just created
			ts.RootFolder.DeleteTask(taskName)
		End Using
	End Sub

End Module
