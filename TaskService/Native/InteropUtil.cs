using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Runtime.InteropServices
{
	internal static class InteropUtil
	{
		internal const int cbBuffer = 256;

		public static T ToStructure<T>(IntPtr ptr) => (T)Marshal.PtrToStructure(ptr, typeof(T));

		public static IntPtr StructureToPtr(object value)
		{
			IntPtr ret = Marshal.AllocHGlobal(Marshal.SizeOf(value));
			Marshal.StructureToPtr(value, ret, false);
			return ret;
		}

		public static void AllocString(ref IntPtr ptr, ref uint size)
		{
			FreeString(ref ptr, ref size);
			if (size == 0) size = cbBuffer;
			ptr = Marshal.AllocHGlobal(cbBuffer);
		}

		public static void FreeString(ref IntPtr ptr, ref uint size)
		{
			if (ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
				size = 0;
			}
		}

		public static string GetString(IntPtr pString) => Marshal.PtrToStringUni(pString);

		public static bool SetString(ref IntPtr ptr, ref uint size, string value = null)
		{
			string s = GetString(ptr);
			if (value == string.Empty) value = null;
			if (string.Compare(s, value) != 0)
			{
				FreeString(ref ptr, ref size);
				if (value != null)
				{
					ptr = Marshal.StringToHGlobalUni(value);
					size = (uint)value.Length + 1;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Converts an <see cref="IntPtr"/> that points to a C-style array into a CLI array.
		/// </summary>
		/// <typeparam name="S">Type of native structure used by the C-style array.</typeparam>
		/// <typeparam name="T">Output type for the CLI array. <typeparamref name="S"/> must be able to convert to <typeparamref name="T"/>.</typeparam>
		/// <param name="ptr">The <see cref="IntPtr"/> pointing to the native array.</param>
		/// <param name="count">The number of items in the native array.</param>
		/// <returns>An array of type <typeparamref name="T"/> containing the converted elements of the native array.</returns>
		public static T[] ToArray<S, T>(IntPtr ptr, int count) where S : IConvertible
		{
			IntPtr tempPtr;
			T[] ret = new T[count];
			int stSize = Marshal.SizeOf(typeof(S));
			for (int i = 0; i < count; i++)
			{
				tempPtr = new IntPtr(ptr.ToInt64() + (i * stSize));
				S val = ToStructure<S>(tempPtr);
				ret[i] = (T)Convert.ChangeType(val, typeof(T));
			}
			return ret;
		}

		/// <summary>
		/// Converts an <see cref="IntPtr"/> that points to a C-style array into a CLI array.
		/// </summary>
		/// <typeparam name="T">Type of native structure used by the C-style array.</typeparam>
		/// <param name="ptr">The <see cref="IntPtr"/> pointing to the native array.</param>
		/// <param name="count">The number of items in the native array.</param>
		/// <returns>An array of type <typeparamref name="T"/> containing the elements of the native array.</returns>
		public static T[] ToArray<T>(IntPtr ptr, int count)
		{
			IntPtr tempPtr;
			T[] ret = new T[count];
			int stSize = Marshal.SizeOf(typeof(T));
			for (int i = 0; i < count; i++)
			{
				tempPtr = new IntPtr(ptr.ToInt64() + (i * stSize));
				ret[i] = ToStructure<T>(tempPtr);
			}
			return ret;
		}
	}

	internal class ComEnumerator<T, E> : IEnumerator<T>, IDisposable where E : IEnumerable where T : class
	{
		protected IEnumerator iEnum;
		private Converter<object, T> conv;

		public ComEnumerator()
		{
			iEnum = null;
			conv = DefaultConverter;
		}

		public ComEnumerator(E collection, Converter<object, T> converter = null)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));
			iEnum = collection.GetEnumerator();
			conv = converter == null ? DefaultConverter : converter;
		}

		object IEnumerator.Current => Current;

		public virtual T Current => conv(iEnum?.Current);

		protected bool HasEnum => iEnum != null;

		private T DefaultConverter(object o)
		{
			if (o == null)
				return default(T);
			return (T)Activator.CreateInstance(typeof(T), Reflection.BindingFlags.CreateInstance | Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Public, null, new object[] { o }, null);
		}

		public virtual void Dispose()
		{
			iEnum = null;
		}

		public virtual bool MoveNext() => iEnum?.MoveNext() ?? false;

		public virtual void Reset()
		{
			iEnum?.Reset();
		}
	}
}