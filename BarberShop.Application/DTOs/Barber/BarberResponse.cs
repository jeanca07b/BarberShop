namespace BarberShop.Application.DTOs.Barber
{
    public class BarberResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
