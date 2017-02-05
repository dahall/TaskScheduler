using System.Collections.Generic;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace System.Security.Policy
{
	/// <summary>Provides access to the local security authority on a given server.</summary>
	public class LocalSecurity
	{
		private static LocalSecurity local;
		private SafeLsaHandle handle;
		private string svr;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalSecurity"/> class.
		/// </summary>
		public LocalSecurity() : this(null) { }

		/// <summary>Initializes a new instance of the <see cref="LocalSecurity"/> class.</summary>
		/// <param name="server">The server. Use <c>null</c> for the local server.</param>
		/// <param name="accessRights">The access rights mask for the actions to be taken.</param>
		/// <exception cref="System.ComponentModel.Win32Exception"></exception>
		public LocalSecurity(string server = null, LsaPolicyAccessRights accessRights = LsaPolicyAccessRights.AllAccess)
		{
			LSA_UNICODE_STRING systemName = server;
			LSA_OBJECT_ATTRIBUTES objectAttributes = LSA_OBJECT_ATTRIBUTES.Empty;
			ThrowIfLsaError(LsaOpenPolicy(systemName, ref objectAttributes, 0x000F0000 | (int)accessRights, out handle)); // Add in STANDARD_RIGHTS_REQUIRED
			svr = server;
		}

		/// <summary>Access rights for a local security policy.</summary>
		[Flags]
		public enum LsaPolicyAccessRights
		{
			/// <summary>
			/// This access type is needed to read the target system's miscellaneous security policy information. This
			/// includes the default quota, auditing, server state and role information, and trust information. This
			/// access type is also needed to enumerate trusted domains, accounts, and privileges.
			/// </summary>
			ViewLocalInformation = 1,

			/// <summary>This access type is needed to view audit trail or audit requirements information.</summary>
			ViewAuditInformation = 2,

			/// <summary>
			/// This access type is needed to view sensitive information, such as the names of accounts established for
			/// trusted domain relationships.
			/// </summary>
			GetPrivateInformation = 4,

			/// <summary>This access type is needed to change the account domain or primary domain information.</summary>
			TrustAdmin = 8,

			/// <summary>This access type is needed to create a new Account object.</summary>
			CreateAccount = 0x10,

			/// <summary>This access type is needed to create a new Private Data object.</summary>
			CreateSecret = 0x20,

			/// <summary>Set the default system quotas that are applied to user accounts.</summary>
			SetDefaultQuotaLimits = 0x80,

			/// <summary>This access type is needed to update the auditing requirements of the system.</summary>
			SetAuditRequirements = 0x100,

			/// <summary>
			/// This access type is needed to change the characteristics of the audit trail such as its maximum size or
			/// the retention period for audit records, or to clear the log.
			/// </summary>
			AuditLogAdmin = 0x200,

			/// <summary>
			/// This access type is needed to modify the server state or role (master/replica) information. It is also
			/// needed to change the replica source and account name information.
			/// </summary>
			ServerAdmin = 0x400,

			/// <summary>This access type is needed to translate between names and SIDs.</summary>
			LookupNames = 0x800,

			/// <summary>All access.</summary>
			AllAccess = 0xFFF,
		}

		/// <summary>Account rights determine the type of logon that a user account can perform. An administrator assigns account rights to user and group accounts. Each user's account rights include those granted to the user and to the groups to which the user belongs.</summary>
		[Flags]
		public enum LsaSecurityAccessRights
		{
			/// <summary>Required for an account to log on using the interactive logon type.</summary>
			InteractiveLogon = 0x00000001,

			/// <summary>Required for an account to log on using the network logon type.</summary>
			NetworkLogon = 0x00000002,

			/// <summary>Required for an account to log on using the batch logon type.</summary>
			BatchLogon = 0x00000004,

			/// <summary>Required for an account to log on using the service logon type.</summary>
			ServiceLogon = 0x00000010,

			/// <summary>Explicitly denies an account the right to log on using the interactive logon type.</summary>
			DenyInteractiveLogon = 0x00000040,

			/// <summary>Explicitly denies an account the right to log on using the network logon type.</summary>
			DenyNetworkLogon = 0x00000080,

			/// <summary>Explicitly denies an account the right to log on using the batch logon type.</summary>
			DenyBatchLogon = 0x00000100,

			/// <summary>Explicitly denies an account the right to log on using the service logon type.</summary>
			DenyServiceLogon = 0x00000200,

			/// <summary>Remote interactive logon</summary>
			RemoteInteractiveLogon = 0x00000400,

			/// <summary>Explicitly denies an account the right to log on remotely using the interactive logon type.</summary>
			DenyRemoteInteractiveLogon = 0x00000800,
		}

		[Flags]
		private enum LsaAccountAccessMask
		{
			View = 0x00000001,
			AdjustPrivileges = 0x00000002,
			AdjustQuotas = 0x00000004,
			AdjustSystemAccess = 0x00000008,
		}

		/// <summary>Gets the current user's account rights.</summary>
		/// <value>The current user's account rights.</value>
		public Rights CurrentAccountRights => new Rights(this);

		/// <summary>Gets the current user's system access.</summary>
		/// <value>The current user's system access.</value>
		public SystemAccess CurrentSystemAccess => new SystemAccess(this);

		/// <summary>Gets a <see cref="LocalSecurity"/> instance for the local server and rights to lookup names.</summary>
		/// <value>A <see cref="LocalSecurity"/> instance for the local server.</value>
		public static LocalSecurity Local => local ?? (local = new LocalSecurity());

		/// <summary>Enumerates the accounts with the specified privilege.</summary>
		/// <param name="privilege">The privilege name.</param>
		/// <returns>
		/// An array of <see cref="Principal.SecurityIdentifier"/> objects representing all accounts with the specified privilege.
		/// </returns>
		/// <exception cref="System.ComponentModel.Win32Exception">Unable to enumerate accounts.</exception>
		public Principal.SecurityIdentifier[] EnumerateAccountsWithRight(string privilege)
		{
			SafeLsaMemoryHandle buffer;
			uint count;
			ThrowIfLsaError(LsaEnumerateAccountsWithUserRight(handle, privilege, out buffer, out count));
			return Array.ConvertAll(buffer.DangerousGetHandle().ToArray<LSA_ENUMERATION_INFORMATION>((int)count), i => new Principal.SecurityIdentifier(i.Sid));
		}

		/// <summary>Gets the account rights for the specified user.</summary>
		/// <param name="user">The user name of the account for which to manage privileges.</param>
		/// <returns>A <see cref="Rights"/> instance for the specified user.</returns>
		public Rights UserAccountRights(string user) => new Rights(this, user);

		/// <summary>Gets the system access for the specified user.</summary>
		/// <param name="user">The user name of the account for which to manage privileges.</param>
		/// <returns>A <see cref="SystemAccess"/> instance for the specified user.</returns>
		public SystemAccess UserSystemAccess(string user) => new SystemAccess(this, user);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool LookupAccountName(string lpSystemName, string lpAccountName, SafeMemoryHandle psid, ref int cbsid, Text.StringBuilder domainName, ref int cbdomainLength, ref int use);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaAddAccountRights(SafeLsaHandle PolicyHandle, SafeMemoryHandle AccountSid, LSA_UNICODE_STRING[] UserRights, int CountOfRights);

		[DllImport("advapi32.dll")]
		private static extern int LsaClose(IntPtr ObjectHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaCreateAccount(SafeLsaHandle PolicyHandle, SafeMemoryHandle AccountSid, LsaAccountAccessMask DesiredAccess, out SafeLsaHandle AccountHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaEnumerateAccountRights(SafeLsaHandle PolicyHandle, SafeMemoryHandle AccountSid, out IntPtr UserRightsPtr, out int CountOfRights);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaEnumerateAccountsWithUserRight(SafeLsaHandle PolicyHandle, LSA_UNICODE_STRING UserRights, out SafeLsaMemoryHandle EnumerationBuffer, out uint CountReturned);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaFreeMemory(IntPtr Buffer);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaGetSystemAccessAccount(SafeLsaHandle AccountHandle, out int SystemAccess);

		[DllImport("advapi32.dll")]
		private static extern int LsaNtStatusToWinError(int status);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaOpenAccount(SafeLsaHandle PolicyHandle, SafeMemoryHandle AccountSid, LsaAccountAccessMask DesiredAccess, out SafeLsaHandle AccountHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaOpenPolicy(LSA_UNICODE_STRING SystemName, ref LSA_OBJECT_ATTRIBUTES ObjectAttributes, int DesiredAccess, out SafeLsaHandle PolicyHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaRemoveAccountRights(SafeLsaHandle PolicyHandle, SafeMemoryHandle AccountSid, LSA_UNICODE_STRING[] UserRights, int CountOfRights);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaSetSystemAccessAccount(SafeLsaHandle AccountHandle, int SystemAccess);

		private void AddRight(string accountName, string privilegeName)
		{
			LSA_UNICODE_STRING[] userRights = { privilegeName };
			ThrowIfLsaError(LsaAddAccountRights(handle, GetSid(accountName), userRights, userRights.Length));
		}

		private SafeLsaHandle GetAccount(string accountName, LsaAccountAccessMask mask = LsaAccountAccessMask.View)
		{
			var sid = GetSid(accountName);
			SafeLsaHandle hAcct;
			int res = LsaNtStatusToWinError(LsaOpenAccount(handle, sid, mask, out hAcct));
			if (res == 2)
				ThrowIfLsaError(LsaCreateAccount(handle, sid, mask, out hAcct));
			else if (res != 0)
				throw new ComponentModel.Win32Exception(res);
			return hAcct;
		}

		private string[] GetRights(string accountName)
		{
			IntPtr userRightsPtr;
			int countOfRights;
			int result = LsaNtStatusToWinError(LsaEnumerateAccountRights(handle, GetSid(accountName), out userRightsPtr, out countOfRights));
			if (result == 2) // Try adding account and retrying
			{
				using (GetAccount(accountName))
					result = LsaNtStatusToWinError(LsaEnumerateAccountRights(handle, GetSid(accountName), out userRightsPtr, out countOfRights));
			}
			if (result != 0 && result != 2)
				throw new ComponentModel.Win32Exception(result);
			return Array.ConvertAll(userRightsPtr.ToArray<LSA_UNICODE_STRING>(countOfRights), u => u.ToString());
		}

		private SafeMemoryHandle GetSid(string accountName)
		{
			int sidSize = 0, nameSize = 0, accountType = 0;
			LookupAccountName(svr, accountName, new SafeMemoryHandle(), ref sidSize, null, ref nameSize, ref accountType);
			var domainName = new Text.StringBuilder(nameSize);
			SafeMemoryHandle sid = new SafeMemoryHandle(sidSize);
			if (!LookupAccountName(string.Empty, accountName, sid, ref sidSize, domainName, ref nameSize, ref accountType))
				throw new ComponentModel.Win32Exception();
			return sid;
		}

		private LsaSecurityAccessRights GetSystemAccess(SafeLsaHandle hAcct)
		{
			int rights;
			ThrowIfLsaError(LsaGetSystemAccessAccount(hAcct, out rights));
			return (LsaSecurityAccessRights)rights;
		}

		private void RemoveRight(string accountName, string privilegeName)
		{
			LSA_UNICODE_STRING[] userRights = new LSA_UNICODE_STRING[] { privilegeName };
			ThrowIfLsaError(LsaRemoveAccountRights(handle, GetSid(accountName), userRights, userRights.Length));
		}

		private void SetSystemAccess(SafeLsaHandle hAcct, LsaSecurityAccessRights rights)
		{
			LsaSecurityAccessRights cur = GetSystemAccess(hAcct);
			ThrowIfLsaError(LsaSetSystemAccessAccount(hAcct, (int)(cur | rights)));
		}

		private static void ThrowIfLsaError(int lsaRetVal)
		{
			int ret = LsaNtStatusToWinError(lsaRetVal);
			if (ret != 0)
				throw new ComponentModel.Win32Exception(ret);
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct LSA_ENUMERATION_INFORMATION
		{
			public IntPtr Sid;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct LSA_OBJECT_ATTRIBUTES
		{
			private int Length;
			private IntPtr RootDirectory;
			private IntPtr ObjectName;
			private UInt32 Attributes;
			private IntPtr SecurityDescriptor;
			private IntPtr SecurityQualityOfService;

			public static LSA_OBJECT_ATTRIBUTES Empty => new LSA_OBJECT_ATTRIBUTES { Length = Marshal.SizeOf(typeof(LSA_OBJECT_ATTRIBUTES)) };
		}

		/// <summary>Allows for the privileges of a user to be retrieved, enumerated and set.</summary>
		/// <seealso cref="IEnumerable{T}"/>
		public class Rights : IEnumerable<string>
		{
			private LocalSecurity ctrl;
			private string user;

			/// <summary>Initializes a new instance of the <see cref="Rights"/> class.</summary>
			/// <param name="parent">The parent.</param>
			/// <param name="userName">Name of the user.</param>
			public Rights(LocalSecurity parent, string userName = null)
			{
				ctrl = parent; user = userName ?? CurrentUserName;
			}

			private static string CurrentUserName => Principal.WindowsIdentity.GetCurrent().Name;

			/// <summary>Gets or sets the enablement of the specified privilege.</summary>
			/// <value><c>true</c> if the specified privilege is enabled; otherwise <c>false</c>.</value>
			/// <param name="right">The name of the privilege.</param>
			/// <returns>A value that represents the enablement of the specified privilege.</returns>
			public bool this[string right]
			{
				get { return Array.IndexOf(ctrl.GetRights(user), right) != -1; }
				set { if (value) ctrl.AddRight(user, right); else ctrl.RemoveRight(user, right); }
			}

			Collections.IEnumerator Collections.IEnumerable.GetEnumerator() => GetEnumerator();

			/// <summary>Returns an enumerator that iterates all of the user's current privileges.</summary>
			/// <returns>
			/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
			/// </returns>
			public IEnumerator<string> GetEnumerator() => Array.AsReadOnly(ctrl.GetRights(user)).GetEnumerator();
		}

		/// <summary>Allows for the privileges of a user to be retrieved, enumerated and set.</summary>
		/// <seealso cref="System.Collections.Generic.IEnumerable{T}"/>
		public class SystemAccess
		{
			private LocalSecurity ctrl;
			private string user;

			/// <summary>Initializes a new instance of the <see cref="Rights"/> class.</summary>
			/// <param name="parent">The parent.</param>
			/// <param name="userName">Name of the user.</param>
			public SystemAccess(LocalSecurity parent, string userName = null)
			{
				ctrl = parent; user = userName ?? CurrentUserName;
			}

			private static string CurrentUserName => Principal.WindowsIdentity.GetCurrent().Name;

			/// <summary>Gets or sets the enablement of the specified privilege.</summary>
			/// <value><c>true</c> if the specified privilege is enabled; otherwise <c>false</c>.</value>
			/// <param name="right">The name of the privilege.</param>
			/// <returns>A value that represents the enablement of the specified privilege.</returns>
			public bool this[LsaSecurityAccessRights right]
			{
				get { return (ctrl.GetSystemAccess(ctrl.GetAccount(user)) & right) == right; }
				set
				{
					var hAcct = ctrl.GetAccount(user, LsaAccountAccessMask.View | LsaAccountAccessMask.AdjustSystemAccess);
					var cur = ctrl.GetSystemAccess(hAcct);
					bool hasFlag = cur.HasFlag(right);
					if ((hasFlag && value) || (!hasFlag && !value))
						return;
					if (value)
						cur |= right;
					else
						cur &= ~right;
					ctrl.SetSystemAccess(hAcct, cur);
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private sealed class LSA_UNICODE_STRING : IDisposable
		{
			private ushort length;
			private ushort MaximumLength;
			private IntPtr Buffer;

			public LSA_UNICODE_STRING() : this(null)
			{
			}

			public LSA_UNICODE_STRING(string s)
			{
				if (s == null)
				{
					length = MaximumLength = 0;
					Buffer = IntPtr.Zero;
				}
				else
				{
					// Unicode strings max. 32KB
					if (s.Length > 0x7ffe)
						throw new ArgumentException("String too long");
					Buffer = Marshal.StringToHGlobalUni(s);
					own = true;
					length = (ushort)(s.Length * Text.UnicodeEncoding.CharSize);
					MaximumLength = (ushort)(length + Text.UnicodeEncoding.CharSize);
				}
			}

			public int Length => length / Text.UnicodeEncoding.CharSize;

			public override string ToString() => Marshal.PtrToStringUni(Buffer, Length);

			private bool own { get; }

			void IDisposable.Dispose()
			{
				if (own && Buffer != IntPtr.Zero)
					Marshal.FreeHGlobal(Buffer);
			}

			public static implicit operator LSA_UNICODE_STRING(string s) => new LSA_UNICODE_STRING(s);
		}

		private sealed class SafeLsaHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeLsaHandle() : base(true)
			{
			}

			internal SafeLsaHandle(IntPtr ptr) : base(true)
			{
				SetHandle(ptr);
			}

			protected override bool ReleaseHandle() => LsaClose(handle) == 0;
		}

		private sealed class SafeLsaMemoryHandle : SafeBuffer
		{
			public SafeLsaMemoryHandle() : base(true)
			{
			}

			internal SafeLsaMemoryHandle(IntPtr ptr) : base(true)
			{
				SetHandle(ptr);
			}

			protected override bool ReleaseHandle() => LsaFreeMemory(handle) == 0;
		}

		private sealed class SafeMemoryHandle : SafeBuffer
		{
			public SafeMemoryHandle() : base(true)
			{
			}

			public SafeMemoryHandle(int bytes) : base(true)
			{
				SetHandle(Marshal.AllocHGlobal(bytes));
			}

			internal SafeMemoryHandle(IntPtr ptr) : base(true)
			{
				SetHandle(ptr);
			}

			protected override bool ReleaseHandle()
			{
				try { Marshal.FreeHGlobal(handle); handle = IntPtr.Zero; return true; } catch { return false; }
			}
		}
	}
}