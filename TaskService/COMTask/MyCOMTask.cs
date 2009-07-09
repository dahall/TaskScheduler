using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;
using Microsoft.Win32.TaskScheduler;

namespace COMTask
{
	/// <summary>
	///  This task will write an entry to a log file every 5 seconds while active until 12 writes.
	/// </summary>
	[ComVisible(true), Guid("CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734"), ClassInterface(ClassInterfaceType.None)]
	public sealed class MyCOMTask : ITaskHandler
	{
		private ITaskHandlerStatus handlerService;
		private Timer timer;
		private DateTime lastWriteTime = DateTime.MinValue;
		private byte writeCount = 0;
		private const string file = @"C:\TaskLog.txt";

		void ITaskHandler.Start(object pHandlerServices, string Data)
		{
			handlerService = pHandlerServices as ITaskHandlerStatus;
			lastWriteTime = DateTime.Now;
			timer_Elapsed(null, null);
			timer.Enabled = true;
		}

		void ITaskHandler.Stop(out int pRetCode)
		{
			timer.Enabled = false;
			pRetCode = 0;
		}

		void ITaskHandler.Pause()
		{
			timer.Enabled = false;
		}

		void ITaskHandler.Resume()
		{
			timer.Enabled = true;
		}

		public MyCOMTask()
		{
			timer = new Timer(5000) { AutoReset = true };
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (writeCount < 12)
			{
				try
				{
					using (StreamWriter wri = File.AppendText(file))
						wri.WriteLine("Log entry {0}", DateTime.Now);

					handlerService.UpdateStatus((short)(++writeCount / 12), string.Format("Log file started at {0}", lastWriteTime));
				}
				catch { }
			}

			if (writeCount >= 12)
			{
				timer.Enabled = false;
				writeCount = 0;
				handlerService.TaskCompleted(0);
			}
		}

	}
}
