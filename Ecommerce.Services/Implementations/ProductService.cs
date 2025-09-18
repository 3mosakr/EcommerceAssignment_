using Ecommerce.DataAccess.Repositories.Interfaces;
using Ecommerce.Entities.Models;
using Ecommerce.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _repo.GetAllAsync();

        public async Task<Product?> GetProductByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Product?> GetProductByCodeAsync(string code) => await _repo.GetByCodeAsync(code);

        public async Task<Product?> AddProductAsync(Product product)
        {
            await _repo.AddAsync(product);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product)
        {
            await _repo.UpdateAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return false;
            await _repo.DeleteAsync(product);
            return true;
        }
    }
}
