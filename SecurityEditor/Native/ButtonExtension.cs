using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	internal static class ButtonExtension
	{
		public static void SetElevationRequiredState(this ButtonBase btn, bool required = true)
		{
			if (System.Environment.OSVersion.Version.Major >= 6)
			{
				const uint BCM_SETSHIELD = 0x160C;    //Elevated button
				btn.FlatStyle = FlatStyle.System;
				SendMessage(btn.Handle, BCM_SETSHIELD, IntPtr.Zero, required ? new IntPtr(0xFFFFFFFF) : IntPtr.Zero);
				btn.Invalidate();
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	}
}