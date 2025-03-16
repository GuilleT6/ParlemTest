using Microsoft.AspNetCore.Mvc;
using ParlemTest.Application.Services;
using ParlemTest.Domain.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParlemTest.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<CustomersController> _logger;

        public ProductsController(IProductService productService,
            ILogger<CustomersController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();

                return products?.Count == 0
                    ? NotFound("No products found.")
                    : Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unexpected error occurred while retrieving all products.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);

                return product == null
                    ? NotFound($"Product not found.")
                    : Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product with ID {id}.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(productDto);

                return createdProduct == null
                    ? BadRequest("Invalid product data. Could not create product.")
                    : CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto productDto)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProductAsync(id, productDto);

                return updatedProduct == null
                    ? NotFound("Product not found.")
                    : Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with ID {id}.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var isDeleted = await _productService.DeleteProductAsync(id);
                return !isDeleted
                    ? NotFound($"Product not found.")
                    : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting product with ID {id}.");
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}
