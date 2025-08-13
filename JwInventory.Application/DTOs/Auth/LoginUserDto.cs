using System;

namespace JwInventory.Application.DTOs.Auth
{
    /// <summary>
    /// DTO para login de usuário.
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>Email do usuário.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Senha do usuário.</summary>
        public string Password { get; set; } = string.Empty;
    }
}
