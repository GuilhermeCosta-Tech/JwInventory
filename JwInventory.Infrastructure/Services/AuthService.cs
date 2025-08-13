using JwInventory.Application.DTOs.Auth;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JwInventory.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<PessoaComAcesso> _userManager;
        private readonly SignInManager<PessoaComAcesso> _signInManager;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly RoleManager<PerfilDeAcesso> _roleManager;

        public AuthService(
            UserManager<PessoaComAcesso> userManager,
            SignInManager<PessoaComAcesso> signInManager,
            JwtTokenGenerator tokenGenerator,
            RoleManager<PerfilDeAcesso> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
        }

        public async Task<UserResponse> RegisterAsync(RegisterUserDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Usuário já existe com este email.");

            PessoaComAcesso newUser;
            var role = dto.Role ?? "Colaborador"; 

            switch (role.ToLower())
            {
                case "admin":
                    newUser = new AdminUser { UserName = dto.Name, Email = dto.Email };
                    break;
                case "gerente":
                    newUser = new ManagerUser { UserName = dto.Name, Email = dto.Email };
                    break;
                default:
                    newUser = new EmployeeUser { UserName = dto.Name, Email = dto.Email };
                    role = "Colaborador";
                    break;
            }

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // Adiciona a Role ao usuário
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new PerfilDeAcesso(role));
            }
            await _userManager.AddToRoleAsync(newUser, role);


            var roles = await _userManager.GetRolesAsync(newUser);
            var (token, expiration) = _tokenGenerator.GenerateToken(newUser, roles);

            return new UserResponse
            {
                Id = newUser.Id.ToString(),
                Name = newUser.UserName,
                Email = newUser.Email,
                Token = token,
                Expiration = expiration
            };
        }

        public async Task<UserResponse> LoginAsync(LoginUserDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("Credenciais inválidas.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new Exception("Credenciais inválidas.");

            var roles = await _userManager.GetRolesAsync(user);
            var (token, expiration) = _tokenGenerator.GenerateToken(user, roles);

            return new UserResponse
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
                Email = user.Email,
                Token = token,
                Expiration = expiration
            };
        }
    }
}

