using MongoDB.Bson;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;

namespace ParlemTest.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(ObjectId id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(ObjectId id, Product product);
        Task<bool> DeleteProductAsync(ObjectId id);

    }
}
