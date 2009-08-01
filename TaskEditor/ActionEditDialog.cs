using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class ActionEditDialog : Form
	{
		private Action action;

		public ActionEditDialog()
		{
			InitializeComponent();
			actionsCombo.SelectedIndex = 0;
		}

		public Action Action
		{
			get
			{
				return action;
			}
			set
			{
				action = value;
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

		private void okBtn_Click(object sender, EventArgs e)
		{
			UpdateAction();
			DialogResult = DialogResult.OK;
			Close();
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

		private void actionsCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			settingsTabs.SelectedTab = settingsTabs.TabPages[actionsCombo.SelectedIndex];
		}

		private void comCLSIDText_Validating(object sender, CancelEventArgs e)
		{
			//TODO: Check for valid COM object
			try { new Guid(comCLSIDText.Text); }
			catch { e.Cancel = true; }
			e.Cancel = false;
		}
	}
}
