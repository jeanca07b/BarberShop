using BarberShop.Domain.Entities;
using BarberShop.Domain.Enums;
using BarberShop.Domain.Repositories;
using BarberShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly BarberShopDbContext _context;

        public AppointmentRepository(BarberShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
        }

        public async Task<List<Appointment>> GetByBarberAndDateAsync(Guid barberId, DateTime date)
        {
            return await _context.Appointments
                .AsNoTracking()
                .Where(a => a.BarberId == barberId
                         && a.Date == date.Date
                         && a.Status != AppointmentStatus.Cancelled)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
