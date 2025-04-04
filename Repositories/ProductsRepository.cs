using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProduzirAPI.Data;
using ProduzirAPI.Models.Domain;

namespace ProduzirAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
               .Include(c => c.ProductClass)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(c => c.ProductClass)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;

        }

        public async Task<bool> UpdateProductAsync(Product product)
        {

            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;



        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<ProductClass?> GetProductClassById(int classId)
        {
            return await _context.ProductClasses.FirstOrDefaultAsync(x => x.Id == classId);


        }

    }
}