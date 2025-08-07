using JwInventory.Application.DTOs.Auth;
using JwInventory.Application.DTOs.User;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Domain.Enums;
using JwInventory.Domain.Interfaces.Services;
using JwInventory.Infrastructure.Security;

namespace JwInventory.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthService(IUserRepository userRepository, JwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponse> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Usuário já existe com este email.");

            var createUserDto = new CreateUserDto
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashHelper.HashPassword(dto.Password),
                Role = "User"
            };

            var createdUser = await _userRepository.CreateUserAsync(createUserDto);

            var (token, expiration) = _tokenGenerator.GenerateToken(createdUser.Id.ToString(),
                createdUser.Email,
                Enum.Parse<UserRole>(createdUser.Role));

            return new UserResponse
            {
                Token = token,
                Expiration = expiration
            };
        }

        public async Task<UserResponse> LoginAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !HashHelper.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Credenciais inválidas.");

            var (token, expiration) = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Email, Enum.Parse<UserRole>(user.Role));

            return new UserResponse
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}

