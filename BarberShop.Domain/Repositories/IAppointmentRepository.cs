using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<List<Appointment>> GetByBarberAndDateAsync(Guid barberId, DateTime date);
        Task SaveChangesAsync();
    }
}
