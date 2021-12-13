# ![](docs/icons/tsnew48.png) Task Scheduler Managed Wrapper
[![Version](https://img.shields.io/github/release/dahall/TaskScheduler.svg?style=flat-square)](https://github.com/dahall/TaskScheduler/releases) [![Downloads](https://img.shields.io/nuget/dt/TaskScheduler.svg?style=flat-square)](https://www.nuget.org/packages/TaskScheduler/) [![Build status](https://ci.appveyor.com/api/projects/status/entp0dead4840cwm?svg=true)](https://ci.appveyor.com/project/dahall/taskscheduler)

> The original .NET wrapper for the Windows Task Scheduler that aggregates the multiple versions and provides localized controls for editing.

## Quick Links
* [Wiki](https://github.com/dahall/TaskScheduler/wiki) - Sample code, library how-to, troubleshooting, etc.
* [API documentation](https://dahall.github.io/TaskScheduler) - Class/method/property documentation and examples
* [Full Issues Log](https://github.com/dahall/TaskScheduler/issues?q=) - Use the search box to see if your question may already be answered.
* [Discussion Forum](https://github.com/dahall/TaskScheduler/discussions) - Users helping users, enhancement requests, Q&A (retired Google forum [here](https://groups.google.com/forum/#!forum/taskscheduler))
* [ITaskHandler Template and Sample Project](https://github.com/dahall/ITaskHandlerTemplate) - Use this to create your own COM based  assembly for in-process actions.
* [Troubleshooting Tool](https://github.com/dahall/TaskSchedulerConfig) - Tool to help identify and fix configuration and connectivity issues. (ClickOnce installer [here](https://github.com/dahall/TaskSchedulerConfig/blob/master/publish/setup.exe?raw=true))

## Installation
This project's assemblies are available via NuGet or manually from the .zip files in the [Releases](https://github.com/dahall/TaskScheduler/releases) section.

|Link|Package Name|Description|
|------------|------------|-----------|
|[![NuGet](https://img.shields.io/nuget/v/TaskScheduler.svg?style=flat-square)](https://www.nuget.org/packages/TaskScheduler)| TaskScheduler|Main Library|
|[![NuGet](https://img.shields.io/nuget/v/TaskSchedulerEditor.svg?style=flat-square)](https://www.nuget.org/packages/TaskSchedulerEditor)|TaskSchedulerEditor|UI Library|

You can also find prerelease NuGet pacakges on the [project's AppVeyor NuGet feed](https://ci.appveyor.com/nuget/taskscheduler-prerelease).

Once referenced by your project, all classes can be found in the `Microsoft.Win32.TaskScheduler` namespace.

## Project Components
### Main Library
Microsoft introduced version 2.0 (internally version 1.2) with a completely new object model with Windows Vista. The managed assembly closely resembles the new object model, but allows the 1.0 (internally version 1.1) COM objects to be manipulated. It will automatically choose the most recent version of the library found on the host system (up through 1.4). Core features include:

* Separate, functionally identical, libraries for .NET 2.0, 3.5, 4.0, 4.52, 5.0, 6.0, .NET Standard 2.0, .NET Core 2.0, 2.1, 3.0, 3.1.
* Unlike the base COM libraries, this wrapper helps to create and view tasks up and down stream.
* Written in C#, but works with any .NET language including scripting languages (e.g. PowerShell).
* Supports all V2 native properties, even under V1 systems.
* Maintain EmailAction and ShowMessageAction using PowerShell scripts for systems after Win8 where these actions have been deprecated.
* Supports all action types (not just ExecAction) on V1 systems (XP/WS2003) and earlier (if PowerShell is installed).
* Supports multiple actions on V1 systems (XP/WS2003). Native library only supports a single action.
* Supports serialization to XML for both 1.0 and 2.0 tasks (base library only supports 2.0)
* Supports task validation for targeted version.
* Supports secure task reading and maintenance.
* Fluent methods for task creation.
* Cron syntax for trigger creation.
* Supports reading "custom" triggers under Win8 and later.
* Numerous work-arounds and checks to compensate for base library shortcomings.

The currently supported localizations include: English, Spanish, Italian, French, Chinese (Simplified), German, Polish and Russian. Others localizations can be added upon request, especially if you're willing to help with translations.   

The project is based on work the originator started in January 2002 with the 1.0 library that is currently hosted on [CodeProject](http://www.codeproject.com/KB/system/taskschedulerlibrary.aspx).  

### UI Library
A second library includes localized and localizable GUI editors and a wizard for tasks which mimic the ones in Vista and later and adds optional pages for new properties. Following is the list of available UI controls:

* Task editor dialog and tabbed control which mimics system editor (TaskEditDialog)
* Task editor dialog using newer UI scheme (TaskOptionsEditor)
* Task creation wizard which mimics system Basic editor
* Action editor dialog
* Trigger editor dialog
* Event viewer dialog
* Task / task folder selection dialog
* Task history viewer
* Task run-times viewer
* Task service connection dialog
* Simplified classes for pulling events from the system event log.
* A DropDownCheckList control that is very useful for selecting flag type enumerations.
* A FullDateTimePicker control which allows both date and time selection in a single control.
* A CredentialsDialog class for prompting for a password which wraps the Windows API.

## Sample Code
There is a help file included with the download that provides an overview of the various classes. There are numerous examples under the "Documentation" tab.  

You can perform a number of actions in a single line of code:  
```C#
// Run a program every day on the local machine
TaskService.Instance.AddTask("Test", QuickTriggerType.Daily, "myprogram.exe", "-a arg");

// Run a custom COM handler on the last day of every month
TaskService.Instance.AddTask("Test", new MonthlyTrigger { RunOnLastDayOfMonth = true }, 
    new ComHandlerAction(new Guid("{CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734}")));
```

For many more options, use the library classes to build a complex task. Below is a brief example of how to use the library from C#.  
```C#
using System;
using Microsoft.Win32.TaskScheduler;

class Program
{
   static void Main(string[] args)
   {
      // Get the service on the remote machine
      using (TaskService ts = new TaskService(@"\\RemoteServer", "username", "domain", "password"))
      {
         // Create a new task definition and assign properties
         TaskDefinition td = ts.NewTask();
         td.RegistrationInfo.Description = "Does something";

         // Create a trigger that will fire the task at this time every other day
         td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });

         // Create an action that will launch Notepad whenever the trigger fires
         td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

         // Register the task in the root folder.
         // (Use the username here to ensure remote registration works.)
         ts.RootFolder.RegisterTaskDefinition(@"Test", td, TaskCreation.CreateOrUpdate, "username");
      }
   }
}
```

For extended examples on how to the use the library, look in the source code area or look at the [Examples Page](https://github.com/dahall/TaskScheduler/wiki/Examples). The library closely follows the Task Scheduler 2.0 Scripting classes. Microsoft has some examples on [Microsoft Docs](https://docs.microsoft.com/en-us/windows/win32/taskschd/task-scheduler-start-page) around it that may further help you understand how to use this library.

___
This project appreciatively uses:

[<img alt="ReSharper from JetBrains" src="docs/icons/resharper-logo.svg" height="5%" width="5%"/> ReSharper from JetBrains](https://www.jetbrains.com/?from=Task%20Scheduler%20Managed%20Wrapper)

[<img alt="Sandcastle Help File Builder" src="https://github.com/EWSoftware/SHFB/blob/master/NuGet/SHFB.png?raw=true"/> Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB)
