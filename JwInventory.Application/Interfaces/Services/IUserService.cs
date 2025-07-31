using JwInventory.Application.DTOs.User;

namespace JwInventory.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task GetByEmailAsync(string email);

    }
}
