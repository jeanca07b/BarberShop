using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string State { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? PlaceId { get; private set; }

        private Address() { }

        public Address(
            string street,
            string city,
            string state,
            string country,
            double latitude,
            double longitude,
            string? placeId)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
            PlaceId = placeId;
        }
    }
}
