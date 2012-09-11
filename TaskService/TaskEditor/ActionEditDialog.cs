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
	public partial class ActionEditDialog : Form
	{
		private Action action;
		private bool isV2 = true;
		private bool useUnifiedSchedulingEngine = false;

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
			this.Action = action;
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		/// <value>The action.</value>
		[DefaultValue(null), Browsable(false)]
		public Action Action
		{
			get
			{
				return action;
			}
			set
			{
				action = value;

				// Try and determine if this is a V1 task
				string id = action.Id;
				try { action.Id = "test"; action.Id = id; SupportV1Only = false; }
				catch { SupportV1Only = true; }

				actionsCombo.SelectedIndex = actionsCombo.Items.IndexOf((long)action.ActionType);

				if (action is ExecAction)
				{
					execProgText.Text = ((ExecAction)action).Path;
					execArgText.Text = ((ExecAction)action).Arguments;
					execDirText.Text = ((ExecAction)action).WorkingDirectory;
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
				}
				else if (action is ShowMessageAction)
				{
					msgTitleText.Text = ((ShowMessageAction)action).Title;
					msgMsgText.Text = ((ShowMessageAction)action).MessageBody;
				}
				else if (action is ComHandlerAction)
				{
					ComCLSID = ((ComHandlerAction)action).ClassId;
					comDataText.Text = ((ComHandlerAction)action).Data;
				}
				ValidateCurrentAction();
			}
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

		private Guid ComCLSID
		{
			get
			{
				string guid = toolTip.GetToolTip(comCLSIDText);
				try { Guid g = new Guid(guid); return g; }
				catch { }
				return Guid.Empty;
			}
			set
			{
				if (value == Guid.Empty)
				{
					comCLSIDText.Clear();
					toolTip.SetToolTip(comCLSIDText, null);
				}
				else
				{
					toolTip.SetToolTip(comCLSIDText, value.ToString());
					comCLSIDText.Text = GetNameForCLSID(value);
				}
			}
		}

		internal static string GetNameForCLSID(Guid guid)
		{
			using (RegistryKey k = Registry.ClassesRoot.OpenSubKey("CLSID", false))
			{
				if (k != null)
				{
					using (RegistryKey k2 = k.OpenSubKey(guid.ToString("B"), false))
						return k2 != null ? k2.GetValue(null) as string : null;
				}
			}
			return null;
		}

		private void actionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch ((TaskActionType)Convert.ToInt32(((DropDownCheckListItem)actionsCombo.SelectedItem).Value))
			{
				case TaskActionType.ComHandler:
					settingsTabs.SelectedTab = comTab;
					break;
				case TaskActionType.SendEmail:
					settingsTabs.SelectedTab = emailTab;
					break;
				case TaskActionType.ShowMessage:
					settingsTabs.SelectedTab = messageTab;
					break;
				case TaskActionType.Execute:
				default:
					settingsTabs.SelectedTab = execTab;
					break;
			}
			ValidateCurrentAction();
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void emailAttachementBrowseBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				emailAttachmentText.Text = openFileDialog1.FileName;
		}

		private void execProgBrowseBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				execProgText.Text = openFileDialog1.FileName;
		}

		private void getCLSIDButton_Click(object sender, EventArgs e)
		{
			try
			{
				ComObjectSelectionDialog dlg = new ComObjectSelectionDialog();
				dlg.SupportedInterface = new Guid("839D7762-5121-4009-9234-4F0D19394F04");
				if (dlg.ShowDialog(this) == DialogResult.OK)
					ComCLSID = dlg.CLSID;
			}
			catch { }
			ValidateCurrentAction();
		}

		private void keyField_TextChanged(object sender, EventArgs e)
		{
			ValidateCurrentAction();
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			UpdateAction();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void ResetCombo()
		{
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
			actionsCombo.SelectedIndex = 0;
			actionsCombo.EndUpdate();
		}

		private void UpdateAction()
		{
			Action ret;
			switch (actionsCombo.SelectedIndex)
			{
				case 0:
				default:
					ret = new ExecAction(execProgText.Text, null, null);
					if (execArgText.TextLength > 0)
						((ExecAction)ret).Arguments = execArgText.Text;
					if (execDirText.TextLength > 0)
						((ExecAction)ret).WorkingDirectory = execDirText.Text;
					break;
				case 1:
					ret = new EmailAction(emailSubjectText.Text, emailFromText.Text,
						emailToText.Text, emailTextText.Text, emailSMTPText.Text);
					if (emailAttachmentText.TextLength > 0)
						((EmailAction)ret).Attachments = new object[] { emailAttachmentText.Text };
					break;
				case 2:
					ret = new ShowMessageAction(msgMsgText.Text, msgTitleText.Text);
					break;
				case 3:
					ret = new ComHandlerAction(ComCLSID, comDataText.TextLength > 0 ? comDataText.Text : null);
					break;
			}
			action = ret;
		}

		private void ValidateCurrentAction()
		{
			bool isValid = false;
			switch (settingsTabs.SelectedIndex)
			{
				case 0:
				default:
					isValid = execProgText.TextLength > 0;
					break;
				case 1:
					isValid = emailSubjectText.TextLength > 0 && emailFromText.TextLength > 0 &&
						emailToText.TextLength > 0 && emailTextText.TextLength > 0 && emailSMTPText.TextLength > 0;
					break;
				case 2:
					isValid = msgMsgText.TextLength > 0;
					break;
				case 3:
					isValid = ComCLSID != null;
					break;
			}
			okBtn.Enabled = isValid;
		}
	}
}