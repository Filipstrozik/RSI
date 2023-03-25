using Grpc.Net.Client;
using GrpcStreamingClient;
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7215");
var client = new ImageService.ImageServiceClient(channel);
// Open the file for reading
using (var fileStream = File.OpenRead("C:\\Users\\filip\\Documents\\Sem6\\RSI\\lab05\\GrpcStreamingClient\\GrpcStreamingClient\\Files\\test.png"))
{
    // Create a stream of ImageData objects
    using (var call = client.StreamToServer())
    {
        // Stream the file data to the server
        byte[] buffer = new byte[4096];
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

// Shutdown the gRPC channel
await channel.ShutdownAsync();