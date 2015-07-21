using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Win32.TaskScheduler.OptionPanels
{
	/// <summary>
	/// A check box that can wrap its text onto multiple lines as needed.
	/// </summary>
	internal class WrappingCheckBox : System.Windows.Forms.CheckBox
	{
		static Size MaxSize = new Size(Int32.MaxValue, Int32.MaxValue);
		System.Drawing.Size cachedSizeOfOneLineOfText = System.Drawing.Size.Empty;

		public WrappingCheckBox()
		{
			TextChanged += (o, e) => CacheTextSize();
			FontChanged += (o, e) => CacheTextSize();
		}

		private void CacheTextSize()
		{
			cachedSizeOfOneLineOfText = TextRenderer.MeasureText(Text, Font, MaxSize, TextFormatFlags.WordBreak);
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size prefSize = base.GetPreferredSize(proposedSize);
			if ((prefSize.Width > proposedSize.Width) && (!String.IsNullOrEmpty(Text) && !(proposedSize.Width.Equals(Int32.MaxValue) || proposedSize.Height.Equals(Int32.MaxValue))))
			{
				// we have the possiblility of wrapping... back out the single line of text
				Size bordersAndPadding = prefSize - cachedSizeOfOneLineOfText;
				// add back in the text size, subtract baseprefsize.width and 3 from proposed size width so they wrap properly
				Size newConstraints = proposedSize - bordersAndPadding - new Size(3, 0);
				prefSize = bordersAndPadding + TextRenderer.MeasureText(Text, Font, newConstraints, TextFormatFlags.WordBreak);
			}
			return prefSize;
		}
	}

	internal class WrappingRadioButton : System.Windows.Forms.RadioButton
	{
		static Size MaxSize = new Size(Int32.MaxValue, Int32.MaxValue);
		System.Drawing.Size cachedSizeOfOneLineOfText = System.Drawing.Size.Empty;

		public WrappingRadioButton()
		{
			TextChanged += (o, e) => CacheTextSize();
			FontChanged += (o, e) => CacheTextSize();
		}

		private void CacheTextSize()
		{
			if (String.IsNullOrEmpty(Text))
				cachedSizeOfOneLineOfText = System.Drawing.Size.Empty;
			else
				cachedSizeOfOneLineOfText = TextRenderer.MeasureText(Text, Font, MaxSize, TextFormatFlags.WordBreak);
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size prefSize = base.GetPreferredSize(proposedSize);
			if ((prefSize.Width > proposedSize.Width) && (!String.IsNullOrEmpty(Text) && !proposedSize.Width.Equals(Int32.MaxValue) || !proposedSize.Height.Equals(Int32.MaxValue)))
			{
				// we have the possiblility of wrapping... back out the single line of text
				Size bordersAndPadding = prefSize - cachedSizeOfOneLineOfText;
				// add back in the text size, subtract baseprefsize.width and 3 from proposed size width so they wrap properly
				Size newConstraints = proposedSize - bordersAndPadding - new Size(3, 0);
				prefSize = bordersAndPadding + TextRenderer.MeasureText(Text, Font, newConstraints, TextFormatFlags.WordBreak);
			}
			return prefSize;
		}
	}
}