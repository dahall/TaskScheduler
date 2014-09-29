using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	public partial class OptionPanel : UserControl
	{
		public OptionPanel()
		{
			InitializeComponent();
		}

		[Browsable(true), DefaultValue((string)null), Category("Appearance")]
		public override string Text
		{
			get { return panelTitleLabel.Text; }
			set { panelTitleLabel.Text = value; }
		}

		[Browsable(true), DefaultValue(null), Category("Appearance")]
		public Image Image
		{
			get { return panelImage.Image; }
			set { panelImage.Image = value; }
		}

		public virtual TaskDefinition TaskDefintion
		{
			get { return null; }
			set { }
		}

		public virtual bool Editable
		{
			get { return true; }
			set { }
		}
	}
}
