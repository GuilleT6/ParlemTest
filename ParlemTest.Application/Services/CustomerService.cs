using AutoMapper;
using MongoDB.Bson;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;
using ParlemTest.Domain.Interfaces;
using ParlemTest.Domain.Repositories;


namespace ParlemTest.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var customerDtos = _mapper.Map<List<GetCustomerDto>>(customers);

            return customerDtos;

        }
        public async Task<GetCustomerDto?> GetCustomerByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }
            var customer = await _customerRepository.GetCustomerByIdAsync(objectId);

            return _mapper.Map<GetCustomerDto?>(customer);

        }

        public async Task<List<GetProductDto>> GetCustomerProductsByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                throw new ArgumentException("Invalid id format", nameof(id));
            }

            var products = await _customerRepository.GetCustomerProductsByIdAsync(objectId);

            if (products == null || products.Count == 0)
            {
                return new List<GetProductDto>();
            }

            return _mapper.Map<List<GetProductDto>>(products);

        }
        public async Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customer.Id);
            if (existingCustomer != null)
            {
                throw new InvalidOperationException("A customer with this ID already exists.");
            }

            await _customerRepository.AddCustomerAsync(customer);
            return _mapper.Map<CustomerDto?>(customer);

        }

        public async Task<CustomerDto?> UpdateCustomerAsync(string id, UpdateCustomerDto customerDto)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(objectId);

            if (existingCustomer == null)
            {
                return null;
            }

            var updatedCustomer = _mapper.Map(customerDto, existingCustomer);

            await _customerRepository.UpdateCustomerAsync(updatedCustomer);
            return _mapper.Map<CustomerDto?>(updatedCustomer);

        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return false;
            }

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(objectId);
            if (existingCustomer == null)
            {
                return false;
            }

            await _customerRepository.DeleteCustomerAsync(objectId);
            return true;

        }

        public async Task<bool> AddProductToCustomerAsync(string id, CreateProductDto productDto)
        {
            var idObject = ObjectId.Parse(id);
            var product = _mapper.Map<Product>(productDto);
            return await _customerRepository.AddProductToCustomerAsync(idObject, product);
        }

        public async Task<bool> RemoveProductFromCustomerAsync(string customerId, string productId)
        {

            var customerIdObject = ObjectId.Parse(customerId);
            var productIdObject = ObjectId.Parse(productId);
            return await _customerRepository.RemoveProductFromCustomerAsync(customerIdObject, productIdObject);

        }
    }
}
