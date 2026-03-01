using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace BarberShop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BarberShopDbContext _context;

        public UserRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
