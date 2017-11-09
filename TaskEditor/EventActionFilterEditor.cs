using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler.EditorProperties;
using Microsoft.Win32.TaskScheduler.Events;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Dialog that enables editing of event queries, specifically for creating filters.</summary>
	public partial class EventActionFilterEditor : Form
	{
		private static readonly Regex IDRegex;
		private static StringNode Logs;
		private static readonly LogTimeItem[] LogTimeBaseItems;
		private static List<string> Providers;

		private bool internalSet;
		private int lastLogTimeComboIndex;
		private EventQuery ql = new EventQuery();
		private bool queryTextIsDirty;
		private string subscription;

		static EventActionFilterEditor()
		{
			IDRegex = new Regex(@"^-?\d+(-\d+)?(,-?\d+(-\d+)?)*$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
			LogTimeBaseItems = new LogTimeItem[7];
			LogTimeBaseItems[0] = new LogTimeItem { Text = Resources.EventLogTimeAnyTime, DiffTime = TimeSpan.Zero };
			LogTimeBaseItems[1] = new LogTimeItem { Text = Resources.EventLogTimeHour, DiffTime = TimeSpan.FromHours(1) };
			LogTimeBaseItems[2] = new LogTimeItem { Text = Resources.EventLogTime12Hours, DiffTime = TimeSpan.FromHours(12) };
			LogTimeBaseItems[3] = new LogTimeItem { Text = Resources.EventLogTimeDay, DiffTime = TimeSpan.FromHours(24) };
			LogTimeBaseItems[4] = new LogTimeItem { Text = Resources.EventLogTimeWeek, DiffTime = TimeSpan.FromDays(7) };
			LogTimeBaseItems[5] = new LogTimeItem { Text = Resources.EventLogTime30Days, DiffTime = TimeSpan.FromDays(30) };
			LogTimeBaseItems[6] = new LogTimeItem { Text = Resources.EventLogTimeCustom };
		}

		/// <summary>Initializes a new instance of the <see cref="EventActionFilterEditor"/> class.</summary>
		public EventActionFilterEditor()
		{
			InitializeComponent();
			// Logged
			logTimeCombo.Items.AddRange(LogTimeBaseItems);
			logTimeCombo.SelectedIndex = 0;
			// Event level
			criticalLevelCheckBox.Tag = new[] { 1 };
			errorLevelCheckBox.Tag = new[] { 2 };
			warningLevelCheckBox.Tag = new[] { 3 };
			infoLevelCheckBox.Tag = new[] { 0, 4 };
			verboseLevelCheckBox.Tag = new[] { 5 };
			// Text boxes
			eventIDsText.Tag = eventIDsText.Text;
			userText.Tag = userText.Text;
			computerText.Tag = computerText.Text;
			// Logs and providers
			eventLogCombo.CheckAllText = Resources.EventLogsAll;
			UpdateLogList(eventLogCombo.Items);
			eventSourceCombo.CheckAllText = Resources.EventSourcesAll;
			UpdateProviderList(eventSourceCombo.Items);
			eventSourceCombo.Sorted = true;
			// Categories
			categoryCombo.CheckAllText = Resources.EventTasksAll;
			// Keywords
			keywordsCombo.CheckAllText = Resources.EventKeywordsAll;
			keywordsCombo.InitializeFromEnum(typeof(StandardEventKeywords), Resources.ResourceManager, "EventKeywords", new[] { "None", "CorrelationHint" });
			keywordsCombo.Sorted = true;
			ResetFilterControls();
		}

		/// <summary>Initializes a new instance of the <see cref="EventActionFilterEditor"/> class.</summary>
		/// <param name="eventSubscription">The event query XML used to initialize the dialog.</param>
		public EventActionFilterEditor(string eventSubscription) : this()
		{
			Subscription = eventSubscription;
		}

		/// <summary>Gets or sets the event query XML. Setting this value will reinitialize the dialog.</summary>
		/// <value>The event query XML.</value>
		public string Subscription
		{
			get => subscription;
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					subscription = null;
					ql = new EventQuery();
				}
				else
				{
					try
					{
						ql = EventQuery.Deserialize(value);
						subscription = EventQuery.Serialize(ql);
						Initialize();
					}
					catch
					{
						subscription = value;
					}
				}
			}
		}

		private static void UpdateLogList(TreeNodeCollection nodes, string targetServer = null)
		{
			if (Logs == null)
			{
				Logs = new StringNode(null);
				// Add standard nodes
				var std = new StringNode(Resources.EventLogParentStandard);
				string[] stdLogs = { "Application", "Security", "Setup", "System", "ForwardedEvents" };
				foreach (var s in stdLogs)
					std.Nodes.Add(new StringNode(s, s));
				std.LastChild.Text = "Forwarded Events";
				Logs.Nodes.Add(std);
				// Get all event logs and remove standard ones
				var list = SystemEventEnumerator.GetEventLogStrings(targetServer);
				foreach (var s in stdLogs)
					list.Remove(s);
				// Add app nodes
				StringNode curCompare = null, appNode = new StringNode(Resources.EventLogParentApps);
				Logs.Nodes.Add(appNode);
				var max = 0;
				var partList = list.ConvertAll(s => { var a = s.Split('-', '/', '\\'); max = Math.Max(max, a.Length); return a; });
				//partList.Sort((a, b) => string.Compare(a[0], b[0]));
				for (var i = 0; i < partList.Count; i++)
				{
					var lastParent = appNode;
					for (var j = 0; j < partList[i].Length; j++)
					{
						if (curCompare != null && string.Compare(curCompare, partList[i][j], true) == 0)
						{
							lastParent = curCompare;
							curCompare = curCompare.LastChild;
						}
						else
						{
							var sn = new StringNode(partList[i][j]);
							if (j == partList[i].Length - 1)
								sn.Path = list[i];
							lastParent.Nodes.Add(sn);
							lastParent = sn;
						}
					}
					curCompare = appNode.LastChild;
				}
			}
			Logs.UpdateTreeView(nodes);
		}

		private static void UpdateProviderList(CheckedListBox.ObjectCollection items, string targetServer = null)
		{
			if (Providers == null)
			{
				Providers = SystemEventEnumerator.GetEventProviders(targetServer, null, true);
			}
			items.Clear();
			items.AddRange(Providers.ConvertAll(s => { var p = s.Split('|'); return new ListControlItem(p[1], p[0]); }).ToArray());
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void categoryCombo_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				ql.Query.Tasks.Clear();
				foreach (var item in categoryCombo.SelectedItems)
					if (item.Value is int i)
						ql.Query.Tasks.Add(i);
			}
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			ResetFilterControls();
		}

		private void dataBtn_Click(object sender, EventArgs e)
		{
			using (var dlg = new EventActionFilterDataEditor())
			{
				dlg.DataItems = new Dictionary<string, string>(ql.Query.Data);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					ql.Query.Data = new Dictionary<string, string>(dlg.DataItems);
				}
			}
		}

		private void editManuallyCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (internalSet)
			{
				queryText.Enabled = editManuallyCheckBox.Checked;
			}
		}

		private void EnableControls()
		{
			var enableCtrls = eventLogCombo.CheckedItems.Count > 0;
			eventLogCombo.Enabled = enableCtrls || byLogRadio.Checked;
			eventSourceCombo.Enabled = eventIDsText.Enabled = keywordsCombo.Enabled =
				userText.Enabled = computerText.Enabled = enableCtrls;
			foreach (var t in new[] { eventIDsText, userText, computerText })
				nullableText_Leave(t, EventArgs.Empty);
		}

		private void eventIDsText_TextChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				var match = false;
				if (eventIDsText.TextLength == 0 || (match = IDRegex.IsMatch(eventIDsText.Text)) || Equals(eventIDsText.Text, eventIDsText.Tag))
				{
					errorProvider.SetError(eventIDsText, String.Empty);
					if (match)
						ql.Query.IDString = eventIDsText.Text;
				}
				else
					errorProvider.SetError(eventIDsText, Resources.Error_EventTriggerIDInvalid);
			}
		}

		private void eventLogCombo_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				ql.Query.Select.Clear();
				var selLogs = eventLogCombo.CheckedItems.FindAll(n => !string.IsNullOrEmpty(n.Name)).ConvertAll(n => n.Name);
				foreach (var log in selLogs)
					ql.Query.AddPath(log);
				EnableControls();
			}
		}

		private void eventSourceCombo_SelectedItemsChanged(object sender, EventArgs e)
		{
			var provs = Array.ConvertAll(eventSourceCombo.SelectedItems, i => (string)i.Value);
			if (!internalSet)
			{
				ql.Query.Providers = new List<string>(provs);
				if (eventLogCombo.CheckedItems.Count == 0)
				{
					var logs = SystemEventEnumerator.GetLogsForProviders(null, provs);
					foreach (var log in logs)
						eventLogCombo.CheckValue(log);
					EnableControls();
				}
			}
			var tasks = SystemEventEnumerator.GetEventTasks(null, provs);
			if (tasks.Count == 0)
				categoryCombo.Enabled = false;
			else
			{
				categoryCombo.Enabled = true;
				categoryCombo.BeginUpdate();
				categoryCombo.Items.Clear();
				categoryCombo.Items.Add(new ListControlItem(categoryCombo.CheckAllText));
				categoryCombo.Items.AddRange(tasks.ConvertAll(kv => new ListControlItem(kv.Value, kv.Key)).ToArray());
				categoryCombo.EndUpdate();
			}
		}

		private void Initialize()
		{
			internalSet = true;

			try
			{
				// Log time
				InitLogTime(ql.Query.Times);
				// Level
				var ctrls = new[] { criticalLevelCheckBox, errorLevelCheckBox, warningLevelCheckBox, infoLevelCheckBox, verboseLevelCheckBox };
				foreach (var cb in ctrls)
					for (var i = 0; i < ((int[])cb.Tag).Length; i++)
						cb.Checked = ql.Query.Levels.Contains(((int[])cb.Tag)[i]);
				// Logs
				eventLogCombo.UncheckAllItems();
				foreach (var s in ql.Query.Select)
					eventLogCombo.CheckValue(s.Path);
				// Providers
				eventSourceCombo.CheckItems(o => ql.Query.Providers.FindIndex(s => Equals(((ListControlItem)o).Value, s)) != -1);
				byLogRadio.Checked = ql.Query.Providers.Count == 0;
				bySourceRadio.Checked = ql.Query.Providers.Count > 0;
				// EventIDs
				eventIDsText.Text = ql.Query.IDString;
				// Tasks
				categoryCombo.CheckItems(o => ql.Query.Tasks.FindIndex(i => Equals(((ListControlItem)o).Value, i)) != -1);
				// Keywords
				keywordsCombo.CheckedFlagValue = ql.Query.Keywords;
				// User
				userText.Text = ql.Query.User;
				// Computer
				computerText.Text = string.Join(",", ql.Query.Computers.ToArray());
			}
			catch { }

			internalSet = false;
			EnableControls();
		}

		private void InitLogTime(EventQuery.CQuery.CTimeCreated time)
		{
			var selIdx = -1;
			if (time == null)
			{
				selIdx = 0;
			}
			else if (!time.HasDates)
			{
				for (var i = 0; i < LogTimeBaseItems.Length; i++)
					if (LogTimeBaseItems[i].DiffTime == time.span)
					{
						selIdx = i;
						break;
					}
				if (selIdx == -1)
					throw new ArgumentOutOfRangeException(nameof(time), "Unable to identify selection option for specified log time.");
			}
			else
			{
				var x = (time.low.HasValue ? 1 : 0) | (time.high.HasValue ? 2 : 0);
				string resStr;
				switch (x)
				{
					case 1: resStr = Resources.EventLogTimeCustomFrom; break;
					case 2: resStr = Resources.EventLogTimeCustomTo; break;
					case 3: resStr = Resources.EventLogTimeCustomFromTo; break;
					default: resStr = null; break;
				}
				if (resStr == null)
					selIdx = 0;
				else
				{
					var txt = string.Format(resStr, time.low, time.high);
					if (logTimeCombo.Items.Count == 7)
						logTimeCombo.Items.Insert(6, txt);
					else
						logTimeCombo.Items[6] = txt;
					selIdx = 6;
				}
			}
			logTimeCombo.SelectedIndex = selIdx;
		}

		private void keywordsCombo_SelectedItemsChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				ql.Query.Keywords = keywordsCombo.CheckedFlagValue;
			}
		}

		private void level_checkedChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				ql.Query.Levels.Clear();
				var ctrls = new[] { criticalLevelCheckBox, errorLevelCheckBox, warningLevelCheckBox, infoLevelCheckBox, verboseLevelCheckBox };
				foreach (var cb in ctrls)
					if (cb.Checked) ql.Query.Levels.AddRange((int[])cb.Tag);
			}
		}

		private void logTimeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!internalSet)
			{
				var isCustom = logTimeCombo.SelectedIndex == logTimeCombo.Items.Count - 1;
				var isInserted = logTimeCombo.Items.Count == 8 && logTimeCombo.SelectedIndex == 6;
				if (isCustom)
				{
					using (var dlg = new EventActionFilterTimeEditor())
					{
						if (ql.Query.Times != null && ql.Query.Times.HasDates) { dlg.FromDateTime = ql.Query.Times.low; dlg.ToDateTime = ql.Query.Times.high; }
						if (dlg.ShowDialog(this) == DialogResult.OK)
						{
							ql.Query.Times = new EventQuery.CQuery.CTimeCreated(dlg.FromDateTime, dlg.ToDateTime);
							InitLogTime(ql.Query.Times);
						}
						else
							logTimeCombo.SelectedIndex = lastLogTimeComboIndex;
					}
				}
				else if (isInserted) { }
				else
				{
					lastLogTimeComboIndex = logTimeCombo.SelectedIndex;
					if (logTimeCombo.Items.Count == 8) logTimeCombo.Items.RemoveAt(6);
					ql.Query.Times = new EventQuery.CQuery.CTimeCreated(((LogTimeItem)logTimeCombo.Items[lastLogTimeComboIndex]).DiffTime);
				}
			}
		}

		private void nullableText_Enter(object sender, EventArgs e)
		{
			if (!(sender is TextBox tb)) return;
			if (Equals(tb.Text, tb.Tag))
				tb.Clear();
		}

		private void nullableText_Leave(object sender, EventArgs e)
		{
			if (!(sender is TextBox tb)) return;
			if (tb.TextLength == 0)
				tb.Text = (string)tb.Tag;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab == xmlTab && !UpdateQLFromText()) return;
			subscription = EventQuery.Serialize(ql);
			DialogResult = DialogResult.OK;
			Close();
		}

		private void queryText_DragDrop(object sender, DragEventArgs e)
		{
			if (!queryText.Enabled || queryText.ReadOnly)
				return;

			try
			{
				var text = string.Empty;
				if (e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					var files = (string[])e.Data.GetData(DataFormats.FileDrop);
					text = File.ReadAllText(files[0]);
				}
				else if (e.Data.GetDataPresent(DataFormats.Text, true))
				{
					text = e.Data.GetData(DataFormats.Text, true).ToString();
				}
				if (!text.StartsWith("<QueryList>", true, CultureInfo.CurrentCulture))
					throw new FormatException();
				queryText.Text = text;
			}
			catch
			{
				MessageBox.Show(this, Resources.Error_InvalidQueryFormat, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void queryText_DragEnter(object sender, DragEventArgs e)
		{
			if (queryText.Enabled && !queryText.ReadOnly && e.Data.GetDataPresent(DataFormats.Text, true) || e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void queryText_TextChanged(object sender, EventArgs e)
		{
			queryTextIsDirty = true;
		}

		private void radio_CheckedChanged(object sender, EventArgs e)
		{
			eventLogCombo.Enabled = byLogRadio.Checked || eventLogCombo.CheckedItems.Count > 0;
			eventSourceCombo.Enabled = bySourceRadio.Checked || eventSourceCombo.SelectedItems.Length > 0;
		}

		private void ResetFilterControls()
		{
			criticalLevelCheckBox.Checked = warningLevelCheckBox.Checked = verboseLevelCheckBox.Checked =
				errorLevelCheckBox.Checked = infoLevelCheckBox.Checked = false;
			logTimeCombo.SelectedIndex = 0;
			eventIDsText.Text = userText.Text = computerText.Text = string.Empty;
			foreach (var t in new[] { eventIDsText, userText, computerText })
				nullableText_Leave(t, EventArgs.Empty);
			eventLogCombo.UncheckAllItems();
			eventSourceCombo.UncheckAllItems();
			categoryCombo.UncheckAllItems();
			keywordsCombo.UncheckAllItems();
			byLogRadio.Checked = true;
			eventLogCombo.Enabled = true;
		}

		private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPage != xmlTab)
			{
				e.Cancel = !UpdateQLFromText();
			}
		}

		private bool UpdateQLFromText(bool initForm = true)
		{
			if (!queryTextIsDirty) return true;
			try
			{
				ql = EventQuery.Deserialize(queryText.Text);
				queryTextIsDirty = false;
				if (initForm)
					Initialize();
			}
			catch (Exception ex)
			{
				var s = Resources.Error_EventFilterBadQuery;
				if (ex is InvalidOperationException && ex.InnerException is InvalidOperationException && ex.InnerException.Data.Contains("Remaining text"))
					s += string.Format("\r\n" + Resources.Error_EventFilterBadQueryText, ex.InnerException.Data["Remaining text"].ToString());
				MessageBox.Show(this, s, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private void wrapCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			queryText.WordWrap = wrapCheckBox.Checked;
		}

		private void xmlTab_Enter(object sender, EventArgs e)
		{
			if (queryTextIsDirty) return;
			queryText.Text = EventQuery.Serialize(ql);
			queryTextIsDirty = false;
		}

		private void xmlTab_Leave(object sender, EventArgs e)
		{
		}

		private class LogTimeItem
		{
			public TimeSpan DiffTime;
			public string Text;

			public override string ToString() => Text;
		}

		private class StringNode
		{
			public readonly List<StringNode> Nodes = new List<StringNode>(0);
			public string Path;
			public string Text;

			public StringNode(string text, string path = null)
			{
				Text = text;
				Path = path;
			}

			internal StringNode LastChild => Nodes.Count == 0 ? null : Nodes[Nodes.Count - 1];

			public static implicit operator string(StringNode n) => n.Text;

			public void UpdateTreeView(TreeNodeCollection nodes)
			{
				nodes.Clear();
				UpdateNodes(Nodes, nodes);
				foreach (TreeNode n in nodes)
					n.Expand();
			}

			private static void UpdateNodes(List<StringNode> nodes, TreeNodeCollection coll)
			{
				foreach (var item in nodes)
				{
					try
					{
						var n = DropDownCheckTree.AddValue(coll, item.Text, item.Path);
						UpdateNodes(item.Nodes, n.Nodes);
					}
					catch (Exception e) { Debug.WriteLine($"Unable to add node '{item.Text}': {e.Message}"); }
				}
			}
		}
	}
}