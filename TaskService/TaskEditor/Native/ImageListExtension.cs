using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	internal static class ImageListExtension
	{
		private static Dictionary<ImageList, List<int>> imageListOverlays = new Dictionary<ImageList, List<int>>();

		/// <summary>
		/// Assigns the image at the specified index as an overlay and returns is overlay index.
		/// </summary>
		/// <param name="imageList">The image list.</param>
		/// <param name="imageIndex">Index of the image within the ImageList.</param>
		/// <returns>The 1 based index of the overlay.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">The imageIndex is not in the current list.</exception>
		/// <exception cref="System.ComponentModel.Win32Exception">The image cannot be added as an overlay.</exception>
		public static int SetImageIndexAsOverlay(this ImageList imageList, int imageIndex)
		{
			if (imageIndex < 0 || imageIndex >= imageList.Images.Count)
				throw new ArgumentOutOfRangeException("imageIndex");
			List<int> vals;
			if (!imageListOverlays.TryGetValue(imageList, out vals))
			{
				imageList.RecreateHandle += imageList_RecreateHandle;
				imageListOverlays.Add(imageList, vals = new List<int>(15));
			}
			vals.Add(imageIndex);
			int overlayIndex = vals.Count;
			if (!Microsoft.Win32.NativeMethods.ImageList_SetOverlayImage(imageList.Handle, imageIndex, overlayIndex))
				throw new Win32Exception();
			return overlayIndex;
		}

		/// <summary>
		/// Adds the specified image to the ImageList as an overlay, using the specified color to determine transparency.
		/// </summary>
		/// <param name="imageList">The image list.</param>
		/// <param name="value">A Bitmap of the image to add to the list.</param>
		/// <param name="transparentColor">The color to use as the transparent color within the Bitmap.</param>
		/// <returns>The 1 based index of the overlay.</returns>
		/// <exception cref="System.ComponentModel.Win32Exception">The image cannot be added as an overlay.</exception>
		public static int AddOverlay(this ImageList imageList, Image value, Color transparentColor)
		{
			int idx = imageList.Images.Add(value, transparentColor);
			return SetImageIndexAsOverlay(imageList, idx);
		}

		private static void imageList_RecreateHandle(object sender, EventArgs e)
		{
			List<int> vals;
			if (imageListOverlays.TryGetValue(sender as ImageList, out vals))
			{
				for (int i = 0; i < vals.Count; i++)
					Microsoft.Win32.NativeMethods.ImageList_SetOverlayImage(((ImageList)sender).Handle, vals[i], i + 1);
			}
		}
	}
}