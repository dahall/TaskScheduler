using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace TaskSchedulerMockup
{
	public partial class HomePanel : UserControl, ISupportTasks
	{
		private object currentEventTaskId;
		private SendOrPostCallback onActiveTasksRetrieved;
		private SendOrPostCallback onEventStatusRetrieved;
		private readonly HybridDictionary tasks = new HybridDictionary();
		private TaskService ts;

		public HomePanel()
		{
			InitializeComponent();
			InitializeDelegates();
			comboBox1.SelectedIndex = 0;
		}

		private delegate void WorkerEventHandler(TimeSpan span, AsyncOperation asyncOp);

		public ToolStrip MenuItems => null;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TaskService TaskService
		{
			get { return ts; }
			set
			{
				ts = value;
				RefreshPanels();
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (!Visible)
			{
				Debug.WriteLine("Leaving HomePanel and terminating tasks.");
				object[] array;
				lock (tasks.SyncRoot)
				{
					array = new object[tasks.Keys.Count];
					tasks.Keys.CopyTo(array, 0);
				}
				foreach (var t in array)
					CancelAsync(t);
			}
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x0202: // WM_LBUTTONUP
					DefWndProc(ref m);
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		private static int ActiveTaskSorter(ListViewItem al, ListViewItem bl)
		{
			Task a = al?.Tag as Task, b = bl?.Tag as Task;
			DateTime an = DateTime.MinValue, bn = DateTime.MinValue;
			var ax = a == null || (an = a.NextRunTime) == DateTime.MinValue;
			var bx = b == null || (bn = b.NextRunTime) == DateTime.MinValue;
			if (ax && !bx)
				return -1;
			if (!ax && bx)
				return 1;
			if (ax)
				return string.Compare(a?.Name, b?.Name, StringComparison.CurrentCulture);
			return DateTime.Compare(an, bn);
		}

		private void ActiveTasksComplete(object state)
		{
			var e = state as ListViewItemEventArgs;
			if (e == null || e.Cancelled) return;
			activeListView.UseWaitCursor = false;
			if (e.Items != null && e.Items.Length > 0)
				activeListView.Items.AddRange(e.Items);
			label6.Text = string.Format(label6.Tag.ToString(), activeListView.Items.Count);
		}

		private void ActiveTasksDoWork(TimeSpan span, AsyncOperation asyncOp)
		{
			Exception e = null;
			var list = new List<ListViewItem>();
			var cancelled = false;

			if (!IsTaskCanceled(asyncOp.UserSuppliedState))
			{
				try
				{
					using (var lts = new TaskService(ts.TargetServer, ts.UserName, ts.UserAccountDomain, ts.UserPassword, ts.HighestSupportedVersion.Minor == 1) {AllowReadOnlyTasks = true})
					{
						foreach (var t in lts.RootFolder.EnumerateTasks(x => x.IsActive, true))
						{
							if (IsTaskCanceled(asyncOp.UserSuppliedState))
							{
								Debug.WriteLine("HomePanel.ActiveTasks cancelled.");
								cancelled = true;
								break;
							}
							try
							{
								var next = t.NextRunTime;
								list.Add(new ListViewItem(new[] { t.Name, next == DateTime.MinValue ? "" : next.ToString("G"), t.Definition.Triggers.ToString(), t.Folder.Path }) { Tag = t });
							}
							catch
							{
								Debug.WriteLine($"Unable to insert task '{t.Path}' into active task list.");
							}
						}
						if (!cancelled)
							list.Sort(ActiveTaskSorter);
					}
				}
				catch (Exception ex)
				{
					e = ex;
				}
			}

			ActiveTasksSignalCompletion(list.ToArray(), e, cancelled, asyncOp);
		}

		private void ActiveTasksGetAsync(object taskId)
		{
			var asyncOp = AsyncOperationManager.CreateOperation(taskId);
			lock (tasks.SyncRoot)
			{
				if (tasks.Contains(taskId))
					throw new ArgumentException("Task identifier must be unique.", nameof(taskId));
				tasks[taskId] = asyncOp;
			}

			WorkerEventHandler worker = ActiveTasksDoWork;
			worker.BeginInvoke(TimeSpan.Zero, asyncOp, null, null);
		}

		private void ActiveTasksSignalCompletion(ListViewItem[] items, Exception exception, bool canceled, AsyncOperation asyncOp)
		{
			if (!canceled)
				lock (tasks.SyncRoot)
					tasks.Remove(asyncOp.UserSuppliedState);

			var e = new ListViewItemEventArgs(items, exception, canceled, asyncOp.UserSuppliedState);
			asyncOp.PostOperationCompleted(onActiveTasksRetrieved, e);
		}

		private void ActiveTasksUpdate()
		{
			label6.Text = "Reading data...";
			activeListView.Items.Clear();
			activeListView.UseWaitCursor = true;
			ActiveTasksGetAsync(Guid.NewGuid());
			//activeBackgroundWorker.RunWorkerAsync();
		}

		private void CancelAsync(object taskId)
		{
			var asyncOp = tasks[taskId] as AsyncOperation;
			if (asyncOp != null)
				lock (tasks.SyncRoot)
					tasks.Remove(taskId);
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ts != null)
				EventStatusUpdate();
		}

		private void EventStatusComplete(object state)
		{
			currentEventTaskId = null;
			var e = state as ListViewItemEventArgs;
			if (e == null || e.Error != null || e.Cancelled || e.Items == null || e.Items.Length == 0)
			{
				label4.Text = "No results";
			}
			else
			{
#if !NET20
				ListViewGroup lvgroup = new ListViewGroup();
				statusListView.BeginUpdate();
				int running = 0, succeeded = 0, stopped = 0, failed = 0;
				for (int i = 0; i < e.Items.Length; i++)
				{
					if (lvgroup.Header != e.Items[i].Text)
					{
						lvgroup = new ListViewGroup(e.Items[i].Text);
						statusListView.Groups.Add(lvgroup);
						lvgroup.SetCollapsible(true);
					}
					e.Items[i].Group = lvgroup;
					statusListView.Items.Add(e.Items[i]);
					switch (((CorrelatedTaskEvent)e.Items[i].Tag).RunResult)
					{
						case CorrelatedTaskEvent.Status.StillRunning:
							running++;
							break;

						case CorrelatedTaskEvent.Status.Success:
							succeeded++;
							break;

						case CorrelatedTaskEvent.Status.Failure:
							failed++;
							break;

						case CorrelatedTaskEvent.Status.Terminated:
							stopped++;
							break;

						default:
							break;
					}
				}
				statusListView.EndUpdate();
				label4.Text = string.Format(label4.Tag.ToString(), e.Items.Length, running, succeeded, stopped, failed);
#endif
			}
			statusListView.UseWaitCursor = false;
		}

		private void EventStatusDoWork(TimeSpan span, AsyncOperation asyncOp)
		{
			Exception e = null;
			var list = new List<ListViewItem>();

			if (!IsTaskCanceled(asyncOp.UserSuppliedState))
			{
#if !NET20
				try
				{
					if (Environment.OSVersion.Version.Major > 5)
					{
						DateTime st = DateTime.Now.Subtract(span);
						CorrelatedTaskEventLog log = new CorrelatedTaskEventLog(st, null, ts.TargetServer);
						foreach (var t in log)
						{
							if (IsTaskCanceled(asyncOp.UserSuppliedState))
								break;
							list.Add(new ListViewItem(new string[] { t.TaskName, t.RunResult.ToString(), t.RunStart.ToString(), t.RunEnd.ToString(), t.TriggeredBy.ToString() }) { Tag = t });
						}
					}
				}
				catch (Exception ex)
				{
					e = ex;
				}
#endif
			}

			EventStatusSignalCompletion(list.ToArray(), e, IsTaskCanceled(asyncOp.UserSuppliedState), asyncOp);
		}

		private void EventStatusGetAsync(TimeSpan span, object taskId)
		{
			var asyncOp = AsyncOperationManager.CreateOperation(taskId);
			lock (tasks.SyncRoot)
			{
				if (tasks.Contains(taskId))
					throw new ArgumentException("Task identifier must be unique.", nameof(taskId));
				tasks[taskId] = asyncOp;
			}

			WorkerEventHandler worker = EventStatusDoWork;
			worker.BeginInvoke(span, asyncOp, null, null);
		}

		private void EventStatusSignalCompletion(ListViewItem[] items, Exception exception, bool canceled, AsyncOperation asyncOp)
		{
			if (!canceled)
				lock (tasks.SyncRoot)
					tasks.Remove(asyncOp.UserSuppliedState);

			var e = new ListViewItemEventArgs(items, exception, canceled, asyncOp.UserSuppliedState);
			asyncOp.PostOperationCompleted(onEventStatusRetrieved, e);
		}

		private void EventStatusUpdate()
		{
			label4.Text = "Reading data...";
			statusListView.Items.Clear();
			statusListView.Groups.Clear();
			statusListView.UseWaitCursor = true;
			var span = TimeSpan.Zero;
			switch (comboBox1.SelectedIndex)
			{
				case 0:
					span = TimeSpan.FromHours(1);
					break;

				case 1:
					span = TimeSpan.FromDays(1);
					break;

				case 2:
					span = TimeSpan.FromDays(7);
					break;

				case 3:
					span = TimeSpan.FromDays(30);
					break;
			}
			if (currentEventTaskId != null)
				CancelAsync(currentEventTaskId);
			EventStatusGetAsync(span, currentEventTaskId = Guid.NewGuid());
			/*statusBackgroundWorker.CancelAsync();
			while (statusBackgroundWorker.IsBusy)
				System.Threading.Thread.Sleep(250);
			statusBackgroundWorker.RunWorkerAsync(comboBox1.SelectedIndex);*/
		}

		private void InitializeDelegates()
		{
			onActiveTasksRetrieved = ActiveTasksComplete;
			onEventStatusRetrieved = EventStatusComplete;
		}

		private bool IsTaskCanceled(object taskId) => tasks[taskId] == null;

		private void refreshButton_Click(object sender, EventArgs e)
		{
			RefreshPanels();
		}

		private void RefreshPanels()
		{
			var now = DateTime.Now;

			// Update summary
			summaryLabel.Text = $"Task Scheduler Summary (Last refreshed: {now:G})";
			footerLabel.Text = $"Last refreshed at {now:G}";

			if (ts == null)
				return;

			// Update status list
			EventStatusUpdate();

			// Update active list
			ActiveTasksUpdate();
		}

		private class ListViewItemEventArgs : AsyncCompletedEventArgs
		{
			private ListViewItem[] items;

			public ListViewItemEventArgs(ListViewItem[] items, Exception e, bool canceled, object state) : base(e, canceled, state)
			{
				Items = items;
			}

			public ListViewItem[] Items
			{
				get
				{
					RaiseExceptionIfNecessary();
					return items;
				}
				private set { items = value; }
			}
		}
	}
}