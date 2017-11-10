namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class TriggersOptionPanel : OptionPanel
	{
		public TriggersOptionPanel()
		{
			InitializeComponent();
		}

		protected override void InitializePanel()
		{
			triggerCollectionUI1.AvailableTriggers = parent.AvailableTriggers;
			triggerCollectionUI1.RefreshState();
		}
	}
}
