using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class BarberBreak
    {
        public Guid Id { get; private set; }

        public Guid BarberId { get; private set; }

        public DayOfWeek DayOfWeek { get; private set; }

        public TimeSpan StartTime { get; private set; }

        public TimeSpan EndTime { get; private set; }

        private BarberBreak() { }

        public BarberBreak(Guid barberId, DayOfWeek dayOfWeek,
            TimeSpan startTime, TimeSpan endTime)
        {
            Id = Guid.NewGuid();
            BarberId = barberId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
