using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal class GenericSafeHandle : SafeHandle
	{
		private HandleCloser closeMethod;

		public delegate bool HandleCloser(IntPtr ptr);

		public GenericSafeHandle(IntPtr ptr, HandleCloser closeMethod, bool ownsHandle = true)
			: base(ptr, ownsHandle)
		{
			if (closeMethod == null)
				throw new ArgumentNullException(nameof(closeMethod));
			this.closeMethod = closeMethod;
		}

		public override bool IsInvalid => base.handle == IntPtr.Zero;

		public static implicit operator IntPtr(GenericSafeHandle h) => h.DangerousGetHandle();

		protected override bool ReleaseHandle()
		{
			if (!IsInvalid)
				return closeMethod(base.handle);
			return true;
		}
	}
}