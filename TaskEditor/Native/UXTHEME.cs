// Requires Gdi\Rect.cs

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		private const string UXTHEME = "uxtheme.dll";

		[Flags]
		public enum BufferedPaintAnimationStyle
		{
			None = 0,
			Linear = 1,
			Cubic = 2,
			Sine = 3
		}

		public enum BufferedPaintBufferFormat
		{
			CompatibleBitmap,
			Dib,
			TopDownDib,
			TopDownMonoDib
		}

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

		public enum ThemePropertyOrigin
		{
			State = 0,
			Part = 1,
			Class = 2,
			Global = 3,
			NotFound = 4
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
		public static extern IntPtr BeginBufferedAnimation(IntPtr hwnd, IntPtr hdcTarget,
			ref Rectangle rcTarget, BufferedPaintBufferFormat dwFormat, IntPtr pPaintParams,
			ref BufferedPaintAnimationParams pAnimationParams, out IntPtr phdcFrom, out IntPtr phdcTo);

		[DllImport(UXTHEME)]
		public static extern IntPtr BufferedPaintInit();

		[DllImport(UXTHEME)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BufferedPaintRenderAnimation(IntPtr hwnd, IntPtr hdcTarget);

		[DllImport(UXTHEME)]
		public static extern IntPtr BufferedPaintStopAllAnimations(IntPtr hwnd);

		[DllImport(UXTHEME)]
		public static extern IntPtr BufferedPaintUnInit();

		[DllImport(UXTHEME)]
		public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, 
			ref NativeMethods.RECT pRect, ref NativeMethods.RECT pClipRect);

		[DllImport(UXTHEME)]
		public static extern int DrawThemeIcon(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, 
			ref NativeMethods.RECT pRect, IntPtr himl, int iImageIndex);

		[DllImport(UXTHEME, CharSet = CharSet.Unicode)]
		public static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, 
			string text, int iCharCount, int dwFlags, ref NativeMethods.RECT pRect, ref DrawThemeTextOptions pOptions);

		[DllImport(UXTHEME)]
		public static extern IntPtr EndBufferedAnimation(IntPtr hbpAnimation, bool fUpdateTarget);

		public static int[] GetThemeIntList(IntPtr hTheme, int iPartId, int iStateId, int iPropId)
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				INTLIST_OLD l;
				int r = GetThemeIntListPreVista(hTheme, iPartId, iStateId, iPropId, out l);
				if (r != 0)
					return null;
				int[] outlist = new int[l.iValueCount];
				for (int i = 0; i < l.iValueCount; i++)
					outlist[i] = l.iValues[i];
				return outlist;
			}
			else
			{
				INTLIST l;
				int r = GetThemeIntList(hTheme, iPartId, iStateId, iPropId, out l);
				if (r != 0)
					return null;
				int[] outlist = new int[l.iValueCount];
				for (int i = 0; i < l.iValueCount; i++)
					outlist[i] = l.iValues[i];
				return outlist;
			}
		}

		[DllImport(UXTHEME, ExactSpelling = true)]
		static extern Int32 GetThemeIntList(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out INTLIST pIntList);

		[DllImport(UXTHEME, EntryPoint = "GetThemeIntList")]
		static extern Int32 GetThemeIntListPreVista(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out INTLIST_OLD pIntList);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, 
			int iPropId, IntPtr prc, out NativeMethods.RECT pMargins);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void GetThemePropertyOrigin(IntPtr hTheme, int iPartId, int iStateId, 
			int iPropId, out ThemePropertyOrigin pOrigin);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void GetThemeTransitionDuration(IntPtr hTheme, int iPartId, int iStateIdFrom, int iStateIdTo,
			int iPropId, ref UInt32 pdwDuration);

		[DllImport(UXTHEME, ExactSpelling = true, PreserveSig = false)]
		public static extern void SetWindowThemeAttribute(IntPtr hWnd, WindowThemeAttributeType wtype, 
			ref WTA_OPTIONS attributes, int size);

		[StructLayout(LayoutKind.Sequential)]
		public struct BufferedPaintAnimationParams
		{
			private Int32 cbSize, dwFlags;
			private BufferedPaintAnimationStyle style;
			private UInt32 dwDuration;

			public BufferedPaintAnimationParams(BufferedPaintAnimationStyle animStyle = BufferedPaintAnimationStyle.Linear, UInt32 dur = 0)
			{
				cbSize = Marshal.SizeOf(typeof(BufferedPaintAnimationParams));
				dwFlags = 0;
				dwDuration = dur;
				style = animStyle;
			}

			public BufferedPaintAnimationStyle AnimationStyle
			{
				get { return style; }
				set { style = value; }
			}

			public UInt32 Duration
			{
				get { return dwDuration; }
				set { dwDuration = value; }
			}
		}

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

		[StructLayout(LayoutKind.Sequential)]
		struct INTLIST
		{
			public int iValueCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 402)]
			public int[] iValues;
		}

		[StructLayout(LayoutKind.Sequential)]
		struct INTLIST_OLD
		{
			public int iValueCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public int[] iValues;
		}
	}
}