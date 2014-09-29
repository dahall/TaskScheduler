using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	public partial class OptionPanelHeaderLabel : Control
	{
		static StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };

		public OptionPanelHeaderLabel()
		{
			InitializeComponent();
			this.Dock = DockStyle.Top;
			this.Font = new Font(this.Font, FontStyle.Bold);
			this.Margin = Padding.Empty;
		}

		protected override void OnParentFontChanged(EventArgs e)
		{
			base.OnParentFontChanged(e);
			this.Font = new Font(this.Parent.Font, FontStyle.Bold);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			pe.Graphics.FillRectangle(SystemBrushes.Control, this.Bounds);
			pe.Graphics.DrawLine(SystemPens.ControlDark, this.Bounds.X, this.Bounds.Bottom - 1, this.Bounds.Right, this.Bounds.Bottom - 1);
			pe.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlDark, Rectangle.Inflate(this.Bounds, -4, 0), sf);
		}
	}
}
