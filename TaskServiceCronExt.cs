using System;
using System.Collections.Generic;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed partial class TaskService
	{
	}

	public abstract partial class Trigger
	{
		private static bool? foundCronLib = null;
		private static Type cronType = null;

		/// <summary>
		/// Creates a trigger using a cron string.
		/// </summary>
		/// <param name="cronString">String using cron defined syntax for specifying a time interval.</param>
		/// <returns><see cref="Trigger"/> of specified type.</returns>
		public static Trigger CreateTrigger(string cronString)
		{
			if (!foundCronLib.HasValue)
			{
				try
				{
					foundCronLib = false;
					System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom("NCrontab.dll");
					if (asm != null)
					{
						cronType = asm.GetType("NCrontab.CrontabSchedule", false, false);
						if (cronType != null)
							foundCronLib = true;
					}
				}
				catch { }
			}

			if (foundCronLib == true && cronType != null)
			{
				try
				{
					object o = Activator.CreateInstance(cronType, span);
					if (o != null)
					{
						System.Reflection.MethodInfo mi = timeSpan2Type.GetMethod("ToString", new Type[] { typeof(string) });
						if (mi != null)
						{
							object r = mi.Invoke(o, new object[] { "f" });
							if (r != null)
								return r.ToString();
						}
					}
				}
				catch { }
			}

			return null;
		}

		private void Sch(string expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			var tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (tokens.Length != 5)
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid crontab expression. It must contain at least 5 components of a schedule "
					+ "(in the sequence of minutes, hours, days, months, days of week).", expression));
			}

			// min, hr, days, months, daysOfWeek
			var fields = new NCrontab.CrontabField[5];
			for (var i = 0; i < fields.Length; i++)
			{
				var field = NCrontab.CrontabField.TryParse((NCrontab.CrontabFieldKind)i, tokens[i]);
				if (field.IsError)
					throw field.Error;
				fields[i] = field.Value;
			}

			List<Time> times = new List<Time>();
			for (int h = fields[1].GetFirst(); h != -1; h = fields[1].Next(h))
			{
				for (int m = fields[1].GetFirst(); m != -1; m = fields[1].Next(m))
				{
				}
			}
		}

		private struct Time
		{
			public int Hour, Minute;
		}
	}
}
