using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProduzirAPI.Models.Domain;
using ProduzirAPI.Models.DTOs;
using ProduzirAPI.Repositories;

namespace ProduzirAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var products = await _productsRepository.GetAllProductsAsync();

            if (products != null)
            {
                Console.WriteLine("HELLO");
                Console.WriteLine(products.FirstOrDefault());
                return Ok(_mapper.Map<List<ProductDTO>>(products));
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(_mapper.Map<ProductDTO>(product));
            }
            return NotFound();
        }
    }
}