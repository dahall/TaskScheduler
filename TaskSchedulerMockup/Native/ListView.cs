using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVBKIMAGE bkImage);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVCOLUMN column);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVGROUP group);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVGROUPMETRICS metrics);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVHITTESTINFO hitTestInfo);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVINSERTMARK insertMark);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, System.Drawing.Point wParam, [In, Out] NativeMethods.LVINSERTMARK insertMark);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVITEM item);

		[DllImport(USER32, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SendMessage(IntPtr hWnd, ListViewMessage message, [In, Out] ref LVITEMINDEX wParam, [In] int lParam);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, int wParam, [In, Out] NativeMethods.LVTILEVIEWINFO tileViewInfo);

		[DllImport(USER32, SetLastError = true)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, ListViewMessage message, ListViewImageList wParam, [In, Out] IntPtr hImageList);

		private const int LVM_FIRST = 0x1000;
		private const int LVN_FIRST = -0x100;

		public static readonly IntPtr LPSTR_TEXTCALLBACK = (IntPtr)(-1);
		public const int I_IMAGECALLBACK = (-1);
		public const int I_IMAGENONE = (-2);
		public const int I_COLUMNSCALLBACK = (-1);

		public enum ListViewArrange
		{
			Normal = 0,
			SnapToGrid = 5
		}

		/// <summary>
		/// LVM_ Messages for SendMessage
		/// </summary>
		public enum ListViewMessage : uint
		{
			SetUnicodeFormat         = 0x2005,        // CCM_SETUNICODEFORMAT,
			GetUnicodeFormat         = 0x2006,        // CCM_GETUNICODEFORMAT,
			GetBkColor               = (LVM_FIRST + 0),
			SetBkColor               = (LVM_FIRST + 1),
			GetImageList             = (LVM_FIRST + 2),
			SetImageList             = (LVM_FIRST + 3),
			GetItemCount             = (LVM_FIRST + 4),
			GetItem                  = (LVM_FIRST + 75),
			SetItem                  = (LVM_FIRST + 76),
			InsertItem               = (LVM_FIRST + 77),
			DeleteItem               = (LVM_FIRST + 8),
			DeleteAllItems           = (LVM_FIRST + 9),
			GetCallbackMask          = (LVM_FIRST + 10),
			SetCallbackMask          = (LVM_FIRST + 11),
			GetNextItem              = (LVM_FIRST + 12),
			FindItem                 = (LVM_FIRST + 83),
			GetItemRect              = (LVM_FIRST + 14),
			SetItemPosition          = (LVM_FIRST + 15),
			GetItemPosition          = (LVM_FIRST + 16),
			GetStringWidth           = (LVM_FIRST + 87),
			HitTest                  = (LVM_FIRST + 18),
			EnsureVisible            = (LVM_FIRST + 19),
			Scroll                   = (LVM_FIRST + 20),
			RedrawItems              = (LVM_FIRST + 21),
			Arrange                  = (LVM_FIRST + 22),
			EditLabel                = (LVM_FIRST + 118),
			GetEditControl           = (LVM_FIRST + 24),
			GetColumn                = (LVM_FIRST + 95),
			SetColumn                = (LVM_FIRST + 96),
			InsertColumn             = (LVM_FIRST + 97),
			DeleteColumn             = (LVM_FIRST + 28),
			GetColumnWidth           = (LVM_FIRST + 29),
			SetColumnWidth           = (LVM_FIRST + 30),
			GetHeader                = (LVM_FIRST + 31),
			CreateDragImage          = (LVM_FIRST + 33),
			GetViewRect              = (LVM_FIRST + 34),
			GetTextColor             = (LVM_FIRST + 35),
			SetTextColor             = (LVM_FIRST + 36),
			GetTextBkColor           = (LVM_FIRST + 37),
			SetTextBkColor           = (LVM_FIRST + 38),
			GetTopIndex              = (LVM_FIRST + 39),
			GetCountPerPage          = (LVM_FIRST + 40),
			GetOrigin                = (LVM_FIRST + 41),
			Update                   = (LVM_FIRST + 42),
			SetItemState             = (LVM_FIRST + 43),
			GetItemState             = (LVM_FIRST + 44),
			GetItemText              = (LVM_FIRST + 115),
			SetItemText              = (LVM_FIRST + 116),
			SetItemCount             = (LVM_FIRST + 47),
			SortItems                = (LVM_FIRST + 48),
			SetItemPosition32        = (LVM_FIRST + 49),
			GetSelectedCount         = (LVM_FIRST + 50),
			GetItemSpacing           = (LVM_FIRST + 51),
			GetISearchString         = (LVM_FIRST + 117),
			SetIconSpacing           = (LVM_FIRST + 53),
			SetExtendedListViewStyle = (LVM_FIRST + 54),            // optional wParam == mask
			GetExtendedListViewStyle = (LVM_FIRST + 55),
			GetSubitemRect           = (LVM_FIRST + 56),
			SubItemHitTest           = (LVM_FIRST + 57),
			SetColumnOrderArray      = (LVM_FIRST + 58),
			GetColumnOrderArray      = (LVM_FIRST + 59),
			SetHotItem               = (LVM_FIRST + 60),
			GetHotItem               = (LVM_FIRST + 61),
			SetHotCursor             = (LVM_FIRST + 62),
			GetHotCursor             = (LVM_FIRST + 63),
			ApproximateViewRect      = (LVM_FIRST + 64),
			SetWorkAreas             = (LVM_FIRST + 65),
			GetWorkAreas             = (LVM_FIRST + 70),
			GetNumberOfWorkAreas     = (LVM_FIRST + 73),
			GetSelectionMark         = (LVM_FIRST + 66),
			SetSelectionMark         = (LVM_FIRST + 67),
			SetHoverTime             = (LVM_FIRST + 71),
			GetHoverTime             = (LVM_FIRST + 72),
			SetTooltips              = (LVM_FIRST + 74),
			GetTooltips              = (LVM_FIRST + 78),
			SortItemsEx              = (LVM_FIRST + 81),
			SetBkImage               = (LVM_FIRST + 138),
			GetBkImage               = (LVM_FIRST + 139),
			SetSelectedColumn        = (LVM_FIRST + 140),
			SetView                  = (LVM_FIRST + 142),
			GetView                  = (LVM_FIRST + 143),
			InsertGroup              = (LVM_FIRST + 145),
			SetGroupInfo             = (LVM_FIRST + 147),
			GetGroupInfo             = (LVM_FIRST + 149),
			RemoveGroup              = (LVM_FIRST + 150),
			MoveGroup                = (LVM_FIRST + 151),
			GetGroupCount            = (LVM_FIRST + 152),
			GetGroupInfoByIndex      = (LVM_FIRST + 153),
			MoveItemToGroup          = (LVM_FIRST + 154),
			GetGroupRect             = (LVM_FIRST + 98),
			SetGroupMetrics          = (LVM_FIRST + 155),
			GetGroupMetrics          = (LVM_FIRST + 156),
			EnableGroupView          = (LVM_FIRST + 157),
			SortGroups               = (LVM_FIRST + 158),
			InsertGroupSorted        = (LVM_FIRST + 159),
			RemoveAllGroups          = (LVM_FIRST + 160),
			HasGroup                 = (LVM_FIRST + 161),
			GetGroupState            = (LVM_FIRST + 92),
			GetFocusedGroup          = (LVM_FIRST + 93),
			SetTileViewInfo          = (LVM_FIRST + 162),
			GetTileViewInfo          = (LVM_FIRST + 163),
			SetTileInfo              = (LVM_FIRST + 164),
			GetTileInfo              = (LVM_FIRST + 165),
			SetInsertMark            = (LVM_FIRST + 166),
			GetInsertMark            = (LVM_FIRST + 167),
			InsertMarkHitTest        = (LVM_FIRST + 168),
			GetInsertMarkRect        = (LVM_FIRST + 169),
			SetInsertMarkColor       = (LVM_FIRST + 170),
			GetInsertMarkColor       = (LVM_FIRST + 171),
			GetSelectedColumn        = (LVM_FIRST + 174),
			IsGroupViewEnabled       = (LVM_FIRST + 175),
			GetOutlineColor          = (LVM_FIRST + 176),
			SetOutlineColor          = (LVM_FIRST + 177),
			CancelEditLabel          = (LVM_FIRST + 179),
			MapIndexToDd             = (LVM_FIRST + 180),
			MapIdToIndex             = (LVM_FIRST + 181),
			IsItemVisible            = (LVM_FIRST + 182),
			GetAccVersion            = (LVM_FIRST + 193),
			GetEmptyText             = (LVM_FIRST + 204),
			GetFooterRect            = (LVM_FIRST + 205),
			GetFooterInfo            = (LVM_FIRST + 206),
			GetFooterItemRect        = (LVM_FIRST + 207),
			GetFooterItem            = (LVM_FIRST + 208),
			GetItemIndexRect         = (LVM_FIRST + 209),
			SetItemIndexState        = (LVM_FIRST + 210),
			GetNextItemIndex         = (LVM_FIRST + 211),
			SetPreserveAlpha         = (LVM_FIRST + 212),
			/*SetBkImage               = SETBKIMAGEW,
			GetBkImage               = GETBKIMAGEW,*/
		}

		public enum ListViewNotifications
		{
			BeginDrag = -109,
			BeginLabelEdit = -175,
			BeginrDrag = -111,
			ColumnClick = -108,
			EndLabelEdit = -176,
			GetDispInfo = -177,
			GetInfoTip = -158,
			ItemActivate = -114,
			ItemChanged = -101,
			ItemChanging = -100,
			KeyDown = -155,
			OdCachehInt = -113,
			OdFindItem = -179,
			OdStateChanged = -115,
			SetDispInfo = -178,
			ColumnDropDown = -164,
		}

		[Flags]
		public enum ListViewBkImageFlag : uint
		{
			SourceNone = 0x00000000,
			SourceHbitmap = 0x00000001,
			SourceUrl = 0x00000002,
			SourceMask = 0x00000003,
			StyleNormal = 0x00000000,
			StyleTile = 0x00000010,
			StyleMask = 0x00000010,
			FlagTileOffset = 0x00000100,
			TypeWatermark = 0x10000000,
			FlagAlphaBlend = 0x20000000,
		}

		[Flags]
		public enum ListViewColumMask
		{
			Fmt = 0x0001,
			Width = 0x0002,
			Text = 0x0004,
			Subitem = 0x0008,
			Image = 0x0010,
			Order = 0x0020,
			MinWidth = 0x0040,
			DefaultWidth = 0x0080,
			IdealWidth = 0x0100,
		}

		[Flags]
		public enum ListViewColumnFormat
		{
			Left = 0x0000,
			Right = 0x0001,
			Center = 0x0002,
			JustifyMask = 0x0003,
			Image = 0x0800,
			BitmapOnRight = 0x1000,
			ColHasImages = 0x8000,
			FixedWidth = 0x00100,
			NoDpiScale = 0x40000,
			FixedRatio = 0x80000,
			LineBreak = 0x100000,
			Fill = 0x200000,
			Wrap = 0x400000,
			NoTitle = 0x800000,
			TilePlacementMask = LineBreak | Fill,
			SplitButton = 0x1000000,
		}

		[Flags]
		public enum ListViewFindInfoFlag
		{
			Param = 0x0001,
			String = 0x0002,
			Substring = 0x0004,
			Partial = 0x0008,
			Wrap = 0x0020,
			NearestXY = 0x0040,
		}

		public enum ListViewGroupRect
		{
			Group,
			Header,
			Label,
			SubsetLink
		}

		[Flags]
		public enum ListViewGroupMask : uint
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
		public enum ListViewGroupMetricsMask
		{
			None = 0x00000000,
			BorderSize = 0x00000001,
			BorderColor = 0x00000002,
			TextColor = 0x00000004
		}

		[Flags]
		public enum ListViewGroupState : uint
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

		[Flags]
		public enum ListViewHitTestFlag : uint
		{
			Nowhere = 0x00000001,
			OnItemIcon = 0x00000002,
			OnItemLabel = 0x00000004,
			OnItemStateIcon = 0x00000008,
			OnItem = (OnItemIcon | OnItemLabel | OnItemStateIcon),
			Above = 0x00000008,
			Below = 0x00000010,
			ToRight = 0x00000020,
			ToLeft = 0x00000040,
			ExGroupHeader = 0x10000000,
			ExGroupFooter = 0x20000000,
			ExGroupCollapse = 0x40000000,
			ExGroupBackground = 0x80000000,
			ExGroupStateIcon = 0x01000000,
			ExGroupSubsetLink = 0x02000000,
			ExGroup = (ExGroupBackground | ExGroupCollapse | ExGroupFooter | ExGroupHeader | ExGroupStateIcon | ExGroupSubsetLink),
			ExOnContents = 0x04000000,
			ExFooter = 0x08000000,
		}

		public enum ListViewImageList
		{
			Normal,
			Small,
			State,
			GroupHeader
		}

		public enum ListViewInsertMarkFlag
		{
			Before,
			After
		}

		[Flags]
		public enum ListViewItemMask : uint
		{
			Text = 0x00000001,
			Image = 0x00000002,
			Param = 0x00000004,
			State = 0x00000008,
			Indent = 0x00000010,
			NoRecompute = 0x00000800,
			GroupId = 0x00000100,
			Columns = 0x00000200,
			ColFmt = 0x00010000,
			DISetItem = 0x1000,
			All = 0x0001FFFF
		}

		public enum ListViewItemRect
		{
			Bounds,
			Icon,
			Label,
			SelectBounds
		}

		[Flags]
		public enum ListViewItemState : uint
		{
			None = 0x0000,
			Focused = 0x0001,
			Selected = 0x0002,
			Cut = 0x0004,
			DropHilited = 0x0008,
			Glow = 0x0010,
			//Activating = 0x0020,
			OverlayMask = 0x0F00,
			StateImageMask = 0xF000,
			All = 0xFFFFFFFF
		}

		[Flags]
		public enum ListViewNextItemFlag
		{
			All = 0x0000,
			Focused = 0x0001,
			Selected = 0x0002,
			Cut = 0x0004,
			DropHilited = 0x0008,
			StateMask = (Focused | Selected | Cut | DropHilited),
			VisibleOrder = 0x0010,
			Previous = 0x0020,
			VisibleOnly = 0x0040,
			SameGroupOnly = 0x0080,
			Above = 0x0100,
			Below = 0x0200,
			ToLeft = 0x0400,
			ToRight = 0x0800,
			DirectionMask = (Above | Below | ToLeft | ToRight),
		}

		[Flags]
		public enum ListViewStyle
		{
			Icon = 0x0000,
			Report = 0x0001,
			SmallIcon = 0x0002,
			List = 0x0003,
			TypeMask = 0x0003,
			SingleSel = 0x0004,
			ShowSelAlways = 0x0008,
			SortAscending = 0x0010,
			SortDescending = 0x0020,
			ShareImageLists = 0x0040,
			NoLabelWrap = 0x0080,
			AutoArrange = 0x0100,
			EditLabels = 0x0200,
			OwnerData = 0x1000,
			NoScroll = 0x2000,
			TypeStyleMask = 0xfc00,
			AlignTop = 0x0000,
			AlignLeft = 0x0800,
			AlignMask = 0x0c00,
			OwnerDrawFixed = 0x0400,
			NoColumnHeader = 0x4000,
			NoSortHeader = 0x8000,
		}

		[Flags]
		public enum ListViewStyleEx : uint
		{
			Gridlines = 0x00000001,
			SubitemImages = 0x00000002,
			Checkboxes = 0x00000004,
			TrackSelect = 0x00000008,
			HeaderDragDrop = 0x00000010,
			FullRowSelect = 0x00000020,
			OneClickActivate = 0x00000040,
			TwoClickActivate = 0x00000080,
			FlatSb = 0x00000100,
			Regional = 0x00000200,
			InfoTip = 0x00000400,
			UnderlineHot = 0x00000800,
			UnderlineCold = 0x00001000,
			MultiWorkAreas = 0x00002000,
			LabelTip = 0x00004000,
			BorderSelect = 0x00008000,
			DoubleBuffer = 0x00010000,
			HideLabels = 0x00020000,
			SingleRow = 0x00040000,
			SnapToGrid = 0x00080000,
			SimpleSelect = 0x00100000,
			JustifyColumns = 0x00200000,
			TransparentBkgnd = 0x00400000,
			TransparentShadowText = 0x00800000,
			AutoAutoArrange = 0x01000000,
			HeaderInAllViews = 0x02000000,
			AutoCheckSelect = 0x08000000,
			AutoSizeColumns = 0x10000000,
			ColumnSnapPoints = 0x40000000,
			ColumnOverflow = 0x80000000,
		}

		[Flags]
		public enum ListViewTileViewFlag : uint
		{
			Autosize    = 0x00000000,
			FixedWidth  = 0x00000001,
			FixedHeight = 0x00000002,
			FixedSize   = 0x00000003,
			Extended    = 0x00000004,
		}

		[Flags]
		public enum ListViewTileViewMask : uint
		{
			TileSize       = 0x00000001,
			Columns        = 0x00000002,
			LabelMargin    = 0x00000004,
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVBKIMAGE : IDisposable
		{
			public ListViewBkImageFlag ulFlags;
			private IntPtr hBmp = IntPtr.Zero;
			private IntPtr pszImage = IntPtr.Zero;
			private uint cchImageMax;
			public int xOffset;
			public int yOffset;

			public LVBKIMAGE(System.Drawing.Bitmap bmp, bool isWatermark, bool isWatermarkAlphaBlended)
			{
				Bitmap = bmp;
				ulFlags = isWatermark ? ListViewBkImageFlag.TypeWatermark : ListViewBkImageFlag.SourceHbitmap;
				if (isWatermark && isWatermarkAlphaBlended)
					ulFlags |= ListViewBkImageFlag.FlagAlphaBlend;
			}

			public LVBKIMAGE(System.Drawing.Bitmap bmp, bool isTiled)
			{
				Bitmap = bmp;
				ulFlags = ListViewBkImageFlag.SourceHbitmap;
				if (isTiled)
					ulFlags |= ListViewBkImageFlag.StyleTile;
			}

			public LVBKIMAGE(string url, bool isTiled)
			{
				Url = url;
				ulFlags = ListViewBkImageFlag.SourceUrl;
				if (isTiled)
					ulFlags |= ListViewBkImageFlag.StyleTile;
			}

			public LVBKIMAGE() : this(ListViewBkImageFlag.SourceNone) { }

			public LVBKIMAGE(ListViewBkImageFlag flags)
			{
				ulFlags = flags;
				if (ulFlags.IsFlagSet(ListViewBkImageFlag.SourceUrl))
				{
					cchImageMax = 1024;
					InteropUtil.AllocString(ref pszImage, ref cchImageMax);
				}
			}

			public System.Drawing.Bitmap Bitmap
			{
				get { return hBmp != IntPtr.Zero ? System.Drawing.Bitmap.FromHbitmap(hBmp) : null; }
				set { hBmp = (value == null) ? IntPtr.Zero : value.GetHbitmap(); }
			}

			public string Url
			{
				get { return InteropUtil.GetString(pszImage); }
				set { InteropUtil.SetString(ref pszImage, ref cchImageMax, value); }
			}

			void IDisposable.Dispose()
			{
				InteropUtil.FreeString(ref pszImage, ref cchImageMax);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVCOLUMN : IDisposable
		{
			private ListViewColumMask mask;
			private ListViewColumnFormat fmt;
			private int cx;
			private IntPtr pszText;
			private uint cchTextMax;
			private int iSubItem;
			private int iImage;
			private int iOrder;
			private int cxMin;
			private int cxDefault;
			private int cxIdeal;

			public LVCOLUMN(ListViewColumMask mask)
			{
				this.mask = mask;
				if (mask.IsFlagSet(ListViewColumMask.Text))
					InteropUtil.AllocString(ref pszText, ref cchTextMax);
			}

			public ListViewColumnFormat Format
			{
				get { return fmt; }
				set { fmt = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.Fmt, true); }
			}

			public string Text
			{
				get { return InteropUtil.GetString(pszText); }
				set { InteropUtil.SetString(ref pszText, ref cchTextMax, value); EnumUtil.SetFlags(ref mask, ListViewColumMask.Text, value != null); }
			}

			public int Subitem
			{
				get { return iSubItem; }
				set { iSubItem = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.Subitem, true); }
			}

			public int ImageListIndex
			{
				get { return iImage; }
				set { iImage = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.Image, true); }
			}

			public int ColumnPosition
			{
				get { return iOrder; }
				set { iOrder = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.Order, true); }
			}

			public int DefaultWidth
			{
				get { return cxDefault; }
				set { cxDefault = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.DefaultWidth, true); }
			}

			public int MinWidth
			{
				get { return cxMin; }
				set { cxMin = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.MinWidth, true); }
			}

			public int IdealWidth
			{
				get { return cxIdeal; }
				set { cxIdeal = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.IdealWidth, true); }
			}

			public int Width
			{
				get { return cx; }
				set { cx = value; EnumUtil.SetFlags(ref mask, ListViewColumMask.Width, true); }
			}

			void IDisposable.Dispose()
			{
				InteropUtil.FreeString(ref pszText, ref cchTextMax);
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct LVFINDINFO
		{
			private ListViewFindInfoFlag flags;
			private string psz;
			private IntPtr lParam;
			private int ptX;
			private int ptY;
			private int vkDirection;

			public LVFINDINFO(string searchString, bool allowPartial, bool wrap)
			{
				psz = searchString;
				flags = ListViewFindInfoFlag.String;
				if (allowPartial)
					flags |= ListViewFindInfoFlag.Partial;
				if (wrap)
					flags |= ListViewFindInfoFlag.Wrap;
				lParam = IntPtr.Zero;
				ptX = ptY = vkDirection = 0;
			}

			public LVFINDINFO(IntPtr lParam)
			{
				flags = ListViewFindInfoFlag.Param;
				psz = null;
				this.lParam = lParam;
				ptX = ptY = vkDirection = 0;
			}

			public void FindNearestToPoint(System.Drawing.Point pt, System.Windows.Forms.SearchDirectionHint searchDirection)
			{
				ptX = pt.X;
				ptY = pt.Y;
				vkDirection = (int)searchDirection;
				flags |= ListViewFindInfoFlag.NearestXY;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public partial class LVGROUP : IDisposable
		{
			private int cbSize;
			private ListViewGroupMask mask;
			private IntPtr pszHeader;
			private uint cchHeader;
			private IntPtr pszFooter;
			private uint cchFooter;
			private int iGroupId;
			private ListViewGroupState stateMask;
			private ListViewGroupState state;
			private uint uAlign;
			private IntPtr pszSubtitle;
			private uint cchSubtitle;
			private IntPtr pszTask;
			private uint cchTask;
			private IntPtr pszDescriptionTop;
			private uint cchDescriptionTop;
			private IntPtr pszDescriptionBottom;
			private uint cchDescriptionBottom;
			private int iTitleImage;
			private int iExtendedImage;
			private int iFirstItem;
			private uint cItems;
			private IntPtr pszSubsetTitle;
			private uint cchSubsetTitle;

			public LVGROUP(ListViewGroup grp) : this(ListViewGroupMask.Header | ListViewGroupMask.GroupId | ListViewGroupMask.Align, grp.Header)
			{
				SetAlignment(grp.HeaderAlignment, HorizontalAlignment.Left);
			}

			/*public LVGROUP(ListViewGroupEx grp)
			{
				this.cbSize = Marshal.SizeOf(this);
				this.Header = grp.Header;
				this.ID = grp.ID;
				this.SetAlignment(grp.HeaderAlignment, grp.FooterAlignment);
				this.Footer = grp.Footer;
				this.DescriptionBottom = grp.DescriptionBottom;
				this.DescriptionTop = grp.DescriptionTop;
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
					Header = header;
				else if ((mask & ListViewGroupMask.Header) != 0)
					InteropUtil.AllocString(ref pszHeader, ref cchHeader);

				if ((mask & ListViewGroupMask.Footer) != 0)
					InteropUtil.AllocString(ref pszFooter, ref cchFooter);

				if ((mask & ListViewGroupMask.Subtitle) != 0)
					InteropUtil.AllocString(ref pszSubtitle, ref cchSubtitle);

				if ((mask & ListViewGroupMask.Task) != 0)
					InteropUtil.AllocString(ref pszTask, ref cchTask);

				if ((mask & ListViewGroupMask.DescriptionBottom) != 0)
					InteropUtil.AllocString(ref pszDescriptionBottom, ref cchDescriptionBottom);

				if ((mask & ListViewGroupMask.DescriptionTop) != 0)
					InteropUtil.AllocString(ref pszDescriptionTop, ref cchDescriptionTop);
			}

			public string DescriptionBottom
			{
				get { return InteropUtil.GetString(pszDescriptionBottom); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionBottom, InteropUtil.SetString(ref pszDescriptionBottom, ref cchDescriptionBottom, value)); }
			}

			public string DescriptionTop
			{
				get { return InteropUtil.GetString(pszDescriptionTop); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionTop, InteropUtil.SetString(ref pszDescriptionTop, ref cchDescriptionTop, value)); }
			}

			public string Footer
			{
				get { return InteropUtil.GetString(pszFooter); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionTop, InteropUtil.SetString(ref pszFooter, ref cchFooter, value)); }
			}

			public int ID
			{
				get { return iGroupId; }
				set { iGroupId = value; mask |= ListViewGroupMask.GroupId; }
			}

			public int TitleImageIndex
			{
				get { return iTitleImage; }
				set { iTitleImage = value; mask |= ListViewGroupMask.TitleImage; }
			}

			public int ExtendedImageIndex
			{
				get { return iExtendedImage; }
				set { iExtendedImage = value; mask |= ListViewGroupMask.ExtendedImage; }
			}

			public int FirstItem => iFirstItem;

			public uint ItemCount => cItems;

			public void GetAlignment(out HorizontalAlignment header, out HorizontalAlignment footer)
			{
				header = ((uAlign & 2) != 0) ? HorizontalAlignment.Center : ((uAlign & 4) != 0) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
				footer = ((uAlign & 0x10) != 0) ? HorizontalAlignment.Center : ((uAlign & 0x20) != 0) ? HorizontalAlignment.Right : HorizontalAlignment.Left;
			}

			public void SetAlignment(HorizontalAlignment header, HorizontalAlignment footer)
			{
				uAlign = (uint)(footer == HorizontalAlignment.Left ? 8 : (footer == HorizontalAlignment.Center ? 0x10 : 0x20)) |
					(uint)(header == HorizontalAlignment.Left ? 1 : (header == HorizontalAlignment.Center ? 2 : 4));
				mask |= ListViewGroupMask.Align;
			}

			public string Header
			{
				get { return InteropUtil.GetString(pszHeader); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionTop, InteropUtil.SetString(ref pszHeader, ref cchHeader, value)); }
			}

			public string Subtitle
			{
				get { return InteropUtil.GetString(pszSubtitle); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionTop, InteropUtil.SetString(ref pszSubtitle, ref cchSubtitle, value)); }
			}

			public string Task
			{
				get { return InteropUtil.GetString(pszTask); }
				set { EnumUtil.SetFlags(ref mask, ListViewGroupMask.DescriptionTop, InteropUtil.SetString(ref pszTask, ref cchTask, value)); }
			}

			public void Dispose()
			{
				InteropUtil.FreeString(ref pszHeader, ref cchHeader);
				InteropUtil.FreeString(ref pszFooter, ref cchFooter);
				InteropUtil.FreeString(ref pszSubtitle, ref cchSubtitle);
				InteropUtil.FreeString(ref pszTask, ref cchTask);
				InteropUtil.FreeString(ref pszDescriptionBottom, ref cchDescriptionBottom);
				InteropUtil.FreeString(ref pszDescriptionTop, ref cchDescriptionTop);
			}

			public void SetState(ListViewGroupState state, bool on = true)
			{
				mask |= ListViewGroupMask.State;
				stateMask |= state;
				EnumUtil.SetFlags(ref this.state, state, on);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVGROUPMETRICS
		{
			private uint cbSize = ((uint)Marshal.SizeOf(typeof(NativeMethods.LVGROUPMETRICS)));
			public ListViewGroupMetricsMask mask;
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
			public uint crLeft;
			public uint crTop;
			public uint crRight;
			public uint crBottom;
			public uint crHeader;
			public uint crFooter;

			public LVGROUPMETRICS(ListViewGroupMetricsMask mask = ListViewGroupMetricsMask.None)
			{
				this.mask = mask;
			}

			public LVGROUPMETRICS(System.Windows.Forms.Padding padding)
			{
				Padding = padding;
			}

			public LVGROUPMETRICS(int left, int top, int right, int bottom)
			{
				mask = ListViewGroupMetricsMask.BorderSize;
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public System.Windows.Forms.Padding Padding
			{
				get { return new Padding(Left, Top, Right, Bottom); }
				set
				{
					Left = value.Left;
					Top = value.Top;
					Right = value.Right;
					Bottom = value.Bottom;
					mask |= ListViewGroupMetricsMask.BorderSize;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVHITTESTINFO
		{
			private int pt_x;
			private int pt_y;
			private ListViewHitTestFlag flags;
			private int iItem;
			private int iSubItem;
			private int iGroup;

			public LVHITTESTINFO(System.Drawing.Point pt)
			{
				pt_x = pt.X; pt_y = pt.Y;
			}

			public ListViewHitTestFlag Flags => flags;
			public int ItemIndex => iItem;
			public int SubitemIndex => iSubItem;
			public int GroupIndex => iGroup;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVINSERTMARK
		{
			public uint cbSize = ((uint)Marshal.SizeOf(typeof(NativeMethods.LVINSERTMARK)));
			public ListViewInsertMarkFlag dwFlags;
			public int iItem;
			private int dwReserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LVITEMINDEX
		{
			public int iItem;
			public int iGroup;
		}

		public struct LVTILECOLUMNINFO
		{
			public uint columnIndex;
			public ListViewColumnFormat format;

			public LVTILECOLUMNINFO(uint colIdx, ListViewColumnFormat fmt = 0)
			{
				columnIndex = colIdx;
				format = fmt;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVITEM : IDisposable
		{
			private ListViewItemMask mask;
			private int iItem;
			private int iSubItem;
			private uint state;
			private ListViewItemState stateMask;
			private IntPtr pszText;
			private uint cchTextMax;
			private int iImage;
			private IntPtr lParam;
			private int iIndent;
			private int iGroupId;
			private uint cColumns;
			private IntPtr puColumns;
			private IntPtr piColFmt;
			private int iGroup;

			public LVITEM(int item, int subitem = 0, ListViewItemMask mask = ListViewItemMask.All, ListViewItemState stateMask = ListViewItemState.None)
			{
				if (mask.IsFlagSet(ListViewItemMask.Text))
				{
					cchTextMax = 1024;
					InteropUtil.AllocString(ref pszText, ref cchTextMax);
				}
				iItem = item;
				iSubItem = subitem;
				this.stateMask = stateMask;
			}

			public LVITEM(int item, int subitem, string text)
			{
				iItem = item;
				iSubItem = subitem;
				Text = text;
			}

			public LVITEM(int item)
			{
				iItem = item;
			}

			public int GroupId
			{
				get { return iGroupId; }
				set { iGroupId = value; EnumUtil.SetFlags(ref mask, ListViewItemMask.GroupId, true); }
			}

			public int ImageIndex
			{
				get { return iImage; }
				set { iImage = value; EnumUtil.SetFlags(ref mask, ListViewItemMask.Image, true); }
			}

			public int Indent
			{
				get { return iIndent; }
				set { iIndent = value; EnumUtil.SetFlags(ref mask, ListViewItemMask.Indent, true); }
			}

			public IntPtr LParam
			{
				get { return lParam; }
				set { lParam = value; EnumUtil.SetFlags(ref mask, ListViewItemMask.Param, true); }
			}

			public string Text
			{
				get { return InteropUtil.GetString(pszText); }
				set { InteropUtil.SetString(ref pszText, ref cchTextMax, value); EnumUtil.SetFlags(ref mask, ListViewItemMask.Text, true); }
			}

			public LVTILECOLUMNINFO[] TileColumns
			{
				get
				{
					var ret = new LVTILECOLUMNINFO[cColumns];
					var cols = new int[cColumns];
					var fmts = new int[cColumns];
					Marshal.Copy(puColumns, cols, 0, (int)cColumns);
					if (piColFmt != IntPtr.Zero)
						Marshal.Copy(piColFmt, fmts, 0, (int)cColumns);
					for (int i = 0; i < cColumns; i++)
						ret[i] = new LVTILECOLUMNINFO() { columnIndex = (uint)cols[i], format = (ListViewColumnFormat)fmts[i] };
					return ret;
				}
				set
				{
					if (value == null)
						throw new ArgumentNullException();
					cColumns = (uint)value.Length;
					if (value.Length > 0)
					{
						var cols = new int[cColumns];
						var fmts = new int[cColumns];
						bool hasFmts = false;
						for (int i = 0; i < cColumns; i++)
						{
							cols[i] = (int)value[i].columnIndex;
							fmts[i] = (int)value[i].format;
							if (fmts[i] != 0) hasFmts = true;
						}
						puColumns = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * (int)cColumns);
						Marshal.Copy(cols, 0, puColumns, (int)cColumns);
						EnumUtil.SetFlags(ref mask, ListViewItemMask.Columns, true);
						if (hasFmts)
						{
							piColFmt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * (int)cColumns);
							Marshal.Copy(fmts, 0, piColFmt, (int)cColumns);
							EnumUtil.SetFlags(ref mask, ListViewItemMask.ColFmt, true);
						}
					}
					else
					{
						puColumns = IntPtr.Zero;
						piColFmt = IntPtr.Zero;
						EnumUtil.SetFlags(ref mask, ListViewItemMask.ColFmt | ListViewItemMask.Columns, false);
					}
				}
			}

			public int[] VisibleTileColumns
			{
				get
				{
					var cols = new int[cColumns];
					Marshal.Copy(puColumns, cols, 0, (int)cColumns);
					return cols;
				}
				set
				{
					if (value == null)
						value = new int[0];
					cColumns = (uint)value.Length;
					if (value.Length > 0)
					{
						puColumns = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * (int)cColumns);
						Marshal.Copy(value, 0, puColumns, (int)cColumns);
						mask.SetFlags(ListViewItemMask.Columns, true);
					}
					else
					{
						puColumns = IntPtr.Zero;
						mask.SetFlags(ListViewItemMask.Columns, false);
					}
				}
			}

			public ListViewItemState GetState() => (ListViewItemState)(state & 0x000000FF);

			public bool GetState(ListViewItemState state) => ((ListViewItemState)this.state).IsFlagSet(state);

			public void SetState(ListViewItemState state, bool on = true)
			{
				mask |= ListViewItemMask.State;
				stateMask |= state;
				ListViewItemState tempState = GetState();
				EnumUtil.SetFlags(ref tempState, state, on);
				this.state = (uint)tempState | (this.state & 0xFFFFFF00);
			}

			public uint OverlayImageIndex
			{
				get { return (state & 0x00000F00) >> 8; }
				set
				{
					if (value > 15)
						throw new ArgumentOutOfRangeException("OverlayImageIndex", "Overlay image index must be between 0 and 15");
					mask |= ListViewItemMask.State;
					stateMask |= ListViewItemState.OverlayMask;
					state = (value << 8) | (state & 0xFFFFF0FF);
				}
			}

			public uint StateImageIndex
			{
				get { return (state & 0x0000F000) >> 12; }
				set
				{
					if (value > 15)
						throw new ArgumentOutOfRangeException("StateImageIndex", "State image index must be between 0 and 15");
					mask |= ListViewItemMask.State;
					stateMask |= ListViewItemState.StateImageMask;
					state = (value << 12) | (state & 0xFFFF0FFF);
				}
			}

			public override string ToString() => ("LVITEM: pszText = " + Text + ", iItem = " + iItem.ToString(CultureInfo.InvariantCulture) + ", iSubItem = " + iSubItem.ToString(CultureInfo.InvariantCulture) + ", state = " + state.ToString(CultureInfo.InvariantCulture) + ", iGroupId = " + iGroupId.ToString(CultureInfo.InvariantCulture) + ", cColumns = " + cColumns.ToString(CultureInfo.InvariantCulture));

			void IDisposable.Dispose()
			{
				InteropUtil.FreeString(ref pszText, ref cchTextMax);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public class LVTILEVIEWINFO
		{
			private uint cbSize = ((uint)Marshal.SizeOf(typeof(NativeMethods.LVTILEVIEWINFO)));
			private ListViewTileViewMask dwMask;
			private ListViewTileViewFlag dwFlags;
			private NativeMethods.SIZE sizeTile;
			private int cLines;
			private NativeMethods.RECT rcLabelMargin;

			public LVTILEVIEWINFO(ListViewTileViewMask mask)
			{
				dwMask = mask;
			}

			public bool AutoSize
			{
				get { return dwFlags.IsFlagSet(ListViewTileViewFlag.Autosize); }
				set { dwFlags = ListViewTileViewFlag.Autosize; dwMask |= ListViewTileViewMask.TileSize; sizeTile.height = sizeTile.width = 0; }
			}

			public System.Drawing.Size TileSize
			{
				get { return sizeTile; }
				set { sizeTile = value; dwMask |= ListViewTileViewMask.TileSize; dwFlags |= ListViewTileViewFlag.FixedSize; }
			}

			public int TileHeight
			{
				get { return sizeTile.height; }
				set { sizeTile.height = value; dwMask |= ListViewTileViewMask.TileSize; dwFlags |= ListViewTileViewFlag.FixedHeight; }
			}

			public int TileWidth
			{
				get { return sizeTile.width; }
				set { sizeTile.width = value; dwMask |= ListViewTileViewMask.TileSize; dwFlags |= ListViewTileViewFlag.FixedWidth; }
			}

			public int MaxTextLines
			{
				get { return cLines; }
				set { cLines = value; dwMask |= ListViewTileViewMask.Columns; }
			}

			public System.Windows.Forms.Padding TilePadding
			{
				get { return new Padding(rcLabelMargin.Left, rcLabelMargin.Top, rcLabelMargin.Right, rcLabelMargin.Bottom); }
				set { rcLabelMargin = new RECT(value.Left, value.Top, value.Right, value.Bottom); dwMask |= ListViewTileViewMask.LabelMargin; }
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NMLISTVIEW
		{
			public NativeMethods.NMHDR hdr;
			public int iItem;
			public int iSubItem;
			public int uNewState;
			public int uOldState;
			public int uChanged;
			public System.Drawing.Point ptAction;
			public IntPtr lParam;
		}
	}
}