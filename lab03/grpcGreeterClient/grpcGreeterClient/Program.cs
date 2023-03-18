using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcGreeterClient.Services;
using GrpcGreeterClient;

MyData.Info();

//using var channel = GrpcChannel.ForAddress("http://172.20.10.2:7019");
using var channel = GrpcChannel.ForAddress("http://localhost:7019");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
        new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greetings: " + reply.Message);
Console.WriteLine("Press any key to exit");
Console.ReadKey();


// See https://aka.ms/new-console-template for more information


/*klient czytamy imie i wysylalmy do serwisu serwis wyswietli Witad <imie> + czas;
serwe startuje i client to mydatainfo();*/