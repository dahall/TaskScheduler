using System;
using Microsoft.Win32.TaskScheduler;

namespace TestTaskServiceConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Root folder tasks:");
			//using (var ts = new TaskService(null, forceV1: true))
			var ts = TaskService.Instance;
				foreach (var t in ts.RootFolder.EnumerateTasks())
					Console.WriteLine(t.Name);
		}
	}
}

/*namespace System.Runtime.InteropServices.ComTypes
{
	using System.Collections;

	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}

	[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00020400-0000-0000-C000-000000000046")]
	internal interface IDispatch
	{
		[PreserveSig]
		int GetTypeInfoCount(out int info);

		[PreserveSig]
		int GetTypeInfo(int iTInfo, int lcid, out ComTypes.ITypeInfo ppTInfo);

		void GetIDsOfNames([MarshalAs(UnmanagedType.LPStruct)] Guid iid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgszNames, int cNames, int lcid, [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] int[] rgDispId);

		void Invoke(int dispIdMember, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, int lcid, ComTypes.INVOKEKIND wFlags, [In, Out] [MarshalAs(UnmanagedType.LPArray)] ComTypes.DISPPARAMS[] paramArray, out object pVarResult, out ComTypes.EXCEPINFO pExcepInfo, out uint puArgErr);
	}

	internal static class COMHelper
	{
		public static int GetDispId(object rcw, string methodName)
		{
			IDispatch dispatchObject = rcw as IDispatch;
			if (dispatchObject == null)
			{
				Console.WriteLine("Passed-in argument is not a IDispatch object");
				return -1;
			}


			int[] dispIds = new int[1];
			Guid emtpyRiid = Guid.Empty;
			dispatchObject.GetIDsOfNames(
				emtpyRiid,
				new string[] { methodName },
				1,
				0,
				dispIds);

			if (dispIds[0] == -1)
			{
				Console.WriteLine("Method name {0} cannot be recognized.", methodName);
			}

			return dispIds[0];
		}

		public static object Invoke(IDispatch target, int dispId)
		{
			if (target == null) { Console.WriteLine("Cannot cast target to IDispatch."); return null; }

			IntPtr variantArgArray = IntPtr.Zero, dispIdArray = IntPtr.Zero, tmpVariants = IntPtr.Zero;
			int argCount = 0;

			var paramArray = new ComTypes.DISPPARAMS[1];
			paramArray[0].rgvarg = variantArgArray;
			paramArray[0].cArgs = argCount;
			paramArray[0].cNamedArgs = 0;
			paramArray[0].rgdispidNamedArgs = IntPtr.Zero;

			ComTypes.EXCEPINFO info = default(ComTypes.EXCEPINFO);
			object result = null;

			try
			{
				uint puArgErrNotUsed = 0;
				target.Invoke(dispId, new Guid(), 0x0409, ComTypes.INVOKEKIND.INVOKE_FUNC, paramArray, out result, out info, out puArgErrNotUsed);
			}
			catch (Exception ex)
			{
				Console.WriteLine("IDispatch.Invoke failed: {0}", ex.Message);
			}

			return result;
		}

		[DllImport("ole32.dll")]
		internal static extern int CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string lpszProgID, out Guid pclsid);
	}
}*/