using AutoMapper;
using MongoDB.Bson;
using Moq;
using ParlemTest.Application.Services;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;
using ParlemTest.Domain.Repositories;

namespace ParlemTest.UnitTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _customerService = new CustomerService(_customerRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsMappedCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = ObjectId.GenerateNewId(), GivenName = "Joan", FamilyName1 = "Garcia", Email = "joan.garcia@example.com", Phone = "666111222" }
            };

            var customerDtos = new List<GetCustomerDto>
            {
                new GetCustomerDto { FullName = "Joan Garcia", Email = "joan.garcia@example.com", Phone = "666111222" }
            };

            _customerRepoMock.Setup(r => r.GetAllCustomersAsync()).ReturnsAsync(customers);
            _mapperMock.Setup(m => m.Map<List<GetCustomerDto>>(customers)).Returns(customerDtos);

            var result = await _customerService.GetAllCustomersAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Joan Garcia", result[0].FullName);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ValidId_ReturnsCustomer()
        {
            var id = ObjectId.GenerateNewId();
            var customer = new Customer
            {
                Id = id,
                GivenName = "Maria",
                FamilyName1 = "Lopez",
                Email = "maria.lopez@example.com",
                Phone = "634567890"
            };

            var customerDto = new GetCustomerDto
            {
                FullName = "Maria Lopez",
                Email = "maria.lopez@example.com",
                Phone = "634567890"
            };

            _customerRepoMock.Setup(r => r.GetCustomerByIdAsync(id)).ReturnsAsync(customer);
            _mapperMock.Setup(m => m.Map<GetCustomerDto?>(customer)).Returns(customerDto);

            var result = await _customerService.GetCustomerByIdAsync(id.ToString());

            Assert.NotNull(result);
            Assert.Equal("Maria Lopez", result!.FullName);
            Assert.Equal("maria.lopez@example.com", result!.Email);
            Assert.Equal("634567890", result!.Phone);
        }

        [Fact]
        public async Task GetCustomerProductsByIdAsync_CustomerNotFound_ReturnsEmptyList()
        {
            var id = ObjectId.GenerateNewId();

            _customerRepoMock.Setup(r => r.GetCustomerProductsByIdAsync(id)).ReturnsAsync(new List<Product>());
            _mapperMock.Setup(m => m.Map<List<GetProductDto>>(It.IsAny<List<Product>>())).Returns(new List<GetProductDto>());

            var result = await _customerService.GetCustomerProductsByIdAsync(id.ToString());

            Assert.Empty(result);
        }

        [Fact]
        public async Task AddProductToCustomerAsync_ValidCustomer_ReturnsTrue()
        {
            var customerId = ObjectId.GenerateNewId().ToString();
            var productDto = new CreateProductDto { ProductName = "Product1", ProductTypeName = "Type1", NumeracioTerminal = "12345", SoldAt = DateTime.Now };
            var product = new Product { ProductName = "Product1", ProductTypeName = "Type1", NumeracioTerminal = "12345", SoldAt = DateTime.Now };

            _mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(product);
            _customerRepoMock.Setup(r => r.AddProductToCustomerAsync(It.IsAny<ObjectId>(), It.IsAny<Product>())).ReturnsAsync(true);

            var result = await _customerService.AddProductToCustomerAsync(customerId, productDto);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProductFromCustomerAsync_ValidCustomerAndProduct_ReturnsTrue()
        {
            var customerId = ObjectId.GenerateNewId().ToString();
            var productId = ObjectId.GenerateNewId().ToString();

            _customerRepoMock.Setup(r => r.RemoveProductFromCustomerAsync(It.IsAny<ObjectId>(), It.IsAny<ObjectId>())).ReturnsAsync(true);

            var result = await _customerService.RemoveProductFromCustomerAsync(customerId, productId);

            Assert.True(result);
        }
    }
}