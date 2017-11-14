using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	internal static class ControlExtension
	{
		/// <summary>
		/// Performs an action on a control after its handle has been created. If the control's handle has already been created, the action is executed immediately.
		/// </summary>
		/// <param name="ctrl">This control.</param>
		/// <param name="action">The action to execute.</param>
		public static void CallWhenHandleValid(this Control ctrl, Action<Control> action)
		{
			if (ctrl.IsHandleCreated)
			{
				action(ctrl);
			}
			else
			{
				void OnLayout(object sender, LayoutEventArgs e)
				{
					if (!ctrl.IsHandleCreated) return;
					ctrl.Layout -= OnLayout;
					action(ctrl);
				}

				ctrl.Layout += OnLayout;
			}
		}

		public static void EnableChildren(this Control ctl, bool enabled)
		{
			foreach (Control sub in ctl.Controls)
			{
				if (sub is ButtonBase || sub is ListControl || sub is TextBoxBase)
					sub.Enabled = enabled;
				sub.EnableChildren(enabled);
			}
		}

		/// <summary>
		/// Gets a string using a message pattern that asks for the string length by sending a GetXXXLen message and then a GetXXXText message.
		/// </summary>
		/// <param name="ctrl">The control.</param>
		/// <param name="getLenMsg">The MSG.</param>
		/// <param name="getTextMsg"></param>
		/// <returns></returns>
		public static string GetMessageString(this Control ctrl, uint getLenMsg, uint getTextMsg)
		{
			if (!ctrl.IsHandleCreated) return null;
			var cp = NativeMethods.SendMessage(ctrl.Handle, getLenMsg).ToInt32() + 1;
			var sb = new StringBuilder(cp);
			NativeMethods.SendMessage(ctrl.Handle, getTextMsg, ref cp, sb);
			return sb.ToString();
		}

		/// <summary>
		/// Gets the control in the list of parents of type <c>T</c>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Control"/> based <see cref="Type"/> of the parent control to retrieve.</typeparam>
		/// <param name="ctrl">This control.</param>
		/// <returns>The parent control matching T or null if not found.</returns>
		public static T GetParent<T>(this Control ctrl) where T : class
		{
			var p = ctrl.Parent;
			while (p != null & !(p is T))
				p = p.Parent;
			return p as T;
		}

		/// <summary>
		/// Gets the top-most control in the list of parents of type <c>T</c>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Control"/> based <see cref="Type"/> of the parent control to retrieve.</typeparam>
		/// <param name="ctrl">This control.</param>
		/// <returns>The top-most parent control matching T or null if not found.</returns>
		public static T GetTopMostParent<T>(this Control ctrl) where T : Control, new()
		{
			var stack = new Stack<Control>();
			var p = ctrl.Parent;
			while (p != null)
			{
				stack.Push(p);
				p = p.Parent;
			}
			while (stack.Count > 0)
				if ((p = stack.Pop()) is T)
					return p as T;
			return null;
		}

		/// <summary>
		/// Gets the right to left property.
		/// </summary>
		/// <param name="ctrl">This control.</param>
		/// <returns>Culture defined direction of text for this control.</returns>
		public static RightToLeft GetRightToLeftProperty(this Control ctrl) => ctrl.RightToLeft == RightToLeft.Inherit ? GetRightToLeftProperty(ctrl.Parent) : ctrl.RightToLeft;

		/// <summary>
		/// Determines whether this control is in design mode.
		/// </summary>
		/// <param name="ctrl">This control.</param>
		/// <returns><c>true</c> if in design mode; otherwise, <c>false</c>.</returns>
		public static bool IsDesignMode(this Control ctrl)
		{
			if (ctrl.Parent == null)
				return true;
			var p = ctrl.Parent;
			while (p != null)
			{
				var site = p.Site;
				if (site != null && site.DesignMode)
					return true;
				p = p.Parent;
			}
			return false;
		}

		public static void SetStyle(this Control ctrl, int style, bool on = true)
		{
			const int GWL_STYLE = -16;
			var oldstyle = NativeMethods.GetWindowLong(ctrl.Handle, GWL_STYLE).ToInt32();
			if ((oldstyle & style) != style && on)
				NativeMethods.SetWindowLong(ctrl.Handle, GWL_STYLE, new IntPtr(oldstyle | style));
			else if ((oldstyle & style) == style && !on)
				NativeMethods.SetWindowLong(ctrl.Handle, GWL_STYLE, new IntPtr(oldstyle & ~style));
			ctrl.Refresh();
		}
	}
}