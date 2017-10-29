//using CubicOrange.Windows.Forms.ActiveDirectory;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		public static partial class AccountUtils
		{
			/// <summary> 
			/// The function checks whether the primary access token of the process belongs  
			/// to user account that is a member of the local Administrators group, even if  
			/// it currently is not elevated. 
			/// </summary> 
			/// <returns> 
			/// Returns true if the primary access token of the process belongs to user  
			/// account that is a member of the local Administrators group. Returns false  
			/// if the token does not. 
			/// </returns> 
			/// <exception cref="System.ComponentModel.Win32Exception"> 
			/// When any native Windows API call fails, the function throws a Win32Exception  
			/// with the last error code. 
			/// </exception> 
			public static bool IsUserInAdminGroup()
			{
				bool fInAdminGroup = false;
				SafeTokenHandle hTokenToCheck = null;

				// Open the access token of the current process for query and duplicate.
				SafeTokenHandle hToken = SafeTokenHandle.FromCurrentProcess(AccessTypes.TokenQuery | AccessTypes.TokenDuplicate);

				// Determine whether system is running Windows Vista or later operating  
				// systems (major version >= 6) because they support linked tokens, but  
				// previous versions (major version < 6) do not. 
				if (Environment.OSVersion.Version.Major >= 6)
				{
					// Running Windows Vista or later (major version >= 6).  
					// Determine token type: limited, elevated, or default.  

					// Marshal the TOKEN_ELEVATION_TYPE enum from native to .NET. 
					TOKEN_ELEVATION_TYPE elevType = hToken.GetInfo<TOKEN_ELEVATION_TYPE>(TOKEN_INFORMATION_CLASS.TokenElevationType);

					// If limited, get the linked elevated token for further check. 
					if (elevType == TOKEN_ELEVATION_TYPE.Limited)
					{
						// Marshal the linked token value from native to .NET. 
						IntPtr hLinkedToken = hToken.GetInfo<IntPtr>(TOKEN_INFORMATION_CLASS.TokenLinkedToken);
						hTokenToCheck = new SafeTokenHandle(hLinkedToken);
					}
				}

				// CheckTokenMembership requires an impersonation token. If we just got  
				// a linked token, it already is an impersonation token.  If we did not  
				// get a linked token, duplicate the original into an impersonation  
				// token for CheckTokenMembership. 
				if (hTokenToCheck == null)
				{
					if (!NativeMethods.DuplicateToken(hToken, SECURITY_IMPERSONATION_LEVEL.Identification, out hTokenToCheck))
						throw new Win32Exception();
				}

				// Check if the token to be checked contains admin SID. 
				WindowsIdentity id = new WindowsIdentity(hTokenToCheck.DangerousGetHandle());
				WindowsPrincipal principal = new WindowsPrincipal(id);
				fInAdminGroup = principal.IsInRole(WindowsBuiltInRole.Administrator);

				return fInAdminGroup;
			}

			/*public static void ElevateApplication()
			{
				if (!CurrentUserIsAdmin(null))
				{
					// Launch itself as administrator 
					ProcessStartInfo proc = new ProcessStartInfo(System.Windows.Forms.Application.ExecutablePath) { UseShellExecute = true, WorkingDirectory = Environment.CurrentDirectory, Verb = "runas" };
					try
					{
						Process.Start(proc);
						System.Windows.Forms.Application.Exit();
					}
					catch { }
				}
			}*/

			public static bool CurrentUserIsAdmin(string computerName)
			{
				if (!string.IsNullOrEmpty(computerName))
					return true;

				WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			}

			public static bool UserIsServiceAccount(string userName)
			{
				if (string.IsNullOrEmpty(userName))
					userName = WindowsIdentity.GetCurrent().Name;
				NTAccount acct = new NTAccount(userName);
				try
				{
					SecurityIdentifier si = (SecurityIdentifier)acct.Translate(typeof(SecurityIdentifier));
					return (si.IsWellKnown(WellKnownSidType.LocalSystemSid) || si.IsWellKnown(WellKnownSidType.NetworkServiceSid) || si.IsWellKnown(WellKnownSidType.LocalServiceSid));
				}
				catch { }
				return false;
			}

			public static string SidStringFromUserName(string userName)
			{
				NTAccount acct = new NTAccount(userName);
				try
				{
					SecurityIdentifier si = (SecurityIdentifier)acct.Translate(typeof(SecurityIdentifier));
					return si.ToString();
				}
				catch { }
				return null;
			}

			public static string UserNameFromSidString(string sid)
			{
				try
				{
					SecurityIdentifier si = new SecurityIdentifier(sid);
					NTAccount acct = (NTAccount)si.Translate(typeof(NTAccount));
					return acct.Value;
				}
				catch { }
				return null;
			}

			/*private static bool LookupAccountSid(string computerName, IntPtr sid, out string accountName, out string domainName, out SID_NAME_USE use)
			{
				int anLen = 0x100;
				int dnLen = 0x100;
				StringBuilder acctName = new StringBuilder(anLen);
				StringBuilder domName = new StringBuilder(dnLen);
				if (NativeMethods.LookupAccountSid(computerName, sid, acctName, ref anLen, domName, ref dnLen, out use))
				{
					accountName = acctName.ToString().TrimEnd('$');
					domainName = domName.ToString();
					return true;
				}
				accountName = domainName = null;
				return false;
			}

			private static bool FindUserFromSid(IntPtr incomingSid, string computerName, ref string userName)
			{
				SID_NAME_USE use;
				string acctName, domainName;
				if (!LookupAccountSid(computerName, incomingSid, out acctName, out domainName, out use))
					throw new Win32Exception();
				bool flag = use == SID_NAME_USE.SidTypeUser;
				if (userName == null)
					return flag;

				if (!string.IsNullOrEmpty(domainName))
					domainName = computerName;
				userName = $"{domainName}\\{acctName}";
				return flag;
			}

			private static string FormattedUserNameFromSid(IntPtr incomingSid, string computerName)
			{
				string userName = string.Empty;
				FindUserFromSid(incomingSid, computerName, ref userName);
				if (!string.IsNullOrEmpty(userName))
				{
					SecurityIdentifier identifier = new SecurityIdentifier(incomingSid);
					string[] strArray = userName.Split(new char[] { '\\' });
					if (strArray.Length != 2)
					{
						return userName;
					}
					string str2 = strArray[1];
					if ((identifier.IsWellKnown(WellKnownSidType.NetworkServiceSid) || identifier.IsWellKnown(WellKnownSidType.AnonymousSid)) || ((identifier.IsWellKnown(WellKnownSidType.LocalSystemSid) || identifier.IsWellKnown(WellKnownSidType.LocalServiceSid)) || identifier.IsWellKnown(WellKnownSidType.LocalSid)))
					{
						return str2;
					}
					if (string.Compare(strArray[0], computerName, StringComparison.CurrentCultureIgnoreCase) == 0)
					{
						userName = str2;
					}
				}
				return userName;
			}

			private static string FormattedUserNameFromStringSid(string incomingSid, string computerName)
			{
				string str = string.Empty;
				IntPtr zero = IntPtr.Zero;
				if (!ConvertStringSidToSid(incomingSid, ref zero))
				{
					throw new Win32Exception();
				}
				str = FormattedUserNameFromSid(zero, computerName);
				Marshal.FreeHGlobal(zero);
				return str;
			}*/
		}
	}
}