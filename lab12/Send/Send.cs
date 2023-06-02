using System.Text;
using RabbitMQ.Client;

MyData.Info();

var factory = new ConnectionFactory { HostName = "10.182.36.179", Port = 5672, UserName = "guest", Password = "guest" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

// make a loop that will send 10 messages with a 1 second delay between them (use Thread.Sleep(1000);) set the message as "Send1 " + i where i is the number of the message

for (int i = 0; i < 5; i++)
{
    // add a timestamp to the message

    string message = "Piotr " + i + " " + DateTime.Now.ToString("h:mm:ss tt");
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


//string message = "Hello World!";
//var body = Encoding.UTF8.GetBytes(message);

// channel.BasicPublish(exchange: "",
//                      routingKey: "hello",
//                      basicProperties: null,
//                      body: body);

// Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();