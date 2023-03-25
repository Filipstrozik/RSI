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
            using (var fileStream = File.Create("received.png"))
            {
                // Read data from the client stream and write it to the file stream
                while (await requestStream.MoveNext())
                {
                    ImageData imageData = requestStream.Current;
                    await fileStream.WriteAsync(imageData.Data.ToByteArray());
                }
            }

            // Return an Empty response to the client
            return new Empty();
        }

        public override async Task StreamToClient(Empty request, IServerStreamWriter<ImageData> responseStream, ServerCallContext context)
        {
            // Send a test image to the client
            byte[] testImageData = File.ReadAllBytes("test.png");
            await responseStream.WriteAsync(new ImageData { Data = Google.Protobuf.ByteString.CopyFrom(testImageData) });

            // You can also stream multiple images here by repeatedly calling WriteAsync on responseStream
        }
    }
}