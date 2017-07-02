using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Win32.TaskScheduler
{
	public abstract class UnboundObject<T> : IDisposable, ICloneable, IEquatable<T>, INotifyPropertyChanged
	{
		/// <summary>List of unbound values when working with Actions not associated with a registered task.</summary>
		protected Dictionary<string, object> unboundValues = new Dictionary<string, object>();

		private bool disposedValue = false; // To detect redundant calls

		~UnboundObject()
		{
			Dispose(false);
		}

		/// <summary>Occurs when a property value changes.</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Gets the bindable object.</summary>
		/// <value>The bindable object.</value>
		protected abstract object BindableObject { get; }

		/// <summary>Gets a value indicating whether this instance is bound.</summary>
		/// <value><c>true</c> if this instance is bound; otherwise, <c>false</c>.</value>
		protected virtual bool IsBound => BindableObject != null;

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public abstract object Clone();

		/// <summary>Determines whether the specified <see cref="System.Object"/>, is equal to this instance.</summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
		/// <returns>
		/// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is T)
				return Equals((T)obj);
			return base.Equals(obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.</returns>
		public abstract bool Equals(T other);

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Returns a <see cref="System.String"/> that represents this action.</summary>
		/// <param name="culture">The culture.</param>
		/// <returns>String representation of action.</returns>
		public virtual string ToString(System.Globalization.CultureInfo culture)
		{
			using (new CultureSwitcher(culture))
				return ToString();
		}

		/// <summary>Binds the values.</summary>
		/// <param name="obj">The object.</param>
		protected void BindValues(object obj)
		{
			foreach (string key in unboundValues.Keys)
			{
				try
				{
					object o = unboundValues[key];
					CheckBindValue(key, ref o);
					ReflectionHelper.SetProperty(obj, key, unboundValues[key]);
				}
				catch (System.Reflection.TargetInvocationException tie) { throw tie.InnerException; }
				catch { }
			}
			unboundValues.Clear();
			unboundValues = null;
		}

		/// <summary>Checks the bind value.</summary>
		/// <param name="key">The key.</param>
		/// <param name="o">The o.</param>
		protected virtual void CheckBindValue(string key, ref object o) { }

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing) { }

		protected P GetProperty<P, B>(string propName, P defaultValue = default(P))
		{
			if (IsBound)
				return ReflectionHelper.GetProperty((B)BindableObject, propName, defaultValue);
			return (unboundValues.ContainsKey(propName)) ? (P)unboundValues[propName] : defaultValue;
		}

		protected P GetProperty<P, B, F>(string propName, Func<F, P> conv, P defaultValue = default(P))
		{
			if (IsBound)
				return conv(ReflectionHelper.GetProperty((B)BindableObject, propName, default(F)));
			return (unboundValues.ContainsKey(propName)) ? (P)unboundValues[propName] : defaultValue;
		}

		protected void OnPropertyChanged(string propName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		protected void SetProperty<P, B>(string propName, P value, Predicate<P> handler = null)
		{
			if (IsBound)
				ReflectionHelper.SetProperty((B)BindableObject, propName, value);
			else
			{
				if (handler == null || !handler(value))
				{
					if (Equals(value, default(P)))
						unboundValues.Remove(propName);
					else
						unboundValues[propName] = value;
				}
			}
			OnPropertyChanged(propName);
		}
	}
}