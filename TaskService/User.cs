using System;
using System.Security.Principal;

namespace Microsoft.Win32.TaskScheduler
{
	internal class User : IEquatable<User>, IDisposable
	{
		private WindowsIdentity acct;

		public User(string userName = null)
		{
			var cur = WindowsIdentity.GetCurrent();
			if (userName == null || cur.Name.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
			{
				acct = cur;
			}
			else if (userName != null && userName.Contains("\\"))
			{
				using (var ds = new Microsoft.Win32.NativeMethods.DsHandle())
					acct = new WindowsIdentity(ds.CrackName(userName));
			}
			else
				acct = new WindowsIdentity(userName);
		}

		internal User(WindowsIdentity wid)
		{
			acct = wid;
		}

		public static User Current => new User(WindowsIdentity.GetCurrent());

		public bool IsAdmin => new WindowsPrincipal(acct).IsInRole(WindowsBuiltInRole.Administrator);

		public bool IsCurrent => acct.User.Equals(WindowsIdentity.GetCurrent().User);

		public bool IsServiceAccount
		{
			get
			{
				try
				{
					SecurityIdentifier si = acct.User;
					return (si.IsWellKnown(WellKnownSidType.LocalSystemSid) || si.IsWellKnown(WellKnownSidType.NetworkServiceSid) || si.IsWellKnown(WellKnownSidType.LocalServiceSid));
				}
				catch { }
				return false;
			}
		}

		public bool IsSystem => acct.IsSystem;

		public string Name => acct.Name;

		public string SidString => acct.User.ToString();

		public static User FromSidString(string sid)
		{
			SecurityIdentifier si = new SecurityIdentifier(sid);
			NTAccount acct = (NTAccount)si.Translate(typeof(NTAccount));
			return new User(acct.Value);
		}

		public void Dispose()
		{
			acct.Dispose();
		}

		public override bool Equals(object obj)
		{
			User user = obj as User;
			if (user != null)
				return this.Equals(user);
			WindowsIdentity wid = obj as WindowsIdentity;
			if (wid != null)
				return acct.User.Equals(wid.User);
			try
			{
				string un = obj as string;
				if (un != null)
					return this.Equals(new User(un));
			}
			catch { }
			return base.Equals(obj);
		}

		public bool Equals(User other) => (other != null) ? acct.User.Equals(other.acct.User) : false;

		public override int GetHashCode() => acct.User.GetHashCode();
	}
}