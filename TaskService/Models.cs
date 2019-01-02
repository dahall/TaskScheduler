using System;
using System.Collections.Generic;

namespace Microsoft.Win32.TaskScheduler.Models
{
	public interface IAction
	{
		TaskActionType ActionType { get; }
		string Id { get; set; }
	}

	public interface IActionCollection : IList<IAction>, System.Collections.IList
	{
		string Context { get; set; }
		IAction AddNew(TaskActionType actionType);
	}

	public interface IBootTrigger : ITrigger { }

	public interface IComHandlerAction : IAction
	{
		Guid ClassId { get; set; }
		string Data { get; set; }
	}

	public interface ICustomTrigger : ITrigger
	{
		string Name { get; }
		IDictionary<string, string> Properties { get; }
	}

	public interface IDailyTrigger : ITrigger
	{
		short DaysInterval { get; set; }
	}

	public interface IEmailAction : IAction
	{
		string Server { get; set; }
		string Subject { get; set; }
		string To { get; set; }
		string Cc { get; set; }
		string Bcc { get; set; }
		string ReplyTo { get; set; }
		string From { get; set; }
		System.Net.Mail.MailPriority Priority { get; set; }
		IDictionary<string, string> HeaderFields { get; }
		string Body { get; set; }
		string[] Attachments { get; set; }
	}

	public interface IEventTrigger : ITrigger
	{
		string Subscription { get; set; }
		IDictionary<string, string> ValueQueries { get; }
	}

	public interface IExecAction : IAction
	{
		string Arguments { get; set; }
		string Path { get; set; }
		string WorkingDirectory { get; set; }
	}

	public interface IIdleSettings
	{
		TimeSpan? IdleDurationNullable { get; set; }
		bool RestartOnIdle { get; set; }
		bool StopOnIdleEnd { get; set; }
		TimeSpan? WaitTimeoutNullable { get; set; }
	}

	public interface IIdleTrigger : ITrigger { }

	public interface ILogonTrigger : ITrigger
	{
		string UserId { get; set; }
	}

	public interface IMaintenanceSettings
	{
		TimeSpan? DeadlineNullable { get; set; }
		bool Exclusive { get; set; }
		TimeSpan? PeriodNullable { get; set; }
	}

	public interface IMonthlyDOWTrigger : ITrigger
	{
		DaysOfTheWeek DaysOfWeek { get; set; }
		MonthsOfTheYear MonthsOfYear { get; set; }
		bool RunOnLastWeekOfMonth { get; set; }
		WhichWeek WeeksOfMonth { get; set; }
	}

	public interface IMonthlyTrigger : ITrigger
	{
		int[] DaysOfMonth { get; set; }
		MonthsOfTheYear MonthsOfYear { get; set; }
		bool RunOnLastDayOfMonth { get; set; }
	}

	public interface INetworkSettings
	{
		Guid Id { get; set; }
		string Name { get; set; }
	}

	public interface IPrincipal
	{
		string DisplayName { get; set; }
		string GroupId { get; set; }
		string Id { get; set; }
		string UserId { get; set; }
	}

	public interface IRegisteredTask
	{
		ITaskDefinition Definition { get; }
		bool Enabled { get; set; }
		ITaskFolder Folder { get; }
		DateTime? LastRunTime { get; }
		int? LastTaskResult { get; }
		string Name { get; }
		DateTime? NextRunTime { get; }
		int? NumberOfMissedRuns { get; }
		string Path { get; }
		TaskState State { get; }
		string Xml { get; }
		IReadOnlyList<IRunningTask> GetInstances();
		IReadOnlyList<DateTime> GetRunTimes(DateTime start, DateTime end, uint count);
		string GetSecurityDescriptor(System.Security.AccessControl.SecurityInfos includeSections);
		IRunningTask Run(params string[] parameters);
		IRunningTask RunEx(string[] parameters, TaskRunFlags flags, int sessionId, string user);
		void SetSecurityDescriptor(string sddl, TaskSetSecurityOptions options);
		void Stop();
	}

	public interface IRegistrationTrigger : ITrigger { }

	public interface IRepetitionPattern
	{
		TimeSpan? DurationNullable { get; set; }
		TimeSpan? IntervalNullable { get; set; }
		bool StopAtDurationEnd { get; set; }
	}

	public interface IRunningTask
	{
		string CurrentAction { get; }
		uint EnginePID { get; }
		Guid? InstanceGuidNullable { get; }
		string Name { get; }
		string Path { get; }
		TaskState State { get; }
		void Refresh();
		void Stop();
	}

	public interface ISessionStateChangeTrigger : ITrigger
	{
		TaskSessionStateChangeType StateChange { get; set; }
		string UserId { get; set; }
	}

	public interface IShowMessageAction : IAction
	{
		string MessageBody { get; set; }
		string Title { get; set; }
	}

	public interface ITaskDefinition
	{
		ActionCollection Actions { get; }
		string Data { get; set; }
		TaskCompatibility LowestSupportedVersion { get; }
		TaskPrincipal Principal { get; }
		TaskRegistrationInfo RegistrationInfo { get; }
		TaskSettings Settings { get; }
		TriggerCollection Triggers { get; }

		bool Validate(bool throwException = false);
	}

	public interface ITaskFolder
	{
		string Name { get; }
		string Path { get; }
		ITaskFolder CreateFolder(string subFolderName, string sddl);
		void DeleteFolder(string subFolderName);
		void DeleteTask(string Name);
		ITaskFolder GetFolder(string path);
		ICollection<ITaskFolder> GetFolders();
		string GetSecurityDescriptor(System.Security.AccessControl.SecurityInfos includeSections);
		IRegisteredTask GetTask(string path);
		IReadOnlyList<IRegisteredTask> GetTasks(bool includeHidden = false);
		IRegisteredTask RegisterTask(string path, string xmlText, TaskCreation flags, string userId, string password, TaskLogonType logonType, string sddl);
		IRegisteredTask RegisterTaskDefinition(string path, ITaskDefinition pDefinition, TaskCreation flags, string userId, string password, TaskLogonType logonType, string sddl);
		void SetSecurityDescriptor(string sddl, TaskSetSecurityOptions options);
	}

	public interface ITaskService
	{
		bool Connected { get; }
		string ConnectedDomain { get; }
		string ConnectedUser { get; }
		Version HighestVersion { get; }
		ITaskFolder RootFolder { get; }
		string TargetServer { get; }
		void Connect(string serverName, string user, string domain, string password);
		ITaskFolder GetFolder(string path);
		IReadOnlyList<IRunningTask> GetRunningTasks(bool includeHidden = false);
		IRegisteredTask GetTask(string fullPath);
		ITaskDefinition NewTask();
	}

	public interface ITaskSettings
	{
		bool AllowDemandStart { get; set; }
		bool AllowHardTerminate { get; set; }
		TaskCompatibility Compatibility { get; set; }
		TimeSpan? DeleteExpiredTaskAfter { get; set; }
		bool DisallowStartIfOnBatteries { get; set; }
		bool DisallowStartOnRemoteAppSession { get; set; }
		bool Enabled { get; set; }
		TimeSpan? ExecutionTimeLimit { get; set; }
		bool Hidden { get; set; }
		IIdleSettings IdleSettings { get; }
		IMaintenanceSettings MaintenanceSettings { get; }
		TaskInstancesPolicy MultipleInstances { get; set; }
		INetworkSettings NetworkSettings { get; }
		System.Diagnostics.ProcessPriorityClass Priority { get; set; }
		int? RestartCount { get; set; }
		TimeSpan? RestartInterval { get; set; }
		bool RunOnlyIfIdle { get; set; }
		bool RunOnlyIfNetworkAvailable { get; set; }
		bool StartWhenAvailable { get; set; }
		bool StopIfGoingOnBatteries { get; set; }
		bool UseUnifiedSchedulingEngine { get; set; }
		bool Volatile { get; set; }
		bool WakeToRun { get; set; }
	}

	public interface ITimeTrigger : ITrigger { }

	public interface ITriggerCollection : IList<ITrigger>, System.Collections.IList
	{
		ITrigger AddNew(TaskTriggerType actionType);
	}

	public interface ITrigger
	{
		bool Enabled { get; set; }
		DateTime? EndBoundaryNullable { get; set; }
		TimeSpan? ExecutionTimeLimitNullable { get; set; }
		string Id { get; set; }
		IRepetitionPattern Repetition { get; }
		DateTime StartBoundary { get; set; }
		TaskTriggerType TriggerType { get; }
	}

	public interface IWeeklyTrigger : ITrigger
	{
		DaysOfTheWeek DaysOfWeek { get; set; }
		short WeeksInterval { get; set; }
	}
}