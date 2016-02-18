using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		internal const string CREDUI = "credui.dll";
		private const int CRED_MAX_USERNAME_LENGTH = (256 + 1 + 256);
		private const int CREDUI_MAX_CAPTION_LENGTH = 128;
		private const int CREDUI_MAX_MESSAGE_LENGTH = 32767;
		private const int CREDUI_MAX_PASSWORD_LENGTH = (512 / 2);
		private const int CREDUI_MAX_USERNAME_LENGTH = CRED_MAX_USERNAME_LENGTH;

		public enum CredUIReturnCodes
		{
			Success = 0,
			Cancelled = 1223,
			NoSuchLogonSession = 1312,
			NotFound = 1168,
			InvalidAccountName = 1315,
			InsufficientBuffer = 122,
			InvalidParameter = 87,
			InvalidFlags = 1004,
		}

		/// <summary>
		/// Options for the display of the <see cref="System.Windows.Forms.CredentialsDialog"/> and its functionality.
		/// </summary>
		[Flags]
		public enum CredentialsDialogOptions
		{
			/// <summary>
			/// Default flags settings These are the following values:
			/// <see cref="GenericCredentials"/>, <see cref="AlwaysShowUI"/> and <see cref="ExpectConfirmation"/>
			/// </summary>
			Default = GenericCredentials | AlwaysShowUI | ExpectConfirmation,
			/// <summary>No options are set.</summary>
			None = 0,
			/// <summary>Notify the user of insufficient credentials by displaying the "Logon unsuccessful" balloon tip.</summary>
			IncorrectPassword = 0x1,
			/// <summary>Do not store credentials or display check boxes. You can pass ShowSaveCheckBox with this newDS to display the Save check box only, and the result is returned in the <see cref="System.Windows.Forms.CredentialsDialog.SaveChecked"/> property.</summary>
			DoNotPersist = 0x2,
			/// <summary>Populate the combo box with local administrators only.</summary>
			RequestAdministrator = 0x4,
			/// <summary>Populate the combo box with user name/password only. Do not display certificates or smart cards in the combo box.</summary>
			ExcludeCertificates = 0x8,
			/// <summary>Populate the combo box with certificates and smart cards only. Do not allow a user name to be entered.</summary>
			RequireCertificate = 0x10,
			/// <summary>If the check box is selected, show the Save check box and return <c>true</c> in the <see cref="System.Windows.Forms.CredentialsDialog.SaveChecked"/> property, otherwise, return <c>false</c>. Check box uses the value in the <see cref="System.Windows.Forms.CredentialsDialog.SaveChecked"/> property by default.</summary>
			ShowSaveCheckBox = 0x40,
			/// <summary>Specifies that a user interface will be shown even if the credentials can be returned from an existing credential in credential manager. This newDS is permitted only if GenericCredentials is also specified.</summary>
			AlwaysShowUI = 0x80,
			/// <summary>Populate the combo box with certificates or smart cards only. Do not allow a user name to be entered.</summary>
			RequireSmartcard = 0x100,
			/// <summary></summary>
			PasswordOnlyOk = 0x200,
			/// <summary></summary>
			ValidateUsername = 0x400,
			/// <summary></summary>
			CompleteUsername = 0x800,
			/// <summary>Do not show the Save check box, but the credential is saved as though the box were shown and selected.</summary>
			Persist = 0x1000,
			/// <summary>This newDS is meaningful only in locating a matching credential to pre-fill the dialog box, should authentication fail. When this newDS is specified, wildcard credentials will not be matched. It has no effect when writing a credential. CredUI does not create credentials that contain wildcard characters. Any found were either created explicitly by the user or created programmatically, as happens when a RAS connection is made.</summary>
			ServerCredential = 0x4000,
			/// <summary>Specifies that the caller will call ConfirmCredentials after checking to determine whether the returned credentials are actually valid. This mechanism ensures that credentials that are not valid are not saved to the credential manager. Specify this newDS in all cases unless DoNotPersist is specified.</summary>
			ExpectConfirmation = 0x20000,
			/// <summary>Consider the credentials entered by the user to be generic credentials, rather than windows credentials.</summary>
			GenericCredentials = 0x40000,
			/// <summary>The credential is a "RunAs" credential. The TargetName parameter specifies the name of the command or program being run. It is used for prompting purposes only.</summary>
			UsernameTargetCredentials = 0x80000,
			/// <summary></summary>
			KeepUsername = 0x100000,
		}

		[Flags]
		public enum WindowsCredentialsDialogOptions
		{
			/// <summary>No options are set.</summary>
			None = 0,
			/// <summary>The caller is requesting that the credential provider return the user name and password in plain text. This value cannot be combined with SecurePrompt.</summary>
			Generic = 0x1,
			/// <summary>The Save check box is displayed in the dialog box.</summary>
			ShowSaveCheckBox = 0x2,
			/// <summary>Only credential providers that support the authentication package specified by the authPackage parameter should be enumerated. This value cannot be combined with InAuthBufferCredentialsOnly.</summary>
			AuthPackageOnly = 0x10,
			/// <summary>Only the credentials specified by the InAuthBuffer parameter for the authentication package specified by the authPackage parameter should be enumerated. If this flag is set, and the InAuthBuffer parameter is NULL, the function fails. This value cannot be combined with AuthPackageOnly.</summary>
			InAuthBufferCredentialsOnly = 0x20,
			/// <summary>Credential providers should enumerate only administrators. This value is intended for User Account Control (UAC) purposes only. We recommend that external callers not set this flag.</summary>
			EnumerateOnlyAdministrators = 0x100,
			/// <summary>Only the incoming credentials for the authentication package specified by the authPackage parameter should be enumerated.</summary>
			EnumerateCurrentUser = 0x200,
			/// <summary>The credential dialog box should be displayed on the secure desktop. This value cannot be combined with Generic. Windows Vista: This value is not supported until Windows Vista with SP1.</summary>
			SecurePrompt = 0x1000,
			/// <summary>The credential provider should align the credential BLOB pointed to by the refOutAuthBuffer parameter to a 32-bit boundary, even if the provider is running on a 64-bit system.</summary>
			Pack32WOW = 0x10000000,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct CREDUI_INFO
		{
			private int cbSize;
			public IntPtr hwndParent;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszMessageText;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszCaptionText;
			public IntPtr hbmBanner;

			public CREDUI_INFO(IntPtr hwndOwner, string caption, string message, Bitmap banner)
			{
				cbSize = Marshal.SizeOf(typeof(CREDUI_INFO));
				hwndParent = hwndOwner;
				pszCaptionText = caption;
				pszMessageText = message;
				hbmBanner = banner != null ? banner.GetHbitmap() : IntPtr.Zero;
			}

			public void Dispose()
			{
				if (hbmBanner != IntPtr.Zero)
					DeleteObject(hbmBanner);
			}
		}

		[DllImport(CREDUI, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CredPackAuthenticationBuffer(
			int dwFlags,
			IntPtr pszUserName,
			IntPtr pszPassword,
			IntPtr pPackedCredentials,
			ref int pcbPackedCredentials);

		[DllImport(CREDUI, CharSet = CharSet.Unicode, EntryPoint = "CredUIConfirmCredentialsW")]
		public static extern CredUIReturnCodes CredUIConfirmCredentials(string targetName, [MarshalAs(UnmanagedType.Bool)] bool confirm);

		[DllImport(CREDUI, CharSet = CharSet.Unicode, EntryPoint = "CredUIPromptForCredentialsW")]
		public static extern CredUIReturnCodes CredUIPromptForCredentials(ref CREDUI_INFO creditUR,
			string targetName,
			IntPtr reserved1,
			int iError,
			StringBuilder userName,
			int maxUserName,
			StringBuilder password,
			int maxPassword,
			[MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
			CredentialsDialogOptions flags);

		[DllImport(CREDUI, CharSet = CharSet.Unicode)]
		public static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(ref CREDUI_INFO notUsedHere,
			int authError,
			ref uint authPackage,
			IntPtr InAuthBuffer,
			uint InAuthBufferSize,
			out IntPtr refOutAuthBuffer,
			out uint refOutAuthBufferSize,
			[MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
			WindowsCredentialsDialogOptions flags);

		[DllImport(CREDUI, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CredUnPackAuthenticationBuffer(
			int dwFlags,
			IntPtr pAuthBuffer,
			int cbAuthBuffer,
			StringBuilder pszUserName,
			ref int pcchMaxUserName,
			StringBuilder pszDomainName,
			ref int pcchMaxDomainame,
			StringBuilder pszPassword,
			ref int pcchMaxPassword);

		[DllImport(CREDUI, CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool CredUnPackAuthenticationBuffer(
			int dwFlags,
			IntPtr pAuthBuffer,
			int cbAuthBuffer,
			IntPtr pszUserName,
			ref int pcchMaxUserName,
			IntPtr pszDomainName,
			ref int pcchMaxDomainame,
			IntPtr pszPassword,
			ref int pcchMaxPassword);

		/// <summary>
		/// Safe container for an authentication buffer. Allows for creation using native <c>CredPackAuthenticationBuffer</c> method or assignment from an existing <c>IntPtr</c>. Can unpack to <see cref="string"/> or <see cref="SecureString"/> values.
		/// </summary>
		public class AuthenticationBuffer : IDisposable
		{
			private IntPtr buffer = IntPtr.Zero;
			private int bufferSize = 0;

			private class SafeCoTaskMemString : GenericSafeHandle
			{
				public SafeCoTaskMemString(string s) : base(Marshal.StringToCoTaskMemUni(s), h => { Marshal.ZeroFreeCoTaskMemUnicode(h); return true; }) { }
				public SafeCoTaskMemString(SecureString s) : base(Marshal.SecureStringToCoTaskMemUnicode(s), h => { Marshal.ZeroFreeCoTaskMemUnicode(h); return true; }) { }
				public SafeCoTaskMemString(int size) : base(Marshal.AllocCoTaskMem(size), h => { Marshal.ZeroFreeCoTaskMemUnicode(h); return true; }) { Size = size; }
				public SecureString ToSecureString() => DangerousGetHandle().ToSecureString();
				public int Size { get; } = -1;
			}

			public AuthenticationBuffer(string userName, string password)
			{
				var pUserName = new SafeCoTaskMemString(userName ?? "");
				var pPassword = new SafeCoTaskMemString(password ?? "");
				Init(0, pUserName, pPassword);
			}

			public AuthenticationBuffer(SecureString userName, SecureString password)
			{
				var pUserName = new SafeCoTaskMemString(userName);
				var pPassword = new SafeCoTaskMemString(password);
				Init(0, pUserName, pPassword);
			}

			public AuthenticationBuffer(IntPtr authBuffer, int authBufferSize)
			{
				buffer = authBuffer;
				bufferSize = authBufferSize;
			}

			public IntPtr DangerousHandle => buffer;

			public int Size => bufferSize;

			private void Init(int flags, SafeCoTaskMemString pUserName, SafeCoTaskMemString pPassword)
			{
				if (!CredPackAuthenticationBuffer(flags, pUserName, pPassword, IntPtr.Zero, ref bufferSize) && Marshal.GetLastWin32Error() == 122) /*ERROR_INSUFFICIENT_BUFFER*/
				{
					buffer = Marshal.AllocCoTaskMem(bufferSize);
					if (!CredPackAuthenticationBuffer(flags, pUserName, pPassword, buffer, ref bufferSize))
						throw new Win32Exception();
				}
				else
					throw new Win32Exception();
			}

			public void UnPack(bool decryptProtectedCredentials, out string userName, out string domainName, out string password)
			{
				var pUserName = new StringBuilder(CRED_MAX_USERNAME_LENGTH);
				var pDomainName = new StringBuilder(CRED_MAX_USERNAME_LENGTH);
				var pPassword = new StringBuilder(CREDUI_MAX_PASSWORD_LENGTH);
				int userNameSize = pUserName.Capacity;
				int domainNameSize = pDomainName.Capacity;
				int passwordSize = pPassword.Capacity;

				if (!CredUnPackAuthenticationBuffer(decryptProtectedCredentials ? 0x1 : 0x0, buffer, bufferSize,
					pUserName, ref userNameSize, pDomainName, ref domainNameSize, pPassword, ref passwordSize))
					throw new Win32Exception();

				userName = pUserName.ToString();
				domainName = pDomainName.ToString();
				password = pPassword.ToString();
			}

			public void UnPack(bool decryptProtectedCredentials, out SecureString userName, out SecureString domainName, out SecureString password)
			{
				var pUserName = new SafeCoTaskMemString(CRED_MAX_USERNAME_LENGTH);
				var pDomainName = new SafeCoTaskMemString(CRED_MAX_USERNAME_LENGTH);
				var pPassword = new SafeCoTaskMemString(CREDUI_MAX_PASSWORD_LENGTH);
				int userNameSize = pUserName.Size;
				int domainNameSize = pDomainName.Size;
				int passwordSize = pPassword.Size;

				if (!CredUnPackAuthenticationBuffer(decryptProtectedCredentials ? 0x1 : 0x0, buffer, bufferSize,
					pUserName, ref userNameSize, pDomainName, ref domainNameSize, pPassword, ref passwordSize))
					throw new Win32Exception();

				userName = pUserName.ToSecureString();
				domainName = pUserName.ToSecureString();
				password = pUserName.ToSecureString();
			}

			public void Dispose()
			{
				if (buffer != IntPtr.Zero)
					Marshal.FreeCoTaskMem(buffer);
			}

			public static implicit operator IntPtr(AuthenticationBuffer b) => b.buffer;
		}
	}
}

namespace System.Security
{
	internal static class SecureStringExt
	{
		public static string ToInsecureString(this SecureString s)
		{
			IntPtr p = IntPtr.Zero;
			try
			{
				p = Marshal.SecureStringToCoTaskMemUnicode(s);
				return Marshal.PtrToStringUni(p);
			}
			finally
			{
				if (p != IntPtr.Zero)
					Marshal.ZeroFreeCoTaskMemUnicode(p);
			}
		}

		public static SecureString ToSecureString(this IntPtr p)
		{
			SecureString s = new SecureString();
			int i = 0;
			while (true)
			{
				Char c = (Char)Marshal.ReadInt16(p, ((i++) * sizeof(Int16)));
				if (c == '\u0000')
					break;
				s.AppendChar(c);
			}
			s.MakeReadOnly();
			return s;
		}

		public static SecureString ToSecureString(this IntPtr p, int length)
		{
			SecureString s = new SecureString();
			for (var i = 0; i < length; i++)
				s.AppendChar((Char)Marshal.ReadInt16(p, i * sizeof(Int16)));
			s.MakeReadOnly();
			return s;
		}

		public static SecureString ToSecureString(this string s)
		{
			SecureString ss = new SecureString();
			foreach (var c in s)
				ss.AppendChar(c);
			ss.MakeReadOnly();
			return ss;
		}
	}
}