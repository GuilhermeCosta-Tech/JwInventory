//using FluentValidation;
//using JwInventory.Application.DTOs.Product;
//using JwInventory.Application.Interfaces.Repositories;
//using JwInventory.Application.Interfaces.Services;
//using JwInventory.Domain.Entities;
//using JwInventory.Domain.Interfaces.Repositories;
//using JwInventory.Domain.Validations;
//using JwInventory.Domain.Validations.Explain;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JwInventory.Infrastructure.Services
//{
//    public class ProductService : IProductService
//    {
//        private readonly IProductRepository _productRepository;
//        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;
//        public Task<ProductDto> CreateAsync(ProductDto productDto)
//        {
//            var validatedProduct = new ProductValidation();
//            var result = validatedProduct.Validate(productDto);

//            if (!result.IsValid)
//            {
//                foreach (var error in result.Errors)
//                {
//                    throw new ValidationException(error.ErrorMessage);
//                }
//            }

//            throw new NotImplementedException();
//        }

//        public Task<Response> CreateAsync(InternalTransfer product)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> DeleteAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Response<List<InternalTransfer>>> GetAllAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ProductDto> GetByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Response<InternalTransfer>> GetPriceAsync(int price)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Response<List<InternalTransfer>>> GetProductsByNameAsync(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<ProductDto>> ListByFilterAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Response> UpdateAsync(Guid id, InternalTransfer product)
//        {
//            throw new NotImplementedException();
//        }

//        Task<Response<bool>> IProductService.DeleteAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        Task<Response<InternalTransfer>> IProductService.GetByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
