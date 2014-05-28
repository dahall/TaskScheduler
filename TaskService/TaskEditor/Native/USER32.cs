using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		internal const string USER32 = "user32.dll";

		[DllImport(USER32, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		[DllImport(USER32, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, System.Drawing.Point pt, System.Windows.Forms.GetChildAtPointSkip uFlags);

		[DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);

		[DllImport(USER32, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool InvalidateRect(IntPtr hWnd, [In] ref NativeMethods.RECT rect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

		[DllImport(USER32, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

		[DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ScreenToClient(IntPtr hWnd, [In, Out] ref System.Drawing.Point lpPoint);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref RECT rect);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpString);

		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS
		{
			public IntPtr hwnd;
			public IntPtr hwndInsertAfter;
			public int x;
			public int y;
			public int cx;
			public int cy;
			public int flags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
			public IntPtr hwndFrom;
			public IntPtr idFrom;
			public int code;
		}
	}
}
