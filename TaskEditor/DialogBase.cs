using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>
	/// Abstract form that hides all events and properties that aren't essential for a dialog.
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(DialogBase), "TaskDialog")]
	public abstract class DialogBase : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DialogBase"/> class.
		/// </summary>
		public DialogBase()
		{
			base.MaximizeBox = false;
			base.ShowIcon = false;
			base.SizeGripStyle = Forms.SizeGripStyle.Hide;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		/// <summary>
		/// Occurs when the form is activated in code or by the user.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Activated
		{
			add { base.Activated += value; }
			remove { base.Activated -= value; }
		}

		/// <summary>
		/// Occurs when the value of the <see cref="T:System.Windows.Forms.BindingContext"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BindingContextChanged
		{
			add { base.BindingContextChanged += value; }
			remove { base.BindingContextChanged -= value; }
		}

		/// <summary>
		/// Occurs when the focus or keyboard user interface (UI) cues change.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event UICuesEventHandler ChangeUICues
		{
			add { base.ChangeUICues += value; }
			remove { base.ChangeUICues -= value; }
		}

		/// <summary>
		/// Occurs when the control is clicked.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add { base.Click += value; }
			remove { base.Click -= value; }
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ClientSize"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ClientSizeChanged
		{
			add { base.ClientSizeChanged += value; }
			remove { base.ClientSizeChanged -= value; }
		}

		/// <summary>
		/// Occurs when a new control is added to the <see cref="T:System.Windows.Forms.Control.ControlCollection"/>.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add { base.ControlAdded += value; }
			remove { base.ControlAdded -= value; }
		}

		/// <summary>
		/// Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection"/>.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlRemoved
		{
			add { base.ControlRemoved += value; }
			remove { base.ControlRemoved -= value; }
		}

		/// <summary>
		/// Occurs when the form loses focus and is no longer the active form.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Deactivate
		{
			add { base.Deactivate += value; }
			remove { base.Deactivate -= value; }
		}

		/// <summary>
		/// Occurs when the control is double-clicked.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add { base.DoubleClick += value; }
			remove { base.DoubleClick -= value; }
		}

		/// <summary>
		/// Occurs when a drag-and-drop operation is completed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragDrop
		{
			add { base.DragDrop += value; }
			remove { base.DragDrop -= value; }
		}

		/// <summary>
		/// Occurs when an object is dragged into the control's bounds.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragEnter
		{
			add { base.DragEnter += value; }
			remove { base.DragEnter -= value; }
		}

		/// <summary>
		/// Occurs when an object is dragged out of the control's bounds.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DragLeave
		{
			add { base.DragLeave += value; }
			remove { base.DragLeave -= value; }
		}

		/// <summary>
		/// Occurs when an object is dragged over the control's bounds.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event DragEventHandler DragOver
		{
			add { base.DragOver += value; }
			remove { base.DragOver -= value; }
		}

		/// <summary>
		/// Occurs when the control is entered.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add { base.Enter += value; }
			remove { base.Enter -= value; }
		}

		/// <summary>
		/// Occurs after the form is closed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event FormClosedEventHandler FormClosed
		{
			add { base.FormClosed += value; }
			remove { base.FormClosed -= value; }
		}

		/// <summary>
		/// Occurs before the form is closed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event FormClosingEventHandler FormClosing
		{
			add { base.FormClosing += value; }
			remove { base.FormClosing -= value; }
		}

		/// <summary>
		/// Occurs during a drag operation.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event GiveFeedbackEventHandler GiveFeedback
		{
			add { base.GiveFeedback += value; }
			remove { base.GiveFeedback -= value; }
		}

		/// <summary>
		/// Occurs when the Help button is clicked.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler HelpButtonClicked
		{
			add { base.HelpButtonClicked += value; }
			remove { base.HelpButtonClicked -= value; }
		}

		/// <summary>
		/// Occurs when the user requests help for a control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event HelpEventHandler HelpRequested
		{
			add { base.HelpRequested += value; }
			remove { base.HelpRequested -= value; }
		}

		/// <summary>
		/// Occurs after the input language of the form has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event InputLanguageChangedEventHandler InputLanguageChanged
		{
			add { base.InputLanguageChanged += value; }
			remove { base.InputLanguageChanged -= value; }
		}

		/// <summary>
		/// Occurs when the user attempts to change the input language for the form.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event InputLanguageChangingEventHandler InputLanguageChanging
		{
			add { base.InputLanguageChanging += value; }
			remove { base.InputLanguageChanging -= value; }
		}

		/// <summary>
		/// Occurs when a key is pressed while the control has focus.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add { base.KeyDown += value; }
			remove { base.KeyDown -= value; }
		}

		/// <summary>
		/// Occurs when a key is pressed while the control has focus.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add { base.KeyPress += value; }
			remove { base.KeyPress -= value; }
		}

		/// <summary>
		/// Occurs when a key is released while the control has focus.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add { base.KeyUp += value; }
			remove { base.KeyUp -= value; }
		}

		/// <summary>
		/// Occurs when a control should reposition its child controls.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event LayoutEventHandler Layout
		{
			add { base.Layout += value; }
			remove { base.Layout -= value; }
		}

		/// <summary>
		/// Occurs when the input focus leaves the control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add { base.Leave += value; }
			remove { base.Leave -= value; }
		}

		/// <summary>
		/// Occurs before a form is displayed for the first time.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Load
		{
			add { base.Load += value; }
			remove { base.Load -= value; }
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximizedBounds"/> property has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MaximizedBoundsChanged
		{
			add { base.MaximizedBoundsChanged += value; }
			remove { base.MaximizedBoundsChanged -= value; }
		}

		/// <summary>
		/// Occurs when a multiple-document interface (MDI) child form is activated or closed within an MDI application.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MdiChildActivate
		{
			add { base.MdiChildActivate += value; }
			remove { base.MdiChildActivate -= value; }
		}

		/// <summary>
		/// Occurs when the control loses or gains mouse capture.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseCaptureChanged
		{
			add { base.MouseCaptureChanged += value; }
			remove { base.MouseCaptureChanged -= value; }
		}

		/// <summary>
		/// Occurs when the control is clicked by the mouse.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseClick
		{
			add { base.MouseClick += value; }
			remove { base.MouseClick -= value; }
		}

		/// <summary>
		/// Occurs when the control is double clicked by the mouse.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add { base.MouseDoubleClick += value; }
			remove { base.MouseDoubleClick -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is over the control and a mouse button is pressed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add { base.MouseDown += value; }
			remove { base.MouseDown -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer enters the control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseEnter
		{
			add { base.MouseEnter += value; }
			remove { base.MouseEnter -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer rests on the control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add { base.MouseHover += value; }
			remove { base.MouseHover -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer leaves the control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseLeave
		{
			add { base.MouseLeave += value; }
			remove { base.MouseLeave -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is moved over the control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add { base.MouseMove += value; }
			remove { base.MouseMove -= value; }
		}

		/// <summary>
		/// Occurs when the mouse pointer is over the control and a mouse button is released.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add { base.MouseUp += value; }
			remove { base.MouseUp -= value; }
		}

		/// <summary>
		/// Occurs when the control is moved.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Move
		{
			add { base.Move += value; }
			remove { base.Move -= value; }
		}

		/// <summary>
		/// Occurs when the control is redrawn.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add { base.Paint += value; }
			remove { base.Paint -= value; }
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Parent"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ParentChanged
		{
			add { base.ParentChanged += value; }
			remove { base.ParentChanged -= value; }
		}

		/// <summary>
		/// Occurs before the <see cref="E:System.Windows.Forms.Control.KeyDown"/> event when a key is pressed while focus is on this control.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event PreviewKeyDownEventHandler PreviewKeyDown
		{
			add { base.PreviewKeyDown += value; }
			remove { base.PreviewKeyDown -= value; }
		}

		/// <summary>
		/// Occurs when <see cref="T:System.Windows.Forms.AccessibleObject"/> is providing help to accessibility applications.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
		{
			add { base.QueryAccessibilityHelp += value; }
			remove { base.QueryAccessibilityHelp -= value; }
		}

		/// <summary>
		/// Occurs during a drag-and-drop operation and enables the drag source to determine whether the drag-and-drop operation should be canceled.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event QueryContinueDragEventHandler QueryContinueDrag
		{
			add { base.QueryContinueDrag += value; }
			remove { base.QueryContinueDrag -= value; }
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Region"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RegionChanged
		{
			add { base.RegionChanged += value; }
			remove { base.RegionChanged -= value; }
		}

		/// <summary>
		/// Occurs when the control is resized.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Resize
		{
			add { base.Resize += value; }
			remove { base.Resize -= value; }
		}

		/// <summary>
		/// Occurs when a form enters resizing mode.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ResizeBegin
		{
			add { base.ResizeBegin += value; }
			remove { base.ResizeBegin -= value; }
		}

		/// <summary>
		/// Occurs when a form exits resizing mode.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ResizeEnd
		{
			add { base.ResizeEnd += value; }
			remove { base.ResizeEnd -= value; }
		}

		/// <summary>
		/// Occurs when the user or code scrolls through the client area.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event ScrollEventHandler Scroll
		{
			add { base.Scroll += value; }
			remove { base.Scroll -= value; }
		}

		/// <summary>
		/// Occurs whenever the form is first displayed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Shown
		{
			add { base.Shown += value; }
			remove { base.Shown -= value; }
		}

		/// <summary>
		/// Occurs when the control style changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler StyleChanged
		{
			add { base.StyleChanged += value; }
			remove { base.StyleChanged -= value; }
		}

		/// <summary>
		/// Occurs when the system colors change.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler SystemColorsChanged
		{
			add { base.SystemColorsChanged += value; }
			remove { base.SystemColorsChanged -= value; }
		}

		/// <summary>
		/// Occurs when the control is finished validating.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add { base.Validated += value; }
			remove { base.Validated -= value; }
		}

		/// <summary>
		/// Occurs when the control is validating.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add { base.Validating += value; }
			remove { base.Validating -= value; }
		}
		
		/// <summary>
		/// Occurs when [auto size changed].
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when [auto validate changed].
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackColor"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.BackgroundImageLayout"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.CausesValidation"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

#if !NET5_0_OR_GREATER && !NETCOREAPP
		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenu"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuChanged
		{
			add
			{
				base.ContextMenuChanged += value;
			}
			remove
			{
				base.ContextMenuChanged -= value;
			}
		}
#endif

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenuStrip"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Cursor"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Dock"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled"/> property value has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Font"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.ImeMode"/> property has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Location"/> property value has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when [margin changed].
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MarginChanged
		{
			add
			{
				base.MarginChanged += value;
			}
			remove
			{
				base.MarginChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MaximumSize"/> property has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MaximumSizeChanged
		{
			add
			{
				base.MaximumSizeChanged += value;
			}
			remove
			{
				base.MaximumSizeChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the value of the <see cref="P:System.Windows.Forms.Form.MinimumSize"/> property has changed.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MinimumSizeChanged
		{
			add
			{
				base.MinimumSizeChanged += value;
			}
			remove
			{
				base.MinimumSizeChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the control's padding changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>
		/// Occurs after the value of the <see cref="P:System.Windows.Forms.Form.RightToLeftLayout"/> property changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.RightToLeftLayoutChanged += value;
			}
			remove
			{
				base.RightToLeftLayoutChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Size"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler SizeChanged
		{
			add
			{
				base.SizeChanged += value;
			}
			remove
			{
				base.SizeChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when [tab stop changed].
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Text"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="P:System.Windows.Forms.Control.Visible"/> property value changes.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		/// <summary>
		/// Gets or sets the button on the form that is clicked when the user presses the ENTER key.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.IButtonControl"/> that represents the button to use as the accept button for the form.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl AcceptButton
		{
			get
			{
				return base.AcceptButton;
			}
			set
			{
				base.AcceptButton = value;
			}
		}

		/// <summary>
		/// Gets or sets the description of the control used by accessibility client applications.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The description of the control used by accessibility client applications. The default is null.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleDescription
		{
			get
			{
				return base.AccessibleDescription;
			}
			set
			{
				base.AccessibleDescription = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the control used by accessibility client applications.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The name of the control used by accessibility client applications. The default is null.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new string AccessibleName
		{
			get
			{
				return base.AccessibleName;
			}
			set
			{
				base.AccessibleName = value;
			}
		}

		/// <summary>
		/// Gets or sets the accessible role of the control
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the values of <see cref="T:System.Windows.Forms.AccessibleRole"/>. The default is Default.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value assigned is not one of the <see cref="T:System.Windows.Forms.AccessibleRole"/> values.
		/// </exception>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new AccessibleRole AccessibleRole
		{
			get
			{
				return base.AccessibleRole;
			}
			set
			{
				base.AccessibleRole = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control can accept data that the user drags onto it.
		/// </summary>
		/// <value></value>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>
		/// Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles"/> values. The default is Top and Left.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form adjusts its size to fit the height of the font used on the form and scales its controls.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form will automatically scale itself and its controls based on the current font assigned to the form; otherwise, false. The default is true.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Obsolete("This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public new bool AutoScale
		{
#pragma warning disable 0618
			get { return base.AutoScale; }
			set { base.AutoScale = value; }
#pragma warning restore 0618
		}

		/// <summary>
		/// Gets or sets the base size used for autoscaling of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the base size that this form uses for autoscaling.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Size AutoScaleBaseSize
		{
			get
			{
				return base.AutoScaleBaseSize;
			}
			set
			{
				base.AutoScaleBaseSize = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form enables autoscrolling.
		/// </summary>
		/// <value></value>
		/// <returns>true to enable autoscrolling on the form; otherwise, false. The default is false.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the auto-scroll margin.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the height and width of the auto-scroll margin in pixels.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// The <see cref="P:System.Drawing.Size.Height"/> or <see cref="P:System.Drawing.Size.Width"/> value assigned is less than 0.
		/// </exception>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(typeof(Size), "0, 0")]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum size of the auto-scroll.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that determines the minimum size of the virtual area through which the user can scroll.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(typeof(Size), "0, 0")]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>
		/// Resize the form according to the setting of <see cref="P:System.Windows.Forms.Form.AutoSizeMode"/>.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form will automatically resize; false if it must be manually resized.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the mode by which the form automatically resizes itself.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.AutoSizeMode"/> enumerated value. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly"/>.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode"/> value.
		/// </exception>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
				base.AutoSizeMode = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether controls in this container will be automatically validated when the focus changes.
		/// </summary>
		/// <value>
		/// An <see cref="T:System.Windows.Forms.AutoValidate"/> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit"/>.
		/// </value>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		/// <value>
		/// A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.
		/// </value>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the background image displayed in the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Drawing.Image"/> that represents the image to display in the background of the control.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>
		/// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout"/> enumeration.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the values of <see cref="T:System.Windows.Forms.ImageLayout"/> (<see cref="F:System.Windows.Forms.ImageLayout.Center"/> , <see cref="F:System.Windows.Forms.ImageLayout.None"/>, <see cref="F:System.Windows.Forms.ImageLayout.Stretch"/>, <see cref="F:System.Windows.Forms.ImageLayout.Tile"/>, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom"/>). <see cref="F:System.Windows.Forms.ImageLayout.Tile"/> is the default value.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The specified enumeration value does not exist.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>
		/// Gets or sets the button control that is clicked when the user presses the ESC key.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Windows.Forms.IButtonControl"/> that represents the cancel button for the form.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new IButtonControl CancelButton
		{
			get
			{
				return base.CancelButton;
			}
			set
			{
				base.CancelButton = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.
		/// </summary>
		/// <value></value>
		/// <returns>true if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, false. The default is true.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the client area of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the size of the form's client area.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size ClientSize
		{
			get
			{
				return base.ClientSize;
			}
			set
			{
				base.ClientSize = value;
			}
		}

#if !NET5_0_OR_GREATER && !NETCOREAPP
		/// <summary>
		/// Gets or sets the shortcut menu associated with the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.ContextMenu"/> that represents the shortcut menu associated with the control.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
			}
		}
#endif

		/// <summary>
		/// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip"/> associated with this control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Windows.Forms.ContextMenuStrip"/> for this control, or null if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip"/>. The default is null.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether a control box is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form displays a control box in the upper left corner of the form; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool ControlBox
		{
			get
			{
				return base.ControlBox;
			}
			set
			{
				base.ControlBox = value;
			}
		}

		/// <summary>
		/// Gets or sets the cursor that is displayed when the mouse pointer is over the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Cursor"/> that represents the cursor to display when the mouse pointer is over the control.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>
		/// Gets the data bindings for the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.ControlBindingsCollection"/> that contains the <see cref="T:System.Windows.Forms.Binding"/> objects for the control.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ControlBindingsCollection DataBindings => base.DataBindings;

		/// <summary>
		/// Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.DockStyle"/> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None"/>.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle"/> values.
		/// </exception>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>
		/// Gets the dock padding settings for all edges of the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges"/> that represents the padding for all the edges of a docked control.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ScrollableControl.DockPaddingEdges DockPadding => base.DockPadding;

		/// <summary>
		/// Gets or sets a value indicating whether the control can respond to user interaction.
		/// </summary>
		/// <value></value>
		/// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(true)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Drawing.Font"/> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont"/> property.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>
		/// Gets or sets the foreground color of the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The foreground <see cref="T:System.Drawing.Color"/> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor"/> property.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the border style of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormBorderStyle"/> that represents the style of border to display for the form. The default is FormBorderStyle.Sizable.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value specified is outside the range of valid values.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new FormBorderStyle FormBorderStyle
		{
			get
			{
				return base.FormBorderStyle;
			}
			set
			{
				base.FormBorderStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether a Help button should be displayed in the caption box of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true to display a Help button in the form's caption bar; otherwise, false. The default is false.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool HelpButton
		{
			get
			{
				return base.HelpButton;
			}
			set
			{
				base.HelpButton = value;
			}
		}

		/// <summary>
		/// Gets or sets the icon for the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Drawing.Icon"/> that represents the icon for the form.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Icon Icon
		{
			get
			{
				return base.Icon;
			}
			set
			{
				base.Icon = value;
			}
		}

		/// <summary>
		/// Gets or sets the Input Method Editor (IME) mode of the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.ImeMode"/> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit"/>.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode"/> enumeration values.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form is a container for multiple-document interface (MDI) child forms.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form is a container for MDI child forms; otherwise, false. The default is false.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool IsMdiContainer
		{
			get
			{
				return base.IsMdiContainer;
			}
			set
			{
				base.IsMdiContainer = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form will receive key events before the event is passed to the control that has focus.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form will receive all key events; false if the currently selected control on the form receives key events. The default is false.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool KeyPreview
		{
			get
			{
				return base.KeyPreview;
			}
			set
			{
				base.KeyPreview = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.Drawing.Point"/> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form"/> in screen coordinates.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The <see cref="T:System.Drawing.Point"/> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.Form"/> in screen coordinates.
		/// </returns>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>
		/// Gets or sets the primary menu container for the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.MenuStrip"/> that represents the container for the menu structure of the form. The default is null.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new MenuStrip MainMenuStrip
		{
			get { return base.MainMenuStrip; }
			set { base.MainMenuStrip = value; }
		}
		
		/// <summary>
		/// Gets or sets the margin.
		/// </summary>
		/// <value>The margin.</value>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(false)]
		public new bool MaximizeBox
		{
			get
			{
				return base.MaximizeBox;
			}
			set
			{
				base.MaximizeBox = value;
			}
		}

		/// <summary>
		/// Gets the maximum size the form can be resized to.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the maximum size for the form.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// The values of the height or width within the <see cref="T:System.Drawing.Size"/> object are less than zero.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

#if !NET5_0_OR_GREATER && !NETCOREAPP
		/// <summary>
		/// Gets or sets the <see cref="T:System.Windows.Forms.MainMenu"/> that is displayed in the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.MainMenu"/> that represents the menu to display in the form.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new MainMenu Menu
		{
			get
			{
				return base.Menu;
			}
			set
			{
				base.Menu = value;
			}
		}
#endif

		/// <summary>
		/// Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(true)]
		public new bool MinimizeBox
		{
			get
			{
				return base.MinimizeBox;
			}
			set
			{
				base.MinimizeBox = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum size the form can be resized to.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the minimum size for the form.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// The values of the height or width within the <see cref="T:System.Drawing.Size"/> object are less than zero.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>
		/// Gets or sets the opacity level of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The level of opacity for the form. The default is 1.00.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
		public new double Opacity
		{
			get
			{
				return base.Opacity;
			}
			set
			{
				base.Opacity = value;
			}
		}

		/// <summary>
		/// Gets or sets padding within the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.Padding"/> representing the control's internal spacing characteristics.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// One of the <see cref="T:System.Windows.Forms.RightToLeft"/> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit"/>.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft"/> values.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether right-to-left mirror placement is turned on.
		/// </summary>
		/// <value></value>
		/// <returns>true if right-to-left mirror placement is turned on; otherwise, false for standard child control placement. The default is false.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override bool RightToLeftLayout
		{
			get
			{
				return base.RightToLeftLayout;
			}
			set
			{
				base.RightToLeftLayout = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether an icon is displayed in the caption bar of the form.
		/// </summary>
		/// <value></value>
		/// <returns>true if the form displays an icon in the caption bar; otherwise, false. The default is true.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(false)]
		public new bool ShowIcon
		{
			get
			{
				return base.ShowIcon;
			}
			set
			{
				base.ShowIcon = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form is displayed in the Windows taskbar.
		/// </summary>
		/// <value></value>
		/// <returns>true to display the form in the Windows taskbar at run time; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(false)]
		public new bool ShowInTaskbar
		{
			get
			{
				return base.ShowInTaskbar;
			}
			set
			{
				base.ShowInTaskbar = value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Size"/> that represents the size of the form.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>
		/// Gets or sets the style of the size grip to display in the lower-right corner of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.SizeGripStyle"/> that represents the style of the size grip to display. The default is <see cref="F:System.Windows.Forms.SizeGripStyle.Auto"/></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value specified is outside the range of valid values.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(typeof(SizeGripStyle), "Hide")]
		public new SizeGripStyle SizeGripStyle
		{
			get
			{
				return base.SizeGripStyle;
			}
			set
			{
				base.SizeGripStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets the starting position of the form at run time.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormStartPosition"/> that represents the starting position of the form.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value specified is outside the range of valid values.
		/// </exception>
		[DefaultValue(typeof(FormStartPosition), "CenterParent")]
		public new FormStartPosition StartPosition
		{
			get
			{
				return base.StartPosition;
			}
			set
			{
				base.StartPosition = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
		/// </summary>
		/// <value><c>true</c> if the user can give the focus to the control using the TAB key; otherwise, <c>false</c>. The default is <c>true</c>.</value>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>
		/// Gets or sets the object that contains data about the control.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An <see cref="T:System.Object"/> that contains data about the control. The default is null.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
			}
		}

		/// <summary>
		/// Gets or sets the text associated with this control.
		/// </summary>
		/// <value>The text associated with this control.</value>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the form should be displayed as a topmost form.
		/// </summary>
		/// <value></value>
		/// <returns>true to display the form as a topmost form; otherwise, false. The default is false.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TopMost
		{
			get
			{
				return base.TopMost;
			}
			set
			{
				base.TopMost = value;
			}
		}

		/// <summary>
		/// Gets or sets the color that will represent transparent areas of the form.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Drawing.Color"/> that represents the color to display transparently on the form.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new Color TransparencyKey
		{
			get
			{
				return base.TransparencyKey;
			}
			set
			{
				base.TransparencyKey = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use the wait cursor for the current control and all child controls.
		/// </summary>
		/// <value></value>
		/// <returns>true to use the wait cursor for the current control and all child controls; otherwise, false. The default is false.
		/// </returns>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool UseWaitCursor
		{
			get
			{
				return base.UseWaitCursor;
			}
			set
			{
				base.UseWaitCursor = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control and all its parent controls are displayed.
		/// </summary>
		/// <value></value>
		/// <returns>true if the control and all its parent controls are displayed; otherwise, false. The default is true.
		/// </returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DefaultValue(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>
		/// Gets or sets the form's window state.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Windows.Forms.FormWindowState"/> that represents the window state of the form. The default is FormWindowState.Normal.
		/// </returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		/// The value specified is outside the range of valid values.
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/>
		/// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public new FormWindowState WindowState
		{
			get
			{
				return base.WindowState;
			}
			set
			{
				base.WindowState = value;
			}
		}
	}
}
