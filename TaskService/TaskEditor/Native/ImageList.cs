using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[DllImport("comctl32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal extern static bool ImageList_SetOverlayImage(IntPtr himl, int iImage, int iOverlay);
	}
}
