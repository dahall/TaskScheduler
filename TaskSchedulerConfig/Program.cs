using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using Microsoft.Win32.TaskScheduler;

namespace TaskSchedulerConfig
{
	class Program
	{
		static void Main(string[] args)
		{
			var pArgs = new ProgramArguments();
			if (Parser.ParseArgumentsWithUsage(args, pArgs))
			{
				string svr;
				bool hasV2 = TaskService.Instance.HighestSupportedVersion > new Version(1, 1);

				var localVldr = new Validator(null);

				if (pArgs.report)
				{
					var r = new Reporter(localVldr);
					Console.WriteLine();
					Console.WriteLine($"Configuration report for local computer:");
					Console.WriteLine($"========================================");
					Console.WriteLine();
					Console.WriteLine($"General Information");
					Console.WriteLine($"-------------------");
					ConsoleWriteLines(r.General());
					Console.WriteLine();
					Console.WriteLine($"Version 1 (XP/Server 2000)");
					Console.WriteLine($"--------------------------");
					ConsoleWriteLines(r.V1());
					if (hasV2)
					{
						Console.WriteLine();
						Console.WriteLine($"Version 2 (Vista/2003 and later)");
						Console.WriteLine($"--------------------------------");
						ConsoleWriteLines(r.V2());
					}


					if (!string.IsNullOrEmpty(pArgs.server))
					{
						svr = pArgs.server;
						var rVldr = new Validator(svr);
						Console.WriteLine($"Connecting to {svr}...");

						var rr = new Reporter(rVldr);
						Console.WriteLine();
						Console.WriteLine($"Configuration report for local computer:");
						Console.WriteLine($"========================================");
						Console.WriteLine();
						Console.WriteLine($"General Information");
						Console.WriteLine($"-------------------");
						ConsoleWriteLines(rr.General());
						Console.WriteLine();
						Console.WriteLine($"Version 1 (XP/Server 2000)");
						Console.WriteLine($"--------------------------");
						ConsoleWriteLines(rr.V1());
						if (hasV2)
						{
							Console.WriteLine();
							Console.WriteLine($"Version 2 (Vista/2003 and later)");
							Console.WriteLine($"--------------------------------");
							ConsoleWriteLines(rr.V2());
						}
					}
				}

				if (pArgs.fix)
				{
					Console.WriteLine($"Repairing local computer...");
				}
			}

			Console.ReadKey();
		}

		static void ConsoleWriteLines(string[] lines)
		{
			foreach (string s in lines)
				Console.WriteLine(s);
		}

		class ProgramArguments
		{
			//[Argument(ArgumentType.AtMostOnce, HelpText = "Name of remote server to check. Omitting this value will check the local machine.")]
			public string server = null;

		    //[Argument(ArgumentType.AtMostOnce, HelpText="Specifies that a repair of broken functions should be attempted.", DefaultValue = false)]
		    public bool fix = false;

		    [DefaultArgument(ArgumentType.AtMostOnce, HelpText="Specifies that a report should be shown. If no other options are shown, this is the default.", DefaultValue = true)]
		    public bool report = true;
		}
	}
}
