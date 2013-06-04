using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		const string KERNEL32 = "Kernel32.dll";

		[DllImport(KERNEL32, CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);

		/// <summary>
		/// The GlobalLock function locks a global memory object and returns a pointer to the first byte of the object's memory block.
		/// GlobalLock function increments the lock count by one.
		/// Needed for the clipboard functions when getting the data from IDataObject
		/// </summary>
		/// <param name="hMem"></param>
		/// <returns></returns>
		[DllImport(KERNEL32, SetLastError = true)]
		public static extern IntPtr GlobalLock(IntPtr hMem);

		/// <summary>
		/// The GlobalUnlock function decrements the lock count associated with a memory object.
		/// </summary>
		/// <param name="hMem"></param>
		/// <returns></returns>
		[DllImport(KERNEL32, SetLastError = true)]
		public static extern bool GlobalUnlock(IntPtr hMem);
	}
}
