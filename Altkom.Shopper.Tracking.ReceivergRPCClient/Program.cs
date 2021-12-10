using Altkom.Shopper.Tracking.Api;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Threading;
using Grpc.Core;

namespace Altkom.Shopper.Tracking.ReceivergRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Receiver Client!");

            const string url = "http://localhost:5010";            

            var channel = GrpcChannel.ForAddress(url);
            var client = new Tracking.Api.TrackingService.TrackingServiceClient(channel);


            var request = new SubscribeLocationRequest { VehicleId = 100 };

            var responseStream = client.SubscribeLocation(request);


            // < C# 8.0
            //CancellationTokenSource cts = new CancellationTokenSource();

            //while (await responseStream.ResponseStream.MoveNext(cts.Token))
            //{
            //    var message = responseStream.ResponseStream.Current;

            //    Console.WriteLine($"lng: {message.Longitude} lat: {message.Latitude}");
            //}        

            // Od C# 8.0
            // using Grpc.Core
            await foreach (var message in responseStream.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine($"lng: {message.Longitude} lat: {message.Latitude}");
            }


            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();


        }
    }
}
