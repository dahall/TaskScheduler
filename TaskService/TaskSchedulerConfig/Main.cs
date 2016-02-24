using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Res = TaskSchedulerConfig.Properties.Resources;

namespace TaskSchedulerConfig
{
	public partial class Main : Form
	{
		private ListViewItem curSel;
		private Configurer fixer;
		private Dictionary<string, ListViewGroup> groups = new Dictionary<string, ListViewGroup>();
		private Validator v;

		public Main()
		{
			InitializeComponent();
		}

		private void AddLocalItem(string group, string text, object o = null, bool success = true, Predicate<object> fix = null, object fixParam = null, string tooltip = null)
		{
			ListViewGroup g = null;
			if (!string.IsNullOrEmpty(group) && !groups.TryGetValue(group, out g))
				groups.Add(group, g = localConfigList.Groups.Add(group, group));
			var i = new ListViewItem(string.Format(text, o), success ? 1 : 0);
			if (fix != null)
				i.Tag = new FixInfo(fix, fixParam);
			i.ToolTipText = tooltip;
			localConfigList.Items.Add(i);
			i.Group = g;
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void fixAllBtn_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in localConfigList.Items)
				if (item.ImageIndex == 0 && item.Tag != null)
					if (RunFix(item.Tag as FixInfo))
						item.ImageIndex = 1;
		}

		private void fixItemBtn_Click(object sender, EventArgs e)
		{
			var fi = curSel?.Tag as FixInfo;
			if (curSel != null && RunFix(fi))
				curSel.ImageIndex = 1;
		}

		private void localConfigList_SelectedIndexChanged(object sender, EventArgs e)
		{
			curSel = localConfigList.SelectedItems.Count > 0 ? localConfigList.SelectedItems[0] : null;
			fixItemBtn.Enabled = (curSel?.ImageIndex ?? 1) == 0 && curSel?.Tag != null;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			v = new Validator(null);
			fixer = new Configurer(v);
			retestBtn_Click(this, EventArgs.Empty);
		}

		private void retestBtn_Click(object sender, EventArgs e)
		{
			localConfigList.Items.Clear();
			Predicate<Diagnostics.Diagnostic> c = d => { try { return !d.Troubleshooter(null); } catch { return false; } };
			Predicate<Diagnostics.Diagnostic> f = d => { try { d.Resolution.Resolver(null); return !d.Troubleshooter(null); } catch { return false; } };

			foreach (var d in new Diagnostics(null))
			{
				AddLocalItem(null, d.Name, null, c(d), o => f(d), null, d.Description);
			}

			/*bool v2 = Environment.OSVersion.Version.Major > 5;

			AddLocalItem(Res.GroupGeneral, Res.UserIsAdmin, v.User, v.UserIsAdmin);
			AddLocalItem(Res.GroupGeneral, Res.UserIsServerOperator, v.User, v.UserIsServerOperator);
			AddLocalItem(Res.GroupGeneral, Res.UserIsBackupOperator, v.User, v.UserIsBackupOperator);
			AddLocalItem(Res.GroupGeneral, Res.FirewallEnabled, null, v.Firewall.Enabled, fixer.EnableFirewall);

			AddLocalItem(Res.GroupV1, Res.UserHasCorrectRights, v.User, v.UserIsAdmin || v.UserIsBackupOperator || v.UserIsServerOperator);
			AddLocalItem(Res.GroupV1, Res.FirewallRuleEnabled, Res.FileAndPrinterSharingRule, v.Firewall.Rules[Firewall.Rule.FileAndPrinterSharing], fixer.EnableFirewallRule, Firewall.Rule.FileAndPrinterSharing);

			if (v2)
			{
				AddLocalItem(Res.GroupV2, Res.FirewallRuleEnabled, Res.RemoteTaskManagementRule, v.Firewall.Rules[Firewall.Rule.RemoteTaskManagement], fixer.EnableFirewallRule, Firewall.Rule.RemoteTaskManagement);
				AddLocalItem(Res.GroupV2, Res.RemoteRegistryServiceRunning, null, v.RemoteRegistryServiceRunning, fixer.RunRemoteRegistryService);
			}*/

			bool hasError = false;
			foreach (ListViewItem item in localConfigList.Items)
				if (item.ImageIndex == 0 && item.Tag != null)
					hasError = true;
			fixAllBtn.Enabled = hasError;
		}

		private bool RunFix(FixInfo fi)
		{
			if (fi != null)
			{
				try
				{
					return fi.meth(fi.methParam);
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return false;
		}

		private class FixInfo
		{
			public Predicate<object> meth;
			public object methParam;

			public FixInfo(Predicate<object> m, object p)
			{
				meth = m; methParam = p;
			}
		}
	}
}