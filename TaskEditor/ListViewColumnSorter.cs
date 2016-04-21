using System.Collections.Generic;

namespace System.Windows.Forms
{
	internal class ListViewColumnSorter : IComparer<ListViewItem>, System.Collections.IComparer
	{
		private System.Collections.CaseInsensitiveComparer ObjectCompare = new System.Collections.CaseInsensitiveComparer(System.Globalization.CultureInfo.InvariantCulture);

		public ListViewColumnSorter() { }

		public int Compare(ListViewItem listviewX, ListViewItem listviewY)
		{
			// Compare the two items
			int compareResult = ObjectCompare.Compare(listviewX.SubItems[SortColumn].Tag ?? listviewX.SubItems[SortColumn].Text, listviewY.SubItems[SortColumn].Tag ?? listviewY.SubItems[SortColumn].Text);

			// Calculate correct return value based on object comparison
			if (Order == SortOrder.Ascending)
			{
				// Ascending sort is selected, return normal result of compare operation
				return compareResult;
			}
			else if (Order == SortOrder.Descending)
			{
				// Descending sort is selected, return negative result of compare operation
				return (-compareResult);
			}
			// Return '0' to indicate they are equal
			return 0;
		}

		public void ResortOnColumn(int column)
		{
			if (column == SortColumn)
			{
				// Reverse the current sort direction for this column.
				Order = Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
				NewSortSameColumn = true;
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				SortColumn = column;
				Order = SortOrder.Ascending;
				NewSortSameColumn = false;
			}
		}

		int System.Collections.IComparer.Compare(object x, object y)
		{
			if (x is ListViewItem && y is ListViewItem)
				return Compare((ListViewItem)x, (ListViewItem)y);
			return ObjectCompare.Compare(x, y);
		}

		public bool NewSortSameColumn { get; set; } = false;

		public SortOrder Order { get; set; } = SortOrder.None;

		/// <summary>
		/// Gets or sets column to sort on.
		/// </summary>
		/// <value>
		/// The sort column.
		/// </value>
		public int SortColumn { get; set; } = 0;

		public bool Group { get; set; } = false;

		public void Reset()
		{
			Group = false;
			SortColumn = 0;
			Order = SortOrder.Descending;
			NewSortSameColumn = false;
		}
	}
}
