using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class NativeMethods
	{
		public static class AccountUtils
		{
			private static string systemAccount, networkServiceAccount, localServiceAccount;

			public static bool CurrentUserIsAdmin(string computerName)
			{
				if (!string.IsNullOrEmpty(computerName) || computerName == ".")
					return true;

				WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
				return principal.IsInRole(0x220);
			}

			public static bool SelectAccount(System.Windows.Forms.IWin32Window parent, string targetComputerName, ref string acctName, out bool isGroup, out bool isService)
			{
				CubicOrange.Windows.Forms.ActiveDirectory.DirectoryObjectPickerDialog dlg = new CubicOrange.Windows.Forms.ActiveDirectory.DirectoryObjectPickerDialog();
				dlg.TargetComputer = targetComputerName;
				if (dlg.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK)
				{
					if (dlg.SelectedObject != null)
					{
						acctName = dlg.SelectedObject.Name;
						isGroup = dlg.SelectedObject.SchemaClassName.Equals("Group", StringComparison.OrdinalIgnoreCase);
						isService = NativeMethods.AccountUtils.UserIsServiceAccount(acctName);
						return true;
					}
				}
				isGroup = isService = false;
				return false;
			}

			/*public static bool SelectAccount(System.Windows.Forms.IWin32Window parent, string targetComputerName, ref string acctName, ref bool isGroup, ref bool isService)
			{
				NativeMethods.ObjectPicker dlg = new NativeMethods.ObjectPicker();
				dlg.TargetComputer = targetComputerName;
				if (dlg.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK && dlg.Picks.Length > 0)
				{
					acctName = dlg.Picks[0].ObjectName;
					isGroup = NativeMethods.ObjectPickerTypes.Group == dlg.Picks[0].ObjectType;
					isService = NativeMethods.AccountUtils.UserIsServiceAccount(acctName);
					return true;
				}
				return false;
			}*/

			public static bool UserIsServiceAccount(string userName)
			{
				if (((systemAccount == null) || (networkServiceAccount == null)) || (localServiceAccount == null))
				{
					systemAccount = FormattedUserNameFromStringSid("S-1-5-18", null);
					networkServiceAccount = FormattedUserNameFromStringSid("S-1-5-20", null);
					localServiceAccount = FormattedUserNameFromStringSid("S-1-5-19", null);
				}

				int num = string.Compare(userName, systemAccount, StringComparison.CurrentCultureIgnoreCase);
				if (num != 0)
				{
					num = string.Compare(userName, networkServiceAccount, StringComparison.CurrentCultureIgnoreCase);
				}
				if (num != 0)
				{
					num = string.Compare(userName, localServiceAccount, StringComparison.CurrentCultureIgnoreCase);
				}
				return (0 == num);
			}

			[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
			internal static extern bool ConvertStringSidToSid([In, MarshalAs(UnmanagedType.LPTStr)] string pStringSid, ref IntPtr sid);

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern bool LookupAccountSid(string systemName, byte[] accountSid, StringBuilder accountName, ref int nameLength, StringBuilder domainName, ref int domainLength, out int accountType);

			internal static uint LookupAccountSid(SecurityIdentifier sid, out string accountName, out string domainName, out int use)
			{
				uint num = 0;
				byte[] binaryForm = new byte[sid.BinaryLength];
				sid.GetBinaryForm(binaryForm, 0);
				int capacity = 0x44;
				int num3 = 0x44;
				StringBuilder builder = new StringBuilder(capacity);
				StringBuilder builder2 = new StringBuilder(num3);
				if (!LookupAccountSid(null, binaryForm, builder, ref capacity, builder2, ref num3, out use))
				{
					num = (uint)Marshal.GetLastWin32Error();
				}
				accountName = builder.ToString().TrimEnd(new char[] { '$' });
				domainName = builder2.ToString();
				return num;
			}

			private static bool FindUserFromSid(IntPtr incomingSid, string computerName, ref string userName)
			{
				int num3;
				bool flag = false;
				int cchName = 0;
				int cchReferencedDomainName = 0;
				StringBuilder referencedDomainName = new StringBuilder();
				int error = 0;
				StringBuilder name = new StringBuilder();
				if (!LookupAccountSid(computerName, incomingSid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out num3))
				{
					error = Marshal.GetLastWin32Error();
					if (0x7a != error)
					{
						throw new Win32Exception(error);
					}
				}
				if ((error == 0) || (0x7a == error))
				{
					referencedDomainName = new StringBuilder(cchReferencedDomainName);
					name = new StringBuilder(cchName);
					if (!LookupAccountSid(computerName, incomingSid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out num3))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					flag = IsUserFromUse(num3);
					if (userName == null)
					{
						return flag;
					}
					if (0 < cchReferencedDomainName)
					{
						userName = referencedDomainName.ToString();
					}
					else
					{
						userName = computerName;
					}
					userName = string.Format((IFormatProvider)CultureInfo.CurrentCulture.GetFormat(typeof(string)), "{0}\\{1}", new object[] { userName, name });
				}
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
			}

			private static bool IsUserFromUse(int use)
			{
				bool flag = false;
				if (use == 1)
				{
					flag = true;
				}
				return flag;
			}

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			private static extern bool LookupAccountSid([In, MarshalAs(UnmanagedType.LPTStr)] string systemName, IntPtr sid, StringBuilder name, ref int cchName, StringBuilder referencedDomainName, ref int cchReferencedDomainName, out int use);
		}
	}
}