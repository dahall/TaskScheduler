using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Microsoft.Win32.TaskScheduler.Design
{
	internal interface ITaskServiceAssignable
	{
		TaskService TaskService { get; set; }
	}

	internal class TaskServiceComponentDesigner : ComponentDesigner
	{
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			var refs = GetService(typeof(IReferenceService)) as IReferenceService;
			var tsColl = refs?.GetReferences(typeof(TaskService));
			System.Diagnostics.Debug.Assert(refs != null && tsColl != null && tsColl.Length > 0, "Designer couldn't find host, reference service, or existing TaskService.");
			if (tsColl != null && tsColl.Length > 0)
			{
				ITaskServiceAssignable tsComp = Component as ITaskServiceAssignable;
				TaskService ts = tsColl[0] as TaskService;
				if (tsComp != null)
					tsComp.TaskService = ts;
				else
				{
					var pi = Component.GetType().GetProperty("TaskService", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic, null, typeof(TaskService), System.Type.EmptyTypes, null);
					if (pi != null)
						pi.SetValue(Component, ts, null);
				}
			}
		}
	}
}
