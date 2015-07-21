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

		public static IntPtr GetWindowLong(IntPtr hWnd, int nIndex)
		{
			if (IntPtr.Size == 4)
				return GetWindowLong32(hWnd, nIndex);
			return GetWindowLongPtr64(hWnd, nIndex);
		}

		[DllImport(USER32, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
		public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

		[DllImport(USER32, EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
		public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

		[DllImport(USER32, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool InvalidateRect(IntPtr hWnd, [In] ref NativeMethods.RECT rect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

		[DllImport(USER32, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool InvalidateRect(IntPtr hWnd, IntPtr rect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

		[DllImport(USER32, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int LoadString(IntPtr hInstance, int uID, out IntPtr lpBuffer, int nBufferMax);

		[DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ScreenToClient(IntPtr hWnd, [In, Out] ref System.Drawing.Point lpPoint);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, IntPtr lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		public static IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam = 0) => SendMessage(hWnd, Msg, wParam, 0);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref RECT rect);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [In, MarshalAs(UnmanagedType.LPWStr)] string lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, ref int wParam, [In, Out, MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder lParam);

		public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
		{
			if (IntPtr.Size == 4)
				return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}

		[DllImport(USER32, EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
		private static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport(USER32, EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
		private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

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
