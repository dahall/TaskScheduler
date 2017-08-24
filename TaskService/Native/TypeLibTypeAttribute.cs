#if NETSTANDARD
using System;

namespace Microsoft.Win32.TaskScheduler.Native
{
    public class TypeLibTypeAttribute : Attribute
    {
        public TypeLibTypeAttribute(short v)
        {
        }
    }
}
#endif