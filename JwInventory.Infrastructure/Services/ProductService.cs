using AutoMapper;
using FluentValidation;
using JwInventory.Application.DTOs.Product;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwInventory.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var existingProducts = await _productRepository.SearchAsync(productDto.Name);
            if (existingProducts.Any(p => p.Name.Equals(productDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ValidationException("Um produto com este nome já existe.");
            }

            await _productRepository.AddAsync(productDto);
            return productDto;
        }
        public Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var existingProduct = _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return Task.FromResult<ProductDto>(null);
            }
            _mapper.Map(productDto, existingProduct);
            _productRepository.UpdateProductAsync(existingProduct);
            return Task.FromResult(_mapper.Map<ProductDto>(existingProduct));
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(id);
            return true;
        }

        public Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm)
        {
            var products = _productRepository.SearchAsync(searchTerm);
            if (products == null)
            {
                return Task.FromResult<IEnumerable<ProductDto>>(new List<ProductDto>());
            }
            return Task.FromResult(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products ?? new List<ProductDto>();
        }

        public Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return Task.FromResult<ProductDto>(null);
            }
            return Task.FromResult(_mapper.Map<ProductDto>(product));
        }

        public Task<List<ProductDto>> ListByFilterAsync()
        {
            var products = _productRepository.GetAllProductsAsync();
            if (products == null)
            {
                return Task.FromResult(new List<ProductDto>());
            }
            return Task.FromResult(_mapper.Map<List<ProductDto>>(products));
        }

        Task IProductService.CreateProductAsync(ProductDto productDto)
        {

            return CreateProductAsync(productDto);
        }
    }
}