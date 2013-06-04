using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/*/// <summary>
	/// 
	/// </summary>
	public enum FolderBrowserDialogOptions
	{
		/// <summary></summary>
		Folders,
		/// <summary></summary>
		FoldersAndFiles,
		/// <summary></summary>
		Computers,
		/// <summary></summary>
		Printers
	}*/

	/// <summary>
	/// Class to let the user browse for a folder.
	/// </summary>
	[System.Drawing.ToolboxBitmap("Dialog.bmp"), Description("Dialog that browses network computers.")]
	public class FolderBrowserDialog2 : CommonDialog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FolderBrowserDialog2"/> class.
		/// </summary>
		public FolderBrowserDialog2()
		{
			Reset();
		}

		/// <summary>
		/// Gets a <see cref="FolderBrowserDialog2"/> that browses network computers.
		/// </summary>
		/// <returns>A <see cref="FolderBrowserDialog2"/> that browses network computers.</returns>
		public static FolderBrowserDialog2 ComputerBrowser
		{
			get
			{
				FolderBrowserDialog2 dlg = new FolderBrowserDialog2();
				dlg.BrowserFlag = (int)BrowseInfoFlag.BIF_BROWSEFORCOMPUTER;
				dlg.RootFolder = (Environment.SpecialFolder)0x12;
				return dlg;
			}
		}

		/// <summary>
		/// Gets a <see cref="FolderBrowserDialog2"/> that browses printers.
		/// </summary>
		/// <returns>A <see cref="FolderBrowserDialog2"/> that browses printers.</returns>
		public static FolderBrowserDialog2 PrinterBrowser
		{
			get
			{
				FolderBrowserDialog2 dlg = new FolderBrowserDialog2();
				dlg.BrowserFlag = (int)BrowseInfoFlag.BIF_BROWSEFORPRINTER;
				dlg.RootFolder = (Environment.SpecialFolder)4;
				return dlg;
			}
		}

		#region Enumerations

		// BFFM
		/// <summary>
		/// Enumeration with dialog messages used by the Browse For Folder dialog box.
		/// </summary>
		private enum BrowseForFolderMessages : uint
		{
			// statusMessage from browser
			BFFM_INITIALIZED = 1,
			BFFM_SELCHANGED = 2,
			BFFM_VALIDATEFAILEDA = 3,				// lParam:szPath ret:1(cont),0(EndDialog)
			BFFM_VALIDATEFAILEDW = 4,				// lParam:wzPath ret:1(cont),0(EndDialog)
			BFFM_IUNKNOWN = 5, 				// provides IUnknown to client. lParam: IUnknown*

			// messages to browser
			// 0x400 = WM_USER
			BFFM_SETSTATUSTEXTA = (0x0400 + 100),
			BFFM_ENABLEOK = (0x0400 + 101),
			BFFM_SETSELECTIONA = (0x0400 + 102),
			BFFM_SETSELECTIONW = (0x0400 + 103),
			BFFM_SETSTATUSTEXTW = (0x0400 + 104),
			BFFM_SETOKTEXT = (0x0400 + 105),	// Unicode only
			BFFM_SETEXPANDED = (0x0400 + 106)	// Unicode only
		}

		// BIF
		/// <summary>
		/// Flags enumeration to specify the dialog style.
		/// </summary>
		[Flags]
		private enum BrowseInfoFlag : uint
		{
			BIF_RETURNONLYFSDIRS = 0x0001,	// For finding a folder to start document searching
			BIF_DONTGOBELOWDOMAIN = 0x0002,	// For starting the Find Computer
			BIF_STATUSTEXT = 0x0004,	// Top of the dialog has 2 lines of okText for BROWSEINFO.lpszTitle and
			// one line if this flag is set.  Passing the statusMessage
			// BFFM_SETSTATUSTEXTA to the hwnd can set the rest of the okText.
			// This is not used with BIF_USENEWUI and BROWSEINFO.lpszTitle gets
			// all three lines of okText.
			BIF_RETURNFSANCESTORS = 0x0008,
			BIF_EDITBOX = 0x0010,	// Add an editbox to the dialog
			BIF_VALIDATE = 0x0020,	// insist on valid result (or CANCEL)
			BIF_NEWDIALOGSTYLE = 0x0040,	// Use the new dialog layout with the ability to resize
			// Caller needs to call OleInitialize() before using this API
			BIF_USENEWUI = (BIF_NEWDIALOGSTYLE | BIF_EDITBOX),
			BIF_BROWSEINCLUDEURLS = 0x0080,    // Allow URLs to be displayed or entered. (Requires BIF_USENEWUI)
			BIF_UAHINT = 0x0100,    // Add a UA hint to the dialog, in place of the edit box. May not be
			// combined with BIF_EDITBOX
			BIF_NONEWFOLDERBUTTON = 0x0200,    // Do not add the "New Folder" button to the dialog.  Only applicable
			// with BIF_NEWDIALOGSTYLE.
			BIF_NOTRANSLATETARGETS = 0x0400,    // don't traverse target as shortcut
			BIF_BROWSEFORCOMPUTER = 0x1000,	// Browsing for Computers.
			BIF_BROWSEFORPRINTER = 0x2000,	// Browsing for Printers
			BIF_BROWSEINCLUDEFILES = 0x4000,	// Browsing for Everything
			BIF_SHAREABLE = 0x8000		// sharable resources displayed (remote shares, requires BIF_USENEWUI)
		}

		#endregion Enumerations

		#region Delegates

		/// <summary>
		/// Signature for the BrowseCallBackProc callback.
		/// </summary>
		/// <param name="hwnd">Window handle of the browse dialog box.</param>
		/// <param name="uMsg">Dialog box event that generated the statusMessage.</param>
		/// <param name="lParam">Value whose meaning depends on the event specified in uMsg.</param>
		/// <param name="lpData">Application-defined value that was specified in the lParam member of the BROWSEINFO structure used in the call to SHBrowseForFolder.</param>
		/// <returns>Returns 0 except in the case of BFFM_VALIDATEFAILED. For that flag, returns 0 to dismiss the dialog or nonzero to keep the dialog displayed.</returns>
		private delegate int BrowseCallBackProc(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData);

		#endregion Delegates

		#region Events

		/// <summary>
		/// Occurs when dialog box has been initialized and primary values have been set.
		/// </summary>
		public event EventHandler<FolderBrowserDialogInitializedEventArgs> Initialized;

		/// <summary>
		/// Event that is raised when the user selects an invalid folder.
		/// </summary>
		public event EventHandler<InvalidFolderEventArgs> InvalidFolderSelected;

		/// <summary>
		/// Occurs when <see cref="SelectedPath"/> property has changed.
		/// </summary>
		public event PropertyChangedEventHandler SelectedPathChanged;

		#endregion Events

		#region Properties

		/// <summary>
		/// Gets or sets an additional settings flag.
		/// </summary>
		[DefaultValue(0), Category("Folder Browsing"), Localizable(false)]
		public int BrowserFlag
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the caption of the dialog box.
		/// </summary>
		[DefaultValue(""), Category("Folder Browsing"), Localizable(true)]
		public string Caption
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the description shown to the user.
		/// </summary>
		[DefaultValue(""), Category("Folder Browsing"), Localizable(true)]
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the text on the OK button.
		/// </summary>
		[DefaultValue(""), Category("Folder Browsing"), Localizable(true)]
		public string OkText
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the root folder.
		/// </summary>
		[DefaultValue(0), Localizable(false), Category("Folder Browsing")]
		public Environment.SpecialFolder RootFolder
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the path selected by the user. The initially selected path if set before 
		/// the dialog box is shown.
		/// </summary>
		[DefaultValue(""), Category("Folder Browsing"), Localizable(true)]
		public string SelectedPath
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets whether or not to show files as well.
		/// </summary>
		[DefaultValue(false), Localizable(false), Category("Folder Browsing")]
		public bool ShowFiles
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets whether or not to show an edit box for the folder path.
		/// </summary>
		[DefaultValue(false), Localizable(false), Category("Folder Browsing")]
		public bool ShowFolderPathEditBox
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets whether or not to show the new folder button.
		/// </summary>
		[DefaultValue(false), Localizable(false), Category("Folder Browsing")]
		public bool ShowNewFolderButton
		{
			get; set;
		}

		#endregion Properties

		#region Methods

		/// <summary>
		/// When overridden in a derived class, resets the properties of a common dialog box to their default values.
		/// </summary>
		public override void Reset()
		{
			this.BrowserFlag = 0;
			this.Caption = this.Description = this.OkText = this.SelectedPath = string.Empty;
			this.RootFolder = (Environment.SpecialFolder)0;
			this.ShowFiles = this.ShowFolderPathEditBox = this.ShowNewFolderButton = false;
		}

		/// <summary>
		/// Shows the dialog box to let the user browse for and select a folder.
		/// </summary>
		/// <param name="parentWindowHandle">The HWND of the parent window.</param>
		/// <returns>The selected folder or <c>null</c> if no folder was selected by the user.</returns>
		protected override bool RunDialog(IntPtr parentWindowHandle)
		{
			// Make sure OLE is initialized. This is a prerequisite for calling SHBrowseForFolder.
			NativeMethods.OleInitialize(IntPtr.Zero);

			IntPtr rpidl = IntPtr.Zero;
			NativeMethods.SHGetSpecialFolderLocation(parentWindowHandle, (int)this.RootFolder, out rpidl);
			if (rpidl == IntPtr.Zero)
			{
				NativeMethods.SHGetSpecialFolderLocation(parentWindowHandle, 0, out rpidl);
				if (rpidl == IntPtr.Zero)
					throw new InvalidOperationException("No root folder specified for FolderBrowserDialog2.");
			}

			if (Control.CheckForIllegalCrossThreadCalls && (Application.OleRequired() != System.Threading.ApartmentState.STA))
				throw new System.Threading.ThreadStateException("Debugging Exception Only. Thread must be STA.");

			BrowseInfoFlag browseInfoFlag = BrowseInfoFlag.BIF_NEWDIALOGSTYLE | BrowseInfoFlag.BIF_SHAREABLE;
			if (!ShowNewFolderButton)
				browseInfoFlag |= BrowseInfoFlag.BIF_NONEWFOLDERBUTTON;
			if (ShowFolderPathEditBox)
				browseInfoFlag |= BrowseInfoFlag.BIF_EDITBOX | BrowseInfoFlag.BIF_VALIDATE;
			if (ShowFiles)
				browseInfoFlag |= BrowseInfoFlag.BIF_BROWSEINCLUDEFILES;
			browseInfoFlag |= (BrowseInfoFlag)BrowserFlag;

			NativeMethods.BROWSEINFO bi;
			bi.hwndOwner = parentWindowHandle;
			bi.pidlRoot = rpidl;
			bi.pszDisplayName = new string('\0', 260);
			bi.lpszTitle = Description;
			bi.ulFlags = (uint)browseInfoFlag;
			bi.lpfn = new NativeMethods.BrowseCallBackProc(OnBrowseEvent);
			bi.lParam = IntPtr.Zero;
			bi.iImage = 0;

			StringBuilder sb = new StringBuilder(260);
			IntPtr pidl = IntPtr.Zero;
			try
			{
				pidl = NativeMethods.SHBrowseForFolder(ref bi);
				if (((browseInfoFlag & BrowseInfoFlag.BIF_BROWSEFORPRINTER) == BrowseInfoFlag.BIF_BROWSEFORPRINTER) ||
					((browseInfoFlag & BrowseInfoFlag.BIF_BROWSEFORCOMPUTER) == BrowseInfoFlag.BIF_BROWSEFORCOMPUTER))
				{
					this.SelectedPath = bi.pszDisplayName;
				}
				else
				{
					if (pidl == IntPtr.Zero || 0 == NativeMethods.SHGetPathFromIDList(pidl, sb))
						return false;
					this.SelectedPath = sb.ToString();
				}
				return true;
			}
			finally
			{
				if (rpidl != IntPtr.Zero)
					Marshal.FreeCoTaskMem(rpidl);
				if (pidl != IntPtr.Zero)
					Marshal.FreeCoTaskMem(pidl);
			}
		}

		/// <summary>
		/// Callback for Windows.
		/// </summary>
		/// <param name="hwnd">Window handle of the browse dialog box.</param>
		/// <param name="uMsg">Dialog box event that generated the statusMessage.</param>
		/// <param name="lParam">Value whose meaning depends on the event specified in uMsg.</param>
		/// <param name="lpData">Application-defined value that was specified in the lParam member of the BROWSEINFO structure used in the call to SHBrowseForFolder.</param>
		/// <returns>Returns 0 except in the case of BFFM_VALIDATEFAILED. For that flag, returns 0 to dismiss the dialog or nonzero to keep the dialog displayed.</returns>
		//[CLSCompliant(false)]
		private int OnBrowseEvent(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData)
		{
			BrowseForFolderMessages messsage = (BrowseForFolderMessages)uMsg;
			switch (messsage)
			{
				case BrowseForFolderMessages.BFFM_INITIALIZED:
					// Dialog is being initialized, so set the initial parameters
					if (!String.IsNullOrEmpty(Caption))
						NativeMethods.SetWindowText(hwnd, Caption);

					if (!String.IsNullOrEmpty(SelectedPath))
					{
						NativeMethods.SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETEXPANDED, (IntPtr)1, SelectedPath);
						NativeMethods.SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETSELECTIONW, (IntPtr)1, SelectedPath);
					}

					if (!String.IsNullOrEmpty(OkText))
						NativeMethods.SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETOKTEXT, (IntPtr)0, OkText);

					if (this.Initialized != null)
					{
						EventHandler<FolderBrowserDialogInitializedEventArgs> h = this.Initialized;
						h(this, new FolderBrowserDialogInitializedEventArgs(hwnd));
					}

					return 0;

				case BrowseForFolderMessages.BFFM_SELCHANGED:
					try
					{
						StringBuilder sb = new StringBuilder(260);
						if (lParam == IntPtr.Zero || 0 == NativeMethods.SHGetPathFromIDList(lParam, sb))
							return 0;
						this.SelectedPath = sb.ToString();
					}
					catch { return 0; }
					if (this.SelectedPathChanged != null)
					{
						PropertyChangedEventHandler h = this.SelectedPathChanged;
						h(this, new PropertyChangedEventArgs("SelectedPath"));
					}
					return 0;

				case BrowseForFolderMessages.BFFM_VALIDATEFAILEDA:
				case BrowseForFolderMessages.BFFM_VALIDATEFAILEDW:
					if (this.InvalidFolderSelected != null)
					{
						string folderName;
						if (messsage == BrowseForFolderMessages.BFFM_VALIDATEFAILEDA)
							folderName = Marshal.PtrToStringAnsi(lParam);
						else
							folderName = Marshal.PtrToStringUni(lParam);

						EventHandler<InvalidFolderEventArgs> h = this.InvalidFolderSelected;
						InvalidFolderEventArgs e = new InvalidFolderEventArgs(folderName, true);
						h(this, e);
						return (e.DismissDialog ? 0 : 1);
					}
					return 0;

				default:
					return 0;
			}
		}

		/// <summary>
		/// Enables or disables the OK button in the dialog box.
		/// </summary>
		/// <param name="hwnd">The hwnd of the dialog box.</param>
		/// <param name="isEnabled">Whether or not the OK button should be enabled.</param>
		private static void EnableOk(IntPtr hwnd, bool isEnabled)
		{
			NativeMethods.SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_ENABLEOK, (IntPtr)0, (IntPtr)(isEnabled ? 1 : 0));
		}

		/// <summary>
		/// Sets the status text of the dialog box.
		/// </summary>
		/// <param name="hwnd">The hwnd of the dialog box.</param>
		/// <param name="statusText">The status text to set.</param>
		private static void SetStatusText(IntPtr hwnd, string statusText)
		{
			NativeMethods.SendMessage(hwnd, (uint)BrowseForFolderMessages.BFFM_SETSTATUSTEXTW, (IntPtr)1, statusText);
		}

		#endregion Methods
	}

	/// <summary>
	/// Event arguments for when an invalid fodler is selected.
	/// </summary>
	public class InvalidFolderEventArgs : EventArgs
	{
		/// <summary>
		/// Constructs an instance.
		/// </summary>
		/// <param name="folderName">The name of the invalid folder.</param>
		/// <param name="dismissDialog">Whether or not to dismiss the dialog.</param>
		public InvalidFolderEventArgs(string folderName, bool dismissDialog)
		{
			FolderName = folderName;
			DismissDialog = dismissDialog;
		}

		/// <summary>
		/// Gets or sets whether or not to dismiss the dialog box.
		/// </summary>
		public bool DismissDialog { get; set; }

		/// <summary>
		/// Gets the name of the invalid folder.
		/// </summary>
		public string FolderName { get; private set; }
	}

	/// <summary>
	/// Event arguments for when the <see cref="FolderBrowserDialog2"/> has been initialized.
	/// </summary>
	public class FolderBrowserDialogInitializedEventArgs : EventArgs
	{
		/// <summary>
		/// The HWND of the dialog box.
		/// </summary>
		public readonly IntPtr hwnd;

		/// <summary>
		/// Initializes a new instance of the <see cref="FolderBrowserDialogInitializedEventArgs"/> class.
		/// </summary>
		/// <param name="hwnd">The HWND of the dialog box.</param>
		public FolderBrowserDialogInitializedEventArgs(IntPtr hwnd)
		{
			this.hwnd = hwnd;
		}
	}
}