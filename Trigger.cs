using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Defines the type of triggers that can be used by tasks.</summary>
	public enum TaskTriggerType
	{
		/// <summary>Triggers the task when the computer boots.</summary>
		Boot = 8,
		/// <summary>Triggers the task on a daily schedule.</summary>
		Daily = 2,
		/// <summary>Triggers the task when a specific event occurs. Version 1.2 only.</summary>
		Event = 0,
		/// <summary>Triggers the task when the computer goes into an idle state.</summary>
		Idle = 6,
		/// <summary>Triggers the task when a specific user logs on.</summary>
		Logon = 9,
		/// <summary>Triggers the task on a monthly schedule.</summary>
		Monthly = 4,
		/// <summary>Triggers the task on a monthly day-of-week schedule.</summary>
		MonthlyDOW = 5,
		/// <summary>Triggers the task when the task is registered. Version 1.2 only.</summary>
		Registration = 7,
		/// <summary>Triggers the task when a specific user session state changes. Version 1.2 only.</summary>
		SessionStateChange = 11,
		/// <summary>Triggers the task at a specific time of day.</summary>
		Time = 1,
		/// <summary>Triggers the task on a weekly schedule.</summary>
		Weekly = 3
	}

	/// <summary>Values for days of the week (Monday, Tuesday, etc.)</summary>
	[Flags]
	public enum DaysOfTheWeek : short
	{
		/// <summary>Sunday</summary>
		Sunday = 0x1,
		/// <summary>Monday</summary>
		Monday = 0x2,
		/// <summary>Tuesday</summary>
		Tuesday = 0x4,
		/// <summary>Wednesday</summary>
		Wednesday = 0x8,
		/// <summary>Thursday</summary>
		Thursday = 0x10,
		/// <summary>Friday</summary>
		Friday = 0x20,
		/// <summary>Saturday</summary>
		Saturday = 0x40,
		/// <summary>All days</summary>
		AllDays = 0x7F
	}

	/// <summary>Values for week of month (first, second, ..., last)</summary>
	public enum WhichWeek : short
	{
		/// <summary>First week of the month</summary>
		FirstWeek = 1,
		/// <summary>Second week of the month</summary>
		SecondWeek = 2,
		/// <summary>Third week of the month</summary>
		ThirdWeek = 4,
		/// <summary>Fourth week of the month</summary>
		FourthWeek = 8,
		/// <summary>Fifth week of the month</summary>
		FifthWeek = 0x10,
		/// <summary>Last week of the month</summary>
		LastWeek = 0x20,
		/// <summary>Last week of the month</summary>
		AllWeeks = 0x1F
	}

	/// <summary>Values for months of the year (January, February, etc.)</summary>
	[Flags]
	public enum MonthsOfTheYear : short
	{
		/// <summary>January</summary>
		January = 0x1,
		/// <summary>February</summary>
		February = 0x2,
		/// <summary>March</summary>
		March = 0x4,
		/// <summary>April</summary>
		April = 0x8,
		/// <summary>May</summary>
		May = 0x10,
		/// <summary>June</summary>
		June = 0x20,
		/// <summary>July</summary>
		July = 0x40,
		/// <summary>August</summary>
		August = 0x80,
		/// <summary>September</summary>
		September = 0x100,
		/// <summary>October</summary>
		October = 0x200,
		/// <summary>November</summary>
		November = 0x400,
		/// <summary>December</summary>
		December = 0x800,
		/// <summary>All months</summary>
		AllMonths = 0xFFF
	}

	/// <summary>
	/// Provides the common properties that are inherited by all trigger classes.
	/// </summary>
	public abstract class Trigger : IDisposable
	{
		internal V1Interop.ITaskTrigger v1Trigger = null;
		internal V1Interop.TaskTrigger v1TriggerData;
		internal V2Interop.ITrigger v2Trigger = null;
		internal TaskTriggerType ttype;

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTriggerType type) :
			this(trigger, trigger.GetTrigger())
		{
			v1TriggerData.TriggerSize = (ushort)Marshal.SizeOf(typeof(V1Interop.TaskTrigger));
			v1TriggerData.Type = type;
			v1TriggerData.Flags = 0;
		}

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTrigger data)
		{
			v1Trigger = trigger;
			v1TriggerData = data;
		}

		internal Trigger(V2Interop.ITrigger iTrigger)
		{
			v2Trigger = iTrigger;
			this.StartBoundary = DateTime.Now;
		}

		internal Trigger(TaskTriggerType triggerType)
		{
			this.ttype = triggerType;
			unboundValues = new Dictionary<string, object>();

			v1TriggerData = new V1Interop.TaskTrigger();
			v1TriggerData.TriggerSize = (ushort)Marshal.SizeOf(typeof(V1Interop.TaskTrigger));
			try { v1TriggerData.Type = ConvertToV1TriggerType(this.ttype); } catch { }

			this.StartBoundary = DateTime.Now;
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public virtual void Dispose()
		{
			if (v2Trigger != null)
				Marshal.ReleaseComObject(v2Trigger);
			if (v1Trigger != null)
				Marshal.ReleaseComObject(v1Trigger);
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		protected Dictionary<string, object> unboundValues;

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual bool Bound
		{
			get
			{
				if (v1Trigger != null)
					return v1Trigger.GetTrigger().Equals(v1TriggerData);
				return (v2Trigger != null);
			}
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual void Bind(V1Interop.ITask iTask)
		{
			if (v1Trigger == null)
			{
				ushort idx;
				v1Trigger = iTask.CreateTrigger(out idx);
			}
			SetV1TriggerData();
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual void Bind(V2Interop.ITaskDefinition iTaskDef)
		{
			V2Interop.ITriggerCollection iTriggers = iTaskDef.Triggers;
			v2Trigger = iTriggers.Create(ttype);
			Marshal.ReleaseComObject(iTriggers);
			foreach (string key in unboundValues.Keys)
			{
				try
				{
					object o = unboundValues[key];
					if (o is TimeSpan)
						o = Task.TimeSpanToString((TimeSpan)o);
					if (o is DateTime)
						o = ((DateTime)o).ToString(V2BoundaryDateFormat);
					v2Trigger.GetType().InvokeMember(key, System.Reflection.BindingFlags.SetProperty, null, v2Trigger, new object[] { o });
				}
				catch (System.Reflection.TargetInvocationException tie) { throw tie.InnerException; }
				catch { }
			}
			unboundValues.Clear();
			unboundValues = null;
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal void SetV1TriggerData()
		{
			if (v1Trigger != null)
				v1Trigger.SetTrigger(ref v1TriggerData);
		}

		internal static V1Interop.TaskTriggerType ConvertToV1TriggerType(TaskTriggerType type)
		{
			if (type == TaskTriggerType.Registration || type == TaskTriggerType.Event || type == TaskTriggerType.SessionStateChange)
				throw new NotV1SupportedException();
			int v1tt = (int)type - 1;
			if (v1tt >= 7) v1tt--;
			return (V1Interop.TaskTriggerType)v1tt;
		}

		internal static Trigger CreateTrigger(V1Interop.ITaskTrigger trigger)
		{
			return CreateTrigger(trigger, trigger.GetTrigger().Type);
		}

		internal static Trigger CreateTrigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTriggerType triggerType)
		{
			switch (triggerType)
			{
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunOnce:
					return new TimeTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunDaily:
					return new DailyTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunWeekly:
					return new WeeklyTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunMonthly:
					return new MonthlyTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunMonthlyDOW:
					return new MonthlyDOWTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnIdle:
					return new IdleTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnSystemStart:
					return new BootTrigger(trigger);
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnLogon:
					return new LogonTrigger(trigger);
				default:
					break;
			}
			return null;
		}

		internal static Trigger CreateTrigger(V2Interop.ITrigger iTrigger)
		{
			switch (iTrigger.Type)
			{
				case TaskTriggerType.Boot:
					return new BootTrigger((V2Interop.IBootTrigger)iTrigger);
				case TaskTriggerType.Daily:
					return new DailyTrigger((V2Interop.IDailyTrigger)iTrigger);
				case TaskTriggerType.Event:
					return new EventTrigger((V2Interop.IEventTrigger)iTrigger);
				case TaskTriggerType.Idle:
					return new IdleTrigger((V2Interop.IIdleTrigger)iTrigger);
				case TaskTriggerType.Logon:
					return new LogonTrigger((V2Interop.ILogonTrigger)iTrigger);
				case TaskTriggerType.Monthly:
					return new MonthlyTrigger((V2Interop.IMonthlyTrigger)iTrigger);
				case TaskTriggerType.MonthlyDOW:
					return new MonthlyDOWTrigger((V2Interop.IMonthlyDOWTrigger)iTrigger);
				case TaskTriggerType.Registration:
					return new RegistrationTrigger((V2Interop.IRegistrationTrigger)iTrigger);
				case TaskTriggerType.SessionStateChange:
					return new SessionStateChangeTrigger((V2Interop.ISessionStateChangeTrigger)iTrigger);
				case TaskTriggerType.Time:
					return new TimeTrigger((V2Interop.ITimeTrigger)iTrigger);
				case TaskTriggerType.Weekly:
					return new WeeklyTrigger((V2Interop.IWeeklyTrigger)iTrigger);
				default:
					break;
			}
			return null;
		}

		/// <summary>
		/// Gets or sets the identifier for the trigger. Cannot set with Task Scheduler 1.0.
		/// </summary>
		public string Id
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Id;
				if (v1Trigger != null)
					return v1Trigger.GetTriggerString();
				return (unboundValues.ContainsKey("Id") ? (string)unboundValues["Id"] : null);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.Id = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["Id"] = value;
			}
		}

		private RepetitionPattern repititionPattern = null;
		/// <summary>
		/// Gets a <see cref="RepetitionPattern"/> instance that indicates how often the task is run and how long the repetition pattern is repeated after the task is started.
		/// </summary>
		public RepetitionPattern Repetition
		{
			get
			{
				if (repititionPattern == null)
				{
					if (this.v2Trigger != null)
						repititionPattern = new RepetitionPattern(this.v2Trigger.Repetition);
					else
						repititionPattern = new RepetitionPattern(this.v1Trigger, this.v1TriggerData);
				}
				return repititionPattern;
			}
		}

		/// <summary>
		/// Gets or sets the maximum amount of time that the task launched by this trigger is allowed to run. Not available with Task Scheduler 1.0.
		/// </summary>
		public TimeSpan ExecutionTimeLimit
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(v2Trigger.ExecutionTimeLimit);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("ExecutionTimeLimit") ? (TimeSpan)unboundValues["ExecutionTimeLimit"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.ExecutionTimeLimit = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["ExecutionTimeLimit"] = value;
			}
		}

		internal const string V2BoundaryDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFF";

		/// <summary>
		/// Gets or sets the date and time when the trigger is activated.
		/// </summary>
		public DateTime StartBoundary
		{
			get
			{
				if (v2Trigger != null)
					return DateTime.Parse(v2Trigger.StartBoundary);
				return v1TriggerData.BeginDate;
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.StartBoundary = value.ToString(V2BoundaryDateFormat);
				else
				{
					v1TriggerData.BeginDate = value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["StartBoundary"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the date and time when the trigger is deactivated. The trigger cannot start the task after it is deactivated.
		/// </summary>
		public DateTime EndBoundary
		{
			get
			{
				if (v2Trigger != null)
					return DateTime.Parse(v2Trigger.EndBoundary);
				return v1TriggerData.EndDate;
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.EndBoundary = value.ToString(V2BoundaryDateFormat);
				else
				{
					v1TriggerData.EndDate = value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["EndBoundary"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates whether the trigger is enabled.
		/// </summary>
		public bool Enabled
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Enabled;
				return ((v1TriggerData.Flags & V1Interop.TaskTriggerFlags.Disabled) != V1Interop.TaskTriggerFlags.Disabled);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.Enabled = value;
				else
				{
					if (!value)
						v1TriggerData.Flags |= V1Interop.TaskTriggerFlags.Disabled;
					else
						v1TriggerData.Flags &= ~V1Interop.TaskTriggerFlags.Disabled;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["Enabled"] = value;
				}
			}
		}

		/// <summary>
		/// Returns a string representing this trigger.
		/// </summary>
		/// <returns>String value of trigger.</returns>
		public override string ToString()
		{
			return this.Id;
		}
	}

	/// <summary>
	/// Defines how often the task is run and how long the repetition pattern is repeated after the task is started.
	/// </summary>
	public class RepetitionPattern : IDisposable
	{
		private V1Interop.ITaskTrigger v1Trigger = null;
		private V1Interop.TaskTrigger v1TriggerData;
		private V2Interop.IRepetitionPattern v2Pattern = null;

		internal RepetitionPattern(V1Interop.ITaskTrigger trigger, V1Interop.TaskTrigger data) { v1Trigger = trigger; v1TriggerData = data; }
		internal RepetitionPattern(V2Interop.IRepetitionPattern pattern) { v2Pattern = pattern; }

		internal void Bind()
		{
			if (v1Trigger != null)
				v1Trigger.SetTrigger(ref v1TriggerData);
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Pattern != null) Marshal.ReleaseComObject(v2Pattern);
			v1Trigger = null;
		}

		/// <summary>
		/// Gets or sets the amount of time between each restart of the task.
		/// </summary>
		public TimeSpan Interval
		{
			get
			{
				if (v2Pattern != null)
					return Task.StringToTimeSpan(v2Pattern.Interval);
				return TimeSpan.FromMinutes(v1TriggerData.MinutesInterval);
			}
			set
			{
				if (v2Pattern != null)
					v2Pattern.Interval = Task.TimeSpanToString(value);
				else
				{
					v1TriggerData.MinutesInterval = (uint)value.TotalMinutes;
					Bind();
				}
			}
		}

		/// <summary>
		/// Gets or sets how long the pattern is repeated.
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				if (v2Pattern != null)
					return Task.StringToTimeSpan(v2Pattern.Duration);
				return TimeSpan.FromMinutes(v1TriggerData.MinutesDuration);
			}
			set
			{
				if (v2Pattern != null)
					v2Pattern.Duration = Task.TimeSpanToString(value);
				else
				{
					v1TriggerData.MinutesDuration = (uint)value.TotalMinutes;
					Bind();
				}
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates if a running instance of the task is stopped at the end of repetition pattern duration.
		/// </summary>
		public bool StopAtDurationEnd
		{
			get
			{
				if (v2Pattern != null)
					return v2Pattern.StopAtDurationEnd;
				return (v1TriggerData.Flags & V1Interop.TaskTriggerFlags.KillAtDurationEnd) == V1Interop.TaskTriggerFlags.KillAtDurationEnd;
			}
			set
			{
				if (v2Pattern != null)
					v2Pattern.StopAtDurationEnd = value;
				else
				{
					if (value)
						v1TriggerData.Flags |= V1Interop.TaskTriggerFlags.KillAtDurationEnd;
					else
						v1TriggerData.Flags &= ~V1Interop.TaskTriggerFlags.KillAtDurationEnd;
					Bind();
				}
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when the system is booted.
	/// </summary>
	public class BootTrigger : Trigger
	{
		internal BootTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnSystemStart) { }
		internal BootTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="BootTrigger"/>.
		/// </summary>
		public BootTrigger() : base(TaskTriggerType.Boot) { }

		/// <summary>
		/// Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.
		/// </summary>
		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IBootTrigger)v2Trigger).Delay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("Delay") ? (TimeSpan)unboundValues["Delay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IBootTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["Delay"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when a system event occurs. Not available on Task Scheduler 1.0.
	/// </summary>
	public class EventTrigger : Trigger
	{
		internal EventTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="EventTrigger"/>.
		/// </summary>
		public EventTrigger() : base(TaskTriggerType.Event) { }

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			base.Bind(iTaskDef);
			if (nvc != null)
				nvc.Bind(((V2Interop.IEventTrigger)v2Trigger).ValueQueries);
		}

		/// <summary>
		/// Gets or sets the XPath query string that identifies the event that fires the trigger.
		/// </summary>
		public string Subscription
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IEventTrigger)v2Trigger).Subscription;
				return (unboundValues.ContainsKey("Subscription") ? (string)unboundValues["Subscription"] : null);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IEventTrigger)v2Trigger).Subscription = value;
				else
					unboundValues["Subscription"] = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.
		/// </summary>
		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IEventTrigger)v2Trigger).Delay);
				return (unboundValues.ContainsKey("Delay") ? (TimeSpan)unboundValues["Delay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IEventTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					unboundValues["Delay"] = value;
			}
		}

		private NamedValueCollection nvc = null;

		/// <summary>
		/// Gets a collection of named XPath queries. Each query in the collection is applied to the last matching event XML returned from the subscription query specified in the Subscription property. The name of the query can be used as a variable in the message of a <see cref="ShowMessageAction"/> action.
		/// </summary>
		public NamedValueCollection ValueQueries
		{
			get
			{
				if (nvc == null)
				{
					if (v2Trigger == null)
						nvc = new NamedValueCollection();
					nvc = new NamedValueCollection(((V2Interop.IEventTrigger)v2Trigger).ValueQueries);
				}
				return nvc;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task based on a daily schedule. For example, the task starts at a specific time every day, every other day, every third day, and so on.
	/// </summary>
	public class DailyTrigger : Trigger
	{
		internal DailyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunDaily) { Init(); }
		internal DailyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { Init(); }

		/// <summary>
		/// Creates an unbound instance of a <see cref="DailyTrigger"/>.
		/// </summary>
		public DailyTrigger() : base(TaskTriggerType.Daily) { Init(); }

		private void Init()
		{
			this.DaysInterval = 1;
		}

		/// <summary>
		/// Sets or retrieves the interval between the days in the schedule.
		/// </summary>
		public short DaysInterval
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IDailyTrigger)v2Trigger).DaysInterval;
				return (short)v1TriggerData.Data.daily.DaysInterval;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).DaysInterval = value;
				else
				{
					v1TriggerData.Data.daily.DaysInterval = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["DaysInterval"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a delay time that is randomly added to the start time of the trigger.
		/// </summary>
		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RandomDelay"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when the computer goes into an idle state. For information about idle conditions, see Task Idle Conditions.
	/// </summary>
	public class IdleTrigger : Trigger
	{
		internal IdleTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnIdle) { }
		internal IdleTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="IdleTrigger"/>.
		/// </summary>
		public IdleTrigger() : base(TaskTriggerType.Idle) { }
	}

	/// <summary>
	/// Represents a trigger that starts a task when a user logs on. When the Task Scheduler service starts, all logged-on users are enumerated and any tasks registered with logon triggers that match the logged on user are run. Not available on Task Scheduler 1.0.
	/// </summary>
	public class LogonTrigger : Trigger
	{
		internal LogonTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnLogon) { }
		internal LogonTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="LogonTrigger"/>.
		/// </summary>
		public LogonTrigger() : base(TaskTriggerType.Logon) { }

		/// <summary>
		/// Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.
		/// </summary>
		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.ILogonTrigger)v2Trigger).Delay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("Delay") ? (TimeSpan)unboundValues["Delay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ILogonTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["Delay"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the user.
		/// </summary>
		public string UserId
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ILogonTrigger)v2Trigger).UserId;
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("UserId") ? (string)unboundValues["UserId"] : null);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ILogonTrigger)v2Trigger).UserId = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["UserId"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task on a monthly day-of-week schedule. For example, the task starts on every first Thursday, May through October.
	/// </summary>
	public class MonthlyDOWTrigger : Trigger
	{
		internal MonthlyDOWTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunMonthlyDOW) { Init(); }
		internal MonthlyDOWTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { Init(); }

		/// <summary>
		/// Creates an unbound instance of a <see cref="MonthlyDOWTrigger"/>.
		/// </summary>
		public MonthlyDOWTrigger() : base(TaskTriggerType.MonthlyDOW) { Init(); }

		private void Init()
		{
			this.DaysOfWeek = DaysOfTheWeek.Sunday;
			this.MonthsOfYear = MonthsOfTheYear.AllMonths;
			this.WeeksOfMonth = WhichWeek.FirstWeek;
		}

		/// <summary>
		/// Gets or sets the days of the week during which the task runs.
		/// </summary>
		public DaysOfTheWeek DaysOfWeek
		{
			get
			{
				if (v2Trigger != null)
					return (DaysOfTheWeek)((V2Interop.IMonthlyDOWTrigger)v2Trigger).DaysOfWeek;
				return (DaysOfTheWeek)v1TriggerData.Data.monthlyDOW.DaysOfTheWeek;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).DaysOfWeek = (short)value;
				else
				{
					v1TriggerData.Data.monthlyDOW.DaysOfTheWeek = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["DaysOfWeek"] = (short)value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the weeks of the month during which the task runs.
		/// </summary>
		public WhichWeek WeeksOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return (WhichWeek)((V2Interop.IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth;
				int wk = 1 << (v1TriggerData.Data.monthlyDOW.WhichWeek - 1);
				if (wk == 0x10) wk = 0x20;
				return (WhichWeek)wk;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth = (short)value;
				else
				{
					int idx = Array.IndexOf<ushort>(new ushort[] { 0x1, 0x2, 0x4, 0x8, 0x20 }, (ushort)value);
					if (idx >= 0)
						v1TriggerData.Data.monthlyDOW.WhichWeek = (ushort)(idx + 1);
					else
						throw new NotV1SupportedException("Only a single week can be set with Task Scheduler 1.0.");
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["WeeksOfMonth"] = (short)value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the months of the year during which the task runs.
		/// </summary>
		public MonthsOfTheYear MonthsOfYear
		{
			get
			{
				if (v2Trigger != null)
					return (MonthsOfTheYear)((V2Interop.IMonthlyDOWTrigger)v2Trigger).MonthsOfYear;
				return (MonthsOfTheYear)v1TriggerData.Data.monthlyDOW.Months;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).MonthsOfYear = (short)value;
				else
				{
					v1TriggerData.Data.monthlyDOW.Months = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["MonthsOfYear"] = (short)value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task runs on the last week of the month.
		/// </summary>
		public bool RunOnLastWeekOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					return (unboundValues.ContainsKey("RunOnLastWeekOfMonth") ? (bool)unboundValues["RunOnLastWeekOfMonth"] : false);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RunOnLastWeekOfMonth"] = value;
			}
		}

		/// <summary>
		/// Gets or sets a delay time that is randomly added to the start time of the trigger.
		/// </summary>
		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RandomDelay"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a job based on a monthly schedule. For example, the task starts on specific days of specific months.
	/// </summary>
	public class MonthlyTrigger : Trigger
	{
		internal MonthlyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunMonthly) { Init(); }
		internal MonthlyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { Init(); }

		/// <summary>
		/// Creates an unbound instance of a <see cref="MonthlyTrigger"/>.
		/// </summary>
		public MonthlyTrigger() : base(TaskTriggerType.Monthly) { Init(); }

		private void Init()
		{
			this.DaysOfMonth = new int[] { 1 };
			this.MonthsOfYear = MonthsOfTheYear.AllMonths;
		}

		/// <summary>
		/// Convert an integer representing a mask to an array where each element contains the index
		/// of a bit that is ON in the mask.  Bits are considered to number from 1 to 32.
		/// </summary>
		/// <param name="mask">An interger to be interpreted as a mask.</param>
		/// <returns>An array with an element for each bit of the mask which is ON.</returns>
		internal static int[] MaskToIndices(int mask)
		{
			//count bits in mask
			int cnt = 0;
			for (int i = 0; (mask >> i) > 0; i++)
				cnt = cnt + (1 & (mask >> i));
			//allocate return array with one entry for each bit
			int[] indices = new int[cnt];
			//fill array with bit indices
			cnt = 0;
			for (int i = 0; (mask >> i) > 0; i++)
				if ((1 & (mask >> i)) == 1)
					indices[cnt++] = i + 1;
			return indices;
		}

		/// <summary>
		/// Converts an array of bit indices into a mask with bits  turned ON at every index
		/// contained in the array.  Indices must be from 1 to 32 and bits are numbered the same.
		/// </summary>
		/// <param name="indices">An array with an element for each bit of the mask which is ON.</param>
		/// <returns>An interger to be interpreted as a mask.</returns>
		internal static int IndicesToMask(int[] indices)
		{
			int mask = 0;
			foreach (int index in indices)
			{
				if (index < 1 || index > 31) throw new ArgumentException("Days must be in the range 1..31");
				mask = mask | 1 << (index - 1);
			}
			return mask;
		}

		/// <summary>
		/// Gets or sets the days of the month during which the task runs.
		/// </summary>
		public int[] DaysOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return MaskToIndices(((V2Interop.IMonthlyTrigger)v2Trigger).DaysOfMonth);
				return MaskToIndices((int)v1TriggerData.Data.monthlyDate.Days);
			}
			set
			{
				int mask = IndicesToMask(value);
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).DaysOfMonth = mask;
				else
				{
					v1TriggerData.Data.monthlyDate.Days = (uint)mask;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["DaysOfMonth"] = mask;
				}
			}
		}

		/// <summary>
		/// Gets or sets the months of the year during which the task runs.
		/// </summary>
		public MonthsOfTheYear MonthsOfYear
		{
			get
			{
				if (v2Trigger != null)
					return (MonthsOfTheYear)((V2Interop.IMonthlyTrigger)v2Trigger).MonthsOfYear;
				return (MonthsOfTheYear)v1TriggerData.Data.monthlyDOW.Months;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).MonthsOfYear = (short)value;
				else
				{
					v1TriggerData.Data.monthlyDOW.Months = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["MonthsOfYear"] = (short)value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates that the task runs on the last day of the month.
		/// </summary>
		public bool RunOnLastDayOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IMonthlyTrigger)v2Trigger).RunOnLastDayOfMonth;
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RunOnLastDayOfMonth") ? (bool)unboundValues["RunOnLastDayOfMonth"] : false);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).RunOnLastDayOfMonth = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RunOnLastDayOfMonth"] = value;
			}
		}

		/// <summary>
		/// Gets or sets a delay time that is randomly added to the start time of the trigger.
		/// </summary>
		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RandomDelay"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when the task is registered or updated. Not available on Task Scheduler 1.0.
	/// </summary>
	public class RegistrationTrigger : Trigger
	{
		internal RegistrationTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="RegistrationTrigger"/>.
		/// </summary>
		public RegistrationTrigger() : base(TaskTriggerType.Registration) { }

		/// <summary>
		/// Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.
		/// </summary>
		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IRegistrationTrigger)v2Trigger).Delay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("Delay") ? (TimeSpan)unboundValues["Delay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IRegistrationTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["Delay"] = value;
			}
		}
	}

	/// <summary>
	/// Triggers tasks for console connect or disconnect, remote connect or disconnect, or workstation lock or unlock notifications.
	/// </summary>
	public class SessionStateChangeTrigger : Trigger
	{
		internal SessionStateChangeTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="SessionStateChangeTrigger"/>.
		/// </summary>
		public SessionStateChangeTrigger() : base(TaskTriggerType.SessionStateChange) { }

		/// <summary>
		/// Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.
		/// </summary>
		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.ISessionStateChangeTrigger)v2Trigger).Delay);
				return (unboundValues.ContainsKey("Delay") ? (TimeSpan)unboundValues["Delay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ISessionStateChangeTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					unboundValues["Delay"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the user for the Terminal Server session. When a session state change is detected for this user, a task is started.
		/// </summary>
		public string UserId
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ILogonTrigger)v2Trigger).UserId;
				return (unboundValues.ContainsKey("UserId") ? (string)unboundValues["UserId"] : null);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ILogonTrigger)v2Trigger).UserId = value;
				else
					unboundValues["UserId"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the kind of Terminal Server session change that would trigger a task launch.
		/// </summary>
		public TaskSessionStateChangeType StateChange
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ISessionStateChangeTrigger)v2Trigger).StateChange;
				return (unboundValues.ContainsKey("StateChange") ? (TaskSessionStateChangeType)unboundValues["StateChange"] : TaskSessionStateChangeType.ConsoleConnect);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ISessionStateChangeTrigger)v2Trigger).StateChange = value;
				else
					unboundValues["StateChange"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task at a specific date and time.
	/// </summary>
	public class TimeTrigger : Trigger
	{
		internal TimeTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunOnce) { }
		internal TimeTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		/// <summary>
		/// Creates an unbound instance of a <see cref="TimeTrigger"/>.
		/// </summary>
		public TimeTrigger() : base(TaskTriggerType.Time) { }

		/// <summary>
		/// Gets or sets a delay time that is randomly added to the start time of the trigger.
		/// </summary>
		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RandomDelay"] = value;
			}
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task based on a weekly schedule. For example, the task starts at 8:00 A.M. on a specific day of the week every week or every other week.
	/// </summary>
	public class WeeklyTrigger : Trigger
	{
		internal WeeklyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunWeekly) { Init(); }
		internal WeeklyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { Init(); }

		/// <summary>
		/// Creates an unbound instance of a <see cref="WeeklyTrigger"/>.
		/// </summary>
		public WeeklyTrigger() : base(TaskTriggerType.Weekly) { Init(); }

		private void Init()
		{
			this.DaysOfWeek = DaysOfTheWeek.Sunday;
			this.WeeksInterval = 1;
		}

		/// <summary>
		/// Gets or sets the days of the week on which the task runs.
		/// </summary>
		public DaysOfTheWeek DaysOfWeek
		{
			get
			{
				if (v2Trigger != null)
					return (DaysOfTheWeek)((V2Interop.IWeeklyTrigger)v2Trigger).DaysOfWeek;
				return (DaysOfTheWeek)v1TriggerData.Data.weekly.DaysOfTheWeek;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IWeeklyTrigger)v2Trigger).DaysOfWeek = (short)value;
				else
				{
					v1TriggerData.Data.weekly.DaysOfTheWeek = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["DaysOfWeek"] = (short)value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the interval between the weeks in the schedule.
		/// </summary>
		public short WeeksInterval
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IWeeklyTrigger)v2Trigger).WeeksInterval;
				return (short)v1TriggerData.Data.weekly.WeeksInterval;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IWeeklyTrigger)v2Trigger).WeeksInterval = value;
				else
				{
					v1TriggerData.Data.weekly.WeeksInterval = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues["WeeksInterval"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a delay time that is randomly added to the start time of the trigger.
		/// </summary>
		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues["RandomDelay"] = value;
			}
		}
	}
}
