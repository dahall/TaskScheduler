using System;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>A generic text/value class.</summary>
	/// <typeparam name="T">Any object type.</typeparam>
	public class TextValueItem<T> : IComparable, IComparable<TextValueItem<T>>, IEquatable<TextValueItem<T>>
	{
		/// <summary>Initializes a new instance of the <see cref="TextValueItem{T}"/> class.</summary>
		/// <param name="value">The value.</param>
		public TextValueItem(T value) : this(value?.ToString() ?? "", value) { }

		/// <summary>Initializes a new instance of the <see cref="TextValueItem{T}"/> class.</summary>
		/// <param name="text">The text.</param>
		/// <param name="value">The value.</param>
		public TextValueItem(string text, T value = default)
		{
			Text = text; Value = value;
		}

		/// <summary>Gets or sets the text.</summary>
		/// <value>The text.</value>
		public string Text { get; set; }

		/// <summary>Gets or sets the value.</summary>
		/// <value>The value.</value>
		public T Value { get; set; }

		/// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			switch (obj)
			{
				case null:
					return false;
				case TextValueItem<T> lci:
					return Equals(lci);
				case string s:
					return Text.Equals(obj);
			}
			return obj.GetType() == Value.GetType() && Value.Equals(obj);
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode() => new { Text, Value }.GetHashCode();

		/// <summary>Compares the current object with another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
		/// </returns>
		public int CompareTo(TextValueItem<T> other) => string.Compare(Text, other?.Text, StringComparison.CurrentCulture);

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		public bool Equals(TextValueItem<T> other)
		{
			if (other == null) return false;
			return Text == other.Text && (Value?.Equals(other.Value) ?? false);
		}

		/// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
		/// <returns>A <see cref="System.String"/> that represents this instance.</returns>
		public override string ToString() => Text;

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
		/// </returns>
		int IComparable.CompareTo(object obj)
		{
			switch (obj)
			{
				case TextValueItem<T> lci:
					return CompareTo(lci);
				case string s:
					return string.Compare(Text, s, StringComparison.CurrentCulture);
				default:
					return 1;
			}
		}
	}
}