using System;
using System.EnterpriseServices;
using System.Runtime.InteropServices;

namespace RichTaskActionHandler
{
	[ObjectPooling(MinPoolSize = 2, MaxPoolSize = 10, CreationTimeout = 20)]
	[Transaction(TransactionOption.Required)]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("A846D159-BE7D-49C6-9381-92102AEEA9FA")]
	public class RichTaskActionHandler : COMTask.TaskHandlerBase
    {
		public override void Start(string data)
		{
		}

		public override int Stop()
		{
			return 0;
		}

		public override void Pause()
		{
		}

		public override void Resume()
		{
		}
    }
}
