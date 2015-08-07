using Microsoft.CSharp;
using Microsoft.Win32.TaskScheduler;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TestTaskService
{
	internal partial class ScriptTestDlg : Form
	{
		private CSharpCodeProvider provider;
		private CompilerParameters compilerParams;

		public ScriptTestDlg()
		{
			InitializeComponent();
		}

		public TaskService TaskService { get; set; }

		private void runBtn_Click(object sender, EventArgs e)
		{
			if (provider == null)
			{
				Dictionary<string, string> providerOptions = new Dictionary<string, string> { {"CompilerVersion", "v4.0"} };
				provider = new CSharpCodeProvider(providerOptions);
				compilerParams = new CompilerParameters(new string[] { "System.dll", "System.Xml.dll", Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Microsoft.Win32.TaskScheduler.dll") }) { GenerateInMemory = true, GenerateExecutable = false };
			}

			string code = String.Concat(@"using System; using Microsoft.Win32.TaskScheduler; namespace RuntimeNS { public static class RuntimeRunner { public static void Run(TaskService ts, System.IO.StringWriter output) { ", textBox1.Text, @" } } }");
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, code);
			if (results.Errors.Count != 0)
			{
				string[] strArr = new string[results.Output.Count];
				results.Output.CopyTo(strArr, 0);
				MessageBox.Show(string.Join("\r\n", strArr), "Compiler Errors");
			}
			else
			{
				MethodInfo mi = results.CompiledAssembly.GetType("RuntimeNS.RuntimeRunner").GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
				var sw = new System.IO.StringWriter();
				try { mi.Invoke(null, new object[] { TaskService, sw }); MessageBox.Show(sw.ToString(), "Results"); }
				catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception"); }
			}
		}

		private void closeBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
