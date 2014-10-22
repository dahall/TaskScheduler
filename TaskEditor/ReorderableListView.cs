using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	internal class ReorderableListView : ListView
	{
		private bool allowRowReorder;

		public ReorderableListView() : base()
		{
			this.AllowRowReorder = true;
		}

		public bool AllowRowReorder
		{
			get { return this.allowRowReorder; }
			set
			{
				this.allowRowReorder = value;
				base.AllowDrop = value;
			}
		}

		public new SortOrder Sorting
		{
			get { return SortOrder.None; }
			set { base.Sorting = SortOrder.None; }
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (this.AllowRowReorder && base.SelectedItems.Count > 0)
			{
				Point cp = base.PointToClient(new Point(e.X, e.Y));
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
				}
			}
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			if (this.AllowRowReorder && e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
				e.Effect = DragDropEffects.Move;
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			if (this.AllowRowReorder && e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
			{
				Point cp = base.PointToClient(new Point(e.X, e.Y));
				ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
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
				}
			}
		}

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			base.OnItemDrag(e);
			if (this.AllowRowReorder)
				base.DoDragDrop(this.SelectedItems, DragDropEffects.Move);
		}
	}
}