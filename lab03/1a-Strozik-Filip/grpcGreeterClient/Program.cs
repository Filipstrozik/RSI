using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcGreeterClient.Services;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://192.168.43.3:5000");
var client = new Greeter.GreeterClient(channel);
MyData.Info();
Console.Write("Podaj imię: ");
string name = Console.ReadLine();
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = name });
Console.WriteLine(reply.Message);
//Console.ReadKey();