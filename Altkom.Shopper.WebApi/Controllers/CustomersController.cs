using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Altkom.Shopper.Models.SearchCriterias;
using Altkom.Shopper.WebApi.DTO;
using Microsoft.AspNetCore.JsonPatch;
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

        // DTO (Data Transfer Object)

        // GET api/customers/{id}
        [HttpGet("{id}", Name = nameof(GetCustomerById))]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = customerRepository.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // https://docs.microsoft.com/pl-pl/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraint-reference

        // GET api/customers/{pesel}
        [HttpGet("{pesel:length(11)}")]
        public ActionResult<Customer> GetByPesel(string pesel)
        {
            var customer = customerRepository.Get(pesel);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        private static CustomerDTO Map(Customer customer)
        {
            // Map
            return new CustomerDTO
            {
                Id = customer.Id,
                FullName = $"{customer.FirstName} {customer.LastName}",
                Email = customer.Email,
                Gender = customer.Gender,
                Pesel = customer.Pesel
            };
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

      

        // GET api/customers?Gender={gender}&from={from}&to={to}
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get([FromQuery] CustomerSearchCriteria searchCriteria)
        {
            var customers = customerRepository.Get(searchCriteria);

            return Ok(customers);

        }

        // POST api/customers
        [HttpPost]
        public ActionResult<Customer> Post([FromBody] Customer customer)
        {
            customerRepository.Add(customer);

            // zła praktyka
            // return Created($"https://localhost:5001/api/customers/{customer.Id}", customer);

            // return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);

            return CreatedAtRoute(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Customer customer)
        {
            if (id!=customer.Id)
            {
                return BadRequest();
            }

            customerRepository.Update(customer);

            return NoContent();
        }

        // Install-Package Microsoft.AspNetCore.JsonPatch
        // Content-Type: application/json-patch+json

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<Customer> patchCustomer)
        {
            customerRepository.Update(id, patchCustomer);

            return NoContent();
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            customerRepository.Remove(id);

            return Ok();
        }


    }
}
