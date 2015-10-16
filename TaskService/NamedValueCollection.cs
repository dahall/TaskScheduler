using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Pair of name and value.
	/// </summary>
	public class NameValuePair : IXmlSerializable
	{
		private V2Interop.ITaskNamedValuePair v2Pair = null;
		private string name, value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NameValuePair"/> class.
		/// </summary>
		public NameValuePair() { }

		internal NameValuePair(V2Interop.ITaskNamedValuePair iPair)
		{
			v2Pair = iPair;
		}

		internal NameValuePair(string name, string value)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
				throw new ArgumentException("Both name and value must be non-empty strings.");
			this.name = name; this.value = value;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name
		{
			get { return v2Pair == null ? name : v2Pair.Name; }
			set { if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Name)); if (v2Pair == null) name = value; else v2Pair.Name = value; }
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public string Value
		{
			get { return v2Pair == null ? value : v2Pair.Value; }
			set { if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(Value)); if (v2Pair == null) this.value = value; else v2Pair.Value = value; }
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is NameValuePair)
				return ((NameValuePair)obj).Name == Name && ((NameValuePair)obj).Value == Value;
			if (obj is V2Interop.ITaskNamedValuePair)
				return ((V2Interop.ITaskNamedValuePair)obj).Name == Name && ((V2Interop.ITaskNamedValuePair)obj).Value == Value;
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode() => new { A = Name, B = Value }.GetHashCode();

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString() => $"{Name}={Value}";

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			if (reader.MoveToContent() == System.Xml.XmlNodeType.Element && reader.LocalName == "Value")
			{
				Name = reader.GetAttribute("name");
				Value = reader.ReadString();
				reader.Read();
			}
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteAttributeString("name", Name);
			writer.WriteString(Value);
		}
	}

	/// <summary>
	/// Contains a collection of name-value pairs.
	/// </summary>
	public sealed class NamedValueCollection : IDisposable, ICollection<NameValuePair>, IDictionary<string, string>
	{
		private V2Interop.ITaskNamedValueCollection v2Coll = null;
		private List<NameValuePair> unboundDict = null;

		internal NamedValueCollection(V2Interop.ITaskNamedValueCollection iColl) { v2Coll = iColl; }

		internal NamedValueCollection()
		{
			unboundDict = new List<NameValuePair>(5);
		}

		internal bool Bound => v2Coll != null;

		internal void Bind(V2Interop.ITaskNamedValueCollection iTaskNamedValueCollection)
		{
			v2Coll = iTaskNamedValueCollection;
			v2Coll.Clear();
			foreach (var item in unboundDict)
				v2Coll.Create(item.Name, item.Value);
		}

		/// <summary>
		/// Copies current <see cref="NamedValueCollection"/> to another.
		/// </summary>
		/// <param name="destCollection">The destination collection.</param>
		public void CopyTo(NamedValueCollection destCollection)
		{
			if (v2Coll != null)
			{
				for (int i = 1; i <= Count; i++)
					destCollection.Add(v2Coll[i].Name, v2Coll[i].Value);
			}
			else
			{
				foreach (var item in unboundDict)
					destCollection.Add(item.Name, item.Value);
			}
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
		public int Count => v2Coll != null ? v2Coll.Count : unboundDict.Count;

		/// <summary>
		/// Gets a collection of the names.
		/// </summary>
		/// <value>
		/// The names.
		/// </value>
		public ICollection<string> Names
		{
			get
			{
				if (v2Coll == null)
					return unboundDict.ConvertAll<string>(p => p.Name);

				List<string> ret = new List<string>(v2Coll.Count);
				foreach (V2Interop.ITaskNamedValuePair item in v2Coll)
					ret.Add(item.Name);
				return ret;
			}
		}

		/// <summary>
		/// Gets a collection of the values.
		/// </summary>
		/// <value>
		/// The values.
		/// </value>
		public ICollection<string> Values
		{
			get
			{
				if (v2Coll == null)
					return unboundDict.ConvertAll<string>(p => p.Value);

				List<string> ret = new List<string>(v2Coll.Count);
				foreach (V2Interop.ITaskNamedValuePair item in v2Coll)
					ret.Add(item.Value);
				return ret;
			}
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
				if (v2Coll != null)
					return v2Coll[++index].Value;
				return unboundDict[index].Value;
			}
		}

		/// <summary>
		/// Gets the value of the item with the specified name.
		/// </summary>
		/// <param name="name">Name to get the value for.</param>
		/// <returns>Value for the name, or null if not found.</returns>
		public string this[string name]
		{
			get
			{
				string ret;
				TryGetValue(name, out ret);
				return ret;
			}
			set
			{
				if (v2Coll == null)
				{
					int idx = unboundDict.FindIndex(p => p.Name == name);
					var nvp = new NameValuePair(name, value);
					if (idx == -1)
						unboundDict.Add(nvp);
					else
						unboundDict[idx] = nvp;
				}
				else
				{
					var array = new KeyValuePair<string, string>[Count];
					((ICollection<KeyValuePair<string, string>>)this).CopyTo(array, 0);
					int idx = Array.FindIndex(array, p => p.Key == name);
					if (idx == -1)
						v2Coll.Create(name, value);
					else
					{
						array[idx] = new KeyValuePair<string, string>(name, value);
						v2Coll.Clear();
						for (int i = 0; i < array.Length; i++)
							v2Coll.Create(array[i].Key, array[i].Value);
					}
				}
			}
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		public void Add(NameValuePair item)
		{
			Add(item.Name, item.Value);
		}

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
				unboundDict.Add(new NameValuePair(Name, Value));
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
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<NameValuePair> GetEnumerator()
		{
			if (v2Coll == null)
				return unboundDict.GetEnumerator();

			return new ComEnumerator<NameValuePair, V2Interop.ITaskNamedValueCollection>(v2Coll, o => new NameValuePair((V2Interop.ITaskNamedValuePair)o));
		}

		/// <summary>
		/// Removes the name-value pair with the specified key from the collection.
		/// </summary>
		/// <param name="name">The name associated with a value in a name-value pair.</param>
		/// <returns></returns>
		public bool Remove(string name)
		{
			if (v2Coll == null)
			{
				int idx = unboundDict.FindIndex(p => p.Name == name);
				if (idx != -1)
					unboundDict.RemoveAt(idx);
				return (idx != -1);
			}

			for (int i = 0; i < v2Coll.Count; i++)
			{
				if (name == v2Coll[i].Name)
				{
					v2Coll.Remove(i);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Removes a selected name-value pair from the collection.
		/// </summary>
		/// <param name="index">Index of the pair to remove.</param>
		public void RemoveAt(int index)
		{
			if (v2Coll != null)
				v2Coll.Remove(index);
			else
				unboundDict.RemoveAt(index);
		}

		/// <summary>
		/// Gets the value associated with the specified name.
		/// </summary>
		/// <param name="name">The name whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified name, if the name is found; otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if the collection contains an element with the specified name; otherwise, <c>false</c>.</returns>
		public bool TryGetValue(string name, out string value)
		{
			if (v2Coll != null)
			{
				foreach (V2Interop.ITaskNamedValuePair item in v2Coll)
				{
					if (string.Compare(item.Name, name, false) == 0)
					{
						value = item.Value;
						return true;
					}
				}
				value = null;
				return false;
			}

			var nvp = unboundDict.Find(p => p.Name == name);
			value = nvp?.Value;
			return nvp != null;
		}

		/// <summary>
		/// Gets the collection enumerator for the name-value collection.
		/// </summary>
		/// <returns>An <see cref="System.Collections.IEnumerator"/> for the collection.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		bool ICollection<NameValuePair>.Contains(NameValuePair item)
		{
			if (v2Coll == null)
				return unboundDict.Contains(item);

			foreach (V2Interop.ITaskNamedValuePair invp in v2Coll)
				if (item.Equals(invp)) return true;
			return false;
		}

		void ICollection<NameValuePair>.CopyTo(NameValuePair[] array, int arrayIndex)
		{
			if (v2Coll == null)
				unboundDict.CopyTo(array, arrayIndex);
			else
			{
				if (array.Length - arrayIndex < v2Coll.Count)
					throw new ArgumentException("Items in collection exceed available items in destination array.");
				if (arrayIndex < 0)
					throw new ArgumentException("Array index must be 0 or greater.", nameof(arrayIndex));
				for (int i = 0; i < v2Coll.Count; i++)
					array[i + arrayIndex] = new NameValuePair(v2Coll[i]);
			}
		}

		bool ICollection<NameValuePair>.IsReadOnly => false;

		ICollection<string> IDictionary<string, string>.Keys => Names;

		bool ICollection<KeyValuePair<string, string>>.IsReadOnly => false;

		bool ICollection<NameValuePair>.Remove(NameValuePair item)
		{
			if (v2Coll == null)
				return unboundDict.Remove(item);

			for (int i = 0; i < v2Coll.Count; i++)
			{
				if (item.Equals(v2Coll[i]))
				{
					v2Coll.Remove(i);
					return true;
				}
			}
			return false;
		}

		bool IDictionary<string, string>.ContainsKey(string key) => Names.Contains(key);

		void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
		{
			Add(item.Key, item.Value);
		}

		bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item) =>
			((ICollection<NameValuePair>)this).Contains(new NameValuePair(item.Key, item.Value));

		void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
		{
			if (array.Length < Count + arrayIndex)
				throw new ArgumentOutOfRangeException(nameof(array), "Array has insufficient capacity to support copy.");
			foreach (var item in ((IEnumerable<KeyValuePair<string, string>>)this))
				array[arrayIndex++] = item;
		}

		bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item) =>
			((ICollection<NameValuePair>)this).Remove(new NameValuePair(item.Key, item.Value));

		IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
		{
			foreach (var nvp in this)
				yield return new KeyValuePair<string, string>(nvp.Name, nvp.Value);
		}
	}
}
