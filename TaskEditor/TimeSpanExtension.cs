using System;
using System.Text;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class TimeSpanExtension
	{
		public static string ToString(this TimeSpan span, string format)
		{
			StringBuilder sb = new StringBuilder();
			if (span.Days > 0)
				sb.AppendFormat("{0} {1}", span.Days, span.Days == 1 ? Properties.Resources.TimeSpanDay : Properties.Resources.TimeSpanDays);
			if (span.Hours > 0)
				sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : ", ",
					span.Hours, span.Hours == 1 ? Properties.Resources.TimeSpanHour : Properties.Resources.TimeSpanHours);
			if (span.Minutes > 0)
				sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : ", ",
					span.Minutes, span.Minutes == 1 ? Properties.Resources.TimeSpanMinute : Properties.Resources.TimeSpanMinutes);
			if (span.Seconds > 0)
				sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : ", ",
					span.Seconds, span.Seconds == 1 ? Properties.Resources.TimeSpanSecond : Properties.Resources.TimeSpanSeconds);
			if (sb.Length == 0 && span.TotalSeconds > 0)
				sb.AppendFormat("{0} {1}", span.TotalSeconds, Properties.Resources.TimeSpanSeconds);
			return sb.ToString();
		}

		public static TimeSpan Parse(string value)
		{
			return TimeSpan.Zero;
		}
	}
}
