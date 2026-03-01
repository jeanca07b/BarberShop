using BarberShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class Barber : BaseEntity
    {

        public string FullName { get; private set; } = null!;

        public string? Phone { get; private set; }

        public string? Description { get; private set; }

        public ICollection<BusinessBarber> BusinessBarbers { get; private set; } = new List<BusinessBarber>();

        public bool IsActive { get; private set; } = true;

        public Guid UserId { get; private set; }

        private Barber() { }

        public Barber(string fullName, string? phone, string? description, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.");

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            FullName = fullName;
            Phone = phone;
            Description = description;
            UserId = userId;
            IsActive = true;
        }
        public void Update(string fullName, string? phone, string? description)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.");

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            FullName = fullName;
            Phone = phone;
            Description = description;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }

}
