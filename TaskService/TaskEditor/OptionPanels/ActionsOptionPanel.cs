namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class ActionsOptionPanel : OptionPanel
	{
		public ActionsOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			actionCollectionUI1.RefreshState();
		}
	}
}
