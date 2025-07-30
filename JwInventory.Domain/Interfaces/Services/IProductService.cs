using JwInventory.Application.DTOs.Product;
using JwInventory.Domain.Entities;
using JwInventory.Domain.Validations.Explain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response> CreateAsync(InternalTransfer product);
        Task<Response> UpdateAsync(Guid id, InternalTransfer product);
        Task<Response<InternalTransfer>> GetByIdAsync(Guid id);
        Task<Response<InternalTransfer>> GetPriceAsync(int price);
        Task<Response<List<InternalTransfer>>> GetProductsByNameAsync(string name);  
        Task<Response<List<InternalTransfer>>> GetAllAsync();
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
