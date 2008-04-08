using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Defines the type of actions a task can perform.
	/// </summary>
	/// <remarks>The action type is defined when the action is created and cannot be changed later. See <see cref="ActionCollection.AddNew"/>.</remarks>
	public enum TaskActionType
	{
		/// <summary>This action fires a handler.</summary>
		ComHandler = 5,
		/// <summary>This action performs a command-line operation. For example, the action can run a script, launch an executable, or, if the name of a document is provided, find its associated application and launch the application with the document.</summary>
		Execute = 0,
		/// <summary>This action sends and e-mail.</summary>
		SendEmail = 6,
		/// <summary>This action shows a message box.</summary>
		ShowMessage = 7
	}

	/// <summary>
	/// Abstract base class that provides the common properties that are inherited by all action objects. An action object is created by the <see cref="ActionCollection.AddNew"/> method.
	/// </summary>
	public abstract class Action : IDisposable
	{
		internal V2Interop.IAction iAction = null;

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual bool Bound { get { return this.iAction != null; } }

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual void Bind(V1Interop.ITask iTask)
		{
		}

		/// <summary>In testing and may change. Do not use until officially introduced into library.</summary>
		internal virtual void Bind(V2Interop.ITaskDefinition iTaskDef)
		{
			V2Interop.IActionCollection iActions = iTaskDef.Actions;
			switch (this.GetType().Name)
			{
				case "ComHandlerAction":
					iAction = iActions.Create(TaskActionType.ComHandler);
					break;
				case "ExecAction":
					iAction = iActions.Create(TaskActionType.Execute);
					break;
				case "EmailAction":
					iAction = iActions.Create(TaskActionType.SendEmail);
					break;
				case "ShowMessageAction":
					iAction = iActions.Create(TaskActionType.ShowMessage);
					break;
				default:
					throw new ArgumentException();
			}
			Marshal.ReleaseComObject(iActions);
			foreach (string key in unboundValues.Keys)
			{
				try
				{
					iAction.GetType().InvokeMember(key, System.Reflection.BindingFlags.SetProperty, null, iAction, new object[] { unboundValues[key] });
				}
				catch (System.Reflection.TargetInvocationException tie) { throw tie.InnerException; }
				catch { }
			}
			unboundValues.Clear();
			unboundValues = null;
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
		/// Gets or sets the identifier of the action.
		/// </summary>
		public virtual string Id
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Id") ? (string)unboundValues["Id"] : null) : this.iAction.Id; }
			set { if (iAction == null) unboundValues["Id"] = value; else this.iAction.Id = value; }
		}

		/// <summary>
		/// Returns the action Id.
		/// </summary>
		/// <returns>String representation of action.</returns>
		public override string ToString()
		{
			return this.Id;
		}

		/// <summary>
		/// Creates a specialized class from a defined interface.
		/// </summary>
		/// <param name="iAction">Version 2.0 Action interface.</param>
		/// <returns>Specialized action class</returns>
		internal static Action CreateAction(V2Interop.IAction iAction)
		{
			switch (iAction.Type)
			{
				case TaskActionType.ComHandler:
					return new ComHandlerAction((V2Interop.IComHandlerAction)iAction);
				case TaskActionType.SendEmail:
					return new EmailAction((V2Interop.IEmailAction)iAction);
				case TaskActionType.ShowMessage:
					return new ShowMessageAction((V2Interop.IShowMessageAction)iAction);
				case TaskActionType.Execute:
				default:
					return new ExecAction((V2Interop.IExecAction)iAction);
			}
		}
	}

	/// <summary>
	/// Represents an action that fires a handler. Only available on Task Scheduler 2.0.
	/// </summary>
	public sealed class ComHandlerAction : Action
	{
		/// <summary>
		/// Creates an unbound instance of <see cref="ComHandlerAction"/>.
		/// </summary>
		/// <param name="classId">Identifier of the handler class.</param>
		/// <param name="data">Addition data associated with the handler.</param>
		public ComHandlerAction(Guid classId, string data)
		{
			this.ClassId = classId;
			this.Data = data;
		}

		internal ComHandlerAction(V2Interop.IComHandlerAction action)
		{
			iAction = action;
		}

		/// <summary>
		/// Gets or sets the identifier of the handler class.
		/// </summary>
		public Guid ClassId
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("ClassId") ? (Guid)unboundValues["ClassId"] : Guid.Empty) : new Guid(((V2Interop.IComHandlerAction)iAction).ClassId); }
			set { if (iAction == null) unboundValues["ClassId"] = value.ToString(); else ((V2Interop.IComHandlerAction)iAction).ClassId = value.ToString(); }
		}

		/// <summary>
		/// Gets or sets additional data that is associated with the handler.
		/// </summary>
		public string Data
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Data") ? (string)unboundValues["Data"] : null) : ((V2Interop.IComHandlerAction)iAction).Data; }
			set { if (iAction == null) unboundValues["Data"] = value; else ((V2Interop.IComHandlerAction)iAction).Data = value; }
		}

		/// <summary>
		/// Gets a string representation of the <see cref="ComHandlerAction"/>.
		/// </summary>
		/// <returns>String represention this action.</returns>
		public override string ToString()
		{
			return string.Format("Custom Handler\t{0}", this.ClassId);
		}
	}

	/// <summary>
	/// Represents an action that executes a command-line operation.
	/// </summary>
	public sealed class ExecAction : Action
	{
		private V1Interop.ITask v1Task;

		/// <summary>
		/// Creates a new instance of an <see cref="ExecAction"/> that can be added to <see cref="TaskDefinition.Actions"/>.
		/// </summary>
		/// <param name="path">Path to an executable file.</param>
		/// <param name="arguments">Arguments associated with the command-line operation. This value can be null.</param>
		/// <param name="workingDirectory">Directory that contains either the executable file or the files that are used by the executable file. This value can be null.</param>
		public ExecAction(string path, string arguments, string workingDirectory)
		{
			this.Path = path;
			this.Arguments = arguments;
			this.WorkingDirectory = workingDirectory;
		}

		internal ExecAction(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ExecAction(V2Interop.IExecAction action)
		{
			iAction = action;
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

		internal override void Bind(V1Interop.ITask v1Task)
		{
			object o;
			if (unboundValues.TryGetValue("Path", out o) && o != null)
				v1Task.SetApplicationName((string)o);
			if (unboundValues.TryGetValue("Arguments", out o) && o != null)
				v1Task.SetParameters((string)o);
			if (unboundValues.TryGetValue("WorkingDirectory", out o) && o != null)
				v1Task.SetWorkingDirectory((string)o);
		}

		/// <summary>
		/// Gets or sets the identifier of the action.
		/// </summary>
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
		public string Path
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetApplicationName();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).Path;
				return unboundValues.ContainsKey("Path") ? (string)unboundValues["Path"] : null;
			}
			set
			{
				if (v1Task != null)
					v1Task.SetApplicationName(value);
				else if (iAction != null)
					((V2Interop.IExecAction)iAction).Path = value;
				else
					unboundValues["Path"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the arguments associated with the command-line operation.
		/// </summary>
		public string Arguments
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetParameters();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).Arguments;
				return unboundValues.ContainsKey("Arguments") ? (string)unboundValues["Arguments"] : null;
			}
			set
			{
				if (v1Task != null)
					v1Task.SetParameters(value);
				else if (iAction != null)
					((V2Interop.IExecAction)iAction).Arguments = value;
				else
					unboundValues["Arguments"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the directory that contains either the executable file or the files that are used by the executable file.
		/// </summary>
		public string WorkingDirectory
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetWorkingDirectory();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).WorkingDirectory;
				return unboundValues.ContainsKey("WorkingDirectory") ? (string)unboundValues["WorkingDirectory"] : null;
			}
			set
			{
				if (v1Task != null)
					v1Task.SetWorkingDirectory(value);
				else if (iAction != null)
					((V2Interop.IExecAction)iAction).WorkingDirectory = value;
				else
					unboundValues["WorkingDirectory"] = value;
			}
		}

		/// <summary>
		/// Gets a string representation of the <see cref="ExecAction"/>.
		/// </summary>
		/// <returns>String represention this action.</returns>
		public override string ToString()
		{
			return string.Format("Start a program\t{0} {1}", this.Path, this.Arguments);
		}
	}

	/// <summary>
	/// Represents an action that sends an e-mail.
	/// </summary>
	public sealed class EmailAction : Action
	{
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
			this.Subject = subject;
			this.From = from;
			this.To = to;
			this.Body = body;
			this.Server = mailServer;
		}

		internal EmailAction(V2Interop.IEmailAction action)
		{
			iAction = action;
		}

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			base.Bind(iTaskDef);
			if (nvc != null)
				nvc.Bind(((V2Interop.IEmailAction)iAction).HeaderFields);
		}

		/// <summary>
		/// Gets or sets the name of the server that you use to send e-mail from.
		/// </summary>
		public string Server
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Server") ? (string)unboundValues["Server"] : null) : ((V2Interop.IEmailAction)iAction).Server; }
			set { if (iAction == null) unboundValues["Server"] = value; else ((V2Interop.IEmailAction)iAction).Server = value; }
		}

		/// <summary>
		/// Gets or sets the subject of the e-mail.
		/// </summary>
		public string Subject
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Subject") ? (string)unboundValues["Subject"] : null) : ((V2Interop.IEmailAction)iAction).Subject; }
			set { if (iAction == null) unboundValues["Subject"] = value; else ((V2Interop.IEmailAction)iAction).Subject = value; }
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to send the e-mail to.
		/// </summary>
		public string To
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("To") ? (string)unboundValues["To"] : null) : ((V2Interop.IEmailAction)iAction).To; }
			set { if (iAction == null) unboundValues["To"] = value; else ((V2Interop.IEmailAction)iAction).To = value; }
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to Cc in the e-mail.
		/// </summary>
		public string Cc
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Cc") ? (string)unboundValues["Cc"] : null) : ((V2Interop.IEmailAction)iAction).Cc; }
			set { if (iAction == null) unboundValues["Cc"] = value; else ((V2Interop.IEmailAction)iAction).Cc = value; }
		}

		/// <summary>
		/// Gets or sets the e-mail address or addresses that you want to Bcc in the e-mail.
		/// </summary>
		public string Bcc
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Bcc") ? (string)unboundValues["Bcc"] : null) : ((V2Interop.IEmailAction)iAction).Bcc; }
			set { if (iAction == null) unboundValues["Bcc"] = value; else ((V2Interop.IEmailAction)iAction).Bcc = value; }
		}

		/// <summary>
		/// Gets or sets the e-mail address that you want to reply to.
		/// </summary>
		public string ReplyTo
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("ReplyTo") ? (string)unboundValues["ReplyTo"] : null) : ((V2Interop.IEmailAction)iAction).ReplyTo; }
			set { if (iAction == null) unboundValues["ReplyTo"] = value; else ((V2Interop.IEmailAction)iAction).ReplyTo = value; }
		}

		/// <summary>
		/// Gets or sets the e-mail address that you want to send the e-mail from.
		/// </summary>
		public string From
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("From") ? (string)unboundValues["From"] : null) : ((V2Interop.IEmailAction)iAction).From; }
			set { if (iAction == null) unboundValues["From"] = value; else ((V2Interop.IEmailAction)iAction).From = value; }
		}

		private NamedValueCollection nvc = null;

		/// <summary>
		/// Gets or sets the header information in the e-mail message to send.
		/// </summary>
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
		/// Gets or sets the body of the e-mail that contains the e-mail message.
		/// </summary>
		public string Body
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Body") ? (string)unboundValues["Body"] : null) : ((V2Interop.IEmailAction)iAction).Body; }
			set { if (iAction == null) unboundValues["Body"] = value; else ((V2Interop.IEmailAction)iAction).Body = value; }
		}

		/// <summary>
		/// Gets or sets an array of attachments that is sent with the e-mail.
		/// </summary>
		public object[] Attachments
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Attachments") ? (object[])unboundValues["Attachments"] : null) : ((V2Interop.IEmailAction)iAction).Attachments; }
			set { if (iAction == null) unboundValues["Attachments"] = value; else ((V2Interop.IEmailAction)iAction).Attachments = value; }
		}

		/// <summary>
		/// Gets a string representation of the <see cref="EmailAction"/>.
		/// </summary>
		/// <returns>String represention this action.</returns>
		public override string ToString()
		{
			return string.Format("Send an e-mail\t{1} {0}", this.Subject, this.To);
		}
	}

	/// <summary>
	/// Represents an action that shows a message box when a task is activated.
	/// </summary>
	public sealed class ShowMessageAction : Action
	{
		/// <summary>
		/// Creates a new unbound instance of <see cref="ShowMessageAction"/>.
		/// </summary>
		/// <param name="messageBody">Message text that is displayed in the body of the message box.</param>
		/// <param name="title">Title of the message box.</param>
		public ShowMessageAction(string messageBody, string title)
		{
			this.MessageBody = messageBody;
			this.Title = title;
		}

		internal ShowMessageAction(V2Interop.IShowMessageAction action)
		{
			iAction = action;
		}

		/// <summary>
		/// Gets or sets the title of the message box.
		/// </summary>
		public string Title
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("Title") ? (string)unboundValues["Title"] : null) : ((V2Interop.IShowMessageAction)iAction).Title; }
			set { if (iAction == null) unboundValues["Title"] = value; else ((V2Interop.IShowMessageAction)iAction).Title = value; }
		}

		/// <summary>
		/// Gets or sets the message text that is displayed in the body of the message box.
		/// </summary>
		public string MessageBody
		{
			get { return (iAction == null) ? (unboundValues.ContainsKey("MessageBody") ? (string)unboundValues["MessageBody"] : null) : ((V2Interop.IShowMessageAction)iAction).MessageBody; }
			set { if (iAction == null) unboundValues["MessageBody"] = value; else ((V2Interop.IShowMessageAction)iAction).MessageBody = value; }
		}

		/// <summary>
		/// Gets a string representation of the <see cref="ShowMessageAction"/>.
		/// </summary>
		/// <returns>String represention this action.</returns>
		public override string ToString()
		{
			return string.Format("Show a message\t{0}", this.Title);
		}
	}

	/// <summary>
	/// Collection that contains the actions that are performed by the task.
	/// </summary>
	/// <remarks>A Task Scheduler 1.0 task can only contain a single <see cref="ExecAction"/>.</remarks>
	public sealed class ActionCollection : IEnumerable<Action>, IDisposable
	{
		private V1Interop.ITask v1Task;
		private V2Interop.ITaskDefinition v2Def;
		private V2Interop.IActionCollection v2Coll;

		internal ActionCollection(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ActionCollection(V2Interop.ITaskDefinition iTaskDef)
		{
			v2Def = iTaskDef;
			v2Coll = iTaskDef.Actions;
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			v1Task = null;
			v2Def = null;
			v2Coll = null;
		}

		/// <summary>
		/// Adds an action to the task.
		/// </summary>
		/// <param name="action">A derived <see cref="Action"/> class.</param>
		public Action Add(Action action)
		{
			if (v2Def != null)
				action.Bind(v2Def);
			else
				action.Bind(v1Task);
			return action;
		}

		/// <summary>
		/// Adds a new <see cref="Action"/> instance to the task.
		/// </summary>
		/// <param name="actionType">Type of task to be created</param>
		/// <returns>Specialized <see cref="Action"/> instance.</returns>
		public Action AddNew(TaskActionType actionType)
		{
			if (v1Task != null)
				return new ExecAction(v1Task);

			return Action.CreateAction(v2Coll.Create(actionType));
		}

		/// <summary>
		/// Clears all actions from the task.
		/// </summary>
		public void Clear()
		{
			if (v2Coll != null)
				v2Coll.Clear();
			else
			{
				for (int i = this.Count - 1; i >= 0; i--)
					RemoveAt(i);
			}
		}

		/// <summary>
		/// Removes the action at a specified index.
		/// </summary>
		/// <param name="index">Index of action to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
		public void RemoveAt(int index)
		{
			if (index >= this.Count)
				throw new ArgumentOutOfRangeException("index", index, "Failed to remove Trigger. Index out of range.");
			if (v2Coll != null)
				v2Coll.Remove(++index);
			else
				throw new NotV1SupportedException("There can be only a single action and it cannot be removed.");
		}

		/// <summary>
		/// Gets a specified action from the collection.
		/// </summary>
		/// <param name="index">The index of the action to be retrieved.</param>
		/// <returns>Specialized <see cref="Action"/> instance.</returns>
		public Action this[int index]
		{
			get
			{
				if (v2Coll != null)
					return Action.CreateAction(v2Coll[++index]);
				if (index == 0)
					return new ExecAction(v1Task.GetApplicationName(), v1Task.GetParameters(), v1Task.GetWorkingDirectory());
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the principal for the task.
		/// </summary>
		public string Context
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Context;
				return string.Empty;
			}
			set
			{
				if (v2Coll != null)
					v2Coll.Context = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets the number of actions in the collection.
		/// </summary>
		public int Count
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Count;
				return 1;
			}
		}

		/// <summary>
		/// Gets or sets an XML-formatted version of the collection.
		/// </summary>
		public string XmlText
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.XmlText;
				throw new NotV1SupportedException();
			}
			set
			{
				if (v2Coll != null)
					v2Coll.XmlText = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Retrieves an enumeration of each of the actions.
		/// </summary>
		/// <returns>Returns an object that implements the <see cref="IEnumerator"/> interface and that can iterate through the <see cref="Action"/> objects within the <see cref="ActionCollection"/>.</returns>
		public IEnumerator<Action> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		internal class Enumerator : IEnumerator<Action>
		{
			private V1Interop.ITask v1Task;
			private int v1Pos = -1;
			private IEnumerator v2Enum;
			private ActionCollection parent;

			internal Enumerator(V1Interop.ITask task)
			{
				v1Task = task;
			}

			internal Enumerator(ActionCollection iColl)
			{
				parent = iColl;
				if (iColl.v2Coll != null)
					v2Enum = iColl.v2Coll.GetEnumerator();
			}

			public Action Current
			{
				get
				{
					if (v2Enum != null)
					{
						V2Interop.IAction iAction = v2Enum.Current as V2Interop.IAction;
						if (iAction != null)
							return Action.CreateAction(iAction);
					}
					if (v1Pos == 0)
						return new ExecAction(v1Task.GetApplicationName(), v1Task.GetParameters(), v1Task.GetWorkingDirectory());
					throw new InvalidOperationException();
				}
			}

			/// <summary>
			/// Releases all resources used by this class.
			/// </summary>
			public void Dispose()
			{
				v1Task = null;
				v2Enum = null;
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				if (v2Enum != null)
					return v2Enum.MoveNext();
				return ++v1Pos == 0;
			}

			public void Reset()
			{
				if (v2Enum != null)
					v2Enum.Reset();
				v1Pos = -1;
			}
		}
	}

}
