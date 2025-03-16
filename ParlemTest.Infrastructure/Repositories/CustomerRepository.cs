using MongoDB.Bson;
using MongoDB.Driver;
using ParlemTest.Domain.Entities;
using ParlemTest.Domain.Repositories;

namespace ParlemTest.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(IMongoDatabase database)
        {
            _customerCollection = database.GetCollection<Customer>("customers");
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var filter = Builders<Customer>.Filter.Empty;
            return  await _customerCollection.Find(filter).ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(ObjectId id)
        {
            return await _customerCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetCustomerProductsByIdAsync(ObjectId id)
        {
            var customer = await _customerCollection
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return new List<Product>();
            }

            return customer.Products;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
        
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customer.Id);
            var result = await _customerCollection.ReplaceOneAsync(filter, customer);

            if (result.MatchedCount == 0)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Id} not found.");
            }

            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(ObjectId id)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            var result = await _customerCollection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<bool> AddProductToCustomerAsync(ObjectId customerId, Product product)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
            var update = Builders<Customer>.Update.Push(c => c.Products, product);

            var result = await _customerCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }
        public async Task<bool> RemoveProductFromCustomerAsync(ObjectId customerId, ObjectId productId)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
            var update = Builders<Customer>.Update.PullFilter(c => c.Products, p => p.Id == productId);

            var result = await _customerCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;
        }

    }
}
