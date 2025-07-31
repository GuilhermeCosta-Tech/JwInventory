using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Entities
{
    public class User
    {
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public User()
        {
            // Foto de perfil padrão
        }

        public User(string name,
            string email, 
            string passwordHash, 
            string role = "User")
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public string PasswordHash { get; set; } = string.Empty; 
        public string Role { get; set; } = "User"; // Default role
    }
}
