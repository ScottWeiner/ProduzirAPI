using System;
using System.Collections.Generic;
using System.Globalization;
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

                return Ok(_mapper.Map<List<ProductDTO>>(products));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO createProductDto)
        {
            if (createProductDto != null)
            {
                var classStub = new ProductClass { Id = createProductDto.ClassId };

                var newProduct = new Product
                {
                    Number = createProductDto.Number,
                    Description = createProductDto.Description,
                    Weight = createProductDto.Weight,

                    ProductClass = new ProductClass { Id = createProductDto.ClassId }
                };

                await _productsRepository.CreateProductAsync(newProduct);
                if (newProduct.Id == 0)
                {
                    return BadRequest("New product did not save");
                }

                var productToReturn = _mapper.Map<ProductDTO>(newProduct);

                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, productToReturn);
            }

            return BadRequest("Could not create product. Possible missing info in the request");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(_mapper.Map<ProductDTO>(product));
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO productDto)
        {
            var productToUpdate = await _productsRepository.GetProductByIdAsync(productDto.Id);

            if (productToUpdate == null)
            {
                return BadRequest("That product does not exist");
            }

            productToUpdate.Description = productDto.Description;



            bool success = await _productsRepository.UpdateProductAsync(productToUpdate);

            if (success)
            {
                return Ok("Product updated successfully");
            }

            return BadRequest("Product did not update correctly");


        }
    }
}

