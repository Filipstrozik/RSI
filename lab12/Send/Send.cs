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

        

        var factory = new ConnectionFactory { HostName = "10.182.17.252", Port = 5672, UserName = "admin", Password = "admin" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "filip_piotr",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        DateTime endTime = DateTime.Now.AddSeconds(DurationSeconds);
        int counter = 0;
        while (DateTime.Now < endTime)
        {
            // serialize message to JSON format
            string message = JsonConvert.SerializeObject(new { name = "Filip", time = DateTime.Now.TimeOfDay, counter = counter++ });

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "filip_piotr",
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine($" [x] Sent {message}");

            // Random sleep between 1 and 2 seconds
            Random rnd = new Random();
            int sleep = rnd.Next(1000, 2000);
            Thread.Sleep(sleep);
        }

        var endMarkerBody = Encoding.UTF8.GetBytes(EndMarkerMessage);
        channel.BasicPublish(exchange: "",
                             routingKey: "filip_piotr",
                             basicProperties: null,
                             body: endMarkerBody);

        Console.WriteLine($" [x] Sent end marker '{EndMarkerMessage}'");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}


