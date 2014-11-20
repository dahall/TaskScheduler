using Microsoft.Win32.TaskScheduler;
using System;
using System.EnterpriseServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace $safeprojectname$
{
	/// <summary>
	/// 
	/// </summary>
	[ObjectPooling(MinPoolSize = 2, MaxPoolSize = 10, CreationTimeout = 20)]
	[Transaction(TransactionOption.Required)]
	[ComVisible(true), Guid("$guid2$"), ClassInterface(ClassInterfaceType.None)]
	public class TaskHandler1 : TaskHandlerBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskHandler1"/> class.
		/// </summary>
		public TaskHandler1()
		{
		}

		/// <summary>
		/// Called to start the COM handler.
		/// </summary>
		/// <param name="data">Data string passed in from Task Scheduler action.</param>
		public override void Start(string data)
		{
			// When implementing this method, the handler should return control immediately.

			// After the handler starts its processing, it can call the this.StatusHandler.UpdateStatus method to indicate
			// its percentage of completion or call the this.StatusHandler.TaskCompleted method to indicate when the
			// handler has completed its processing. 

			// if (notFinishedWorking)
			//     this.StatusHandler.UpdateStatus(percentComplete, statusMessageString);
			// else
			//     this.StatusHandler.TaskCompleted(0); // or an error code on failure
		}

		/// <summary>
		/// Called to stop the COM handler.
		/// </summary>
		/// <returns>The return code that the Task Schedule will raise as an event when the COM handler action is completed. Return 0 on success.</returns>
		public override int Stop()
		{
			return 0;
		}

		/// <summary>
		/// Called to pause the COM handler. Implementing this method is optional.
		/// </summary>
		public override void Pause()
		{
		}

		/// <summary>
		/// Called to resume the COM handler. Implementing this method is optional.
		/// </summary>
		public override void Resume()
		{
		}
	}
}
