using JwInventory.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(ProductDto productDto);
        Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm);
        Task<IEnumerable<ProductDto>> GetProductsByNameAsync(string name);
    }
}

        //Task<IEnumerable<ProductDto>> GetAllAsync();
        //Task<ProductDto> GetByIdAsync(Guid id);
        //Task<ProductDto> CreateAsync(ProductDto productDto);
        //Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto);
        //Task<bool> DeleteAsync(Guid id);
        //Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm);
        //Task<IEnumerable<ProductDto>> GetProductsByNameAsync(string name);