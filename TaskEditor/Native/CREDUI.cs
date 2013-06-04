using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>
	/// Options for the display of the <see cref="CredentialsDialog"/> and its functionality.
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
		/// <summary>Do not store credentials or display check boxes. You can pass ShowSaveCheckBox with this flag to display the Save check box only, and the result is returned in the <see cref="CredentialsDialog.SaveChecked"/> property.</summary>
		DoNotPersist = 0x2,
		/// <summary>Populate the combo box with local administrators only.</summary>
		RequestAdministrator = 0x4,
		/// <summary>Populate the combo box with user name/password only. Do not display certificates or smart cards in the combo box.</summary>
		ExcludeCertificates = 0x8,
		/// <summary>Populate the combo box with certificates and smart cards only. Do not allow a user name to be entered.</summary>
		RequireCertificate = 0x10,
		/// <summary>If the check box is selected, show the Save check box and return <c>true</c> in the <see cref="CredentialsDialog.SaveChecked"/> property, otherwise, return <c>false</c>. Check box uses the value in the <see cref="CredentialsDialog.SaveChecked"/> property by default.</summary>
		ShowSaveCheckBox = 0x40,
		/// <summary>Specifies that a user interface will be shown even if the credentials can be returned from an existing credential in credential manager. This flag is permitted only if GenericCredentials is also specified.</summary>
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
		/// <summary>This flag is meaningful only in locating a matching credential to prefill the dialog box, should authentication fail. When this flag is specified, wildcard credentials will not be matched. It has no effect when writing a credential. CredUI does not create credentials that contain wildcard characters. Any found were either created explicitly by the user or created programmatically, as happens when a RAS connection is made.</summary>
		ServerCredential = 0x4000,
		/// <summary>Specifies that the caller will call ConfirmCredentials after checking to determine whether the returned credentials are actually valid. This mechanism ensures that credentials that are not valid are not saved to the credential manager. Specify this flag in all cases unless DoNotPersist is specified.</summary>
		ExpectConfirmation = 0x20000,
		/// <summary>Consider the credentials entered by the user to be generic credentials, rather than windows credentials.</summary>
		GenericCredentials = 0x40000,
		/// <summary>The credential is a "runas" credential. The TargetName parameter specifies the name of the command or program being run. It is used for prompting purposes only.</summary>
		UsernameTargetCredentials = 0x80000,
		/// <summary></summary>
		KeepUsername = 0x100000,
	}
}

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		internal const string CREDUI = "credui.dll";

		public enum CredUIReturnCodes
		{
			NO_ERROR = 0,
			ERROR_CANCELLED = 1223,
			ERROR_NO_SUCH_LOGON_SESSION = 1312,
			ERROR_NOT_FOUND = 1168,
			ERROR_INVALID_ACCOUNT_NAME = 1315,
			ERROR_INSUFFICIENT_BUFFER = 122,
			ERROR_INVALID_PARAMETER = 87,
			ERROR_INVALID_FLAGS = 1004,
		}

		//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct CREDUI_INFO
		{
			public int cbSize;
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
			System.Windows.Forms.CredentialsDialogOptions flags);
	}
}
