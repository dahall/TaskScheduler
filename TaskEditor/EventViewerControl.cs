using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Control that displays a TaskEvent.
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

		/// <summary>Gets or sets the task event.</summary>
		/// <value>The task event.</value>
		[DefaultValue(null)]
		public TaskEvent TaskEvent
		{
			get { return taskEvent; }
			set
			{
				taskEvent = value;
				if (value != null)
				{
					computerText.Text = taskEvent.EventRecord.MachineName;
					detailsText.Text = taskEvent.EventRecord.FormatDescription();
					eventIdText.Text = taskEvent.EventRecord.Id.ToString();
					System.Collections.Generic.List<string> keywords = new System.Collections.Generic.List<string>();
					foreach (string s in taskEvent.EventRecord.KeywordsDisplayNames)
						keywords.Add(s);
					keywordsText.Text = string.Join(", ", keywords.ToArray());
					levelText.Text = taskEvent.EventRecord.LevelDisplayName;
					loggedText.Text = taskEvent.EventRecord.TimeCreated.Value.ToString("G");
					logNameText.Text = taskEvent.EventRecord.LogName;
					opCodeText.Text = $"{taskEvent.EventRecord.OpcodeDisplayName} ({taskEvent.EventRecord.Opcode})";
					sourceText.Text = "TaskScheduler";
					taskCategoryText.Text = taskEvent.TaskCategory;
					userText.Text = taskEvent.UserId.Translate(typeof(System.Security.Principal.NTAccount)).Value;
					if (textViewRadio.Checked)
						ShowHtml();
					else
						ShowXml();
				}
				else
				{
					computerText.Clear();
					detailsText.Clear();
					eventIdText.Clear();
					htmlText.DocumentText = string.Empty;
					keywordsText.Clear();
					levelText.Clear();
					loggedText.Clear();
					logNameText.Clear();
					opCodeText.Clear();
					sourceText.Clear();
					taskCategoryText.Clear();
					userText.Clear();
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
		[DefaultValue(0)]
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
					if (xslt == null)
					{
						xslt = new XslCompiledTransform();
						xslt.Load(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "EventViewer_EventDetails.xsl");
					}
					XmlDocument document = new XmlDocument();
					document.LoadXml(taskEvent.EventRecord.ToXml());
					if (tmpHtmlFile == null)
						tmpHtmlFile = GetTempFile("html");
					StreamWriter writer = File.CreateText(tmpHtmlFile);
					xslt.Transform((System.Xml.XPath.IXPathNavigable)document, null, (TextWriter)writer);
					writer.Close();
					htmlText.Navigate(tmpHtmlFile);
				}
				catch (FileNotFoundException)
				{
					xslt = null;
					htmlText.DocumentText = string.Format(htmlFmt, "EventProperties_XSLT_not_found", "", "red");
				}
				catch (XsltException)
				{
					xslt = null;
					htmlText.DocumentText = string.Format(htmlFmt, "EventProperties_XSLT_error", "", "red");
				}
				catch (XmlException)
				{
					xslt = null;
					UnicodeEncoding encoding = new UnicodeEncoding();
					byte[] bytes = encoding.GetBytes(string.Format(htmlFmt, "InvalidEventXml", taskEvent.EventRecord.ToXml(), "black"));
					htmlText.DocumentText = encoding.GetString(bytes);
				}
			}
			else
			{
				htmlText.DocumentText = string.Empty;
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
					tmpXmlFile = GetTempFile("xml");
				XmlDocument document = new XmlDocument();
				document.LoadXml(taskEvent.EventRecord.ToXml());
				document.Save(tmpXmlFile);
				htmlText.Navigate(tmpXmlFile);
			}
			else
			{
				htmlText.DocumentText = string.Empty;
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
			htmlText.Document.ExecCommand("Copy", false, null);
		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			htmlText.Document.ExecCommand("SelectAll", false, null);
		}
	}
}
