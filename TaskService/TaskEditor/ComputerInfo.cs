using System.Management;

namespace Microsoft.Win32.TaskScheduler
{
	private static class ComputerInfo
	{
		public static string OSFullName
		{
			get
			{
				string result = string.Empty;
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
				foreach (ManagementObject os in searcher.Get())
				{
					result = os["Caption"].ToString();
					break;
				}
				return result;
			}
		}
	}
}
