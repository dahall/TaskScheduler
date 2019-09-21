using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Win32.TaskScheduler.Models
{
	/// <summary></summary>
	public interface IAction
	{
		/// <summary>Gets the type of the action.</summary>
		/// <value>The type of the action.</value>
		TaskActionType ActionType { get; }

		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		string Id { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="IList{IAction}"/>
	/// <seealso cref="IList"/>
	public interface IActionCollection : IList<IAction>, IList
	{
		/// <summary>Gets or sets the context.</summary>
		/// <value>The context.</value>
		string Context { get; set; }

		/// <summary>Adds the new.</summary>
		/// <param name="actionType">Type of the action.</param>
		/// <returns></returns>
		IAction AddNew(TaskActionType actionType);
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IBootTrigger : ITrigger { }

	/// <summary></summary>
	/// <seealso cref="IAction"/>
	public interface IComHandlerAction : IAction
	{
		/// <summary>Gets or sets the class identifier.</summary>
		/// <value>The class identifier.</value>
		Guid ClassId { get; set; }

		/// <summary>Gets or sets the data.</summary>
		/// <value>The data.</value>
		string Data { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface ICustomTrigger : ITrigger
	{
		/// <summary>Gets the name.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Name { get; }

		/// <summary>Gets the properties.</summary>
		/// <value>Returns a <see cref="IDictionary{TKey, TValue}"/> value.</value>
		IDictionary<string, string> Properties { get; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IDailyTrigger : ITrigger
	{
		/// <summary>Gets or sets the days interval.</summary>
		/// <value>The days interval.</value>
		short DaysInterval { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="IAction"/>
	public interface IEmailAction : IAction
	{
		/// <summary>Gets or sets the attachments.</summary>
		/// <value>The attachments.</value>
		string[] Attachments { get; set; }

		/// <summary>Gets or sets the BCC.</summary>
		/// <value>The BCC.</value>
		string Bcc { get; set; }

		/// <summary>Gets or sets the body.</summary>
		/// <value>The body.</value>
		string Body { get; set; }

		/// <summary>Gets or sets the cc.</summary>
		/// <value>The cc.</value>
		string Cc { get; set; }

		/// <summary>Gets or sets from.</summary>
		/// <value>From.</value>
		string From { get; set; }

		/// <summary>Gets the header fields.</summary>
		/// <value>Returns a <see cref="IDictionary{TKey, TValue}"/> value.</value>
		IDictionary<string, string> HeaderFields { get; }

		/// <summary>Gets or sets the priority.</summary>
		/// <value>The priority.</value>
		System.Net.Mail.MailPriority Priority { get; set; }

		/// <summary>Gets or sets the reply to.</summary>
		/// <value>The reply to.</value>
		string ReplyTo { get; set; }

		/// <summary>Gets or sets the server.</summary>
		/// <value>The server.</value>
		string Server { get; set; }

		/// <summary>Gets or sets the subject.</summary>
		/// <value>The subject.</value>
		string Subject { get; set; }

		/// <summary>Gets or sets to.</summary>
		/// <value>To.</value>
		string To { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IEventTrigger : ITrigger
	{
		/// <summary>Gets or sets the subscription.</summary>
		/// <value>The subscription.</value>
		string Subscription { get; set; }

		/// <summary>Gets the value queries.</summary>
		/// <value>Returns a <see cref="IDictionary{TKey, TValue}"/> value.</value>
		IDictionary<string, string> ValueQueries { get; }
	}

	/// <summary></summary>
	/// <seealso cref="IAction"/>
	public interface IExecAction : IAction
	{
		/// <summary>Gets or sets the arguments.</summary>
		/// <value>The arguments.</value>
		string Arguments { get; set; }

		/// <summary>Gets or sets the path.</summary>
		/// <value>The path.</value>
		string Path { get; set; }

		/// <summary>Gets or sets the working directory.</summary>
		/// <value>The working directory.</value>
		string WorkingDirectory { get; set; }
	}

	/// <summary></summary>
	public interface IIdleSettings
	{
		/// <summary>Gets or sets the idle duration nullable.</summary>
		/// <value>The idle duration nullable.</value>
		TimeSpan? IdleDurationNullable { get; set; }

		/// <summary>Gets or sets a value indicating whether [restart on idle].</summary>
		/// <value><see langword="true"/> if [restart on idle]; otherwise, <see langword="false"/>.</value>
		bool RestartOnIdle { get; set; }

		/// <summary>Gets or sets a value indicating whether [stop on idle end].</summary>
		/// <value><see langword="true"/> if [stop on idle end]; otherwise, <see langword="false"/>.</value>
		bool StopOnIdleEnd { get; set; }

		/// <summary>Gets or sets the wait timeout nullable.</summary>
		/// <value>The wait timeout nullable.</value>
		TimeSpan? WaitTimeoutNullable { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IIdleTrigger : ITrigger { }

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface ILogonTrigger : ITrigger
	{
		/// <summary>Gets or sets the user identifier.</summary>
		/// <value>The user identifier.</value>
		string UserId { get; set; }
	}

	/// <summary></summary>
	public interface IMaintenanceSettings
	{
		/// <summary>Gets or sets the deadline nullable.</summary>
		/// <value>The deadline nullable.</value>
		TimeSpan? DeadlineNullable { get; set; }

		/// <summary>Gets or sets a value indicating whether this <see cref="IMaintenanceSettings"/> is exclusive.</summary>
		/// <value><see langword="true"/> if exclusive; otherwise, <see langword="false"/>.</value>
		bool Exclusive { get; set; }

		/// <summary>Gets or sets the period nullable.</summary>
		/// <value>The period nullable.</value>
		TimeSpan? PeriodNullable { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IMonthlyDOWTrigger : ITrigger
	{
		/// <summary>Gets or sets the days of week.</summary>
		/// <value>The days of week.</value>
		DaysOfTheWeek DaysOfWeek { get; set; }

		/// <summary>Gets or sets the months of year.</summary>
		/// <value>The months of year.</value>
		MonthsOfTheYear MonthsOfYear { get; set; }

		/// <summary>Gets or sets a value indicating whether [run on last week of month].</summary>
		/// <value><see langword="true"/> if [run on last week of month]; otherwise, <see langword="false"/>.</value>
		bool RunOnLastWeekOfMonth { get; set; }

		/// <summary>Gets or sets the weeks of month.</summary>
		/// <value>The weeks of month.</value>
		WhichWeek WeeksOfMonth { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IMonthlyTrigger : ITrigger
	{
		/// <summary>Gets or sets the days of month.</summary>
		/// <value>The days of month.</value>
		int[] DaysOfMonth { get; set; }

		/// <summary>Gets or sets the months of year.</summary>
		/// <value>The months of year.</value>
		MonthsOfTheYear MonthsOfYear { get; set; }

		/// <summary>Gets or sets a value indicating whether [run on last day of month].</summary>
		/// <value><see langword="true"/> if [run on last day of month]; otherwise, <see langword="false"/>.</value>
		bool RunOnLastDayOfMonth { get; set; }
	}

	/// <summary></summary>
	public interface INetworkSettings
	{
		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		Guid Id { get; set; }

		/// <summary>Gets or sets the name.</summary>
		/// <value>The name.</value>
		string Name { get; set; }
	}

	/// <summary></summary>
	public interface IPrincipal
	{
		/// <summary>Gets or sets the display name.</summary>
		/// <value>The display name.</value>
		string DisplayName { get; set; }

		/// <summary>Gets or sets the group identifier.</summary>
		/// <value>The group identifier.</value>
		string GroupId { get; set; }

		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		string Id { get; set; }

		/// <summary>Gets or sets the user identifier.</summary>
		/// <value>The user identifier.</value>
		string UserId { get; set; }
	}

	/// <summary></summary>
	public interface IRegisteredTask
	{
		/// <summary>Gets the definition.</summary>
		/// <value>Returns a <see cref="ITaskDefinition"/> value.</value>
		ITaskDefinition Definition { get; }

		/// <summary>Gets or sets a value indicating whether this <see cref="IRegisteredTask"/> is enabled.</summary>
		/// <value><see langword="true"/> if enabled; otherwise, <see langword="false"/>.</value>
		bool Enabled { get; set; }

		/// <summary>Gets the folder.</summary>
		/// <value>Returns a <see cref="ITaskFolder"/> value.</value>
		ITaskFolder Folder { get; }

		/// <summary>Gets the last run time.</summary>
		/// <value>Returns a <see cref="Nullable{DateTime}"/> value.</value>
		DateTime? LastRunTime { get; }

		/// <summary>Gets the last task result.</summary>
		/// <value>Returns a <see cref="Nullable{Int32}"/> value.</value>
		int? LastTaskResult { get; }

		/// <summary>Gets the name.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Name { get; }

		/// <summary>Gets the next run time.</summary>
		/// <value>Returns a <see cref="Nullable{DateTime}"/> value.</value>
		DateTime? NextRunTime { get; }

		/// <summary>Gets the number of missed runs.</summary>
		/// <value>Returns a <see cref="Nullable{Int32}"/> value.</value>
		int? NumberOfMissedRuns { get; }

		/// <summary>Gets the path.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Path { get; }

		/// <summary>Gets the state.</summary>
		/// <value>Returns a <see cref="TaskState"/> value.</value>
		TaskState State { get; }

		/// <summary>Gets the XML.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Xml { get; }

		/// <summary>Gets the instances.</summary>
		/// <returns></returns>
		IReadOnlyList<IRunningTask> GetInstances();

		/// <summary>Gets the run times.</summary>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		IReadOnlyList<DateTime> GetRunTimes(DateTime start, DateTime end, uint count);

		/// <summary>Gets the security descriptor.</summary>
		/// <param name="includeSections">The include sections.</param>
		/// <returns></returns>
		string GetSecurityDescriptor(System.Security.AccessControl.SecurityInfos includeSections);

		/// <summary>Runs the specified parameters.</summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		IRunningTask Run(params string[] parameters);

		/// <summary>Runs the ex.</summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="flags">The flags.</param>
		/// <param name="sessionId">The session identifier.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		IRunningTask RunEx(string[] parameters, TaskRunFlags flags, int sessionId, string user);

		/// <summary>Sets the security descriptor.</summary>
		/// <param name="sddl">The SDDL.</param>
		/// <param name="options">The options.</param>
		void SetSecurityDescriptor(string sddl, TaskSetSecurityOptions options);

		/// <summary>Stops this instance.</summary>
		void Stop();
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IRegistrationTrigger : ITrigger { }

	/// <summary></summary>
	public interface IRepetitionPattern
	{
		/// <summary>Gets or sets the duration nullable.</summary>
		/// <value>The duration nullable.</value>
		TimeSpan? DurationNullable { get; set; }

		/// <summary>Gets or sets the interval nullable.</summary>
		/// <value>The interval nullable.</value>
		TimeSpan? IntervalNullable { get; set; }

		/// <summary>Gets or sets a value indicating whether [stop at duration end].</summary>
		/// <value><see langword="true"/> if [stop at duration end]; otherwise, <see langword="false"/>.</value>
		bool StopAtDurationEnd { get; set; }
	}

	/// <summary></summary>
	public interface IRunningTask
	{
		/// <summary>Gets the current action.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string CurrentAction { get; }

		/// <summary>Gets the engine pid.</summary>
		/// <value>Returns a <see cref="uint"/> value.</value>
		uint EnginePID { get; }

		/// <summary>Gets the instance unique identifier nullable.</summary>
		/// <value>Returns a <see cref="Nullable{Guid}"/> value.</value>
		Guid? InstanceGuidNullable { get; }

		/// <summary>Gets the name.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Name { get; }

		/// <summary>Gets the path.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Path { get; }

		/// <summary>Gets the state.</summary>
		/// <value>Returns a <see cref="TaskState"/> value.</value>
		TaskState State { get; }

		/// <summary>Refreshes this instance.</summary>
		void Refresh();

		/// <summary>Stops this instance.</summary>
		void Stop();
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface ISessionStateChangeTrigger : ITrigger
	{
		/// <summary>Gets or sets the state change.</summary>
		/// <value>The state change.</value>
		TaskSessionStateChangeType StateChange { get; set; }

		/// <summary>Gets or sets the user identifier.</summary>
		/// <value>The user identifier.</value>
		string UserId { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="IAction"/>
	public interface IShowMessageAction : IAction
	{
		/// <summary>Gets or sets the message body.</summary>
		/// <value>The message body.</value>
		string MessageBody { get; set; }

		/// <summary>Gets or sets the title.</summary>
		/// <value>The title.</value>
		string Title { get; set; }
	}

	/// <summary></summary>
	public interface ITaskDefinition
	{
		/// <summary>Gets the actions.</summary>
		/// <value>Returns a <see cref="ActionCollection"/> value.</value>
		ActionCollection Actions { get; }

		/// <summary>Gets or sets the data.</summary>
		/// <value>The data.</value>
		string Data { get; set; }

		/// <summary>Gets the lowest supported version.</summary>
		/// <value>Returns a <see cref="TaskCompatibility"/> value.</value>
		TaskCompatibility LowestSupportedVersion { get; }

		/// <summary>Gets the principal.</summary>
		/// <value>Returns a <see cref="TaskPrincipal"/> value.</value>
		TaskPrincipal Principal { get; }

		/// <summary>Gets the registration information.</summary>
		/// <value>Returns a <see cref="TaskRegistrationInfo"/> value.</value>
		TaskRegistrationInfo RegistrationInfo { get; }

		/// <summary>Gets the settings.</summary>
		/// <value>Returns a <see cref="TaskSettings"/> value.</value>
		TaskSettings Settings { get; }

		/// <summary>Gets the triggers.</summary>
		/// <value>Returns a <see cref="TriggerCollection"/> value.</value>
		TriggerCollection Triggers { get; }

		/// <summary>Validates the specified throw exception.</summary>
		/// <param name="throwException">if set to <see langword="true"/> [throw exception].</param>
		/// <returns></returns>
		bool Validate(bool throwException = false);
	}

	/// <summary></summary>
	public interface ITaskFolder
	{
		/// <summary>Gets the name.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Name { get; }

		/// <summary>Gets the path.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string Path { get; }

		/// <summary>Creates the folder.</summary>
		/// <param name="subFolderName">Name of the sub folder.</param>
		/// <param name="sddl">The SDDL.</param>
		/// <returns></returns>
		ITaskFolder CreateFolder(string subFolderName, string sddl);

		/// <summary>Deletes the folder.</summary>
		/// <param name="subFolderName">Name of the sub folder.</param>
		void DeleteFolder(string subFolderName);

		/// <summary>Deletes the task.</summary>
		/// <param name="Name">The name.</param>
		void DeleteTask(string Name);

		/// <summary>Gets the folder.</summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		ITaskFolder GetFolder(string path);

		/// <summary>Gets the folders.</summary>
		/// <returns></returns>
		ICollection<ITaskFolder> GetFolders();

		/// <summary>Gets the security descriptor.</summary>
		/// <param name="includeSections">The include sections.</param>
		/// <returns></returns>
		string GetSecurityDescriptor(System.Security.AccessControl.SecurityInfos includeSections);

		/// <summary>Gets the task.</summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		IRegisteredTask GetTask(string path);

		/// <summary>Gets the tasks.</summary>
		/// <param name="includeHidden">if set to <see langword="true"/> [include hidden].</param>
		/// <returns></returns>
		IReadOnlyList<IRegisteredTask> GetTasks(bool includeHidden = false);

		/// <summary>Registers the task.</summary>
		/// <param name="path">The path.</param>
		/// <param name="xmlText">The XML text.</param>
		/// <param name="flags">The flags.</param>
		/// <param name="userId">The user identifier.</param>
		/// <param name="password">The password.</param>
		/// <param name="logonType">Type of the logon.</param>
		/// <param name="sddl">The SDDL.</param>
		/// <returns></returns>
		IRegisteredTask RegisterTask(string path, string xmlText, TaskCreation flags, string userId, string password, TaskLogonType logonType, string sddl);

		/// <summary>Registers the task definition.</summary>
		/// <param name="path">The path.</param>
		/// <param name="pDefinition">The p definition.</param>
		/// <param name="flags">The flags.</param>
		/// <param name="userId">The user identifier.</param>
		/// <param name="password">The password.</param>
		/// <param name="logonType">Type of the logon.</param>
		/// <param name="sddl">The SDDL.</param>
		/// <returns></returns>
		IRegisteredTask RegisterTaskDefinition(string path, ITaskDefinition pDefinition, TaskCreation flags, string userId, string password, TaskLogonType logonType, string sddl);

		/// <summary>Sets the security descriptor.</summary>
		/// <param name="sddl">The SDDL.</param>
		/// <param name="options">The options.</param>
		void SetSecurityDescriptor(string sddl, TaskSetSecurityOptions options);
	}

	/// <summary></summary>
	public interface ITaskService
	{
		/// <summary>Gets a value indicating whether this <see cref="ITaskService"/> is connected.</summary>
		/// <value><see langword="true"/> if connected; otherwise, <see langword="false"/>.</value>
		bool Connected { get; }

		/// <summary>Gets the connected domain.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string ConnectedDomain { get; }

		/// <summary>Gets the connected user.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string ConnectedUser { get; }

		/// <summary>Gets the highest version.</summary>
		/// <value>Returns a <see cref="Version"/> value.</value>
		Version HighestVersion { get; }

		/// <summary>Gets the root folder.</summary>
		/// <value>Returns a <see cref="ITaskFolder"/> value.</value>
		ITaskFolder RootFolder { get; }

		/// <summary>Gets the target server.</summary>
		/// <value>Returns a <see cref="string"/> value.</value>
		string TargetServer { get; }

		/// <summary>Connects the specified server name.</summary>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="user">The user.</param>
		/// <param name="domain">The domain.</param>
		/// <param name="password">The password.</param>
		void Connect(string serverName, string user, string domain, string password);

		/// <summary>Gets the folder.</summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		ITaskFolder GetFolder(string path);

		/// <summary>Gets the running tasks.</summary>
		/// <param name="includeHidden">if set to <see langword="true"/> [include hidden].</param>
		/// <returns></returns>
		IReadOnlyList<IRunningTask> GetRunningTasks(bool includeHidden = false);

		/// <summary>Gets the task.</summary>
		/// <param name="fullPath">The full path.</param>
		/// <returns></returns>
		IRegisteredTask GetTask(string fullPath);

		/// <summary>News the task.</summary>
		/// <returns></returns>
		ITaskDefinition NewTask();
	}

	/// <summary></summary>
	public interface ITaskSettings
	{
		/// <summary>Gets or sets a value indicating whether [allow demand start].</summary>
		/// <value><see langword="true"/> if [allow demand start]; otherwise, <see langword="false"/>.</value>
		bool AllowDemandStart { get; set; }

		/// <summary>Gets or sets a value indicating whether [allow hard terminate].</summary>
		/// <value><see langword="true"/> if [allow hard terminate]; otherwise, <see langword="false"/>.</value>
		bool AllowHardTerminate { get; set; }

		/// <summary>Gets or sets the compatibility.</summary>
		/// <value>The compatibility.</value>
		TaskCompatibility Compatibility { get; set; }

		/// <summary>Gets or sets the delete expired task after.</summary>
		/// <value>The delete expired task after.</value>
		TimeSpan? DeleteExpiredTaskAfter { get; set; }

		/// <summary>Gets or sets a value indicating whether [disallow start if on batteries].</summary>
		/// <value><see langword="true"/> if [disallow start if on batteries]; otherwise, <see langword="false"/>.</value>
		bool DisallowStartIfOnBatteries { get; set; }

		/// <summary>Gets or sets a value indicating whether [disallow start on remote application session].</summary>
		/// <value><see langword="true"/> if [disallow start on remote application session]; otherwise, <see langword="false"/>.</value>
		bool DisallowStartOnRemoteAppSession { get; set; }

		/// <summary>Gets or sets a value indicating whether this <see cref="ITaskSettings"/> is enabled.</summary>
		/// <value><see langword="true"/> if enabled; otherwise, <see langword="false"/>.</value>
		bool Enabled { get; set; }

		/// <summary>Gets or sets the execution time limit.</summary>
		/// <value>The execution time limit.</value>
		TimeSpan? ExecutionTimeLimit { get; set; }

		/// <summary>Gets or sets a value indicating whether this <see cref="ITaskSettings"/> is hidden.</summary>
		/// <value><see langword="true"/> if hidden; otherwise, <see langword="false"/>.</value>
		bool Hidden { get; set; }

		/// <summary>Gets the idle settings.</summary>
		/// <value>Returns a <see cref="IIdleSettings"/> value.</value>
		IIdleSettings IdleSettings { get; }

		/// <summary>Gets the maintenance settings.</summary>
		/// <value>Returns a <see cref="IMaintenanceSettings"/> value.</value>
		IMaintenanceSettings MaintenanceSettings { get; }

		/// <summary>Gets or sets the multiple instances.</summary>
		/// <value>The multiple instances.</value>
		TaskInstancesPolicy MultipleInstances { get; set; }

		/// <summary>Gets the network settings.</summary>
		/// <value>Returns a <see cref="INetworkSettings"/> value.</value>
		INetworkSettings NetworkSettings { get; }

		/// <summary>Gets or sets the priority.</summary>
		/// <value>The priority.</value>
		System.Diagnostics.ProcessPriorityClass Priority { get; set; }

		/// <summary>Gets or sets the restart count.</summary>
		/// <value>The restart count.</value>
		int? RestartCount { get; set; }

		/// <summary>Gets or sets the restart interval.</summary>
		/// <value>The restart interval.</value>
		TimeSpan? RestartInterval { get; set; }

		/// <summary>Gets or sets a value indicating whether [run only if idle].</summary>
		/// <value><see langword="true"/> if [run only if idle]; otherwise, <see langword="false"/>.</value>
		bool RunOnlyIfIdle { get; set; }

		/// <summary>Gets or sets a value indicating whether [run only if network available].</summary>
		/// <value><see langword="true"/> if [run only if network available]; otherwise, <see langword="false"/>.</value>
		bool RunOnlyIfNetworkAvailable { get; set; }

		/// <summary>Gets or sets a value indicating whether [start when available].</summary>
		/// <value><see langword="true"/> if [start when available]; otherwise, <see langword="false"/>.</value>
		bool StartWhenAvailable { get; set; }

		/// <summary>Gets or sets a value indicating whether [stop if going on batteries].</summary>
		/// <value><see langword="true"/> if [stop if going on batteries]; otherwise, <see langword="false"/>.</value>
		bool StopIfGoingOnBatteries { get; set; }

		/// <summary>Gets or sets a value indicating whether [use unified scheduling engine].</summary>
		/// <value><see langword="true"/> if [use unified scheduling engine]; otherwise, <see langword="false"/>.</value>
		bool UseUnifiedSchedulingEngine { get; set; }

		/// <summary>Gets or sets a value indicating whether this <see cref="ITaskSettings"/> is volatile.</summary>
		/// <value><see langword="true"/> if volatile; otherwise, <see langword="false"/>.</value>
		bool Volatile { get; set; }

		/// <summary>Gets or sets a value indicating whether [wake to run].</summary>
		/// <value><see langword="true"/> if [wake to run]; otherwise, <see langword="false"/>.</value>
		bool WakeToRun { get; set; }
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface ITimeTrigger : ITrigger { }

	/// <summary></summary>
	public interface ITrigger
	{
		/// <summary>Gets or sets a value indicating whether this <see cref="ITrigger"/> is enabled.</summary>
		/// <value><see langword="true"/> if enabled; otherwise, <see langword="false"/>.</value>
		bool Enabled { get; set; }

		/// <summary>Gets or sets the end boundary nullable.</summary>
		/// <value>The end boundary nullable.</value>
		DateTime? EndBoundaryNullable { get; set; }

		/// <summary>Gets or sets the execution time limit nullable.</summary>
		/// <value>The execution time limit nullable.</value>
		TimeSpan? ExecutionTimeLimitNullable { get; set; }

		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		string Id { get; set; }

		/// <summary>Gets the repetition.</summary>
		/// <value>Returns a <see cref="IRepetitionPattern"/> value.</value>
		IRepetitionPattern Repetition { get; }

		/// <summary>Gets or sets the start boundary.</summary>
		/// <value>The start boundary.</value>
		DateTime StartBoundary { get; set; }

		/// <summary>Gets the type of the trigger.</summary>
		/// <value>The type of the trigger.</value>
		TaskTriggerType TriggerType { get; }
	}

	/// <summary></summary>
	/// <seealso cref="IList{ITrigger}"/>
	/// <seealso cref="IList"/>
	public interface ITriggerCollection : IList<ITrigger>, IList
	{
		/// <summary>Adds the new.</summary>
		/// <param name="actionType">Type of the action.</param>
		/// <returns></returns>
		ITrigger AddNew(TaskTriggerType actionType);
	}

	/// <summary></summary>
	/// <seealso cref="ITrigger"/>
	public interface IWeeklyTrigger : ITrigger
	{
		/// <summary>Gets or sets the days of week.</summary>
		/// <value>The days of week.</value>
		DaysOfTheWeek DaysOfWeek { get; set; }

		/// <summary>Gets or sets the weeks interval.</summary>
		/// <value>The weeks interval.</value>
		short WeeksInterval { get; set; }
	}
}