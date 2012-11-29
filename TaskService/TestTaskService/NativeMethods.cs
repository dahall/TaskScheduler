using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Win32.CommCtrl
{
	[Flags]
	internal enum ListViewGroupMask : uint
	{
		None = 0x00000000,
		Header = 0x00000001,
		Footer = 0x00000002,
		State = 0x00000004,
		Align = 0x00000008,
		GroupId = 0x00000010,
		Subtitle = 0x00000100,
		Task = 0x00000200,
		DescriptionTop = 0x00000400,
		DescriptionBottom = 0x00000800,
		TitleImage = 0x00001000,
		ExtendedImage = 0x00002000,
		Items = 0x00004000,
		Subset = 0x00008000,
		SubsetItems = 0x00010000,
	}

	[Flags]
	internal enum ListViewGroupState : uint
	{
		Normal = 0x00000000,
		Collapsed = 0x00000001,
		Hidden = 0x00000002,
		NoHeader = 0x00000004,
		Collapsible = 0x00000008,
		Focused = 0x00000010,
		Selected = 0x00000020,
		Subseted = 0x00000040,
		SubsetLinkFocused = 0x00000080,
	}

	internal static class NativeMethods
	{
		internal const int cbBuffer = 256;
		internal const int LVM_FIRST = 0x1000;
		internal const int LVM_GETGROUPCOUNT = LVM_FIRST + 152;
		internal const int LVM_GETGROUPINFO = LVM_FIRST + 149;
		internal const int LVM_GETGROUPINFOBYINDEX = LVM_FIRST + 153;
		internal const int LVM_GETGROUPSTATE = LVM_FIRST + 92;
		internal const int LVM_INSERTGROUP = LVM_FIRST + 145;
		internal const int LVM_MOVEGROUP = LVM_FIRST + 151;
		internal const int LVM_MOVEITEMTOGROUP = LVM_FIRST + 154;
		internal const int LVM_REMOVEGROUP = LVM_FIRST + 150;
		internal const int LVM_SETGROUPINFO = LVM_FIRST + 147;
		internal const int LVM_SETIMAGELIST = LVM_FIRST + 3;
		internal const int LVM_UPDATE = LVM_FIRST + 42;

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int SendMessage(IntPtr window, int message, int wParam, [In, Out] LVGROUP group);

		[DllImport("user32.dll", SetLastError = true)]
		internal static extern int SendMessage(IntPtr window, int message, int wParam, IntPtr lParam);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class LVGROUP : IDisposable
		{
			public int cbSize;
			public ListViewGroupMask mask;
			public IntPtr pszHeader;
			public uint cchHeader;
			public IntPtr pszFooter;
			public uint cchFooter;
			public int iGroupId;
			public ListViewGroupState stateMask;
			public ListViewGroupState state;
			public uint uAlign;
			public IntPtr pszSubtitle;
			public uint cchSubtitle;
			public IntPtr pszTask;
			public uint cchTask;
			public IntPtr pszDescriptionTop;
			public uint cchDescriptionTop;
			public IntPtr pszDescriptionBottom;
			public uint cchDescriptionBottom;
			public int iTitleImage;
			public int iExtendedImage;
			public int iFirstItem;
			public uint cItems;
			public IntPtr pszSubsetTitle;
			public uint cchSubsetTitle;

			public LVGROUP(ListViewGroup grp)
				: this(ListViewGroupMask.Header | ListViewGroupMask.GroupId | ListViewGroupMask.Align, grp.Header)
			{
				this.SetAlignment(grp.HeaderAlignment, HorizontalAlignment.Left);
			}

			/*public LVGROUP(ListViewGroupEx grp)
				: this(grp.baseGroup)
			{
				this.Footer = grp.Footer;
				this.DescriptionBottom = grp.DescriptionBottom;
				this.DescriptionTop = grp.DescriptionTop;
				this.FooterAlignment = grp.FooterAlignment;
				this.Subtitle = grp.Subtitle;
				this.Task = grp.Task;
				if (grp.TitleImageIndex > 0)
				{
					this.iTitleImage = grp.TitleImageIndex;
					this.mask |= ListViewGroupMask.TitleImage;
				}
				ListViewGroupState s, m;
				grp.GetSetState(out m, out s);
				if (s != ListViewGroupState.Normal)
				{
					this.stateMask = m;
					this.state = s;
					this.mask |= ListViewGroupMask.State;
				}
			}*/

			public LVGROUP(ListViewGroupMask mask = ListViewGroupMask.None, string header = null)
			{
				cbSize = Marshal.SizeOf(this);
				this.mask = mask;

				if (header != null)
					this.Header = header;
				else if ((mask & ListViewGroupMask.Header) != 0)
					AllocString(ref pszHeader, ref cchHeader);

				if ((mask & ListViewGroupMask.Footer) != 0)
					AllocString(ref pszFooter, ref cchFooter);

				if ((mask & ListViewGroupMask.Subtitle) != 0)
					AllocString(ref pszSubtitle, ref cchSubtitle);

				if ((mask & ListViewGroupMask.Task) != 0)
					AllocString(ref pszTask, ref cchTask);

				if ((mask & ListViewGroupMask.DescriptionBottom) != 0)
					AllocString(ref pszDescriptionBottom, ref cchDescriptionBottom);

				if ((mask & ListViewGroupMask.DescriptionTop) != 0)
					AllocString(ref pszDescriptionTop, ref cchDescriptionTop);
			}

			public string DescriptionBottom
			{
				get { return GetString(pszDescriptionBottom); }
				set { if (SetString(ref pszDescriptionBottom, ref cchDescriptionBottom, value)) mask |= ListViewGroupMask.DescriptionBottom; }
			}

			public string DescriptionTop
			{
				get { return GetString(pszDescriptionTop); }
				set { if (SetString(ref pszDescriptionTop, ref cchDescriptionTop, value)) mask |= ListViewGroupMask.DescriptionTop; }
			}

			public string Footer
			{
				get { return GetString(pszFooter); }
				set { if (SetString(ref pszFooter, ref cchFooter, value)) mask |= ListViewGroupMask.Footer; }
			}

			public void GetAlignment(out HorizontalAlignment header, out HorizontalAlignment footer)
			{
				header = ((this.uAlign & 2) != 0) ? HorizontalAlignment.Center : ((this.uAlign & 4) != 0) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
				footer = ((this.uAlign & 0x10) != 0) ? HorizontalAlignment.Center : ((this.uAlign & 0x20) != 0) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
			}

			public void SetAlignment(HorizontalAlignment header, HorizontalAlignment footer)
			{
				this.uAlign = (uint)(footer == HorizontalAlignment.Left ? 8 : (footer == HorizontalAlignment.Center ? 0x10 : 0x20)) |
					(uint)(header == HorizontalAlignment.Left ? 1 : (header == HorizontalAlignment.Center ? 2 : 4));
				this.mask |= ListViewGroupMask.Align;
			}

			public string Header
			{
				get { return GetString(pszHeader); }
				set { if (SetString(ref pszHeader, ref cchHeader, value)) mask |= ListViewGroupMask.Header; }
			}

			public string Subtitle
			{
				get { return GetString(pszSubtitle); }
				set { if (SetString(ref pszSubtitle, ref cchSubtitle, value)) mask |= ListViewGroupMask.Subtitle; }
			}

			public string Task
			{
				get { return GetString(pszTask); }
				set { if (SetString(ref pszTask, ref cchTask, value)) mask |= ListViewGroupMask.Task; }
			}

			public int TitleImageIndex
			{
				get { return iTitleImage; }
				set
				{
					if (value != iTitleImage)
					{
						mask |= ListViewGroupMask.TitleImage;
						iTitleImage = value;
					}
				}
			}

			public void Dispose()
			{
				FreeString(ref pszHeader, ref cchHeader);
				FreeString(ref pszFooter, ref cchFooter);
				FreeString(ref pszSubtitle, ref cchSubtitle);
				FreeString(ref pszTask, ref cchTask);
				FreeString(ref pszDescriptionBottom, ref cchDescriptionBottom);
				FreeString(ref pszDescriptionTop, ref cchDescriptionTop);
			}

			public void SetState(ListViewGroupState state, bool on = true)
			{
				this.mask |= ListViewGroupMask.State;
				this.stateMask |= state;
				this.state |= on ? state : 0;
			}

			private void AllocString(ref IntPtr ptr, ref uint size)
			{
				FreeString(ref ptr, ref size);
				ptr = Marshal.AllocHGlobal(cbBuffer);
				Marshal.WriteIntPtr(ptr, IntPtr.Zero);
				size = cbBuffer;
			}

			private void FreeString(ref IntPtr ptr, ref uint size)
			{
				if (ptr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(ptr);
					ptr = IntPtr.Zero;
					size = 0;
				}
			}

			private string GetString(IntPtr pString)
			{
				return Marshal.PtrToStringUni(pString);
			}

			private bool SetString(ref IntPtr ptr, ref uint size, string value = null)
			{
				string s = GetString(ptr);
				if (value == string.Empty) value = null;
				if (string.Compare(s, value) != 0)
				{
					FreeString(ref ptr, ref size);
					if (value != null)
					{
						ptr = Marshal.StringToHGlobalUni(value);
						size = (uint)value.Length + 1;
					}
					return true;
				}
				return false;
			}
		}
	}
}
