using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Abstract class for throwing a method specific exception.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	public abstract class TSNotSupportedException : Exception
	{
		string myMessage;
		internal TSNotSupportedException()
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			myMessage = string.Format("{0}.{1} is not supported on {2}", methodBase.DeclaringType.Name, methodBase.Name, this.LibName);
		}

		internal TSNotSupportedException(string message)
		{
			myMessage = message;
		}

		/// <summary>
		/// Gets a message that describes the current exception.
		/// </summary>
		public override string Message
		{
			get { return myMessage; }
		}

		internal abstract string LibName { get; }
	}

	/// <summary>
	/// Thrown when the calling method is not supported by Task Scheduler 1.0.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	public class NotV1SupportedException : TSNotSupportedException
	{
		internal NotV1SupportedException() : base() { }
		internal NotV1SupportedException(string message) : base(message) { }
		internal override string LibName { get { return "Task Scheduler 1.0"; } }
	}

	/// <summary>
	/// Thrown when the calling method is not supported by Task Scheduler 1.0.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	public class NotV2SupportedException : TSNotSupportedException
	{
		internal NotV2SupportedException() : base() { }
		internal NotV2SupportedException(string message) : base(message) { }
		internal override string LibName { get { return "Task Scheduler 2.0 (1.2)"; } }
	}
}
