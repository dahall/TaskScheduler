using System;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>Extension methods for <see cref="TaskService"/>.</summary>
	public static class TaskServiceExtensions
	{
		/// <summary>Filtered the supplied available actions based on this <see cref="TaskDefinition"/> and the version of the Task Scheduler.</summary>
		/// <param name="td">The <see cref="TaskDefinition"/> instance.</param>
		/// <param name="availableActions">The available actions.</param>
		/// <param name="taskSchedulerVersion">The Task Scheduler version.</param>
		/// <returns>The filtered set of available actions.</returns>
		public static AvailableActions GetFilteredAvailableActions(this TaskDefinition td, AvailableActions availableActions, Version taskSchedulerVersion) =>
			GetFilteredAvailableActions(availableActions, taskSchedulerVersion, td.Settings.UseUnifiedSchedulingEngine, td.Actions.PowerShellConversion);

		/// <summary>Filtered the supplied available actions based on this version of the Task Scheduler and options that could be set on the <see cref="TaskDefinition"/>.</summary>
		/// <param name="ts">The <see cref="TaskService"/> instance.</param>
		/// <param name="availableActions">The available actions.</param>
		/// <param name="useUnifiedSchedulingEngine">if set to <c>true</c> assume the task will use the Unified Scheduling Engine.</param>
		/// <param name="psOption">The PowerShell conversion options to assume are in place.</param>
		/// <returns>The filtered set of available actions.</returns>
		public static AvailableActions GetFilteredAvailableActions(this TaskService ts, AvailableActions availableActions, bool useUnifiedSchedulingEngine = false, PowerShellActionPlatformOption psOption = PowerShellActionPlatformOption.All) =>
			GetFilteredAvailableActions(availableActions, ts.HighestSupportedVersion, useUnifiedSchedulingEngine, psOption);

		internal static AvailableActions GetFilteredAvailableActions(AvailableActions availableActions, Version tsVer, bool useUnifiedSchedulingEngine, PowerShellActionPlatformOption psOption)
		{
			var ret = availableActions;
			var isV1 = tsVer < new Version(1, 2);
			var isAfter7 = tsVer > new Version(1, 3);
			var isWin7 = tsVer == new Version(1, 3);
			// ComHandler not supported in V1
			if (isV1)
				ret &= ~AvailableActions.ComHandler;
			// Email and Message actions were made available in Vista (v1.2) and deprecated in Windows 8 (v1.4)
			// This library can optionally make them available regardless of version
			// Unified Sch Eng disallows these same actions in Win7 (v1.3)
			if ((isV1 && !psOption.IsFlagSet(PowerShellActionPlatformOption.Version1)) || (!psOption.IsFlagSet(PowerShellActionPlatformOption.Version2) && ((useUnifiedSchedulingEngine && isWin7) || isAfter7)))
				ret &= ~(AvailableActions.SendEmail | AvailableActions.ShowMessage);
			if (ret == 0) throw new InvalidOperationException("No actions are available to display given the current settings.");
			return ret;
		}

		/// <summary>Filtered the supplied available triggers based on this <see cref="TaskDefinition"/> and the version of the Task Scheduler.</summary>
		/// <param name="td">The <see cref="TaskDefinition"/> instance.</param>
		/// <param name="availableTriggers">The available triggers.</param>
		/// <param name="taskSchedulerVersion">The Task Scheduler version.</param>
		/// <param name="showCustom">Show <see cref="CustomTrigger"/> instances.</param>
		/// <returns>The filtered set of available triggers.</returns>
		public static AvailableTriggers GetFilteredAvailableTriggers(this TaskDefinition td, AvailableTriggers availableTriggers, Version taskSchedulerVersion, bool showCustom = false) =>
			GetFilteredAvailableTriggers(availableTriggers, taskSchedulerVersion, td.Settings.UseUnifiedSchedulingEngine, showCustom);

		/// <summary>Filtered the supplied available triggers based on this version of the Task Scheduler and options that could be set on the <see cref="TaskDefinition"/>.</summary>
		/// <param name="ts">The <see cref="TaskService"/> instance.</param>
		/// <param name="availableTriggers">The available triggers.</param>
		/// <param name="useUnifiedSchedulingEngine">if set to <c>true</c> assume the task will use the Unified Scheduling Engine.</param>
		/// <returns>The filtered set of available triggers.</returns>
		public static AvailableTriggers GetFilteredAvailableTriggers(this TaskService ts, AvailableTriggers availableTriggers, bool useUnifiedSchedulingEngine = false) =>
			GetFilteredAvailableTriggers(availableTriggers, ts.HighestSupportedVersion, useUnifiedSchedulingEngine, true);

		internal static AvailableTriggers GetFilteredAvailableTriggers(AvailableTriggers availableTriggers, Version tsVer, bool useUnifiedSchedulingEngine, bool showCustom)
		{
			var isV1 = tsVer < new Version(1, 2);
			var ret = availableTriggers;
			// Remove all non-V1 triggers if set
			if (isV1)
				ret &= ~(AvailableTriggers.Event | AvailableTriggers.Registration | AvailableTriggers.SessionStateChange);
			// Remove custom trigger if not shown or v1
			if (!showCustom || isV1)
				ret &= ~AvailableTriggers.Custom;
			// Remove unsupported USE triggers only on Win7
			if (useUnifiedSchedulingEngine && tsVer == new Version(1, 3))
				ret &= ~(AvailableTriggers.Monthly | AvailableTriggers.MonthlyDOW);
			return ret != 0 ? ret : throw new InvalidOperationException("No triggers are available to display given the current settings.");
		}
	}
}
