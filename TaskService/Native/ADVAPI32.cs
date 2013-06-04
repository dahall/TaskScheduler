using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		const string ADVAPI32 = "advapi32.dll";

		[DllImport(ADVAPI32, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ConvertStringSidToSid([In, MarshalAs(UnmanagedType.LPTStr)] string pStringSid, ref IntPtr sid);

		[DllImport(ADVAPI32, SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

		[DllImport(ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool LookupAccountSid(string systemName, byte[] accountSid, StringBuilder accountName, ref int nameLength, StringBuilder domainName, ref int domainLength, out int accountType);

		[DllImport(ADVAPI32, CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool LookupAccountSid([In, MarshalAs(UnmanagedType.LPTStr)] string systemName, IntPtr sid, StringBuilder name, ref int cchName, StringBuilder referencedDomainName, ref int cchReferencedDomainName, out int use);

		public sealed class SafeTokenHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			private SafeTokenHandle() : base(true) { }

			[DllImport("kernel32.dll"), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), System.Security.SuppressUnmanagedCodeSecurity]
			[return: MarshalAs(UnmanagedType.Bool)]
			private static extern bool CloseHandle(IntPtr handle);

			protected override bool ReleaseHandle()
			{
				return CloseHandle(handle);
			}
		}
	}
}
