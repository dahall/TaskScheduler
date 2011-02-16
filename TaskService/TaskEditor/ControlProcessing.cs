using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class ControlProcessing
	{
		public static RightToLeft GetRightToLeftProperty(this Control ctl)
		{
			if (ctl.RightToLeft == RightToLeft.Inherit)
			{
				return GetRightToLeftProperty(ctl.Parent);
			}
			return ctl.RightToLeft;
		}
	}
}
