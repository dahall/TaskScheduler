using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace Microsoft.Win32.TaskScheduler.Design
{
	internal class TaskPropertiesControlDesigner : ControlDesigner
	{
		private static string[] propsToRemove = new string[] { "AutoScrollOffset", "AutoSize", "AutoSizeMode", "BackColor",
			"BackgroundImage", "BackgroundImageLayout", "ContextMenuStrip", "Cursor", "ForeColor", "UseWaitCursor", "Visible" };

		private DesignerActionListCollection actionList;

		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (actionList == null)
					actionList = new DesignerActionListCollection(new DesignerActionList[] { new InternalActionList(this) });
				return actionList;
			}
		}

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			base.Initialize(component);
			DesignerActionService service = this.GetService(typeof(DesignerActionService)) as DesignerActionService;
			if (service != null)
				service.Remove(component);
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			base.PreFilterProperties(properties);
			foreach (string p in propsToRemove)
				if (properties.Contains(p))
					properties.Remove(p);
		}

		internal class InternalActionList : DesignerActionList
		{
			public InternalActionList(TaskPropertiesControlDesigner ctrlDesigner) : base(ctrlDesigner.Component)
			{
				base.AutoShow = true;
			}

			public AvailableTaskTabs AvailableTabs
			{
				get { return this.Control.AvailableTabs; }
				set { SetProperty("AvailableTabs", value); }
			}

			public bool Editable
			{
				get { return this.Control.Editable; }
				set { SetProperty("Editable", value); }
			}

			public bool ShowErrors
			{
				get { return this.Control.ShowErrors; }
				set { SetProperty("ShowErrors", value); }
			}

			private TaskPropertiesControl Control
			{
				get { return this.Component as TaskPropertiesControl; }
			}

			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection col = new DesignerActionItemCollection();
				col.Add(new DesignerActionHeaderItem("Behavior"));
				col.Add(new DesignerActionPropertyItem("AvailableTabs", "Displayed tabs:", "Behavior"));
				col.Add(new DesignerActionPropertyItem("Editable", "Editable:", "Behavior"));
				col.Add(new DesignerActionPropertyItem("ShowErrors", "Show errors:", "Behavior"));
				return col;
			}

			private void SetProperty(string propertyName, object value)
			{
				PropertyDescriptor property = TypeDescriptor.GetProperties(this.Control)[propertyName];
				if (property != null)
					property.SetValue(this.Control, value);
			}
		}
	}
}