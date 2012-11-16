using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace TestTaskService
{
	[Designer(typeof(HidableDetailPanelDesigner))]
	public partial class HidableDetailPanel : UserControl
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
			get { return headerTitle.Text; }
			set { headerTitle.Text = value; }
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
		public Panel DetailArea
		{
			get { return detailPanel; }
		}

		private void hideButton_Click(object sender, EventArgs e)
		{
			if (detailHidden)
			{
				detailHidden = !detailHidden;
				this.Height = defaultHeight;
				hideButton.Text = "5";
			}
			else
			{
				detailHidden = !detailHidden;
				this.Height = headerHeight;
				hideButton.Text = "6";
			}
		}

		private void HidableDetailPanel_Load(object sender, EventArgs e)
		{
			defaultHeight = this.Height;
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

			if (this.Control is HidableDetailPanel)
				this.EnableDesignMode(((HidableDetailPanel)this.Control).DetailArea, "DetailArea");

			DesignerActionService service = this.GetService(typeof(DesignerActionService)) as DesignerActionService;
			if (service != null)
				service.Remove(component);
		}
	}
}
