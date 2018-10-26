using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Win32.TaskScheduler
{
	public abstract partial class Trigger
	{
		/// <summary>Creates a trigger using a cron string.</summary>
		/// <param name="cronString">String using cron defined syntax for specifying a time interval. See remarks for syntax.</param>
		/// <returns>Array of <see cref="Trigger"/> representing the specified cron string.</returns>
		/// <exception cref="System.NotImplementedException">Unsupported cron string.</exception>
		/// <remarks>
		/// <note type="note"> This method does not support all combinations of cron strings. Please test extensively before use. Please post an issue with any
		/// syntax that should work, but doesn't.</note>
		/// <para>The following combinations are known <c>not</c> to work:</para>
		/// <list type="bullet">
		/// <item><description>Intervals on months (e.g. "* * * */5 *")</description></item>
		/// <item><description>Intervals on DOW (e.g. "* * * * MON/3")</description></item>
		/// </list>
		/// <para>
		/// This section borrows liberally from the site http://www.nncron.ru/help/EN/working/cron-format.htm. The cron format consists of five fields separated
		/// by white spaces:
		/// </para>
		/// <code>
		///   &lt;Minute&gt; &lt;Hour&gt; &lt;Day_of_the_Month&gt; &lt;Month_of_the_Year&gt; &lt;Day_of_the_Week&gt;
		/// </code>
		/// <para>Each item has bounds as defined by the following:</para>
		/// <code>
		///   * * * * *
		///   | | | | |
		///   | | | | +---- Day of the Week   (range: 1-7, 1 standing for Monday)
		///   | | | +------ Month of the Year (range: 1-12)
		///   | | +-------- Day of the Month  (range: 1-31)
		///   | +---------- Hour              (range: 0-23)
		///   +------------ Minute            (range: 0-59)
		/// </code>
		/// <para>Any of these 5 fields may be an asterisk (*). This would mean the entire range of possible values, i.e. each minute, each hour, etc.</para>
		/// <para>
		/// Any of the first 4 fields can be a question mark ("?"). It stands for the current time, i.e. when a field is processed, the current time will be
		/// substituted for the question mark: minutes for Minute field, hour for Hour field, day of the month for Day of month field and month for Month field.
		/// </para>
		/// <para>Any field may contain a list of values separated by commas, (e.g. 1,3,7) or a range of values (two integers separated by a hyphen, e.g. 1-5).</para>
		/// <para>
		/// After an asterisk (*) or a range of values, you can use character / to specify that values are repeated over and over with a certain interval between
		/// them. For example, you can write "0-23/2" in Hour field to specify that some action should be performed every two hours (it will have the same effect
		/// as "0,2,4,6,8,10,12,14,16,18,20,22"); value "*/4" in Minute field means that the action should be performed every 4 minutes, "1-30/3" means the same
		/// as "1,4,7,10,13,16,19,22,25,28".
		/// </para>
		/// </remarks>
		public static Trigger[] FromCronFormat([NotNull] string cronString)
		{
			var cron = CronExpression.Parse(cronString);
			System.Diagnostics.Debug.WriteLine($"{cronString}=M:{cron.Minutes}; H:{cron.Hours}; D:{cron.Days}; M:{cron.Months}; W:{cron.DOW}");

			var ret = new List<Trigger>();

			// There isn't a clean mechanism to handle intervals on DOW or months, so punt
			//if (cron.DOW.IsIncr) throw new NotSupportedException();
			//if (cron.Months.IsIncr) throw new NotSupportedException();

			// WeeklyTrigger
			if (cron.Days.FullRange && cron.Months.FullRange && !cron.DOW.IsEvery)
			{
				var tr = new WeeklyTrigger(cron.DOW.ToDOW());
				ret.AddRange(ProcessCronTimes(cron, tr));
			}

			// MonthlyDOWTrigger
			if (!cron.DOW.FullRange && (!cron.Days.FullRange || !cron.Months.FullRange))
			{
				var tr = new MonthlyDOWTrigger(cron.DOW.ToDOW(), cron.Months.ToMOY(), WhichWeek.AllWeeks);
				ret.AddRange(ProcessCronTimes(cron, tr));
			}

			// MonthlyTrigger
			if (!cron.Days.FullRange || !cron.Months.FullRange && cron.DOW.FullRange)
			{
				var tr = new MonthlyTrigger(1, cron.Months.ToMOY()) { DaysOfMonth = cron.Days.Values.ToArray() };
				ret.AddRange(ProcessCronTimes(cron, tr));
			}

			// DailyTrigger
			if (cron.Days.FullRange && cron.Months.FullRange && cron.DOW.IsEvery)
			{
				var tr = new DailyTrigger((short)cron.Days.Increment);
				ret.AddRange(ProcessCronTimes(cron, tr));
			}

			// Fail out
			if (ret.Count == 0)
				throw new NotSupportedException();

			return ret.ToArray();
		}

		private static IEnumerable<Trigger> ProcessCronTimes(CronExpression cron, Trigger baseTrigger)
		{
			// Sequential hours, every minute
			// "* * * * *"
			// "* 2-6 * * *"
			if (cron.Minutes.FullRange && (cron.Hours.IsEvery || cron.Hours.IsRange))
			{
				System.Diagnostics.Debug.WriteLine("Minutes.FullRange && (Hours.IsEvery || Hours.IsRange)");
				yield return MakeTrigger(
					new TimeSpan(cron.Hours.FirstValue, 0, 0),
					TimeSpan.FromMinutes(cron.Minutes.Increment),
					TimeSpan.FromHours(cron.Hours.Duration));
			}
			// Non-sequential hours, every minute
			// "* 3,5,6 * * *"
			// "* 3-15/3 * * *"
			else if (cron.Minutes.FullRange && (cron.Hours.IsList || cron.Hours.IsIncr))
			{
				System.Diagnostics.Debug.WriteLine("Minutes.FullRange && (Hours.IsList || Hours.IsIncr)");
				foreach (var h in cron.Hours.Values)
				{
					yield return MakeTrigger(
						new TimeSpan(h, 0, 0),
						TimeSpan.FromMinutes(cron.Minutes.Increment),
						TimeSpan.FromHours(1));
				}
			}
			// Non-repeating minutes, every hour
			// "3,6 * * * *" Every hour starting at 12:03 and 12:06
			// "3-33 * * * *"
			// "3-33/6 * * * *"
			// "3,6 * 3-5 * *"
			// "3-33 3-5 * * *"
			// "3-33/6 3-5 * * *"
			else if (!cron.Minutes.FullRange && (cron.Hours.IsEvery || cron.Hours.IsRange))
			{
				System.Diagnostics.Debug.WriteLine("!Minutes.FullRange && (Hours.IsEvery || Hours.IsRange)");
				foreach (var m in cron.Minutes.Values)
				{
					yield return MakeTrigger(
						new TimeSpan(cron.Hours.FirstValue, m, 0),
						TimeSpan.FromHours(1),
						TimeSpan.FromHours(cron.Hours.Duration));
				}
			}
			// Sequential or repeating minutes, and non-sequential hours
			else if ((cron.Minutes.IsRange || cron.Minutes.IsIncr) && (cron.Hours.IsList || cron.Hours.IsIncr))
			{
				System.Diagnostics.Debug.WriteLine("(Minutes.IsRange || Minutes.IsIncr) && (Hours.IsList || Hours.IsIncr)");
				foreach (var h in cron.Hours.Values)
				{
					yield return MakeTrigger(
						new TimeSpan(h, cron.Minutes.FirstValue, 0),
						TimeSpan.FromMinutes(cron.Minutes.Increment),
						TimeSpan.FromMinutes(cron.Minutes.Duration));
				}
			}
			// Non-sequential, hours and minutes
			// "3,6 3,6 * * *" Every day at 3:03, 3:06, 6:03 and 6:06
			// "3/6 3/6 * * *" Every day at 3:03, 3:06, 6:03 and 6:06
			else
			{
				System.Diagnostics.Debug.WriteLine("Minutes.IsList && (Hours.IsIncr || Hours.IsList)");
				foreach (var h in cron.Hours.Values)
					foreach (var m in cron.Minutes.Values)
						yield return MakeTrigger(new TimeSpan(h, m, 0));
			}

			Trigger MakeTrigger(TimeSpan start, TimeSpan interval = default, TimeSpan duration = default)
			{
				var newTr = (Trigger)baseTrigger.Clone();
				newTr.StartBoundary = newTr.StartBoundary.Date + start;
				if (interval != default)
				{
					newTr.Repetition.Interval = interval;
					newTr.Repetition.Duration = duration;
				}
				return newTr;
			}
		}

		internal class CronExpression
		{
			private FieldVal[] Fields = new FieldVal[5];

			private CronExpression() { }

			public enum CronFieldType { Minutes, Hours, Days, Months, DaysOfWeek };

			public FieldVal Days => Fields[2];

			public FieldVal DOW => Fields[4];

			public FieldVal Hours => Fields[1];

			public FieldVal Minutes => Fields[0];

			public FieldVal Months => Fields[3];

			public static CronExpression Parse(string cronString)
			{
				var ret = new CronExpression();
				if (cronString == null)
					throw new ArgumentNullException(nameof(cronString));

				var tokens = cronString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length != 5)
				{
					throw new ArgumentException($"'{cronString}' is not a valid crontab expression. It must contain at least 5 components of a schedule "
						+ "(in the sequence of minutes, hours, days, months, days of week).");
				}

				// min, hr, days, months, daysOfWeek
				for (var i = 0; i < ret.Fields.Length; i++)
					ret.Fields[i] = FieldVal.Parse(tokens[i], (CronFieldType)i);

				return ret;
			}

			public struct FieldVal
			{
				private const string rangeRegEx = @"^(?:(?<A>\*)|(?<D1>\d+)(?:-(?<D2>\d+))?)(?:\/(?<I>\d+))?$";
				private readonly static Dictionary<string, string> dow = new Dictionary<string, string>
				{
					{ "SUN", "0" },
					{ "MON", "1" },
					{ "TUE", "2" },
					{ "WED", "3" },
					{ "THU", "4" },
					{ "FRI", "5" },
					{ "SAT", "6" },
				};
				private readonly static Dictionary<string, string> mon = new Dictionary<string, string>
				{
					{ "JAN", "1" },
					{ "FEB", "2" },
					{ "MAR", "3" },
					{ "APR", "4" },
					{ "MAY", "5" },
					{ "JUN", "6" },
					{ "JUL", "7" },
					{ "AUG", "8" },
					{ "SEP", "9" },
					{ "OCT", "10" },
					{ "NOV", "11" },
					{ "DEC", "12" },
				};
				private readonly static Dictionary<CronFieldType, MinMax> validRange = new Dictionary<CronFieldType, MinMax>
				{
					{ CronFieldType.Days, new MinMax(1, 31) },
					{ CronFieldType.DaysOfWeek, new MinMax(0, 6) },
					{ CronFieldType.Hours, new MinMax(0, 23) },
					{ CronFieldType.Minutes, new MinMax(0, 59) },
					{ CronFieldType.Months, new MinMax(1, 12) },
				};
				private CronFieldType cft;
				private FieldFlags flags;
				private int incr;
				private int[] vals;
				public FieldVal(CronFieldType cft) { this.cft = cft; flags = 0; vals = new int[0]; incr = 1; FullRange = false; }

				enum FieldFlags { List, Every, Range, Increment };
				public int Duration => vals.Length == 1 ? 1 : vals[1] - vals[0] + 1;
				public int Increment => incr;
				public bool IsEvery { get => flags == FieldFlags.Every; private set => flags = FieldFlags.Every; }
				public bool IsIncr { get => flags == FieldFlags.Increment; private set => flags = FieldFlags.Increment; }
				public bool IsList { get => flags == 0; private set => flags = FieldFlags.List; }
				public bool IsRange { get => flags == FieldFlags.Range; private set => flags = FieldFlags.Range; }
				public bool FullRange { get; private set; }
				public int FirstValue => vals[0];
				public IEnumerable<int> Values
				{
					get
					{
						if (flags == 0)
						{
							foreach (var i in vals)
								yield return i;
						}
						else
						{
							for (int i = vals[0]; i <= vals[1]; i += incr)
								yield return i;
						}
					}
				}

				public DaysOfTheWeek ToDOW()
				{
					if (IsEvery) return DaysOfTheWeek.AllDays;
					DaysOfTheWeek ret = 0;
					foreach (var i in Values)
						ret |= (DaysOfTheWeek)(1 << i);
					return ret;
				}

				public MonthsOfTheYear ToMOY()
				{
					if (IsEvery) return MonthsOfTheYear.AllMonths;
					MonthsOfTheYear ret = 0;
					foreach (var i in Values)
						ret |= (MonthsOfTheYear)(1 << (i - 1));
					return ret;
				}

				public static FieldVal Parse(string str, CronFieldType cft)
				{
					var res = new FieldVal(cft);
					if (string.IsNullOrEmpty(str))
						throw new ArgumentNullException(nameof(str), "A crontab field value cannot be empty.");

					// Do substitutions
					str = DoSubs(str, cft);

					// Look first for a list of values (e.g. 1,2,3).
					if (System.Text.RegularExpressions.Regex.IsMatch(str, @"^\d+(,\d+)*$"))
					{
						if (str.Contains("/")) throw new NotSupportedException();

						res.vals = str.Split(',').Select(ParseInt).OrderBy(i => i).Distinct().ToArray();
						res.Validate();
						return res;
					}

					// Look for *|nn[-nn][/n] pattern
					var match = System.Text.RegularExpressions.Regex.Match(str, rangeRegEx);
					if (match.Success)
					{
						bool hasAst = res.FullRange = match.Groups["A"].Success;
						if (match.Groups["I"].Success)
						{
							res.incr = ParseInt(match.Groups["I"].Value);
							res.IsIncr = true;
						}
						else
						{
							if (hasAst)
								res.IsEvery = true;
							else
								res.IsRange = true;
						}
						var mm = validRange[cft];
						var start = hasAst ? mm.Min : ParseInt(match.Groups["D1"].Value);
						var end = hasAst ? mm.Max : (match.Groups["D2"].Success ? ParseInt(match.Groups["D2"].Value) : (res.IsIncr ? mm.Max : start));
						if (end < start) throw new ArgumentOutOfRangeException();
						if (start == end && res.IsRange)
						{
							res.IsList = true;
							res.vals = new[] { start };
						}
						else
							res.vals = new[] { start, end };
						res.Validate();
						return res;
					}

					throw new FormatException();
				}

				public override string ToString() => $"Type:{flags}; Vals:{string.Join(",", vals.Select(i => i.ToString()).ToArray())}; Incr:{incr}";

				private void Validate()
				{
					var l = validRange[cft];
					if (vals.Any(i => i < l.Min || i > l.Max)) throw new ArgumentOutOfRangeException();
					if (IsIncr && (incr < l.Min || incr > l.Max)) throw new ArgumentOutOfRangeException();
				}

				private static string DoSubs(string str, CronFieldType cft)
				{
					var sb = new System.Text.StringBuilder(str);

					// Handle SUN-SAT strings
					if (cft == CronFieldType.DaysOfWeek)
					{
						foreach (var kv in dow)
							sb.Replace(kv.Key, kv.Value);
					}

					// Handle JAN–DEC strings
					if (cft == CronFieldType.Months)
					{
						foreach (var kv in mon)
							sb.Replace(kv.Key, kv.Value);
					}

					// Check for "?" and substitute current time
					if (sb.Length == 1 && sb.ToString() == "?")
					{
						var now = DateTime.Now;
						var nval = 0;
						switch (cft)
						{
							case CronFieldType.Minutes:
								nval = now.Minute;
								break;
							case CronFieldType.Hours:
								nval = now.Hour;
								break;
							case CronFieldType.Days:
								nval = now.Day;
								break;
							case CronFieldType.Months:
								nval = now.Month;
								break;
							case CronFieldType.DaysOfWeek:
								nval = (int)now.DayOfWeek;
								break;
							default:
								break;
						}
						sb.Remove(0, 1);
						sb.Append(nval);
					}

					// Expand or collapse ranges
					var minMax = validRange[cft];
					foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(sb.ToString(), @"(\d+)-(\d+)"))
					{
						var low = ParseInt(m.Groups[1].Value);
						var high = ParseInt(m.Groups[2].Value);
						if (low == minMax.Min && high == minMax.Max)
							sb.Replace(m.Value, "*");
						else if (sb.ToString().Contains(','))
						{
							var rsb = new System.Text.StringBuilder(low.ToString());
							for (int i = low; i < high; i++)
								rsb.Append($",{i + 1}");
							sb.Replace(m.Value, rsb.ToString());
						}
					}

					return sb.ToString();
				}

				private static int ParseInt(string str) => int.Parse(str.Trim());

				private struct MinMax
				{
					public int Min, Max;
					public MinMax(int min, int max) { Min = min; Max = max; }
				}
			}
		}
	}
}