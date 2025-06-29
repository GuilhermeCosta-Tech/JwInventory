using JwInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public UserRole Role { get; protected set; }

        protected User() { }

        protected User(string name, string email, string password, UserRole role)
        {
            Name = name;
            Email = email.ToLowerInvariant();
            Password = password;
            Role = role;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        public void UpdateRole(UserRole role)
        {
            if (!Enum.IsDefined(typeof(UserRole), role))
                throw new ArgumentException("Invalid user role.", nameof(role));
            Role = role;
        }
    }
}