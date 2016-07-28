#if !NET_45_OR_GREATER
using System;

namespace System.Collections.Generic
{
	public interface IReadOnlyCollection<T> : IEnumerable<T>
	{
		int Count { get; }
	}

	public interface IReadOnlyList<T> : IReadOnlyCollection<T>
	{
		T this[int index] { get; }
	}
}
#endif