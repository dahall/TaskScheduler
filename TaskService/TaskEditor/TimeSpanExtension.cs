using System;

namespace Microsoft.Win32.TaskScheduler
{
    internal static class TimeSpanExtension
    {
        public static TimeSpan Parse(string value)
        {
            return Parse(value, String.Empty);
        }

        public static TimeSpan Parse(string value, string formattedZero)
        {
            System.Globalization.TimeSpanFormatInfo fi = System.Globalization.TimeSpanFormatInfo.CurrentInfo;
            fi.TimeSpanZeroDisplay = formattedZero;
            return fi.Parse(value);
        }

        public static string ToString(this TimeSpan span, string format)
        {
            return System.Globalization.TimeSpanFormatInfo.CurrentInfo.Format(format, span, null);
        }
    }
}