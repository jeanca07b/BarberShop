using System.ComponentModel.DataAnnotations;

namespace BarberShop.Application.DTOs.Barber
{
    public class CreateBarberRequest
    {
        public string FullName { get; set; } = null!;
        public Guid BusinessId { get; set; }
        public Guid UserId { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}