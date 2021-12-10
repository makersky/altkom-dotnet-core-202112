using Altkom.Shopper.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shopper.ReceiverSignalRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Receiver Signal-R!");

            const string url = "http://localhost:5000/signalr/customers";

            // Install-Package Microsoft.AspNetCore.SignalR.Client

            string token = "your-token";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url, 
                    options => options.Headers.Add("Authorization", $"Bearer {token}" ))
                .WithAutomaticReconnect()
                .Build();

            connection.Reconnecting += Connection_Reconnecting;

            connection.Reconnected += Connection_Reconnected;

            connection.On<string>("YouHaveGotMessage",
                message => Console.WriteLine($"Received {message}"));

            connection.On<Customer>("AddedCustomer",
                customer => Console.WriteLine($"Received {customer.FirstName} {customer.LastName}"));

            Console.WriteLine($"Connecting to {url}...");
            await connection.StartAsync();
            Console.WriteLine("Connected.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();

        }

        private static Task Connection_Reconnected(string arg)
        {
            Console.WriteLine("Reconnected.");

            return Task.CompletedTask;
        }

        private static Task Connection_Reconnecting(Exception arg)
        {
            Console.WriteLine("Reconnecting...");

            return Task.CompletedTask;
        }
    }
}
