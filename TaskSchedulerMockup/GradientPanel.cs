using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TaskSchedulerMockup
{
	internal class GradientPanel : Panel
	{
		private static readonly Color defBgClr2 = SystemColors.ControlDark;

		public GradientPanel()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ContainerControl | ControlStyles.ResizeRedraw, true);
		}

		[Category("Appearance")]
		public Color BackColor2 { get; set; } = defBgClr2;

		[DefaultValue(typeof(LinearGradientMode), "Vertical"), Category("Appearance")]
		public LinearGradientMode GradientMode { get; set; } = LinearGradientMode.Vertical;

		private void ResetBackColor2() { BackColor2 = defBgClr2; }

		private bool ShouldSerializeBackColor2() => BackColor2 != defBgClr2;

		protected override void OnPaint(PaintEventArgs e)
		{
			using (var brush = new LinearGradientBrush(base.Bounds, BackColor, BackColor2, GradientMode))
				e.Graphics.FillRectangle(brush, base.Bounds);
			var r = new Rectangle(base.Bounds.X, base.Bounds.Y, base.Width - 1, base.Height - 1);
			if (BorderStyle == BorderStyle.FixedSingle)
				e.Graphics.DrawRectangle(SystemPens.WindowFrame, r);
			else if (BorderStyle == BorderStyle.Fixed3D)
				ControlPaint.DrawBorder3D(e.Graphics, r);
		}
	}
}
