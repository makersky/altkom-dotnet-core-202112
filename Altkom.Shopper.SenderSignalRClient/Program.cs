using Altkom.Shopper.Fakers;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shopper.SenderSignalRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Sender Signal-R!");

            const string url = "http://localhost:5000/signalr/customers";

            // Install-Package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine($"Connecting to {url}...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            Console.WriteLine("Sending...");
            await connection.SendAsync("SendMessage", "Hello World!");
            Console.WriteLine("Sent.");

            var customerFaker = new CustomerFaker();

            var customers = customerFaker.GenerateForever();

            foreach (var customer in customers)
            {                
                await connection.SendAsync("SendAddedCustomer", customer);

                Console.WriteLine($"Sent {customer.FirstName} {customer.LastName}");

                await Task.Delay(TimeSpan.FromMilliseconds(1));
            }


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();




        }
    }
}
