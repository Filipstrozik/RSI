﻿using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "192.168.0.132", Port = 5672, UserName = "guest", Password = "guest" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "",
                     routingKey: "hello",
                     basicProperties: null,
                     body: body);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();