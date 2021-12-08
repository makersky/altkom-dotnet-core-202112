using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shopper.FakeRepositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomerRepository()
        {
           
        }

        public void Add(Customer customer)
        {
            int lastId = customers.Max(c => c.Id);

            customer.Id = ++lastId;

            customers.Add(customer);
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
    }
}
