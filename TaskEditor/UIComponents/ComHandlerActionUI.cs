using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[System.ComponentModel.DefaultEvent("KeyValueChanged"), System.ComponentModel.DefaultProperty("Action")]
	internal partial class ComHandlerActionUI : UserControl, IActionHandler
	{
		public ComHandlerActionUI()
		{
			InitializeComponent();
			errorProvider.SetIconPadding(comCLSIDText, -18);
			comCLSIDText.TextChanged += (sender, args) => { if (!comCLSIDText.ReadOnly) KeyValueChanged?.Invoke(this, EventArgs.Empty); };
		}

		public event EventHandler KeyValueChanged;

		[System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public Action Action
		{
			get { return new ComHandlerAction(ComCLSID, comDataText.TextLength > 0 ? comDataText.Text : null); }
			set
			{
				var ca = value as ComHandlerAction;
				if (ca == null) return;
				ComCLSID = ca.ClassId;
				comDataText.Text = ca.Data;
			}
		}

		private Guid ComCLSID
		{
			get
			{
				if (comCLSIDText.ReadOnly)
					try { return new Guid(toolTip.GetToolTip(comCLSIDText)); } catch { }
				else
					try { return new Guid(comCLSIDText.Text); } catch { }
				return Guid.Empty;
			}
			set
			{
				if (value == Guid.Empty)
				{
					comCLSIDText.Clear();
					toolTip.SetToolTip(comCLSIDText, null);
				}
				else
				{
					toolTip.SetToolTip(comCLSIDText, value.ToString());
					comCLSIDText.Text = GetNameForCLSID(value) ?? value.ToString();
				}

				KeyValueChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		public bool CanValidate => ComCLSID != Guid.Empty;

		public bool ValidateFields() => true;

		public void Run()
		{
			TaskService.RunComHandlerActionAsync(ComCLSID, i => { }, comDataText.TextLength == 0 ? null : comDataText.Text, -1, (i, m) => { });
			//var strs = new List<string>();
			//TaskService.RunComHandlerActionAsync(ComCLSID, i => { strs.Add($"Completed. Code={i}"); }, comDataText.TextLength == 0 ? null : comDataText.Text, -1, (i, m) => { strs.Add($"{i}% - {m}"); });
			//MessageBox.Show(this.ParentForm, string.Join("\r\n", strs.ToArray()), @"COM Handler Results", MessageBoxButtons.OK);
		}

		private static string GetNameForCLSID(Guid guid)
		{
			using (var k = Registry.ClassesRoot.OpenSubKey("CLSID", false))
			{
				if (k == null) return null;
				using (var k2 = k.OpenSubKey(guid.ToString("B"), false))
					return k2?.GetValue(null) as string;
			}
		}

		private void comCLSIDText_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (comCLSIDText.ReadOnly && comCLSIDText.TextLength == 0)
				return;

			Guid? og = null;
			if (comCLSIDText.ReadOnly)
				try { og = new Guid(toolTip.GetToolTip(comCLSIDText)); } catch { }
			else
				try { og = new Guid(comCLSIDText.Text); } catch { }
			if (og.HasValue)
				errorProvider.SetError(comCLSIDText, "");
			else
			{
				errorProvider.SetError(comCLSIDText, "Invalid GUID format for CLSID");
				e.Cancel = true;
			}
		}

		private void getCLSIDButton_Click(object sender, EventArgs e)
		{
			clsidMenuStrip.Show(getCLSIDButton, new System.Drawing.Point(0, getCLSIDButton.Height));
		}

		private void lookupCLSIDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				var dlg = new ComObjectSelectionDialog {SupportedInterface = new Guid("839D7762-5121-4009-9234-4F0D19394F04")};
				if (dlg.ShowDialog(this) != DialogResult.OK) return;
				comCLSIDText.ReadOnly = true;
				ComCLSID = dlg.CLSID;
			}
			catch { }
		}

		private void manuallyEnterCLSIDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			comCLSIDText.ReadOnly = false;
		}
	}
}