using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// A check list in a drop down combo box.
	/// </summary>
	public partial class DropDownCheckTree : CustomComboBox
	{
		private List<TreeNode> checkedItems = new List<TreeNode>();
		private System.Windows.Forms.TreeView checkedTreeView;
		private bool updatingChecks;
		private Dictionary<string, TreeNode> values = new Dictionary<string, TreeNode>();

		/// <summary>
		/// Initializes a new instance of the <see cref="DropDownCheckTree"/> class.
		/// </summary>
		public DropDownCheckTree()
		{
			this.checkedTreeView = new System.Windows.Forms.TreeView()
			{
				BorderStyle = System.Windows.Forms.BorderStyle.None,
				CheckBoxes = true,
				Name = "checkedTreeView",
				ShowLines = true,
				ShowPlusMinus = true,
				Tag = this,
				Visible = false
			};
			this.checkedTreeView.AfterCheck += checkedTreeView_ItemCheck;
			base.DropDownControl = this.checkedTreeView;
			base.DropSize = new System.Drawing.Size(base.DropDownWidth, 400);
		}

		private delegate void NodeAction(TreeNode node, bool childrenChecked);

		/// <summary>
		/// Occurs when the <see cref="CheckedItems"/> property changes.
		/// </summary>
		[Category("Action"), Description("Occurs when the SelectedItems property changes.")]
		public event EventHandler SelectedItemsChanged;

		/// <summary>
		/// Gets or sets the text used on the Check All Items item that, when clicked, will check all the other items.
		/// </summary>
		/// <value>The text.</value>
		[DefaultValue((string)null), Category("Appearance"), Localizable(true)]
		public string CheckAllText { get; set; }

		/// <summary>
		/// Gets the checked items.
		/// </summary>
		/// <value>The checked items.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<TreeNode> CheckedItems
		{
			get { return this.checkedItems; }
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
			get { return base.FormattingEnabled; }
			set { base.FormattingEnabled = value; }
		}

		/// <summary>
		/// Gets the list of all check list items.
		/// </summary>
		/// <value>The items.</value>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")]
		public new TreeNodeCollection Items
		{
			get { return this.checkedTreeView.Nodes; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether the items in the combo box are sorted.
		/// </summary>
		/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">
		/// An attempt was made to sort a <see cref="T:System.Windows.Forms.ComboBox"/> that is attached to a data source.
		/// </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		public new bool Sorted
		{
			get { return this.checkedTreeView.Sorted; }
			set { this.checkedTreeView.Sorted = value; }
		}

		/// <summary>
		/// Checks the matching items.
		/// </summary>
		/// <param name="match">The match.</param>
		/// <param name="keepExisting">if set to <c>true</c> keep existing checked items.</param>
		public void CheckItems(Predicate<string> match, bool keepExisting = false)
		{
			ForEachChildNode(this.checkedTreeView.Nodes, n => { if (match == null || match(n.Name)) n.Checked = true; else if (!keepExisting) n.Checked = false; });
			UpdateText();
		}

		/// <summary>
		/// Checks the item in the tree whose value matches the one specified by <paramref name="value"/>.
		/// </summary>
		/// <param name="value">The value to match.</param>
		/// <param name="check">if set to <c>true</c> check the matching item, else uncheck it.</param>
		public void CheckValue(string value, bool check = true)
		{
			TreeNode node;
			if (value != null && value is string)
				if (values.TryGetValue((string)value, out node))
					node.Checked = check;
			UpdateText();
		}

		/// <summary>
		/// Unchecks all items in the tree.
		/// </summary>
		public void UncheckAllItems()
		{
			updatingChecks = true;
			for (int i = checkedItems.Count - 1; i >= 0; i--)
				checkedItems[i].Checked = false;
			checkedItems.Clear();
			updatingChecks = false;
			UpdateText();
		}

		/// <summary>
		/// Updates the text associated with each item of the check list.
		/// </summary>
		public void UpdateText()
		{
			if (this.checkedItems.Count == 0)
			{
				this.Text = string.Empty;
			}
			else
			{
				if (this.checkedItems.Count == this.checkedTreeView.GetNodeCount(true) && this.CheckAllText != null)
					this.Text = this.CheckAllText;
				else
				{
					var selNodes = this.checkedItems.FindAll(n => !string.IsNullOrEmpty(n.Name)).ConvertAll<string>(t => (string)t.Name);
					selNodes.Sort();
					this.Text = string.Join(", ", selNodes.ToArray());
				}
			}
		}

		internal static TreeNode AddValue(TreeNodeCollection nodeColl, string text, string key)
		{
			TreeNode child = nodeColl.Add(key, text);
			if (key != null)
			{
				if (child.TreeView != null && child.TreeView.Parent != null && child.TreeView.Tag is DropDownCheckTree)
					((DropDownCheckTree)child.TreeView.Tag).values.Add(key, child);
			}
			return child;
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
		protected virtual void OnSelectedItemsChanged()
		{
			EventHandler h = this.SelectedItemsChanged;
			if (h != null)
				h(this, EventArgs.Empty);
		}

		private bool AreChildrenChecked(TreeNode treeNode, NodeAction nodeAction = null)
		{
			bool foundCheck = false;
			foreach (TreeNode n in treeNode.Nodes)
			{
				if (n.Checked || AreChildrenChecked(n))
				{
					foundCheck = true;
					break;
				}
			}
			if (nodeAction != null)
				nodeAction(treeNode, foundCheck);
			return foundCheck;
		}

		private void checkedTreeView_ItemCheck(object sender, TreeViewEventArgs e)
		{
			// Update checked items list
			if (e.Node.Checked)
				this.checkedItems.Add(e.Node);
			else
				this.checkedItems.Remove(e.Node);

			// Update parent and children checks appropriately
			if (!updatingChecks)
			{
				updatingChecks = true;
				SetCheckOnChildren(e.Node);
				TreeNode n = e.Node;
				while (n.Parent != null)
				{
					n.Parent.Checked = e.Node.Checked ? true : AreChildrenChecked(n.Parent);
					n = n.Parent;
				}
				OnSelectedItemsChanged();
				updatingChecks = false;
			}
		}

		private void ForEachChildNode(TreeNodeCollection pNodes, Action<TreeNode> action, bool allChildren = true)
		{
			foreach (TreeNode node in pNodes)
			{
				action(node);
				if (allChildren)
					ForEachChildNode(node.Nodes, action, allChildren);
			}
		}

		private void SetCheckOnChildren(TreeNode treeNode)
		{
			bool pChecked = treeNode.Checked;
			ForEachChildNode(treeNode.Nodes, n => { n.Checked = pChecked; });
		}
	}
}