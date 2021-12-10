using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Altkom.Shopper.Models.SearchCriterias;
using Bogus;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shopper.FakeRepositories
{
    public class FakeCustomerRepositoryOptions
    {
        public int Quantity { get; set; }
    }

    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerRepository(Faker<Customer> faker, IOptions<FakeCustomerRepositoryOptions> options)
        {
            customers = faker.Generate(options.Value.Quantity);
        }

        public void Add(Customer customer)
        {
            int lastId = customers.Max(c => c.Id);

            customer.Id = ++lastId;

            customers.Add(customer);
        }

        public bool Exists(int id)
        {
            return customers.Any(c => c.Id == id);
        }

        public bool Exists(string pesel)
        {
            return customers.Any(c => c.Pesel == pesel);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer Get(int id)
        {
            return customers.SingleOrDefault(c => c.Id == id);
        }

        public Customer Get(string pesel)
        {
            return customers.SingleOrDefault(c => c.Pesel == pesel);
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> query = customers.AsQueryable();

            if (searchCriteria.Gender.HasValue)
            {
                query = query.Where(c => c.Gender == searchCriteria.Gender);
            }

            if (searchCriteria.From.HasValue)
            {
                query = query.Where(c => c.Debit >= searchCriteria.From);
            }

            if (searchCriteria.To.HasValue)
            {
                query = query.Where(c => c.Debit <= searchCriteria.To);
            }

            return query.ToList();            

        }

        public void Remove(int id)
        {
            customers.Remove(Get(id));
        }

        public void Update(Customer customer)
        {
            Customer oldCustomer = Get(customer.Id);
            oldCustomer.FirstName = customer.FirstName;
            oldCustomer.LastName = customer.LastName;
            oldCustomer.Pesel = customer.Pesel;
            oldCustomer.Email = customer.Email;
        }

        public void Update(int id, JsonPatchDocument<Customer> patchCustomer)
        {
            Customer oldCustomer = Get(id);

            patchCustomer.ApplyTo(oldCustomer);
        }
    }
}
