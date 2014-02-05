// Requires UxTheme\UXTHEME.cs, Gdi\SafeHandles.cs
using Microsoft.Win32;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.VisualStyles
{
	internal static class VisualStyleRendererExtension
	{
		public static void DrawText(this VisualStyleRenderer rnd, IDeviceContext dc, ref Rectangle bounds, string text, System.Windows.Forms.TextFormatFlags flags, NativeMethods.DrawThemeTextOptions options)
		{
			NativeMethods.RECT rc = new NativeMethods.RECT(bounds);
			using (SafeGDIHandle hdc = new SafeGDIHandle(dc))
				NativeMethods.DrawThemeTextEx(rnd.Handle, hdc, rnd.Part, rnd.State, text, text.Length, (int)flags, ref rc, ref options);
			bounds = rc;
		}

		public enum IntegerListProperty
		{
			TransitionDuration = 6000
		}

		public static int[] GetIntegerList(this VisualStyleRenderer rnd, IntegerListProperty prop)
		{
			return NativeMethods.GetThemeIntList(rnd.Handle, rnd.Part, rnd.State, (int)prop);
		}

		public static System.Windows.Forms.Padding GetMargins2(this VisualStyleRenderer rnd, IDeviceContext dc, MarginProperty prop)
		{
			NativeMethods.RECT rc;
			using (SafeGDIHandle hdc = new SafeGDIHandle(dc))
				NativeMethods.GetThemeMargins(rnd.Handle, hdc, rnd.Part, rnd.State, (int)prop, IntPtr.Zero, out rc);
			return new System.Windows.Forms.Padding(rc.Left, rc.Top, rc.Right, rc.Bottom);
		}

		public static NativeMethods.ThemePropertyOrigin GetPropertyOrigin(this VisualStyleRenderer rnd, int propId)
		{
			NativeMethods.ThemePropertyOrigin ret;
			NativeMethods.GetThemePropertyOrigin(rnd.Handle, rnd.Part, rnd.State, (int)propId, out ret);
			return ret;
		}

		public static System.UInt32 GetTransitionDuration(this VisualStyleRenderer rnd, int toState)
		{
			System.UInt32 dwDuration = 0;
			NativeMethods.GetThemeTransitionDuration(rnd.Handle, rnd.Part, rnd.State, toState, (int)IntegerListProperty.TransitionDuration, ref dwDuration);
			return dwDuration;
		}

		/// <summary>
		/// Sets attributes to control how visual styles are applied to a specified window.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <param name="attr">The attributes to apply or disable.</param>
		/// <param name="enable">if set to <c>true</c> enable the attribute, otherwise disable it.</param>
		public static void SetWindowThemeAttribute(this IWin32Window window, NativeMethods.WindowThemeNonClientAttributes attr, bool enable = true)
		{
			NativeMethods.WTA_OPTIONS ops = new NativeMethods.WTA_OPTIONS();
			ops.Flags = attr;
			ops.Mask = enable ? (uint)attr : 0;
			try { NativeMethods.SetWindowThemeAttribute(window.Handle, NativeMethods.WindowThemeAttributeType.WTA_NONCLIENT, ref ops, Marshal.SizeOf(ops)); }
			catch (EntryPointNotFoundException) { }
			catch { throw; }
		}

#if BITMAPINFO
		private delegate void DrawWrapperMethod(IntPtr hdc);

		public static void DrawGlassBackground(this VisualStyleRenderer rnd, IDeviceContext dc, Rectangle bounds, Rectangle clipRectangle, bool rightToLeft = false)
		{
			DrawWrapper(rnd, dc, bounds,
				delegate(IntPtr memoryHdc)
				{
					NativeMethods.RECT rBounds = new NativeMethods.RECT(bounds);
					NativeMethods.RECT rClip = new NativeMethods.RECT(clipRectangle);
					// Draw background
					if (rightToLeft) NativeMethods.SetLayout(memoryHdc, 1);
					NativeMethods.DrawThemeBackground(rnd.Handle, memoryHdc, rnd.Part, rnd.State, ref rBounds, ref rClip);
					NativeMethods.SetLayout(memoryHdc, 0);
				}
			);
		}

		public static void DrawGlassIcon(this VisualStyleRenderer rnd, Graphics g, Rectangle bounds, ImageList imgList, int imgIndex)
		{
			DrawWrapper(rnd, g, bounds,
				delegate(IntPtr memoryHdc)
				{
					NativeMethods.RECT rBounds = new NativeMethods.RECT(bounds);
					NativeMethods.DrawThemeIcon(rnd.Handle, memoryHdc, rnd.Part, rnd.State, ref rBounds, imgList.Handle, imgIndex);
				}
			);
		}

		public static void DrawGlassImage(this VisualStyleRenderer rnd, Graphics g, Rectangle bounds, Image img, bool disabled = false)
		{
			DrawWrapper(rnd, g, bounds,
				delegate(IntPtr memoryHdc)
				{
					using (Graphics mg = Graphics.FromHdc(memoryHdc))
					{
						if (disabled)
							ControlPaint.DrawImageDisabled(mg, img, bounds.X, bounds.Y, Color.Transparent);
						else
							mg.DrawImage(img, bounds);
					}
				}
			);
		}

		public static void DrawGlowingText(this VisualStyleRenderer rnd, IDeviceContext dc, Rectangle bounds, string text, Font font, Color color, System.Windows.Forms.TextFormatFlags flags)
		{
			DrawWrapper(rnd, dc, bounds,
				delegate(IntPtr memoryHdc) {
					// Create and select font
					using (NativeMethods.SafeDCObjectHandle fontHandle = new NativeMethods.SafeDCObjectHandle(memoryHdc, font.ToHfont()))
					{
						// Draw glowing text
						NativeMethods.DrawThemeTextOptions dttOpts = new NativeMethods.DrawThemeTextOptions(true);
						dttOpts.TextColor = color;
						dttOpts.GlowSize = 10;
						dttOpts.AntiAliasedAlpha = true;
						NativeMethods.RECT textBounds = new NativeMethods.RECT(4, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
						NativeMethods.DrawThemeTextEx(rnd.Handle, memoryHdc, rnd.Part, rnd.State, text, text.Length, (int)flags, ref textBounds, ref dttOpts);
					}
				}
			);
		}

		/*public static void DrawGlowingText(this VisualStyleRenderer rnd, IDeviceContext dc, Rectangle bounds, string text, Font font, Color color, System.Windows.Forms.TextFormatFlags flags)
		{
			using (SafeGDIHandle primaryHdc = new SafeGDIHandle(dc))
			{
				// Create a memory DC so we can work offscreen
				using (SafeCompatibleDCHandle memoryHdc = new SafeCompatibleDCHandle(primaryHdc))
				{
					// Create a device-independent bitmap and select it into our DC
					BITMAPINFO info = new BITMAPINFO(bounds.Width, -bounds.Height);
					using (SafeDCObjectHandle dib = new SafeDCObjectHandle(memoryHdc, GDI.CreateDIBSection(primaryHdc, ref info, 0, 0, IntPtr.Zero, 0)))
					{
						// Create and select font
						using (SafeDCObjectHandle fontHandle = new SafeDCObjectHandle(memoryHdc, font.ToHfont()))
						{
							// Draw glowing text
							DrawThemeTextOptions dttOpts = new DrawThemeTextOptions(true);
							dttOpts.TextColor = color;
							dttOpts.GlowSize = 10;
							dttOpts.AntiAliasedAlpha = true;
							NativeMethods.RECT textBounds = new NativeMethods.RECT(4, 0, bounds.Right - bounds.Left, bounds.Bottom - bounds.Top);
							DrawThemeTextEx(rnd.Handle, memoryHdc, rnd.Part, rnd.State, text, text.Length, (int)flags, ref textBounds, ref dttOpts);

							// Copy to foreground
							const int SRCCOPY = 0x00CC0020;
							GDI.BitBlt(primaryHdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, SRCCOPY);
						}
					}
				}
			}
		}*/

		private static void DrawWrapper(VisualStyleRenderer rnd, IDeviceContext dc, Rectangle bounds, DrawWrapperMethod func)
		{
			using (SafeGDIHandle primaryHdc = new SafeGDIHandle(dc))
			{
				// Create a memory DC so we can work offscreen
				using (NativeMethods.SafeCompatibleDCHandle memoryHdc = new NativeMethods.SafeCompatibleDCHandle(primaryHdc))
				{
					// Create a device-independent bitmap and select it into our DC
					NativeMethods.BITMAPINFO info = new NativeMethods.BITMAPINFO(bounds.Width, -bounds.Height);
					using (NativeMethods.SafeDCObjectHandle dib = new NativeMethods.SafeDCObjectHandle(memoryHdc, NativeMethods.CreateDIBSection(primaryHdc, ref info, 0, IntPtr.Zero, IntPtr.Zero, 0)))
					{
						// Call method
						func(memoryHdc);

						// Copy to foreground
						const int SRCCOPY = 0x00CC0020;
						NativeMethods.BitBlt(primaryHdc, bounds.Left, bounds.Top, bounds.Width, bounds.Height, memoryHdc, 0, 0, SRCCOPY);
					}
				}
			}
		}
#endif
	}
}