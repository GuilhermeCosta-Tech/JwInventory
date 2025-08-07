using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public CreateProductDto(string productName, string productDescription)
        {
            ProductName = productName;
            ProductDescription = productDescription;
        }
    }
}
