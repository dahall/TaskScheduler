using Microsoft.Win32.TaskScheduler.Design;
using Microsoft.Win32.TaskScheduler.EditorProperties;
using Microsoft.Win32.TaskScheduler.OptionPanels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Dialog that allows tasks to be edited</summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms")]
	[ToolboxBitmap(typeof(TaskEditDialog), "TaskDialog")]
	[Designer(typeof(TaskServiceComponentDesigner))]
	[DefaultProperty("Editable"), DesignTimeVisible(true)]
	[Description("Dialog allowing the editing of a task.")]
	public partial class TaskOptionsEditor :
#if DEBUG
		Form
#else
		DialogBase
#endif
		, ITaskEditor
	{
		private readonly Dictionary<ToolStripMenuItem, OptionPanel> panels = new Dictionary<ToolStripMenuItem, OptionPanel>(10);
		private OptionPanel curPanel;
		private bool editable, onAssignment;
		private int hoverIndex = -1;
		private bool lockTaskName;
		private Task task;
		private TaskDefinition td;
		private bool titleSet;

		/// <summary>Initializes a new instance of the <see cref="TaskOptionsEditor"/> class.</summary>
		public TaskOptionsEditor()
		{
			InitializeComponent();
			panels.Add(generalItem, new GeneralOptionPanel());
			panels.Add(triggersItem, new TriggersOptionPanel());
			panels.Add(actionsItem, new ActionsOptionPanel());
			panels.Add(securityItem, new SecurityOptionPanel());
			panels.Add(startupItem, new StartupOptionPanel());
			panels.Add(runItem, new RuntimeOptionPanel());
			foreach (var p in menuItemsContainer.Items)
				menuList.Items.Add(p);
			UpdateTitleFont();
		}

		/// <summary>Initializes a new instance of the <see cref="TaskOptionsEditor"/> class.</summary>
		/// <param name="task">The task.</param>
		/// <param name="editable">If set to <c>true</c> the task will be editable in the dialog.</param>
		/// <param name="registerOnAccept">If set to <c>true</c> the task will be registered when OK is pressed.</param>
		public TaskOptionsEditor(Task task, bool editable = true, bool registerOnAccept = true) : this()
		{
			Editable = editable;
			Initialize(task);
			RegisterTaskOnAccept = registerOnAccept;
		}

		/// <summary>Initializes a new instance of the <see cref="TaskOptionsEditor"/> class.</summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		/// <param name="editable">If set to <c>true</c> the task will be editable in the dialog.</param>
		/// <param name="registerOnAccept">If set to <c>true</c> the task will be registered when OK is pressed.</param>
		/// <param name="taskName">If set, assigns this name to the task's name field.</param>
		/// <param name="taskFolder">If set, assigns this path to the task's folder.</param>
		public TaskOptionsEditor(TaskService service, TaskDefinition td = null, bool editable = true, bool registerOnAccept = true, string taskName = null, string taskFolder = null) : this()
		{
			Editable = editable;
			Initialize(service, td, taskName, taskFolder);
			RegisterTaskOnAccept = registerOnAccept;
		}

		/// <summary>Gets or sets the available actions.</summary>
		/// <value>The available actions.</value>
		[DefaultValue(typeof(AvailableActions), nameof(AvailableActions.AllActions))]
		[Category("Appearance")]
		public AvailableActions AvailableActions { get; set; } = AvailableActions.AllActions;

		/*/// <summary>Gets or sets the available tabs.</summary>
		/// <value>The available tabs.</value>
		[DefaultValue(AvailableTaskTabs.Default), Category("Behavior"), Description("Determines which tabs are shown.")]
		public AvailableTaskTabs AvailableTabs
		{
			get { return taskPropertiesControl1.AvailableTabs; }
			set { taskPropertiesControl1.AvailableTabs = value; }
		}*/

		/// <summary>Gets or sets the available triggers.</summary>
		/// <value>The available triggers.</value>
		[DefaultValue(typeof(AvailableTriggers), nameof(AvailableTriggers.AllTriggers))]
		[Category("Appearance")]
		public AvailableTriggers AvailableTriggers { get; set; } = AvailableTriggers.AllTriggers;

		/// <summary>Gets or sets a value indicating whether to convert references to resource strings in libraries to their value.</summary>
		/// <value><c>true</c> if references to resource strings are converted; otherwise, <c>false</c>.</value>
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Converts string references in libraries to value.")]
		public bool ConvertResourceStringReferences { get; set; } = true;

		/// <summary>Gets or sets a value indicating whether this <see cref="TaskEditDialog"/> is editable.</summary>
		/// <value><c>true</c> if editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		public bool Editable
		{
			get => editable;
			set
			{
				if (value && task != null && task.ReadOnly)
					value = false;

				if (editable != value)
				{
					editable = value;
					ReinitializeControls();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this task definition is v2.</summary>
		/// <value><c>true</c> if this task definition is v2; otherwise, <c>false</c>.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsV2 { get; private set; }

		/// <summary>Gets or sets a value indicating whether to register task when Accept (OK) button pressed.</summary>
		/// <value><c>true</c> if updated task is to be registered; otherwise, <c>false</c>.</value>
		[Category("Behavior"), DefaultValue(false)]
		public bool RegisterTaskOnAccept { get; set; }

		/// <summary>Gets or sets a value indicating whether a button is shown when editing an action that allows user to execute the current action.</summary>
		/// <value><c>true</c> if button is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a button is shown when editing an action that allows user to execute the current action.")]
		public bool ShowActionRunButton { get; set; } = false;

		/// <summary>
		/// Gets or sets a value indicating whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert
		/// unsupported actions.
		/// </summary>
		/// <value><c>true</c> if check box is shown; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance"), Description("Determines whether a check box is shown on Actions tab that allows user to specify if PowerShell may be used to convert unsupported actions.")]
		public bool ShowConvertActionsToPowerShellCheck { get; set; } = false;

		/// <summary>Gets or sets a value indicating whether errors are shown in the UI.</summary>
		/// <value><c>true</c> if errors are shown; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors { get; set; } = true;

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get => task;
			private set
			{
				task = value;
				if (task != null)
				{
					TaskService = task.TaskService;
					TaskFolder = task.Folder.Path;
					if (task.ReadOnly)
						Editable = false;
					TaskDefinition = task.Definition;
				}
			}
		}

		/// <summary>Gets the <see cref="TaskDefinition"/> in its edited state.</summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition
		{
			get => td;
			private set
			{
				if (TaskService == null)
					throw new ArgumentNullException("TaskDefinition cannot be set until TaskService has been set with a valid object.");

				onAssignment = true;
				td = value ?? throw new ArgumentNullException("TaskDefinition cannot be set to null.");
				SetVersionComboItems();
				IsV2 = td.Settings.Compatibility >= TaskCompatibility.V2 &&
					   TaskService.HighestSupportedVersion >= new Version(1, 2);
				taskNameText.Text = task?.Name ?? string.Empty;
				ReinitializeControls();
				onAssignment = false;
			}
		}

		/// <summary>
		/// Gets or sets the folder for the task. If control is initialized with a <see cref="Task"/>, this value will be set to the folder of the registered task.
		/// </summary>
		/// <value>The task folder name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskFolder { get; set; }

		/// <summary>
		/// Gets or sets the name of the task. If control is initialized with a <see cref="Task"/>, this value will be set to the name of the registered task.
		/// </summary>
		/// <value>The task name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string TaskName
		{
			get => taskNameText.Text;
			set => taskNameText.Text = value;
		}

		/// <summary>
		/// If setup with a TaskDefinition and not a Task, and if Editable is <c>true</c>, then you can set this value to <c>false</c> to prevent the user from
		/// editing the TaskName.
		/// </summary>
		[Browsable(false)]
		public bool TaskNameIsEditable
		{
			get => task == null && editable && !lockTaskName;
			set { if (task == null && editable) taskNameText.ReadOnly = lockTaskName = !value; }
		}

		/// <summary>Gets the <see cref="TaskService"/> assigned at initialization.</summary>
		/// <value>The task service.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskService TaskService { get; set; }

		/// <summary>Gets or sets the title.</summary>
		/// <value>The title.</value>
		[Category("Appearance"), Description("A string to display in the title bar of the dialog box."), Localizable(true)]
		public string Title
		{
			get => Text;
			set { Text = value; titleSet = true; }
		}

		/// <summary>Initializes the control for the editing of a new <see cref="TaskDefinition"/>.</summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		/// <param name="taskName">If set, assigns this name to the task's name field.</param>
		/// <param name="taskFolder">If set, assigns this path to the task's folder.</param>
		public void Initialize(TaskService service, TaskDefinition td = null, string taskName = null, string taskFolder = null)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (!titleSet)
				Text = string.Format(Resources.TaskEditDlgTitle, "New Task", TaskEditDialog.GetServerString(service));
			TaskService = service;
			task = null;
			if (!this.IsDesignMode())
				if (td == null)
				{
					var temp = service.NewTask();
					if (service.HighestSupportedVersion == new Version(1, 1))
						temp.Settings.Compatibility = TaskCompatibility.V1;
					TaskDefinition = temp;
				}
				else
				{
					TaskDefinition = td;
				}
			TaskName = taskName;
			TaskFolder = taskFolder;
		}

		/// <summary>Initializes the control for the editing of an existing <see cref="Task"/>.</summary>
		/// <param name="taskInstance">A <see cref="Task"/> instance.</param>
		public void Initialize(Task taskInstance)
		{
			Task = taskInstance ?? throw new ArgumentNullException(nameof(taskInstance));
			if (!titleSet)
				Text = string.Format(Resources.TaskEditDlgTitle, taskInstance.Name, TaskEditDialog.GetServerString(taskInstance.TaskService));
		}

		/// <summary>Reinitializes all the controls based on current <see cref="TaskDefinition"/> values.</summary>
		public void ReinitializeControls()
		{
			taskNameText.ReadOnly = !(Task == null && Editable);
			taskVersionCombo.Enabled = Editable;
			if (curPanel == null && td != null)
				//this.menuList.Items[0].PerformClick();
				menuList.SelectedIndex = 0;
			curPanel?.Initialize(this);
		}

		/// <summary>Gets a string value that is converted if needed.</summary>
		/// <param name="input">The input string.</param>
		/// <returns>
		/// If the <see cref="ConvertResourceStringReferences"/> is <c>true</c>, and value is in the "$(string)" format, the reference will be pulled and returned.
		/// </returns>
		internal string GetStringValue(string input)
		{
			try
			{
				if (input != null && input.StartsWith(@"$(") && input.EndsWith(")") && ConvertResourceStringReferences)
					return NativeMethods.NativeResource.GetResourceString(input);
			}
			catch { }
			return input;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged"/> event when the <see cref="P:System.Windows.Forms.Control.Font"/> property value
		/// of the control's container changes.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnParentFontChanged(EventArgs e)
		{
			base.OnParentFontChanged(e);
			UpdateTitleFont();
		}

		private static Rectangle AdjustRect(Rectangle rect, int x, int y = 0, int w = 0, int h = 0) => new Rectangle(rect.X + x, rect.Y + y, rect.Width + w, rect.Height + h);

		private static Color LightenColor(Color colorIn, int percent)
		{
			if (percent < 0 || percent > 100)
				throw new ArgumentOutOfRangeException(nameof(percent));

			return Color.FromArgb(colorIn.A, colorIn.R + (int)((255f - colorIn.R) / 100f * percent),
				colorIn.G + (int)((255f - colorIn.G) / 100f * percent), colorIn.B + (int)((255f - colorIn.B) / 100f * percent));
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			if (!(sender is ToolStripMenuItem item) || !panels.TryGetValue(item, out var panel)) return;
			bodyPanel.SuspendLayout();
			panel.Dock = DockStyle.Fill;
			panelTitleLabel.Text = panel.Title;
			panelImage.Image = panel.Image;
			panelImage.Visible = panel.Image != null;
			if (curPanel != null)
				bodyPanel.Controls.Remove(curPanel);
			bodyPanel.Controls.Add(panel);
			bodyPanel.Controls.SetChildIndex(panel, 0);
			curPanel = panel;
			if (td != null)
				panel.Initialize(this);
			bodyPanel.ResumeLayout();
		}

		private void menuList_DrawItem(object sender, DrawItemEventArgs e)
		{
			var sel = SystemColors.ControlLight;
			var hot = LightenColor(SystemColors.ControlLight, 50);

			var fc = ForeColor;
			var bc = SystemColors.Window;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				bc = sel;
			if (e.Index == hoverIndex)
				bc = hot;
			if ((e.State & DrawItemState.Grayed) == DrawItemState.Grayed)
				fc = SystemColors.GrayText;
			using (Brush bgb = new SolidBrush(bc))
			{
				e.Graphics.FillRectangle(bgb, e.Bounds);
			}
			TextRenderer.DrawText(e.Graphics, menuList.Items[e.Index].ToString(), Font, AdjustRect(e.Bounds, 4, 0, -4, 0), fc,
				bc, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
			if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
				using (var bgb = new Pen(sel))
				{
					e.Graphics.DrawRectangle(bgb, AdjustRect(e.Bounds, 0, 0, -1, -1));
				}
		}

		private void menuList_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight *= 2;
		}

		private void menuList_MouseLeave(object sender, EventArgs e)
		{
			if (hoverIndex > -1)
			{
				hoverIndex = -1;
				menuList.Invalidate();
			}
		}

		private void menuList_MouseMove(object sender, MouseEventArgs e)
		{
			var index = menuList.IndexFromPoint(e.Location);
			if (index != hoverIndex)
			{
				hoverIndex = index;
				menuList.Invalidate();
			}
		}

		private void menuList_SelectedIndexChanged(object sender, EventArgs e)
		{
			menuItem_Click(menuList.SelectedItem as ToolStripMenuItem, EventArgs.Empty);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (TaskDefinition.Actions.Count == 0)
			{
				MessageBox.Show(Resources.TaskMustHaveActionsError, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (TaskDefinition.Settings.DeleteExpiredTaskAfter != TimeSpan.Zero &&
				!TaskEditDialog.ValidateOneTriggerExpires(TaskDefinition.Triggers))
			{
				MessageBox.Show(Resources.Error_TaskDeleteMustHaveExpiringTrigger, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (TaskDefinition.LowestSupportedVersion > TaskDefinition.Settings.Compatibility)
			{
				MessageBox.Show(Resources.Error_TaskPropertiesIncompatibleSimple, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (RegisterTaskOnAccept)
				if (Task != null && !Task.Definition.Principal.RequiresPassword())
				{
					Task.RegisterChanges();
				}
				else
				{
					var user = TaskDefinition.Principal.ToString();
					user = string.IsNullOrEmpty(user) ? WindowsIdentity.GetCurrent().Name : user;
					string pwd = null;
					var fld = TaskService.GetFolder(TaskFolder);
					if (TaskDefinition.Principal.RequiresPassword())
					{
						pwd = TaskEditDialog.InvokeCredentialDialog(user, this);
						if (pwd == null)
						{
							//throw new System.Security.Authentication.AuthenticationException(EditorProperties.Resources.UserAuthenticationError);
							MessageBox.Show(Resources.Error_PasswordMustBeProvided, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
					Task = fld.RegisterTaskDefinition(TaskName, TaskDefinition, TaskCreation.CreateOrUpdate,
						user, pwd, TaskDefinition.Principal.LogonType);
				}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void ResetTaskNameIsEditable()
		{
			lockTaskName = false;
		}

		private void ResetTitle()
		{
			var resources = new ComponentResourceManager(typeof(TaskOptionsEditor));
			Text = resources.GetString("$this.Text");
		}

		private void SetVersionComboItems()
		{
			const int expectedVersions = 6;

			taskVersionCombo.BeginUpdate();
			taskVersionCombo.Items.Clear();
			var versions = Resources.TaskCompatibility.Split('|');
			if (versions.Length != expectedVersions)
				throw new ArgumentOutOfRangeException(nameof(TaskDefinition), @"Locale specific information about supported Operating Systems is insufficient.");
			var libVerMinor = TaskService.LibraryVersion.Minor > 5 ? 5 : TaskService.LibraryVersion.Minor;
			var max = TaskService == null ? expectedVersions - 1 : Math.Min(libVerMinor, TaskService.HighestSupportedVersion.Minor);
			if (Environment.OSVersion.Version >= new Version(6, 2))
				using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
				{
					versions[libVerMinor] = key?.GetValue("ProductName", Environment.OSVersion).ToString();
				}
			var comp = td?.Settings.Compatibility ?? TaskCompatibility.V1;
			var lowestComp = td?.LowestSupportedVersion ?? TaskCompatibility.V1;
			if (lowestComp == TaskCompatibility.V1 && task != null && task.Folder.Path != "\\")
				lowestComp = TaskCompatibility.V2;
			switch (comp)
			{
				case TaskCompatibility.AT:
					for (var i = max; i > 1; i--)
						taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.Add(new ComboItem(versions[0], 0));
					break;

				default:
					for (var i = max; i > 0; i--)
						taskVersionCombo.Items.Add(new ComboItem(versions[i], i, comp >= lowestComp));
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.IndexOf((int)comp);
					break;
			}
			taskVersionCombo.EndUpdate();
		}

		private bool ShouldSerializeTaskNameIsEditable()
		{
			return task == null && editable && lockTaskName;
		}

		private bool ShouldSerializeTitle()
		{
			var resources = new ComponentResourceManager(typeof(TaskOptionsEditor));
			return Text != resources.GetString("$this.Text");
		}

		private void taskNameText_Validating(object sender, CancelEventArgs e)
		{
			var inv = Path.GetInvalidFileNameChars();
			e.Cancel = !ValidateText(taskNameText, s => s.Length > 0 && s.IndexOfAny(inv) == -1,
				Resources.Error_InvalidNameFormat);
		}

		private void TaskOptionsEditor_HelpButtonClicked(object sender, CancelEventArgs e)
		{
		}

		private void taskVersionCombo_GotFocus(object sender, EventArgs e)
		{
			var lowestComp = td?.LowestSupportedVersion ?? TaskCompatibility.V1;
			foreach (ComboItem ci in taskVersionCombo.Items)
			{
				ci.Enabled = ci.Version >= (int)lowestComp;
			}
		}

		private void taskVersionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			IsV2 = taskVersionCombo.SelectedIndex == -1 || ((ComboItem)taskVersionCombo.SelectedItem).Version > 1;
			var priorSetting = td?.Settings.Compatibility ?? TaskCompatibility.V1;
			if (!onAssignment && td != null && taskVersionCombo.SelectedIndex != -1)
				td.Settings.Compatibility = (TaskCompatibility)((ComboItem)taskVersionCombo.SelectedItem).Version;

			if (onAssignment && task == null || sender != null && td != null && priorSetting > td.Settings.Compatibility)
				try
				{
					if (!onAssignment && td != null)
					{
						td.Validate(true);
						ReinitializeControls();
					}
				}
				catch (InvalidOperationException ex)
				{
					var msg = new StringBuilder();
					if (ShowErrors)
					{
						msg.AppendLine(Resources.Error_TaskPropertiesIncompatible);
						foreach (var item in ex.Data.Keys)
							msg.AppendLine($"- {item} {ex.Data[item]}");
					}
					else
					{
						msg.Append(Resources.Error_TaskPropertiesIncompatibleSimple);
					}
					MessageBox.Show(this, msg.ToString(), Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					taskVersionCombo.SelectedIndex = taskVersionCombo.Items.IndexOf((int)priorSetting);
				}
		}

		private void UpdateTitleFont()
		{
			panelTitleLabel.Font = new Font(Font.FontFamily, Font.Size + 1, FontStyle.Bold, Font.Unit);
		}

		private bool ValidateText(Control ctrl, Predicate<string> pred, string error)
		{
			var valid = pred(ctrl.Text);
			//errorProvider.SetError(ctrl, valid ? string.Empty : error);
			//OnComponentError(valid ? ComponentErrorEventArgs.Empty : new ComponentErrorEventArgs(null, error));
			//hasError = valid;
			return valid;
		}

		private class ComboItem : IEnableable
		{
			public readonly string Text;
			public readonly int Version;

			public ComboItem(string text, int ver, bool enabled = true) { Text = text; Version = ver; Enabled = enabled; }

			public bool Enabled { get; set; }

			public override bool Equals(object obj)
			{
				switch (obj)
				{
					case null:
						return false;
					case ComboItem ci:
						return Version == ci.Version;
					case int i:
						return Version == i;
					default:
						return string.Compare(Text, obj.ToString(), StringComparison.Ordinal) == 0;
				}
			}

			public override int GetHashCode() => Version.GetHashCode();

			public override string ToString() => Text;
		}
	}
}