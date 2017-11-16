namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class ActionsOptionPanel : OptionPanel
	{
		public ActionsOptionPanel()
		{
			InitializeComponent();
			// If the value of PowerShellConversion changes, update which actions can be edited
			actionCollectionUI1.PowerShellConversionChanged += (sender, args) => parent.UpdateAvailableActions(parent.AvailableActions);
		}

		protected override void InitializePanel()
		{
			actionCollectionUI1.AvailableActions = parent.AvailableActions;
			actionCollectionUI1.ShowActionRunButton = parent.ShowActionRunButton;
			actionCollectionUI1.ShowPowerShellConversionCheck = parent.ShowConvertActionsToPowerShellCheck;
			actionCollectionUI1.RefreshState();
		}
	}
}
