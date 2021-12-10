using Altkom.Shopper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.Hubs
{
    // [Authorize]
    public class CustomersHub : Hub
    {
        private readonly ILogger<CustomersHub> logger;

        public CustomersHub(ILogger<CustomersHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {            

            

            logger.LogInformation(Context.ConnectionId);

            this.Groups.AddToGroupAsync(Context.ConnectionId, "GroupA");

            return base.OnConnectedAsync();
        }      

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("YouHaveGotMessage", message);
        }

        public async Task SendAddedCustomer(Customer customer)
        {
            await Clients.Others.SendAsync("AddedCustomer", customer);

            // await Clients.Group("GrupaA").SendAsync("AddedCustomer", customer);
        }

        // All = Others + Caller

        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
