using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace BarberShop.Infrastructure.Persistence.Repositories
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarberShopDbContext _context;

        public BarberRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Barber barber)
        {
            await _context.Barbers.AddAsync(barber);
        }

        public async Task<Barber?> GetByIdAsync(Guid id)
        {
            return await _context.Barbers
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Barber>> GetByBusinessAsync(Guid businessId)
        {
            return await _context.BusinessBarbers
                .Where(bb => bb.BusinessId == businessId &&
                             bb.Status == BusinessBarberStatus.Approved)
                .Select(bb => bb.Barber)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}