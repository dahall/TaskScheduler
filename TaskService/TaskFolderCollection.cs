﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Provides information and control for a collection of folders that contain tasks.
	/// </summary>
	public sealed class TaskFolderCollection : ICollection<TaskFolder>, IDisposable
	{
		private readonly TaskFolder parent;
		private readonly TaskFolder[] v1FolderList;
		private readonly V2Interop.ITaskFolderCollection v2FolderList;

		internal TaskFolderCollection()
		{
			v1FolderList = new TaskFolder[0];
		}

		internal TaskFolderCollection([NotNull] TaskFolder folder, [NotNull] V2Interop.ITaskFolderCollection iCollection)
		{
			parent = folder;
			v2FolderList = iCollection;
		}

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		public int Count => v2FolderList?.Count ?? v1FolderList.Length;

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
		/// </summary>
		bool ICollection<TaskFolder>.IsReadOnly => false;

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
					return new TaskFolder(parent.TaskService, v2FolderList[++index]);
				return v1FolderList[index];
			}
		}

		/// <summary>
		/// Gets the specified folder from the collection.
		/// </summary>
		/// <param name="path">The path of the folder to be retrieved.</param>
		/// <returns>A TaskFolder instance that represents the requested folder.</returns>
		public TaskFolder this[[NotNull] string path]
		{
			get
			{
				try
				{
					if (v2FolderList != null)
						return parent.GetFolder(path);
					if (v1FolderList != null && v1FolderList.Length > 0 && (path == string.Empty || path == "\\"))
						return v1FolderList[0];
				}
				catch { }
				throw new ArgumentException(@"Path not found", nameof(path));
			}
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <exception cref="System.NotImplementedException">This action is technically unfeasible due to limitations of the underlying library. Use the <see cref="TaskFolder.CreateFolder(string, string, bool)"/> instead.</exception>
		public void Add([NotNull] TaskFolder item) { throw new NotImplementedException(); }

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		public void Clear()
		{
			if (v2FolderList != null)
			{
				for (int i = v2FolderList.Count; i > 0; i--)
					parent.DeleteFolder(v2FolderList[i].Name, false);
			}
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		public bool Contains([NotNull] TaskFolder item)
		{
			if (v2FolderList != null)
			{
				for (int i = v2FolderList.Count; i > 0; i--)
					if (string.Equals(item.Path, v2FolderList[i].Path, StringComparison.CurrentCultureIgnoreCase))
						return true;
			}
			else
				return item.Path == "\\";
			return false;
		}

		/// <summary>
		/// Copies the elements of the ICollection to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from <see cref="ICollection{T}"/>. The Array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		public void CopyTo(TaskFolder[] array, int arrayIndex)
		{
			if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (array == null) throw new ArgumentNullException(nameof(array));
		    if (v2FolderList != null)
		    {
		        if (arrayIndex + Count > array.Length)
		            throw new ArgumentException();
		        for (var x = 0; x < v2FolderList.Count; x++)
		            array[arrayIndex++] = new TaskFolder(parent.TaskService, v2FolderList[x]);
		    }
		    else
		    {
		        if (arrayIndex + v1FolderList.Length > array.Length)
		            throw new ArgumentException();
		        v1FolderList.CopyTo(array, arrayIndex);
		    }
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

		/// <summary>
		/// Determines whether the specified folder exists.
		/// </summary>
		/// <param name="path">The path of the folder.</param>
		/// <returns>true if folder exists; otherwise, false.</returns>
		public bool Exists([NotNull] string path)
		{
			try
			{
				parent.GetFolder(path);
				return true;
			}
			catch { }
			return false;
		}

		/// <summary>
		/// Gets a list of items in a collection.
		/// </summary>
		/// <returns>Enumerated list of items in the collection.</returns>
		public IEnumerator<TaskFolder> GetEnumerator()
		{
			if (v2FolderList != null)
				return new System.Runtime.InteropServices.ComEnumerator<TaskFolder>(() => v2FolderList.Count, n => v2FolderList[n], o => new TaskFolder(parent.TaskService, (V2Interop.ITaskFolder)o));
			return Array.AsReadOnly(v1FolderList).GetEnumerator();
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
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		public bool Remove([NotNull] TaskFolder item)
		{
			if (v2FolderList != null)
			{
				for (int i = v2FolderList.Count; i > 0; i--)
				{
					if (string.Equals(item.Path, v2FolderList[i].Path, StringComparison.CurrentCultureIgnoreCase))
					{
						try
						{
							parent.DeleteFolder(v2FolderList[i].Name);
						}
						catch
						{
							return false;
						}
						return true;
					}
				}
			}
			return false;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
