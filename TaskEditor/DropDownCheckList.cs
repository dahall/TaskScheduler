using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
    internal class DropDownCheckListItem
    {
        public DropDownCheckListItem()
            : this(string.Empty, null)
        {
        }

        public DropDownCheckListItem(object value)
            : this(value.ToString(), value)
        {
        }

        public DropDownCheckListItem(string text)
            : this(text, text)
        {
        }

        public DropDownCheckListItem(string text, object value)
        {
            Text = text; Value = value;
        }

        public string Text
        {
            get; set;
        }

        public object Value
        {
            get; set;
        }

        public override bool Equals(object obj)
        {
            if (obj is DropDownCheckListItem)
                return Text == ((DropDownCheckListItem)obj).Text && Value == ((DropDownCheckListItem)obj).Value;
            if (obj.GetType() == Value.GetType())
                return Value.Equals(obj);
            if (obj is string)
                return Text.Equals(obj);
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return this.Text;
        }
    }

    internal static class ComboBoxExtension
    {
        public static void InitializeFromEnum(System.Collections.IList list, Type enumType, System.Resources.ResourceManager mgr, string prefix, out long allVal)
        {
            list.Clear();
            allVal = 0;
            Array vals = Enum.GetValues(enumType);
            Array names = Enum.GetNames(enumType);
            for (int i = 0; i < vals.Length; i++)
            {
                long val = Convert.ToInt64(vals.GetValue(i));
                allVal |= val;
                string text = mgr.GetString(prefix + names.GetValue(i).ToString());
                if (text.Length > 1) text = text.Substring(0, 1).ToUpper() + text.Substring(1);
                list.Add(new DropDownCheckListItem(text, val));
            }
        }
    }

    internal partial class DropDownCheckList : CustomComboBox
    {
        private System.Windows.Forms.CheckedListBox checkedListBox1;

        public DropDownCheckList()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox()
            {
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                CheckOnClick = true,
                FormattingEnabled = true,
                Location = new System.Drawing.Point(17, 35),
                MultiColumn = false,
                Name = "checkedListBox1",
                Size = new System.Drawing.Size(187, 105),
                TabIndex = 0
            };
            this.checkedListBox1.ItemCheck += new ItemCheckEventHandler(checkedListBox1_ItemCheck);
            base.DropDownControl = this.checkedListBox1;
            /*base.DataSource = this.checkedListBox1.Items;
            base.ValueMember = "Value";
            base.DisplayMember = "Text";*/
        }

		[Category("Action"), Description("Occurs when the SelectedItems property changes.")]
		public event EventHandler SelectedItemsChanged;

		[DefaultValue(null),
        Category("Appearance")]
        public string CheckAllText
        {
            get; set;
        }

        public long CheckedFlagValue
        {
            get
            {
                long ret = 0;
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
					object o = checkedListBox1.CheckedItems[i];
                    if (o is DropDownCheckListItem)
                        o = ((DropDownCheckListItem)o).Value;
                    try { ret |= Convert.ToInt64(o); }
                    catch {}
                }
                return ret;
            }
            set
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    long? val = null;
                    object o = checkedListBox1.Items[i];
                    if (checkedListBox1.Items[i] is DropDownCheckListItem)
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        Category("Data")]
        public new CheckedListBox.ObjectCollection Items
        {
            get { return this.checkedListBox1.Items; }
        }

        [DefaultValue(false),
        Category("Appearance")]
        public bool MultiColumnList
        {
            get { return this.checkedListBox1.MultiColumn; }
            set { this.checkedListBox1.MultiColumn = value; }
        }

        public DropDownCheckListItem[] SelectedItems
        {
            get
            {
                var c = this.checkedListBox1.CheckedItems;
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

        public void InitializeFromEnum(Type enumType, System.Resources.ResourceManager mgr, string prefix)
        {
            long allVal;
            ComboBoxExtension.InitializeFromEnum(this.checkedListBox1.Items, enumType, mgr, prefix, out allVal);
            if (!string.IsNullOrEmpty(this.CheckAllText))
                this.checkedListBox1.Items.Insert(0, new DropDownCheckListItem(this.CheckAllText, allVal));
        }

        public void RemoveItem(int index)
        {
            this.checkedListBox1.Items.RemoveAt(index);
        }

        public void SetItemChecked(int index, bool value)
        {
            this.checkedListBox1.SetItemChecked(index, value);
            UpdateText();
        }

        public void UpdateText()
        {
            List<string> items = new List<string>(this.checkedListBox1.CheckedItems.Count);
            foreach (var item in this.checkedListBox1.CheckedItems)
                items.Add(item.ToString());
            if (!string.IsNullOrEmpty(CheckAllText) && items.Count > 0 && items[0] == CheckAllText) items.RemoveAt(0);
			string newText = string.Join(", ", items.ToArray());
			if (newText != this.Text)
			{
				this.Text = newText;
				OnSelectedItemsChanged(EventArgs.Empty);
			}
		}

        internal void InitializeFromRange(int start, int end)
        {
            this.checkedListBox1.Items.Clear();
            for (int i = start; i <= end; i++)
            {
                this.checkedListBox1.Items.Add(new DropDownCheckListItem(i));
            }
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            UpdateText();
        }

		protected virtual void OnSelectedItemsChanged(EventArgs eventArgs)
		{
			EventHandler h = this.SelectedItemsChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

		void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0 && !string.IsNullOrEmpty(CheckAllText) && this.checkedListBox1.Items.Count > 1)
            {
                bool chk = !GetItemChecked(0);
                for (int i = 1; i < this.checkedListBox1.Items.Count; i++)
                {
                    SetItemChecked(i, chk);
                }
            }
        }
    }
}