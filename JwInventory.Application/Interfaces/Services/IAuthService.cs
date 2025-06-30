using JwInventory.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto);
        Task<AuthResponseDto> LoginAsync(LoginUserDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
