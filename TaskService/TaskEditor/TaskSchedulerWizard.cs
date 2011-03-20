using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// A wizard that walks the user through the creation of a simple task.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Wizard that walks the user through the creation of a simple task."), Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DesignTimeVisible(true), DefaultProperty("RegisterTaskOnFinish")]
	public sealed partial class TaskSchedulerWizard :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private Action action;
		private Trigger trigger;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSchedulerWizard"/> class.
		/// </summary>
		public TaskSchedulerWizard()
		{
			InitializeComponent();
			wizardControl1.TitleIcon = this.Icon;
			RegisterTaskOnFinish = false;
		}

		/// <summary>
		/// Gets or sets a value indicating whether to register the task on Finish.
		/// </summary>
		/// <value><c>true</c> if task registered on Finish; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Indicates whether to register the task on Finish")]
		public bool RegisterTaskOnFinish
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form displays an icon in the caption bar; otherwise, false. The default is true.
		/// </returns>
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DefaultValue(true)]
		public new bool ShowIcon
		{
			get { return base.ShowIcon; }
			set { base.ShowIcon = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskDefinition"/>.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskDefinition TaskDefinition
		{
			get; set;
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

		private void actionSelectionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			actionSelectPage.AllowNext = (actionSelectionList.SelectedIndex >= 0);
		}

		private void actionSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			switch (actionSelectionList.SelectedIndex)
			{
				case 0:
					e.Page.NextPage = runActionPage;
					action = new ExecAction();
					break;
				case 1:
					e.Page.NextPage = msgActionPage;
					action = new ShowMessageAction();
					break;
				case 2:
					e.Page.NextPage = emailActionPage;
					action = new EmailAction();
					break;
				default:
					break;
			}
		}

		private void dailyTriggerPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			trigger.StartBoundary = dailyStartTimePicker.Value;
			((DailyTrigger)trigger).DaysInterval = (short)dailyRecurNumUpDn.Value;
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

		private void execProgBrowseBtn_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				execProgText.Text = openFileDialog1.FileName;
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
					MessageBox.Show(this, Properties.Resources.WizardMonthlyTriggerInvalid, Properties.Resources.WizardMonthlyTriggerErrorTitle);
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
					MessageBox.Show(this, Properties.Resources.WizardMonthlyDOWTriggerInvalid, Properties.Resources.WizardMonthlyTriggerErrorTitle);
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
				monthlyDaysDropDown.Items.Add(new DropDownCheckListItem(Properties.Resources.Last, 99));
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
				MessageBox.Show(this, Properties.Resources.WizardEventTriggerInvalid, Properties.Resources.WizardEventTriggerErrorTitle);
			}
		}

		private void onEventTriggerPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			if (onEventLogCombo.Items.Count == 0)
				onEventLogCombo.Items.AddRange(SystemEventEnumerator.GetEventLogs(null));
		}

		private void runActionPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			((ExecAction)action).Path = execProgText.Text;
			((ExecAction)action).Arguments = execArgText.Text;
			((ExecAction)action).WorkingDirectory = execDirText.Text;
		}

		private void summaryPage_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
		{
			sumNameText.Text = nameText.Text;
			sumDescText.Text = descText.Text;
			sumTriggerText.Text = trigger.ToString();
			sumActionText.Text = TaskPropertiesControl.BuildEnumString(TaskPropertiesControl.taskSchedResources, "ActionType", action.ActionType) + ": " + action.ToString();
		}

		private void triggerSelectionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			triggerSelectPage.AllowNext = (triggerSelectionList.SelectedIndex >= 0);
		}

		private void triggerSelectPage_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
		{
			switch (triggerSelectionList.SelectedIndex)
			{
				case 0:
					e.Page.NextPage = dailyTriggerPage;
					trigger = new DailyTrigger();
					dailyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case 1:
					e.Page.NextPage = weeklyTriggerPage;
					trigger = new WeeklyTrigger();
					weeklyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case 2:
					e.Page.NextPage = monthlyTriggerPage;
					trigger = new MonthlyTrigger();
					monthlyStartTimePicker.Value = trigger.StartBoundary;
					break;
				case 3:
					e.Page.NextPage = oneTimeTriggerPage;
					trigger = new TimeTrigger();
					oneTimeStartTimePicker.Value = trigger.StartBoundary;
					break;
				case 4:
					e.Page.NextPage = actionSelectPage;
					trigger = new BootTrigger();
					break;
				case 5:
					e.Page.NextPage = actionSelectPage;
					trigger = new LogonTrigger();
					break;
				case 6:
					e.Page.NextPage = onEventTriggerPage;
					trigger = new EventTrigger();
					break;
				default:
					e.Cancel = true;
					break;
			}
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

			TaskDefinition td = this.TaskService.NewTask();
			td.Data = TaskName;
			td.RegistrationInfo.Description = descText.Text;
			td.Triggers.Add(trigger);
			td.Actions.Add(action);
			this.TaskDefinition = td;
			if (RegisterTaskOnFinish || openDlgAfterCheck.Checked)
			{
				Task t = this.TaskService.RootFolder.RegisterTaskDefinition(TaskName, td);
				if (openDlgAfterCheck.Checked)
				{
					TaskEditDialog dlg = new TaskEditDialog();
					dlg.Editable = true;
					dlg.Initialize(t);
					dlg.RegisterTaskOnAccept = true;
					dlg.ShowDialog(this.ParentForm);
				}
			}

			if (myTS)
				this.TaskService = null;
		}
	}
}