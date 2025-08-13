using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.User
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string? PasswordHash { get; set; }
        public string Role { get; set; } = "User"; // Default role is "User"
        public UpdateUserDto(Guid id, string name, string description, string email, DateTime createdAt, string? passwordHash = null, string role = "User")
        {
            Id = id;
            Name = name;
            Description = description;
            Email = email;
            CreatedAt = createdAt;
            PasswordHash = passwordHash;
            Role = role;
        }
    }
}
