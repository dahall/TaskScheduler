using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that allows tasks to be edited
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Dialog allowing the editing of a task.")]
	[Designer(typeof(Design.TaskServiceComponentDesigner))]
	[DefaultProperty("AvailableTabs"), DesignTimeVisible(true)]
	[System.Drawing.ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	public partial class TaskEditDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private bool titleSet = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEditDialog"/> class.
		/// </summary>
		public TaskEditDialog()
		{
			InitializeComponent();
			RegisterTaskOnAccept = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEditDialog"/> class.
		/// </summary>
		/// <param name="task">The task.</param>
		/// <param name="editable">If set to <c>true</c> the task will be editable in the dialog.</param>
		/// <param name="registerOnAccept">If set to <c>true</c> the task will be registered when OK is pressed.</param>
		public TaskEditDialog(Task task, bool editable = true, bool registerOnAccept = true)
		{
			InitializeComponent();
			Editable = editable;
			Initialize(task);
			RegisterTaskOnAccept = registerOnAccept;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEditDialog"/> class.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		/// <param name="editable">If set to <c>true</c> the task will be editable in the dialog.</param>
		/// <param name="registerOnAccept">If set to <c>true</c> the task will be registered when OK is pressed.</param>
		public TaskEditDialog(TaskService service, TaskDefinition td = null, bool editable = true, bool registerOnAccept = true)
		{
			InitializeComponent();
			Editable = editable;
			Initialize(service, td);
			RegisterTaskOnAccept = registerOnAccept;
		}

		/// <summary>
		/// Gets or sets the available tabs.
		/// </summary>
		/// <value>
		/// The available tabs.
		/// </value>
		[DefaultValue(AvailableTaskTabs.Default), Category("Behavior"), Description("Determines which tabs are shown.")]
		public AvailableTaskTabs AvailableTabs
		{
			get { return taskPropertiesControl1.AvailableTabs; }
			set { taskPropertiesControl1.AvailableTabs = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to convert references to resource strings in libraries to their value.
		/// </summary>
		/// <value><c>true</c> if references to resource strings are converted; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Converts string references in libraries to value.")]
		public bool ConvertResourceStringReferences
		{
			get { return taskPropertiesControl1.ConvertResourceStringReferences; }
			set { taskPropertiesControl1.ConvertResourceStringReferences = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskEditDialog"/> is editable.
		/// </summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		public bool Editable
		{
			get { return taskPropertiesControl1.Editable; }
			set { taskPropertiesControl1.Editable = value; }
		}

		/// <summary>
		/// Gets or sets the maximum history count. Use -1 for infinite or to retrieve all items.
		/// </summary>
		/// <value>
		/// The maximum history count.
		/// </value>
		[DefaultValue(-1), Category("Behavior"), Description("Determines maximum number of history items to retrieve.")]
		public int MaxHistoryCount
		{
			get { return taskPropertiesControl1.MaxHistoryCount; }
			set { taskPropertiesControl1.MaxHistoryCount = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to register task when Accept (OK) button pressed.
		/// </summary>
		/// <value><c>true</c> if updated task is to be registered; otherwise, <c>false</c>.</value>
		[Category("Behavior"), DefaultValue(false)]
		public bool RegisterTaskOnAccept { get; set; }

		/// <summary>Gets or sets a value indicating whether a button is shown when editing an action that allows user to execute the current action.</summary>
		/// <value><c>true</c> if button is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a button is shown when editing an action that allows user to execute the current action.")]
		public bool ShowActionRunButton
		{
			get { return taskPropertiesControl1.ShowActionRunButton; }
			set { taskPropertiesControl1.ShowActionRunButton = value; }
		}

		/// <summary>Gets or sets a value indicating whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert unsupported actions.</summary>
		/// <value><c>true</c> if check box is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert unsupported actions.")]
		public bool ShowConvertActionsToPowerShellCheck
		{
			get { return taskPropertiesControl1.ShowConvertActionsToPowerShellCheck; }
			set { taskPropertiesControl1.ShowConvertActionsToPowerShellCheck = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Additions' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Additions tab; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the 'Additions' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowAddedPropertiesTab
		{
			get { return taskPropertiesControl1.ShowAddedPropertiesTab; }
			set { taskPropertiesControl1.ShowAddedPropertiesTab = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value>
		///   <c>true</c> if errors are shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors
		{
			get { return taskPropertiesControl1.ShowErrors; }
			set { taskPropertiesControl1.ShowErrors = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Info' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Info tab; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the 'Info' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowRegistrationInfoTab
		{
			get { return taskPropertiesControl1.ShowRegistrationInfoTab; }
			set { taskPropertiesControl1.ShowRegistrationInfoTab = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to show the 'Run Times' tab.
		/// </summary>
		/// <value><c>true</c> if showing the Run Times tab; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether the 'Run Times' tab is shown."), Obsolete("Please use the AvailableTabs property.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowRunTimesTab
		{
			get { return taskPropertiesControl1.ShowRunTimesTab; }
			set { taskPropertiesControl1.ShowRunTimesTab = value; }
		}

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get { return taskPropertiesControl1.Task; }
			set { taskPropertiesControl1.Initialize(value); }
		}

		/// <summary>
		/// Gets the <see cref="TaskDefinition"/> in its edited state.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition => taskPropertiesControl1.TaskDefinition;

		/// <summary>
		/// Gets or sets the folder for the task. If control is initialized with a <see cref="Task"/>, this value will be set to the folder of the registered task.
		/// </summary>
		/// <value>The task folder name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskFolder
		{
			get { return taskPropertiesControl1.TaskFolder; }
			set { taskPropertiesControl1.TaskFolder = value; }
		}

		/// <summary>
		/// Gets or sets the name of the task. If control is initialized with a <see cref="Task"/>, this value will be set to the name of the registered task.
		/// </summary>
		/// <value>The task name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskName
		{
			get { return taskPropertiesControl1.TaskName; }
			set { taskPropertiesControl1.TaskName = value; okBtn.Enabled = IsValidTaskName(value); }
		}

		/// <summary>
		/// Gets the <see cref="TaskService"/> assigned at initialization.
		/// </summary>
		/// <value>The task service.</value>
		[DefaultValue(null), Category("Data"), Description("The TaskService for this dialog.")]
		public TaskService TaskService
		{
			get { return taskPropertiesControl1.TaskService; }
			set { taskPropertiesControl1.Initialize(value); }
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[Category("Appearance"), Description("A string to display in the title bar of the dialog box."), Localizable(true)]
		public string Title
		{
			get { return base.Text; }
			set { base.Text = value; titleSet = true; }
		}

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public void Initialize(TaskService service, TaskDefinition td = null)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (!titleSet)
				Text = string.Format(EditorProperties.Resources.TaskEditDlgTitle, "New Task", GetServerString(service));
			okBtn.Enabled = false;
			taskPropertiesControl1.Initialize(service, td);
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			if (task == null)
				throw new ArgumentNullException(nameof(task));
			if (!titleSet)
				Text = string.Format(EditorProperties.Resources.TaskEditDlgTitle, task.Name, GetServerString(task.TaskService));
			taskPropertiesControl1.Initialize(task);
		}

		internal static string GetServerString(TaskService service) => service.TargetServer == null || Environment.MachineName.Equals(service.TargetServer, StringComparison.CurrentCultureIgnoreCase) ?
				EditorProperties.Resources.LocalMachine : service.TargetServer;

		internal static string InvokeCredentialDialog(string userName, IWin32Window owner)
		{
			CredentialsDialog dlg = new CredentialsDialog(EditorProperties.Resources.TaskSchedulerName,
				EditorProperties.Resources.CredentialPromptMessage, userName);
			dlg.ValidatePassword = true;
			if (dlg.ShowDialog(owner) == DialogResult.OK)
				return dlg.Password;
			return null;
		}

		private static bool IsValidTaskName(string name) => !string.IsNullOrEmpty(name) && name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1;

		/// <summary>
		/// Handles the Click event of the okBtn control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks>Changed in release 1.8.4 so that when a user cancels the password dialog, it no longer throws an exception but rather displays an error.</remarks>
		private void okBtn_Click(object sender, System.EventArgs e)
		{
			if (TaskDefinition.Actions.Count == 0)
			{
				MessageBox.Show(EditorProperties.Resources.TaskMustHaveActionsError, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (TaskDefinition.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero && !ValidateOneTriggerExpires())
			{
				MessageBox.Show(EditorProperties.Resources.Error_TaskDeleteMustHaveExpiringTrigger, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (TaskDefinition.LowestSupportedVersion > TaskDefinition.Settings.Compatibility)
			{
				MessageBox.Show(EditorProperties.Resources.Error_TaskPropertiesIncompatibleSimple, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (RegisterTaskOnAccept)
			{
				if (Task != null && !TaskDefinition.Principal.RequiresPassword())
					Task.RegisterChanges();
				else
				{
					var user = TaskDefinition.Principal.ToString();
					user = string.IsNullOrEmpty(user) ? WindowsIdentity.GetCurrent().Name : user;
					string pwd = null;
					TaskFolder fld = TaskService.GetFolder(TaskFolder);
					if (TaskDefinition.Principal.RequiresPassword())
					{
						pwd = InvokeCredentialDialog(user, this);
						if (pwd == null)
						{
							//throw new System.Security.Authentication.AuthenticationException(EditorProperties.Resources.UserAuthenticationError);
							MessageBox.Show(EditorProperties.Resources.Error_PasswordMustBeProvided, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
					Task = fld.RegisterTaskDefinition(taskPropertiesControl1.TaskName, TaskDefinition, TaskCreation.CreateOrUpdate,
						user, pwd, TaskDefinition.Principal.LogonType);
				}
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void ResetTitle()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskEditDialog));
			base.Text = resources.GetString("$this.Text");
		}

		private bool ShouldSerializeTitle()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskEditDialog));
			return base.Text != resources.GetString("$this.Text");
		}

		private void taskPropertiesControl1_ComponentError(object sender, TaskPropertiesControl.ComponentErrorEventArgs e)
		{
			okBtn.Enabled = (e == TaskPropertiesControl.ComponentErrorEventArgs.Empty);
		}

		private bool ValidateOneTriggerExpires() => ValidateOneTriggerExpires(TaskDefinition.Triggers);

		internal static bool ValidateOneTriggerExpires(System.Collections.Generic.IEnumerable<Trigger> triggers)
		{
			foreach (var tr in triggers)
			{
				if (tr.EndBoundary != DateTime.MaxValue)
					return true;
			}
			return false;
		}
	}
}