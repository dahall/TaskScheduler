using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog allowing the viewing of a task event.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog allowing the viewing of a task event.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(true)]
	public partial class EventViewerDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private TaskEvent curEvent;

		/// <summary>
		/// Initializes a new instance of the <see cref="EventViewerDialog"/> class.
		/// </summary>
		public EventViewerDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes the dialog with the specified task event.
		/// </summary>
		/// <param name="taskEvent">The task event.</param>
		public void Initialize(TaskEvent taskEvent)
		{
			CurrentEvent = taskEvent;
		}

		private TaskEvent CurrentEvent
		{
			get { return curEvent; }
			set
			{
				curEvent = value;
				this.Text = string.Format(EditorProperties.Resources.EventPropertiesDialogTitle, curEvent.EventId);
				eventViewerControl1.TaskEvent = curEvent;
			}
		}

		private void copyBtn_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Log Name:      {0}\r\n", curEvent.EventRecord.LogName);
			sb.AppendFormat("Source:        {0}\r\n", "TaskScheduler");
			sb.AppendFormat("Date:          {0:U}\r\n", curEvent.EventRecord.TimeCreated);
			sb.AppendFormat("Event ID:      {0}\r\n", curEvent.EventRecord.Id);
			sb.AppendFormat("Task Category: {0}\r\n", curEvent.EventRecord.TaskDisplayName);
			sb.AppendFormat("Level:         {0}\r\n", curEvent.EventRecord.LevelDisplayName);
			sb.AppendFormat("Keywords:      {0}\r\n", curEvent.EventRecord.Keywords);
			sb.AppendFormat("User:          {0}\r\n", curEvent.UserId.Translate(typeof(System.Security.Principal.NTAccount)).Value);
			sb.AppendFormat("Computer:      {0}\r\n", curEvent.EventRecord.MachineName);
			sb.AppendFormat("Description:\r\n{0}\r\n", curEvent.EventRecord.FormatDescription());
			sb.Append("Event Xml:\r\n");
			sb.Append(curEvent.EventRecord.ToXml());
			Clipboard.SetText(sb.ToString(), TextDataFormat.Text);
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void prevBtn_Click(object sender, EventArgs e)
		{

		}

		private void nextBtn_Click(object sender, EventArgs e)
		{

		}
	}
}
