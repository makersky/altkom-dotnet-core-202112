using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.Tracking.Api.Services
{
    public class MyTrackingService : TrackingService.TrackingServiceBase
    {
        private readonly ILogger<MyTrackingService> logger;

        public MyTrackingService(ILogger<MyTrackingService> logger)
        {
            this.logger = logger;
        }

        public override Task<AddLocationResponse> AddLocation(AddLocationRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.VehicleId} lng: {request.Longitude} lat: {request.Latitude}");

            var response = new AddLocationResponse { IsConfirmed = true };

            return Task.FromResult(response);
        }

        public override async Task SubscribeLocation(
            SubscribeLocationRequest request, 
            IServerStreamWriter<SubscribeLocationResponse> responseStream, 
            ServerCallContext context)
        {

            var responses = new Faker<SubscribeLocationResponse>()
                .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
                .GenerateForever();

            foreach (var response in responses)
            {
                await responseStream.WriteAsync(response);

                logger.LogInformation($"lng: {response.Longitude} lat: {response.Latitude}");

                await Task.Delay(TimeSpan.FromSeconds(10));
            }

            
        }


    }
}
