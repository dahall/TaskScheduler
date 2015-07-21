using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	internal
#if !DEBUG
	abstract
#endif
	partial class OptionPanel : UserControl
	{
		protected bool onAssignment = false;
		protected TaskOptionsEditor parent;

		public OptionPanel()
		{
			InitializeComponent();
		}

		[Browsable(true), DefaultValue(null), Category("Appearance"), Localizable(true), Bindable(true)]
		public Image Image { get; set; }

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		[Browsable(true), DefaultValue((string)null), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Localizable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
		public string Title { get; set; }

		internal TaskDefinition td => parent.TaskDefinition;

		public void Initialize(TaskOptionsEditor editor)
		{
			parent = editor;
			onAssignment = true;
			InitializePanel();
			onAssignment = false;
		}

#if DEBUG
		protected virtual void InitializePanel() { }
#else
		protected abstract void InitializePanel();
#endif
	}
}
