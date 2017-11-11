using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		private const Int32 HDM_FIRST = 0x1200;
		private const Int32 HDN_FIRST = (-300);

		[DllImport(USER32, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, HeaderMessage message, int wParam, [In, Out] NativeMethods.HDITEM item);

		[DllImport(USER32, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, HeaderMessage message, int wParam, [In, Out] NativeMethods.HDLAYOUT layout);

		[DllImport(USER32, SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, HeaderMessage message, int wParam, [In, Out] NativeMethods.HDHITTESTINFO hittest);

		[Flags]
		public enum HeaderHitTestFlag : uint
		{
			NoWhere = 0x0001,
			OnHeader = 0x0002,
			OnDivider = 0x0004,
			OnDivOpen = 0x0008,
			OnFilter = 0x0010,
			OnFilterButton = 0x0020,
			Above = 0x0100,
			Below = 0x0200,
			ToRight = 0x0400,
			ToLeft = 0x0800,
			OnItemStateIcon = 0x1000,
			OnDropdown = 0x2000,
			OnOverflow = 0x4000,
		}

		[Flags]
		public enum HeaderItemFormat : uint
		{
			Left = 0x0000, // Same as LVCFMT_LEFT
			Right = 0x0001, // Same as LVCFMT_RIGHT
			Center = 0x0002, // Same as LVCFMT_CENTER
			JustifyMask = 0x0003, // Same as LVCFMT_JUSTIFYMASK
			RtlReading = 0x0004, // Same as LVCFMT_LEFT
			Checkbox = 0x0040,
			Checked = 0x0080,
			FixedWidth = 0x0100, // Can't resize the column; same as LVCFMT_FIXED_WIDTH
			SortDown = 0x0200,
			SortUp = 0x0400,
			Image = 0x0800, // Same as LVCFMT_IMAGE
			BitmapOnRight = 0x1000, // Same as LVCFMT_BITMAP_ON_RIGHT
			Bitmap = 0x2000,
			String = 0x4000,
			OwnerDraw = 0x8000, // Same as LVCFMT_COL_HAS_IMAGES
			SplitButton = 0x1000000
		}

		public enum HeaderItemImageDisplay
		{
			None,
			Bitmap = 0x2000,
			ImageListItem = 0x0800,
			DownArrow = 0x0200,
			UpArrow = 0x0400,
		}

		[Flags]
		public enum HeaderItemMask : uint
		{
			Width = 0x0001,
			Height = Width,
			Text = 0x0002,
			Format = 0x0004,
			LParam = 0x0008,
			Bitmap = 0x0010,
			Image = 0x0020,
			DISetItem = 0x0040,
			Order = 0x0080,
			Filter = 0x0100,
			State = 0x0200,
			All = 0x03FF,
		}

		public enum HeaderMessage : uint
		{
			GetItemCount = (HDM_FIRST + 0), // 0, 0
			InsertItem = (HDM_FIRST + 10), // int, HDITEM
			DeleteItem = (HDM_FIRST + 2), // int, 0
			GetItem = (HDM_FIRST + 11), // int, HDITEM
			SetItem = (HDM_FIRST + 12), // int, HDITEM
			Layout = (HDM_FIRST + 5), // 0, HDLAYOUT
			HitTest = (HDM_FIRST + 6), // 0, HDHITTEST
			GetItemRect = (HDM_FIRST + 7), // int, RECT*
			SetImageList = (HDM_FIRST + 8), // HDSIL_, hImageList
			GetImageList = (HDM_FIRST + 9), // 0, 0
			OrderToIndex = (HDM_FIRST + 15), // int, 0
			CreateDragImage = (HDM_FIRST + 16), // int, 0
			GetOrderArray = (HDM_FIRST + 17), // iCount, lpArray
			SetOrderArray = (HDM_FIRST + 18), // iCount, lpArray
			SetHotDivider = (HDM_FIRST + 19), // bool, int
			SetBitmapMargin = (HDM_FIRST + 20),// iWidth, 0
			GetBitmapMargin = (HDM_FIRST + 21), // 0,0
			SetUnicodeFormat = 0x2005,        // CCM_SETUNICODEFORMAT,
			GetUnicodeFormat = 0x2006,        // CCM_GETUNICODEFORMAT,
			SetFilterChangeTimeout = (HDM_FIRST + 22), // 0, int
			EditFilter = (HDM_FIRST + 23), // int, bool
			ClearFilter = (HDM_FIRST + 24), // int, 0
			GetItemDropDownRect = (HDM_FIRST + 25), // int, RECT
			GetOverflowRect = (HDM_FIRST + 26), // 0, RECT*
			GetFocusedItem = (HDM_FIRST + 27), // 0,0
			SetFocusedItem = (HDM_FIRST + 28), // 0, int
		}

		public enum HeaderNotification : int
		{
			ItemChanging       = (HDN_FIRST-20),
			ItemChanged        = (HDN_FIRST-21),
			ItemClick          = (HDN_FIRST-22),
			ItemDblClick       = (HDN_FIRST-23),
			DividerDblClick    = (HDN_FIRST-25),
			BeginTrack         = (HDN_FIRST-26),
			EndTrack           = (HDN_FIRST-27),
			Track              = (HDN_FIRST-28),
			GetDispInfo        = (HDN_FIRST-29),
			BeginDrag          = (HDN_FIRST-10),
			EndDrag            = (HDN_FIRST-11),
			FilterChange       = (HDN_FIRST-12),
			FilterBtnClick     = (HDN_FIRST-13),
			BeginFilterEdit    = (HDN_FIRST-14),
			EndFilterEdit      = (HDN_FIRST-15),
			ItemStateIconClick = (HDN_FIRST-16),
			ItemKeyDown        = (HDN_FIRST-17),
			DropDown           = (HDN_FIRST-18),
			OverflowClick      = (HDN_FIRST-19),
		}

		[StructLayout(LayoutKind.Sequential)]
		public class HDHITTESTINFO
		{
			private int pt_x;
			private int pt_y;
			private HeaderHitTestFlag flags;
			private int iItem;

			public HDHITTESTINFO(System.Drawing.Point pt)
			{
				pt_x = pt.X; pt_y = pt.Y;
			}

			public HeaderHitTestFlag Flags => flags;

			public int ItemIndex => iItem;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class HDITEM : IDisposable
		{
			private HeaderItemMask mask = 0;
			private int cxy = 0;
			private IntPtr pszText = IntPtr.Zero;
			private IntPtr hbm = IntPtr.Zero;
			private uint cchTextMax = 0;
			private HeaderItemFormat fmt = 0;
			private IntPtr lParam = IntPtr.Zero;
			private int iImage = 0;
			private int iOrder = 0;
			private int type = 0;
			private IntPtr pvFilter = IntPtr.Zero;
			private int state = 0;

			public HDITEM(HeaderItemMask mask = HeaderItemMask.All)
			{
				if (mask.IsFlagSet(HeaderItemMask.Text))
				{
					cchTextMax = 1024;
					InteropUtil.AllocString(ref pszText, ref cchTextMax);
				}
			}

			public HDITEM(string text = null)
			{
				Text = text;
			}

			public System.Drawing.Bitmap Bitmap
			{
				get { return (hbm == IntPtr.Zero) ? null : System.Drawing.Bitmap.FromHbitmap(hbm); }
				set { hbm = (value == null) ? IntPtr.Zero : value.GetHbitmap(); EnumUtil.SetFlags(ref mask, HeaderItemMask.Bitmap, true); }
			}

			public bool Checked
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.Checked); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.Checked, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public object Filter
			{
				get
				{
					if (!mask.IsFlagSet(HeaderItemMask.Filter))
						return null;
					switch (type)
					{
						case 0: // HDFT_ISSTRING
							return Marshal.PtrToStringAuto(pvFilter);

						case 1: // HDFT_ISNUMBER
							return pvFilter.ToInt32();

						case 2: // HDFT_ISDATE
							return (DateTime)pvFilter.ToStructure<SYSTEMTIME>();

						case 0x8000: // HDFT_HASNOVALUE
							return null;

						default:
							throw new InvalidOperationException();
					}
				}
				set
				{
					if (value == null)
					{
						type = 0x8000;
						Marshal.FreeHGlobal(pvFilter);
						pvFilter = IntPtr.Zero;
					}
					else
					{
						if (value is DateTime) value = (SYSTEMTIME)value;

						if (value is string)
							pvFilter = Marshal.StringToHGlobalUni((string)value);
						else if (value is int)
							pvFilter = new IntPtr((int)value);
						else if (value is SYSTEMTIME)
							pvFilter = InteropUtil.StructureToPtr(value);
						else
							throw new ArgumentException("Value must be a string, integer, DateTime or SYSTEMTIME");
					}
					EnumUtil.SetFlags(ref mask, HeaderItemMask.Filter, true);
				}
			}

			public bool FixedWidth
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.FixedWidth); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.FixedWidth, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public bool Focused
			{
				get { return state == 1; }
				set { state = value ? 1 : 0; EnumUtil.SetFlags(ref mask, HeaderItemMask.State, true); }
			}

			public HeaderItemFormat Format
			{
				get { return fmt; }
				set { fmt = value; EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public LeftRightAlignment ImageAlignment
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.BitmapOnRight) ? LeftRightAlignment.Right : LeftRightAlignment.Left; }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.BitmapOnRight, value == LeftRightAlignment.Right); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public HeaderItemImageDisplay ImageDisplay
			{
				get
				{
					if (fmt.IsFlagSet(HeaderItemFormat.Bitmap))
						return HeaderItemImageDisplay.Bitmap;
					else if (fmt.IsFlagSet(HeaderItemFormat.Image))
						return HeaderItemImageDisplay.ImageListItem;
					else if (fmt.IsFlagSet(HeaderItemFormat.SortDown))
						return HeaderItemImageDisplay.DownArrow;
					else if (fmt.IsFlagSet(HeaderItemFormat.SortUp))
						return HeaderItemImageDisplay.UpArrow;
					return HeaderItemImageDisplay.None;
				}
				set
				{
					const HeaderItemFormat imgMask = HeaderItemFormat.Bitmap | HeaderItemFormat.Image | HeaderItemFormat.SortUp | HeaderItemFormat.SortDown;
					EnumUtil.SetFlags(ref fmt, imgMask, false);
					EnumUtil.SetFlags(ref fmt, (HeaderItemFormat)value, true);
					EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true);
				}
			}

			public int ImageIndex
			{
				get { return iImage; }
				set { iImage = value; EnumUtil.SetFlags(ref mask, HeaderItemMask.Image, true); }
			}

			public IntPtr LParam
			{
				get { return lParam; }
				set { lParam = value; EnumUtil.SetFlags(ref mask, HeaderItemMask.LParam, true); }
			}

			public int Order
			{
				get { return iOrder; }
				set { iOrder = value; EnumUtil.SetFlags(ref mask, HeaderItemMask.Order, true); }
			}

			public bool OwnerDrawn
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.OwnerDraw); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.OwnerDraw, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public bool RightToLeft
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.RtlReading); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.RtlReading, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public bool ShowCheckbox
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.Checkbox); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.Checkbox, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public bool ShowSplitButton
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.SplitButton); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.SplitButton, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public bool ShowText
			{
				get { return fmt.IsFlagSet(HeaderItemFormat.String); }
				set { EnumUtil.SetFlags(ref fmt, HeaderItemFormat.String, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true); }
			}

			public string Text
			{
				get { return InteropUtil.GetString(pszText); }
				set { InteropUtil.SetString(ref pszText, ref cchTextMax, value); EnumUtil.SetFlags(ref mask, HeaderItemMask.Text, value != null); }
			}

			public HorizontalAlignment TextAlignment
			{
				get { return (HorizontalAlignment)(fmt & HeaderItemFormat.JustifyMask); }
				set
				{
					EnumUtil.SetFlags(ref fmt, HeaderItemFormat.JustifyMask, false);
					EnumUtil.SetFlags(ref fmt, (HeaderItemFormat)value, true);
					EnumUtil.SetFlags(ref mask, HeaderItemMask.Format, true);
				}
			}

			public int WidthOrHeight
			{
				get { return cxy; }
				set { cxy = value; EnumUtil.SetFlags(ref mask, HeaderItemMask.Width, true); }
			}

			public void Dispose()
			{
				InteropUtil.FreeString(ref pszText, ref cchTextMax);
				if (mask.IsFlagSet(HeaderItemMask.Filter) && (type == 0 || type == 2))
					Marshal.FreeHGlobal(pvFilter);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class HDLAYOUT : IDisposable
		{
			private IntPtr prc;
			private IntPtr pwpos;

			public HDLAYOUT(ref RECT rc)
			{
				prc = InteropUtil.StructureToPtr(rc);
				pwpos = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDOWPOS)));
			}

			public WINDOWPOS Position => pwpos.ToStructure<WINDOWPOS>();

			public void Dispose()
			{
				Marshal.FreeHGlobal(prc);
				Marshal.FreeHGlobal(pwpos);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class NMHDDISPINFO
		{
			public NMHDR hdr;
			public int iItem;
			public uint mask;
			public IntPtr pszText = IntPtr.Zero;
			public int cchTextMax;
			public int iImage;
			public IntPtr lParam = IntPtr.Zero;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class NMHDFILTERBTNCLICK
		{
			public NMHDR hdr;
			public int iItem;
			public RECT rc;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class NMHEADER
		{
			public NativeMethods.NMHDR nmhdr;
			public int iItem;
			public int iButton;
			public IntPtr pItem = IntPtr.Zero;
		}
	}
}