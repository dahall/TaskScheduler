namespace System.Windows.Forms
{
	internal static class TreeViewExtension
	{
		public static void SetExplorerTheme(this TreeView treeView, bool on = true)
		{
			// Make sure the TVS_NOHSCROLL style is set
			const int TVS_NOHSCROLL = 0x8000;
			treeView.SetStyle(TVS_NOHSCROLL);

			// Set explorer theme, set critical properties, and set extended styles
			Microsoft.Win32.NativeMethods.SetWindowTheme(treeView.Handle, "explorer", null);
			const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
			const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
			const int TVS_EX_AUTOHSCROLL = 0x0020;
			treeView.HotTracking = true;
			treeView.ShowLines = false;
			Microsoft.Win32.NativeMethods.SendMessage(treeView.Handle, TVM_SETEXTENDEDSTYLE, TVS_EX_FADEINOUTEXPANDOS | TVS_EX_AUTOHSCROLL, TVS_EX_FADEINOUTEXPANDOS | TVS_EX_AUTOHSCROLL);
		}
	}
}