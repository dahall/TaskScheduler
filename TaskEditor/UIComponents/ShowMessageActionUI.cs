using System;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	[System.ComponentModel.DefaultEvent("KeyValueChanged"), System.ComponentModel.DefaultProperty("Action")]
	internal partial class ShowMessageActionUI : UserControl, IActionHandler
	{
		public ShowMessageActionUI()
		{
			InitializeComponent();
		}

		public event EventHandler KeyValueChanged;

		[System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public Action Action
		{
			get { return new ShowMessageAction(msgMsgText.Text, msgTitleText.Text); }
			set
			{
				var ma = value as ShowMessageAction;
				if (ma == null) return;
				msgTitleText.Text = ma.Title;
				msgMsgText.Text = ma.MessageBody;
			}
		}

		public bool CanValidate => msgMsgText.TextLength > 0;

		public void Run()
		{
			MessageBox.Show(this, msgMsgText.Text, msgTitleText.Text, MessageBoxButtons.OK);
		}

		public bool ValidateFields() => true;

		private void msgMsgText_TextChanged(object sender, EventArgs e)
		{
			KeyValueChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}