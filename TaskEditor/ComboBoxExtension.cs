using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Microsoft.Win32.TaskScheduler;

namespace System.Windows.Forms
{
	internal static class ComboBoxExtension
	{
		public static void InitializeFromEnum(IList list, Type enumType, ResourceManager mgr, string prefix, out long allVal, string[] exclude = null)
		{
			list.Clear();
			allVal = 0;
			if (prefix == null) prefix = string.Empty;
			var vals = Enum.GetValues(enumType);
			var names = Enum.GetNames(enumType);
			for (var i = 0; i < vals.Length; i++)
			{
				var val = Convert.ToInt64(vals.GetValue(i));
				if (exclude == null || Array.IndexOf<string>(exclude, names[i]) == -1)
				{
					allVal |= val;
					var text = mgr.GetString(prefix + names[i], System.Globalization.CultureInfo.CurrentUICulture);
					if (string.IsNullOrEmpty(text))
						text = names[i];
					//text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);
					list.Add(new ListControlItem(text, val));
				}
			}
		}

		public static void InitializeFromEnum<T>(IList list, ResourceManager mgr, string prefix, out long allVal, Func<string, T, object> creator = null, string[] exclude = null)
		{
			var enumType = typeof(T);
			if (!enumType.IsEnum)
				throw new ArgumentException("Specified type is not an enumeration.", nameof(enumType));
			if (mgr == null)
				throw new ArgumentNullException(nameof(mgr), "A valid ResourceManager instance must be provided.");
			if (creator == null) creator = (s, t) => new TextValueItem<T>(s, t);
			list.Clear();
			allVal = 0;
			if (prefix == null) prefix = string.Empty;
			var vals = Enum.GetValues(enumType);
			var names = Enum.GetNames(enumType);
			for (var i = 0; i < vals.Length; i++)
			{
				var val = Convert.ToInt64(vals.GetValue(i));
				if (exclude != null && Array.IndexOf(exclude, names[i]) != -1) continue;
				allVal |= val;
				var text = mgr.GetString(prefix + names[i], CultureInfo.CurrentUICulture);
				if (string.IsNullOrEmpty(text))
					text = names[i];
				//text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text);
				list.Add(creator(text, (T)vals.GetValue(i)));
			}
		}

		public static void InitializeFromEnum<T>(this ComboBox ctrl, ResourceManager mgr, out long allVal, string prefix = null, IEnumerable<T> exclude = null)
		{
			ctrl.BeginUpdate();
			InitializeFromEnum<T>(ctrl.Items, mgr, prefix, out allVal, null, exclude?.Select(t => t.ToString()).ToArray());
			ctrl.EndUpdate();
		}

		public static void InitializeFromEnum<T>(this ListBox ctrl, ResourceManager mgr, out long allVal, string prefix = null, IEnumerable<T> exclude = null)
		{
			ctrl.BeginUpdate();
			InitializeFromEnum<T>(ctrl.Items, mgr, prefix, out allVal, null, exclude?.Select(t => t.ToString()).ToArray());
			ctrl.EndUpdate();
		}
	}
}