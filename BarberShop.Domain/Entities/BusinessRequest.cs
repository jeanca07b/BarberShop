using BarberShop.Domain.Common;
using BarberShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.Entities
{
    public class BusinessRequest : BaseEntity
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public string Phone { get; private set; } = null!;

        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string PlaceId { get; private set; } = null!;

        public BusinessRequestStatus Status { get; private set; }

        private BusinessRequest() { }

        public BusinessRequest(
            Guid userId,
            string name,
            string? description,
            string phone,
            string street,
            string city,
            string state,
            string country,
            double latitude,
            double longitude,
            string placeId)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Phone = phone;
            Street = street;
            City = city;
            State = state;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
            PlaceId = placeId;

            Status = BusinessRequestStatus.Pending;
        }

        public void Approve() => Status = BusinessRequestStatus.Approved;
        public void Reject() => Status = BusinessRequestStatus.Rejected;
    }
}
