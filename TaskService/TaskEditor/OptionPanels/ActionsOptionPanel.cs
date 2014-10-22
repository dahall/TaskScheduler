using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class ActionsOptionPanel : OptionPanel
	{
		public ActionsOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			actionDeleteButton.Visible = actionEditButton.Visible = actionNewButton.Visible = parent.Editable;
			actionListView.Enabled = actionUpButton.Visible = actionDownButton.Visible = parent.Editable;
			actionListView.Items.Clear();
			if (td.Actions.Count > 0) // Added to make sure that if this is V1 and the ExecAction is invalid, that dialog won't show any actions.
			{
				foreach (Action act in td.Actions)
					AddActionToList(act, -1);
			}
			SetActionButtonState();
		}

		private void AddActionToList(Action act, int index)
		{
			ListViewItem lvi = new ListViewItem(new string[] {
					TaskEnumGlobalizer.GetString(act.ActionType),
					act.ToString() }) { Tag = act };
			if (index < 0)
				actionListView.Items.Add(lvi);
			else
				actionListView.Items.Insert(index, lvi);
		}

		private void SetActionButtonState()
		{
			actionUpButton.Enabled = actionDownButton.Enabled = actionListView.Items.Count > 1;
			actionNewButton.Enabled = parent.Editable && (parent.IsV2 || actionListView.Items.Count == 0);
			actionEditButton.Enabled = actionDeleteButton.Enabled = parent.Editable && actionListView.Items.Count > 0 && actionListView.SelectedIndices.Count > 0;
		}

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				td.Actions.RemoveAt(idx);
				actionListView.Items.RemoveAt(idx);
				SetActionButtonState();
			}
		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{
			if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != (this.actionListView.Items.Count - 1)))
			{
				int index = actionListView.SelectedIndices[0];
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				td.Actions.RemoveAt(index);
				actionListView.Items.Insert(index + 1, lvi);
				td.Actions.Insert(index + 1, aTemp as Action);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			int idx = actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;
			if (idx >= 0)
			{
				using (ActionEditDialog dlg = new ActionEditDialog(actionListView.Items[idx].Tag as Action))
				{
					if (!parent.IsV2 && !dlg.SupportV1Only) dlg.SupportV1Only = true;
					dlg.Text = EditorProperties.Resources.ActionDlgEditCaption;
					dlg.UseUnifiedSchedulingEngine = td.Settings.UseUnifiedSchedulingEngine;
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						actionListView.Items.RemoveAt(idx);
						td.Actions[idx] = dlg.Action;
						AddActionToList(dlg.Action, idx);
						actionListView.Items[idx].Selected = true;
					}
				}
			}
		}

		private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (parent.Editable)
				actionEditButton_Click(sender, EventArgs.Empty);
		}

		private void actionListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetActionButtonState();
		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{
			using (ActionEditDialog dlg = new ActionEditDialog { SupportV1Only = !parent.IsV2 })
			{
				dlg.Text = EditorProperties.Resources.ActionDlgNewCaption;
				dlg.UseUnifiedSchedulingEngine = td.Settings.UseUnifiedSchedulingEngine;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					td.Actions.Add(dlg.Action);
					AddActionToList(dlg.Action, -1);
					SetActionButtonState();
				}
			}
		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{
			if ((this.actionListView.SelectedIndices.Count == 1) && (this.actionListView.SelectedIndices[0] != 0))
			{
				int index = actionListView.SelectedIndices[0];
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				td.Actions.RemoveAt(index);
				actionListView.Items.Insert(index - 1, lvi);
				td.Actions.Insert(index - 1, aTemp);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}
	}
}
