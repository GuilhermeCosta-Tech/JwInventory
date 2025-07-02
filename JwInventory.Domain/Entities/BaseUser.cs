using JwInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public abstract class BaseUser
    {
        protected BaseUser(string name, string email, string passwordHash, UserRole gerente)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public string PasswordHash { get; set; } = "";
            public string Role { get; set; } = "User"; // Default role

    }
}