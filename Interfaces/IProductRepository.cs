using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOTNET_iyzico.Dtos.Product;
using DOTNET_iyzico.Models;

namespace DOTNET_iyzico.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task<List<Product>> GetAllAsync();
        Task<Product?> DeleteAsync(Guid id);
        Task<Product?> GetByIdAsync(Guid id);
    }
}