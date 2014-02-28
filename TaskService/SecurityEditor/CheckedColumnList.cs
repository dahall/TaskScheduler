using GroupControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SecurityEditor
{
	#region Enumerations

	/// <summary>
	/// State of check box
	/// </summary>
	public enum CheckState
	{
		NotAvailable = 0,
		CheckedDisabled = 1,
		UncheckedDisabled = 2,
		Checked = 4,
		Unchecked = 5,
	}

	#endregion Enumerations

	/// <summary>
	/// A list of permissions
	/// </summary>
	public class CheckedColumnList : ControlListBase
	{
		private ItemIndex focusItem = ItemIndex.Empty, hoverItem = ItemIndex.Empty, pressingItem = ItemIndex.Empty;
		private Padding itemPadding = new Padding(2, 0, 0, 0);
		private CheckedColumnListItemCollection items;
		private bool mouseTracking;
		private Brush textBrush;
		private StringFormat textFormat = new StringFormat(StringFormatFlags.NoWrap) { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter};

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckedColumnList"/> class.
		/// </summary>
		public CheckedColumnList() : base()
		{
			items = new CheckedColumnListItemCollection(this);
			this.BackColor = SystemColors.Window;
			this.Padding = new Padding(2, 2, 0, 5);
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ColumnCount = 2;
		}

		/// <summary>
		/// Occurs when [item changed].
		/// </summary>
		public event EventHandler<ItemChangedEventArgs> ItemChanged;

		/// <summary>
		/// Gets or sets the column count.
		/// </summary>
		/// <value>The column count.</value>
		[Category("Behavior"), DefaultValue(2), Description("Determines the number of check columns to display.")]
		public virtual int ColumnCount { get; set; }

		/// <summary>
		/// Gets the items.
		/// </summary>
		[Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual CheckedColumnListItemCollection Items
		{
			get { return items; }
		}

		protected override ControlListBase.PaintBackgroundMethod BackgroundRenderer
		{
			get { return this.RenderBackground; }
		}

		private void RenderBackground(Graphics g, Rectangle bounds, Control childControl)
		{
			using (var bb = new SolidBrush(this.BackColor))
				g.FillRectangle(bb, bounds);
		}

		/// <summary>
		/// Gets the base list of items.
		/// </summary>
		/// <value>
		/// Any list supportting and <see cref="T:System.Collections.IList"/> interface.
		/// </value>
		protected override System.Collections.IList BaseItems
		{
			get { return items; }
		}

		/// <summary>
		/// Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.
		/// </summary>
		/// <param name="index">The zero-based index of the item to scroll into view.</param>
		public override void EnsureVisible(int index)
		{
			Rectangle r = GetItemRect(index);
			Rectangle scrollRect = new Rectangle(-this.AutoScrollPosition.X, -this.AutoScrollPosition.Y, this.ClientRectangle.Size.Width, this.ClientRectangle.Size.Height);
			System.Diagnostics.Debug.WriteLine(string.Format("EnsVis: Cl: {0}; It: {1}", scrollRect, r));
			if (!scrollRect.Contains(r))
			{
				this.AutoScrollPosition = r.Location;
				Refresh();
			}
		}

		/// <summary>
		/// Gets the specified item's tooltip text.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <returns>
		/// Tooltip text to display. <c>null</c> or <see cref="F:System.String.Empty"/> to display no tooltip.
		/// </returns>
		protected override string GetItemToolTipText(int index)
		{
			return string.Empty;
		}

		/// <summary>
		/// Determines whether this list has the specified mnemonic in its members.
		/// </summary>
		/// <param name="charCode">The mnumonic character.</param>
		/// <returns>
		///   <c>true</c> if list has the mnemonic; otherwise, <c>false</c>.
		/// </returns>
		protected override bool ListHasMnemonic(char charCode)
		{
			return false;
		}

		/// <summary>
		/// Measures the specified item.
		/// </summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics"/> reference.</param>
		/// <param name="index">The index of the item.</param>
		/// <param name="maxSize">Maximum size of the item. Usually only constrains the width.</param>
		/// <returns>
		/// Minimum size for the item.
		/// </returns>
		protected override Size MeasureItem(System.Drawing.Graphics g, int index, System.Drawing.Size maxSize)
		{
			return new Size(maxSize.Width, GetCheckboxSize(g).Height + itemPadding.Vertical);
		}

		/// <summary>
		/// Raises the <see cref="E:ItemChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="SecurityEditor.CheckedColumnList.ItemChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnItemChanged(ItemChangedEventArgs e)
		{
			EventHandler<ItemChangedEventArgs> handler = ItemChanged;
			if (handler != null)
				handler(this, e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (focusItem == ItemIndex.Empty)
				FocusNextItem(focusItem, true);
			if (focusItem.Row != -1)
				InvalidateItem(focusItem.Row);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (focusItem.Row != -1)
				InvalidateItem(focusItem.Row);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			Point mPt = OffsetForScroll(e.Location);
			int i = GetItemAtLocation(mPt);
			if (i != -1)
			{
				bool found = false;
				for (int j = 0; j < ColumnCount; j++)
				{
					if (items[i].ValueBounds[j].Contains(mPt) && GetItemValue(i, j) >= CheckState.Checked)
					{
						pressingItem = new ItemIndex(i, j);
						focusItem = pressingItem;
						found = true;
						Invalidate();
						break;
					}
				}
				if (!found)
					FocusNextItem(new ItemIndex(i - 1, ColumnCount - 1), true);
			}
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			mouseTracking = true;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			mouseTracking = false;
			hoverItem = ItemIndex.Empty;
			Invalidate();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (mouseTracking)
			{
				ItemIndex newItem = ItemIndex.Empty;
				Point mPt = OffsetForScroll(e.Location);
				int i = GetItemAtLocation(mPt);
				if (i >= 0)
				{
					for (int j = 0; j < ColumnCount; j++)
					{
						if (items[i].ValueBounds != null && items[i].ValueBounds[j].Contains(mPt))
						{
							newItem = new ItemIndex(i, j);
							break;
						}
					}
				}
				if (newItem != hoverItem)
				{
					hoverItem = newItem;
					InvalidateItem(i);
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (pressingItem != ItemIndex.Empty)
			{
				ToggleItem(pressingItem);
				int r = pressingItem.Row;
				pressingItem = ItemIndex.Empty;
				InvalidateItem(r);
			}
			base.OnMouseUp(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
		/// </summary>
		/// <param name="pe">An <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs pe)
		{
			textBrush = null;
			textBrush = new SolidBrush(this.ForeColor); //new SolidBrush(this.Enabled ? this.ForeColor : SystemColors.GrayText);
			base.OnPaint(pe);
		}

		/// <summary>
		/// Paints the specified item.
		/// </summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics"/> reference.</param>
		/// <param name="index">The index of the item.</param>
		/// <param name="bounds">The bounds in which to paint the item.</param>
		protected override void PaintItem(System.Drawing.Graphics g, int index, System.Drawing.Rectangle bounds)
		{
			Size chkSize = GetCheckboxSize(g);
			int colWidth = chkSize.Width * 4;
			int textWidth = bounds.Width - (this.ColumnCount * colWidth) - itemPadding.Horizontal;
			if (items[index].ValueBounds == null || items[index].ValueBounds.Length != ColumnCount)
				items[index].ValueBounds = new Rectangle[ColumnCount];
			Point pt = new Point(bounds.X + textWidth + itemPadding.Left + (chkSize.Width * 3 / 2), bounds.Y + itemPadding.Top);
			// Draw check boxes
			for (int i = 0; i < this.ColumnCount; i++)
			{
				items[index].ValueBounds[i] = new Rectangle(pt, chkSize);
				CheckState state = GetItemValue(index, i);
				if (state != CheckState.NotAvailable)
				{
					System.Windows.Forms.VisualStyles.CheckBoxState cbs = ((int)state % 3 == 1) ? System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal : System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
					if ((int)state / 3 == 0)
						cbs += 3;
					//else if (idx == PressingItem)
					//	cbs += 2;
					else if (hoverItem.Equals(index, i))
						cbs++;
					if (this.Enabled)
					{
						CheckBoxRenderer.DrawCheckBox(g, pt, cbs);
						if (this.Focused && focusItem.Equals(index, i))
						{
							Rectangle fr = new Rectangle(pt, chkSize);
							fr.Inflate(2, 2);
							ControlPaint.DrawFocusRectangle(g, fr);
						}
					}
					else
					{
						if (cbs >= System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal)
						{
							var vsr = new System.Windows.Forms.VisualStyles.VisualStyleRenderer("MENU", 11, cbs == System.Windows.Forms.VisualStyles.CheckBoxState.CheckedDisabled ? 2 : 1);
							vsr.DrawBackground(g, Rectangle.Inflate(new Rectangle(pt, chkSize), 2, 2));
						}
					}
				}
				pt.X += colWidth;
			}
			// Draw text
			Rectangle r = bounds;
			r.Width = textWidth;
			g.DrawString(items[index].Text, this.Font, textBrush, r, textFormat);
			if (this.Focused && focusItem.Row == index && focusItem.Col == -1)
				ControlPaint.DrawFocusRectangle(g, r);
		}

		/// <summary>
		/// Processes a keyboard event.
		/// </summary>
		/// <param name="ke">The <see cref="KeyEventArgs"/> associated with the key press.</param>
		/// <returns><c>true</c> if the key was processed by the control; otherwise, <c>false</c>.</returns>
		protected override bool ProcessKey(KeyEventArgs ke)
		{
			bool ret = false;
			switch (ke.KeyCode)
			{
				case Keys.Down:
				case Keys.Right:
					if (FocusNextItem(focusItem, true))
					{
						EnsureVisible(focusItem.Row);
						ret = true;
					}
					break;
				case Keys.Enter:
					break;
				case Keys.Escape:
					break;
				case Keys.Up:
				case Keys.Left:
					if (FocusNextItem(focusItem, false))
					{
						EnsureVisible(focusItem.Row);
						ret = true;
					}
					break;
				case Keys.Space:
					ToggleItem(focusItem);
					break;
				case Keys.Tab:
					if (FocusNextItem(focusItem, !ke.Shift))
					{
						EnsureVisible(focusItem.Row);
						ret = true;
					}
					break;
				default:
					break;
			}
			if (ret) ke.SuppressKeyPress = true;
			return ret;
		}

		/// <summary>
		/// Focuses the next item.
		/// </summary>
		/// <param name="i">The current item.</param>
		/// <param name="forward">if set to <c>true</c>, moves to the next item, otherwise moves to the previous item.</param>
		/// <returns><c>true</c> on success, <c>false</c> otherwise.</returns>
		private bool FocusNextItem(ItemIndex i, bool forward)
		{
			if (this.BaseItems.Count > 0)
			{
				if (i.Row > items.Count || i.Col > ColumnCount)
					throw new IndexOutOfRangeException();
				ItemIndex idx = GetNextEnabledItemIndex(i, forward);
				if (idx != ItemIndex.Empty)
				{
					SetFocused(idx);
					return true;
				}
			}
			return false;
		}

		private Size GetCheckboxSize(Graphics g)
		{
			return CheckBoxRenderer.GetGlyphSize(g, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
		}

		private CheckState GetItemValue(int row, int col)
		{
			if (row >= 0 && col >= 0 && row < items.Count && col < items[row].Values.Length)
				return items[row].Values[col];
			return CheckState.NotAvailable;
		}

		/// <summary>
		/// Gets the index of the next enabled item.
		/// </summary>
		/// <param name="startIndex">The start index.</param>
		/// <param name="forward">if set to <c>true</c> find subsequent item, prior item if <c>false</c>.</param>
		/// <returns>Index of next enabled item, or -1 if not found.</returns>
		private ItemIndex GetNextEnabledItemIndex(ItemIndex startIndex, bool forward)
		{
			int startRow = forward ? startIndex.Row + 1 : startIndex.Row - 1;
			ItemIndex ret = GetNextItemIndex(startIndex, forward);
			while (ret != ItemIndex.Empty)
			{
				CheckState val = GetItemValue(ret.Row, ret.Col);
				if (val >= CheckState.Checked)
					return ret;
				ret = GetNextItemIndex(ret, forward);
				if (forward)
				{
					if (startRow < ret.Row || (ret.Row == -1 && startRow < items.Count))
						return new ItemIndex(startRow, -1);
				}
				else
				{
					if (startRow > ret.Row || ret.Row == -1)
						return new ItemIndex(startRow, -1);
				}
			}
			return ItemIndex.Empty;
		}

		private ItemIndex GetNextItemIndex(ItemIndex startIndex, bool forward)
		{
			if (ColumnCount == 0 || items.Count == 0)
				return ItemIndex.Empty;

			ItemIndex newIdx = startIndex;
			if (forward)
			{
				if (newIdx == ItemIndex.Empty)
					return new ItemIndex(0, 0);

				if (++newIdx.Col == ColumnCount)
				{
					newIdx.Col = 0;
					if (++newIdx.Row == items.Count)
						return ItemIndex.Empty;
				}
			}
			else
			{
				if (newIdx == ItemIndex.Empty)
					return new ItemIndex(items.Count - 1, ColumnCount - 1);

				if (--newIdx.Col < 0)
				{
					newIdx.Col = ColumnCount - 1;
					if (--newIdx.Row < 0)
						return ItemIndex.Empty;
				}
			}
			return newIdx;
		}

		private void SetFocused(ItemIndex idx)
		{
			if (focusItem != idx)
			{
				int old = focusItem.Row;
				focusItem = idx;
				Invalidate();
				//InvalidateItem(old);
				//InvalidateItem(focusItem.Row);
			}
		}

		/// <summary>
		/// Flips the indicated items check state.
		/// </summary>
		/// <param name="itemIndex">Index of the item to toggle.</param>
		private void ToggleItem(ItemIndex itemIndex)
		{
			var state = GetItemValue(itemIndex.Row, itemIndex.Col);
			if (state >= CheckState.Checked)
			{
				items[itemIndex.Row].Values[itemIndex.Col] = state == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
				InvalidateItem(itemIndex.Row);
				OnItemChanged(new ItemChangedEventArgs(items[itemIndex.Row], itemIndex.Col));
			}
		}

		private struct ItemIndex : IComparable<ItemIndex>
		{
			public static readonly ItemIndex Empty = new ItemIndex(-1, -1);

			public int Col, Row;

			public ItemIndex(int row, int col)
			{
				Row = row;
				Col = col;
			}

			public static bool operator !=(ItemIndex a, ItemIndex b)
			{
				return a.CompareTo(b) != 0;
			}

			public static bool operator ==(ItemIndex a, ItemIndex b)
			{
				return a.CompareTo(b) == 0;
			}

			public int CompareTo(ItemIndex b)
			{
				int res = Row.CompareTo(b.Row);
				if (res == 0)
					return Col.CompareTo(b.Col);
				return res;
			}

			public bool Equals(int row, int col)
			{
				return Row == row && Col == col;
			}

			public override bool Equals(object obj)
			{
				if (obj is ItemIndex)
					return this.Equals(((ItemIndex)obj).Row, ((ItemIndex)obj).Col);
				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return string.Format("({0},{1})", Row, Col);
			}
		}

		/// <summary>
		/// <see cref="CheckedColumnList"/> item change event args.
		/// </summary>
		public class ItemChangedEventArgs : EventArgs
		{
			/// <summary>
			/// Represents an empty item
			/// </summary>
			public static new readonly ItemChangedEventArgs Empty = new ItemChangedEventArgs();

			internal ItemChangedEventArgs(CheckedColumnListItem item = null, int col = -1)
			{
				Item = item;
				ChangedColumn = col;
			}

			/// <summary>
			/// Gets the changed column.
			/// </summary>
			public int ChangedColumn { get; private set; }

			/// <summary>
			/// Gets the item.
			/// </summary>
			public CheckedColumnListItem Item { get; private set; }
		}
	}

	/// <summary>
	/// An item for a <see cref="CheckedColumnList"/>.
	/// </summary>
	public class CheckedColumnListItem
	{
		internal Rectangle[] ValueBounds;

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckedColumnListItem"/> class.
		/// </summary>
		public CheckedColumnListItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CheckedColumnListItem"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="values">The values.</param>
		public CheckedColumnListItem(string text, params CheckState[] values)
		{
			this.Text = text;
			this.Values = values;
		}

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>
		/// The tag.
		/// </value>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Bindable(false)]
		public object Tag { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		[Category("Appearance"), DefaultValue((string)null)]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the values.
		/// </summary>
		/// <value>
		/// The values.
		/// </value>
		[Category("Appearance"), DefaultValue(null)]
		public CheckState[] Values { get; set; }
	}

	/// <summary>
	/// A collection of <see cref="CheckedColumnListItem"/> for the <see cref="CheckedColumnList.Items"/> property.
	/// </summary>
	public class CheckedColumnListItemCollection : EventedList<CheckedColumnListItem>
	{
		internal CheckedColumnListItemCollection(CheckedColumnList parent)
		{
			Parent = parent;
		}

		private CheckedColumnList Parent
		{
			get; set;
		}

		/// <summary>
		/// Called when [item added].
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		protected override void OnItemAdded(int index, CheckedColumnListItem value)
		{
			base.OnItemAdded(index, value);
		}

		/// <summary>
		/// Called when [item changed].
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		protected override void OnItemChanged(int index, CheckedColumnListItem oldValue, CheckedColumnListItem newValue)
		{
			base.OnItemChanged(index, oldValue, newValue);
		}

		/// <summary>
		/// Called when [item deleted].
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="value">The value.</param>
		protected override void OnItemDeleted(int index, CheckedColumnListItem value)
		{
			base.OnItemDeleted(index, value);
		}

		/// <summary>
		/// Called when [reset].
		/// </summary>
		protected override void OnReset()
		{
			base.OnReset();
		}
	}
}