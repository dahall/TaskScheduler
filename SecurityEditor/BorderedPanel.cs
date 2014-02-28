using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
	internal class BorderedPanel : Panel
	{
		private static readonly Color defaultBorderColor = SystemColors.ControlDark;
		private Color borderColor = defaultBorderColor;

		public BorderedPanel()
		{
			this.SetStyle(ControlStyles.UserPaint, true);
			base.BorderStyle = BorderStyle.None;
		}

		[Category("Appearance")]
		public Color BorderColor
		{
			get { return borderColor; }
			set { borderColor = value; Invalidate(); }
		}

		[DefaultValue(BorderStyle.None), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private new BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
			set { base.BorderStyle = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			using (var bb = new SolidBrush(base.BackColor))
				e.Graphics.FillRectangle(bb, base.ClientRectangle);
			using (var bp = new Pen(borderColor))
				e.Graphics.DrawRectangle(bp, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
		}

		internal void ResetBorderColor()
		{
			borderColor = defaultBorderColor;
		}

		internal bool ShouldSerializeBorderColor()
		{
			return borderColor != defaultBorderColor;
		}
	}
}
