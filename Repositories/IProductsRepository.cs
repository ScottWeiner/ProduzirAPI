using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProduzirAPI.Models.Domain;

namespace ProduzirAPI.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}