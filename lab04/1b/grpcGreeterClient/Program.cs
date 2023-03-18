using System.Drawing;
using System.Threading.Tasks;
using Grpc.Net.Client;
using grpcGreeterClient.Services;
using GrpcGreeterClient;
using Point = GrpcGreeterClient.Point;

// The port number must match the port of the gRPC server.
//using var channel = GrpcChannel.ForAddress("http://192.168.43.3:5000");
using var channel = GrpcChannel.ForAddress("http://localhost:7019");
var client = new Greeter.GreeterClient(channel);
MyData.Info();
Console.Write("Podaj imię: ");
string name = Console.ReadLine();
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = name });
Console.WriteLine(reply.Message);


// Read the first point's information from the console
Console.Write("Enter the name of the first point: ");
string name1 = Console.ReadLine();
Console.Write("Enter the latitude of the first point: ");
double latitude1 = double.Parse(Console.ReadLine());
Console.Write("Enter the longitude of the first point: ");
double longitude1 = double.Parse(Console.ReadLine());

// Read the second point's information from the console
Console.Write("Enter the name of the second point: ");
string name2 = Console.ReadLine();
Console.Write("Enter the latitude of the second point: ");
double latitude2 = double.Parse(Console.ReadLine());
Console.Write("Enter the longitude of the second point: ");
double longitude2 = double.Parse(Console.ReadLine());

// Send the points' information to the gRPC server to compute the distance
var distanceReply = await client.DistanceAsync(new DistanceRequest
{
    P1 = new Point
    {
        City = name1,
        Latitude = latitude1,
        Longitude = longitude1
    },
    P2 = new Point
    {
        City = name2,
        Latitude = latitude2,
        Longitude = longitude2
    }
});

Console.WriteLine($"The distance between {name1} and {name2} is {distanceReply.Distance} km.");

//Console.ReadKey();