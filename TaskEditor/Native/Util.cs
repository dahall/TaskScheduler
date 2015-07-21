using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public static class Util
		{
			// Methods
			private static int GetEmbeddedNullStringLengthAnsi(string s)
			{
				int index = s.IndexOf('\0');
				if (index > -1)
				{
					string str = s.Substring(0, index);
					string str2 = s.Substring(index + 1);
					return ((GetPInvokeStringLength(str) + GetEmbeddedNullStringLengthAnsi(str2)) + 1);
				}
				return GetPInvokeStringLength(s);
			}

			public static int GetPInvokeStringLength(string s)
			{
				if (string.IsNullOrEmpty(s))
					return 0;
				if (Marshal.SystemDefaultCharSize == 2)
					return s.Length;
				if (s.IndexOf('\0') > -1)
					return GetEmbeddedNullStringLengthAnsi(s);
				return lstrlen(s);
			}

			public static int HIWORD(int n) => ((n >> 0x10) & 0xffff);

			public static int HIWORD(IntPtr n) => HIWORD((int)((long)n));

			public static int LOWORD(int n) => (n & 0xffff);

			public static int LOWORD(IntPtr n) => LOWORD((int)((long)n));

			[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
			private static extern int lstrlen(string s);

			public static int MAKELONG(int low, int high) => ((high << 0x10) | (low & 0xffff));

			public static IntPtr MAKELPARAM(int low, int high) => (IntPtr)((high << 0x10) | (low & 0xffff));

			public static int SignedHIWORD(int n) => (short)((n >> 0x10) & 0xffff);

			public static int SignedHIWORD(IntPtr n) => SignedHIWORD((int)((long)n));

			public static int SignedLOWORD(int n) => (short)(n & 0xffff);

			public static int SignedLOWORD(IntPtr n) => SignedLOWORD((int)((long)n));
		}
	}
}
