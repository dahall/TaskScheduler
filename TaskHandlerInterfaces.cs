using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	[ComImport, Guid("839D7762-5121-4009-9234-4F0D19394F04"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), System.Security.SuppressUnmanagedCodeSecurity]
	public interface ITaskHandler
	{
		void Start([In, MarshalAs(UnmanagedType.IUnknown)] object pHandlerServices, [In, MarshalAs(UnmanagedType.BStr)] string Data);
		void Stop([MarshalAs(UnmanagedType.Error)] out int pRetCode);
		void Pause();
		void Resume();
	}

	[ComImport, Guid("EAEC7A8F-27A0-4DDC-8675-14726A01A38A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
	System.Security.SuppressUnmanagedCodeSecurity]
	public interface ITaskHandlerStatus
	{
		void UpdateStatus([In] short percentComplete, [In, MarshalAs(UnmanagedType.BStr)] string statusMessage);
		void TaskCompleted([In, MarshalAs(UnmanagedType.Error)] int taskErrCode);
	}
}
