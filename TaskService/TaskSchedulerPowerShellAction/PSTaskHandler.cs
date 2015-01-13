using Microsoft.Win32.TaskScheduler;
using System;
using System.EnterpriseServices;
using System.Management.Automation;
using System.Runtime.InteropServices;

namespace TaskSchedulerPowerShellAction
{
	/// <summary>
	/// A Task Scheduler COM Handler that executes a PowerShell script.
	/// </summary>
	[ObjectPooling(MinPoolSize = 2, MaxPoolSize = 10, CreationTimeout = 20)]
	[Transaction(TransactionOption.Required)]
	[ComVisible(true), Guid("dab4c1e3-cd12-46f1-96fc-3981143c9bab"), ClassInterface(ClassInterfaceType.None)]
	public class PSTaskHandler : TaskHandlerBase
	{
		private PowerShell psInstance;
		private IAsyncResult result;

		/// <summary>
		/// Initializes a new instance of the <see cref="PSTaskHandler"/> class.
		/// </summary>
		public PSTaskHandler()
		{
		}

		/// <summary>
		/// Called to start the COM handler.
		/// </summary>
		/// <param name="data">Data string passed in from Task Scheduler action.</param>
		public override void Start(string data)
		{
			psInstance = PowerShell.Create();
			psInstance.AddScript(data);
			result = psInstance.BeginInvoke();
			psInstance.InvocationStateChanged += PowerShellInstance_InvocationStateChanged;
		}

		/// <summary>
		/// Called to stop the COM handler.
		/// </summary>
		/// <returns>The return code that the Task Schedule will raise as an event when the COM handler action is completed. Return 0 on success.</returns>
		public override int Stop()
		{
			if (psInstance != null && result != null)
				psInstance.Stop();
			return 0;
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; otherwise, false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (psInstance != null)
			{
				psInstance.InvocationStateChanged -= PowerShellInstance_InvocationStateChanged;
				psInstance.Dispose();
			}
		}

		private void PowerShellInstance_InvocationStateChanged(object sender, PSInvocationStateChangedEventArgs e)
		{
			switch (e.InvocationStateInfo.State)
			{
				case PSInvocationState.Completed:
				case PSInvocationState.Disconnected:
				case PSInvocationState.Failed:
				case PSInvocationState.NotStarted:
				case PSInvocationState.Stopped:
					base.StatusHandler.TaskCompleted(e.InvocationStateInfo.Reason != null ? e.InvocationStateInfo.Reason.HResult : 0);
					break;
				//case PSInvocationState.Running:
				//case PSInvocationState.Stopping:
				default:
					break;
			}
		}
	}
}