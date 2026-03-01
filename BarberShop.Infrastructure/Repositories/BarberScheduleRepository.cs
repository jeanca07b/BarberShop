using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberShop.Infrastructure.Repositories
{
    public class BarberScheduleRepository : IBarberScheduleRepository
    {
        private readonly BarberShopDbContext _context;

        public BarberScheduleRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task<BarberSchedule?> GetByBarberAndDayAsync(Guid barberId, DayOfWeek day)
        {
            return await _context.BarberSchedules
                .FirstOrDefaultAsync(s => s.BarberId == barberId && s.DayOfWeek == day);
        }
    }
}
