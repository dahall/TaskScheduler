using System;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class TimeSpanExtension
	{
		public static string ToString(this TimeSpan span, string format)
		{
			char fmt = string.IsNullOrEmpty(format) ? 'n' : format[0];
			string zeroFormat = (format.Length > 1 && format[1] == ';') ? format.Substring(2) : string.Empty;

			StringBuilder sb = new StringBuilder();
			switch (fmt)
			{
				case 'n':
					return span.ToString();
				case 'f':
					string sep = Properties.Resources.TimeSpanSeparator;
					if (span == TimeSpan.Zero && !string.IsNullOrEmpty(zeroFormat))
						return zeroFormat;
					if (span.Days > 0)
						sb.AppendFormat("{0} {1}", span.Days, span.Days == 1 ? Properties.Resources.TimeSpanDay : Properties.Resources.TimeSpanDays);
					if (span.Hours > 0)
						sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : sep,
							span.Hours, span.Hours == 1 ? Properties.Resources.TimeSpanHour : Properties.Resources.TimeSpanHours);
					if (span.Minutes > 0)
						sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : sep,
							span.Minutes, span.Minutes == 1 ? Properties.Resources.TimeSpanMinute : Properties.Resources.TimeSpanMinutes);
					if (span.Seconds > 0)
						sb.AppendFormat("{0}{1} {2}", sb.Length == 0 ? string.Empty : sep,
							span.Seconds, span.Seconds == 1 ? Properties.Resources.TimeSpanSecond : Properties.Resources.TimeSpanSeconds);
					if (sb.Length == 0 && span.TotalSeconds > 0)
						sb.AppendFormat("{0} {1}", span.TotalSeconds, Properties.Resources.TimeSpanSeconds);
					break;
				case 's':
					sb.AppendFormat("{0} {1}", span.TotalSeconds, span.TotalSeconds == 1 ? Properties.Resources.TimeSpanSecond : Properties.Resources.TimeSpanSeconds);
					break;
				case 'm':
					sb.AppendFormat("{0} {1}", span.TotalMinutes, span.TotalMinutes == 1 ? Properties.Resources.TimeSpanMinute : Properties.Resources.TimeSpanMinutes);
					break;
				case 'h':
					sb.AppendFormat("{0} {1}", span.TotalHours, span.TotalHours == 1 ? Properties.Resources.TimeSpanHour : Properties.Resources.TimeSpanHours);
					break;
				case 'd':
					sb.AppendFormat("{0} {1}", span.TotalDays, span.TotalDays == 1 ? Properties.Resources.TimeSpanDay : Properties.Resources.TimeSpanDays);
					break;
				default:
					throw new FormatException("Invalid format specified");
			}
			return sb.ToString();
		}

		public static TimeSpan Parse(string value)
		{
			return Parse(value, String.Empty);
		}

		public static TimeSpan Parse(string value, string formattedZero)
		{
			// Try for the easy one
			TimeSpan ret = TimeSpan.MinValue;
			if (TimeSpan.TryParse(value, out ret))
				return ret;

			// Setup
			CultureInfo currentCulture = CultureInfo.CurrentCulture;

			if (!string.IsNullOrEmpty(formattedZero) && (string.Compare(value, formattedZero, StringComparison.CurrentCultureIgnoreCase) == 0))
			{
				ret = TimeSpan.Zero;
			}
			else
			{
				StringBuilder sb = new StringBuilder(value);
				foreach (var s in Properties.Resources.TimeSpanSecondStrings.Split(','))
					sb.Replace(s, "s");
				foreach (var s in Properties.Resources.TimeSpanMinuteStrings.Split(','))
					sb.Replace(s, "m");
				foreach (var s in Properties.Resources.TimeSpanHourStrings.Split(','))
					sb.Replace(s, "h");
				foreach (var s in Properties.Resources.TimeSpanDayStrings.Split(','))
					sb.Replace(s, "d");
				Regex regex = new Regex(@"\s*(?:(?<d>\d+)\s*d)?(?:\s*|\s*,\s*)(?:(?<h>\d+)\s*h)?(?:\s*|\s*,\s*)(?:(?<m>\d+)\s*m)?(?:\s*|\s*,\s*)(?:(?<s>\d+)\s*s)?\s*",
					RegexOptions.IgnoreCase | RegexOptions.Compiled);
				Match m = regex.Match(sb.ToString());
				if (!m.Success)
					throw new FormatException();
				int d = 0, h = 0, mi = 0, sc = 0;
				if (m.Groups["d"].Success) d = int.Parse(m.Groups["d"].Value);
				if (m.Groups["h"].Success) h = int.Parse(m.Groups["h"].Value);
				if (m.Groups["m"].Success) mi = int.Parse(m.Groups["m"].Value);
				if (m.Groups["s"].Success) sc = int.Parse(m.Groups["s"].Value);
				ret = new TimeSpan(d, h, mi, sc);
			}
			return ret;
		}
	}
}
