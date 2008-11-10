Imports Microsoft.Win32.TaskScheduler

Module Module1

    Sub Main()
        Dim ts As New TaskService(Nothing, Nothing, Nothing, Nothing, True)
        Dim td As TaskDefinition = ts.NewTask
        td.RegistrationInfo.Description = "Test"
        Dim wt As New WeeklyTrigger()
        wt.DaysOfWeek = DaysOfTheWeek.Monday Or DaysOfTheWeek.Tuesday Or DaysOfTheWeek.Wednesday Or DaysOfTheWeek.Thursday Or DaysOfTheWeek.Friday Or DaysOfTheWeek.Saturday
        wt.WeeksInterval = 2
        wt.StartBoundary = Now
        wt.Repetition.Duration = TimeSpan.FromMinutes(660)
        wt.Repetition.Interval = TimeSpan.FromMinutes(10)
        td.Triggers.Add(wt)
        'Dim tt As New TimeTrigger()
        'tt.Repetition.Duration = TimeSpan.FromMinutes(660)
        'tt.Repetition.Interval = TimeSpan.FromMinutes(10)
        'td.Triggers.Add(tt)
        td.Actions.Add(New ExecAction("notepad.exe", "E:\Temp\Test.log", Nothing))
        ts.RootFolder.RegisterTaskDefinition("Test", td)
        ts.RootFolder.DeleteTask("Test")
    End Sub

End Module
