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
		private Task task;
#if NET_35_OR_GREATER
		private int selectedIndex = -1;
		private GrpCtrlDLL::System.Collections.Generic.SparseArray<ListViewItem> vcache;
		private TaskEventEnumerator vevEnum;
		private TaskEventLog vlog;
#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskHistoryControl"/> class.
		/// </summary>
		public TaskHistoryControl()
		{
			InitializeComponent();
			this.ShowErrors = true;
			historyListView.VirtualMode = true;
		}

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value><c>true</c> if errors are shown; otherwise, <c>false</c>.</value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		public bool ShowErrors { get; set; }

		/// <summary>
		/// Activates this instance. Call when the control receives initial focus or needs to refresh.
		/// </summary>
		/// <param name="t">The <see cref="Task"/> for which to get the history.</param>
		public void Activate(Task t)
		{
			this.task = t;
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
				historyStopStartBtn.ImageIndex = 10;
			}
			historyListView.Cursor = Cursors.WaitCursor;
			historyBackgroundWorker.RunWorkerAsync();
			historyListView.Items.Clear();
			historyDetailView.ActiveTab = EventViewerControl.EventViewerActiveTab.General;
			historyHeader_Refresh(-1);
			historySplitContainer.Panel2Collapsed = false;
#if NET_35_OR_GREATER
			historyDetailView.TaskEvent = null;
			selectedIndex = -1;
			vcache = new GrpCtrlDLL::System.Collections.Generic.SparseArray<ListViewItem>();
			vlog = new TaskEventLog(task.TaskService.TargetServer, task.Path);
			vevEnum = vlog.GetEnumerator() as TaskEventEnumerator;
#endif
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
			if (vcache[e.StartIndex] == null && vevEnum != null)
			{
				vevEnum.Seek(System.IO.SeekOrigin.Begin, e.StartIndex);
				int n = e.StartIndex;
				while (vevEnum.MoveNext() && n <= e.EndIndex)
					vcache[n++] = BuildItem(vevEnum.Current);
			}
#endif
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

		/// <summary>
		/// Specialized ListView that passes on MouseMove events
		/// </summary>
		private class ListViewEx : ListView
		{
			/// <summary>
			/// Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)"/>.
			/// </summary>
			/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
			protected override void WndProc(ref Message m)
			{
				if (m.Msg != 0x0200)
					base.WndProc(ref m);
			}
		}
	}
}