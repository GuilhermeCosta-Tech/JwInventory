using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.Auth
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Role { get; set; } = "Colaborador";
    }
}
