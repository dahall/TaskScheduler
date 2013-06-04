using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		const string OLE32 = "Ole32.dll";

		/// <summary>
		/// Enumeration used when initializing the COM library
		/// </summary>
		public enum COINIT
		{
			MultiThreaded = 0x0,
			ApartmentThreaded = 0x2,
			DisableOLE1DDE = 0x4,
			SpeedOverMemory = 0x8
		}

		[DllImport(OLE32, ExactSpelling = true, SetLastError = false)]
		public static extern void OleInitialize(IntPtr pvReserved);

		[DllImport(OLE32, ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = false, PreserveSig = false)]
		public static extern void CoInitializeEx(IntPtr pvReserved, COINIT coInit);
		
		[DllImport(OLE32)]
		public static extern void ReleaseStgMedium([In] ref STGMEDIUM pMedium);

		[StructLayout(LayoutKind.Sequential)]
		public struct STGMEDIUM
		{
			[MarshalAs(UnmanagedType.U4)]
			public int tymed;
			public IntPtr data;
			[MarshalAs(UnmanagedType.IUnknown)]
			public object pUnkForRelease;
		}
	}
}
