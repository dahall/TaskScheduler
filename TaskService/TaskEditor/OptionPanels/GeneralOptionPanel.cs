
namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	public partial class GeneralOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public GeneralOptionPanel()
		{
			InitializeComponent();
		}

		public override bool Editable
		{
			get
			{
				return base.Editable;
			}
			set
			{
				base.Editable = value;
			}
		}

		public override TaskDefinition TaskDefintion
		{
			get
			{
				return base.TaskDefintion;
			}
			set
			{
				base.TaskDefintion = value;
			}
		}
	}
}
