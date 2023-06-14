using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

public class MyData
{
    private readonly ILogger<MyData> _logger;

    public MyData(ILogger<MyData> logger)
    {
        _logger = logger;
    }

    // use Logger to log at info level
    public void Info()
    {
        string log = "";
        log += "Filip Strózik, 260377 \n";
        log += "Piotr Grygoruk, 260299 \n";

        log += DateTime.UtcNow.ToString("dd MMM, HH:mm:ss") + "\n";
        log += Environment.Version.ToString() + "\n";
        log += "OS: " + Environment.OSVersion.ToString() + "\n";

        log += Environment.UserName + "\n";

        try
        {
            log += GetLocalIPAddress() + "\n";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error retrieving local IP address");
            throw;
        }

        _logger.LogInformation(log);
    }

    private string GetLocalIPAddress()
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
