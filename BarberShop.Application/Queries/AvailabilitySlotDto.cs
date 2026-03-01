using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.Queries
{
    public record AvailabilitySlotDto(
        TimeSpan StartTime,
        TimeSpan EndTime,
        bool IsAvailable
    );
}
