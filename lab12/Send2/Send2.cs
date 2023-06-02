using System;
using System.Text;
using RabbitMQ.Client;
using System.Threading;

public class Program
{
    private const int DurationSeconds = 10;
    private const string EndMarkerMessage = "EndMarker";


    public static void Main()
    {
        MyData.Info();

        var factory = new ConnectionFactory { HostName = "localhost" };
        // var factory = new ConnectionFactory { HostName = "10.182.36.179", Port = 5672, UserName = "guest", Password = "guest" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        DateTime endTime = DateTime.Now.AddSeconds(DurationSeconds);

        while (DateTime.Now < endTime)
        {
            string message = "Piotr " + DateTime.Now.ToString("h:mm:ss tt");
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


