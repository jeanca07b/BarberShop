using Microsoft.EntityFrameworkCore;
using BarberShop.Application.Interfaces;
using BarberShop.Domain.Entities;
using BarberShop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Infrastructure.Repositories
{
    public class BarberBreakRepository : IBarberBreakRepository
    {
        private readonly BarberShopDbContext _context;

        public BarberBreakRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<BarberBreak>> GetByBarberAndDayAsync(Guid barberId, DayOfWeek day)
        {
            return await _context.BarberBreaks
                .Where(b => b.BarberId == barberId && b.DayOfWeek == day)
                .ToListAsync();
        }
    }
}
