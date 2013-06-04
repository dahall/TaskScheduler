using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		const string GDI32 = "gdi32.dll";

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);

		[DllImport(GDI32, ExactSpelling = true, SetLastError = true)]
		public static extern uint SetLayout(IntPtr hdc, uint dwLayout);
	}
}