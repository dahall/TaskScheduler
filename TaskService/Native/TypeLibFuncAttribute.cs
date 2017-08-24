#if NETSTANDARD
using System;

namespace Microsoft.Win32.TaskScheduler.Native
{
    public class TypeLibFuncAttribute : Attribute
    {
        public TypeLibFuncAttribute(short v)
        {
            
        }
    }
}
#endif