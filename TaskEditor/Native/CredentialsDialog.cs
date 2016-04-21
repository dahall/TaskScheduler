using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>
	/// Dialog box which prompts for user credentials using the Win32 CREDUI methods.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.CommonDialog" />
	[ToolboxItem(true), System.Drawing.ToolboxBitmap(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog), "Dialog"), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), 
	DesignTimeVisible(true), Description("Dialog that prompts the user for credentials."), 
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class CredentialsDialog : CommonDialog
	{
		private const int maxStringLength = 100;

		/// <summary>
		/// Initializes a new instance of the <see cref="CredentialsDialog"/> class.
		/// </summary>
		public CredentialsDialog()
		{
			Reset();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CredentialsDialog"/> class.
		/// </summary>
		/// <param name="caption">The caption.</param>
		/// <param name="message">The message.</param>
		/// <param name="userName">Name of the user.</param>
		public CredentialsDialog(string caption = null, string message = null, string userName = null) : this()
		{
			Caption = caption;
			Message = message;
			UserName = userName;
		}

		/// <summary>
		/// Gets or sets the Windows Error Code that caused this credential dialog to appear, if applicable.
		/// </summary>
		[DefaultValue(0), Category("Data"), Description("Windows Error Code that caused this credential dialog")]
		public int AuthenticationError { get; set; }

		/// <summary>
		/// Gets or sets the image to display as the banner for the dialog. Only visible on Windows XP and earlier systems.
		/// </summary>
		[DefaultValue((string)null), Category("Appearance"), Description("Image to display in dialog banner")]
		public Bitmap Banner { get; set; }

		/// <summary>
		/// Gets or sets the caption for the dialog
		/// </summary>
		[DefaultValue((string)null), Category("Appearance"), Description("Caption to display for dialog")]
		public string Caption { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to encrypt password.
		/// </summary>
		/// <value><c>true</c> if password is to be encrypted; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether to encrypt password")]
		public bool EncryptPassword { get; set; }

		/// <summary>Gets or sets a value indicating whether to force XP style dialog even on newer systems.</summary>
		/// <value><c>true</c> if forcing to pre-Vista style; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Indicates whether to force XP style dialog even on newer systems.")]
		public bool ForcePreVistaStyle { get; set; }

		/// <summary>
		/// Gets or sets the message to display on the dialog
		/// </summary>
		[DefaultValue((string)null), Category("Appearance"), Description("Message to display in the dialog")]
		public string Message { get; set; }

		/// <summary>
		/// Gets the password entered by the user
		/// </summary>
		[DefaultValue((string)null), Browsable(false)]
		public string Password { get; private set; }

		/// <summary>
		/// Gets or sets a boolean indicating if the save check box was checked
		/// </summary>
		[DefaultValue(false), Category("Behavior"), Description("Indicates if the save check box is checked.")]
		public bool SaveChecked { get; set; }

		/// <summary>
		/// Gets the password entered by the user using an encrypted string
		/// </summary>
		[DefaultValue(null), Browsable(false)]
		public SecureString SecurePassword { get; private set; }

		/// <summary>
		/// Gets or sets the name of the target for these credentials
		/// </summary>
		/// <remarks>
		/// This value is used as a key to store the credentials if persisted
		/// </remarks>
		[DefaultValue((string)null), Category("Data"), Description("Target for the credentials")]
		public string Target { get; set; }

		/// <summary>
		/// Gets or sets the username entered
		/// </summary>
		/// <remarks>
		/// If non-empty before calling <see cref="RunDialog"/>, this value will be displayed in the dialog
		/// </remarks>
		[DefaultValue((string)null), Category("Data"), Description("User name displayed in the dialog")]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the password should be validated before returning.
		/// </summary>
		/// <value>
		///   <c>true</c> if the password should be validated; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates if the password should be validated before returning.")]
		public bool ValidatePassword { get; set; }

		/// <summary>
		/// Gets a default value for the target.
		/// </summary>
		/// <value>The default target.</value>
		private string DefaultTarget => Environment.UserDomainName;

		/// <summary>
		/// Confirms the credentials.
		/// </summary>
		/// <param name="storedCredentials">If set to <c>true</c> the credentials are stored in the credential manager.</param>
		public void ConfirmCredentials(bool storedCredentials)
		{
			NativeMethods.CredUIReturnCodes ret = NativeMethods.CredUIConfirmCredentials(Target, storedCredentials);
			if (ret != NativeMethods.CredUIReturnCodes.Success && ret != NativeMethods.CredUIReturnCodes.InvalidParameter)
				throw new InvalidOperationException($"Unable to confirm credentials. Error: 0x{ret:X}");
		}

		/// <summary>
		/// When overridden in a derived class, resets the properties of a common dialog box to their default values.
		/// </summary>
		public override void Reset()
		{
			Target = UserName = Caption = Message = Password = null;
			Banner = null;
			EncryptPassword = SaveChecked = false;
		}

		private bool IsValidPassword(string userName, string password)
		{
			NativeMethods.SafeTokenHandle token;
			string[] udn = userName.Split('\\');
			string domain = udn.Length == 2 ? udn[0] : null;
			string user = udn.Length == 2 ? udn[1] : udn[0];
			try
			{
				if (NativeMethods.LogonUser(user, domain, password, 3, 0, out token) != 0)
					return true;
			}
			catch { }
			return false;
		}

		/// <summary>
		/// When overridden in a derived class, specifies a common dialog box.
		/// </summary>
		/// <param name="parentWindowHandle">A value that represents the window handle of the owner window for the common dialog box.</param>
		/// <returns>
		/// true if the dialog box was successfully run; otherwise, false.
		/// </returns>
		protected override bool RunDialog(IntPtr parentWindowHandle)
		{
			NativeMethods.CREDUI_INFO info = new NativeMethods.CREDUI_INFO(parentWindowHandle, Caption, Message, Banner);
			try
			{
				if (Environment.OSVersion.Version.Major <= 5 || ForcePreVistaStyle)
				{
					StringBuilder userName = new StringBuilder(UserName, maxStringLength);
					StringBuilder password = new StringBuilder(maxStringLength);
					bool save = SaveChecked;

					if (string.IsNullOrEmpty(Target)) Target = DefaultTarget;
					NativeMethods.CredUIReturnCodes ret = NativeMethods.CredUIPromptForCredentials(ref info, Target, IntPtr.Zero,
						AuthenticationError, userName, maxStringLength, password, maxStringLength, ref save, 
						NativeMethods.CredentialsDialogOptions.Default | (SaveChecked ? NativeMethods.CredentialsDialogOptions.ShowSaveCheckBox : 0));
					switch (ret)
					{
						case NativeMethods.CredUIReturnCodes.Success:
							if (ValidatePassword && !IsValidPassword(userName.ToString(), password.ToString()))
								return false;
							/*if (save)
							{
								CredUIReturnCodes cret = CredUIConfirmCredentials(this.Target, false);
								if (cret != CredUIReturnCodes.NO_ERROR && cret != CredUIReturnCodes.ERROR_INVALID_PARAMETER)
								{
									this.Options |= CredentialsDialogOptions.IncorrectPassword;
									return false;
								}
							}*/
							break;
						case NativeMethods.CredUIReturnCodes.Cancelled:
							return false;
						default:
							throw new InvalidOperationException($"Unknown error in CredentialsDialog. Error: 0x{ret:X}");
					}

					if (EncryptPassword)
					{
						// Convert the password to a SecureString
						SecureString newPassword = StringBuilderToSecureString(password);

						// Clear the old password and set the new one (read-only)
						if (SecurePassword != null)
							SecurePassword.Dispose();
						newPassword.MakeReadOnly();
						SecurePassword = newPassword;
					}
					else
						Password = password.ToString();

					// Update other properties
					UserName = userName.ToString();
					SaveChecked = save;
					return true;
				}
				else
				{
					NativeMethods.WindowsCredentialsDialogOptions flag = NativeMethods.WindowsCredentialsDialogOptions.Generic;
					if (SaveChecked)
						flag |= NativeMethods.WindowsCredentialsDialogOptions.ShowSaveCheckBox;

					NativeMethods.AuthenticationBuffer buf = null;
					if (EncryptPassword && SecurePassword != null)
						buf = new NativeMethods.AuthenticationBuffer(UserName.ToSecureString(), SecurePassword);
					else
						buf = new NativeMethods.AuthenticationBuffer(UserName, Password);

					IntPtr outAuthBuffer = IntPtr.Zero;
					uint outAuthBufferSize = 0, authPackage = 0;
					bool save = SaveChecked;
					var retVal = NativeMethods.CredUIPromptForWindowsCredentials(ref info, 0, ref authPackage,
						buf, (uint)buf.Size, out outAuthBuffer, out outAuthBufferSize, ref save, flag);
					var outAuth = new NativeMethods.AuthenticationBuffer(outAuthBuffer, (int)outAuthBufferSize);

					if (retVal == NativeMethods.CredUIReturnCodes.Cancelled)
						return false;
					if (retVal != NativeMethods.CredUIReturnCodes.Success)
						throw new Win32Exception((int)retVal);

					SaveChecked = save;
					if (EncryptPassword)
					{
						SecureString u, d, p;
						outAuth.UnPack(true, out u, out d, out p);
						Password = null;
						SecurePassword = p;
						UserName = $"{d.ToInsecureString()}\\{u.ToInsecureString()}".TrimStart('\\');
					}
					else
					{
						string u, d, p;
						outAuth.UnPack(true, out u, out d, out p);
						Password = p;
						SecurePassword = null;
						UserName = $"{d}\\{u}".TrimStart('\\');
						if (ValidatePassword && !IsValidPassword(UserName, Password))
							return false;
					}
					return true;
				}
			}
			finally
			{
				info.Dispose();
			}
		}

		private static SecureString StringBuilderToSecureString(StringBuilder password)
		{
			// Copy the password into the secure string, zeroing the original buffer as we go
			SecureString newPassword = new SecureString();
			for (int i = 0; i < password.Length; i++)
			{
				newPassword.AppendChar(password[i]);
				password[i] = '\0';
			}
			return newPassword;
		}
	}
}