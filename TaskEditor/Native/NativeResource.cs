using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public class NativeResource : IDisposable
		{
			IntPtr hLib = IntPtr.Zero;

			public NativeResource(string filename)
			{
				hLib = LoadLibrary(filename);
				if (hLib == IntPtr.Zero)
					throw new System.ComponentModel.Win32Exception();
			}

			public void Dispose()
			{
				if (hLib != IntPtr.Zero)
					FreeLibrary(hLib);
			}

			public string GetString(int id)
			{
				IntPtr ptr;
				int len = LoadString(hLib, id, out ptr, 0);
				if (len == 0)
					return null;
				return Marshal.PtrToStringAuto(ptr, len);
			}

			public static string GetResourceString(string resourceReference)
			{
				string[] parts = resourceReference.Split(',');
				if (parts.Length != 2)
					throw new ArgumentException("Invalid string format.", "resourceReference");
				int id;
				if (!int.TryParse(parts[1], out id))
					throw new ArgumentException("Invalid resource identifier.", "resourceReference");
				string fn;
				try { fn = System.Environment.ExpandEnvironmentVariables(parts[0]); }
				catch (Exception ex) { throw new ArgumentException("Invalid file name part.", "resourceReference", ex); }
				using (var nr = new NativeResource(fn))
					return nr.GetString(id);
			}
		}
	}
}
