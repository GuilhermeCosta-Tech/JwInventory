using JwInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public abstract class BaseUser(string name,
        string email,
        string passwordHash,
        UserRole colaborador)
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string PasswordHash { get; set; } = passwordHash;
        public string Role { get; set; } = "User"; // Default role

    }
}