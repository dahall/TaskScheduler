using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Split-panel that displays a list of all events associated with a task with a hidable detail pane.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog), "TaskControl")]
	public partial class TaskHistoryControl : UserControl
	{
		private long historyEventCount = 0;
		private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
		private Task task;
		private int selectedIndex = -1;
		private IList<ListViewItem> vcache = new System.Collections.Generic.SparseArray<ListViewItem>();
		private TaskEventEnumerator vevEnum;
		private TaskEventLog vlog;

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskHistoryControl"/> class.
		/// </summary>
		public TaskHistoryControl()
		{
			InitializeComponent();
			ShowErrors = true;
			historyListView.ListViewItemSorter = lvwColumnSorter;
			historyListView.VirtualMode = true;
			historyListView.ColumnContextMenuStrip = columnContextMenu;
			if (historyListImages.Images.Count == 0)
			{
				historyListImages.Images.Add(EditorProperties.Resources.empty, Color.FromArgb(0xff, 0, 0xff));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.critical, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.error, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.warning, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.info, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.verbose, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.auditSuccess, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.auditFail, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.filter, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.refresh, 0x10, 0x10));
				historyListImages.Images.Add(new Icon(EditorProperties.Resources.end, 0x10, 0x10));
				historyFilterIcon.ImageIndex = 8;
				historyClearBtn.ImageIndex = 9;
				refreshBtn.ImageIndex = 9;
			}
		}

		/// <summary>
		/// Gets or sets the maximum query count. Use -1 for all or infinite.
		/// </summary>
		/// <value>
		/// The maximum query count.
		/// </value>
		[DefaultValue(-1), Category("Behavior"), Description("Determines maximum number of history items to retrieve.")]
		public int MaxQueryCount { get; set; } = -1;

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value><c>true</c> if errors are shown; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors { get; set; }

		/// <summary>
		/// Gets or sets the task used to retrieve the history for this control.
		/// </summary>
		/// <value>
		/// The task whose history is displayed.
		/// </value>
		[DefaultValue(null), Browsable(false)]
		public Task Task
		{
			get { return task; }
			set
			{
				task = value;
				historyDetailView.ActiveTab = EventViewerControl.EventViewerActiveTab.General;
				historySplitContainer.Panel2Collapsed = false;
				if (value != null)
					vlog = CreateLogInstance();
				RefreshHistory();
			}
		}

		/// <summary>
		/// Activates this instance. Call when the control receives initial focus or needs to refresh.
		/// </summary>
		/// <param name="t">The <see cref="Task"/> for which to get the history.</param>
		[Obsolete("The Activate method is being deprecated. Use the Task property instead.")]
		public void Activate(Task t)
		{
			Task = t;
		}

		/// <summary>
		/// Refreshes the history list of the control using current sorting and grouping settings.
		/// </summary>
		public void RefreshHistory()
		{
			historyListView.Cursor = Cursors.WaitCursor;
			historyListView.VirtualListSize = 0;
			historyListView.Refresh();
			historyHeader_Refresh(-1);
			historyDetailView.TaskEvent = null;
			selectedIndex = -1;
			vcache.Clear();
			historyBackgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			// TODO: Read last status from user options
			// Get column names and add them to the context menu
			var c = historyListView.Columns;
			int insIdx = columnContextMenu.Items.IndexOf(columnContextMenuBreak);
			for (int i = 0; i < c.Count; i++)
			{
				var tsi = new ToolStripMenuItem(c[i].Text, null, columnContextMenuColumnHeaderItem_onClick) { Checked = c[i].Width > 0, Tag = c[i].Index };
				columnContextMenu.Items.Insert(++insIdx, tsi);
			}
		}

		private ListViewItem BuildItem(TaskEvent item)
		{
			var kwds = new List<string>(item.EventRecord.KeywordsDisplayNames);
			return new ListViewItem(new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(),
				item.TaskCategory, item.OpCode, item.ActivityId.ToString(), string.Join(", ", kwds.ToArray()), "TaskScheduler",
				item.UserId.Translate(typeof(System.Security.Principal.NTAccount)).ToString(), item.EventRecord.LogName, 
				item.EventRecord.MachineName, item.ProcessId.ToString(), item.EventRecord.ThreadId.ToString() },
				item.EventRecord.Level.GetValueOrDefault(0)) { Tag = item };
		}

		private void columnContextMenuColumnHeaderItem_onClick(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			int cIndex = (int)item.Tag;
			item.Checked = !item.Checked;
			if (item.Checked)
			{
				historyListView.Columns[cIndex].Width = 100; // TODO: Be more accurate here and get a good standard width
			}
			else
				historyListView.Columns[cIndex].Width = 0;
			PersistColumnSettings();
		}

		private TaskEventLog CreateLogInstance() => new TaskEventLog(task.TaskService.TargetServer, task.Path);

		private void FetchEnumEvents(int startIndex, int endIndex)
		{
			int n = startIndex;
			vevEnum.Seek(System.IO.SeekOrigin.Begin, n);
			while (vevEnum.MoveNext() && n <= endIndex)
				vcache[n++] = BuildItem(vevEnum.Current);
		}

		private void histDetailHideBtn_Click(object sender, EventArgs e)
		{
			historySplitContainer.Panel2Collapsed = true;
		}

		private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				if (!lvwColumnSorter.Group && lvwColumnSorter.SortColumn == 1)
				{
					e.Result = null;
					((BackgroundWorker)sender).ReportProgress(100, vlog.Count);
				}
				else
				{
					TaskEventLog log = CreateLogInstance();
					List<ListViewItem> list;
					if (MaxQueryCount == -1)
						list = new List<TaskEvent>(log).ConvertAll(te => BuildItem(te));
					else
					{
						list = new List<ListViewItem>();
						int i = 0;
						foreach (var te in log)
						{
							if (++i > MaxQueryCount)
								break;
							list.Add(BuildItem(te));
						}
					}
					list.Sort(lvwColumnSorter);
					e.Result = list;
					((BackgroundWorker)sender).ReportProgress(100, log.Count);
				}
			}
			catch (Exception ex) { e.Result = ex; }
		}

		private void historyBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState is long)
			{
				historyListView.VirtualListSize = (int)Math.Min(Int32.MaxValue, (long)e.UserState);
				historyHeader_Refresh((long)e.UserState);
			}
		}

		private void historyBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled || !IsHandleCreated)
				return;
			historyListView.Cursor = Cursors.Default;
			historyListView.BeginUpdate();
			if (e.Error != null || e.Result is Exception)
			{
				historyHeader_Refresh(0L);
				var exc = (e.Result as Exception) ?? e.Error;
				if (exc != null && ShowErrors)
					MessageBox.Show(this, string.Format(EditorProperties.Resources.Error_CannotRetrieveHistory, exc.Message), EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (e.Result == null)
			{
				historyListView.Items.Clear();
				historyListView.VirtualMode = true;
				vcache = new SparseArray<ListViewItem>();
				vevEnum = vlog.GetEnumerator(lvwColumnSorter.Order == SortOrder.Ascending) as TaskEventEnumerator;
			}
			else
			{
				historyListView.VirtualMode = false;
				vevEnum = null;
				vcache = (IList<ListViewItem>)e.Result;
				historyListView.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
				if (lvwColumnSorter.Group)
					SetupGroups();
				else
					historyListView.ShowGroups = false;
			}
			historyListView.EndUpdate();
			historyListView.Focus();
		}

		private void SetupGroups()
		{
			historyListView.Groups.Clear();
			int col = lvwColumnSorter.SortColumn;
			ListViewGroupEx g = new ListViewGroupEx();
			for (int i = 0; i < historyListView.Items.Count; i++)
			{
				var lvi = historyListView.Items[i];
				string cText = lvi.SubItems[col].Text;
				if (cText != g.Header)
				{
					g = historyListView.Groups.Add(cText);
					g.Collapsible = true;
					g.Collapsed = false;
				}
				g.Items.Add(lvi);
			}
			historyListView.ShowGroups = true;
		}

		private void historyHeader_Refresh(long cnt)
		{
			if (historyEventCount != cnt)
			{
				historyEventCount = cnt;
				historyHeader.Text = cnt == -1 ? EditorProperties.Resources.LoadingPrompt : string.Format(EditorProperties.Resources.HistoryHeader, cnt);
			}
		}

		private void historyListView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
		{
			if (vcache[e.StartIndex] == null && vevEnum != null)
			{
				FetchEnumEvents(e.StartIndex, e.EndIndex);
			}
		}

		private void historyListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			lvwColumnSorter.ResortOnColumn(e.Column);
			historyListView.SetSortIcon(lvwColumnSorter.SortColumn, lvwColumnSorter.Order);
			RefreshHistory();
		}

		private void historyListView_ColumnReordered(object sender, ColumnReorderedEventArgs e)
		{
			PersistColumnSettings();
		}

		private void historyListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			PersistColumnSettings();
		}

		private void historyListView_DoubleClick(object sender, EventArgs e)
		{
			if (selectedIndex != -1)
			{
				ListViewItem lvi = vcache[selectedIndex];
				if (lvi != null)
				{
					EventViewerDialog dlg = new EventViewerDialog();
					dlg.Initialize(lvi.Tag as TaskEvent, null); //TaskService == null ? new TaskEventLog(task.Path) : new TaskEventLog(TaskService.TargetServer, task.Path));
					dlg.ShowDialog(this);
				}
			}
		}

		private void historyListView_MouseClick(object sender, MouseEventArgs e)
		{
			var lvi = historyListView.GetItemAt(e.X, e.Y);
			if (lvi != null && e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				listContextMenu.Show(historyListView, e.Location);
			}
		}

		private void historyListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			ListViewItem item = (e.ItemIndex >= 0 && e.ItemIndex < vcache.Count) ? vcache[e.ItemIndex] : null;
			//System.Diagnostics.Debug.WriteLine(string.Format("RetrieveLVI: InCache={0}, Msg={1}", item!=null, Environment.StackTrace));
			if (item == null && vevEnum != null)
			{
				FetchEnumEvents(e.ItemIndex, e.ItemIndex);
				item = vcache[e.ItemIndex];
			}
			if (item != null)
			{
				e.Item = item;
				if (historyListView.SelectedIndices.Count == 0)
					historyListView.SelectedIndices.Add(e.ItemIndex);
			}
		}

		private void historyListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (historyListView.SelectedIndices.Count > 0)
			{
				int newSelIdx = historyListView.SelectedIndices[0];
				SelectItemChanged(newSelIdx);
			}
			else
			{
				selectedIndex = -1;
				historyDetailView.TaskEvent = null;
				historyDetailTitleText.Text = string.Empty;
			}
		}

		private void PersistColumnSettings()
		{
			// TODO: Figure out how to persist column settings
		}
		
		private void SelectItemChanged(int newSelIdx)
		{
			if (selectedIndex != newSelIdx)
			{
				selectedIndex = newSelIdx;
				ListViewItem lvi = vcache[selectedIndex];
				if (lvi != null)
				{
					TaskEvent ev = lvi.Tag as TaskEvent;
					historyDetailView.TaskEvent = ev;
					historyDetailTitleText.Text = ev == null ? string.Empty : string.Format(EditorProperties.Resources.EventDetailHeader, ev.EventId);
				}
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RefreshHistory();
		}

		private void eventPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			historyListView_DoubleClick(null, EventArgs.Empty);
		}

		private void attachTaskToThisEventToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedIndex != -1)
			{
				var taskEvent = (TaskEvent)vcache[selectedIndex].Tag;
				if (taskEvent != null)
				{
					var td = task.TaskService.NewTask();
					var eventId = taskEvent.EventId;
					td.Triggers.Add(new EventTrigger("Microsoft-Windows-TaskScheduler/Operational", "TaskScheduler", eventId));
					td.Actions.Add(new ExecAction());
					using (var wiz = new TaskSchedulerWizard(task.TaskService, td, true))
					{
						wiz.AvailableTriggers = TaskSchedulerWizard.AvailableWizardTriggers.Event;
						wiz.AvailableActions = TaskSchedulerWizard.AvailableWizardActions.Execute;
						wiz.AvailablePages = TaskSchedulerWizard.AvailableWizardPages.IntroPage | TaskSchedulerWizard.AvailableWizardPages.SecurityPage | TaskSchedulerWizard.AvailableWizardPages.SummaryPage | TaskSchedulerWizard.AvailableWizardPages.TriggerEditPage | TaskSchedulerWizard.AvailableWizardPages.ActionEditPage;
						wiz.TaskName = $"Microsoft-Windows-TaskScheduler_Operational_Microsoft-Windows-TaskScheduler_{eventId}";
						wiz.ShowDialog(this);
					}
				}
			}
		}

		private void saveAllEventsAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				// TODO: Save event list
			}
		}

		private static Converter<ColumnHeader, string> cdel = delegate(ColumnHeader c) { return c.Text; };

		private static List<ColumnHeader> GetColumnHeaderList(ListView.ColumnHeaderCollection col)
		{
			var l = new List<ColumnHeader>(col.Count);
			for (int i = 0; i < col.Count; i++)
				l.Add(col[i]);
			return l;
		}

		private static List<string> GetColumnHeaderTextList(ListView.ColumnHeaderCollection col) => GetColumnHeaderList(col).ConvertAll<string>(cdel);

		private void addremoveColumnsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var cols = GetColumnHeaderList(historyListView.Columns);
			var allHeaders = cols.ConvertAll<string>(cdel).ToArray();
			var shownHeaders = cols.FindAll(delegate(ColumnHeader c) { return c.Width > 0; });
			shownHeaders.Sort(delegate(ColumnHeader a, ColumnHeader b) { return a.DisplayIndex.CompareTo(b.DisplayIndex); });
			using (ListColumnEditor colEd = new ListColumnEditor(allHeaders, allHeaders, shownHeaders.ConvertAll<string>(cdel).ToArray()))
			{
				if (colEd.ShowDialog(this) == DialogResult.OK)
				{
					// TODO: Reorder columns
				}
			}
		}

		private void sortEventsByThisColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
			{
				int col = (int)item.Tag;
				historyListView_ColumnClick(historyListView, new ColumnClickEventArgs(col));
				removeSortingToolStripMenuItem.Visible = true;
			}
		}

		private void removeSortingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			historyListView_ColumnClick(historyListView, new ColumnClickEventArgs(1));
			removeSortingToolStripMenuItem.Visible = false;
		}

		private void groupEventsByThisColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			if (item != null)
			{
				int col = (int)item.Tag;
				lvwColumnSorter.Group = true;
				if (lvwColumnSorter.SortColumn != col || vevEnum != null)
					historyListView_ColumnClick(historyListView, new ColumnClickEventArgs(col));
				else
					SetupGroups();
				groupEventsByThisColumnToolStripMenuItem.Visible = false;
				removeGroupingOfEventsToolStripMenuItem.Visible = expandAllGroupsToolStripMenuItem.Visible = collapseAllGroupsToolStripMenuItem.Visible = true;
			}
		}

		private void removeGroupingOfEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lvwColumnSorter.Group = false;
			groupEventsByThisColumnToolStripMenuItem.Visible = true;
			removeGroupingOfEventsToolStripMenuItem.Visible = expandAllGroupsToolStripMenuItem.Visible = collapseAllGroupsToolStripMenuItem.Visible = false;
		}

		private void expandAllGroupsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var item in historyListView.Groups)
				item.Collapsed = false;
		}

		private void collapseAllGroupsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var item in historyListView.Groups)
				item.Collapsed = true;
		}
	}
}