using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestTaskService
{
	internal class HideDetailControl : UserControl
	{
		// Fields
		private Button arrowButton;
		private Pen arrowButtonFocusPen = new Pen(SystemColors.ControlText);
		private ImageList arrowImageList = new ImageList();
		private IContainer components;
		internal static int DefaultHeaderPanelHeight = 0x18;
		private Panel detailPanel;
		private const int downArrowImageIdx = 0;
		private int fixedHeight;
		private GradientPanel headerPanel;
		private bool isDetailsHidden;
		private bool isFixedHeight;
		private const int LogPixelsY = 90;
		private Control mainDetailControl;
		private int minHeight;
		private Label titleLabel;
		private const int upArrowImageIdx = 1;

		// Methods
		static HideDetailControl()
		{
			/*int windowDC = NativeMethods.GetWindowDC(NativeMethods.GetDesktopWindow());
			int deviceCaps = NativeMethods.GetDeviceCaps(windowDC, 90);
			if ((NativeMethods.ReleaseDC(NativeMethods.GetDesktopWindow(), windowDC) == 1) && (deviceCaps != 0x60))
			{
				DefaultHeaderPanelHeight = (DefaultHeaderPanelHeight * deviceCaps) / 0x60;
			}*/
		}

		internal HideDetailControl()
		{
			this.InitializeComponent();
			this.isFixedHeight = true;
			this.fixedHeight = 200;
			base.Height = this.fixedHeight;
			this.titleLabel.AutoSize = true;
			this.titleLabel.Tag = "";
			this.titleLabel.Text = string.Empty;
			this.arrowImageList.Images.Add(Properties.Resources.MarlettFontDownArrow);
			this.arrowImageList.Images.Add(Properties.Resources.MarlettFontUpArrow);
			this.arrowImageList.TransparentColor = Color.FromArgb(0xff, 0, 0xff);
			this.SetToUpArrow();
			this.arrowButton.Location = new Point(this.headerPanel.ClientSize.Width - this.arrowButton.Width, Math.Max(0, (this.headerPanel.Height - this.arrowButton.Height) / 2));
			this.headerPanel.Height = DefaultHeaderPanelHeight;
			this.headerPanel.BorderStyle = BorderStyle.FixedSingle;
			this.headerPanel.Width = base.ClientSize.Width;
			this.headerPanel.Cursor = Cursors.Hand;
			this.headerPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left;
			this.detailPanel.Location = new Point(0, this.headerPanel.Height);
			this.detailPanel.Width = base.ClientSize.Width;
			this.detailPanel.Height = base.Height - this.headerPanel.Height;
			this.detailPanel.Anchor = AnchorStyles.Right | AnchorStyles.Left;
		}

		internal void AddControlToDetailPanel(Control control)
		{
			this.AddControlToDetailPanel(control, false);
		}

		internal void AddControlToDetailPanel(Control control, bool isMainControl)
		{
			this.detailPanel.Controls.Add(control);
			if (isMainControl)
			{
				this.mainDetailControl = control;
			}
		}

		internal void AddDetailPanelVisibleChangedHandler(EventHandler handler)
		{
			this.detailPanel.VisibleChanged += handler;
		}

		private void AdjustTextAndLocationOfHeaderPanelLabels()
		{
			this.headerPanel.SuspendLayout();
			this.titleLabel.MaximumSize = new Size((this.headerPanel.Width - this.arrowButton.Width) - 6, this.titleLabel.MaximumSize.Height);
			this.titleLabel.Text = (string)this.titleLabel.Tag;
			this.titleLabel.Location = new Point(this.titleLabel.Location.X, Math.Max(0, (this.headerPanel.Height - this.titleLabel.Height) / 2));
			this.headerPanel.ResumeLayout(false);
		}

		private void arrowButton_Paint(object sender, PaintEventArgs e)
		{
			if (this.arrowButton.ContainsFocus)
			{
				e.Graphics.DrawRectangle(this.arrowButtonFocusPen, new Rectangle(3, 3, this.arrowButton.Width - 6, this.arrowButton.Height - 6));
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			if (disposing && (this.arrowButtonFocusPen != null))
			{
				this.arrowButtonFocusPen.Dispose();
			}
			base.Dispose(disposing);
		}

		private void headerPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.isDetailsHidden = !this.isDetailsHidden;
				this.detailPanel.Height = 0;
				this.detailPanel.Visible = !this.isDetailsHidden;
				if (this.isDetailsHidden)
				{
					this.SetToDownArrow();
				}
				else
				{
					this.SetToUpArrow();
				}
				if (base.Parent != null)
				{
					((HideDetailControlHolder)base.Parent).AdjustLayoutAfterExpandOrCollapse();
				}
				this.arrowButton.Focus();
			}
		}

		private void headerPanel_SizeChanged(object sender, EventArgs e)
		{
			this.AdjustTextAndLocationOfHeaderPanelLabels();
		}

		private void HideDetailControl_Load(object sender, EventArgs e)
		{
			this.SetRightToLeft();
		}

		private void HideDetailControl_RightToLeftChanged(object sender, EventArgs e)
		{
			this.SetRightToLeft();
		}

		private void InitializeComponent()
		{
			this.detailPanel = new Panel();
			this.headerPanel = new GradientPanel();
			this.arrowButton = new Button();
			this.titleLabel = new Label();
			this.headerPanel.SuspendLayout();
			base.SuspendLayout();
			this.detailPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.detailPanel.AutoScroll = true;
			this.detailPanel.Location = new Point(0, 23);
			this.detailPanel.Size = new Size(886, 269);
			this.detailPanel.TabIndex = 1;
			this.detailPanel.Name = "detailPanel";
			this.detailPanel.BackColor = SystemColors.Control;
			this.headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.headerPanel.Location = new Point(0, 0);
			this.headerPanel.Size = new Size(886, 28);
			this.headerPanel.TabIndex = 0;
			this.headerPanel.BackColor = SystemColors.Control;
			this.headerPanel.Controls.Add(this.arrowButton);
			this.headerPanel.Controls.Add(this.titleLabel);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.MouseClick += new MouseEventHandler(this.headerPanel_MouseClick);
			this.headerPanel.SizeChanged += new EventHandler(this.headerPanel_SizeChanged);
			this.arrowButton.BackColor = Color.Transparent;
			this.arrowButton.Dock = DockStyle.Right;
			this.arrowButton.FlatStyle = FlatStyle.Flat;
			this.arrowButton.Location = new Point(867, 0);
			this.arrowButton.Margin = new Padding(0);
			this.arrowButton.Size = new Size(19, 28);
			this.arrowButton.TabIndex = 3;
			this.arrowButton.FlatAppearance.BorderColor = SystemColors.ControlText;
			this.arrowButton.FlatAppearance.BorderSize = 0;
			this.arrowButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
			this.arrowButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
			this.arrowButton.Name = "arrowButton";
			this.arrowButton.UseVisualStyleBackColor = false;
			this.arrowButton.MouseClick += new MouseEventHandler(this.headerPanel_MouseClick);
			this.arrowButton.Paint += new PaintEventHandler(this.arrowButton_Paint);
			this.titleLabel.Anchor = AnchorStyles.Left;
			this.titleLabel.AutoSize = true;
			this.titleLabel.Dock = DockStyle.Left;
			this.titleLabel.Location = new Point(0, 0);
			this.titleLabel.Padding = new Padding(3, 0, 0, 0);
			this.titleLabel.Size = new Size(50, 20);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.TextAlign = ContentAlignment.TopLeft;
			this.titleLabel.BackColor = Color.Transparent;
			this.titleLabel.MinimumSize = new Size(50, 20);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.MouseClick += new MouseEventHandler(this.headerPanel_MouseClick);
			this.AutoScaleDimensions = new SizeF(7, 15);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Transparent;
			this.Size = new Size(886, 292);
			base.Controls.Add(this.detailPanel);
			base.Controls.Add(this.headerPanel);
			base.Name = "HideDetailControl";
			base.Load += new EventHandler(this.HideDetailControl_Load);
			base.RightToLeftChanged += new EventHandler(this.HideDetailControl_RightToLeftChanged);
			this.headerPanel.ResumeLayout(false);
			this.headerPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		internal void SetDetailPanelHeight(int height)
		{
			this.detailPanel.Height = height;
			if (this.mainDetailControl != null)
			{
				this.mainDetailControl.Height = height - 20;
				this.mainDetailControl.Location = new Point(10, 10);
			}
		}

		internal void SetDetailPanelLocation(int x, int y)
		{
			this.detailPanel.Location = new Point(x, y);
		}

		internal void SetHeaderPanelLocation(int x, int y)
		{
			this.headerPanel.Location = new Point(x, y);
		}

		internal static RightToLeft GetRightToLeftProperty(Control ctl)
		{
			if (ctl.RightToLeft == RightToLeft.Inherit)
			{
				return GetRightToLeftProperty(ctl.Parent);
			}
			return ctl.RightToLeft;
		}

		internal static void SetRightToLeftPropertyForChildControls(Control parentControl)
		{
			RightToLeft rightToLeft = parentControl.RightToLeft;
			Control parent = parentControl;
			while (rightToLeft == RightToLeft.Inherit)
			{
				parent = parent.Parent;
				if (parent != null)
				{
					rightToLeft = RightToLeft.No;
					return;
				}
				rightToLeft = parent.RightToLeft;
			}
		}
		
		private void SetRightToLeft()
		{
			RightToLeft rightToLeftProperty = GetRightToLeftProperty(this);
			this.arrowButton.Dock = (rightToLeftProperty == RightToLeft.Yes) ? DockStyle.Left : DockStyle.Right;
			this.titleLabel.Dock = (rightToLeftProperty == RightToLeft.Yes) ? DockStyle.Right : DockStyle.Left;
			SetRightToLeftPropertyForChildControls(this);
			SetRightToLeftPropertyForChildControls(this.detailPanel);
			SetRightToLeftPropertyForChildControls(this.headerPanel);
		}

		internal void SetTitleText(string newTitle)
		{
			this.titleLabel.Tag = newTitle;
			this.titleLabel.Text = newTitle;
			this.AdjustTextAndLocationOfHeaderPanelLabels();
		}

		private void SetToDownArrow()
		{
			this.arrowButton.Image = this.arrowImageList.Images[0];
		}

		private void SetToUpArrow()
		{
			this.arrowButton.Image = this.arrowImageList.Images[1];
		}

		// Properties
		internal int DetailPanelHeight
		{
			get
			{
				return this.detailPanel.Height;
			}
		}

		internal int DetailPanelWidth
		{
			get
			{
				return this.detailPanel.Width;
			}
		}

		internal int FixedHeight
		{
			get
			{
				return this.fixedHeight;
			}
			set
			{
				this.fixedHeight = value;
			}
		}

		internal int HeaderPanelHeight
		{
			get
			{
				return this.headerPanel.Height;
			}
		}

		internal bool IsDetailsHidden
		{
			get
			{
				return this.isDetailsHidden;
			}
		}

		internal bool IsFixedHeight
		{
			get
			{
				return this.isFixedHeight;
			}
			set
			{
				this.isFixedHeight = value;
			}
		}

		internal int MinHeight
		{
			get
			{
				return this.minHeight;
			}
			set
			{
				this.minHeight = value;
			}
		}
	}
}
