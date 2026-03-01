using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly BarberShopDbContext _context;

        public ServiceRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Service>> GetByBusinessAsync(Guid businessId)
        {
            return await _context.Services
                .Where(s => s.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}