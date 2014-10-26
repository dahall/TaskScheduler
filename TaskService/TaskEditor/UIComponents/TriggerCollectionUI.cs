using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	public partial class TriggerCollectionUI : UserControl, ITaskEditorUIElement
	{
		ITaskDefinitionEditor editor;

		public TriggerCollectionUI()
		{
			InitializeComponent();
		}

		private int SelectedIndex { get { return triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1; } }

		public void RefreshState()
		{
			if (editor == null)
				editor = this.GetParent<ITaskDefinitionEditor>();
			if (editor != null && editor.TaskDefinition != null)
			{
				triggerDeleteButton.Visible = triggerEditButton.Visible = triggerNewButton.Visible = editor.Editable;
				triggerListView.Enabled = editor.Editable;
				triggerListView.Items.Clear();
				foreach (Trigger tr in editor.TaskDefinition.Triggers)
				{
					AddTriggerToList(tr, -1);
				}
				SetTriggerButtonState();
			}
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			editor = this.GetParent<ITaskDefinitionEditor>();
			RefreshState();
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
			triggerNewButton.Enabled = editor.Editable;
			triggerEditButton.Enabled = triggerDeleteButton.Enabled = editor.Editable && SelectedIndex > -1;
		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			int idx = SelectedIndex;
			if (idx >= 0)
			{
				editor.TaskDefinition.Triggers.RemoveAt(idx);
				triggerListView.Items.RemoveAt(idx);
			}
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			int idx = SelectedIndex;
			if (idx >= 0)
			{
				if (editor.TaskDefinition.Triggers[idx].TriggerType == TaskTriggerType.Custom)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_CannotEditTrigger, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.None);
					return;
				}

				using (TriggerEditDialog dlg = new TriggerEditDialog(editor.TaskDefinition.Triggers[idx], editor.TaskDefinition.Settings.Compatibility < TaskCompatibility.V2))
				{
					dlg.UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine;
					dlg.TargetServer = editor.TaskService.TargetServer;
					dlg.Text = EditorProperties.Resources.TriggerDlgEditCaption;
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						triggerListView.Items.RemoveAt(idx);
						editor.TaskDefinition.Triggers[idx] = dlg.Trigger;
						AddTriggerToList(dlg.Trigger, idx);
						triggerListView.Items[idx].Selected = true;
					}
				}
			}
		}

		private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editor.Editable)
				triggerEditButton_Click(sender, EventArgs.Empty);
		}

		private void triggerListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetTriggerButtonState();
		}

		private void triggerListView_SizeChanged(object sender, EventArgs e)
		{
			triggerListView.AdjustColumnToFill(1);
		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{
			using (TriggerEditDialog dlg = new TriggerEditDialog(null, editor.TaskDefinition.Settings.Compatibility < TaskCompatibility.V2))
			{
				dlg.UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine;
				dlg.TargetServer = editor.TaskService.TargetServer;
				dlg.Text = EditorProperties.Resources.TriggerDlgNewCaption;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					editor.TaskDefinition.Triggers.Add(dlg.Trigger);
					AddTriggerToList(dlg.Trigger, -1);
				}
			}
		}
	}
}
