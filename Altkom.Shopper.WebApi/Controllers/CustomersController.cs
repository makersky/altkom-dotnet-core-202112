using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.Controllers
{
    public class CustomersController : ControllerBase
    {
        [HttpGet("api/ping")]
        public string Ping()
        {
            return "Pong";
        }

        // GET api/customers

    }
}
