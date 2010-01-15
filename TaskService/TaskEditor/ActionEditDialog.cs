using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// An editor that handles all Task actions.
	/// </summary>
    public partial class ActionEditDialog : Form
    {
        private Action action;
		private bool isV2 = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionEditDialog"/> class.
		/// </summary>
		public ActionEditDialog()
		{
			InitializeComponent();
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

				if (action is ExecAction)
                {
                    actionsCombo.SelectedIndex = 0;
                    execProgText.Text = ((ExecAction)action).Path;
                    execArgText.Text = ((ExecAction)action).Arguments;
                    execDirText.Text = ((ExecAction)action).WorkingDirectory;
                }
                else if (action is EmailAction)
                {
                    actionsCombo.SelectedIndex = 1;
                    emailFromText.Text = ((EmailAction)action).From;
                    emailToText.Text = ((EmailAction)action).To;
                    emailSubjectText.Text = ((EmailAction)action).Subject;
                    emailTextText.Text = ((EmailAction)action).Body;
                    //emailAttachmentText.Text = ((EmailAction)action).Attachments;
                    emailSMTPText.Text = ((EmailAction)action).Server;
                }
                else if (action is ShowMessageAction)
                {
                    actionsCombo.SelectedIndex = 2;
                    msgTitleText.Text = ((ShowMessageAction)action).Title;
                    msgMsgText.Text = ((ShowMessageAction)action).MessageBody;
                }
                else if (action is ComHandlerAction)
                {
                    actionsCombo.SelectedIndex = 3;
                    comCLSIDText.Text = ((ComHandlerAction)action).ClassId.ToString();
                    comDataText.Text = ((ComHandlerAction)action).Data;
                }
            }
        }

		/// <summary>
		/// Gets or sets a value indicating whether this editor only supports V1 actions.
		/// </summary>
		/// <value><c>true</c> if supports V1 only; otherwise, <c>false</c>.</value>
		public bool SupportV1Only
		{
			get { return !isV2; }
			set
			{
				isV2 = !value;
				if (!isV2 && actionsCombo.Items.Count > 1)
				{
					actionsCombo.SelectedIndex = 0;
					actionsCombo.Items.RemoveAt(3);
					actionsCombo.Items.RemoveAt(2);
					actionsCombo.Items.RemoveAt(1);
				}
				else if (isV2 && actionsCombo.Items.Count < 4)
				{
					System.Resources.ResourceManager rm = new System.Resources.ResourceManager(this.GetType());
					actionsCombo.Items.Add(rm.GetString("actionsCombo.Items1"));
					actionsCombo.Items.Add(rm.GetString("actionsCombo.Items2"));
					actionsCombo.Items.Add(rm.GetString("actionsCombo.Items3"));
				}
			}
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
                    break;
                case 2:
                    ret = new ShowMessageAction(msgMsgText.Text, msgTitleText.Text);
                    break;
                case 3:
                    ret = new ComHandlerAction(new Guid(comCLSIDText.Text), null);
                    if (comDataText.TextLength > 0)
                        ((ComHandlerAction)ret).Data = comDataText.Text;
                    break;
            }
            action = ret;
        }

        private void actionsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingsTabs.SelectedTab = settingsTabs.TabPages[actionsCombo.SelectedIndex];
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comCLSIDText_Validating(object sender, CancelEventArgs e)
        {
            Guid guid = Guid.Empty;
            // See if this is a valid GUID string
            try { guid = new Guid(comCLSIDText.Text); } catch {}
            if (guid != Guid.Empty)
            {
                // See if this is a valid COM object
                try
                {
                    Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\" + guid.ToString("B"));
                    if (key == null)
                        guid = Guid.Empty;
                    else
                        key.Close();
                }
                catch { guid = Guid.Empty; }
            }
            e.Cancel = guid == Guid.Empty;
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

        private void okBtn_Click(object sender, EventArgs e)
        {
            UpdateAction();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}