using System;
using System.Reflection;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	public static class ListViewGroupExtension
	{
		private static PropertyInfo GroupIdProperty;

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
			return (IsWinVista() ? GetState(group, NativeMethods.ListViewGroupState.Collapsible) : false);
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
			if (!IsWinVista())
				throw new PlatformNotSupportedException();
			SetState(group, NativeMethods.ListViewGroupState.Collapsible, value);
		}

		public static void SetFooter(this ListViewGroup group, string footer = null, HorizontalAlignment footerAlignment = HorizontalAlignment.Left)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.Footer = footer;
					lvgroup.SetAlignment(group.HeaderAlignment, footerAlignment);
					NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetGroupInfo, (IntPtr)groupId, lvgroup);
				}
			}
		}

		public static void SetTask(this ListViewGroup group, string task)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.Task = task;
					NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetGroupInfo, (IntPtr)groupId, lvgroup);
				}
			}
		}

		public static void SetGroupImageList(this ListViewGroup group, ImageList imageList)
		{
			if (!group.ListView.IsHandleCreated)
				throw new InvalidOperationException();
			IntPtr lparam = (imageList == null) ? IntPtr.Zero : imageList.Handle;
			NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetImageList, (IntPtr)3, lparam);
		}

		public static void SetImage(this ListViewGroup group, int titleImageIndex, string descriptionTop = null, string descriptionBottom = null)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.None))
				{
					lvgroup.TitleImageIndex = titleImageIndex;
					if (descriptionBottom != null)
						lvgroup.DescriptionBottom = descriptionBottom;
					if (descriptionTop != null)
						lvgroup.DescriptionTop = descriptionTop;
					NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetGroupInfo, (IntPtr)groupId, lvgroup);
				}
			}
		}

		private static int GetGroupId(ListViewGroup group)
		{
			if (GroupIdProperty == null)
				GroupIdProperty = typeof(ListViewGroup).GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance);
			return ((GroupIdProperty != null) ? ((int) GroupIdProperty.GetValue(group, null)) : -1);
		}

		private static bool GetState(ListViewGroup group, NativeMethods.ListViewGroupState state)
		{
			int groupId = GetGroupId(group);
			if (groupId < 0)
				return false;
			return (NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.GetGroupState, (IntPtr)groupId, new IntPtr((int)state)).ToInt32() & (int)state) != 0;
		}

		private static bool IsWinVista()
		{
			return System.Environment.OSVersion.Version.Major >= 6;
		}

		private static void SetState(ListViewGroup group, NativeMethods.ListViewGroupState state, bool value)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.State);
				{
					lvgroup.SetState(state, value);
					NativeMethods.SendMessage(group.ListView.Handle, (uint)NativeMethods.ListViewMessage.SetGroupInfo, (IntPtr)groupId, lvgroup);
				}
			}
		}
	}
}