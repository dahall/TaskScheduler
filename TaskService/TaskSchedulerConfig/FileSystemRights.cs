using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;

namespace System.IO
{
	internal static class FileSystemInfoExtensions
	{
		/// <summary>Determines the effective access rights for the given user.</summary>
		/// <param name="path">The directory.</param>
		/// <param name="windowsIdentity">The windows identity.</param>
		/// <returns>Effective rights for identity.</returns>
		public static FileSystemRights GetEffectiveRights(this DirectoryInfo path, WindowsIdentity windowsIdentity) =>
			GetEffectiveRights<FileSystemRights, FileSystemAccessRule>(path.GetAccessControl(), windowsIdentity);

		/// <summary>Determines the effective access rights for the given user.</summary>
		/// <param name="path">The file.</param>
		/// <param name="windowsIdentity">The windows identity.</param>
		/// <returns>Effective rights for identity.</returns>
		public static FileSystemRights GetEffectiveRights(this FileInfo path, WindowsIdentity windowsIdentity) =>
			GetEffectiveRights<FileSystemRights, FileSystemAccessRule>(path.GetAccessControl(), windowsIdentity);

		private static T GetEffectiveRights<T, R>(CommonObjectSecurity securityObject, WindowsIdentity windowsIdentity) where T : struct, IConvertible where R : AccessRule
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
			if (windowsIdentity == null)
				throw new ArgumentNullException(nameof(windowsIdentity));
			if (securityObject == null)
				throw new ArgumentNullException(nameof(securityObject));

			int denyRights = 0, allowRights = 0;

			// get all access rules for the path - this works for a directory path as well as a file path
			AuthorizationRuleCollection authorizationRules = securityObject.GetAccessRules(true, true, typeof(SecurityIdentifier));

			// get the user's sids
			List<string> sids = new List<string>(windowsIdentity.Groups.Select(g => g.Value));
			sids.Insert(0, windowsIdentity.User.Value);

			// get the access rules filtered by the user's sids
			var rules = (from rule in authorizationRules.Cast<R>()
						 where sids.Contains(rule.IdentityReference.Value)
						 select rule);

			System.Reflection.PropertyInfo pi = null;
			foreach (var rule in rules)
			{
				if (pi == null)
					pi = rule.GetType().GetProperty("AccessMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, typeof(int), Type.EmptyTypes, null);
				if (pi == null)
					throw new InvalidOperationException("Unable to retrieve access mask.");
				if (rule.AccessControlType == AccessControlType.Deny)
					denyRights |= (int)pi.GetValue(rule, null);
				else
					allowRights |= (int)pi.GetValue(rule, null);
			}

			return (T)Enum.ToObject(typeof(T), (allowRights | denyRights) ^ denyRights);
		}
	}
}
