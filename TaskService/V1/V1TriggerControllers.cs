using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler.V1Interop
{
	internal abstract class V1Trigger : ITriggerImpl, IDisposable
	{
		protected ITaskTrigger iTrigger;
		protected TaskTrigger triggerData;

		public V1Trigger(ITaskTrigger trigger, TaskTriggerType type)
		{
			iTrigger = trigger;
			triggerData = trigger.GetTrigger();
			triggerData.Type = type;
		}

		public virtual TimeSpan? Delay
		{
			get { return null; }
			set { throw new NotV1SupportedException(); }
		}

		public bool Enabled
		{
			get { return !triggerData.Flags.IsFlagSet(TaskTriggerFlags.Disabled); } 
			set
			{
				triggerData.Flags.SetFlags(TaskTriggerFlags.Disabled, !value);
				SetData();
			}
		}

		public DateTime? EndBoundary
		{
			get { return triggerData.EndDate; }
			set
			{
				triggerData.EndDate = value;
				SetData();
			}
		}

		public TimeSpan? ExecutionTimeLimit
		{
			get { return null; }
			set { throw new NotV1SupportedException(); }
		}

		public string Id
		{
			get { return null; }
			set { throw new NotV1SupportedException(); }
		}

		public TimeSpan? RepetitionDuration
		{
			get { return TimeSpan.FromMinutes(triggerData.MinutesDuration); }
			set
			{
				triggerData.MinutesDuration = (uint)value.GetValueOrDefault(TimeSpan.Zero).TotalMinutes;
				SetData();
			}
		}

		public TimeSpan? RepetitionInterval
		{
			get { return TimeSpan.FromMinutes(triggerData.MinutesInterval); }
			set
			{
				if (value != TimeSpan.Zero && value < TimeSpan.FromMinutes(1))
					throw new ArgumentOutOfRangeException(nameof(RepetitionInterval));
				triggerData.MinutesInterval = (uint)value.GetValueOrDefault(TimeSpan.Zero).TotalMinutes;
				SetData();
			}
		}

		public bool RepetitionStopAtDurationEnd
		{
			get { return triggerData.Flags.IsFlagSet(TaskTriggerFlags.KillAtDurationEnd); }
			set
			{
				triggerData.Flags.SetFlags(TaskTriggerFlags.KillAtDurationEnd, value);
				SetData();
			}
		}

		public DateTime StartBoundary
		{
			get { return triggerData.BeginDate; }
			set
			{
				triggerData.BeginDate = value;
				SetData();
			}
		}

		public TaskScheduler.TaskTriggerType TriggerType => ConvertFromV1TriggerType(triggerData.Type);

		public virtual void Dispose()
		{
			if (iTrigger != null)
			{
				Marshal.ReleaseComObject(iTrigger);
				iTrigger = null;
			}
		}

		protected void SetData()
		{
			if (triggerData.MinutesInterval != 0 && triggerData.MinutesInterval >= triggerData.MinutesDuration)
				throw new ArgumentException("Trigger repetition interval must be less than trigger repetition duration under Task Scheduler 1.0.");
			if (triggerData.BeginDate == DateTime.MinValue)
				triggerData.BeginDate = DateTime.Now;
			iTrigger.SetTrigger(ref triggerData);
			System.Diagnostics.Debug.WriteLine(triggerData);
		}

		internal static TaskScheduler.TaskTriggerType ConvertFromV1TriggerType(TaskTriggerType v1Type)
		{
			int v2tt = (int)v1Type + 1;
			if (v2tt > 6) v2tt++;
			return (TaskScheduler.TaskTriggerType)v2tt;
		}

		internal static TaskTriggerType ConvertToV1TriggerType(TaskScheduler.TaskTriggerType type)
		{
			if (type == TaskScheduler.TaskTriggerType.Registration || type == TaskScheduler.TaskTriggerType.Event || type == TaskScheduler.TaskTriggerType.SessionStateChange)
				throw new NotV1SupportedException();
			int v1tt = (int)type - 1;
			if (v1tt >= 7) v1tt--;
			return (TaskTriggerType)v1tt;
		}
	}
}
