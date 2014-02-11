using System;
using System.ComponentModel;
using System.Security.Principal;

namespace SecurityEditor
{
	internal class AccountInfo
	{
		public SecurityIdentifier sid;
		public string domainName;
		public string accountName;
		public Microsoft.Win32.NativeMethods.SidNameUse use;

		public AccountInfo(SecurityIdentifier s, string targetComputer = null)
		{
			sid = s;
			uint res = Microsoft.Win32.NativeMethods.AccountUtils.LookupAccountSid(s, out accountName, out domainName, out use, targetComputer);
			if (res != 0)
				throw new Win32Exception((int)res);
		}

		public override string ToString()
		{
			System.Diagnostics.Debug.WriteLine("{0}\\{1}: {2} ({3})", domainName, accountName, sid.Value, use);
			if (use == Microsoft.Win32.NativeMethods.SidNameUse.WellKnownGroup)
				return accountName;
			return string.Format("{0} ({1}\\{0})", accountName, sid.Value.StartsWith("S-1-5-32") ? Environment.MachineName : domainName);
		}

		public static implicit operator SecurityIdentifier(AccountInfo ai)
		{
			return ai.sid;
		}
	}
}
