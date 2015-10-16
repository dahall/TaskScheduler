using System;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class TimeSpanExtension
	{
		public static TimeSpan Parse(string value) => Parse(value, String.Empty);

		public static TimeSpan Parse(string value, string formattedZero)
		{
			System.Globalization.TimeSpan2FormatInfo fi = System.Globalization.TimeSpan2FormatInfo.CurrentInfo;
			fi.TimeSpanZeroDisplay = formattedZero;
			return TimeSpan2.Parse(value, fi);
		}

		public static string ToString(this TimeSpan span, string format) => System.Globalization.TimeSpan2FormatInfo.CurrentInfo.Format(format, span, null);
	}
}