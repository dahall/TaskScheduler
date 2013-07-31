using System;

namespace Microsoft.Win32.TaskScheduler
{
	public sealed partial class TaskService
	{
		/// <summary>
		/// Initial call for a Fluent model of creating a task.
		/// </summary>
		/// <param name="path">The path of the program to run.</param>
		/// <returns>An <see cref="ActionBuilder"/> instance.</returns>
		public ActionBuilder Execute(string path)
		{
			return new ActionBuilder(new BuilderInfo(this), path);
		}
	}

	/// <summary>
	/// Fluent helper class. Not intended for use.
	/// </summary>
	internal sealed class BuilderInfo
	{
		public TaskService ts;
		public TaskDefinition td;

		public BuilderInfo(TaskService taskSvc)
		{
			ts = taskSvc;
			td = ts.NewTask();
		}
	}

	public abstract class BaseBuilder
	{
		internal BuilderInfo tb;

		internal BaseBuilder(BuilderInfo taskBuilder)
		{
			tb = taskBuilder;
		}

		internal TaskDefinition TaskDef { get { return tb.td; } }
	}

	/// <summary>
	/// Fluent helper class. Not intended for use.
	/// </summary>
	public class ActionBuilder : BaseBuilder
	{
		internal ActionBuilder(BuilderInfo taskBuilder, string path)
			: base(taskBuilder)
		{
			TaskDef.Actions.Add(new ExecAction(path));
		}

		public ActionBuilder WithArguments(string args)
		{
			((ExecAction)TaskDef.Actions[0]).Arguments = args;
			return this;
		}

		public ActionBuilder InWorkingDirectory(string dir)
		{
			((ExecAction)TaskDef.Actions[0]).WorkingDirectory = dir;
			return this;
		}

		public IntervalTriggerBuilder Every(short num)
		{
			return new IntervalTriggerBuilder(tb, num);
		}

		public MonthlyDOWTriggerBuilder OnAll(DaysOfTheWeek dow)
		{
			return new MonthlyDOWTriggerBuilder(tb, dow);
		}

		public MonthlyTriggerBuilder InTheMonthOf(MonthsOfTheYear moy)
		{
			return new MonthlyTriggerBuilder(tb, moy);
		}

		public TriggerBuilder Once()
		{
			return new TriggerBuilder(tb, TaskTriggerType.Time);
		}

		public TriggerBuilder OnBoot()
		{
			return new TriggerBuilder(tb, TaskTriggerType.Boot);
		}

		public TriggerBuilder OnIdle()
		{
			return new TriggerBuilder(tb, TaskTriggerType.Idle);
		}

		public TriggerBuilder OnStateChange(TaskSessionStateChangeType changeType)
		{
			var b = new TriggerBuilder(tb, TaskTriggerType.SessionStateChange);
			((SessionStateChangeTrigger)b.trigger).StateChange = changeType;
			return b;
		}

		public TriggerBuilder AtLogon()
		{
			return new TriggerBuilder(tb, TaskTriggerType.Logon);
		}

		public TriggerBuilder AtLogonOf(string userId)
		{
			var b = new TriggerBuilder(tb, TaskTriggerType.Logon);
			((LogonTrigger)b.trigger).UserId = userId;
			return b;
		}

		public TriggerBuilder AtTaskRegistration()
		{
			return new TriggerBuilder(tb, TaskTriggerType.Registration);
		}
	}

	public class MonthlyTriggerBuilder : BaseBuilder
	{
		private TriggerBuilder trb;

		internal MonthlyTriggerBuilder(BuilderInfo taskBuilder, MonthsOfTheYear moy)
			: base(taskBuilder)
		{
			this.trb = new TriggerBuilder(taskBuilder, moy);
		}

		public TriggerBuilder OnTheDays(params int[] days)
		{
			((MonthlyTrigger)trb.trigger).DaysOfMonth = days;
			return trb;
		}
	}

	public class MonthlyDOWTriggerBuilder : BaseBuilder
	{
		private TriggerBuilder trb;

		internal MonthlyDOWTriggerBuilder(BuilderInfo taskBuilder, DaysOfTheWeek dow)
			: base(taskBuilder)
		{
			this.trb = new TriggerBuilder(taskBuilder, dow);
		}

		public MonthlyDOWTriggerBuilder In(WhichWeek ww)
		{
			((MonthlyDOWTrigger)trb.trigger).WeeksOfMonth = ww;
			return this;
		}

		public TriggerBuilder Of(MonthsOfTheYear moy)
		{
			((MonthlyDOWTrigger)trb.trigger).MonthsOfYear = moy;
			return trb;
		}
	}

	public class WeeklyTriggerBuilder : TriggerBuilder
	{
		internal WeeklyTriggerBuilder(BuilderInfo taskBuilder, short interval) : base(taskBuilder)
		{
			TaskDef.Triggers.Add(trigger = new WeeklyTrigger() { WeeksInterval = interval });
		}

		public TriggerBuilder On(DaysOfTheWeek dow)
		{
			((WeeklyTrigger)trigger).DaysOfWeek = dow;
			return this as TriggerBuilder;
		}
	}

	public class IntervalTriggerBuilder : BaseBuilder
	{
		internal short interval = 0;

		internal IntervalTriggerBuilder(BuilderInfo taskBuilder, short interval)
			: base(taskBuilder)
		{
			this.interval = interval;
		}

		public TriggerBuilder Days()
		{
			return new TriggerBuilder(tb) { trigger = TaskDef.Triggers.Add(new DailyTrigger(this.interval)) };
		}

		public WeeklyTriggerBuilder Weeks()
		{
			return new WeeklyTriggerBuilder(tb, interval);
		}
	}

	/// <summary>
	/// Fluent helper class. Not intended for use.
	/// </summary>
	public class TriggerBuilder : BaseBuilder
	{
		internal Trigger trigger;

		internal TriggerBuilder(BuilderInfo taskBuilder)
			: base(taskBuilder)
		{
		}

		internal TriggerBuilder(BuilderInfo taskBuilder, DaysOfTheWeek dow)
			: this(taskBuilder)
		{
			TaskDef.Triggers.Add(trigger = new MonthlyDOWTrigger(dow));
		}

		internal TriggerBuilder(BuilderInfo taskBuilder, MonthsOfTheYear moy)
			: this(taskBuilder)
		{
			TaskDef.Triggers.Add(trigger = new MonthlyTrigger() { MonthsOfYear = moy });
		}

		internal TriggerBuilder(BuilderInfo taskBuilder, TaskTriggerType taskTriggerType)
			: this(taskBuilder)
		{
			TaskDef.Triggers.Add(trigger = Trigger.CreateTrigger(taskTriggerType));
		}

		public TriggerBuilder Starting(int year, int month, int day)
		{
			trigger.StartBoundary = new DateTime(year, month, day, trigger.StartBoundary.Hour, trigger.StartBoundary.Minute, trigger.StartBoundary.Second);
			return this;
		}

		public TriggerBuilder Starting(int year, int month, int day, int hour, int min, int sec)
		{
			trigger.StartBoundary = new DateTime(year, month, day, hour, min, sec);
			return this;
		}

		public TriggerBuilder Starting(string dt)
		{
			trigger.StartBoundary = DateTime.Parse(dt);
			return this;
		}

		public TriggerBuilder Starting(DateTime dt)
		{
			trigger.StartBoundary = dt;
			return this;
		}

		public TriggerBuilder Ending(int year, int month, int day)
		{
			trigger.EndBoundary = new DateTime(year, month, day, trigger.StartBoundary.Hour, trigger.StartBoundary.Minute, trigger.StartBoundary.Second);
			return this;
		}

		public TriggerBuilder Ending(int year, int month, int day, int hour, int min, int sec)
		{
			trigger.EndBoundary = new DateTime(year, month, day, hour, min, sec);
			return this;
		}

		public TriggerBuilder Ending(string dt)
		{
			trigger.EndBoundary = DateTime.Parse(dt);
			return this;
		}

		public TriggerBuilder Ending(DateTime dt)
		{
			trigger.EndBoundary = dt;
			return this;
		}

		public TriggerBuilder RepeatingEvery(TimeSpan span)
		{
			trigger.Repetition.Interval = span;
			return this;
		}

		public TriggerBuilder RepeatingEvery(string span)
		{
			trigger.Repetition.Interval = TimeSpan.Parse(span);
			return this;
		}

		public TriggerBuilder RunningAtMostFor(TimeSpan span)
		{
			trigger.Repetition.Interval = span;
			return this;
		}

		public TriggerBuilder RunningAtMostFor(string span)
		{
			trigger.Repetition.Interval = TimeSpan.Parse(span);
			return this;
		}

		/// <summary>
		/// Assigns the name of the task and registers it.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>A registerd <see cref="Task"/> instance.</returns>
		public Task AsTask(string name)
		{
			return tb.ts.RootFolder.RegisterTaskDefinition(name, TaskDef);
		}
	}
}
