using BarberShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Interfaces
{
    public interface IBarberBreakRepository
    {
        Task<List<BarberBreak>> GetByBarberAndDayAsync(Guid barberId, DayOfWeek day);
    }
}
