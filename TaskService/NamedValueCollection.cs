using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Contains a collection of name-value pairs.
	/// </summary>
	public class NamedValueCollection : IDisposable, System.Collections.IEnumerable
	{
		private V2Interop.ITaskNamedValueCollection v2Coll = null;
		private Dictionary<string, string> unboundDict = null;

		internal NamedValueCollection(V2Interop.ITaskNamedValueCollection iColl) { v2Coll = iColl; }

		internal NamedValueCollection()
		{
			unboundDict = new Dictionary<string, string>(5); ;
		}

		internal bool Bound
		{
			get { return v2Coll != null; }
		}

		internal void Bind(V2Interop.ITaskNamedValueCollection iTaskNamedValueCollection)
		{
			v2Coll = iTaskNamedValueCollection;
			v2Coll.Clear();
			foreach (var item in unboundDict)
				v2Coll.Create(item.Key, item.Value);
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (v2Coll != null) Marshal.ReleaseComObject(v2Coll);
		}

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		public int Count
		{
			get { return v2Coll != null ? v2Coll.Count : unboundDict.Count; }
		}

		/// <summary>
		/// Gets the value of the item at the specified index.
		/// </summary>
		/// <param name="index">The index of the item being requested.</param>
		/// <returns>The value of the name-value pair at the specified index.</returns>
		public string this[int index]
		{
			get
			{
				return v2Coll[index].Value;
			}
		}

		// TODO: Figure out how to make this more of a real collection
		/*public string this[string name]
		{
			get { throw new NotImplementedException(); }
		}*/

		/// <summary>
		/// Adds a name-value pair to the collection.
		/// </summary>
		/// <param name="Name">The name associated with a value in a name-value pair.</param>
		/// <param name="Value">The value associated with a name in a name-value pair.</param>
		public void Add(string Name, string Value)
		{
			if (v2Coll != null)
				v2Coll.Create(Name, Value);
			else
				unboundDict.Add(Name, Value);
		}

		/// <summary>
		/// Removes a selected name-value pair from the collection.
		/// </summary>
		/// <param name="index">Index of the pair to remove.</param>
		public void Remove(int index)
		{
			v2Coll.Remove(index);
		}

		/// <summary>
		/// Clears the entire collection of name-value pairs.
		/// </summary>
		public void Clear()
		{
			if (v2Coll != null)
				v2Coll.Clear();
			else
				unboundDict.Clear();
		}

		/// <summary>
		/// Gets the collection enumerator for the name-value collection.
		/// </summary>
		/// <returns>An <see cref="System.Collections.IEnumerator"/> for the collection.</returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			if (v2Coll != null)
				return v2Coll.GetEnumerator();
			else
				return unboundDict.GetEnumerator();
		}
	}
}
