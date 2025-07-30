using FluentValidation;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Domain.Interfaces.Repositories;
using JwInventory.Domain.Validations;
using JwInventory.Domain.Validations.Explain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IProductRepository = JwInventory.Domain.Interfaces.Repositories.IProductRepository;

namespace JwInventory.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

        public Task<Response> CreateAsync(InternalTransfer product)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<InternalTransfer>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<InternalTransfer>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<InternalTransfer>> GetPriceAsync(int price)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<InternalTransfer>>> GetProductsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(Guid id, InternalTransfer product)
        {
            throw new NotImplementedException();
        }

        //Task<Response> IProductService.CreateAsync(InternalTransfer product)
        //{
        //    var response = new Response();

        //    var validation = new ProductValidation();
        //    var result = validation.Validate(product);

        //    if (!result.IsValid)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //        }
        //    }
    }
}
