// Requires Gdi\RECT.cs
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		private const string UXTHEME = "uxtheme.dll";

		[Flags]
		public enum DrawThemeTextOptionsFlags : int
		{
			TextColor = 1,
			BorderColor = 2,
			ShadowColor = 4,
			ShadowType = 8,
			ShadowOffset = 16,
			BorderSize = 32,
			FontProp = 64,
			ColorProp = 128,
			StateId = 256,
			CalcRect = 512,
			ApplyOverlay = 1024,
			GlowSize = 2048,
			Callback = 4096,
			Composited = 8192
		}

		public enum DrawThemeTextSystemFonts
		{
			Caption = 801,
			SmallCaption = 802,
			Menu = 803,
			Status = 804,
			MessageBox = 805,
			IconTitle = 806
		}

		public enum IntegerListProperty
		{
			TransitionDuration = 6000
		}

		public enum TextShadowType : int
		{
			None = 0,
			Single = 1,
			Continuous = 2
		}

		public enum WindowThemeAttributeType
		{
			WTA_NONCLIENT = 1,
		}

		[Flags]
		public enum WindowThemeNonClientAttributes : uint
		{
			/// <summary>Do Not Draw The Caption (Text)</summary>
			NoDrawCaption = 0x00000001,
			/// <summary>Do Not Draw the Icon</summary>
			NoDrawIcon = 0x00000002,
			/// <summary>Do Not Show the System Menu</summary>
			NoSysMenu = 0x00000004,
			/// <summary>Do Not Mirror the Question mark Symbol</summary>
			NoMirrorHelp = 0x00000008
		}

		[DllImport(UXTHEME)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref NativeMethods.RECT pRect, ref NativeMethods.RECT pClipRect);

		[DllImport(UXTHEME)]
		public static extern int DrawThemeIcon(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, ref NativeMethods.RECT pRect, IntPtr himl, int iImageIndex);

		[DllImport(UXTHEME, CharSet = CharSet.Unicode)]
		public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string text, int iCharCount, int dwFlags, ref NativeMethods.RECT pRect, ref DrawThemeTextOptions pOptions);

		[DllImport(UXTHEME, ExactSpelling = true)]
		public static extern int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr prc, out NativeMethods.RECT pMargins);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void GetThemeTransitionDuration(IntPtr hTheme, int iPartId, int iStateIdFrom, int iStateIdTo, int iPropId, ref UInt32 pdwDuration);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false, CharSet = CharSet.Unicode)]
		public static extern void SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, ref WTA_OPTIONS attributes, int size);

		[StructLayout(LayoutKind.Sequential)]
		public struct DrawThemeTextOptions
		{
			private int dwSize;
			private DrawThemeTextOptionsFlags dwFlags;
			private int crText;
			private int crBorder;
			private int crShadow;
			private TextShadowType iTextShadowType;
			private System.Drawing.Point ptShadowOffset;
			private int iBorderSize;
			private int iFontPropId;
			private int iColorPropId;
			private int iStateId;
			private bool fApplyOverlay;
			private int iGlowSize;
			private int pfnDrawTextCallback;
			private IntPtr lParam;

			public DrawThemeTextOptions(bool init)
				: this()
			{
				dwSize = Marshal.SizeOf(typeof(DrawThemeTextOptions));
			}

			public Color AlternateColor
			{
				get { return ColorTranslator.FromWin32(iColorPropId); }
				set
				{
					iColorPropId = ColorTranslator.ToWin32(value);
					dwFlags |= DrawThemeTextOptionsFlags.ColorProp;
				}
			}

			public DrawThemeTextSystemFonts AlternateFont
			{
				get { return (DrawThemeTextSystemFonts)iFontPropId; }
				set
				{
					iFontPropId = (int)value;
					dwFlags |= DrawThemeTextOptionsFlags.FontProp;
				}
			}

			public bool AntiAliasedAlpha
			{
				get { return (dwFlags & DrawThemeTextOptionsFlags.Composited) == DrawThemeTextOptionsFlags.Composited; }
				set
				{
					if (value)
						dwFlags |= DrawThemeTextOptionsFlags.Composited;
					else
						dwFlags &= ~DrawThemeTextOptionsFlags.Composited;
				}
			}

			public bool ApplyOverlay
			{
				get { return fApplyOverlay; }
				set
				{
					fApplyOverlay = value;
					dwFlags |= DrawThemeTextOptionsFlags.ApplyOverlay;
				}
			}

			public Color BorderColor
			{
				get { return ColorTranslator.FromWin32(crBorder); }
				set
				{
					crBorder = ColorTranslator.ToWin32(value);
					dwFlags |= DrawThemeTextOptionsFlags.BorderColor;
				}
			}

			public int BorderSize
			{
				get { return iBorderSize; }
				set
				{
					iBorderSize = value;
					dwFlags |= DrawThemeTextOptionsFlags.BorderSize;
				}
			}

			public int GlowSize
			{
				get { return iGlowSize; }
				set
				{
					iGlowSize = value;
					dwFlags |= DrawThemeTextOptionsFlags.GlowSize;
				}
			}

			public bool ReturnCalculatedRectangle
			{
				get { return (dwFlags & DrawThemeTextOptionsFlags.CalcRect) == DrawThemeTextOptionsFlags.CalcRect; }
				set
				{
					if (value)
						dwFlags |= DrawThemeTextOptionsFlags.CalcRect;
					else
						dwFlags &= ~DrawThemeTextOptionsFlags.CalcRect;
				}
			}

			public Color ShadowColor
			{
				get { return ColorTranslator.FromWin32(crShadow); }
				set
				{
					crShadow = ColorTranslator.ToWin32(value);
					dwFlags |= DrawThemeTextOptionsFlags.ShadowColor;
				}
			}

			public Point ShadowOffset
			{
				get { return new Point(ptShadowOffset.X, ptShadowOffset.Y); }
				set
				{
					ptShadowOffset = value;
					dwFlags |= DrawThemeTextOptionsFlags.ShadowOffset;
				}
			}

			public TextShadowType ShadowType
			{
				get { return iTextShadowType; }
				set
				{
					iTextShadowType = value;
					dwFlags |= DrawThemeTextOptionsFlags.ShadowType;
				}
			}

			public Color TextColor
			{
				get { return ColorTranslator.FromWin32(crText); }
				set
				{
					crText = ColorTranslator.ToWin32(value);
					dwFlags |= DrawThemeTextOptionsFlags.TextColor;
				}
			}
		}

		/// <summary>
		/// The Options of What Attributes to Add/Remove
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct WTA_OPTIONS
		{
			public WindowThemeNonClientAttributes Flags;
			public uint Mask;
		}
	}
}