#if !NET_35_OR_GREATER
using System;

namespace System.Runtime.Serialization
{
	internal class DataContractAttribute : Attribute
	{
		public string Name { get; set; }
		public string Namespace { get; set; }
	}

	internal class DataMemberAttribute : Attribute
	{
		public string Name { get; set; }
		public string Namespace { get; set; }
	}

	internal class EnumMemberAttribute : Attribute
	{
		public string Name { get; set; }
		public string Namespace { get; set; }
	}

	internal class CollectionDataContractAttribute : Attribute
	{
		public string Name { get; set; }
		public string Namespace { get; set; }
	}
}
#endif