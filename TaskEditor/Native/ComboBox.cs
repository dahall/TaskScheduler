using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

		[StructLayout(LayoutKind.Sequential)]
		public struct COMBOBOXINFO
		{
			private Int32 cbSize;
			public RECT rcItem;
			public RECT rcButton;
			public ComboBoxButtonState buttonState;
			public IntPtr hwndCombo;
			public IntPtr hwndEdit;
			public IntPtr hwndList;

			public static COMBOBOXINFO FromComboBox(System.Windows.Forms.ComboBox cb)
			{
				if (!cb.IsHandleCreated)
					throw new ArgumentException("ComboBox must have its handle created.", "cb");

				var cbi = new COMBOBOXINFO() { cbSize = Marshal.SizeOf(typeof(COMBOBOXINFO)) };
				GetComboBoxInfo(cb.Handle, ref cbi);
				return cbi;
			}

			public bool Invisible
			{
				get { return (buttonState & ComboBoxButtonState.Invisible) == ComboBoxButtonState.Invisible; }
			}

			public bool Pressed
			{
				get { return (buttonState & ComboBoxButtonState.Pressed) == ComboBoxButtonState.Pressed; }
			}

			public System.Drawing.Rectangle ItemRectangle { get { return rcItem; } }

			public System.Drawing.Rectangle ButtonRectangle { get { return rcButton; } }
		}

		public enum ComboBoxButtonState
		{
			None = 0,
			Invisible = 0x00008000,
			Pressed = 0x00000008
		}
	}
}
