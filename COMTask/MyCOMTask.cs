using Microsoft.Win32.TaskScheduler;
using System;
using System.EnterpriseServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace COMTask
{
	/// <summary>
	///  This task will write an entry to a log file every 5 seconds while active until 12 writes.
	/// </summary>
	[ObjectPooling(MinPoolSize = 2, MaxPoolSize = 10, CreationTimeout = 20)]
	[Transaction(TransactionOption.Required)]
	[ComVisible(true), Guid("CE7D4428-8A77-4c5d-8A13-5CAB5D1EC734"), ClassInterface(ClassInterfaceType.None)]
	public class MyCOMTask : TaskHandlerBase
	{
		private Timer timer;
		private DateTime lastWriteTime = DateTime.MinValue;
		private byte writeCount = 0;
		private const string file = @"C:\TaskLog.txt";

		public MyCOMTask()
		{
			timer = new Timer(5000) { AutoReset = true };
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
		}

		public override void Start(string data)
		{
			lastWriteTime = DateTime.Now;
			timer_Elapsed(null, null);
			timer.Enabled = true;
		}

		public override int Stop()
		{
			timer.Enabled = false;
			return 0;
		}

		public override void Pause()
		{
			timer.Enabled = false;
		}

		public override void Resume()
		{
			timer.Enabled = true;
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (writeCount < 12)
			{
				try
				{
					using (StreamWriter wri = File.AppendText(file))
						wri.WriteLine("Log entry {0}", DateTime.Now);

					StatusHandler.UpdateStatus((short)(++writeCount / 12), $"Log file started at {lastWriteTime}");
				}
				catch { }
			}

			if (writeCount >= 12)
			{
				timer.Enabled = false;
				writeCount = 0;
				StatusHandler.TaskCompleted(0);
			}
		}
	}
}
