using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	internal class ListViewEx : ListView
	{
		private bool collapsible = false;
		private bool disposingImageList = false;
		private ListViewGroupCollectionEx groups;
		private ImageList imageListGroup;

		public ListViewEx() : base()
		{
		}

		[DefaultValue(false), Category("Behavior")]
		public bool CollapsibleGroups
		{
			get { return collapsible; }
			set
			{
				if (value != collapsible)
				{
					collapsible = value;
					SetAllGroupState(NativeMethods.ListViewGroupState.Collapsible | NativeMethods.ListViewGroupState.Normal, collapsible);
				}
			}
		}

		[DefaultValue(null), Category("Behavior")]
		public ContextMenuStrip ColumnContextMenuStrip { get; set; }

		[DefaultValue((string)null), Category("Behavior")]
		public ImageList GroupHeaderImageList
		{
			get { return imageListGroup; }
			set
			{
				if (imageListGroup != value)
				{
					if (this.imageListGroup != null)
					{
						this.imageListGroup.RecreateHandle -= this.GroupImageListRecreateHandle;
						this.imageListGroup.Disposed -= this.DetachImageList;
					}
					this.imageListGroup = value;
					if (value != null)
					{
						value.RecreateHandle += this.GroupImageListRecreateHandle;
						value.Disposed += this.DetachImageList;
					}
					GroupImageListRecreateHandle(imageListGroup, EventArgs.Empty);
					if (!this.disposingImageList)
						UpdateListViewItemsLocations();
				}
			}
		}

		[MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Behavior"), Localizable(true), Editor(typeof(ListViewGroupCollectionExEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public new ListViewGroupCollectionEx Groups
		{
			get
			{
				if (this.groups == null)
					this.groups = new ListViewGroupCollectionEx(this);
				return this.groups;
			}
		}

		internal ListViewGroupCollection BaseGroups
		{
			get { return base.Groups; }
		}

		internal IntPtr HeaderHandle
		{
			get
			{
				if (this.IsHandleCreated)
					return NativeMethods.SendMessage(this.Handle, (uint)NativeMethods.ListViewMessage.GetHeader, IntPtr.Zero, IntPtr.Zero);
				return IntPtr.Zero;
			}
		}

		internal int GetGroupId(ListViewGroup group)
		{
			NativeMethods.LVGROUP mgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.GroupId);
			try
			{
				if (0 != SendMessage(NativeMethods.ListViewMessage.GetGroupInfoByIndex, base.Groups.IndexOf(group), mgroup))
					return mgroup.ID;
			}
			catch { }
			return -1;
		}

		internal NativeMethods.ListViewGroupState GetGroupState(ListViewGroup group, NativeMethods.ListViewGroupState stateMask = (NativeMethods.ListViewGroupState)0xFF)
		{
			return (NativeMethods.ListViewGroupState)SendMessage(NativeMethods.ListViewMessage.GetGroupState, GetGroupId(group), new IntPtr((int)stateMask));
		}

		internal void RecreateHandleInternal()
		{
			if (base.IsHandleCreated && (this.StateImageList != null))
			{
				SendMessage(NativeMethods.ListViewMessage.Update, -1, IntPtr.Zero);
			}
			base.RecreateHandle();
		}

		internal void UpdateGroupNative(ListViewGroupEx group, bool invalidate = true)
		{
			using (NativeMethods.LVGROUP nGroup = new NativeMethods.LVGROUP(group))
			{
				nGroup.SetState(NativeMethods.ListViewGroupState.Collapsible, collapsible);
				SendMessage(NativeMethods.ListViewMessage.SetGroupInfo, group.ID, nGroup);
			}
			if (invalidate)
				base.Invalidate();
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.groups != null)
			{
				for (int i = 0; i < this.groups.Count; i++)
					UpdateGroupNative(this.groups[i], false);
			}
		}

		private void WmReflectNotify(ref Message m)
		{
			var nm = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			if (nm.code == (int)NativeMethods.ListViewNotifications.ColumnDropDown)
			{
				var nmlv = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
				var iCol = nmlv.iSubItem;
				NativeMethods.RECT rc = new NativeMethods.RECT();
				NativeMethods.SendMessage(HeaderHandle, (uint)NativeMethods.HeaderMessage.GetItemDropDownRect, (IntPtr)iCol, ref rc);
				if (this.ColumnContextMenuStrip != null)
				{
					this.ColumnContextMenuStrip.Tag = iCol;
					this.ColumnContextMenuStrip.Show(this, rc.X, rc.Bottom);
				}
			}
		}

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x7b: //WM_CONTEXTMENU
					{
						int lp = m.LParam.ToInt32();
						Point pt = new Point(((lp << 16) >> 16), lp >> 16);
						IntPtr hHdr = HeaderHandle;
						if (0 != NativeMethods.MapWindowPoints(IntPtr.Zero, hHdr, ref pt, 1))
						{
							var hti = new NativeMethods.HDHITTESTINFO(pt);
							int item = NativeMethods.SendMessage(hHdr, NativeMethods.HeaderMessage.HitTest, 0, hti).ToInt32();
							if (item != -1)
							{
								if (ColumnContextMenuStrip != null)
									ColumnContextMenuStrip.Show(pt);
							}
						}
					}
					break;

				case 0x204E: // WM_NOTIFY
					WmReflectNotify(ref m);
					break;
				case 0x0200: // WM_MOUSEMOVE
					return;
				case 0x0202: // WM_LBUTTONUP
				default:
					break;
			}
			base.WndProc(ref m);
		}

		private void DetachImageList(object sender, EventArgs e)
		{
			this.disposingImageList = true;
			try
			{
				this.GroupHeaderImageList = null;
			}
			finally
			{
				this.disposingImageList = false;
			}
			this.UpdateListViewItemsLocations();
		}

		private void GroupImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr lparam = (this.GroupHeaderImageList == null) ? IntPtr.Zero : this.GroupHeaderImageList.Handle;
				SendMessage(NativeMethods.ListViewMessage.SetImageList, 3, lparam);
			}
		}

		private int SendMessage(NativeMethods.ListViewMessage msg, int wParam, IntPtr lParam)
		{
			return NativeMethods.SendMessage(this.Handle, (uint)msg, (IntPtr)wParam, lParam).ToInt32();
		}

		private int SendMessage(NativeMethods.ListViewMessage msg, int wParam, NativeMethods.LVGROUP group)
		{
			return NativeMethods.SendMessage(this.Handle, msg, wParam, group).ToInt32();
		}

		private void SetAllGroupState(NativeMethods.ListViewGroupState state, bool on = true)
		{
			NativeMethods.LVGROUP group = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.State);
			group.SetState(state, on);
			//group.Subtitle = "Dog";

			foreach (ListViewGroup g in this.BaseGroups)
				SendMessage(NativeMethods.ListViewMessage.SetGroupInfo, GetGroupId(g), group);

			using (NativeMethods.LVGROUP grp = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.Subtitle))
			{
				int res = SendMessage(NativeMethods.ListViewMessage.GetGroupInfoByIndex, 0, grp);
				if (res >= 0)
					System.Diagnostics.Debug.WriteLine(grp.Subtitle);
			}

			RecreateHandle();
		}

		internal void SetGroupState(ListViewGroupEx group, NativeMethods.ListViewGroupState item, bool value)
		{
			NativeMethods.LVGROUP mgroup = new NativeMethods.LVGROUP(NativeMethods.ListViewGroupMask.State);
			mgroup.SetState(item, value);
			SendMessage(NativeMethods.ListViewMessage.SetGroupInfo, GetGroupId(group), mgroup);
		}

		public void SetSortIcon(int columnIndex, SortOrder order)
		{
			IntPtr columnHeader = this.HeaderHandle;

			for (int columnNumber = 0; columnNumber <= this.Columns.Count - 1; columnNumber++)
			{
				// Get current listview column info
				var lvcol = new NativeMethods.LVCOLUMN(NativeMethods.ListViewColumMask.Fmt);
				NativeMethods.SendMessage(this.Handle, NativeMethods.ListViewMessage.GetColumn, columnNumber, lvcol);

				// Get current header info
				var hditem = new NativeMethods.HDITEM(NativeMethods.HeaderItemMask.Format | NativeMethods.HeaderItemMask.DISetItem);
				NativeMethods.SendMessage(columnHeader, NativeMethods.HeaderMessage.GetItem, columnNumber, hditem);

				// Update header with column info
				hditem.Format |= (NativeMethods.HeaderItemFormat)((uint)lvcol.Format & 0x1001803);
				if ((lvcol.Format & NativeMethods.ListViewColumnFormat.NoTitle) == 0)
					hditem.ShowText = true;

				// Set header image info
				if (!(order == SortOrder.None) && columnNumber == columnIndex)
					hditem.ImageDisplay = (order == System.Windows.Forms.SortOrder.Descending) ? NativeMethods.HeaderItemImageDisplay.DownArrow : NativeMethods.HeaderItemImageDisplay.UpArrow;
				else
					hditem.ImageDisplay = NativeMethods.HeaderItemImageDisplay.None;

				// Update header
				NativeMethods.SendMessage(columnHeader, NativeMethods.HeaderMessage.SetItem, columnNumber, hditem);
			}
		}

		private void UpdateListViewItemsLocations()
		{
			if (((!this.VirtualMode && base.IsHandleCreated) && this.AutoArrange))
			{
				try
				{
					this.BeginUpdate();
					SendMessage(NativeMethods.ListViewMessage.Update, -1, IntPtr.Zero);
				}
				finally
				{
					this.EndUpdate();
				}
			}
		}
	}

	[ListBindable(false)]
	internal class ListViewGroupCollectionEx : System.Collections.Generic.IList<ListViewGroupEx>
	{
		private System.Collections.Generic.List<ListViewGroupEx> list = new System.Collections.Generic.List<ListViewGroupEx>();
		private ListViewEx listView;

		internal ListViewGroupCollectionEx(ListViewEx listView)
			: base()
		{
			this.listView = listView;
		}

		public int Count
		{
			get { return list.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public ListViewGroupEx this[int index]
		{
			get { return list[index]; }
			set { list[index] = value; }
		}

		public ListViewGroupEx this[string key]
		{
			get
			{
				if (this.list != null)
				{
					for (int i = 0; i < this.list.Count; i++)
					{
						if (string.Compare(key, this[i].Name, false, System.Globalization.CultureInfo.CurrentCulture) == 0)
						{
							return this[i];
						}
					}
				}
				return null;
			}
			set
			{
				int num = -1;
				if (this.list != null)
				{
					for (int i = 0; i < this.list.Count; i++)
					{
						if (string.Compare(key, this[i].Name, false, System.Globalization.CultureInfo.CurrentCulture) == 0)
						{
							num = i;
							break;
						}
					}
					if (num != -1)
					{
						this.list[num] = value;
					}
				}
			}
		}

		public void Add(ListViewGroupEx item)
		{
			list.Add(item);
			listView.BaseGroups.Add(item.baseGroup);
		}

		public ListViewGroupEx Add(string header)
		{
			var ret = new ListViewGroupEx(header);
			this.Add(ret);
			return ret;
		}

		public void AddRange(IEnumerable<ListViewGroupEx> items)
		{
			this.listView.BeginUpdate();
			foreach (var item in items)
				this.Add(item);
			this.listView.EndUpdate();
		}

		public void AddRange(IEnumerable<string> items)
		{
			this.listView.BeginUpdate();
			foreach (var item in items)
				this.Add(item);
			this.listView.EndUpdate();
		}

		public void Clear()
		{
			listView.BaseGroups.Clear();
			list.Clear();
		}

		public bool Contains(ListViewGroupEx item)
		{
			return list.Contains(item);
		}

		public void CopyTo(ListViewGroupEx[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public System.Collections.Generic.IEnumerator<ListViewGroupEx> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(ListViewGroupEx item)
		{
			return list.IndexOf(item);
		}

		public void Insert(int index, ListViewGroupEx item)
		{
			list.Insert(index, item);
			listView.BaseGroups.Insert(index, item.baseGroup);
		}

		public bool Remove(ListViewGroupEx item)
		{
			listView.BaseGroups.Remove(item.baseGroup);
			return list.Remove(item);
		}

		public void RemoveAt(int index)
		{
			Remove(this[index]);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}

	[Serializable, ToolboxItem(false), DesignTimeVisible(false), DefaultProperty("Header"), TypeConverter(typeof(ListViewGroupExConverter))]
	internal class ListViewGroupEx
	{
		internal ListViewGroup baseGroup;

		string footer, task, subtitle, descTop, descBottom;
		HorizontalAlignment footerAlign = HorizontalAlignment.Left;
		private int titleImageIndex;
		private bool? collapsed, hidden, noheader, focused, selected, collapsible, subseted, subsetlinkfocused;

		public ListViewGroupEx()
		{
			baseGroup = new ListViewGroup();
		}

		public ListViewGroupEx(string header, HorizontalAlignment headerAlignment = HorizontalAlignment.Left, string key = null)
		{
			baseGroup = new ListViewGroup(key, header) { HeaderAlignment = headerAlignment };
		}

		public bool Collapsed
		{
			get { return GetState(NativeMethods.ListViewGroupState.Collapsed | NativeMethods.ListViewGroupState.Normal, ref this.collapsed); }
			set { SetState(NativeMethods.ListViewGroupState.Collapsed | NativeMethods.ListViewGroupState.Normal, value, ref this.collapsed); }
		}

		public bool Hidden
		{
			get { return GetState(NativeMethods.ListViewGroupState.Hidden, ref this.hidden); }
			set { SetState(NativeMethods.ListViewGroupState.Hidden, value, ref this.hidden); }
		}

		public bool NoHeader
		{
			get { return GetState(NativeMethods.ListViewGroupState.NoHeader, ref this.noheader); }
			set { SetState(NativeMethods.ListViewGroupState.NoHeader, value, ref this.noheader); }
		}

		public bool Collapsible
		{
			get { return GetState(NativeMethods.ListViewGroupState.Collapsible, ref this.collapsible); }
			set { SetState(NativeMethods.ListViewGroupState.Collapsible, value, ref this.collapsible); }
		}

		public bool Focused
		{
			get { return GetState(NativeMethods.ListViewGroupState.Focused, ref this.focused); }
			set { SetState(NativeMethods.ListViewGroupState.Focused, value, ref this.focused); }
		}

		public bool Selected
		{
			get { return GetState(NativeMethods.ListViewGroupState.Selected, ref this.selected); }
			set { SetState(NativeMethods.ListViewGroupState.Selected, value, ref this.selected); }
		}

		public bool Subseted
		{
			get { return GetState(NativeMethods.ListViewGroupState.Subseted, ref this.subseted); }
			set { SetState(NativeMethods.ListViewGroupState.Subseted, value, ref this.subseted); }
		}

		public bool SubsetLinkFocused
		{
			get { return GetState(NativeMethods.ListViewGroupState.SubsetLinkFocused, ref this.subsetlinkfocused); }
			set { SetState(NativeMethods.ListViewGroupState.SubsetLinkFocused, value, ref this.subsetlinkfocused); }
		}

		private void SetState(NativeMethods.ListViewGroupState item, bool on, ref bool? localVar)
		{
			if (this.ListView != null)
				this.ListView.SetGroupState(this, item, on);
			localVar = on;
		}

		private bool GetState(NativeMethods.ListViewGroupState item, ref bool? localVar)
		{
			if (this.ListView != null)
			{
				NativeMethods.ListViewGroupState s = this.ListView.GetGroupState(this, item);
				localVar = ((s & item) != 0);
			}
			return localVar.GetValueOrDefault(false);
		}
		
		[DefaultValue(""), Category("Appearance")]
		public string DescriptionBottom
		{
			get { return descBottom != null ? descBottom : string.Empty; }
			set
			{
				if (descBottom != value)
				{
					this.descBottom = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(""), Category("Appearance")]
		public string DescriptionTop
		{
			get { return descTop != null ? descTop : string.Empty; }
			set
			{
				if (descTop != value)
				{
					this.descTop = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(""), Category("Appearance")]
		public string Footer
		{
			get { return footer != null ? footer : string.Empty; }
			set
			{
				if (footer != value)
				{
					this.footer = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(0), Category("Appearance")]
		public HorizontalAlignment FooterAlignment
		{
			get { return footerAlign; }
			set
			{
				if (footerAlign != value)
				{
					this.footerAlign = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(""), Category("Appearance")]
		public string Header
		{
			get { return baseGroup.Header; }
			set { baseGroup.Header = value; }
		}

		[DefaultValue(0), Category("Appearance")]
		public HorizontalAlignment HeaderAlignment
		{
			get { return baseGroup.HeaderAlignment; }
			set { baseGroup.HeaderAlignment = value; }
		}

		internal int ID
		{
			get
			{
				if (baseGroup.ListView != null && baseGroup.ListView is ListViewEx)
					return ((ListViewEx)baseGroup.ListView).GetGroupId(baseGroup);
				return 0;
			}
		}

		[Browsable(false)]
		public ListView.ListViewItemCollection Items
		{
			get { return baseGroup.Items; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public ListViewEx ListView
		{
			get { return baseGroup.ListView as ListViewEx; }
		}

		[DefaultValue(""), Category("Behavior"), Browsable(true)]
		public string Name
		{
			get { return baseGroup.Name; }
			set { baseGroup.Name = value; }
		}

		[DefaultValue(""), Category("Appearance")]
		public string Subtitle
		{
			get { return subtitle != null ? subtitle : string.Empty; }
			set
			{
				if (subtitle != value)
				{
					this.subtitle = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		[Localizable(false), Bindable(true), TypeConverter(typeof(StringConverter)), Category("Data"), DefaultValue((string)null)]
		public object Tag
		{
			get { return baseGroup.Tag; }
			set { baseGroup.Tag = value; }
		}

		[DefaultValue(""), Category("Appearance")]
		public string Task
		{
			get { return task != null ? task : string.Empty; }
			set
			{
				if (task != value)
				{
					this.task = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}

		public int TitleImageIndex
		{
			get { return titleImageIndex; }
			set
			{
				if (titleImageIndex != value)
				{
					titleImageIndex = value;
					if (this.ListView != null)
						this.ListView.UpdateGroupNative(this);
				}
			}
		}
		
		public static implicit operator ListViewGroup(ListViewGroupEx x)
		{
			return x.baseGroup;
		}

		internal void GetSetState(out NativeMethods.ListViewGroupState m, out NativeMethods.ListViewGroupState s)
		{
			m = s = NativeMethods.ListViewGroupState.Normal;
			if (collapsed.HasValue) { m |= NativeMethods.ListViewGroupState.Collapsed; if (collapsed.Value) s |= NativeMethods.ListViewGroupState.Collapsed; }
			if (collapsible.HasValue) { m |= NativeMethods.ListViewGroupState.Collapsible; if (collapsible.Value) s |= NativeMethods.ListViewGroupState.Collapsible; }
			if (focused.HasValue) { m |= NativeMethods.ListViewGroupState.Focused; if (focused.Value) s |= NativeMethods.ListViewGroupState.Focused; }
			if (hidden.HasValue) { m |= NativeMethods.ListViewGroupState.Hidden; if (hidden.Value) s |= NativeMethods.ListViewGroupState.Hidden; }
			if (noheader.HasValue) { m |= NativeMethods.ListViewGroupState.NoHeader; if (noheader.Value) s |= NativeMethods.ListViewGroupState.NoHeader; }
			if (selected.HasValue) { m |= NativeMethods.ListViewGroupState.Selected; if (selected.Value) s |= NativeMethods.ListViewGroupState.Selected; }
			if (subseted.HasValue) { m |= NativeMethods.ListViewGroupState.Subseted; if (subseted.Value) s |= NativeMethods.ListViewGroupState.Subseted; }
			if (subsetlinkfocused.HasValue) { m |= NativeMethods.ListViewGroupState.SubsetLinkFocused; if (subsetlinkfocused.Value) s |= NativeMethods.ListViewGroupState.SubsetLinkFocused; }
		}
	}

	internal class ListViewGroupCollectionExEditor : CollectionEditor
	{
		// Fields
		private object editValue;

		// Methods
		public ListViewGroupCollectionExEditor(Type type)
			: base(type)
		{
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			this.editValue = value;
			object obj2 = base.EditValue(context, provider, value);
			this.editValue = null;
			return obj2;
		}

		protected override object CreateInstance(Type itemType)
		{
			ListViewGroupEx group = (ListViewGroupEx)base.CreateInstance(itemType);
			group.Name = this.CreateListViewGroupName((ListViewGroupCollectionEx)this.editValue);
			return group;
		}

		private string CreateListViewGroupName(ListViewGroupCollectionEx lvgCollection)
		{
			string str = "ListViewGroupEx";
			INameCreationService service = base.GetService(typeof(INameCreationService)) as INameCreationService;
			IContainer container = base.GetService(typeof(IContainer)) as IContainer;
			if ((service != null) && (container != null))
			{
				str = service.CreateName(container, typeof(ListViewGroupEx));
			}
			while (char.IsDigit(str[str.Length - 1]))
			{
				str = str.Substring(0, str.Length - 1);
			}
			int num = 1;
			string str2 = str + num.ToString(System.Globalization.CultureInfo.CurrentCulture);
			while (lvgCollection[str2] != null)
			{
				num++;
				str2 = str + num.ToString(System.Globalization.CultureInfo.CurrentCulture);
			}
			return str2;
		}
	}

	internal class ListViewGroupExConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((((sourceType == typeof(string)) && (context != null)) && (context.Instance is ListViewItem)) || base.CanConvertFrom(context, sourceType));
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return ((destinationType == typeof(InstanceDescriptor)) || ((((destinationType == typeof(string)) && (context != null)) && (context.Instance is ListViewItem)) || base.CanConvertTo(context, destinationType)));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string str = ((string)value).Trim();
				if ((context != null) && (context.Instance != null))
				{
					ListViewItem instance = context.Instance as ListViewItem;
					if ((instance != null) && (instance.ListView != null))
					{
						foreach (ListViewGroupEx group in ((ListViewEx)instance.ListView).Groups)
						{
							if (group.Header == str)
							{
								return group;
							}
						}
					}
				}
			}
			if ((value != null) && !value.Equals("None"))
			{
				return base.ConvertFrom(context, culture, value);
			}
			return null;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if ((destinationType == typeof(InstanceDescriptor)) && (value is ListViewGroupEx))
			{
				ListViewGroupEx group = (ListViewGroupEx)value;
				ConstructorInfo constructor = typeof(ListViewGroupEx).GetConstructor(System.Type.EmptyTypes);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, null, false);
				}
			}
			if ((destinationType == typeof(string)) && (value == null))
			{
				return "None";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if ((context != null) && (context.Instance != null))
			{
				ListViewItem instance = context.Instance as ListViewItem;
				if ((instance != null) && (instance.ListView != null))
				{
					ArrayList values = new ArrayList();
					foreach (ListViewGroup group in instance.ListView.Groups)
					{
						values.Add(group);
					}
					values.Add(null);
					return new TypeConverter.StandardValuesCollection(values);
				}
			}
			return null;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}