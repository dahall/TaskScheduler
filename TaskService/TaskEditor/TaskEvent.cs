using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Win32.TaskScheduler
{
    internal sealed class TaskEvent
    {
        internal TaskEvent(EventRecord rec)
        {
            this.EventId = rec.Id;
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

        public Guid? ActivityId
        {
            get; internal set;
        }

        //public string Description { get; internal set; }
        public int EventId
        {
            get; internal set;
        }

        public string Level
        {
            get; internal set;
        }

        public string OpCode
        {
            get; internal set;
        }

        public int? ProcessId
        {
            get; internal set;
        }

        public long? RecordId
        {
            get; internal set;
        }

        public string TaskCategory
        {
            get; internal set;
        }

        public DateTime? TimeCreated
        {
            get; internal set;
        }

        public System.Security.Principal.SecurityIdentifier UserId
        {
            get; internal set;
        }

        public byte? Version
        {
            get; internal set;
        }

        public override string ToString()
        {
            return TaskCategory;
        }
    }

    internal sealed class TaskEventEnumerator : IEnumerator<TaskEvent>
    {
        private EventRecord curRec;
        private EventLogReader log;

        internal TaskEventEnumerator(EventLogReader log)
        {
            this.log = log;
        }

        public TaskEvent Current
        {
            get { return new TaskEvent(curRec); }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        public void Dispose()
        {
            log.Dispose();
            log = null;
        }

        public bool MoveNext()
        {
            return (curRec = log.ReadEvent()) != null;
        }

        public void Reset()
        {
            log.Seek(System.IO.SeekOrigin.Begin, 0L);
        }
    }

    internal sealed class TaskEventLog : IEnumerable<TaskEvent>
    {
        private EventLogQuery q;

        public TaskEventLog(string taskPath)
            : this(".", taskPath)
        {
        }

        public TaskEventLog(string machineName, string taskPath)
        {
            const string queryString =
                "<QueryList>" +
                "  <Query Id=\"0\" Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
                "    <Select Path=\"Microsoft-Windows-TaskScheduler/Operational\">" +
                "        *[EventData[Data[@Name=\"TaskName\"]=\"{0}\"]]" +
                "    </Select>" +
                "  </Query>" +
                "</QueryList>";

            q = new EventLogQuery("Microsoft-Windows-TaskScheduler/Operational", PathType.LogName, string.Format(queryString, taskPath));
        }

        public IEnumerator<TaskEvent> GetEnumerator()
        {
            return new TaskEventEnumerator(new EventLogReader(q));
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}