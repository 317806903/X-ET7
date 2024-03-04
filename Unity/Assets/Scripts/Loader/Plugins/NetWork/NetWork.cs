
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using ET;

    public class NetWork
    {
		public static string GetIP()
		{
	#if UNITY_ANDROID && !UNITY_EDITOR
				 return System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
				.AddressList.First(
					f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				.ToString();
	#endif
			string outputIp4 = "";
			string outputIp6 = "";
			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
			{
	#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
				NetworkInterfaceType _type1 = NetworkInterfaceType.Wireless80211;
				NetworkInterfaceType _type2 = NetworkInterfaceType.Ethernet;

				if ((item.NetworkInterfaceType == _type1 || item.NetworkInterfaceType == _type2) && item.OperationalStatus == OperationalStatus.Up)
	#endif
				{
					foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							outputIp4 = ip.Address.ToString();
							Log.Debug("IP4:" + outputIp4);
						}
						else if (ip.Address.AddressFamily == AddressFamily.InterNetworkV6)
						{
							outputIp6 = ip.Address.ToString();
							Log.Debug("IP6:" + outputIp6);
						}
					}
				}
			}

			if (string.IsNullOrEmpty(outputIp4) == false)
			{
				return outputIp4;
			}
			return outputIp6;
		}
	}