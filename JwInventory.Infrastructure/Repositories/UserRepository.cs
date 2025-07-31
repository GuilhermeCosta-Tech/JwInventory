using JwInventory.Application.DTOs.User;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Data;
using System;
using System.Data.Entity;

public class UserRepository : IUserRepository
{
    private readonly JwInventoryDbContext _context;

    public UserRepository(JwInventoryDbContext context) => _context = context;
    public async Task<UserDto> GetByIdAsync(Guid id)
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
            CreatedAt = user.CreatedAt
        });
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = dto.CreatedAt,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            CreatedAt = user.CreatedAt,
            Email = user.Email
        };
    }

    public async Task<UserDto> UpdateAsync(Guid id, UserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new Exception("Usuário não encontrado");
        user.Name = dto.Name;
        user.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.PasswordHash))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
        }
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) throw new Exception("Usuário não encontrado");
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
