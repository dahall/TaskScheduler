using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[System.ComponentModel.DefaultEvent("KeyValueChanged"), System.ComponentModel.DefaultProperty("Action")]
	internal partial class EmailActionUI : UserControl, IActionHandler
	{
		public EmailActionUI()
		{
			InitializeComponent();
		}

		private void emailAttachementBrowseBtn_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
				emailAttachmentText.Text = string.Join(";", openFileDialog1.FileNames);
		}

		[System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public Action Action
		{
			get
			{
				Action ret = new EmailAction(emailSubjectText.Text, emailFromText.Text,
					emailToText.Text, emailTextText.Text, emailSMTPText.Text);
				if (emailAttachmentText.TextLength > 0)
					((EmailAction)ret).Attachments = Array.ConvertAll<string, object>(emailAttachmentText.Text.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries), s => s);
				return ret;
			}
			set
			{
				var ea = value as EmailAction;
				if (ea == null) return;
				emailFromText.Text = ea.From;
				emailToText.Text = ea.To;
				emailSubjectText.Text = ea.Subject;
				emailTextText.Text = ea.Body;
				if (ea.Attachments != null && ea.Attachments.Length > 0)
					emailAttachmentText.Text = string.Join(";", Array.ConvertAll<object, string>(ea.Attachments, o => (string)o));
				emailSMTPText.Text = ea.Server;
			}
		}

		public bool ValidateFields() => true;

		public void Run() { MessageBox.Show(this.ParentForm, "", null, MessageBoxButtons.OK, MessageBoxIcon.Error); }

		public bool CanValidate => emailSubjectText.TextLength > 0 && emailFromText.TextLength > 0 &&
				emailToText.TextLength > 0 && emailTextText.TextLength > 0 && emailSMTPText.TextLength > 0;

		public event EventHandler KeyValueChanged;

		private void keyField_TextChanged(object sender, EventArgs e)
		{
			KeyValueChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}