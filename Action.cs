using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Microsoft.Win32.TaskScheduler
{
	public enum TaskActionType
	{
		ComHandler = 5,
		Execute = 0,
		SendEmail = 6,
		ShowMessage = 7
	}

	public abstract class Action : IDisposable
	{
		internal V2Interop.IAction iAction = null;
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();

		internal abstract void Bind(V2Interop.ITaskDefinition iTaskDef);

		public virtual void Dispose()
		{
			if (iAction != null)
				Marshal.ReleaseComObject(iAction);
		}

		public virtual string Id
		{
			get { return (iAction == null) ? (string)unboundValues["Id"] : this.iAction.Id; }
			set { if (iAction == null) unboundValues["Id"] = value; else this.iAction.Id = value; }
		}

		internal virtual bool Bound { get { return this.iAction != null; } }

		protected void BindValues()
		{
			foreach (string key in unboundValues.Keys)
			{
				iAction.GetType().InvokeMember(key, System.Reflection.BindingFlags.SetProperty, null, iAction, new object[] { unboundValues[key] });
			}
		}

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

	public class ComHandlerAction : Action
	{
		internal ComHandlerAction(V2Interop.IComHandlerAction action)
		{
			iAction = action;
		}

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			//iAction = iTaskDef.Actions.Create(InternalV2.TaskActionType.ComHandler);
			BindValues();
		}

		public Guid ClassId
		{
			get { return (iAction == null) ? (Guid)unboundValues["ClassId"] : new Guid(((V2Interop.IComHandlerAction)iAction).ClassId); }
			set { if (iAction == null) unboundValues["ClassId"] = value; else ((V2Interop.IComHandlerAction)iAction).ClassId = value.ToString(); }
		}

		public string Data
		{
			get { return (iAction == null) ? (string)unboundValues["Data"] : ((V2Interop.IComHandlerAction)iAction).Data; }
			set { if (iAction == null) unboundValues["Data"] = value; else ((V2Interop.IComHandlerAction)iAction).Data = value; }
		}
	}

	public class ExecAction : Action
	{
		private V1Interop.ITask v1Task;

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

		internal void Bind(V1Interop.ITask v1Task)
		{
			object o;
			if (unboundValues.TryGetValue("Path", out o))
				v1Task.SetApplicationName((string)o);
			if (unboundValues.TryGetValue("Arguments", out o))
				v1Task.SetParameters((string)o);
			if (unboundValues.TryGetValue("WorkingDirectory", out o))
				v1Task.SetWorkingDirectory((string)o);
		}

		internal override void Bind(V2Interop.ITaskDefinition iTaskDef)
		{
			iAction = iTaskDef.Actions.Create(TaskActionType.Execute);
			BindValues();
		}

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
					throw new NotSupportedException();
				base.Id = value;
			}
		}

		public string Path
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetApplicationName();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).Path;
				return (string)unboundValues["Path"];
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

		public string Arguments
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetParameters();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).Arguments;
				return (string)unboundValues["Arguments"];
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

		public string WorkingDirectory
		{
			get
			{
				if (v1Task != null)
					return v1Task.GetWorkingDirectory();
				if (iAction != null)
					return ((V2Interop.IExecAction)iAction).WorkingDirectory;
				return (string)unboundValues["WorkingDirectory"];
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
	}

	public class EmailAction : Action
	{
		internal EmailAction(V2Interop.IEmailAction action)
		{
			iAction = action;
		}

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			//iAction = iTaskDef.Actions.Create(Microsoft.Win32.TaskScheduler.InternalV2.TaskActionType.SendEmail);
			BindValues();
		}

		public string Server
		{
			get { return (iAction == null) ? (string)unboundValues["Server"] : ((V2Interop.IEmailAction)iAction).Server; }
			set { if (iAction == null) unboundValues["Server"] = value; else ((V2Interop.IEmailAction)iAction).Server = value; }
		}

		public string Subject
		{
			get { return (iAction == null) ? (string)unboundValues["Subject"] : ((V2Interop.IEmailAction)iAction).Subject; }
			set { if (iAction == null) unboundValues["Subject"] = value; else ((V2Interop.IEmailAction)iAction).Subject = value; }
		}

		public string To
		{
			get { return (iAction == null) ? (string)unboundValues["To"] : ((V2Interop.IEmailAction)iAction).To; }
			set { if (iAction == null) unboundValues["To"] = value; else ((V2Interop.IEmailAction)iAction).To = value; }
		}

		public string Cc
		{
			get { return (iAction == null) ? (string)unboundValues["Cc"] : ((V2Interop.IEmailAction)iAction).Cc; }
			set { if (iAction == null) unboundValues["Cc"] = value; else ((V2Interop.IEmailAction)iAction).Cc = value; }
		}

		public string Bcc
		{
			get { return (iAction == null) ? (string)unboundValues["Bcc"] : ((V2Interop.IEmailAction)iAction).Bcc; }
			set { if (iAction == null) unboundValues["Bcc"] = value; else ((V2Interop.IEmailAction)iAction).Bcc = value; }
		}

		public string ReplyTo
		{
			get { return (iAction == null) ? (string)unboundValues["ReplyTo"] : ((V2Interop.IEmailAction)iAction).ReplyTo; }
			set { if (iAction == null) unboundValues["ReplyTo"] = value; else ((V2Interop.IEmailAction)iAction).ReplyTo = value; }
		}

		public string From
		{
			get { return (iAction == null) ? (string)unboundValues["From"] : ((V2Interop.IEmailAction)iAction).From; }
			set { if (iAction == null) unboundValues["From"] = value; else ((V2Interop.IEmailAction)iAction).From = value; }
		}

		/*public Microsoft.Win32.TaskScheduler.InternalV2.ITaskNamedValueCollection HeaderFields
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}*/

		public string Body
		{
			get { return (iAction == null) ? (string)unboundValues["Body"] : ((V2Interop.IEmailAction)iAction).Body; }
			set { if (iAction == null) unboundValues["Body"] = value; else ((V2Interop.IEmailAction)iAction).Body = value; }
		}

		public object[] Attachments
		{
			get { return (iAction == null) ? (object[])unboundValues["Attachments"] : ((V2Interop.IEmailAction)iAction).Attachments; }
			set { if (iAction == null) unboundValues["Attachments"] = value; else ((V2Interop.IEmailAction)iAction).Attachments = value; }
		}
	}

	public class ShowMessageAction : Action
	{
		public ShowMessageAction(string messageBody, string title)
		{
			this.MessageBody = messageBody;
			this.Title = title;
		}

		internal ShowMessageAction(V2Interop.IShowMessageAction action)
		{
			iAction = action;
		}

		internal override void Bind(Microsoft.Win32.TaskScheduler.V2Interop.ITaskDefinition iTaskDef)
		{
			//iAction = iTaskDef.Actions.Create(Microsoft.Win32.TaskScheduler.InternalV2.TaskActionType.ShowMessage);
			BindValues();
		}

		public string Title
		{
			get { return (iAction == null) ? (string)unboundValues["Title"] : ((V2Interop.IShowMessageAction)iAction).Title; }
			set { if (iAction == null) unboundValues["Title"] = value; else ((V2Interop.IShowMessageAction)iAction).Title = value; }
		}

		public string MessageBody
		{
			get { return (iAction == null) ? (string)unboundValues["MessageBody"] : ((V2Interop.IShowMessageAction)iAction).MessageBody; }
			set { if (iAction == null) unboundValues["MessageBody"] = value; else ((V2Interop.IShowMessageAction)iAction).MessageBody = value; }
		}
	}

	public sealed class ActionCollection : ICollection<Action>, IDisposable
	{
		private V1Interop.ITask v1Task;
		private V2Interop.IActionCollection v2Coll;

		internal ActionCollection(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ActionCollection(V2Interop.ITaskDefinition iTaskDef)
		{
			v2Coll = iTaskDef.Actions;
		}

		public void Dispose()
		{
			v1Task = null;
			v2Coll = null;
		}

		public void Add(Action action) { throw new NotImplementedException(); }

		public Action AddNew(TaskActionType actionType)
		{
			if (v1Task != null)
				return new ExecAction(v1Task);

			return Action.CreateAction(v2Coll.Create(actionType));
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(Action item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Action[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Remove(Action item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Action> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public class Enumerator : IEnumerator<Action>
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
