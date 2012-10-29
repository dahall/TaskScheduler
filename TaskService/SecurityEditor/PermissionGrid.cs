using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SecurityEditor
{
	public partial class PermissionGrid : UserControl
	{
		internal const int maxCols = 3;
		private int minColWidth = 13;
		private bool dirty = false;
		private PermissionItem[] permissions;

		public PermissionGrid()
		{
			permissions = new PermissionItem[0];
			InitializeComponent();
			//gridPanel.AutoScroll = true;
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PermissionItem[] Permissions
		{
			get { return permissions; }
			set
			{
				permissions = value;
				dirty = true;
				PerformLayout();
			}
		}

		public void ClearAll()
		{
			foreach (var perm in permissions)
			{
				for (int i = 0; i < maxCols; i++)
				{
					perm.colChecks[i].Checked = false;
				}
			}
		}

		public void SetColumns(params string[] columns)
		{
			using (Graphics g = Graphics.FromHwnd(this.Handle))
				minColWidth = CheckBoxRenderer.GetGlyphSize(g, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal).Width;

			for (int i = 0; i < maxCols; i++)
			{
				if (i < columns.Length)
				{
					labelPanel.Controls["col" + (i + 1).ToString() + "Label"].Text = columns[i];
					labelPanel.ColumnStyles[i + 1].Width = minColWidth * 4;
					gridPanel.ColumnStyles[i + 1].Width = minColWidth * 4;
				}
				else
				{
					labelPanel.ColumnStyles[i + 1].Width = 0;
					gridPanel.ColumnStyles[i + 1].Width = 0;
				}
			}

			dirty = true;
			PerformLayout();
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);

			if (dirty)
			{
				gridPanel.SuspendLayout();
				for (int i = gridPanel.Controls.Count - 1; i >= 0; i--)
				{
					if (gridPanel.Controls[i] is CheckBox)
						((CheckBox)gridPanel.Controls[i]).CheckedChanged -= PermissionGrid_CheckedChanged;
					gridPanel.Controls.RemoveAt(i);
				}
				if (permissions != null && permissions.Length > 0)
				{
					gridPanel.RowCount = permissions.Length;
					for (int i = 0; i < permissions.Length; i++)
					{
						gridPanel.Controls.Add(permissions[i].label, 0, i);
						for (int j = 0; j < maxCols; j++)
						{
							Padding margin = permissions[i].colChecks[j].Margin;
							margin.Left = (int)Math.Ceiling(minColWidth * 1.5);
							permissions[i].colChecks[j].Margin = margin;
							gridPanel.Controls.Add(permissions[i].colChecks[j], j + 1, i);
							permissions[i].colChecks[j].CheckedChanged += PermissionGrid_CheckedChanged;
						}
					}
				}
				gridPanel.ResumeLayout();
				dirty = false;
			}
		}

		private void PermissionGrid_CheckedChanged(object sender, EventArgs e)
		{
			var p = gridPanel.GetCellPosition(sender as Control);
			if (permissions != null && p.Row < permissions.Length)
				permissions[p.Row].ColumnChecked[p.Column - 1] = ((CheckBox)sender).Checked;
		}

		private void panel1_SizeChanged(object sender, EventArgs e)
		{
			// Resize gridPanel
			gridPanel.AutoScroll = true; // (panel1.Height < gridPanel.PreferredSize.Height);
			//gridPanel.Bounds = panel1.ClientRectangle;
			gridPanel.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
			//gridPanel.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100f);

			// If scrolling, adjust labelPanel
			Padding lPad = labelPanel.Padding;
			lPad.Right = labelPanel.Width - gridPanel.ClientRectangle.Width;
			labelPanel.Padding = lPad;
		}
	}

	public class PermissionItem
	{
		public string Text;
		public bool[] ColumnChecked;
		public bool[] ColumnEnabled;
		public int Permission;
		internal Label label;
		internal CheckBox[] colChecks;

		public PermissionItem(string text, int permission, bool col1Checked = false, bool col1Enabled = true, bool col2Checked = false, bool col2Enabled = true, bool col3Checked = false, bool col3Enabled = true)
		{
			Text = text;
			ColumnChecked = new bool[] { col1Checked, col2Checked, col3Checked };
			ColumnEnabled = new bool[] { col1Enabled, col2Enabled, col3Enabled };
			colChecks = new CheckBox[PermissionGrid.maxCols];
			
			int tabIndex = 0;
			label = new Label() { AutoSize = true, Text = text, TabIndex = tabIndex++ };
			label.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			for (int i = 0; i < PermissionGrid.maxCols; i++)
			{
				colChecks[i] = new CheckBox() { AutoSize = true, Checked = ColumnChecked[i], Enabled = ColumnEnabled[i],
					TabIndex = tabIndex++, TextAlign = System.Drawing.ContentAlignment.MiddleCenter, UseVisualStyleBackColor = true };
				colChecks[i].Margin = new System.Windows.Forms.Padding(13, 3, 3, 1);
			}
		}
	}
}
