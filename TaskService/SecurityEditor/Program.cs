using System;
using System.Windows.Forms;

namespace SecurityEditor
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SecurityEditorDialog() { ObjectName = "Test", SecurityDescriptorSddlForm = "D:P(A;;FA;;;BA)(A;;FRFW;;;SY)(A;;FRFX;;;LS)" });
		}
	}
}
