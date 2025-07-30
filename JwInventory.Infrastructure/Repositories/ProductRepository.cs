using JwInventory.Application.DTOs.Product;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly JwInventoryDbContext _context;
        public ProductRepository(JwInventoryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Nome, 
                    Description = p.Description,
                    Price = p.Preco,
                    StockQuantity = p.StockQuantity
                })
                .ToListAsync();
        }
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Nome, 
                Description = product.Description,
                Price = product.Preco,
                StockQuantity = product.StockQuantity
            };
        }

        public async Task<ProductDto> CreateAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Id = productDto.Id,
                Nome = productDto.Name,
                Description = productDto.Description,
                Preco = productDto.Price,
                StockQuantity = productDto.StockQuantity
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Nome,
                Description = product.Description,
                Price = product.Preco,
                StockQuantity = product.StockQuantity
            };
        } 
    }
}
