using System.Drawing;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct SIZE
		{
			public int width;
			public int height;

			public SIZE(int w, int h)
			{
				width = w; height = h;
			}

			public Size ToSize() => this;

			public static implicit operator Size(SIZE s) => new Size(s.width, s.height);

			public static implicit operator SIZE(Size s) => new SIZE(s.Width, s.Height);
		}
	}
}
