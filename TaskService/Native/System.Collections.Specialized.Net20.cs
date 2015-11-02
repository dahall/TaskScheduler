#if !NET_40_OR_GREATER
using System;

namespace System.Collections.Specialized
{
	public interface INotifyCollectionChanged
	{
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}

	public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);

	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int newIndex, int oldIndex)
		{
			Action = action;
			NewItems = newItems;
			NewStartingIndex = newIndex;
			OldItems = oldItems;
			OldStartingIndex = oldIndex;
		}

		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action) :
			this(action, null, null, -1, -1) { }

		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object item, int idx = -1) :
			this(action, new object[] { item }, null, idx, -1) { }

		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object item, object item2, int idx) :
			this(action, new object[] { item }, new object[] { item2 }, idx, -1) { }

		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems) :
			this(action, newItems, null, -1, -1) { }

		public NotifyCollectionChangedAction Action { get; }
		public IList NewItems { get; }
		public int NewStartingIndex { get; }
		public IList OldItems { get; }
		public int OldStartingIndex { get; }
	}

	public enum NotifyCollectionChangedAction
	{
		Add = 0,
		Move = 3,
		Remove = 1,
		Replace = 2,
		Reset = 4
	}
}
#endif