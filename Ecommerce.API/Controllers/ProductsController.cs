using Ecommerce.Entities.Models;
using Ecommerce.Services.Dto.Product;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, IImageService imageService, ILogger<ProductsController> logger)
        {
            _productService = service;
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new ApiResponse<IEnumerable<Product>>(products, "All products retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<string>("Product not found"));

            return Ok(new ApiResponse<Product>(product, "Product retrieved successfully"));
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var product = await _productService.GetProductByCodeAsync(code);
            if (product == null) return NotFound(new ApiResponse<string>("Product not found"));
            return Ok(new ApiResponse<Product>(product, "Product retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Invalid data"));

            // Handle image upload
            string? imagePath = null;
            if (dto.Image != null)
            {
                imagePath = await _imageService.SaveImageAsync(dto.Image);
            }

            // map dto to entity
            var product = new Product
            {
                Category = dto.Category,
                ProductCode = dto.ProductCode,
                Name = dto.Name,
                Price = dto.Price,
                MinimumQuantity = dto.MinimumQuantity,
                DiscountRate = dto.DiscountRate,
                ImagePath = imagePath
            };

            var created = await _productService.AddProductAsync(product);
            return Ok(new ApiResponse<Product>(created, "Product created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest(new ApiResponse<string>("Product ID mismatch"));

            var updated = await _productService.UpdateProductAsync(product);
            return Ok(new ApiResponse<Product>(updated, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success) return NotFound(new ApiResponse<string>("Product not found"));
            return Ok(new ApiResponse<string>( "Product deleted successfully"));
        }
    }
}
