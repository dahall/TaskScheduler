using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
#if SAFEDCHANDLE_REQUIRED
	internal static partial class NativeMethods
	{
		public class SafeDCObjectHandle : SafeHandle
		{
			public SafeDCObjectHandle(IntPtr hdc, IntPtr hObj)
				: base(IntPtr.Zero, true)
			{
				if (hdc != null)
				{
					NativeMethods.SelectObject(hdc, hObj);
					base.SetHandle(hObj);
				}
			}

			public override bool IsInvalid
			{
				get { return base.handle == IntPtr.Zero; }
			}

			public static implicit operator IntPtr(SafeDCObjectHandle h)
			{
				return h.DangerousGetHandle();
			}

			protected override bool ReleaseHandle()
			{
				if (!IsInvalid)
					NativeMethods.DeleteObject(base.handle);
				return true;
			}
		}

		public class SafeCompatibleDCHandle : SafeHandle
		{
			public SafeCompatibleDCHandle(IntPtr hdc)
				: base(IntPtr.Zero, true)
			{
				if (hdc != null)
				{
					base.SetHandle(NativeMethods.CreateCompatibleDC(hdc));
				}
			}

			public override bool IsInvalid
			{
				get { return base.handle == IntPtr.Zero; }
			}

			public static implicit operator IntPtr(SafeCompatibleDCHandle h)
			{
				return h.DangerousGetHandle();
			}

			protected override bool ReleaseHandle()
			{
				if (!IsInvalid)
					NativeMethods.DeleteDC(base.handle);
				return true;
			}
		}
	}
#endif

	internal class SafeGDIHandle : SafeHandle
	{
		private IDeviceContext idc;

		public SafeGDIHandle(IDeviceContext dc)
			: base(IntPtr.Zero, true)
		{
			if (dc != null)
			{
				idc = dc;
				base.SetHandle(idc.GetHdc());
			}
		}

		public override bool IsInvalid => base.handle == IntPtr.Zero;

		public static implicit operator IntPtr(SafeGDIHandle h) => h.DangerousGetHandle();

		protected override bool ReleaseHandle()
		{
			if (idc != null)
				idc.ReleaseHdc();
			return true;
		}
	}
}
