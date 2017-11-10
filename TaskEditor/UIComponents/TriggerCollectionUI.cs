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

		/// <summary>Gets or sets the available triggers.</summary>
		/// <value>The available triggers.</value>
		[DefaultValue(typeof(AvailableTriggers), nameof(AvailableTriggers.AllTriggers)), Category("Appearance")]
		public AvailableTriggers AvailableTriggers { get; set; } = AvailableTriggers.AllTriggers;

		[DefaultValue(false), Category("Appearance")]
		public bool UseModernUI
		{
			get => modern;
			set
			{
				if (modern == value) return;
				modern = value;
				if (!DesignMode && value && imageList.Images.Count == 0)
					InitializeModernImages();
				RefreshState();
			}
		}

		private Trigger SelectedTrigger
		{
			get { var idx = SelectedIndex; return idx == -1 ? null : triggerListView.Items[idx].Tag as Trigger; }
		}

		private bool SelectedTriggerIsAvailable => SelectedIndex != -1 && AvailableTriggers.IsFlagSet(TypeToAv(SelectedTrigger.TriggerType));

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

		private static AvailableTriggers TypeToAv(TaskTriggerType triggerType) => (AvailableTriggers)Enum.Parse(typeof(AvailableTriggers), triggerType.ToString());

		private void AddTriggerToList(Trigger tr, int index)
		{
			var imgIdx = (int)tr.TriggerType;
			var txt = tr.ToString();
			var lvi = new ListViewItem(new[] { TaskEnumGlobalizer.GetString(tr.TriggerType), txt, tr.Enabled ? EditorProperties.Resources.Enabled : EditorProperties.Resources.Disabled }, imgIdx) { Tag = tr, ToolTipText = txt };
			lvi = index < 0 ? triggerListView.Items.Add(lvi) : triggerListView.Items.Insert(index, lvi);
			if (!modern) return;
			var nlvi = new NativeMethods.LVITEM(lvi.Index) { VisibleTileColumns = new[] { 1 } };
			if (!tr.Enabled)
				nlvi.OverlayImageIndex = (uint)disabledOverlayImageIndex;
			NativeMethods.SendMessage(triggerListView.Handle, NativeMethods.ListViewMessage.SetItem, 0, nlvi);
		}

		private void enableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var i = SelectedIndex;
			if (i < 0) return;
			editor.TaskDefinition.Triggers[i].Enabled = !editor.TaskDefinition.Triggers[i].Enabled;
			RefreshState();
		}

		private TriggerEditDialog GetTriggerEditDialog(string caption, Trigger t = null)
		{
			return new TriggerEditDialog(t, editor.TaskDefinition.Settings.Compatibility < TaskCompatibility.V2)
			{
				AvailableTriggers = AvailableTriggers,
				TargetServer = editor.TaskService.TargetServer,
				Text = caption,
				UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine
			};
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

		private void SetTriggerButtonState()
		{
			var editable = editor.Editable;
			var idx = SelectedIndex;
			triggerNewButton.Enabled = newTriggerToolStripMenuItem.Visible = editable;
			var available = SelectedTriggerIsAvailable;
			triggerEditButton.Enabled = editTriggerToolStripMenuItem.Visible = triggerDeleteButton.Enabled = deleteTriggerToolStripMenuItem.Visible = editable && idx > -1 && available;
			if (idx >= 0 && available)
				enableToolStripMenuItem.Visible = !(disableToolStripMenuItem.Visible = editor.TaskDefinition.Triggers[idx].Enabled);
			else
				enableToolStripMenuItem.Visible = disableToolStripMenuItem.Visible = false;
		}

		private void triggerDeleteButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx < 0) return;
			editor.TaskDefinition.Triggers.RemoveAt(idx);
			triggerListView.Items.RemoveAt(idx);
		}

		private void triggerEditButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx < 0) return;
			/*if (editor.TaskDefinition.Triggers[idx].TriggerType == TaskTriggerType.Custom)
			{
				MessageBox.Show(this, EditorProperties.Resources.Error_CannotEditTrigger, EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.None);
				return;
			}*/
			using (var dlg = GetTriggerEditDialog(EditorProperties.Resources.TriggerDlgEditCaption, editor.TaskDefinition.Triggers[idx]))
			{
				if (dlg.ShowDialog() != DialogResult.OK) return;
				triggerListView.Items.RemoveAt(idx);
				editor.TaskDefinition.Triggers[idx] = dlg.Trigger;
				AddTriggerToList(dlg.Trigger, idx);
				triggerListView.Items[idx].Selected = true;
			}
		}

		private void triggerListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editor.Editable && SelectedTriggerIsAvailable)
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
			using (var dlg = GetTriggerEditDialog(EditorProperties.Resources.TriggerDlgNewCaption))
			{
				if (dlg.ShowDialog() != DialogResult.OK) return;
				editor.TaskDefinition.Triggers.Add(dlg.Trigger);
				AddTriggerToList(dlg.Trigger, -1);
			}
		}
	}
}