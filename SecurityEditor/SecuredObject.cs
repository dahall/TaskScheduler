using System;
using System.Security.AccessControl;

namespace SecurityEditor
{
	/* Derivitives of NativeObjectSecurity: (default is Group|Owner|Access)
		Security												Object.GetSecurityControl()						V	Sec	Nm
		===========												===========================						==	===	===
		System.IO.Pipes.PipeSecurity							System.IO.Pipes.PipeStream						3.5	N
		EventWaitHandleSecurity									System.Threading.EventWaitHandle				2	N
		FileSystemSecurity										System.IO.DirectoryInfo, FileInfo				2	Y	Name
		FileSystemSecurity										System.IO.FileStream							2	N	Name
		MutexSecurity											System.Threading.Mutex							2	N
		ObjectSecurity<T>																						4	N
		RegistrySecurity										System.Win32.RegistryKey						2	Y	Name
		SemaphoreSecurity										System.Threading.Semaphore						2	N
		System.IO.MemoryMappedFiles.MemoryMappedFileSecurity	System.IO.MemoryMappedFiles.MemoryMappedFile	4	N
	*/

	internal class SecuredObject
	{
		public SecuredObject(object knownObject)
		{
			// If just being passed a security object, grab it and stop
			if (knownObject is CommonObjectSecurity)
			{
				ObjectSecurity = knownObject as CommonObjectSecurity;
			}
			else
			{
				// Special handling for Tasks
				if (knownObject.GetType().FullName == "Microsoft.Win32.TaskScheduler.Task" || knownObject.GetType().FullName == "Microsoft.Win32.TaskScheduler.TaskFolder")
				{
					AccessRights.AddTaskRights();

					IsContainer = knownObject.GetType().Name == "TaskFolder";

					var piTS = knownObject.GetType().GetProperty("TaskService", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
					if (piTS == null)
						throw new ArgumentException("Invalid Task type.");
					var piSv = piTS.PropertyType.GetProperty("TargetServer", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
					if (piSv == null)
						throw new ArgumentException("Invalid Task type.");
					TargetServer = (string)piSv.GetValue(piTS.GetValue(knownObject, null), null);
				}

				// Get the security object using the standard "GetAccessControl" method
				System.Reflection.MethodInfo mi = null;
				object[] methodParams = null;
				mi = knownObject.GetType().GetMethod("GetAccessControl", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, System.Reflection.CallingConventions.Any, new Type[] { typeof(AccessControlSections) }, null);
				if (mi != null)
					methodParams = new object[] { AccessControlSections.All };
				else
					mi = knownObject.GetType().GetMethod("GetAccessControl", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, System.Reflection.CallingConventions.Any, Type.EmptyTypes, null);
				if (mi != null)
				{
					try { ObjectSecurity = mi.Invoke(knownObject, methodParams) as CommonObjectSecurity; }
					catch
					{
						if (methodParams != null)
						{
							methodParams = new object[] { AccessControlSections.Access | AccessControlSections.Group | AccessControlSections.Owner };
							try { ObjectSecurity = mi.Invoke(knownObject, methodParams) as CommonObjectSecurity; } catch { }
						}
					}
				}
				else
					throw new ArgumentException("Object must have a GetAccessControl member.");

				// Get the object name using a "Name" property if one exists
				var pi = knownObject.GetType().GetProperty("FullName", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				if (pi == null)
					pi = knownObject.GetType().GetProperty("Name", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				if (pi != null)
					DisplayName = knownObject.ToString();

				// Set the base object
				BaseObject = knownObject;
			}
			this.IsContainer = SecuredObject.IsContainerObject(ObjectSecurity);
			this.MandatoryLabel = new SystemMandatoryLabel(this.ObjectSecurity);
		}

		public static bool IsContainerObject(CommonObjectSecurity sec)
		{
			return !Array.Exists<string>(new string[] { "FileSecurity", "PipeSecurity", "CryptoKeySecurity", "MemoryMappedFileSecurity", "TaskSecurity" }, delegate(string s) { return sec.GetType().Name == s; });
		}

		public SystemMandatoryLabel MandatoryLabel { get; private set; }

		[Flags]
		public enum SystemMandatoryLabelPolicy
		{
			None = 0,
			NoWriteUp = 1,
			NoReadUp = 2,
			NoExecuteUp = 4
		}

		public enum SystemMandatoryLabelLevel
		{
			None = 0,
			Low = 0x1000,
			Medium = 0x2000,
			High = 0x3000
		}

		public class SystemMandatoryLabel
		{
			public SystemMandatoryLabel(CommonObjectSecurity sec)
			{
				Policy = SystemMandatoryLabelPolicy.None;
				Level = SystemMandatoryLabelLevel.None;

				try
				{
					var sd = new RawSecurityDescriptor(sec.GetSecurityDescriptorBinaryForm(), 0);
					if (sd.SystemAcl != null)
					{
						foreach (var ace in sd.SystemAcl)
						{
							if ((int)ace.AceType == 0x11)
							{
								byte[] aceBytes = new byte[ace.BinaryLength];
								ace.GetBinaryForm(aceBytes, 0);
								//_policy = new IntegrityPolicy(aceBytes, 4);
								//_level = new IntegrityLevel(aceBytes, 8);
							}
						}
					}

				}
				catch { }
				/*byte[] saclBinaryForm = new byte[sd.SystemAcl.BinaryLength];
				sd.SystemAcl.GetBinaryForm(saclBinaryForm, 0);
				GenericAce ace = null;
				if (null != saclBinaryForm)
				{
					RawAcl aclRaw = new RawAcl(saclBinaryForm, 0);
					if (0 >= aclRaw.Count) throw new ArgumentException("No ACEs in ACL", "saclBinaryForm");
					ace = aclRaw[0];
					if (Win32.SYSTEM_MANDATORY_LABEL_ACE_TYPE != (int)ace.AceType)
						throw new ArgumentException("No Mandatory Integrity Label in ACL", "saclBinaryForm");
					byte[] aceBytes = new byte[ace.BinaryLength];
					ace.GetBinaryForm(aceBytes, 0);
					_policy = new IntegrityPolicy(aceBytes, 4);
					_level = new IntegrityLevel(aceBytes, 8);
					return;
				}
				throw new ArgumentNullException("saclBinaryForm");*/
			}

			public bool IsSet { get { return Policy != SystemMandatoryLabelPolicy.None && Level != SystemMandatoryLabelLevel.None; } }

			public SystemMandatoryLabelPolicy Policy { get; private set; }

			public SystemMandatoryLabelLevel Level { get; private set; }
		}

		public object BaseObject { get; private set; }

		public string DisplayName { get; set; }

		public bool IsContainer { get; set; }

		public CommonObjectSecurity ObjectSecurity { get; private set; }

		public string TargetServer { get; set; }

		public object GetAccessMask(AuthorizationRule rule)
		{
			return GetAccessMask(this.ObjectSecurity, rule);
		}

		public static object GetAccessMask(CommonObjectSecurity acl, AuthorizationRule rule)
		{
			if (rule.GetType() == acl.AccessRuleType || rule.GetType() == acl.AuditRuleType)
			{
				Type accRightType = acl.AccessRightType;
				foreach (var pi in rule.GetType().GetProperties())
					if (pi.PropertyType == accRightType)
						return Enum.ToObject(accRightType, pi.GetValue(rule, null));
			}
			throw new ArgumentException();
		}

		public void Persist(object newBase = null)
		{
			object obj = (newBase != null) ? newBase : BaseObject;
			if (obj == null)
				throw new ArgumentNullException("Either newBase or BaseObject must not be null.");

			var mi = obj.GetType().GetMethod("SetAccessControl", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null, Type.EmptyTypes, null);
			if (mi == null)
				throw new InvalidOperationException("Either newBase or BaseObject must represent a securable object.");

			mi.Invoke(obj, new object[] { this.ObjectSecurity });
		}
	}
}