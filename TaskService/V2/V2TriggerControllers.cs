using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Microsoft.Win32.TaskScheduler.V2Interop.V2Util;

namespace Microsoft.Win32.TaskScheduler.V2Interop
{
	internal abstract class V2Trigger : ITriggerImpl, IDisposable
	{
		protected ITrigger iTrigger;
		private IRepetitionPattern repetitionPattern = null;

		public V2Trigger(ITrigger t)
		{
			iTrigger = t;
			TriggerType = iTrigger.Type;
			if (string.IsNullOrEmpty(iTrigger.StartBoundary) && this is ICalendarTrigger)
				StartBoundary = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
		}

		public abstract TimeSpan? Delay { get; set; }

		public bool Enabled
		{
			get { return iTrigger.Enabled; }
			set { iTrigger.Enabled = value; }
		}

		public DateTime? EndBoundary
		{
			get { return StringToDateTime(iTrigger.EndBoundary); }
			set { iTrigger.EndBoundary = DateTimeToString(value, DateTime.MaxValue); }
		}

		public TimeSpan? ExecutionTimeLimit
		{
			get { return StringToTimeSpan(iTrigger.ExecutionTimeLimit); }
			set { iTrigger.ExecutionTimeLimit = TimeSpanToString(value, TimeSpan.Zero); }
		}

		public string Id
		{
			get { return iTrigger.Id; }
			set { iTrigger.Id = value; }
		}

		private IRepetitionPattern Repetition => repetitionPattern ?? (repetitionPattern = iTrigger.Repetition);

		public TimeSpan? RepetitionDuration
		{
			get { return StringToTimeSpan(Repetition.Duration); }
			set { Repetition.Duration = TimeSpanToString(value, TimeSpan.Zero); }
		}

		public TimeSpan? RepetitionInterval
		{
			get { return StringToTimeSpan(Repetition.Interval); }
			set { Repetition.Interval = TimeSpanToString(value, TimeSpan.Zero); }
		}

		public bool RepetitionStopAtDurationEnd
		{
			get { return Repetition.StopAtDurationEnd; }
			set { Repetition.StopAtDurationEnd = value; }
		}

		public DateTime StartBoundary
		{
			get { return StringToDateTime(iTrigger.StartBoundary).GetValueOrDefault(DateTime.MinValue); }
			set { iTrigger.StartBoundary = DateTimeToString(value, DateTime.MinValue); }
		}

		public TaskTriggerType TriggerType { get; }

		public virtual void Dispose()
		{
			if (iTrigger != null)
			{
				Marshal.ReleaseComObject(iTrigger);
				iTrigger = null;
			}
		}
	}

	internal class V2BootTrigger : V2Trigger, IBootTriggerImpl
	{
		static V2BootTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2BootTrigger)); }

		public V2BootTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IBootTrigger)iTrigger).Delay); }
			set { ((IBootTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}
	}

	internal class V2DailyTrigger : V2Trigger, IDailyTriggerImpl
	{
		static V2DailyTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2DailyTrigger)); }

		public V2DailyTrigger(ITrigger t) : base(t) { }

		public short DaysInterval
		{
			get { return ((IDailyTrigger)iTrigger).DaysInterval; }
			set { ((IDailyTrigger)iTrigger).DaysInterval = value; }
		}

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IDailyTrigger)iTrigger).RandomDelay); }
			set { ((IDailyTrigger)iTrigger).RandomDelay = TimeSpanToString(value); }
		}
	}

	internal class V2EventTrigger : V2Trigger, IEventTriggerImpl
	{
		private NamedValueCollection nvc = null;

		static V2EventTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2EventTrigger)); }

		public V2EventTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IEventTrigger)iTrigger).Delay); }
			set { ((IEventTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}

		public string Subscription
		{
			get { return ((IEventTrigger)iTrigger).Subscription; }
			set { ((IEventTrigger)iTrigger).Subscription = value; }
		}

		public IDictionary<string, string> ValueQueries => nvc ?? (nvc = new NamedValueCollection(((IEventTrigger)iTrigger).ValueQueries));
	}

	internal class V2IdleTrigger : V2Trigger, IIdleTriggerImpl
	{
		static V2IdleTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2IdleTrigger)); }

		public V2IdleTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return null; }
			set { throw new NotV2SupportedException(); }
		}
	}

	internal class V2LogonTrigger : V2Trigger, ILogonTriggerImpl
	{
		static V2LogonTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2LogonTrigger)); }

		public V2LogonTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((ILogonTrigger)iTrigger).Delay); }
			set { ((ILogonTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}

		public string UserId
		{
			get { return ((ILogonTrigger)iTrigger).UserId; }
			set { ((ILogonTrigger)iTrigger).UserId = value; }
		}
	}

	internal class V2MonthlyDOWTrigger : V2Trigger, IMonthlyDOWTriggerImpl
	{
		static V2MonthlyDOWTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2MonthlyDOWTrigger)); }

		public V2MonthlyDOWTrigger(ITrigger t) : base(t) { }

		public DaysOfTheWeek DaysOfWeek
		{
			get { return (DaysOfTheWeek)((IMonthlyDOWTrigger)iTrigger).DaysOfWeek; }
			set { ((IMonthlyDOWTrigger)iTrigger).DaysOfWeek = (short)value; }
		}

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IMonthlyDOWTrigger)iTrigger).RandomDelay); }
			set { ((IMonthlyDOWTrigger)iTrigger).RandomDelay = TimeSpanToString(value); }
		}

		public MonthsOfTheYear MonthsOfYear
		{
			get { return (MonthsOfTheYear)((IMonthlyDOWTrigger)iTrigger).MonthsOfYear; }
			set { ((IMonthlyDOWTrigger)iTrigger).MonthsOfYear = (short)value; }
		}

		public bool RunOnLastWeekOfMonth
		{
			get { return ((IMonthlyDOWTrigger)iTrigger).RunOnLastWeekOfMonth; }
			set { ((IMonthlyDOWTrigger)iTrigger).RunOnLastWeekOfMonth = value; }
		}

		public WhichWeek WeeksOfMonth
		{
			get
			{
				WhichWeek ww = (WhichWeek)((IMonthlyDOWTrigger)iTrigger).WeeksOfMonth;
				// Following addition gives accurate results for confusing RunOnLastWeekOfMonth property (thanks kbergeron)
				if (((IMonthlyDOWTrigger)iTrigger).RunOnLastWeekOfMonth)
					ww |= WhichWeek.LastWeek;
				return ww;
			}
			set { ((IMonthlyDOWTrigger)iTrigger).WeeksOfMonth = (short)value; }
		}
	}

	internal class V2MonthlyTrigger : V2Trigger, IMonthlyTriggerImpl
	{
		static V2MonthlyTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2MonthlyTrigger)); }

		public V2MonthlyTrigger(ITrigger t) : base(t) { }

		public byte[] DaysOfMonth
		{
			get { return MaskToIndices(((IMonthlyTrigger)iTrigger).DaysOfMonth); }
			set { ((IMonthlyTrigger)iTrigger).DaysOfMonth = IndicesToMask(value); }
		}

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IMonthlyTrigger)iTrigger).RandomDelay); }
			set { ((IMonthlyTrigger)iTrigger).RandomDelay = TimeSpanToString(value); }
		}

		public MonthsOfTheYear MonthsOfYear
		{
			get { return (MonthsOfTheYear)((IMonthlyTrigger)iTrigger).MonthsOfYear; }
			set { ((IMonthlyTrigger)iTrigger).MonthsOfYear = (short)value; }
		}

		public bool RunOnLastDayOfMonth
		{
			get { return ((IMonthlyTrigger)iTrigger).RunOnLastDayOfMonth; }
			set { ((IMonthlyTrigger)iTrigger).RunOnLastDayOfMonth = value; }
		}
	}

	internal class V2RegistrationTrigger : V2Trigger, IRegistrationTriggerImpl
	{
		static V2RegistrationTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2RegistrationTrigger)); }

		public V2RegistrationTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IRegistrationTrigger)iTrigger).Delay); }
			set { ((IRegistrationTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}
	}

	internal class V2SessionStateChangeTrigger : V2Trigger, ISessionStateChangeTriggerImpl
	{
		static V2SessionStateChangeTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2SessionStateChangeTrigger)); }

		public V2SessionStateChangeTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((ISessionStateChangeTrigger)iTrigger).Delay); }
			set { ((ISessionStateChangeTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}

		public TaskSessionStateChangeType StateChange
		{
			get { return ((ISessionStateChangeTrigger)iTrigger).StateChange; }
			set { ((ISessionStateChangeTrigger)iTrigger).StateChange = value; }
		}

		public string UserId
		{
			get { return ((ISessionStateChangeTrigger)iTrigger).UserId; }
			set { ((ISessionStateChangeTrigger)iTrigger).UserId = value; }
		}
	}

	internal class V2TimeTrigger : V2Trigger, ITimeTriggerImpl
	{
		static V2TimeTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2TimeTrigger)); }

		public V2TimeTrigger(ITrigger t) : base(t) { }

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((ITimeTrigger)iTrigger).Delay); }
			set { ((ITimeTrigger)iTrigger).Delay = TimeSpanToString(value); }
		}
	}

	internal class V2WeeklyTrigger : V2Trigger, IWeeklyTriggerImpl
	{
		static V2WeeklyTrigger() { TaskServiceFactory.RegisterType(TaskServiceEngine.WinV2, typeof(V2WeeklyTrigger)); }

		public V2WeeklyTrigger(ITrigger t) : base(t) { }

		public DaysOfTheWeek DaysOfWeek
		{
			get { return (DaysOfTheWeek)((IWeeklyTrigger)iTrigger).DaysOfWeek; }
			set { ((IWeeklyTrigger)iTrigger).DaysOfWeek = (short)value; }
		}

		public override TimeSpan? Delay
		{
			get { return StringToTimeSpan(((IWeeklyTrigger)iTrigger).RandomDelay); }
			set { ((IWeeklyTrigger)iTrigger).RandomDelay = TimeSpanToString(value); }
		}

		public short WeeksInterval
		{
			get { return ((IWeeklyTrigger)iTrigger).WeeksInterval; }
			set { ((IWeeklyTrigger)iTrigger).WeeksInterval = value; }
		}
	}

	internal class V2TriggerCollection : IList<ITriggerModel>, IDisposable
	{
		private ITriggerCollection triggers;

		public V2TriggerCollection(ITriggerCollection triggers)
		{
			this.triggers = triggers;
		}

		public ITriggerModel this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Add(ITriggerModel item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(ITriggerModel item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(ITriggerModel[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public IEnumerator<ITriggerModel> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public int IndexOf(ITriggerModel item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, ITriggerModel item)
		{
			throw new NotImplementedException();
		}

		public bool Remove(ITriggerModel item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}

}
