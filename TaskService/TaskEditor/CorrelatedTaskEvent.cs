#if NET_35_OR_GREATER
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Historical event information for a task.
	/// </summary>
	public sealed class CorrelatedTaskEvent
	{
		/// <summary>
		/// Status of correlated task events
		/// </summary>
		public enum Status
		{
			/// <summary>Task is still running.</summary>
			StillRunning,
			/// <summary>Task has successfully completed.</summary>
			Success,
			/// <summary>Task failed to start.</summary>
			Failure,
			/// <summary>Task was terminated due to exceeding time for execution.</summary>
			Terminated
		}

		/// <summary>
		/// Type of trigger that initiated the task execution.
		/// </summary>
		public enum TriggerType
		{
			/// <summary>Scheduled time.</summary>
			Schedule = 107,
			/// <summary>On an event.</summary>
			Event = 108,
			/// <summary>When task is registered.</summary>
			Registration = 109,
			/// <summary>When system is idle.</summary>
			Idle = 117,
			/// <summary>When system boots.</summary>
			Boot = 118,
			/// <summary>When a user logs on.</summary>
			Logon = 119,
			/// <summary>When a session connects.</summary>
			SessionConnect = 120,
			/// <summary>When a session disconnects.</summary>
			SessionDisconnect = 121,
			/// <summary>When a remote session connects.</summary>
			RemoteConnect = 122,
			/// <summary>When a remote session disconnects.</summary>
			RemoteDisconnect = 123,
			/// <summary>When computer is locked.</summary>
			WorkstationLock = 124,
			/// <summary>When computer is unlocked.</summary>
			WorkstationUnlock = 125
		}

		private List<EventRecord> records = new List<EventRecord>();

		internal CorrelatedTaskEvent(Guid activityId, string taskName)
		{
			this.ActivityId = activityId;
			this.TaskName = taskName;
			this.TriggeredBy = TriggerType.Event;
		}

		/// <summary>
		/// Gets the activity id.
		/// </summary>
		public Guid ActivityId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the error code.
		/// </summary>
		public long ErrorCode { get; internal set; }

		/// <summary>
		/// Gets the underlying <see cref="EventRecord"/> instances.
		/// </summary>
		public IList<EventRecord> EventRecords
		{
			get { return records; }
		}

		/// <summary>
		/// Gets the task name.
		/// </summary>
		public string TaskName
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the time started.
		/// </summary>
		public DateTime RunStart
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the time ended.
		/// </summary>
		public DateTime? RunEnd
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the result status.
		/// </summary>
		public Status RunResult
		{
			get; internal set;
		}

		/// <summary>
		/// Gets how the task was triggered.
		/// </summary>
		public TriggerType TriggeredBy
		{
			get; internal set;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return TaskName + ActivityId.ToString("D");
		}

		internal void SetCompletion(int code, DateTime end)
		{
			this.RunEnd = end;
			switch (code)
			{
				case 102:
					this.RunResult = Status.Success;
					break;
				case 103:
					this.RunResult = Status.Failure;
					break;
				case 111:
					this.RunResult = Status.Terminated;
					break;
				default:
					break;
			}
		}
	}

	/// <summary>
	/// An enumerator over a task's history of events.
	/// </summary>
	public sealed class CorrelatedTaskEventEnumerator : IEnumerator<CorrelatedTaskEvent>
	{
		private TaskEventLog log;
		private IEnumerator<IGrouping<Guid, TaskEvent>> query;

		internal CorrelatedTaskEventEnumerator(TaskEventLog log)
		{
			this.log = log;
			this.query = log.OrderBy(ev => ev).GroupBy(ev => ev.ActivityId.GetValueOrDefault(Guid.Empty), ev => ev).GetEnumerator();
		}

		/// <summary>
		/// Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <returns>
		/// The element in the collection at the current position of the enumerator.
		///   </returns>
		public CorrelatedTaskEvent Current
		{
			get
			{
				CorrelatedTaskEvent ce = null;
				foreach (var item in query.Current)
				{
					if (ce == null)
						ce = new CorrelatedTaskEvent(item.ActivityId.GetValueOrDefault(), item.TaskPath);
					ce.EventRecords.Add(item.EventRecord);
					switch (item.EventId)
					{
						case 100:
							ce.RunStart = item.TimeCreated.Value;
							break;
						case 102:
						case 111:
							ce.SetCompletion(item.EventId, item.TimeCreated.Value);
							break;
						case 103:
							ce.SetCompletion(item.EventId, item.TimeCreated.Value);
							ce.ErrorCode = Convert.ToInt64(item.EventRecord.Properties[3].Value);
							break;
						case 107:
						case 108:
						case 109:
						case 117:
						case 118:
						case 119:
						case 120:
						case 121:
						case 122:
						case 123:
						case 124:
						case 125:
							ce.TriggeredBy = (CorrelatedTaskEvent.TriggerType)item.EventId;
							break;
						default:
							break;
					}
				}
				return ce;
			}
		}

		/// <summary>
		/// Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <returns>
		/// The element in the collection at the current position of the enumerator.
		///   </returns>
		object System.Collections.IEnumerator.Current
		{
			get { return this.Current; }
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.query.Dispose();
			this.log = null;
		}

		/// <summary>
		/// Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns>
		/// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		///   </exception>
		public bool MoveNext()
		{
			return query.MoveNext();
		}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first element in the collection.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		///   </exception>
		public void Reset()
		{
			query.Reset();
		}
	}

	/// <summary>
	/// Historical event log for a task. Only available for Windows Vista and Windows Server 2008 and later systems.
	/// </summary>
	public sealed class CorrelatedTaskEventLog : IEnumerable<CorrelatedTaskEvent>
	{
		private TaskEventLog taskLog;

		/// <summary>
		/// Initializes a new instance of the <see cref="CorrelatedTaskEventLog"/> class that looks at all task events from a specified time.
		/// </summary>
		/// <param name="startTime">The start time.</param>
		/// <param name="taskName">Name of the task.</param>
		/// <param name="machineName">Name of the machine (optional).</param>
		public CorrelatedTaskEventLog(DateTime startTime, string taskName = null, string machineName = null)
		{
			taskLog = new TaskEventLog(startTime, taskName, machineName);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<CorrelatedTaskEvent> GetEnumerator()
		{
			return new CorrelatedTaskEventEnumerator(taskLog);
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
#endif