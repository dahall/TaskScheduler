using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Win32.TaskScheduler.Tests
{
	[TestFixture]
	public class TestTaskService
	{
		[Test]
		public void TestCtor()
		{
			Assert.That(() => new TaskService(Server, User, Domain, Pwd, true).Dispose(), Throws.Nothing);
		}

		[TestCaseSource(typeof(TestCaseReader), "FromFile", new object[] { @"C:\Temp\ServerConnectionTestCases.txt" })]
		public void TestCtorV1(string Server, string User, string Domain, string Pwd, bool UserIsAdmin, bool ValidSvr, bool ValidCred)
		{
			try
			{
				new TaskService(Server, User, Domain, Pwd, true).Dispose();
			}
			catch (Exception e)
			{
				if (ValidCred && ValidSvr) Assert.Fail(e.ToString());
			}
		}

		[TestCaseSource(typeof(TestCaseReader), "FromFile", new object[] { @"C:\Temp\ServerConnectionTestCases.txt" })]
		public void TestCtorV2(string Server, string User, string Domain, string Pwd, bool UserIsAdmin, bool ValidSvr, bool ValidCred)
		{
			try
			{
				new TaskService(Server, User, Domain, Pwd, false).Dispose();
			}
			catch (Exception e)
			{
				if (ValidCred && ValidSvr) Assert.Fail(e.ToString());
			}
		}

		[Test]
		public void TestInstance()
		{
			Assert.That(TaskService.Instance, Is.Not.Null);
			Assert.That(TaskService.Instance.Connected, Is.True);
		}

		[Test]
		public void TestLibraryVersion()
		{
			switch (Environment.OSVersion.Version.Major)
			{
				case 5:
					Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(1));
					break;
				case 6:
					switch (Environment.OSVersion.Version.Minor)
					{
						case 0:
							Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(2));
							break;
						case 1:
							Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(3));
							break;
						case 2:
							Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(4));
							break;
						case 3:
							var build = uint.Parse(Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "0").ToString());
							if (build < 1703)
								Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(5));
							else
								Assert.That(TaskService.LibraryVersion.Minor, Is.EqualTo(6));
							break;
					}
					break;
			}
		}
	}
}