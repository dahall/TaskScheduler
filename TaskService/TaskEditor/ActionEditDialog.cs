using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// An editor that handles all Task actions.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog allowing the editing of a task action.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	public partial class ActionEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private Action action;
		private bool allowRun = false;
		private bool isV2 = true;
		private bool onAssignment = false;
		private bool useUnifiedSchedulingEngine = false;
		private UIComponents.IActionHandler curHandler = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionEditDialog"/> class.
		/// </summary>
		public ActionEditDialog()
		{
			InitializeComponent();
			ResetCombo();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionEditDialog"/> class with the provided action.
		/// </summary>
		/// <param name="action">The action.</param>
		public ActionEditDialog(Action action) : this()
		{
			Action = action;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		[DefaultValue(null), Browsable(false)]
		public Action Action
		{
			get { return action; }
			set
			{
				onAssignment = true;
				action = value;
				actionsCombo.SelectedIndex = actionsCombo.Items.IndexOf((long)action.ActionType);
				actionIdText.Text = action.Id;
				curHandler.Action = action;
				onAssignment = false;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show a button allowing the action to be run in real time.
		/// </summary>
		/// <value>
		///   <c>true</c> if allow run; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior")]
		public bool AllowRun
		{
			get { return allowRun; }
			set { allowRun = value; runActionBtn.Visible = value; }
		}

		/// <summary>
		/// Gets or sets the prompt text at the top of the dialog.
		/// </summary>
		/// <value>The text to use as a prompt.</value>
		[DefaultValue("You must specify what action this task will perform."), Category("Appearance")]
		public string Prompt
		{
			get { return promptLabel.Text; }
			set { promptLabel.Text = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this editor only supports V1 actions.
		/// </summary>
		/// <value><c>true</c> if supports V1 only; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool SupportV1Only
		{
			get { return !isV2; }
			set
			{
				if (value == isV2)
				{
					isV2 = !value;
					ResetCombo();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether dialog should restrict items to those available when using the Unified Scheduling Engine.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(false), Category("Behavior")]
		public bool UseUnifiedSchedulingEngine
		{
			get { return useUnifiedSchedulingEngine; }
			set
			{
				if (!isV2 && value)
					throw new NotSupportedException("Version 1.0 of the Task Scheduler library cannot use the Unified Scheduling Engine.");
				if (value != useUnifiedSchedulingEngine)
				{
					useUnifiedSchedulingEngine = value;
					ResetCombo();
				}
			}
		}

		private void actionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch ((TaskActionType)Convert.ToInt32(((DropDownCheckListItem)actionsCombo.SelectedItem).Value))
			{
				case TaskActionType.ComHandler:
					settingsTabs.SelectedTab = comTab;
					curHandler = comHandlerActionUI1;
					break;
				case TaskActionType.SendEmail:
					settingsTabs.SelectedTab = emailTab;
					curHandler = emailActionUI1;
					break;
				case TaskActionType.ShowMessage:
					settingsTabs.SelectedTab = messageTab;
					curHandler = showMessageActionUI1;
					break;
				case TaskActionType.Execute:
				default:
					settingsTabs.SelectedTab = execTab;
					curHandler = execActionUI1;
					break;
			}
			ValidateCurrentAction();
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void keyField_TextChanged(object sender, EventArgs e)
		{
			ValidateCurrentAction();
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			UpdateAction();
			Close();
		}

		private void ResetCombo()
		{
			int curType = actionsCombo.SelectedIndex == -1 ? -1 : Convert.ToInt32(((DropDownCheckListItem)actionsCombo.SelectedItem).Value);
			actionsCombo.BeginUpdate();
			actionsCombo.Items.Clear();
			long allVal;
			ComboBoxExtension.InitializeFromEnum(actionsCombo.Items, typeof(TaskActionType), EditorProperties.Resources.ResourceManager, "ActionType", out allVal);
			if (!isV2)
			{
				actionsCombo.Items.RemoveAt(actionsCombo.Items.IndexOf((long)TaskActionType.ComHandler));
				actionsCombo.Items.RemoveAt(actionsCombo.Items.IndexOf((long)TaskActionType.SendEmail));
				actionsCombo.Items.RemoveAt(actionsCombo.Items.IndexOf((long)TaskActionType.ShowMessage));
			}
			else if (useUnifiedSchedulingEngine)
			{
				actionsCombo.Items.RemoveAt(actionsCombo.Items.IndexOf((long)TaskActionType.SendEmail));
				actionsCombo.Items.RemoveAt(actionsCombo.Items.IndexOf((long)TaskActionType.ShowMessage));
			}
			if (curType > -1)
				curType = actionsCombo.Items.IndexOf((long)curType);
			if (curType == -1) curType = 0;
			actionsCombo.SelectedIndex = curType;
			actionsCombo.EndUpdate();
			runActionBtn.Visible = AllowRun;
		}

		private void UpdateAction()
		{
			action = curHandler.Action;
			actionIdText_TextChanged(null, EventArgs.Empty);
		}

		private void ValidateCurrentAction()
		{
			okBtn.Enabled = curHandler.IsActionValid();
		}

		private void actionIdText_TextChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				action.Id = actionIdText.TextLength == 0 ? null : actionIdText.Text;
		}

		private void runActionBtn_Click(object sender, EventArgs e)
		{
			curHandler.Run();
		}
	}
}