//using JwInventory.Application.Interfaces.Repositories;
//using JwInventory.Domain.Entities;
//using JwInventory.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace JwInventory.Infrastructure.Repositories
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly JwInventoryDbContext _context;

//        public UserRepository(JwInventoryDbContext context) => _context = context;

//        public async Task AddAsync(User user)
//        {
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//        }


//        public async Task<User?> GetByEmailAsync(string email)
//        {
//            return await _context.Users
//                .AsNoTracking()
//                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
//        }

//        public async Task<bool> EmailExistsAsync(string email)
//        {
//            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
//        }
//    }
//}
