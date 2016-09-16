namespace Microsoft.Win32.TaskScheduler.UIComponents
{
	internal interface IActionHandler
	{
		event System.EventHandler KeyValueChanged;
		Action Action { get; set; }
		bool CanValidate { get; }
		bool ValidateFields();
		void Run();
	}
}
