using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog that will enumerate and display a list of COM objects and allow for a single selection.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms.Control.TopLevel"), Description("Dialog that will enumerate and display a list of COM objects."), Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), DesignTimeVisible(true)]
	public partial class ComObjectSelectionDialog :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		private const int chunk = 500;

		private Guid supportedInterface = Guid.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="ComObjectSelectionDialog"/> class.
		/// </summary>
		public ComObjectSelectionDialog()
		{
			InitializeComponent();
			CLSID = Guid.Empty;
			ServerType = SupportedServers.All;
		}

		/// <summary>
		/// COM server types that can be displayed by a <see cref="ComObjectSelectionDialog"/>.
		/// </summary>
		[Flags]
		public enum SupportedServers
		{
			/// <summary>
			/// In process
			/// </summary>
			InProcess = 1,
			/// <summary>
			/// Out of process
			/// </summary>
			OutOfProcess = 2,
			/// <summary>
			/// All server types
			/// </summary>
			All = 3
		}

		/// <summary>
		/// Gets or sets the CLSID.
		/// </summary>
		/// <value>The CLSID.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid CLSID
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the type of COM servers to display.
		/// </summary>
		/// <value>The type of the server.</value>
		[Category("Data"), DefaultValue(typeof(SupportedServers), "All"), Description("The type of COM servers to display.")]
		public SupportedServers ServerType
		{
			get; set;
		}

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

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker bw = sender as BackgroundWorker;
			List<ListViewItem> items = new List<ListViewItem>(chunk);
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
								using (RegistryKey k2 = k.OpenSubKey(l[i]))
								{
									if (ServerType == SupportedServers.OutOfProcess)
									{
										using (RegistryKey k3 = k2.OpenSubKey("LocalServer32", false))
											if (k3 != null)
												items.Add(new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, k3.GetValue(null) as string }));
									}
									else
									{
										using (RegistryKey k3 = k2.OpenSubKey("InProcServer32", false))
											if (k3 != null)
												items.Add(new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, k3.GetValue(null) as string }));
											else
												items.Add(new ListViewItem(new string[] { l[i].ToLower(), k2.GetValue(null) as string, null }));
									}
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
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
			CLSID = Guid.Empty;
		}

		private void ComObjectSelectionDialog_Load(object sender, EventArgs e)
		{
			listView1.Items.Clear();
			backgroundWorker1.RunWorkerAsync();
		}

		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
		}

		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			CLSID = new Guid(e.Item.SubItems[0].Text);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			backgroundWorker1.CancelAsync();
			if (!SupportsInterface())
				MessageBox.Show(this, Properties.Resources.ComObjectDoesNotSupportInterfaceErrorMessage, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private bool SupportsInterface()
		{
			if (SupportedInterface == Guid.Empty)
				return true;

			object o = null;
			IntPtr iu = IntPtr.Zero, ppv = IntPtr.Zero;
			try
			{
				o = Activator.CreateInstance(Type.GetTypeFromCLSID(CLSID, false));
				iu = System.Runtime.InteropServices.Marshal.GetIUnknownForObject(o);
				return 0 == System.Runtime.InteropServices.Marshal.QueryInterface(iu, ref supportedInterface, out ppv);
			}
			catch
			{
				return false;
			}
			finally
			{
				if (ppv != IntPtr.Zero) System.Runtime.InteropServices.Marshal.Release(ppv);
				if (iu != IntPtr.Zero) System.Runtime.InteropServices.Marshal.Release(iu);
				if (o != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
			}
		}

		private class ListViewItemComparer : System.Collections.IComparer
		{
			private int col;

			public ListViewItemComparer(int column = 0)
			{
				col = column;
			}

			public int Compare(object x, object y)
			{
				return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
			}
		}
	}
}