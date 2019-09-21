using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Vanara.Extensions;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>An editor that handles all Task actions.</summary>
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
		private bool allowRun;
		private AvailableActions availableActions = AvailableActions.AllActions;
		private UIComponents.IActionHandler curHandler;
		private bool isV2 = true;
		private bool onAssignment;
		private bool useUnifiedSchedulingEngine;

		/// <summary>Initializes a new instance of the <see cref="ActionEditDialog"/> class.</summary>
		public ActionEditDialog()
		{
			InitializeComponent();
			ResetCombo();
		}

		/// <summary>Initializes a new instance of the <see cref="ActionEditDialog"/> class with the provided action.</summary>
		/// <param name="action">The action.</param>
		/// <param name="supportV1Only"><c>true</c> if supports V1 only; otherwise, <c>false</c>.</param>
		/// <param name="allowedActions">The allowed actions.</param>
		public ActionEditDialog(Action action, bool supportV1Only = false, AvailableActions allowedActions = AvailableActions.AllActions) : this()
		{
			isV2 = !supportV1Only;
			AvailableActions = allowedActions;
			Action = action;
		}

		/// <summary>Gets or sets the action.</summary>
		/// <value>The action.</value>
		[DefaultValue(null), Browsable(false)]
		public Action Action
		{
			get => action;
			set
			{
				onAssignment = true;
				if (value != null && !AvailableActions.IsFlagSet((AvailableActions)(1 << (int)value.ActionType)))
					throw new ArgumentException("Type of Action is not permitted.", nameof(Action));
				if (value == null) value = GetFirstAvailableAction();
				action = value;
				actionsCombo.SelectedIndex = actionsCombo.IndexOfItemValue(action.ActionType);
				if (actionsCombo.SelectedIndex == -1)
					throw new ArgumentException("Type of Action is not permitted.", nameof(Action));
				actionIdText.Text = action?.Id;
				curHandler.Action = action;
				onAssignment = false;
			}
		}

		/// <summary>Gets or sets a value indicating whether to show a button allowing the action to be run in real time.</summary>
		/// <value><c>true</c> if allow run; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool AllowRun
		{
			get => allowRun;
			set { allowRun = value; runActionBtn.Visible = value; }
		}

		/// <summary>Gets or sets the available actions.</summary>
		/// <value>The available actions.</value>
		[DefaultValue(typeof(AvailableActions), nameof(AvailableActions.AllActions)), Category("Appearance")]
		public AvailableActions AvailableActions
		{
			get => availableActions;
			set
			{
				if (value == 0) throw new ArgumentException("Value cannot be 0", nameof(AvailableActions));
				if (value == availableActions) return;
				if (!value.IsValid()) throw new ArgumentOutOfRangeException(nameof(AvailableActions));
				availableActions = value;
				ResetCombo();
			}
		}

		/// <summary>Gets or sets the prompt text at the top of the dialog.</summary>
		/// <value>The text to use as a prompt.</value>
		[DefaultValue("You must specify what action this task will perform."), Category("Appearance")]
		public string Prompt
		{
			get => promptLabel.Text;
			set => promptLabel.Text = value;
		}

		/// <summary>Gets or sets a value indicating whether this editor only supports V1 actions.</summary>
		/// <value><c>true</c> if supports V1 only; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool SupportV1Only
		{
			get => !isV2;
			set
			{
				if (value != isV2) return;
				isV2 = !value;
				ResetCombo();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether dialog should restrict items to those available when using the Unified Scheduling Engine.
		/// </summary>
		/// <value><c>true</c> if using the Unified Scheduling Engine; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior")]
		public bool UseUnifiedSchedulingEngine
		{
			get => useUnifiedSchedulingEngine;
			set
			{
				if (!isV2 && value)
					throw new NotSupportedException("Version 1.0 of the Task Scheduler library cannot use the Unified Scheduling Engine.");
				if (value == useUnifiedSchedulingEngine) return;
				useUnifiedSchedulingEngine = value;
				ResetCombo();
			}
		}

		private IList<TaskActionType> InvalidActions
		{
			get
			{
				var x = AvailableActions.AllActions & ~AvailableActions;
				return x == 0 ? null : x.GetFlags().Where(a => a != AvailableActions.AllActions).Select(AvToType).ToList();
			}
		}

		private void actionIdText_TextChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				action.Id = actionIdText.TextLength == 0 ? null : actionIdText.Text;
		}

		private void actionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selVal = (actionsCombo.SelectedItem as TextValueItem<TaskActionType>)?.Value;
			switch (selVal)
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
					settingsTabs.SelectedTab = execTab;
					curHandler = execActionUI1;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
			DetermineIfCanValidate();
		}

		private static TaskActionType AvToType(AvailableActions av) => (TaskActionType)av.BitPosition();

		private void cancelBtn_Click(object sender, EventArgs e) => Close();

		private void DetermineIfCanValidate() => okBtn.Enabled = runActionBtn.Enabled = curHandler.CanValidate;

		private Action GetFirstAvailableAction() => Action.CreateAction(AvToType(AvailableActions.GetFlags().First()));

		private void keyField_TextChanged(object sender, EventArgs e) => DetermineIfCanValidate();

		private void okBtn_Click(object sender, EventArgs e)
		{
			if (!curHandler.ValidateFields()) return;
			UpdateAction();
			Close();
		}

		private void ResetCombo()
		{
			var curItem = actionsCombo.SelectedItem as TextValueItem<TaskActionType>;
			actionsCombo.BeginUpdate();
			actionsCombo.InitializeFromEnum(EditorProperties.Resources.ResourceManager, out _, "ActionType", InvalidActions);
			actionsCombo.EndUpdate();
			if (curItem == null) return;
			actionsCombo.SelectedIndex = actionsCombo.IndexOfItemValue(curItem.Value);
			if (actionsCombo.SelectedIndex == -1)
				throw new ArgumentException("Type of current Action is not permitted.", nameof(Action));
		}

		private void runActionBtn_Click(object sender, EventArgs e) => curHandler.Run();

		private void UpdateAction()
		{
			action = curHandler.Action;
			actionIdText_TextChanged(null, EventArgs.Empty);
		}
	}
}