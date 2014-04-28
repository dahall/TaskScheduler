extern alias GrpCtrlDLL;
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
	public partial class TaskHistoryControl : UserControl
	{
		private long historyEventCount = 0;
		private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
		private Task task;
#if NET_35_OR_GREATER
		private int selectedIndex = -1;
		private GrpCtrlDLL::System.Collections.Generic.SparseArray<ListViewItem> vcache = new GrpCtrlDLL::System.Collections.Generic.SparseArray<ListViewItem>();
		private TaskEventEnumerator vevEnum;
		private TaskEventLog vlog;
		private int sortedColumn = -1;
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskHistoryControl"/> class.
		/// </summary>
		public TaskHistoryControl()
		{
			InitializeComponent();
			this.ShowErrors = true;
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
		public Task Task
		{
			get { return this.task; }
			set
			{
				this.task = value;
				historyDetailView.ActiveTab = EventViewerControl.EventViewerActiveTab.General;
				historySplitContainer.Panel2Collapsed = false;
#if NET_35_OR_GREATER
				vlog = new TaskEventLog(task.TaskService.TargetServer, task.Path);
#endif
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
			this.Task = t;
		}

		public void RefreshHistory()
		{
			historyListView.Cursor = Cursors.WaitCursor;
			historyListView.Items.Clear();
			historyHeader_Refresh(-1);
#if NET_35_OR_GREATER
			historyDetailView.TaskEvent = null;
			selectedIndex = -1;
			vcache.Clear();
			vevEnum = vlog.GetEnumerator() as TaskEventEnumerator;
#endif
			historyBackgroundWorker.RunWorkerAsync();
		}

#if NET_35_OR_GREATER
		private ListViewItem BuildItem(TaskEvent item)
		{
			return new ListViewItem(new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(),
				item.TaskCategory, item.OpCode, item.ActivityId.ToString() }, item.EventRecord.Level.GetValueOrDefault(0)) { Tag = item };
		}
#endif

		private void histDetailHideBtn_Click(object sender, EventArgs e)
		{
			historySplitContainer.Panel2Collapsed = true;
		}

		private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
#if NET_35_OR_GREATER
				TaskEventLog log = new TaskEventLog(task.TaskService.TargetServer, task.Path);
				e.Result = log.Count;
#else
				e.Result = new PlatformNotSupportedException(EditorProperties.Resources.Error_EventsNotSupported);
#endif
			}
			catch (Exception ex) { e.Result = ex; }
		}

		private void historyBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			historyListView.Cursor = Cursors.Default;
			if (e.Result is long)
			{
				historyListView.VirtualListSize = (int)Math.Min(Int32.MaxValue, (long)e.Result);
				historyHeader_Refresh((long)e.Result);
			}
			else
			{
				historyHeader_Refresh(0L);
				if (e.Result is Exception && ShowErrors)
					MessageBox.Show(this, string.Format(EditorProperties.Resources.Error_CannotRetrieveHistory, ((Exception)e.Result).Message), EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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
#if NET_35_OR_GREATER
			if (vcache[e.StartIndex] == null)
			{
				if (lvwColumnSorter.SortColumn == 0)
				{
					if (vevEnum == null)
						vevEnum = vlog.GetEnumerator(lvwColumnSorter.Order == SortOrder.Descending) as TaskEventEnumerator;
					if (vevEnum != null)
					{
						vevEnum.Seek(System.IO.SeekOrigin.Begin, e.StartIndex);
						int n = e.StartIndex;
						while (vevEnum.MoveNext() && n <= e.EndIndex)
							vcache[n++] = BuildItem(vevEnum.Current);
					}
				}
				else
				{

				}
			}
#endif
		}

		private void historyListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			historyListView.Clear();
			vcache.Clear();
			if (e.Column == lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				lvwColumnSorter.Order = lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
		}

		private void historyListView_DoubleClick(object sender, EventArgs e)
		{
#if NET_35_OR_GREATER
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
#endif
		}

		private void historyListView_MouseClick(object sender, MouseEventArgs e)
		{
			var lvi = historyListView.GetItemAt(e.X, e.Y);
			if (lvi != null)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					listContextMenu.Show(historyListView, e.Location);
				}
			}
		}

		private void historyListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
#if NET_35_OR_GREATER
			ListViewItem item = vcache[e.ItemIndex];
			//System.Diagnostics.Debug.WriteLine(string.Format("RetrieveLVI: InCache={0}, Msg={1}", item!=null, Environment.StackTrace));
			if (item == null && vevEnum != null)
			{
				vevEnum.Seek(System.IO.SeekOrigin.Begin, e.ItemIndex);
				if (vevEnum.MoveNext())
				{
					item = BuildItem(vevEnum.Current);
					vcache[e.ItemIndex] = item;
				}
			}
			if (item != null)
			{
				e.Item = item;
				if (selectedIndex == -1)
				{
					e.Item.Selected = true;
					SelectItemChanged(e.ItemIndex);
				}
			}
#endif
		}

		private void historyListView_SelectedIndexChanged(object sender, EventArgs e)
		{
#if NET_35_OR_GREATER
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
#endif
		}

		private void sorterBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			TaskEventLog log = new TaskEventLog(task.TaskService.TargetServer, task.Path);
			List<TaskEvent> eList = new List<TaskEvent>(log);
			List<ListViewItem> list = eList.ConvertAll<ListViewItem>(delegate(TaskEvent te) { return BuildItem(te); });
			list.Sort(lvwColumnSorter);
			e.Result = list;
		}

		private void sorterBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			vcache.Clear();
			List<ListViewItem> list = (List<ListViewItem>)e.Result;
			for (int i = 0; i < list.Count; i++)
				vcache.Add(list[i]);
		}

		private void SelectItemChanged(int newSelIdx)
		{
#if NET_35_OR_GREATER
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
#endif
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.RefreshHistory();
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
					using (var wiz = new TaskSchedulerWizard())
					{
						var td = task.TaskService.NewTask();
						var eventId = taskEvent.EventId;
						td.Triggers.Add(new EventTrigger("Microsoft-Windows-TaskScheduler/Operational", "TaskScheduler", eventId));
						td.Actions.Add(new ExecAction());
						wiz.Initialize(task.TaskService, td);
						wiz.TaskName = string.Format("Microsoft-Windows-TaskScheduler_Operational_Microsoft-Windows-TaskScheduler_{0}", eventId);
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

		private void addremoveColumnsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var cols = new string[historyListView.Columns.Count];
			for (int i = 0; i < historyListView.Columns.Count; i++)
				cols[i] = historyListView.Columns[i].Text;
			// TODO: Get current columns in order
			var curCols = cols;
			using (ListColumnEditor colEd = new ListColumnEditor(cols, cols, curCols))
			{
				if (colEd.ShowDialog(this) == DialogResult.OK)
				{
					// TODO: Reorder columns
				}
			}
		}

		private void sortEventsByThisColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			historyListView_ColumnClick(historyListView, new ColumnClickEventArgs(historyListView.LastColumnClicked));
		}

		private void groupEventsByThisColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO:
		}

		internal class ListViewColumnSorter : IComparer<ListViewItem>, System.Collections.IComparer
		{
			private System.Collections.CaseInsensitiveComparer ObjectCompare = new System.Collections.CaseInsensitiveComparer(System.Globalization.CultureInfo.InvariantCulture);

			public int Compare(ListViewItem listviewX, ListViewItem listviewY)
			{
				// Compare the two items
				int compareResult = ObjectCompare.Compare(listviewX.SubItems[SortColumn].Text, listviewY.SubItems[SortColumn].Text);

				// Calculate correct return value based on object comparison
				if (Order == SortOrder.Ascending)
				{
					// Ascending sort is selected, return normal result of compare operation
					return compareResult;
				}
				else if (Order == SortOrder.Descending)
				{
					// Descending sort is selected, return negative result of compare operation
					return (-compareResult);
				}
				// Return '0' to indicate they are equal
				return 0;
			}

			int System.Collections.IComparer.Compare(object x, object y)
			{
				if (x is ListViewItem && y is ListViewItem)
					return Compare((ListViewItem)x, (ListViewItem)y);
				return ObjectCompare.Compare(x, y);
			}

			public int SortColumn { get; set; }

			public SortOrder Order { get; set; }
		}

		/// <summary>
		/// Specialized ListView that passes on MouseMove events
		/// </summary>
		private class ListViewEx : ListView
		{
			Dictionary<int, Rectangle> columns = new Dictionary<int, Rectangle>();

			public ListViewEx()
			{
				OwnerDraw = true;//This will help the OnDrawColumnHeader be called.
				LastColumnClicked = -1;
			}

			public ContextMenuStrip ColumnContextMenuStrip { get; set; }

			public int LastColumnClicked { get; set; }

			protected override void OnDrawItem(DrawListViewItemEventArgs e)
			{
				e.DrawDefault = true;
				base.OnDrawItem(e);
			}

			protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
			{
				e.DrawDefault = true;
				base.OnDrawSubItem(e);
			}

			protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
			{
				columns[e.ColumnIndex] = RectangleToScreen(e.Bounds);
				e.DrawDefault = true;
				base.OnDrawColumnHeader(e);
			}

			/// <summary>
			/// Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)"/>.
			/// </summary>
			/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 0x7b)//WM_CONTEXTMENU
				{
					int lp = m.LParam.ToInt32();
					Point pt = new Point(((lp << 16) >> 16), lp >> 16);
					foreach (KeyValuePair<int, Rectangle> p in columns)
					{
						if (p.Value.Contains(pt))
						{
							LastColumnClicked = p.Key;
							if (ColumnContextMenuStrip != null)
								ColumnContextMenuStrip.Show(pt);
							break;
						}
					}
				}
				if (m.Msg != 0x0200)
					base.WndProc(ref m);
			}
		}
	}
}