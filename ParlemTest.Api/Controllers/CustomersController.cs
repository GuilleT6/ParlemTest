using Microsoft.AspNetCore.Mvc;
using ParlemTest.Application.Services;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;

namespace ParlemTest.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService,
            IProductService productService,
            ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();

                return customers?.Count == 0
                    ? NotFound("No customers found.")
                    : Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unexpected error occurred while retrieving all customers.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);

                return customer == null
                    ? NotFound("Customer not found.")
                    : Ok(customer);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex,
                    "An invalid argument was passed while retrieving information for customer {CustomerId}.",
                    id);
                return BadRequest("One or more input parameters are invalid.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unexpected error occurred while retrieving data for customer {CustomerId}.",
                    id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCustomerProducts(string id)
        {
            try
            {
                var products = await _customerService.GetCustomerProductsByIdAsync(id);

                return products?.Count == 0
                    ? NotFound("No products found for this customer.")
                    : Ok(products);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex,
                    "Error retrieving products for customer {CustomerId}",
                    id);
                return BadRequest("One or more input parameters are invalid.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            try
            {
                var createdCustomer = await _customerService.CreateCustomerAsync(customerDto);

                return createdCustomer == null
                    ? BadRequest("Invalid customer data. Could not create customer.")
                    : CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.Id }, createdCustomer);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error creating customer.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] UpdateCustomerDto customerDto)
        {
            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);

                return updatedCustomer == null
                    ? NotFound("Customer not found.")
                    : Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer {CustomerId}", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                var isDeleted = await _customerService.DeleteCustomerAsync(id);

                return !isDeleted
                    ? NotFound("Customer not found.")
                    : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error deleting customer {CustomerId}",
                    id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProductToCustomer(string id, [FromBody] CreateProductDto productDto)
        {
            try
            {
                var isAdded = await _customerService.AddProductToCustomerAsync(id, productDto);

                return !isAdded
                    ? BadRequest("Error adding product to customer.")
                    : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product to customer {CustomerId}", id);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{customerId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductFromCustomer(string customerId, string productId)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(customerId);
                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }

                var isDeleted = await _customerService.RemoveProductFromCustomerAsync(customerId, productId);

                return !isDeleted
                    ? NotFound("Product not found for this customer.")
                    : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing product from customer {CustomerId}", customerId);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
