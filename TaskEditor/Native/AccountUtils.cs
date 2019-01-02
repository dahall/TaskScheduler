using System.Security.Principal;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public static partial class AccountUtils
		{
			public static bool CurrentUserIsAdmin(string computerName)
			{
				if (!string.IsNullOrEmpty(computerName))
					return true;

				var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}

			public static string SidStringFromUserName(string userName)
			{
				var acct = new NTAccount(userName);
				try
				{
					var si = (SecurityIdentifier)acct.Translate(typeof(SecurityIdentifier));
					return si.ToString();
				}
				catch { }
				return null;
			}

			public static bool UserIsServiceAccount(string userName)
			{
				if (string.IsNullOrEmpty(userName))
					userName = WindowsIdentity.GetCurrent().Name;
				var acct = new NTAccount(userName);
				try
				{
					var si = (SecurityIdentifier)acct.Translate(typeof(SecurityIdentifier));
					return (si.IsWellKnown(WellKnownSidType.LocalSystemSid) || si.IsWellKnown(WellKnownSidType.NetworkServiceSid) || si.IsWellKnown(WellKnownSidType.LocalServiceSid));
				}
				catch { }
				return false;
			}

			public static string UserNameFromSidString(string sid)
			{
				try
				{
					var si = new SecurityIdentifier(sid);
					var acct = (NTAccount)si.Translate(typeof(NTAccount));
					return acct.Value;
				}
				catch { }
				return null;
			}
		}
	}
}