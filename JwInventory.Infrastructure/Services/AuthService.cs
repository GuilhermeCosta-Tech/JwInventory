using BCrypt.Net;
using JwInventory.Application.DTOs.Auth;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Domain.Enums;
using JwInventory.Domain.Interfaces.Services;
using JwInventory.Infrastructure.Security;


namespace JwInventory.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            // Valida role
            if (!Enum.TryParse<UserRole>(dto.Role, true, out var role))
                throw new ArgumentException("Role inválida");

            // Hash da senha
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Cria usuário apropriado (Admin, Colaborador etc)
            User user = role switch
            {
                UserRole.Admin => new AdminUser(dto.Name, dto.Email, passwordHash),
                UserRole.Gerente => new ManagerUser(dto.Name, dto.Email, passwordHash),
                _ => new EmployeeUser(dto.Name, dto.Email, passwordHash)
            };

            // Aqui será salvo no banco futuramente...

            var (token, expires) = _jwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Email, user.Role);

            return new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString(), // Em breve será persistido
                ExpiresAt = expires,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginUserDto dto)
        {
            // Buscar usuário do banco (em breve)
            var user = GetFakeUser(dto.Email); // temporário

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Senha inválida");

            var (token, expires) = _jwtTokenGenerator.GenerateToken(user.Id.ToString(), user.Email, user.Role);

            return new AuthResponseDto
            {
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString(),
                ExpiresAt = expires,
                Role = user.Role.ToString()
            };
        }

        // TEMPORÁRIO - simula usuário do banco
        private User GetFakeUser(string email)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("123456");
            return new AdminUser("Guilherme", email, hash);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // Em breve: validar refresh token salvo no banco

            throw new NotImplementedException();
        }
    }
}
