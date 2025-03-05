using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProduzirAPI.Models.Domain;
using ProduzirAPI.Repositories;

namespace ProduzirAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _productsRepository.GetAllProductsAsync();

            if (products != null)
            {
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            return Ok(id);
        }
    }
}