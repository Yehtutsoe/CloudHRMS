using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Caching.Memory;

namespace CloudHRMS.Utility.NetworkHelper
{
    public static class NetworkHelper
    {
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(10);

        // Get Public IP of Machine (cached)
        public static string GetMachinePublicIP(IMemoryCache cache)
        {
            string cacheKey = "MachinePublicIP";
            
            // Check if IP address is already cached
            if (!cache.TryGetValue(cacheKey, out string publicIP))
            {
                // If not cached, retrieve Public IP and cache it
                publicIP = "127.0.0.1";
                try
                {
                    publicIP = new WebClient().DownloadString("https://api.ipify.org");
                }
                catch (Exception)
                {
                    publicIP = GetLocalIPAddress();
                }

                // Cache the Public IP for the specified duration
                cache.Set(cacheKey, publicIP, CacheExpiration);
            }

            return publicIP;
        }

        // Get Local IP of Machine
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
