using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>
	/// Interface that exposes an <c>Enabled</c> property for an item supplied to <see cref="DisabledItemComboBox"/>.
	/// </summary>
	public interface IEnableable
	{
		/// <summary>Gets a value indicating whether an item is enabled.</summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		bool Enabled { get; }
	}

	/// <summary>
	/// A version of <see cref="ComboBox"/> that allows for disabled items.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog), "Control")]
	public class DisabledItemComboBox : ComboBox
	{
		private const TextFormatFlags tff = TextFormatFlags.Default | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;
		private bool animationsNeedCleanup;
		private bool BufferedPaintSupported = false;
		private ComboBoxState currentState = ComboBoxState.Normal, newState = ComboBoxState.Normal;
		private LBNativeWindow dropDownWindow;
		private AnimationTransition<ComboBoxState>[] transitions;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisabledItemComboBox"/> class.
		/// </summary>
		public DisabledItemComboBox()
		{
			this.SetStyle(/*ControlStyles.Opaque |*/ ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			if ((Environment.OSVersion.Version.Major >= 6) && VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles)
			{
				BufferedPaintSupported = true;
				var vsr = new VisualStyleRenderer("COMBOBOX", 5, 0);
				transitions = new AnimationTransition<ComboBoxState>[] {
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Hot),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Pressed),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Normal, ComboBoxState.Disabled),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Normal),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Pressed),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Hot, ComboBoxState.Disabled),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Pressed, ComboBoxState.Normal),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Pressed, ComboBoxState.Hot),
					new AnimationTransition<ComboBoxState>(vsr, ComboBoxState.Disabled, ComboBoxState.Normal)
				};
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether your code or the operating system will handle drawing of elements in the list.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration values. The default is <see cref="F:System.Windows.Forms.DrawMode.Normal" />.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		[DefaultValue(DrawMode.OwnerDrawFixed), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
			set { base.DrawMode = value; }
		}

		/// <summary>
		/// Gets or sets a value specifying the style of the combo box.
		/// </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ComboBoxStyle" /> values. The default is DropDown.</returns>
		///   <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   </PermissionSet>
		[DefaultValue(ComboBoxStyle.DropDownList), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
			set { base.DropDownStyle = value; }
		}

		/// <summary>
		/// Gets or sets the state of the combobox.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		private ComboBoxState State
		{
			get
			{
				return currentState;
			}
			set
			{
				bool diff = !Object.Equals(currentState, value);
				newState = value;
				if (diff)
				{
					if (animationsNeedCleanup && this.IsHandleCreated) NativeMethods.BufferedPaintStopAllAnimations(this.Handle);
					this.Invalidate();
				}
			}
		}

		/// <summary>
		/// Determines whether an item is enabled.
		/// </summary>
		/// <param name="idx">The index of the item.</param>
		/// <returns>
		///   <c>true</c> if enabled; otherwise, <c>false</c>.
		/// </returns>
		public bool IsItemEnabled(int idx)
		{
			return !(idx > -1 && idx < this.Items.Count && this.Items[idx] is IEnableable && !((IEnableable)this.Items[idx]).Enabled);
		}

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ComboBox" /> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (animationsNeedCleanup)
			{
				NativeMethods.BufferedPaintUnInit();
				animationsNeedCleanup = false;
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DrawItem" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			string itemString = e.Index >= 0 ? this.GetItemText(this.Items[e.Index]) : string.Empty;

			if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit)
			{
				if (e.Index >= 0)
				{
					bool iEnabled = IsItemEnabled(e.Index);
					if (iEnabled)
					{
						e.DrawBackground();
						e.DrawFocusRectangle();
					}
					else
					{
						using (var bb = new SolidBrush(e.BackColor))
							e.Graphics.FillRectangle(bb, e.Bounds);
					}
					TextRenderer.DrawText(e.Graphics, itemString, e.Font, Rectangle.Inflate(e.Bounds, -2, 0), iEnabled ? (/*e.Index == this.SelectedIndex ? SystemColors.HighlightText :*/ e.ForeColor) : SystemColors.GrayText, tff);
				}
			}
			base.OnDrawItem(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDown" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown(e);
			State = ComboBoxState.Pressed;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ComboBox.DropDownClosed" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnDropDownClosed(EventArgs e)
		{
			base.OnDropDownClosed(e);
			State = ComboBoxState.Normal;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (BufferedPaintSupported)
			{
				NativeMethods.BufferedPaintInit();
				animationsNeedCleanup = true;
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnHandleDestroyed(System.EventArgs e)
		{
			if (dropDownWindow != null)
				dropDownWindow.DestroyHandle();
			base.OnHandleDestroyed(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			int idx = FindEnabledString(e.KeyChar.ToString(), this.SelectedIndex);
			if (idx == -1 || idx == this.SelectedIndex)
				e.Handled = true;
			base.OnKeyPress(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			Invalidate();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			State = ComboBoxState.Pressed;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = ComboBoxState.Hot;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (State != ComboBoxState.Pressed)
				State = ComboBoxState.Normal;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (State != ComboBoxState.Pressed)
				State = ComboBoxState.Hot;
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (BufferedPaintSupported)
			{
				bool stateChanged = !Object.Equals(currentState, newState);

				using (var hdc = new SafeGDIHandle(e.Graphics))
				{
					if (!hdc.IsInvalid)
					{
						// see if this paint was generated by a soft-fade animation
						if (!NativeMethods.BufferedPaintRenderAnimation(this.Handle, hdc))
						{
							NativeMethods.BufferedPaintAnimationParams animParams = new NativeMethods.BufferedPaintAnimationParams(NativeMethods.BufferedPaintAnimationStyle.Linear);

							// get appropriate animation time depending on state transition (or 0 if unchanged)
							if (stateChanged)
							{
								foreach (var item in transitions)
									if (item.currentState == currentState && item.newState == newState)
									{
										animParams.Duration = item.duration;
										break;
									}
							}

							Rectangle rc = this.ClientRectangle;
							IntPtr hdcFrom, hdcTo;
							IntPtr hbpAnimation = NativeMethods.BeginBufferedAnimation(this.Handle, hdc, ref rc, NativeMethods.BufferedPaintBufferFormat.CompatibleBitmap, IntPtr.Zero, ref animParams, out hdcFrom, out hdcTo);
							if (hbpAnimation != IntPtr.Zero)
							{
								if (hdcFrom != IntPtr.Zero)
								{
									using (Graphics gfxFrom = Graphics.FromHdc(hdcFrom))
										PaintControl(new PaintEventArgs(gfxFrom, e.ClipRectangle));
								}
								currentState = newState;
								if (hdcTo != IntPtr.Zero)
								{
									using (Graphics gfxTo = Graphics.FromHdc(hdcTo))
										PaintControl(new PaintEventArgs(gfxTo, e.ClipRectangle));
								}
								NativeMethods.EndBufferedAnimation(hbpAnimation, true);
							}
							else
							{
								hdc.Dispose();
								currentState = newState;
								PaintControl(e);
							}
						}
					}
				}
			}
			else
			{
				// buffered painting not supported, just paint using the current state
				currentState = newState;
				PaintControl(e);
			}
		}

		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			// don't paint the control's background
		}

		/// <summary>
		/// Paints the control.
		/// </summary>
		/// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
		protected virtual void PaintControl(PaintEventArgs e)
		{
			var cbi = NativeMethods.COMBOBOXINFO.FromComboBox(this);

			string itemText = this.SelectedIndex >= 0 ? this.GetItemText(this.SelectedItem) : string.Empty;
			ComboBoxState state = Enabled ? currentState : ComboBoxState.Disabled;
			Rectangle tr = cbi.rcItem;
			/*Rectangle tr = this.ClientRectangle;
			tr.Width -= (SystemInformation.VerticalScrollBarWidth + 2);
			tr.Inflate(0, -2);
			tr.Offset(1, 0);*/
			Rectangle br = cbi.rcButton;
			bool vsSuccess = false;
			if (VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles)
			{
				/*Rectangle r = Rectangle.Inflate(this.ClientRectangle, 1, 1);
				if (this.DropDownStyle != ComboBoxStyle.DropDownList)
				{
					e.Graphics.Clear(this.BackColor);
					ComboBoxRenderer.DrawTextBox(e.Graphics, r, itemText, this.Font, tr, tff, state);
					ComboBoxRenderer.DrawDropDownButton(e.Graphics, br, state);
				}
				else*/
				{
					try
					{
						var vr = new VisualStyleRenderer("Combobox", this.DropDownStyle == ComboBoxStyle.DropDownList ? 5 : 4, (int)state);
						vr.DrawParentBackground(e.Graphics, this.ClientRectangle, this);
						vr.DrawBackground(e.Graphics, this.ClientRectangle);
						if (this.DropDownStyle != ComboBoxStyle.DropDownList) br.Inflate(1, 1);
						Rectangle cr = this.DropDownStyle == ComboBoxStyle.DropDownList ? Rectangle.Inflate(br, -1, -1) : br;
						vr.SetParameters("Combobox", 7, (int)(br.Contains(this.PointToClient(Cursor.Position)) ? state : ComboBoxState.Normal));
						vr.DrawBackground(e.Graphics, br, cr);
						if (this.Focused && State != ComboBoxState.Pressed)
						{
							Size sz = TextRenderer.MeasureText(e.Graphics, "Wg", this.Font, tr.Size, TextFormatFlags.Default);
							Rectangle fr = Rectangle.Inflate(tr, 0, ((sz.Height - tr.Height) / 2) + 1);
							ControlPaint.DrawFocusRectangle(e.Graphics, fr);
						}
						TextRenderer.DrawText(e.Graphics, itemText, this.Font, tr, this.ForeColor, tff);
						vsSuccess = true;
					}
					catch { }
				}
			}

			if (!vsSuccess)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("CR:{0};ClR:{1};Foc:{2};St:{3};Tx:{4}", ClientRectangle, e.ClipRectangle, this.Focused, state, itemText));
				e.Graphics.Clear(this.BackColor);
				ControlPaint.DrawBorder3D(e.Graphics, this.ClientRectangle, Border3DStyle.Sunken);
				ControlPaint.DrawComboButton(e.Graphics, br, this.Enabled ? (state == ComboBoxState.Pressed ? ButtonState.Pushed : ButtonState.Normal) : ButtonState.Inactive);
				//using (var bb = new SolidBrush(this.BackColor))
				//	e.Graphics.FillRectangle(bb, tr);
				if (this.Focused)
				{
					Size sz = TextRenderer.MeasureText(e.Graphics, "Wg", this.Font, tr.Size, TextFormatFlags.Default);
					Rectangle fr = Rectangle.Inflate(tr, 0, ((sz.Height - tr.Height) / 2) + 1);
					e.Graphics.FillRectangle(SystemBrushes.Highlight, fr);
					ControlPaint.DrawFocusRectangle(e.Graphics, fr); //, this.ForeColor, SystemColors.Highlight);
				}
				TextRenderer.DrawText(e.Graphics, itemText, this.Font, tr, this.Focused ? SystemColors.HighlightText : this.ForeColor, tff);
			}
		}

		/// <summary>
		/// Processes a command key.
		/// </summary>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		/// true if the character was processed by the control; otherwise, false.
		/// </returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			int visItems = this.DropDownHeight / this.ItemHeight;
			switch (keyData)
			{
				case Keys.Down:
				case Keys.Right:
					this.SelectedIndex = GetNextEnabledItemIndex(this.SelectedIndex, true);
					return true;

				case Keys.Up:
				case Keys.Left:
					this.SelectedIndex = GetNextEnabledItemIndex(this.SelectedIndex, false);
					return true;

				case Keys.PageDown:
					if (this.SelectedIndex + visItems > this.Items.Count)
						this.SelectedIndex = GetNextEnabledItemIndex(this.Items.Count, false);
					else
						this.SelectedIndex = GetNextEnabledItemIndex(this.SelectedIndex + visItems, true);
					return true;

				case Keys.PageUp:
					if (this.SelectedIndex - visItems < 0)
						this.SelectedIndex = GetNextEnabledItemIndex(-1, true);
					else
						this.SelectedIndex = GetNextEnabledItemIndex(this.SelectedIndex - visItems, false);
					return true;

				case Keys.Home:
					this.SelectedIndex = GetNextEnabledItemIndex(-1, true);
					return true;

				case Keys.End:
					this.SelectedIndex = GetNextEnabledItemIndex(this.Items.Count, false);
					return true;

				case Keys.Enter:
					Point pt = NativeMethods.MapPointToClient(dropDownWindow, Cursor.Position);
					int idx = this.dropDownWindow.IndexFromPoint(pt.X, pt.Y);
					if (idx >= 0 && IsItemEnabled(idx))
						return false;
					this.DroppedDown = false;
					return true;

				case Keys.Escape:
					this.DroppedDown = false;
					return true;

				default:
					break;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (((int)((long)m.WParam)) == 0x3e80001)
			{
				dropDownWindow = new LBNativeWindow(m.LParam, this);
			}
		}

		private int FindEnabledString(string str, int startIndex)
		{
			if ((str != null) && (this.Items != null))
			{
				if ((startIndex < -1) || (startIndex >= this.Items.Count))
					return -1;
				int length = str.Length;
				int num2 = 0;
				for (int i = (startIndex + 1) % this.Items.Count; num2 < this.Items.Count; i = (i + 1) % this.Items.Count)
				{
					num2++;
					if (IsItemEnabled(i) && string.Compare(str, 0, this.GetItemText(this.Items[i]), 0, length, true, System.Globalization.CultureInfo.CurrentCulture) == 0)
						return i;
				}
			}
			return -1;
		}

		private int GetNextEnabledItemIndex(int startIndex, bool forward = true)
		{
			if (forward)
			{
				for (int i = startIndex + 1; i < this.Items.Count; i++)
				{
					if (IsItemEnabled(i))
						return i;
				}
				return startIndex;
			}
			else
			{
				for (int i = startIndex - 1; i >= 0; i--)
				{
					if (IsItemEnabled(i))
						return i;
				}
				return startIndex;
			}
		}
		private struct AnimationTransition<T>
		{
			public T currentState, newState;
			public UInt32 duration;

			public AnimationTransition(VisualStyleRenderer rnd, T fromState, T toState)
			{
				if (rnd.State != Convert.ToInt32(fromState))
					rnd.SetParameters(rnd.Class, rnd.Part, Convert.ToInt32(fromState));
				currentState = fromState;
				newState = toState;
				duration = rnd.GetTransitionDuration(Convert.ToInt32(toState));
			}
		}

		private class LBNativeWindow : NativeWindow
		{
			private DisabledItemComboBox Parent;

			public LBNativeWindow(IntPtr handle, DisabledItemComboBox parent)
			{
				Parent = parent;
				base.AssignHandle(handle);
			}

			public int IndexFromPoint(int x, int y)
			{
				int n = NativeMethods.SendMessage(base.Handle, 0x1a9 /* LB_ITEMFROMPOINT */, IntPtr.Zero, NativeMethods.Util.MAKELPARAM(x, y)).ToInt32();
				if (NativeMethods.Util.HIWORD(n) == 0)
					return NativeMethods.Util.LOWORD(n);
				return -1;
			}

			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 0x0202 || m.Msg == 0x0201 || m.Msg == 0x0203) /* WM_LBUTTONUP or WM_LBUTTONDOWN or WM_LBUTTONDBLCLK */
				{
					int idx = IndexFromPoint(NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam));
					if (idx >= 0 && !Parent.IsItemEnabled(idx))
						return;
				}
				base.WndProc(ref m);
			}
		}
	}
}