using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

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
			IntPtr token = IntPtr.Zero;
			IntPtr tokenDuplicate = IntPtr.Zero;
			try
			{
				if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(domainName) && string.IsNullOrEmpty(password))
				{
					identity = WindowsIdentity.GetCurrent();
				}
				else
				{
					if (LogonUser(userName, domainName, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
					{
						if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
						{
							identity = new WindowsIdentity(tokenDuplicate);
							impersonationContext = identity.Impersonate();
						}
						else
						{
							throw new Win32Exception(Marshal.GetLastWin32Error());
						}
					}
					else
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
				}
			}
			finally
			{
				if (token != IntPtr.Zero)
					CloseHandle(token);
				if (tokenDuplicate != IntPtr.Zero)
					CloseHandle(tokenDuplicate);
			}
		}

		public void Dispose()
		{
			if (impersonationContext != null)
				impersonationContext.Undo();
			if (identity != null)
				identity.Dispose();
		}

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword,
			int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool CloseHandle(IntPtr handle);

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