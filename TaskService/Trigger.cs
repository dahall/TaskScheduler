using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;

namespace Microsoft.Win32.TaskScheduler
{
    #region Enumerations

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

    /// <summary>Defines the type of triggers that can be used by tasks.</summary>
    public enum TaskTriggerType
    {
        /// <summary>Triggers the task when a specific event occurs. Version 1.2 only.</summary>
        Event = 0,
        /// <summary>Triggers the task at a specific time of day.</summary>
        Time = 1,
        /// <summary>Triggers the task on a daily schedule.</summary>
        Daily = 2,
        /// <summary>Triggers the task on a weekly schedule.</summary>
        Weekly = 3,
        /// <summary>Triggers the task on a monthly schedule.</summary>
        Monthly = 4,
        /// <summary>Triggers the task on a monthly day-of-week schedule.</summary>
        MonthlyDOW = 5,
        /// <summary>Triggers the task when the computer goes into an idle state.</summary>
        Idle = 6,
        /// <summary>Triggers the task when the task is registered. Version 1.2 only.</summary>
        Registration = 7,
        /// <summary>Triggers the task when the computer boots.</summary>
        Boot = 8,
        /// <summary>Triggers the task when a specific user logs on.</summary>
        Logon = 9,
        /// <summary>Triggers the task when a specific user session state changes. Version 1.2 only.</summary>
        SessionStateChange = 11,
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
        /// <summary>Last week of the month</summary>
        LastWeek = 0x10,
        /// <summary>Every week of the month</summary>
        AllWeeks = 0x1F
    }

    #endregion Enumerations

    #region Interfaces

    /// <summary>
    /// Interface for triggers that support a delay.
    /// </summary>
    public interface ITriggerDelay
    {
        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan Delay { get; set; }
    }

    /// <summary>
    /// Interface for triggers that support a user identifier.
    /// </summary>
    public interface ITriggerUserId
    {
        /// <summary>
        /// Gets or sets the user for the <see cref="Trigger"/>.
        /// </summary>
        string UserId { get; set; }
    }

	#endregion Interfaces

	/// <summary>
	/// Abstract base class which provides the common properties that are inherited by all trigger classes. A trigger can be created using the <see cref="TriggerCollection.Add"/> or the <see cref="TriggerCollection.AddNew"/> method.
	/// </summary>
	public abstract class Trigger : IDisposable, ICloneable
	{
		internal const string V2BoundaryDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFK";

		internal TaskTriggerType ttype;
		internal V1Interop.ITaskTrigger v1Trigger = null;
		internal V1Interop.TaskTrigger v1TriggerData;
		internal V2Interop.ITrigger v2Trigger = null;

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		protected Dictionary<string, object> unboundValues;

		private RepetitionPattern repititionPattern = null;

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTriggerType type)
			: this(trigger, trigger.GetTrigger())
		{
		}

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTrigger data)
		{
			v1Trigger = trigger;
			v1TriggerData = data;
			ttype = ConvertFromV1TriggerType(data.Type);
		}

		internal Trigger(V2Interop.ITrigger iTrigger)
		{
			v2Trigger = iTrigger;
			this.ttype = iTrigger.Type;
			if (string.IsNullOrEmpty(v2Trigger.StartBoundary))
				this.StartBoundary = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
		}

		internal Trigger(TaskTriggerType triggerType)
		{
			this.ttype = triggerType;
			unboundValues = new Dictionary<string, object>();

			v1TriggerData.TriggerSize = (ushort)Marshal.SizeOf(typeof(V1Interop.TaskTrigger));
			try { v1TriggerData.Type = ConvertToV1TriggerType(this.ttype); }
			catch { }

			this.StartBoundary = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates whether the trigger is enabled.
		/// </summary>
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Enabled;
				return !((v1TriggerData.Flags & V1Interop.TaskTriggerFlags.Disabled) == V1Interop.TaskTriggerFlags.Disabled);
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
					else if (!value)
						unboundValues["Enabled"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the date and time when the trigger is deactivated. The trigger cannot start the task after it is deactivated.
		/// </summary>
		[DefaultValue(0)]
		public DateTime EndBoundary
		{
			get
			{
				if (v2Trigger != null)
					return string.IsNullOrEmpty(v2Trigger.EndBoundary) ? DateTime.MaxValue : DateTime.Parse(v2Trigger.EndBoundary);
				return (unboundValues!=null && unboundValues.ContainsKey("EndBoundary")) ? (DateTime)unboundValues["EndBoundary"] : v1TriggerData.EndDate;
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.EndBoundary = value == DateTime.MaxValue ? null : value.ToString(V2BoundaryDateFormat);
				else
				{
					v1TriggerData.EndDate = value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else if (value != DateTime.MaxValue)
						unboundValues["EndBoundary"] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum amount of time that the task launched by this trigger is allowed to run. Not available with Task Scheduler 1.0.
		/// </summary>
		[DefaultValue(0)]
		public TimeSpan ExecutionTimeLimit
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(v2Trigger.ExecutionTimeLimit);
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return ((unboundValues!=null && unboundValues.ContainsKey("ExecutionTimeLimit")) ? (TimeSpan)unboundValues["ExecutionTimeLimit"] : TimeSpan.Zero);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.ExecutionTimeLimit = Task.TimeSpanToString(value);
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else if (value != TimeSpan.Zero)
					unboundValues["ExecutionTimeLimit"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the identifier for the trigger. Cannot set with Task Scheduler 1.0.
		/// </summary>
		[DefaultValue(null)]
		public string Id
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Id;
				if (v1Trigger != null)
					throw new NotV1SupportedException();
				return (unboundValues.ContainsKey("Id") ? (string)unboundValues["Id"] : null);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.Id = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else if (!string.IsNullOrEmpty(value))
					unboundValues["Id"] = value;
			}
		}

		/// <summary>
		/// Gets a <see cref="RepetitionPattern"/> instance that indicates how often the task is run and how long the repetition pattern is repeated after the task is started.
		/// </summary>
		public RepetitionPattern Repetition
		{
			get
			{
				if (repititionPattern == null)
					repititionPattern = new RepetitionPattern(this);
				return repititionPattern;
			}
		}

		/// <summary>
		/// Gets or sets the date and time when the trigger is activated.
		/// </summary>
		public DateTime StartBoundary
		{
			get
			{
				if (v2Trigger != null)
					return DateTime.Parse(v2Trigger.StartBoundary);
				return (unboundValues!=null && unboundValues.ContainsKey("StartBoundary")) ? (DateTime)unboundValues["StartBoundary"] : v1TriggerData.BeginDate;
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
		/// Gets the type of the trigger.
		/// </summary>
		/// <value>The <see cref="TaskTriggerType"/> of the trigger.</value>
		public TaskTriggerType TriggerType
		{
			get { return ttype; }
		}

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

		/// <summary>
		/// Creates a new <see cref="Trigger"/> that is an unbound copy of this instance.
		/// </summary>
		/// <returns>
		/// A new <see cref="Trigger"/> that is an unbound copy of this instance.
		/// </returns>
		public object Clone()
		{
			Trigger ret = CreateTrigger(this.TriggerType);
			ret.CopyProperties(this);
			return ret;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public virtual void CopyProperties(Trigger sourceTrigger)
		{
			this.Enabled = sourceTrigger.Enabled;
			this.EndBoundary = sourceTrigger.EndBoundary;
			try { this.ExecutionTimeLimit = sourceTrigger.ExecutionTimeLimit; }
			catch { }
			this.Repetition.Duration = sourceTrigger.Repetition.Duration;
			this.Repetition.Interval = sourceTrigger.Repetition.Interval;
			this.Repetition.StopAtDurationEnd = sourceTrigger.Repetition.StopAtDurationEnd;
			this.StartBoundary = sourceTrigger.StartBoundary;
			if (sourceTrigger is ITriggerDelay && this is ITriggerDelay)
				try { ((ITriggerDelay)this).Delay = ((ITriggerDelay)sourceTrigger).Delay; }
				catch { }
			if (sourceTrigger is ITriggerUserId && this is ITriggerUserId)
				try { ((ITriggerUserId)this).UserId = ((ITriggerUserId)sourceTrigger).UserId; }
				catch { }
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

		/// <summary>
		/// Returns a string representing this trigger.
		/// </summary>
		/// <returns>String value of trigger.</returns>
		public override string ToString()
		{
			if (v2Trigger != null)
				return V2GetTriggerString() + V2BaseTriggerString();
			if (v1Trigger != null)
				return v1Trigger.GetTriggerString();
			return string.Empty;
		}

		internal static string BuildEnumString(string preface, object enumValue)
		{
			string[] vals = enumValue.ToString().Split(new string[] { ", " }, StringSplitOptions.None);
			if (vals.Length == 0)
				return string.Empty;

			for (int i = 0; i < vals.Length; i++)
			{
				vals[i] = Properties.Resources.ResourceManager.GetString(preface + vals[i]);
			}
			return string.Join(", ", vals);
		}

		internal static TaskTriggerType ConvertFromV1TriggerType(V1Interop.TaskTriggerType v1Type)
		{
			int v2tt = (int)v1Type + 1;
			if (v2tt > 6) v2tt++;
			return (TaskTriggerType)v2tt;
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
			Trigger t = null;
			switch (triggerType)
			{
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunOnce:
					t = new TimeTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunDaily:
					t = new DailyTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunWeekly:
					t = new WeeklyTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunMonthly:
					t = new MonthlyTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.RunMonthlyDOW:
					t = new MonthlyDOWTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnIdle:
					t = new IdleTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnSystemStart:
					t = new BootTrigger(trigger);
					break;
				case Microsoft.Win32.TaskScheduler.V1Interop.TaskTriggerType.OnLogon:
					t = new LogonTrigger(trigger);
					break;
				default:
					break;
			}
			//if (t != null) t.ttype = triggerType;
			return t;
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

		internal static Trigger CreateTrigger(TaskTriggerType triggerType)
		{
			switch (triggerType)
			{
				case TaskTriggerType.Boot:
					return new BootTrigger();
				case TaskTriggerType.Daily:
					return new DailyTrigger();
				case TaskTriggerType.Event:
					return new EventTrigger();
				case TaskTriggerType.Idle:
					return new IdleTrigger();
				case TaskTriggerType.Logon:
					return new LogonTrigger();
				case TaskTriggerType.Monthly:
					return new MonthlyTrigger();
				case TaskTriggerType.MonthlyDOW:
					return new MonthlyDOWTrigger();
				case TaskTriggerType.Registration:
					return new RegistrationTrigger();
				case TaskTriggerType.SessionStateChange:
					return new SessionStateChangeTrigger();
				case TaskTriggerType.Time:
					return new TimeTrigger();
				case TaskTriggerType.Weekly:
					return new WeeklyTrigger();
				default:
					break;
			}
			return null;
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

			this.repititionPattern = new RepetitionPattern(this);
			this.repititionPattern.Bind();
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal void SetV1TriggerData()
		{
			if (v1Trigger != null)
				v1Trigger.SetTrigger(ref v1TriggerData);
			System.Diagnostics.Debug.WriteLine(v1TriggerData);
		}

		/// <summary>
		/// Gets the non-localized trigger string for V2 triggers.
		/// </summary>
		/// <returns>String describing the trigger.</returns>
		protected virtual string V2GetTriggerString()
		{
			return string.Empty;
		}

		private string V2BaseTriggerString()
		{
			StringBuilder ret = new StringBuilder();
			if (this.Repetition.Interval != TimeSpan.Zero)
			{
				ret.AppendFormat(" {0} {1}", Properties.Resources.TriggerBase1, this.Repetition.Interval);
				if (this.Repetition.Duration == TimeSpan.Zero)
					ret.Append(" " + Properties.Resources.TriggerBase2);
				else
					ret.AppendFormat(" {0} {1}", Properties.Resources.TriggerBase3, this.Repetition.Duration);
				ret.Append(".");
			}
			if (!string.IsNullOrEmpty(v2Trigger.EndBoundary))
				ret.AppendFormat(" {0} {1:G}.", Properties.Resources.TriggerBase4, this.EndBoundary);
			if (ret.Length > 0)
				ret.Insert(0, " -");
			return ret.ToString();
		}
	}

	/// <summary>
    /// Represents a trigger that starts a task when the system is booted.
    /// </summary>
    public sealed class BootTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="BootTrigger"/>.
        /// </summary>
        public BootTrigger()
            : base(TaskTriggerType.Boot)
        {
        }

        internal BootTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.OnSystemStart)
        {
        }

        internal BootTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

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
                else if (value != TimeSpan.Zero)
                    unboundValues["Delay"] = value;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            return Properties.Resources.TriggerBoot1;
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task based on a daily schedule. For example, the task starts at a specific time every day, every other day, every third day, and so on.
    /// </summary>
    public sealed class DailyTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="DailyTrigger"/>.
        /// </summary>
        public DailyTrigger()
            : base(TaskTriggerType.Daily)
        {
            this.DaysInterval = 1;
        }

        internal DailyTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.RunDaily)
        {
        }

        internal DailyTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
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
                    else if (value != 0)
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
				else if (value != TimeSpan.Zero)
					unboundValues["RandomDelay"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan ITriggerDelay.Delay
        {
            get { return this.RandomDelay; }
            set { this.RandomDelay = value; }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            if (sourceTrigger.GetType() == this.GetType())
            {
                this.DaysInterval = ((DailyTrigger)sourceTrigger).DaysInterval;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            if (this.DaysInterval == 1)
                return string.Format(Properties.Resources.TriggerDaily1, this.StartBoundary);
            return string.Format(Properties.Resources.TriggerDaily2, this.StartBoundary, this.DaysInterval);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task when a system event occurs. Not available on Task Scheduler 1.0.
    /// </summary>
    public sealed class EventTrigger : Trigger, ITriggerDelay
    {
        private NamedValueCollection nvc = null;

        /// <summary>
        /// Creates an unbound instance of a <see cref="EventTrigger"/>.
        /// </summary>
        public EventTrigger()
            : base(TaskTriggerType.Event)
        {
        }

		/// <summary>
		/// Initializes an unbound instance of the <see cref="EventTrigger"/> class and sets a basic event.
		/// </summary>
		/// <param name="log">The event's log.</param>
		/// <param name="source">The event's source. Can be <c>null</c>.</param>
		/// <param name="eventId">The event's id. Can be <c>null</c>.</param>
		public EventTrigger(string log, string source, int? eventId) : this() { SetBasic(log, source, eventId); }

        internal EventTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
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
				else if (value != TimeSpan.Zero)
					unboundValues["Delay"] = value;
            }
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
                else if (!string.IsNullOrEmpty(value))
                    unboundValues["Subscription"] = value;
            }
        }

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
                    else
                        nvc = new NamedValueCollection(((V2Interop.IEventTrigger)v2Trigger).ValueQueries);
                }
                return nvc;
            }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            this.Subscription = ((EventTrigger)sourceTrigger).Subscription;
            ((EventTrigger)sourceTrigger).ValueQueries.CopyTo(this.ValueQueries);
        }

        /// <summary>
        /// Gets basic event information.
        /// </summary>
        /// <param name="log">The event's log.</param>
        /// <param name="source">The event's source. Can be <c>null</c>.</param>
        /// <param name="eventId">The event's id. Can be <c>null</c>.</param>
        /// <returns><c>true</c> if subscription represents a basic event, <c>false</c> if not.</returns>
        public bool GetBasic(out string log, out string source, out int? eventId)
        {
            log = source = null;
            eventId = null;
            using (System.IO.MemoryStream str = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(this.Subscription)))
            {
                using (System.Xml.XmlTextReader rdr = new System.Xml.XmlTextReader(str))
                {
                    rdr.MoveToContent();
                    rdr.ReadStartElement("QueryList");
                    if (rdr.Name == "Query" && rdr.MoveToAttribute("Path"))
                    {
                        string path = rdr.Value;
                        if (rdr.MoveToElement() && rdr.ReadToDescendant("Select") && path.Equals(rdr["Path"], StringComparison.InvariantCultureIgnoreCase))
                        {
                            string content = rdr.ReadString();
                            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(content,
                                @"\*(?:\[System\[(?:Provider\[\@Name='(?<s>[^']+)'\])?(?:\s+and\s+)?(?:EventID=(?<e>\d+))?\]\])",
                                System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                                System.Text.RegularExpressions.RegexOptions.Compiled |
                                System.Text.RegularExpressions.RegexOptions.Singleline |
                                System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
                            if (m.Success)
                            {
                                log = path;
                                if (m.Groups["s"].Success)
                                    source = m.Groups["s"].Value;
                                if (m.Groups["e"].Success)
                                    eventId = Convert.ToInt32(m.Groups["e"].Value);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the subscription for a basic event. This will replace the contents of the <see cref="Subscription"/> property and clear all entries in the <see cref="ValueQueries"/> property.
        /// </summary>
        /// <param name="log">The event's log.</param>
        /// <param name="source">The event's source. Can be <c>null</c>.</param>
        /// <param name="eventId">The event's id. Can be <c>null</c>.</param>
        public void SetBasic(string log, string source, int? eventId)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(log))
                throw new ArgumentNullException("log");
            sb.AppendFormat("<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*", log);
            bool hasSource = !string.IsNullOrEmpty(source), hasId = eventId.HasValue;
            if (hasSource || hasId)
            {
                sb.Append("[System[");
                if (hasSource)
                    sb.AppendFormat("Provider[@Name='{0}']", source);
                if (hasSource && hasId)
                    sb.Append(" and ");
                if (hasId)
                    sb.AppendFormat("EventID={0}", eventId.Value);
                sb.Append("]]");
            }
            sb.Append("</Select></Query></QueryList>");
            this.ValueQueries.Clear();
            this.Subscription = sb.ToString();
        }

        internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
        {
            base.Bind(iTaskDef);
            if (nvc != null)
                nvc.Bind(((V2Interop.IEventTrigger)v2Trigger).ValueQueries);
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string log, source; int? id;
            if (this.GetBasic(out log, out source, out id))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(Properties.Resources.TriggerEventBasic1, log);
                if (!string.IsNullOrEmpty(source))
                    sb.AppendFormat(Properties.Resources.TriggerEventBasic2, source);
                if (id.HasValue)
                    sb.AppendFormat(Properties.Resources.TriggerEventBasic3, id.Value);
                return sb.ToString();
            }
            return Properties.Resources.TriggerEvent1;
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task when the computer goes into an idle state. For information about idle conditions, see Task Idle Conditions.
    /// </summary>
    public sealed class IdleTrigger : Trigger
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="IdleTrigger"/>.
        /// </summary>
        public IdleTrigger()
            : base(TaskTriggerType.Idle)
        {
        }

        internal IdleTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.OnIdle)
        {
        }

        internal IdleTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            return Properties.Resources.TriggerIdle1;
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task when a user logs on. When the Task Scheduler service starts, all logged-on users are enumerated and any tasks registered with logon triggers that match the logged on user are run. Not available on Task Scheduler 1.0.
    /// </summary>
    public sealed class LogonTrigger : Trigger, ITriggerDelay, ITriggerUserId
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="LogonTrigger"/>.
        /// </summary>
        public LogonTrigger()
            : base(TaskTriggerType.Logon)
        {
        }

        internal LogonTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.OnLogon)
        {
        }

        internal LogonTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

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
				else if (value != TimeSpan.Zero)
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
                else if (!string.IsNullOrEmpty(value))
                    unboundValues["UserId"] = value;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string user = string.IsNullOrEmpty(this.UserId) ? Properties.Resources.TriggerAnyUser : this.UserId;
            return string.Format(Properties.Resources.TriggerLogon1, user);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task on a monthly day-of-week schedule. For example, the task starts on every first Thursday, May through October.
    /// </summary>
    public sealed class MonthlyDOWTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="MonthlyDOWTrigger"/>.
        /// </summary>
        public MonthlyDOWTrigger()
            : base(TaskTriggerType.MonthlyDOW)
        {
            this.DaysOfWeek = DaysOfTheWeek.Sunday;
            this.MonthsOfYear = MonthsOfTheYear.AllMonths;
            this.WeeksOfMonth = WhichWeek.FirstWeek;
        }

        internal MonthlyDOWTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.RunMonthlyDOW)
        {
        }

        internal MonthlyDOWTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
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
                    else if ((short)value != 0)
                        unboundValues["DaysOfWeek"] = (short)value;
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
					else if ((short)value != 0)
						unboundValues["MonthsOfYear"] = (short)value;
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
                    return Task.StringToTimeSpan(((V2Interop.IMonthlyDOWTrigger)v2Trigger).RandomDelay);
                if (v1Trigger != null)
                    throw new NotV1SupportedException();
                return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
            }
            set
            {
                if (v2Trigger != null)
                    ((V2Interop.IMonthlyDOWTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
                else if (v1Trigger != null)
                    throw new NotV1SupportedException();
				else if (value != TimeSpan.Zero)
					unboundValues["RandomDelay"] = value;
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
                else if (value)
                    unboundValues["RunOnLastWeekOfMonth"] = value;
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
					else if ((short)value != 0)
						unboundValues["WeeksOfMonth"] = (short)value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan ITriggerDelay.Delay
        {
            get { return this.RandomDelay; }
            set { this.RandomDelay = value; }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            if (sourceTrigger.GetType() == this.GetType())
            {
                this.DaysOfWeek = ((MonthlyDOWTrigger)sourceTrigger).DaysOfWeek;
                this.MonthsOfYear = ((MonthlyDOWTrigger)sourceTrigger).MonthsOfYear;
                try { this.RunOnLastWeekOfMonth = ((MonthlyDOWTrigger)sourceTrigger).RunOnLastWeekOfMonth; } catch { }
                this.WeeksOfMonth = ((MonthlyDOWTrigger)sourceTrigger).WeeksOfMonth;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string ww = BuildEnumString("WW", this.WeeksOfMonth);
            string days = this.DaysOfWeek == DaysOfTheWeek.AllDays ? Properties.Resources.DOWAllDays : BuildEnumString("DOW", this.DaysOfWeek);
            string months = this.MonthsOfYear == MonthsOfTheYear.AllMonths ? Properties.Resources.MOYAllMonths : BuildEnumString("MOY", this.MonthsOfYear);
            return string.Format("At {0:t} on the {1} {2:f} each {3}, starting {0:d}", this.StartBoundary, ww, days, months);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a job based on a monthly schedule. For example, the task starts on specific days of specific months.
    /// </summary>
    public sealed class MonthlyTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="MonthlyTrigger"/>.
        /// </summary>
        public MonthlyTrigger()
            : base(TaskTriggerType.Monthly)
        {
            this.DaysOfMonth = new int[] { 1 };
            this.MonthsOfYear = MonthsOfTheYear.AllMonths;
        }

        internal MonthlyTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.RunMonthly)
        {
        }

        internal MonthlyTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
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
                    else if (mask != 0)
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
					else if ((short)value != 0)
						unboundValues["MonthsOfYear"] = (short)value;
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
                    return Task.StringToTimeSpan(((V2Interop.IMonthlyTrigger)v2Trigger).RandomDelay);
                if (v1Trigger != null)
                    throw new NotV1SupportedException();
                return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
            }
            set
            {
                if (v2Trigger != null)
                    ((V2Interop.IMonthlyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
                else if (v1Trigger != null)
                    throw new NotV1SupportedException();
				else if (value != TimeSpan.Zero)
					unboundValues["RandomDelay"] = value;
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
                else if (value)
                    unboundValues["RunOnLastDayOfMonth"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan ITriggerDelay.Delay
        {
            get { return this.RandomDelay; }
            set { this.RandomDelay = value; }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            if (sourceTrigger.GetType() == this.GetType())
            {
                this.DaysOfMonth = ((MonthlyTrigger)sourceTrigger).DaysOfMonth;
                this.MonthsOfYear = ((MonthlyTrigger)sourceTrigger).MonthsOfYear;
                try { this.RunOnLastDayOfMonth = ((MonthlyTrigger)sourceTrigger).RunOnLastDayOfMonth; } catch { }
            }
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
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string days = string.Join(", ", Array.ConvertAll(this.DaysOfMonth, delegate(int i) { return i.ToString(); }));
            string months = this.MonthsOfYear == MonthsOfTheYear.AllMonths ? Properties.Resources.MOYAllMonths : BuildEnumString("MOY", this.MonthsOfYear);
            return string.Format(Properties.Resources.TriggerMonthly1, this.StartBoundary, days, months);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task when the task is registered or updated. Not available on Task Scheduler 1.0.
    /// </summary>
    public sealed class RegistrationTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="RegistrationTrigger"/>.
        /// </summary>
        public RegistrationTrigger()
            : base(TaskTriggerType.Registration)
        {
        }

        internal RegistrationTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

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
				else if (value != TimeSpan.Zero)
					unboundValues["Delay"] = value;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            return Properties.Resources.TriggerRegistration1;
        }
    }

    /// <summary>
    /// Defines how often the task is run and how long the repetition pattern is repeated after the task is started.
    /// </summary>
    public sealed class RepetitionPattern : IDisposable
    {
        private Trigger pTrigger;
        private V2Interop.IRepetitionPattern v2Pattern = null;

        internal RepetitionPattern(Trigger parent)
        {
            pTrigger = parent;
            if (pTrigger.v2Trigger != null)
                v2Pattern = pTrigger.v2Trigger.Repetition;
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
                return TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesDuration);
            }
            set
            {
                if (v2Pattern != null)
                    v2Pattern.Duration = Task.TimeSpanToString(value);
                else
                {
                    pTrigger.v1TriggerData.MinutesDuration = (uint)value.TotalMinutes;
                    Bind();
                }
            }
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
                return TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesInterval);
            }
            set
            {
                if (v2Pattern != null)
                    v2Pattern.Interval = Task.TimeSpanToString(value);
                else
                {
                    pTrigger.v1TriggerData.MinutesInterval = (uint)value.TotalMinutes;
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
                return (pTrigger.v1TriggerData.Flags & V1Interop.TaskTriggerFlags.KillAtDurationEnd) == V1Interop.TaskTriggerFlags.KillAtDurationEnd;
            }
            set
            {
                if (v2Pattern != null)
                    v2Pattern.StopAtDurationEnd = value;
                else
                {
                    if (value)
                        pTrigger.v1TriggerData.Flags |= V1Interop.TaskTriggerFlags.KillAtDurationEnd;
                    else
                        pTrigger.v1TriggerData.Flags &= ~V1Interop.TaskTriggerFlags.KillAtDurationEnd;
                    Bind();
                }
            }
        }

        /// <summary>
        /// Releases all resources used by this class.
        /// </summary>
        public void Dispose()
        {
            if (v2Pattern != null) Marshal.ReleaseComObject(v2Pattern);
        }

        internal void Bind()
        {
            if (pTrigger.v1Trigger != null)
                pTrigger.v1Trigger.SetTrigger(ref pTrigger.v1TriggerData);
            else if (pTrigger.v2Trigger != null)
            {
                if (pTrigger.v1TriggerData.MinutesInterval != 0)
                    v2Pattern.Interval = string.Format("PT{0}M", pTrigger.v1TriggerData.MinutesInterval);
                if (pTrigger.v1TriggerData.MinutesDuration != 0)
                    v2Pattern.Duration = string.Format("PT{0}M", pTrigger.v1TriggerData.MinutesDuration);
                v2Pattern.StopAtDurationEnd = (pTrigger.v1TriggerData.Flags & V1Interop.TaskTriggerFlags.KillAtDurationEnd) == V1Interop.TaskTriggerFlags.KillAtDurationEnd;
            }
        }
    }

    /// <summary>
    /// Triggers tasks for console connect or disconnect, remote connect or disconnect, or workstation lock or unlock notifications.
    /// </summary>
    public sealed class SessionStateChangeTrigger : Trigger, ITriggerDelay, ITriggerUserId
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="SessionStateChangeTrigger"/>.
        /// </summary>
        public SessionStateChangeTrigger()
            : base(TaskTriggerType.SessionStateChange)
        {
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="SessionStateChangeTrigger"/> class.
		/// </summary>
		/// <param name="stateChange">The state change.</param>
		public SessionStateChangeTrigger(TaskSessionStateChangeType stateChange) : this()
		{
			this.StateChange = stateChange;
		}

        internal SessionStateChangeTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

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
				else if (value != TimeSpan.Zero)
					unboundValues["Delay"] = value;
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

        /// <summary>
        /// Gets or sets the user for the Terminal Server session. When a session state change is detected for this user, a task is started.
        /// </summary>
        public string UserId
        {
            get
            {
                if (v2Trigger != null)
                    return ((V2Interop.ISessionStateChangeTrigger)v2Trigger).UserId;
                return (unboundValues.ContainsKey("UserId") ? (string)unboundValues["UserId"] : null);
            }
            set
            {
                if (v2Trigger != null)
                    ((V2Interop.ISessionStateChangeTrigger)v2Trigger).UserId = value;
                else if (!string.IsNullOrEmpty(value))
                    unboundValues["UserId"] = value;
            }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            if (sourceTrigger.GetType() == this.GetType())
                this.StateChange = ((SessionStateChangeTrigger)sourceTrigger).StateChange;
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string str = Properties.Resources.ResourceManager.GetString("TriggerSession" + this.StateChange.ToString());
            string user = string.IsNullOrEmpty(this.UserId) ? Properties.Resources.TriggerAnyUser : this.UserId;
            if (this.StateChange != TaskSessionStateChangeType.SessionLock && this.StateChange != TaskSessionStateChangeType.SessionUnlock)
                user = string.Format(Properties.Resources.TriggerSessionUserSession, user);
            return string.Format(str, user);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task at a specific date and time.
    /// </summary>
    public sealed class TimeTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="TimeTrigger"/>.
        /// </summary>
        public TimeTrigger()
            : base(TaskTriggerType.Time)
        {
        }

        internal TimeTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.RunOnce)
        {
        }

        internal TimeTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
        }

        /// <summary>
        /// Gets or sets a delay time that is randomly added to the start time of the trigger.
        /// </summary>
        public TimeSpan RandomDelay
        {
            get
            {
                if (v2Trigger != null)
                    return Task.StringToTimeSpan(((V2Interop.ITimeTrigger)v2Trigger).RandomDelay);
                if (v1Trigger != null)
                    throw new NotV1SupportedException();
                return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
            }
            set
            {
                if (v2Trigger != null)
                    ((V2Interop.ITimeTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
                else if (v1Trigger != null)
                    throw new NotV1SupportedException();
                else if (value != TimeSpan.Zero)
                    unboundValues["RandomDelay"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan ITriggerDelay.Delay
        {
            get { return this.RandomDelay; }
            set { this.RandomDelay = value; }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            return string.Format(Properties.Resources.TriggerTime1, this.StartBoundary);
        }
    }

    /// <summary>
    /// Represents a trigger that starts a task based on a weekly schedule. For example, the task starts at 8:00 A.M. on a specific day of the week every week or every other week.
    /// </summary>
    public sealed class WeeklyTrigger : Trigger, ITriggerDelay
    {
        /// <summary>
        /// Creates an unbound instance of a <see cref="WeeklyTrigger"/>.
        /// </summary>
        public WeeklyTrigger()
            : base(TaskTriggerType.Weekly)
        {
            this.DaysOfWeek = DaysOfTheWeek.Sunday;
            this.WeeksInterval = 1;
        }

        internal WeeklyTrigger(V1Interop.ITaskTrigger iTrigger)
            : base(iTrigger, V1Interop.TaskTriggerType.RunWeekly)
        {
        }

        internal WeeklyTrigger(V2Interop.ITrigger iTrigger)
            : base(iTrigger)
        {
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
                    else if ((short)value != 0)
                        unboundValues["DaysOfWeek"] = (short)value;
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
                    return Task.StringToTimeSpan(((V2Interop.IWeeklyTrigger)v2Trigger).RandomDelay);
                if (v1Trigger != null)
                    throw new NotV1SupportedException();
                return (unboundValues.ContainsKey("RandomDelay") ? (TimeSpan)unboundValues["RandomDelay"] : TimeSpan.Zero);
            }
            set
            {
                if (v2Trigger != null)
                    ((V2Interop.IWeeklyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
                else if (v1Trigger != null)
                    throw new NotV1SupportedException();
				else if (value != TimeSpan.Zero)
					unboundValues["RandomDelay"] = value;
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
					else if ((short)value != 0)
						unboundValues["WeeksInterval"] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the amount of time before the task is started.
        /// </summary>
        /// <value>The delay duration.</value>
        TimeSpan ITriggerDelay.Delay
        {
            get { return this.RandomDelay; }
            set { this.RandomDelay = value; }
        }

        /// <summary>
        /// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
        /// </summary>
        /// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
        public override void CopyProperties(Trigger sourceTrigger)
        {
            base.CopyProperties(sourceTrigger);
            if (sourceTrigger.GetType() == this.GetType())
            {
                this.DaysOfWeek = ((WeeklyTrigger)sourceTrigger).DaysOfWeek;
                this.WeeksInterval = ((WeeklyTrigger)sourceTrigger).WeeksInterval;
            }
        }

        /// <summary>
        /// Gets the non-localized trigger string for V2 triggers.
        /// </summary>
        /// <returns>String describing the trigger.</returns>
        protected override string V2GetTriggerString()
        {
            string days = this.DaysOfWeek == DaysOfTheWeek.AllDays ? Properties.Resources.DOWAllDays : BuildEnumString("DOW", this.DaysOfWeek);
            return string.Format(this.WeeksInterval == 1 ? Properties.Resources.TriggerWeekly1Week : Properties.Resources.TriggerWeeklyMultWeeks,
                this.StartBoundary, days, this.WeeksInterval);
        }
    }
}