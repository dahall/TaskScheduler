namespace System.Windows.Forms
{
	public class CommandLink : Button
	{
		const int BS_COMMANDLINK = 0x0000000E;

		public CommandLink()
		{
			this.FlatStyle = FlatStyle.System;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cParams = base.CreateParams;
				cParams.Style |= BS_COMMANDLINK;
				return cParams;
			}
		}
	}
}
