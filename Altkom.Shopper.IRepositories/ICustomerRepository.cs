using Altkom.Shopper.Models;
using Altkom.Shopper.Models.SearchCriterias;
using System;
using System.Collections.Generic;

namespace Altkom.Shopper.IRepositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        Customer Get(string pesel);

        // zła praktyka:
        // IEnumerable<Customer> Get(Gender? gender, decimal? from, decimal? to);

        // dobra praktyka:
        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);

        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(int id);
    }

}
