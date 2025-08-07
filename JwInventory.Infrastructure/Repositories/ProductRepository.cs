using JwInventory.Application.DTOs.Product;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly JwInventoryDbContext _context;

        public ProductRepository(JwInventoryDbContext dbContext) => _context = dbContext;
        public async Task CreateProductAsync(ProductDto productDto)
        {
            var product = new Product(productDto.Name, productDto.Id)
            {
                Description = productDto.Description,
                Preco = productDto.Price,
                StockQuantity = productDto.StockQuantity
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var product = new Product(productDto.Name, productDto.Id)
            {
                Description = productDto.Description,
                Preco = productDto.Price,
                StockQuantity = productDto.StockQuantity
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductDto> UpdateAsync(Guid id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Produto não encontrado.");

            product.Nome = productDto.Name;
            product.Description = productDto.Description;
            product.Preco = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;

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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Nome.Contains(searchTerm) || p.Description.Contains(searchTerm))
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

        public async Task<List<ProductDto>?> GetAllProductsAsync()
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

        public async Task<ProductDto?> GetByIdAsync(Guid id)
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

        public void UpdateProductAsync(Task<ProductDto?> existingProduct)
        {
            var productDto = existingProduct.Result;
            if (productDto == null)
                throw new KeyNotFoundException("Produto não encontrado.");
            var product = _context.Products.Find(productDto.Id);
            if (product == null)
                throw new KeyNotFoundException("Produto não encontrado.");
            product.Nome = productDto.Name;
            product.Description = productDto.Description;
            product.Preco = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}