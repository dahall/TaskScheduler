using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class ActionCollectionUI : UserControl, ITaskEditorUIElement
	{
		ITaskDefinitionEditor editor;
		bool modern = false;

		public ActionCollectionUI()
		{
			InitializeComponent();
		}

		[DefaultValue(false), Category("Appearance")]
		public bool UseModernUI
		{
			get { return modern;}
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
			imageList.Images.Add(EditorProperties.Resources.ActionTypeExecuteImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.ActionTypeComHandlerImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.ActionTypeSendEmailImage, Color.Transparent);
			imageList.Images.Add(EditorProperties.Resources.ActionTypeShowMessageImage, Color.Transparent);
		}

		private int SelectedIndex => actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;

		public void RefreshState()
		{
			if (editor == null)
				editor = this.GetParent<ITaskDefinitionEditor>();
			if (editor != null && editor.TaskDefinition != null)
			{
				actionDeleteButton.Visible = actionEditButton.Visible = actionNewButton.Visible = editor.Editable;
				actionListView.Enabled = editor.Editable;
				actionListView.BeginUpdate();
				actionListView.View = modern ? View.Tile : View.Details;
				actionListView.Items.Clear();
				if (editor.TaskDefinition.Actions.Count > 0) // Added to make sure that if this is V1 and the ExecAction is invalid, that dialog won't show any actions.
				{
					foreach (Action act in editor.TaskDefinition.Actions)
						AddActionToList(act, -1);
				}
				if (modern)
				{
					actionListView.Alignment = ListViewAlignment.Top;
					actionListView.AdjustTileToWidth();
					NativeMethods.SendMessage(actionListView.Handle, (uint)NativeMethods.ListViewMessage.SetExtendedListViewStyle, new IntPtr(0x200000), new IntPtr(0x200000));
				}
				else
				{
					actionListView.Alignment = ListViewAlignment.Top;
					if (actionListView.Items.Count > 0)
						actionListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
					actionListView.AdjustColumnToFill();
				}
				actionListView.EndUpdate();
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
			if (index > -1 && index < actionListView.Items.Count - 1)
			{
				actionListView.BeginUpdate();
				ListViewItem lvi = actionListView.Items[index];
				Action aTemp = ((Action)lvi.Tag).Clone() as Action;
				actionListView.Items.RemoveAt(index);
				editor.TaskDefinition.Actions.RemoveAt(index);
				actionListView.Items.Insert(index + 1, lvi);
				editor.TaskDefinition.Actions.Insert(index + 1, aTemp as Action);
				lvi.Tag = aTemp;
				actionListView.EndUpdate();
			}
		}

		private bool SetActionEditDialogV1 => (!editor.IsV2 && (editor.TaskDefinition.Actions.PowerShellConversion & PowerShellActionPlatformOption.Version1) == 0);

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			int idx = SelectedIndex;
			if (idx >= 0)
			{
				using (ActionEditDialog dlg = new ActionEditDialog(actionListView.Items[idx].Tag as Action) { SupportV1Only = SetActionEditDialogV1 })
				{
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
			if (modern)
				actionListView.AdjustTileToWidth();
			else
				actionListView.AdjustColumnToFill();
		}

		private void actionNewButton_Click(object sender, EventArgs e)
		{
			using (ActionEditDialog dlg = new ActionEditDialog { SupportV1Only = SetActionEditDialogV1 })
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
				ListViewItem lvi = actionListView.Items[index];
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
			int imgIdx = (int)act.ActionType;
			if (imgIdx > 0) imgIdx -= 4;
			string txt = act.ToString();
			ListViewItem lvi = new ListViewItem(new string[] {
					TaskEnumGlobalizer.GetString(act.ActionType),
					txt }, imgIdx) { Tag = act, ToolTipText = txt };
			if (index < 0)
				actionListView.Items.Add(lvi);
			else
				actionListView.Items.Insert(index, lvi);
		}

		private void SetActionButtonState()
		{
			bool editable = editor.Editable;
			int selectedIndex = SelectedIndex;
			upDownTableLayoutPanel.Visible = moveUpToolStripMenuItem.Visible = moveDownToolStripMenuItem.Visible = editable;
			if (editable)
			{
				actionUpButton.Enabled = moveUpToolStripMenuItem.Visible = selectedIndex > 0;
				actionDownButton.Enabled = moveDownToolStripMenuItem.Visible = selectedIndex > -1 && selectedIndex < actionListView.Items.Count - 1;
			}
			actionNewButton.Enabled = newActionToolStripMenuItem.Visible = editable && (editor.IsV2 || actionListView.Items.Count == 0 || (editor.TaskDefinition.Actions.PowerShellConversion & PowerShellActionPlatformOption.Version1) != 0);
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

		public static void AdjustTileToWidth(this ListView lvw, int maxLines = 1, int iconSpacing = 4)
		{
			const string str = "Wg";
			var lvTVInfo = new NativeMethods.LVTILEVIEWINFO(0) { IconTextSpacing = iconSpacing, MaxTextLines = maxLines };
			var sb = new System.Text.StringBuilder(str);
			for (int i = 0; i < maxLines; i++)
				sb.Append("\r" + str);
			using (Graphics g = lvw.CreateGraphics())
				lvTVInfo.TileSize = new Size(lvw.ClientSize.Width, Math.Max(lvw.LargeImageList.ImageSize.Height, TextRenderer.MeasureText(g, sb.ToString(), lvw.Font).Height));
			NativeMethods.SendMessage(lvw.Handle, NativeMethods.ListViewMessage.SetTileViewInfo, 0, lvTVInfo);
			//var lvTVInfo = new NativeMethods.LVTILEVIEWINFO(0) { TileWidth = lvw.ClientSize.Width };
			//NativeMethods.SendMessage(lvw.Handle, NativeMethods.ListViewMessage.SetTileViewInfo, 0, lvTVInfo);
			//NativeMethods.SendMessage(lvw.Handle, (uint)NativeMethods.ListViewMessage.SetExtendedListViewStyle, new IntPtr(0x200000), new IntPtr(0x200000));
		}
	}
}
