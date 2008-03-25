using System;
using System.Collections.Generic;

namespace Microsoft.Win32.TaskScheduler
{
	public class TaskFolderCollection : ICollection<TaskFolder>
	{
		private TaskScheduler.V2Interop.ITaskFolderCollection v2FolderList = null;
		private TaskFolder[] v1FolderList = null;

		internal TaskFolderCollection()
		{
			v1FolderList = new TaskFolder[0];
		}

		internal TaskFolderCollection(TaskFolder v1Folder)
		{
			v1FolderList = new TaskFolder[] { v1Folder };
		}

		internal TaskFolderCollection(TaskScheduler.V2Interop.ITaskFolderCollection iCollection)
		{
			v2FolderList = iCollection;
		}

		public void Dispose()
		{
			if (v1FolderList != null && v1FolderList.Length > 0)
			{
				v1FolderList[0].Dispose();
				v1FolderList[0] = null;
			}
			if (v2FolderList != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(v2FolderList);
		}

		public void Add(TaskFolder item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(TaskFolder item)
		{
			throw new NotSupportedException();
		}

		public int IndexOf(TaskFolder item)
		{
			return IndexOf(item.Path);
		}

		public int IndexOf(string path)
		{
			if (v2FolderList != null)
			{
				for (int i = 0; i < v2FolderList.Count; i++)
				{
					if (v2FolderList[new System.Runtime.InteropServices.VariantWrapper(i)].Path == path)
						return i;
				}
				return -1;
			}
			else
				return (v1FolderList.Length > 0 && (path == string.Empty || path == "\\")) ? 0 : -1;
		}

		public TaskFolder this[int index]
		{
			get
			{
				if (v2FolderList != null)
					return new TaskFolder(v2FolderList[index]);
				return v1FolderList[index];
			}
		}

		public TaskFolder this[string path]
		{
			get
			{
				if (v2FolderList != null)
					return new TaskFolder(v2FolderList[path]);
				if (v1FolderList != null && v1FolderList.Length > 0 && (path == string.Empty || path == "\\"))
					return v1FolderList[0];
				throw new ArgumentException("Path not found");
			}
		}

		public bool Contains(TaskFolder item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(TaskFolder[] array, int arrayIndex)
		{
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException();
			if (array == null) throw new ArgumentNullException();
			if (v2FolderList != null)
			{
				if (arrayIndex + this.Count > array.Length)
					throw new ArgumentException();
				foreach (TaskScheduler.V2Interop.ITaskFolder f in v2FolderList)
					array[arrayIndex++] = new TaskFolder(f);
			}
			else
			{
				if (arrayIndex > v1FolderList.Length - 1)
					throw new ArgumentException();
				v1FolderList.CopyTo(array, arrayIndex);
			}
		}

		public int Count
		{
			get { return (v2FolderList != null) ? v2FolderList.Count : v1FolderList.Length; }
		}

		public IEnumerator<TaskFolder> GetEnumerator()
		{
			TaskFolder[] eArray = new TaskFolder[this.Count];
			this.CopyTo(eArray, 0);
			return new TaskFolderEnumerator(eArray);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private class TaskFolderEnumerator : IEnumerator<TaskFolder>
		{
			private TaskFolder[] folders = null;
			private System.Collections.IEnumerator iEnum = null;

			internal TaskFolderEnumerator(TaskFolder[] f)
			{
				folders = f;
				iEnum = f.GetEnumerator();
			}

			public void Dispose()
			{
			}

			public TaskFolder Current
			{
				get { return iEnum.Current as TaskFolder; }
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
