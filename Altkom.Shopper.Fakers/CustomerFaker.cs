using Altkom.Shopper.Models;
using Bogus;
using System;
using Bogus.Extensions.Poland;

namespace Altkom.Shopper.Fakers
{

    // Install-Package Bogus
    // Install-Package Sulmar.Bogus.Extensions.Poland
    public class CustomerFaker : Faker<Customer>
    {
        // snippet: ctor + 2 x Tab
        public CustomerFaker()
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.Pesel, f => f.Person.Pesel());
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.Debit, f => Math.Round( f.Random.Decimal(0, 1000),0));
        }
    }
}
