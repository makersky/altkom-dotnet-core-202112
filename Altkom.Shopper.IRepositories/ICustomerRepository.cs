using Altkom.Shopper.Models;
using System;
using System.Collections.Generic;

namespace Altkom.Shopper.IRepositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        Customer Get(string pesel);
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }

}
