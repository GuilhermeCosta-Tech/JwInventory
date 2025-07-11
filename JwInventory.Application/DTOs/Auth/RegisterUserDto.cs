using System;

namespace JwInventory.Application.DTOs.Auth
{
    /// <summary>
    /// DTO para registrar um novo usuário.
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>Nome do usuário.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Email do usuário.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Senha do usuário.</summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>Papel do usuário (ex: "User", "Admin").</summary>
        public string Role { get; set; } = "User";
    }
}
