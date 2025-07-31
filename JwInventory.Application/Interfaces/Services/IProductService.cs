using JwInventory.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<Response> CreateAsync(ProductDto productDto);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<ProductDto>> ListByFilterAsync();    
    }
}
