using BarberShop.Domain.Common;
using BarberShop.Domain.Enums;

namespace BarberShop.Domain.Entities
{
    public class BusinessBarber : BaseEntity
    {
        public Guid BusinessId { get; private set; }
        public Guid BarberId { get; private set; }

        public BusinessBarberStatus Status { get; private set; }

        public Business Business { get; private set; } = null!;
        public Barber Barber { get; private set; } = null!;

        private BusinessBarber() { }

        public BusinessBarber(Guid businessId, Guid barberId)
        {
            BusinessId = businessId;
            BarberId = barberId;
            Status = BusinessBarberStatus.Pending;
        }

        public void Approve()
        {
            Status = BusinessBarberStatus.Approved;
        }

        public void Reject()
        {
            Status = BusinessBarberStatus.Rejected;
        }
    }
}