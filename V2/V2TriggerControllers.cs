using System;
using static Microsoft.Win32.TaskScheduler.V2Interop.V2Util;

namespace Microsoft.Win32.TaskScheduler.V2Interop
{
	internal class V2Trigger : ITriggerImpl
	{
		ITrigger iTrigger;
		IRepetitionPattern repetitionPattern = null;

		public V2Trigger() { }

		public object BaseObject { get; set; }

		public bool Enabled
		{
			get { return iTrigger.Enabled; }
			set { iTrigger.Enabled = value; }
		}

		public DateTime? EndBoundary
		{
			get { return StringToDT(iTrigger.EndBoundary); }
			set { iTrigger.EndBoundary = DTToString(value, DateTime.MaxValue); }
		}

		public TimeSpan? ExecutionTimeLimit
		{
			get { return StringToTS(iTrigger.ExecutionTimeLimit); }
			set { iTrigger.ExecutionTimeLimit = TSToString(value, TimeSpan.Zero); }
		}

		public string Id
		{
			get { return iTrigger.Id; }
			set { iTrigger.Id = value; }
		}

		private IRepetitionPattern Repetition => repetitionPattern ?? (repetitionPattern = iTrigger.Repetition);

		public TimeSpan? RepetitionDuration
		{
			get { return StringToTS(Repetition.Duration); }
			set { Repetition.Duration = TSToString(value, TimeSpan.Zero); }
		}

		public TimeSpan? RepetitionInterval
		{
			get { return StringToTS(Repetition.Interval); }
			set { Repetition.Interval = TSToString(value, TimeSpan.Zero); }
		}

		public bool RepetitionStopAtDurationEnd
		{
			get { return Repetition.StopAtDurationEnd; }
			set { Repetition.StopAtDurationEnd = value; }
		}

		public DateTime StartBoundary
		{
			get { return StringToDT(iTrigger.StartBoundary).GetValueOrDefault(DateTime.MinValue); }
			set { iTrigger.StartBoundary = DTToString(value, DateTime.MinValue); }
		}
	}

	internal static class V2Util
	{
		internal const string V2BoundaryDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFK";
		internal static readonly System.Globalization.CultureInfo DefaultDateCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

		public static DateTime? StringToDT(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				DateTime o;
				if (DateTime.TryParse(value, out o))
					return o;
			}
			return null;
		}

		public static TimeSpan? StringToTS(string value) => value == null ? (TimeSpan?)null : Task.StringToTimeSpan(value);

		public static string DTToString(DateTime? value, DateTime? nullEquiv = null) => (!value.HasValue || (nullEquiv.HasValue && value.Value == nullEquiv.Value)) ? null : value.Value.ToString(V2BoundaryDateFormat, DefaultDateCulture);

		public static string TSToString(TimeSpan? value, TimeSpan? nullEquiv = null) => (!value.HasValue || (nullEquiv.HasValue && value.Value == nullEquiv.Value)) ? null : Task.TimeSpanToString(value.Value);
	}
}
