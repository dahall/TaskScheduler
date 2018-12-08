using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using static Vanara.PInvoke.NTDSApi;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		/// <summary>Class that provides methods against a AD domain service.</summary>
		/// <seealso cref="System.IDisposable"/>
		[SuppressUnmanagedCodeSecurity, ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public class DomainService : IDisposable
		{
			private readonly SafeDsHandle handle = SafeDsHandle.Null;

			/// <summary>Initializes a new instance of the <see cref="DomainService"/> class.</summary>
			/// <param name="domainControllerName">Name of the domain controller.</param>
			/// <param name="dnsDomainName">Name of the DNS domain.</param>
			/// <exception cref="System.ComponentModel.Win32Exception"></exception>
			public DomainService(string domainControllerName = null, string dnsDomainName = null) => DsBind(domainControllerName, dnsDomainName, out handle);

			/// <summary>Converts a directory service object name from any format to the UPN.</summary>
			/// <param name="name">The name to convert.</param>
			/// <returns>The corresponding UPN.</returns>
			/// <exception cref="System.Security.SecurityException">Unable to resolve user name.</exception>
			public string CrackName(string name)
			{
				var res = CrackNames(new string[] { name });
				if (res == null || res.Length == 0 || res[0].status != DS_NAME_ERROR.DS_NAME_NO_ERROR)
					throw new SecurityException("Unable to resolve user name.");
				return res[0].pName;
			}

			/// <summary>
			/// Converts an array of directory service object names from one format to another. Name conversion enables client applications
			/// to map between the multiple names used to identify various directory service objects.
			/// </summary>
			/// <param name="names">The names to convert.</param>
			/// <param name="flags">Values used to determine how the name syntax will be cracked.</param>
			/// <param name="formatOffered">Format of the input names.</param>
			/// <param name="formatDesired">Desired format for the output names.</param>
			/// <returns>An array of DS_NAME_RESULT_ITEM structures. Each element of this array represents a single converted name.</returns>
			public DS_NAME_RESULT_ITEM[] CrackNames(string[] names = null, DS_NAME_FLAGS flags = DS_NAME_FLAGS.DS_NAME_NO_FLAGS, DS_NAME_FORMAT formatOffered = DS_NAME_FORMAT.DS_UNKNOWN_NAME, DS_NAME_FORMAT formatDesired = DS_NAME_FORMAT.DS_USER_PRINCIPAL_NAME) => DsCrackNames(handle, names, formatDesired, formatOffered, flags);

			/// <summary>Releases unmanaged and - optionally - managed resources.</summary>
			void IDisposable.Dispose() => handle?.Dispose();
		}
	}
}