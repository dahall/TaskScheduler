using Microsoft.Win32.SafeHandles;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum TaskRights
	{
		ChangePermissions = 0x40000,
		Delete = 0x10000,
		FullControl = 0x1f0001,
		Modify = 1,
		ReadPermissions = 0x20000,
		Synchronize = 0x100000,
		TakeOwnership = 0x80000
	}

	/// <summary>
	/// 
	/// </summary>
	public sealed class TaskAccessRule : AccessRule
	{
		// Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskAccessRule"/> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="eventRights">The event rights.</param>
		/// <param name="type">The type.</param>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public TaskAccessRule(IdentityReference identity, TaskRights eventRights, AccessControlType type)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskAccessRule"/> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="eventRights">The event rights.</param>
		/// <param name="type">The type.</param>
		public TaskAccessRule(string identity, TaskRights eventRights, AccessControlType type)
			: this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		internal TaskAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Properties
		/// <summary>
		/// Gets the task rights.
		/// </summary>
		/// <value>
		/// The task rights.
		/// </value>
		public TaskRights TaskRights
		{
			get
			{
				return (TaskRights)base.AccessMask;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public sealed class TaskAuditRule : AuditRule
	{
		// Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskAuditRule"/> class.
		/// </summary>
		/// <param name="identity">The identity.</param>
		/// <param name="eventRights">The event rights.</param>
		/// <param name="flags">The flags.</param>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public TaskAuditRule(IdentityReference identity, TaskRights eventRights, AuditFlags flags)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		internal TaskAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Properties
		/// <summary>
		/// Gets the task rights.
		/// </summary>
		/// <value>
		/// The task rights.
		/// </value>
		public TaskRights TaskRights
		{
			get
			{
				return (TaskRights)base.AccessMask;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public sealed class TaskSecurity : NativeObjectSecurity
	{
		// Methods
		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSecurity"/> class.
		/// </summary>
		public TaskSecurity() : base(true, ResourceType.ProviderDefined)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSecurity"/> class.
		/// </summary>
		/// <param name="task">The task.</param>
		public TaskSecurity(Microsoft.Win32.TaskScheduler.Task task)
			: this(task.Name, AccessControlSections.Access | AccessControlSections.Group | AccessControlSections.Owner)
		{
			this.SetSecurityDescriptorSddlForm(task.GetSecurityDescriptorSddlForm(Microsoft.Win32.TaskScheduler.TaskSecurityDescriptorSections.All));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskSecurity"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="includeSections">The include sections.</param>
		[SecuritySafeCritical]
		public TaskSecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(TaskSecurity._HandleErrorCode), null)
		{
		}

		[SecurityCritical]
		internal TaskSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(TaskSecurity._HandleErrorCode), null)
		{
		}

		// Properties
		/// <summary>
		/// Gets the <see cref="T:System.Type" /> of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.
		/// </summary>
		/// <returns>The type of the securable object associated with this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		public override Type AccessRightType
		{
			get { return typeof(TaskRights); }
		}

		/// <summary>
		/// Gets the <see cref="T:System.Type" /> of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.
		/// </summary>
		/// <returns>The type of the object associated with the access rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		public override Type AccessRuleType
		{
			get { return typeof(TaskAccessRule); }
		}

		/// <summary>
		/// Gets the <see cref="T:System.Type" /> object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object. The <see cref="T:System.Type" /> object must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.
		/// </summary>
		/// <returns>The type of the object associated with the audit rules of this <see cref="T:System.Security.AccessControl.ObjectSecurity" /> object.</returns>
		public override Type AuditRuleType
		{
			get { return typeof(TaskAuditRule); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.
		/// </summary>
		/// <param name="identityReference">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		/// <returns>
		/// The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.
		/// </returns>
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new TaskAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		/// <summary>
		/// Adds the access rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void AddAccessRule(TaskAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		/// <summary>
		/// Adds the audit rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void AddAuditRule(TaskAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.
		/// </summary>
		/// <param name="identityReference">The identity to which the audit rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the audit rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited audit rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">Specifies the conditions for which the rule is audited.</param>
		/// <returns>
		/// The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.
		/// </returns>
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new TaskAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		/// <summary>
		/// Removes the access rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <returns></returns>
		public bool RemoveAccessRule(TaskAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		/// <summary>
		/// Removes the access rule all.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void RemoveAccessRuleAll(TaskAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		/// <summary>
		/// Removes the access rule specific.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void RemoveAccessRuleSpecific(TaskAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		/// <summary>
		/// Removes the audit rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <returns></returns>
		public bool RemoveAuditRule(TaskAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		/// <summary>
		/// Removes the audit rule all.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void RemoveAuditRuleAll(TaskAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		/// <summary>
		/// Removes the audit rule specific.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void RemoveAuditRuleSpecific(TaskAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		/// <summary>
		/// Resets the access rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void ResetAccessRule(TaskAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		/// <summary>
		/// Sets the access rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void SetAccessRule(TaskAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		/// <summary>
		/// Sets the audit rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		public void SetAuditRule(TaskAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		internal AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections none = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				none = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				none |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				none |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				none |= AccessControlSections.Group;
			}
			return none;
		}

		[SecurityCritical]
		internal void Persist(SafeWaitHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					bool flag;
					bool flag2;
					base.Persist(handle, accessControlSectionsFromChanges);
					base.AccessRulesModified = flag = false;
					base.AuditRulesModified = flag2 = flag;
					base.OwnerModified = base.GroupModified = flag2;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception exception = null;
			int num = errorCode;
			if (((num != 2) && (num != 6)) && (num != 0x7b))
			{
				return exception;
			}
			if ((name != null) && (name.Length != 0))
			{
				return new WaitHandleCannotBeOpenedException("WaitHandle cannot be opened: Invalid Handle");
			}
			return new WaitHandleCannotBeOpenedException();
		}
	}
}