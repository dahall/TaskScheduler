## About
A Windows Forms User Interface library supporting [**TaskScheduler**](https://www.nuget.org/packages/TaskScheduler), a .NET wrapper for the [Windows Task Scheduler](https://docs.microsoft.com/en-us/windows/win32/taskschd/task-scheduler-start-page). It provides controls mimicking all the UI components of the Windows Task Scheduler app with extended features and supporting controls.

More information can be found on the [project page on GitHub](https://github.com/dahall/taskscheduler).

## Support
Below are links to sites that provide in-depth examples, documentation and discussions. Please go here first with your questions as the community has been active for over a decade.
* [Wiki](https://github.com/dahall/TaskScheduler/wiki) - Sample code, library how-to, troubleshooting, etc.
* [API documentation](https://dahall.github.io/TaskScheduler) - Class/method/property documentation and examples
* [Full Issues Log](https://github.com/dahall/TaskScheduler/issues?q=) - Use the search box to see if your question may already be answered.
* [Discussion Forum](https://github.com/dahall/TaskScheduler/discussions) - Users helping users, enhancement requests, Q&A (retired Google forum [here](https://groups.google.com/forum/#!forum/taskscheduler))

## Key Features
Localized and localizable UI editors and a wizard for tasks which mimic the ones in Vista and later and adds optional pages for new properties. Following is the list of available UI controls:

* Task editor dialog and tabbed control which mimics system editor (`TaskEditDialog` and `TaskPropertiesControl`)
* Task editor dialog using more modern UI scheme (`TaskOptionsEditor`)
* Task creation wizard (`TaskSchedulerWizard`)
* Action editor dialog (`ActionEditDialog`)
* Trigger editor dialog (`TriggerEditDialog`)
* Windows Event Log viewer which works for all events, not just Task Scheduler's (`EventViewerDialog` and `EventViewerControl`)
* Task / task folder selection dialog (`TaskBrowserDialog`)
* Task history viewer (`TaskHistoryControl`)
* Task run-times viewer (`TaskRunTimesControl` and `TaskRunTimesDialog`)
* Task service remote connection dialog (`TaskServiceConnectDialog`)
* Simplified classes for pulling events from the system event log.
* A `DropDownCheckList` control that is very useful for selecting flag type enumerations.
* A `FullDateTimePicker` control which allows both date and time selection in a single control.
* A `CredentialsDialog` class for prompting for a password which wraps the Windows API.

The currently supported localizations include: English, Spanish, Italian, French, Chinese (Simplified), German, Polish and Russian.

## Usage
```C#
// Create a new task
Task t = TaskService.Instance.AddTask("Test", QuickTriggerType.Daily, "myprogram.exe");

// Edit task and re-register if user clicks Ok
TaskEditDialog editorForm = new TaskEditDialog(task: t, editable: true, registerOnAccept: true);
editorForm.ShowDialog();
```

For extended examples on how to the use the library, look at the [Examples Page](https://github.com/dahall/TaskScheduler/wiki/Examples).