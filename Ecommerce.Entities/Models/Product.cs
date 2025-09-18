using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string ProductCode { get; set; } = string.Empty; // unique

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? ImagePath { get; set; } // local storage path

        [Range(0.01, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int MinimumQuantity { get; set; }

        [Range(0, 100)]
        public double DiscountRate { get; set; }
    }
}
