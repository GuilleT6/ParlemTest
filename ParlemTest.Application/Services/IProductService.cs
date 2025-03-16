using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;

namespace ParlemTest.Application.Services
{
    public interface IProductService
    {
        Task<List<GetProductDto>> GetAllProductsAsync();
        Task<GetProductDto?> GetProductByIdAsync(string id);
        Task<ProductDto?> CreateProductAsync(CreateProductDto productDto);
        Task<ProductDto?> UpdateProductAsync(string id, UpdateProductDto productDto);
        Task<bool> DeleteProductAsync(string id);
    }
}
