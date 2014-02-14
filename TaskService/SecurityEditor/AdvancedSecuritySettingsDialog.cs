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
	public partial class AdvancedSecuritySettingsDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private string objName = string.Empty;
		private NativeObjectSecurity sec;

		public AdvancedSecuritySettingsDialog()
		{
			InitializeComponent();
			try
			{
				using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MMC\SnapIns\FX:{c7b8fb06-bfe1-4c2e-9217-7a69a95bbac4}"))
					helpProvider1.HelpNamespace = key.GetValue("HelpTopic", string.Empty).ToString();
			}
			catch { }
			Editable = false;
		}

		[DefaultValue(false)]
		public bool Editable
		{
			get { return permEditor.Editable; }
			set
			{
				permEditor.Editable = audEditor.Editable = value;
				audHeaderLayoutPanel.Visible = audEditor.Visible = value;
				notEditableLayoutPanel.Visible = !value;
			}
		}

		[DefaultValue("")]
		public string ObjectName
		{
			get { return objName; }
			set
			{
				objName = value;
				this.objNameText.Text = objName;
			}
		}

		public void Initialize(System.IO.FileInfo file, bool editable = true)
		{
			this.Initialize(file.FullName, file.GetAccessControl(), null, editable);
		}

		public void Initialize(string objName, System.Security.AccessControl.NativeObjectSecurity objSec, string targetComputer = null, bool editable = true)
		{
			this.ObjectName = objName;
			sec = objSec;
			this.TargetComputer = targetComputer;
			this.Editable = editable;
		}

		public void Initialize(Microsoft.Win32.TaskScheduler.Task task, bool editable = true)
		{
			this.Initialize(task.Name, new TaskSecurity(task), task.TaskService.TargetServer, editable);
		}

		public NativeObjectSecurity ObjectSecurity
		{
			get { return sec; }
			set
			{
				sec = value;
				permEditor.ObjectSecurity = sec;
				audEditor.ObjectSecurity = sec;
				ownerText.Text = new AccountInfo((SecurityIdentifier)sec.GetOwner(typeof(SecurityIdentifier))).ToString();
			}
		}

		public string TargetComputer
		{
			get { return permEditor.TargetComputer; }
			set
			{
				permEditor.TargetComputer = value;
				audEditor.TargetComputer = value;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.IsDesignMode())
			{
				audContinueBtn.SetElevationRequiredState(true);
			}
			var parentBackColor = this.GetTrueParentBackColor();
			objNameText.BackColor = ownerText.BackColor = intLevelText.BackColor = parentBackColor;
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okBtn_Click(object sender, System.EventArgs e)
		{
			// TODO: Gather sections and create new "sec"
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
