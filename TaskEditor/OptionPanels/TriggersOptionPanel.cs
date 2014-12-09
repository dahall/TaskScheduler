namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class TriggersOptionPanel : Microsoft.Win32.TaskScheduler.OptionPanels.OptionPanel
	{
		public TriggersOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			triggerCollectionUI1.RefreshState();
		}
	}
}
