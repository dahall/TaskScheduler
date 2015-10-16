using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Collection that contains the actions that are performed by the task.
	/// </summary>
	/// <remarks>A Task Scheduler 1.0 task can only contain a single <see cref="ExecAction"/>.</remarks>
	[XmlRoot("Actions", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class ActionCollection : IList<Action>, IDisposable, IXmlSerializable, IList
	{
		private V1Interop.ITask v1Task;
		private V2Interop.IActionCollection v2Coll;
		private V2Interop.ITaskDefinition v2Def;

		internal ActionCollection(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ActionCollection(V2Interop.ITaskDefinition iTaskDef)
		{
			v2Def = iTaskDef;
			v2Coll = iTaskDef.Actions;
			UnconvertUnsupportedActions();
		}

		/// <summary>
		/// Gets or sets the identifier of the principal for the task.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[System.Xml.Serialization.XmlAttribute(AttributeName = "Context", DataType = "IDREF")]
		public string Context
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Context;
				return string.Empty;
			}
			set
			{
				if (v2Coll != null)
					v2Coll.Context = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets the number of actions in the collection.
		/// </summary>
		public int Count
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Count;
				return ((string)v1Task.GetApplicationName()).Length == 0 ? 0 : 1;
			}
		}

		bool ICollection.IsSynchronized => false;

		object ICollection.SyncRoot => this;

		bool ICollection<Action>.IsReadOnly => false;

		bool IList.IsFixedSize => false;

		bool IList.IsReadOnly => false;

		/// <summary>
		/// Gets or sets an XML-formatted version of the collection.
		/// </summary>
		public string XmlText
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.XmlText;
				return XmlSerializationHelper.WriteObjectToXmlText(this);
			}
			set
			{
				if (v2Coll != null)
					v2Coll.XmlText = value;
				else
					XmlSerializationHelper.ReadObjectFromXmlText(value, this);
			}
		}

		object IList.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (Action)value; }
		}

		/// <summary>
		/// Gets or sets a an action at the specified index.
		/// </summary>
		/// <value>The zero-based index of the action to get or set.</value>
		public Action this[int index]
		{
			get
			{
				if (v2Coll != null)
					return Action.CreateAction(v2Coll[++index]);
				if (index == 0)
					return new ExecAction(v1Task);
				throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (Count <= index)
					throw new ArgumentOutOfRangeException(nameof(index), index, "Index is not a valid index in the ActionCollection");
				Insert(index, value);
				RemoveAt(index + 1);
			}
		}

		/// <summary>
		/// Gets or sets a specified action from the collection.
		/// </summary>
		/// <value>
		/// The <see cref="Action"/>.
		/// </value>
		/// <param name="actionId">The id (<see cref="Action.Id" />) of the action to be retrieved.</param>
		/// <returns>
		/// Specialized <see cref="Action" /> instance.
		/// </returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		/// <exception cref="System.NullReferenceException"></exception>
		/// <exception cref="System.InvalidOperationException">Mismatching Id for action and lookup.</exception>
		public Action this[string actionId]
		{
			get
			{
				if (string.IsNullOrEmpty(actionId))
					throw new ArgumentNullException(nameof(actionId));
				var t = Find(a => string.Equals(a.Id, actionId));
				if (t != null)
					return t;
				throw new ArgumentOutOfRangeException(nameof(actionId));
			}
			set
			{
				if (value == null)
					throw new NullReferenceException();
				if (string.IsNullOrEmpty(actionId))
					throw new ArgumentNullException(nameof(actionId));
				if (actionId != value.Id)
					throw new InvalidOperationException("Mismatching Id for action and lookup.");
				int index = IndexOf(actionId);
				if (index >= 0)
					this[index] = value;
				else
					Add(value);
			}
		}

		/// <summary>
		/// Adds an action to the task.
		/// </summary>
		/// <param name="action">A derived <see cref="Action"/> class.</param>
		/// <returns>The bound <see cref="Action"/> that was added to the collection.</returns>
		public Action Add(Action action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));
			if (v2Def != null)
				action.Bind(v2Def);
			else
				action.Bind(v1Task);
			return action;
		}

		/// <summary>
		/// Adds an <see cref="ExecAction"/> to the task.
		/// </summary>
		/// <param name="path">Path to an executable file.</param>
		/// <param name="arguments">Arguments associated with the command-line operation. This value can be null.</param>
		/// <param name="workingDirectory">Directory that contains either the executable file or the files that are used by the executable file. This value can be null.</param>
		/// <returns>The bound <see cref="ExecAction"/> that was added to the collection.</returns>
		public ExecAction Add(string path, string arguments = null, string workingDirectory = null) =>
			(ExecAction)Add(new ExecAction(path, arguments, workingDirectory));

		/// <summary>
		/// Adds a new <see cref="Action"/> instance to the task.
		/// </summary>
		/// <param name="actionType">Type of task to be created</param>
		/// <returns>Specialized <see cref="Action"/> instance.</returns>
		public Action AddNew(TaskActionType actionType)
		{
			if (v1Task != null)
				return new ExecAction(v1Task);

			return Action.CreateAction(v2Coll.Create(actionType));
		}

		/// <summary>
		/// Adds a collection of actions to the end of the <see cref="ActionCollection" />.
		/// </summary>
		/// <param name="actions">The actions to be added to the end of the <see cref="ActionCollection" />. The collection itself cannot be <c>null</c> and cannot contain <c>null</c> elements.</param>
		/// <exception cref="ArgumentNullException"><paramref name="actions" /> is <c>null</c>.</exception>
		public void AddRange(IEnumerable<Action> actions)
		{
			if (actions == null)
				throw new ArgumentNullException(nameof(actions));
			if (v1Task != null && Count > 0)
				throw new System.InvalidOperationException("Cannot add more than one action to a V1 task.");
			int i = 0;
			foreach (var item in actions)
			{
				if (v1Task != null && ++i > 1)
					throw new System.InvalidOperationException("Cannot add more than one action to a V1 task.");
				Add(item);
			}
		}

		/// <summary>
		/// Clears all actions from the task.
		/// </summary>
		public void Clear()
		{
			if (v2Coll != null)
				v2Coll.Clear();
			else
				Add(new ExecAction());
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
		/// </returns>
		public bool Contains(Action item) => Find(a => a.Equals(item)) != null;

		/// <summary>
		/// Determines whether the specified action type is contained in this collection.
		/// </summary>
		/// <param name="actionType">Type of the action.</param>
		/// <returns>
		///   <c>true</c> if the specified action type is contained in this collection; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsType(Type actionType) => Find(a => a.GetType() == actionType) != null;

		/// <summary>
		/// Copies the elements of the <see cref="ActionCollection" /> to an <see cref="T:Action[]" />, starting at a particular <see cref="T:Action[]" /> index.
		/// </summary>
		/// <param name="array">The <see cref="T:Action[]" /> that is the destination of the elements copied from <see cref="ActionCollection" />. The <see cref="T:Action[]" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <see cref="T:Action[]" /> at which copying begins.</param>
		public void CopyTo(Action[] array, int arrayIndex)
		{
			CopyTo(0, array, arrayIndex, Count);
		}

		/// <summary>
		/// Copies the elements of the <see cref="ActionCollection" /> to an <see cref="T:Action[]" />, starting at a particular <see cref="T:Action[]" /> index.
		/// </summary>
		/// <param name="index">The zero-based index in the source at which copying begins.</param>
		/// <param name="array">The <see cref="T:Action[]" /> that is the destination of the elements copied from <see cref="ActionCollection" />. The <see cref="T:Action[]" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <see cref="T:Action[]" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="System.ArgumentException">The number of elements in the source <see cref="ActionCollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		public void CopyTo(int index, Action[] array, int arrayIndex, int count)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));
			if (index < 0 || index >= Count)
				throw new ArgumentOutOfRangeException(nameof(index));
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			if (count < 0 || count > (Count - index))
				throw new ArgumentOutOfRangeException(nameof(count));
			if ((Count - index) > (array.Length - arrayIndex))
				throw new ArgumentOutOfRangeException(nameof(arrayIndex));
			for (int i = 0; i < count; i++)
				array[arrayIndex + i] = (Action)this[index + i].Clone();
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			v1Task = null;
			v2Def = null;
			if (v2Coll != null) Marshal.ReleaseComObject(v2Coll);
		}

		/// <summary>
		/// Searches for an <see cref="Action"/> that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire collection.
		/// </summary>
		/// <param name="match">The <see cref="Predicate{Action}"/> delegate that defines the conditions of the <see cref="Action"/> to search for.</param>
		/// <returns>The first <see cref="Action"/> that matches the conditions defined by the specified predicate, if found; otherwise, <c>null</c>.</returns>
		public Action Find(Predicate<Action> match)
		{
			if (match == null)
				throw new ArgumentNullException(nameof(match));
			foreach (var item in this)
				if (match(item)) return item;
			return null;
		}

		/// <summary>
		/// Searches for an <see cref="Action"/> that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the collection that starts at the specified index and contains the specified number of elements.
		/// </summary>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the collection to search.</param>
		/// <param name="match">The <see cref="Predicate{Action}"/> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
		public int FindIndexOf(int startIndex, int count, Predicate<Action> match)
		{
			if (startIndex < 0 || startIndex >= Count)
				throw new ArgumentOutOfRangeException(nameof(startIndex));
			if (startIndex + count > Count)
				throw new ArgumentOutOfRangeException(nameof(count));
			if (match == null)
				throw new ArgumentNullException(nameof(match));
			for (int i = startIndex; i < startIndex + count; i++)
				if (match(this[i])) return i;
			return -1;
		}

		/// <summary>
		/// Searches for an <see cref="Action"/> that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the collection.
		/// </summary>
		/// <param name="match">The <see cref="Predicate{Action}"/> delegate that defines the conditions of the element to search for.</param>
		/// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
		public int FindIndexOf(Predicate<Action> match) => FindIndexOf(0, Count, match);

		/// <summary>
		/// Retrieves an enumeration of each of the actions.
		/// </summary>
		/// <returns>Returns an object that implements the <see cref="IEnumerator"/> interface and that can iterate through the <see cref="Action"/> objects within the <see cref="ActionCollection"/>.</returns>
		public IEnumerator<Action> GetEnumerator()
		{
			if (v2Coll != null)
				return new ComEnumerator<Action, V2Interop.IActionCollection>(v2Coll, o => Action.CreateAction(o as V2Interop.IAction));
			return new V1ActionEnumerator(v1Task);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
				throw new RankException("Multi-dimensional arrays are not supported.");
			Action[] src = new Action[Count];
			CopyTo(src, 0);
			Array.Copy(src, 0, array, index, Count);
		}

		void ICollection<Action>.Add(Action item)
		{
			Add(item);
		}

		int IList.Add(object value)
		{
			Add((Action)value);
			return Count - 1;
		}

		bool IList.Contains(object value) => Contains((Action)value);

		int IList.IndexOf(object value) => IndexOf((Action)value);

		void IList.Insert(int index, object value)
		{
			Insert(index, (Action)value);
		}

		void IList.Remove(object value)
		{
			Remove((Action)value);
		}

		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
		/// <returns>
		/// The index of <paramref name="item" /> if found in the list; otherwise, -1.
		/// </returns>
		public int IndexOf(Action item) => FindIndexOf(a => a.Equals(item));

		/// <summary>
		/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
		/// </summary>
		/// <param name="actionId">The id (<see cref="Action.Id"/>) of the action to be retrieved.</param>
		/// <returns>
		/// The index of <paramref name="actionId" /> if found in the list; otherwise, -1.
		/// </returns>
		public int IndexOf(string actionId)
		{
			if (string.IsNullOrEmpty(actionId))
				throw new ArgumentNullException(nameof(actionId));
			return FindIndexOf(a => string.Equals(a.Id, actionId));
		}

		/// <summary>
		/// Inserts an action at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which action should be inserted.</param>
		/// <param name="action">The action to insert into the list.</param>
		public void Insert(int index, Action action)
		{
			if (v2Coll == null && Count > 0)
				throw new NotV1SupportedException("Only a single action is allowed.");
			if (action == null)
				throw new ArgumentNullException(nameof(action));
			if (index >= Count)
				throw new ArgumentOutOfRangeException(nameof(index));

			Action[] pushItems = new Action[Count - index];
			CopyTo(index, pushItems, 0, Count - index);
			for (int j = Count - 1; j >= index; j--)
				RemoveAt(j);
			Add(action);
			for (int k = 0; k < pushItems.Length; k++)
				Add(pushItems[k]);
		}

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("Actions", TaskDefinition.tns);
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				Action newAction = null;
				switch (reader.LocalName)
				{
					case "Exec":
						newAction = AddNew(TaskActionType.Execute);
						break;

					default:
						reader.Skip();
						break;
				}
				if (newAction != null)
					XmlSerializationHelper.ReadObject(reader, newAction);
			}
			reader.ReadEndElement();
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			if (Count > 0)
			{
				XmlSerializationHelper.WriteObject(writer, this[0] as ExecAction);
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
		/// <returns>
		/// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
		/// </returns>
		public bool Remove(Action item)
		{
			int idx = IndexOf(item);
			if (idx != -1)
			{
				try
				{
					RemoveAt(idx);
					return true;
				}
				catch { }
			}
			return false;
		}

		/// <summary>
		/// Removes the action at a specified index.
		/// </summary>
		/// <param name="index">Index of action to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
		public void RemoveAt(int index)
		{
			if (index >= Count)
				throw new ArgumentOutOfRangeException(nameof(index), index, "Failed to remove action. Index out of range.");
			if (v2Coll != null)
				v2Coll.Remove(++index);
			else if (index == 0)
				Add(new ExecAction());
			else
				throw new NotV1SupportedException("There can be only a single action and it cannot be removed.");
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Copies the elements of the <see cref="ActionCollection"/> to a new array.
		/// </summary>
		/// <returns>An array containing copies of the elements of the <see cref="ActionCollection"/>.</returns>
		public Action[] ToArray()
		{
			var ret = new Action[Count];
			CopyTo(ret, 0);
			return ret;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the actions in this collection.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the actions in this collection.
		/// </returns>
		public override string ToString()
		{
			if (Count == 1)
				return this[0].ToString();
			if (Count > 1)
				return Properties.Resources.MultipleActions;
			return string.Empty;
		}

		internal void ConvertUnsupportedActions()
		{
			for (int i = 0; i < Count; i++)
			{
				Action action = this[i];
				var bindable = action as IBindAsExecAction;
				if (TaskService.LibraryVersion.Minor > 3 && bindable != null)
				{
					string cmd = bindable.GetPowerShellCommand();
					this[i] = ExecAction.AsPowerShellCmd(action.ActionType.ToString(), cmd);
				}
				/*else if (action is IExtendExecAction)
				{
					this[i] = ((IExtendExecAction)action).ToExecAction();
				}*/
			}
		}

		internal void UnconvertUnsupportedActions()
		{
			for (int i = 0; i < Count; i++)
			{
				ExecAction action = this[i] as ExecAction;
				if (action != null)
				{
					if (TaskService.LibraryVersion.Minor > 3)
					{
						var match = action.GetPowerShellCmd();
						if (match != null)
						{
							Action newAction = null;
							if (match[0] == "SendEmail")
								newAction = EmailAction.FromPowerShellCommand(match[1]);
							else if (match[0] == "ShowMessage")
								newAction = ShowMessageAction.FromPowerShellCommand(match[1]);
							if (newAction != null)
							{
								this[i] = newAction;
								continue;
							}
						}
					}
				}
			}
		}

		internal class V1ActionEnumerator : IEnumerator<Action>, IDisposable
		{
			private int v1Pos = -1;
			private V1Interop.ITask v1Task;

			internal V1ActionEnumerator(V1Interop.ITask task) : base()
			{
				v1Task = task;
			}

			public Action Current
			{
				get
				{
					if (v1Pos == 0)
						return new ExecAction(v1Task.GetApplicationName(), v1Task.GetParameters(), v1Task.GetWorkingDirectory());
					throw new InvalidOperationException();
				}
			}

			object IEnumerator.Current => Current;

			/// <summary>
			/// Releases all resources used by this class.
			/// </summary>
			public void Dispose()
			{
				v1Task = null;
			}

			public bool MoveNext() => ++v1Pos == 0;

			public void Reset()
			{
				v1Pos = -1;
			}
		}
	}
}