using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Provides access to the local security authority on a given server.</summary>
	public class LocalSecurity
	{
		private static LocalSecurity local;
		private SafeLsaPolicyHandle handle;
		private string svr;

		/// <summary>Initializes a new instance of the <see cref="LocalSecurity"/> class.</summary>
		/// <param name="server">The server. Use <c>null</c> for the local server.</param>
		/// <param name="accessRights">The access rights mask for the actions to be taken.</param>
		/// <exception cref="System.ComponentModel.Win32Exception"></exception>
		public LocalSecurity(string server = null, LsaPolicyAccessRights accessRights = LsaPolicyAccessRights.LookupNames)
		{
			LSA_UNICODE_STRING systemName = server;
			LSA_OBJECT_ATTRIBUTES objectAttributes = LSA_OBJECT_ATTRIBUTES.Empty;
			var ret = LsaOpenPolicy(systemName, ref objectAttributes, accessRights, out handle);
			var winErrorCode = LsaNtStatusToWinError(Convert.ToInt32(ret));
			if (winErrorCode != 0)
				throw new System.ComponentModel.Win32Exception(winErrorCode);
			svr = server;
			CurrentAccountRights = new Rights(this);
		}

		/// <summary>Access rights for a local security policy.</summary>
		[Flags]
		public enum LsaPolicyAccessRights : int
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
		}

		/// <summary>Gets the current user's account rights.</summary>
		/// <value>The current user's account rights.</value>
		public Rights CurrentAccountRights { get; }

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
			int count;
			int res = LsaNtStatusToWinError(LsaEnumerateAccountsWithUserRight(handle, privilege, out buffer, out count));
			if (res != 0)
				throw new System.ComponentModel.Win32Exception(res);
			return Array.ConvertAll(buffer.DangerousGetHandle().ToArray<LSA_ENUMERATION_INFORMATION>(count), i => new Principal.SecurityIdentifier(i.Sid));
		}

		/// <summary>Gets the account rights for the specified user.</summary>
		/// <param name="user">The user name of the account for which to manage privileges.</param>
		/// <returns>A <see cref="Rights"/> instance for the specified user.</returns>
		public Rights UserAccountRights(string user) => new Rights(this, user);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool LookupAccountName(string lpSystemName, string lpAccountName, SafeMemoryHandle psid, ref int cbsid, System.Text.StringBuilder domainName, ref int cbdomainLength, ref int use);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaAddAccountRights(SafeLsaPolicyHandle PolicyHandle, SafeMemoryHandle AccountSid, LSA_UNICODE_STRING[] UserRights, int CountOfRights);

		[DllImport("advapi32.dll")]
		private static extern int LsaClose(IntPtr ObjectHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaEnumerateAccountRights(SafeLsaPolicyHandle PolicyHandle, SafeMemoryHandle AccountSid, out IntPtr UserRightsPtr, out int CountOfRights);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaEnumerateAccountsWithUserRight(SafeLsaPolicyHandle PolicyHandle, LSA_UNICODE_STRING UserRights, out SafeLsaMemoryHandle EnumerationBuffer, out int CountReturned);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaFreeMemory(IntPtr Buffer);

		[DllImport("advapi32.dll")]
		private static extern int LsaNtStatusToWinError(int status);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern UInt32 LsaOpenPolicy(LSA_UNICODE_STRING SystemName, ref LSA_OBJECT_ATTRIBUTES ObjectAttributes, LsaPolicyAccessRights DesiredAccess, out SafeLsaPolicyHandle PolicyHandle);

		[DllImport("advapi32.dll", PreserveSig = true)]
		private static extern int LsaRemoveAccountRights(SafeLsaPolicyHandle PolicyHandle, SafeMemoryHandle AccountSid, LSA_UNICODE_STRING[] UserRights, int CountOfRights);

		private void AddRight(string accountName, string privilegeName)
		{
			LSA_UNICODE_STRING[] userRights = new LSA_UNICODE_STRING[] { privilegeName };
			int res = LsaNtStatusToWinError(LsaAddAccountRights(handle, GetSid(accountName), userRights, userRights.Length));
			if (res != 0)
				throw new System.ComponentModel.Win32Exception(res);
		}

		private string[] GetRights(string accountName)
		{
			IntPtr userRightsPtr;
			int countOfRights = 0;
			int result = LsaNtStatusToWinError(LsaEnumerateAccountRights(handle, GetSid(accountName), out userRightsPtr, out countOfRights));
			if (result != 0 && result != 2)
				throw new System.ComponentModel.Win32Exception(result);
			return Array.ConvertAll(userRightsPtr.ToArray<LSA_UNICODE_STRING>(countOfRights), u => u.ToString());
		}

		private SafeMemoryHandle GetSid(string accountName)
		{
			int sidSize = 0, nameSize = 0, accountType = 0;
			LookupAccountName(svr, accountName, new SafeMemoryHandle(), ref sidSize, null, ref nameSize, ref accountType);
			var domainName = new System.Text.StringBuilder(nameSize);
			SafeMemoryHandle sid = new SafeMemoryHandle(Marshal.AllocHGlobal(sidSize));
			if (!LookupAccountName(string.Empty, accountName, sid, ref sidSize, domainName, ref nameSize, ref accountType))
				throw new System.ComponentModel.Win32Exception();
			return sid;
		}

		private void RemoveRight(string accountName, string privilegeName)
		{
			LSA_UNICODE_STRING[] userRights = new LSA_UNICODE_STRING[] { privilegeName };
			int res = LsaNtStatusToWinError(LsaRemoveAccountRights(handle, GetSid(accountName), userRights, userRights.Length));
			if (res != 0)
				throw new System.ComponentModel.Win32Exception(res);
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
		/// <seealso cref="System.Collections.Generic.IEnumerable{System.String}"/>
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

			private static string CurrentUserName => System.Security.Principal.WindowsIdentity.GetCurrent().Name;

			/// <summary>Gets or sets the enablement of the specified privilege.</summary>
			/// <value><c>true</c> if the specified privilege is enabled; otherwise <c>false</c>.</value>
			/// <param name="right">The name of the privilege.</param>
			/// <returns>A value that represents the enablement of the specified privilege.</returns>
			public bool this[string right]
			{
				get { return Array.IndexOf(ctrl.GetRights(user), right) != -1; }
				set { if (value) ctrl.AddRight(user, right); else ctrl.RemoveRight(user, right); }
			}

			/// <summary>Returns an enumerator that iterates all of the user's current privileges.</summary>
			/// <returns>
			/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
			/// </returns>
			public IEnumerator<string> GetEnumerator() => Array.AsReadOnly(ctrl.GetRights(user)).GetEnumerator();

			Collections.IEnumerator Collections.IEnumerable.GetEnumerator() => GetEnumerator();
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal class LSA_UNICODE_STRING
		{
			private ushort length;
			private ushort MaximumLength;

			[MarshalAs(UnmanagedType.LPWStr)]
			private string Buffer;

			private LSA_UNICODE_STRING(string s)
			{
				// Unicode strings max. 32KB
				if (s.Length > 0x7ffe)
					throw new ArgumentException("String too long");
				Buffer = s;
				length = (ushort)(s.Length * 2);
				MaximumLength = (ushort)(length + 2);
			}

			public int Length => length;

			public override string ToString() => Buffer?.Substring(0, length);

			public static implicit operator LSA_UNICODE_STRING(string s) => s == null ? null : new LSA_UNICODE_STRING(s);
		}

		private sealed class SafeLsaMemoryHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeLsaMemoryHandle() : base(true) { }

			internal SafeLsaMemoryHandle(IntPtr ptr) : base(true) { SetHandle(ptr); }

			protected override bool ReleaseHandle() => LsaFreeMemory(handle) == 0;
		}

		private sealed class SafeLsaPolicyHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeLsaPolicyHandle() : base(true)
			{
			}

			internal SafeLsaPolicyHandle(IntPtr ptr) : base(true)
			{
				SetHandle(ptr);
			}

			protected override bool ReleaseHandle() => LsaClose(handle) == 0;
		}

		private sealed class SafeMemoryHandle : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			public SafeMemoryHandle() : base(true)
			{
			}

			internal SafeMemoryHandle(IntPtr ptr) : base(true)
			{
				SetHandle(ptr);
			}

			protected override bool ReleaseHandle()
			{
				Marshal.FreeHGlobal(handle);
				handle = IntPtr.Zero;
				return true;
			}
		}
	}

	/// <summary>Account rights constants that determine the type of logon that a user account can perform.</summary>
	public class LocalSecurityAccountPrivileges
	{
		/// <summary>Required for an account to log on using the interactive logon type.</summary>
		public const string InteractiveLogon = "SeInteractiveLogonRight";

		/// <summary>Required for an account to log on using the batch logon type.</summary>
		public const string LogonAsBatchJob = "SeBatchLogonRight";

		/// <summary>Required for an account to log on using the service logon type.</summary>
		public const string LogonAsService = "SeServiceLogonRight";

		/// <summary>Required for an account to log on using the network logon type.</summary>
		public const string NetworkLogon = "SeNetworkLogonRight";
	}
}