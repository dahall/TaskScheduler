using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that will enumerate and display a list of COM objects and allow for a single selection.
	/// </summary>
	[ToolboxItem(true), System.Drawing.ToolboxBitmap(typeof(ComObjectSelectionDialog), "Dialog"), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"),
	Description("Dialog that will enumerate and display a list of COM objects."),
	Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
	DesignTimeVisible(true)]
	public partial class ComObjectSelectionDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private const int chunk = 200;

		private System.Collections.Generic.Queue<Guid> guidsToValidate;
		private bool loading;
		private Guid supportedInterface = Guid.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="ComObjectSelectionDialog"/> class.
		/// </summary>
		public ComObjectSelectionDialog()
		{
			InitializeComponent();
			CLSID = Guid.Empty;
			ServerType = SupportedServers.InProcess;
		}

		/// <summary>
		/// COM server types that can be displayed by a <see cref="ComObjectSelectionDialog"/>.
		/// </summary>
		[Flags]
		public enum SupportedServers
		{
			/// <summary>In process</summary>
			InProcess = 1,
			/// <summary>Out of process</summary>
			OutOfProcess = 2,
			/// <summary>All server types</summary>
			All = 3
		}

		private delegate void ItemInvoke(Guid key);

		/// <summary>
		/// Gets or sets the CLSID.
		/// </summary>
		/// <value>The CLSID.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid CLSID { get; set; }

		/// <summary>
		/// Gets or sets the type of COM servers to display.
		/// </summary>
		/// <value>The type of the server.</value>
		[Category("Data"), DefaultValue(typeof(SupportedServers), "InProcess"), Description("The type of COM servers to display.")]
		public SupportedServers ServerType { get; set; }

		/// <summary>
		/// Gets or sets the COM interface that the selected item must support.
		/// </summary>
		/// <value>The COM interface.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid SupportedInterface
		{
			get { return supportedInterface; }
			set { supportedInterface = value; }
		}

		private static bool SupportsInterface(Guid clsid, Guid iGuid)
		{
			if (iGuid == Guid.Empty)
				return true;

			object o = null;
			IntPtr iu = IntPtr.Zero, ppv = IntPtr.Zero;
			try
			{
				o = Activator.CreateInstance(Type.GetTypeFromCLSID(clsid, false));
				iu = System.Runtime.InteropServices.Marshal.GetIUnknownForObject(o);
				return 0 == System.Runtime.InteropServices.Marshal.QueryInterface(iu, ref iGuid, out ppv);
			}
			catch
			{
				return false;
			}
			finally
			{
				if (ppv != IntPtr.Zero) System.Runtime.InteropServices.Marshal.Release(ppv);
				if (iu != IntPtr.Zero) System.Runtime.InteropServices.Marshal.Release(iu);
				o = null;
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker bw = sender as BackgroundWorker;
			List<ListViewItem> items = new List<ListViewItem>(chunk);
			if (supportedInterface != Guid.Empty)
				guidsToValidate = new Queue<Guid>();
			loading = true;
			using (RegistryKey k = Registry.ClassesRoot.OpenSubKey("CLSID", false))
			{
				try
				{
					string[] l = k.GetSubKeyNames();
					for (int i = 0; i < l.Length; i++)
					{
						if (bw.CancellationPending)
						{
							e.Cancel = true;
							break;
						}
						int mod = i % chunk;
						if (i != 0 && mod == 0)
						{
							bw.ReportProgress(i * 100 / (l.Length - 1), items.ToArray());
							items.Clear();
						}

						if (l[i].Length == 38 && l[i][0] == '{')
						{
							try
							{
								ListViewItem lvi = null;
								using (RegistryKey k2 = k.OpenSubKey(l[i]))
								{
									if (ServerType == SupportedServers.OutOfProcess)
									{
										using (RegistryKey k3 = k2.OpenSubKey("LocalServer32", false))
											if (k3 != null)
												lvi = new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, k3.GetValue(null) as string });
									}
									else
									{
										using (RegistryKey k3 = k2.OpenSubKey("InProcServer32", false))
											if (k3 != null)
												lvi = new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, k3.GetValue(null) as string });
											else
												lvi = new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, null });
									}
								}

								if (lvi != null)
								{
									lvi.Name = lvi.SubItems[0].Text;
									items.Add(lvi);
									if (guidsToValidate != null)
										guidsToValidate.Enqueue(new Guid(l[i]));
								}
							}
							catch { }
						}

						if (i == l.Length - 1)
						{
							bw.ReportProgress(i * 100 / (l.Length - 1), items.ToArray());
						}
					}
				}
				catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.ToString()); }
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ListViewItem[] items = e.UserState as ListViewItem[];
			listView1.Items.AddRange(items);
			progressBar1.Value = e.ProgressPercentage;
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			listView1.EndUpdate();
			progressBar1.Hide();
			searchPanel.Show();
			loading = false;
			/*if (supportedInterface != null && supportedInterface != Guid.Empty)
				backgroundWorker2.RunWorkerAsync(listView1.Items);
			else*/
			UseWaitCursor = false;
		}

		private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			while (loading || guidsToValidate.Count > 0)
			{
				while (loading && guidsToValidate.Count == 0 && !e.Cancel)
					System.Threading.Thread.Sleep(50);

				if (e.Cancel)
					break;

				if (guidsToValidate.Count > 0)
				{
					Guid g = guidsToValidate.Dequeue();
					if (!SupportsInterface(g, supportedInterface))
						Invoke(new ItemInvoke(DisableKey), g);
				}
			}
		}

		private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			UseWaitCursor = false;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
			backgroundWorker2.CancelAsync();
			CLSID = Guid.Empty;
		}

		private void ComObjectSelectionDialog_Load(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			listView1.BeginUpdate();
			progressBar1.Value = 0;
			progressBar1.Show();
			searchPanel.Hide();
			backgroundWorker1.RunWorkerAsync();
			UseWaitCursor = true;
		}

		private void DisableItem(ListViewItem item)
		{
			if (item != null)
			{
				item.ForeColor = System.Drawing.SystemColors.GrayText;
				item.Tag = false;
			}
		}

		private void DisableKey(Guid key)
		{
			int i = listView1.Items.IndexOfKey(key.ToString("B").ToLower());
			if (i != -1)
				DisableItem(listView1.Items[i]);
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
		}

		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			CLSID = new Guid(e.Item.SubItems[0].Text);
			bool valid = (e.Item.Tag != null && (bool)e.Item.Tag == false) ? false : SupportsInterface(CLSID, supportedInterface);
			okButton.Enabled = valid;
			if (!valid)
				DisableItem(e.Item);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
			backgroundWorker2.CancelAsync();
			if (!SupportsInterface(CLSID, supportedInterface))
				MessageBox.Show(this, EditorProperties.Resources.ComObjectDoesNotSupportInterfaceErrorMessage, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				DialogResult = DialogResult.OK;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			ListViewItem res = listView1.FindItemWithText(textBox1.Text, true, listView1.TopItem.Index);
			if (res == null)
				res = listView1.FindItemWithText(textBox1.Text, true, 0);
			if (res != null)
			{
				res.Selected = true;
				listView1.TopItem = res;
			}
		}

		private class ListViewItemComparer : System.Collections.IComparer
		{
			private int col;

			public ListViewItemComparer(int column = 0)
			{
				col = column;
			}

			public int Compare(object x, object y) => String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
		}
	}
}