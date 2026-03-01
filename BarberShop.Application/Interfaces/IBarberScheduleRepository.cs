using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Repositories
{
    public interface IBarberScheduleRepository
    {
        Task<BarberSchedule?> GetByBarberAndDayAsync(Guid barberId, DayOfWeek day);
    }
}
