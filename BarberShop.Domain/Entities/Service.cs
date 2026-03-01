using BarberShop.Domain.Common;

namespace BarberShop.Domain.Entities
{
    public class Service : BaseEntity
    {
        public Guid BusinessId { get; private set; }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public decimal Price { get; private set; }
        public int DurationInMinutes { get; private set; }

        public bool IsActive { get; private set; }

        protected Service() { }

        public Service(Guid businessId, string name, string description, decimal price, int durationInMinutes)
        {
            if (businessId == Guid.Empty)
                throw new ArgumentException("BusinessId is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Service name is required.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (durationInMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            BusinessId = businessId;
            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            DurationInMinutes = durationInMinutes;
            IsActive = true;
        }

        public void Update(string name, string description, decimal price, int durationInMinutes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Service name is required.");

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.");

            if (durationInMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            if (description?.Length > 500)
                throw new ArgumentException("Description cannot exceed 500 characters.");

            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            DurationInMinutes = durationInMinutes;
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