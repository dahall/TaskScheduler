using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Microsoft.Win32.TaskScheduler
{
	class SecEdShim
	{
		static Type dlgType;
		static MethodInfo initMI, showDlgMI;
		static PropertyInfo sddlPI;
		object dlg;

		static SecEdShim()
		{
			try
			{
				System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom("SecurityEditor.dll");
				if (asm != null)
				{
					dlgType = asm.GetType("Community.Windows.Forms.AccessControlEditorDialog", false, false);
					if (dlgType != null)
					{
						initMI = dlgType.GetMethod("Initialize", new Type[] { typeof(object) });
						showDlgMI = dlgType.GetMethod("ShowDialog", new Type[] { typeof(System.Windows.Forms.IWin32Window) });
						sddlPI = dlgType.GetProperty("SDDL", typeof(string));
					}
				}
			}
			catch { dlgType = null; }
			if (dlgType != null && (initMI == null || showDlgMI == null || sddlPI == null))
				dlgType = null;
		}

		private SecEdShim()
		{
			dlg = Activator.CreateInstance(dlgType);
		}

		public string SecurityDescriptorSddlForm
		{
			get { return sddlPI.GetValue(dlg, null).ToString(); }
		}

		public void Initialize(object taskObj)
		{
			initMI.Invoke(dlg, new object[] { taskObj });
		}

		public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner)
		{
			return (System.Windows.Forms.DialogResult)showDlgMI.Invoke(dlg, new object[] { owner });
		}

		public static SecEdShim GetNew()
		{
			if (dlgType != null)
				return new SecEdShim();
			return null;
		}

		public static bool IsValid { get { return dlgType != null; } }
	}
}
