using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed class TaskCollection : ICollection<Task>
	{
		private V1Interop.ITaskScheduler v1TS = null;
		private V2Interop.IRegisteredTaskCollection v2Coll = null;

		internal TaskCollection(V1Interop.ITaskScheduler ts)
		{
			v1TS = ts;
		}

		internal TaskCollection(V2Interop.IRegisteredTaskCollection iTaskColl)
		{
			v2Coll = iTaskColl;
		}

		public IEnumerator<Task> GetEnumerator()
		{
			if (v1TS != null)
				return new V1TaskEnumerator(v1TS);
			return new V2TaskEnumerator(v2Coll);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public class V1TaskEnumerator : IEnumerator<Task>
		{
			private V1Interop.IEnumWorkItems wienum = null;
			private V1Interop.ITaskScheduler m_ts = null;
			private Guid ITaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
			private string curItem = null;

			/// <summary>
			/// Internal constructor - Only accessable through <see cref="Scheduler.Tasks.GetEnumerator()"/>
			/// </summary>
			/// <param name="ts">ITaskScheduler instance</param>
			internal V1TaskEnumerator(V1Interop.ITaskScheduler ts)
			{
				m_ts = ts;
				wienum = m_ts.Enum();
				Reset();
			}

			/// <summary>
			/// Retrieves the current task.  See <see cref="System.Collections.IEnumerator.Current"/> for more information.
			/// </summary>
			public Microsoft.Win32.TaskScheduler.Task Current
			{
				get { return new Task(m_ts.Activate(curItem, ref ITaskGuid)); }
			}

			public void Dispose()
			{
				if (wienum != null) Marshal.ReleaseComObject(wienum);
				m_ts = null;
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			/// <summary>
			/// Moves to the next task. See <see cref="IEnumerator.MoveNext()"/> for more information.
			/// </summary>
			/// <returns>true if next task found, false if no more tasks.</returns>
			public bool MoveNext()
			{
				IntPtr names = IntPtr.Zero;
				curItem = null;
				try
				{
					uint uFetched = 0;
					wienum.Next(1, out names, out uFetched);
					if (uFetched == 1)
					{
						using (V1Interop.CoTaskMemString name = new V1Interop.CoTaskMemString(Marshal.ReadIntPtr(names)))
							curItem = name.ToString();
					}
				}
				catch { }
				finally
				{
					Marshal.FreeCoTaskMem(names);
				}
				return (curItem != null);
			}

			/// <summary>
			/// Reset task enumeration. See <see cref="IEnumerator.Reset()"/> for more information.
			/// </summary>
			public void Reset()
			{
				curItem = null;
				wienum.Reset();
			}
		}

		public class V2TaskEnumerator : IEnumerator<Task>
		{
			private System.Collections.IEnumerator iEnum;

			internal V2TaskEnumerator(TaskScheduler.V2Interop.IRegisteredTaskCollection iTaskColl)
			{
				iEnum = iTaskColl.GetEnumerator();
			}

			public Task Current
			{
				get { return new Task((TaskScheduler.V2Interop.IRegisteredTask)iEnum.Current); }
			}

			public void Dispose()
			{
				iEnum = null;
			}

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
		}

		public void Add(Task item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(Task item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Task[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Count;
				int i = 0;
				V1TaskEnumerator v1te = new V1TaskEnumerator(v1TS);
				while (v1te.MoveNext())
					i++;
				return i;
			}
		}

		public Task this[string index]
		{
			get
			{
				if (v2Coll != null)
					return new Task(v2Coll[index]);
				Guid ITaskGuid = Marshal.GenerateGuidForType(typeof(V1Interop.ITask));
				return new Task(v1TS.Activate(index, ref ITaskGuid));
			}
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(Task item)
		{
			throw new NotSupportedException();
		}
	}

	public sealed class RunningTaskCollection : IEnumerable<RunningTask>
	{
		private TaskScheduler.V1Interop.ITaskScheduler v1TS = null;
		private TaskScheduler.V2Interop.IRunningTaskCollection v2Coll = null;

		internal RunningTaskCollection(TaskScheduler.V1Interop.ITaskScheduler ts)
		{
			v1TS = ts;
		}

		internal RunningTaskCollection(TaskScheduler.V2Interop.IRunningTaskCollection iTaskColl)
		{
			v2Coll = iTaskColl;
		}

		public IEnumerator<RunningTask> GetEnumerator()
		{
			if (v2Coll != null)
				return new RunningTaskEnumerator(v2Coll);
			return new V1RunningTaskEnumerator(v1TS);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public class V1RunningTaskEnumerator : IEnumerator<RunningTask>
		{
			private TaskCollection.V1TaskEnumerator tEnum;

			internal V1RunningTaskEnumerator(V1Interop.ITaskScheduler ts)
			{
				tEnum = new TaskCollection.V1TaskEnumerator(ts);
			}

			public bool MoveNext()
			{
				if (tEnum.MoveNext())
				{
					if (tEnum.Current.State == TaskState.Running)
						return true;
					return this.MoveNext();
				}
				return false;
			}

			public RunningTask Current
			{
				get { return new RunningTask(tEnum.Current); }
			}

			public void Dispose()
			{
				tEnum.Dispose();
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public void Reset()
			{
				tEnum.Reset();
			}
		}

		public class RunningTaskEnumerator : IEnumerator<RunningTask>
		{
			private System.Collections.IEnumerator iEnum;

			internal RunningTaskEnumerator(TaskScheduler.V2Interop.IRunningTaskCollection iTaskColl)
			{
				iEnum = iTaskColl.GetEnumerator();
			}

			public RunningTask Current
			{
				get { return new RunningTask((TaskScheduler.V2Interop.IRunningTask)iEnum.Current); }
			}

			public void Dispose()
			{
				iEnum = null;
			}

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
		}

	}

}
