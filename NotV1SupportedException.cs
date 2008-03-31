using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Thrown when the calling method is not supported by Task Scheduler 1.0.
	/// </summary>
	[System.Diagnostics.DebuggerStepThrough]
	public class NotV1SupportedException : Exception
	{
		string myMessage;
		internal NotV1SupportedException()
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame stackFrame = stackTrace.GetFrame(1);
			MethodBase methodBase = stackFrame.GetMethod();
			myMessage = string.Format("{0}.{1} is not supported on Task Scheduler 1.0.", methodBase.DeclaringType.Name, methodBase.Name);
		}

		internal NotV1SupportedException(string message)
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
	}
}
