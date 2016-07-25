using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Win32.TaskScheduler.V1Interop
{
	internal class V1Trigger : ITriggerImpl
	{
		private ITaskTrigger v1Trigger;
		private TaskTrigger v1TriggerData;

		public V1Trigger() { }

		public object BaseObject { get; set; }

		public bool Enabled
		{
			get { return !v1TriggerData.Flags.IsFlagSet(V1Interop.TaskTriggerFlags.Disabled); } 
			set
			{
				v1TriggerData.Flags.SetFlags(V1Interop.TaskTriggerFlags.Disabled, !value);
				SetData();
			}
		}

		public DateTime? EndBoundary
		{
			get { return v1TriggerData.EndDate; }
			set
			{
				v1TriggerData.EndDate = value;
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
			get { return TimeSpan.FromMinutes(v1TriggerData.MinutesDuration); }
			set
			{
				v1TriggerData.MinutesDuration = (uint)value.GetValueOrDefault(TimeSpan.Zero).TotalMinutes;
				SetData();
			}
		}

		public TimeSpan? RepetitionInterval
		{
			get { return TimeSpan.FromMinutes(v1TriggerData.MinutesInterval); }
			set
			{
				if (value != TimeSpan.Zero && value < TimeSpan.FromMinutes(1))
					throw new ArgumentOutOfRangeException(nameof(RepetitionInterval));
				v1TriggerData.MinutesInterval = (uint)value.GetValueOrDefault(TimeSpan.Zero).TotalMinutes;
				SetData();
			}
		}

		public bool RepetitionStopAtDurationEnd
		{
			get { return v1TriggerData.Flags.IsFlagSet(TaskTriggerFlags.KillAtDurationEnd); }
			set
			{
				v1TriggerData.Flags.SetFlags(TaskTriggerFlags.KillAtDurationEnd, value);
				SetData();
			}
		}

		public DateTime StartBoundary
		{
			get { return v1TriggerData.BeginDate; }
			set
			{
				v1TriggerData.BeginDate = value;
				SetData();
			}
		}

		private void SetData()
		{
			if (v1TriggerData.MinutesInterval != 0 && v1TriggerData.MinutesInterval >= v1TriggerData.MinutesDuration)
				throw new ArgumentException("Trigger repetition interval must be less than trigger repetition duration under Task Scheduler 1.0.");
			if (v1TriggerData.BeginDate == DateTime.MinValue)
				v1TriggerData.BeginDate = DateTime.Now;
			v1Trigger.SetTrigger(ref v1TriggerData);
			System.Diagnostics.Debug.WriteLine(v1TriggerData);
		}
	}
}
