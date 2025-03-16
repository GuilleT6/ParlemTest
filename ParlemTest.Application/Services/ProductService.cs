using AutoMapper;
using MongoDB.Bson;
using ParlemTest.Domain.DTOs;
using ParlemTest.Domain.Entities;
using ParlemTest.Domain.Interfaces;

namespace ParlemTest.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<GetProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            var productsDtos = _mapper.Map<List<GetProductDto>>(products);

            return productsDtos;
        }

        public async Task<GetProductDto?> GetProductByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var product = await _productRepository.GetProductByIdAsync(objectId);

            return _mapper.Map<GetProductDto?>(product);

        }
        public async Task<ProductDto?> CreateProductAsync(CreateProductDto productDto)
        {
            var newProduct = _mapper.Map<Product>(productDto);
            var createdProduct = await _productRepository.CreateProductAsync(newProduct);

            return _mapper.Map<ProductDto?>(createdProduct);

        }

        public async Task<ProductDto?> UpdateProductAsync(string id, UpdateProductDto productDto)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(objectId);

            if (existingProduct == null)
            {
                return null;
            }

            _mapper.Map(productDto, existingProduct);
            var updatedProduct = await _productRepository.UpdateProductAsync(objectId, existingProduct);
            return _mapper.Map<ProductDto?>(updatedProduct);


        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return false;
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(objectId);

            if (existingProduct == null)
            {
                return false;
            }

            return await _productRepository.DeleteProductAsync(objectId);

        }

    }
}
