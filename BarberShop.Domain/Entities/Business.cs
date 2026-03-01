using BarberShop.Domain.Common;
using BarberShop.Domain.ValueObjects;

namespace BarberShop.Domain.Entities
{
    public class Business : BaseEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public Address Address { get; private set; } = null!;
        public string? Phone { get; private set; }

        public bool IsActive { get; private set; } = true;

        public Guid OwnerUserId { get; private set; }

        public ICollection<BusinessBarber> BusinessBarbers { get; private set; }
            = new List<BusinessBarber>();

        private Business() { }

        public Business(
            string name,
            string? description,
            Address address,
            string? phone,
            Guid ownerUserId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Business name is required.");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (ownerUserId == Guid.Empty)
                throw new ArgumentException("OwnerUserId is required.");

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            Name = name;
            Description = description;
            Address = address;
            Phone = phone;
            OwnerUserId = ownerUserId;
        }

        public void Update(
            string name,
            string? description,
            string? phone,
            Address address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Business name is required.");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            Name = name;
            Description = description;
            Phone = phone;
            Address = address;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}