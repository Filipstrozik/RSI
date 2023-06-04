using System;
using System.Text;
using RabbitMQ.Client;
using System.Threading;
using Newtonsoft.Json;

public class Program
{
    private const int DurationSeconds = 10;
    private const string EndMarkerMessage = "EndMarker";


    public static void Main()
    {
        MyData.Info();

        // var factory = new ConnectionFactory { HostName = "10.182.36.179", Port = 5672, UserName = "guest", Password = "guest" };
        var factory = new ConnectionFactory { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        DateTime endTime = DateTime.Now.AddSeconds(DurationSeconds);
        int counter = 0;
        while (DateTime.Now < endTime)
        {
            // serialize message to JSON format
            string message = JsonConvert.SerializeObject(new { name = "Piotr", time = DateTime.Now.ToString("hh:mm:ss"), counter = counter++ });

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent {message}");

            // Random sleep between 1 and 1.5 seconds
            Random rnd = new Random();
            int sleep = rnd.Next(1000, 1500);
            Thread.Sleep(sleep);
        }

        var endMarkerBody = Encoding.UTF8.GetBytes(EndMarkerMessage);
        channel.BasicPublish(exchange: "",
                             routingKey: "hello",
                             basicProperties: null,
                             body: endMarkerBody);

        Console.WriteLine($" [x] Sent end marker '{EndMarkerMessage}'");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}


