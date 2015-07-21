using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	internal class ListViewReorderedEventArgs : EventArgs
	{
		public ListViewReorderedEventArgs(int oldIndex, int newIndex)
		{
			OldIndex = oldIndex;
			NewIndex = newIndex;
		}

		public int OldIndex { get; set; }
		public int NewIndex { get; set; }
	}

	internal class ReorderableListView : ListView
	{
		private class ListViewIndexComparer : System.Collections.IComparer
		{
			public int Compare(object x, object y) => ((ListViewItem)x).Index - ((ListViewItem)y).Index;
		}

		public event EventHandler<ListViewReorderedEventArgs> Reordered;
	
		private bool allowRowReorder;

		public ReorderableListView() : base()
		{
			AllowRowReorder = true;
			MultiSelect = false;
			ListViewItemSorter = new ListViewIndexComparer();
		}

		[DefaultValue(true)]
		public override bool AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}

		[DefaultValue(true), Category("Behavior")]
		public bool AllowRowReorder
		{
			get { return allowRowReorder; }
			set
			{
				allowRowReorder = value;
				base.AllowDrop = value;
			}
		}

		[Browsable(false), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool MultiSelect
		{
			get { return base.MultiSelect; }
			set { base.MultiSelect = value; }
		}

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new SortOrder Sorting
		{
			get { return base.Sorting; }
			set { base.Sorting = value; }
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (AllowRowReorder && base.SelectedItems.Count > 0)
			{
				// Retrieve the index of the insertion mark; 
				int targetIndex = base.InsertionMark.Index;

				// If the insertion mark is not visible, exit the method. 
				if (targetIndex == -1) return;

				// If the insertion mark is to the right of the item with 
				// the corresponding index, increment the target index. 
				if (base.InsertionMark.AppearsAfterItem) targetIndex++;

				// Retrieve the dragged item.
				ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
				int oldIndex = draggedItem.Index;

				// Insert a copy of the dragged item at the target index. 
				// A copy must be inserted before the original item is removed 
				// to preserve item index values. 
				base.Items.Insert(targetIndex, (ListViewItem)draggedItem.Clone());

				// Remove the original copy of the dragged item.
				base.Items.Remove(draggedItem);

				/*Point cp = base.PointToClient(new Point(e.X, e.Y));
				ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
				if (dragToItem != null)
				{
					int dropIndex = dragToItem.Index;
					if (dropIndex > base.SelectedItems[0].Index)
						dropIndex++;
					var insertItems = new List<ListViewItem>(base.SelectedItems.Count);
					foreach (ListViewItem item in base.SelectedItems)
						insertItems.Add((ListViewItem)item.Clone());
					for (int i = insertItems.Count - 1; i >= 0; i--)
						base.Items.Insert(dropIndex, insertItems[i]);
					foreach (ListViewItem removeItem in base.SelectedItems)
						base.Items.Remove(removeItem);
				}*/

				if (oldIndex < targetIndex)
					targetIndex--;
				OnReordered(new ListViewReorderedEventArgs(oldIndex, targetIndex));
			}
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			if (AllowRowReorder && e.Data.GetDataPresent(typeof(ListViewItem)))
				e.Effect = e.AllowedEffect; // DragDropEffects.Move;
		}

		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			base.InsertionMark.Index = -1;
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			if (AllowRowReorder && e.Data.GetDataPresent(typeof(ListViewItem)))
			{
				Point cp = base.PointToClient(new Point(e.X, e.Y));
				int targetIndex = base.InsertionMark.NearestIndex(cp);
				if (targetIndex > -1)
				{
					// Determine whether the mouse pointer is to the left or 
					// the right of the midpoint of the closest item and set 
					// the InsertionMark.AppearsAfterItem property accordingly.
					Rectangle itemBounds = base.GetItemRect(targetIndex);
					base.InsertionMark.AppearsAfterItem = (cp.Y > itemBounds.Top + (itemBounds.Height / 2));
				}
				base.InsertionMark.Index = targetIndex;
				e.Effect = targetIndex > -1 ? DragDropEffects.Move : DragDropEffects.None;
				/*ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
				if (hoverItem != null)
				{
					foreach (ListViewItem moveItem in base.SelectedItems)
					{
						if (moveItem.Index == hoverItem.Index)
						{
							hoverItem.EnsureVisible();
							return;
						}
					}
					base.OnDragOver(e);
					e.Effect = DragDropEffects.Move;
					hoverItem.EnsureVisible();
				}*/
			}
		}

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			base.OnItemDrag(e);
			if (AllowRowReorder)
				base.DoDragDrop(e.Item, DragDropEffects.Move);
		}

		protected virtual void OnReordered(ListViewReorderedEventArgs e)
		{
			var h = Reordered;
			if (h != null)
				h(this, e);
		}
	}
}