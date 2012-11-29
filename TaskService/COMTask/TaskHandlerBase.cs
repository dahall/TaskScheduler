using Microsoft.Win32.TaskScheduler;

namespace COMTask
{
	/// <summary>
	/// Virtual base class for a COM-based Task Handler
	/// </summary>
	public abstract class TaskHandlerBase : ITaskHandler
	{
		public ITaskHandlerStatus StatusHandler { get; private set; }

		/// <summary>
		/// Starts the specified status.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="data">The data.</param>
		public abstract void Start(string data);

		/// <summary>
		/// Stops this instance.
		/// </summary>
		/// <returns></returns>
		public virtual int Stop() { return 0; }

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		public virtual void Pause() { }

		/// <summary>
		/// Resumes this instance.
		/// </summary>
		public virtual void Resume() { }

		void ITaskHandler.Start(object pHandlerServices, string Data)
		{
			StatusHandler = pHandlerServices as ITaskHandlerStatus;
			Start(Data);
		}

		void ITaskHandler.Stop(out int pRetCode)
		{
			pRetCode = Stop();
		}

		void ITaskHandler.Pause()
		{
			Pause();
		}

		void ITaskHandler.Resume()
		{
			Resume();
		}
	}
}
