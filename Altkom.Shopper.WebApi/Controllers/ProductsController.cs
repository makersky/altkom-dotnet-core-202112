using Altkom.Shopper.IRepositories;
using Altkom.Shopper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shopper.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(productRepository.Get());
        }


        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            productRepository.Add(product);

            return CreatedAtRoute(new { id = product.Id }, product);
        }
    }
}
