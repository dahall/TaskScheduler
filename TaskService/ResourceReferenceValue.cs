using System;
using System.Runtime.InteropServices;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Some string values of <see cref="TaskDefinition"/> properties can be set to retrieve their value from existing DLLs as a resource. This class facilitates creating those reference strings.
	/// </summary>
	[PublicAPI]
	public class ResourceReferenceValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceReferenceValue"/> class.
		/// </summary>
		/// <param name="dllPath">The DLL path.</param>
		/// <param name="resourceId">The resource identifier.</param>
		public ResourceReferenceValue([NotNull] string dllPath, int resourceId)
		{
			ResourceFilePath = dllPath;
			ResourceIdentifier = resourceId;
		}

		/// <summary>
		/// Gets or sets the resource file path. This can be a relative path, full path or lookup path (e.g. %SystemRoot%\System32\ResourceName.dll).
		/// </summary>
		/// <value>
		/// The resource file path.
		/// </value>
		public string ResourceFilePath { get; set; }

		/// <summary>
		/// Gets or sets the resource identifier.
		/// </summary>
		/// <value>The resource identifier.</value>
		public int ResourceIdentifier { get; set; }

		/// <summary>
		/// Performs an implicit conversion from <see cref="Microsoft.Win32.TaskScheduler.ResourceReferenceValue" /> to <see cref="System.String" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator string(ResourceReferenceValue value) => value.ToString();

		/// <summary>
		/// Parses the input string. String must be in the format "$(@ [Dll], [ResourceID])".
		/// </summary>
		/// <param name="value">The input string value.</param>
		/// <returns>A new <see cref="ResourceReferenceValue" /> instance on success or <c>null</c> on failure.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c></exception>
		/// <exception cref="FormatException"><paramref name="value"/> is not in the format "$(@ [Dll], [ResourceID])"</exception>
		[NotNull]
		public static ResourceReferenceValue Parse([NotNull] string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));
			ResourceReferenceValue r;
			if (!TryParse(value, out r))
				throw new FormatException();
			return r;
		}

		/// <summary>
		/// Tries to parse to input string. String must be in the format "$(@ [Dll], [ResourceID])".
		/// </summary>
		/// <param name="value">The input string value.</param>
		/// <param name="resourceRef">The resource reference to be returned. On failure, this value equals <c>null</c>.</param>
		/// <returns>A new <see cref="ResourceReferenceValue"/> instance on success or <c>null</c> on failure.</returns>
		public static bool TryParse(string value, out ResourceReferenceValue resourceRef)
		{
			if (!string.IsNullOrEmpty(value))
			{
				var m = System.Text.RegularExpressions.Regex.Match(value, @"^\$\(\@ (?<x>[^,]+), (?<i>-?\d+)\)$");
				if (m.Success)
				{
					resourceRef = new ResourceReferenceValue(m.Groups["x"].Value, int.Parse(m.Groups["i"].Value));
					return true;
				}
			}
			resourceRef = null;
			return false;
		}

		/// <summary>
		/// Gets the result of pulling the string from the resource file using the identifier.
		/// </summary>
		/// <returns><see cref="string"/> from resource file.</returns>
		/// <exception cref="System.IO.FileNotFoundException"><see cref="ResourceFilePath"/> cannot be found.</exception>
		/// <exception cref="System.ComponentModel.Win32Exception">Unable to load <see cref="ResourceFilePath"/> or string identified by <see cref="ResourceIdentifier"/>.</exception>
		[NotNull]
		public string GetResolvedString()
		{
			if (!System.IO.File.Exists(ResourceFilePath))
				throw new System.IO.FileNotFoundException("Invalid resource file path.", ResourceFilePath);
			IntPtr hLib = IntPtr.Zero;
			try
			{
				hLib = NativeMethods.LoadLibrary(ResourceFilePath);
				if (hLib == IntPtr.Zero)
					throw new System.ComponentModel.Win32Exception();
				var sb = new StringBuilder(8192);
				int l = LoadString(hLib, ResourceIdentifier, sb, sb.Capacity);
				if (l == 0)
					throw new System.ComponentModel.Win32Exception();
				return sb.ToString(0, l);
			}
			finally
			{
				if (hLib != IntPtr.Zero)
					NativeMethods.FreeLibrary(hLib);
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> in the format required by the Task Scheduler to reference a string in a DLL.
		/// </summary>
		/// <returns>A formatted <see cref="System.String" /> in the format $(@ [Dll], [ResourceID]).</returns>
		public override string ToString() => $"$(@ {ResourceFilePath}, {ResourceIdentifier})";

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
		private static extern int LoadString(IntPtr hInstance, int wID, StringBuilder lpBuffer, int nBufferMax);
	}
}