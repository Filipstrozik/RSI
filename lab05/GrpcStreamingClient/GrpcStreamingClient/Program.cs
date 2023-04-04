using Grpc.Core;
using Grpc.Net.Client;
using GrpcStreamingClient;
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7215");
var client = new ImageService.ImageServiceClient(channel);
// Open the file for reading

MyData.Info();

Console.WriteLine("Wybierz opcję");
Console.WriteLine("1 - Wysyłanie pliku");
Console.WriteLine("2 - Pobieranie pliku");
string choice = Console.ReadLine();

Console.WriteLine("Podaj scieżkę do pliku");
string filepath = Console.ReadLine();

switch (choice)
{
    case "1":
        try
        {
            using (var fileStream = File.OpenRead(filepath))
            {
                // Create a stream of ImageData objects
                using (var call = client.StreamToServer())
                {
                    // Stream the file data to the server
                    byte[] buffer = new byte[256];
                    int bytesRead;
                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await call.RequestStream.WriteAsync(new ImageData { Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead) });
                    }
                    await call.RequestStream.CompleteAsync();

                    // Wait for the server to finish processing the stream
                    await call.ResponseAsync;
                }
            }
            Console.WriteLine("Plik wysłany poprawnie");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Niepoprawna sciezka do pliku!");   
        }
        
        break;  
    case "2":
        using (var call = client.StreamToClient(new Google.Protobuf.WellKnownTypes.Empty()))
        {
            // Create a file stream to write the received data
            try
            {
                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate))
                {
                    // Read data from the server stream and write it to the file stream
                    while (await call.ResponseStream.MoveNext())
                    {
                        ImageData imageData = call.ResponseStream.Current;
                        await fileStream.WriteAsync(imageData.Data.ToByteArray());
                        Console.WriteLine(imageData);
                    }
                }
                Console.WriteLine("Plik pobrany poprawnie");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Niepoprawna sciezka do pliku!");
            }
        }
        break;
    default:
        Console.WriteLine("Niepoprawny wybór");
        break;
}

// Shutdown the gRPC channel
await channel.ShutdownAsync();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();