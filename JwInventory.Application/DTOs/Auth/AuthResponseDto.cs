using System;

namespace JwInventory.Application.DTOs.Auth
{
    /// <summary>
    /// DTO para resposta de autenticação.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>Token de acesso JWT.</summary>
        public string AcessToken { get; set; } = string.Empty;

        /// <summary>Token de atualização.</summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>Data e hora de expiração do token.</summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>Papel do usuário.</summary>
        public string Role { get; set; } = string.Empty;
    }
}
