using System.Drawing;
using System.Security.Cryptography;
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

Console.WriteLine("Wybierz jedną z opcji: ");
Console.WriteLine ("1 - Oblicz odległość pomiędzy dwoma punktami P1 i P2: ");
Console.WriteLine("2 - Oblicz odległość między punktami P1 i P3, z przystankiem w punkcie P2");
string choice = Console.ReadLine();


// Read the first point's information from the console
Console.Write("Podaj nazwę pierwszego puktu: ");
string name1 = Console.ReadLine();
Console.Write("Podaj szerokość geograficzną pierwszego punktu: ");
double latitude1 = double.Parse(Console.ReadLine());
Console.Write("Podaj długość geograficzną pierwszego punktu: ");
double longitude1 = double.Parse(Console.ReadLine());

// Read the second point's information from the console
Console.Write("Podaj nazwę drugiego punktu: ");
string name2 = Console.ReadLine();
Console.Write("Podaj szerokość geograficzną drugiego punktu: ");
double latitude2 = double.Parse(Console.ReadLine());
Console.Write("Podaj długość geograficzną drugiego punktu: ");
double longitude2 = double.Parse(Console.ReadLine());


// Send the points' information to the gRPC server to compute the distance

Point p1 = new Point
{
    City = name1,
    Latitude = latitude1,
    Longitude = longitude1
};

Point p2 = new Point
{
    City = name2,
    Latitude = latitude2,
    Longitude = longitude2
};

var distanceReply1 = await client.DistanceAsync(new DistanceRequest
{
    P1 = p1,
    P2 = p2
});


//Calculating additional distance from p2 to p3
if(choice == "2")
{
    Console.Write("Podaj nazwę trzeciego punktu: ");
    string name3 = Console.ReadLine();
    Console.Write("Podaj szerokość geograficzną trzeciego punktu: ");
    double latitude3 = double.Parse(Console.ReadLine());
    Console.Write("Podaj długość geograficzną trzeciego punktu: ");
    double longitude3 = double.Parse(Console.ReadLine());

    Point p3 = new Point
    {
        City = name3,
        Latitude = latitude3,
        Longitude = longitude3
    };

    var distanceReply2 = await client.DistanceAsync(new DistanceRequest
    {
        P1 = p2,
        P2 = p3
    });

    Console.WriteLine($"Dystans między {name1} a {name3} z przystankiem w {name2} wynosi {distanceReply1.Distance + distanceReply2.Distance} km.");
}
else
{
    Console.WriteLine($"Dystans między {name1} a {name2} wynosi {distanceReply1.Distance} km.");
}

//Console.ReadKey();