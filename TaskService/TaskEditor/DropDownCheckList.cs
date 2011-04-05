using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// A check list in a drop down combo box.
	/// </summary>
	public partial class DropDownCheckList : CustomComboBox
	{
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private bool privateSet = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckList"/> class.
		/// </summary>
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
		}

		/// <summary>
		/// Occurs when the <see cref="SelectedItems"/> property changes.
		/// </summary>
		[Category("Action"), Description("Occurs when the SelectedItems property changes.")]
		public event EventHandler SelectedItemsChanged;

		/// <summary>
		/// Gets or sets a value indicating whether to allow only one checked item.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if allowing only one checked item; otherwise, <c>false</c>.
		/// </value>
		[Category("Behavior"), DefaultValue(false)]
		public bool AllowOnlyOneCheckedItem { get; set; }

		/// <summary>
		/// Gets or sets the text used on the Check All Items item that, when clicked, will check all the other items.
		/// </summary>
		/// <value>The text.</value>
		[DefaultValue((string)null), Category("Appearance")]
		public string CheckAllText
		{
			get; set;
		}

		/// <summary>
		/// Gets or sets the logical AND value of all checked items.
		/// </summary>
		/// <value>The checked flags bitwise value.</value>
		[DefaultValue(0L), Category("Data"), Browsable(false)]
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
				privateSet = true;
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
				privateSet = false;
				UpdateText();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember"/> property of the <see cref="T:System.Windows.Forms.ListControl"/>.
		/// </summary>
		/// <value></value>
		/// <returns>true if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember"/> property is enabled; otherwise, false. The default is false.
		/// </returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FormattingEnabled
		{
			get { return base.FormattingEnabled;  }
			set { base.FormattingEnabled = value; }
		}

		/// <summary>
		/// Gets the list of all check list items.
		/// </summary>
		/// <value>The items.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")]
		public new CheckedListBox.ObjectCollection Items
		{
			get { return this.checkedListBox1.Items; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether to display the list with multiple columns.
		/// </summary>
		/// <value><c>true</c> if multi-column; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance")]
		public bool MultiColumnList
		{
			get { return this.checkedListBox1.MultiColumn; }
			set { this.checkedListBox1.MultiColumn = value; }
		}

		/// <summary>
		/// Gets the selected items.
		/// </summary>
		/// <value>The selected items.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>
		/// Gets the check state of the specified item.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <returns><c>true</c> if item is checked; otherwise, <c>false</c>.</returns>
		public bool GetItemChecked(int index)
		{
			return this.checkedListBox1.GetItemChecked(index);
		}

		/// <summary>
		/// Initializes the check list from an enumerated type.
		/// </summary>
		/// <param name="enumType">The enumerated type.</param>
		/// <param name="mgr">The <see cref="System.Resources.ResourceManager"/> that holds the display text for each enumerated value.</param>
		/// <param name="prefix">The prefix used in front of the enumeration value to pull from the resource file.</param>
		public void InitializeFromEnum(Type enumType, System.Resources.ResourceManager mgr, string prefix)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException("Specified type is not an enumeration.", "enumType");
			if (mgr == null)
				throw new ArgumentNullException("mgr", "A valid ResourceManager instance must be provided.");
			long allVal;
			ComboBoxExtension.InitializeFromEnum(this.checkedListBox1.Items, enumType, mgr, prefix, out allVal);
			if (!string.IsNullOrEmpty(this.CheckAllText))
				this.checkedListBox1.Items.Insert(0, new DropDownCheckListItem(this.CheckAllText, allVal));
		}

		/// <summary>
		/// Removes the specifed item from the check list.
		/// </summary>
		/// <param name="index">The index of the item to remove.</param>
		public void RemoveItem(int index)
		{
			this.checkedListBox1.Items.RemoveAt(index);
		}

		/// <summary>
		/// Sets the specified item's check state.
		/// </summary>
		/// <param name="index">The index of the item.</param>
		/// <param name="value">Checked if set to <c>true</c>; otherwise unchecked.</param>
		public void SetItemChecked(int index, bool value)
		{
			this.checkedListBox1.SetItemChecked(index, value);
			UpdateText();
		}

		/// <summary>
		/// Updates the text associated with each item of the check list.
		/// </summary>
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
			privateSet = true;
			this.checkedListBox1.Items.Clear();
			for (int i = start; i <= end; i++)
			{
				this.checkedListBox1.Items.Add(new DropDownCheckListItem(i));
			}
			privateSet = false;
		}

		/// <summary>
		/// Raises the <see cref="System.Windows.Forms.ComboBox.DropDownClosed"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnDropDownClosed(EventArgs e)
		{
			base.OnDropDownClosed(e);
			UpdateText();
		}

		/// <summary>
		/// Raises the <see cref="SelectedItemsChanged"/> event.
		/// </summary>
		/// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected virtual void OnSelectedItemsChanged(EventArgs eventArgs)
		{
			EventHandler h = this.SelectedItemsChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

		void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (!privateSet)
			{
				privateSet = true;
				if (e.Index == 0 && !string.IsNullOrEmpty(CheckAllText) && this.checkedListBox1.Items.Count > 1)
				{
					bool chk = !GetItemChecked(0);
					if (!chk) this.checkedListBox1.SetItemChecked(1, true);
					for (int i = chk ? 1 : 2; i < this.checkedListBox1.Items.Count; i++)
						this.checkedListBox1.SetItemChecked(i, chk);
				}
				else
				{
					if (AllowOnlyOneCheckedItem)
					{
						if (e.NewValue == CheckState.Checked)
						{
							foreach (var i in this.checkedListBox1.CheckedIndices)
								this.checkedListBox1.SetItemChecked((int)i, false);
						}
						else
							e.NewValue = CheckState.Checked;
					}
					else
					{
						if (e.NewValue == CheckState.Unchecked && this.checkedListBox1.CheckedIndices.Count == 1 && this.checkedListBox1.CheckedIndices[0] == e.Index)
							e.NewValue = CheckState.Checked;
					}
				}
				privateSet = false;
			}
			//base.PreventPopupHide = this.checkedListBox1.CheckedIndices.Count == 1 && this.checkedListBox1.CheckedIndices[0] == e.Index && e.NewValue == CheckState.Unchecked;
		}
	}

	/// <summary>
	/// An item in a <see cref="DropDownCheckList"/>.
	/// </summary>
	public class DropDownCheckListItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckListItem"/> class.
		/// </summary>
		public DropDownCheckListItem()
			: this(string.Empty, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckListItem"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public DropDownCheckListItem(object value)
			: this(value.ToString(), value)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckListItem"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		public DropDownCheckListItem(string text)
			: this(text, text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckListItem"/> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="value">The value.</param>
		public DropDownCheckListItem(string text, object value)
		{
			Text = text; Value = value;
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value
		{
			get;
			set;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj"/> parameter is null.
		/// </exception>
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

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return Text.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
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
}