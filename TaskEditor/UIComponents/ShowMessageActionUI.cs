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

		[System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		public Action Action
		{
			get
			{
				return new ShowMessageAction(msgMsgText.Text, msgTitleText.Text);
			}
			set
			{
				msgTitleText.Text = ((ShowMessageAction)value).Title;
				msgMsgText.Text = ((ShowMessageAction)value).MessageBody;
			}
		}

		public bool IsActionValid() => msgMsgText.TextLength > 0;

		public event EventHandler KeyValueChanged;

		private void msgMsgText_TextChanged(object sender, EventArgs e)
		{
			EventHandler h = KeyValueChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}
	}
}
