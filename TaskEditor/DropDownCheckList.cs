using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>A check list in a drop down combo box.</summary>
	[ToolboxBitmap(typeof(TaskEditDialog), "Control")]
	public class DropDownCheckList : CustomComboBox
	{
		private readonly CheckedListBox checkedListBox1;
		private readonly Timer onCheckTimer;
		private int lastCheckHash;
		private bool privateSet;

		/// <summary>Initializes a new instance of the <see cref="DropDownCheckList"/> class.</summary>
		public DropDownCheckList()
		{
			onCheckTimer = new Timer { Interval = 150 };
			onCheckTimer.Tick += onCheckTimer_Tick;
			checkedListBox1 = new CheckedListBox
			{
				BorderStyle = BorderStyle.None,
				CheckOnClick = true,
				FormattingEnabled = true,
				Location = new Point(17, 35),
				MultiColumn = false,
				Name = "checkedListBox1",
				Size = new Size(187, 105),
				TabIndex = 0
			};
			checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
			DropDownControl = checkedListBox1;
		}

		/// <summary>Occurs when the <see cref="SelectedItems"/> property changes.</summary>
		[Category("Action"), Description("Occurs when the SelectedItems property changes.")]
		public event EventHandler SelectedItemsChanged;

		/// <summary>Gets or sets a value indicating whether to allow only one checked item.</summary>
		/// <value><c>true</c> if allowing only one checked item; otherwise, <c>false</c>.</value>
		[Category("Behavior"), DefaultValue(false)]
		public bool AllowOnlyOneCheckedItem { get; set; }

		/// <summary>Gets or sets the text used on the Check All Items item that, when clicked, will check all the other items.</summary>
		/// <value>The text.</value>
		[DefaultValue(null), Category("Appearance"), Localizable(true)]
		public string CheckAllText { get; set; }

		/// <summary>Gets or sets the logical AND value of all checked items.</summary>
		/// <value>The checked flags bitwise value.</value>
		[DefaultValue(0L), Category("Data"), Browsable(false)]
		public long CheckedFlagValue
		{
			get
			{
				long ret = 0;
				for (var i = 0; i < checkedListBox1.CheckedItems.Count; i++)
				{
					var o = checkedListBox1.CheckedItems[i];
					if (o is ListControlItem li)
						o = li.Value;
					try { ret |= Convert.ToInt64(o); }
					catch { }
				}
				return ret;
			}
			set
			{
				privateSet = true;
				checkedListBox1.BeginUpdate();
				for (var i = 0; i < checkedListBox1.Items.Count; i++)
				{
					long? val = null;
					var o = checkedListBox1.Items[i];
					if (checkedListBox1.Items[i] is ListControlItem)
						o = ((ListControlItem)o).Value;
					try { val = Convert.ToInt64(o); }
					catch { }

					if (val.HasValue && (val.Value & value) == val.Value)
						checkedListBox1.SetItemCheckState(i, CheckState.Checked);
					else
						checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
				}
				checkedListBox1.EndUpdate();
				privateSet = false;
				CheckedItemsChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether formatting is applied to the <see cref="P:System.Windows.Forms.ListControl.DisplayMember"/> property of the <see cref="T:System.Windows.Forms.ListControl"/>.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// true if formatting of the <see cref="P:System.Windows.Forms.ListControl.DisplayMember"/> property is enabled; otherwise, false. The default is false.
		/// </returns>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool FormattingEnabled
		{
			get => base.FormattingEnabled;
			set => base.FormattingEnabled = value;
		}

		/// <summary>Gets or sets a value indicating whether to display the list with multiple columns.</summary>
		/// <value><c>true</c> if multi-column; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Appearance")]
		public bool MultiColumnList
		{
			get => checkedListBox1.MultiColumn;
			set => checkedListBox1.MultiColumn = value;
		}

		/// <summary>Gets or sets a value indicating whether at least one item must be checked.</summary>
		/// <value><c>true</c> if at least one item must be checked; otherwise, <c>false</c>.</value>
		[Category("Behavior"), DefaultValue(false)]
		public bool RequireAtLeastOneCheckedItem { get; set; }

		/// <summary>Gets the selected items.</summary>
		/// <value>The selected items.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListControlItem[] SelectedItems
		{
			get
			{
				var c = checkedListBox1.CheckedItems;
				var ret = new ListControlItem[c.Count];
				for (var i = 0; i < ret.Length; i++)
				{
					ret[i] = c[i] as ListControlItem;
					if (ret[i] == null)
						ret[i] = new ListControlItem(c[i]);
				}
				return ret;
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">
		/// An attempt was made to sort a <see cref="T:System.Windows.Forms.ComboBox"/> that is attached to a data source.
		/// </exception>
		/// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new bool Sorted
		{
			get => checkedListBox1.Sorted;
			set => checkedListBox1.Sorted = value;
		}

		/// <summary>Gets the list of all check list items.</summary>
		/// <value>The items.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")]
		public new CheckedListBox.ObjectCollection Items => checkedListBox1.Items;

		/// <summary>Checks the matching items.</summary>
		/// <param name="match">The match.</param>
		/// <param name="keepExisting">if set to <c>true</c> keep existing checked items.</param>
		public void CheckItems(Predicate<object> match, bool keepExisting = false)
		{
			for (var i = 0; i < checkedListBox1.Items.Count; i++)
			{
				if (match == null || match(checkedListBox1.Items[i]))
					SetItemChecked(i, true);
				else if (!keepExisting)
					SetItemChecked(i, false);
			}
		}

		/// <summary>Gets the check state of the specified item.</summary>
		/// <param name="index">The index of the item.</param>
		/// <returns><c>true</c> if item is checked; otherwise, <c>false</c>.</returns>
		public bool GetItemChecked(int index) => checkedListBox1.GetItemChecked(index);

		/// <summary>Initializes the check list from an enumerated type and sets its initial checks.</summary>
		/// <typeparam name="T">Enumerated type used to set the items.</typeparam>
		/// <param name="val">The flag value used to check the items.</param>
		/// <param name="mgr">The <see cref="System.Resources.ResourceManager"/> that holds the display text for each enumerated value.</param>
		/// <param name="prefix">
		/// (Optional) The prefix used in front of the enumeration value to pull from the resource file. If <c>null</c>, then this value defaults to the name of
		/// the enumerated type specified by <typeparamref name="T"/>.
		/// </param>
		/// <param name="exclude">(Optional) The excluded items from the enumerated type.</param>
		public void InitializeAndSet<T>(T val, ResourceManager mgr, string prefix = null, T[] exclude = null) where T : struct, IConvertible
		{
			EnumUtil.CheckIsEnum<T>(true);
			var excl = exclude == null ? null : Array.ConvertAll(exclude, t => t.ToString());
			InitializeFromEnum(typeof(T), mgr, prefix, excl);
			CheckedFlagValue = Convert.ToInt64(val);
		}

		/// <summary>Initializes the check list from an enumerated type.</summary>
		/// <param name="enumType">The enumerated type.</param>
		/// <param name="mgr">The <see cref="System.Resources.ResourceManager"/> that holds the display text for each enumerated value.</param>
		/// <param name="prefix">
		/// (Optional) The prefix used in front of the enumeration value to pull from the resource file. If <c>null</c>, then this value defaults to the name of
		/// the enumerated type specified by <paramref name="enumType"/>.
		/// </param>
		/// <param name="exclude">(Optional) The excluded items from the enumerated type.</param>
		public void InitializeFromEnum(Type enumType, ResourceManager mgr, string prefix = null, string[] exclude = null)
		{
			if (!enumType.IsEnum)
				throw new ArgumentException("Specified type is not an enumeration.", nameof(enumType));
			if (mgr == null)
				throw new ArgumentNullException(nameof(mgr), "A valid ResourceManager instance must be provided.");
			checkedListBox1.BeginUpdate();
			ComboBoxExtension.InitializeFromEnum(checkedListBox1.Items, enumType, mgr, prefix, out var allVal, exclude);
			if (!string.IsNullOrEmpty(CheckAllText))
				checkedListBox1.Items.Insert(0, new ListControlItem(CheckAllText, allVal));
			checkedListBox1.EndUpdate();
		}

		/// <summary>Initializes the check list from an enumerated type defined in the TaskScheduler assembly</summary>
		/// <param name="enumType">The enumerated type.</param>
		public void InitializeFromTaskEnum(Type enumType)
		{
			checkedListBox1.BeginUpdate();
			checkedListBox1.Items.Clear();
			long allVal = 0;
			var vals = Enum.GetValues(enumType);
			for (var i = 0; i < vals.Length; i++)
			{
				var val = Convert.ToInt64(vals.GetValue(i));
				allVal |= val;
				var text = TaskEnumGlobalizer.GetString(vals.GetValue(i));
				checkedListBox1.Items.Add(new ListControlItem(text, val));
			}
			if (!string.IsNullOrEmpty(CheckAllText))
				checkedListBox1.Items.Insert(0, new ListControlItem(CheckAllText, allVal));
			checkedListBox1.EndUpdate();
		}

		/// <summary>Removes the specified item from the check list.</summary>
		/// <param name="index">The index of the item to remove.</param>
		public void RemoveItem(int index)
		{
			checkedListBox1.Items.RemoveAt(index);
			CheckedItemsChanged();
		}

		/// <summary>Sets the specified item's check state.</summary>
		/// <param name="index">The index of the item.</param>
		/// <param name="value">Checked if set to <c>true</c>; otherwise unchecked.</param>
		public void SetItemChecked(int index, bool value)
		{
			if (GetItemChecked(index) != value)
			{
				checkedListBox1.SetItemChecked(index, value);
				CheckedItemsChanged();
			}
		}

		/// <summary>Unchecks all items.</summary>
		public void UncheckAllItems()
		{
			CheckItems(o => false);
		}

		/// <summary>Updates the text associated with each item of the check list.</summary>
		public void UpdateText()
		{
			var hasCheckAll = !string.IsNullOrEmpty(CheckAllText);
			if (hasCheckAll && checkedListBox1.Items.Count == checkedListBox1.CheckedItems.Count)
				Text = CheckAllText;
			else
			{
				var items = new List<string>(checkedListBox1.CheckedItems.Count);
				foreach (var ci in checkedListBox1.CheckedItems)
				{
					if (!hasCheckAll || ci.ToString() != CheckAllText)
						items.Add(ci.ToString());
				}
				Text = string.Join(", ", items.ToArray());
			}
		}

		internal void InitializeFromRange(int start, int end)
		{
			privateSet = true;
			checkedListBox1.BeginUpdate();
			checkedListBox1.Items.Clear();
			for (var i = start; i <= end; i++)
				checkedListBox1.Items.Add(new ListControlItem(i));
			checkedListBox1.EndUpdate();
			privateSet = false;
		}

		/// <summary>Raises the <see cref="CustomComboBox.DropDown"/> event.</summary>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected override void OnDropDown(EventArgs args)
		{
			base.OnDropDown(args);
			CheckedItemsChanged();
		}

		/// <summary>Raises the <see cref="CustomComboBox.DropDownClosed"/> event.</summary>
		/// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected override void OnDropDownClosed(EventArgs args)
		{
			base.OnDropDownClosed(args);
			CheckedItemsChanged();
		}

		/// <summary>Raises the <see cref="SelectedItemsChanged"/> event.</summary>
		protected virtual void OnSelectedItemsChanged()
		{
			SelectedItemsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void CheckedItemsChanged()
		{
			var newHash = checkedListBox1.CheckedItems.OfType<ListControlItem>().GetItemHashCode();
			if (lastCheckHash != newHash)
			{
				lastCheckHash = newHash;
				UpdateText();
				OnSelectedItemsChanged();
			}
		}

		private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (!privateSet)
			{
				privateSet = true;
				var hasCheckAll = !string.IsNullOrEmpty(CheckAllText);
				var chk = e.NewValue == CheckState.Checked;
				checkedListBox1.BeginUpdate();
				if (e.Index == 0 && hasCheckAll && checkedListBox1.Items.Count > 1)
				{
					for (var i = 1; i < checkedListBox1.Items.Count; i++)
						SetItemChecked(i, chk);
					if (!chk && RequireAtLeastOneCheckedItem)
					{
						SetItemChecked(0, false);
						SetItemChecked(1, true);
					}
				}
				else
				{
					if (AllowOnlyOneCheckedItem)
					{
						if (chk)
						{
							foreach (var i in checkedListBox1.CheckedIndices)
								SetItemChecked((int)i, false);
						}
						else
							e.NewValue = CheckState.Checked;
					}
					else
					{
						if (!chk && RequireAtLeastOneCheckedItem && checkedListBox1.CheckedIndices.Count == 1 && checkedListBox1.CheckedIndices[0] == e.Index)
							e.NewValue = CheckState.Checked;
						if (hasCheckAll)
						{
							if (!chk && GetItemChecked(0))
								SetItemChecked(0, false);
							else if (chk && !GetItemChecked(0))
							{
								var allChecked = true;
								for (var i = 1; i < checkedListBox1.Items.Count; i++)
									if (i != e.Index && !GetItemChecked(i)) { allChecked = false; break; }
								if (allChecked)
									SetItemChecked(0, true);
							}
						}
						if (checkedListBox1.IsHandleCreated)
							BeginInvoke((MethodInvoker)CheckedItemsChanged);
					}
				}
				checkedListBox1.EndUpdate();
				privateSet = false;
			}
			//base.PreventPopupHide = this.checkedListBox1.CheckedIndices.Count == 1 && this.checkedListBox1.CheckedIndices[0] == e.Index && e.NewValue == CheckState.Unchecked;
		}

		private void onCheckTimer_Tick(object sender, EventArgs e)
		{
			onCheckTimer.Stop();
			CheckedItemsChanged();
		}
	}
}