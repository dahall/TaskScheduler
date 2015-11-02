using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System
{
	internal static class EnumUtil
	{
		public static void CheckIsEnum<T>(bool checkHasFlags = false)
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
			if (checkHasFlags && !IsFlags<T>())
				throw new ArgumentException($"Type '{typeof(T).FullName}' doesn't have the 'Flags' attribute");
		}

		public static bool IsFlags<T>() => Attribute.IsDefined(typeof(T), typeof(FlagsAttribute));

		public static void CheckHasValue<T>(T value, string argName = null)
		{
			CheckIsEnum<T>();
			if (IsFlags<T>())
			{
				long allFlags = 0L;
				foreach (T flag in Enum.GetValues(typeof(T)))
					allFlags |= Convert.ToInt64(flag);
				if ((allFlags & Convert.ToInt64(value)) != 0L)
					return;
			}
			else if (Enum.IsDefined(typeof(T), value))
				return;
			throw new InvalidEnumArgumentException(argName == null ? "value" : argName, Convert.ToInt32(value), typeof(T));
		}

		public static bool IsFlagSet<T>(this T flags, T flag) where T : struct, IConvertible
		{
			CheckIsEnum<T>(true);
			long flagValue = Convert.ToInt64(flag);
			return (Convert.ToInt64(flags) & flagValue) == flagValue;
		}

		public static void SetFlags<T>(ref T flags, T flag, bool set = true) where T : struct, IConvertible
		{
			CheckIsEnum<T>(true);
			long flagsValue = Convert.ToInt64(flags);
			long flagValue = Convert.ToInt64(flag);
			if (set)
				flagsValue |= flagValue;
			else
				flagsValue &= (~flagValue);
			flags = (T)Enum.ToObject(typeof(T), flagsValue);
		}

		public static T SetFlags<T>(this T flags, T flag, bool set = true) where T : struct, IConvertible
		{
			T ret = flags;
			SetFlags<T>(ref ret, flag, set);
			return ret;
		}

		public static T ClearFlags<T>(this T flags, T flag) where T : struct, IConvertible => flags.SetFlags(flag, false);

		public static IEnumerable<T> GetFlags<T>(this T value) where T : struct, IConvertible
		{
			CheckIsEnum<T>(true);
			foreach (T flag in Enum.GetValues(typeof(T)))
			{
				if (value.IsFlagSet(flag))
					yield return flag;
			}
		}

		public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct, IConvertible
		{
			CheckIsEnum<T>(true);
			long lValue = 0;
			foreach (T flag in flags)
			{
				long lFlag = Convert.ToInt64(flag);
				lValue |= lFlag;
			}
			return (T)Enum.ToObject(typeof(T), lValue);
		}
		
		public static string GetDescription<T>(this T value) where T : struct, IConvertible
		{
			CheckIsEnum<T>();
			string name = Enum.GetName(typeof(T), value);
			if (name != null)
			{
				FieldInfo field = typeof(T).GetField(name);
				if (field != null)
				{
					DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (attr != null)
					{
						return attr.Description;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object or returns the value of <paramref name="defaultVal"/>. If <paramref name="defaultVal"/> is undefined, it returns the first declared item in the enumerated type.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value"/>.</typeparam>
		/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore case; <c>false</c> to consider case.</param>
		/// <param name="defaultVal">The default value.</param>
		/// <returns>An object of type <typeparamref name="TEnum"/> whose value is represented by value.</returns>
		public static TEnum TryParse<TEnum>(string value, bool ignoreCase = false, TEnum defaultVal = default(TEnum)) where TEnum : struct, IConvertible
		{
			CheckIsEnum<TEnum>();
			try { return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase); } catch { }
			if (!Enum.IsDefined(typeof(TEnum), defaultVal))
			{
				var v = Enum.GetValues(typeof(TEnum));
				if (v != null && v.Length > 0)
					return (TEnum)v.GetValue(0);
			}
			return defaultVal;
		}
	}
}
