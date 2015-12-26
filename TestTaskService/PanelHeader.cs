using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestTaskService
{
	internal partial class PanelHeader : Control
	{
		private enum State
		{
			Disabled = 0,
			Normal = 1,
			Hot = 2,
			Checked = 3,
			HotChecked = 4,
			Selected = 5,
			HotSelected = 6,
			CheckedSelected = 7,
			HotCheckedSelected = 8,
		}

		bool hoverImage = false, check = false, selected = false, hot = false;

		public PanelHeader()
		{
		}

		public event EventHandler CheckChanged;
		private const int lPad = 4, tbPad = 2;

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			// Get text height
			Size tsz = TextRenderer.MeasureText(Text, Font);
			// Center image on near side
			Size isz = imageList?.ImageSize ?? Size.Empty;
			int fullHeight = Math.Max(tsz.Height, isz.Height) + (tbPad * 2);
			// Paint image
			int x = lPad;
			if (isz.Width > 0)
			{
				// Get state image
				int iidx = 0; // TODO
				// Get image draw rect
				var ir = new Rectangle(x, (fullHeight - isz.Height) / 2, isz.Width, isz.Height);
				imageList.Draw(pe.Graphics, ir.Location, iidx);
				x += (lPad + isz.Width);
			}
			// Draw truncated text
			TextRenderer.DrawText(pe.Graphics, Text, Font, new Rectangle(x, tbPad, Width - x, fullHeight), ForeColor, BackColor, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
		}

		public bool Checked
		{
			get { return check; }
			set
			{
				if (check != value)
				{
					check = value;
					CheckChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public bool Selected
		{
			get { return selected; }
			set
			{
				if (selected != value)
				{
					selected = value;
				}
			}
		}

		private ImageList imageList;

		public ImageList ImageList
		{
			get { return imageList; }
			set { imageList = value; }
		}

		protected bool MouseOverImage => hoverImage;

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (CanSelect && !hoverImage)
				Selected = !Selected;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (hoverImage || !CanSelect)
				Checked = !Checked;
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			hot = true;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			hoverImage = imageList != null && e.X <= imageList.ImageSize.Width;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			hot = hoverImage = false;
		}
	}
}
