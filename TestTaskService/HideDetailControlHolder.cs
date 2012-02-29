using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestTaskService
{
	internal class HideDetailControlHolder : UserControl
	{
		// Fields
		private bool adjustHeightOnHeightChange;
		private IContainer components;
		private object layoutLock = new object();
		private int oldClientHeight = -2147483648;
		private int oldClientWidth = -2147483648;
		private int xPadding = 10;
		private int yPadding = 10;

		// Methods
		internal HideDetailControlHolder()
		{
			this.InitializeComponent();
		}

		internal void AdjustLayoutAfterExpandOrCollapse()
		{
			lock (this.layoutLock)
			{
				if ((base.Controls != null) && (base.Controls.Count > 0))
				{
					base.SuspendLayout();
					int height = base.Height;
					int num2 = 0;
					int num3 = 0;
					foreach (HideDetailControl control in base.Controls)
					{
						if (control.IsDetailsHidden)
						{
							num2 += HideDetailControl.DefaultHeaderPanelHeight;
						}
						else
						{
							if (control.IsFixedHeight)
							{
								num2 += control.FixedHeight;
								continue;
							}
							num3++;
						}
					}
					int num4 = (base.Controls.Count + 1) * this.yPadding;
					int num5 = 0;
					if (num3 > 0)
					{
						num5 = ((height - num2) - num4) / num3;
					}
					int xPadding = this.xPadding;
					int yPadding = this.yPadding;
					bool flag = true;
					foreach (HideDetailControl control2 in base.Controls)
					{
						if (flag)
						{
							xPadding = control2.Location.X;
							yPadding = control2.Location.Y;
							flag = false;
						}
						else
						{
							control2.Location = new Point(xPadding, yPadding);
						}
						if (control2.IsDetailsHidden)
						{
							control2.Height = HideDetailControl.DefaultHeaderPanelHeight;
							control2.SetHeaderPanelLocation(0, 0);
						}
						else if (control2.IsFixedHeight)
						{
							control2.Height = control2.FixedHeight;
							control2.SetDetailPanelHeight(control2.Height - control2.HeaderPanelHeight);
							control2.SetHeaderPanelLocation(0, 0);
							control2.SetDetailPanelLocation(0, control2.HeaderPanelHeight);
						}
						else
						{
							control2.Height = Math.Max(num5, control2.MinHeight);
							control2.SetDetailPanelHeight(control2.Height - control2.HeaderPanelHeight);
							control2.SetHeaderPanelLocation(0, 0);
							control2.SetDetailPanelLocation(0, control2.HeaderPanelHeight);
						}
						yPadding += control2.Height + this.yPadding;
					}
					base.ResumeLayout(false);
					base.PerformLayout();
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void HideDetailControlHolder_Layout(object sender, LayoutEventArgs e)
		{
			if (this.oldClientWidth != base.ClientSize.Width)
			{
				this.oldClientWidth = base.ClientSize.Width;
				for (int i = 0; i < base.Controls.Count; i++)
				{
					int width;
					if (i == 0)
					{
						width = Math.Max(0, this.oldClientWidth - (2 * base.Controls[i].Location.X));
					}
					else
					{
						width = base.Controls[i - 1].Width;
					}
					if (base.Controls[i].Width != width)
					{
						base.Controls[i].Width = width;
					}
				}
			}
			if (this.adjustHeightOnHeightChange && (this.oldClientHeight != base.ClientSize.Height))
			{
				this.oldClientHeight = base.ClientSize.Height;
				this.AdjustLayoutAfterExpandOrCollapse();
			}
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(7f, 15f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoScroll = true;
			base.AutoScrollMargin = new Size(0, 10);
			this.BackColor = SystemColors.Window;
			base.Name = "HideDetailControlHolder";
			base.Size = new Size(590, 0x19f);
			base.Layout += new LayoutEventHandler(this.HideDetailControlHolder_Layout);
			base.ResumeLayout(false);
		}

		// Properties
		internal bool AdjustHeightOnHeightChange
		{
			get
			{
				return this.adjustHeightOnHeightChange;
			}
			set
			{
				this.adjustHeightOnHeightChange = value;
			}
		}

		internal int XPadding
		{
			get
			{
				return this.xPadding;
			}
			set
			{
				this.xPadding = value;
			}
		}

		internal int YPadding
		{
			get
			{
				return this.yPadding;
			}
			set
			{
				this.yPadding = value;
			}
		}
	}
}
