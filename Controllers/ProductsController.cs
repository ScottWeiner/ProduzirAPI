using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromForm] CreateProductDTO createProductDto)
        {
            if (createProductDto != null)
            {

                var classObject = await _productsRepository.GetProductClassById(createProductDto.ClassId);
                if (classObject == null)
                {
                    return BadRequest("Product Class not found");
                }

                Console.WriteLine(JsonSerializer.Serialize(createProductDto));

                var newProduct = new Product
                {
                    Number = createProductDto.Number,
                    Description = createProductDto.Description,
                    Weight = createProductDto.Weight,

                    ProductClass = classObject
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
        public async Task<ActionResult<ProductDTO>> UpdateProduct([FromForm] UpdateProductDTO updateProductDto)
        {
            var productToUpdate = await _productsRepository.GetProductByIdAsync(updateProductDto.Id);

            if (productToUpdate == null)
            {
                return BadRequest("That product does not exist");
            }

            productToUpdate.Description = updateProductDto.Description;
            productToUpdate.Weight = updateProductDto.Weight;
            productToUpdate.ImageUrl = updateProductDto.ImageUrl;

            if (productToUpdate.ProductClass.Id != updateProductDto.ClassId)
            {
                var newClass = await _productsRepository.GetProductClassById(updateProductDto.ClassId);
                if (newClass != null)
                {
                    productToUpdate.ProductClass = newClass;
                }
                //Else, don't change anything about it if we can't find a product_class

            }

            bool success = await _productsRepository.UpdateProductAsync(productToUpdate);

            if (success)
            {
                return Ok("Product updated successfully");
            }

            return BadRequest("Product did not update correctly");


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult?> DeleteProductById(int id)
        {


            var success = await _productsRepository.DeleteProductAsync(id);

            if (success)
            {
                return Ok("product successfully deleted");
            }

            return BadRequest("Can't find that product for deletion!");
        }
    }
}

