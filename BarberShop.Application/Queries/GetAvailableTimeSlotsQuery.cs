using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Queries
{
    public record GetAvailableTimeSlotsQuery(
        Guid BarberId,
        Guid ServiceId,
        DateTime Date
    );
}
