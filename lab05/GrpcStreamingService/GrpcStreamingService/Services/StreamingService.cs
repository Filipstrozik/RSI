using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcStreamingService;

namespace GrpcStreamingService.Services
{
    class StreamingService: ImageService.ImageServiceBase
    {
        public override async Task<Empty> StreamToServer(IAsyncStreamReader<ImageData> requestStream, ServerCallContext context)
        {
            // Create a file stream to write the received data
            using (var fileStream = File.Create(@"Files\received.png"))
            {
                // Read data from the client stream and write it to the file stream
                while (await requestStream.MoveNext())
                {
                    ImageData imageData = requestStream.Current;
                    await fileStream.WriteAsync(imageData.Data.ToByteArray());

                    Console.WriteLine(imageData);
                    Thread.Sleep(100);
                }
            }

            // Return an Empty response to the client
            return new Empty();
        }

        public override async Task StreamToClient(Empty request, IServerStreamWriter<ImageData> responseStream, ServerCallContext context)
        {
            using (var fileStream = File.OpenRead(@"Files\received.png"))
            {
                // Create a buffer to hold the data read from the file
                byte[] buffer = new byte[256]; // 256 bytes buffer size

                // Read the file in chunks and write each chunk to the response stream
                int bytesRead;
                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    // Create a new ImageData message and set its data field to the chunk of file data
                    var imageData = new ImageData { Data = ByteString.CopyFrom(buffer, 0, bytesRead) };

                    // Write the ImageData message to the response stream
                    await responseStream.WriteAsync(imageData);
                    Console.WriteLine(imageData);
                    Thread.Sleep(100);
                }
            }
        }
    }
}