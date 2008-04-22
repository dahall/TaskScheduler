using System;
using System.Collections.Generic;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Provides information and control for a collection of folders that contain tasks.
	/// </summary>
	public sealed class TaskFolderCollection : IEnumerable<TaskFolder>
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

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
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

		/*
		/// <summary>
		/// Returns the index of the TaskFolder within the collection.
		/// </summary>
		/// <param name="item">TaskFolder to find.</param>
		/// <returns>Index of the TaskFolder; -1 if not found.</returns>
		public int IndexOf(TaskFolder item)
		{
			return IndexOf(item.Path);
		}

		/// <summary>
		/// Returns the index of the TaskFolder within the collection.
		/// </summary>
		/// <param name="path">Path to find.</param>
		/// <returns>Index of the TaskFolder; -1 if not found.</returns>
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
		*/

		/// <summary>
		/// Gets the specified folder from the collection.
		/// </summary>
		/// <param name="index">The index of the folder to be retrieved.</param>
		/// <returns>A TaskFolder instance that represents the requested folder.</returns>
		public TaskFolder this[int index]
		{
			get
			{
				if (v2FolderList != null)
					return new TaskFolder(v2FolderList[++index]);
				return v1FolderList[index];
			}
		}

		/// <summary>
		/// Gets the specified folder from the collection.
		/// </summary>
		/// <param name="path">The path of the folder to be retrieved.</param>
		/// <returns>A TaskFolder instance that represents the requested folder.</returns>
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

		/*
		/// <summary>
		/// Determines whether the collection contains a specific key and value.
		/// </summary>
		/// <param name="item">The TaskFolder to locate in the collection.</param>
		/// <returns>true if item is found in the collection; otherwise, false.</returns>
		public bool Contains(TaskFolder item)
		{
			return IndexOf(item) != -1;
		}
		*/

		/// <summary>
		/// Copies the elements of the ICollection to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from <see cref="ICollection{T}"/>. The Array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		internal void CopyTo(TaskFolder[] array, int arrayIndex)
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

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		public int Count
		{
			get { return (v2FolderList != null) ? v2FolderList.Count : v1FolderList.Length; }
		}

		/// <summary>
		/// Gets a list of items in a collection.
		/// </summary>
		/// <returns>Enumerated list of items in the collection.</returns>
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

			/// <summary>
			/// Releases all resources used by this class.
			/// </summary>
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
