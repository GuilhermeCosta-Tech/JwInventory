using JwInventory.Application.DTOs.Product;

namespace JwInventory.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDto productDto);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto);
        Task<bool> DeleteAsync(Guid id);
        Task<List<ProductDto>> ListByFilterAsync();
    }
}
