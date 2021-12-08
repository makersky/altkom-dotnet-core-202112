using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }


        [HttpGet("api/ping")]
        public string Ping()
        {
            return "Pong";
        }

        // GET api/customers
        [HttpGet("api/customers")]
        public IEnumerable<Customer> Get()
        {
            var customers = customerRepository.Get();

            return customers;
        }

        // GET api/customers/{id}
        [HttpGet("api/customers/{id}")]
        public Customer Get(int id)
        {
            var customer = customerRepository.Get(id);

            return customer;
        }

    }
}
