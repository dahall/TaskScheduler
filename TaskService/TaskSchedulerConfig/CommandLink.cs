using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TaskSchedulerConfig
{
	internal class CommandLink : Button
	{
		private const int BCM_SETNOTE = 0x00001609;
		private const int BCM_SETSHIELD = 0x0000160C;
		private const int BS_COMMANDLINK = 0x0000000E;

		private Color activeTextColor;
		private string description;
		private Label lblDescription;
		private Label lblText;
		private bool mActivated;
		private DisplayStyle mDisplayStyle = DisplayStyle.Arrow;
		private bool mMouseOver;
		private PictureBox picIcon;
		private string text;
		private Color textColor;

		public CommandLink()
		{
			if (IsVistaOrLater)
			{
				FlatStyle = FlatStyle.System;
			}
			else
			{
				BackColor = Parent?.BackColor ?? Color.White;
				FlatStyle = FlatStyle.Standard;
				Size = new Size(400, 72);
				TabStop = false;
				UseVisualStyleBackColor = false;

				textColor = Color.FromArgb(21, 28, 85);
				activeTextColor = Color.FromArgb(7, 74, 229);

				lblText = new Label { AutoSize = true, BackColor = Color.Transparent, ForeColor = textColor, Location = new Point(27, 10), Name = "lblText", Size = new Size(0, 21), TabIndex = 0 };
				lblText.Font = new Font("Segoe UI", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
				lblText.ForeColor = Color.FromArgb((int)(byte)21, (int)(byte)28, (int)(byte)85);
				lblText.Click += (o, e) => OnClick(e);
				lblText.MouseLeave += CommandLink_MouseLeave;
				Controls.Add(lblText);

				lblDescription = new Label { BackColor = Color.Transparent, ForeColor = textColor, Location = new Point(33, 36), Name = "lblDescription", Size = new Size(364, 32), TabIndex = 1, UseCompatibleTextRendering = true };
				lblDescription.Font = new Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
				lblDescription.ForeColor = Color.FromArgb((int)(byte)21, (int)(byte)28, (int)(byte)85);
				lblDescription.Click += (o, e) => OnClick(e);
				lblDescription.MouseLeave += CommandLink_MouseLeave;
				Controls.Add(lblDescription);

				picIcon = new PictureBox { BackColor = Color.Transparent, Location = new Point(10, 13), Name = "picIcon", Size = new Size(16, 16), TabIndex = 2, TabStop = false };
				picIcon.Image = Properties.Resources.restarrow;
				picIcon.Click += (o, e) => OnClick(e);
				picIcon.MouseLeave += CommandLink_MouseLeave;
				Controls.Add(picIcon);

				Click += (o, e) => Selected?.Invoke(this, EventArgs.Empty);
				MouseEnter += (o, e) => MouseOver = true;
				MouseLeave += CommandLink_MouseLeave;

				MouseOver = false;
				Activated = false;
			}
		}

		public CommandLink(DisplayStyle style, string text = null, string description = null) : this()
		{
			Style = style;
			Text = text;
			Description = description;
		}

		public event EventHandler Selected;

		public enum DisplayStyle
		{
			Arrow,
			Shield,
		}

		[Bindable(true)]
		[DefaultValue(null)]
		public string Description
		{
			get { return description; }
			set
			{
				if (IsVistaOrLater)
					SendMessage(Handle, BCM_SETNOTE, IntPtr.Zero, value);
				else
					lblDescription.Text = value;
				description = value;
			}
		}

		[Bindable(true)]
		[DefaultValue(DisplayStyle.Arrow)]
		public DisplayStyle Style
		{
			get { return mDisplayStyle; }
			set
			{
				bool bInv = false;
				if (mDisplayStyle != value)
				{
					bInv = true;
				}
				mDisplayStyle = value;
				if (bInv)
				{
					if (IsVistaOrLater)
					{
						if (mDisplayStyle == DisplayStyle.Shield)
							SendMessage(Handle, BCM_SETSHIELD, 0, 1);
					}
					else
					{
						base.FlatStyle = FlatStyle.Standard;

						switch (mDisplayStyle)
						{
							case DisplayStyle.Arrow:
								picIcon.Image = mMouseOver ? Properties.Resources.selarrow : Properties.Resources.restarrow;
								break;

							case DisplayStyle.Shield:
								picIcon.Image = Properties.Resources.shield;
								break;

							default:
								picIcon.Image = null;
								break;
						}
						Invalidate();
					}
				}
			}
		}

		[Bindable(true)]
		[DefaultValue(null)]
		public override string Text
		{
			get { return text; }
			set
			{
				if (IsVistaOrLater)
					base.Text = value;
				else
					lblText.Text = value;
				text = value;
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				if (IsVistaOrLater)
					cp.Style |= BS_COMMANDLINK;
				return cp;
			}
		}

		private bool Activated
		{
			get { return mActivated; }
			set
			{
				bool bInv = false;
				if (mActivated != value)
					bInv = true;
				mActivated = value;
				if (bInv)
					Invalidate();
			}
		}

		private bool Default => (object.ReferenceEquals(FindForm().AcceptButton, this));

		private static bool IsVistaOrLater => System.Environment.OSVersion.Version.Major > 5;

		private bool MouseOver
		{
			get { return mMouseOver; }
			set
			{
				bool bInv = false;
				if (mMouseOver != value)
					bInv = true;
				mMouseOver = value;
				if (!IsVistaOrLater && bInv)
				{
					if (mMouseOver == true)
					{
						lblText.ForeColor = activeTextColor;
						lblDescription.ForeColor = activeTextColor;
						if (Style == DisplayStyle.Arrow)
							picIcon.Image = Properties.Resources.selarrow;
					}
					else
					{
						lblText.ForeColor = textColor;
						lblDescription.ForeColor = textColor;
						if (Style == DisplayStyle.Arrow)
						{
							picIcon.Image = Properties.Resources.restarrow;
						}
					}
					Invalidate();
				}
			}
		}

		public void ActivateChanged(bool activate)
		{
			Activated = activate;
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			if (IsVistaOrLater)
			{
				base.OnPaint(e);
			}
			else
			{
				if (base.FlatStyle != FlatStyle.Standard)
					base.FlatStyle = FlatStyle.Standard;

				Pen oPen = null;
				Rectangle r = ClientRectangle;
				Brush oBrush = null;

				r.Width -= 1;
				r.Height -= 1;

				if (MouseOver)
				{
					//the mouse is over is, draw the hover border
					oPen = new Pen(Color.FromArgb(198, 198, 198));
					oBrush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, BackColor, Color.FromArgb(246, 246, 246), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
				}
				else if (Activated && Default)
				{
					//draw the blue border
					oPen = new Pen(Color.FromArgb(198, 244, 255));
					r.Width -= 2;
					r.Height -= 2;
					r.X += 1;
					r.Y += 1;
				}

				if (oBrush == null)
				{
					e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
				}
				else
				{
					e.Graphics.FillRectangle(oBrush, ClientRectangle);
					oBrush.Dispose();
				}
				if (oPen != null)
				{
					oPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
					e.Graphics.DrawLine(oPen, r.X + 2, r.Y, r.X + r.Width - 2, r.Y);
					e.Graphics.DrawLine(oPen, r.X + r.Width - 2, r.Y, r.X + r.Width, r.Y + 2);
					e.Graphics.DrawLine(oPen, r.X + r.Width, r.Y + 2, r.X + r.Width, r.Y + r.Height - 2);
					e.Graphics.DrawLine(oPen, r.X + r.Width, r.Y + r.Height - 2, r.X + r.Width - 2, r.Y + r.Height);
					e.Graphics.DrawLine(oPen, r.X + r.Width - 2, r.Y + r.Height, r.X + 2, r.Y + r.Height);
					e.Graphics.DrawLine(oPen, r.X + 2, r.Y + r.Height, r.X, r.Y + r.Height - 2);
					e.Graphics.DrawLine(oPen, r.X, r.Y + r.Height - 2, r.X, r.Y + 2);
					e.Graphics.DrawLine(oPen, r.X, r.Y + 2, r.X + 2, r.Y);
					oPen.Dispose();
				}
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

		[DllImport("user32", CharSet = CharSet.Unicode)]
		private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

		private void CommandLink_MouseLeave(object sender, System.EventArgs e)
		{
			MouseOver = ClientRectangle.Contains(PointToClient(Control.MousePosition));
			Invalidate();
		}
	}
}