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

        
    }
}
