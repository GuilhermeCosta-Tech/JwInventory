//using JwInventory.Application.DTOs.Auth;
//using JwInventory.Application.Interfaces.Repositories;
//using JwInventory.Domain.Entities;
//using JwInventory.Domain.Enums;
//using JwInventory.Domain.Interfaces.Services;
//using JwInventory.Infrastructure.Repositories.Interfaces;
//using JwInventory.Infrastructure.Security;

//namespace JwInventory.Infrastructure.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly JwtTokenGenerator _tokenGenerator;

//        public AuthService(IUserRepository userRepository, JwtTokenGenerator tokenGenerator)
//        {
//            _userRepository = userRepository;
//            _tokenGenerator = tokenGenerator;
//        }

//        public async Task<string> RegisterAsync(RegisterUserDto dto)
//        {
//            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
//            if (existingUser != null)
//                throw new Exception("Usuário já existe com este email.");

//            var user = new User
//            {
//                Id = Guid.NewGuid(),
//                Name = dto.Name,
//                Email = dto.Email,
//                PasswordHash = HashHelper.HashPassword(dto.Password),
//                Role = dto.Role
//            };

//            await _userRepository.AddAsync(user);

//            // Corrigido: passando os parâmetros corretos e pegando apenas o token
//            var (token, _) = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Email, Enum.Parse<UserRole>(user.Role));
//            return token;
//        }

//        public async Task<string> LoginAsync(LoginUserDto dto)
//        {
//            var user = await _userRepository.GetByEmailAsync(dto.Email);
//            if (user == null || !HashHelper.VerifyPassword(dto.Password, user.PasswordHash))
//                throw new Exception("Credenciais inválidas.");

//            var (token, _) = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Email, Enum.Parse<UserRole>(user.Role));
//            return token;
//        }
//    }
//}
