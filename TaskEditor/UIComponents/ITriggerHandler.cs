namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	interface ITriggerHandler
	{
		Trigger Trigger { get; set; }
		bool IsV2 { get; set; }
		bool ShowStartBoundary { get; set; }
		bool IsTriggerValid();
	}
}
