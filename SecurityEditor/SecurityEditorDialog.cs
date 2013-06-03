using System;
using System.ComponentModel;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace SecurityEditor
{
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog that allows editing of a Security Descriptor."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true)]
	public partial class SecurityEditorDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private string objName;
		private string sd;
		private FileSecurity sec = new FileSecurity();

		public SecurityEditorDialog()
		{
			InitializeComponent();
			try
			{
				using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MMC\SnapIns\FX:{c7b8fb06-bfe1-4c2e-9217-7a69a95bbac4}"))
					helpProvider1.HelpNamespace = key.GetValue("HelpTopic", string.Empty).ToString();
			}
			catch { }
		}

		public string ObjectName
		{
			get { return objName; }
			set
			{
				objName = value;
				aclEditor1.ObjectName = aclEditor2.ObjectName = ownerEditor1.ObjectName = value;
			}
		}

		public string SecurityDescriptorSddlForm
		{
			get { return sd; }
			set
			{
				sd = value;
				sec.SetSecurityDescriptorSddlForm(sd);

				aclEditor1.ObjectSecurity = sec;
				aclEditor2.ObjectSecurity = sec;
				ownerEditor1.Identity = sec.GetOwner(typeof(NTAccount));
			}
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			sd = sec.GetSecurityDescriptorSddlForm(AccessControlSections.All);
			Close();
		}

		internal static string GetLocalizedString(object obj)
		{
			string key = obj.ToString();
			string[] keys;
			if (obj is Enum)
			{
				string[] res = key.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < res.Length; i++)
					res[i] = obj.GetType().Name + res[i];
				keys = res;
			}
			else
				keys = new string[] { key };
			for (int i = 0; i < keys.Length; i++)
			{
				try
				{
					string ret = Properties.Resources.ResourceManager.GetString(keys[i], System.Globalization.CultureInfo.CurrentUICulture);
					if (!string.IsNullOrEmpty(ret))
						keys[i] = ret;
				}
				catch { }
			}
			return string.Join(", ", keys);
		}
	}
}
