using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Vanara.Extensions;
using static Vanara.PInvoke.MSTask;
using static Vanara.PInvoke.TaskSchd;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Values for days of the week (Monday, Tuesday, etc.)</summary>
	[Flags]
	public enum DaysOfTheWeek : ushort
	{
		/// <summary>Sunday</summary>
		Sunday = TaskDaysOfTheWeek.TASK_SUNDAY,

		/// <summary>Monday</summary>
		Monday = TaskDaysOfTheWeek.TASK_MONDAY,

		/// <summary>Tuesday</summary>
		Tuesday = TaskDaysOfTheWeek.TASK_TUESDAY,

		/// <summary>Wednesday</summary>
		Wednesday = TaskDaysOfTheWeek.TASK_WEDNESDAY,

		/// <summary>Thursday</summary>
		Thursday = TaskDaysOfTheWeek.TASK_THURSDAY,

		/// <summary>Friday</summary>
		Friday = TaskDaysOfTheWeek.TASK_FRIDAY,

		/// <summary>Saturday</summary>
		Saturday = TaskDaysOfTheWeek.TASK_SATURDAY,

		/// <summary>All days</summary>
		AllDays = 0x7F
	}

	/// <summary>Values for months of the year (January, February, etc.)</summary>
	[Flags]
	public enum MonthsOfTheYear : ushort
	{
		/// <summary>January</summary>
		January = TaskMonths.TASK_JANUARY,

		/// <summary>February</summary>
		February = TaskMonths.TASK_FEBRUARY,

		/// <summary>March</summary>
		March = TaskMonths.TASK_MARCH,

		/// <summary>April</summary>
		April = TaskMonths.TASK_APRIL,

		/// <summary>May</summary>
		May = TaskMonths.TASK_MAY,

		/// <summary>June</summary>
		June = TaskMonths.TASK_JUNE,

		/// <summary>July</summary>
		July = TaskMonths.TASK_JULY,

		/// <summary>August</summary>
		August = TaskMonths.TASK_AUGUST,

		/// <summary>September</summary>
		September = TaskMonths.TASK_SEPTEMBER,

		/// <summary>October</summary>
		October = TaskMonths.TASK_OCTOBER,

		/// <summary>November</summary>
		November = TaskMonths.TASK_NOVEMBER,

		/// <summary>December</summary>
		December = TaskMonths.TASK_DECEMBER,

		/// <summary>All months</summary>
		AllMonths = 0xFFF
	}

	/// <summary>Defines the type of triggers that can be used by tasks.</summary>
	[DefaultValue(Time)]
	public enum TaskTriggerType
	{
		/// <summary>Triggers the task when a specific event occurs. Version 1.2 only.</summary>
		Event = TASK_TRIGGER_TYPE2.TASK_TRIGGER_EVENT,

		/// <summary>Triggers the task at a specific time of day.</summary>
		Time = TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME,

		/// <summary>Triggers the task on a daily schedule.</summary>
		Daily = TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY,

		/// <summary>Triggers the task on a weekly schedule.</summary>
		Weekly = TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY,

		/// <summary>Triggers the task on a monthly schedule.</summary>
		Monthly = TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY,

		/// <summary>Triggers the task on a monthly day-of-week schedule.</summary>
		MonthlyDOW = TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLYDOW,

		/// <summary>Triggers the task when the computer goes into an idle state.</summary>
		Idle = TASK_TRIGGER_TYPE2.TASK_TRIGGER_IDLE,

		/// <summary>Triggers the task when the task is registered. Version 1.2 only.</summary>
		Registration = TASK_TRIGGER_TYPE2.TASK_TRIGGER_REGISTRATION,

		/// <summary>Triggers the task when the computer boots.</summary>
		Boot = TASK_TRIGGER_TYPE2.TASK_TRIGGER_BOOT,

		/// <summary>Triggers the task when a specific user logs on.</summary>
		Logon = TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON,

		/// <summary>Triggers the task when a specific user session state changes. Version 1.2 only.</summary>
		SessionStateChange = TASK_TRIGGER_TYPE2.TASK_TRIGGER_SESSION_STATE_CHANGE,

		/// <summary>Triggers the custom trigger. Version 1.3 only.</summary>
		Custom = TASK_TRIGGER_TYPE2.TASK_TRIGGER_CUSTOM_TRIGGER_01,
	}

	/// <summary>Values for week of month (first, second, ..., last)</summary>
	[Flags]
	public enum WhichWeek : ushort
	{
		/// <summary>First week of the month</summary>
		FirstWeek = TaskWhichWeek.TASK_FIRST_WEEK,

		/// <summary>Second week of the month</summary>
		SecondWeek = TaskWhichWeek.TASK_SECOND_WEEK,

		/// <summary>Third week of the month</summary>
		ThirdWeek = TaskWhichWeek.TASK_THIRD_WEEK,

		/// <summary>Fourth week of the month</summary>
		FourthWeek = TaskWhichWeek.TASK_FOURTH_WEEK,

		/// <summary>Last week of the month</summary>
		LastWeek = TaskWhichWeek.TASK_LAST_WEEK,

		/// <summary>Every week of the month</summary>
		AllWeeks = 0x1F
	}

	/// <summary>Interface that categorizes the trigger as a calendar trigger.</summary>
	public interface ICalendarTrigger { }

	/// <summary>Interface for triggers that support a delay.</summary>
	public interface ITriggerDelay
	{
		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan Delay { get; set; }

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? DelayNullable { get; set; }
	}

	/// <summary>Interface for triggers that support a user identifier.</summary>
	public interface ITriggerUserId
	{
		/// <summary>Gets or sets the user for the <see cref="Trigger"/>.</summary>
		string UserId { get; set; }
	}

	/// <summary>Represents a trigger that starts a task when the system is booted.</summary>
	/// <remarks>
	/// A BootTrigger will fire when the system starts. It can only be delayed. All triggers that support a delay implement the ITriggerDelay interface.
	/// </remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create trigger that fires 5 minutes after the system starts.
	/// BootTrigger bt = new BootTrigger();
	/// bt.Delay = TimeSpan.FromMinutes(5);  // V2 only
	/// ]]>
	/// </code>
	/// </example>
	public sealed class BootTrigger : Trigger, ITriggerDelay, Models.IBootTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="BootTrigger"/>.</summary>
		public BootTrigger() : base(TaskTriggerType.Boot) { }

		internal BootTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_AT_SYSTEMSTART)
		{
		}

		internal BootTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan Delay
		{
			get => DelayNullable.GetValueOrDefault();
			set => DelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? DelayNullable
		{
			get => v2Trigger != null ? ((IBootTrigger)v2Trigger).Delay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(Delay));
			set
			{
				if (v2Trigger != null)
					((IBootTrigger)v2Trigger).Delay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(Delay)] = value;
			}
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => Properties.Resources.TriggerBoot1;
	}

	/// <summary>
	/// Represents a custom trigger. This class is based on undocumented features and may change. <note>This type of trigger is only
	/// available for reading custom triggers. It cannot be used to create custom triggers.</note>
	/// </summary>
	public sealed class CustomTrigger : Trigger, ITriggerDelay, Models.ICustomTrigger
	{
		private TimeSpan? delay = null;

		internal CustomTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets a value that indicates the amount of time between the trigger events and when the task is started.</summary>
		/// <exception cref="System.NotImplementedException">This value cannot be set.</exception>
		public TimeSpan Delay
		{
			get => delay.GetValueOrDefault();
			set => throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates the amount of time between the trigger events and when the task is started.</summary>
		/// <exception cref="System.NotImplementedException">This value cannot be set.</exception>
		public TimeSpan? DelayNullable
		{
			get => delay;
			set => throw new NotImplementedException();
		}

		/// <summary>Gets the name of the custom trigger type.</summary>
		/// <value>The name of the XML element representing this custom trigger.</value>
		public string Name { get; private set; } = string.Empty;

		/// <summary>Gets the properties from the XML definition if possible.</summary>
		[XmlArray, XmlArrayItem("Property")]
		public NamedValueCollection Properties { get; } = new NamedValueCollection();

		/// <summary>Gets the properties from the XML definition if possible.</summary>
		IDictionary<string, string> Models.ICustomTrigger.Properties => Properties;

		/// <summary>Clones this instance.</summary>
		/// <returns>This method will always throw an exception.</returns>
		/// <exception cref="System.InvalidOperationException">CustomTrigger cannot be cloned due to OS restrictions.</exception>
		public override object Clone() => throw new InvalidOperationException("CustomTrigger cannot be cloned due to OS restrictions.");

		/// <summary>Updates custom properties from XML provided by definition.</summary>
		/// <param name="xml">The XML from the TaskDefinition.</param>
		internal void UpdateFromXml(string xml)
		{
			Properties.Clear();
			try
			{
				var xmlDoc = new System.Xml.XmlDocument();
				xmlDoc.LoadXml(xml);
				var nsmgr = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
				nsmgr.AddNamespace("n", "http://schemas.microsoft.com/windows/2004/02/mit/task");
				var elem = xmlDoc.DocumentElement?.SelectSingleNode("n:Triggers/*[@id='" + Id + "']", nsmgr);
				if (elem == null)
				{
					var nodes = xmlDoc.GetElementsByTagName("WnfStateChangeTrigger");
					if (nodes.Count == 1)
						elem = nodes[0];
				}

				if (elem == null) return;

				Name = elem.LocalName;
				foreach (System.Xml.XmlNode node in elem.ChildNodes)
				{
					switch (node.LocalName)
					{
						case "Delay":
							delay = Task.StringToTimeSpan(node.InnerText);
							break;

						case "StartBoundary":
						case "Enabled":
						case "EndBoundary":
						case "ExecutionTimeLimit":
							break;

						default:
							Properties.Add(node.LocalName, node.InnerText);
							break;
					}
				}
			}
			catch { /* ignored */ }
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => TaskScheduler.Properties.Resources.TriggerCustom1;
	}

	/// <summary>
	/// Represents a trigger that starts a task based on a daily schedule. For example, the task starts at a specific time every day, every
	/// other day, every third day, and so on.
	/// </summary>
	/// <remarks>A DailyTrigger will fire at a specified time every day or interval of days.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a trigger that runs every other day and will start randomly between 10 a.m. and 12 p.m.
	/// DailyTrigger dt = new DailyTrigger();
	/// dt.StartBoundary = DateTime.Today + TimeSpan.FromHours(10);
	/// dt.DaysInterval = 2;
	/// dt.RandomDelay = TimeSpan.FromHours(2); // V2 only
	/// ]]>
	/// </code>
	/// </example>
	[XmlRoot("CalendarTrigger", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class DailyTrigger : Trigger, ICalendarTrigger, ITriggerDelay, IXmlSerializable, Models.IDailyTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="DailyTrigger"/>.</summary>
		/// <param name="daysInterval">Interval between the days in the schedule.</param>
		public DailyTrigger(short daysInterval = 1) : base(TaskTriggerType.Daily) => DaysInterval = daysInterval;

		internal DailyTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_DAILY)
		{
			if (v1TriggerData.Type.Daily.DaysInterval == 0)
				v1TriggerData.Type.Daily.DaysInterval = 1;
		}

		internal DailyTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Sets or retrieves the interval between the days in the schedule.</summary>
		[DefaultValue(1)]
		public short DaysInterval
		{
			get => v2Trigger != null ? ((IDailyTrigger)v2Trigger).DaysInterval : (short)v1TriggerData.Type.Daily.DaysInterval;
			set
			{
				if (v2Trigger != null)
					((IDailyTrigger)v2Trigger).DaysInterval = value;
				else
				{
					v1TriggerData.Type.Daily.DaysInterval = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(DaysInterval)] = value;
				}
			}
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RandomDelay
		{
			get => RandomDelayNullable.GetValueOrDefault();
			set => RandomDelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? RandomDelayNullable
		{
			get => v2Trigger != null ? ((IDailyTrigger)v2Trigger).RandomDelay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(RandomDelay));
			set
			{
				if (v2Trigger != null)
					((IDailyTrigger)v2Trigger).RandomDelay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RandomDelay)] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan ITriggerDelay.Delay
		{
			get => RandomDelay;
			set => RandomDelay = value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? ITriggerDelay.DelayNullable
		{
			get => RandomDelayNullable;
			set => RandomDelayNullable = value;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is DailyTrigger dt)
			{
				DaysInterval = dt.DaysInterval;
			}
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is DailyTrigger dt && base.Equals(dt) && DaysInterval == dt.DaysInterval;

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader) => CalendarTrigger.ReadXml(reader, this, ReadMyXml);

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) => CalendarTrigger.WriteXml(writer, this, WriteMyXml);

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => DaysInterval == 1 ?
			string.Format(Properties.Resources.TriggerDaily1, AdjustToLocal(StartBoundary)) :
			string.Format(Properties.Resources.TriggerDaily2, AdjustToLocal(StartBoundary), DaysInterval);

		private void ReadMyXml(System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("ScheduleByDay");
			if (reader.MoveToContent() == System.Xml.XmlNodeType.Element && reader.LocalName == "DaysInterval")
				// ReSharper disable once AssignNullToNotNullAttribute
				DaysInterval = (short)reader.ReadElementContentAs(typeof(short), null);
			reader.Read();
			reader.ReadEndElement();
		}

		private void WriteMyXml(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("ScheduleByDay");
			writer.WriteElementString("DaysInterval", DaysInterval.ToString());
			writer.WriteEndElement();
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when a system event occurs. <note>Only available for Task Scheduler 2.0 on Windows Vista or
	/// Windows Server 2003 and later.</note>
	/// </summary>
	/// <remarks>The EventTrigger runs when a system event fires.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a trigger that will fire whenever a level 2 system event fires.
	/// EventTrigger eTrigger = new EventTrigger();
	/// eTrigger.Subscription = @"<QueryList><Query Id='1'><Select Path='System'>*[System/Level=2]</Select></Query></QueryList>";
	/// eTrigger.ValueQueries.Add("Name", "Value");
	/// ]]>
	/// </code>
	/// </example>
	[XmlType(IncludeInSchema = false)]
	public sealed class EventTrigger : Trigger, ITriggerDelay, Models.IEventTrigger
	{
		private NamedValueCollection nvc;

		/// <summary>Creates an unbound instance of a <see cref="EventTrigger"/>.</summary>
		public EventTrigger() : base(TaskTriggerType.Event) { }

		/// <summary>Initializes an unbound instance of the <see cref="EventTrigger"/> class and sets a basic event.</summary>
		/// <param name="log">The event's log.</param>
		/// <param name="source">The event's source. Can be <c>null</c>.</param>
		/// <param name="eventId">The event's id. Can be <c>null</c>.</param>
		public EventTrigger(string log, string source, int? eventId) : this() => SetBasic(log, source, eventId);

		internal EventTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Delay
		{
			get => DelayNullable.GetValueOrDefault();
			set => DelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? DelayNullable
		{
			get => v2Trigger != null ? ((IEventTrigger)v2Trigger).Delay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(Delay));
			set
			{
				if (v2Trigger != null)
					((IEventTrigger)v2Trigger).Delay = value;
				else
					unboundValues[nameof(Delay)] = value;
			}
		}

		/// <summary>Gets or sets the XPath query string that identifies the event that fires the trigger.</summary>
		[DefaultValue(null)]
		public string Subscription
		{
			get => v2Trigger != null ? ((IEventTrigger)v2Trigger).Subscription : GetUnboundValueOrDefault<string>(nameof(Subscription));
			set
			{
				if (v2Trigger != null)
					((IEventTrigger)v2Trigger).Subscription = value;
				else
					unboundValues[nameof(Subscription)] = value;
			}
		}

		/// <summary>
		/// Gets or sets a collection of named XPath queries. Each query in the collection is applied to the last matching event XML returned
		/// from the subscription query specified in the <see cref="Subscription"/> property.
		/// </summary>
		[XmlArray]
		[XmlArrayItem("Value", typeof(NameValuePair))]
		public NamedValueCollection ValueQueries => nvc ?? (nvc = v2Trigger == null ? new NamedValueCollection() : new NamedValueCollection(((IEventTrigger)v2Trigger).ValueQueries));

		/// <summary>
		/// Gets or sets a collection of named XPath queries. Each query in the collection is applied to the last matching event XML returned
		/// from the subscription query specified in the <see cref="Subscription"/> property.
		/// </summary>
		IDictionary<string, string> Models.IEventTrigger.ValueQueries => ValueQueries;

		/// <summary>Builds an event log XML query string based on the input parameters.</summary>
		/// <param name="log">The event's log.</param>
		/// <param name="source">The event's source. Can be <c>null</c>.</param>
		/// <param name="eventId">The event's id. Can be <c>null</c>.</param>
		/// <returns>XML query string.</returns>
		/// <exception cref="System.ArgumentNullException">log</exception>
		public static string BuildQuery(string log, string source, int? eventId)
		{
			var sb = new StringBuilder();
			if (string.IsNullOrEmpty(log))
				throw new ArgumentNullException(nameof(log));
			sb.AppendFormat("<QueryList><Query Id=\"0\" Path=\"{0}\"><Select Path=\"{0}\">*", log);
			bool hasSource = !string.IsNullOrEmpty(source), hasId = eventId.HasValue;
			if (hasSource || hasId)
			{
				sb.Append("[System[");
				if (hasSource)
					sb.AppendFormat("Provider[@Name='{0}']", source);
				if (hasSource && hasId)
					sb.Append(" and ");
				if (hasId)
					sb.AppendFormat("EventID={0}", eventId.Value);
				sb.Append("]]");
			}
			sb.Append("</Select></Query></QueryList>");
			return sb.ToString();
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is EventTrigger et)
			{
				Subscription = et.Subscription;
				et.ValueQueries.CopyTo(ValueQueries);
			}
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is EventTrigger et && base.Equals(et) && Subscription == et.Subscription;

		/// <summary>Gets basic event information.</summary>
		/// <param name="log">The event's log.</param>
		/// <param name="source">The event's source. Can be <c>null</c>.</param>
		/// <param name="eventId">The event's id. Can be <c>null</c>.</param>
		/// <returns><c>true</c> if subscription represents a basic event, <c>false</c> if not.</returns>
		public bool GetBasic(out string log, out string source, out int? eventId)
		{
			log = source = null;
			eventId = null;
			if (!string.IsNullOrEmpty(Subscription))
			{
				using (var str = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Subscription)))
				{
					using (var rdr = new System.Xml.XmlTextReader(str))
					{
						rdr.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
						try
						{
							rdr.MoveToContent();
							rdr.ReadStartElement("QueryList");
							if (rdr.Name == "Query" && rdr.MoveToAttribute("Path"))
							{
								var path = rdr.Value;
								if (rdr.MoveToElement() && rdr.ReadToDescendant("Select") && path.Equals(rdr["Path"], StringComparison.InvariantCultureIgnoreCase))
								{
									var content = rdr.ReadString();
									var m = System.Text.RegularExpressions.Regex.Match(content,
										@"\*(?:\[System\[(?:Provider\[\@Name='(?<s>[^']+)'\])?(?:\s+and\s+)?(?:EventID=(?<e>\d+))?\]\])",
										System.Text.RegularExpressions.RegexOptions.IgnoreCase |
										System.Text.RegularExpressions.RegexOptions.Compiled |
										System.Text.RegularExpressions.RegexOptions.Singleline |
										System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
									if (m.Success)
									{
										log = path;
										if (m.Groups["s"].Success)
											source = m.Groups["s"].Value;
										if (m.Groups["e"].Success)
											eventId = Convert.ToInt32(m.Groups["e"].Value);
										return true;
									}
								}
							}
						}
						catch { /* ignored */ }
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Sets the subscription for a basic event. This will replace the contents of the <see cref="Subscription"/> property and clear all
		/// entries in the <see cref="ValueQueries"/> property.
		/// </summary>
		/// <param name="log">The event's log.</param>
		/// <param name="source">The event's source. Can be <c>null</c>.</param>
		/// <param name="eventId">The event's id. Can be <c>null</c>.</param>
		public void SetBasic([NotNull] string log, string source, int? eventId)
		{
			ValueQueries.Clear();
			Subscription = BuildQuery(log, source, eventId);
		}

		internal override void Bind(ITaskDefinition iTaskDef)
		{
			base.Bind(iTaskDef);
			nvc?.Bind(((IEventTrigger)v2Trigger).ValueQueries);
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			if (!GetBasic(out var log, out var source, out var id))
				return Properties.Resources.TriggerEvent1;
			var sb = new StringBuilder();
			sb.AppendFormat(Properties.Resources.TriggerEventBasic1, log);
			if (!string.IsNullOrEmpty(source))
				sb.AppendFormat(Properties.Resources.TriggerEventBasic2, source);
			if (id.HasValue)
				sb.AppendFormat(Properties.Resources.TriggerEventBasic3, id.Value);
			return sb.ToString();
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when the computer goes into an idle state. For information about idle conditions, see Task
	/// Idle Conditions.
	/// </summary>
	/// <remarks>
	/// An IdleTrigger will fire when the system becomes idle. It is generally a good practice to set a limit on how long it can run using
	/// the ExecutionTimeLimit property.
	/// </remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// IdleTrigger it = new IdleTrigger();
	/// ]]>
	/// </code>
	/// </example>
	public sealed class IdleTrigger : Trigger, Models.IIdleTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="IdleTrigger"/>.</summary>
		public IdleTrigger() : base(TaskTriggerType.Idle) { }

		internal IdleTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_ON_IDLE)
		{
		}

		internal IdleTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => Properties.Resources.TriggerIdle1;
	}

	/// <summary>
	/// Represents a trigger that starts a task when a user logs on. When the Task Scheduler service starts, all logged-on users are
	/// enumerated and any tasks registered with logon triggers that match the logged on user are run. Not available on Task Scheduler 1.0.
	/// </summary>
	/// <remarks>
	/// A LogonTrigger will fire after a user logs on. It can only be delayed. Under V2, you can specify which user it applies to.
	/// </remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Add a general logon trigger
	/// LogonTrigger lt1 = new LogonTrigger();
	///
	/// // V2 only: Add a delayed logon trigger for a specific user
	/// LogonTrigger lt2 = new LogonTrigger { UserId = "LocalUser" };
	/// lt2.Delay = TimeSpan.FromMinutes(15);
	/// ]]>
	/// </code>
	/// </example>
	public sealed class LogonTrigger : Trigger, ITriggerDelay, ITriggerUserId, Models.ILogonTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="LogonTrigger"/>.</summary>
		public LogonTrigger() : base(TaskTriggerType.Logon) { }

		internal LogonTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_AT_LOGON)
		{
		}

		internal LogonTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan Delay
		{
			get => DelayNullable.GetValueOrDefault();
			set => DelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? DelayNullable
		{
			get => v2Trigger != null ? ((ILogonTrigger)v2Trigger).Delay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(Delay));
			set
			{
				if (v2Trigger != null)
					((ILogonTrigger)v2Trigger).Delay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(Delay)] = value;
			}
		}

		/// <summary>
		/// <para>Gets or sets The identifier of the user. For example, "MyDomain\MyName" or for a local account, "Administrator".</para>
		/// <para>This property can be in one of the following formats:</para>
		/// <para>• User name or SID: The task is started when the user logs on to the computer.</para>
		/// <para>• NULL: The task is started when any user logs on to the computer.</para>
		/// </summary>
		/// <remarks>
		/// If you want a task to be triggered when any member of a group logs on to the computer rather than when a specific user logs on,
		/// then do not assign a value to the LogonTrigger.UserId property. Instead, create a logon trigger with an empty LogonTrigger.UserId
		/// property and assign a value to the principal for the task using the Principal.GroupId property.
		/// </remarks>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(null)]
		[XmlIgnore]
		public string UserId
		{
			get => v2Trigger != null ? ((ILogonTrigger)v2Trigger).UserId : GetUnboundValueOrDefault<string>(nameof(UserId));
			set
			{
				if (v2Trigger != null)
					((ILogonTrigger)v2Trigger).UserId = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(UserId)] = value;
			}
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			var user = string.IsNullOrEmpty(UserId) ? Properties.Resources.TriggerAnyUser : UserId;
			return string.Format(Properties.Resources.TriggerLogon1, user);
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task on a monthly day-of-week schedule. For example, the task starts on every first Thursday, May
	/// through October.
	/// </summary>
	[XmlRoot("CalendarTrigger", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class MonthlyDOWTrigger : Trigger, ICalendarTrigger, ITriggerDelay, IXmlSerializable, Models.IMonthlyDOWTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="MonthlyDOWTrigger"/>.</summary>
		/// <param name="daysOfWeek">The days of the week.</param>
		/// <param name="monthsOfYear">The months of the year.</param>
		/// <param name="weeksOfMonth">The weeks of the month.</param>
		public MonthlyDOWTrigger(DaysOfTheWeek daysOfWeek = DaysOfTheWeek.Sunday, MonthsOfTheYear monthsOfYear = MonthsOfTheYear.AllMonths, WhichWeek weeksOfMonth = WhichWeek.FirstWeek) : base(TaskTriggerType.MonthlyDOW)
		{
			DaysOfWeek = daysOfWeek;
			MonthsOfYear = monthsOfYear;
			WeeksOfMonth = weeksOfMonth;
		}

		internal MonthlyDOWTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_MONTHLYDOW)
		{
			if (v1TriggerData.Type.MonthlyDOW.rgfMonths == 0)
				v1TriggerData.Type.MonthlyDOW.rgfMonths = (TaskMonths)MonthsOfTheYear.AllMonths;
			if (v1TriggerData.Type.MonthlyDOW.rgfDaysOfTheWeek == 0)
				v1TriggerData.Type.MonthlyDOW.rgfDaysOfTheWeek = TaskDaysOfTheWeek.TASK_SUNDAY;
		}

		internal MonthlyDOWTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets the days of the week during which the task runs.</summary>
		[DefaultValue(0)]
		public DaysOfTheWeek DaysOfWeek
		{
			get => v2Trigger != null
				? (DaysOfTheWeek)((IMonthlyDOWTrigger)v2Trigger).DaysOfWeek
				: (DaysOfTheWeek)v1TriggerData.Type.MonthlyDOW.rgfDaysOfTheWeek;
			set
			{
				if (v2Trigger != null)
					((IMonthlyDOWTrigger)v2Trigger).DaysOfWeek = (TaskDaysOfTheWeek)value;
				else
				{
					v1TriggerData.Type.MonthlyDOW.rgfDaysOfTheWeek = (TaskDaysOfTheWeek)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(DaysOfWeek)] = value;
				}
			}
		}

		/// <summary>Gets or sets the months of the year during which the task runs.</summary>
		[DefaultValue(0)]
		public MonthsOfTheYear MonthsOfYear
		{
			get => v2Trigger != null
				? (MonthsOfTheYear)((IMonthlyDOWTrigger)v2Trigger).MonthsOfYear
				: (MonthsOfTheYear)v1TriggerData.Type.MonthlyDOW.rgfMonths;
			set
			{
				if (v2Trigger != null)
					((IMonthlyDOWTrigger)v2Trigger).MonthsOfYear = (TaskMonths)value;
				else
				{
					v1TriggerData.Type.MonthlyDOW.rgfMonths = (TaskMonths)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(MonthsOfYear)] = value;
				}
			}
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RandomDelay
		{
			get => RandomDelayNullable.GetValueOrDefault();
			set => RandomDelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? RandomDelayNullable
		{
			get => v2Trigger != null ? ((IMonthlyDOWTrigger)v2Trigger).RandomDelay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(RandomDelay));
			set
			{
				if (v2Trigger != null)
					((IMonthlyDOWTrigger)v2Trigger).RandomDelay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RandomDelay)] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates that the task runs on the last week of the month.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(false)]
		[XmlIgnore]
		public bool RunOnLastWeekOfMonth
		{
			get => ((IMonthlyDOWTrigger)v2Trigger)?.RunOnLastWeekOfMonth ?? GetUnboundValueOrDefault(nameof(RunOnLastWeekOfMonth), false);
			set
			{
				if (v2Trigger != null)
					((IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RunOnLastWeekOfMonth)] = value;
			}
		}

		/// <summary>Gets or sets the weeks of the month during which the task runs.</summary>
		[DefaultValue(0)]
		public WhichWeek WeeksOfMonth
		{
			get
			{
				if (v2Trigger == null)
					return v1Trigger != null
						? v1TriggerData.Type.MonthlyDOW.GetV2WhichWeek()
						: GetUnboundValueOrDefault(nameof(WeeksOfMonth), WhichWeek.FirstWeek);
				var ww = (WhichWeek)((IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth;
				// Following addition give accurate results for confusing RunOnLastWeekOfMonth property (thanks kbergeron)
				if (((IMonthlyDOWTrigger)v2Trigger).RunOnLastWeekOfMonth)
					ww |= WhichWeek.LastWeek;
				return ww;
			}
			set
			{
				// In Windows 10, the native library no longer acknowledges the LastWeek value and requires the RunOnLastWeekOfMonth to be
				// expressly set. I think this is wrong so I am correcting their changed functionality. (thanks @SebastiaanPolfliet)
				if (value.IsFlagSet(WhichWeek.LastWeek))
					RunOnLastWeekOfMonth = true;
				if (v2Trigger != null)
				{
					((IMonthlyDOWTrigger)v2Trigger).WeeksOfMonth = (TaskWeeksOfMonth)value;
				}
				else
				{
					try
					{
						v1TriggerData.Type.MonthlyDOW.SetV2WhichWeek(value);
					}
					catch (NotV1SupportedException)
					{
						if (v1Trigger != null) throw;
					}
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(WeeksOfMonth)] = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan ITriggerDelay.Delay
		{
			get => RandomDelay;
			set => RandomDelay = value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? ITriggerDelay.DelayNullable
		{
			get => RandomDelayNullable;
			set => RandomDelayNullable = value;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is MonthlyDOWTrigger mt)
			{
				DaysOfWeek = mt.DaysOfWeek;
				MonthsOfYear = mt.MonthsOfYear;
				try { RunOnLastWeekOfMonth = mt.RunOnLastWeekOfMonth; } catch { /* ignored */ }
				WeeksOfMonth = mt.WeeksOfMonth;
			}
			if (sourceTrigger is MonthlyTrigger m)
				MonthsOfYear = m.MonthsOfYear;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is MonthlyDOWTrigger mt && base.Equals(other) && DaysOfWeek == mt.DaysOfWeek &&
			MonthsOfYear == mt.MonthsOfYear && WeeksOfMonth == mt.WeeksOfMonth && v1Trigger == null && RunOnLastWeekOfMonth == mt.RunOnLastWeekOfMonth;

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader) => CalendarTrigger.ReadXml(reader, this, ReadMyXml);

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) => CalendarTrigger.WriteXml(writer, this, WriteMyXml);

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			var ww = TaskEnumGlobalizer.GetString(WeeksOfMonth);
			var days = TaskEnumGlobalizer.GetString(DaysOfWeek);
			var months = TaskEnumGlobalizer.GetString(MonthsOfYear);
			return string.Format(Properties.Resources.TriggerMonthlyDOW1, AdjustToLocal(StartBoundary), ww, days, months);
		}

		/// <summary>Reads the subclass XML for V1 streams.</summary>
		/// <param name="reader">The reader.</param>
		private void ReadMyXml([NotNull] System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("ScheduleByMonthDayOfWeek");
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				switch (reader.LocalName)
				{
					case "Weeks":
						reader.Read();
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							if (reader.LocalName == "Week")
							{
								var wk = reader.ReadElementContentAsString();
								if (wk == "Last")
									WeeksOfMonth = WhichWeek.LastWeek;
								else
								{
									switch (int.Parse(wk))
									{
										case 1:
											WeeksOfMonth = WhichWeek.FirstWeek;
											break;

										case 2:
											WeeksOfMonth = WhichWeek.SecondWeek;
											break;

										case 3:
											WeeksOfMonth = WhichWeek.ThirdWeek;
											break;

										case 4:
											WeeksOfMonth = WhichWeek.FourthWeek;
											break;

										default:
											throw new System.Xml.XmlException("Week element must contain a 1-4 or Last as content.");
									}
								}
							}
						}
						reader.ReadEndElement();
						break;

					case "DaysOfWeek":
						reader.Read();
						DaysOfWeek = 0;
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							try
							{
								DaysOfWeek |= (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), reader.LocalName);
							}
							catch
							{
								throw new System.Xml.XmlException("Invalid days of the week element.");
							}
							reader.Read();
						}
						reader.ReadEndElement();
						break;

					case "Months":
						reader.Read();
						MonthsOfYear = 0;
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							try
							{
								MonthsOfYear |= (MonthsOfTheYear)Enum.Parse(typeof(MonthsOfTheYear), reader.LocalName);
							}
							catch
							{
								throw new System.Xml.XmlException("Invalid months of the year element.");
							}
							reader.Read();
						}
						reader.ReadEndElement();
						break;

					default:
						reader.Skip();
						break;
				}
			}
			reader.ReadEndElement();
		}

		/// <summary>Writes the subclass XML for V1 streams.</summary>
		/// <param name="writer">The writer.</param>
		private void WriteMyXml([NotNull] System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("ScheduleByMonthDayOfWeek");

			writer.WriteStartElement("Weeks");
			if ((WeeksOfMonth & WhichWeek.FirstWeek) == WhichWeek.FirstWeek)
				writer.WriteElementString("Week", "1");
			if ((WeeksOfMonth & WhichWeek.SecondWeek) == WhichWeek.SecondWeek)
				writer.WriteElementString("Week", "2");
			if ((WeeksOfMonth & WhichWeek.ThirdWeek) == WhichWeek.ThirdWeek)
				writer.WriteElementString("Week", "3");
			if ((WeeksOfMonth & WhichWeek.FourthWeek) == WhichWeek.FourthWeek)
				writer.WriteElementString("Week", "4");
			if ((WeeksOfMonth & WhichWeek.LastWeek) == WhichWeek.LastWeek)
				writer.WriteElementString("Week", "Last");
			writer.WriteEndElement();

			writer.WriteStartElement("DaysOfWeek");
			foreach (DaysOfTheWeek e in Enum.GetValues(typeof(DaysOfTheWeek)))
				if (e != DaysOfTheWeek.AllDays && (DaysOfWeek & e) == e)
					writer.WriteElementString(e.ToString(), null);
			writer.WriteEndElement();

			writer.WriteStartElement("Months");
			foreach (MonthsOfTheYear e in Enum.GetValues(typeof(MonthsOfTheYear)))
				if (e != MonthsOfTheYear.AllMonths && (MonthsOfYear & e) == e)
					writer.WriteElementString(e.ToString(), null);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}

	/// <summary>
	/// Represents a trigger that starts a job based on a monthly schedule. For example, the task starts on specific days of specific months.
	/// </summary>
	[XmlRoot("CalendarTrigger", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class MonthlyTrigger : Trigger, ICalendarTrigger, ITriggerDelay, IXmlSerializable, Models.IMonthlyTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="MonthlyTrigger"/>.</summary>
		/// <param name="dayOfMonth">
		/// The day of the month. This must be a value between 1 and 32. If this value is set to 32, then the
		/// <see cref="RunOnLastDayOfMonth"/> value will be set and no days will be added regardless of the month.
		/// </param>
		/// <param name="monthsOfYear">The months of the year.</param>
		public MonthlyTrigger(int dayOfMonth = 1, MonthsOfTheYear monthsOfYear = MonthsOfTheYear.AllMonths) : base(TaskTriggerType.Monthly)
		{
			if (dayOfMonth < 1 || dayOfMonth > 32) throw new ArgumentOutOfRangeException(nameof(dayOfMonth));
			if (!monthsOfYear.IsValid()) throw new ArgumentOutOfRangeException(nameof(monthsOfYear));
			if (dayOfMonth == 32)
			{
				DaysOfMonth = new int[0];
				RunOnLastDayOfMonth = true;
			}
			else
				DaysOfMonth = new[] { dayOfMonth };
			MonthsOfYear = monthsOfYear;
		}

		internal MonthlyTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_MONTHLYDATE)
		{
			if (v1TriggerData.Type.MonthlyDate.Months == 0)
				v1TriggerData.Type.MonthlyDate.Months = (TaskMonths)MonthsOfTheYear.AllMonths;
			if (v1TriggerData.Type.MonthlyDate.Days == 0)
				v1TriggerData.Type.MonthlyDate.Days = 1;
		}

		internal MonthlyTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets the days of the month during which the task runs.</summary>
		public int[] DaysOfMonth
		{
			get => v2Trigger != null ? MaskToIndices(((IMonthlyTrigger)v2Trigger).DaysOfMonth) : MaskToIndices((int)v1TriggerData.Type.MonthlyDate.Days);
			set
			{
				var mask = IndicesToMask(value);
				if (v2Trigger != null)
					((IMonthlyTrigger)v2Trigger).DaysOfMonth = mask;
				else
				{
					v1TriggerData.Type.MonthlyDate.Days = (uint)mask;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(DaysOfMonth)] = mask;
				}
			}
		}

		/// <summary>Gets or sets the months of the year during which the task runs.</summary>
		[DefaultValue(0)]
		public MonthsOfTheYear MonthsOfYear
		{
			get => v2Trigger != null
				? (MonthsOfTheYear)((IMonthlyTrigger)v2Trigger).MonthsOfYear
				: (MonthsOfTheYear)v1TriggerData.Type.MonthlyDOW.rgfMonths;
			set
			{
				if (v2Trigger != null)
					((IMonthlyTrigger)v2Trigger).MonthsOfYear = (TaskMonths)value;
				else
				{
					v1TriggerData.Type.MonthlyDOW.rgfMonths = (TaskMonths)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(MonthsOfYear)] = value;
				}
			}
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RandomDelay
		{
			get => RandomDelayNullable.GetValueOrDefault();
			set => RandomDelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? RandomDelayNullable
		{
			get => v2Trigger != null ? ((IMonthlyTrigger)v2Trigger).RandomDelay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(RandomDelay));
			set
			{
				if (v2Trigger != null)
					((IMonthlyTrigger)v2Trigger).RandomDelay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RandomDelay)] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates that the task runs on the last day of the month.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(false)]
		[XmlIgnore]
		public bool RunOnLastDayOfMonth
		{
			get => ((IMonthlyTrigger)v2Trigger)?.RunOnLastDayOfMonth ?? GetUnboundValueOrDefault(nameof(RunOnLastDayOfMonth), false);
			set
			{
				if (v2Trigger != null)
					((IMonthlyTrigger)v2Trigger).RunOnLastDayOfMonth = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RunOnLastDayOfMonth)] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan ITriggerDelay.Delay
		{
			get => RandomDelay;
			set => RandomDelay = value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? ITriggerDelay.DelayNullable
		{
			get => RandomDelayNullable;
			set => RandomDelayNullable = value;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is MonthlyTrigger mt)
			{
				DaysOfMonth = mt.DaysOfMonth;
				MonthsOfYear = mt.MonthsOfYear;
				try { RunOnLastDayOfMonth = mt.RunOnLastDayOfMonth; } catch { /* ignored */ }
			}
			if (sourceTrigger is MonthlyDOWTrigger mdt)
				MonthsOfYear = mdt.MonthsOfYear;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is MonthlyTrigger mt && base.Equals(mt) && ListsEqual(DaysOfMonth, mt.DaysOfMonth) &&
			MonthsOfYear == mt.MonthsOfYear && v1Trigger == null && RunOnLastDayOfMonth == mt.RunOnLastDayOfMonth;

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader) => CalendarTrigger.ReadXml(reader, this, ReadMyXml);

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) => CalendarTrigger.WriteXml(writer, this, WriteMyXml);

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			var days = string.Join(Properties.Resources.ListSeparator, Array.ConvertAll(DaysOfMonth, i => i.ToString()));
			if (RunOnLastDayOfMonth)
				days += (days.Length == 0 ? "" : Properties.Resources.ListSeparator) + Properties.Resources.WWLastWeek;
			var months = TaskEnumGlobalizer.GetString(MonthsOfYear);
			return string.Format(Properties.Resources.TriggerMonthly1, AdjustToLocal(StartBoundary), days, months);
		}

		/// <summary>
		/// Converts an array of bit indices into a mask with bits turned ON at every index contained in the array. Indices must be from 1 to
		/// 32 and bits are numbered the same.
		/// </summary>
		/// <param name="indices">An array with an element for each bit of the mask which is ON.</param>
		/// <returns>An integer to be interpreted as a mask.</returns>
		private static int IndicesToMask([NotNull] int[] indices)
		{
			if (indices is null || indices.Length == 0) return 0;
			var mask = 0;
			foreach (var index in indices)
			{
				if (index < 1 || index > 31) throw new ArgumentException("Days must be in the range 1..31");
				mask = mask | 1 << (index - 1);
			}
			return mask;
		}

		/// <summary>Compares two collections.</summary>
		/// <typeparam name="T">Item type of collections.</typeparam>
		/// <param name="left">The first collection.</param>
		/// <param name="right">The second collection</param>
		/// <returns><c>true</c> if the collections values are equal; <c>false</c> otherwise.</returns>
		private static bool ListsEqual<T>(ICollection<T> left, ICollection<T> right) where T : IComparable
		{
			if (left == null && right == null) return true;
			if (left == null || right == null) return false;
			if (left.Count != right.Count) return false;
			List<T> l1 = new List<T>(left), l2 = new List<T>(right);
			l1.Sort(); l2.Sort();
			for (var i = 0; i < l1.Count; i++)
				if (l1[i].CompareTo(l2[i]) != 0) return false;
			return true;
		}

		/// <summary>
		/// Convert an integer representing a mask to an array where each element contains the index of a bit that is ON in the mask. Bits
		/// are considered to number from 1 to 32.
		/// </summary>
		/// <param name="mask">An integer to be interpreted as a mask.</param>
		/// <returns>An array with an element for each bit of the mask which is ON.</returns>
		private static int[] MaskToIndices(int mask)
		{
			//count bits in mask
			var cnt = 0;
			for (var i = 0; mask >> i > 0; i++)
				cnt = cnt + (1 & (mask >> i));
			//allocate return array with one entry for each bit
			var indices = new int[cnt];
			//fill array with bit indices
			cnt = 0;
			for (var i = 0; mask >> i > 0; i++)
				if ((1 & (mask >> i)) == 1)
					indices[cnt++] = i + 1;
			return indices;
		}

		/// <summary>Reads the subclass XML for V1 streams.</summary>
		/// <param name="reader">The reader.</param>
		private void ReadMyXml([NotNull] System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("ScheduleByMonth");
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				switch (reader.LocalName)
				{
					case "DaysOfMonth":
						reader.Read();
						var days = new List<int>();
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							if (reader.LocalName != "Day") continue;
							var sday = reader.ReadElementContentAsString();
							if (sday.Equals("Last", StringComparison.InvariantCultureIgnoreCase)) continue;
							var day = int.Parse(sday);
							if (day >= 1 && day <= 31)
								days.Add(day);
						}
						DaysOfMonth = days.ToArray();
						reader.ReadEndElement();
						break;

					case "Months":
						reader.Read();
						MonthsOfYear = 0;
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							try
							{
								MonthsOfYear |= (MonthsOfTheYear)Enum.Parse(typeof(MonthsOfTheYear), reader.LocalName);
							}
							catch
							{
								throw new System.Xml.XmlException("Invalid months of the year element.");
							}
							reader.Read();
						}
						reader.ReadEndElement();
						break;

					default:
						reader.Skip();
						break;
				}
			}
			reader.ReadEndElement();
		}

		private void WriteMyXml([NotNull] System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("ScheduleByMonth");

			writer.WriteStartElement("DaysOfMonth");
			foreach (var day in DaysOfMonth)
				writer.WriteElementString("Day", day.ToString());
			writer.WriteEndElement();

			writer.WriteStartElement("Months");
			foreach (MonthsOfTheYear e in Enum.GetValues(typeof(MonthsOfTheYear)))
				if (e != MonthsOfTheYear.AllMonths && (MonthsOfYear & e) == e)
					writer.WriteElementString(e.ToString(), null);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task when the task is registered or updated. Not available on Task Scheduler 1.0. <note>Only
	/// available for Task Scheduler 2.0 on Windows Vista or Windows Server 2003 and later.</note>
	/// </summary>
	/// <remarks>The RegistrationTrigger will fire after the task is registered (saved). It is advisable to put in a delay.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a trigger that will fire the task 5 minutes after its registered
	/// RegistrationTrigger rTrigger = new RegistrationTrigger();
	/// rTrigger.Delay = TimeSpan.FromMinutes(5);
	/// ]]>
	/// </code>
	/// </example>
	[XmlType(IncludeInSchema = false)]
	public sealed class RegistrationTrigger : Trigger, ITriggerDelay, Models.IRegistrationTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="RegistrationTrigger"/>.</summary>
		public RegistrationTrigger() : base(TaskTriggerType.Registration) { }

		internal RegistrationTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan Delay
		{
			get => DelayNullable.GetValueOrDefault();
			set => DelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? DelayNullable
		{
			get => v2Trigger != null ? ((IRegistrationTrigger)v2Trigger).Delay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(Delay));
			set
			{
				if (v2Trigger != null)
					((IRegistrationTrigger)v2Trigger).Delay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(Delay)] = value;
			}
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => Properties.Resources.TriggerRegistration1;
	}

	/// <summary>Defines how often the task is run and how long the repetition pattern is repeated after the task is started.</summary>
	/// <remarks>This can be used directly or by assignment for a <see cref="Trigger"/>.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a time trigger with a repetition
	/// var tt = new TimeTrigger(new DateTime().Now.AddHours(1));
	/// // Set the time in between each repetition of the task after it starts to 30 minutes.
	/// tt.Repetition.Interval = TimeSpan.FromMinutes(30); // Default is TimeSpan.Zero (or never)
	/// // Set the time the task will repeat to 1 day.
	/// tt.Repetition.Duration = TimeSpan.FromDays(1); // Default is TimeSpan.Zero (or never)
	/// // Set the task to end even if running when the duration is over
	/// tt.Repetition.StopAtDurationEnd = true; // Default is false;
	///
	/// // Do the same as above with a constructor
	/// tt = new TimeTrigger(new DateTime().Now.AddHours(1)) { Repetition = new RepetitionPattern(TimeSpan.FromMinutes(30), TimeSpan.FromDays(1), true) };
	/// ]]>
	/// </code>
	/// </example>
	[XmlRoot("Repetition", Namespace = TaskDefinition.tns, IsNullable = true)]
	[TypeConverter(typeof(RepetitionPatternConverter))]
	public sealed class RepetitionPattern : IDisposable, IXmlSerializable, IEquatable<RepetitionPattern>, Models.IRepetitionPattern
	{
		private readonly Trigger pTrigger;
		private readonly IRepetitionPattern v2Pattern;
		private TimeSpan? unboundInterval = null, unboundDuration = null;
		private bool unboundStopAtDurationEnd;

		/// <summary>Initializes a new instance of the <see cref="RepetitionPattern"/> class.</summary>
		/// <param name="interval">
		/// The amount of time between each restart of the task. The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </param>
		/// <param name="duration">
		/// The duration of how long the pattern is repeated. The minimum time allowed is one minute. If <c>TimeSpan.Zero</c> is specified,
		/// the pattern is repeated indefinitely.
		/// </param>
		/// <param name="stopAtDurationEnd">
		/// If set to <c>true</c> the running instance of the task is stopped at the end of repetition pattern duration.
		/// </param>
		public RepetitionPattern(TimeSpan? interval, TimeSpan? duration, bool stopAtDurationEnd = false)
		{
			IntervalNullable = interval;
			DurationNullable = duration;
			StopAtDurationEnd = stopAtDurationEnd;
		}

		internal RepetitionPattern([NotNull] Trigger parent)
		{
			pTrigger = parent;
			if (pTrigger?.v2Trigger != null)
				v2Pattern = pTrigger.v2Trigger.Repetition;
		}

		/// <summary>Gets or sets how long the pattern is repeated.</summary>
		/// <value>
		/// The duration that the pattern is repeated. The minimum time allowed is one minute. If <c>TimeSpan.Zero</c> is specified, the
		/// pattern is repeated indefinitely.
		/// </value>
		/// <remarks>If you specify a repetition duration for a task, you must also specify the repetition interval.</remarks>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Duration
		{
			get => DurationNullable.GetValueOrDefault();
			set => DurationNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets how long the pattern is repeated.</summary>
		/// <value>
		/// The duration that the pattern is repeated. The minimum time allowed is one minute. If <see langword="null"/> is specified, the
		/// pattern is repeated indefinitely.
		/// </value>
		/// <remarks>If you specify a repetition duration for a task, you must also specify the repetition interval.</remarks>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? DurationNullable
		{
			get => v2Pattern != null ? v2Pattern.Duration.Value : (pTrigger != null ? TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesDuration) : unboundDuration);
			set
			{
				if (value.HasValue && value.Value < TimeSpan.FromMinutes(1))
					throw new ArgumentOutOfRangeException(nameof(Duration));
				if (v2Pattern != null)
				{
					v2Pattern.Duration = value;
				}
				else if (pTrigger != null)
				{
					pTrigger.v1TriggerData.MinutesDuration = (uint)value.GetValueOrDefault().TotalMinutes;
					Bind();
				}
				else
					unboundDuration = value;
			}
		}

		/// <summary>Gets or sets the amount of time between each restart of the task.</summary>
		/// <value>
		/// The amount of time between each restart of the task. The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </value>
		/// <remarks>If you specify a repetition duration for a task, you must also specify the repetition interval.</remarks>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Interval
		{
			get => IntervalNullable.GetValueOrDefault();
			set => IntervalNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets the amount of time between each restart of the task.</summary>
		/// <value>
		/// The amount of time between each restart of the task. The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </value>
		/// <remarks>If you specify a repetition duration for a task, you must also specify the repetition interval.</remarks>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? IntervalNullable
		{
			get => v2Pattern != null ? v2Pattern.Interval.Value : (pTrigger != null ? TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesInterval) : unboundInterval);
			set
			{
				if ((v2Pattern != null || pTrigger == null) && value.HasValue && (value.Value < TimeSpan.FromMinutes(1) || value.Value > TimeSpan.FromDays(31)))
					throw new ArgumentOutOfRangeException(nameof(Interval));
				if (v2Pattern != null)
				{
					v2Pattern.Interval = value;
				}
				else if (pTrigger != null)
				{
					if (value.HasValue && value.Value < TimeSpan.FromMinutes(1))
						throw new ArgumentOutOfRangeException(nameof(Interval));
					pTrigger.v1TriggerData.MinutesInterval = (uint)value.GetValueOrDefault().TotalMinutes;
					Bind();
				}
				else
					unboundInterval = value;
			}
		}

		/// <summary>
		/// Gets or sets a Boolean value that indicates if a running instance of the task is stopped at the end of repetition pattern duration.
		/// </summary>
		[DefaultValue(false)]
		public bool StopAtDurationEnd
		{
			get
			{
				if (v2Pattern != null)
					return v2Pattern.StopAtDurationEnd;
				if (pTrigger != null)
					return pTrigger.v1TriggerData.rgFlags.IsFlagSet(TaskTriggerFlags.TASK_TRIGGER_FLAG_KILL_AT_DURATION_END);
				return unboundStopAtDurationEnd;
			}
			set
			{
				if (v2Pattern != null)
					v2Pattern.StopAtDurationEnd = value;
				else if (pTrigger != null)
				{
					pTrigger.v1TriggerData.rgFlags = pTrigger.v1TriggerData.rgFlags.SetFlags(TaskTriggerFlags.TASK_TRIGGER_FLAG_KILL_AT_DURATION_END, value);
					Bind();
				}
				else
					unboundStopAtDurationEnd = value;
			}
		}

		/// <summary>Releases all resources used by this class.</summary>
		public void Dispose()
		{
			if (v2Pattern != null) Marshal.ReleaseComObject(v2Pattern);
		}

		/// <summary>Determines whether the specified <see cref="System.Object"/>, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
		// ReSharper disable once BaseObjectEqualsIsObjectEquals
		public override bool Equals(object obj) => obj is RepetitionPattern pattern ? Equals(pattern) : base.Equals(obj);

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public bool Equals(RepetitionPattern other) => other != null && Duration == other.Duration && Interval == other.Interval && StopAtDurationEnd == other.StopAtDurationEnd;

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode() => new { A = Duration, B = Interval, C = StopAtDurationEnd }.GetHashCode();

		/// <summary>Determines whether any properties for this <see cref="RepetitionPattern"/> have been set.</summary>
		/// <returns><c>true</c> if properties have been set; otherwise, <c>false</c>.</returns>
		public bool IsSet()
		{
			if (v2Pattern != null)
				return v2Pattern.StopAtDurationEnd || !string.IsNullOrEmpty(v2Pattern.Duration.StringValue) || !string.IsNullOrEmpty(v2Pattern.Interval.StringValue);
			if (pTrigger != null)
				return pTrigger.v1TriggerData.rgFlags.IsFlagSet(TaskTriggerFlags.TASK_TRIGGER_FLAG_KILL_AT_DURATION_END) || pTrigger.v1TriggerData.MinutesDuration > 0 || pTrigger.v1TriggerData.MinutesInterval > 0;
			return false;
		}

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement(XmlSerializationHelper.GetElementName(this), TaskDefinition.tns);
				XmlSerializationHelper.ReadObjectProperties(reader, this, ReadXmlConverter);
				reader.ReadEndElement();
			}
			else
				reader.Skip();
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) => XmlSerializationHelper.WriteObjectProperties(writer, this);

		internal void Bind()
		{
			if (pTrigger.v1Trigger != null)
				pTrigger.SetV1TriggerData();
			else if (pTrigger.v2Trigger != null)
			{
				v2Pattern.Interval = pTrigger.v1TriggerData.MinutesInterval < 1 ? (TimeSpan?)null : TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesInterval);
				v2Pattern.Duration = pTrigger.v1TriggerData.MinutesDuration < 1 ? (TimeSpan?)null : TimeSpan.FromMinutes(pTrigger.v1TriggerData.MinutesDuration);
				v2Pattern.StopAtDurationEnd = pTrigger.v1TriggerData.rgFlags.IsFlagSet(TaskTriggerFlags.TASK_TRIGGER_FLAG_KILL_AT_DURATION_END);
			}
		}

		internal void Set([NotNull] RepetitionPattern value)
		{
			Duration = value.Duration;
			Interval = value.Interval;
			StopAtDurationEnd = value.StopAtDurationEnd;
		}

		private bool ReadXmlConverter(System.Reflection.PropertyInfo pi, object obj, ref object value)
		{
			if (pi.Name != "Interval" || !(value is TimeSpan span) || span.Equals(TimeSpan.Zero) || Duration > span)
				return false;
			Duration = span.Add(TimeSpan.FromMinutes(1));
			return true;
		}
	}

	/// <summary>
	/// Triggers tasks for console connect or disconnect, remote connect or disconnect, or workstation lock or unlock notifications.
	/// <note>Only available for Task Scheduler 2.0 on Windows Vista or Windows Server 2003 and later.</note>
	/// </summary>
	/// <remarks>
	/// The SessionStateChangeTrigger will fire after six different system events: connecting or disconnecting locally or remotely, or
	/// locking or unlocking the session.
	/// </remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleConnect, UserId = "joe" };
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.ConsoleDisconnect };
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteConnect };
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.RemoteDisconnect };
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionLock, UserId = "joe" };
	/// new SessionStateChangeTrigger { StateChange = TaskSessionStateChangeType.SessionUnlock };
	/// ]]>
	/// </code>
	/// </example>
	[XmlType(IncludeInSchema = false)]
	public sealed class SessionStateChangeTrigger : Trigger, ITriggerDelay, ITriggerUserId, Models.ISessionStateChangeTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="SessionStateChangeTrigger"/>.</summary>
		public SessionStateChangeTrigger() : base(TaskTriggerType.SessionStateChange) { }

		/// <summary>Initializes a new instance of the <see cref="SessionStateChangeTrigger"/> class.</summary>
		/// <param name="stateChange">The state change.</param>
		/// <param name="userId">The user identifier.</param>
		public SessionStateChangeTrigger(TaskSessionStateChangeType stateChange, string userId = null) : this() { StateChange = stateChange; UserId = userId; }

		internal SessionStateChangeTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		public TimeSpan Delay
		{
			get => DelayNullable.GetValueOrDefault();
			set => DelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time between when the system is booted and when the task is started.</summary>
		[DefaultValue(typeof(TimeSpan?), null)]
		public TimeSpan? DelayNullable
		{
			get => v2Trigger != null ? ((ISessionStateChangeTrigger)v2Trigger).Delay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(Delay));
			set
			{
				if (v2Trigger != null)
					((ISessionStateChangeTrigger)v2Trigger).Delay = value;
				else
					unboundValues[nameof(Delay)] = value;
			}
		}

		/// <summary>Gets or sets the kind of Terminal Server session change that would trigger a task launch.</summary>
		[DefaultValue(1)]
		public TaskSessionStateChangeType StateChange
		{
			get => (TaskSessionStateChangeType?)(((ISessionStateChangeTrigger)v2Trigger)?.StateChange) ?? GetUnboundValueOrDefault(nameof(StateChange), TaskSessionStateChangeType.ConsoleConnect);
			set
			{
				if (v2Trigger != null)
					((ISessionStateChangeTrigger)v2Trigger).StateChange = (TASK_SESSION_STATE_CHANGE_TYPE)value;
				else
					unboundValues[nameof(StateChange)] = value;
			}
		}

		/// <summary>
		/// Gets or sets the user for the Terminal Server session. When a session state change is detected for this user, a task is started.
		/// </summary>
		[DefaultValue(null)]
		public string UserId
		{
			get => v2Trigger != null ? ((ISessionStateChangeTrigger)v2Trigger).UserId : GetUnboundValueOrDefault<string>(nameof(UserId));
			set
			{
				if (v2Trigger != null)
					((ISessionStateChangeTrigger)v2Trigger).UserId = value;
				else
					unboundValues[nameof(UserId)] = value;
			}
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is SessionStateChangeTrigger st && !StateChangeIsSet())
				StateChange = st.StateChange;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is SessionStateChangeTrigger st && base.Equals(st) && StateChange == st.StateChange;

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			var str = Properties.Resources.ResourceManager.GetString("TriggerSession" + StateChange.ToString());
			var user = string.IsNullOrEmpty(UserId) ? Properties.Resources.TriggerAnyUser : UserId;
			if (StateChange != TaskSessionStateChangeType.SessionLock && StateChange != TaskSessionStateChangeType.SessionUnlock)
				user = string.Format(Properties.Resources.TriggerSessionUserSession, user);
			return string.Format(str, user);
		}

		/// <summary>Returns a value indicating if the StateChange property has been set.</summary>
		/// <returns>StateChange property has been set.</returns>
		private bool StateChangeIsSet() => v2Trigger != null || (unboundValues?.ContainsKey("StateChange") ?? false);
	}

	/// <summary>Represents a trigger that starts a task at a specific date and time.</summary>
	/// <remarks>A TimeTrigger runs at a specified date and time.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a trigger that runs the last minute of this year
	/// TimeTrigger tTrigger = new TimeTrigger();
	/// tTrigger.StartBoundary = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 0);
	/// ]]>
	/// </code>
	/// </example>
	public sealed class TimeTrigger : Trigger, ITriggerDelay, ICalendarTrigger, Models.ITimeTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="TimeTrigger"/>.</summary>
		public TimeTrigger() : base(TaskTriggerType.Time) { }

		/// <summary>Creates an unbound instance of a <see cref="TimeTrigger"/> and assigns the execution time.</summary>
		/// <param name="startBoundary">Date and time for the trigger to fire.</param>
		public TimeTrigger(DateTime startBoundary) : base(TaskTriggerType.Time) => StartBoundary = startBoundary;

		internal TimeTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_ONCE)
		{
		}

		internal TimeTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RandomDelay
		{
			get => RandomDelayNullable.GetValueOrDefault();
			set => RandomDelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? RandomDelayNullable
		{
			get => v2Trigger != null ? ((ITimeTrigger)v2Trigger).RandomDelay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(RandomDelay));
			set
			{
				if (v2Trigger != null)
					((ITimeTrigger)v2Trigger).RandomDelay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RandomDelay)] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan ITriggerDelay.Delay
		{
			get => RandomDelay;
			set => RandomDelay = value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? ITriggerDelay.DelayNullable
		{
			get => RandomDelayNullable;
			set => RandomDelayNullable = value;
		}

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString() => string.Format(Properties.Resources.TriggerTime1, AdjustToLocal(StartBoundary));
	}

	/// <summary>
	/// Abstract base class which provides the common properties that are inherited by all trigger classes. A trigger can be created using
	/// the <see cref="TriggerCollection.Add{TTrigger}"/> or the <see cref="TriggerCollection.AddNew"/> method.
	/// </summary>
	public abstract partial class Trigger : IDisposable, ICloneable, IEquatable<Trigger>, IComparable, IComparable<Trigger>, Models.ITrigger
	{
		internal const string V2BoundaryDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'FFFK";

		internal static readonly CultureInfo DefaultDateCulture = CultureInfo.CreateSpecificCulture("en-US");
		internal ITaskTrigger v1Trigger;
		internal TASK_TRIGGER v1TriggerData;
		internal ITrigger v2Trigger;
		/// <summary>Property values which have not yet been applied to the underlying COM object.</summary>
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();
		private static bool? foundTimeSpan2;
		private static Type timeSpan2Type;
		private readonly TASK_TRIGGER_TYPE2 ttype;
		private RepetitionPattern repititionPattern;

		internal Trigger([NotNull] ITaskTrigger trigger, TASK_TRIGGER_TYPE type)
		{
			v1Trigger = trigger;
			v1TriggerData = trigger.GetTrigger();
			v1TriggerData.TriggerType = type;
			ttype = ConvertFromV1TriggerType(type);
		}

		internal Trigger([NotNull] ITrigger iTrigger)
		{
			v2Trigger = iTrigger;
			ttype = iTrigger.Type;
			if (v2Trigger.StartBoundary.Value == null && this is ICalendarTrigger)
				StartBoundary = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
		}

		internal Trigger(TaskTriggerType triggerType)
		{
			ttype = (TASK_TRIGGER_TYPE2)triggerType;

			v1TriggerData.cbTriggerSize = (ushort)Marshal.SizeOf(typeof(TASK_TRIGGER));
			if (triggerType != TaskTriggerType.Registration && triggerType != TaskTriggerType.Event && triggerType != TaskTriggerType.SessionStateChange)
				v1TriggerData.TriggerType = ConvertToV1TriggerType(triggerType);

			if (this is ICalendarTrigger)
				StartBoundary = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the trigger is enabled.</summary>
		public bool Enabled
		{
			get => v2Trigger?.Enabled ?? GetUnboundValueOrDefault(nameof(Enabled), !v1TriggerData.rgFlags.IsFlagSet(TaskTriggerFlags.TASK_TRIGGER_FLAG_DISABLED));
			set
			{
				if (v2Trigger != null)
					v2Trigger.Enabled = value;
				else
				{
					v1TriggerData.rgFlags = v1TriggerData.rgFlags.SetFlags(TaskTriggerFlags.TASK_TRIGGER_FLAG_DISABLED, !value);
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(Enabled)] = value;
				}
			}
		}

		/// <summary>Gets or sets the date and time when the trigger is deactivated. The trigger cannot start the task after it is deactivated.</summary>
		/// <remarks>
		/// <para>
		/// Version 1 (1.1 on all systems prior to Vista) of the native library only allows for the Day, Month and Year values of the
		/// <see cref="DateTime"/> structure.
		/// </para>
		/// <para>
		/// Version 2 (1.2 or higher) of the native library only allows for both date and time and all <see cref="DateTime.Kind"/> values.
		/// However, the user interface and <see cref="Trigger.ToString()"/> methods will always show the time translated to local time. The
		/// library makes every attempt to maintain the Kind value. When using the UI elements provided in the TaskSchedulerEditor library,
		/// the "Synchronize across time zones" checkbox will be checked if the Kind is Local or Utc. If the Kind is Unspecified and the user
		/// selects the checkbox, the Kind will be changed to Utc and the time adjusted from the value displayed as the local time.
		/// </para>
		/// </remarks>
		[DefaultValue(typeof(DateTime), "9999-12-31T23:59:59.9999999")]
		public DateTime EndBoundary
		{
			get => EndBoundaryNullable.GetValueOrDefault(DateTime.MaxValue);
			set => EndBoundaryNullable = value == DateTime.MaxValue ? (DateTime?)null : value;
		}

		/// <summary>Gets or sets the date and time when the trigger is deactivated. The trigger cannot start the task after it is deactivated.</summary>
		/// <remarks>
		/// <para>
		/// Version 1 (1.1 on all systems prior to Vista) of the native library only allows for the Day, Month and Year values of the
		/// <see cref="DateTime"/> structure.
		/// </para>
		/// <para>
		/// Version 2 (1.2 or higher) of the native library only allows for both date and time and all <see cref="DateTime.Kind"/> values.
		/// However, the user interface and <see cref="Trigger.ToString()"/> methods will always show the time translated to local time. The
		/// library makes every attempt to maintain the Kind value. When using the UI elements provided in the TaskSchedulerEditor library,
		/// the "Synchronize across time zones" checkbox will be checked if the Kind is Local or Utc. If the Kind is Unspecified and the user
		/// selects the checkbox, the Kind will be changed to Utc and the time adjusted from the value displayed as the local time.
		/// </para>
		/// </remarks>
		[DefaultValue(typeof(DateTime?), null)]
		[XmlIgnore]
		public DateTime? EndBoundaryNullable
		{
			get => v2Trigger != null ? v2Trigger.EndBoundary.Value : GetUnboundValueOrDefault(nameof(EndBoundary), v1TriggerData.EndDate);
			set
			{
				if (v2Trigger != null)
				{
					if (value.HasValue && value.Value <= StartBoundary)
						throw new ArgumentException(Properties.Resources.Error_TriggerEndBeforeStart);
					v2Trigger.EndBoundary = value;
				}
				else
				{
					v1TriggerData.EndDate = value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(EndBoundary)] = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum amount of time that the task launched by this trigger is allowed to run. Not available with Task
		/// Scheduler 1.0.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan ExecutionTimeLimit
		{
			get => ExecutionTimeLimitNullable.GetValueOrDefault();
			set => ExecutionTimeLimitNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>
		/// Gets or sets the maximum amount of time that the task launched by this trigger is allowed to run. Not available with Task
		/// Scheduler 1.0.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? ExecutionTimeLimitNullable
		{
			get => v2Trigger != null ? v2Trigger.ExecutionTimeLimit.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(ExecutionTimeLimit));
			set
			{
				if (v2Trigger != null)
					v2Trigger.ExecutionTimeLimit = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(ExecutionTimeLimit)] = value;
			}
		}

		/// <summary>Gets or sets the identifier for the trigger. Cannot set with Task Scheduler 1.0.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(null)]
		[XmlIgnore]
		public string Id
		{
			get => v2Trigger != null ? v2Trigger.Id : GetUnboundValueOrDefault<string>(nameof(Id));
			set
			{
				if (v2Trigger != null)
					v2Trigger.Id = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(Id)] = value;
			}
		}

		/// <summary>
		/// Gets a <see cref="RepetitionPattern"/> instance that indicates how often the task is run and how long the repetition pattern is
		/// repeated after the task is started.
		/// </summary>
		public RepetitionPattern Repetition
		{
			get => repititionPattern ?? (repititionPattern = new RepetitionPattern(this));
			set => Repetition.Set(value);
		}

		/// <summary>Gets or sets the date and time when the trigger is activated.</summary>
		/// <remarks>
		/// <para>
		/// Version 1 (1.1 on all systems prior to Vista) of the native library only allows for <see cref="DateTime"/> values where the
		/// <see cref="DateTime.Kind"/> is unspecified. If the DateTime value Kind is <see cref="DateTimeKind.Local"/> then it will be used as is.
		/// If the DateTime value Kind is <see cref="DateTimeKind.Utc"/> then it will be converted to the local time and then used.
		/// </para>
		/// <para>
		/// Version 2 (1.2 or higher) of the native library only allows for all <see cref="DateTime.Kind"/> values. However, the user
		/// interface and <see cref="Trigger.ToString()"/> methods will always show the time translated to local time. The library makes
		/// every attempt to maintain the Kind value. When using the UI elements provided in the TaskSchedulerEditor library, the
		/// "Synchronize across time zones" checkbox will be checked if the Kind is Local or Utc. If the Kind is Unspecified and the user
		/// selects the checkbox, the Kind will be changed to Utc and the time adjusted from the value displayed as the local time.
		/// </para>
		/// <para>
		/// Under Version 2, when converting the string used in the native library for this value (ITrigger.Startboundary) this library will
		/// behave as follows:
		/// <list type="bullet">
		/// <item><description>YYYY-MM-DDTHH:MM:SS format uses DateTimeKind.Unspecified and the time specified.</description></item>
		/// <item><description>YYYY-MM-DDTHH:MM:SSZ format uses DateTimeKind.Utc and the time specified as the GMT time.</description></item>
		/// <item><description>YYYY-MM-DDTHH:MM:SS±HH:MM format uses DateTimeKind.Local and the time specified in that time zone.</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		public DateTime StartBoundary
		{
			get => StartBoundaryNullable.GetValueOrDefault(DateTime.MinValue);
			set => StartBoundaryNullable = value == DateTime.MinValue ? (DateTime?)null : value;
		}

		/// <summary>Gets or sets the date and time when the trigger is activated.</summary>
		/// <remarks>
		/// <para>
		/// Version 1 (1.1 on all systems prior to Vista) of the native library only allows for <see cref="DateTime"/> values where the
		/// <see cref="DateTime.Kind"/> is unspecified. If the DateTime value Kind is <see cref="DateTimeKind.Local"/> then it will be used as is.
		/// If the DateTime value Kind is <see cref="DateTimeKind.Utc"/> then it will be converted to the local time and then used.
		/// </para>
		/// <para>
		/// Version 2 (1.2 or higher) of the native library only allows for all <see cref="DateTime.Kind"/> values. However, the user
		/// interface and <see cref="Trigger.ToString()"/> methods will always show the time translated to local time. The library makes
		/// every attempt to maintain the Kind value. When using the UI elements provided in the TaskSchedulerEditor library, the
		/// "Synchronize across time zones" checkbox will be checked if the Kind is Local or Utc. If the Kind is Unspecified and the user
		/// selects the checkbox, the Kind will be changed to Utc and the time adjusted from the value displayed as the local time.
		/// </para>
		/// <para>
		/// Under Version 2, when converting the string used in the native library for this value (ITrigger.Startboundary) this library will
		/// behave as follows:
		/// <list type="bullet">
		/// <item><description>YYYY-MM-DDTHH:MM:SS format uses DateTimeKind.Unspecified and the time specified.</description></item>
		/// <item><description>YYYY-MM-DDTHH:MM:SSZ format uses DateTimeKind.Utc and the time specified as the GMT time.</description></item>
		/// <item><description>YYYY-MM-DDTHH:MM:SS±HH:MM format uses DateTimeKind.Local and the time specified in that time zone.</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		public DateTime? StartBoundaryNullable
		{
			get
			{
				if (v2Trigger == null) return GetUnboundValueOrDefault<DateTime?>(nameof(StartBoundary), v1TriggerData.BeginDate);
				var sb = v2Trigger.StartBoundary;
				if (string.IsNullOrEmpty(sb.StringValue))
					return null;
				var ret = sb.Value.Value;
				return sb.StringValue.EndsWith("Z") ? ret.ToUniversalTime() : ret;
			}
			set
			{
				if (v2Trigger != null)
				{
					if (value.HasValue && value.Value > EndBoundary)
						throw new ArgumentException(Properties.Resources.Error_TriggerEndBeforeStart);
					v2Trigger.StartBoundary = value;
				}
				else
				{
					v1TriggerData.BeginDate = value.GetValueOrDefault(DateTime.MinValue);
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(StartBoundary)] = value;
				}
			}
		}

		/// <summary>Gets the type of the trigger.</summary>
		/// <value>The <see cref="TaskTriggerType"/> of the trigger.</value>
		[XmlIgnore]
		public TaskTriggerType TriggerType => (TaskTriggerType)ttype;

		/// <summary>
		/// Gets a <see cref="Models.IRepetitionPattern"/> instance that indicates how often the task is run and how long the repetition
		/// pattern is repeated after the task is started.
		/// </summary>
		Models.IRepetitionPattern Models.ITrigger.Repetition => this.Repetition;

		/// <summary>Creates the specified trigger.</summary>
		/// <param name="triggerType">Type of the trigger to instantiate.</param>
		/// <returns><see cref="Trigger"/> of specified type.</returns>
		public static Trigger CreateTrigger(TaskTriggerType triggerType)
		{
			switch (triggerType)
			{
				case TaskTriggerType.Boot:
					return new BootTrigger();

				case TaskTriggerType.Daily:
					return new DailyTrigger();

				case TaskTriggerType.Event:
					return new EventTrigger();

				case TaskTriggerType.Idle:
					return new IdleTrigger();

				case TaskTriggerType.Logon:
					return new LogonTrigger();

				case TaskTriggerType.Monthly:
					return new MonthlyTrigger();

				case TaskTriggerType.MonthlyDOW:
					return new MonthlyDOWTrigger();

				case TaskTriggerType.Registration:
					return new RegistrationTrigger();

				case TaskTriggerType.SessionStateChange:
					return new SessionStateChangeTrigger();

				case TaskTriggerType.Time:
					return new TimeTrigger();

				case TaskTriggerType.Weekly:
					return new WeeklyTrigger();

				case TaskTriggerType.Custom:
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(triggerType), triggerType, null);
			}
			return null;
		}

		/// <summary>Creates a new <see cref="Trigger"/> that is an unbound copy of this instance.</summary>
		/// <returns>A new <see cref="Trigger"/> that is an unbound copy of this instance.</returns>
		public virtual object Clone()
		{
			var ret = CreateTrigger(TriggerType);
			ret.CopyProperties(this);
			return ret;
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current
		/// instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>A value that indicates the relative order of the objects being compared.</returns>
		public int CompareTo(Trigger other) => string.Compare(Id, other?.Id, StringComparison.InvariantCulture);

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public virtual void CopyProperties(Trigger sourceTrigger)
		{
			if (sourceTrigger == null)
				return;
			Enabled = sourceTrigger.Enabled;
			EndBoundaryNullable = sourceTrigger.EndBoundaryNullable;
			try { ExecutionTimeLimitNullable = sourceTrigger.ExecutionTimeLimitNullable; }
			catch { /* ignored */ }
			Id = sourceTrigger.Id;
			Repetition.DurationNullable = sourceTrigger.Repetition.DurationNullable;
			Repetition.IntervalNullable = sourceTrigger.Repetition.IntervalNullable;
			Repetition.StopAtDurationEnd = sourceTrigger.Repetition.StopAtDurationEnd;
			StartBoundaryNullable = sourceTrigger.StartBoundaryNullable;
			if (sourceTrigger is ITriggerDelay delay && this is ITriggerDelay)
				try { ((ITriggerDelay)this).DelayNullable = delay.DelayNullable; }
				catch { /* ignored */ }
			if (sourceTrigger is ITriggerUserId id && this is ITriggerUserId)
				try { ((ITriggerUserId)this).UserId = id.UserId; }
				catch { /* ignored */ }
		}

		/// <summary>Releases all resources used by this class.</summary>
		public virtual void Dispose()
		{
			if (v2Trigger != null)
				Marshal.ReleaseComObject(v2Trigger);
			if (v1Trigger != null)
				Marshal.ReleaseComObject(v1Trigger);
		}

		/// <summary>Determines whether the specified <see cref="System.Object"/>, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
		// ReSharper disable once BaseObjectEqualsIsObjectEquals
		public override bool Equals(object obj) => obj is Trigger trigger ? Equals(trigger) : base.Equals(obj);

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public virtual bool Equals(Trigger other)
		{
			if (other == null) return false;
			var ret = TriggerType == other.TriggerType && Enabled == other.Enabled && EndBoundaryNullable == other.EndBoundaryNullable &&
				ExecutionTimeLimitNullable == other.ExecutionTimeLimitNullable && Id == other.Id && Repetition.Equals(other.Repetition) &&
				StartBoundaryNullable == other.StartBoundaryNullable;
			if (other is ITriggerDelay delay && this is ITriggerDelay)
				try { ret = ret && ((ITriggerDelay)this).DelayNullable == delay.DelayNullable; }
				catch { /* ignored */ }
			if (other is ITriggerUserId id && this is ITriggerUserId)
				try { ret = ret && ((ITriggerUserId)this).UserId == id.UserId; }
				catch { /* ignored */ }
			return ret;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode() => new
		{
			A = TriggerType,
			B = Enabled,
			C = EndBoundaryNullable,
			D = ExecutionTimeLimitNullable,
			E = Id,
			F = Repetition,
			G = StartBoundaryNullable,
			H = (this as ITriggerDelay)?.DelayNullable,
			I = (this as ITriggerUserId)?.UserId
		}.GetHashCode();

		/// <summary>Sets the repetition.</summary>
		/// <param name="interval">
		/// The amount of time between each restart of the task. The maximum time allowed is 31 days, and the minimum time allowed is 1 minute.
		/// </param>
		/// <param name="duration">
		/// The duration of how long the pattern is repeated. The minimum time allowed is one minute. If <c>TimeSpan.Zero</c> is specified,
		/// the pattern is repeated indefinitely.
		/// </param>
		/// <param name="stopAtDurationEnd">
		/// if set to <c>true</c> the running instance of the task is stopped at the end of repetition pattern duration.
		/// </param>
		[Obsolete("Set the Repetition property directly with a new instance of RepetitionPattern.", false)]
		public void SetRepetition(TimeSpan interval, TimeSpan duration, bool stopAtDurationEnd = true)
		{
			Repetition.Duration = duration;
			Repetition.Interval = interval;
			Repetition.StopAtDurationEnd = stopAtDurationEnd;
		}

		/// <summary>Returns a string representing this trigger.</summary>
		/// <returns>String value of trigger.</returns>
		public override string ToString() => v1Trigger != null ? v1Trigger.GetTriggerString() : V2GetTriggerString() + V2BaseTriggerString();

		/// <summary>Returns a <see cref="System.String"/> that represents this trigger in a specific language.</summary>
		/// <param name="culture">The language of the resulting string.</param>
		/// <returns>String value of trigger.</returns>
		public virtual string ToString([NotNull] CultureInfo culture)
		{
			using (new CultureSwitcher(culture))
				return ToString();
		}

		int IComparable.CompareTo(object obj) => CompareTo(obj as Trigger);

		internal static DateTime AdjustToLocal(DateTime dt) => dt.Kind == DateTimeKind.Utc ? dt.ToLocalTime() : dt;

		internal static TASK_TRIGGER_TYPE ConvertToV1TriggerType(TaskTriggerType type)
		{
			if (type == TaskTriggerType.Registration || type == TaskTriggerType.Event || type == TaskTriggerType.SessionStateChange)
				throw new NotV1SupportedException();
			var tv1 = (int)type - 1;
			if (tv1 >= 7) tv1--;
			return (TASK_TRIGGER_TYPE)tv1;
		}

		internal static Trigger CreateTrigger([NotNull] ITaskTrigger trigger) => CreateTrigger(trigger, trigger.GetTrigger().TriggerType);

		internal static Trigger CreateTrigger([NotNull] ITaskTrigger trigger, TASK_TRIGGER_TYPE triggerType)
		{
			Trigger t;
			switch (triggerType)
			{
				case TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_ONCE:
					t = new TimeTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_DAILY:
					t = new DailyTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_WEEKLY:
					t = new WeeklyTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_MONTHLYDATE:
					t = new MonthlyTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_MONTHLYDOW:
					t = new MonthlyDOWTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_ON_IDLE:
					t = new IdleTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_AT_SYSTEMSTART:
					t = new BootTrigger(trigger);
					break;

				case TASK_TRIGGER_TYPE.TASK_EVENT_TRIGGER_AT_LOGON:
					t = new LogonTrigger(trigger);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(triggerType), triggerType, null);
			}
			return t;
		}

		internal static Trigger CreateTrigger([NotNull] ITrigger iTrigger, ITaskDefinition iDef = null)
		{
			switch (iTrigger.Type)
			{
				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_BOOT:
					return new BootTrigger((IBootTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY:
					return new DailyTrigger((IDailyTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_EVENT:
					return new EventTrigger((IEventTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_IDLE:
					return new IdleTrigger((IIdleTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON:
					return new LogonTrigger((ILogonTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY:
					return new MonthlyTrigger((IMonthlyTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLYDOW:
					return new MonthlyDOWTrigger((IMonthlyDOWTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_REGISTRATION:
					return new RegistrationTrigger((IRegistrationTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_SESSION_STATE_CHANGE:
					return new SessionStateChangeTrigger((ISessionStateChangeTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME:
					return new TimeTrigger((ITimeTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY:
					return new WeeklyTrigger((IWeeklyTrigger)iTrigger);

				case TASK_TRIGGER_TYPE2.TASK_TRIGGER_CUSTOM_TRIGGER_01:
					var ct = new CustomTrigger(iTrigger);
					if (iDef != null)
						try { ct.UpdateFromXml(iDef.XmlText); } catch { /* ignored */ }
					return ct;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>Gets the best time span string.</summary>
		/// <param name="span">The <see cref="TimeSpan"/> to display.</param>
		/// <returns>Either the full string representation created by TimeSpan2 or the default TimeSpan representation.</returns>
		internal static string GetBestTimeSpanString(TimeSpan span)
		{
			// See if the TimeSpan2 assembly is accessible
			if (!foundTimeSpan2.HasValue)
			{
				try
				{
					foundTimeSpan2 = false;
					timeSpan2Type = ReflectionExtensions.LoadType("System.TimeSpan2", "TimeSpan2.dll");
					if (timeSpan2Type != null)
						foundTimeSpan2 = true;
				}
				catch { /* ignored */ }
			}

			// If the TimeSpan2 assembly is available, try to call the ToString("f") method and return the result.
			if (foundTimeSpan2 == true && timeSpan2Type != null)
			{
				try
				{
					return timeSpan2Type.InvokeMethod<string>(new object[] { span }, "ToString", "f");
				}
				catch { /* ignored */ }
			}

			return span.ToString();
		}

		internal virtual void Bind([NotNull] ITask iTask)
		{
			if (v1Trigger == null)
			{
				v1Trigger = iTask.CreateTrigger(out var _);
			}
			SetV1TriggerData();
		}

		internal virtual void Bind([NotNull] ITaskDefinition iTaskDef)
		{
			var iTriggers = iTaskDef.Triggers;
			v2Trigger = iTriggers.Create(ttype);
			Marshal.ReleaseComObject(iTriggers);
			if ((unboundValues.TryGetValue("StartBoundary", out var dt) ? (DateTime?)dt : StartBoundaryNullable).GetValueOrDefault(DateTime.MaxValue) > (unboundValues.TryGetValue("EndBoundary", out dt) ? (DateTime?)dt : EndBoundaryNullable).GetValueOrDefault(DateTime.MaxValue))
				throw new ArgumentException(Properties.Resources.Error_TriggerEndBeforeStart);
			foreach (var key in unboundValues.Keys)
			{
				try
				{
					var o = unboundValues[key];
					CheckBindValue(key, ref o);
					v2Trigger.GetType().InvokeMember(key, System.Reflection.BindingFlags.SetProperty, null, v2Trigger, new[] { o });
				}
				catch (System.Reflection.TargetInvocationException tie) when (tie.InnerException != null) { throw tie.InnerException; }
				catch { /* ignored */ }
			}
			unboundValues.Clear();
			unboundValues = null;

			repititionPattern = new RepetitionPattern(this);
			repititionPattern.Bind();
		}

		/// <summary>Assigns the unbound TriggerData structure to the V1 trigger instance.</summary>
		internal void SetV1TriggerData()
		{
			if (v1TriggerData.MinutesInterval != 0 && v1TriggerData.MinutesInterval >= v1TriggerData.MinutesDuration)
				throw new ArgumentException("Trigger.Repetition.Interval must be less than Trigger.Repetition.Duration under Task Scheduler 1.0.");
			if (v1TriggerData.EndDate <= v1TriggerData.BeginDate)
				throw new ArgumentException(Properties.Resources.Error_TriggerEndBeforeStart);
			if (v1TriggerData.BeginDate == DateTime.MinValue)
				v1TriggerData.BeginDate = DateTime.Now;
			v1Trigger?.SetTrigger(v1TriggerData);
			System.Diagnostics.Debug.WriteLine(v1TriggerData);
		}

		/// <summary>Checks the bind value for any conversion.</summary>
		/// <param name="key">The key (property) name.</param>
		/// <param name="o">The value.</param>
		protected virtual void CheckBindValue(string key, ref object o)
		{
			if (o is TimeSpan ts)
				o = Task.TimeSpanToString(ts);
			if (o is DateTime dt)
				o = dt.ToString(V2BoundaryDateFormat, DefaultDateCulture);
		}

		/// <summary>Gets the unbound value or a default.</summary>
		/// <typeparam name="T">Return type.</typeparam>
		/// <param name="prop">The property name.</param>
		/// <param name="def">The default value if not found in unbound value list.</param>
		/// <returns>The unbound value, if set, or the default value.</returns>
		protected T GetUnboundValueOrDefault<T>(string prop, T def = default) => unboundValues.TryGetValue(prop, out var val) ? (T)val : def;

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected virtual string V2GetTriggerString() => string.Empty;

		private static TASK_TRIGGER_TYPE2 ConvertFromV1TriggerType(TASK_TRIGGER_TYPE v1Type)
		{
			var tv2 = (int)v1Type + 1;
			if (tv2 > 6) tv2++;
			return (TASK_TRIGGER_TYPE2)tv2;
		}

		private string V2BaseTriggerString()
		{
			var ret = new StringBuilder();
			if (Repetition.Interval != TimeSpan.Zero)
			{
				var sduration = Repetition.Duration == TimeSpan.Zero ? Properties.Resources.TriggerDuration0 : string.Format(Properties.Resources.TriggerDurationNot0, GetBestTimeSpanString(Repetition.Duration));
				ret.AppendFormat(Properties.Resources.TriggerRepetition, GetBestTimeSpanString(Repetition.Interval), sduration);
			}
			if (EndBoundary != DateTime.MaxValue)
				ret.AppendFormat(Properties.Resources.TriggerEndBoundary, AdjustToLocal(EndBoundary));
			if (ret.Length > 0)
				ret.Insert(0, Properties.Resources.HyphenSeparator);
			return ret.ToString();
		}
	}

	/// <summary>
	/// Represents a trigger that starts a task based on a weekly schedule. For example, the task starts at 8:00 A.M. on a specific day of
	/// the week every week or every other week.
	/// </summary>
	/// <remarks>A WeeklyTrigger runs at a specified time on specified days of the week every week or interval of weeks.</remarks>
	/// <example>
	/// <code lang="cs">
	/// <![CDATA[
	/// // Create a trigger that runs on Monday every third week just after midnight.
	/// WeeklyTrigger wTrigger = new WeeklyTrigger();
	/// wTrigger.StartBoundary = DateTime.Today + TimeSpan.FromSeconds(15);
	/// wTrigger.DaysOfWeek = DaysOfTheWeek.Monday;
	/// wTrigger.WeeksInterval = 3;
	/// ]]>
	/// </code>
	/// </example>
	[XmlRoot("CalendarTrigger", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class WeeklyTrigger : Trigger, ICalendarTrigger, ITriggerDelay, IXmlSerializable, Models.IWeeklyTrigger
	{
		/// <summary>Creates an unbound instance of a <see cref="WeeklyTrigger"/>.</summary>
		/// <param name="daysOfWeek">The days of the week.</param>
		/// <param name="weeksInterval">The interval between the weeks in the schedule.</param>
		public WeeklyTrigger(DaysOfTheWeek daysOfWeek = DaysOfTheWeek.Sunday, short weeksInterval = 1) : base(TaskTriggerType.Weekly)
		{
			DaysOfWeek = daysOfWeek;
			WeeksInterval = weeksInterval;
		}

		internal WeeklyTrigger([NotNull] ITaskTrigger iTrigger) : base(iTrigger, TASK_TRIGGER_TYPE.TASK_TIME_TRIGGER_WEEKLY)
		{
			if (v1TriggerData.Type.Weekly.rgfDaysOfTheWeek == 0)
				v1TriggerData.Type.Weekly.rgfDaysOfTheWeek = TaskDaysOfTheWeek.TASK_SUNDAY;
			if (v1TriggerData.Type.Weekly.WeeksInterval == 0)
				v1TriggerData.Type.Weekly.WeeksInterval = 1;
		}

		internal WeeklyTrigger([NotNull] ITrigger iTrigger) : base(iTrigger)
		{
		}

		/// <summary>Gets or sets the days of the week on which the task runs.</summary>
		[DefaultValue(0)]
		public DaysOfTheWeek DaysOfWeek
		{
			get => v2Trigger != null
				? (DaysOfTheWeek)((IWeeklyTrigger)v2Trigger).DaysOfWeek
				: (DaysOfTheWeek)v1TriggerData.Type.Weekly.rgfDaysOfTheWeek;
			set
			{
				if (v2Trigger != null)
					((IWeeklyTrigger)v2Trigger).DaysOfWeek = (TaskDaysOfTheWeek)value;
				else
				{
					v1TriggerData.Type.Weekly.rgfDaysOfTheWeek = (TaskDaysOfTheWeek)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(DaysOfWeek)] = value;
				}
			}
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan), "00:00:00")]
		[XmlIgnore]
		public TimeSpan RandomDelay
		{
			get => RandomDelayNullable.GetValueOrDefault();
			set => RandomDelayNullable = value == TimeSpan.Zero ? (TimeSpan?)null : value;
		}

		/// <summary>Gets or sets a delay time that is randomly added to the start time of the trigger.</summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(typeof(TimeSpan?), null)]
		[XmlIgnore]
		public TimeSpan? RandomDelayNullable
		{
			get => v2Trigger != null ? ((IWeeklyTrigger)v2Trigger).RandomDelay.Value : GetUnboundValueOrDefault<TimeSpan?>(nameof(RandomDelay));
			set
			{
				if (v2Trigger != null)
					((IWeeklyTrigger)v2Trigger).RandomDelay = value;
				else if (v1Trigger != null)
					throw new NotV1SupportedException();
				else
					unboundValues[nameof(RandomDelay)] = value;
			}
		}

		/// <summary>Gets or sets the interval between the weeks in the schedule.</summary>
		[DefaultValue(1)]
		public short WeeksInterval
		{
			get => ((IWeeklyTrigger)v2Trigger)?.WeeksInterval ?? (short)v1TriggerData.Type.Weekly.WeeksInterval;
			set
			{
				if (v2Trigger != null)
					((IWeeklyTrigger)v2Trigger).WeeksInterval = value;
				else
				{
					v1TriggerData.Type.Weekly.WeeksInterval = (ushort)value;
					if (v1Trigger != null)
						SetV1TriggerData();
					else
						unboundValues[nameof(WeeksInterval)] = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan ITriggerDelay.Delay
		{
			get => RandomDelay;
			set => RandomDelay = value;
		}

		/// <summary>Gets or sets a value that indicates the amount of time before the task is started.</summary>
		/// <value>The delay duration.</value>
		TimeSpan? ITriggerDelay.DelayNullable
		{
			get => RandomDelayNullable;
			set => RandomDelayNullable = value;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Trigger"/> the current instance. This will not copy any properties associated with
		/// any derived triggers except those supporting the <see cref="ITriggerDelay"/> interface.
		/// </summary>
		/// <param name="sourceTrigger">The source <see cref="Trigger"/>.</param>
		public override void CopyProperties(Trigger sourceTrigger)
		{
			base.CopyProperties(sourceTrigger);
			if (sourceTrigger is WeeklyTrigger wt)
			{
				DaysOfWeek = wt.DaysOfWeek;
				WeeksInterval = wt.WeeksInterval;
			}
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public override bool Equals(Trigger other) => other is WeeklyTrigger wt && base.Equals(wt) && DaysOfWeek == wt.DaysOfWeek && WeeksInterval == wt.WeeksInterval;

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader) => CalendarTrigger.ReadXml(reader, this, ReadMyXml);

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) => CalendarTrigger.WriteXml(writer, this, WriteMyXml);

		/// <summary>Gets the non-localized trigger string for V2 triggers.</summary>
		/// <returns>String describing the trigger.</returns>
		protected override string V2GetTriggerString()
		{
			var days = TaskEnumGlobalizer.GetString(DaysOfWeek);
			return string.Format(WeeksInterval == 1 ? Properties.Resources.TriggerWeekly1Week : Properties.Resources.TriggerWeeklyMultWeeks, AdjustToLocal(StartBoundary), days, WeeksInterval);
		}

		/// <summary>Reads the subclass XML for V1 streams.</summary>
		/// <param name="reader">The reader.</param>
		private void ReadMyXml(System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("ScheduleByWeek");
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				switch (reader.LocalName)
				{
					case "WeeksInterval":
						WeeksInterval = (short)reader.ReadElementContentAsInt();
						break;

					case "DaysOfWeek":
						reader.Read();
						DaysOfWeek = 0;
						while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
						{
							try
							{
								DaysOfWeek |= (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), reader.LocalName);
							}
							catch
							{
								throw new System.Xml.XmlException("Invalid days of the week element.");
							}
							reader.Read();
						}
						reader.ReadEndElement();
						break;

					default:
						reader.Skip();
						break;
				}
			}
			reader.ReadEndElement();
		}

		/// <summary>Writes the subclass XML for V1 streams.</summary>
		/// <param name="writer">The writer.</param>
		private void WriteMyXml(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("ScheduleByWeek");

			if (WeeksInterval != 1)
				writer.WriteElementString("WeeksInterval", WeeksInterval.ToString());

			writer.WriteStartElement("DaysOfWeek");
			foreach (DaysOfTheWeek e in Enum.GetValues(typeof(DaysOfTheWeek)))
				if (e != DaysOfTheWeek.AllDays && (DaysOfWeek & e) == e)
					writer.WriteElementString(e.ToString(), null);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}

	internal static class CalendarTrigger
	{
		internal delegate void CalendarXmlReader(System.Xml.XmlReader reader);

		internal delegate void CalendarXmlWriter(System.Xml.XmlWriter writer);

		public static void WriteXml([NotNull] System.Xml.XmlWriter writer, [NotNull] Trigger t, [NotNull] CalendarXmlWriter calWriterProc)
		{
			if (!t.Enabled)
				writer.WriteElementString("Enabled", System.Xml.XmlConvert.ToString(t.Enabled));
			if (t.EndBoundary != DateTime.MaxValue)
				writer.WriteElementString("EndBoundary", System.Xml.XmlConvert.ToString(t.EndBoundary, System.Xml.XmlDateTimeSerializationMode.RoundtripKind));
			XmlSerializationHelper.WriteObject(writer, t.Repetition);
			writer.WriteElementString("StartBoundary", System.Xml.XmlConvert.ToString(t.StartBoundary, System.Xml.XmlDateTimeSerializationMode.RoundtripKind));
			calWriterProc(writer);
		}

		internal static Trigger GetTriggerFromXml([NotNull] System.Xml.XmlReader reader)
		{
			Trigger t = null;
			var xml = reader.ReadOuterXml();
			var match = System.Text.RegularExpressions.Regex.Match(xml, @"\<(?<T>ScheduleBy.+)\>");
			if (match.Success && match.Groups.Count == 2)
			{
				switch (match.Groups[1].Value)
				{
					case "ScheduleByDay":
						t = new DailyTrigger();
						break;

					case "ScheduleByWeek":
						t = new WeeklyTrigger();
						break;

					case "ScheduleByMonth":
						t = new MonthlyTrigger();
						break;

					case "ScheduleByMonthDayOfWeek":
						t = new MonthlyDOWTrigger();
						break;
				}

				if (t != null)
				{
					using (var ms = new System.IO.StringReader(xml))
					{
						using (var iReader = System.Xml.XmlReader.Create(ms))
						{
							((IXmlSerializable)t).ReadXml(iReader);
						}
					}
				}
			}
			return t;
		}

		internal static void ReadXml([NotNull] System.Xml.XmlReader reader, [NotNull] Trigger t, [NotNull] CalendarXmlReader calReaderProc)
		{
			reader.ReadStartElement("CalendarTrigger", TaskDefinition.tns);
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				switch (reader.LocalName)
				{
					case "Enabled":
						t.Enabled = reader.ReadElementContentAsBoolean();
						break;

					case "EndBoundary":
						t.EndBoundary = reader.ReadElementContentAsDateTime();
						break;

					case "RandomDelay":
						((ITriggerDelay)t).DelayNullable = Task.StringToTimeSpan(reader.ReadElementContentAsString());
						break;

					case "StartBoundary":
						t.StartBoundary = reader.ReadElementContentAsDateTime();
						break;

					case "Repetition":
						XmlSerializationHelper.ReadObject(reader, t.Repetition);
						break;

					case "ScheduleByDay":
					case "ScheduleByWeek":
					case "ScheduleByMonth":
					case "ScheduleByMonthDayOfWeek":
						calReaderProc(reader);
						break;

					default:
						reader.Skip();
						break;
				}
			}
			reader.ReadEndElement();
		}
	}

	internal sealed class RepetitionPatternConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			var rp = (RepetitionPattern)value;
			if (destinationType != typeof(string)) return base.ConvertTo(context, culture, value, destinationType);
			if (rp.Interval == TimeSpan.Zero) return "";
			var sduration = rp.Duration == TimeSpan.Zero ? Properties.Resources.TriggerDuration0 : string.Format(Properties.Resources.TriggerDurationNot0Short, Trigger.GetBestTimeSpanString(rp.Duration));
			return string.Format(Properties.Resources.TriggerRepetitionShort, Trigger.GetBestTimeSpanString(rp.Interval), sduration);
		}
	}
}