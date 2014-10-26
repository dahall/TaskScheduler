using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	public partial class ActionCollectionUI : UserControl, ITaskEditorUIElement
	{
		ITaskDefinitionEditor editor;

		public ActionCollectionUI()
		{
			InitializeComponent();
		}

		private int SelectedIndex { get { return actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1; } }

		public void RefreshState()
		{
			if (editor == null)
				editor = this.GetParent<ITaskDefinitionEditor>();
			if (editor != null && editor.TaskDefinition != null)
			{
				actionListView.Enabled = editor.Editable;
				actionListView.Items.Clear();
				if (editor.TaskDefinition.Actions.Count > 0) // Added to make sure that if this is V1 and the ExecAction is invalid, that dialog won't show any actions.
				{
					foreach (Action act in editor.TaskDefinition.Actions)
						AddActionToList(act, -1);
				}
				SetActionButtonState();
			}
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			editor = this.GetParent<ITaskDefinitionEditor>();
			RefreshState();
		}

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = SelectedIndex;
			if (idx >= 0)
			{
				editor.TaskDefinition.Actions.RemoveAt(idx);
				actionListView.Items.RemoveAt(idx);
				SetActionButtonState();
			}
		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{
			int index = SelectedIndex;
			if (index > -1 && index < this.actionListView.Items.Count - 1)
			{
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				editor.TaskDefinition.Actions.RemoveAt(index);
				actionListView.Items.Insert(index + 1, lvi);
				editor.TaskDefinition.Actions.Insert(index + 1, aTemp as Action);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			int idx = SelectedIndex;
			if (idx >= 0)
			{
				using (ActionEditDialog dlg = new ActionEditDialog(actionListView.Items[idx].Tag as Action))
				{
					if (!editor.IsV2 && !dlg.SupportV1Only) dlg.SupportV1Only = true;
					dlg.Text = EditorProperties.Resources.ActionDlgEditCaption;
					dlg.UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine;
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						actionListView.Items.RemoveAt(idx);
						editor.TaskDefinition.Actions[idx] = dlg.Action;
						AddActionToList(dlg.Action, idx);
						actionListView.Items[idx].Selected = true;
					}
				}
			}
		}

		private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editor.Editable)
				actionEditButton_Click(sender, EventArgs.Empty);
		}

		void actionListView_Reordered(object sender, ListViewReorderedEventArgs e)
		{
			Action aTemp = editor.TaskDefinition.Actions[e.OldIndex].Clone() as Action;
			editor.TaskDefinition.Actions.RemoveAt(e.OldIndex);
			editor.TaskDefinition.Actions.Insert(e.NewIndex, aTemp as Action);
		}

		private void actionListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetActionButtonState();
		}

		private void actionListView_SizeChanged(object sender, EventArgs e)
		{
			actionListView.AdjustColumnToFill();
		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{
			using (ActionEditDialog dlg = new ActionEditDialog { SupportV1Only = !editor.IsV2 })
			{
				dlg.Text = EditorProperties.Resources.ActionDlgNewCaption;
				dlg.UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					editor.TaskDefinition.Actions.Add(dlg.Action);
					AddActionToList(dlg.Action, -1);
					SetActionButtonState();
				}
			}
		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{
			int index = SelectedIndex;
			if (index > 0)
			{
				actionListView.BeginUpdate();
				ListViewItem lvi = this.actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				editor.TaskDefinition.Actions.RemoveAt(index);
				actionListView.Items.Insert(index - 1, lvi);
				editor.TaskDefinition.Actions.Insert(index - 1, aTemp);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
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
			bool editable = this.editor.Editable;
			int selectedIndex = SelectedIndex;
			upDownTableLayoutPanel.Visible = moveUpToolStripMenuItem.Visible = moveDownToolStripMenuItem.Visible = editable;
			if (editable)
			{
				actionUpButton.Enabled = moveUpToolStripMenuItem.Visible = selectedIndex > 0;
				actionDownButton.Enabled = moveDownToolStripMenuItem.Visible = selectedIndex > -1 && selectedIndex < actionListView.Items.Count - 1;
			}
			actionNewButton.Enabled = newActionToolStripMenuItem.Visible = editable && (editor.IsV2 || actionListView.Items.Count == 0);
			actionEditButton.Enabled = actionDeleteButton.Enabled = editActionToolStripMenuItem.Visible = deleteActionToolStripMenuItem.Visible = editable && selectedIndex > -1;
		}
	}

	internal static class ListViewExtensions
	{
		public static void AdjustColumnToFill(this ListView lvw, int columnIndex = -1)
		{
			int nWidth = lvw.ClientSize.Width; // Get width of client area.
			int idx = columnIndex == -1 ? lvw.Columns.Count - 1 : columnIndex;

			// Loop through all columns except the last one.
			for (int i = 0; i < lvw.Columns.Count; i++)
			{
				// Subtract width of the column from the width of the client area.
				if (i != idx)
					nWidth -= lvw.Columns[i].Width;

				// If the width goes below 1, then no need to keep going
				// because the last column can't be sized to fit due to
				// the widths of the columns before it.
				if (nWidth < 1)
					break;
			};

			// If there is any width remaining, that will be the width of the last column.
			if (nWidth > 0)
				lvw.Columns[idx].Width = nWidth;
		}
	}
}
