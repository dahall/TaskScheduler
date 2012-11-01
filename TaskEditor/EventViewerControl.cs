using System.Windows.Forms;
using System.Text;
using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Control that displays a <see cref="TaskEvent"/>.
	/// </summary>
	internal partial class EventViewerControl : UserControl
	{
		private TaskEvent taskEvent;
		private string tmpXmlFile, tmpHtmlFile;
		private XslCompiledTransform xslt;

		/// <summary>
		/// Initializes a new instance of the <see cref="EventViewerControl"/> class.
		/// </summary>
		public EventViewerControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the task event.
		/// </summary>
		/// <value>
		/// The task event.
		/// </value>
		public TaskEvent TaskEvent
		{
			get { return taskEvent; }
			set
			{
				this.taskEvent = value;
				if (value != null)
				{
					this.computerText.Text = taskEvent.EventRecord.MachineName;
					this.detailsText.Text = taskEvent.EventRecord.FormatDescription();
					this.eventIdText.Text = taskEvent.EventRecord.Id.ToString();
					System.Collections.Generic.List<string> keywords = new System.Collections.Generic.List<string>();
					foreach (string s in taskEvent.EventRecord.KeywordsDisplayNames)
						keywords.Add(s);
					this.keywordsText.Text = string.Join(", ", keywords.ToArray());
					this.levelText.Text = taskEvent.EventRecord.LevelDisplayName;
					this.loggedText.Text = taskEvent.EventRecord.TimeCreated.Value.ToString("G");
					this.logNameText.Text = taskEvent.EventRecord.LogName;
					this.opCodeText.Text = string.Format("{0} ({1})", taskEvent.EventRecord.OpcodeDisplayName, taskEvent.EventRecord.Opcode);
					this.sourceText.Text = "TaskScheduler";
					this.taskCategoryText.Text = taskEvent.TaskCategory;
					this.userText.Text = taskEvent.UserId.Translate(typeof(System.Security.Principal.NTAccount)).Value;
					if (this.textViewRadio.Checked)
						ShowHtml();
					else
						ShowXml();
				}
				else
				{
					this.computerText.Clear();
					this.detailsText.Clear();
					this.eventIdText.Clear();
					this.htmlText.DocumentText = string.Empty;
					this.keywordsText.Clear();
					this.levelText.Clear();
					this.loggedText.Clear();
					this.logNameText.Clear();
					this.opCodeText.Clear();
					this.sourceText.Clear();
					this.taskCategoryText.Clear();
					this.userText.Clear();
				}
			}
		}

		/// <summary>
		/// Available tabs within the control
		/// </summary>
		public enum EventViewerActiveTab
		{
			/// <summary>General tab</summary>
			General,
			/// <summary>Details tab</summary>
			Details
		}

		/// <summary>
		/// Gets or sets the active tab.
		/// </summary>
		public EventViewerActiveTab ActiveTab
		{
			get { return (EventViewerActiveTab)tabControl1.SelectedIndex; }
			set { tabControl1.SelectedIndex = (int)value; }
		}

		public new void Dispose()
		{
			base.Dispose();
			if (tmpXmlFile != null)
				File.Delete(tmpXmlFile);
			if (tmpHtmlFile != null)
				File.Delete(tmpHtmlFile);
		}

		private void ShowHtml()
		{
			const string htmlFmt = "<html><body><b><font color=\"{2}\">{0}</font></b><br/>{1}</body></html>";
			if (!string.IsNullOrEmpty(taskEvent.EventRecord.ToXml()))
			{
				try
				{
					if (this.xslt == null)
					{
						xslt = new XslCompiledTransform();
						this.xslt.Load(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "EventViewer_EventDetails.xsl");
					}
					XmlDocument document = new XmlDocument();
					document.LoadXml(taskEvent.EventRecord.ToXml());
					if (tmpHtmlFile == null)
						this.tmpHtmlFile = GetTempFile("html");
					StreamWriter writer = File.CreateText(this.tmpHtmlFile);
					this.xslt.Transform((System.Xml.XPath.IXPathNavigable)document, null, (TextWriter)writer);
					writer.Close();
					this.htmlText.Navigate(this.tmpHtmlFile);
				}
				catch (FileNotFoundException)
				{
					this.xslt = null;
					this.htmlText.DocumentText = string.Format(htmlFmt, "EventProperties_XSLT_not_found", "", "red");
				}
				catch (XsltException)
				{
					this.xslt = null;
					this.htmlText.DocumentText = string.Format(htmlFmt, "EventProperties_XSLT_error", "", "red");
				}
				catch (XmlException)
				{
					this.xslt = null;
					UnicodeEncoding encoding = new UnicodeEncoding();
					byte[] bytes = encoding.GetBytes(string.Format(htmlFmt, "InvalidEventXml", taskEvent.EventRecord.ToXml(), "black"));
					this.htmlText.DocumentText = encoding.GetString(bytes);
				}
			}
			else
			{
				this.htmlText.DocumentText = string.Empty;
			}
		}

		private string GetTempFile(string ext)
		{
			string temp = Path.GetTempFileName();
			temp = Path.Combine(Path.GetDirectoryName(temp), Path.GetFileNameWithoutExtension(temp) + "." + ext);
			File.Create(temp).Close();
			return temp;
		}

		private void ShowXml()
		{
			if (!string.IsNullOrEmpty(taskEvent.EventRecord.ToXml()))
			{
				if (tmpXmlFile == null)
					this.tmpXmlFile = GetTempFile("xml");
				XmlDocument document = new XmlDocument();
				document.LoadXml(taskEvent.EventRecord.ToXml());
				document.Save(this.tmpXmlFile);
				this.htmlText.Navigate(this.tmpXmlFile);
			}
			else
			{
				this.htmlText.DocumentText = string.Empty;
			}
		}

		private void infoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			const string fmt = @"http://www.bing.com/search?q=%22Microsoft-Windows-TaskScheduler%22+%22Event+ID+{0}%22";
			System.Diagnostics.Process.Start(string.Format(fmt, taskEvent.EventId));
		}

		private void textViewRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			ShowHtml();
		}

		private void xmlViewRadio_CheckedChanged(object sender, System.EventArgs e)
		{
			ShowXml();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.htmlText.Document.ExecCommand("Copy", false, null);
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.htmlText.Document.ExecCommand("SelectAll", false, null);
		}
	}
}
