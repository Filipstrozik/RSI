using Grpc.Core;
using GrpcGreeter;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Witaj " + request.Name + "!\n" +(DateTime.UtcNow).ToString("dd MMM, HH:mm:ss")
            });
        }

        public override Task<DistanceReply> Distance(DistanceRequest request, ServerCallContext context)
        {
            double R = 6371; // Earth's radius in km

            double lat1 = Math.PI * request.P1.Latitude / 180.0;
            double lat2 = Math.PI * request.P2.Latitude / 180.0;
            double lon1 = Math.PI * request.P1.Longitude / 180.0;
            double lon2 = Math.PI * request.P2.Longitude / 180.0;

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;

            return Task.FromResult(new DistanceReply { Distance = distance });
        }
    }
}