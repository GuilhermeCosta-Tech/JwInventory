using JwInventory.Application.DTOs.Auth;

namespace JwInventory.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterUserDto dto);
        Task<string> LoginAsync(LoginUserDto dto);
    }
}
