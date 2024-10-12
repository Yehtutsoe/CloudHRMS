using System.Net;
using System.Net.Sockets;

namespace CloudHRMS.Utility.Network
{
	public static class NetworkHelper
	{
		public static string GetMechinePublicIP()
		{
			string ip = "127.0.0.1";
			try
			{
				ip =new System.Net.WebClient().DownloadString("https://api.ipify.org");

			}catch(Exception e)
			{
				ip=GetLocalIPAddress();	
			}

			return ip;
		}

		public static string GetLocalIPAddress()
		{
			
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach( var ip in host.AddressList)
			{
				if(ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No Network adapter");
		}
	}
}
