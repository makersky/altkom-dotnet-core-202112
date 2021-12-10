using Altkom.Shopper.Models;
using Bogus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Altkom.Shopper.WorkerServiceApi.BackgroundServices
{
    public class CustomerWorkerService : BackgroundService
    {
        private readonly ILogger<CustomerWorkerService> logger;
        private readonly Faker<Customer> customerFaker;

        public CustomerWorkerService(
            ILogger<CustomerWorkerService> logger,
            Faker<Customer> customerFaker)
        {
            this.logger = logger;
            this.customerFaker = customerFaker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var customer = customerFaker.Generate();

                logger.LogInformation($"{customer.FirstName} {customer.LastName}");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
