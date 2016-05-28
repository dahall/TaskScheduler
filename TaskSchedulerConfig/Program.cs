using System.Windows.Forms;

namespace TaskSchedulerConfig
{
	class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WizardForm());
		}
	}
}
