using System.Drawing;
using System.Text;
using static Vanara.PInvoke.ComCtl32;
using static Vanara.PInvoke.User32_Gdi;

namespace System.Windows.Forms
{
	internal static class ListViewExtensions
	{
		public static void AdjustColumnToFill(this ListView lvw, int columnIndex = -1)
		{
			var nWidth = lvw.ClientSize.Width; // Get width of client area.
			var idx = columnIndex == -1 ? lvw.Columns.Count - 1 : columnIndex;

			// Loop through all columns except the last one.
			for (var i = 0; i < lvw.Columns.Count; i++)
			{
				// Subtract width of the column from the width of the client area.
				if (i != idx)
					nWidth -= lvw.Columns[i].Width;

				// If the width goes below 1, then no need to keep going because the last column can't be sized to fit due to the widths of
				// the columns before it.
				if (nWidth < 1)
					break;
			}

			// If there is any width remaining, that will be the width of the last column.
			if (nWidth > 0)
				lvw.Columns[idx].Width = nWidth;
		}

		public static void AdjustTileToWidth(this ListView lvw, int maxLines = 1, int iconSpacing = 4)
		{
			const string str = "Wg";
			var lvTVInfo = new LVTILEVIEWINFO(0) { TilePadding = new Vanara.PInvoke.RECT(iconSpacing, 0, 0, 0), MaxTextLines = maxLines };
			var sb = new StringBuilder(str);
			for (var i = 0; i < maxLines; i++)
				sb.Append("\r" + str);
			using (var g = lvw.CreateGraphics())
				lvTVInfo.TileSize = new Size(lvw.ClientSize.Width, Math.Max(lvw.LargeImageList.ImageSize.Height, TextRenderer.MeasureText(g, sb.ToString(), lvw.Font).Height));
			SendMessage(lvw.Handle, ListViewMessage.LVM_SETTILEVIEWINFO, 0, ref lvTVInfo);
			//var lvTVInfo = new LVTILEVIEWINFO(0) { TileWidth = lvw.ClientSize.Width };
			//SendMessage(lvw.Handle, ListViewMessage.SetTileViewInfo, 0, lvTVInfo);
			//SendMessage(lvw.Handle, (uint)ListViewMessage.SetExtendedListViewStyle, new IntPtr(0x200000), new IntPtr(0x200000));
		}
	}
}