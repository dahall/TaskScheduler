using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler.Events
{
	/// <summary>
	/// Object which encapsulates the filter query used by <see cref="EventTrigger"/> objects.
	/// </summary>
	[XmlRoot("QueryList", IsNullable = false)]
	public class EventQuery
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventQuery"/> class.
		/// </summary>
		public EventQuery() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="EventQuery"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="eventIDs">The event ids.</param>
		/// <param name="startTime">The start time.</param>
		/// <param name="endTime">The end time.</param>
		public EventQuery(string path, int[] eventIDs = null, DateTime? startTime = null, DateTime? endTime = null)
		{
			Query.AddPath(path);
			if (eventIDs != null)
				Query.IDs.AddRange(Array.ConvertAll<int, CQuery.CID>(eventIDs, i => new CQuery.CID(i)));
			if (startTime.HasValue || endTime.HasValue)
				Query.Times = new CQuery.CTimeCreated(startTime, endTime);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EventQuery"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="lastSpan">The last span.</param>
		/// <param name="eventIDs">The event ids.</param>
		public EventQuery(string path, TimeSpan lastSpan, int[] eventIDs = null)
		{
			Query.AddPath(path);
			if (eventIDs != null)
				Query.IDs.AddRange(Array.ConvertAll<int, CQuery.CID>(eventIDs, i => new CQuery.CID(i)));
			Query.Times = new CQuery.CTimeCreated(lastSpan);
		}

		/// <summary>
		/// The query sub element
		/// </summary>
		[XmlElement]
		public CQuery Query = new CQuery();

		private static Serializer<EventQuery> QLSerializer = new Serializer<EventQuery>();

		/// <summary>
		/// Deserializes the specified XML.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		public static EventQuery Deserialize(string xml) => QLSerializer.Deserialize(xml);

		/// <summary>
		/// Serializes the specified q.
		/// </summary>
		/// <param name="q">The q.</param>
		/// <returns></returns>
		public static string Serialize(EventQuery q)
		{
			q.FinalizeObject();
			return QLSerializer.Serialize(q);
		}

		internal void FinalizeObject()
		{
			Query.Suppress.Clear();
			if (Query.SuppressedIDs.Count > 0)
			{
				foreach (var i in Query.Select)
					Query.Suppress.Add(new CQuery.CSuppress(Query, i.Path));
			}
		}

		/// <summary>
		/// Represents the Query sub object.
		/// </summary>
		public class CQuery : IXmlSerializable
		{
			/// <summary>
			/// The identifier
			/// </summary>
			[XmlAttribute]
			public int Id = 0;

			/// <summary>
			/// The path
			/// </summary>
			[XmlAttribute]
			public string Path;

			/// <summary>
			/// The computers
			/// </summary>
			[XmlIgnore]
			public List<string> Computers = new List<string>();

			/// <summary>
			/// The data items (e.g. TaskName) that are being requested
			/// </summary>
			[XmlIgnore]
			public Dictionary<string, string> Data = new Dictionary<string, string>();

			/// <summary>
			/// The ids
			/// </summary>
			[XmlIgnore]
			public List<CID> IDs = new List<CID>();

			/// <summary>
			/// The is select vs suppress
			/// </summary>
			[XmlIgnore]
			public bool IsSelect = true;

			/// <summary>
			/// The keywords
			/// </summary>
			[XmlIgnore]
			public long Keywords = 0;

			/// <summary>
			/// The levels
			/// </summary>
			[XmlIgnore]
			public List<int> Levels = new List<int>();

			/// <summary>
			/// The providers
			/// </summary>
			[XmlIgnore]
			public List<string> Providers = new List<string>();

			/// <summary>
			/// The select
			/// </summary>
			[XmlElement("Select")]
			public List<CSelect> Select = new List<CSelect>();

			/// <summary>
			/// The suppress
			/// </summary>
			[XmlElement("Suppress")]
			public List<CSuppress> Suppress = new List<CSuppress>();

			/// <summary>
			/// The suppressed ids
			/// </summary>
			[XmlIgnore]
			public List<CID> SuppressedIDs = new List<CID>();

			/// <summary>
			/// The tasks
			/// </summary>
			[XmlIgnore]
			public List<int> Tasks = new List<int>();

			/// <summary>
			/// The times
			/// </summary>
			[XmlIgnore]
			public CTimeCreated Times;

			/// <summary>
			/// The user
			/// </summary>
			[XmlIgnore]
			public string User = null;

			/// <summary>
			/// Gets or sets the identifier string.
			/// </summary>
			/// <value>
			/// The identifier string.
			/// </value>
			[XmlIgnore]
			public string IDString
			{
				get
				{
					int tc = SuppressedIDs.Count + IDs.Count;
					if (tc == 0) return string.Empty;
					var output = new string[tc];
					for (int i = 0; i < IDs.Count; i++)
						output[i] = IDs[i].Text;
					for (int i = 0; i < SuppressedIDs.Count; i++)
						output[i + IDs.Count] = SuppressedIDs[i].Text;
					return string.Join(",", output);
				}
				set
				{
					SuppressedIDs.Clear();
					IDs.Clear();
					value = value.Replace(" ", "");
					var IDRegex = new Regex(@"(?<r>(?<r1>\d+)-(?<r2>\d+))|(?<n>-?\d+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
					foreach (Match m in IDRegex.Matches(value))
					{
						if (m.Groups["n"].Success)
						{
							var i = int.Parse(m.Groups["n"].Value);
							if (i >= 0)
								IDs.Add(new CID(i));
							else
								SuppressedIDs.Add(new CID(-i));
						}
						else if (m.Groups["r"].Success)
							IDs.Add(new CID(int.Parse(m.Groups["r1"].Value), int.Parse(m.Groups["r2"].Value)));
					}
				}
			}

			/// <summary>
			/// Adds the path.
			/// </summary>
			/// <param name="path">The path.</param>
			public void AddPath(string path)
			{
				Path = path;
				Select.Add(new CSelect(this, path));
			}

			/// <summary>
			/// Adds the validated computers.
			/// </summary>
			/// <param name="computers">The computers.</param>
			public void AddValidatedComputers(string computers)
			{
				Computers.Clear();
				// TODO: Validate items
				Computers.AddRange(computers.Replace(" ", "").Split(',', ';'));
			}

			/// <summary>
			/// Adds the validated user.
			/// </summary>
			/// <param name="user">The user.</param>
			/// <exception cref="System.Security.Principal.IdentityNotMappedException"></exception>
			public void AddValidatedUser(string user)
			{
				string sid = NativeMethods.AccountUtils.SidStringFromUserName(user);
				if (sid == null)
					throw new System.Security.Principal.IdentityNotMappedException();
				User = sid;
			}

			System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

			void IXmlSerializable.ReadXml(XmlReader reader)
			{
				if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Query")
				{
					Id = int.Parse(reader["Id"]);
					Path = reader["Path"];
					if (reader.ReadToDescendant("Select"))
					{
						while (reader.MoveToContent() == XmlNodeType.Element && (reader.LocalName == "Select" || reader.LocalName == "Suppress"))
						{
							if (reader.LocalName == "Select")
							{
								CSelect sel = new CSelect(this, null);
								sel.ReadXml(reader);
								Select.Add(sel);
							}
							else
							{
								CSuppress sup = new CSuppress(this, null);
								sup.ReadXml(reader);
								Suppress.Add(sup);
							}
						}
					}
					reader.ReadEndElement();
				}
			}

			void IXmlSerializable.WriteXml(XmlWriter writer)
			{
				// Serialize attributes
				writer.WriteAttributeString("Id", Id.ToString());
				if (!string.IsNullOrEmpty(Path))
					writer.WriteAttributeString("Path", Path);
				// Serialize Selects
				var selSerializer = new Serializer<CSelect>();
				foreach (var s in Select)
					selSerializer.Serialize(writer, s);
				// Serialize Suppress
				var supSerializer = new Serializer<CSuppress>();
				foreach (var s in Suppress)
					supSerializer.Serialize(writer, s);
			}

			/// <summary>
			/// Represents event IDs
			/// </summary>
			public class CID
			{
				private int low, high;

				/// <summary>
				/// Initializes a new instance of the <see cref="CID"/> class.
				/// </summary>
				/// <param name="id">The identifier.</param>
				public CID(int id)
				{
					low = id; high = -1;
				}

				/// <summary>
				/// Initializes a new instance of the <see cref="CID"/> class.
				/// </summary>
				/// <param name="idLow">The identifier low.</param>
				/// <param name="idHigh">The identifier high.</param>
				public CID(int idLow, int idHigh)
				{
					low = idLow; high = idHigh;
				}

				/// <summary>
				/// Gets the text.
				/// </summary>
				/// <value>
				/// The text.
				/// </value>
				[XmlIgnore]
				public string Text
				{
					get
					{
						if (high == -1)
							return $"{low}";
						return $"{low}-{high}";
					}
				}

				/// <summary>
				/// Returns a <see cref="System.String" /> that represents this instance.
				/// </summary>
				/// <returns>
				/// A <see cref="System.String" /> that represents this instance.
				/// </returns>
				public override string ToString()
				{
					if (high == -1)
						return $"EventID={low}";
					return $"(EventID >= {low} and EventID <= {high})";
				}
			}

			/// <summary>
			/// Represents the Select element
			/// </summary>
			[XmlRoot("Select")]
			public class CSelect : IXmlSerializable
			{
				/// <summary>
				/// The path
				/// </summary>
				[XmlAttribute]
				public string Path;

				/// <summary>
				/// The parent
				/// </summary>
				[XmlIgnore]
				public CQuery Parent;

				/// <summary>
				/// The is select
				/// </summary>
				[XmlIgnore]
				protected bool IsSelect = true;

				// Regex patterns to find different values
				private const string computer = @"(?<strs>\(\s*(?:Computer\s*=\s*'([^']+)')(?:\s*or\s*(?:Computer\s*=\s*'([^']+)'))*\s*\))";
				private const string evid = @"(?:EventID\s*=\s*(?<int>-?\d+))";
				private const string evidrange = @"(?<ints>EventID\s*>=\s*(\d+)\s*and\s*EventID\s*<=\s*(\d+))";
				private const string keyword = @"band\s*\(\s*Keywords\s*,\s*(?<long>\d+)\s*\)";
				private const string level = @"(?<ints>\(\s*(?:Level\s*=\s*(\d+))(?:\s*or\s*(?:Level\s*=\s*(\d+)))*\s*\))";
				private const string prov = @"(?<strs>Provider\s*\[\s*(?:@Name\s*=\s*'([^']+)')(?:\s*or\s*(?:@Name\s*=\s*'([^']+)'))*\s*\])";
				private const string tasks = @"(?<ints>\(\s*(?:Task\s*=\s*(\d+))(?:\s*or\s*(?:Task\s*=\s*(\d+)))*\s*\))";
				private const string time2dates = @"(?<dates>TimeCreated\s*\[\s*(?:\s*@SystemTime\s*([<>]=)\s*'([^']+)')\s*and\s*@SystemTime\s*([<>]=)\s*'([^']+)'\s*\])";
				private const string timedate2end = @"TimeCreated\s*\[\s*(?:\s*@SystemTime\s*>=\s*'(?<date>[^']+)')\s*\]";
				private const string timediff = @"TimeCreated\s*\[\s*(?:timediff\s*\(\s*@SystemTime\s*\)\s*<=\s*(?<long>\d+))\s*\]";
				private const string timestart2date = @"TimeCreated\s*\[\s*(?:\s*@SystemTime\s*<=\s*'(?<date>[^']+)')\s*\]";
				private const string user = @"Security\s*\[\s*(?:@UserID\s*=\s*'(?<str>[^']+)')\s*\]";
				private const string OR = " or ";
				private const string AND = " and ";
				private const string eventData = @"\s*Data\s*\[\s*@Name\s*=\s*'(?<key>[^']*)'\s*\]\s*=\s*'(?<val>[^']*)'\s*(?:and\s*)?";

				/// <summary>
				/// Initializes a new instance of the <see cref="CSelect"/> class.
				/// </summary>
				/// <param name="parent">The parent.</param>
				/// <param name="path">The path.</param>
				public CSelect(CQuery parent, string path)
				{
					Parent = parent;
					Path = path;
				}

				/// <summary>
				/// Initializes a new instance of the <see cref="CSelect"/> class.
				/// </summary>
				protected CSelect() { }

				private void CheckSelectValue(string value)
				{
					string[] inner = InnerValue(value);
					if (inner[0] == null && inner[1] == null) return;
					string newVal = null;
					if (inner[0] != null)
					{
						newVal = Regex.Replace(inner[0], string.Join("|", new string[] { computer, evidrange, evid, keyword, level, prov, tasks, user, time2dates, timedate2end, timestart2date, timediff, AND, @"\s*\(\s*\)\s*", @"\s+" }), string.Empty, RegexOptions.IgnoreCase);
						if (newVal != null)
							newVal = Regex.Replace(newVal, @"\s*\(\s*(?:or\s*)*\)\s*", string.Empty, RegexOptions.IgnoreCase);
						ThrowIfBadXml(value, newVal, "Select");
					}
					if (inner[1] != null)
						newVal = Regex.Replace(inner[1], eventData, string.Empty, RegexOptions.IgnoreCase);
					ThrowIfBadXml(value, newVal, "Select");
				}

				private void CheckSuppressValue(string value)
				{
					string[] inner = InnerValue(value);
					if (inner[0] == null) return;
					string newVal = Regex.Replace(inner[0], evid, string.Empty, RegexOptions.IgnoreCase);
					ThrowIfBadXml(value, newVal, "Suppress");
				}

				private string[] InnerValue(string value)
				{
					string[] ret = new string[] { null, null };
					var r = new Regex(@"^\s*\*\s*(?:\[\s*System\s*\[(?<sys>.*)\]\s*\]\s*)?(?:and\s*)?(?:\*\s*\[\s*EventData\s*\[(?<data>.*)\]\s*\]\s*)?$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft);
					Match m = r.Match(value);
					if (m.Success)
					{
						if (m.Groups["sys"].Success)
							ret[0] = m.Groups["sys"].Value;
						if (m.Groups["data"].Success)
							ret[1] = m.Groups["data"].Value;
					}
					return ret;
				}

				private void ThrowIfBadXml(string value, string xtraXml, string parentNode)
				{
					if (Regex.Replace(xtraXml, @"\s+", string.Empty).Length > 0)
					{
						var exc = new InvalidOperationException($"Invalid value for {parentNode} node.");
						exc.Data.Add("Remaining text", xtraXml);
						var idx = value.IndexOf(xtraXml);
						if (idx > -1)
							exc.Data.Add("Value index", idx);
						throw exc;
					}
				}

				/// <summary>
				/// Gets or sets the value.
				/// </summary>
				/// <value>
				/// The value.
				/// </value>
				[XmlText]
				public virtual string Value
				{
					get
					{
						var sb = new System.Text.StringBuilder();
						sb.Append("*");
						if (IsSelect)
						{
							if (Parent.Providers.Count > 0)
								sb.AppendFormat("Provider[{0}]", string.Join(OR, Parent.Providers.ConvertAll<string>(s => $"@Name='{s}'").ToArray()));
							if (Parent.Computers.Count > 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("({0})", string.Join(OR, Parent.Computers.ConvertAll<string>(i => $"Computer='{i}'").ToArray()));
							}
							if (Parent.Levels.Count > 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("({0})", string.Join(OR, Parent.Levels.ConvertAll<string>(i => $"Level={i}").ToArray()));
							}
							if (Parent.Tasks.Count > 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("({0})", string.Join(OR, Parent.Tasks.ConvertAll<string>(i => $"Task={i}").ToArray()));
							}
							if (Parent.Keywords != 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("(band(Keywords,{0}))", Parent.Keywords);
							}
							if (Parent.IDs.Count > 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("({0})", string.Join(OR, Parent.IDs.ConvertAll<string>(i => i.ToString()).ToArray()));
							}
							if (!string.IsNullOrEmpty(Parent.User))
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("Security[@UserID='{0}']", Parent.User);
							}
							if (Parent.Times != null && ((Parent.Times.span.HasValue && Parent.Times.span.Value != TimeSpan.Zero) || Parent.Times.HasDates))
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("TimeCreated[{0}]", Parent.Times);
							}
						}
						else
						{
							if (Parent.SuppressedIDs.Count > 0)
							{
								if (sb.Length > 1) sb.Append(AND);
								sb.AppendFormat("({0})", string.Join(OR, Parent.SuppressedIDs.ConvertAll<string>(i => i.ToString()).ToArray()));
							}
						}
						if (sb.Length > 1)
						{
							sb.Insert(1, "[System[");
							sb.Append("]]");
						}
						if (IsSelect && Parent.Data.Count > 0)
						{
							if (sb.Length > 1)
								sb.Append(AND + "*");
							sb.Append("[EventData[");
							var dataItems = new List<string>();
							foreach (var kv in Parent.Data)
								dataItems.Add($"Data[@Name='{kv.Key}']='{kv.Value}'");
							sb.Append(string.Join(AND, dataItems.ToArray()));
							sb.Append("]]");
						}
						return sb.ToString();
					}
					set
					{
						if (IsSelect)
						{
							CheckSelectValue(value);

							// Providers
							Parent.Providers.AddRange(GetParsedValue(prov, value).ConvertAll(o => (string)o));
							// Levels
							Parent.Levels.AddRange(GetParsedValue(level, value).ConvertAll(o => (int)o));
							// IDs
							Parent.IDs.AddRange(GetParsedValue(evid, value).ConvertAll(o => new CID((int)o)));
							var idr = GetParsedValue(evidrange, value);
							for (int i = 0; i < idr.Count; i += 2)
								Parent.IDs.Add(new CID((int)idr[i], (int)idr[i + 1]));
							// Tasks
							Parent.Tasks.AddRange(GetParsedValue(tasks, value).ConvertAll(o => (int)o));
							// Keywords
							var k = GetParsedValue(keyword, value);
							if (k.Count > 0) Parent.Keywords = (long)k[0];
							// Users
							var u = GetParsedValue(user, value);
							if (u.Count > 0) Parent.User = NativeMethods.AccountUtils.UserNameFromSidString((string)u[0]);
							// Computers
							Parent.Computers.AddRange(GetParsedValue(computer, value).ConvertAll(o => (string)o));
							// Times
							var tc = GetParsedValue(timediff, value);
							if (tc.Count > 0)
								Parent.Times = new CTimeCreated(TimeSpan.FromMilliseconds((long)tc[0]));
							else
							{
								DateTime? s = null, e = null;
								tc = GetParsedValue(time2dates, value);
								if (tc.Count > 1)
								{
									s = (DateTime)tc[0];
									e = (DateTime)tc[1];
								}
								else
								{
									tc = GetParsedValue(timestart2date, value);
									if (tc.Count > 0)
										e = (DateTime)tc[0];
									else
									{
										tc = GetParsedValue(timedate2end, value);
										if (tc.Count > 0)
											s = (DateTime)tc[0];
									}
								}
								if (s.HasValue || e.HasValue)
									Parent.Times = new CTimeCreated(s, e);
							}
							// Data value
							var regex = new Regex(eventData, RegexOptions.IgnoreCase | RegexOptions.Compiled);
							foreach (Match m in regex.Matches(value))
							{
								if (m.Groups["key"].Success && m.Groups["val"].Success)
									Parent.Data.Add(m.Groups["key"].Value, m.Groups["val"].Value);
							}
						}
						else
						{
							CheckSuppressValue(value);
							// SuppressedIDs
							Parent.SuppressedIDs.AddRange(GetParsedValue(evid, value).ConvertAll(o => new CID(-(int)o)));
						}
					}
				}

				System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema() => null;

				/// <summary>
				/// Generates an object from its XML representation.
				/// </summary>
				/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
				public void ReadXml(XmlReader reader)
				{
					if (reader.MoveToContent() == XmlNodeType.Element)
					{
						if (reader.LocalName == "Suppress")
							IsSelect = false;
						Path = reader["Path"];
						Value = reader.ReadString();
						reader.ReadEndElement();
					}
				}

				/// <summary>
				/// Converts an object into its XML representation.
				/// </summary>
				/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
				public void WriteXml(XmlWriter writer)
				{
					writer.WriteAttributeString("Path", Path);
					writer.WriteString(Value);
				}

				private List<object> GetParsedValue(string regexPattern, string input)
				{
					var regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
					List<object> l = new List<object>();
					int i;
					foreach (Match m in regex.Matches(input))
					{
						if (m.Groups["str"].Success)
							l.Add(m.Groups["str"].Value);
						else if (m.Groups["long"].Success)
							l.Add(long.Parse(m.Groups["long"].Value));
						else if (m.Groups["int"].Success)
							l.Add(int.Parse(m.Groups["int"].Value));
						else if (m.Groups["date"].Success)
							l.Add(DateTime.Parse(m.Groups["date"].Value));
						else if (m.Groups["strs"].Success)
						{
							for (i = 1; i < m.Groups.Count - 1; i++)
								if (m.Groups[i].Captures.Count > 0)
									l.Add(m.Groups[i].Value);
						}
						else if (m.Groups["ints"].Success)
						{
							for (i = 1; i < m.Groups.Count - 1; i++)
								if (m.Groups[i].Captures.Count > 0)
									l.Add(int.Parse(m.Groups[i].Value));
						}
						else if (m.Groups["dates"].Success)
						{
							char lastGL = '\0';
							for (i = 1; i < m.Groups.Count - 1; i++)
							{
								if (m.Groups[i].Success)
								{
									if (m.Groups[i].Value.Length > 0 && (m.Groups[i].Value[0] == '<' || m.Groups[i].Value[0] == '>'))
										lastGL = m.Groups[i].Value[0];
									else
									{
										var dt = DateTime.Parse(m.Groups[i].Value);
										if (lastGL == '>')
											l.Insert(0, dt);
										else
											l.Add(dt);
									}
								}
							}
						}
					}
					return l;
				}
			}

			/// <summary>
			/// Represents the Suppress element
			/// </summary>
			[XmlRoot("Suppress")]
			public class CSuppress : CSelect
			{
				/// <summary>
				/// Initializes a new instance of the <see cref="CSuppress"/> class.
				/// </summary>
				/// <param name="parent">The parent.</param>
				/// <param name="path">The path.</param>
				public CSuppress(CQuery parent, string path) : base(parent, path) { IsSelect = false; }
				/// <summary>
				/// Initializes a new instance of the <see cref="CSuppress"/> class.
				/// </summary>
				protected CSuppress() { IsSelect = false; }
			}

			/// <summary>
			/// Represents the event time creation values
			/// </summary>
			public class CTimeCreated
			{
				/// <summary>
				/// The low
				/// </summary>
				public DateTime? low, high;
				/// <summary>
				/// The span
				/// </summary>
				public System.TimeSpan? span;

				/// <summary>
				/// Initializes a new instance of the <see cref="CTimeCreated"/> class.
				/// </summary>
				/// <param name="last">The last.</param>
				public CTimeCreated(TimeSpan last)
				{
					span = last;
				}

				/// <summary>
				/// Initializes a new instance of the <see cref="CTimeCreated"/> class.
				/// </summary>
				/// <param name="start">The start.</param>
				/// <param name="end">The end.</param>
				public CTimeCreated(DateTime? start, DateTime? end)
				{
					low = start; high = end;
				}

				/// <summary>
				/// Gets a value indicating whether this instance has dates.
				/// </summary>
				/// <value>
				///   <c>true</c> if this instance has dates; otherwise, <c>false</c>.
				/// </value>
				public bool HasDates => !span.HasValue;

				/// <summary>
				/// Returns a <see cref="System.String" /> that represents this instance.
				/// </summary>
				/// <returns>
				/// A <see cref="System.String" /> that represents this instance.
				/// </returns>
				public override string ToString()
				{
					if (span.HasValue)
					{
						if (span.Value != TimeSpan.Zero && span.Value != TimeSpan.MaxValue)
							return $"timediff(@SystemTime) <= {span.Value.TotalMilliseconds}";
					}
					else
					{
						string d1 = low.HasValue ? $"@SystemTime >= '{low.Value}'" : null;
						string d2 = high.HasValue ? $"@SystemTime <= '{high.Value}'" : null;
						if (low.HasValue && !high.HasValue)
							return d1;
						else if (!low.HasValue && high.HasValue)
							return d2;
						else if (low.HasValue && high.HasValue)
							return string.Concat(d1, " and ", d2);
					}
					return null;
				}
			}
		}

		internal class Serializer<T> where T : class
		{
			private static readonly XmlSerializerNamespaces ns;
			private XmlSerializer ser;
			static Serializer()
			{
				ns = new XmlSerializerNamespaces();
				ns.Add("", "");
			}

			public Serializer()
			{
				ser = new XmlSerializer(typeof(T));
			}

			public T Deserialize(string xml) => Deserialize(XmlReader.Create(new System.IO.StringReader(xml)));

			public T Deserialize(XmlReader reader) => (T)ser.Deserialize(reader);

			public string Serialize(T obj)
			{
				var writer = new StringBuilder();
				using (var xmlW = XmlWriter.Create(writer, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true }))
				{
					ser.Serialize(xmlW, obj, ns);
					xmlW.Flush();
					return writer.ToString();
				}
			}

			public void Serialize(XmlWriter writer, T obj)
			{
				ser.Serialize(writer, obj, ns);
			}
		}
	}
}