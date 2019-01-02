using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.Framework
{
	public class TestCaseReader
	{
		public static IEnumerable FromFile(string path)
		{
			string[] args = null;
			int iname = -1, ireturns = -1;
			foreach (var line in System.IO.File.ReadAllLines(path))
			{
				if (string.IsNullOrEmpty(line)) continue;
				var values = new List<string>(line.Split('\t'));
				if (args == null)
					ProcessHeader(values);
				else
					yield return Make(values);
			}

			void ProcessHeader(List<string> items)
			{
				iname = items.IndexOf("Name");
				ireturns = items.IndexOf("Returns");
				if (iname >= 0) items.RemoveAt(iname);
				if (ireturns >= 0) { ireturns = iname >= 0 && iname < ireturns ? ireturns - 1 : ireturns; items.RemoveAt(ireturns); }
				args = items.ToArray();
			}

			TestCaseData Make(List<string> values)
			{
				string nameVal = null, retVal = null;
				if (iname >= 0) { nameVal = values[iname]; values.RemoveAt(iname); }
				if (ireturns >= 0) { retVal = values[ireturns]; values.RemoveAt(ireturns); }
				var objs = values.Cast<object>().ToArray();
				for (var i = 0; i < objs.Length; i++)
					if (objs[i] is string s) { if (s == "") objs[i] = null; else if (s == "TRUE") objs[i] = false; else if (s == "FALSE") objs[i] = false; }
				var tcd = new TestCaseData(objs);
				if (!(nameVal is null)) tcd.SetName(nameVal);
				if (!(retVal is null)) tcd.Returns(retVal);
				//return nameVal is null ? tcd.SetArgDisplayNames(args) : tcd;
				return tcd;
			}
		}
	}
}