using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AcessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string Role { get; set; } = string.Empty;












    }
    //    public AuthResponseDto(string token, string name, string email, int role, DateTime expiration)
    //    {
    //        Token = token;
    //        Name = name;
    //        Email = email;
    //        Role = role;
    //        Expiration = expiration;
    //    }

}
