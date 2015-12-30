using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace TestTaskService
{
	[Designer(typeof(HidableDetailPanelDesigner))]
	public partial class HidableDetailPanel : Control
	{
		private const int headerHeight = 24;
		private int defaultHeight = 100;
		private bool detailHidden = false;

		public HidableDetailPanel()
		{
			InitializeComponent();
			tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
			tableLayoutPanel.RowStyles[0].Height = headerHeight;
		}

		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get { return headerPanel.Text; }
			set { headerPanel.Text = value; }
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
		public Panel DetailArea => detailPanel;

		private void headerPanel_CheckedChanged(object sender, EventArgs e)
		{
			if (detailHidden)
			{
				detailHidden = !detailHidden;
				Height = defaultHeight;
			}
			else
			{
				detailHidden = !detailHidden;
				Height = headerHeight;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (!detailHidden)
				defaultHeight = Height;
		}
	}

	public class HidableDetailPanelDesigner : System.Windows.Forms.Design.ParentControlDesigner
	{
		/*private static string[] propsToRemove = new string[] { "Anchor", "AutoScrollOffset", "AutoSize", "BackColor",
			"BackgroundImage", "BackgroundImageLayout", "ContextMenuStrip", "Cursor", "Dock", "Enabled", "Font",
			"ForeColor", "Location", "Margin", "MaximumSize", "MinimumSize", "Padding", "Size", "TabStop", "UseWaitCursor",
			"Visible" };

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			base.PreFilterProperties(properties);
			foreach (string p in propsToRemove)
				if (properties.Contains(p))
					properties.Remove(p);
		}*/

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);

			if (Control is HidableDetailPanel)
				EnableDesignMode(((HidableDetailPanel)Control).DetailArea, "DetailArea");

			DesignerActionService service = GetService(typeof(DesignerActionService)) as DesignerActionService;
			if (service != null)
				service.Remove(component);
		}
	}
}
