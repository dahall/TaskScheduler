using System;
using System.Runtime.InteropServices;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.User32;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public class NativeResource : IDisposable
		{
			private readonly SafeHINSTANCE hLib;

			public NativeResource(string filename)
			{
				if (string.IsNullOrEmpty(filename))
					throw new ArgumentNullException(nameof(filename));
				if (filename.IndexOf('%') >= 0)
					filename = Environment.ExpandEnvironmentVariables(filename);
				hLib = LoadLibrary(filename);
				if (hLib.IsInvalid)
					throw new System.ComponentModel.Win32Exception();
			}

			/// <summary>
			/// Gets the resource string from a library using the string format "@[library path],[id]". Optionally, it can be contained in a
			/// "$([string])" wrapper.
			/// </summary>
			/// <param name="resourceReference">The resource reference string.</param>
			/// <returns>
			/// If <paramref name="resourceReference"/> equals <c>null</c>, <c>null</c> is returned. Otherwise, it returns the referenced
			/// string pulled from the library using the identifier.
			/// </returns>
			/// <exception cref="System.ArgumentException">
			/// Invalid string format. - resourceReference or Invalid resource identifier. - resourceReference
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
				if (!int.TryParse(m.Groups["i"].Value, out var id) || id > 0)
					throw new ArgumentException(@"Invalid resource identifier.", nameof(resourceReference));
				using (var nr = new NativeResource(m.Groups["f"].Value))
					return nr.GetString(-id);
			}

			public void Dispose() => hLib?.Dispose();

			public string GetString(int id)
			{
				var len = LoadString(hLib, id, out var ptr, 0);
				return len == 0 ? null : Marshal.PtrToStringAuto(ptr, len);
			}
		}
	}
}