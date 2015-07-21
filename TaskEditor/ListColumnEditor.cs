using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Dialog allowing the selections of columns for a list.
	/// </summary>
	[ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Dialog allowing the selections of columns for a list.")]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("DisplayedColumns"), DesignTimeVisible(true)]
	[ToolboxBitmap(typeof(ListColumnEditor), "Dialog")]
	public partial class ListColumnEditor :
#if DEBUG
		Form
#else
		DialogBase
#endif
	{
		string[] cols, defCols;

		private ListColumnEditor() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ListColumnEditor"/> class.
		/// </summary>
		/// <param name="columns">The list of all possible column entries.</param>
		/// <param name="defaultColumns">The default column entries.</param>
		/// <param name="displayedColumns">The displayed column entries.</param>
		public ListColumnEditor(string[] columns, string[] defaultColumns, string[] displayedColumns)
		{
			InitializeComponent();
			cols = (string[])columns.Clone();
			defCols = (string[])defaultColumns.Clone();
			DisplayedColumns = (string[])displayedColumns.Clone();
			availColsListBox.Items.AddRange(GetAvailableColumns(columns, displayedColumns));
			availColsListBox_SelectedIndexChanged(null, EventArgs.Empty);
			dispColsListBox.Items.AddRange(displayedColumns);
			dispColsListBox_SelectedIndexChanged(null, EventArgs.Empty);
		}

		/// <summary>
		/// Gets the displayed columns.
		/// </summary>
		/// <value>
		/// The displayed columns.
		/// </value>
		public string[] DisplayedColumns { get; private set; }

		private string[] GetAvailableColumns(string[] columns, string[] displayedColumns)
		{
			var ret = new List<string>();
			for (int i = 0; i < columns.Length; i++)
			{
				if (!Array.Exists<string>(displayedColumns, delegate(string s) { return s == columns[i]; }))
					ret.Add(columns[i]);
			}
			return ret.ToArray();
		}

		private void addBtn_Click(object sender, EventArgs e)
		{
			int idx = dispColsListBox.Items.Add(availColsListBox.SelectedItem.ToString());
			availColsListBox.Items.RemoveAt(availColsListBox.SelectedIndex);
			dispColsListBox.SelectedIndex = idx;
		}

		private void remBtn_Click(object sender, EventArgs e)
		{
			int idx = availColsListBox.Items.Add(dispColsListBox.SelectedItem.ToString());
			dispColsListBox.Items.RemoveAt(dispColsListBox.SelectedIndex);
			availColsListBox.SelectedIndex = idx;
		}

		private void upBtn_Click(object sender, EventArgs e)
		{
			string val = dispColsListBox.SelectedItem.ToString();
			int newIdx = dispColsListBox.SelectedIndex - 1;
			dispColsListBox.Items.RemoveAt(dispColsListBox.SelectedIndex);
			dispColsListBox.Items.Insert(newIdx, val);
			dispColsListBox.SelectedIndex = newIdx;
		}

		private void downBtn_Click(object sender, EventArgs e)
		{
			string val = dispColsListBox.SelectedItem.ToString();
			int newIdx = dispColsListBox.SelectedIndex + 1;
			dispColsListBox.Items.RemoveAt(dispColsListBox.SelectedIndex);
			dispColsListBox.Items.Insert(newIdx, val);
			dispColsListBox.SelectedIndex = newIdx;
		}

		private void restoreBtn_Click(object sender, EventArgs e)
		{
			availColsListBox.BeginUpdate();
			availColsListBox.Items.Clear();
			availColsListBox.Items.AddRange(GetAvailableColumns(cols, defCols));
			availColsListBox.EndUpdate();
			availColsListBox_SelectedIndexChanged(null, EventArgs.Empty);

			dispColsListBox.BeginUpdate();
			dispColsListBox.Items.Clear();
			dispColsListBox.Items.AddRange(defCols);
			dispColsListBox.EndUpdate();
			dispColsListBox_SelectedIndexChanged(null, EventArgs.Empty);
		}

		private void okBtn_Click(object sender, EventArgs e)
		{
			string[] res = new string[dispColsListBox.Items.Count];
			for (int i = 0; i < dispColsListBox.Items.Count; i++)
				res[i] = dispColsListBox.Items[i].ToString();
			DisplayedColumns = res;
			Close();
		}

		private void cancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void availColsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			addBtn.Enabled = availColsListBox.SelectedIndex != -1;
		}

		private void dispColsListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool validSel = dispColsListBox.SelectedIndex != -1;
			remBtn.Enabled = validSel;
			upBtn.Enabled = validSel && dispColsListBox.SelectedIndex > 0;
			downBtn.Enabled = validSel && dispColsListBox.Items.Count > 1 && dispColsListBox.SelectedIndex < (dispColsListBox.Items.Count - 1);
		}

		private void dispColsListBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (dispColsListBox.SelectedItem == null) return;
			dispColsListBox.DoDragDrop(dispColsListBox.SelectedItem, DragDropEffects.Move);
		}

		private void dispColsListBox_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void dispColsListBox_DragDrop(object sender, DragEventArgs e)
		{
			Point point = dispColsListBox.PointToClient(new Point(e.X, e.Y));
			int index = dispColsListBox.IndexFromPoint(point);
			if (index < 0) index = dispColsListBox.Items.Count - 1;
			object data = e.Data.GetData(typeof(string));
			dispColsListBox.Items.Remove(data);
			dispColsListBox.Items.Insert(index, data);
		}
	}
}
