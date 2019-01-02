using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;
using static Vanara.PInvoke.AdvApi32;

namespace Microsoft.Win32
{
	/// <summary>
	/// Impersonation of a user. Allows to execute code under another user context. Please note that the account that instantiates the
	/// Impersonator class needs to have the 'Act as part of operating system' privilege set.
	/// </summary>
	internal class WindowsImpersonatedIdentity : IDisposable, IIdentity
	{
#if !(NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1)
		private WindowsImpersonationContext impersonationContext = null;
#endif
		SafeHTOKEN token;
		private WindowsIdentity identity = null;

		/// <summary>
		/// Constructor. Starts the impersonation with the given credentials. Please note that the account that instantiates the Impersonator
		/// class needs to have the 'Act as part of operating system' privilege set.
		/// </summary>
		/// <param name="userName">The name of the user to act as.</param>
		/// <param name="domainName">The domain name of the user to act as.</param>
		/// <param name="password">The password of the user to act as.</param>
		public WindowsImpersonatedIdentity(string userName, string domainName, string password)
		{
			if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(domainName) && string.IsNullOrEmpty(password))
			{
				identity = WindowsIdentity.GetCurrent();
			}
			else
			{
				if (LogonUser(userName, domainName, password, domainName == null ? LogonUserType.LOGON32_LOGON_NEW_CREDENTIALS : LogonUserType.LOGON32_LOGON_INTERACTIVE, domainName == null ? LogonUserProvider.LOGON32_PROVIDER_WINNT50 : LogonUserProvider.LOGON32_PROVIDER_DEFAULT, out token))
				{
#if (NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1)
					if (!ImpersonateLoggedOnUser(token.DangerousGetHandle()))
						throw new Win32Exception();
#else
					identity = new WindowsIdentity(token.DangerousGetHandle());
					impersonationContext = identity.Impersonate();
#endif
				}
				else
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
		}

		public string AuthenticationType => identity?.AuthenticationType;

		public bool IsAuthenticated => identity == null ? false : identity.IsAuthenticated;

		public string Name => identity?.Name;

		public void Dispose()
		{
#if (NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1)
			RevertToSelf();
#else
			if (impersonationContext != null)
				impersonationContext.Undo();
#endif
			token?.Dispose();
			if (identity != null)
				identity.Dispose();
		}
	}
}