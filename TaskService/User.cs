using System;
using System.Security.Principal;

namespace Microsoft.Win32.TaskScheduler
{
	internal class User : IEquatable<User>, IDisposable
	{
		private WindowsIdentity acct;
		private SecurityIdentifier sid;

		public User(string userName = null)
		{
			var cur = WindowsIdentity.GetCurrent();
			if (string.IsNullOrEmpty(userName)) userName = null;
			// 2018-03-02: Hopefully not a breaking change, but by adding in the comparison of an account name without a domain and the current user,
			//    there is a chance that current implementations will break given the condition that a local account with the same name as a domain
			//    account exists and the intention was to prefer the local account. In such a case, the developer should prepend the user name in
			//    TaskDefinition.Principal.UserId with the machine name of the local machine.
			if (userName == null || cur.Name.Equals(userName, StringComparison.InvariantCultureIgnoreCase) || GetUser(cur.Name).Equals(userName, StringComparison.InvariantCultureIgnoreCase))
			{
				acct = cur;
				sid = acct.User;
			}
			else if (userName.Contains("\\"))
			{
				try
				{
					using (var ds = new NativeMethods.DomainService())
						acct = new WindowsIdentity(ds.CrackName(userName)); sid = acct.User;
				} catch { }
			}

			if (acct == null)
			{
				if (userName != null && userName.Contains("@"))
				{
					acct = new WindowsIdentity(userName);
					sid = acct.User;
				}

				if (acct == null && userName != null)
				{
					var ntacct = new NTAccount(userName);
					try { sid = (SecurityIdentifier)ntacct.Translate(typeof(SecurityIdentifier)); } catch { }
				}
			}

			string GetUser(string domUser)
			{
				var split = domUser.Split('\\');
				return split.Length == 2 ? split[1] : domUser;
			}
		}

		internal User(WindowsIdentity wid)
		{
			acct = wid;
		}

		public static User Current => new User(WindowsIdentity.GetCurrent());

		public bool IsAdmin => acct != null ? new WindowsPrincipal(acct).IsInRole(WindowsBuiltInRole.Administrator) : false;

		public bool IsCurrent => acct?.User.Equals(WindowsIdentity.GetCurrent().User) ?? false;

		public bool IsServiceAccount
		{
			get
			{
				try
				{
					return (sid != null && (sid.IsWellKnown(WellKnownSidType.LocalSystemSid) || sid.IsWellKnown(WellKnownSidType.NetworkServiceSid) || sid.IsWellKnown(WellKnownSidType.LocalServiceSid)));
				}
				catch { }
				return false;
			}
		}

		public bool IsSystem => sid != null && sid.IsWellKnown(WellKnownSidType.LocalSystemSid);

		public string Name => acct?.Name ?? ((NTAccount)sid?.Translate(typeof(NTAccount)))?.Value;

		public string SidString => sid?.ToString();

		public static User FromSidString(string sid)
		{
			SecurityIdentifier si = new SecurityIdentifier(sid);
			NTAccount acct = (NTAccount)si.Translate(typeof(NTAccount));
			return new User(acct.Value);
		}

		public void Dispose()
		{
			if (acct != null)
				acct.Dispose();
		}

		public override bool Equals(object obj)
		{
			User user = obj as User;
			if (user != null)
				return this.Equals(user);
			WindowsIdentity wid = obj as WindowsIdentity;
			if (wid != null && sid != null)
				return sid.Equals(wid.User);
			try
			{
				string un = obj as string;
				if (un != null)
					return this.Equals(new User(un));
			}
			catch { }
			return base.Equals(obj);
		}

		public bool Equals(User other) => (other != null && sid != null) ? sid.Equals(other.sid) : false;

		public override int GetHashCode() => sid?.GetHashCode() ?? 0;
	}
}