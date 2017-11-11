using Microsoft.Win32.TaskScheduler.EditorProperties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal partial class ActionCollectionUI : UserControl, ITaskEditorUIElement
	{
		private ITaskDefinitionEditor editor;
		private bool modern;

		public ActionCollectionUI()
		{
			InitializeComponent();
		}

		/// <summary>Gets or sets the available actions.</summary>
		/// <value>The available actions.</value>
		[DefaultValue(typeof(AvailableActions), nameof(AvailableActions.AllActions)), Category("Appearance")]
		public AvailableActions AvailableActions { get; set; } = AvailableActions.AllActions;

		/// <summary>Gets or sets a value indicating whether a button is shown when editing an action that allows user to execute the current action.</summary>
		/// <value><c>true</c> if button is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a button is shown when editing an action that allows user to execute the current action.")]
		public bool ShowActionRunButton { get; set; }

		[DefaultValue(false), Category("Appearance")]
		public bool ShowPowerShellConversionCheck
		{
			get => allowPowerShellConvCheck.Visible;
			set => allowPowerShellConvCheck.Visible = value;
		}

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

		private Action SelectedAction
		{
			get { var idx = SelectedIndex; return idx == -1 ? null : actionListView.Items[idx].Tag as Action; }
		}

		private bool SelectedActionIsAvailable => SelectedIndex != -1 && AvailableActions.IsFlagSet(TypeToAv(SelectedAction.ActionType));

		private int SelectedIndex => actionListView.SelectedIndices.Count > 0 ? actionListView.SelectedIndices[0] : -1;

		private bool SetActionEditDialogV1 => !editor.IsV2 && !editor.TaskDefinition.Actions.PowerShellConversion.IsFlagSet(PowerShellActionPlatformOption.Version1);

		public void RefreshState()
		{
			if (editor == null)
				editor = this.GetParent<ITaskDefinitionEditor>();
			if (editor?.TaskDefinition == null) return;
			actionDeleteButton.Visible = actionEditButton.Visible = actionNewButton.Visible = editor.Editable;
			actionListView.Enabled = allowPowerShellConvCheck.Enabled = editor.Editable;
			allowPowerShellConvCheck.Checked = editor.TaskDefinition.Actions.PowerShellConversion.IsFlagSet(PowerShellActionPlatformOption.Version1);
			actionListView.BeginUpdate();
			actionListView.View = modern ? View.Tile : View.Details;
			actionListView.Items.Clear();
			if (editor.TaskDefinition.Actions.Count > 0) // Added to make sure that if this is V1 and the ExecAction is invalid, that dialog won't show any actions.
			{
				foreach (var act in editor.TaskDefinition.Actions)
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

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			editor = this.GetParent<ITaskDefinitionEditor>();
			RefreshState();
		}

		private static AvailableActions TypeToAv(TaskActionType actionType) => (AvailableActions)Enum.Parse(typeof(AvailableActions), actionType.ToString());

		private void actionDeleteButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx >= 0)
			{
				editor.TaskDefinition.Actions.RemoveAt(idx);
				actionListView.Items.RemoveAt(idx);
				SetActionButtonState();
			}
		}

		private void actionDownButton_Click(object sender, EventArgs e)
		{
			var index = SelectedIndex;
			if (index <= -1 || index >= actionListView.Items.Count - 1) return;
			actionListView.BeginUpdate();
			var lvi = actionListView.Items[index];
			var aTemp = ((Action)lvi.Tag).Clone() as Action;
			actionListView.Items.RemoveAt(index);
			editor.TaskDefinition.Actions.RemoveAt(index);
			actionListView.Items.Insert(index + 1, lvi);
			editor.TaskDefinition.Actions.Insert(index + 1, aTemp);
			lvi.Tag = aTemp;
			actionListView.EndUpdate();
		}

		private void actionEditButton_Click(object sender, EventArgs e)
		{
			var idx = SelectedIndex;
			if (idx < 0) return;
			using (var dlg = GetActionEditDialog(Resources.ActionDlgEditCaption, SelectedAction))
			{
				if (dlg.ShowDialog() != DialogResult.OK || dlg.Action == null) return;
				actionListView.Items.RemoveAt(idx);
				editor.TaskDefinition.Actions[idx] = dlg.Action;
				AddActionToList(dlg.Action, idx);
				actionListView.Items[idx].Selected = true;
			}
		}

		private void actionListView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (editor.Editable && SelectedActionIsAvailable)
				actionEditButton_Click(sender, EventArgs.Empty);
		}

		private void actionListView_Reordered(object sender, ListViewReorderedEventArgs e)
		{
			var aTemp = editor.TaskDefinition.Actions[e.OldIndex].Clone() as Action;
			editor.TaskDefinition.Actions.RemoveAt(e.OldIndex);
			editor.TaskDefinition.Actions.Insert(e.NewIndex, aTemp);
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
			using (var dlg = GetActionEditDialog(Resources.ActionDlgNewCaption))
			{
				if (dlg.ShowDialog() != DialogResult.OK || dlg.Action == null) return;
				editor.TaskDefinition.Actions.Add(dlg.Action);
				AddActionToList(dlg.Action, -1);
				SetActionButtonState();
			}
		}

		private void actionUpButton_Click(object sender, EventArgs e)
		{
			var index = SelectedIndex;
			if (index <= 0) return;
			actionListView.BeginUpdate();
			var lvi = actionListView.Items[index];
			var aTemp = ((Action)lvi.Tag).Clone() as Action;
			actionListView.Items.RemoveAt(index);
			editor.TaskDefinition.Actions.RemoveAt(index);
			actionListView.Items.Insert(index - 1, lvi);
			editor.TaskDefinition.Actions.Insert(index - 1, aTemp);
			lvi.Tag = aTemp;
			actionListView.EndUpdate();
		}

		private void AddActionToList(Action act, int index)
		{
			var imgIdx = (int)act.ActionType;
			if (imgIdx > 0) imgIdx -= 4;
			var txt = act.ToString();
			var lvi = new ListViewItem(new[] { TaskEnumGlobalizer.GetString(act.ActionType), txt }, imgIdx) { Tag = act, ToolTipText = txt };
			if (index < 0)
				actionListView.Items.Add(lvi);
			else
				actionListView.Items.Insert(index, lvi);
		}

		private void allowPowerShellConvCheck_CheckedChanged(object sender, EventArgs e)
		{
			editor.TaskDefinition.Actions.PowerShellConversion = allowPowerShellConvCheck.Checked ? PowerShellActionPlatformOption.All : PowerShellActionPlatformOption.Version2;
		}

		private ActionEditDialog GetActionEditDialog(string caption, Action a = null)
		{
			return new ActionEditDialog
			{
				Action = a,
				AllowRun = ShowActionRunButton,
				AvailableActions = AvailableActions,
				StartPosition = FormStartPosition.CenterParent,
				SupportV1Only = SetActionEditDialogV1,
				Text = caption,
				UseUnifiedSchedulingEngine = editor.TaskDefinition.Settings.UseUnifiedSchedulingEngine
			};
		}

		private void InitializeModernImages()
		{
			imageList.Images.Add(Resources.ActionTypeExecuteImage, Color.Transparent);
			imageList.Images.Add(Resources.ActionTypeComHandlerImage, Color.Transparent);
			imageList.Images.Add(Resources.ActionTypeSendEmailImage, Color.Transparent);
			imageList.Images.Add(Resources.ActionTypeShowMessageImage, Color.Transparent);
		}

		private void SetActionButtonState()
		{
			var editable = editor.Editable;
			var selectedIndex = SelectedIndex;
			upDownTableLayoutPanel.Visible = moveUpToolStripMenuItem.Visible = moveDownToolStripMenuItem.Visible = editable;
			if (editable)
			{
				actionUpButton.Enabled = moveUpToolStripMenuItem.Visible = selectedIndex > 0;
				actionDownButton.Enabled = moveDownToolStripMenuItem.Visible = selectedIndex > -1 && selectedIndex < actionListView.Items.Count - 1;
			}
			actionNewButton.Enabled = newActionToolStripMenuItem.Visible = editable && (editor.IsV2 || actionListView.Items.Count == 0 || editor.TaskDefinition.Actions.PowerShellConversion.IsFlagSet(PowerShellActionPlatformOption.Version1));
			actionEditButton.Enabled = actionDeleteButton.Enabled = editActionToolStripMenuItem.Visible = deleteActionToolStripMenuItem.Visible = editable && SelectedActionIsAvailable;
		}
	}
}