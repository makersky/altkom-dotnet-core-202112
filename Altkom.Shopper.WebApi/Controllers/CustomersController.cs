using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Altkom.Shopper.Models.SearchCriterias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }


        [HttpGet("/api/ping")]        
        public string Ping()
        {
            return "Pong";
        }

        // GET api/customers
        //[HttpGet]
        //public ActionResult<IEnumerable<Customer>> Get()
        //{
        //    var customers = customerRepository.Get();

        //    return Ok(customers);
        //}

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = customerRepository.Get(id);

            if (customer==null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // [AcceptVerbs("HEAD")]
        [HttpHead("{id}")]
        public ActionResult Head(int id)
        {
            if (!customerRepository.Exists(id))
            {
                return NotFound();
            }

            return Ok();
        }

        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraint-reference

        // GET api/customers/{pesel}
        [HttpGet("{pesel:length(11)}")]
        public ActionResult<Customer> Get(string pesel)
        {
            var customer = customerRepository.Get(pesel);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET api/customers?Gender={gender}&from={from}&to={to}
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get(CustomerSearchCriteria searchCriteria)
        {
            var customers = customerRepository.Get(searchCriteria);

            return Ok(customers);

        }


    }
}
