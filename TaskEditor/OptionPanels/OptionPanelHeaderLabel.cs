using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal partial class OptionPanelHeaderLabel : Control
	{
		static StringFormat sf = new StringFormat() { LineAlignment = StringAlignment.Center };
		private const int indent = 5;

		public OptionPanelHeaderLabel()
		{
			InitializeComponent();
			Dock = DockStyle.Top;
			Font = new Font(Font, FontStyle.Bold);
			Margin = Padding.Empty;
		}

		protected override void OnParentFontChanged(EventArgs e)
		{
			base.OnParentFontChanged(e);
			Font = new Font(Parent.Font, FontStyle.Bold);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			pe.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
			pe.Graphics.DrawLine(SystemPens.ControlDark, ClientRectangle.X, ClientRectangle.Bottom - 1, ClientRectangle.Right, ClientRectangle.Bottom - 1);
			pe.Graphics.DrawString(Text, Font, SystemBrushes.ControlDarkDark, new Rectangle(ClientRectangle.X + indent, ClientRectangle.Y, ClientRectangle.Width - indent, ClientRectangle.Height), sf);
		}
	}
}
