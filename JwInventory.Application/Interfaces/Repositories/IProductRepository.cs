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
        Task CreateProductAsync(ProductDto productDto);
        Task AddAsync(ProductDto productDto);
        Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm);
        Task<List<ProductDto>?> GetAllProductsAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        void UpdateProductAsync(Task<ProductDto?> existingProduct);
    }
}