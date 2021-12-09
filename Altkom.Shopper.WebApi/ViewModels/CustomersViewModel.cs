using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.ViewModels
{
    public class CustomersViewModel
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersViewModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void Add(Customer customer)
        {
            // 1.
            customerRepository.Add(customer);

            // 2.
            // TODO: send welcome email

            // 3.
            // TODO: create account 

        }
    }
}
