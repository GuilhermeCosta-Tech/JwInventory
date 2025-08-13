using JwInventory.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PasswordHash { get; set; }
        public string Role { get; set; } = "User"; 
    }
}
