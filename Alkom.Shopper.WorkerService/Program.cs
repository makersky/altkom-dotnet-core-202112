using Altkom.Shopper.Fakers;
using Altkom.Shopper.Models;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkom.Shopper.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }       

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService(options => options.ServiceName = "Shopper Service")  // Install-Package Microsoft.Extensions.Hosting.WindowsServices
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<Faker<Customer>, CustomerFaker>();
                    // services.AddHostedService<Worker>();
                    services.AddHostedService<CustomerWorkerService>();
                });



        // 1. CMD> dotnet publish -r win-x64 -c Release
        // 2. Uruchomiæ CMD jako administrator
        // 3. CMD> sc create ShopperService BinPath = C:\..\Alkom.Shopper.WorkerService.exe
    }
}
