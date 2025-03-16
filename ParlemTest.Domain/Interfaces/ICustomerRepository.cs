using MongoDB.Bson;
using ParlemTest.Domain.Entities;

namespace ParlemTest.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(ObjectId id);
        Task<List<Product>> GetCustomerProductsByIdAsync(ObjectId id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(ObjectId id);
        Task<bool> AddProductToCustomerAsync(ObjectId customerId, Product product);
        Task<bool> RemoveProductFromCustomerAsync(ObjectId customerId, ObjectId productId);

    }
}
