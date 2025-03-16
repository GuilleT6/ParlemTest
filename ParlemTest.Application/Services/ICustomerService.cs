using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;
using System.Threading.Tasks;

namespace ParlemTest.Application.Services
{
    public interface ICustomerService
    {
        Task<List<GetCustomerDto>> GetAllCustomersAsync();
        Task<GetCustomerDto?> GetCustomerByIdAsync(string id);
        Task<List<GetProductDto>> GetCustomerProductsByIdAsync(string id);
        Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto customerDto);
        Task<CustomerDto?> UpdateCustomerAsync(string id, UpdateCustomerDto customerDto);
        Task<bool> DeleteCustomerAsync(string id);
        Task<bool> AddProductToCustomerAsync(string id, CreateProductDto productDto);
        Task<bool> RemoveProductFromCustomerAsync(string customerId, string productId);
    }
}
