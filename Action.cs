using Microsoft.Win32.TaskScheduler.V2Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Defines the type of actions a task can perform.
	/// </summary>
	/// <remarks>
	/// The action type is defined when the action is created and cannot be changed later. See <see cref="ActionCollection.AddNew"/>.
	/// </remarks>
	public enum TaskActionType
	{
		/// <summary>This action fires a handler.</summary>
		ComHandler = 5,

		/// <summary>
		/// This action performs a command-line operation. For example, the action can run a script,
		/// launch an executable, or, if the name of a document is provided, find its associated
		/// application and launch the application with the document.
		/// </summary>
		Execute = 0,

		/// <summary>This action sends and e-mail.</summary>
		SendEmail = 6,

		/// <summary>This action shows a message box.</summary>
		ShowMessage = 7
	}

	/// <summary>
	/// An interface that exposes the ability to convert an actions functionality to a PowerShell script.
	/// </summary>
	internal interface IBindAsExecAction
	{
		/// <summary>
		/// Gets the PowerShell script for an action.
		/// </summary>
		/// <returns>Single line PowerShell script string.</returns>
		string GetPowerShellCommand();
	}

	/*/// <summary>
	/// An interface that exposes the ability for an action derivative to convert itself to and from and ExecAction.
	/// </summary>
	internal interface IExtendExecAction
	{
		/// <summary>
		/// Converts this action to an <see cref="ExecAction"/>.
		/// </summary>
		/// <returns>New <see cref="ExecAction"/> whose properties are set in such as way as to indicate it as this type.</returns>
		ExecAction ToExecAction();

		/// <summary>
		/// Initializes this action from an <see cref="ExecAction" />.
		/// </summary>
		/// <param name="execAction">The <see cref="ExecAction" /> to use for initialization of this type.</param>
		void FromExecAction(ExecAction execAction);
	}*/

	/// <summary>
	/// Abstract base class that provides the common properties that are inherited by all action
	/// objects. An action object is created by the <see cref="ActionCollection.AddNew"/> method.
	/// </summary>
	public abstract class Action : IDisposable, ICloneable, IEquatable<Action>
	{
		internal V2Interop.IAction iAction = null;

		/// <summary>List of unbound values when working with Actions not associated with a registered task.</summary>
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();

		internal Action() { }

		internal Action(IAction action)
		{
			iAction = action;
		}

		/// <summary>
		/// Gets the type of the action.
		/// </summary>
		/// <value>The type of the action.</value>
		[XmlIgnore]
		public TaskActionType ActionType => iAction?.Type ?? InternalActionType;

		/// <summary>
		/// Gets or sets the identifier of the action.
		/// </summary>
		[DefaultValue(null)]
		[XmlAttribute(AttributeName = "id", DataType = "ID")]
		public virtual string Id
		{
			get { return GetProperty<string, IAction>(nameof(Id)); }
			set { SetProperty<string, IAction>(nameof(Id), value); }
		}

		internal virtual bool Bound => iAction != null;

		internal abstract TaskActionType InternalActionType { get; }

		/// <summary>
		/// Creates the specified action.
		/// </summary>
		/// <param name="actionType">Type of the action to instantiate.</param>
		/// <returns><see cref="Action"/> of specified type.</returns>
		[Obsolete("Given the expansion to allow for custom Actions, this method no longer is guaranteed to return a valid object.", true)]
		public static Action CreateAction(TaskActionType actionType) => Activator.CreateInstance(GetObjectType(actionType)) as Action;

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		/// A new object that is a copy of this instance.
		/// </returns>
		public object Clone()
		{
			Action ret = Activator.CreateInstance(GetType()) as Action;
			ret.CopyProperties(this);
			return ret;
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public virtual void Dispose()
		{
			if (iAction != null)
				Marshal.ReleaseComObject(iAction);
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is Action)
				return Equals((Action)obj);
			return base.Equals(obj);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool Equals(Action other) => ActionType == other.ActionType && Id == other.Id;

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode() => new { A = ActionType, B = Id }.GetHashCode();

		/// <summary>
		/// Returns the action Id.
		/// </summary>
		/// <returns>String representation of action.</returns>
		public override string ToString() => Id;

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this action.
		/// </summary>
		/// <param name="culture">The culture.</param>
		/// <returns>String representation of action.</returns>
		public virtual string ToString(System.Globalization.CultureInfo culture)
		{
			using (new CultureSwitcher(culture))
				return ToString();
		}

		/// <summary>
		/// Creates a specialized class from a defined interface.
		/// </summary>
		/// <param name="iAction">Version 2.0 Action interface.</param>
		/// <returns>Specialized action class</returns>
		internal static Action CreateAction(V2Interop.IAction iAction)
		{
			Type t = GetObjectType(iAction.Type);
			return Activator.CreateInstance(t, BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { iAction }, null) as Action;
		}

		internal static Type GetObjectType(TaskActionType actionType)
		{
			switch (actionType)
			{
				case TaskActionType.ComHandler:
					return typeof(ComHandlerAction);
				case TaskActionType.SendEmail:
					return typeof(EmailAction);
				case TaskActionType.ShowMessage:
					return typeof(ShowMessageAction);
				case TaskActionType.Execute:
				default:
					return typeof(ExecAction);
			}
		}

		internal virtual void Bind(V1Interop.ITask iTask)
		{
		}

		internal virtual void Bind(V2Interop.ITaskDefinition iTaskDef)
		{
			V2Interop.IActionCollection iActions = iTaskDef.Actions;
			CreateV2Action(iActions);
			Marshal.ReleaseComObject(iActions);
			foreach (string key in unboundValues.Keys)
			{
				try { ReflectionHelper.SetProperty(iAction, key, unboundValues[key]); }
				catch (TargetInvocationException tie) { throw tie.InnerException; }
				catch { }
			}
			unboundValues.Clear();
		}

		internal abstract void CreateV2Action(V2Interop.IActionCollection iActions);

		internal T GetProperty<T, B>(string propName, T defaultValue = default(T))
		{
			if (iAction == null)
				return (unboundValues.ContainsKey(propName)) ? (T)unboundValues[propName] : defaultValue;
			return ReflectionHelper.GetProperty((B)iAction, propName, defaultValue);
		}

		internal void SetProperty<T, B>(string propName, T value)
		{
			if (iAction == null)
			{
				if (Equals(value, default(T)))
					unboundValues.Remove(propName);
				else
					unboundValues[propName] = value;
			}
			else
				ReflectionHelper.SetProperty((B)iAction, propName, value);
		}

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected virtual void CopyProperties(Action sourceAction)
		{
			Id = sourceAction.Id;
		}
	}

	/// <summary>
	/// Represents an action that fires a handler. Only available on Task Scheduler 2.0.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	[XmlRoot("ComHandler", Namespace = TaskDefinition.tns, IsNullable = false)]
	public class ComHandlerAction : Action
	{
		/// <summary>
		/// Creates an unbound instance of <see cref="ComHandlerAction"/>.
		/// </summary>
		public ComHandlerAction() { }

		/// <summary>
		/// Creates an unbound instance of <see cref="ComHandlerAction"/>.
		/// </summary>
		/// <param name="classId">Identifier of the handler class.</param>
		/// <param name="data">Addition data associated with the handler.</param>
		public ComHandlerAction(Guid classId, string data)
		{
			ClassId = classId;
			Data = data;
		}

		internal ComHandlerAction(V2Interop.IAction action) : base(action) { }

		/// <summary>
		/// Gets or sets the identifier of the handler class.
		/// </summary>
		public Guid ClassId
		{
			get { return new Guid(GetProperty<string, IComHandlerAction>(nameof(ClassId), Guid.Empty.ToString())); }
			set { SetProperty<string, IComHandlerAction>(nameof(ClassId), value.ToString()); }
		}

		/// <summary>
		/// Gets the name of the object referred to by <see cref="ClassId"/>.
		/// </summary>
		public string ClassName => GetNameForCLSID(ClassId);

		/// <summary>
		/// Gets or sets additional data that is associated with the handler.
		/// </summary>
		[DefaultValue(null)]
		public string Data
		{
			get { return GetProperty<string, IComHandlerAction>(nameof(Data)); }
			set { SetProperty<string, IComHandlerAction>(nameof(Data), value); }
		}

		internal override TaskActionType InternalActionType => TaskActionType.ComHandler;

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Action other) => base.Equals(other) && ClassId == ((ComHandlerAction)other).ClassId && Data == ((ComHandlerAction)other).Data;

		/// <summary>
		/// Gets a string representation of the <see cref="ComHandlerAction"/>.
		/// </summary>
		/// <returns>String representation of this action.</returns>
		public override string ToString() => string.Format(Properties.Resources.ComHandlerAction, ClassId, Data, Id, ClassName);

		/// <summary>
		/// Gets the name for CLSID.
		/// </summary>
		/// <param name="guid">The unique identifier.</param>
		/// <returns></returns>
		internal static string GetNameForCLSID(Guid guid)
		{
			using (RegistryKey k = Registry.ClassesRoot.OpenSubKey("CLSID", false))
			{
				if (k != null)
				{
					using (RegistryKey k2 = k.OpenSubKey(guid.ToString("B"), false))
						return k2 != null ? k2.GetValue(null) as string : null;
				}
			}
			return null;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected override void CopyProperties(Action sourceAction)
		{
			if (sourceAction.GetType() == GetType())
			{
				base.CopyProperties(sourceAction);
				ClassId = ((ComHandlerAction)sourceAction).ClassId;
				Data = ((ComHandlerAction)sourceAction).Data;
			}
		}

		internal override void CreateV2Action(IActionCollection iActions)
		{
			iAction = iActions.Create(TaskActionType.ComHandler);
		}
	}

	/// <summary>
	/// Represents an action that sends an e-mail.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	[XmlRoot("SendEmail", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class EmailAction : Action, IBindAsExecAction
	{
		const string ImportanceHeader = "Importance";

		private NamedValueCollection nvc = null;

		/// <summary>
		/// Creates an unbound instance of <see cref="EmailAction"/>.
		/// </summary>
		public EmailAction() { }

		/// <summary>
		/// Creates an unbound instance of <see cref="EmailAction"/>.
		/// </summary>
		/// <param name="subject">Subject of the e-mail.</param>
		/// <param name="from">E-mail address that you want to send the e-mail from.</param>
		/// <param name="to">E-mail address or addresses that you want to send the e-mail to.</param>
		/// <param name="body">Body of the e-mail that contains the e-mail message.</param>
		/// <param name="mailServer">Name of the server that you use to send e-mail from.</param>
		public EmailAction(string subject, string from, string to, string body, string mailServer)
		{
			Subject = subject;
			From = from;
			To = to;
			Body = body;
			Server = mailServer;
		}

		internal EmailAction(V2Interop.IAction action) : base(action) { }

		/// <summary>
		/// Gets or sets an array of file paths to be sent as attachments with the e-mail. Each item must be a <see cref="System.String"/> value containing a path to file.
		/// </summary>
		[XmlArray("Attachments", IsNullable = true)]
		[XmlArrayItem(typeof(string), ElementName = "File")]
		[DefaultValue(null)]
		public object[] Attachments
		{
			get { return GetProperty<object[], IEmailAction>(nameof(Attachments)); }
			set
			{
				if (value != null)
				{
					if (value.Length > 8)
						throw new ArgumentOutOfRangeException("Attachments", "Attachments array cannot contain more than 8 items.");
					foreach (var o in value)
						if (!(o is string) || !System.IO.File.Exists((string)o))
							throw new ArgumentException("Each value of the array must contain a valid file reference.", nameof(Attachments));
				}
				if (iAction == null && (value == null || value.Length == 0))
					unboundValues.Remove(nameof(Attachments));
				else
					SetProperty<object[], IEmailAction>(nameof(Attachments), value);
			}
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to Bcc in the e-mail.
		/// </summary>
		[DefaultValue(null)]
		public string Bcc
		{
			get { return GetProperty<string, IEmailAction>(nameof(Bcc)); }
			set { SetProperty<string, IEmailAction>(nameof(Bcc), value); }
		}

		/// <summary>
		/// Gets or sets the body of the e-mail that contains the e-mail message.
		/// </summary>
		[DefaultValue(null)]
		public string Body
		{
			get { return GetProperty<string, IEmailAction>(nameof(Body)); }
			set { SetProperty<string, IEmailAction>(nameof(Body), value); }
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to Cc in the e-mail.
		/// </summary>
		[DefaultValue(null)]
		public string Cc
		{
			get { return GetProperty<string, IEmailAction>(nameof(Cc)); }
			set { SetProperty<string, IEmailAction>(nameof(Cc), value); }
		}

		/// <summary>
		/// Gets or sets the e-mail address that you want to send the e-mail from.
		/// </summary>
		[DefaultValue(null)]
		public string From
		{
			get { return GetProperty<string, IEmailAction>(nameof(From)); }
			set { SetProperty<string, IEmailAction>(nameof(From), value); }
		}

		/// <summary>
		/// Gets or sets the header information in the e-mail message to send.
		/// </summary>
		[XmlArray]
		[XmlArrayItem("HeaderField", typeof(NameValuePair))]
		public NamedValueCollection HeaderFields
		{
			get
			{
				if (nvc == null)
				{
					if (iAction != null)
						nvc = new NamedValueCollection(((V2Interop.IEmailAction)iAction).HeaderFields);
					else
						nvc = new NamedValueCollection();
				}
				return nvc;
			}
		}

		/// <summary>
		/// Gets or sets the priority of the e-mail message.
		/// </summary>
		/// <value>
		/// A <see cref="System.Net.Mail.MailPriority"/> that contains the priority of this message.
		/// </value>
		[XmlIgnore]
		[DefaultValue(typeof(System.Net.Mail.MailPriority), "Normal")]
		public System.Net.Mail.MailPriority Priority
		{
			get
			{
				string s;
				System.Net.Mail.MailPriority res = System.Net.Mail.MailPriority.Normal;
				if (nvc != null && HeaderFields.TryGetValue(ImportanceHeader, out s))
				{
					try { res = (System.Net.Mail.MailPriority)Enum.Parse(typeof(System.Net.Mail.MailPriority), s, true); }
					catch { }
				}
				return res;
			}
			set
			{
				HeaderFields[ImportanceHeader] = value.ToString();
			}
		}

		/// <summary>
		/// Gets or sets the e-mail address that you want to reply to.
		/// </summary>
		[DefaultValue(null)]
		public string ReplyTo
		{
			get { return GetProperty<string, IEmailAction>(nameof(ReplyTo)); }
			set { SetProperty<string, IEmailAction>(nameof(ReplyTo), value); }
		}

		/// <summary>
		/// Gets or sets the name of the server that you use to send e-mail from.
		/// </summary>
		[DefaultValue(null)]
		public string Server
		{
			get { return GetProperty<string, IEmailAction>(nameof(Server)); }
			set { SetProperty<string, IEmailAction>(nameof(Server), value); }
		}

		/// <summary>
		/// Gets or sets the subject of the e-mail.
		/// </summary>
		[DefaultValue(null)]
		public string Subject
		{
			get { return GetProperty<string, IEmailAction>(nameof(Subject)); }
			set { SetProperty<string, IEmailAction>(nameof(Subject), value); }
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to send the e-mail to.
		/// </summary>
		[DefaultValue(null)]
		public string To
		{
			get { return GetProperty<string, IEmailAction>(nameof(To)); }
			set { SetProperty<string, IEmailAction>(nameof(To), value); }
		}

		internal override TaskActionType InternalActionType => TaskActionType.SendEmail;

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Action other) => base.Equals(other) && (this as IBindAsExecAction).GetPowerShellCommand() == (other as IBindAsExecAction).GetPowerShellCommand();

		string IBindAsExecAction.GetPowerShellCommand()
		{
			// Send-MailMessage [-To] <String[]> [-Subject] <String> [[-Body] <String> ] [[-SmtpServer] <String> ] -From <String> [-Attachments <String[]> ]
			//    [-Bcc <String[]> ] [-BodyAsHtml] [-Cc <String[]> ] [-Credential <PSCredential> ] [-DeliveryNotificationOption <DeliveryNotificationOptions> ]
			//    [-Encoding <Encoding> ] [-Port <Int32> ] [-Priority <MailPriority> ] [-UseSsl] [ <CommonParameters>]
			bool bodyIsHtml = Body != null && Body.Trim().StartsWith("<") && Body.Trim().EndsWith(">");
			var sb = new System.Text.StringBuilder();
			sb.AppendFormat("Send-MailMessage -From '{0}' -Subject '{1}' -SmtpServer '{2}' -Encoding UTF8", Prep(From), ToUTF8(Prep(Subject)), Prep(Server));
			if (!string.IsNullOrEmpty(To))
				sb.AppendFormat(" -To {0}", ToPS(To));
			if (!string.IsNullOrEmpty(Cc))
				sb.AppendFormat(" -Cc {0}", ToPS(Cc));
			if (!string.IsNullOrEmpty(Bcc))
				sb.AppendFormat(" -Bcc {0}", ToPS(Bcc));
			if (bodyIsHtml)
				sb.Append(" -BodyAsHtml");
			if (!string.IsNullOrEmpty(Body))
				sb.AppendFormat(" -Body '{0}'", ToUTF8(Prep(Body)));
			if (Attachments != null && Attachments.Length > 0)
				sb.AppendFormat(" -Attachments {0}", ToPS(Array.ConvertAll<object, string>(Attachments, o => Prep(o.ToString()))));
			var hdr = new List<string>(HeaderFields.Names);
			if (hdr.Contains(ImportanceHeader))
			{
				var p = Priority;
				if (p != System.Net.Mail.MailPriority.Normal)
					sb.Append($" -Priority {p.ToString()}");
				hdr.Remove(ImportanceHeader);
			}
			if (hdr.Count > 0)
				throw new InvalidOperationException("Under Windows 8 and later, EmailAction objects are converted to PowerShell. This action contains headers that are not supported.");
			return sb.ToString();

			/*var msg = new System.Net.Mail.MailMessage(this.From, this.To, this.Subject, this.Body);
			if (!string.IsNullOrEmpty(this.Bcc))
				msg.Bcc.Add(this.Bcc);
			if (!string.IsNullOrEmpty(this.Cc))
				msg.CC.Add(this.Cc);
			if (!string.IsNullOrEmpty(this.ReplyTo))
				msg.ReplyTo = new System.Net.Mail.MailAddress(this.ReplyTo);
			if (this.Attachments != null && this.Attachments.Length > 0)
				foreach (string s in this.Attachments)
					msg.Attachments.Add(new System.Net.Mail.Attachment(s));
			if (this.nvc != null)
				foreach (var ha in this.HeaderFields)
					msg.Headers.Add(ha.Name, ha.Value);
			var client = new System.Net.Mail.SmtpClient(this.Server);
			client.Send(msg);*/
		}

		/// <summary>
		/// Gets a string representation of the <see cref="EmailAction"/>.
		/// </summary>
		/// <returns>String representation of this action.</returns>
		public override string ToString() => string.Format(Properties.Resources.EmailAction, Subject, To, Cc, Bcc, From, ReplyTo, Body, Server, Id);

		internal static Action FromPowerShellCommand(string p)
		{
			var match = System.Text.RegularExpressions.Regex.Match(p, @"^Send-MailMessage -From '(?<from>(?:[^']|'')*)' -Subject '(?<subject>(?:[^']|'')*)' -SmtpServer '(?<server>(?:[^']|'')*)'(?: -Encoding UTF8)?(?: -To (?<to>'(?:(?:[^']|'')*)'(?:, '(?:(?:[^']|'')*)')*))?(?: -Cc (?<cc>'(?:(?:[^']|'')*)'(?:, '(?:(?:[^']|'')*)')*))?(?: -Bcc (?<bcc>'(?:(?:[^']|'')*)'(?:, '(?:(?:[^']|'')*)')*))?(?:(?: -BodyAsHtml)? -Body '(?<body>(?:[^']|'')*)')?(?: -Attachments (?<att>'(?:(?:[^']|'')*)'(?:, '(?:(?:[^']|'')*)')*))?(?: -Priority (?<imp>High|Normal|Low))?\s*$");
			if (match.Success)
			{
				EmailAction action = new EmailAction(UnPrep(FromUTF8(match.Groups["subject"].Value)), UnPrep(match.Groups["from"].Value), FromPS(match.Groups["to"]), UnPrep(FromUTF8(match.Groups["body"].Value)), UnPrep(match.Groups["server"].Value))
				{ Cc = FromPS(match.Groups["cc"]), Bcc = FromPS(match.Groups["bcc"]) };
				if (match.Groups["att"].Success)
					action.Attachments = Array.ConvertAll<string, object>(FromPS(match.Groups["att"].Value), s => s);
				if (match.Groups["imp"].Success)
					action.HeaderFields[ImportanceHeader] = match.Groups["imp"].Value;
				return action;
			}
			return null;
		}

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			base.Bind(iTaskDef);
			if (nvc != null)
				nvc.Bind(((V2Interop.IEmailAction)iAction).HeaderFields);
		}

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected override void CopyProperties(Action sourceAction)
		{
			if (sourceAction.GetType() == GetType())
			{
				base.CopyProperties(sourceAction);
				if (((EmailAction)sourceAction).Attachments != null)
					Attachments = (object[])((EmailAction)sourceAction).Attachments.Clone();
				Bcc = ((EmailAction)sourceAction).Bcc;
				Body = ((EmailAction)sourceAction).Body;
				Cc = ((EmailAction)sourceAction).Cc;
				From = ((EmailAction)sourceAction).From;
				if (((EmailAction)sourceAction).nvc != null)
					((EmailAction)sourceAction).HeaderFields.CopyTo(HeaderFields);
				ReplyTo = ((EmailAction)sourceAction).ReplyTo;
				Server = ((EmailAction)sourceAction).Server;
				Subject = ((EmailAction)sourceAction).Subject;
				To = ((EmailAction)sourceAction).To;
			}
		}

		private static string[] FromPS(string p)
		{
			var list = p.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
			return Array.ConvertAll<string, string>(list, i => UnPrep(i).Trim('\''));
		}

		private static string FromPS(System.Text.RegularExpressions.Group g, string delimeter = ";")
		{
			if (g.Success)
				return string.Join(delimeter, FromPS(g.Value));
			return null;
		}

		private static string FromUTF8(string s)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
			return System.Text.Encoding.Default.GetString(bytes);
		}

		private static string Prep(string s) => s?.Replace("'", "''");

		private static string ToPS(string input, char[] delimeters = null)
		{
			if (delimeters == null)
				delimeters = new char[] { ';', ',' };
			return ToPS(Array.ConvertAll<string, string>(input.Split(delimeters), i => Prep(i.Trim())));
		}

		private static string ToPS(string[] input) => string.Join(", ", Array.ConvertAll<string, string>(input, i => string.Concat("'", i.Trim(), "'")));

		private static string ToUTF8(string s)
		{
			if (s == null) return null;
			byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
			return System.Text.Encoding.UTF8.GetString(bytes);
		}

		private static string UnPrep(string s) => s?.Replace("''", "'");

		internal override void CreateV2Action(IActionCollection iActions)
		{
			iAction = iActions.Create(TaskActionType.SendEmail);
		}
	}

	/// <summary>
	/// Represents an action that executes a command-line operation.
	/// </summary>
	[XmlRoot("Exec", Namespace = TaskDefinition.tns, IsNullable = false)]
	public class ExecAction : Action
	{
#if DEBUG
		internal const string PowerShellArgFormat = "-NoExit -Command \"& {{<# {0}:{1} #> {2}}}\"";
#else
		internal const string PowerShellArgFormat = "-NoLogo -NonInteractive -WindowStyle Hidden -Command \"& {{<# {0}:{1} #> {2}}}\"";
#endif
		internal const string PowerShellPath = "powershell";
		internal const string ScriptIdentifer = "TSML_20140424";

		private V1Interop.ITask v1Task;

		/// <summary>
		/// Creates a new instance of an <see cref="ExecAction"/> that can be added to <see cref="TaskDefinition.Actions"/>.
		/// </summary>
		public ExecAction() { }

		/// <summary>
		/// Creates a new instance of an <see cref="ExecAction"/> that can be added to <see cref="TaskDefinition.Actions"/>.
		/// </summary>
		/// <param name="path">Path to an executable file.</param>
		/// <param name="arguments">Arguments associated with the command-line operation. This value can be null.</param>
		/// <param name="workingDirectory">Directory that contains either the executable file or the files that are used by the executable file. This value can be null.</param>
		public ExecAction(string path, string arguments = null, string workingDirectory = null)
		{
			Path = path;
			Arguments = arguments;
			WorkingDirectory = workingDirectory;
		}

		internal ExecAction(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ExecAction(V2Interop.IAction action) : base(action) { }

		/// <summary>
		/// Gets or sets the arguments associated with the command-line operation.
		/// </summary>
		[DefaultValue("")]
		public string Arguments
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetParameters();
				return GetProperty<string, IExecAction>(nameof(Arguments), "");
			}
			set
			{
				if (v1Task != null)
					v1Task.SetParameters(value);
				else
					SetProperty<string, IExecAction>(nameof(Arguments), value);
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the action.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[DefaultValue(null)]
		[XmlAttribute(AttributeName = "id", DataType = "ID")]
		[XmlIgnore]
		public override string Id
		{
			get
			{
				if (v1Task != null)
					return System.IO.Path.GetFileNameWithoutExtension(Task.GetV1Path(v1Task)) + "_Action";
				return base.Id;
			}
			set
			{
				if (v1Task != null)
					throw new NotV1SupportedException();
				base.Id = value;
			}
		}

		/// <summary>
		/// Gets or sets the path to an executable file.
		/// </summary>
		[XmlElement("Command")]
		[DefaultValue("")]
		public string Path
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetApplicationName();
				return GetProperty<string, IExecAction>(nameof(Path), "");
			}
			set
			{
				if (v1Task != null)
					v1Task.SetApplicationName(value);
				else
					SetProperty<string, IExecAction>(nameof(Path), value);
			}
		}

		/// <summary>
		/// Gets or sets the directory that contains either the executable file or the files that are used by the executable file.
		/// </summary>
		[DefaultValue("")]
		public string WorkingDirectory
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetWorkingDirectory();
				return GetProperty<string, IExecAction>(nameof(WorkingDirectory), "");
			}
			set
			{
				if (v1Task != null)
					v1Task.SetWorkingDirectory(value);
				else
					SetProperty<string, IExecAction>(nameof(WorkingDirectory), value);
			}
		}

		internal override bool Bound
		{
			get
			{
				if (v1Task != null)
					return true;
				return base.Bound;
			}
		}

		internal override TaskActionType InternalActionType => TaskActionType.Execute;

		/// <summary>
		/// Gets a value indicating whether this instance is a PowerShell command.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is a PowerShell command; otherwise, <c>false</c>.
		/// </value>
		internal bool IsPowerShellCmd => Path != null && (Path.EndsWith(PowerShellPath, StringComparison.InvariantCultureIgnoreCase) || Path.EndsWith(PowerShellPath + ".exe", StringComparison.InvariantCultureIgnoreCase));

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Action other) => base.Equals(other) && Path == ((ExecAction)other).Path && Arguments == ((ExecAction)other).Arguments && WorkingDirectory == ((ExecAction)other).WorkingDirectory;

		/// <summary>
		/// Gets a string representation of the <see cref="ExecAction"/>.
		/// </summary>
		/// <returns>String representation of this action.</returns>
		public override string ToString() => string.Format(Properties.Resources.ExecAction, Path, Arguments, WorkingDirectory, Id);

		internal string[] GetPowerShellCmd()
		{
			if (IsPowerShellCmd)
			{
				if (Arguments != null && Arguments.Contains(ExecAction.ScriptIdentifer))
				{
					var match = System.Text.RegularExpressions.Regex.Match(Arguments, @"<# " + ExecAction.ScriptIdentifer + ":(?<type>\\w+) #> (?<cmd>.+)}\"$");
					if (match.Success)
						return new string[] { match.Groups["type"].Value, match.Groups["cmd"].Value };
				}
			}
			return null;
		}

		internal static ExecAction AsPowerShellCmd(string actionType, string cmd) => new ExecAction(PowerShellPath, BuildPowerShellCmd(actionType, cmd));

		internal static string BuildPowerShellCmd(string actionType, string cmd) => string.Format(PowerShellArgFormat, ScriptIdentifer, actionType, cmd);

		internal override void Bind(V1Interop.ITask v1Task)
		{
			object o = null;
			unboundValues.TryGetValue("Path", out o);
			v1Task.SetApplicationName(o == null ? string.Empty : o.ToString());
			o = null;
			unboundValues.TryGetValue("Arguments", out o);
			v1Task.SetParameters(o == null ? string.Empty : o.ToString());
			o = null;
			unboundValues.TryGetValue("WorkingDirectory", out o);
			v1Task.SetWorkingDirectory(o == null ? string.Empty : o.ToString());
		}

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected override void CopyProperties(Action sourceAction)
		{
			if (sourceAction.GetType() == GetType())
			{
				base.CopyProperties(sourceAction);
				Path = ((ExecAction)sourceAction).Path;
				Arguments = ((ExecAction)sourceAction).Arguments;
				WorkingDirectory = ((ExecAction)sourceAction).WorkingDirectory;
			}
		}

		internal override void CreateV2Action(IActionCollection iActions)
		{
			iAction = iActions.Create(TaskActionType.Execute);
		}
	}

	/*/// <summary>
	/// An action that will run a windowless PowerShell script command.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	internal sealed class PowerShellAction : ExecAction
	{
		private const string PowerShellActionType = "CustomPS";

		/// <summary>
		/// Initializes a new instance of the <see cref="PowerShellAction" /> class.
		/// </summary>
		public PowerShellAction() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="PowerShellAction" /> class.
		/// </summary>
		/// <param name="command">The PowerShell command.</param>
		public PowerShellAction(string command)
		{
			Command = command;
		}

		/// <summary>
		/// Gets or sets the PowerShell command.
		/// </summary>
		/// <value>
		/// The PowerShell command.
		/// </value>
		public string Command
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		private new string Arguments { get; }

		private new string Path { get; }

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Action other) =>
			base.Equals(other) && Command == ((PowerShellAction)other).Command && WorkingDirectory == ((PowerShellAction)other).WorkingDirectory;

		/// <summary>
		/// Gets a string representation of the <see cref="PowerShellAction"/>.
		/// </summary>
		/// <returns>String representation of this action.</returns>
		public override string ToString() => Command;

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected override void CopyProperties(Action sourceAction)
		{
			if (sourceAction.GetType() == GetType())
			{
				base.CopyProperties(sourceAction);
				Command = ((PowerShellAction)sourceAction).Command;
				WorkingDirectory = ((PowerShellAction)sourceAction).WorkingDirectory;
			}
		}

		internal override void CreateV2Action(IActionCollection iActions)
		{
			iAction = iActions.Create(TaskActionType.Execute);
		}
	}*/

	/// <summary>
	/// Represents an action that shows a message box when a task is activated.
	/// </summary>
	[XmlType(IncludeInSchema = false)]
	[XmlRoot("ShowMessage", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class ShowMessageAction : Action, IBindAsExecAction
	{
		/// <summary>
		/// Creates a new unbound instance of <see cref="ShowMessageAction"/>.
		/// </summary>
		public ShowMessageAction() { }

		/// <summary>
		/// Creates a new unbound instance of <see cref="ShowMessageAction"/>.
		/// </summary>
		/// <param name="messageBody">Message text that is displayed in the body of the message box.</param>
		/// <param name="title">Title of the message box.</param>
		public ShowMessageAction(string messageBody, string title)
		{
			MessageBody = messageBody;
			Title = title;
		}

		internal ShowMessageAction(V2Interop.IAction action) : base(action) { }

		/// <summary>
		/// Gets or sets the message text that is displayed in the body of the message box.
		/// </summary>
		[XmlElement("Body")]
		[DefaultValue(null)]
		public string MessageBody
		{
			get { return GetProperty<string, IShowMessageAction>(nameof(MessageBody)); }
			set { SetProperty<string, IShowMessageAction>(nameof(MessageBody), value); }
		}

		/// <summary>
		/// Gets or sets the title of the message box.
		/// </summary>
		[DefaultValue(null)]
		public string Title
		{
			get { return GetProperty<string, IShowMessageAction>(nameof(Title)); }
			set { SetProperty<string, IShowMessageAction>(nameof(Title), value); }
		}

		internal override TaskActionType InternalActionType => TaskActionType.ShowMessage;

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(Action other) => base.Equals(other) && (this as IBindAsExecAction).GetPowerShellCommand() == (other as IBindAsExecAction).GetPowerShellCommand();

		string IBindAsExecAction.GetPowerShellCommand()
		{
			// [System.Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms'); [System.Windows.Forms.MessageBox]::Show('Your_Desired_Message','Your_Desired_Title')
			var sb = new System.Text.StringBuilder("[System.Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms'); [System.Windows.Forms.MessageBox]::Show('");
			sb.Append(MessageBody.Replace("'", "''"));
			if (Title != null)
			{
				sb.Append("','");
				sb.Append(Title.Replace("'", "''"));
			}
			sb.Append("')");
			return sb.ToString();
		}

		/// <summary>
		/// Gets a string representation of the <see cref="ShowMessageAction"/>.
		/// </summary>
		/// <returns>String representation of this action.</returns>
		public override string ToString() => string.Format(Properties.Resources.ShowMessageAction, Title, MessageBody, Id);

		internal static Action FromPowerShellCommand(string p)
		{
			var match = System.Text.RegularExpressions.Regex.Match(p, @"^\[System.Reflection.Assembly\]::LoadWithPartialName\('System.Windows.Forms'\); \[System.Windows.Forms.MessageBox\]::Show\('(?<msg>(?:[^']|'')*)'(?:,'(?<t>(?:[^']|'')*)')?\)$");
			if (match.Success)
			{
				return new ShowMessageAction(match.Groups["msg"].Value.Replace("''", "'"), match.Groups["t"].Success ? match.Groups["t"].Value.Replace("''", "'") : null);
			}
			return null;
		}

		/// <summary>
		/// Copies the properties from another <see cref="Action"/> the current instance.
		/// </summary>
		/// <param name="sourceAction">The source <see cref="Action"/>.</param>
		protected override void CopyProperties(Action sourceAction)
		{
			if (sourceAction.GetType() == GetType())
			{
				base.CopyProperties(sourceAction);
				Title = ((ShowMessageAction)sourceAction).Title;
				MessageBody = ((ShowMessageAction)sourceAction).MessageBody;
			}
		}

		internal override void CreateV2Action(IActionCollection iActions)
		{
			iAction = iActions.Create(TaskActionType.ShowMessage);
		}
	}
}