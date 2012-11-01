using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Historical event information for a task.
	/// </summary>
	public sealed class TaskEvent
	{
		internal TaskEvent(EventRecord rec)
		{
			this.EventId = rec.Id;
			this.EventRecord = rec;
			this.Version = rec.Version;
			this.TaskCategory = rec.TaskDisplayName;
			this.OpCode = rec.OpcodeDisplayName;
			this.TimeCreated = rec.TimeCreated;
			this.RecordId = rec.RecordId;
			this.ActivityId = rec.ActivityId;
			this.Level = rec.LevelDisplayName;
			this.UserId = rec.UserId;
			this.ProcessId = rec.ProcessId;
		}

		/// <summary>
		/// Gets the activity id.
		/// </summary>
		public Guid? ActivityId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the event id.
		/// </summary>
		public int EventId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the underlying <see cref="EventRecord"/>.
		/// </summary>
		public EventRecord EventRecord
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the level.
		/// </summary>
		public string Level
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the op code.
		/// </summary>
		public string OpCode
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the process id.
		/// </summary>
		public int? ProcessId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the record id.
		/// </summary>
		public long? RecordId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the task category.
		/// </summary>
		public string TaskCategory
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the time created.
		/// </summary>
		public DateTime? TimeCreated
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the user id.
		/// </summary>
		public System.Security.Principal.SecurityIdentifier UserId
		{
			get; internal set;
		}

		/// <summary>
		/// Gets the version.
		/// </summary>
		public byte? Version
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
			return TaskCategory;
		}
	}

	/// <summary>
	/// An enumerator over a task's history of events.
	/// </summary>
	public sealed class TaskEventEnumerator : IEnumerator<TaskEvent>
	{
		private EventRecord curRec;
		private EventLogReader log;

		internal TaskEventEnumerator(EventLogReader log)
		{
			this.log = log;
		}

		/// <summary>
		/// Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <returns>
		/// The element in the collection at the current position of the enumerator.
		///   </returns>
		public TaskEvent Current
		{
			get { return new TaskEvent(curRec); }
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
			log.Dispose();
			log = null;
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
			return (curRec = log.ReadEvent()) != null;
		}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first element in the collection.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// The collection was modified after the enumerator was created.
		///   </exception>
		public void Reset()
		{
			log.Seek(System.IO.SeekOrigin.Begin, 0L);
		}
	}

	/// <summary>
	/// Historical event log for a task. Only available for Windows Vista and Windows Server 2008 and later systems.
	/// </summary>
	public sealed class TaskEventLog : IEnumerable<TaskEvent>
	{
		private EventLogQuery q;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventLog"/> class.
		/// </summary>
		/// <param name="taskPath">The task path. This can be retrieved using the <see cref="Task.Path"/> property.</param>
		/// <exception cref="NotSupportedException">Thrown when instantiated on an OS prior to Windows Vista.</exception>
		public TaskEventLog(string taskPath)
			: this(".", taskPath)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventLog"/> class.
		/// </summary>
		/// <param name="machineName">Name of the machine.</param>
		/// <param name="taskPath">The task path. This can be retrieved using the <see cref="Task.Path"/> property.</param>
		/// <exception cref="NotSupportedException">Thrown when instantiated on an OS prior to Windows Vista.</exception>
		public TaskEventLog(string machineName, string taskPath)
		{
			if (System.Environment.OSVersion.Version.Major < 6)
				throw new NotSupportedException("Enumeration of task history not available on systems prior to Windows Vista and Windows Server 2008.");

			const string queryString =
				"<QueryList>" +
				"  <Query Id=\"0\" Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
				"    <Select Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
				"        *[EventData[Data[@Name=\"TaskName\"]=\"{0}\"]]" +
				"    </Select>" +
				"  </Query>" +
				"</QueryList>";

			q = new EventLogQuery("Microsoft-Windows-TaskScheduler/Operational", PathType.LogName, string.Format(queryString, taskPath)) { ReverseDirection = true };
			if (machineName != null && machineName != "." && !machineName.Equals(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase))
				q.Session = new EventLogSession(machineName);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<TaskEvent> GetEnumerator()
		{
			return new TaskEventEnumerator(new EventLogReader(q));
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