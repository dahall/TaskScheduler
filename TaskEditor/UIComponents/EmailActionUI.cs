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
				emailFromText.Text = ((EmailAction)value).From;
				emailToText.Text = ((EmailAction)value).To;
				emailSubjectText.Text = ((EmailAction)value).Subject;
				emailTextText.Text = ((EmailAction)value).Body;
				if (((EmailAction)value).Attachments != null && ((EmailAction)value).Attachments.Length > 0)
					emailAttachmentText.Text = string.Join(";", Array.ConvertAll<object, string>(((EmailAction)value).Attachments, o => (string)o));
				emailSMTPText.Text = ((EmailAction)value).Server;
			}
		}

		public bool AllowRun { get; set; }

		public bool IsActionValid() => emailSubjectText.TextLength > 0 && emailFromText.TextLength > 0 &&
				emailToText.TextLength > 0 && emailTextText.TextLength > 0 && emailSMTPText.TextLength > 0;

		public event EventHandler KeyValueChanged;

		private void keyField_TextChanged(object sender, EventArgs e)
		{
			EventHandler h = KeyValueChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}
	}
}
