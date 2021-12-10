using Altkom.Shopper.Tracking.Api;
using Bogus;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shopper.Tracking.SendergRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5010";

            Console.WriteLine("Hello gRPC Sender Client!");

            var channel = GrpcChannel.ForAddress(url);
            var client = new Tracking.Api.TrackingService.TrackingServiceClient(channel);

            // Install-Package Bogus
            var requests = new Faker<AddLocationRequest>()
                .RuleFor(p => p.VehicleId, f => f.Random.Int())
                .RuleFor(p => p.Longitude, f => (float) f.Address.Longitude())
                .RuleFor(p=>p.Latitude, f=> (float) f.Address.Latitude())
                .GenerateForever();

            foreach (var request in requests)
            {
                Console.WriteLine($"Send {request.VehicleId} lng: {request.Longitude} lat: {request.Latitude}");
                var response = await client.AddLocationAsync(request);
                Console.WriteLine($"Sent {response.IsConfirmed}");

                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            
        }
    }
}
