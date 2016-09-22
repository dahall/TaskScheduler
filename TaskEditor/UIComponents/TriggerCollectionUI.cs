using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class TriggerCollectionUI : UserControl, ITaskEditorUIElement
	{
		private int disabledOverlayImageIndex = -1;
		private ITaskDefinitionEditor editor;
		private bool modern;

		public TriggerCollectionUI()
		{
			InitializeComponent();
		}

		[DefaultValue(false), Category("Appearance")]
		public bool UseModernUI
		{
			get { return modern; }
			set
			{
				if (modern != value)
				{
					modern = value;
					if (!DesignMode && value && imageList.Images.Count == 0)
						InitializeModernImages();
					RefreshState();
				}
			}
		}

		private void InitializeModernImages()
		{
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeEventImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeTimeImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeDailyImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeWeeklyImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeMonthlyImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeMonthlyDOWImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeIdleImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeRegistrationImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeBootImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeLogonImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeLogonImage, Color.Transparent); // Added to make enum int line up
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeSessionStateChangeImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.TriggerTypeCustomImage, Color.Transparent);
			disabledOverlayImageIndex = imageList.AddOverlay(EditorProperties.Resources.TriggerTypeStateDisabled, Color.Transparent);
		}

		private int SelectedIndex => triggerListView.SelectedIndices.Count > 0 ? triggerListView.SelectedIndices[0] : -1;

		public void RefreshState()
		{
			if (editor == null)
				editor = this.GetParent<ITaskDefinitionEditor>();
			if (editor?.TaskDefinition != null)
			{
				triggerDeleteButton.Visible = triggerEditButton.Visible = triggerNewButton.Visible = editor.Editable;
				triggerListView.Enabled = editor.Editable;
				triggerListView.BeginUpdate();
				triggerListView.Items.Clear();
				triggerListView.View = modern ? View.Tile : View.Details;
				foreach (var tr in editor.TaskDefinition.Triggers)
				{
					AddTriggerToList(tr, -1);
				}
				if (modern)
				{
					triggerListView.Alignment = ListViewAlignment.Top;
					triggerListView.AdjustTileToWidth();
					NativeMethods.SendMessage(triggerListView.Handle, (uint)NativeMethods.ListViewMessage.SetExtendedListViewStyle, new IntPtr(0x200000), new IntPtr(0x200000));
				}
				else
				{
					triggerListView.Alignment = ListViewAlignment.Top;
					if (triggerListView.Items.Count > 0)
						triggerListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
					triggerListView.AdjustColumnToFill();
				}
				triggerListView.EndUpdate();
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
			var imgIdx = (int)tr.TriggerType;
			var txt = tr.ToString();
			var lvi = new ListViewItem(new [] {
					TaskEnumGlobalizer.GetString(tr.TriggerType), txt,
					tr.Enabled ? EditorProperties.Resources.Enabled : EditorProperties.Resources.Disabled
				}, imgIdx) { Tag = tr, ToolTipText = txt };
			lvi = index < 0 ? triggerListView.Items.Add(lvi) : triggerListView.Items.Insert(index, lvi);
			if (modern)
			{
				var nlvi = new NativeMethods.LVITEM(lvi.Index) { VisibleTileColumns = new[] { 1 } };
				if (!tr.Enabled)
					nlvi.OverlayImageIndex = 1;
				NativeMethods.SendMessage(triggerListView.Handle, NativeMethods.ListViewMessage.SetItem, 0, nlvi);
			}
		}

		private void SetTriggerButtonState()
		{
			var idx = SelectedIndex;
			triggerNewButton.Enabled = newTriggerToolStripMenuItem.Visible = editor.Editable;
			triggerEditButton.Enabled = editTriggerToolStripMenuItem.Visible = triggerDeleteButton.Enabled = deleteTriggerToolStripMenuItem.Visible = editor.Editable && idx > -1;
			if (idx >= 0)
				enableToolStripMenuItem.Visible = !(disableToolStripMenuItem.Visible = editor.TaskDefinition.Triggers[idx].Enabled);
			else
				enableToolStripMenuItem.Visible = disableToolStripMenuItem.Visible = false;

		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx >= 0)
			{
				editor.TaskDefinition.Triggers.RemoveAt(idx);
				triggerListView.Items.RemoveAt(idx);
			}
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx >= 0)
			{
				/*if (editor.TaskDefinition.Triggers[idx].TriggerType == TaskTriggerType.Custom)
				{
					MessageBox.Show(this, EditorProperties.Resources.Error_CannotEditTrigger, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.None);
					return;
				}*/

				using (var dlg = new TriggerEditDialog(editor.TaskDefinition.Triggers[idx], editor.TaskDefinition.Settings.Compatibility < TaskCompatibility.V2))
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
			if (modern)
				triggerListView.AdjustTileToWidth();
			else
				triggerListView.AdjustColumnToFill(1);
		}

		private void triggerNewButton_Click(object sender, EventArgs e)
		{
			using (var dlg = new TriggerEditDialog(null, editor.TaskDefinition.Settings.Compatibility < TaskCompatibility.V2))
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

		private void enableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var i = SelectedIndex;
			if (i >= 0)
			{
				editor.TaskDefinition.Triggers[i].Enabled = !editor.TaskDefinition.Triggers[i].Enabled;
				RefreshState();
			}
		}
	}
}
