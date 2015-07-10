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
				this.width = w; this.height = h;
			}

			public Size ToSize()
			{
				return this;
			}

			public static implicit operator Size(SIZE s)
			{
				return new Size(s.width, s.height);
			}

			public static implicit operator SIZE(Size s)
			{
				return new SIZE(s.Width, s.Height);
			}
		}
	}
}
