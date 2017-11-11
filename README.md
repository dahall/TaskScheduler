# Task Scheduler Managed Wrapper
[![Version](https://img.shields.io/github/release/dahall/TaskScheduler.svg?style=flat-square)](https://github.com/dahall/TaskScheduler/releases)

> Provides a .NET wrapper for the Windows Task Scheduler. It aggregates the multiple versions, provides an editor and allows for localization. [![Downloads](https://img.shields.io/nuget/dt/TaskScheduler.svg?style=flat-square)](https://www.nuget.org/packages/TaskScheduler/) 

## Quick Links
* [Discussion Forum (users helping users, enhancement requests, Q&A)](https://groups.google.com/forum/#!forum/taskscheduler)
* [Documentation Wiki (samples, library how-to, etc.)](https://github.com/dahall/TaskScheduler/wiki/Documentation)
* [API documentation (class/method/property help)](https://dahall.github.io/TaskScheduler)
* [Issues](https://github.com/dahall/TaskScheduler/issues)

## Installation
This project's assemblies are available via NuGet.

|Link|Package Name|Description|
|------------|------------|-----------|
|[![NuGet](https://img.shields.io/nuget/v/TaskScheduler.svg?style=flat-square)](https://www.nuget.org/packages/TaskScheduler)| TaskScheduler|Main Library|
|[![NuGet](https://img.shields.io/nuget/v/TaskSchedulerEditor.svg?style=flat-square)](https://www.nuget.org/packages/TaskSchedulerEditor)|TaskSchedulerEditor|UI Library|

## Project Components
### Main Library
Microsoft introduced version 2.0 (internally version 1.2) with a completely new object model with Windows Vista. The managed assembly closely resembles the new object model, but allows the 1.0 (internally version 1.1) COM objects to be manipulated. It will automatically choose the most recent version of the library found on the host system (up through 1.4). Core features include:

* Separate, functionally identical, libraries for .NET 2.0 and 4.0\.
* Unlike the base library, this wrapper helps to create and view tasks up and down stream.
* Written in C#, but works with any .NET language including scripting languages (e.g. PowerShell).
* Supports almost all V2 native properties, even under V1 systems.
* Maintain EmailAction and ShowMessageAction using PowerShell scripts for systems after Win8 where these actions have been deprecated
* Supports all action types (not just ExecAction) on V1 systems (XP/WS2003) and earlier (if PowerShell is installed).
* Supports multiple actions on V1 systems (XP/WS2003). Native library only supports a single action.
* Supports serialization to XML for both 1.0 and 2.0 tasks (base library only supports 2.0)
* Supports task validation for targeted version.
* Supports secure task reading and maintenance.
* Fluent methods for task creation.
* Cron syntax for trigger creation.
* Supports reading "custom" triggers under Win8 and later.
* Numerous work-arounds and checks to compensate for base library shortcomings.

The project supports a number of languages and, upon request, is ready to support others. The currently supported languages include: English, Spanish, Italian, French, Chinese (Simplified), German.  

The project is based on work the originator started in January 2002 with the 1.0 library that is currently hosted on [CodeProject](http://www.codeproject.com/KB/system/taskschedulerlibrary.aspx).  

### UI Library
There is a second library that includes localized and localizable GUI editors and a wizard for tasks which mimic the ones in Vista and later and adds optional pages for new properties. Following is the list of available UI controls:

* A DropDownCheckList control that is very useful for selecting flag type enumerations.
* A FullDateTimePicker control which allows both date and time selection in a single control.
* A CredentialsDialog class for prompting for a password which wraps the Windows API.
* Simplified classes for pulling events from the system event log.
* Action editor dialog
* Trigger editor dialog
* Task editor dialog and tabbed control
* Event viewer dialog
* Task / task folder selection dialog
* Task history viewer
* Task run-times viewer
* Task creation wizard
* Task service connection dialog

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
      using (TaskService ts = new TaskService(@"\\RemoteServer"))
      {
         // Create a new task definition and assign properties
         TaskDefinition td = ts.NewTask();
         td.RegistrationInfo.Description = "Does something";

         // Create a trigger that will fire the task at this time every other day
         td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });

         // Create an action that will launch Notepad whenever the trigger fires
         td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

         // Register the task in the root folder
         ts.RootFolder.RegisterTaskDefinition(@"Test", td);
      }
   }
}
```

For extended examples on how to the use the library, look in the source code area or look at the [Examples Page](https://github.com/dahall/TaskScheduler/wiki/Examples). The library closely follows the Task Scheduler 2.0 Scripting classes. Microsoft has some examples on [MSDN](http://msdn2.microsoft.com/en-us/library/aa384006(VS.85).aspx) around it that may further help you understand how to use this library.
