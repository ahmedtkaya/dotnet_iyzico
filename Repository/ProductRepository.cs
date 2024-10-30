using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DOTNET_iyzico.Interfaces;
using DOTNET_iyzico.Data;
using DOTNET_iyzico.Models;
using DOTNET_iyzico.Dtos.Product;

namespace DOTNET_iyzico.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;

        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Brand = createProductDto.Brand,
                Price = createProductDto.Price,
                Currency = createProductDto.Currency,
                Stock = createProductDto.Stock,
                ItemType = createProductDto.ItemType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Veritabanına Product ekleme
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // ID'nin oluşması için kaydediyoruz

            // Kategorileri ekleme
            foreach (var categoryName in createProductDto.Categories)
            {
                var productCategory = new ProductCategory
                {
                    CategoryName = categoryName,
                    ProductId = product.Id
                };
                await _context.ProductCategories.AddAsync(productCategory);
            }

            // Görselleri ekleme
            foreach (var imagePath in createProductDto.ImagePaths)
            {
                var productImage = new ProductImage
                {
                    ImagePath = imagePath,
                    ProductId = product.Id
                };
                await _context.ProductImages.AddAsync(productImage);
            }

            // Tüm değişiklikleri kaydet
            await _context.SaveChangesAsync();

        }
        public async Task<Product?> DeleteAsync(Guid id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (productModel == null)
            {
                return null;
            }
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

    }
}