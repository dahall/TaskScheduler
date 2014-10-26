using System.ComponentModel;

namespace Microsoft.Win32.TaskScheduler
{
	public interface ITaskEditor : ITaskDefinitionEditor
	{
		/// <summary>
		/// Gets the current <see cref="Task"/>. This is only the task used to initialize this control. The updates made to the control are not registered.
		/// </summary>
		/// <value>The task.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		Task Task { get; }

		/// <summary>
		/// Gets or sets the folder for the task. If control is initialized with a <see cref="Task"/>, this value will be set to the folder of the registered task.
		/// </summary>
		/// <value>The task folder name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		string TaskFolder { get; set; }

		/// <summary>
		/// Gets or sets the name of the task. If control is initialized with a <see cref="Task"/>, this value will be set to the name of the registered task.
		/// </summary>
		/// <value>The task name.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		string TaskName { get; set; }
	}

	public interface ITaskDefinitionEditor
	{
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TaskEditDialog"/> is editor.Editable.
		/// </summary>
		/// <value><c>true</c> if editor.Editable; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Category("Behavior"), Description("Determines whether the task can be edited.")]
		bool Editable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether errors are shown in the UI.
		/// </summary>
		/// <value>
		///   <c>true</c> if errors are shown; otherwise, <c>false</c>.
		/// </value>
		[DefaultValue(true), Category("Behavior"), Description("Determines whether errors are shown in the UI.")]
		bool ShowErrors { get; set; }

		/// <summary>
		/// Gets the <see cref="TaskDefinition"/> in its edited state.
		/// </summary>
		/// <value>The task definition.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		TaskDefinition TaskDefinition { get; }

		/// <summary>
		/// Gets the <see cref="TaskService"/> assigned at initialization.
		/// </summary>
		/// <value>The task service.</value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		TaskService TaskService { get; }

		/// <summary>
		/// Gets or sets a value indicating whether this task definition is v2.
		/// </summary>
		/// <value>
		///   <c>true</c> if this task definition is v2; otherwise, <c>false</c>.
		/// </value>
		bool IsV2 { get; }
	}

	public interface ITaskEditorUIElement
	{
		void RefreshState();
	}
}
