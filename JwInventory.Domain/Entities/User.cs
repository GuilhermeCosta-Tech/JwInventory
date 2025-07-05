using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // Default to empty string
        public string Email { get; set; } = string.Empty; // Default to empty string
        public string PasswordHash { get; set; } = string.Empty; // Default to empty string
        public string Role { get; set; } = "User"; // Default role
    }
}
