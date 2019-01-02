using System;
using System.Reflection;
using Vanara.Extensions;

namespace Microsoft.Win32.TaskScheduler
{
	internal class SecEdShim
	{
		private static readonly Type dlgType;
		private static readonly MethodInfo init2MI;
		private static readonly MethodInfo initMI;
		private static readonly PropertyInfo sddlPI;
		private static readonly MethodInfo showDlgMI;
		private readonly object dlg;

		static SecEdShim()
		{
			try
			{
				dlgType = ReflectionExtensions.LoadType("Community.Windows.Forms.AccessControlEditorDialog", "SecurityEditor.dll");
				if (dlgType != null)
				{
					initMI = dlgType.GetMethod("Initialize", new[] { typeof(object) });
					init2MI = dlgType.GetMethod("Initialize", new[] { typeof(string), typeof(string), typeof(bool), typeof(System.Security.AccessControl.ResourceType), typeof(byte[]), typeof(string) });
					showDlgMI = dlgType.GetMethod("ShowDialog", new[] { typeof(System.Windows.Forms.IWin32Window) });
					sddlPI = dlgType.GetProperty("SDDL", typeof(string));
				}
			}
			catch { dlgType = null; }
			if (dlgType != null && (initMI == null || showDlgMI == null || sddlPI == null))
				dlgType = null;
		}

		private SecEdShim() => dlg = Activator.CreateInstance(dlgType);

		public static bool IsValid => dlgType != null;
		public string SecurityDescriptorSddlForm => sddlPI.GetValue(dlg, null).ToString();

		public static SecEdShim GetNew() => dlgType != null ? new SecEdShim() : null;

		public void Initialize(object taskObj) => initMI.Invoke(dlg, new[] { taskObj });

		public void Initialize(string displayName, bool isContainer, string targetServer, TaskSecurity taskSecurity)
		{
			var rt = (System.Security.AccessControl.ResourceType)99;
			init2MI.Invoke(dlg, new object[] { displayName, displayName, isContainer, rt, taskSecurity.GetSecurityDescriptorBinaryForm(), targetServer });
		}

		public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner) => (System.Windows.Forms.DialogResult)showDlgMI.Invoke(dlg, new object[] { owner });
	}
}