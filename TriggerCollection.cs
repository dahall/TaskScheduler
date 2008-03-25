using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed class TriggerCollection : IEnumerable<Trigger>, IDisposable
	{
		private V1Interop.ITask v1Task = null;
		private V2Interop.ITriggerCollection v2Coll = null;

		internal TriggerCollection(V1Interop.ITask iTask)
		{
			v1Task = iTask;
		}

		internal TriggerCollection(V2Interop.ITriggerCollection iColl)
		{
			v2Coll = iColl;
		}

		public void Dispose()
		{
			if (v2Coll != null) Marshal.ReleaseComObject(v2Coll);
			v1Task = null;
		}

		#region IEnumerable<Trigger> Members

		public IEnumerator<Trigger> GetEnumerator()
		{
			if (v1Task != null)
				return new V1TriggerEnumerator(v1Task);
			return new V2TriggerEnumerator(v2Coll);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

		public sealed class V1TriggerEnumerator : IEnumerator<Trigger>
		{
			private V1Interop.ITask iTask;
			private short curItem = -1;

			internal V1TriggerEnumerator(V1Interop.ITask task)
			{
				iTask = task;
			}

			public Trigger Current
			{
				get
				{
					return Trigger.CreateTrigger(iTask.GetTrigger((ushort)curItem));
				}
			}

			public void Dispose()
			{
				iTask = null;
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				if (++curItem >= iTask.GetTriggerCount())
					return false;
				return true;
			}

			public void Reset()
			{
				curItem = -1;
			}
		}

		public sealed class V2TriggerEnumerator : IEnumerator<Trigger>
		{
			private System.Collections.IEnumerator iEnum;

			internal V2TriggerEnumerator(V2Interop.ITriggerCollection iColl)
			{
				iEnum = iColl.GetEnumerator();
			}

			#region IEnumerator<Trigger> Members

			public Trigger Current
			{
				get
				{
					return Trigger.CreateTrigger((V2Interop.ITrigger)iEnum.Current);
				}
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				iEnum = null;
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				return iEnum.MoveNext();
			}

			public void Reset()
			{
				iEnum.Reset();
			}

			#endregion
		}

		public void Add(Trigger trigger)
		{
			throw new NotImplementedException();
		}

		public Trigger AddNew(TaskTriggerType taskTriggerType)
		{
			if (v1Task != null)
			{
				if (taskTriggerType == TaskTriggerType.Registration || taskTriggerType == TaskTriggerType.Event || taskTriggerType == TaskTriggerType.SessionStateChange)
					throw new NotSupportedException();
				int v1tt = (int)taskTriggerType - 1;
				if (v1tt >= 7) v1tt--;
				ushort idx;
				return Trigger.CreateTrigger(v1Task.CreateTrigger(out idx), (V1Interop.TaskTriggerType)v1tt);
			}

			return Trigger.CreateTrigger(v2Coll.Create(taskTriggerType));
		}

		internal void Bind()
		{
			foreach (Trigger t in this)
				t.Bind();
		}
	}
}
