using System;
using System.Collections.Generic;
using System.Linq;
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
		/// <summary>
		/// Sunday
		/// </summary>
		Sunday = 0x1,
		/// <summary>
		/// Monday
		/// </summary>
		Monday = 0x2,
		/// <summary>
		/// Tuesday
		/// </summary>
		Tuesday = 0x4,
		/// <summary>
		/// Wednesday
		/// </summary>
		Wednesday = 0x8,
		/// <summary>
		/// Thursday
		/// </summary>
		Thursday = 0x10,
		/// <summary>
		/// Friday
		/// </summary>
		Friday = 0x20,
		/// <summary>
		/// Saturday
		/// </summary>
		Saturday = 0x40
	}

	/// <summary>Values for week of month (first, second, ..., last)</summary>
	public enum WhichWeek : short
	{
		/// <summary>
		/// First week of the month
		/// </summary>
		FirstWeek = 1,
		/// <summary>
		/// Second week of the month
		/// </summary>
		SecondWeek = 2,
		/// <summary>
		/// Third week of the month
		/// </summary>
		ThirdWeek = 3,
		/// <summary>
		/// Fourth week of the month
		/// </summary>
		FourthWeek = 4,
		/// <summary>
		/// Last week of the month
		/// </summary>
		LastWeek = 5
	}

	/// <summary>Values for months of the year (January, February, etc.)</summary>
	[Flags]
	public enum MonthsOfTheYear : short
	{
		/// <summary>
		/// January
		/// </summary>
		January = 0x1,
		/// <summary>
		/// February
		/// </summary>
		February = 0x2,
		/// <summary>
		/// March
		/// </summary>
		March = 0x4,
		/// <summary>
		/// April
		/// </summary>
		April = 0x8,
		/// <summary>
		///May 
		/// </summary>
		May = 0x10,
		/// <summary>
		/// June
		/// </summary>
		June = 0x20,
		/// <summary>
		/// July
		/// </summary>
		July = 0x40,
		/// <summary>
		/// August
		/// </summary>
		August = 0x80,
		/// <summary>
		/// September
		/// </summary>
		September = 0x100,
		/// <summary>
		/// October
		/// </summary>
		October = 0x200,
		/// <summary>
		/// November
		/// </summary>
		November = 0x400,
		/// <summary>
		/// December
		/// </summary>
		December = 0x800
	}

	public abstract class Trigger : IDisposable
	{
		internal V1Interop.ITaskTrigger v1Trigger = null;
		internal V1Interop.TaskTrigger v1TriggerData;
		internal V2Interop.ITrigger v2Trigger = null;
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTriggerType type) :
			this(trigger, trigger.GetTrigger())
		{
			v1TriggerData.TriggerSize = (ushort)Marshal.SizeOf(typeof(V1Interop.TaskTrigger));
			v1TriggerData.Type = type;
		}

		internal Trigger(V1Interop.ITaskTrigger trigger, V1Interop.TaskTrigger data)
		{
			v1Trigger = trigger;
			v1TriggerData = data;
		}

		internal Trigger(V2Interop.ITrigger iTrigger)
		{
			v2Trigger = iTrigger;
		}

		internal Trigger()
		{
		}

		public virtual void Dispose()
		{
			if (v2Trigger != null)
				Marshal.ReleaseComObject(v2Trigger);
			if (v1Trigger != null)
				Marshal.ReleaseComObject(v1Trigger);
		}

		internal void Bind()
		{
			if (v1Trigger != null)
				v1Trigger.SetTrigger(ref v1TriggerData);
		}

		protected void BindValues()
		{
			if (v2Trigger != null)
				foreach (string key in unboundValues.Keys)
					v2Trigger.GetType().InvokeMember(key, System.Reflection.BindingFlags.SetProperty, null, v2Trigger, new object[] { unboundValues[key] });
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

		public string Id
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Id;
				return v1Trigger.GetTriggerString();
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.Id = value;
				else
					throw new NotSupportedException();
			}
		}

		private RepetitionPattern repititionPattern = null;
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

		public TimeSpan ExecutionTimeLimit
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(v2Trigger.ExecutionTimeLimit);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.ExecutionTimeLimit = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}

		private const string V2BoundaryDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		public DateTime StartBoundary
		{
			get
			{
				if (v2Trigger != null)
					return DateTime.ParseExact(v2Trigger.StartBoundary, V2BoundaryDateFormat, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
				return v1TriggerData.BeginDate;
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.StartBoundary = value.ToString(V2BoundaryDateFormat);
				else
				{
					v1TriggerData.BeginDate = value;
					Bind();
				}
			}
		}

		public DateTime EndBoundary
		{
			get
			{
				if (v2Trigger != null)
					return DateTime.ParseExact(v2Trigger.EndBoundary, V2BoundaryDateFormat, null, System.Globalization.DateTimeStyles.AdjustToUniversal);
				return v1TriggerData.EndDate;
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.EndBoundary = value.ToString(V2BoundaryDateFormat);
				else
				{
					v1TriggerData.EndDate = value;
					Bind();
				}
			}
		}

		public bool Enabled
		{
			get
			{
				if (v2Trigger != null)
					return v2Trigger.Enabled;
				return ((v1TriggerData.Flags & V1Interop.TaskTriggerFlags.Disabled) == V1Interop.TaskTriggerFlags.Disabled);
			}
			set
			{
				if (v2Trigger != null)
					v2Trigger.Enabled = value;
				else
				{
					if (value)
						v1TriggerData.Flags |= V1Interop.TaskTriggerFlags.Disabled;
					else
						v1TriggerData.Flags &= ~V1Interop.TaskTriggerFlags.Disabled;
					Bind();
				}
			}
		}

		internal bool Bound
		{
			get
			{
				if (v1Trigger != null)
					return v1Trigger.GetTrigger().Equals(v1TriggerData);
				return false;
			}
		}

		/*internal void Bind(V1Interop.ITaskTrigger iTrigger)
		{
			if (v1Trigger != null)
				v1Trigger.Bind(iTrigger);
		}

		internal void Bind(V2Interop.ITrigger iTrigger)
		{
			if (v2Trigger != null)
				v2Trigger.Bind(iTrigger);
		}*/
	}

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

		public void Dispose()
		{
			if (v2Pattern != null) Marshal.ReleaseComObject(v2Pattern);
			v1Trigger = null;
		}

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

	public class BootTrigger : Trigger
	{
		internal BootTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnSystemStart) { }
		internal BootTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IBootTrigger)v2Trigger).Delay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IBootTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class EventTrigger : Trigger
	{
		internal EventTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public string Subscription
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IEventTrigger)v2Trigger).Subscription;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IEventTrigger)v2Trigger).Subscription = value;
				else
					throw new NotSupportedException();
			}
		}

		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IEventTrigger)v2Trigger).Delay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IEventTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}

		private TaskNamedValueCollection nvc = null;

		public TaskNamedValueCollection ValueQueries
		{
			get
			{
				if (nvc == null)
				{
					if (v2Trigger == null)
						throw new NotSupportedException();
					nvc = new TaskNamedValueCollection(((V2Interop.IEventTrigger)v2Trigger).ValueQueries);
				}
				return nvc;
			}
		}

		public class TaskNamedValueCollection : IDisposable, System.Collections.IEnumerable
		{
			private V2Interop.ITaskNamedValueCollection v2Coll = null;

			internal TaskNamedValueCollection(V2Interop.ITaskNamedValueCollection iColl) { v2Coll = iColl; }

			public void Dispose()
			{
				if (v2Coll != null) Marshal.ReleaseComObject(v2Coll);
			}

			public int Count
			{
				get { return v2Coll.Count; }
			}

			public string this[int index]
			{
				get
				{
					return v2Coll[index].Value;
				}
			}
			
			// TODO: Figure out how to make this more of a real collection
			/*public string this[string name]
			{
				get { throw new NotImplementedException(); }
			}*/

			public void Add(string Name, string Value)
			{
				v2Coll.Create(Name, Value);
			}

			public void Remove(int index)
			{
				v2Coll.Remove(index);
			}

			public void Clear()
			{
				v2Coll.Clear();
			}

			public System.Collections.IEnumerator GetEnumerator()
			{
				return v2Coll.GetEnumerator();
			}
		}
	}

	public class DailyTrigger : Trigger
	{
		internal DailyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunDaily) { }
		internal DailyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

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
					Bind();
				}
			}
		}

		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IDailyTrigger)v2Trigger).RandomDelay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IDailyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class IdleTrigger : Trigger
	{
		internal IdleTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnIdle) { }
		internal IdleTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }
	}

	public class LogonTrigger : Trigger
	{
		internal LogonTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.OnLogon) { }
		internal LogonTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.ILogonTrigger)v2Trigger).Delay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ILogonTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}

		public string UserId
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ILogonTrigger)v2Trigger).UserId;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ILogonTrigger)v2Trigger).UserId = value;
				else
					throw new NotSupportedException();
			}
		}
	}

	public class MonthlyDOWTrigger : Trigger
	{
		internal MonthlyDOWTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunMonthlyDOW) { }
		internal MonthlyDOWTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

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
					Bind();
				}
			}
		}

		public WhichWeek WeeksOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return (WhichWeek)((V2Interop.IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth;
				return (WhichWeek)v1TriggerData.Data.monthlyDOW.WhichWeek;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth = (short)value;
				else
				{
					v1TriggerData.Data.monthlyDOW.WhichWeek = (ushort)value;
					Bind();
				}
			}
		}

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
					Bind();
				}
			}
		}

		public bool RunOnLastWeekOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth;
				return (v1TriggerData.Data.monthlyDOW.WhichWeek & (short)WhichWeek.LastWeek) == (short)WhichWeek.LastWeek;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth = value;
				else
				{
					WhichWeek wom = this.WeeksOfMonth;
					if (value)
						this.WeeksOfMonth |= WhichWeek.LastWeek;
					else
						this.WeeksOfMonth ^= ~WhichWeek.LastWeek;
					Bind();
				}
			}
		}

		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IMonthlyDOWTrigger)v2Trigger).RandomDelay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyDOWTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class MonthlyTrigger : Trigger
	{
		internal MonthlyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunMonthly) { }
		internal MonthlyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

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
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).DaysOfMonth = IndicesToMask(value);
				else
				{
					v1TriggerData.Data.monthlyDate.Days = (uint)IndicesToMask(value);
					Bind();
				}
			}
		}

		public MonthsOfTheYear MonthsOfYear
		{
			get
			{
				if (v2Trigger != null)
					return (MonthsOfTheYear)((V2Interop.IMonthlyTrigger)v2Trigger).MonthsOfYear;
				return (MonthsOfTheYear)v1TriggerData.Data.monthlyDate.Months;
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).MonthsOfYear = (short)value;
				else
				{
					v1TriggerData.Data.monthlyDate.Months = (ushort)value;
					Bind();
				}
			}
		}

		public bool RunOnLastDayOfMonth
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.IMonthlyTrigger)v2Trigger).RunOnLastDayOfMonth;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).RunOnLastDayOfMonth = value;
				else
					throw new NotSupportedException();
			}
		}

		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IMonthlyTrigger)v2Trigger).RandomDelay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IMonthlyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class RegistrationTrigger : Trigger
	{
		internal RegistrationTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IRegistrationTrigger)v2Trigger).Delay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IRegistrationTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class SessionStateChangeTrigger : Trigger
	{
		internal SessionStateChangeTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public TimeSpan Delay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.ISessionStateChangeTrigger)v2Trigger).Delay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ISessionStateChangeTrigger)v2Trigger).Delay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}

		public string UserId
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ISessionStateChangeTrigger)v2Trigger).UserId;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ISessionStateChangeTrigger)v2Trigger).UserId = value;
				else
					throw new NotSupportedException();
			}
		}

		public TaskSessionStateChangeType StateChange
		{
			get
			{
				if (v2Trigger != null)
					return ((V2Interop.ISessionStateChangeTrigger)v2Trigger).StateChange;
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ISessionStateChangeTrigger)v2Trigger).StateChange = value;
				else
					throw new NotSupportedException();
			}
		}
	}

	public class TimeTrigger : Trigger
	{
		internal TimeTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunOnce) { }
		internal TimeTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.ITimeTrigger)v2Trigger).RandomDelay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.ITimeTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}

	public class WeeklyTrigger : Trigger
	{
		internal WeeklyTrigger(V1Interop.ITaskTrigger iTrigger) : base(iTrigger, V1Interop.TaskTriggerType.RunWeekly) { }
		internal WeeklyTrigger(V2Interop.ITrigger iTrigger) : base(iTrigger) { }

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
					Bind();
				}
			}
		}

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
					Bind();
				}
			}
		}

		public TimeSpan RandomDelay
		{
			get
			{
				if (v2Trigger != null)
					return Task.StringToTimeSpan(((V2Interop.IWeeklyTrigger)v2Trigger).RandomDelay);
				throw new NotSupportedException();
			}
			set
			{
				if (v2Trigger != null)
					((V2Interop.IWeeklyTrigger)v2Trigger).RandomDelay = Task.TimeSpanToString(value);
				else
					throw new NotSupportedException();
			}
		}
	}
}
