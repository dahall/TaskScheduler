using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Runtime.ConstrainedExecution;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Impersonation of a user. Allows to execute code under another
	/// user context.
	/// Please note that the account that instantiates the Impersonator class
	/// needs to have the 'Act as part of operating system' privilege set.
	/// </summary>
	internal class WindowsImpersonatedIdentity : IDisposable, IIdentity
	{
		private WindowsImpersonationContext impersonationContext = null;
		private WindowsIdentity identity = null;

		/// <summary>
		/// Constructor. Starts the impersonation with the given credentials.
		/// Please note that the account that instantiates the Impersonator class
		/// needs to have the 'Act as part of operating system' privilege set.
		/// </summary>
		/// <param name="userName">The name of the user to act as.</param>
		/// <param name="domainName">The domain name of the user to act as.</param>
		/// <param name="password">The password of the user to act as.</param>
		public WindowsImpersonatedIdentity(string userName, string domainName, string password)
		{
			SafeTokenHandle token;
			if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(domainName) && string.IsNullOrEmpty(password))
			{
				identity = WindowsIdentity.GetCurrent();
			}
			else
			{
				if (LogonUser(userName, domainName, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out token) != 0)
				{
					using (token)
					{
						identity = new WindowsIdentity(token.DangerousGetHandle());
						impersonationContext = identity.Impersonate();
					}
				}
				else
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
		}

		public void Dispose()
		{
			if (impersonationContext != null)
				impersonationContext.Undo();
			if (identity != null)
				identity.Dispose();
		}

		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword,
			int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

		private sealed class SafeTokenHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
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

		private const int LOGON32_LOGON_INTERACTIVE = 2;
		private const int LOGON32_PROVIDER_DEFAULT = 0;

		public string AuthenticationType
		{
			get { return identity == null ? null : identity.AuthenticationType; }
		}

		public bool IsAuthenticated
		{
			get { return identity == null ? false : identity.IsAuthenticated; }
		}

		public string Name
		{
			get { return identity == null ? null : identity.Name; }
		}
	}
}