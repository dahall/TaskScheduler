using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.TaskScheduler
{
	internal static class NetworkListManager
	{
		// Fields
		private static INetworkListManager manager;

		// Methods
		private static IEnumNetworks GetNetworkEnumerator()
		{
			try
			{
				if (Manager != null)
					return Manager.GetNetworks(NpEnumNetworkList.All);
			}
			catch (UnauthorizedAccessException) { }
			catch (ExternalException) { }
			return null;
		}

		public static NetworkProfile[] GetNetworkList()
		{
			System.Collections.Generic.List<NetworkProfile> list = new System.Collections.Generic.List<NetworkProfile>();
			IEnumNetworks networkEnumerator = GetNetworkEnumerator();
			if (networkEnumerator != null)
			{
				try
				{
					foreach (INetwork network in networkEnumerator)
					{
						list.Add(new NetworkProfile(network.GetNetworkId(), network.GetName()));
					}
				}
				catch (COMException) { }
			}
			return list.ToArray();
		}

		private static INetworkListManager Manager
		{
			get
			{
				if (manager == null)
				{
					try
					{
						manager = (INetworkListManager)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("DCB00C01-570F-4A9B-8D69-199FDBA5723B")));
					}
					catch (UnauthorizedAccessException) { }
					catch (ExternalException) { }
				}
				return manager;
			}
		}

		private enum NpEnumNetworkList
		{
			All = 3,
			Connected = 1,
			Disconnected = 2
		}

		[ComImport, Guid("DCB00003-570F-4A9B-8D69-199FDBA5723B"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
		private interface IEnumNetworks : IEnumerable
		{
		}

		[ComImport, InterfaceType(ComInterfaceType.InterfaceIsDual), Guid("DCB00002-570F-4A9B-8D69-199FDBA5723B")]
		private interface INetwork
		{
			string GetName();
			void SetName(string szNetworkNewName);
			string GetDescription();
			void SetDescription(string szDescription);
			Guid GetNetworkId();
		}

		[ComImport, Guid("DCB00000-570F-4A9B-8D69-199FDBA5723B"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
		private interface INetworkListManager
		{
			IEnumNetworks GetNetworks(NpEnumNetworkList flags);
			INetwork GetNetwork(Guid guid);
			void GetNetworkConnections();
			void GetNetworkConnection();
			bool IsConnectedToInternet { get; }
			bool IsConnected { get; }
			void GetConnectivity();
		}

	}
}
