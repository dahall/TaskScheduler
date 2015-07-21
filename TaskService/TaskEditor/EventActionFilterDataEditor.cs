using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	internal partial class EventActionFilterDataEditor : Form
	{
		private DataSet ds = new DataSet();
		private DataTable dt;

		public EventActionFilterDataEditor()
		{
			InitializeComponent();
			dt = ds.Tables.Add("Main");
			dt.Columns.Add("Name", typeof(string));
			dt.Columns.Add("Value", typeof(string));
			dataGridView1.DataSource = dt;
		}

		public IDictionary<string, string> DataItems
		{
			get
			{
				var dict = new Dictionary<string, string>();
				foreach (DataRow row in dt.Rows)
				{
					var items = row.ItemArray;
					dict.Add((string)items[0], (string)items[1]);
				}
				return dict;
			}
			set
			{
				dt.Clear();
				foreach (string key in value.Keys)
					dt.Rows.Add(key, value[key]);
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
