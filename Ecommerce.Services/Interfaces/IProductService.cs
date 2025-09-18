using Ecommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByCodeAsync(string code);
        Task<Product?> AddProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
