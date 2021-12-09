using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace Altkom.Shopper.FakeRepositories
{
    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        public FakeProductRepository(Faker<Product> faker) : base(faker)
        {
        }

        public IEnumerable<Product> Get(string color)
        {
            return entities.Where(p => p.Color == color);
        }

        public IEnumerable<Product> GetByCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public override void Remove(int id)
        {
            Product product = Get(id);
            product.IsRemoved = true;
        }
    }
}
