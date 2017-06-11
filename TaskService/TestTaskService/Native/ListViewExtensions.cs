using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	public abstract class ListViewGroupingSetTemplate<T> where T : class
	{
		private T[] groupings;
		private Predicate<ListViewItem>[] rules;

		[Localizable(true)]
		public string Name { get; set; }

		public T[] Groupings
		{
			get { return groupings; }
			set
			{
				groupings = value;
			}
		}

		public Predicate<ListViewItem>[] GroupingRules
		{
			get { return rules; }
			set
			{
				rules = value;
			}
		}
	}

	public class ListViewGroupingSet : ListViewGroupingSetTemplate<ListViewGroup>
	{
	}

	public static class ListViewExtension
	{
		private static PropertyInfo GroupIdProperty;

		public static void ApplyGroupingSet(this ListView listView, ListViewGroupingSet set)
		{
			ApplyGroupingSet<ListViewGroup>(listView, set);
		}

		private static void ApplyGroupingSet<T>(this ListView listView, ListViewGroupingSetTemplate<T> set) where T : class
		{
			var vm = listView.VirtualMode;
			listView.BeginUpdate();
			if (vm)
				listView.VirtualMode = false;
			listView.Groups.Clear();
			/*if (listView is ListViewEx)
				(listView as ListViewEx).Groups.AddRange(set.Groupings as ListViewGroupEx[]);
			else*/
				listView.Groups.AddRange(set.Groupings as ListViewGroup[]);
			var other = new List<ListViewItem>();
			foreach (ListViewItem i in listView.Items)
			{
				var found = false;
				for (var r = 0; r < set.GroupingRules.Length; r++)
				{
					if (set.GroupingRules[r](i))
					{
						listView.Groups[r].Items.Add(i);
						found = true;
						break;
					}
				}
				if (!found)
					other.Add(i);
			}
			if (other.Count > 0)
			{
				var og = listView.Groups.Add("Other", "Other");
				for (var oi = 0; oi < other.Count; oi++)
					og.Items.Add(other[oi]);
			}
			if (vm)
				listView.VirtualMode = true;
			listView.ShowGroups = true;
			listView.EndUpdate();
		}

		public static bool GetCollapsed(this ListViewGroup group)
		{
			if (group == null)
				throw new ArgumentNullException();
			return GetState(group, NativeMethods.ListViewGroupState.Normal | NativeMethods.ListViewGroupState.Collapsed);
		}

		public static bool GetCollapsible(this ListViewGroup group)
		{
			if (group == null)
				throw new ArgumentNullException();
			return IsWinVista && GetState(group, NativeMethods.ListViewGroupState.Collapsible);
		}

		public static IntPtr GetHeaderHandle(this ListView listView)
		{
			if (listView.IsHandleCreated)
				return NativeMethods.SendMessage(listView.Handle, (uint)NativeMethods.ListViewMessage.GetHeader, IntPtr.Zero, IntPtr.Zero);
			return IntPtr.Zero;
		}

		public static void InvalidateHeader(this ListView listView)
		{
			NativeMethods.InvalidateRect(GetHeaderHandle(listView), IntPtr.Zero, true);
		}

		public static void SetCollapsed(this ListViewGroup group, bool value)
		{
			if (group == null)
				throw new ArgumentNullException();
			SetState(group, NativeMethods.ListViewGroupState.Normal | NativeMethods.ListViewGroupState.Collapsed, value);
		}

		public static void SetCollapsible(this ListViewGroup group, bool value)
		{
			if (group == null)
				throw new ArgumentNullException();
			if (!IsWinVista)
				throw new PlatformNotSupportedException();
			SetState(group, NativeMethods.ListViewGroupState.Collapsible, value);
		}

		public static void SetColumnDropDown(this ListView listView, int columnIndex, bool enable)
		{
			if (columnIndex < 0 || columnIndex >= 0 && listView.Columns == null || columnIndex >= listView.Columns.Count)
				throw new ArgumentOutOfRangeException(nameof(columnIndex));

			if (listView.IsHandleCreated)
			{
				var lvc = new NativeMethods.LVCOLUMN(NativeMethods.ListViewColumMask.Fmt);
				NativeMethods.SendMessage(listView.Handle, NativeMethods.ListViewMessage.GetColumn, columnIndex, lvc);
				if (enable)
					lvc.Format |= NativeMethods.ListViewColumnFormat.SplitButton;
				else
					lvc.Format &= ~NativeMethods.ListViewColumnFormat.SplitButton;
				NativeMethods.SendMessage(listView.Handle, NativeMethods.ListViewMessage.SetColumn, columnIndex, lvc);
				listView.InvalidateHeader();
			}
		}

		public static void SetOverlayImage(this ListViewItem lvi, int imageIndex)
		{
			if (imageIndex < 1 || imageIndex > 15)
				throw new ArgumentOutOfRangeException(nameof(imageIndex));
			if (lvi.ListView == null)
				throw new ArgumentNullException(nameof(lvi), "ListViewItem must be attached to a valid ListView.");
			var nItem = new NativeMethods.LVITEM(lvi.Index);
			nItem.OverlayImageIndex = (uint)imageIndex;
			if (NativeMethods.SendMessage(lvi.ListView.Handle, NativeMethods.ListViewMessage.SetItem, 0, nItem).ToInt32() == 0)
				throw new Win32Exception();
		}

		public static void SetSortIcon(this ListView listView, int columnIndex, SortOrder order)
		{
			var columnHeader = NativeMethods.SendMessage(listView.Handle, NativeMethods.ListViewMessage.GetHeader, 0, IntPtr.Zero);

			for (var columnNumber = 0; columnNumber <= listView.Columns.Count - 1; columnNumber++)
			{
				// Get current listview column info
				var lvcol = new NativeMethods.LVCOLUMN(NativeMethods.ListViewColumMask.Fmt);
				NativeMethods.SendMessage(listView.Handle, NativeMethods.ListViewMessage.GetColumn, columnNumber, lvcol);

				// Get current header info
				var hditem = new NativeMethods.HDITEM(NativeMethods.HeaderItemMask.Format | NativeMethods.HeaderItemMask.DISetItem);
				NativeMethods.SendMessage(columnHeader, NativeMethods.HeaderMessage.GetItem, columnNumber, hditem);

				// Update header with column info
				hditem.Format |= (NativeMethods.HeaderItemFormat)((uint)lvcol.Format & 0x1001803);
				if ((lvcol.Format & NativeMethods.ListViewColumnFormat.NoTitle) == 0)
					hditem.ShowText = true;

				// Set header image info
				if (order != SortOrder.None && columnNumber == columnIndex)
					hditem.ImageDisplay = order == SortOrder.Descending ? NativeMethods.HeaderItemImageDisplay.DownArrow : NativeMethods.HeaderItemImageDisplay.UpArrow;
				else
					hditem.ImageDisplay = NativeMethods.HeaderItemImageDisplay.None;

				// Update header
				NativeMethods.SendMessage(columnHeader, NativeMethods.HeaderMessage.SetItem, columnNumber, hditem);
			}
		}

		public static void SetFooter(this ListViewGroup group, string footer = null, HorizontalAlignment footerAlignment = HorizontalAlignment.Left)
		{
			var groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (var lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.Footer = footer;
					lvgroup.SetAlignment(group.HeaderAlignment, footerAlignment);
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.ListViewMessage.SetGroupInfo, groupId, lvgroup);
				}
			}
		}

		public static void SetTask(this ListViewGroup group, string task)
		{
			var groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (var lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.Task = task;
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.ListViewMessage.SetGroupInfo, groupId, lvgroup);
				}
			}
		}

		public static void SetGroupImageList(this ListViewGroup group, ImageList imageList)
		{
			if (!group.ListView.IsHandleCreated)
				throw new InvalidOperationException();
			var lparam = imageList?.Handle ?? IntPtr.Zero;
			NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetImageList, (IntPtr)3, lparam);
		}

		public static void SetImage(this ListViewGroup group, int titleImageIndex, string descriptionTop = null, string descriptionBottom = null)
		{
			var groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (var lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.TitleImageIndex = titleImageIndex;
					if (descriptionBottom != null)
						lvgroup.DescriptionBottom = descriptionBottom;
					if (descriptionTop != null)
						lvgroup.DescriptionTop = descriptionTop;
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.ListViewMessage.SetGroupInfo, groupId, lvgroup);
				}
			}
		}

		private static int GetGroupId(ListViewGroup group)
		{
			if (GroupIdProperty == null)
				GroupIdProperty = typeof(ListViewGroup).GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance);
			return (int?) GroupIdProperty?.GetValue(@group, null) ?? -1;
		}

		private static bool GetState(ListViewGroup group, NativeMethods.ListViewGroupState state)
		{
			var groupId = GetGroupId(group);
			if (groupId < 0)
				return false;
			return (NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.GetGroupState, (IntPtr)groupId, new IntPtr((int)state)).ToInt32() & (int)state) != 0;
		}

		private static bool IsWinVista { get; } = Environment.OSVersion.Version.Major >= 6;

		private static void SetState(ListViewGroup group, NativeMethods.ListViewGroupState state, bool value)
		{
			var groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				var lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.State);
				{
					lvgroup.SetState(state, value);
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.ListViewMessage.SetGroupInfo, groupId, lvgroup);
				}
			}
		}

#if UXTHEME
		public static void SetExplorerTheme(this ListView listView, bool on = true)
		{
			NativeMethods.SetWindowTheme(listView.Handle, "explorer", null);
			const uint LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
			const int LVS_EX_DOUBLEBUFFER = 0x00010000;
			NativeMethods.SendMessage(listView.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_DOUBLEBUFFER, LVS_EX_DOUBLEBUFFER);
		}
#endif
	}
}