using System;

namespace JwInventory.Application.DTOs.Auth
{
    /// <summary>
    /// Representa a resposta enviada ao usuário após a autenticação bem-sucedida (login ou registro)
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// ID único do usuário autenticado
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// O nome de usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// O email do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// O token de acesso gerado para a sessão do cliente
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// A data e hora exata da expiração do token
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}
