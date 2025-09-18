using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly string _imageFolderPath;

        public ImageService()
        {
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        public async Task<string?> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_imageFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // /images/{fileName}
            return $"/images/{fileName}";
        }
    }
}
