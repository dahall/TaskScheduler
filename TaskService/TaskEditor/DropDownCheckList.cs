using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	public partial class DropDownCheckList : CustomComboBox
	{
		private System.Windows.Forms.CheckedListBox checkedListBox1;

		public DropDownCheckList()
		{
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.checkedListBox1.CheckOnClick = true;
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(17, 35);
			this.checkedListBox1.MultiColumn = false;
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(187, 105);
			this.checkedListBox1.TabIndex = 0;
			this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);
			base.DropDownControl = this.checkedListBox1;
		}

		void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.Index == 0 && !string.IsNullOrEmpty(CheckAllText) && this.Items.Count > 1)
			{
				bool chk = !GetItemChecked(0);
				for (int i = 1; i < this.Items.Count; i++)
				{
					SetItemChecked(i, chk);
				}
			}
		}

		protected override void OnDropDownClosed(EventArgs e)
		{
			base.OnDropDownClosed(e);
			UpdateText();
		}

		public void UpdateText()
		{
			List<string> items = new List<string>(this.checkedListBox1.CheckedItems.Count);
			foreach (var item in this.checkedListBox1.CheckedItems)
				items.Add(item.ToString());
			if (!string.IsNullOrEmpty(CheckAllText) && items.Count > 0 && items[0] == CheckAllText) items.RemoveAt(0);
			this.Text = string.Join(", ", items.ToArray());
		}

		[DefaultValue(false), Category("Appearance")]
		public bool MultiColumnList
		{
			get { return this.checkedListBox1.MultiColumn; }
			set { this.checkedListBox1.MultiColumn = value; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")]
		public new CheckedListBox.ObjectCollection Items
		{
			get { return this.checkedListBox1.Items; }
		}

		[DefaultValue(null), Category("Appearance")]
		public string CheckAllText { get; set; }

		public long CheckedFlagValue
		{
			get
			{
				long ret = 0;
				for (int i = 0; i < Items.Count; i++)
				{
					object o = Items[i];
					if (Items[i] is DropDownCheckListItem)
						o = ((DropDownCheckListItem)o).Value;
					try { ret |= Convert.ToInt64(o); }
					catch {}
				}
				return ret;
			}
			set
			{
				for (int i = 0; i < Items.Count; i++)
				{
					long? val = null;
					object o = Items[i];
					if (Items[i] is DropDownCheckListItem)
						o = ((DropDownCheckListItem)o).Value;
					try { val = Convert.ToInt64(o); }
					catch { }

					if (val.HasValue && (val.Value & value) == val.Value)
						this.checkedListBox1.SetItemCheckState(i, CheckState.Checked);
					else
						this.checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
				}
				UpdateText();
			}
		}

		public DropDownCheckListItem[] SelectedItems
		{
			get
			{
				ListBox.SelectedObjectCollection c = this.checkedListBox1.SelectedItems;
				DropDownCheckListItem[] ret = new DropDownCheckListItem[c.Count];
				for (int i = 0; i < ret.Length; i++)
				{
					ret[i] = c[i] as DropDownCheckListItem;
					if (ret[i] == null)
						ret[i] = new DropDownCheckListItem(c[i]);
				}
				return ret;
			}
		}

		public bool GetItemChecked(int index)
		{
			return this.checkedListBox1.GetItemChecked(index);
		}

		public void SetItemChecked(int index, bool value)
		{
			this.checkedListBox1.SetItemChecked(index, value);
			UpdateText();
		}

		public void InitializeFromEnum(Type enumType, System.Resources.ResourceManager mgr, string prefix)
		{
			this.Items.Clear();
			long allVal = 0;
			Array vals = Enum.GetValues(enumType);
			Array names = Enum.GetNames(enumType);
			for (int i = 0; i < vals.Length; i++)
			{
				long val = Convert.ToInt64(vals.GetValue(i));
				allVal |= val;
				string text = mgr.GetString(prefix + names.GetValue(i).ToString());
				if (text.Length > 1) text = text.Substring(0, 1).ToUpper() + text.Substring(1);
				this.Items.Add(new DropDownCheckListItem(text, val));
			}
			if (!string.IsNullOrEmpty(this.CheckAllText))
				this.Items.Insert(0, new DropDownCheckListItem(this.CheckAllText, allVal));
		}

		internal void InitializeFromRange(int start, int end)
		{
			this.Items.Clear();
			for (int i = start; i <= end; i++)
			{
				this.Items.Add(new DropDownCheckListItem(i));
			}
		}
	}

	public class DropDownCheckListItem
	{
		public DropDownCheckListItem() : this(string.Empty, null) { }

		public DropDownCheckListItem(object value) : this(value.ToString(), value) { }

		public DropDownCheckListItem(string text) : this(text, text) { }

		public DropDownCheckListItem(string text, object value)
		{
			Text = text; Value = value;
		}

		public string Text { get; set; }
		public object Value { get; set; }

		public override string ToString()
		{
			return this.Text;
		}
	}
}
