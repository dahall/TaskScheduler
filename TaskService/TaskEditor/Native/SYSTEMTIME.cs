using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	internal static partial class NativeMethods
	{
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal struct SYSTEMTIME
		{
			public ushort Year;
			public ushort Month;
			public ushort DayOfWeek;
			public ushort Day;
			public ushort Hour;
			public ushort Minute;
			public ushort Second;
			public ushort Milliseconds;

			public SYSTEMTIME(DateTime dt)
			{
				dt = dt.ToLocalTime();
				Year = Convert.ToUInt16(dt.Year);
				Month = Convert.ToUInt16(dt.Month);
				DayOfWeek = Convert.ToUInt16(dt.DayOfWeek);
				Day = Convert.ToUInt16(dt.Day);
				Hour = Convert.ToUInt16(dt.Hour);
				Minute = Convert.ToUInt16(dt.Minute);
				Second = Convert.ToUInt16(dt.Second);
				Milliseconds = Convert.ToUInt16(dt.Millisecond);
			}

			public SYSTEMTIME(ushort year, ushort month, ushort day, ushort hour = 0, ushort minute = 0, ushort second = 0, ushort millisecond = 0)
			{
				Year = year;
				Month = month;
				Day = day;
				Hour = hour;
				Minute = minute;
				Second = second;
				Milliseconds = millisecond;
				DayOfWeek = 0;
			}

			public static implicit operator DateTime(SYSTEMTIME st)
			{
				if (st.Year == 0 || st == MinValue)
					return DateTime.MinValue;
				if (st == MaxValue)
					return DateTime.MaxValue;
				return new DateTime(st.Year, st.Month, st.Day, st.Hour, st.Minute, st.Second, st.Milliseconds, DateTimeKind.Local);
			}

			public static implicit operator SYSTEMTIME(DateTime dt) => new SYSTEMTIME(dt);

			public static bool operator ==(SYSTEMTIME s1, SYSTEMTIME s2) => (s1.Year == s2.Year && s1.Month == s2.Month && s1.Day == s2.Day && s1.Hour == s2.Hour && s1.Minute == s2.Minute && s1.Second == s2.Second && s1.Milliseconds == s2.Milliseconds);

			public static bool operator !=(SYSTEMTIME s1, SYSTEMTIME s2) => !(s1 == s2);

			public static readonly SYSTEMTIME MinValue, MaxValue;

			static SYSTEMTIME()
			{
				MinValue = new SYSTEMTIME(1601, 1, 1);
				MaxValue = new SYSTEMTIME(30827, 12, 31, 23, 59, 59, 999);
			}

			public override bool Equals(object obj)
			{
				if (obj is SYSTEMTIME)
					return ((SYSTEMTIME)obj) == this;
				return base.Equals(obj);
			}

			public override int GetHashCode() => base.GetHashCode();

			public override string ToString() => ((DateTime)this).ToString();
		}
	}
}
