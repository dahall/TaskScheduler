using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestTaskService
{
	[DefaultProperty("Text"), DefaultEvent("CheckChanged")]
	internal partial class PanelHeader : Control
	{
		private const int lPad = 4, tbPad = 2;
		private const string panelHeaderArrowsImageResName = "PanelHeaderArrows";

		private static readonly Color topBorderColor = Color.FromArgb(238, 238, 238);
		private static readonly Color topBorderColorHot = Color.FromArgb(0, 122, 204);

		private bool hoverImage = false, check = false, selected = false, hot = false;
		private ImageList imageList;

		public PanelHeader()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.StandardClick, true);
			SetStyle(ControlStyles.StandardDoubleClick, false);
			imageList = new ImageList { ImageSize = new Size(7, 11), ColorDepth = ColorDepth.Depth32Bit };
			imageList.Images.AddStrip(Properties.Resources.PanelHeaderArrows);
		}

		public event EventHandler CheckChanged;

		protected enum State
		{
			Normal = 0,
			Hot = 1,
			Checked = 2,
			HotChecked = 3,
			Selected = 4,
			HotSelected = 5,
			CheckedSelected = 6,
			HotCheckedSelected = 7,
			Disabled = 8,
			DisabledChecked = 9,
		}

		[DefaultValue(false)]
		public bool Checked
		{
			get { return check; }
			set
			{
				if (check != value)
				{
					check = value;
					InvalidateImage();
					CheckChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void InvalidateImage()
		{
			Invalidate(new Rectangle(0, 0, imageList.ImageSize.Width + (lPad * 2), Height));
		}

		[DefaultValue(null)]
		public ImageList CustomImageList { get; set; }

		[DefaultValue(false)]
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

		private ImageList ImageList => CustomImageList ?? (imageList.Images.Count > 0 ? imageList : null);

		protected bool MouseOverImage => hoverImage;

		protected State CurrentState => (State)(Enabled ? (hot && hoverImage ? 0x1 : 0) | (check ? 0x2 : 0) | (selected ? 0x4 : 0) : (check ? 9 : 8));

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (CanSelect && !hoverImage)
				Selected = !Selected;
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			hot = true;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			hot = hoverImage = false;
			Invalidate();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			bool overImg = imageList != null && e.X <= (imageList.ImageSize.Width + (lPad * 2));
			if (overImg != hoverImage)
			{
				hoverImage = overImg;
				InvalidateImage();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (hoverImage || !CanSelect)
				Checked = !Checked;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			// Paint background
			pe.Graphics.Clear(BackColor);
			using (Pen p = new Pen(hot ? topBorderColorHot : topBorderColor))
				pe.Graphics.DrawLine(p, 0, 0, Width, 0);

			// Get bold font
			Font bf = new Font(Font, FontStyle.Bold);
			// Get text height
			Size tsz = TextRenderer.MeasureText(Text, bf);
			// Center image on near side
			Size isz = ImageList?.ImageSize ?? Size.Empty;
			// Paint image
			int x = lPad;
			if (isz.Width > 0)
			{
				// Get state image
				int iidx = (int)CurrentState;
				var il = ImageList;
				if (il != null && il.Images.Count > iidx)
				{
					// Get image draw rectangle
					var ir = new Rectangle(x, (Height - isz.Height) / 2, isz.Width, isz.Height);
					il.Draw(pe.Graphics, ir.Location, iidx);
					x += (lPad + isz.Width);
				}
			}
			// Draw truncated text
			TextRenderer.DrawText(pe.Graphics, Text, bf, new Rectangle(x, 0, Width - x, Height), ForeColor, BackColor, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
			// Draw focus border
			if (Focused)
				ControlPaint.DrawFocusRectangle(pe.Graphics, Bounds);
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}
	}
}