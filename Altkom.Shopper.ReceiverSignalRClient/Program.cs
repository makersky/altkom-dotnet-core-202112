﻿using Altkom.Shopper.Models;
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

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

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
    }
}
