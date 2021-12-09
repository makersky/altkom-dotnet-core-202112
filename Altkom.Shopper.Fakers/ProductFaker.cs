using Altkom.Shopper.Models;
using Bogus;

namespace Altkom.Shopper.Fakers
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Color, f => f.Commerce.Color());
            RuleFor(p => p.UnitPrice, f => decimal.Parse(f.Commerce.Price()));
            RuleFor(p => p.Size, f => f.PickRandom<Size>());
        }
    }
}
