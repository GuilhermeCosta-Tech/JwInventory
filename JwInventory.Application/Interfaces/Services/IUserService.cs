using JwInventory.Application.DTOs.User;

namespace JwInventory.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> UpdateAsync(Guid id, UserDto userDto);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDto> GetByEmailAsync(string email);

    }
}
