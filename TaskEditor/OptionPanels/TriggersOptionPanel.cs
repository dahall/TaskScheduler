using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class TriggersOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public TriggersOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			triggerDeleteButton.Visible = triggerEditButton.Visible = triggerNewButton.Visible = parent.Editable;
			triggerListView.Enabled = parent.Editable;
			triggerListView.Items.Clear();
			foreach (Trigger tr in td.Triggers)
			{
				AddTriggerToList(tr, -1);
			}
			SetTriggerButtonState();
		}

		private void AddTriggerToList(Trigger tr, int index)
		{
			ListViewItem lvi = new ListViewItem(new string[] {
					TaskEnumGlobalizer.GetString(tr.TriggerType),
					tr.ToString(),
					tr.Enabled ? EditorProperties.Resources.Enabled : EditorProperties.Resources.Disabled
				});
			if (index < 0)
				triggerListView.Items.Add(lvi);
			else
				triggerListView.Items.Insert(index, lvi);
		}

		private void SetTriggerButtonState()
		{
			triggerNewButton.Enabled = parent.Editable;
			triggerEditButton.Enabled = triggerDeleteButton.Enabled = parent.Editable && triggerListView.Items.Count > 0 && triggerListView.SelectedIndices.Count > 0;
		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				td.Triggers.RemoveAt(idx);
				triggerListView.Items.RemoveAt(idx);
			}
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			int idx = triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				if (td.Triggers[idx].TriggerType == TaskTriggerType.Custom)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_CannotEditTrigger, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.None);
					return;
				}

				using (TriggerEditDialog dlg = new TriggerEditDialog(td.Triggers[idx], td.Settings.Compatibility < TaskCompatibility.V2))
				{
					dlg.UseUnifiedSchedulingEngine = td.Settings.UseUnifiedSchedulingEngine;
					dlg.TargetServer = parent.TaskService.TargetServer;
					dlg.Text = EditorProperties.Resources.TriggerDlgEditCaption;
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						triggerListView.Items.RemoveAt(idx);
						td.Triggers[idx] = dlg.Trigger;
						AddTriggerToList(dlg.Trigger, idx);
						triggerListView.Items[idx].Selected = true;
					}
				}
			}
		}

		private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (parent.Editable)
				triggerEditButton_Click(sender, EventArgs.Empty);
		}

		private void triggerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetTriggerButtonState();
		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{
			using (TriggerEditDialog dlg = new TriggerEditDialog(null, td.Settings.Compatibility < TaskCompatibility.V2))
			{
				dlg.UseUnifiedSchedulingEngine = td.Settings.UseUnifiedSchedulingEngine;
				dlg.TargetServer = parent.TaskService.TargetServer;
				dlg.Text = EditorProperties.Resources.TriggerDlgNewCaption;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					td.Triggers.Add(dlg.Trigger);
					AddTriggerToList(dlg.Trigger, -1);
				}
			}
		}

	}
}
