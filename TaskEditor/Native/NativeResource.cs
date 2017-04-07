using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public class NativeResource : IDisposable
		{
			private readonly IntPtr hLib;

			public NativeResource(string filename)
			{
				if (string.IsNullOrEmpty(filename))
					throw new ArgumentNullException(nameof(filename));
				if (filename.IndexOf('%') >= 0)
					filename = Environment.ExpandEnvironmentVariables(filename);
				hLib = LoadLibrary(filename);
				if (hLib == IntPtr.Zero)
					throw new System.ComponentModel.Win32Exception();
			}

			public void Dispose()
			{
				if (hLib != IntPtr.Zero)
					FreeLibrary(hLib);
			}

			public string GetString(uint id)
			{
				IntPtr ptr;
				var len = LoadString(hLib, id, out ptr, 0);
				return len == 0 ? null : Marshal.PtrToStringAuto(ptr, len);
			}

			/// <summary>
			/// Gets the resource string from a library using the string format "@[library path],[id]". Optionally, it can be contained in a "$([string])" wrapper.
			/// </summary>
			/// <param name="resourceReference">The resource reference string.</param>
			/// <returns>If <paramref name="resourceReference"/> equals <c>null</c>, <c>null</c> is returned. Otherwise, it returns the referenced string pulled from the library using the identifier.</returns>
			/// <exception cref="System.ArgumentException">
			/// Invalid string format. - resourceReference
			/// or
			/// Invalid resource identifier. - resourceReference
			/// </exception>
			public static string GetResourceString(string resourceReference)
			{
				// If null, return null
				if (resourceReference == null) return null;
				// Extract inner value if in $(fn,i) format
				if (resourceReference.StartsWith(@"$(") && resourceReference.EndsWith(")"))
					resourceReference = resourceReference.Substring(2, resourceReference.Length - 3);
				// Match parts
				var m = System.Text.RegularExpressions.Regex.Match(resourceReference, @"^@(?<f>.+),(?<i>-?\d+)(?:;(?<c>.*))?$");
				if (!m.Success)
					throw new ArgumentException(@"Invalid string format.", nameof(resourceReference));
				// Load referenced library and extract string from identifier
				int id;
				if (!int.TryParse(m.Groups["i"].Value, out id) || id > 0)
					throw new ArgumentException(@"Invalid resource identifier.", nameof(resourceReference));
				using (var nr = new NativeResource(m.Groups["f"].Value))
					return nr.GetString((uint)-id);
			}
		}
	}
}
