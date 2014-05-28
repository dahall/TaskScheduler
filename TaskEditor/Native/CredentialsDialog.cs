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
	[ToolboxItem(true), System.Drawing.ToolboxBitmap("Dialog.bmp"), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), 
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
		/// <param name="options">The options.</param>
		public CredentialsDialog(string caption = null, string message = null, string userName = null, CredentialsDialogOptions options = CredentialsDialogOptions.Default) : this()
		{
			this.Caption = caption;
			this.Message = message;
			this.UserName = userName;
			this.Options = options;
		}

		/// <summary>
		/// Gets or sets the Windows Error Code that caused this credential dialog to appear, if applicable.
		/// </summary>
		[System.ComponentModel.DefaultValue(0), Category("Data"), Description("Windows Error Code that caused this credential dialog")]
		public int AuthenticationError { get; set; }

		/// <summary>
		/// Gets or sets the image to display as the banner for the dialog
		/// </summary>
		[System.ComponentModel.DefaultValue((string)null), Category("Appearance"), Description("Image to display in dialog banner")]
		public Bitmap Banner { get; set; }

		/// <summary>
		/// Gets or sets the caption for the dialog
		/// </summary>
		[System.ComponentModel.DefaultValue((string)null), Category("Appearance"), Description("Caption to display for dialog")]
		public string Caption { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to encrypt password.
		/// </summary>
		/// <value><c>true</c> if password is to be encrypted; otherwise, <c>false</c>.</value>
		[System.ComponentModel.DefaultValue(false), Category("Behavior"), Description("Indicates whether to encrypt password")]
		public bool EncryptPassword { get; set; }

		/// <summary>
		/// Gets or sets the message to display on the dialog
		/// </summary>
		[System.ComponentModel.DefaultValue((string)null), Category("Appearance"), Description("Message to display in the dialog")]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the options for the dialog.
		/// </summary>
		/// <value>The options.</value>
		[System.ComponentModel.DefaultValue(typeof(CredentialsDialogOptions), "Default"), Category("Behavior"), Description("Options for the dialog")]
		public CredentialsDialogOptions Options { get; set; }

		/// <summary>
		/// Gets the password entered by the user
		/// </summary>
		[System.ComponentModel.DefaultValue((string)null), Browsable(false)]
		public string Password { get; private set; }

		/// <summary>
		/// Gets or sets a boolean indicating if the save check box was checked
		/// </summary>
		/// <remarks>
		/// Only valid if <see cref="CredentialsDialog.Options"/> has the <see cref="CredentialsDialogOptions.DoNotPersist"/> newDS set.
		/// </remarks>
		[System.ComponentModel.DefaultValue(false), Category("Behavior"), Description("Indicates if the save check box is checked.")]
		public bool SaveChecked { get; set; }

		/// <summary>
		/// Gets the password entered by the user using an encrypted string
		/// </summary>
		[System.ComponentModel.DefaultValue(null), Browsable(false)]
		public SecureString SecurePassword { get; private set; }

		/// <summary>
		/// Gets or sets the name of the target for these credentials
		/// </summary>
		/// <remarks>
		/// This value is used as a key to store the credentials if persisted
		/// </remarks>
		[System.ComponentModel.DefaultValue((string)null), Category("Data"), Description("Target for the credentials")]
		public string Target { get; set; }

		/// <summary>
		/// Gets or sets the username entered
		/// </summary>
		/// <remarks>
		/// If non-empty before calling <see cref="RunDialog"/>, this value will be displayed in the dialog
		/// </remarks>
		[System.ComponentModel.DefaultValue((string)null), Category("Data"), Description("User name displayed in the dialog")]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the password should be validated before returning.
		/// </summary>
		/// <value>
		///   <c>true</c> if the password should be validated; otherwise, <c>false</c>.
		/// </value>
		[System.ComponentModel.DefaultValue(false), Category("Behavior"), Description("Indicates if the password should be validated before returning.")]
		public bool ValidatePassword { get; set; }

		/// <summary>
		/// Gets a default value for the target.
		/// </summary>
		/// <value>The default target.</value>
		private string DefaultTarget
		{
			get { return Environment.UserDomainName; }
		}

		/// <summary>
		/// Confirms the credentials.
		/// </summary>
		/// <param name="storedCredentials">If set to <c>true</c> the credentials are stored in the credential manager.</param>
		public void ConfirmCredentials(bool storedCredentials)
		{
			NativeMethods.CredUIReturnCodes ret = NativeMethods.CredUIConfirmCredentials(this.Target, storedCredentials);
			if (ret != NativeMethods.CredUIReturnCodes.NO_ERROR && ret != NativeMethods.CredUIReturnCodes.ERROR_INVALID_PARAMETER)
				throw new InvalidOperationException(string.Format("Unable to confirm credentials. Error: 0x{0:X}", ret));
		}

		/// <summary>
		/// When overridden in a derived class, resets the properties of a common dialog box to their default values.
		/// </summary>
		public override void Reset()
		{
			this.Target = this.UserName = this.Caption = this.Message = this.Password = null;
			this.Banner = null;
			this.EncryptPassword = this.SaveChecked = false;
			this.Options = CredentialsDialogOptions.Default;
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
			NativeMethods.CREDUI_INFO info = new NativeMethods.CREDUI_INFO(parentWindowHandle, this.Caption, this.Message, this.Banner);
			try
			{
				StringBuilder userName = new StringBuilder(this.UserName, maxStringLength);
				StringBuilder password = new StringBuilder(maxStringLength);
				bool save = this.SaveChecked;

				if (string.IsNullOrEmpty(this.Target)) this.Target = this.DefaultTarget;
				NativeMethods.CredUIReturnCodes ret = NativeMethods.CredUIPromptForCredentials(ref info, this.Target, IntPtr.Zero,
					this.AuthenticationError, userName, maxStringLength, password, maxStringLength, ref save, this.Options);
				switch (ret)
				{
					case NativeMethods.CredUIReturnCodes.NO_ERROR:
						if (this.ValidatePassword && !IsValidPassword(userName.ToString(), password.ToString()))
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
					case NativeMethods.CredUIReturnCodes.ERROR_CANCELLED:
						return false;
					default:
						throw new InvalidOperationException(string.Format("Unknown error in CredentialsDialog. Error: 0x{0:X}", ret));
				}

				if (this.EncryptPassword)
				{
					// Convert the password to a SecureString
					SecureString newPassword = StringBuilderToSecureString(password);

					// Clear the old password and set the new one (read-only)
					if (this.SecurePassword != null)
						this.SecurePassword.Dispose();
					newPassword.MakeReadOnly();
					this.SecurePassword = newPassword;
				}
				else
					this.Password = password.ToString();

				// Update other properties
				this.UserName = userName.ToString();
				this.SaveChecked = save;
				return true;
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