using JwInventory.Application.DTOs.Auth;

/// <summary>
/// Interface para os serviços de autenticação de usuários.
/// </summary>
namespace JwInventory.Application.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="dto">Dados do usuário para registro.</param>
        /// <returns>Token JWT do usuário registrado.</returns>
        Task<string> RegisterAsync(RegisterUserDto dto);

        /// <summary>
        /// Realiza o login de um usuário.
        /// </summary>
        /// <param name="dto">Credenciais do usuário.</param>
        /// <returns>Token JWT se o login for bem-sucedido.</returns>
        Task<string> LoginAsync(LoginUserDto dto);
    }
}
