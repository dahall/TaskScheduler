namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>An item in a <see cref="System.Windows.Forms.ListControl"/>.</summary>
	public class ListControlItem : TextValueItem<object>
	{
		/// <summary>Initializes a new instance of the <see cref="ListControlItem"/> class.</summary>
		/// <param name="value">The value. The text value is the value of the <param name="value"> ToString() method result.</param></param>
		public ListControlItem(object value) : base(value) { }

		/// <summary>Initializes a new instance of the <see cref="ListControlItem"/> class.</summary>
		/// <param name="text">The text.</param>
		/// <param name="value">The value.</param>
		public ListControlItem(string text, object value = null) : base(text, value) { }
	}
}