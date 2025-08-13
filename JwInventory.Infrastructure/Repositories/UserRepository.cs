using JwInventory.Application.DTOs.User;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly JwInventoryDbContext _context;
    private readonly UserManager<PessoaComAcesso> _userManager;

    public UserRepository(JwInventoryDbContext context, UserManager<PessoaComAcesso> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user is null ? throw new Exception("Usuário não encontrado") : new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        });
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        PessoaComAcesso user;

        switch (dto.Role.ToLower())
        {
            case "admin":
                user = new AdminUser { UserName = dto.Name, Email = dto.Email };
                break;
            case "gerente":
                user = new ManagerUser { UserName = dto.Name, Email = dto.Email };
                break;
            case "colaborador":
                user = new EmployeeUser { UserName = dto.Name, Email = dto.Email };
                break;
            default:
                throw new ArgumentException("Role inválida.");
        }

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception($"Erro ao criar usuário: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        return new UserDto
        {
            Name = user.UserName,
            Email = user.Email
        };
    }

    public async Task<UserDto> UpdateAsync(int id, UserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) throw new Exception("Usuário não encontrado");

        user.UserName = dto.Name;
        user.Email = dto.Email;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Falha ao atualizar usuário.");
        }

        if (!string.IsNullOrEmpty(dto.PasswordHash))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, dto.PasswordHash);
            if (!passwordResult.Succeeded)
            {
                throw new Exception("Falha ao atualizar senha.");
            }
        }

        return new UserDto
        {
            Name = user.UserName,
            Email = user.Email,
        };
    }

    public async Task<PessoaComAcesso> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task AddAsync(CreateUserDto createUserDto)
    {
        throw new NotImplementedException();
    }

    public void UpdateAsync(Task<UserDto?> existingUser)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateAsync(Guid id, UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<UserDto> IUserRepository.GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}
