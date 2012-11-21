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
		private SparseArray<ListViewItem> vcache = new SparseArray<ListViewItem>();
		private TaskEventEnumerator vevEnum;
		private TaskEventLog vlog;

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
			historyDetailView.TaskEvent = null;
			historyDetailView.ActiveTab = EventViewerControl.EventViewerActiveTab.General;
			historyHeader_Refresh(-1);
			historyEventCount = 0;
			historySplitContainer.Panel2Collapsed = false;
			vlog = new TaskEventLog(task.TaskService.TargetServer, task.Path);
			vevEnum = vlog.GetEnumerator() as TaskEventEnumerator;
		}

		private ListViewItem BuildItem(TaskEvent item)
		{
			return new ListViewItem(new string[] { item.Level, item.TimeCreated.ToString(), item.EventId.ToString(),
				item.TaskCategory, item.OpCode, item.ActivityId.ToString() }, item.EventRecord.Level.GetValueOrDefault(0)) { Tag = item };
		}

		private void histDetailHideBtn_Click(object sender, EventArgs e)
		{
			historySplitContainer.Panel2Collapsed = true;
		}

		private void historyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				TaskEventLog log = new TaskEventLog(task.TaskService.TargetServer, task.Path);
				e.Result = log.Count;
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
			if (e.Result is Exception && ShowErrors)
				MessageBox.Show(this, string.Format(EditorProperties.Resources.Error_CannotRetrieveHistory, ((Exception)e.Result).Message), EditorProperties.Resources.TaskSchedulerName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				vevEnum.Seek(System.IO.SeekOrigin.Begin, e.StartIndex);
				int n = e.StartIndex;
				while (vevEnum.MoveNext() && n <= e.EndIndex)
					vcache[n++] = BuildItem(vevEnum.Current);
			}
		}

		private void historyListView_DoubleClick(object sender, EventArgs e)
		{
			if (historyListView.SelectedIndices.Count > 0)
			{
				ListViewItem lvi = vcache[historyListView.SelectedIndices[0]];
				if (lvi != null)
				{
					EventViewerDialog dlg = new EventViewerDialog();
					dlg.Initialize(lvi.Tag as TaskEvent, null); //TaskService == null ? new TaskEventLog(task.Path) : new TaskEventLog(TaskService.TargetServer, task.Path));
					dlg.ShowDialog(this);
				}
			}
		}

		private void historyListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			ListViewItem item = vcache[e.ItemIndex];
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
				if (historyListView.SelectedIndices.Count == 0)
					e.Item.Selected = true;
			}
		}

		private void historyListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (historyListView.SelectedIndices.Count > 0)
			{
				ListViewItem lvi = vcache[historyListView.SelectedIndices[0]];
				if (lvi != null)
				{
					TaskEvent ev = lvi.Tag as TaskEvent;
					historyDetailView.TaskEvent = ev;
					historyDetailTitleText.Text = ev == null ? string.Empty : string.Format(EditorProperties.Resources.EventDetailHeader, ev.EventId);
				}
			}
			else
			{
				historyDetailView.TaskEvent = null;
				historyDetailTitleText.Text = string.Empty;
			}
		}
	}
}