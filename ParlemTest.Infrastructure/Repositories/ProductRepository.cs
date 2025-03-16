using MongoDB.Bson;
using MongoDB.Driver;
using ParlemTest.Domain.Entities;
using ParlemTest.Domain.Interfaces;


namespace ParlemTest.Infrastructure.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("products");
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var filter = Builders<Product>.Filter.Empty;
            return await _productCollection.Find(filter).ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(ObjectId id)
        {
            return await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task<Product> UpdateProductAsync(ObjectId id, Product product)
        {
            var result = await _productCollection.ReplaceOneAsync(p => p.Id == id, product);

            if (result.MatchedCount == 0)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            return product;
        }

        public async Task<bool> DeleteProductAsync(ObjectId id)
        {
            var result = await _productCollection.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

    }
}
