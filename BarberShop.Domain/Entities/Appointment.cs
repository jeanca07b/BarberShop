using BarberShop.Domain.Common;
using BarberShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class Appointment : BaseEntity
    {

        public Guid BusinessId { get; private set; }
        public Guid BarberId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ServiceId { get; private set; }

        public DateTime Date { get; private set; }

        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        public decimal ReservationFee { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public Barber? Barber { get; private set; }
        public Customer? Customer { get; private set; }
        public Service? Service { get; private set; }

        private Appointment() { }

        public Appointment(
            Guid businessId,
            Guid customerId,
            Guid barberId,
            Guid serviceId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            decimal reservationFee)
        {
            Id = Guid.NewGuid();
            BusinessId = businessId;
            CustomerId = customerId;
            BarberId = barberId;
            ServiceId = serviceId;
            Date = date.Date;
            StartTime = startTime;
            EndTime = endTime;
            ReservationFee = reservationFee;
            Status = AppointmentStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            if (endTime <= startTime)
                throw new ArgumentException("End time must be greater than start time");
        }

        public void Confirm()
        {
            Status = AppointmentStatus.Confirmed;
        }

        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
        }
    }
}
