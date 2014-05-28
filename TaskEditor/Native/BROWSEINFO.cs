using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[DllImport(SHELL32, CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern IntPtr SHBrowseForFolder(ref NativeMethods.BROWSEINFO lpbi);
	}

	internal static partial class NativeMethods
	{
		// BFFM
		/// <summary>
		/// Enumeration with dialog messages used by the Browse For Folder dialog box.
		/// </summary>
		public enum BrowseForFolderMessages : uint
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
		public enum BrowseInfoFlag : uint
		{
			BIF_RETURNONLYFSDIRS = 0x0001,	// For finding a folder to start document searching
			BIF_DONTGOBELOWDOMAIN = 0x0002,	// For starting the Find Computer
			BIF_STATUSTEXT = 0x0004,	// Top of the dialog has 2 lines of okText for BROWSEINFO.lpszTitle and
			// one line if this newDS is set.  Passing the statusMessage
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

		public delegate int BrowseCallBackProc(IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData);

		/// <summary>
		/// Structure used for the WIN32 API SHBrowseForFolder.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct BROWSEINFO
		{
			public IntPtr hwndOwner;
			public IntPtr pidlRoot;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string pszDisplayName;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpszTitle;
			public uint ulFlags;
			public BrowseCallBackProc lpfn;
			public IntPtr lParam;
			public int iImage;
		}
	}
}