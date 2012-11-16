using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TestTaskService
{
	internal class GradientPanel : Panel
	{
		public GradientPanel()
		{
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		// Methods
		protected override void OnPaint(PaintEventArgs e)
		{
			Brush brush = new LinearGradientBrush(base.Bounds, SystemColors.Control, SystemColors.ControlDark, LinearGradientMode.Vertical);
			e.Graphics.FillRectangle(brush, base.Bounds);
			e.Graphics.DrawRectangle(SystemPens.WindowFrame, Rectangle.Inflate(base.Bounds, -1, -1));
		}
	}
}
