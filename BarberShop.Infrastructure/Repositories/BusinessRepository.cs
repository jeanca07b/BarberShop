using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly BarberShopDbContext _context;

        public BusinessRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Business business)
            => await _context.Businesses.AddAsync(business);

        public async Task<Business?> GetByIdAsync(Guid id)
            => await _context.Businesses
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);

        public async Task<List<Business>> GetAllAsync()
            => await _context.Businesses
                .Where(b => b.IsActive)
                .ToListAsync();


        public void Update(Business business)
            => _context.Businesses.Update(business);

        public void Remove(Business business)
            => _context.Businesses.Remove(business);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
