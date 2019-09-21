using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using Vanara.PInvoke;
using static Vanara.PInvoke.ComCtl32;
using static Vanara.PInvoke.User32;

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
			get => collapsible;
			set
			{
				if (value != collapsible)
				{
					collapsible = value;
					SetAllGroupState(ListViewGroupState.LVGS_COLLAPSIBLE | ListViewGroupState.LVGS_NORMAL, collapsible);
				}
			}
		}

		[DefaultValue(null), Category("Behavior")]
		public ContextMenuStrip ColumnContextMenuStrip { get; set; }

		public IEnumerable<ListViewGroup> EnumGroups
		{
			get
			{
				foreach (ListViewGroup item in Groups)
					yield return item;
			}
		}

		public IEnumerable<ListViewItem> EnumItems
		{
			get
			{
				foreach (ListViewItem item in Items)
					yield return item;
			}
		}

		[DefaultValue(null), Category("Behavior")]
		public ImageList GroupHeaderImageList
		{
			get => imageListGroup;
			set
			{
				if (imageListGroup != value)
				{
					if (imageListGroup != null)
					{
						imageListGroup.RecreateHandle -= GroupImageListRecreateHandle;
						imageListGroup.Disposed -= DetachImageList;
					}
					imageListGroup = value;
					if (value != null)
					{
						value.RecreateHandle += GroupImageListRecreateHandle;
						value.Disposed += DetachImageList;
					}
					GroupImageListRecreateHandle(imageListGroup, EventArgs.Empty);
					if (!disposingImageList)
						UpdateListViewItemsLocations();
				}
			}
		}

		[MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Behavior"), Localizable(true), Editor(typeof(ListViewGroupCollectionExEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public new ListViewGroupCollectionEx Groups
		{
			get
			{
				if (groups == null)
					groups = new ListViewGroupCollectionEx(this);
				return groups;
			}
		}

		internal ListViewGroupCollection BaseGroups => base.Groups;

		internal IntPtr HeaderHandle
		{
			get
			{
				if (IsHandleCreated)
					return User32.SendMessage(Handle, (uint)ListViewMessage.LVM_GETHEADER);
				return IntPtr.Zero;
			}
		}

		public void SetExplorerTheme(bool on = true)
		{
			if (Environment.OSVersion.Version.Major > 5)
			{
				UxTheme.SetWindowTheme(Handle, on ? "explorer" : null, null);
				SendMessage(ListViewMessage.LVM_SETEXTENDEDLISTVIEWSTYLE, (int)ListViewStyleEx.LVS_EX_DOUBLEBUFFER, on ? (IntPtr)ListViewStyleEx.LVS_EX_DOUBLEBUFFER : IntPtr.Zero);
			}
		}

		public void SetSortIcon(int columnIndex, SortOrder order)
		{
			var columnHeader = HeaderHandle;

			for (var columnNumber = 0; columnNumber <= Columns.Count - 1; columnNumber++)
			{
				// Get current listview column info
				var lvcol = new LVCOLUMN(ListViewColumMask.LVCF_FMT);
				User32.SendMessage(Handle, ListViewMessage.LVM_GETCOLUMN, columnNumber, lvcol);

				// Get current header info
				var hditem = new HDITEM(HeaderItemMask.HDI_FORMAT | HeaderItemMask.HDI_DI_SETITEM);
				User32.SendMessage(columnHeader, HeaderMessage.HDM_GETITEM, columnNumber, hditem);

				// Update header with column info
				hditem.Format |= (HeaderItemFormat)((uint)lvcol.Format & 0x1001803);
				if ((lvcol.Format & ListViewColumnFormat.LVCFMT_NO_TITLE) == 0)
					hditem.ShowText = true;

				// Set header image info
				if (!(order == SortOrder.None) && columnNumber == columnIndex)
					hditem.ImageDisplay = (order == System.Windows.Forms.SortOrder.Descending) ? HeaderItemImageDisplay.DownArrow : HeaderItemImageDisplay.UpArrow;
				else
					hditem.ImageDisplay = HeaderItemImageDisplay.None;

				// Update header
				User32.SendMessage(columnHeader, HeaderMessage.HDM_SETITEM, columnNumber, hditem);
			}
		}

		internal int GetGroupId(ListViewGroup group)
		{
			var mgroup = new LVGROUP(ListViewGroupMask.LVGF_GROUPID);
			try
			{
				if (0 != SendMessage(ListViewMessage.LVM_GETGROUPINFOBYINDEX, base.Groups.IndexOf(group), mgroup))
					return mgroup.ID;
			}
			catch { }
			return -1;
		}

		internal ListViewGroupState GetGroupState(ListViewGroup group, ListViewGroupState stateMask = (ListViewGroupState)0xFF) => (ListViewGroupState)SendMessage(ListViewMessage.LVM_GETGROUPSTATE, GetGroupId(group), new IntPtr((int)stateMask));

		internal void RecreateHandleInternal()
		{
			if (base.IsHandleCreated && StateImageList != null)
			{
				SendMessage(ListViewMessage.LVM_UPDATE, -1, IntPtr.Zero);
			}
			base.RecreateHandle();
		}

		internal void SetGroupState(ListViewGroupEx group, ListViewGroupState item, bool value)
		{
			var mgroup = new LVGROUP(ListViewGroupMask.LVGF_STATE);
			mgroup.SetState(item, value);
			SendMessage(ListViewMessage.LVM_SETGROUPINFO, GetGroupId(group), mgroup);
		}

		internal void UpdateGroupNative(ListViewGroupEx group, bool invalidate = true)
		{
			using (var nGroup = new LVGROUP(ListViewGroupMask.LVGF_HEADER | ListViewGroupMask.LVGF_GROUPID | ListViewGroupMask.LVGF_ALIGN, group.Header))
			{
				nGroup.SetState(ListViewGroupState.LVGS_COLLAPSIBLE, collapsible);
				SendMessage(ListViewMessage.LVM_SETGROUPINFO, group.ID, nGroup);
			}
			if (invalidate)
				base.Invalidate();
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (groups != null)
			{
				for (var i = 0; i < groups.Count; i++)
					UpdateGroupNative(groups[i], false);
			}
		}

		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case 0x7b: //WM_CONTEXTMENU
					{
						var lp = m.LParam.ToInt32();
						var pt = new Point((lp << 16) >> 16, lp >> 16);
						pt = PointToClient(pt);
						var hHdr = HeaderHandle;
						var hti = new HDHITTESTINFO { pt = pt };
						var item = User32.SendMessage(hHdr, HeaderMessage.HDM_HITTEST, 0, hti).ToInt32();
						if (item != -1)
						{
							if (ColumnContextMenuStrip != null)
								ColumnContextMenuStrip.Show(this, pt);
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
			try { base.WndProc(ref m); } catch (Exception ex) { System.Diagnostics.Debug.WriteLine($"ListViewEx WndProc Error: {m} - {ex}"); }
		}

		private void DetachImageList(object sender, EventArgs e)
		{
			disposingImageList = true;
			try
			{
				GroupHeaderImageList = null;
			}
			finally
			{
				disposingImageList = false;
			}
			UpdateListViewItemsLocations();
		}

		private void GroupImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				var lparam = (GroupHeaderImageList == null) ? IntPtr.Zero : GroupHeaderImageList.Handle;
				SendMessage(ListViewMessage.LVM_SETIMAGELIST, 3, lparam);
			}
		}

		private int SendMessage(ListViewMessage msg, int wParam = default, IntPtr lParam = default) => User32.SendMessage(Handle, (uint)msg, (IntPtr)wParam, lParam).ToInt32();

		private int SendMessage(ListViewMessage msg, int wParam, LVGROUP group) => User32.SendMessage(Handle, msg, wParam, group).ToInt32();

		private void SetAllGroupState(ListViewGroupState state, bool on = true)
		{
			var group = new LVGROUP(ListViewGroupMask.LVGF_STATE);
			group.SetState(state, on);
			//group.Subtitle = "Dog";

			foreach (ListViewGroup g in BaseGroups)
				SendMessage(ListViewMessage.LVM_SETGROUPINFO, GetGroupId(g), group);

			using (var grp = new LVGROUP(ListViewGroupMask.LVGF_SUBTITLE))
			{
				var res = SendMessage(ListViewMessage.LVM_GETGROUPINFOBYINDEX, 0, grp);
				if (res >= 0)
					System.Diagnostics.Debug.WriteLine(grp.Subtitle);
			}

			RecreateHandle();
		}

		private void UpdateListViewItemsLocations()
		{
			if (!VirtualMode && base.IsHandleCreated && AutoArrange)
			{
				try
				{
					BeginUpdate();
					SendMessage(ListViewMessage.LVM_UPDATE, -1, IntPtr.Zero);
				}
				finally
				{
					EndUpdate();
				}
			}
		}

		private void WmReflectNotify(ref Message m)
		{
			var nm = (NMHDR)m.GetLParam(typeof(NMHDR));
			if (nm.code == (int)ListViewNotification.LVN_COLUMNDROPDOWN)
			{
				var nmlv = (NMLISTVIEW)m.GetLParam(typeof(NMLISTVIEW));
				var iCol = nmlv.iSubItem;
				var rc = new RECT();
				User32.SendMessage(HeaderHandle, HeaderMessage.HDM_GETITEMDROPDOWNRECT, iCol, ref rc);
				rc = RectangleToClient(rc);
				if (ColumnContextMenuStrip != null)
				{
					ColumnContextMenuStrip.Tag = iCol;
					ColumnContextMenuStrip.Show(this, rc.X, rc.bottom);
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
			: base() => this.listView = listView;

		public bool IsReadOnly => false;
		public int Count => list.Count;

		public ListViewGroupEx this[int index]
		{
			get => list[index];
			set => list[index] = value;
		}

		public ListViewGroupEx this[string key]
		{
			get
			{
				if (list != null)
				{
					for (var i = 0; i < list.Count; i++)
					{
						if (string.Compare(key, this[i].Name, false, System.Globalization.CultureInfo.CurrentUICulture) == 0)
						{
							return this[i];
						}
					}
				}
				return null;
			}
			set
			{
				var num = -1;
				if (list != null)
				{
					for (var i = 0; i < list.Count; i++)
					{
						if (string.Compare(key, this[i].Name, false, System.Globalization.CultureInfo.CurrentUICulture) == 0)
						{
							num = i;
							break;
						}
					}
					if (num != -1)
					{
						list[num] = value;
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
			Add(ret);
			return ret;
		}

		public void AddRange(IEnumerable<ListViewGroupEx> items)
		{
			listView.BeginUpdate();
			foreach (var item in items)
				Add(item);
			listView.EndUpdate();
		}

		public void AddRange(IEnumerable<string> items)
		{
			listView.BeginUpdate();
			foreach (var item in items)
				Add(item);
			listView.EndUpdate();
		}

		public void Clear()
		{
			listView.BaseGroups.Clear();
			list.Clear();
		}

		public bool Contains(ListViewGroupEx item) => list.Contains(item);

		public void CopyTo(ListViewGroupEx[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

		public System.Collections.Generic.IEnumerator<ListViewGroupEx> GetEnumerator() => list.GetEnumerator();

		public int IndexOf(ListViewGroupEx item) => list.IndexOf(item);

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

		public void RemoveAt(int index) => Remove(this[index]);

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
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
			editValue = value;
			var obj2 = base.EditValue(context, provider, value);
			editValue = null;
			return obj2;
		}

		protected override object CreateInstance(Type itemType)
		{
			var group = (ListViewGroupEx)base.CreateInstance(itemType);
			group.Name = CreateListViewGroupName((ListViewGroupCollectionEx)editValue);
			return group;
		}

		private string CreateListViewGroupName(ListViewGroupCollectionEx lvgCollection)
		{
			var str = "ListViewGroupEx";
			var container = base.GetService(typeof(IContainer)) as IContainer;
			if (base.GetService(typeof(INameCreationService)) is INameCreationService service && container != null)
			{
				str = service.CreateName(container, typeof(ListViewGroupEx));
			}
			while (char.IsDigit(str[str.Length - 1]))
			{
				str = str.Substring(0, str.Length - 1);
			}
			var num = 1;
			var str2 = str + num.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			while (lvgCollection[str2] != null)
			{
				num++;
				str2 = str + num.ToString(System.Globalization.CultureInfo.CurrentUICulture);
			}
			return str2;
		}
	}

	[Serializable, ToolboxItem(false), DesignTimeVisible(false), DefaultProperty("Header"), TypeConverter(typeof(ListViewGroupExConverter))]
	internal class ListViewGroupEx
	{
		internal ListViewGroup baseGroup;

		private bool? collapsed, hidden, noheader, focused, selected, collapsible, subseted, subsetlinkfocused;
		private string footer, task, subtitle, descTop, descBottom;
		private HorizontalAlignment footerAlign = HorizontalAlignment.Left;
		private int titleImageIndex;

		public ListViewGroupEx() => baseGroup = new ListViewGroup();

		public ListViewGroupEx(string header, HorizontalAlignment headerAlignment = HorizontalAlignment.Left, string key = null) => baseGroup = new ListViewGroup(key, header) { HeaderAlignment = headerAlignment };

		public bool Collapsed
		{
			get => GetState(ListViewGroupState.LVGS_COLLAPSED | ListViewGroupState.LVGS_NORMAL, ref collapsed);
			set => SetState(ListViewGroupState.LVGS_COLLAPSED | ListViewGroupState.LVGS_NORMAL, value, ref collapsed);
		}

		public bool Collapsible
		{
			get => GetState(ListViewGroupState.LVGS_COLLAPSIBLE, ref collapsible);
			set => SetState(ListViewGroupState.LVGS_COLLAPSIBLE, value, ref collapsible);
		}

		[DefaultValue(""), Category("Appearance")]
		public string DescriptionBottom
		{
			get => descBottom ?? string.Empty;
			set
			{
				if (descBottom != value)
				{
					descBottom = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(""), Category("Appearance")]
		public string DescriptionTop
		{
			get => descTop ?? string.Empty;
			set
			{
				if (descTop != value)
				{
					descTop = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		public bool Focused
		{
			get => GetState(ListViewGroupState.LVGS_FOCUSED, ref focused);
			set => SetState(ListViewGroupState.LVGS_FOCUSED, value, ref focused);
		}

		[DefaultValue(""), Category("Appearance")]
		public string Footer
		{
			get => footer ?? string.Empty;
			set
			{
				if (footer != value)
				{
					footer = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(0), Category("Appearance")]
		public HorizontalAlignment FooterAlignment
		{
			get => footerAlign;
			set
			{
				if (footerAlign != value)
				{
					footerAlign = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		[DefaultValue(""), Category("Appearance")]
		public string Header
		{
			get => baseGroup.Header;
			set => baseGroup.Header = value;
		}

		[DefaultValue(0), Category("Appearance")]
		public HorizontalAlignment HeaderAlignment
		{
			get => baseGroup.HeaderAlignment;
			set => baseGroup.HeaderAlignment = value;
		}

		public bool Hidden
		{
			get => GetState(ListViewGroupState.LVGS_HIDDEN, ref hidden);
			set => SetState(ListViewGroupState.LVGS_HIDDEN, value, ref hidden);
		}

		[DefaultValue(""), Category("Behavior"), Browsable(true)]
		public string Name
		{
			get => baseGroup.Name;
			set => baseGroup.Name = value;
		}

		public bool NoHeader
		{
			get => GetState(ListViewGroupState.LVGS_NOHEADER, ref noheader);
			set => SetState(ListViewGroupState.LVGS_NOHEADER, value, ref noheader);
		}

		public bool Selected
		{
			get => GetState(ListViewGroupState.LVGS_SELECTED, ref selected);
			set => SetState(ListViewGroupState.LVGS_SELECTED, value, ref selected);
		}

		public bool Subseted
		{
			get => GetState(ListViewGroupState.LVGS_SUBSETED, ref subseted);
			set => SetState(ListViewGroupState.LVGS_SUBSETED, value, ref subseted);
		}

		public bool SubsetLinkFocused
		{
			get => GetState(ListViewGroupState.LVGS_SUBSETLINKFOCUSED, ref subsetlinkfocused);
			set => SetState(ListViewGroupState.LVGS_SUBSETLINKFOCUSED, value, ref subsetlinkfocused);
		}

		[DefaultValue(""), Category("Appearance")]
		public string Subtitle
		{
			get => subtitle ?? string.Empty;
			set
			{
				if (subtitle != value)
				{
					subtitle = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		[Localizable(false), Bindable(true), TypeConverter(typeof(StringConverter)), Category("Data"), DefaultValue(null)]
		public object Tag
		{
			get => baseGroup.Tag;
			set => baseGroup.Tag = value;
		}

		[DefaultValue(""), Category("Appearance")]
		public string Task
		{
			get => task ?? string.Empty;
			set
			{
				if (task != value)
				{
					task = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		public int TitleImageIndex
		{
			get => titleImageIndex;
			set
			{
				if (titleImageIndex != value)
				{
					titleImageIndex = value;
					if (ListView != null)
						ListView.UpdateGroupNative(this);
				}
			}
		}

		[Browsable(false)]
		public ListView.ListViewItemCollection Items => baseGroup.Items;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public ListViewEx ListView => baseGroup.ListView as ListViewEx;

		internal int ID
		{
			get
			{
				if (baseGroup.ListView != null && baseGroup.ListView is ListViewEx)
					return ((ListViewEx)baseGroup.ListView).GetGroupId(baseGroup);
				return 0;
			}
		}

		public static implicit operator ListViewGroup(ListViewGroupEx x) => x.baseGroup;

		internal void GetSetState(out ListViewGroupState m, out ListViewGroupState s)
		{
			m = s = ListViewGroupState.LVGS_NORMAL;
			if (collapsed.HasValue) { m |= ListViewGroupState.LVGS_COLLAPSED; if (collapsed.Value) s |= ListViewGroupState.LVGS_COLLAPSED; }
			if (collapsible.HasValue) { m |= ListViewGroupState.LVGS_COLLAPSIBLE; if (collapsible.Value) s |= ListViewGroupState.LVGS_COLLAPSIBLE; }
			if (focused.HasValue) { m |= ListViewGroupState.LVGS_FOCUSED; if (focused.Value) s |= ListViewGroupState.LVGS_FOCUSED; }
			if (hidden.HasValue) { m |= ListViewGroupState.LVGS_HIDDEN; if (hidden.Value) s |= ListViewGroupState.LVGS_HIDDEN; }
			if (noheader.HasValue) { m |= ListViewGroupState.LVGS_NOHEADER; if (noheader.Value) s |= ListViewGroupState.LVGS_NOHEADER; }
			if (selected.HasValue) { m |= ListViewGroupState.LVGS_SELECTED; if (selected.Value) s |= ListViewGroupState.LVGS_SELECTED; }
			if (subseted.HasValue) { m |= ListViewGroupState.LVGS_SUBSETED; if (subseted.Value) s |= ListViewGroupState.LVGS_SUBSETED; }
			if (subsetlinkfocused.HasValue) { m |= ListViewGroupState.LVGS_SUBSETLINKFOCUSED; if (subsetlinkfocused.Value) s |= ListViewGroupState.LVGS_SUBSETLINKFOCUSED; }
		}

		private bool GetState(ListViewGroupState item, ref bool? localVar)
		{
			if (ListView != null)
			{
				var s = ListView.GetGroupState(this, item);
				localVar = (s & item) != 0;
			}
			return localVar.GetValueOrDefault(false);
		}

		private void SetState(ListViewGroupState item, bool on, ref bool? localVar)
		{
			if (ListView != null)
				ListView.SetGroupState(this, item, on);
			localVar = on;
		}
	}

	internal class ListViewGroupExConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof(string) && context != null && context.Instance is ListViewItem || base.CanConvertFrom(context, sourceType);

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) && context != null && context.Instance is ListViewItem || base.CanConvertTo(context, destinationType);

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				var str = ((string)value).Trim();
				if (context != null && context.Instance != null)
				{
					if (context.Instance is ListViewItem instance && instance.ListView != null)
					{
						foreach (var group in ((ListViewEx)instance.ListView).Groups)
						{
							if (group.Header == str)
							{
								return group;
							}
						}
					}
				}
			}
			if (value != null && !value.Equals("None"))
			{
				return base.ConvertFrom(context, culture, value);
			}
			return null;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException(nameof(destinationType));
			}
			if (destinationType == typeof(InstanceDescriptor) && value is ListViewGroupEx)
			{
				var group = (ListViewGroupEx)value;
				var constructor = typeof(ListViewGroupEx).GetConstructor(System.Type.EmptyTypes);
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, null, false);
				}
			}
			if (destinationType == typeof(string) && value == null)
			{
				return "None";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context != null && context.Instance != null)
			{
				if (context.Instance is ListViewItem instance && instance.ListView != null)
				{
					var values = new ArrayList();
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

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
	}
}