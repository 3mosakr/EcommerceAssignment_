using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Dto.Product
{
    public class ProductCreateDto
    {
        [Required, MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string ProductCode { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int MinimumQuantity { get; set; }

        [Range(0, 100)]
        public double DiscountRate { get; set; }

        public IFormFile? Image { get; set; }
    }
}
