using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.Auth
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public char Token { get; set; }
    }
}
