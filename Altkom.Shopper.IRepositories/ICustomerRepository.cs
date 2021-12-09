using Altkom.Shopper.Models;
using Altkom.Shopper.Models.SearchCriterias;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace Altkom.Shopper.IRepositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {      
        Customer Get(string pesel);

        // zła praktyka:
        // IEnumerable<Customer> Get(Gender? gender, decimal? from, decimal? to);

        // dobra praktyka:
        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);
        bool Exists(string pesel);

       
    }

}
