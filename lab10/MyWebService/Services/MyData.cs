using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace MyWebService
{
    public class MyData
    {
        public static void Info()
        {
            Debug.Print("Filip Strózik, 260377");
            Debug.Print("Piotr Grygoruk, 260299");
            Debug.Print((DateTime.UtcNow).ToString("dd MMM, HH:mm:ss"));
            Debug.Print(Environment.Version.ToString());
            Debug.Print(Environment.UserName);
            Debug.Print(Environment.OSVersion.ToString());

            try
            {
                Debug.Print(GetLocalIPAddress());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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