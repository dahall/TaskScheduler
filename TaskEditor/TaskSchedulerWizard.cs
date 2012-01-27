using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// A wizard that walks the user through the creation of a simple task.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Wizard that walks the user through the creation of a simple task."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true), DefaultProperty("RegisterTaskOnFinish")]
	public sealed partial class TaskSchedulerWizard :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private Action action;
		private AvailableWizardActions availActions = (AvailableWizardActions)0xD;
		private AvailableWizardPages availPages = (AvailableWizardPages)0x3F;
		private AvailableWizardTriggers availTriggers = (AvailableWizardTriggers)0x7FF;
		private bool flagExecutorIsGroup, flagExecutorIsServiceAccount;
		private bool IsV2 = true;
		private bool onAssignment = false;
		private bool registerTaskOnFinish;
		private Task task;
		private TaskDefinition td;
		private Trigger trigger;
		private bool flagRunOnlyWhenUserIsLoggedOn;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSchedulerWizard"/> class.
		/// </summary>
		public TaskSchedulerWizard()
		{
			InitializeComponent();
			wizardControl1.TitleIcon = this.Icon;
			AllowEditorOnFinish = true;
			RegisterTaskOnFinish = false;
			SetupPages();
			SetupTriggerList();
			SetupActionList();
			repeatSpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.FromMinutes(5), TimeSpan2.FromMinutes(10), TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1) });
			durationSpan.Items.AddRange(new TimeSpan2[] { TimeSpan2.Zero, TimeSpan2.FromMinutes(15), TimeSpan2.FromMinutes(30), TimeSpan2.FromHours(1), TimeSpan2.FromHours(12), TimeSpan2.FromDays(1) });
			durationSpan.FormattedZero = EditorProperties.Resources.TimeSpanIndefinitely;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSchedulerWizard"/> class.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="definition">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public TaskSchedulerWizard(TaskService service, TaskDefinition definition = null)
			: this()
		{
			Initialize(service, definition);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSchedulerWizard"/> class.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public TaskSchedulerWizard(Task task)
			: this()
		{
			Initialize(task);
		}

		/// <summary>
		/// Flags to indicate which actions are available in the <see cref="TaskSchedulerWizard"/>.
		/// </summary>
		[Flags]
		public enum AvailableWizardActions
		{
			/// <summary>This action performs a command-line operation. For example, the action can run a script, launch an executable, or, if the name of a document is provided, find its associated application and launch the application with the document.</summary>
			Execute = 0x1,
			/*/// <summary>This action fires a handler.</summary>
			ComHandler = 0x2,*/
			/// <summary>This action sends and e-mail.</summary>
			SendEmail = 0x4,
			/// <summary>This action shows a message box.</summary>
			ShowMessage = 0x8
		}

		/// <summary>
		/// Flags to indicate which pages are visible in the <see cref="TaskSchedulerWizard"/>.
		/// </summary>
		[Flags]
		public enum AvailableWizardPages
		{
			/// <summary>Displays the introduction page with name and description.</summary>
			IntroPage = 0x1,
			/// <summary>Displays the security options page with user and password options.</summary>
			SecurityPage = 0x10,
			/// <summary>Displays trigger selection page.</summary>
			TriggerSelectPage = 0x2,
			/// <summary>Displays generic trigger properties page.</summary>
			TriggerPropertiesPage = 0x20,
			/// <summary>Displays action selection page.</summary>
			ActionSelectPage = 0x4,
			/// <summary>Displays the summary page.</summary>
			SummaryPage = 0x8
		}

		/// <summary>
		/// Flags to indicate which triggers are available in the <see cref="TaskSchedulerWizard"/>.
		/// </summary>
		[Flags]
		public enum AvailableWizardTriggers
		{
			/// <summary>Triggers the task when a specific event occurs. Version 1.2 only.</summary>
			Event = 0x1,
			/// <summary>Triggers the task at a specific time of day.</summary>
			Time = 0x2,
			/// <summary>Triggers the task on a daily schedule.</summary>
			Daily = 0x4,
			/// <summary>Triggers the task on a weekly schedule.</summary>
			Weekly = 0x8,
			/// <summary>Triggers the task on a monthly schedule.</summary>
			Monthly = 0x10,
			/// <summary>Triggers the task on a monthly day-of-week schedule.</summary>
			MonthlyDOW = 0x20,
			/// <summary>Triggers the task when the computer goes into an idle state.</summary>
			Idle = 0x40,
			/// <summary>Triggers the task when the task is registered. Version 1.2 only.</summary>
			Registration = 0x80,
			/// <summary>Triggers the task when the computer boots.</summary>
			Boot = 0x100,
			/// <summary>Triggers the task when a specific user logs on.</summary>
			Logon = 0x200,
			/// <summary>Triggers the task when a specific user session state changes. Version 1.2 only.</summary>
			SessionStateChange = 0x400,
		}

		/// <summary>
		/// Gets or sets a value indicating whether show a check box on the summary page that will open the full editor when the user presses Finish.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Behavior"), Description("Show a check box on the summary page that will open the full editor.")]
		public bool AllowEditorOnFinish { get; set; }

		/// <summary>
		/// Gets or sets the available actions.
		/// </summary>
		/// <value>
		/// The available actions.
		/// </value>
		[DefaultValue((AvailableWizardActions)0xD)]
		public AvailableWizardActions AvailableActions
		{
			get { return availActions; }
			set
			{
				if (value != availActions && ((availPages & AvailableWizardPages.ActionSelectPage) == AvailableWizardPages.ActionSelectPage))
				{
					availActions = value;
					SetupActionList();
				}
			}
		}

		/// <summary>
		/// Gets or sets the available pages.
		/// </summary>
		/// <value>
		/// The available pages.
		/// </value>
		[DefaultValue((AvailableWizardPages)0x3F)]
		public AvailableWizardPages AvailablePages
		{
			get { return availPages; }
			set
			{
				if (availPages != value)
				{
					availPages = value;
					SetupPages();
					if ((availPages & AvailableWizardPages.TriggerSelectPage) != AvailableWizardPages.TriggerSelectPage)
					{
						availTriggers = 0;
						SetupTriggerList();
					}
					if ((availPages & AvailableWizardPages.ActionSelectPage) != AvailableWizardPages.ActionSelectPage)
					{
						availActions = 0;
						SetupActionList();
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the available triggers.
		/// </summary>
		/// <value>
		/// The available triggers.
		/// </value>
		[DefaultValue((AvailableWizardPages)0x7FF)]
		public AvailableWizardTriggers AvailableTriggers
		{
			get { return availTriggers; }
			set
			{
				if (value != availTriggers && ((availPages & AvailableWizardPages.TriggerSelectPage) == AvailableWizardPages.TriggerSelectPage))
				{
					availTriggers = value;
					SetupTriggerList();
				}
			}
		}

		/// <summary>
		/// Gets or sets the text shown on the summary page prompting for editor on finish click.
		/// </summary>
		/// <value>
		/// The editor on finish text.
		/// </value>
		[DefaultValue((string)null), Category("Appearance")]
		public string EditorOnFinishText { get; set; }

		/// <summary>
		/// Gets or sets the icon for the form.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Drawing.Icon"/> that represents the icon for the form.
		/// </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		///   </PermissionSet>
		public new System.Drawing.Icon Icon
		{
			get { return wizardControl1.TitleIcon; }
			set { wizardControl1.TitleIcon = value; }
		}

		/// <summary>
		/// Gets the password.
		/// </summary>
		[Browsable(false), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Password { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether to register the task on Finish.
		/// </summary>
		/// <value><c>true</c> if task registered on Finish; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether to register the task on Finish")]
		public bool RegisterTaskOnFinish
		{
			get { return registerTaskOnFinish; }
			set
			{
				if (registerTaskOnFinish != value)
				{
					registerTaskOnFinish = value;
					summaryPrompt.Visible = registerTaskOnFinish;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form displays an icon in the caption bar; otherwise, false. The default is true.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool ShowIcon
		{
			get { return base.ShowIcon; }
			set { base.ShowIcon = value; }
		}

		/// <summary>
		/// Gets or sets the summary format string.
		/// </summary>
		/// <value>
		/// The summary format string.
		/// </value>
		[DefaultValue((string)null), Category("Appearance")]
		public string SummaryFormatString { get; set; }

		/// <summary>
		/// Gets or sets the summary registration notice.
		/// </summary>
		/// <value>
		/// The summary registration notice.
		/// </value>
		[DefaultValue((string)null), Category("Appearance")]
		public string SummaryRegistrationNotice { get; set; }

		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Task Task
		{
			get
			{
				return task;
			}
			private set
			{
				task = value;
				if (task != null)
				{
					TaskService = task.TaskService;
					TaskDefinition = task.Definition;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskDefinition"/>.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition
		{
			get
			{
				return td;
			}
			set
			{
				if (TaskService == null)
					throw new ArgumentNullException("TaskDefinition cannot be set until TaskService has been set with a valid object.");

				if (value == null)
					throw new ArgumentNullException("TaskDefinition cannot be set to null.");

				td = value;
				onAssignment = true;
				IsV2 = TaskService.HighestSupportedVersion >= (new Version(1, 2)) && td.Settings.Compatibility == TaskCompatibility.V2;

				// Set General tab
				nameText.Text = task != null ? task.Name : string.Empty;
				descText.Text = td.RegistrationInfo.Description;
				SetUserControls(td.Principal.LogonType);
				taskLoggedOnRadio.Checked = flagRunOnlyWhenUserIsLoggedOn;
				taskLoggedOptionalRadio.Checked = !flagRunOnlyWhenUserIsLoggedOn;
				taskLocalOnlyCheck.Checked = !flagRunOnlyWhenUserIsLoggedOn && td.Principal.LogonType == TaskLogonType.S4U;

				// Setup trigger values
				if (td.Triggers.Count > 0)
					trigger = (Trigger)td.Triggers[0].Clone();
				else
					trigger = new TimeTrigger();
				if (availTriggers > 0)
				{
					switch (trigger.TriggerType)
					{
						case TaskTriggerType.Time:
							oneTimeStartTimePicker.Value = trigger.StartBoundary;
							SetTriggerListItem(AvailableWizardTriggers.Time);
							break;
						case TaskTriggerType.Daily:
							dailyStartTimePicker.Value = trigger.StartBoundary;
							dailyRecurNumUpDn.Value = ((DailyTrigger)trigger).DaysInterval;
							SetTriggerListItem(AvailableWizardTriggers.Daily);
							break;
						case TaskTriggerType.Weekly:
							weeklyTriggerPage_Initialize(null, null);
							weeklyStartTimePicker.Value = trigger.StartBoundary;
							weeklyRecurNumUpDn.Value = ((WeeklyTrigger)trigger).WeeksInterval;
							weeklySunCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Sunday) != 0;
							weeklyMonCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Monday) != 0;
							weeklyTueCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Tuesday) != 0;
							weeklyWedCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Wednesday) != 0;
							weeklyThuCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Thursday) != 0;
							weeklyFriCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Friday) != 0;
							weeklySatCheck.Checked = (((WeeklyTrigger)trigger).DaysOfWeek & DaysOfTheWeek.Saturday) != 0;
							SetTriggerListItem(AvailableWizardTriggers.Weekly);
							break;
						case TaskTriggerType.Monthly:
							monthlyTriggerPage_Initialize(null, null);
							monthlyStartTimePicker.Value = trigger.StartBoundary;
							monthlyDaysRadio.Checked = true;
							monthlyDaysDropDown.CheckedFlagValue = 0L;
							foreach (int i in ((MonthlyTrigger)trigger).DaysOfMonth)
								monthlyDaysDropDown.SetItemChecked(i - 1, true);
							monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyTrigger)trigger).MonthsOfYear;
							monthlyDaysDropDown.SetItemChecked(31, ((MonthlyTrigger)trigger).RunOnLastDayOfMonth);
							SetTriggerListItem(AvailableWizardTriggers.Monthly);
							break;
						case TaskTriggerType.MonthlyDOW:
							monthlyStartTimePicker.Value = trigger.StartBoundary;
							monthlyOnRadio.Checked = true;
							monthlyOnDOWDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).DaysOfWeek;
							monthlyMonthsDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).MonthsOfYear;
							monthlyOnWeekDropDown.CheckedFlagValue = (long)((MonthlyDOWTrigger)trigger).WeeksOfMonth;
							monthlyOnWeekDropDown.SetItemChecked(4, ((MonthlyDOWTrigger)trigger).RunOnLastWeekOfMonth);
							SetTriggerListItem(AvailableWizardTriggers.MonthlyDOW);
							break;
						case TaskTriggerType.Event:
							string log, source; int? id;
							((EventTrigger)trigger).GetBasic(out log, out source, out id);
							onEventTriggerPage_Initialize(null, null);
							onEventLogCombo.Text = log;
							onEventSourceCombo.Text = source;
							onEventIdText.Text = id.HasValue ? id.Value.ToString() : string.Empty;
							SetTriggerListItem(AvailableWizardTriggers.Event);
							break;
						default:
							break;
					}

					bool hasRep = trigger.Repetition.Interval != TimeSpan.Zero;
					if (!hasRep)
					{
						durationSpan.Value = repeatSpan.Value = TimeSpan.Zero;
					}
					else
					{
						durationSpan.Value = trigger.Repetition.Duration;
						repeatSpan.Value = trigger.Repetition.Interval;
					}
					repeatCheckBox.Checked = repeatSpan.Enabled = durationLabel.Enabled = durationSpan.Enabled = hasRep;
					enabledCheckBox.Checked = trigger.Enabled;
				}

				// Setup action values
				if (td.Actions.Count > 0)
					action = (Action)td.Actions[0].Clone();
				else
					action = new ExecAction();
				if (availActions > 0)
				{
					if (action is ExecAction)
					{
						execProgText.Text = ((ExecAction)action).Path;
						execArgText.Text = ((ExecAction)action).Arguments;
						execDirText.Text = ((ExecAction)action).WorkingDirectory;
						SetActionListItem(AvailableWizardActions.Execute);
					}
					else if (action is EmailAction)
					{
						emailFromText.Text = ((EmailAction)action).From;
						emailToText.Text = ((EmailAction)action).To;
						emailSubjectText.Text = ((EmailAction)action).Subject;
						emailTextText.Text = ((EmailAction)action).Body;
						if (((EmailAction)action).Attachments != null && ((EmailAction)action).Attachments.Length > 0)
							emailAttachmentText.Text = ((EmailAction)action).Attachments[0].ToString();
						emailSMTPText.Text = ((EmailAction)action).Server;
						SetActionListItem(AvailableWizardActions.SendEmail);
					}
					else if (action is ShowMessageAction)
					{
						msgTitleText.Text = ((ShowMessageAction)action).Title;
						msgMsgText.Text = ((ShowMessageAction)action).MessageBody;
						SetActionListItem(AvailableWizardActions.ShowMessage);
					}
				}

				onAssignment = false;
			}
		}

		/// <summary>
		/// Gets or sets the name of the task.
		/// </summary>
		/// <value>The name of the task.</value>
		[DefaultValue(""), Category("Appearance"), Description("Name of the task to display")]
		public string TaskName
		{
			get { return nameText.Text; }
			set { nameText.Text = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskService"/>.
		/// </summary>
		/// <value>The task service.</value>
		[DefaultValue(null), Category("Data"), Description("The TaskService for this wizard")]
		public TaskService TaskService { get; set; }

		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		/// <value>
		/// The text associated with this control.
		/// </value>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[Category("Appearance"), Description("A string to display in the title bar of the dialog box."), Localizable(true)]
		public string Title
		{
			get { return wizardControl1.Title; }
			set { wizardControl1.Title = value; }
		}

		/// <summary>
		/// Gets or sets the trigger page prompt.
		/// </summary>
		/// <value>
		/// The trigger page prompt.
		/// </value>
		[DefaultValue((string)null), Category("Appearance"), Description("Trigger page title prompt.")]
		public string TriggerPagePrompt { get; set; }

		/// <summary>
		/// Gets or sets the trigger properties page instructions text.
		/// </summary>
		/// <value>
		/// The trigger properties instruction text.
		/// </value>
		[DefaultValue((string)null), Category("Appearance"), Description("Trigger properties page instruction text.")]
		public string TriggerPropertiesInstructions { get; set; }

		/// <summary>
		/// Initializes the control for the editing of a new <see cref="TaskDefinition"/>.
		/// </summary>
		/// <param name="service">A <see cref="TaskService"/> instance.</param>
		/// <param name="td">An optional <see cref="TaskDefinition"/>. Leaving null creates a new task.</param>
		public void Initialize(TaskService service, TaskDefinition td = null)
		{
			this.TaskService = service;
			this.task = null;
			if (!this.IsDesignMode())
			{
				if (td == null)
					this.TaskDefinition = service.NewTask();
				else
				{
					if (td.Triggers.Count > 1)
						throw new ArgumentException("Only tasks with a single trigger can be used to initialize the wizard.");
					this.TaskDefinition = td;
				}
			}
		}

		/// <summary>
		/// Initializes the control for the editing of an existing <see cref="Task"/>.
		/// </summary>
		/// <param name="task">A <see cref="Task"/> instance.</param>
		public void Initialize(Task task)
		{
			this.Task = task;
		}

		private void actionSelectionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			actionSelectPage.AllowNext = (actionSelectionList.SelectedIndex >= 0);
		}

		private void actionSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			bool hasValue = (this.action != null);
			AvailableWizardActions selAct = (AvailableWizardActions)actionSelectionList.SelectedItem.Tag;
			switch (selAct)
			{
				case AvailableWizardActions.Execute:
					e.Page.NextPage = runActionPage;
					if (!hasValue || this.action.ActionType != TaskActionType.Execute)
						action = new ExecAction();
					break;
				case AvailableWizardActions.SendEmail:
					e.Page.NextPage = emailActionPage;
					if (!hasValue || this.action.ActionType != TaskActionType.SendEmail)
						action = new EmailAction();
					break;
				case AvailableWizardActions.ShowMessage:
					e.Page.NextPage = msgActionPage;
					if (!hasValue || this.action.ActionType != TaskActionType.ShowMessage)
						action = new ShowMessageAction();
					break;
				default:
					e.Cancel = true;
					break;
			}
		}

		private void AddActionToSelectionList(AvailableWizardActions action)
		{
			this.actionSelectionList.Items.Add(new GroupControls.RadioButtonListItem()
			{
				Text = TaskPropertiesControl.BuildEnumString("WizActionText", action),
				Subtext = TaskPropertiesControl.BuildEnumString("WizActionSubtext", action),
				Tag = (int)action
			});
		}

		private void AddTriggerToSelectionList(AvailableWizardTriggers trig)
		{
			this.triggerSelectionList.Items.Add(new GroupControls.RadioButtonListItem()
			{
				Text = TaskPropertiesControl.BuildEnumString("WizTriggerText", trig),
				Subtext = TaskPropertiesControl.BuildEnumString("WizTriggerSubtext", trig),
				Tag = (int)trig
			});
		}

		private void changePrincipalButton_Click(object sender, EventArgs e)
		{
			string acct = String.Empty;
			if (!NativeMethods.AccountUtils.SelectAccount(this, TaskService.TargetServer, ref acct, ref flagExecutorIsGroup, ref flagExecutorIsServiceAccount))
				return;

			if (flagExecutorIsServiceAccount)
			{
				if (!IsV2 && acct != "SYSTEM")
				{
					MessageBox.Show(this, EditorProperties.Resources.TaskSchedulerName, EditorProperties.Resources.Error_NoGroupsUnderV1, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				flagExecutorIsGroup = false;
				if (IsV2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				td.Principal.LogonType = TaskLogonType.ServiceAccount;
				//this.flagExecutorIsCurrentUser = false;
			}
			else if (flagExecutorIsGroup)
			{
				if (!IsV2)
				{
					MessageBox.Show(this, EditorProperties.Resources.TaskSchedulerName, EditorProperties.Resources.Error_NoGroupsUnderV1, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				td.Principal.GroupId = acct;
				td.Principal.UserId = null;
				td.Principal.LogonType = TaskLogonType.Group;
				//this.flagExecutorIsCurrentUser = false;
			}
			else
			{
				if (IsV2)
					td.Principal.GroupId = null;
				td.Principal.UserId = acct;
				//this.flagExecutorIsCurrentUser = this.UserIsExecutor(objArray[0].ObjectName);
				if (td.Principal.LogonType == TaskLogonType.Group)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveToken;
				}
				else if (td.Principal.LogonType == TaskLogonType.ServiceAccount)
				{
					td.Principal.LogonType = TaskLogonType.InteractiveTokenOrPassword;
				}
			}
			SetUserControls(td.Principal.LogonType);
		}

		private void dailyTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			trigger.StartBoundary = dailyStartTimePicker.Value;
			((DailyTrigger)trigger).DaysInterval = (short)dailyRecurNumUpDn.Value;
		}

		private void durationSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				trigger.Repetition.Duration = durationSpan.Value;
				if (trigger.Repetition.Duration < trigger.Repetition.Interval && trigger.Repetition.Duration != TimeSpan.Zero)
				{
					onAssignment = true;
					repeatSpan.Value = trigger.Repetition.Duration - TimeSpan.FromMinutes(1);
					trigger.Repetition.Interval = repeatSpan.Value;
					onAssignment = false;
				}
			}
		}

		private void emailActionPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			EmailAction ea = action as EmailAction;
			if (emailAttachmentText.TextLength > 0)
				ea.Attachments = new object[] { emailAttachmentText.Text };
			else
				ea.Attachments = null;
			ea.From = emailFromText.Text;
			ea.Server = emailSMTPText.Text;
			ea.Subject = emailSubjectText.Text;
			ea.Body = emailTextText.Text;
			ea.To = emailToText.Text;
		}

		private void emailAttachementBrowseBtn_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				emailAttachmentText.Text = openFileDialog1.FileName;
		}

		private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			trigger.Enabled = enabledCheckBox.Checked;
		}

		private void execProgBrowseBtn_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				execProgText.Text = openFileDialog1.FileName;
		}

		private string InvokeCredentialDialog(string userName)
		{
			CredentialsDialog dlg = new CredentialsDialog(EditorProperties.Resources.TaskSchedulerName,
				EditorProperties.Resources.CredentialPromptMessage, userName);
			dlg.Options |= CredentialsDialogOptions.Persist;
			dlg.ValidatePassword = true;
			if (dlg.ShowDialog(this.ParentForm) == DialogResult.OK)
				return dlg.Password;
			return null;
		}

		private void monthlyDaysRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			bool days = monthlyDaysRadio.Checked;
			monthlyDaysDropDown.Enabled = days;
			monthlyOnDOWDropDown.Enabled = monthlyOnWeekDropDown.Enabled = !days;
		}

		private void monthlyTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			if (monthlyDaysRadio.Checked)
			{
				trigger = new MonthlyTrigger() { MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue };
				int[] days = new int[monthlyDaysDropDown.SelectedItems.Length];
				for (int i = 0; i < monthlyDaysDropDown.SelectedItems.Length; i++)
					days[i] = (int)monthlyDaysDropDown.SelectedItems[i].Value;
				((MonthlyTrigger)trigger).DaysOfMonth = days;
				if (days.Length == 0 || monthlyMonthsDropDown.CheckedFlagValue == 0)
				{
					e.Cancel = true;
					MessageBox.Show(this, EditorProperties.Resources.WizardMonthlyTriggerInvalid, EditorProperties.Resources.WizardMonthlyTriggerErrorTitle);
				}
			}
			else
			{
				trigger = new MonthlyDOWTrigger() { MonthsOfYear = (MonthsOfTheYear)monthlyMonthsDropDown.CheckedFlagValue };
				((MonthlyDOWTrigger)trigger).WeeksOfMonth = (WhichWeek)monthlyOnWeekDropDown.CheckedFlagValue;
				((MonthlyDOWTrigger)trigger).DaysOfWeek = (DaysOfTheWeek)monthlyOnDOWDropDown.CheckedFlagValue;
				if (monthlyMonthsDropDown.CheckedFlagValue == 0 || monthlyOnWeekDropDown.CheckedFlagValue == 0 || monthlyOnDOWDropDown.CheckedFlagValue == 0)
				{
					e.Cancel = true;
					MessageBox.Show(this, EditorProperties.Resources.WizardMonthlyDOWTriggerInvalid, EditorProperties.Resources.WizardMonthlyTriggerErrorTitle);
				}
			}
			trigger.StartBoundary = monthlyStartTimePicker.Value;
		}

		private void monthlyTriggerPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			if (monthlyMonthsDropDown.Items.Count == 0)
			{
				monthlyMonthsDropDown.InitializeFromEnum(typeof(MonthsOfTheYear), TaskPropertiesControl.taskSchedResources, "MOY");
				monthlyMonthsDropDown.Items.RemoveAt(13);
				monthlyDaysDropDown.InitializeFromRange(1, 31);
				monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(EditorProperties.Resources.Last, 99));
				monthlyDaysDropDown.MultiColumnList = true;
				monthlyOnWeekDropDown.InitializeFromEnum(typeof(WhichWeek), TaskPropertiesControl.taskSchedResources, "WW");
				monthlyOnWeekDropDown.Items.RemoveAt(5);
				monthlyOnDOWDropDown.InitializeFromEnum(typeof(DaysOfTheWeek), TaskPropertiesControl.taskSchedResources, "DOW");
				monthlyOnDOWDropDown.Items.RemoveAt(8);
			}
			if (!monthlyDaysRadio.Checked && !monthlyOnRadio.Checked)
				monthlyDaysRadio.Checked = true;
		}

		private void msgActionPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			((ShowMessageAction)action).Title = msgTitleText.Text;
			((ShowMessageAction)action).MessageBody = msgMsgText.Text;
		}

		private void nameText_TextChanged(object sender, System.EventArgs e)
		{
			introPage.AllowNext = nameText.TextLength > 0;
		}

		private void oneTimeTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			trigger.StartBoundary = oneTimeStartTimePicker.Value;
		}

		private void onEventLogCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			onEventSourceCombo.Items.Clear();
			onEventSourceCombo.Items.AddRange(SystemEventEnumerator.GetEventSources(null, onEventLogCombo.Text));
		}

		private void onEventTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			if (onEventLogCombo.Text.Length > 0)
			{
				int rid;
				int? id = onEventIdText.TextLength == 0 ? null : (int.TryParse(onEventIdText.Text, out rid) ? (int?)rid : null);
				((EventTrigger)trigger).SetBasic(onEventLogCombo.Text, onEventSourceCombo.Text, id);
			}
			else
			{
				e.Cancel = true;
				MessageBox.Show(this, EditorProperties.Resources.WizardEventTriggerInvalid, EditorProperties.Resources.WizardEventTriggerErrorTitle);
			}
		}

		private void onEventTriggerPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			if (onEventLogCombo.Items.Count == 0)
				onEventLogCombo.Items.AddRange(SystemEventEnumerator.GetEventLogs(null));
		}

		private void repeatCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				if (repeatCheckBox.Checked)
				{
					repeatSpan.Value = repeatSpan.Items[repeatSpan.Items.Count - 1];
					durationSpan.Value = TimeSpan.Zero;
				}
				else
				{
					trigger.Repetition.Duration = trigger.Repetition.Interval = TimeSpan.Zero;
				}
				repeatSpan.Enabled = durationSpan.Enabled = durationLabel.Enabled = repeatCheckBox.Checked;
			}
		}

		private void repeatSpan_ValueChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
			{
				trigger.Repetition.Interval = repeatSpan.Value;
				if (trigger.Repetition.Duration < trigger.Repetition.Interval && trigger.Repetition.Duration != TimeSpan.Zero)
				{
					onAssignment = true;
					durationSpan.Value = trigger.Repetition.Interval + TimeSpan.FromMinutes(1);
					trigger.Repetition.Duration = durationSpan.Value;
					onAssignment = false;
				}
			}
		}

		private void runActionPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			((ExecAction)action).Path = execProgText.Text;
			((ExecAction)action).Arguments = execArgText.Text;
			((ExecAction)action).WorkingDirectory = execDirText.Text;
		}

		private void secOptPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			string user = this.TaskDefinition.Principal.UserId;
			Password = null;
			if (this.TaskDefinition.Principal.LogonType == TaskLogonType.InteractiveTokenOrPassword || this.TaskDefinition.Principal.LogonType == TaskLogonType.Password)
			{
				Password = InvokeCredentialDialog(user);
				if (Password == null)
				{
					MessageBox.Show(this, EditorProperties.Resources.UserAuthenticationError, null);
					e.Cancel = true;
				}
			}
		}

		private void SetActionListItem(AvailableWizardActions availableWizardActions)
		{
			foreach (var item in actionSelectionList.Items)
			{
				if (item.Tag.Equals((int)availableWizardActions))
				{
					actionSelectionList.SelectedItem = item;
					return;
				}
			}
		}

		private bool SetPage(AeroWizard.WizardPage page, int flag, int flagSet)
		{
			bool set = (flagSet & flag) == flag;
			page.Suppress = !set;
			return set;
		}

		private void SetTriggerListItem(AvailableWizardTriggers availableWizardTriggers)
		{
			foreach (var item in triggerSelectionList.Items)
			{
				if (item.Tag.Equals((int)availableWizardTriggers))
				{
					triggerSelectionList.SelectedItem = item;
					return;
				}
			}
		}

		private void SetupActionList()
		{
			actionSelectionList.Items.Clear();

			if (SetPage(runActionPage, (int)AvailableWizardActions.Execute, (int)AvailableActions))
				AddActionToSelectionList(AvailableWizardActions.Execute);
			if (IsV2 && SetPage(msgActionPage, (int)AvailableWizardActions.ShowMessage, (int)AvailableActions))
				AddActionToSelectionList(AvailableWizardActions.ShowMessage);
			if (IsV2 && SetPage(emailActionPage, (int)AvailableWizardActions.SendEmail, (int)AvailableActions))
				AddActionToSelectionList(AvailableWizardActions.SendEmail);
		}

		private void SetupPages()
		{
			SetPage(introPage, (int)AvailableWizardPages.IntroPage, (int)availPages);
			SetPage(triggerSelectPage, (int)AvailableWizardPages.TriggerSelectPage, (int)availPages);
			SetPage(triggerPropPage, (int)AvailableWizardPages.TriggerPropertiesPage, (int)availPages);
			SetPage(actionSelectPage, (int)AvailableWizardPages.ActionSelectPage, (int)availPages);
			SetPage(secOptPage, (int)AvailableWizardPages.SecurityPage, (int)availPages);
			SetPage(summaryPage, (int)AvailableWizardPages.SummaryPage, (int)availPages);
		}

		private void SetupTriggerList()
		{
			triggerSelectionList.Items.Clear();

			if (SetPage(dailyTriggerPage, (int)AvailableWizardTriggers.Daily, (int)AvailableTriggers))
				AddTriggerToSelectionList(AvailableWizardTriggers.Daily);
			if (IsV2 && SetPage(onEventTriggerPage, (int)AvailableWizardTriggers.Event, (int)AvailableTriggers))
				AddTriggerToSelectionList(AvailableWizardTriggers.Event);
			if (SetPage(monthlyTriggerPage, (int)AvailableWizardTriggers.Monthly, (int)AvailableTriggers))
				AddTriggerToSelectionList(AvailableWizardTriggers.Monthly);
			if (SetPage(oneTimeTriggerPage, (int)AvailableWizardTriggers.Time, (int)AvailableTriggers))
				AddTriggerToSelectionList(AvailableWizardTriggers.Time);
			if (SetPage(weeklyTriggerPage, (int)AvailableWizardTriggers.Weekly, (int)AvailableTriggers))
				AddTriggerToSelectionList(AvailableWizardTriggers.Weekly);
		}

		private void SetUserControls(TaskLogonType logonType)
		{
			switch (logonType)
			{
				case TaskLogonType.InteractiveToken:
					this.flagRunOnlyWhenUserIsLoggedOn = true;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = false;
					break;
				case TaskLogonType.Group:
					this.flagRunOnlyWhenUserIsLoggedOn = true;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = true;
					break;
				case TaskLogonType.ServiceAccount:
					this.flagRunOnlyWhenUserIsLoggedOn = false;
					this.flagExecutorIsServiceAccount = true;
					this.flagExecutorIsGroup = false;
					break;
				default:
					this.flagRunOnlyWhenUserIsLoggedOn = false;
					this.flagExecutorIsServiceAccount = false;
					this.flagExecutorIsGroup = false;
					break;
			}

			if (this.flagExecutorIsServiceAccount)
			{
				taskLoggedOnRadio.Enabled = false;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (this.flagExecutorIsGroup)
			{
				taskLoggedOnRadio.Enabled = true;
				taskLoggedOptionalRadio.Enabled = false;
				taskLocalOnlyCheck.Enabled = false;
			}
			else if (this.flagRunOnlyWhenUserIsLoggedOn)
			{
				taskLoggedOnRadio.Enabled = true;
				taskLoggedOptionalRadio.Enabled = true;
				taskLocalOnlyCheck.Enabled = false;
			}
			else
			{
				taskLoggedOnRadio.Enabled = true;
				taskLoggedOptionalRadio.Enabled = true;
				taskLocalOnlyCheck.Enabled = true && (task == null || IsV2);
			}
			if (task != null)
				taskPrincipalText.Text = this.flagExecutorIsGroup ? td.Principal.GroupId : td.Principal.UserId;
			else
				taskPrincipalText.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
		}

		private void summaryPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			summaryPrompt.Visible = RegisterTaskOnFinish;
			if (SummaryRegistrationNotice != null)
				summaryPrompt.Text = SummaryRegistrationNotice;
			openDlgAfterCheck.Visible = AllowEditorOnFinish;
			if (EditorOnFinishText != null)
				openDlgAfterCheck.Text = EditorOnFinishText;
			string fmt = string.IsNullOrEmpty(SummaryFormatString) ? EditorProperties.Resources.WizardSummaryFormatString : SummaryFormatString;
			sumText.Text = string.Format(fmt,
				nameText.Text,
				descText.Text,
				trigger.ToString(),
				TaskPropertiesControl.BuildEnumString(TaskPropertiesControl.taskSchedResources, "ActionType", action.ActionType) + ": " + action.ToString());
			sumText.Select(0, 0);
		}

		private void taskLocalOnlyCheck_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = IsV2 ? ((taskLocalOnlyCheck.Checked) ? TaskLogonType.S4U : TaskLogonType.Password) : TaskLogonType.InteractiveTokenOrPassword;
		}

		private void taskLoggedOnRadio_CheckedChanged(object sender, EventArgs e)
		{
			if (!onAssignment)
				td.Principal.LogonType = TaskLogonType.InteractiveToken;
		}

		private void taskLoggedOptionalRadio_CheckedChanged(object sender, EventArgs e)
		{
			taskLocalOnlyCheck.Enabled = (task == null || IsV2) && taskLoggedOptionalRadio.Checked;
			taskLocalOnlyCheck_CheckedChanged(sender, e);
		}

		private void triggerPropPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			if (this.TriggerPropertiesInstructions != null)
				triggerPropText.Text = this.TriggerPropertiesInstructions;
		}

		private void triggerSelectionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			triggerSelectPage.AllowNext = (triggerSelectionList.SelectedIndex >= 0);
		}

		private void triggerSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			bool hasValue = (this.trigger != null);
			AvailableWizardTriggers selTrig = (AvailableWizardTriggers)triggerSelectionList.SelectedItem.Tag;
			switch (selTrig)
			{
				case AvailableWizardTriggers.Event:
					e.Page.NextPage = onEventTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Event)
						trigger = new EventTrigger();
					break;
				case AvailableWizardTriggers.Time:
					e.Page.NextPage = oneTimeTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Time)
						trigger = new TimeTrigger();
					oneTimeStartTimePicker.Value = trigger.StartBoundary;
					break;
				case AvailableWizardTriggers.Daily:
					e.Page.NextPage = dailyTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Daily)
						trigger = new DailyTrigger();
					dailyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case AvailableWizardTriggers.Weekly:
					e.Page.NextPage = weeklyTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Weekly)
						trigger = new WeeklyTrigger();
					weeklyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case AvailableWizardTriggers.Monthly:
					e.Page.NextPage = monthlyTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Monthly)
						trigger = new MonthlyTrigger();
					monthlyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case AvailableWizardTriggers.MonthlyDOW:
					e.Page.NextPage = monthlyTriggerPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.MonthlyDOW)
						trigger = new MonthlyDOWTrigger();
					monthlyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case AvailableWizardTriggers.Idle:
					e.Page.NextPage = actionSelectPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Idle)
						trigger = new IdleTrigger();
					break;
				case AvailableWizardTriggers.Boot:
					e.Page.NextPage = actionSelectPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Boot)
						trigger = new BootTrigger();
					break;
				case AvailableWizardTriggers.Logon:
					e.Page.NextPage = actionSelectPage;
					if (!hasValue || this.trigger.TriggerType != TaskTriggerType.Logon)
						trigger = new LogonTrigger();
					break;
				default:
					e.Cancel = true;
					break;
			}
		}

		private void triggerSelectPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			if (this.TriggerPagePrompt != null)
				this.triggerSelectPage.Text = this.TriggerPagePrompt;
		}

		private void weeklyCheck_CheckedChanged(object sender, System.EventArgs e)
		{
			var weeklyTrigger = (WeeklyTrigger)trigger;
			var cb = (CheckBox)sender;
			DaysOfTheWeek dow = (DaysOfTheWeek)cb.Tag;

			if (cb.Checked)
				weeklyTrigger.DaysOfWeek |= dow;
			else
			{
				// Ensure that ONE day is always checked.
				if (weeklyTrigger.DaysOfWeek == dow)
					cb.Checked = true;
				else
					weeklyTrigger.DaysOfWeek &= ~dow;
			}
		}

		private void weeklyTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			trigger.StartBoundary = weeklyStartTimePicker.Value;
			((WeeklyTrigger)trigger).WeeksInterval = (short)weeklyRecurNumUpDn.Value;
		}

		private void weeklyTriggerPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			weeklySunCheck.Tag = DaysOfTheWeek.Sunday;
			weeklyMonCheck.Tag = DaysOfTheWeek.Monday;
			weeklyTueCheck.Tag = DaysOfTheWeek.Tuesday;
			weeklyWedCheck.Tag = DaysOfTheWeek.Wednesday;
			weeklyThuCheck.Tag = DaysOfTheWeek.Thursday;
			weeklyFriCheck.Tag = DaysOfTheWeek.Friday;
			weeklySatCheck.Tag = DaysOfTheWeek.Saturday;
			if ((int)((WeeklyTrigger)trigger).DaysOfWeek == 0)
				((WeeklyTrigger)trigger).DaysOfWeek = DaysOfTheWeek.Sunday;
		}

		private void wizardControl1_Finished(object sender, System.EventArgs e)
		{
			bool myTS = false;

			if (this.TaskService == null)
			{
				this.TaskService = new TaskService();
				myTS = true;
			}

			td.Data = TaskName;
			td.RegistrationInfo.Description = descText.Text;
			td.Triggers.Clear();
			td.Triggers.Add(trigger);
			td.Actions.Clear();
			td.Actions.Add(action);
			if (openDlgAfterCheck.Checked)
			{
				TaskEditDialog dlg = new TaskEditDialog();
				dlg.Editable = true;
				dlg.Initialize(this.TaskService, td);
				dlg.RegisterTaskOnAccept = false;
				dlg.TaskName = TaskName;
				if (dlg.ShowDialog(this.ParentForm) == System.Windows.Forms.DialogResult.OK)
					this.td = dlg.TaskDefinition;
			}
			if (RegisterTaskOnFinish)
			{
				this.TaskService.RootFolder.RegisterTaskDefinition(TaskName, td, TaskCreation.CreateOrUpdate,
					td.Principal.ToString(), Password, td.Principal.LogonType);
			}

			if (myTS)
				this.TaskService = null;
		}

		private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
		{
		}
	}
}