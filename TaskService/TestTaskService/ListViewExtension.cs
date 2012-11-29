using System;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.Win32.CommCtrl;
using System.Runtime.InteropServices;

namespace TestTaskService
{
	public static class ListViewGroupExtension
	{
		private static PropertyInfo GroupIdProperty;

		public static bool GetCollapsed(this ListViewGroup group)
		{
			if (group == null)
				throw new ArgumentNullException();
			return GetState(group, ListViewGroupState.Normal | ListViewGroupState.Collapsed);
		}

		public static bool GetCollapsible(this ListViewGroup group)
		{
			if (group == null)
				throw new ArgumentNullException();
			return (IsWinVista() ? GetState(group, ListViewGroupState.Collapsible) : false);
		}

		public static void SetCollapsed(this ListViewGroup group, bool value)
		{
			if (group == null)
				throw new ArgumentNullException();
			SetState(group, ListViewGroupState.Normal | ListViewGroupState.Collapsed, value);
		}

		public static void SetCollapsible(this ListViewGroup group, bool value)
		{
			if (group == null)
				throw new ArgumentNullException();
			if (!IsWinVista())
				throw new PlatformNotSupportedException();
			SetState(group, ListViewGroupState.Collapsible, value);
		}

		public static void SetFooter(this ListViewGroup group, string footer = null, HorizontalAlignment footerAlignment = HorizontalAlignment.Left)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(ListViewGroupMask.None))
				{
					lvgroup.Footer = footer;
					lvgroup.SetAlignment(group.HeaderAlignment, footerAlignment);
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_SETGROUPINFO, groupId, lvgroup);
				}
			}
		}

		public static void SetTask(this ListViewGroup group, string task)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(ListViewGroupMask.None))
				{
					lvgroup.Task = task;
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_SETGROUPINFO, groupId, lvgroup);
				}
			}
		}

		public static void SetGroupImageList(this ListViewGroup group, ImageList imageList)
		{
			if (!group.ListView.IsHandleCreated)
				throw new InvalidOperationException();
			IntPtr lparam = (imageList == null) ? IntPtr.Zero : imageList.Handle;
			NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_SETIMAGELIST, 3, lparam);
		}

		public static void SetImage(this ListViewGroup group, int titleImageIndex, string descriptionTop = null, string descriptionBottom = null)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				using (NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(ListViewGroupMask.None))
				{
					lvgroup.TitleImageIndex = titleImageIndex;
					if (descriptionBottom != null)
						lvgroup.DescriptionBottom = descriptionBottom;
					if (descriptionTop != null)
						lvgroup.DescriptionTop = descriptionTop;
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_SETGROUPINFO, groupId, lvgroup);
				}
			}
		}

		private static int GetGroupId(ListViewGroup group)
		{
			if (GroupIdProperty == null)
				GroupIdProperty = typeof(ListViewGroup).GetProperty("ID", BindingFlags.NonPublic | BindingFlags.Instance);
			return ((GroupIdProperty != null) ? ((int) GroupIdProperty.GetValue(group, null)) : -1);
		}

		private static bool GetState(ListViewGroup group, ListViewGroupState state)
		{
			int groupId = GetGroupId(group);
			if (groupId < 0)
				return false;
			return (NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_GETGROUPSTATE, groupId, new IntPtr((int)state)) & (int)state) != 0;
		}

		private static bool IsWinVista()
		{
			return System.Environment.OSVersion.Version.Major >= 6;
		}

		private static void SetState(ListViewGroup group, ListViewGroupState state, bool value)
		{
			int groupId = GetGroupId(group);
			if (groupId >= 0)
			{
				NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP(ListViewGroupMask.State);
				{
					lvgroup.SetState(state, value);
					NativeMethods.SendMessage(group.ListView.Handle, NativeMethods.LVM_SETGROUPINFO, groupId, lvgroup);
				}
			}
		}
	}
}