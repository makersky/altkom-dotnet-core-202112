using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altkom.Shopper.Models.Validators
{
    // Install-Package FluentValidation
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.UnitPrice).InclusiveBetween(1, 1000);

        }
    }
}
