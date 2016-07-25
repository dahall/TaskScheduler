using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Win32.TaskScheduler
{
	internal interface IImplBase
	{
		object BaseObject { get; set; }
	}

	internal interface IActionImpl : IImplBase
	{
		string Id { get; set; }
	}

	internal interface IExecActionImpl : IActionImpl
	{
		string Arguments { get; set; }
		string Path { get; set; }
		string WorkingDirectory { get; set; }
	}

	internal interface ITriggerImpl : IImplBase
	{
		bool Enabled { get; set; }
		DateTime? EndBoundary { get; set; }
		TimeSpan? ExecutionTimeLimit { get; set; }
		string Id { get; set; }
		TimeSpan? RepetitionDuration { get; set; }
		TimeSpan? RepetitionInterval { get; set; }
		bool RepetitionStopAtDurationEnd { get; set; }
		DateTime StartBoundary { get; set; }
	}

	internal interface IBootTriggerImpl : ITriggerImpl, ITriggerDelay { }

	internal interface IDailyTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		short DaysInterval { get; set; }
	}

	internal interface IEventTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		string Subscription { get; set; }
		IDictionary<string, string> ValueQueries { get; set; }
	}

	internal interface ILogonTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		string UserId { get; set; }
	}

	internal interface IMonthlyDOWTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		DaysOfTheWeek DaysOfWeek { get; set; }
		MonthsOfTheYear MonthsOfYear { get; set; }
		bool RunOnLastWeekOfMonth { get; set; }
		WhichWeek WeeksOfMonth { get; set; }
	}

	internal interface IMonthlyTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		ICollection<short> DaysOfMonth { get; set; }
		MonthsOfTheYear MonthsOfYear { get; set; }
		bool RunOnLastDayOfMonth { get; set; }
	}

	internal interface IRegistrationTriggerImpl : ITriggerImpl, ITriggerDelay { }

	internal interface ISessionStateChangeTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		TaskSessionStateChangeType StateChange { get; set; }
		string UserId { get; set; }
	}

	internal interface ITimeTriggerImpl : ITriggerImpl, ITriggerDelay { }

	internal interface IWeeklyTriggerImpl : ITriggerImpl, ITriggerDelay
	{
		DaysOfTheWeek DaysOfWeek { get; set; }
		short WeeksInterval { get; set; }
	}
}
