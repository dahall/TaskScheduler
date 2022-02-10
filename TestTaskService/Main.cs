using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace TestTaskService
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			Icon = Properties.Resources.TaskScheduler;
			radioButtonList1.SelectedIndex = 0;

			// Add languages to combo
			langCombo.SelectedIndexChanged += langCombo_SelectedIndexChanged;
			langCombo.BeginUpdate();
			langCombo.SelectedIndex = -1;
			foreach (var culture in GetAsmCultures(typeof(Microsoft.Win32.TaskScheduler.TaskEditDialog).Assembly.GetTypes().First(t => t.Name == "Resources")))
				langCombo.Items.Add(culture);
			langCombo.EndUpdate();
			langCombo.SelectedItem = CultureInfo.CurrentUICulture;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Text = $"{Text} - OS: {Environment.OSVersion.Version}, App: {System.Reflection.Assembly.GetEntryAssembly().GetName().Version}";
		}

		private static IEnumerable<CultureInfo> GetAsmCultures(Type type)
		{
			var rm = new ResourceManager(type);
			return CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => !c.Equals(CultureInfo.InvariantCulture) &&
				rm.GetResourceSet(c, true, false) != null);
		}

		private void closeButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void langCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			System.Threading.Thread.CurrentThread.CurrentCulture = langCombo.SelectedItem as CultureInfo;
			System.Threading.Thread.CurrentThread.CurrentUICulture = langCombo.SelectedItem as CultureInfo;
		}

		private void reconnectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			taskServiceConnectDialog1.ShowDialog(this);
		}

		private void runButton_Click(object sender, EventArgs e)
		{
			System.IO.StringWriter output = new System.IO.StringWriter();

			switch (radioButtonList1.SelectedIndex)
			{
				case 0: // Short
					Program.ShortTest(ts, output, textBox2.Text);
					break;
				case 1: // Long
					Program.LongTest(ts, output, textBox2.Text);
					break;
				case 2: // Editor
					Program.EditorTest(ts, output);
					break;
				case 3: // Find action
					Program.FindTaskWithProperty(ts, output, textBox2.Text);
					break;
				case 4: // Wiz
					Program.WizardTest(ts, output);
					break;
				case 5: // MMC
					Program.MMCTest(ts, output);
					break;
				case 6: // Find task
					Program.FindTask(ts, output, textBox2.Text);
					break;
				case 7: // Output XML
					Program.OutputXml(ts, output);
					break;
				case 8: // Output JSON
					Program.OutputJson(ts, output);
					break;
				case 9: // Task Watcher Form
					new TaskWatcherForm().ShowDialog(this);
					break;
				case 10: // Run Code
					new ScriptTestDlg() { TaskService = ts }.ShowDialog(this);
					break;
				case 11: // Fluent
					Program.FluentTest(ts, output);
					break;
				default:
					break;
			}

			textBox1.Text = output.ToString();
		}
	}
}
