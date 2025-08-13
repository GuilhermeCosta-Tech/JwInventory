using JwInventory.Application.DTOs.Product;
using JwInventory.Application.DTOs.User;

namespace JwInventory.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task AddAsync(CreateUserDto createUserDto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> UpdateAsync(Guid id, UserDto userDto);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDto> GetByEmailAsync(string email);
        void UpdateAsync(Task<UserDto?> existingUser);
    }
}
