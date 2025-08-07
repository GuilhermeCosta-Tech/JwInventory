using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public UpdateProductDto(string productName, string description, decimal price, int stockQuantity)
        {
            Name = productName;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
