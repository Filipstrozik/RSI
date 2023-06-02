using System.Text;
using RabbitMQ.Client;

MyData.Info();

var factory = new ConnectionFactory { HostName = "10.182.36.179", Port = 5672, UserName = "guest", Password = "guest" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();



for (int i = 0; i < 10; i++)
{
    string message = "Filip " + i;
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "",
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);

    Console.WriteLine($" [x] Sent {message}");

    // random sleep between 1.5 and 2.5 seconds
    Random rnd = new Random();
    int sleep = rnd.Next(1500, 2500);
    Thread.Sleep(sleep);
}


// channel.QueueDeclare(queue: "hello",
//                      durable: false,
//                      exclusive: false,
//                      autoDelete: false,
//                      arguments: null);

// string message = "Hello World!";
// var body = Encoding.UTF8.GetBytes(message);

// channel.BasicPublish(exchange: "",
//                      routingKey: "hello",
//                      basicProperties: null,
//                      body: body);

// Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();