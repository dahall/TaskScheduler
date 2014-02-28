using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace SecurityEditor
{
	internal class AccessRights
	{
		private static Dictionary<string, List<KeyValuePair<object, string>>> cache = new Dictionary<string, List<KeyValuePair<object, string>>>();
		private static Dictionary<string, Array> knownRights = new Dictionary<string, Array>();

		class ARBlend
		{
			public object Rights;
			public string ResourceName;

			public ARBlend(object rights, string resName)
			{
				Rights = rights; ResourceName = resName;
			}
		}

		static AccessRights()
		{
			knownRights.Add(GetKey(typeof(FileSystemRights)), new object[] { FileSystemRights.FullControl, FileSystemRights.Modify, FileSystemRights.ReadAndExecute, FileSystemRights.Read, FileSystemRights.Write, null });
			knownRights.Add(GetKey(typeof(FileSystemRights), true), new FileSystemRights[] { FileSystemRights.FullControl, FileSystemRights.ExecuteFile, FileSystemRights.ReadData, FileSystemRights.ReadAttributes,
				FileSystemRights.ReadExtendedAttributes, FileSystemRights.CreateFiles, FileSystemRights.CreateDirectories, FileSystemRights.WriteAttributes, FileSystemRights.WriteExtendedAttributes,
				FileSystemRights.Delete, FileSystemRights.ReadPermissions, FileSystemRights.ChangePermissions, FileSystemRights.TakeOwnership });
			knownRights.Add(GetKey(typeof(FileSystemRights)) + "2", new object[] { FileSystemRights.FullControl, FileSystemRights.Modify, new ARBlend(FileSystemRights.ReadAndExecute | FileSystemRights.Write, "FileSystemRightsReadWriteExecute"), FileSystemRights.ReadAndExecute, FileSystemRights.Read, FileSystemRights.Write });
			knownRights.Add(GetKey(typeof(RegistryRights)), new object[] { RegistryRights.FullControl, RegistryRights.ReadKey, null });
			knownRights.Add(GetKey(typeof(RegistryRights), true), new RegistryRights[] { RegistryRights.FullControl, RegistryRights.QueryValues, RegistryRights.SetValue, RegistryRights.CreateSubKey,
				RegistryRights.EnumerateSubKeys, RegistryRights.Notify, RegistryRights.CreateLink, RegistryRights.Delete, RegistryRights.ChangePermissions, RegistryRights.TakeOwnership,
				RegistryRights.ReadPermissions });
		}

		public static string GetLocalizedString(object obj)
		{
			string key = obj.ToString();
			string[] keys;
			string prefix = string.Empty;
			if (obj is Enum)
			{
				keys = key.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
				prefix = obj.GetType().Name;
			}
			else
				keys = new string[] { key };
			for (int i = 0; i < keys.Length; i++)
			{
				try
				{
					string ret = Properties.Resources.ResourceManager.GetString(prefix + keys[i], System.Globalization.CultureInfo.CurrentUICulture);
					if (!string.IsNullOrEmpty(ret))
						keys[i] = ret;
				}
				catch { }
			}
			return string.Join(", ", keys);
		}

		public static string GetLocalizedInheritanceString(AuthorizationRule rule)
		{
			return GetLocalizedInheritanceString(rule.GetType(), rule.PropagationFlags, rule.InheritanceFlags);
		}

		public static string GetLocalizedInheritanceString(Type ruleType, PropagationFlags pFlags, InheritanceFlags iFlags)
		{
			var sb = new System.Text.StringBuilder();
			sb.Append(ruleType.Name.Replace("AccessRule", "").Replace("AuditRule", ""));
			if ((pFlags & PropagationFlags.InheritOnly) != 0) sb.Append("IO");
			if ((iFlags & InheritanceFlags.ContainerInherit) != 0) sb.Append("CI");
			if ((iFlags & InheritanceFlags.ObjectInherit) != 0) sb.Append("OI");
			return AccessRights.GetLocalizedString(sb.ToString());
		}

		public static string GetLocalizedAccessString(object obj)
		{
			Array l;
			if (knownRights.TryGetValue(GetKey(obj.GetType()) + "2", out l))
			{
				bool found = false;
				foreach (var item in l)
				{
					var right = (item is ARBlend) ? ((ARBlend)item).Rights : item;
					if (Enum.Equals(right, obj))
					{
						found = true;
						if (item is ARBlend) obj = ((ARBlend)item).ResourceName;
						break;
					}
				}
				if (!found)
					obj = "Special";
			}
			return GetLocalizedString(obj);
		}

		public static List<KeyValuePair<object, string>> GetValues(Type T, bool advanced = false)
		{
			List<KeyValuePair<object, string>> ret;
			string dKey = GetKey(T, advanced);
			if (!cache.TryGetValue(dKey, out ret))
			{
				ret = new List<KeyValuePair<object, string>>();
				Array l;
				if (!knownRights.TryGetValue(dKey, out l))
				{
					l = Enum.GetValues(T);
					Array.Sort(l);
					Array.Reverse(l);
				}
				for (int i = 0; i < l.Length; i++)
				{
					var value = l.GetValue(i);
					if (value == null)
						ret.Add(new KeyValuePair<object, string>(null, Properties.Resources.ResourceManager.GetString("SpecialPermissions")));
					else
						ret.Add(new KeyValuePair<object, string>(value, GetLocalizedString(value)));
				}
				cache.Add(dKey, ret);
			}
			return ret;
		}

		internal static void AddTaskRights()
		{
			if (!knownRights.ContainsKey(GetKey(typeof(Microsoft.Win32.TaskScheduler.TaskRights))))
			{
				knownRights.Add(GetKey(typeof(Microsoft.Win32.TaskScheduler.TaskRights)), new object[] { Microsoft.Win32.TaskScheduler.TaskRights.FullControl, Microsoft.Win32.TaskScheduler.TaskRights.Read, Microsoft.Win32.TaskScheduler.TaskRights.Write, null });
				knownRights.Add(GetKey(typeof(Microsoft.Win32.TaskScheduler.TaskRights), true), new object[] { Microsoft.Win32.TaskScheduler.TaskRights.FullControl, Microsoft.Win32.TaskScheduler.TaskRights.Synchronize,
					Microsoft.Win32.TaskScheduler.TaskRights.TakeOwnership, Microsoft.Win32.TaskScheduler.TaskRights.ChangePermissions, Microsoft.Win32.TaskScheduler.TaskRights.ReadPermissions,
					Microsoft.Win32.TaskScheduler.TaskRights.Delete, Microsoft.Win32.TaskScheduler.TaskRights.Unknown100, Microsoft.Win32.TaskScheduler.TaskRights.Unknown080,
					Microsoft.Win32.TaskScheduler.TaskRights.Unknown040, Microsoft.Win32.TaskScheduler.TaskRights.Unknown020, Microsoft.Win32.TaskScheduler.TaskRights.Unknown010,
					Microsoft.Win32.TaskScheduler.TaskRights.Unknown008, Microsoft.Win32.TaskScheduler.TaskRights.Unknown004, Microsoft.Win32.TaskScheduler.TaskRights.Unknown002,
					Microsoft.Win32.TaskScheduler.TaskRights.Unknown001 });
			}
		}

		private static string GetKey(Type T, bool advanced = false)
		{
			return T.Name + (advanced ? "Adv" : "");
		}
	}
}