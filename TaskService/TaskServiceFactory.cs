using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Win32.TaskScheduler
{
	internal enum TaskServiceEngine
	{
		WinV1,
		WinV2
	}

	internal static class TaskServiceFactory
	{
		private class Data
		{
			public TaskServiceEngine engine;
			public Type type;
		}

		private static List<Data> data = new List<Data>();

		public static void RegisterType(TaskServiceEngine eng, Type type)
		{
			data.Add(new Data { engine = eng, type = type });
		}

		public static T GetInstance<T>(TaskServiceEngine eng)
		{
			T ret = (T)Activator.CreateInstance(data.First(d => d.engine == eng && d.type is T).type);
			return ret;
		}
	}
}
