using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.DTOs.Business
{
    public class UpdateBusinessRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Phone { get; set; }

        [Required]
        public string Street { get; set; } = null!;

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        public string? PlaceId { get; set; }
    }
}
