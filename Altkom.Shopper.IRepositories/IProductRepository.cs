using Altkom.Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shopper.IRepositories
{

    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> Get(string color);
        IEnumerable<Product> GetByCustomer(int customerId);
    }
}
