namespace System.Windows.Forms
{
	internal static class ControlExtension
	{
		public static RightToLeft GetRightToLeftProperty(this Control ctl)
		{
			if (ctl.RightToLeft == RightToLeft.Inherit)
			{
				return GetRightToLeftProperty(ctl.Parent);
			}
			return ctl.RightToLeft;
		}

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

		public static System.Drawing.Color GetTrueParentBackColor(this Control ctrl)
		{
			if (ctrl.Parent is TabPage && Application.RenderWithVisualStyles && ((TabPage)ctrl.Parent).UseVisualStyleBackColor && (((TabPage)ctrl.Parent).Parent != null && ((TabControl)((TabPage)ctrl.Parent).Parent).Appearance == TabAppearance.Normal))
			{
				var vs = new System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Tab.Body.Normal);
				return vs.GetColor(System.Windows.Forms.VisualStyles.ColorProperty.GlowColor);
			}
			return ctrl.Parent == null ? ctrl.BackColor : ctrl.Parent.BackColor;
		}
	}
}
