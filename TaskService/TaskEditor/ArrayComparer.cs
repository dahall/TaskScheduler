using System.Collections.Generic;
namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Compares two arrays to see if the values inside of the array are the same. This is
	/// dependent on the type contained in the array having a valid Equals() override.
	/// </summary>
	internal static class ArrayComparer
	{
		public static IEnumerable<TResult> Cast<TResult>(this System.Collections.IEnumerable source)
		{
			if (source == null)
				throw new System.ArgumentNullException("source");
			if (default(TResult) != null && source is IEnumerable<TResult>)
				return source as IEnumerable<TResult>;
			return CastImpl<TResult>(source);
		}

		private static IEnumerable<TResult> CastImpl<TResult>(System.Collections.IEnumerable source)
		{
			foreach (object item in source)
				yield return (TResult)item;
		}
		
		public static IEnumerable<TResult> OfType<TResult>(this System.Collections.IEnumerable source)
		{
			if (source == null)
				throw new System.ArgumentNullException("source");
			if (default(TResult) != null && source is IEnumerable<TResult>)
				return source as IEnumerable<TResult>;
			return OfTypeImpl<TResult>(source);
		}

		private static IEnumerable<TResult> OfTypeImpl<TResult>(System.Collections.IEnumerable source)
		{
			foreach (object item in source)
				if (item is TResult)
					yield return (TResult)item;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="coll">The coll.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public static int GetItemHashCode<T>(this IEnumerable<T> coll)
		{
			// if non-null array then go into unchecked block to avoid overflow
			if (coll != null)
			{
				unchecked
				{
					int hash = 17;
					// get hash code for all items in array
					foreach (var item in coll)
						hash = hash * 23 + ((item != null) ? item.GetHashCode() : 0);
					return hash;
				}
			}
			// if null, hash code is zero
			return 0;
		}

		/// <summary>
		/// Compares the contents of both arrays to see if they are equal. This depends on <typeparam name="T"/> having a valid override for Equals().
		/// </summary>
		/// <param name="firstArray">The first array to compare.</param>
		/// <param name="secondArray">The second array to compare.</param>
		/// <returns>True if <paramref name="firstArray"/> and <paramref name="secondArray"/> have equal contents.</returns>
		public static bool Equals<T>(this T[] firstArray, T[] secondArray)
		{
			return Equals(firstArray as IList<T>, secondArray as IList<T>);
		}

		/// <summary>
		/// Compares the contents of both lists to see if they are equal. This depends on <typeparam name="T"/> having a valid override for Equals().
		/// </summary>
		/// <param name="list1">The first list to compare.</param>
		/// <param name="list2">The second list to compare.</param>
		/// <returns>True if <paramref name="list1"/> and <paramref name="list2"/> have equal contents.</returns>
		public static bool Equals<T>(this IList<T> list1, IList<T> list2)
		{
			// if same reference or both null, then equality is true
			if (object.ReferenceEquals(list1, list2))
				return true;

			// otherwise, if both arrays have same length, compare all elements
			if (list1 != null && list2 != null && (list1.Count == list2.Count))
			{
				for (int i = 0; i < list1.Count; i++)
				{
					// if any mismatch, not equal
					if (!object.Equals(list1[i], list2[i]))
						return false;
				}
				// if no mismatches, equal
				return true;
			}

			// if we get here, they are not equal
			return false;
		}
	}
}