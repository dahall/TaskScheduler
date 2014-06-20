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
				LayoutEventHandler handler = null;
				handler = (sender, e) =>
				{
					if (ctrl.IsHandleCreated)
					{
						ctrl.Layout -= handler;
						action(ctrl);
					}
				};
				ctrl.Layout += handler;
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
		/// Gets the control in the list of parents of type <c>T</c>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Control"/> based <see cref="Type"/> of the parent control to retrieve.</typeparam>
		/// <param name="ctrl">This control.</param>
		/// <returns>The parent control matching T or null if not found.</returns>
		public static T GetParent<T>(this Control ctrl) where T : Control, new()
		{
			Control p = ctrl.Parent;
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
			var stack = new System.Collections.Generic.Stack<Control>();
			Control p = ctrl.Parent;
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
		public static RightToLeft GetRightToLeftProperty(this Control ctrl)
		{
			if (ctrl.RightToLeft == RightToLeft.Inherit)
				return GetRightToLeftProperty(ctrl.Parent);
			return ctrl.RightToLeft;
		}

		/// <summary>
		/// Determines whether this control is in design mode.
		/// </summary>
		/// <param name="ctrl">This control.</param>
		/// <returns><c>true</c> if in design mode; otherwise, <c>false</c>.</returns>
		public static bool IsDesignMode(this Control ctrl)
		{
			if (ctrl.Parent == null)
				return true;
			Control p = ctrl.Parent;
			while (p != null)
			{
				var site = p.Site;
				if (site != null && site.DesignMode)
					return true;
				p = p.Parent;
			}
			return false;
		}
	}
}
