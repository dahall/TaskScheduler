using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		internal const string USER32 = "user32.dll";

		[DllImport(USER32, CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		[DllImport(USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClientRect(IntPtr hWnd, [In, Out] ref NativeMethods.RECT rect);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		[DllImport(USER32, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpString);
	}
}
